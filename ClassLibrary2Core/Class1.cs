using Dump;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Transactions;
using System.Web;

namespace ClassLibrary2Core
{
    public class Class1
    {
        public static void Main(string[] args)
        {

            using (TransactionScope inner2Scope = new TransactionScope())
            {
                DumpDBContext dbContextNet = new DumpDBContext();
              DumpDBContext2 dbContextNet2 = new DumpDBContext2();
          //  string sessionToken = GetSessionTokenFromAdo();
                dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
                dbContextNet.SaveChanges();
                string sessionToken1 = GetSessionTokenFromDbContext(dbContextNet);
                string sessionToken2 = GetSessionTokenFromDbContext(dbContextNet2);

                
                inner2Scope.Complete();
            }

        }


      
        static string GetSessionTokenFromDbContext(DbContext db)
        {
            string res = string.Empty;
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"DECLARE @bind_token varchar(255);
                                                                        EXECUTE sp_getbindtoken @bind_token OUTPUT;
                                                                        SELECT @bind_token AS Token; ";
                command.CommandType = CommandType.Text;         
                db.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        res = result.GetString(0);
                    }
                }
              //  db.Database.CloseConnection();
            }
            return res;
        }

        private static string GetSessionTokenFromAdo()
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection("Data Source=.;User Id=sa;Password=sasa;Initial Catalog=BackOffice"))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"DECLARE @bind_token varchar(255);  
                                EXECUTE sp_getbindtoken @bind_token OUTPUT;  
                                SELECT @bind_token AS Token;";
                connection.Open();
             
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    {
                        if (reader.Read())
                        {

                            result = reader.GetString(0);
                            reader.Close();
                        }
                    }
                }
                connection.Close();
            }
            
            return result;
        }
    }
}
