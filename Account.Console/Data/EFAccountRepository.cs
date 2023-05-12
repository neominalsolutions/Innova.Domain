using Account.Console.Data.Contexts;
using Account.Domain.AccountAggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data
{
    public class EFAccountRepository : IAccountRepository
  {
    private readonly BankContext bankContext;


    public EFAccountRepository(BankContext bankContext)
    {
      this.bankContext = bankContext;
    }

    public async Task CreateAsync(Domain.AccountAggregates.Account root)
    {
      await this.bankContext.Accounts.AddAsync(root);
    }

    public async Task DeleteAsync(string Id)
    {
      var rootEntity = await this.bankContext.Accounts.FindAsync(Id);

      if (rootEntity is null)
        throw new Exception("Entity Not Found");

      this.bankContext.Accounts.Remove(rootEntity);
    }

    public Task<Domain.AccountAggregates.Account> FindAsync(Expression<Func<Domain.AccountAggregates.Account, bool>> expression)
    {
      return this.bankContext.Accounts.Include(x => x.Transactions).Where(expression).FirstOrDefaultAsync();
    }

    public IQueryable Query(Expression<Func<Domain.AccountAggregates.Account, bool>> expression)
    {
      return this.bankContext.Accounts.Where(expression).AsNoTracking().AsQueryable();
    }

    public async Task UpdateAsync(Domain.AccountAggregates.Account root)
    {
      //this.bankContext.Attach(root);
      // asNoTracking neden hata veriyor Attach yapmamıza rağmen
      this.bankContext.Accounts.Update(root);
    }

    public async Task<List<Domain.AccountAggregates.Account>> WhereAsync(Expression<Func<Domain.AccountAggregates.Account, bool>> expression)
    {
      return await this.bankContext.Accounts.Where(expression).ToListAsync();
    }
  }
}
