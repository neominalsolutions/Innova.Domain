using Account.Domain.AccountAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Data.Contexts.AccountConfigurations
{
    public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
  {
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
      builder.HasKey(x => x.Id);
      builder.ToTable("AccountTransaction", "BankContext");
      builder.OwnsOne(x => x.ChannelType).Property(x => x.Key).HasColumnName("ChannelType");
      builder.OwnsOne(x => x.ChannelType).Property(x => x.Value).HasColumnName("ChannelTypeCode");

      builder.OwnsOne(x => x.Type).Property(x => x.Key).HasColumnName("TransactionType");
      builder.OwnsOne(x => x.Type).Property(x => x.Value).HasColumnName("TransactionTypeCode");


      builder.OwnsOne(x => x.Money).Property(x => x.Amount).HasColumnName("TransactionAmount");
      builder.OwnsOne(x => x.Money).Property(x => x.Currency).HasColumnName("TransactionCurrency");

    }
  }
}
