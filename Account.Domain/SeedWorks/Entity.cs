using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.SeedWorks
{
  public class Customer : Entity<int>
  {
    public Customer()
    {
      Id = 4;
    }
  }

  public class Employee : Entity<string>
  {
    public Employee()
    {
      Id = Guid.NewGuid().ToString();
    }
  }


  // sadece Id alanı value type olsun diye IComparable interface ile kısıtladık
  // TKey ile generic key tanımlamsı yaptık
  public abstract class Entity<TKey> where TKey:IComparable
  {
    public TKey Id { get; set; }


  }
}
