using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
  public class AccountChannelType : Enumeration
  {
    public static AccountChannelType Online = new AccountChannelType(nameof(Online), 1000);
    public static AccountChannelType Bank = new AccountChannelType(nameof(Bank), 1001);
    public static AccountChannelType ATM = new AccountChannelType(nameof(ATM), 1002);

    public AccountChannelType(string key, int value) : base(key, value)
    {
      // Bank
      // Online
      // ATM
    }
  }
}
