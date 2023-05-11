using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.SeedWorks
{
  public class AggregateTest:AggregateRoot
  {
    public AggregateTest()
    {
      Id = Guid.NewGuid().ToString();
      //AddDomainEvent();
      ClearDomainEvents();
    }
  }


  public class AggregateRoot : Entity<string>, IAggregateRoot
  {
    // field
    private List<INotification> _domainEvents = new List<INotification>();

    // property _domainEvents field eklenenleri döndürsün. setsiz çalışsın
    public IReadOnlyList<INotification> DomainEvents => _domainEvents;

    //public IReadOnlyList<INotification> DomainEvents
    //{
    //  get
    //  {
    //    return _domainEvents;
    //  }
    //}

    public void AddDomainEvent(INotification eventItem)
    {
      _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
      _domainEvents.Clear();
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
      _domainEvents.Remove(eventItem);
    }
  }
}
