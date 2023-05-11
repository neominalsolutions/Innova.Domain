// See https://aka.ms/new-console-template for more information
using Account.Console.Application;
using Account.Console.Data;
using Account.Console.Dto;
using Account.Domain.AccountAggregates;
using Account.Domain.SeedWorks;

Console.WriteLine("Hello, World!");


Account.Domain.AccountAggregates.Account acc = new Account.Domain.AccountAggregates.Account();
//acc.Transactions.Add(new AccountTransaction())
//acc.IsBlocked = true;
//acc.BlockReason = "iptal";
acc.Block("hesap geçişi");
acc.Close("hesap kapanış");

var service = new AccountDomainService(new EFAccountRepository());
acc.Deposit(new Money(5000,"TL"),AccountTransactionChannelType.ATM, service);
var repo = new EFAccountRepository();

try
{
  var depositUseCase = new DepositService(repo, service);
  var depositDto = new AccountDepositDto();
  var response = await depositUseCase.HandleAsync(depositDto); // iş başladı application katmanında 
                                                        // domain katmanında saveChanges yapmak yerine applicationKatmanında saveChanges() işi bitiririz.
}
catch (Exception ex)
{

  Console.WriteLine(ex.Message);
}



//acc.Block("Blocked");

// application layer
// use-case
//var repo = AccountRepository();
//repo.saveChanges();


//service.Deposit()


Location l1 = new Location(23.563, 24.789);
// l1.Lat = 5;

Location l2 = new Location(23.564, 24.786);

Console.WriteLine("l1",l1.ToString());
Console.WriteLine("l2", l2.ToString());


var r1 = Location.Equals(l1, l2); // l1 ile l2 aynı değerlere mi eşit true
var r2 = Location.ReferenceEquals(l1, l2); // aynı class referans mı false


Console.WriteLine($"eşit mi {l1.Equals(l2)}");
