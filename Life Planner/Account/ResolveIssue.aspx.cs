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
    public partial class ResolveIssue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getPostToBeEvaluated();
            getReasonReported();
        }

        protected void getReasonReported()
        {
            string postID = Session["postID"].ToString();
            string reportReason;
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT [reportReason] FROM [CZ2006 - Life Planner].[dbo].[Reporting] WHERE postID = @postID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@postID", postID);
            con.Open();
            reportReason = cmd.ExecuteScalar().ToString();
            labelReportReason.Text = reportReason;
            con.Close();
        }

        protected void authorLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string authorName = lb.Text;
            Session["AuthorName"] = authorName;
            Response.Redirect("~/Account/ViewOtherProfile.aspx");
        }

        protected void getPostToBeEvaluated()
        {
            string postID = Session["postID"].ToString();
            DataTable dt = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT p.[postID],p.[postText], a.[username], p.[datePosted] FROM [CZ2006 - Life Planner].[dbo].[Posts] p INNER JOIN [CZ2006 - Life Planner].[dbo].[AccCreds] a ON p.accID = a.accCredID WHERE p.[postID] = @postID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@postID", postID);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            authorLinkButton.Text = dt.Rows[0]["username"].ToString();
            lblPostID.Text = dt.Rows[0]["postID"].ToString();
            lblPostText.Text = dt.Rows[0]["postText"].ToString();
            lblDatePosted.Text = dt.Rows[0]["datePosted"].ToString();
            con.Close();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string postID = Session["postID"].ToString();

                SqlConnection con1 = new DBManager().getConnection();
                string sql1 = "DELETE FROM [CZ2006 - Life Planner].[dbo].[Reporting] WHERE [postID] = @postID;";
                SqlCommand cmd1 = new SqlCommand(sql1, con1);
                cmd1.Parameters.AddWithValue("@postID", postID);
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();

                SqlConnection con2 = new DBManager().getConnection();
                string sql2 = "DELETE FROM [CZ2006 - Life Planner].[dbo].[Posts] WHERE [postID] = @postID;";
                SqlCommand cmd2 = new SqlCommand(sql2, con2);
                cmd2.Parameters.AddWithValue("@postID", postID);
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();

                Response.Redirect("~/Account/PostIssues.aspx");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The post is not deleted.')", true);
            }
        }

        protected void btnIgnore_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string postID = Session["postID"].ToString();

                SqlConnection con2 = new DBManager().getConnection();
                string sql2 = "UPDATE [CZ2006 - Life Planner].[dbo].[Reporting] SET resolved = @resolved WHERE postID = @postID;";
                SqlCommand cmd2 = new SqlCommand(sql2, con2);
                cmd2.Parameters.AddWithValue("@resolved", 1);
                cmd2.Parameters.AddWithValue("@postID", postID);
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();

                Response.Redirect("~/Account/PostIssues.aspx");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The post is not ignored yet.')", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/PostIssues.aspx");
        }
    }
}