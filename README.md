# Bitcoin
## Configure Application Settings
In **appsettings.json** find section with name **BitcoinDaemon** and specify your bitcoind URL, username and password in appropriate    fields. Also you have to specify connection string to your database in **ConnectionStrings** section.

## Configure bitcoind
Open your **bitcoin.conf** and add or change next settings:
```
walletnotify=curl -H "Authorization: Basic %CredentialsInBase64%" %YourUrl%/api/walletnotify/%s
blocknotify=curl -H "Authorization: Basic %CredentialsInBase64%" %YourUrl%/api/blocknotify/%s
```
Where: 
 * **%CredentialsInBase64%** - credentials for RPC access to bitcoind encoded in base64 in next format: **%username%:%password%**
 * **%YourUrl%** - URL for your bitcoin daemon
 
 Ofcourse, you need curl for it. You can download it [here](https://curl.haxx.se/dlwiz/)
 
 ## EndPoints Description
 
 ### /api/sendbtc
 **Method**: POST
 
 **Request body example**
 ```json
 {
    "address": "2N8hwP1WmJrFF5QWABn38y63uYLhnJYJYTF",
    "amount": 0.05
 }
 ```
 
 ***Response***
 Success: TxId of your transaction
 Fail: Error code and description 
 
 ### /api/getlast
 **Method**: GET
 
 ***Response***
 Returns information about last incoming payments
 
