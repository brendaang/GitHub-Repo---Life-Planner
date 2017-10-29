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
            string postID = labelPostID.Text;
            string postText = txtEditor.Text;

            if (txtEditor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please check that the post you edited is not empty.');", true);
                return;
            }
            else if (messageChecker(txtEditor.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please check your edited post contents. No vulgarities please.');", true);
                return;
            }
            else
            {
                editPost(postID, postText);
                Session["threadID"] = getThreadID();
                Response.Redirect("Posts.aspx");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["threadID"] = getThreadID();
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

        protected void editPost(string postID, string postText)
        {
            SqlConnection con = new DBManager().getConnection();
            string sql = "UPDATE [CZ2006 - Life Planner].[dbo].[Posts] SET postText=@postText WHERE postID=@postID;";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@postID", postID);
            cmd.Parameters.AddWithValue("@postText", postText);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

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

        //vulgarity filter, loading the txt file of vulgarities.
        //and saving into list<string>
        protected List<string> getBadWordList()
        {
            List<string> badWords = new List<string>();
            //get the path to the WordList file
            //string file = System.IO.File.ReadAllText(@"");
            String file = Server.MapPath("/WordList/WordList.txt");

            //Open text file for reading
            using (TextReader reader = new StreamReader(file))
            {
                //Loop through each line in the file
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //remove any whitespace and cast to lower case.
                    string word = line.Trim().ToLower();

                    //add to list in memory
                    badWords.Add(word);
                }
            }
            return badWords;
        }

        //splitting the post word by word and reading through the array, comparing the words.
        protected bool messageChecker(string post)
        {


            char delimiter = ' ';
            string[] words = post.Split(delimiter);
            string[] badWords = getBadWordList().ToArray();

            foreach (string word in words)
            {
                if (this.getBadWordList().Contains(word.ToLower()))
                {
                    //System.Diagnostics.Debug.WriteLine("Inside1");
                    return true;
                }

            }
            for (int j = 0; j < badWords.Count(); j++)
            {
                var regexItem1 = new Regex("(" + badWords[j] + ")");
                for (int i = 0; i < words.Count(); i++)
                {
                    if (regexItem1.IsMatch(words[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}