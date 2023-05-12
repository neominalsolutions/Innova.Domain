using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
  public class OrderStatus : Enumeration
  {
    public static OrderStatus Submitted = new(nameof(Submitted), 100);
    public static OrderStatus Paid = new(nameof(Paid), 110);
    public static OrderStatus Shipped = new(nameof(Shipped),120);
    public static OrderStatus Invoiced = new(nameof(Invoiced),130);
    public static OrderStatus Cancelled = new(nameof(Cancelled), 300);
    public OrderStatus(string key, int value) : base(key, value)
    {
    }
  }
}
