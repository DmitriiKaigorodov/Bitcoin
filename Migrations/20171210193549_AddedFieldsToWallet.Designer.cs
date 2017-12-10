﻿// <auto-generated />
using Bitcoin.Core;
using Bitcoin.Persistent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Bitcoin.Migrations
{
    [DbContext(typeof(BitcoinDbContext))]
    [Migration("20171210193549_AddedFieldsToWallet")]
    partial class AddedFieldsToWallet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bitcoin.Core.Models.Transaction", b =>
                {
                    b.Property<string>("TxId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Confirmations");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("TimeReceived");

                    b.Property<bool>("WasRequested");

                    b.HasKey("TxId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Bitcoin.Core.Models.TransactionDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account");

                    b.Property<string>("Address");

                    b.Property<double>("Amount");

                    b.Property<int>("Category");

                    b.Property<string>("TransactionTxId");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTxId");

                    b.ToTable("TransactionDetails");
                });

            modelBuilder.Entity("Bitcoin.Core.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Balance");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Bitcoin.Core.Models.TransactionDetails", b =>
                {
                    b.HasOne("Bitcoin.Core.Models.Transaction", "Transaction")
                        .WithMany("Details")
                        .HasForeignKey("TransactionTxId");
                });
#pragma warning restore 612, 618
        }
    }
}