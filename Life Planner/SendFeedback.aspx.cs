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

namespace Life_Planner
{
    public partial class Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //hardcoded for now
            submittedBy.Text = "Nurha";
            //txtAuthor.Text = Session["username"].ToString();
            string today = DateTime.Now.ToString();
            txtDatetime.Text = today;
        }

        protected string getAccID()
        {
            //hardcoded for now
            String fName = "Brenda";

            //String acctName = Session["username"].ToString();

            String accountID;
            SqlConnection con2 = new DBManager().getConnection();

            string sql2 = "SELECT [accountID] FROM [CZ2006 - Life Planner].[dbo].[Account] WHERE fName = @fName;";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            cmd2.Parameters.AddWithValue("@fName", fName);
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            if (firstColumn != null)
            {
                accountID = firstColumn.ToString();
            }
            else
            {
                return "Is a Null";
            }
            cmd2.ExecuteNonQuery();
            con2.Close();
            return accountID;
        }

        protected void submitFeedback_Click(object sender, EventArgs e)
        {
            feedbackACK.Text = "Thank you for your feedback! We will try our best to resolve your issues.";
            
            DateTime feedbackDateTime = DateTime.Now;
            String feedbackIssue = txtfeedbackIssue.Text;
            String feedbackContent = txtFeedbackContent.Text;
            String submittedBy = getAccID();

            SqlConnection con1 = new DBManager().getConnection();

            string sql0 = "SET IDENTITY_INSERT ID ON";
            string sql1 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Feedback] (feedbackIssue, feedbackContent, feedbackDateTime, submittedBy) VALUES (@feedbackIssue, @feedbackContent, @feedbackDateTime, @submittedBy);";
            SqlCommand cmd1 = new SqlCommand(sql1, con1);
            SqlCommand cmd0 = new SqlCommand(sql0, con1);
            cmd1.Parameters.AddWithValue("@feedbackIssue", feedbackIssue);
            cmd1.Parameters.AddWithValue("@feedbackContent", feedbackContent);
            cmd1.Parameters.AddWithValue("@feedbackDateTime", feedbackDateTime);
            cmd1.Parameters.AddWithValue("@submittedBy", submittedBy);
            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();

            txtfeedbackIssue.Text = string.Empty;
            txtFeedbackContent.Text = string.Empty;
        }

        protected void clearBtn_Click(object sender, EventArgs e)
        {
            txtfeedbackIssue.Text = string.Empty;
            txtFeedbackContent.Text = string.Empty;
        }
    }
}