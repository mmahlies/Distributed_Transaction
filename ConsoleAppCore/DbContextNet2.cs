
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice
{

    public class DbContextNet2 : DbContext
    {

     //   public DbSet<School> School { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet2").AddInterceptors(new Interceptor()).AddInterceptors(new DbTransactionInterceptor1());


    }

    //public class School
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
