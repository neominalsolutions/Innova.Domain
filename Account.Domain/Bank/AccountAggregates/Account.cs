using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  public class Account : AggregateRoot
  {
    public string AccountNumber { get; init; }
    public string IBAN { get; init; } // Bunlar nesne init olduğunda daha önceden tanımlı olması gereken değerler

    public string CustomerId { get; init; } // Bu hesabı başka müşteriye vermeyiz hesabı kapatmamız lazım

    // Customer Credits
    // Customer Aggregate ayrıca açmamızın sebebi müşteri kredi kullanmak için başvuruda bulunabilir. bu kredi başvuru işlemi müşteriye ait bir işlem olduğundan customer aggregate içerisinde kontrol edilmelidir. Müşterinin kredisi onaylanırsa bu durumda herhangi bir hesabına kredi tutarı yatırılmalıdır. Bu durumda müşteri aggregate ile account aggregate haberleşmesi gerekir. Bunu da Domain event ile yapması gerekir. Aggregatelerin bozulmamsı için bu önemlidir.

    public Money Balance { get; private set; } // Bakiye - 500 TL, Kendi sınıfı içerisinde Bakiye güncellenecektir. private set


    public bool IsClosed { get; private set; } // Müşterinin hesabının kapalı olup olmadığı
    public string? CloseReason { get; private set; }

    public bool IsBlocked { get; private set; } // Hesap bloke edildiğinde para çekme ve para gönderme işlemleri yapılmasın
    public string? BlockReason { get; private set; }

    /// <summary>
    /// Deposit ve WithDraw işlemleri olduğunda sadece account transaction'a append only kayıt düşücez.
    /// </summary>

    private List<AccountTransaction> _transactions = new List<AccountTransaction>();
    public IReadOnlyList<AccountTransaction> Transactions => _transactions;


    public Account()
    {

    }

    public Account(string accountNumber, string iban, string customerId)
    {
      Id = Guid.NewGuid().ToString();
      AccountNumber = accountNumber;
      IBAN = iban;
      CustomerId = customerId;
      Balance = Money.Zero("TL");
    }

    public Account(string accountNumber, string iban, string customerId, bool isBlocked)
    {
      Id = Guid.NewGuid().ToString();
      AccountNumber = accountNumber;
      IBAN = iban;
      CustomerId = customerId;
      Balance = Money.Zero("TL");
      IsBlocked = isBlocked;
    }

    public Account(string accountNumber, string iban, string customerId, bool isBlocked, bool isClosed)
    {
      Id = Guid.NewGuid().ToString();
      AccountNumber = accountNumber;
      IBAN = iban;
      CustomerId = customerId;
      Balance = Money.Zero("TL");
      IsBlocked = isBlocked;
      IsClosed = isClosed;
    }


    public void Block(string blockReason)
    {
      BlockReason = blockReason;
      IsBlocked = true;
    }


    //public void Deposit(Money money, AccountTransactionChannelType transactionChannelType)
    //{
    //  Balance += money;
    //}





    /// <summary>
    /// Para yatırma işlemi
    /// DIP prensimi ile Account Entity ile AccountDomain Service birbirine zayıf bağlı yapmak için araya bir interface IAccountDomainService interface koyduk
    /// Bir entity içerisinde bir servisin bussiness kurallar için yeterli olmadığı durumlarda methoda ilgili domain service dependcy injection yöntemi ile paramtre olarak geçirilir. bu durumda çift taraflı bir kontrol mekanizması olduğu için bu yönteme double dispatch çift taraflı sevk adı verilir. Peki double dispath yapmadan nasıl bu işlemi yapabiliriz.
    /// </summary>
    public void Deposit(Money money, AccountTransactionChannelType transactionChannelType, IAccountDomainService accountDomainService)
    {
      // önce kuralları kontrol etmek için domain service çağıralım.
      // eğer ki aşağıdaki kural setlerinden geçebilir exception almaz isek bu durumda Balance değeri artsın.
      Balance += money;
      accountDomainService.Deposit(this, money, transactionChannelType);


      // farklı bir aggregate üzerinden işlem yapmak istersek bu durumda eventi domain service kodu çalıştıktan sonra çağıralımç


      // deposit işlemi sonrında işlem transaction kaydı girdik account nesnesi update olurken account transaction append only olarak eklenecek.
      _transactions.Add(new AccountTransaction(Id, money, AccountTransactionType.Deposit, transactionChannelType));

      // bakiye artırma işlemi, entity state değiştirme işlemi bu sınıf entity sınıfı içinde yapılıyor. entity state dışında bu state bozucak durumları ise domain serviceler ile koruma altına alabilir. Domain serviceler stateless tanımlanmalıdır entity ait herhangi bir state değişimin domain service içerisinde yapmayız.

      // para yatırma işlemindeki logicler eğer yapabiliyorsa entity içerisinde yaparız.
      // fakat entity içerisinde yapamadığımızı durumlarda olabilir. entity değerlerinin db üzerinden bulunması gibi db bazlı operasyonları burada sınıfların içerisinde yapmamalıyız.
      // belirli logicler uygulanacak
    }


    /// <summary>
    /// Domain Event yöntemi ile bussiness rules check ve aggregate haberleşmesi
    /// </summary>
    /// <param name="money"></param>
    /// <param name="transactionChannelType"></param>
    public void Deposit(Money money, AccountTransactionChannelType transactionChannelType)
    {
      // önce kuralları kontrol etmek için domain service çağıralım.
      // eğer ki aşağıdaki kural setlerinden geçebilir exception almaz isek bu durumda Balance değeri artsın.
      Balance += money;

      // domain kurallarını kontrol etmeyi domain evente bıraktık bu sayede account aggregate ile account aggregate dışındaki başka aggregateler ile işlem yapma fırsatı buluyoruz.
      var @event = new DepositSubmitted(this, money, transactionChannelType);
      AddDomainEvent(@event);


      _transactions.Add(new AccountTransaction(Id, money, AccountTransactionType.Deposit, transactionChannelType));
    }



    public void WithDraw(Money money, AccountTransactionChannelType transactionChannelType, IAccountDomainService accountDomainService)
    {
      Balance -= money;

      accountDomainService.WithDraw(this, money, transactionChannelType);

      _transactions.Add(new AccountTransaction(Id, money, AccountTransactionType.WithDraw, transactionChannelType));
    }



    //public async void Deposit(Money money, AccountTransactionChannelType transactionChannelType, IAccountRepository accountRepository)
    //{

    //  var account2 = await accountRepository.FindAsync(x => x.Id == Id);

    //  Balance += money; 
    //}


    /// <summary>
    /// Hesap kapatma işlemi
    /// </summary>
    public void Close(string closeReason)
    {
      // hesabı kapaması için bakiyesi - olmamalıdır sıfırdan büyük olmalıdır

      if (Balance <= Money.Zero(Balance.Currency))
      {
        throw new Exception("Hesabı kapatmak için bakiyenizin eksi olamaz");
      }

      IsClosed = true;
      CloseReason = closeReason;

      var @event = new AccountClosed(AccountNumber, closeReason, CustomerId);
      AddDomainEvent(@event);

      // Eğer hesap kapama işlemi ile alakalı başka aggregatelere ait bir durum söz konusu ise addDomain event ile ilgili eventi yaz fırlat
      // AddDomainEvent();

    }

    /// <summary>
    /// Hesabı kapalı iken banka kanalı ile ödeme yapıp hesabı açmak istediğimizde burayı çağırırız.
    /// </summary>
    public void Open()
    {
      if (IsBlocked || IsClosed)
      {
        if (Balance > Money.Zero("TL"))
        {
          IsBlocked = false;
          IsClosed = false;
        }
      }
    }
  }
}
