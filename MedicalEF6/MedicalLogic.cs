using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MedicalEF6
{
    public class MedicalLogic
    {
        public static void Logic()
        {
            MedicalContext dbContextNet = new MedicalContext();
            try
            {
                using (TransactionScope innerScope1 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5), TransactionScopeAsyncFlowOption.Enabled))
                {
                    dbContextNet.Student.Add(new Student() { Name = "student from medical" });
                    dbContextNet.SaveChanges();                   
                    innerScope1.Complete();
                //     dbContextNet.RollBack();
                }



                var x = "";

            }
            catch (Exception ex)
            {
                // ex handling 
                throw ex;
                var x = "";

                //  dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession NULL");

             //   return false;
                //    throw;
            }
        }
    }
}
