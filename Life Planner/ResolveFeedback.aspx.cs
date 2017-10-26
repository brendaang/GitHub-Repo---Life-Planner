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

            //string sql2 = "SELECT t.[feedbackID],t.[feedbackDatetime],t.[feedbackIssue],t.[feedbackContent], t.[submittedBy], t.[feedbackStatus] FROM [CZ2006 - Life Planner].[dbo].[Feedback] t";
            string sql2 = "SELECT feedbackDatetime, feedbackIssue, feedbackContent, submittedBy, feedbackStatus FROM dbo.Feedback WHERE @feedbackID = feedbackID";
            SqlCommand cmd2 = new SqlCommand(sql2, con);

            cmd2.Parameters.AddWithValue("@feedbackID", feedbackID);
            con.Open();
            SqlDataReader reader = cmd2.ExecuteReader();
            reader.Read();
            fbkDatetime.Text = reader["feedbackDatetime"].ToString();
            txtfeedbackIssue.Text = reader["feedbackIssue"].ToString();
            fbkContent.Text = reader["feedbackContent"].ToString();
            //resolvedBy.Text = reader["feedbackID"].ToString(); ;
            resolvedBy.Text = feedbackID;

            string today = DateTime.Now.ToString();
            resolvedOn.Text = today;

            reader.Close();
            con.Close();
        }
    }
}