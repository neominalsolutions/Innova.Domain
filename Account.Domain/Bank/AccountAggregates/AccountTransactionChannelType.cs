using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
    public class AccountTransactionChannelType : Enumeration
    {
        public static AccountTransactionChannelType Online = new AccountTransactionChannelType(nameof(Online), 1000);
        public static AccountTransactionChannelType Bank = new AccountTransactionChannelType(nameof(Bank), 1001);
        public static AccountTransactionChannelType ATM = new AccountTransactionChannelType(nameof(ATM), 1002);

        public AccountTransactionChannelType(string key, int value) : base(key, value)
        {
            // Bank
            // Online
            // ATM
        }
    }
}
