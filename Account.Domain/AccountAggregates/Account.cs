using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  public class Account:AggregateRoot
  {
    public string AccountNumber { get; init; }
    public string IBAN { get; init; } // Bunlar nesne init olduğunda daha önceden tanımlı olması gereken değerler

    public string CustomerId { get; init; } // Bu hesabı başka müşteriye vermeyiz hesabı kapatmamız lazım

    // Customer Credits
    // Customer Aggregate ayrıca açmamızın sebebi müşteri kredi kullanmak için başvuruda bulunabilir. bu kredi başvuru işlemi müşteriye ait bir işlem olduğundan customer aggregate içerisinde kontrol edilmelidir. Müşterinin kredisi onaylanırsa bu durumda herhangi bir hesabına kredi tutarı yatırılmalıdır. Bu durumda müşteri aggregate ile account aggregate haberleşmesi gerekir. Bunu da Domain event ile yapması gerekir. Aggregatelerin bozulmamsı için bu önemlidir.

    public Money Balance { get; private set; } // Bakiye - 500 TL, Kendi sınıfı içerisinde Bakiye güncellenecektir. private set


    public bool IsClosed { get; private set; } // Müşterinin hesabının kapalı olup olmadığı
    public string CloseReason { get; private set; }


    /// <summary>
    /// Hesap kapatma işlemi
    /// </summary>
    public void Close(string closeReason)
    {
       // hesabı kapaması için bakiyesi - olmamalıdır sıfırdan büyük olmalıdır

      if(Balance <= Money.Zero(Balance.Currency))
      {
        throw new Exception("Hesabı kapatmak için bakiyenizin eksi olamaz");
      }

      IsClosed = true;
      CloseReason = closeReason;

      var @event = new AccountClosed(AccountNumber,closeReason,CustomerId);
      AddDomainEvent(@event);

      // Eğer hesap kapama işlemi ile alakalı başka aggregatelere ait bir durum söz konusu ise addDomain event ile ilgili eventi yaz fırlat
      // AddDomainEvent();

    }


  }
}
