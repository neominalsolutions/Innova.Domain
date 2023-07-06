using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  public interface IAccountDomainService
  {
    /// <summary>
    /// Deposit ile bussiness caselerimiz yazacağımızı kontrol edeceğimiz exception caseleri fırlatacağımız bir servis tanımı
    /// </summary>
    /// <param name="acc"></param>
    /// <param name="money"></param>
    /// <param name="channelType"></param>
    void Deposit(Account acc, Money money, AccountTransactionChannelType channelType);
    void WithDraw(Account acc, Money money, AccountTransactionChannelType channelType);

  }
}

public class Ogrenci:AggregateRoot
{
  public List<Ders> OgrenciDers { get; set; }


  public void DersNotGirisi(Ders ders, List<Not> notlar)
  {
    foreach (var not in notlar)
    {
      ders.NotEkle(not);
    }

   
  }

}




public class Ders:AggregateRoot
{
  public List<Not> Nots { get; set; }



  public void NotEkle(Not not)
  {
    Nots.Add(not);
  }

  private void NotHesapla()
  {

  }

}

public class Not
{

}