using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleAppNet
{

    public class DbContextNet1 : DbContext
    {

        public DbSet<Student> Student { get; set; }

      
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {           
           // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet1");
            options.UseSqlServer("Server=.;Database=Medical;User Id=sa;Password=sasa;");
            options.AddInterceptors(new Interceptor());
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
