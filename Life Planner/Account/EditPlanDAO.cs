using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Life_Planner.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public class EditPlanDAO
    {
        public string getSchID(string schName)
        {
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT id FROM dbo.Schools WHERE school_name=@school_name";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@school_name", schName);
            con.Open();
            string schID = cmd.ExecuteScalar().ToString();
            con.Close();
            return schID;

        }
        
    }
}