using Account.Domain.Order.OrderAggregates;
using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.CustomerAggregates
{
    public class Customer : AggregateRoot
    {

        /// <summary>
        /// EF de migration işlemleri yaparken boş contructur bırakıyoruz
        /// </summary>
        public Customer()
        {

        }


        public Customer(string name, string surName)
        {
            Name = name;
            SurName = surName;
        }

        // init nesne sadece oluşutururken nesneye değer atamamızı sağlayan bir tanımlama
        // nesnenin propertyleri daha sonradan güncellenecebilecek ise bu durumda private set set kontrol altına alırız.
        public string Name { get; private set; }
        public string SurName { get; private set; }

        public string PhoneNumber { get; private set; }

        public string FullName { get { return $"{Name} {SurName}"; } }

        // Müşterinin hesaplarını görmek istiyoruz
        // DDD de associations ihtiyaca göre olmalı ve karmaşıklığı engellemek için tek taraflı tanımlanmlıdır. Müşterinin hesaplarına müşteri üzerinden erişilir.
        // Hesaptan müşteri erişilmemelidir.
        public IReadOnlyList<Domain.AccountAggregates.Account> Accounts { get; }
        
        



        public void SetName(string name)
        {
            if (name is null)
                throw new ArgumentException("müşteri ismi boş bırakıdı");

            Name = name.Trim();
        }

        public void SetSurName(string surname)
        {
            if (surname is null)
                throw new ArgumentException("müşteri ismi boş bırakıdı");

            SurName = surname.Trim().ToUpper();
        }

        public void SetPhone(string phoneNumber)
        {
            // Telefon numarası formatında mı değil;

            PhoneNumber = phoneNumber;
        }



    }
}
