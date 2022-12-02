﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_API.Models;

#nullable disable

namespace Web_API.Migrations
{
    [DbContext(typeof(BudgetHeroAPIDbContext))]
    partial class BudgetHeroAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("BudgetBudgetCategoryGroup", b =>
                {
                    b.Property<Guid>("BudgetCategoryGroupsBudgetCategoryGroupID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BudgetsBudgetId")
                        .HasColumnType("TEXT");

                    b.HasKey("BudgetCategoryGroupsBudgetCategoryGroupID", "BudgetsBudgetId");

                    b.HasIndex("BudgetsBudgetId");

                    b.ToTable("BudgetBudgetCategoryGroup");
                });

            modelBuilder.Entity("BudgetUser", b =>
                {
                    b.Property<Guid>("BudgetsBudgetId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsersUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("BudgetsBudgetId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("BudgetUser");
                });

            modelBuilder.Entity("ModelsLibrary.BankAccount", b =>
                {
                    b.Property<Guid>("BankAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("BankAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("ModelsLibrary.Budget", b =>
                {
                    b.Property<Guid>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BudgetName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BudgetType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BudgetId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("ModelsLibrary.BudgetCategory", b =>
                {
                    b.Property<Guid>("BudgetCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BudgetCategoryGroupID")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CategoryAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BudgetCategoryID");

                    b.HasIndex("BudgetCategoryGroupID");

                    b.ToTable("BudgetCategories");
                });

            modelBuilder.Entity("ModelsLibrary.BudgetCategoryGroup", b =>
                {
                    b.Property<Guid>("BudgetCategoryGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryGroupDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BudgetCategoryGroupID");

                    b.ToTable("BudgetCategoryGroups");
                });

            modelBuilder.Entity("ModelsLibrary.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BudgetCategoryId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DepositAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ExpenseAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsHousehold")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsTransactionPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionMemo")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionPayee")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionId");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("BudgetCategoryId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ModelsLibrary.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("PercentageMod")
                        .HasColumnType("REAL");

                    b.Property<string>("UserImageLink")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BudgetBudgetCategoryGroup", b =>
                {
                    b.HasOne("ModelsLibrary.BudgetCategoryGroup", null)
                        .WithMany()
                        .HasForeignKey("BudgetCategoryGroupsBudgetCategoryGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.Budget", null)
                        .WithMany()
                        .HasForeignKey("BudgetsBudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BudgetUser", b =>
                {
                    b.HasOne("ModelsLibrary.Budget", null)
                        .WithMany()
                        .HasForeignKey("BudgetsBudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ModelsLibrary.BankAccount", b =>
                {
                    b.HasOne("ModelsLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelsLibrary.BudgetCategory", b =>
                {
                    b.HasOne("ModelsLibrary.BudgetCategoryGroup", "BudgetCategoryGroup")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetCategoryGroup");
                });

            modelBuilder.Entity("ModelsLibrary.Transaction", b =>
                {
                    b.HasOne("ModelsLibrary.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.BudgetCategory", "BudgetCategory")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("BudgetCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
