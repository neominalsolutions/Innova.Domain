using Account.Console.Data.Contexts;
using Account.Domain.CustomerAggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data
{
  public class EFCustomerRepository : ICustomerRepository
  {
    private readonly BankContext bankContext;

    public EFCustomerRepository(BankContext bankContext)
    {
      this.bankContext = bankContext;
    }
    public Task CreateAsync(Customer root)
    {
      throw new NotImplementedException();
    }

    public Task DeleteAsync(string Id)
    {
      throw new NotImplementedException();
    }

    public async Task<Customer> FindAsync(Expression<Func<Customer, bool>> expression)
    {
      return await this.bankContext.Customers.FirstOrDefaultAsync(expression);
    }

    public IQueryable Query(Expression<Func<Customer, bool>> expression)
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer root)
    {
      throw new NotImplementedException();
    }

    public Task<List<Customer>> WhereAsync(Expression<Func<Customer, bool>> expression)
    {
      throw new NotImplementedException();
    }
  }
}
