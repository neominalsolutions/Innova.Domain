using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
  public class OrderSubmitted:INotification
  {
    public decimal TotalAmount { get; init; }
    public string AccountNumber { get; init; }

    public string Currecy { get; init; }

    public string CustomerId { get; set; }



    public OrderSubmitted(decimal totalAmount, string accountNumber, string currency, string customerId)
    {
      this.TotalAmount = totalAmount;
      this.AccountNumber = accountNumber;
      this.Currecy = currency;
      this.CustomerId = customerId;
    }
  }
}
