using Account.Domain.AccountAggregates;
using Account.Domain.CustomerAggregates;
using Account.Domain.Order.BuyerAggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
  // OrderContext Upstream Context
  // BankContext Downstream Context
  // Upstream Context üzerinden yapılan işlemlerde Downstream Contextleri bozmadan oradaki işleyişi bozmadan işlem yapallıyız. IAccountDomainService
  public class OrderSubmittedHandler : INotificationHandler<OrderSubmitted>
  {
    private readonly IAccountRepository accountRepository;
    private readonly IAccountDomainService accountDomainService;
    private readonly ICustomerRepository customerRepository;
    private readonly IBuyerRepository buyerRepository;


    public OrderSubmittedHandler(IAccountRepository accountRepository, IAccountDomainService accountDomainService, ICustomerRepository customerRepository, IBuyerRepository buyerRepository)
    {
      this.accountRepository = accountRepository;
      this.accountDomainService = accountDomainService;
      this.customerRepository = customerRepository;
      this.buyerRepository = buyerRepository;
    }

    public async Task Handle(OrderSubmitted notification, CancellationToken cancellationToken)
    {

      var customer = await this.customerRepository.FindAsync(x => x.Id == notification.CustomerId);
      var buyer = await this.buyerRepository.FindAsync(x => x.Id == notification.CustomerId);

     
      if(customer is null)
      {
        throw new Exception("Böyle bir müşteri hesabı yoktur");
      }

      if(buyer is null)
      {
        // Buyer aggregate üzerinden buyer nesnesini değiştirdik.
        await buyerRepository.CreateAsync(new Buyer(customer.Id, customer.FullName, customer.PhoneNumber));
      }

   
      var acc = await this.accountRepository.FindAsync(x => x.AccountNumber == notification.AccountNumber);
      // hesabımdan şu kadarlık bir harcama tutarı düş.
      // Bank Context ile OrderContext birbirleri ile Domain Event vasıtası ile haberleşiyor.
      // iki farklı context'in birbileri haberleşme noktalarına Context Mapping diyoruz.

      //acc.Balance -= new Money(notification.TotalAmount, notification.Currecy);
      //acc.Transactions.Add(new AccountTransaction());

      acc.WithDraw(new Money(notification.TotalAmount, notification.Currecy), AccountTransactionChannelType.Online,this.accountDomainService);

      

    }
  }
}
