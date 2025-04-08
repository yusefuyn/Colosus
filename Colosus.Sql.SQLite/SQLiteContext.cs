using Colosus.Sql.Entity.Abstracts;
using Colosus.Sql.Entity.Concretes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colosus.Entity.Concretes.DatabaseModel;

namespace Colosus.Sql.SQLite
{
    public class SQLiteContext : BasedDbContext, IContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DatabaseFacade Database { get; set; }
        public DbSet<PaymentTypeFirmRelation> PaymentTypeFirmRelations { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<CustomerFirmRelation> CustomerFirmRelations { get; set; }
        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<CategoryFirmRelation> CategoryFirmRelations { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PaymentAddress> PaymentAddresseses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategoryRelation> ProductCategoryRelations { get; set; }
        public DbSet<UserRoleRelations> UserRoleRelations { get; set; }
        public string DatabaseConnectionStringName { get; set; } = "ColosusMSSqlServerConnection";
        public DbSet<ProductFirmRelation> ProductFirmRelations { get; set; }
        public DbSet<FirmRole> FirmRoles { get; set; }
        public DbSet<FirmUserRelation> FirmUserRelations { get; set; }
        public DbSet<ProductStock> Stocks { get; set; }

        private readonly string _connectionString;

        /// <summary>
        /// Bağlantı için dolu.
        /// </summary>
        /// <param name="ConnectionString"></param>
        public SQLiteContext(string ConnectionString)
        {
            _connectionString = ConnectionString;
            this.Database = base.Database;
        }


        /// <summary>
        /// Migration için boş.
        /// </summary>
        public SQLiteContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        public void MigrateDb()
        {
            this.Database.Migrate();
        }

    }

}
