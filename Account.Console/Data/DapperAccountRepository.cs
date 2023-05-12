
using Account.Domain.AccountAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data
{
    public class DapperAccountRepository : IAccountRepository
  {
    public Task CreateAsync(Domain.AccountAggregates.Account root)
    {
      throw new NotImplementedException();
    }

    public Task DeleteAsync(string Id)
    {
      throw new NotImplementedException();
    }

    public Task<Domain.AccountAggregates.Account> FindAsync(Expression<Func<Domain.AccountAggregates.Account, bool>> expression)
    {
      throw new NotImplementedException();
    }

    public IQueryable Query(Expression<Func<Domain.AccountAggregates.Account, bool>> expression)
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.AccountAggregates.Account root)
    {
      throw new NotImplementedException();
    }

    public Task<List<Domain.AccountAggregates.Account>> WhereAsync(Expression<Func<Domain.AccountAggregates.Account, bool>> expression)
    {
      throw new NotImplementedException();
    }
  }
}
