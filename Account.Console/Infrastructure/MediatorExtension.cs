using Account.Domain.SeedWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Infrastructure
{
  public static class MediatorExtension
  {
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
    {
      // entity framework içerisinde aktif olarak kullanılan change tracker mekanizması ile entity statelere ulaşıp burada ilgili entityler üzerinde bir domain event eklenmiş ise eklenen bu domain eventleri kayıt sırasında fırlatıyoruz.

      // state değişen added,modified,removed olan entityler kim ?
      // state değişen entityler üzerindeki domain eventleri IAggregateRoot implente olan aggregateRoot entityleri bul
      var domainEntities = ctx.ChangeTracker
                              .Entries<IAggregateRoot>()
                              .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

      // kaç farklı entity state değişmiş ise hepsinin eventlerini bulduk
      var domainEvents = domainEntities
          .SelectMany(x => x.Entity.DomainEvents)
          .ToList();

      // artık domainEntities üzerindeki eventleri bulduktan sonra tüm eventleri temizliyoruz.
      domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

      foreach (var domainEvent in domainEvents)
      {
        await mediator.Publish(domainEvent);
      }
      // yakaladığımız domain eventleri mediaTr publish methodu ile notify ediyoruz.
      // bu kod sonrasında Domain event handlerlar tetikleniyor.

    }
  }
}
