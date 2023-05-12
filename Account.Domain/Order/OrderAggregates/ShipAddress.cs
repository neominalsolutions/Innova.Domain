using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Order.OrderAggregates
{
  public class ShipAddress : ValueObject
  {
    public string City { get; init; }
    public string Street { get; init; }
    public string Country { get; init; }

    public ShipAddress(string city, string country, string street)
    {
      City = city;
      Country = country;
      Street = street;
    }

    public override string ToString()
    {
      return $"{Street} {City}/{Country}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return City;
      yield return Country;
      yield return Street;
    }
  }
}
