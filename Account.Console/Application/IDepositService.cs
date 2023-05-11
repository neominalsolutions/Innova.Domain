using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Application
{
  public interface IDepositService<TRequest,TResponse>
  {
    Task<TResponse> HandleAsync(TRequest request);
  }
}
