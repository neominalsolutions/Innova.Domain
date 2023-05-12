using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.AccountAggregates.Exceptions
{
    public class TranferLimitException : ApplicationException
    {
        public TranferLimitException(string message) : base(message)
        {

        }
    }
}
