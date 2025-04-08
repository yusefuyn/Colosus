using Colosus.Entity.Concretes.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Sql.Entity.Abstracts
{
    public interface IContext
    {
        public string DatabaseConnectionStringName { get; protected set; }
        public int SaveChanges();
        public void MigrateDb();
        public DbSet<User> Users { get; set; }
        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
        public DbSet<PaymentAddress> PaymentAddresseses { get; set; }
        public DbSet<CustomerFirmRelation> CustomerFirmRelations { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentTypeFirmRelation> PaymentTypeFirmRelations { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<CategoryFirmRelation> CategoryFirmRelations { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProductStock> Stocks { get; set; }
        public DbSet<ProductFirmRelation> ProductFirmRelations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategoryRelation> ProductCategoryRelations { get; set; }
        public DbSet<FirmRole> FirmRoles { get; set; }
        public DbSet<UserRoleRelations> UserRoleRelations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<FirmUserRelation> FirmUserRelations { get; set; }
    }
}
