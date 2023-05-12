using Account.Console.Data.Contexts;
using Account.Domain.Order.BuyerAggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data
{
  public class EFBuyerRepository : IBuyerRepository
  {
    private readonly OrderContext orderContext;

    public EFBuyerRepository(OrderContext orderContext)
    {
      this.orderContext = orderContext;
    }
    public async Task CreateAsync(Buyer root)
    {
      await orderContext.Buyers.AddAsync(root);
    }

    public Task DeleteAsync(string Id)
    {
      throw new NotImplementedException();
    }

    public async Task<Buyer> FindAsync(Expression<Func<Buyer, bool>> expression)
    {
      return await this.orderContext.Buyers.FirstOrDefaultAsync(expression);
    }

    public IQueryable Query(Expression<Func<Buyer, bool>> expression)
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(Buyer root)
    {
      throw new NotImplementedException();
    }

    public Task<List<Buyer>> WhereAsync(Expression<Func<Buyer, bool>> expression)
    {
      throw new NotImplementedException();
    }
  }
}
