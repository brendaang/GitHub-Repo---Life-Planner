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

        public void executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = getConnection();
                myCommand.Connection.Open();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeInsertQuery - Query: " + _query + " \nException: \n" + e.StackTrace.ToString());
                //return false;
            }
            finally
            {
                myCommand.Connection.Close();
            }
            //return true;
        }
    }
}