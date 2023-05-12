using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
    /// <summary>
    /// Deposit işlemi sonunda bussiness ruleların ve diğer aggregate deki işlemlerin event bazlı yöntilebilmesi için bir event tanımladık
    /// Eventler sadece içerisinde değer gönderen yapılar
    /// Her event bir handler üzerinden consume olur
    /// </summary>
    public class DepositSubmitted : INotification
    {
        public DepositSubmitted(Account account, Money money, AccountTransactionChannelType channelType)
        {
            Account = account;
            Money = money;
            this.channelType = channelType;
        }

        public Account Account { get; init; }

        public Money Money { get; init; }

        public AccountTransactionChannelType channelType { get; init; }


    }
}
