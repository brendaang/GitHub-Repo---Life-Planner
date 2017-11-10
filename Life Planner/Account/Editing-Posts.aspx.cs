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
using System.IO;
using System.Text.RegularExpressions;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public partial class Editing_Posts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getPostToEdit();
            }
        }

        protected void btnEditPost_Click(object sender, EventArgs e)
        {
            String file = Server.MapPath("/WordList/WordList.txt");
            string postID = labelPostID.Text;
            string postText = txtEditor.Text;

            if (txtEditor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please check that the post you edited is not empty.');", true);
                return;
            }
            else if (new CommonMethods().messageChecker(txtEditor.Text, new CommonMethods().getBadWordList(file)))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please check your edited post contents. No vulgarities please.');", true);
                return;
            }
            else
            {
                new CommonMethods().updatePost(postID, postText);
                Session["threadID"] = new CommonMethods().getThreadID(postID);
                Response.Redirect("Posts.aspx");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string postID = Session["postID"].ToString();
            Session["threadID"] = new CommonMethods().getThreadID(postID);
            Response.Redirect("Posts.aspx");
        }

        //methods
        protected void getPostToEdit()
        {
            string postID = Session["postID"].ToString();
            DataTable dt = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT p.[postID],p. [postText],a.[userName], p.[datePosted] FROM[CZ2006 - Life Planner].[dbo].[Posts] p INNER JOIN[CZ2006 - Life Planner].[dbo].[AccCreds] a ON p.accID = a.accountID WHERE p.[postID] = @postID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@postID", postID);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            labelUsername.Text = dt.Rows[0]["username"].ToString();
            labelPostID.Text = dt.Rows[0]["postID"].ToString();
            labelDatePosted.Text = dt.Rows[0]["datePosted"].ToString();
            txtEditor.Text = dt.Rows[0]["postText"].ToString();
            con.Close();
        }

    }
}