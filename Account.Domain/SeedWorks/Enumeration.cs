using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.SeedWorks
{
  public enum OrderStatus
  {
    Submitted=1,
    Paid=2
  }

  // sadece value type değerlere enum verelim

  public class InvoiceStatus : Enumeration
  {
    public static InvoiceStatus InvoiceSubmitted = new(nameof(InvoiceSubmitted),100);
    public static InvoiceStatus InvoiceDeleted = new("Deleted", 200);
 

    public InvoiceStatus(string key, int value) : base(key, value)
    {

      var invoices = InvoiceStatus.GetAll<InvoiceStatus>(); // Enum içerisindeki tüm değerleri getir.
      InvoiceStatus.Equals(InvoiceSubmitted, InvoiceDeleted); // referans ve tip olarak eşitler mi
      InvoiceSubmitted.CompareTo(InvoiceDeleted); // hangisinin value değeri daha büyük



    }
  }

  public abstract class Enumeration : IComparable
  {
    // init ile consturctor üzerinden veri geçişi yapılacak dedik.
    public string Key { get; init; } // Submitted
    public int Value { get; init; } //  100

    // contructor üzerinden değer atamanın bir farklı yazım şekli
    //protected Enumeration(string key, int value) => (Key, Value) = (key, value);
    // yukarıdaki tek satırda yazılmış hali
    protected Enumeration(string key,int value)
    {
      Key = key;
      Value = value;
    }
    public override string ToString() => Key;

    // Elimizdeki tüm listeki elemanların değerlerini okumak için yazılmıştır.
    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    // Nesnelerin değer olarak ve referance olarak birbirine eşit olup olmadığını kontrol ettmemiz gereken durumlarda Equals methodunu kullanabiliriz.
    public override bool Equals(object obj)
    {
      if (obj is not Enumeration otherValue)
      {
        return false;
      }

      var typeMatches = GetType().Equals(obj.GetType());
      var valueMatches = Value.Equals(otherValue.Value);

      return typeMatches && valueMatches;
    }

    // listelenmiş bir şeyin sırlanması için yazılmış
    public int CompareTo(object other) => Value.CompareTo(((Enumeration)other).Value);
  }
}
