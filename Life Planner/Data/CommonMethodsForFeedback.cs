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

namespace Life_Planner.Data
{
    public class CommonMethodsForFeedback
    {
        public string getAccID(string acctName)
        {
            String accountID;
            SqlConnection con2 = new DBManager().getConnection();
            string sql2 = "SELECT accountID FROM dbo.Account WHERE accountID IN (SELECT accountID FROM dbo.AccCreds WHERE username = @acctName);";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            cmd2.Parameters.AddWithValue("@acctName", acctName);
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            accountID = firstColumn.ToString();
            cmd2.ExecuteNonQuery();
            con2.Close();
            return accountID;
        }

        public string getTheName(string acctName)
        {
            String firstName;
            SqlConnection con2 = new DBManager().getConnection();

            string sql2 = "SELECT fName FROM dbo.Account WHERE accountID = @accountID";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);

            cmd2.Parameters.AddWithValue("@accountID", getAccID(acctName));
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            firstName = firstColumn.ToString();
            cmd2.ExecuteNonQuery();
            con2.Close();
            return firstName;
        }

        public string getNameOpen(string acct_Name)
        {
            String firstName;
            SqlConnection con2 = new DBManager().getConnection();

            string sql2 = "SELECT fname FROM dbo.Account WHERE accountID IN (SELECT accountID FROM dbo.AccCreds WHERE username = @acctName);";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);

            cmd2.Parameters.AddWithValue("@acctName", acct_Name);
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            firstName = firstColumn.ToString();
            cmd2.ExecuteNonQuery();
            con2.Close();
            return firstName;
        }

        public string getName(string feedback_ID)
        {
            String firstName;
            SqlConnection con2 = new DBManager().getConnection();

            string sql2 = "SELECT fName FROM dbo.Account WHERE accountID IN (SELECT resolvedBy FROM dbo.ResolveFeedback WHERE feedbackID = @feedbackID)";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            cmd2.Parameters.AddWithValue("@feedbackID", feedback_ID);
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            firstName = firstColumn.ToString();
            cmd2.ExecuteNonQuery();
            con2.Close();
            return firstName;
        }
    }
}