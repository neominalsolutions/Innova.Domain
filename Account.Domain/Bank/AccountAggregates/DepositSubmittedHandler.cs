using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
    // DepositSubmitted Hangi event notify edilecektir.
    // Handler event consume ederler.
    internal class DepositSubmittedHandler : INotificationHandler<DepositSubmitted>
  {
    private readonly IAccountDomainService accountDomainService;
    private readonly IAccountRepository accountRepository;

    public DepositSubmittedHandler(IAccountDomainService accountDomainService, IAccountRepository accountRepository)
    {
      this.accountDomainService = accountDomainService;
      this.accountRepository = accountRepository;
    }

    public async Task Handle(DepositSubmitted notification, CancellationToken cancellationToken)
    {
      this.accountDomainService.Deposit(notification.Account, notification.Money, notification.channelType);

      // farklı aggregatelere ait farklı durumları içeren kodlar yazılabilir.

      // domain service logic kontrol et.
    }
  }
}
