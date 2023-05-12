// See https://aka.ms/new-console-template for more information
using Account.Console.Application;
using Account.Console.Data;
using Account.Console.Data.Contexts;
using Account.Console.Dto;
using Account.Domain.AccountAggregates;
using Account.Domain.CustomerAggregates;
using Account.Domain.Order.BuyerAggregates;
using Account.Domain.Order.OrderAggregates;
using Account.Domain.SeedWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Console.WriteLine("Hello, World!");


#region Test

//Account.Domain.AccountAggregates.Account acc = new Account.Domain.AccountAggregates.Account();
//acc.Transactions.Add(new AccountTransaction())
//acc.IsBlocked = true;
//acc.BlockReason = "iptal";
//acc.Block("hesap geçişi");
//acc.Close("hesap kapanış");

//var service = new AccountDomainService(new EFAccountRepository());
//acc.Deposit(new Money(5000,"TL"),AccountTransactionChannelType.ATM, service);
//var repo = new EFAccountRepository();

//try
//{
//  var depositUseCase = new DepositService(repo, service);
//  var depositDto = new AccountDepositDto();
//  var response = await depositUseCase.HandleAsync(depositDto); // iş başladı application katmanında 
//                                                        // domain katmanında saveChanges yapmak yerine applicationKatmanında saveChanges() işi bitiririz.
//}
//catch (Exception ex)
//{

//  Console.WriteLine(ex.Message);
//}



//acc.Block("Blocked");

// application layer
// use-case
//var repo = AccountRepository();
//repo.saveChanges();


//service.Deposit()


//Location l1 = new Location(23.563, 24.789);
//// l1.Lat = 5;

//Location l2 = new Location(23.564, 24.786);

//Console.WriteLine("l1",l1.ToString());
//Console.WriteLine("l2", l2.ToString());


//var r1 = Location.Equals(l1, l2); // l1 ile l2 aynı değerlere mi eşit true
//var r2 = Location.ReferenceEquals(l1, l2); // aynı class referans mı false


//Console.WriteLine($"eşit mi {l1.Equals(l2)}");

#endregion


public class Program
{

  public static void DepositAndWithDraw(string[] args)
  {
    var host = CreateHostBuilder(args).Build();
    // resolve edicez
    var accountRepo = host.Services.GetRequiredService<IAccountRepository>(); // DapperAccountRepository
    // intance uygulama aldı
    // service locator.
    var accountDomainService = host.Services.GetRequiredService<IAccountDomainService>(); // AccountDomainService
    var mediator = host.Services.GetRequiredService<IMediator>();

    var bankContext = host.Services.GetRequiredService<BankContext>();

    try
    {
      // 1.test case 30000 tl üzerinden atmden para yatırılamaz
      // var acc = new Account.Domain.AccountAggregates.Account("111-222-333-444", "TR 111-222-333-444-555", "1");
      // acc.Deposit(new Money(31000, "TL"), AccountTransactionChannelType.ATM, accountDomainService);

      // 2 test case 10000 tl online atmden para yatırılamaz
      // acc.Deposit(new Money(101000, "TL"), AccountTransactionChannelType.Online, accountDomainService);

      // 3. test case hafta sonu para gönderme
      // acc.Deposit(new Money(6000, "TL"), AccountTransactionChannelType.Online, accountDomainService);

      //var acc2 = new Account.Domain.AccountAggregates.Account("111-222-333-444", "TR 111-222-333-444-555", "1", true); // bloklanmış hesap sadece Banka üzerindne yatabilir
      //acc2.Deposit(new Money(6000, "TL"), AccountTransactionChannelType.Online, accountDomainService);

      // var acc2 = new Account.Domain.AccountAggregates.Account("111-222-333-444", "TR 111-222-333-444-555", "1", false,true); // closed account 
      // acc2.Deposit(new Money(6000, "TL"), AccountTransactionChannelType.Bank, accountDomainService);

      // hesap kapama işlemi
      // var acc3 = new Account.Domain.AccountAggregates.Account("111-222-333-444", "TR 111-222-333-444-555", "1");
      // domain service method parametres olarak çağırma double dipatch yöntemi
      //acc3.Deposit(new Money(600, "TL"), AccountTransactionChannelType.Bank, accountDomainService);
      //acc3.Close("Keyfi");
      // closed account 
      // acc2.Deposit(new Money(6000, "TL"), AccountTransactionChannelType.Bank, accountDomainService);

      // event bazlı domain service çağırma yöntemi, domain event
      //acc3.Deposit(new Money(36000, "TL"), AccountTransactionChannelType.ATM);

      // foreach ile dönüp nesne'in instance ne kadar event varsa hepsini fırlatabiliriz.
      // mediator.Publish(acc3.DomainEvents[0]);
      // Eventi yayımlamak için kullanılıyoruz. mediator üzerinden event fırlatma yapıyoruz.
      // eventin hangi handler'ı tetikleyeceğini kendisi bulacak.

      // repo.saveChanges();

    }
    catch (Exception ex)
    {

      Console.WriteLine(ex.Message);
    }

    // EF ile veri tabanı işlemi

    try
    {
      //var acc = new Account.Domain.AccountAggregates.Account("555-222-333-444", "TR 555-222-333-444", "1");
      //accountRepo.CreateAsync(acc).GetAwaiter().GetResult();


      var acc = accountRepo.FindAsync(x => x.AccountNumber == "111-222-333-444").GetAwaiter().GetResult();
      // acc.Deposit(new Money(35000, "TL"), AccountTransactionChannelType.ATM, accountDomainService); 1.yöntem Double Dispatch Yöntemi

      // acc.Deposit(new Money(200, "TL"), AccountTransactionChannelType.Online);  Domain Event Yöntemi

      // ATM maksimum 30.000 case
      acc.Deposit(new Money(50000, "TL"), AccountTransactionChannelType.ATM);

      accountRepo.UpdateAsync(acc).GetAwaiter().GetResult(); // state değiştirdik.
      int result = bankContext.SaveChangesAsync().GetAwaiter().GetResult();



    }
    catch (Exception ex)
    {

      Console.WriteLine(ex.Message);
    }


  }

  public static void OrderSubmit(string[] args)
  {
    var host = CreateHostBuilder(args).Build();
    // resolve edicez
    var accountRepo = host.Services.GetRequiredService<IAccountRepository>(); // DapperAccountRepository
    // intance uygulama aldı
    // service locator.
    var accountDomainService = host.Services.GetRequiredService<IAccountDomainService>(); // AccountDomainService
    var mediator = host.Services.GetRequiredService<IMediator>();

    var bankContext = host.Services.GetRequiredService<BankContext>();
    var orderContext = host.Services.GetRequiredService<OrderContext>();

    var orderRepo = host.Services.GetRequiredService<IOrderRepository>();

    //var transaction = orderContext.Database.BeginTransaction(); // iş süreci buradan başlıyor. çünkü siparişi başlatıığımız nokta burası ilk transaction burada
    //bankContext.Database.UseTransaction(transaction.GetDbTransaction()); // EF ile transaction share yap.

    try
    {

      

      var acc = accountRepo.FindAsync(x => x.AccountNumber == "111-222-333-444").GetAwaiter().GetResult();

      var order = new Order(acc.CustomerId);
      var items = new List<OrderItem>();
      items.Add(new OrderItem(order.Id, "Hizmet-1", 30, 1));
      items.Add(new OrderItem(order.Id, "Hizmet-2", 50, 2));
      items.Add(new OrderItem(order.Id, "Hizmet-3", 80, 4));
      order.SubmitOrder(acc, items, new ShipAddress("IST", "TR", "üsküdar"));
      // WithDraw
      // Deposit
      // SubmitOrder entity deki state değişir.


      // yani entity framework change tracker ile entity state değişimleri yakalansın diye create,update,delete crud işlemlerini tetikliyoruz.
      orderRepo.CreateAsync(order).GetAwaiter().GetResult(); // OrderContext
      accountRepo.UpdateAsync(acc).GetAwaiter().GetResult(); // BankContext

      //transaction.Commit();


      // kendi başına single transaction sağlıyor.
      // işlem nereden başlarsa ilgili context gitmemiz lazım.
      orderContext.SaveChangesAsync().GetAwaiter().GetResult();
      bankContext.SaveChangesAsync().GetAwaiter().GetResult();
      


    }
    catch (Exception ex)
    {
      //transaction.Rollback();
      Console.WriteLine(ex.Message);
    }

    




  }

  public static void Main(string[] args)
  {

    // DepositAndWithDraw(args);

    OrderSubmit(args);


  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
          services.AddDbContext<BankContext>();
          services.AddDbContext<OrderContext>();
          // IoC Container
          // register edildi.
          services.AddScoped<IAccountRepository, EFAccountRepository>();
          services.AddScoped<IAccountDomainService, AccountDomainService>();
          services.AddScoped<IOrderRepository, EFOrderRepository>();
          services.AddScoped<ICustomerRepository, EFCustomerRepository>();
          services.AddScoped<IBuyerRepository, EFBuyerRepository>();
          services.AddMediatR(opt =>
          {
            opt.RegisterServicesFromAssemblyContaining<Account.Domain.AccountAggregates.Account>();
          });
          // remove the hosted service
          // services.AddHostedService<Worker>();

          // register your services here.
        });
}


