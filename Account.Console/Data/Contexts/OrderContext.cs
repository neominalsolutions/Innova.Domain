using Account.Console.Data.Contexts.OrderConfigurations;
using Account.Console.Infrastructure;
using Account.Domain.Order.BuyerAggregates;
using Account.Domain.Order.OrderAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts
{
  public class OrderContext:DbContext
  {

    private readonly IMediator mediator;

    public OrderContext(IMediator mediator)
    {
      this.mediator = mediator;
    }

    public DbSet<Order> Orders { get; set; } // Aggregate Root
    public DbSet<Buyer> Buyers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new OrderConfigurations.OrderConfiguration());
      modelBuilder.ApplyConfiguration(new OrderConfigurations.OrderItemConfiguration());
      modelBuilder.ApplyConfiguration(new BuyerConfiguration());

      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=(localDB)\\MSSQLLocalDB;Database=DDDSampleDB;Trusted_Connection=True;MultipleActiveResultSets=True");
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      // alt işlemlerde bir sorun varsa ana işlemin transaction gerçekleştirme
      await this.mediator.DispatchDomainEventsAsync(this);
      return await base.SaveChangesAsync(cancellationToken);
    }


  }
}
