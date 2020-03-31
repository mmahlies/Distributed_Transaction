using System;
using System.Collections.Generic;
using System.Text;

namespace BackOffice
{
    public class SqlAggregate
    {
        static StringBuilder _sqlText;
        public static StringBuilder SqlText { get{
                if (_sqlText == null)
                {
                    _sqlText = new StringBuilder();
                }
                return _sqlText;
} set { } }


    }
}
