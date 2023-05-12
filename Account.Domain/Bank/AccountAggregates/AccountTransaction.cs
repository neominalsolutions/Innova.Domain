using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
    // WithDraw -500 TL (Money)
    public class AccountTransaction : Entity<string>
    {



        // Transaction Money 500 TL
        public Money Money { get; init; }

        public AccountTransactionType Type { get; init; } // Deposit, WithDraw

        public AccountTransactionChannelType ChannelType { get; init; } // Bank, Online, ATM

        public string AccountId { get; init; } // FK

        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Sadece constructor vasıtası ile bu değerler kontrolü bir şekilde gönderilsin.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="money"></param>
        /// <param name="type"></param>
        /// <param name="channelType"></param>
        public AccountTransaction(string accountId, Money money, AccountTransactionType type, AccountTransactionChannelType channelType)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Money = money;
            Type = type;
            ChannelType = channelType;
            AccountId = accountId;
        }

        public AccountTransaction()
        {

        }

        /// No aşağıdaki method ile accounttransaction değerini güncellemek doğru değildir. AccountTransaction kaydı Account nesnesi aggregate nesne üzerinden transaction bütünlülüğü bozulmayacak şekilde yönetilmelidir.
        //public void SetAccountId(string accountId)
        //{
        //  AccountId = accountId;
        //}

    }
}
