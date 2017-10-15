using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Life_Planner.Data
{
    public class DBManager
    {
        public SqlConnection getConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life PlannerConnectionString"].ConnectionString);
        }
    }
}