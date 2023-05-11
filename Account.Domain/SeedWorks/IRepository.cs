using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.SeedWorks
{
  public interface IRepository<TAggregateRoot> where TAggregateRoot:AggregateRoot
  {
    Task CreateAsync(TAggregateRoot root);
    Task UpdateAsync(TAggregateRoot root);

    Task DeleteAsync(string Id);

    IQueryable Query(Expression<Func<TAggregateRoot, bool>> expression); // sorgunun sonuna sorgu eklemek için IQuerable tipi tanımladık

    Task<List<TAggregateRoot>> WhereAsync(Expression<Func<TAggregateRoot, bool>> expression);
    Task<TAggregateRoot> FindAsync(Expression<Func<TAggregateRoot, bool>> expression);


  }
}
