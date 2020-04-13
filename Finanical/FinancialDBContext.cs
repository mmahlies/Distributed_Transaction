using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Financial
{
  public  class FinancialDBContext : DbContext
    {

        public FinancialDBContext()
        {
            var result = Z.EntityFramework.Extensions.LicenseManager.ValidateLicense(Z.BulkOperations.ProviderType.SqlServer);
        }

        public DbSet<tb_Financial> tb_Financial { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet2");            
            options.UseSqlServer("Server=.;Database=Financial;User Id=sa;Password=sasa;");
        }
    }

    public class tb_Financial
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
