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
    public partial class CreateThread : System.Web.UI.Page
    {
        protected string getAccID()
        {
            //hardcoded for now
            String acctName = "tingle";

            //String acctName = Session["username"].ToString();

            String accID;
            SqlConnection con1 = new DBManager().getConnection();
            string sql1 = "SELECT [accID] FROM [CZ2006 - Life Planner].[dbo].[Account] WHERE userName = @userName;";
            SqlCommand cmd1 = new SqlCommand(sql1, con1);
            cmd1.Parameters.AddWithValue("@userName", acctName);
            con1.Open();
            accID = cmd1.ExecuteScalar().ToString();
            cmd1.ExecuteNonQuery();
            con1.Close();

            return accID;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //hardcoded for now
            txtAuthor.Text = "tingle";
            //txtAuthor.Text = Session["username"].ToString();
            string today = DateTime.Now.ToString();
            txtDate.Text = today;
        }

        protected void btnCreateThread_Click(object sender, EventArgs e)
        {
            String threadName = txtThreadName.Text;
            String threadDesc = txtThreadDesc.Text;
            String acctName = txtAuthor.Text;
            DateTime dateTime = DateTime.Now;
            String accID = getAccID();


            SqlConnection con3 = new DBManager().getConnection();

            string sql3 =
                "INSERT INTO [CZ2006 - Life Planner].[dbo].[Threads] (threadName, threadDesc, accID, dateCreated) VALUES (@threadName, @threadDesc, @accID, @dateTime);";
            SqlCommand cmd3 = new SqlCommand(sql3, con3);
            cmd3.Parameters.AddWithValue("@threadName", threadName);
            cmd3.Parameters.AddWithValue("@threadDesc", threadDesc);
            cmd3.Parameters.AddWithValue("@accID", accID);
            cmd3.Parameters.AddWithValue("@dateTime", dateTime);
            con3.Open();
            cmd3.ExecuteNonQuery();
            con3.Close();

            Response.Redirect("Forum.aspx");
        }
    }
}