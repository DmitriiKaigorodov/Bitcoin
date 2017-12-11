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
            bool saveFailed; 
            do
            {
                saveFailed = false;
                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    var entries = ex.Entries;

                    foreach(var entry in entries)
                    {
                        var entity = entry.Entity;

                        if(entity == null)
                            continue;

                        entry.Reload();
                    }
                }
            }while(saveFailed);
           
        }
    }
}