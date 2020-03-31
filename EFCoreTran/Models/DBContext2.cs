using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTran.Models
{
  public  class DBContext2 : DbContext
    {
        public DbSet<Persons> Persons { get; set; }

        public DBContext2(DbContextOptions<DBContext2> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Data Source=.;Initial Catalog=db2;Integrated Security=True; ");
    }

    public class Persons
    {
        public int PersonsId { get; set; }
        public string Name { get; set; }        
    }
}
