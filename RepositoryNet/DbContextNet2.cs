
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTC
{

    public class DbContextNet2 : DbContext
    {
     
        public DbSet<School> School { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=.;Database=BackOffice;User Id=sa;Password=sasa;");
    }

    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
