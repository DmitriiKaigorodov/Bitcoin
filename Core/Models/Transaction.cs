using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Bitcoin.Core.Models
{
    public class Transaction
    {
        [Key]
        public string TxId { get; set; }
        [Required]
        public DateTime TimeReceived { get; set; }
        [Required]
        public int Confirmations { get; set; }
        public virtual ICollection<TransactionDetails> Details { get; set; }
        public bool WasRequested { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Transaction()
        {
            Details = new Collection<TransactionDetails>();
        }
    }
}