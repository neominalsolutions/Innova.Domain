using Account.Domain.AccountAggregates.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  // Dış kaynaklara bağlanarak yada dış context veya aggregateler üzerinden bir veri çekip buna göre bir bussiness rule olduğu durumlarda muhteşem bir kolaylık sağlar.
  public class AccountDomainService : IAccountDomainService
  {
    private readonly IAccountRepository accountRepository;


    public AccountDomainService(IAccountRepository accountRepository)
    {
      this.accountRepository = accountRepository;
    }
    
    /// <summary>
    /// Bussiness Case Rules
    /// Hesap blokeli ise Banka kanalı dışından para yatırıamaz \n
    /// Hesap kapalı ise Banka kanalı üzerinden para yatırılamaz \n
    /// Hafta sonları maksimum para transfer limiti 5000 TL dir.
    /// Günlük ATM den para maksimum yatırma limiti 30.000 TL
    /// Günlük Online para yatırma limit 100.000 TL
    /// </summary>
    /// <param name="acc"></param>
    /// <param name="money"></param>
    /// <param name="channelType"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Deposit(Account acc, Money money, AccountTransactionChannelType channelType)
    {
      var today = DateTime.Now;
      // acc şuan üzerinde işlem yapılan account

      if(channelType == AccountTransactionChannelType.Bank)
      {
        if(acc.Balance > Money.Zero("TL"))
        { 
          acc.Open();
        }
      }

 
      if((acc.IsBlocked || acc.IsClosed) && (channelType == AccountTransactionChannelType.ATM || channelType == AccountTransactionChannelType.Online))
      {
        throw new Exception("Kapalı yada blokeli hesaba para yatırılamaz. Lütfen Banka kanalını kullanın");
      }


      if(money > new Money(30000,"TL") && channelType == AccountTransactionChannelType.ATM)
      {
        // TranferLimitException
        throw new TranferLimitException("Günlük ATM den para transfer limiti 30.000 TL'dir");
        // uygulama kodları hatadan dolayı kesilir ve işlem gerçekleşmeyecektir.
      }

      if(money > new Money(10000,"TL") && channelType == AccountTransactionChannelType.Online)
      {   // TranferLimitException
        throw new Exception("Günlük Online para transfer limiti 100.000 TL'dir");
      }

      // not gün içerisinde farklı işlemler üzerinden toplamda 5000 tl'nin üzerinde bir para yatırma yapılmamalı. aşağıdaki kod blogu tek bir işlemde 5000 TL üzerini kontrol eder.
      if(today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
      {
        if(money > new Money(5000, "TL"))
        {
          // TranferLimitException
          throw new Exception("Hafta sonu maksimum 5000 TL para yatırabilirsiniz");
        }
      }

      // domain servicelerde sınıfların stateleri ile ilgili bir değişiklik yapmıyoruz. stateless çalışıyoruz.
      // acc.CustomerId = "324324";

      // service içerisinde state düzgün bir şekilde değişiyor ise son olarak repoya gidip update edebiliriz.
      // yani burada statless çalıştığımız için state değişen nesnelerin son güncel hallerini application katmanı üzerinden koorine ederek repository gönderiyoruz.
      // this.accountRepository.UpdateAsync(acc).GetAwaiter().GetResult();

      // var customer = customerRepo.find(acc.CustomerId);
      // customer.addCreditPoint(5);
      // şuan state değişen 2 nesne var AccountTransaction (Added), Account (Modified), CustomerCredits(Modified)
     // musterinin kredi puanına ekstradan bir değer gir append et.

    }

    public void WithDraw(Account acc, Money money, AccountTransactionChannelType channelType)
    {
      
      if(acc.IsBlocked || acc.IsClosed)
      {
        throw new Exception("Blokeli yada kapalı hesaptan para çekilemez");
      }

      // atmden günlük 5000 TL limit var
      if(channelType == AccountTransactionChannelType.ATM && money > new Money(5000, money.Currency))
      {
        throw new Exception("Günlük ATM para para çekme limit 5000 TL'dir");
      }
      else if(channelType == AccountTransactionChannelType.Online && money > new Money(100000, money.Currency))
      {
        throw new Exception("Günlük Online para çekme limit 100,000 TL'dir");
      }

      if(acc.Balance < Money.Zero(money.Currency))
      {
        // hesabımdaki bakiye bi işlemi yapmak için yetersiz ise
        throw new Exception("Hesap bakiyesi yeterli değil");
      } 


    }
  }
}
