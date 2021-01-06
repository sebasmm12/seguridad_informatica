using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class BaseDA
    {
        protected String ConnectionString;

        public BaseDA()
        {
            ConnectionString = Config.GetConnectionString();
        }
    }
}
