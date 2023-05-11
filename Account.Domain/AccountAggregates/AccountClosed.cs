using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  public class AccountClosed:INotification
  {
    public AccountClosed(string accountNumber, string closeReason, string customerId)
    {
      AccountNumber = accountNumber;
      CloseReason = closeReason;
      CustomerId = customerId;
    }

    public string AccountNumber { get; init; }
    public string CloseReason { get; init; }
    public string CustomerId { get; init; }

 


  }
}
