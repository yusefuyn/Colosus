using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Colosus.MSSql
{
    public class MSSqlContext : BasedDbContext, IContext
    {
        public DbSet<User> Users { get; set;  }
        public DbSet<Product> Products { get; set;  }
        public DbSet<Firm> Firms { get; set;  }
        public DatabaseFacade Database { get; set; }
        public string DatabaseConnectionStringName { get; set; } = "ColosusMSSqlServerConnection";
        private readonly string _connectionString;
        public MSSqlContext(string ConnectionString)
        {
            _connectionString = ConnectionString;
            this.Database = base.Database;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public void MigrateDb()
        {
            this.Database.Migrate();
        }
    }
}
