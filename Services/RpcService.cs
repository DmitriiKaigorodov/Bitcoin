using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bitcoin.Core.Exceptions;
using Bitcoin.Core.Models.Rpc;
using Bitcoin.Core.Services;
using Bitcoin.Core.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Bitcoin.Services
{
    public class RpcService : IRpcService
    {
        private BitcoinDaemonSettings BitcoinDaemonSettings { get; set; }

        public RpcService(IOptionsSnapshot<BitcoinDaemonSettings> optionSnapshot)
        {
            BitcoinDaemonSettings = optionSnapshot.Value;
        }
        public async Task<RpcResponse<T>> SendRequest<T>(RpcRequest request)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(BitcoinDaemonSettings.ServerUrl);
            string authorizationHeader = GenerateBasicAuthHeader();
            webRequest.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            webRequest.Credentials = new NetworkCredential(BitcoinDaemonSettings.RpcUsername, BitcoinDaemonSettings.RpcPassword);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";          
            var bytes = request.ToByteArray();
            webRequest.ContentLength = bytes.Length;
            using (var requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                using(var webResponse = await webRequest.GetResponseAsync())
                {
                    return MakeRpcResponse<T>(webResponse);
                }
            }
            catch(WebException webException)
            { 
                var daemonResponse = webException.Response as HttpWebResponse;

                if(daemonResponse.StatusCode == HttpStatusCode.Unauthorized) 
                {
                    throw new UnauthorizedAccessException("Can not authorize in bitcoin server. Please contact your system administrator"
                    , webException); 
                }           
                return MakeRpcResponse<T>(webException.Response);
            }


          
        }

        private RpcResponse<T> MakeRpcResponse<T>(WebResponse webResponse)
        {
            using(var responseStream = webResponse.GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream))
                {
                    var result = reader.ReadToEnd();                      
                    var response =  JsonConvert.DeserializeObject<RpcResponse<T>>(result); 

                    if(response.Error != null)
                    {
                        throw  new RpcErrorException(response.Error); 
                    }                     
                    return response;             
                }                  
            }
        }
        private string GenerateBasicAuthHeader()
        {
            string authInfo = $"{BitcoinDaemonSettings.RpcUsername}:{BitcoinDaemonSettings.RpcPassword}";
            string authHeaderData = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            return "Basic " + authHeaderData;
        }

    }
}