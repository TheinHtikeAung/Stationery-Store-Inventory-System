using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ApplicationLayer.Utils
{
    public class Factory
    {

        public static DataContext getDataContext()
        {
            return new DataContext();
        }
    }
}
