using Colosus.Business.Abstracts;
using Colosus.Business.Concretes;
using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Operations.Abstracts;
using Colosus.Sql.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Business
{
    public class Operations : IOperations
    {
        private List<DataBaseSetting> dbSettings { get; set; }
        public IContext Context;
        public Operations()
        {
  
        }

        public void AddDbSettings(List<DataBaseSetting> settings)
        {
            dbSettings = new();
            dbSettings.AddRange(settings);
            SetContext(dbSettings.FirstOrDefault(xd => xd.Active == true));
        }

        private void SetContext(DataBaseSetting settings)
        {
            if (settings == null)
                throw new Exception("Açık settings yok.");

            switch (settings.Type)
            {
                case EnumDbType.MsSql:
                    Context = new Colosus.Sql.MSSql.MSSqlContext(settings.Value);
                    break;
                case EnumDbType.SqlLite:
                    Context = new Colosus.Sql.SQLite.SQLiteContext(settings.Value);
                    break;
                default:
                    throw new Exception("Geçersiz veya tanımlanmamış bir DB tipi seçildi.");
            }
        }
        public User? GetUser(string userName, string password) => Context.Users.First(xd => xd.UserName == userName && xd.Password == password);
        public List<Role> GetUserRole(string privateKey) => (from urr in Context.UserRoleRelations
                                                             join r in Context.Roles on urr.RolePrivateKey equals r.PrivateKey
                                                             where urr.UserPrivateKey == privateKey
                                                             select r).ToList();
        private void SaveChanges() => Context.SaveChanges();
        public List<Firm> GetsMyFirms(string userPrivateKey) => (from fur in Context.FirmUserRelations
                                                                 join f in Context.Firms on fur.FirmPrivateKey equals f.PrivateKey
                                                                 where fur.UserPrivateKey == userPrivateKey
                                                                 select f).ToList();

        public Firm GetMyFirm(string userPrivateKey) => (from fur in Context.FirmUserRelations
                                                         join f in Context.Firms on fur.FirmPrivateKey equals f.PrivateKey
                                                         where fur.UserPrivateKey == userPrivateKey
                                                         select f).FirstOrDefault();
        public void SaveEntity(object entity)
        {
            var entityType = entity.GetType();
            var dbSetProperty = Context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.GenericTypeArguments.Contains(entityType));
            if (dbSetProperty != null)
            {
                var dbSet = dbSetProperty.GetValue(Context);
                var addMethod = dbSet.GetType().GetMethod("Add");
                addMethod.Invoke(dbSet, new[] { entity });
                SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Entity türü, geçerli bir DbSet ile eşleşmedi.");
            }
        }

        public bool DatabaseUpdate()
        {
            Context.MigrateDb();
            return true;
        }

        public List<Category> GetCategoriesWithPrivateKey(string FirmPrivateKey) => (from c in Context.Categories
                                                                                     join cfr in Context.CategoryFirmRelations on c.PrivateKey equals cfr.CategoryPrivateKey
                                                                                     where cfr.FirmPrivateKey == FirmPrivateKey
                                                                                     select c).ToList();


        public Category GetCategory(string categoryPublicKey) => Context.Categories.FirstOrDefault(xd => xd.PublicKey == categoryPublicKey);

        public List<Category> GetAllCategories() => Context.Categories.ToList();

        public List<Colosus.Entity.Concretes.DTO.ProductDTO> GetMyFirmProductDTOs(string firmPrivateKey) => (from pfr in Context.ProductFirmRelations
                                                                                                             join p in Context.Products on pfr.ProductPrivateKey equals p.PrivateKey
                                                                                                             join pcr in Context.ProductCategoryRelations on p.PrivateKey equals pcr.ProductPrivateKey
                                                                                                             join c in Context.Categories on pcr.CategoryPrivateKey equals c.PrivateKey
                                                                                                             join f in Context.Firms on pfr.FirmPrivateKey equals f.PrivateKey
                                                                                                             join s in Context.Stocks on p.PrivateKey equals s.ProductPrivateKey into stockGroup
                                                                                                             from s in stockGroup.DefaultIfEmpty()
                                                                                                             where pfr.FirmPrivateKey == firmPrivateKey
                                                                                                             group s by new
                                                                                                             {
                                                                                                                 p.PublicKey,
                                                                                                                 p.Name,
                                                                                                                 SalePrice = p.SalePrice,
                                                                                                                 PurchasePrice = p.PurchasePrice,
                                                                                                                 CategoryName = c.Name,
                                                                                                                 CategoryPublicKey = c.PublicKey,
                                                                                                                 FirmName = f.Name
                                                                                                             } into g
                                                                                                             select new Colosus.Entity.Concretes.DTO.ProductDTO()
                                                                                                             {
                                                                                                                 CategoryName = g.Key.CategoryName,
                                                                                                                 CategoryPublicKey = g.Key.CategoryPublicKey,
                                                                                                                 FirmName = g.Key.Name,
                                                                                                                 ProductCategoryRelationPublicKey = g.Key.PublicKey,
                                                                                                                 PublicKey = g.Key.PublicKey,
                                                                                                                 SalePrice = g.Key.SalePrice,
                                                                                                                 PurchasePrice = g.Key.PurchasePrice,
                                                                                                                 Stock = g.Sum(x => x.Amount),
                                                                                                                 Name = g.Key.Name,
                                                                                                             }).ToList();

        public Product GetMyProduct(string productPublicKey) => Context.Products.FirstOrDefault(xd => xd.PublicKey == productPublicKey);

        public Entity.Concretes.DTO.ProductDTO? GetMyProductDTOs(string productPrivateKey) => (from p in Context.Products
                                                                                               join pcr in Context.ProductCategoryRelations on p.PrivateKey equals pcr.ProductPrivateKey
                                                                                               join pfr in Context.ProductFirmRelations on p.PrivateKey equals pfr.ProductPrivateKey
                                                                                               join f in Context.Firms on pfr.FirmPrivateKey equals f.PrivateKey
                                                                                               join c in Context.Categories on pcr.CategoryPrivateKey equals c.PrivateKey
                                                                                               join s in Context.Stocks on p.PrivateKey equals s.ProductPrivateKey into stockGroup
                                                                                               from s in stockGroup.DefaultIfEmpty()
                                                                                               where p.PrivateKey == productPrivateKey
                                                                                               group s by new
                                                                                               {
                                                                                                   p.PublicKey,
                                                                                                   p.Name,
                                                                                                   SalePrice = p.SalePrice,
                                                                                                   PurchasePrice = p.PurchasePrice,
                                                                                                   CategoryName = c.Name,
                                                                                                   CategoryPublicKey = c.PublicKey,
                                                                                                   FirmName = f.Name
                                                                                               } into g
                                                                                               select new Colosus.Entity.Concretes.DTO.ProductDTO()
                                                                                               {
                                                                                                   CategoryName = g.Key.CategoryName,
                                                                                                   CategoryPublicKey = g.Key.CategoryPublicKey,
                                                                                                   FirmName = g.Key.Name,
                                                                                                   ProductCategoryRelationPublicKey = g.Key.PublicKey,
                                                                                                   PublicKey = g.Key.PublicKey,
                                                                                                   SalePrice = g.Key.SalePrice,
                                                                                                   PurchasePrice = g.Key.PurchasePrice,
                                                                                                   Stock = g.Sum(x => x.Amount),
                                                                                                   Name = g.Key.Name,
                                                                                               }).SingleOrDefault();

        public List<Entity.Concretes.DTO.ProductStockDTO> GetProductStockHistoryDTOs(string productPublicKey) => (from p in Context.Products
                                                                                                                  join s in Context.Stocks on p.PrivateKey equals s.ProductPrivateKey
                                                                                                                  join u in Context.Users on s.UserPrivateKey equals u.PrivateKey
                                                                                                                  where p.PublicKey == productPublicKey
                                                                                                                  select new Colosus.Entity.Concretes.DTO.ProductStockDTO()
                                                                                                                  {
                                                                                                                      PublicKey = s.PublicKey,
                                                                                                                      Amount = s.Amount,
                                                                                                                      CreateDate = s.CreateDate,
                                                                                                                      Description = s.Description,
                                                                                                                      UserFirstAndLastName = $"{u.FirstName} {u.LastName}",
                                                                                                                      UserPublicKey = u.PublicKey
                                                                                                                  }).ToList();

        public List<ProductStock> GetProductStocks(string ProductPrivateKey) => Context.Stocks.Where(xd => xd.ProductPrivateKey == ProductPrivateKey).ToList();
        public void RemoveEntity(object entity)
        {
            var entityType = entity.GetType();
            var dbSetProperty = Context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.GenericTypeArguments.Contains(entityType));

            if (dbSetProperty != null)
            {
                var dbSet = dbSetProperty.GetValue(Context);
                var removeMethod = dbSet.GetType().GetMethod("Remove");
                removeMethod.Invoke(dbSet, new[] { entity });
                SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Entity türü, geçerli bir DbSet ile eşleşmedi.");
            }
        }


        public Firm GetMyFirmWithFirmPublicKey(string firmpublicKey) => Context.Firms.FirstOrDefault(xd => xd.PublicKey == firmpublicKey);

        public List<IndividualCustomer> GetMyFirmIndividualCustomersWithFirmPrivateKey(string firmPrivateKey) => (from cfr in Context.CustomerFirmRelations
                                                                                                                  join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
                                                                                                                  join c in Context.IndividualCustomers on cfr.CustomerPrivateKey equals c.PrivateKey
                                                                                                                  where f.PrivateKey == firmPrivateKey
                                                                                                                  select c).ToList();

        public List<CorporateCustomer> GetMyFirmCorporateCustomersWithFirmPrivateKey(string firmPrivateKey) => (from cfr in Context.CustomerFirmRelations
                                                                                                                join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
                                                                                                                join c in Context.CorporateCustomers on cfr.CustomerPrivateKey equals c.PrivateKey
                                                                                                                where f.PrivateKey == firmPrivateKey
                                                                                                                select c).ToList();

        public List<FastCustomer> GetMyFirmFastCustomersWithFirmPrivateKey(string firmPrivateKey) => (from cfr in Context.CustomerFirmRelations
                                                                                                      join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
                                                                                                      join c in Context.FastCustomers on cfr.CustomerPrivateKey equals c.PrivateKey
                                                                                                      where f.PrivateKey == firmPrivateKey
                                                                                                      select c).ToList();

        public CustomersDTO GetMyFirmCustomersWithPrivateKey(string firmPrivateKey) =>
            new CustomersDTO()
            {
                corporateCustomers = GetMyFirmCorporateCustomersWithFirmPrivateKey(firmPrivateKey),
                individualCustomers = GetMyFirmIndividualCustomersWithFirmPrivateKey(firmPrivateKey),
                fastCustomers = GetMyFirmFastCustomersWithFirmPrivateKey(firmPrivateKey)
            };

        public List<Debt> GetsDebitWithCustomerKey(string customerKey) => Context.Debts.Where(xd => xd.CustomerKey == customerKey).ToList();

        public ICustomer GetICustomerFromCustomerPublicKey(string customerPublicKey)
        {
            ICustomer customer = Context.CorporateCustomers.FirstOrDefault(xd => xd.PublicKey == customerPublicKey);
            if (customer == null)
                customer = Context.IndividualCustomers.FirstOrDefault(xd => xd.PublicKey == customerPublicKey);
            return customer;
        }

        public Debt GetDebt(string debtPublicKey) => Context.Debts.FirstOrDefault(xd => xd.PublicKey == debtPublicKey);

        public void UpdateEntity(object entity)
        {
            var entityType = entity.GetType();
            var dbSetProperty = Context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.GenericTypeArguments.Contains(entityType));

            if (dbSetProperty != null)
            {
                var dbSet = dbSetProperty.GetValue(Context);
                var updateMethod = dbSet.GetType().GetMethod("Update");
                updateMethod.Invoke(dbSet, new[] { entity });
                SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Entity türü, geçerli bir DbSet ile eşleşmedi.");
            }
        }

        public PaymentTypeFirmRelation? GetCurrencyFirmRelation(string paymentTypePublicKey, string firmPublicKey)
        => (from ptfr in Context.PaymentTypeFirmRelations
            join f in Context.Firms on ptfr.FirmPrivateKey equals f.PrivateKey
            join pt in Context.PaymentTypes on ptfr.PaymentTypePrivateKey equals pt.PrivateKey
            where (f.PublicKey == firmPublicKey && pt.PublicKey == paymentTypePublicKey)
            select ptfr).FirstOrDefault();

        public PaymentType GetPaymentType(string paymentTypePublicKey)
            => Context.PaymentTypes.FirstOrDefault(xd => xd.PublicKey == paymentTypePublicKey);

        public List<PaymentType> GetAllPaymentTypeWithFirmPrivateKey(string firmPrivateKey)
            => (from ptfr in Context.PaymentTypeFirmRelations
                join f in Context.Firms on ptfr.FirmPrivateKey equals f.PrivateKey
                join pt in Context.PaymentTypes on ptfr.PaymentTypePrivateKey equals pt.PrivateKey
                where f.PrivateKey == firmPrivateKey
                select pt).ToList();

        public List<Product> GetMyProductWithCategoryPrivateKey(string CategoryPrivateKey)
       => (from p in Context.Products
           join pcr in Context.ProductCategoryRelations on p.PrivateKey equals pcr.ProductPrivateKey
           join c in Context.Categories on pcr.CategoryPrivateKey equals c.PrivateKey
           where c.PrivateKey == CategoryPrivateKey
           select p).ToList();

        public List<PaymentType> RecommendedPaymentType()
        => Context.PaymentTypes.Where(xd => xd.PrivateKey.Contains("All")).ToList();

        public List<Currency> RecommendedCurrency()
        => Context.Currencies.Where(xd => xd.PrivateKey.Contains("All")).ToList();

        public Currency GetCurrency(string currencyPublicKey)
            => Context.Currencies.FirstOrDefault(xd => xd.PublicKey == currencyPublicKey);


        public List<Currency> GetAllCurrencyWithFirmPrivateKey(string firmPrivateKey)
            => (from cfr in Context.CurrencyFirmRelations
                join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
                join c in Context.Currencies on cfr.CurrencyPrivateKey equals c.PrivateKey
                where f.PrivateKey == firmPrivateKey
                select c).ToList();

        public List<Colosus.Entity.Concretes.DTO.DebtPayDTO> GetDebtPayWithDebtPrivateKey(string privateKey)
        => (from dp in Context.DebtPays
            join c in Context.Currencies on dp.CurrencyPrivateKey equals c.PrivateKey
            join p in Context.PaymentTypes on dp.PaymentTypePrivateKey equals p.PrivateKey
            where dp.DebtPrivateKey == privateKey
            select new Colosus.Entity.Concretes.DTO.DebtPayDTO()
            {
                CreateDate = dp.CreateDate,
                Price = dp.Price,
                PublicKey = dp.PublicKey,
                CurrencyName = c.Name,
                PaymentTypeName = p.Name
            }).ToList();

        public ProductStock GetProductStockWithPublicKey(string stockPublicKey)
            => Context.Stocks.FirstOrDefault(xd => xd.PublicKey == stockPublicKey);

        public DebtPay GetDebtPayWithPublicKey(string debtPayPublicKey)
            => Context.DebtPays.FirstOrDefault(xd => xd.PublicKey == debtPayPublicKey);

        public List<ProductDTO> GetMyProductsDTOsWithFirmPrivateKey(string firmPrivateKey)
   => (from p in Context.Products
       join pfr in Context.ProductFirmRelations on p.PrivateKey equals pfr.ProductPrivateKey
       join f in Context.Firms on pfr.FirmPrivateKey equals f.PrivateKey
       join pcr in Context.ProductCategoryRelations on p.PrivateKey equals pcr.ProductPrivateKey
       join c in Context.Categories on pcr.CategoryPrivateKey equals c.PrivateKey
       join s in Context.Stocks on p.PrivateKey equals s.ProductPrivateKey into stockGroup
       from s in stockGroup.DefaultIfEmpty()
       where f.PrivateKey == firmPrivateKey
       select new ProductDTO()
       {
           FirmName = f.Name,
           Name = p.Name,
           PublicKey = p.PublicKey,
           SalePrice = p.SalePrice,
           PurchasePrice = p.PurchasePrice,
           CategoryName = c.Name,
           CategoryPublicKey = c.PublicKey,
           ProductCategoryRelationPublicKey = pcr.PublicKey,
           Stock = s == null ? 0 : s.Amount == null ? 0 : s.Amount
       }).ToList();

        public CustomersDTO GetMyFirmCustomersForFastOps(string firmPrivateKey)
                    => new Entity.Concretes.DTO.CustomersDTO()
                    {
                        corporateCustomers = GetMyFirmCorporateCustomersForFastOps(firmPrivateKey),
                        individualCustomers = GetMyFirmIndividualCustomersForFastOps(firmPrivateKey),
                        fastCustomers = GetMyFirmFastCustomersForFastOps(firmPrivateKey)
                    };

        public List<FastCustomer> GetMyFirmFastCustomersForFastOps(string firmPrivateKey)
            => (from cfr in Context.CustomerFirmRelations
                join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
                join c in Context.FastCustomers on cfr.CustomerPrivateKey equals c.PrivateKey
                where f.PrivateKey == firmPrivateKey
                select c).ToList();
        public List<IndividualCustomer> GetMyFirmIndividualCustomersForFastOps(string firmPrivateKey)
            => (from cfr in Context.CustomerFirmRelations
                join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
                join c in Context.IndividualCustomers on cfr.CustomerPrivateKey equals c.PrivateKey
                where f.PrivateKey == firmPrivateKey && c.VisibleFastOperation
                select c).ToList();
        public List<CorporateCustomer> GetMyFirmCorporateCustomersForFastOps(string firmPrivateKey)
         => (from cfr in Context.CustomerFirmRelations
             join f in Context.Firms on cfr.FirmPrivateKey equals f.PrivateKey
             join c in Context.CorporateCustomers on cfr.CustomerPrivateKey equals c.PrivateKey
             where f.PrivateKey == firmPrivateKey && c.VisibleFastOperation
             select c).ToList();

        public ICustomer GetMyFirmCustomerWithCustomerKey(string CustomerKey)
        {
            var ccustomer = Context.CorporateCustomers.FirstOrDefault(xd => xd.CustomerKey == CustomerKey);
            var icustomer = Context.IndividualCustomers.FirstOrDefault(xd => xd.CustomerKey == CustomerKey);
            var fcustomer = Context.FastCustomers.FirstOrDefault(xd => xd.CustomerKey == CustomerKey);

            if (ccustomer != null)
                return ccustomer;

            if (icustomer != null)
                return icustomer;

            if (fcustomer != null)
                return fcustomer;

            return null;
        }

        public List<ICustomer> GetMyFirmSaleCustomersWithFirmPrivateKey(string privateKey)
        {
            List<ICustomer> returnedList = new();
            returnedList.AddRange((from cc in Context.CorporateCustomers
                                   join cfr in Context.CustomerFirmRelations on cc.PrivateKey equals cfr.CustomerPrivateKey
                                   where cfr.FirmPrivateKey == privateKey && cc.VisibleFastOperation
                                   select cc).ToList());

            returnedList.AddRange((from fc in Context.FastCustomers
                              join cfr in Context.CustomerFirmRelations on fc.PrivateKey equals cfr.CustomerPrivateKey
                              where cfr.FirmPrivateKey == privateKey
                              select fc).ToList());


            returnedList.AddRange((from ic in Context.IndividualCustomers
                              join cfr in Context.CustomerFirmRelations on ic.PrivateKey equals cfr.CustomerPrivateKey
                              where cfr.FirmPrivateKey == privateKey && ic.VisibleFastOperation
                              select ic).ToList());

            return returnedList;
        }

        public ICustomer GetMyFirmCustomersWithPublicKey(string customerPublicKey)
        {
            var ccustomer = Context.CorporateCustomers.FirstOrDefault(xd => xd.PublicKey == customerPublicKey);
            var icustomer = Context.IndividualCustomers.FirstOrDefault(xd => xd.PublicKey == customerPublicKey);
            var fcustomer = Context.FastCustomers.FirstOrDefault(xd => xd.PublicKey == customerPublicKey);

            if (ccustomer != null)
                return ccustomer;

            if (icustomer != null)
                return icustomer;

            if (fcustomer != null)
                return fcustomer;

            return null;
        }
    }
}
