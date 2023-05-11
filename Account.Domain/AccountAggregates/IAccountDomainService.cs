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
