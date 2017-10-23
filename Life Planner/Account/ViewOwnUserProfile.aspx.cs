using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Life_Planner.Data;


namespace Life_Planner.Account
{
    public partial class ViewOwnUserProfile : System.Web.UI.Page


    {

        
        protected void Page_Load(object sender, EventArgs e)
        {

            string myValue = (string)Session["accountID"];
            Response.Write(myValue);
            
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT *  FROM[CZ2006 - Life Planner].[dbo].[Account]";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();

   
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
           fname.Text = reader["fname"].ToString();
          reader.Read();
            lname.Text = reader["lname"].ToString();
            reader.Read();
            //email.Text = reader["email"].ToString();
 

            //reader.Read();
            //birthdate.Text = reader["birthdate"].ToString();
            //reader.Read();
           
            
            //reader.Close();
            //con.Close();

        }
    }
}