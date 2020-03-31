using System;
using System.Collections;
using System.Transactions;
using System.Web;

namespace ClassLibrary3Core
{
    public class Class1
    {
        public void test() {
            using (TransactionScope scope = new TransactionScope())
            {

                var query = HttpUtility.ParseQueryString(string.Empty);
            }
        }
        

    }
}
