﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Scylla.DAL.Models;

namespace Scylla.DAL.Context
{
    public partial class AdventureWorksDW2019Context : DbContext
    {
        
        private readonly string _connectionString;
        public AdventureWorksDW2019Context(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionString");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public virtual DbSet<AdventureWorksDwbuildVersion> AdventureWorksDwbuildVersions { get; set; } = null!;
        public virtual DbSet<DatabaseLog> DatabaseLogs { get; set; } = null!;
        public virtual DbSet<DimAccount> DimAccounts { get; set; } = null!;
        public virtual DbSet<DimCurrency> DimCurrencies { get; set; } = null!;
        public virtual DbSet<DimCustomer> DimCustomers { get; set; } = null!;
        public virtual DbSet<DimDate> DimDates { get; set; } = null!;
        public virtual DbSet<DimDepartmentGroup> DimDepartmentGroups { get; set; } = null!;
        public virtual DbSet<DimEmployee> DimEmployees { get; set; } = null!;
        public virtual DbSet<DimGeography> DimGeographies { get; set; } = null!;
        public virtual DbSet<DimOrganization> DimOrganizations { get; set; } = null!;
        public virtual DbSet<DimProduct> DimProducts { get; set; } = null!;
        public virtual DbSet<DimProductCategory> DimProductCategories { get; set; } = null!;
        public virtual DbSet<DimProductSubcategory> DimProductSubcategories { get; set; } = null!;
        public virtual DbSet<DimPromotion> DimPromotions { get; set; } = null!;
        public virtual DbSet<DimReseller> DimResellers { get; set; } = null!;
        public virtual DbSet<DimSalesReason> DimSalesReasons { get; set; } = null!;
        public virtual DbSet<DimSalesTerritory> DimSalesTerritories { get; set; } = null!;
        public virtual DbSet<DimScenario> DimScenarios { get; set; } = null!;
        public virtual DbSet<FactAdditionalInternationalProductDescription> FactAdditionalInternationalProductDescriptions { get; set; } = null!;
        public virtual DbSet<FactCallCenter> FactCallCenters { get; set; } = null!;
        public virtual DbSet<FactCurrencyRate> FactCurrencyRates { get; set; } = null!;
        public virtual DbSet<FactFinance> FactFinances { get; set; } = null!;
        public virtual DbSet<FactInternetSale> FactInternetSales { get; set; } = null!;
        public virtual DbSet<FactProductInventory> FactProductInventories { get; set; } = null!;
        public virtual DbSet<FactResellerSale> FactResellerSales { get; set; } = null!;
        public virtual DbSet<FactSalesQuotum> FactSalesQuota { get; set; } = null!;
        public virtual DbSet<FactSurveyResponse> FactSurveyResponses { get; set; } = null!;
        public virtual DbSet<NewFactCurrencyRate> NewFactCurrencyRates { get; set; } = null!;
        public virtual DbSet<ProspectiveBuyer> ProspectiveBuyers { get; set; } = null!;
        public virtual DbSet<VAssocSeqLineItem> VAssocSeqLineItems { get; set; } = null!;
        public virtual DbSet<VAssocSeqOrder> VAssocSeqOrders { get; set; } = null!;
        public virtual DbSet<VDmprep> VDmpreps { get; set; } = null!;
        public virtual DbSet<VTargetMail> VTargetMails { get; set; } = null!;
        public virtual DbSet<VTimeSeries> VTimeSeries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdventureWorksDwbuildVersion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AdventureWorksDWBuildVersion");

                entity.Property(e => e.Dbversion)
                    .HasMaxLength(50)
                    .HasColumnName("DBVersion");

                entity.Property(e => e.VersionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DatabaseLog>(entity =>
            {
                entity.HasKey(e => e.DatabaseLogId)
                    .HasName("PK_DatabaseLog_DatabaseLogID")
                    .IsClustered(false);

                entity.ToTable("DatabaseLog");

                entity.Property(e => e.DatabaseLogId).HasColumnName("DatabaseLogID");

                entity.Property(e => e.DatabaseUser).HasMaxLength(128);

                entity.Property(e => e.Event).HasMaxLength(128);

                entity.Property(e => e.Object).HasMaxLength(128);

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.Property(e => e.Schema).HasMaxLength(128);

                entity.Property(e => e.Tsql).HasColumnName("TSQL");

                entity.Property(e => e.XmlEvent).HasColumnType("xml");
            });

            modelBuilder.Entity<DimAccount>(entity =>
            {
                entity.HasKey(e => e.AccountKey);

                entity.ToTable("DimAccount");

                entity.Property(e => e.AccountDescription).HasMaxLength(50);

                entity.Property(e => e.AccountType).HasMaxLength(50);

                entity.Property(e => e.CustomMemberOptions).HasMaxLength(200);

                entity.Property(e => e.CustomMembers).HasMaxLength(300);

                entity.Property(e => e.Operator).HasMaxLength(50);

                entity.Property(e => e.ValueType).HasMaxLength(50);

                entity.HasOne(d => d.ParentAccountKeyNavigation)
                    .WithMany(p => p.InverseParentAccountKeyNavigation)
                    .HasForeignKey(d => d.ParentAccountKey)
                    .HasConstraintName("FK_DimAccount_DimAccount");
            });

            modelBuilder.Entity<DimCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyKey)
                    .HasName("PK_DimCurrency_CurrencyKey");

                entity.ToTable("DimCurrency");

                entity.HasIndex(e => e.CurrencyAlternateKey, "AK_DimCurrency_CurrencyAlternateKey")
                    .IsUnique();

                entity.Property(e => e.CurrencyAlternateKey)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.CurrencyName).HasMaxLength(50);
            });

            modelBuilder.Entity<DimCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerKey)
                    .HasName("PK_DimCustomer_CustomerKey");

                entity.ToTable("DimCustomer");

                entity.HasIndex(e => e.CustomerAlternateKey, "IX_DimCustomer_CustomerAlternateKey")
                    .IsUnique();

                entity.Property(e => e.AddressLine1).HasMaxLength(120);

                entity.Property(e => e.AddressLine2).HasMaxLength(120);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CommuteDistance).HasMaxLength(15);

                entity.Property(e => e.CustomerAlternateKey).HasMaxLength(15);

                entity.Property(e => e.DateFirstPurchase).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EnglishEducation).HasMaxLength(40);

                entity.Property(e => e.EnglishOccupation).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FrenchEducation).HasMaxLength(40);

                entity.Property(e => e.FrenchOccupation).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.HouseOwnerFlag)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.SpanishEducation).HasMaxLength(40);

                entity.Property(e => e.SpanishOccupation).HasMaxLength(100);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(8);

                entity.Property(e => e.YearlyIncome).HasColumnType("money");

                entity.HasOne(d => d.GeographyKeyNavigation)
                    .WithMany(p => p.DimCustomers)
                    .HasForeignKey(d => d.GeographyKey)
                    .HasConstraintName("FK_DimCustomer_DimGeography");
            });

            modelBuilder.Entity<DimDate>(entity =>
            {
                entity.HasKey(e => e.DateKey)
                    .HasName("PK_DimDate_DateKey");

                entity.ToTable("DimDate");

                entity.HasIndex(e => e.FullDateAlternateKey, "AK_DimDate_FullDateAlternateKey")
                    .IsUnique();

                entity.Property(e => e.DateKey).ValueGeneratedNever();

                entity.Property(e => e.EnglishDayNameOfWeek).HasMaxLength(10);

                entity.Property(e => e.EnglishMonthName).HasMaxLength(10);

                entity.Property(e => e.FrenchDayNameOfWeek).HasMaxLength(10);

                entity.Property(e => e.FrenchMonthName).HasMaxLength(10);

                entity.Property(e => e.FullDateAlternateKey).HasColumnType("date");

                entity.Property(e => e.SpanishDayNameOfWeek).HasMaxLength(10);

                entity.Property(e => e.SpanishMonthName).HasMaxLength(10);
            });

            modelBuilder.Entity<DimDepartmentGroup>(entity =>
            {
                entity.HasKey(e => e.DepartmentGroupKey);

                entity.ToTable("DimDepartmentGroup");

                entity.Property(e => e.DepartmentGroupName).HasMaxLength(50);

                entity.HasOne(d => d.ParentDepartmentGroupKeyNavigation)
                    .WithMany(p => p.InverseParentDepartmentGroupKeyNavigation)
                    .HasForeignKey(d => d.ParentDepartmentGroupKey)
                    .HasConstraintName("FK_DimDepartmentGroup_DimDepartmentGroup");
            });

            modelBuilder.Entity<DimEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeKey)
                    .HasName("PK_DimEmployee_EmployeeKey");

                entity.ToTable("DimEmployee");

                entity.Property(e => e.BaseRate).HasColumnType("money");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EmergencyContactName).HasMaxLength(50);

                entity.Property(e => e.EmergencyContactPhone).HasMaxLength(25);

                entity.Property(e => e.EmployeeNationalIdalternateKey)
                    .HasMaxLength(15)
                    .HasColumnName("EmployeeNationalIDAlternateKey");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LoginId)
                    .HasMaxLength(256)
                    .HasColumnName("LoginID");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.ParentEmployeeNationalIdalternateKey)
                    .HasMaxLength(15)
                    .HasColumnName("ParentEmployeeNationalIDAlternateKey");

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.ParentEmployeeKeyNavigation)
                    .WithMany(p => p.InverseParentEmployeeKeyNavigation)
                    .HasForeignKey(d => d.ParentEmployeeKey)
                    .HasConstraintName("FK_DimEmployee_DimEmployee");

                entity.HasOne(d => d.SalesTerritoryKeyNavigation)
                    .WithMany(p => p.DimEmployees)
                    .HasForeignKey(d => d.SalesTerritoryKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DimEmployee_DimSalesTerritory");
            });

            modelBuilder.Entity<DimGeography>(entity =>
            {
                entity.HasKey(e => e.GeographyKey)
                    .HasName("PK_DimGeography_GeographyKey");

                entity.ToTable("DimGeography");

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.CountryRegionCode).HasMaxLength(3);

                entity.Property(e => e.EnglishCountryRegionName).HasMaxLength(50);

                entity.Property(e => e.FrenchCountryRegionName).HasMaxLength(50);

                entity.Property(e => e.IpAddressLocator).HasMaxLength(15);

                entity.Property(e => e.PostalCode).HasMaxLength(15);

                entity.Property(e => e.SpanishCountryRegionName).HasMaxLength(50);

                entity.Property(e => e.StateProvinceCode).HasMaxLength(3);

                entity.Property(e => e.StateProvinceName).HasMaxLength(50);

                entity.HasOne(d => d.SalesTerritoryKeyNavigation)
                    .WithMany(p => p.DimGeographies)
                    .HasForeignKey(d => d.SalesTerritoryKey)
                    .HasConstraintName("FK_DimGeography_DimSalesTerritory");
            });

            modelBuilder.Entity<DimOrganization>(entity =>
            {
                entity.HasKey(e => e.OrganizationKey);

                entity.ToTable("DimOrganization");

                entity.Property(e => e.OrganizationName).HasMaxLength(50);

                entity.Property(e => e.PercentageOfOwnership).HasMaxLength(16);

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.DimOrganizations)
                    .HasForeignKey(d => d.CurrencyKey)
                    .HasConstraintName("FK_DimOrganization_DimCurrency");

                entity.HasOne(d => d.ParentOrganizationKeyNavigation)
                    .WithMany(p => p.InverseParentOrganizationKeyNavigation)
                    .HasForeignKey(d => d.ParentOrganizationKey)
                    .HasConstraintName("FK_DimOrganization_DimOrganization");
            });

            modelBuilder.Entity<DimProduct>(entity =>
            {
                entity.HasKey(e => e.ProductKey)
                    .HasName("PK_DimProduct_ProductKey");

                entity.ToTable("DimProduct");

                entity.HasIndex(e => new { e.ProductAlternateKey, e.StartDate }, "AK_DimProduct_ProductAlternateKey_StartDate")
                    .IsUnique();

                entity.Property(e => e.ArabicDescription).HasMaxLength(400);

                entity.Property(e => e.ChineseDescription).HasMaxLength(400);

                entity.Property(e => e.Class)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Color).HasMaxLength(15);

                entity.Property(e => e.DealerPrice).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EnglishDescription).HasMaxLength(400);

                entity.Property(e => e.EnglishProductName).HasMaxLength(50);

                entity.Property(e => e.FrenchDescription).HasMaxLength(400);

                entity.Property(e => e.FrenchProductName).HasMaxLength(50);

                entity.Property(e => e.GermanDescription).HasMaxLength(400);

                entity.Property(e => e.HebrewDescription).HasMaxLength(400);

                entity.Property(e => e.JapaneseDescription).HasMaxLength(400);

                entity.Property(e => e.ListPrice).HasColumnType("money");

                entity.Property(e => e.ModelName).HasMaxLength(50);

                entity.Property(e => e.ProductAlternateKey).HasMaxLength(25);

                entity.Property(e => e.ProductLine)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.Property(e => e.SizeRange).HasMaxLength(50);

                entity.Property(e => e.SizeUnitMeasureCode)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.SpanishProductName).HasMaxLength(50);

                entity.Property(e => e.StandardCost).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(7);

                entity.Property(e => e.Style)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.ThaiDescription).HasMaxLength(400);

                entity.Property(e => e.TurkishDescription).HasMaxLength(400);

                entity.Property(e => e.WeightUnitMeasureCode)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.HasOne(d => d.ProductSubcategoryKeyNavigation)
                    .WithMany(p => p.DimProducts)
                    .HasForeignKey(d => d.ProductSubcategoryKey)
                    .HasConstraintName("FK_DimProduct_DimProductSubcategory");
            });

            modelBuilder.Entity<DimProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryKey)
                    .HasName("PK_DimProductCategory_ProductCategoryKey");

                entity.ToTable("DimProductCategory");

                entity.HasIndex(e => e.ProductCategoryAlternateKey, "AK_DimProductCategory_ProductCategoryAlternateKey")
                    .IsUnique();

                entity.Property(e => e.EnglishProductCategoryName).HasMaxLength(50);

                entity.Property(e => e.FrenchProductCategoryName).HasMaxLength(50);

                entity.Property(e => e.SpanishProductCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<DimProductSubcategory>(entity =>
            {
                entity.HasKey(e => e.ProductSubcategoryKey)
                    .HasName("PK_DimProductSubcategory_ProductSubcategoryKey");

                entity.ToTable("DimProductSubcategory");

                entity.HasIndex(e => e.ProductSubcategoryAlternateKey, "AK_DimProductSubcategory_ProductSubcategoryAlternateKey")
                    .IsUnique();

                entity.Property(e => e.EnglishProductSubcategoryName).HasMaxLength(50);

                entity.Property(e => e.FrenchProductSubcategoryName).HasMaxLength(50);

                entity.Property(e => e.SpanishProductSubcategoryName).HasMaxLength(50);

                entity.HasOne(d => d.ProductCategoryKeyNavigation)
                    .WithMany(p => p.DimProductSubcategories)
                    .HasForeignKey(d => d.ProductCategoryKey)
                    .HasConstraintName("FK_DimProductSubcategory_DimProductCategory");
            });

            modelBuilder.Entity<DimPromotion>(entity =>
            {
                entity.HasKey(e => e.PromotionKey)
                    .HasName("PK_DimPromotion_PromotionKey");

                entity.ToTable("DimPromotion");

                entity.HasIndex(e => e.PromotionAlternateKey, "AK_DimPromotion_PromotionAlternateKey")
                    .IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EnglishPromotionCategory).HasMaxLength(50);

                entity.Property(e => e.EnglishPromotionName).HasMaxLength(255);

                entity.Property(e => e.EnglishPromotionType).HasMaxLength(50);

                entity.Property(e => e.FrenchPromotionCategory).HasMaxLength(50);

                entity.Property(e => e.FrenchPromotionName).HasMaxLength(255);

                entity.Property(e => e.FrenchPromotionType).HasMaxLength(50);

                entity.Property(e => e.SpanishPromotionCategory).HasMaxLength(50);

                entity.Property(e => e.SpanishPromotionName).HasMaxLength(255);

                entity.Property(e => e.SpanishPromotionType).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimReseller>(entity =>
            {
                entity.HasKey(e => e.ResellerKey)
                    .HasName("PK_DimReseller_ResellerKey");

                entity.ToTable("DimReseller");

                entity.HasIndex(e => e.ResellerAlternateKey, "AK_DimReseller_ResellerAlternateKey")
                    .IsUnique();

                entity.Property(e => e.AddressLine1).HasMaxLength(60);

                entity.Property(e => e.AddressLine2).HasMaxLength(60);

                entity.Property(e => e.AnnualRevenue).HasColumnType("money");

                entity.Property(e => e.AnnualSales).HasColumnType("money");

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.BusinessType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MinPaymentAmount).HasColumnType("money");

                entity.Property(e => e.OrderFrequency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.ProductLine).HasMaxLength(50);

                entity.Property(e => e.ResellerAlternateKey).HasMaxLength(15);

                entity.Property(e => e.ResellerName).HasMaxLength(50);

                entity.HasOne(d => d.GeographyKeyNavigation)
                    .WithMany(p => p.DimResellers)
                    .HasForeignKey(d => d.GeographyKey)
                    .HasConstraintName("FK_DimReseller_DimGeography");
            });

            modelBuilder.Entity<DimSalesReason>(entity =>
            {
                entity.HasKey(e => e.SalesReasonKey)
                    .HasName("PK_DimSalesReason_SalesReasonKey");

                entity.ToTable("DimSalesReason");

                entity.Property(e => e.SalesReasonName).HasMaxLength(50);

                entity.Property(e => e.SalesReasonReasonType).HasMaxLength(50);
            });

            modelBuilder.Entity<DimSalesTerritory>(entity =>
            {
                entity.HasKey(e => e.SalesTerritoryKey)
                    .HasName("PK_DimSalesTerritory_SalesTerritoryKey");

                entity.ToTable("DimSalesTerritory");

                entity.HasIndex(e => e.SalesTerritoryAlternateKey, "AK_DimSalesTerritory_SalesTerritoryAlternateKey")
                    .IsUnique();

                entity.Property(e => e.SalesTerritoryCountry).HasMaxLength(50);

                entity.Property(e => e.SalesTerritoryGroup).HasMaxLength(50);

                entity.Property(e => e.SalesTerritoryRegion).HasMaxLength(50);
            });

            modelBuilder.Entity<DimScenario>(entity =>
            {
                entity.HasKey(e => e.ScenarioKey);

                entity.ToTable("DimScenario");

                entity.Property(e => e.ScenarioName).HasMaxLength(50);
            });

            modelBuilder.Entity<FactAdditionalInternationalProductDescription>(entity =>
            {
                entity.HasKey(e => new { e.ProductKey, e.CultureName })
                    .HasName("PK_FactAdditionalInternationalProductDescription_ProductKey_CultureName");

                entity.ToTable("FactAdditionalInternationalProductDescription");

                entity.Property(e => e.CultureName).HasMaxLength(50);
            });

            modelBuilder.Entity<FactCallCenter>(entity =>
            {
                entity.ToTable("FactCallCenter");

                entity.HasIndex(e => new { e.DateKey, e.Shift }, "AK_FactCallCenter_DateKey_Shift")
                    .IsUnique();

                entity.Property(e => e.FactCallCenterId).HasColumnName("FactCallCenterID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Shift).HasMaxLength(20);

                entity.Property(e => e.WageType).HasMaxLength(15);

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactCallCenters)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactCallCenter_DimDate");
            });

            modelBuilder.Entity<FactCurrencyRate>(entity =>
            {
                entity.HasKey(e => new { e.CurrencyKey, e.DateKey })
                    .HasName("PK_FactCurrencyRate_CurrencyKey_DateKey");

                entity.ToTable("FactCurrencyRate");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactCurrencyRates)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactCurrencyRate_DimCurrency");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactCurrencyRates)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactCurrencyRate_DimDate");
            });

            modelBuilder.Entity<FactFinance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FactFinance");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FinanceKey).ValueGeneratedOnAdd();

                entity.HasOne(d => d.AccountKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.AccountKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactFinance_DimAccount");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactFinance_DimDate");

                entity.HasOne(d => d.DepartmentGroupKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentGroupKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactFinance_DimDepartmentGroup");

                entity.HasOne(d => d.OrganizationKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.OrganizationKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactFinance_DimOrganization");

                entity.HasOne(d => d.ScenarioKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ScenarioKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactFinance_DimScenario");
            });

            modelBuilder.Entity<FactInternetSale>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderNumber, e.SalesOrderLineNumber })
                    .HasName("PK_FactInternetSales_SalesOrderNumber_SalesOrderLineNumber");

                entity.Property(e => e.SalesOrderNumber).HasMaxLength(20);

                entity.Property(e => e.CarrierTrackingNumber).HasMaxLength(25);

                entity.Property(e => e.CustomerPonumber)
                    .HasMaxLength(25)
                    .HasColumnName("CustomerPONumber");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.ExtendedAmount).HasColumnType("money");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ProductStandardCost).HasColumnType("money");

                entity.Property(e => e.SalesAmount).HasColumnType("money");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TaxAmt).HasColumnType("money");

                entity.Property(e => e.TotalProductCost).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactInternetSales)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimCurrency");

                entity.HasOne(d => d.CustomerKeyNavigation)
                    .WithMany(p => p.FactInternetSales)
                    .HasForeignKey(d => d.CustomerKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimCustomer");

                entity.HasOne(d => d.DueDateKeyNavigation)
                    .WithMany(p => p.FactInternetSaleDueDateKeyNavigations)
                    .HasForeignKey(d => d.DueDateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimDate1");

                entity.HasOne(d => d.OrderDateKeyNavigation)
                    .WithMany(p => p.FactInternetSaleOrderDateKeyNavigations)
                    .HasForeignKey(d => d.OrderDateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactInternetSales)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimProduct");

                entity.HasOne(d => d.PromotionKeyNavigation)
                    .WithMany(p => p.FactInternetSales)
                    .HasForeignKey(d => d.PromotionKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimPromotion");

                entity.HasOne(d => d.SalesTerritoryKeyNavigation)
                    .WithMany(p => p.FactInternetSales)
                    .HasForeignKey(d => d.SalesTerritoryKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimSalesTerritory");

                entity.HasOne(d => d.ShipDateKeyNavigation)
                    .WithMany(p => p.FactInternetSaleShipDateKeyNavigations)
                    .HasForeignKey(d => d.ShipDateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInternetSales_DimDate2");

                entity.HasMany(d => d.SalesReasonKeys)
                    .WithMany(p => p.SalesOrders)
                    .UsingEntity<Dictionary<string, object>>(
                        "FactInternetSalesReason",
                        l => l.HasOne<DimSalesReason>().WithMany().HasForeignKey("SalesReasonKey").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FactInternetSalesReason_DimSalesReason"),
                        r => r.HasOne<FactInternetSale>().WithMany().HasForeignKey("SalesOrderNumber", "SalesOrderLineNumber").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FactInternetSalesReason_FactInternetSales"),
                        j =>
                        {
                            j.HasKey("SalesOrderNumber", "SalesOrderLineNumber", "SalesReasonKey").HasName("PK_FactInternetSalesReason_SalesOrderNumber_SalesOrderLineNumber_SalesReasonKey");

                            j.ToTable("FactInternetSalesReason");

                            j.IndexerProperty<string>("SalesOrderNumber").HasMaxLength(20);
                        });
            });

            modelBuilder.Entity<FactProductInventory>(entity =>
            {
                entity.HasKey(e => new { e.ProductKey, e.DateKey });

                entity.ToTable("FactProductInventory");

                entity.Property(e => e.MovementDate).HasColumnType("date");

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactProductInventories)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactProductInventory_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactProductInventories)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactProductInventory_DimProduct");
            });

            modelBuilder.Entity<FactResellerSale>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderNumber, e.SalesOrderLineNumber })
                    .HasName("PK_FactResellerSales_SalesOrderNumber_SalesOrderLineNumber");

                entity.Property(e => e.SalesOrderNumber).HasMaxLength(20);

                entity.Property(e => e.CarrierTrackingNumber).HasMaxLength(25);

                entity.Property(e => e.CustomerPonumber)
                    .HasMaxLength(25)
                    .HasColumnName("CustomerPONumber");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.ExtendedAmount).HasColumnType("money");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ProductStandardCost).HasColumnType("money");

                entity.Property(e => e.SalesAmount).HasColumnType("money");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TaxAmt).HasColumnType("money");

                entity.Property(e => e.TotalProductCost).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactResellerSales)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimCurrency");

                entity.HasOne(d => d.DueDateKeyNavigation)
                    .WithMany(p => p.FactResellerSaleDueDateKeyNavigations)
                    .HasForeignKey(d => d.DueDateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimDate1");

                entity.HasOne(d => d.EmployeeKeyNavigation)
                    .WithMany(p => p.FactResellerSales)
                    .HasForeignKey(d => d.EmployeeKey)
                    .HasConstraintName("FK_FactResellerSales_DimEmployee");

                entity.HasOne(d => d.OrderDateKeyNavigation)
                    .WithMany(p => p.FactResellerSaleOrderDateKeyNavigations)
                    .HasForeignKey(d => d.OrderDateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactResellerSales)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimProduct");

                entity.HasOne(d => d.PromotionKeyNavigation)
                    .WithMany(p => p.FactResellerSales)
                    .HasForeignKey(d => d.PromotionKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimPromotion");

                entity.HasOne(d => d.ResellerKeyNavigation)
                    .WithMany(p => p.FactResellerSales)
                    .HasForeignKey(d => d.ResellerKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimReseller");

                entity.HasOne(d => d.SalesTerritoryKeyNavigation)
                    .WithMany(p => p.FactResellerSales)
                    .HasForeignKey(d => d.SalesTerritoryKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimSalesTerritory");

                entity.HasOne(d => d.ShipDateKeyNavigation)
                    .WithMany(p => p.FactResellerSaleShipDateKeyNavigations)
                    .HasForeignKey(d => d.ShipDateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactResellerSales_DimDate2");
            });

            modelBuilder.Entity<FactSalesQuotum>(entity =>
            {
                entity.HasKey(e => e.SalesQuotaKey)
                    .HasName("PK_FactSalesQuota_SalesQuotaKey");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.SalesAmountQuota).HasColumnType("money");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimDate");

                entity.HasOne(d => d.EmployeeKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.EmployeeKey)
                    .HasConstraintName("FK_FactSalesQuota_DimEmployee");
            });

            modelBuilder.Entity<FactSurveyResponse>(entity =>
            {
                entity.HasKey(e => e.SurveyResponseKey)
                    .HasName("PK_FactSurveyResponse_SurveyResponseKey");

                entity.ToTable("FactSurveyResponse");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EnglishProductCategoryName).HasMaxLength(50);

                entity.Property(e => e.EnglishProductSubcategoryName).HasMaxLength(50);

                entity.HasOne(d => d.CustomerKeyNavigation)
                    .WithMany(p => p.FactSurveyResponses)
                    .HasForeignKey(d => d.CustomerKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSurveyResponse_CustomerKey");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactSurveyResponses)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSurveyResponse_DateKey");
            });

            modelBuilder.Entity<NewFactCurrencyRate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NewFactCurrencyRate");

                entity.Property(e => e.CurrencyDate).HasColumnType("date");

                entity.Property(e => e.CurrencyId)
                    .HasMaxLength(3)
                    .HasColumnName("CurrencyID");
            });

            modelBuilder.Entity<ProspectiveBuyer>(entity =>
            {
                entity.HasKey(e => e.ProspectiveBuyerKey)
                    .HasName("PK_ProspectiveBuyer_ProspectiveBuyerKey");

                entity.ToTable("ProspectiveBuyer");

                entity.Property(e => e.AddressLine1).HasMaxLength(120);

                entity.Property(e => e.AddressLine2).HasMaxLength(120);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.Education).HasMaxLength(40);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.HouseOwnerFlag)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Occupation).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(15);

                entity.Property(e => e.ProspectAlternateKey).HasMaxLength(15);

                entity.Property(e => e.Salutation).HasMaxLength(8);

                entity.Property(e => e.StateProvinceCode).HasMaxLength(3);

                entity.Property(e => e.YearlyIncome).HasColumnType("money");
            });

            modelBuilder.Entity<VAssocSeqLineItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vAssocSeqLineItems");

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.OrderNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<VAssocSeqOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vAssocSeqOrders");

                entity.Property(e => e.IncomeGroup)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber).HasMaxLength(20);

                entity.Property(e => e.Region).HasMaxLength(50);
            });

            modelBuilder.Entity<VDmprep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDMPrep");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.EnglishProductCategoryName).HasMaxLength(50);

                entity.Property(e => e.IncomeGroup)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.OrderNumber).HasMaxLength(20);

                entity.Property(e => e.Region).HasMaxLength(50);
            });

            modelBuilder.Entity<VTargetMail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTargetMail");

                entity.Property(e => e.AddressLine1).HasMaxLength(120);

                entity.Property(e => e.AddressLine2).HasMaxLength(120);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CommuteDistance).HasMaxLength(15);

                entity.Property(e => e.CustomerAlternateKey).HasMaxLength(15);

                entity.Property(e => e.DateFirstPurchase).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EnglishEducation).HasMaxLength(40);

                entity.Property(e => e.EnglishOccupation).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FrenchEducation).HasMaxLength(40);

                entity.Property(e => e.FrenchOccupation).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.HouseOwnerFlag)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.SpanishEducation).HasMaxLength(40);

                entity.Property(e => e.SpanishOccupation).HasMaxLength(100);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(8);

                entity.Property(e => e.YearlyIncome).HasColumnType("money");
            });

            modelBuilder.Entity<VTimeSeries>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimeSeries");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ModelRegion).HasMaxLength(56);

                entity.Property(e => e.ReportingDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
