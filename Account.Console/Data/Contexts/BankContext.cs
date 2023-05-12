using Account.Console.Data.Contexts.AccountConfigurations;
using Account.Console.Infrastructure;
using Account.Domain.CustomerAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts
{
    /// <summary>
    /// Bankacılık işlemlerini yönettiğimiz Bounded Context, Müşterilerin Hesaplarının yöntetimi yaparız. Para çekme ve Para yatırma işlemleri bu Bounded Context üzerinden yapılır.
    /// </summary>
    public class BankContext:DbContext
  {

    private readonly IMediator mediator;

    public BankContext(IMediator mediator)
    {
      this.mediator = mediator;
    }

    // DbSet ile tabloların tanımını yaptık
    // DbSetleri DDD da aggregate bazlı tutuyoruz.
    public DbSet<Domain.AccountAggregates.Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }


    /// <summary>
    /// OnConf
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=(localDB)\\MSSQLLocalDB;Database=DDDSampleDB;Trusted_Connection=True;MultipleActiveResultSets=True");
    }

    /// <summary>
    /// model üzerindeki database yansıtılacak kural setlerini uygulayacağımız kısım
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // 1. value object değerleri db de nasıl göstericez
      // 2. enum değerlerini db nasıl yansıtıcaz
      // entityler üzerinde nasıl bir configurasyon yapıcaz.


      // PK
      modelBuilder.Entity<Domain.AccountAggregates.Account>().ToTable("Account", "BankContext");
      // veri tabanında direk şema bazlı tabloları ayırmak için kullandık
      modelBuilder.Entity<Domain.AccountAggregates.Account>().HasKey(x => x.Id);
      modelBuilder.Entity<Domain.AccountAggregates.Account>().HasIndex(x => x.AccountNumber).IsUnique();
      modelBuilder.Entity<Domain.AccountAggregates.Account>().HasIndex(x => x.IBAN).IsUnique();
      // value object değerlerin database de konfigürasyonu
      modelBuilder.Entity<Domain.AccountAggregates.Account>().OwnsOne(x => x.Balance).Property(x => x.Amount).HasColumnName("Balance_Amount");
      modelBuilder.Entity<Domain.AccountAggregates.Account>().OwnsOne(x => x.Balance).Property(x => x.Currency).HasColumnName("Balance_Currecy");
      // enumerations

      modelBuilder.Entity<Domain.AccountAggregates.Account>().HasMany(x => x.Transactions);

      // account Transactions adında bir navigation property sahip
      // field üzerinden propertylere değer aktarırken bunu ef ye söylediğimiz bir teknik
      var accountTransactionNavigation = modelBuilder.Entity<Domain.AccountAggregates.Account>().Metadata.FindNavigation(nameof(Domain.AccountAggregates.Account.Transactions));

      accountTransactionNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

      // dosyadan okuma tekniğini kullanalım
      modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());

      // customer ayarları

      modelBuilder.Entity<Customer>().ToTable("Customer", "BankContext");
      modelBuilder.Entity<Customer>().HasKey(x => x.Id);
      modelBuilder.Entity<Customer>().Property(x => x.Name).HasMaxLength(50);
      modelBuilder.Entity<Customer>().Property(x => x.SurName).HasMaxLength(70);
      modelBuilder.Entity<Customer>().HasIndex(x => x.PhoneNumber).IsUnique();
      modelBuilder.Entity<Customer>().HasMany(x => x.Accounts);


       base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      // save olmadan önce entityler üzerinde tanımlanmış tüm domain eventleri fırlatmak pulish etmek nesnelerin statelerini domain eventler vasıtası ile değiştirmek sonrasında savechanges ile context bazlı single transaction bütünlülüğü sağlamak.

      // yukarıda domain eventler fırlatılarak entity stateleri domain serviceler vasıtası ile kontrol edilir.validate edilir. veya bir aggreagate den başka bir aggregate'e müdehale edilip başka bir aggregate state değişimi olması sağlanır. ve değişen tüm aggregateler ve aggregate altındaki tüm entity stateleri database savechanges ile uygulanır.
      // Single Transaction Scope
      await this.mediator.DispatchDomainEventsAsync(this);
      // alt işlemlerde bir sorun varsa ana işlemin transaction gerçekleştirme

      return await base.SaveChangesAsync(cancellationToken); // tek bir execute sorgusu
      
    }

  }
}
