using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public partial class PostIssues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable PostIssuesTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT * FROM [CZ2006 - Life Planner].[dbo].[Reporting] WHERE resolved = @resolved";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@resolved", 0);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(PostIssuesTable);
            issuesGridView.DataSource = PostIssuesTable;
            issuesGridView.DataBind();
            con.Close();
        }

        protected void getUser(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string authorName = lb.Text;
            Session["AuthorName"] = authorName;
            Response.Redirect("~/Account/ViewOtherProfile.aspx");
        }

        protected void getIssues(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewIssues")
            {
                //searchFunction();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = issuesGridView.Rows[index];

                string reportID = row.Cells[0].Text;
                string postID = row.Cells[1].Text;
                Session["postID"] = postID;
                Session["reportID"] = reportID;
                Response.Redirect("~/Account/ResolveIssue.aspx");
            }
        }

    }
}