using Account.Domain.Order.BuyerAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts.OrderConfigurations
{
  public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
  {
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
      builder.HasKey(x => x.Id);
      builder.ToTable("Buyer", "OrderContext");
      builder.HasMany(x => x.Orders); // Satın alan kişini birden fazla siparişi vardır
    }
  }
}
