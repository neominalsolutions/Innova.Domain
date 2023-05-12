using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
  public class OrderItem:Entity<string>
  {
    public string OrderId { get; init; }
    public string Description { get; init; } // Hizmet
    public decimal ListPrice { get; init; }
    public int Quantity { get; init; }


    public OrderItem()
    {


    }

    public OrderItem(string orderId, string description, decimal listPrice, int quantity)
    {
      Id = Guid.NewGuid().ToString();
      OrderId = orderId;
      Description = description;
      ListPrice = listPrice;
      Quantity = quantity;
    }
  }
}
