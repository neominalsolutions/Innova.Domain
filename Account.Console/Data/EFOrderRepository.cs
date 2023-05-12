using Account.Console.Data.Contexts;
using Account.Domain.Order.OrderAggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data
{
  public class EFOrderRepository : IOrderRepository
  {

    private readonly OrderContext db;

    public EFOrderRepository(OrderContext db)
    {
      this.db = db;
    }



    public async Task CreateAsync(Order root)
    {
      await db.Orders.AddAsync(root);
    }

    public Task DeleteAsync(string Id)
    {
      throw new NotImplementedException();
    }

    public async Task<Order> FindAsync(Expression<Func<Order, bool>> expression)
    {
      return await db.Orders.Include(x=> x.Items).FirstOrDefaultAsync(expression);
    }

    public IQueryable Query(Expression<Func<Order, bool>> expression)
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(Order root)
    {
      throw new NotImplementedException();
    }

    public Task<List<Order>> WhereAsync(Expression<Func<Order, bool>> expression)
    {
      throw new NotImplementedException();
    }
  }
}
