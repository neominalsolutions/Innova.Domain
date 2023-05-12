using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.BuyerAggregates
{
  public class Buyer:AggregateRoot
  {
    public string FullName { get; init; }
    public string PhoneNumber { get; set; }


    public IReadOnlyList<Order.OrderAggregates.Order> Orders { get; }

    public Buyer(string id,string fullName, string phoneNumber)
    {
      this.Id = id;
      this.FullName = fullName;
      this.PhoneNumber = phoneNumber;
    }
  }
}
