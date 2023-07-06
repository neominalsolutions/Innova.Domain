using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts
{
  public class RootTransaction
  {
    private OrderContext orderContext;
    private BankContext bankContext;


    public RootTransaction(OrderContext context1, BankContext context2)
    {
      this.orderContext = context1;
      this.bankContext = context2;
    }

    public async Task CommitAsync()
    {

      using (var tra1 = await bankContext.Database.BeginTransactionAsync())
      {
        using (var tra2 = await orderContext.Database.BeginTransactionAsync())
        {

          try
          {


            await bankContext.SaveChangesAsync();
            await orderContext.SaveChangesAsync();

           await  tra1.CommitAsync();
            await tra2.CommitAsync();



          }
          catch (Exception)
          {
            await tra1.RollbackAsync();
            await tra2.RollbackAsync();
          }

        }
      }


    }
  }
}
