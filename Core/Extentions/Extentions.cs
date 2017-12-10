using System.Collections.Generic;
using System.Linq;
using Bitcoin.Core.Models;

namespace Bitcoin.Core.Extentions
{
    public static class Extentions
    {
        public static void MarkAsRequested(this IEnumerable<Transaction> transactions)
        {
            transactions.ToList().ForEach( t => t.WasRequested = true);
        }
    }
}