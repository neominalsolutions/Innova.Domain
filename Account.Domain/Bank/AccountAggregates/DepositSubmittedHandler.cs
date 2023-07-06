using Account.Domain.Order.OrderAggregates;
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
    private readonly IOrderRepository orderRepository;

    public DepositSubmittedHandler(IAccountDomainService accountDomainService, IAccountRepository accountRepository, IOrderRepository orderRepository)
    {
      this.accountDomainService = accountDomainService;
      this.accountRepository = accountRepository;
      this.orderRepository = orderRepository;
    }

    public async Task Handle(DepositSubmitted notification, CancellationToken cancellationToken)
    {
      this.accountDomainService.Deposit(notification.Account, notification.Money, notification.channelType);

      var o = new Order.OrderAggregates.Order("1");


      await this.orderRepository.CreateAsync(o);
      o.AddItem("Sipariş-1", 10, 12);

     

      // farklı aggregatelere ait farklı durumları içeren kodlar yazılabilir.

      // domain service logic kontrol et.
    }
  }
}
