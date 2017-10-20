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
    public partial class ViewFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable ViewAllFeedbacks = new DataTable();
            SqlConnection con = new DBManager().getConnection();

            string sql = "SELECT t.[feedbackID],t.[feedbackDatetime],t.[feedbackIssue],t.[feedbackContent], t.[submittedBy], t.[feedbackStatus] FROM [CZ2006 - Life Planner].[dbo].[Feedback] t";
            string sql1 = "SELECT COUNT ([feedbackID]) FROM [CZ2006 - Life Planner].[dbo].[Feedback]";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            con.Open();
            int FbkCount = (int)cmd1.ExecuteScalar();
            countFeedbacks.Text = FbkCount.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewAllFeedbacks);
            feedbackGridView.DataSource = ViewAllFeedbacks;
            feedbackGridView.DataBind();
            
            con.Close();

            NumRecordLoaded.Items.Add("5");
            NumRecordLoaded.Items.Add("10");
            NumRecordLoaded.Items.Add("20");
            NumRecordLoaded.Items.Add("50");
            NumRecordLoaded.Items.Add("100");
        }
    }
}