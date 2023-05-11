using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.SeedWorks
{
  // uygulama için çalıştığımız nesnelerin alt kümelerini yöntetiğimiz root nesne
  // aggregate root olan bir nesne başka bir aggregate root ile konuşmak için domain event fırlatacak.
  // Seedwork de tanımlı bilgiler farklı context farklı domain gibi farklı projelere ayrılmış olan projelerde shared kernal olarak kullanılıyor.
  public interface IAggregateRoot
  {

    /// <summary>
    /// Domain Eventlerin listesini üzerinde tutat
    /// INotification tipinde event göndericez.
    /// </summary>
    IReadOnlyList<INotification> DomainEvents { get; }

    /// <summary>
    /// Sisteme yeni bir event girmek için Addile Event listesine event ekledik.
    /// </summary>
    /// <param name="eventItem"></param>
    public void AddDomainEvent(INotification eventItem);


    /// <summary>
    /// Eventin Remove edilmesi
    /// </summary>
    /// <param name="eventItem"></param>
    public void RemoveDomainEvent(INotification eventItem);


    public void ClearDomainEvents();

  }
}
