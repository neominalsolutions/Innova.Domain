using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.SeedWorks
{

  public class Location:ValueObject
  {
    public double Lat { get; init; }
    public double Lng { get; init; }

    /// <summary>
    /// value object değerleri init ile initialize aşamasında alır. property dışarıdan değer set edilemez immutable çalışır.
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    public Location(double lat, double lng)
    {
      Lat = lat;
      Lng = lng;
    }

    // nesnelerin birbirne olan value eşitliliklerini belirli kısıtlamak veya esneklikler sağladığımız yer
    protected override IEnumerable<object> GetEqualityComponents()
    {
      string value = $"{Math.Round(Lat, 2)};{Math.Round(Lng, 2)}";

      yield return value;
    }
  }



  // tek başına bir anlam ifade etmeyen kendisine ait idsi olmayan sadece değer eşitliği ile ilgilenen, immutable olan yapılar.
  public abstract class ValueObject
  {
    // iki value object referans eşitliğini kontrol eder.
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
      if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
      {
        return false;
      }
      return ReferenceEquals(left, right) || left.Equals(right);
    }

    // sağ value object ile sol value object birbirine eşit değilmi
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
      return !(EqualOperator(left, right));
    }

    // value object nesnelerin birbirlerine eğiştliğini bu abstractdan dönen değere göre kontrol edebiliriz. yazılımcı eşitlik değerine müdehale edebilir. 100 TL == 101 TL, Enlem,Boylam, 21.56,23,56 21,54, 23,58
    protected abstract IEnumerable<object> GetEqualityComponents();

    // şuan üzerinde çalışılan value object tip olarak ve value olarak eşitmi
    public override bool Equals(object obj)
    {
      if (obj == null || obj.GetType() != GetType())
      {
        return false;
      }

      var other = (ValueObject)obj;

      return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    // her objenin bir hash kodu dnası demek
    // burada value object olduğundan dolayı referance type gibi çalışmamısı için value üzerinden hashcode almış.
    public override int GetHashCode()
    {
      return GetEqualityComponents()
          .Select(x => x != null ? x.GetHashCode() : 0)
          .Aggregate((x, y) => x ^ y);
    }
    // Other utility methods
  }
}
