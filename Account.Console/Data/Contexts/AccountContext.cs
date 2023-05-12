using Account.Console.Data.Contexts.AccountConfigurations;
using Account.Console.Infrastructure;
using Account.Domain.AccountAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts
{
  public class AccountContext:DbContext
  {

    private readonly IMediator mediator;

    public AccountContext(IMediator mediator)
    {
      this.mediator = mediator;
    }

    // DbSet ile tabloların tanımını yaptık
    // DbSetleri DDD da aggregate bazlı tutuyoruz.
    public DbSet<Account.Domain.AccountAggregates.Account> Accounts { get; set; }


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
      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().ToTable("Account", "AccountContext");
      // veri tabanında direk şema bazlı tabloları ayırmak için kullandık
      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().HasKey(x => x.Id);
      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().HasIndex(x => x.AccountNumber).IsUnique();
      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().HasIndex(x => x.IBAN).IsUnique();
      // value object değerlerin database de konfigürasyonu
      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().OwnsOne(x => x.Balance).Property(x => x.Amount).HasColumnName("Balance_Amount");
      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().OwnsOne(x => x.Balance).Property(x => x.Currency).HasColumnName("Balance_Currecy");
      // enumerations

      modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().HasMany(x => x.Transactions);

      // account Transactions adında bir navigation property sahip
      // field üzerinden propertylere değer aktarırken bunu ef ye söylediğimiz bir teknik
      var accountTransactionNavigation = modelBuilder.Entity<Account.Domain.AccountAggregates.Account>().Metadata.FindNavigation(nameof(Account.Domain.AccountAggregates.Account.Transactions));

      accountTransactionNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

      // dosyadan okuma tekniğini kullanalım
      modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());


      base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      // save olmadan önce entityler üzerinde tanımlanmış tüm domain eventleri fırlatmak pulish etmek nesnelerin statelerini domain eventler vasıtası ile değiştirmek sonrasında savechanges ile context bazlı single transaction bütünlülüğü sağlamak.
      await this.mediator.DispatchDomainEventsAsync(this);
      // yukarıda domain eventler fırlatılarak entity stateleri domain serviceler vasıtası ile kontrol edilir.validate edilir. veya bir aggreagate den başka bir aggregate'e müdehale edilip başka bir aggregate state değişimi olması sağlanır. ve değişen tüm aggregateler ve aggregate altındaki tüm entity stateleri database savechanges ile uygulanır.
      return await base.SaveChangesAsync(cancellationToken);
    }

  }
}
