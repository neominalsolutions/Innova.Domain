using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
    public class AccountTransactionType : Enumeration
    {
        // WithDraw // Para Çekme 
        // Deposit // Para Yatırma

        public static AccountTransactionType Deposit = new(nameof(Deposit), 100);
        public static AccountTransactionType WithDraw = new(nameof(WithDraw), 200);

        public AccountTransactionType(string key, int value) : base(key, value)
        {
        }
    }
}
