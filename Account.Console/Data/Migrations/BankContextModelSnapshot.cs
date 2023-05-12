﻿// <auto-generated />
using System;
using Account.Console.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Account.Console.Data.Migrations
{
    [DbContext(typeof(BankContext))]
    partial class BankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Account.Domain.AccountAggregates.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BlockReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CloseReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IBAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.HasIndex("IBAN")
                        .IsUnique();

                    b.ToTable("Account", "BankContext");
                });

            modelBuilder.Entity("Account.Domain.AccountAggregates.AccountTransaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountTransaction", "BankContext");
                });

            modelBuilder.Entity("Account.Domain.CustomerAggregates.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Customer", "BankContext");
                });

            modelBuilder.Entity("Account.Domain.AccountAggregates.Account", b =>
                {
                    b.HasOne("Account.Domain.CustomerAggregates.Customer", null)
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Account.Domain.AccountAggregates.Money", "Balance", b1 =>
                        {
                            b1.Property<string>("AccountId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Balance_Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Balance_Currecy");

                            b1.HasKey("AccountId");

                            b1.ToTable("Account", "BankContext");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("Balance")
                        .IsRequired();
                });

            modelBuilder.Entity("Account.Domain.AccountAggregates.AccountTransaction", b =>
                {
                    b.HasOne("Account.Domain.AccountAggregates.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Account.Domain.AccountAggregates.Money", "Money", b1 =>
                        {
                            b1.Property<string>("AccountTransactionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("TransactionAmount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransactionCurrency");

                            b1.HasKey("AccountTransactionId");

                            b1.ToTable("AccountTransaction", "BankContext");

                            b1.WithOwner()
                                .HasForeignKey("AccountTransactionId");
                        });

                    b.OwnsOne("Account.Domain.AccountAggregates.AccountTransactionChannelType", "ChannelType", b1 =>
                        {
                            b1.Property<string>("AccountTransactionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ChannelType");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("ChannelTypeCode");

                            b1.HasKey("AccountTransactionId");

                            b1.ToTable("AccountTransaction", "BankContext");

                            b1.WithOwner()
                                .HasForeignKey("AccountTransactionId");
                        });

                    b.OwnsOne("Account.Domain.AccountAggregates.AccountTransactionType", "Type", b1 =>
                        {
                            b1.Property<string>("AccountTransactionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransactionType");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("TransactionTypeCode");

                            b1.HasKey("AccountTransactionId");

                            b1.ToTable("AccountTransaction", "BankContext");

                            b1.WithOwner()
                                .HasForeignKey("AccountTransactionId");
                        });

                    b.Navigation("ChannelType")
                        .IsRequired();

                    b.Navigation("Money")
                        .IsRequired();

                    b.Navigation("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("Account.Domain.AccountAggregates.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Account.Domain.CustomerAggregates.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}