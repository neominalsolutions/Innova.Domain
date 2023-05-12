using Account.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.CustomerAggregates
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
