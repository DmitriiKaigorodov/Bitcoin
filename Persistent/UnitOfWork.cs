using System.Linq;
using System.Threading.Tasks;
using Bitcoin.Core;
using Bitcoin.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bitcoin.Persistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BitcoinDbContext dbContext;
        public UnitOfWork(BitcoinDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public async Task SaveChanges()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var conflictedObject = ex.Entries.Single().Entity;
                throw new ConcurrencyException(conflictedObject);
            }
           
        }
    }
}