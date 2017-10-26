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
    public partial class ResolveFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string feedbackID = (string)(Session["Feedback_ID"]);
            SqlConnection con = new DBManager().getConnection();

            string sql2 = "SELECT feedbackDatetime, feedbackIssue, feedbackContent, submittedBy, feedbackStatus FROM dbo.Feedback WHERE @feedbackID = feedbackID";
            SqlCommand cmd2 = new SqlCommand(sql2, con);

            cmd2.Parameters.AddWithValue("@feedbackID", feedbackID);
            con.Open();
            SqlDataReader reader = cmd2.ExecuteReader();
            reader.Read();
            fbkDatetime.Text = reader["feedbackDatetime"].ToString();
            txtfeedbackIssue.Text = reader["feedbackIssue"].ToString();
            fbkContent.Text = reader["feedbackContent"].ToString();
            resolvedBy.Text = "Nurha";

            string today = DateTime.Now.ToString();
            resolvedOn.Text = today;

            reader.Close();
            con.Close();
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewFeedback.aspx");
        }

        protected void resolvedBtn_Click(object sender, EventArgs e)
        {
            DateTime resolvedOn = DateTime.Now;
            string feedback_ID = (string)(Session["Feedback_ID"]);
            String resolvedBy = getAccID();
            String resolvedNotes = txtAddnotes.Text;

            SqlConnection con = new DBManager().getConnection();

            string sql0 = "SET IDENTITY_INSERT ID ON";
            string sql3 = "SET XACT_ABORT ON";
            string sql4 = "BEGIN TRANSACTION";
            string sql1 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[ResolveFeedback] (feedbackID, resolvedBy, resolvedOn, feedbackStatus, resolvedNotes) VALUES (@feedbackID, @resolvedBy, @resolvedOn, @feedbackStatus, @resolvedNotes);";
            string sql2 = "UPDATE dbo.Feedback SET feedbackStatus = @feedbackStatus  WHERE feedbackID = @feedbackID";
            string sql5 = "COMMIT TRANSACTION";

            SqlCommand cmd0 = new SqlCommand(sql0, con);
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            SqlCommand cmd4 = new SqlCommand(sql4, con);
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlCommand cmd5 = new SqlCommand(sql5, con);

            cmd1.Parameters.AddWithValue("@feedbackID", feedback_ID);
            cmd1.Parameters.AddWithValue("@resolvedBy", resolvedBy);
            cmd1.Parameters.AddWithValue("@resolvedOn", resolvedOn);
            cmd1.Parameters.AddWithValue("@feedbackStatus", DropDownListStatus.SelectedValue);
            cmd1.Parameters.AddWithValue("@resolvedNotes", resolvedNotes);
            cmd2.Parameters.AddWithValue("@feedbackID", feedback_ID);
            cmd2.Parameters.AddWithValue("@feedbackStatus", DropDownListStatus.SelectedValue);

            con.Open();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            con.Close();

            alert_placeholder.Visible = true;
            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
            resolveACK.Text = "Your resolved to this feedback has been saved.";
        }

        protected string getAccID()
        {
            String fName = "Nurha";

            //String acctName = Session["username"].ToString();

            String accountID;
            SqlConnection con2 = new DBManager().getConnection();

            string sql2 = "SELECT [accountID] FROM [CZ2006 - Life Planner].[dbo].[Account] WHERE fName = @fName;";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            cmd2.Parameters.AddWithValue("@fName", fName);
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            accountID = firstColumn.ToString();
            cmd2.ExecuteNonQuery();
            con2.Close();
            return accountID;
        }

        protected string getFeeebackStatus()
        {
            string feedbackID = (string)(Session["Feedback_ID"]);
            String feedbackStatus;
            SqlConnection con2 = new DBManager().getConnection();

            string sql2 = "SELECT feedbackStatus FROM dbo.Feedback WHERE @feedbackID = feedbackID";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);

            cmd2.Parameters.AddWithValue("@feedbackID", feedbackID);
            con2.Open();
            var firstColumn = cmd2.ExecuteScalar();
            feedbackStatus = firstColumn.ToString();
            cmd2.ExecuteNonQuery();
            con2.Close();
            return feedbackStatus;
        }
    }
}