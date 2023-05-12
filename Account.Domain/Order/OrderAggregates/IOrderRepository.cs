using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
 
  public interface IOrderRepository : IRepository<Order>
  {
  }
}
