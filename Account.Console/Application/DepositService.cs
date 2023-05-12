using Account.Console.Dto;
using Account.Domain.AccountAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Application
{

    public class DepositService : IDepositService<AccountDepositDto, string>
  {

    private readonly IAccountRepository accountRepository;
    private readonly IAccountDomainService accountDomainService;

    public DepositService(IAccountRepository accountRepository, IAccountDomainService accountDomainService)
    {
      this.accountRepository = accountRepository;
      this.accountDomainService = accountDomainService;
    }

    public async Task<string> HandleAsync(AccountDepositDto request)
    {

      var acc = await accountRepository.FindAsync(x => x.AccountNumber == request.AccountNumber);

      // stateless çalışır

      // domain service ile aggergate root deposit işlemi için ayrı ayrı çağırıldığında anlam karmaşası oluşabilir. bu durum application katmanını yazan geliştirici yanlışlıklar domain service deposit methodunu çağırmadan bussiness kurallarsın account deposit methodunu çalıştırabilir. Bu sebeple entity içerisinde domain servicelerin domain e hizmet edecek şekilde çalıştırılmasını tavsiye ediyoruz.

      //if (AccountTransactionChannelType.ATM.Key == request.Channel)
      //{
      //  accountDomainService.Deposit(acc, new Money(request.Amount, request.Currency), AccountTransactionChannelType.ATM);

      //  acc.Deposit(new Money(request.Amount, request.Currency), AccountTransactionChannelType.ATM);
      //}

      // Double Dispatch yöntemi diyoruz.
      acc.Deposit(new Money(request.Amount, request.Currency), AccountTransactionChannelType.ATM, accountDomainService);


      await accountRepository.UpdateAsync(acc);

      // Customer Repo bağlan başka bir iş yap vs.

      // SaveChanges(); veri tabanında artık kaç entity varsa hepsini yansıt

      return acc.Id;


      throw new NotImplementedException();
    }

    /// <summary>
    ///  Use Case Deposit iş akışını başlatan ve koordine eden diğer katmalar ile haberleşerek koordineli bir kod yazmanızı sağlayan bir katman, validasyon, repository, automapper
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

  }
}
