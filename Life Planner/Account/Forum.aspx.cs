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
    public partial class Forum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable ViewThreadsTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT t.[threadID],t.[threadName],t.[threadDesc],a.[username],t.[dateCreated] FROM [CZ2006 - Life Planner].[dbo].[Threads] t INNER JOIN [CZ2006 - Life Planner].[dbo].[AccCreds] a ON t.accID = a.accountID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewThreadsTable);
            threadsGridView.DataSource = ViewThreadsTable;
            threadsGridView.DataBind();
            con.Close();
        }

        //GridView selecting
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchFunction();
        }

        protected void searchFunction()
        {
            //string tName ;
            DataTable ViewThreadsTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT t.[threadID],t.[threadName],t.[threadDesc],a.[username],t.[dateCreated] FROM [CZ2006 - Life Planner].[dbo].[Threads] t INNER JOIN [CZ2006 - Life Planner].[dbo].[AccCreds] a ON t.accID = a.accountID WHERE t.[threadName] LIKE '%' + @pSearch + '%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@pSearch", SqlDbType.NVarChar).Value = tbSearch.Text;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewThreadsTable);
            threadsGridView.DataSource = ViewThreadsTable;
            threadsGridView.DataBind();
            con.Close();
        }

        protected void threadsGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            //set the select index

            searchFunction();

            int intThreadID = (int)threadsGridView.DataKeys[threadsGridView.SelectedIndex].Value;
            string threadID = intThreadID.ToString();
            lblThreadID.Text = threadID;

            //save threadID into session
            Session["ThreadID"] = threadID;

            //Response.Redirect("Posts-View.aspx");
        }

        protected void getPosts(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewPosts")
            {
                //searchFunction();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = threadsGridView.Rows[index];

                string threadID = row.Cells[0].Text;
                Session["ThreadID"] = threadID;
                Response.Redirect("Posts.aspx", false);
            }
        }
        protected void getAuthor(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string authorName = lb.Text;
            Session["AuthorName"] = authorName;
            //Response.Redirect("~/ViewProfiles.aspx");
        }

        protected void createThread_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateThread.aspx");
        }
    }
}