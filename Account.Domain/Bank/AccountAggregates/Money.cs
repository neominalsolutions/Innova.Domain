using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates
{
    public class Money : ValueObject
    {
        // 5000 TL, 500 $
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; init; }
        public string Currency { get; init; }


        public override string ToString()
        {
            return $"{Amount} {Currency}"; // 5000 TL
        }

        // 0 TL 0 $
        public static Money Zero(string curreny)
        {
            return new Money(0, curreny);
        }

        // Para birimlerinin birbirinden operatörler vasıtası ile çıkarma işlemini yaptık
        // acc.Balance - new Money(100,"TL")
        public static Money operator -(Money m1, Money m2)
        {
            ThrowIfCurrencyIsNotMatch(m1, m2);
            return new Money(m1.Amount - m2.Amount, m1.Currency);
        }

        // acc.Balance + new Money(100,"TL")
        public static Money operator +(Money m1, Money m2)
        {
            ThrowIfCurrencyIsNotMatch(m1, m2);
            return new Money(m1.Amount + m2.Amount, m1.Currency);
        }

        // acc.Balance < new Money(5000,"TL") hesapdaki bakiye ile hesaba yatırılacak yada çekilecek paranın büyüklük küçükük kontrolü yapıcaz
        public static bool operator <(Money m1, Money m2)
        {
            ThrowIfCurrencyIsNotMatch(m1, m2);
            return m1.Amount < m2.Amount;
        }
        public static bool operator >(Money m1, Money m2)
        {
            ThrowIfCurrencyIsNotMatch(m1, m2);
            return m1.Amount > m2.Amount;
        }

        public static bool operator >=(Money m1, Money m2)
        {
            ThrowIfCurrencyIsNotMatch(m1, m2);
            return m1.Amount >= m2.Amount;
        }
        public static bool operator <=(Money m1, Money m2)
        {
            ThrowIfCurrencyIsNotMatch(m1, m2);
            return m1.Amount <= m2.Amount;
        }


        /// <summary>
        /// Gönderilen paraların currecy değerleri eşit değilse zaten aynı para birimi değildir. 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ThrowIfCurrencyIsNotMatch(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency) throw new ArgumentException("Currency değerleri eşleşmiyor");
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Math.Round(Amount, 8); // 8 karakterden sonrası için value eşitliği olsun
            yield return Currency.Trim(); // boşluksuz yazılsın TL, Dolar $
        }
    }
}
