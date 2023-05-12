using Account.Domain.Order.OrderAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts.OrderConfigurations
{
  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.ToTable("Order", "OrderContext");
      builder.HasKey(x => x.Id);

      // Value Object
      builder.OwnsOne(x => x.ShipAddress).Property(x => x.City).HasColumnName("Ship_City");
      builder.OwnsOne(x => x.ShipAddress).Property(x => x.Country).HasColumnName("Ship_Country");
      builder.OwnsOne(x => x.ShipAddress).Property(x => x.Street).HasColumnName("Ship_Street");

      // Enumaration
      builder.OwnsOne(x => x.Status).Property(x => x.Key).HasColumnName("Order_Status");
      builder.OwnsOne(x => x.Status).Property(x => x.Value).HasColumnName("Order_Code");

      builder.HasMany(x => x.Items);




      //account bulduğumuzda account transactions bilgilerini veri tabanından load ederken  _transactions field alanına load et.
      var accountTransactionNavigation =
             builder.Metadata.FindNavigation(nameof(Order.Items));

      accountTransactionNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
  }
}
