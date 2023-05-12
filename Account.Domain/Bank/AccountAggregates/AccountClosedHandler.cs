using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  public class AccountClosedHandler : INotificationHandler<AccountClosed>
  {
    // müşteriye sms ve email ile bilgilendirme yaparız. hesap kapanışı ile ilgili.
    public Task Handle(AccountClosed notification, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
