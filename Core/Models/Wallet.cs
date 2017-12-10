using System.ComponentModel.DataAnnotations;

namespace Bitcoin.Core.Models
{
    public class Wallet
    {
        public int Id { get; set; }     
        [Required]
        public double Balance { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}