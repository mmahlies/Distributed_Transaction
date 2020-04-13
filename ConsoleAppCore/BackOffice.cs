﻿using BackOffice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using System.Threading.Tasks;

namespace BackOffice
{
    public class BackOffice
    {
        public static void Main(string[] args)
        {

            //using (TransactionScope inner2Scope = new TransactionScope())
            //{                
            //    BackOfficeDBContext dbContextNet = new BackOfficeDBContext();
            //    BackOfficeDBContext2.BackOfficeDBContext2 BackOfficeDBContext2 = new BackOfficeDBContext2.BackOfficeDBContext2();
            //    BackOfficeDBContext2.BackOfficeDBContext2 dbContextNet2 = new BackOfficeDBContext2.BackOfficeDBContext2();
            //    //  string sessionToken = GetSessionTokenFromAdo();
            //    string sessionToken1 = GetSessionTokenFromDbContext(dbContextNet);
            //    string sessionToken2 = GetSessionTokenFromDbContext(dbContextNet2);

            //    dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
            //    dbContextNet.BulkSaveChanges();

            //    inner2Scope.Complete();
            //}
        }


        public async Task<bool> Logic(DbContext generalDbContextNet)
        {
            BackOfficeDBContext dbContextNet = (BackOfficeDBContext)generalDbContextNet;
            //BackOfficeDBContext dbContextNet = (BackOfficeDBContext)generalDbContextNet;       
            try
            {
                using (TransactionScope innerScope1 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5), TransactionScopeAsyncFlowOption.Enabled))
                {
                    dbContextNet.Teacher.Add(new Teacher() { Name = "backoffice  root1" });
                    dbContextNet.BulkSaveChanges();

                    await AsyncTask(dbContextNet);

                    innerScope1.Complete();
                    //     dbContextNet.RollBack();
                }

                //using (TransactionScope innerScope1 = new TransactionScope())
                //{
                //    var x5 = Transaction.Current?.TransactionInformation?.LocalIdentifier;
                //    BackOfficeDBContext dbContextNet1 = new BackOfficeDBContext();
                //    dbContextNet1.Teacher.Add(new Teacher() { Name = "Teacher root bulk save changes" });
                //    dbContextNet1.BulkSaveChanges();
                //    innerScope1.Complete();
                //}


                return true;

            }
            catch (Exception ex)
            {
                // ex handling 
                throw ex;
                var x = "";

                //  dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession NULL");

                return false;
                //    throw;
            }

        }

        private async static Task AsyncTask(BackOfficeDBContext dbContextNet)
        {
        
                dbContextNet.Teacher.Add(new Teacher() { Name = "backoffice nested from task" });
                //  dbContextNet.SaveChanges();
                //  var teas3 = dbContextNet.Teacher.ToList();

                //inner2Scope.RollBack(dbContextNet);
                await dbContextNet.SaveChangesAsync();
               // inner2Scope.Complete();
               await Task.Delay(500);
            
        }

        private static void BindSessionToken(string token, BackOfficeDBContext dbContextNet)
        {
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"EXEC sp_bindsession '{token}'";
            dbContextNet.Database.OpenConnection();
            var result = command.ExecuteNonQuery();
        }

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }

        static string GetSessionTokenFromDbContext(DbContext db)
        {
            var x = db.Database.GetDbConnection();
            var xx = db.Database.CanConnect();
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
                db.Database.CloseConnection();
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
            }
            return result;
        }


    }

    class DtcValues
    {
        public byte[] Token { get; set; }
        public string Sql { get; set; }
    }
}
