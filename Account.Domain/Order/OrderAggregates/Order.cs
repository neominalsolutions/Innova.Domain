using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
  public class Order:AggregateRoot
  {
    public string BuyerId { get; init; }

    public DateTime OrderDate { get; private set; }

    public ShipAddress ShipAddress { get; private set; } // adress yanlış girip belirli bir süre kadar güncellenebilir.

    public OrderStatus Status { get; set; }

    private List<OrderItem> _items = new List<OrderItem>();
    public IReadOnlyList<OrderItem> Items => _items;

    // Total Setter yazılmadığını not mapped olarak gider tabloda olmaz ama programda kod geliştiriken bze hesaplama konusunda yardımcı olur. encapluation yaptık. 
    // Uyarı Order ile OrderItem Include ile çekmezsek hata yaparız.

    public decimal TotalPrice
    {
      get
      {
        return _items.Sum(x => x.Quantity * x.ListPrice);
      }
    }

    public Order()
    {

    }

    public Order(string buyerId)
    {
      Id = Guid.NewGuid().ToString();
      BuyerId = buyerId;
    }


    private void AddItem(string description, int quantity, decimal listPrice)
    {

      // aynı üründen 1 kerede 20 adeten fazla alınmasını engelleyebilirim. Maksimum 

      if (quantity > 20)
      {
        throw new Exception("Tek seferde en fazla 20 adet ürün alınabilir.");
      }

      _items.Add(new OrderItem(Id, description, listPrice, quantity));

    }

    public void SubmitOrder(Account.Domain.AccountAggregates.Account account,List<OrderItem> items, ShipAddress shipAddress)
    {

      foreach (var item in items)
      {
        AddItem(item.Description, item.Quantity, item.ListPrice);
      }

      Status = OrderStatus.Submitted;
      ShipAddress = shipAddress;
      OrderDate = DateTime.Now;

      // Account üzerinden gerekli miktarı hesaptan çekmesi için bir event fırlatarak 2 farklı bounded context konuşturmam lazım

      var @event = new OrderSubmitted(this.TotalPrice, account.AccountNumber, "TL", account.CustomerId);
      AddDomainEvent(@event);
      // OrderContext üzerinden bu event fırlatılacak.

    }

  }
}
