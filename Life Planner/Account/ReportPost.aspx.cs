using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public partial class ReportPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSpecifyReasons.Enabled = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string postID = Session["postID"].ToString();

            if (RadioButtonList1.SelectedValue == "Others")
            {
                string reason = txtSpecifyReasons.Text;
                if (txtSpecifyReasons.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please type a reason.');", true);
                    return;
                }
                else
                {   string reporter = Session["username"].ToString();
                    DateTime datetimeNow = DateTime.Now;
                    string dateTimeReported = datetimeNow.ToString("yyyy-MM-dd HH:mm:ss");

                    SqlConnection con = new DBManager().getConnection();
                    string sql = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Reporting] (postID, reporter, author, dateTimeReported, reportReason) VALUES (@postID, @reporter, @author, @dateTimeReported, @reportReason);";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@postID", Session["postID"].ToString());
                    cmd.Parameters.AddWithValue("@reporter", Session["username"].ToString());
                    cmd.Parameters.AddWithValue("@author", Session["author"].ToString());
                    cmd.Parameters.AddWithValue("@dateTimeReported", dateTimeReported);
                    cmd.Parameters.AddWithValue("@reportReason", reason);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Session["threadID"] = getThreadID();
                    Response.Redirect("~/Account/Posts.aspx");
                }
            }
            else
            {
                string reason = RadioButtonList1.SelectedValue;
                string reporter = Session["username"].ToString();
                DateTime datetimeNow = DateTime.Now;
                string dateTimeReported = datetimeNow.ToString("yyyy-MM-dd HH:mm:ss");

                SqlConnection con = new DBManager().getConnection();
                string sql = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Reporting] (postID, reporter, author, dateTimeReported, reportReason) VALUES (@postID, @reporter, @author, @dateTimeReported, @reportReason);";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@postID", Session["postID"].ToString());
                cmd.Parameters.AddWithValue("@reporter", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@author", Session["author"].ToString());
                cmd.Parameters.AddWithValue("@dateTimeReported", dateTimeReported);
                cmd.Parameters.AddWithValue("@reportReason", reason);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Session["threadID"] = getThreadID();
                Response.Redirect("~/Account/Posts.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["threadID"] = getThreadID();
            Response.Redirect("~/Account/Posts.aspx");
        }

        protected string getThreadID()
        {
            string postID = Session["postID"].ToString();
            string threadID;

            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT t.[threadID] FROM [CZ2006 - Life Planner].[dbo].[Threads] t INNER JOIN [CZ2006 - Life Planner].[dbo].[Posts] p ON t.threadID = p.threadID WHERE p.[postID] = @postID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@postID", postID);
            con.Open();
            threadID = cmd.ExecuteScalar().ToString();
            con.Close();

            return threadID;

        }
    }
}