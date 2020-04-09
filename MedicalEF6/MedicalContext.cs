using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalEF6
{
    public class MedicalContext : DbContext
    {
        public MedicalContext(): base("Server=.;Database=Medical;User Id=sa;Password=sasa;")
        {
        }
        public DbSet<Student> Student { get; set; }
        //public DbSet<SessionToken> SessionToken { get; set; }


     
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class SessionToken
    {
        public String Token { get; set; }
    }
}
