using System;

namespace Bitcoin.Core.Models.ApiResources
{
    public class LastIncomingModel
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Address { get; set; }
        public int Confirmations { get; set; }

    }
}