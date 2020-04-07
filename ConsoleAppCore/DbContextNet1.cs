using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BackOffice
{

    public class BackOfficeDBContext : DbContext
    {
        public BackOfficeDBContext()
        {

        }


        public DbSet<School> School { get; set; }
        public DbSet<Teacher> Teacher { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {

        });
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet2");            
            options.UseSqlServer("Server=.;Database=BackOffice;User Id=sa;Password=sasa;");

        }


    }

    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
