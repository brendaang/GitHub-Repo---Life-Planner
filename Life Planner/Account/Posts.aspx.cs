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
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public partial class Posts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getThreadName();
                getThreadPosts();
                getThreadDesc();

            }

            lblPostOrReply.Text = "Post To This Thread";
            lblPostOrReplyContent.Text = "Your Post Content: ";
        }

        //setting page number for post paging
        public int setPageNumber()
        {
            string a = pageNum.Text;
            if (a != null)
            {
                try
                {
                    return Convert.ToInt32(a);
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            return 0;
        }

        //to provide additional processing (for paging)
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        //retrieve thread posts from the database
        protected void getThreadPosts()
        {
            string threadID = (string)(Session["ThreadID"]);
            DataTable viewPostsTable = new DataTable();
            SqlConnection con2 = new DBManager().getConnection();
            string sql2 = "SELECT p.[postID],p. [postText],a.[userName], p.[datePosted] FROM [CZ2006 - Life Planner].[dbo].[Posts] p INNER JOIN [CZ2006 - Life Planner].[dbo].[AccCreds] a ON p.accID = a.accountID WHERE p.[threadID] = @threadID ORDER BY p.[datePosted] DESC;";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            cmd2.Parameters.AddWithValue("@threadID", threadID);
            con2.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            adapter.Fill(viewPostsTable);

            con2.Close();

            //check if there are posts in the thread. 
            // if there are no posts, user will be prompted to write a post.
            if (viewPostsTable.Rows.Count == 0)
            {
                lblEmptyThread.Text = "The thread is empty! Please write a post below!";
            }
            else
            {
                //using PagedDataSource to do the paging if there are posts in the thread
                //making use to dataview
                PagedDataSource pgitems = new PagedDataSource();
                DataView dv = new DataView(viewPostsTable);
                pgitems.DataSource = dv;
                pgitems.AllowPaging = true;
                pgitems.PageSize = 5;
                pgitems.CurrentPageIndex = setPageNumber();

                if (pgitems.PageCount > 1)
                {
                    List<string> pages = new List<string>();
                    for (int i = 0; i < pgitems.PageCount; i++)
                    {
                        // this part a bit weird...
                        pages.Add((i + 1).ToString());
                        rptPages.DataSource = pages;
                        rptPages.DataBind();
                    }
                }
                else
                {
                    rptPages.Visible = false;
                }
                Repeater1.DataSource = pgitems;
                Repeater1.DataBind();

            }
        }

        //retrieving the threadname via threadID that was saved in the session
        protected void getThreadName()
        {
            string threadID = (string)(Session["ThreadID"]);
            String threadName;

            SqlConnection con1 = new DBManager().getConnection();
            string sql1 = "SELECT [threadName] FROM [CZ2006 - Life Planner].[dbo].[Threads] WHERE [threadID] = @threadID;";
            SqlCommand cmd1 = new SqlCommand(sql1, con1);
            cmd1.Parameters.AddWithValue("@threadID", threadID);
            con1.Open();
            threadName = cmd1.ExecuteScalar().ToString();
            cmd1.ExecuteNonQuery();
            con1.Close();

            lblTopic.Text = threadName;
        }

        //retrieving the thread description via threadID from session
        protected void getThreadDesc()
        {
            string threadID = (string)(Session["ThreadID"]);
            String threadDesc;

            SqlConnection con1 = new DBManager().getConnection();
            string sql1 = "SELECT [threadDesc] FROM [CZ2006 - Life Planner].[dbo].[Threads] WHERE [threadID] = @threadID;";
            SqlCommand cmd1 = new SqlCommand(sql1, con1);
            cmd1.Parameters.AddWithValue("@threadID", threadID);
            con1.Open();
            threadDesc = cmd1.ExecuteScalar().ToString();
            cmd1.ExecuteNonQuery();
            con1.Close();

            lblThreadDesc.Text = threadDesc;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            //checking if textbox is empty.
            //if there is no post content, do not allow user to proceed.
            if (txtEditor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please type a post or reply before clicking the 'Post' button.');", true);
                return;
            }

            //pass content from rich textbox through the vulgarity checking method.
            //if post contains vulgarities, do not allow user to proceed.
            else if (new CommonMethods().messageChecker(txtEditor.Text, getBadWordList()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please check your post contents. No vulgarities please.');", true);
                return;
            }

            else
            {
                string postText = txtEditor.Text;
                string threadID = (string)(Session["ThreadID"]);
                String username = Session["username"].ToString();
                String accID = new CommonMethods().getAccID(username);
                DateTime dateTime = DateTime.Now;
                SqlConnection con3 = new DBManager().getConnection();
                string sql3 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Posts] (postText, threadID, accID, datePosted) VALUES (@postText, @threadID, @accID, @dateTime);";
                SqlCommand cmd3 = new SqlCommand(sql3, con3);
                cmd3.Parameters.AddWithValue("@postText", postText);
                cmd3.Parameters.AddWithValue("@threadID", threadID);
                cmd3.Parameters.AddWithValue("@accID", accID);
                cmd3.Parameters.AddWithValue("@dateTime", dateTime);
                con3.Open();
                cmd3.ExecuteNonQuery();
                con3.Close();
                Response.Redirect("Posts.aspx");
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Posts.aspx");
        }
        //voting system
        protected void btnLikeOnClick(object sender, EventArgs e)
        {
            Panel pl = ((Button)sender).Parent as Panel;
            if (pl != null)
            {
                LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                Label postID = pl.FindControl("postID") as Label;

                string authorID = new CommonMethods().getAcc(postID.Text);


                if (votingChecker(authorName.Text))
                {
                    string accID = new CommonMethods().getAcc(postID.Text);

                    int voteCount = 1;
                    string voter = Session["username"].ToString();

                    SqlConnection con8 = new DBManager().getConnection();
                    string sql8 = "SELECT [voteCount] FROM [CZ2006 - Life Planner].[dbo].[Voting] WHERE postID = @postID AND voter = @voter;";
                    SqlCommand cmd8 = new SqlCommand(sql8, con8);
                    cmd8.Parameters.AddWithValue("@postID", postID.Text);
                    cmd8.Parameters.AddWithValue("@voter", voter);
                    con8.Open();
                    SqlDataReader dr = cmd8.ExecuteReader();

                    //checker for one-time voting
                    if (dr.Read())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "javascript:alert('You have voted this post once before already!');", true);
                        return;
                    }
                    con8.Close();

                    SqlConnection con7 = new DBManager().getConnection();
                    string sql7 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Voting] (postID, voter, author, voteCount) VALUES (@postID, @voter, @author, @voteCount);";
                    SqlCommand cmd7 = new SqlCommand(sql7, con7);
                    cmd7.Parameters.AddWithValue("@postID", postID.Text);
                    cmd7.Parameters.AddWithValue("@voter", Session["username"].ToString());
                    cmd7.Parameters.AddWithValue("@author", authorName.Text);
                    cmd7.Parameters.AddWithValue("@voteCount", voteCount);
                    con7.Open();
                    cmd7.ExecuteNonQuery();
                    con7.Close();

                    int numLikesBalance = new CommonMethods().getNumLikesBalance(postID.Text);
                    int uponVote = 1;
                    int newNumLikesBalance = numLikesBalance + uponVote;
                    new CommonMethods().updateNumLikesBalance(postID.Text, newNumLikesBalance);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You have liked this post! :)');", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You cannot vote on your own post.');", true);
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Not found!');", true);
                return;
            }
        }

        protected void btnDislikeOnClick(object sender, EventArgs e)
        {
            Panel pl = ((Button)sender).Parent as Panel;
            if (pl != null)
            {
                LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                Label postID = pl.FindControl("postID") as Label;

                string authorID = new CommonMethods().getAcc(postID.Text);

                if (votingChecker(authorName.Text))
                {
                    string accID = new CommonMethods().getAcc(postID.Text);

                    int voteCount = 1;
                    string voter = Session["username"].ToString();

                    SqlConnection con8 = new DBManager().getConnection();
                    string sql8 = "SELECT [voteCount] FROM [CZ2006 - Life Planner].[dbo].[Voting] WHERE postID = @postID AND voter = @voter;";
                    SqlCommand cmd8 = new SqlCommand(sql8, con8);
                    cmd8.Parameters.AddWithValue("@postID", postID.Text);
                    cmd8.Parameters.AddWithValue("@voter", voter);
                    con8.Open();
                    SqlDataReader dr = cmd8.ExecuteReader();

                    //checker for one-time voting
                    if (dr.Read())
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Sorry! You have voted this post once before already!');", true);
                        return;
                    }
                    con8.Close();

                    SqlConnection con7 = new DBManager().getConnection();
                    string sql7 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Voting] (postID, voter, author, voteCount) VALUES (@postID, @voter, @author, @voteCount);";
                    SqlCommand cmd7 = new SqlCommand(sql7, con7);
                    cmd7.Parameters.AddWithValue("@postID", postID.Text);
                    cmd7.Parameters.AddWithValue("@voter", Session["username"].ToString());
                    cmd7.Parameters.AddWithValue("@author", authorName.Text);
                    cmd7.Parameters.AddWithValue("@voteCount", voteCount);
                    con7.Open();
                    cmd7.ExecuteNonQuery();
                    con7.Close();
                    int numDislikesBalance = new CommonMethods().getNumDislikesBalance(postID.Text);
                    int uponVote = 1;
                    int newNumDislikesBalance = numDislikesBalance + uponVote;
                    new CommonMethods().updateNumLikesBalance(postID.Text, newNumDislikesBalance);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You have disliked this post ):');", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You cannot vote on your own post.');", true);
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Not found!');", true);
                return;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Panel pl = ((Button)sender).Parent as Panel;
            if (pl != null)
            {
                LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                Label postID = pl.FindControl("postID") as Label;

                string authorID = new CommonMethods().getAcc(postID.Text);

                if (votingChecker(authorName.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You cannot edit posts by other users!');", true);
                    return;
                }
                else
                {
                    Session["postID"] = postID.Text;
                    Response.Redirect("Editing-Posts.aspx");
                }
            }
        }

        protected void btnQuote_Click(object sender, EventArgs e)
        {
            Panel pl = ((Button)sender).Parent as Panel;
            if (pl != null)
            {
                LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                Label postID = pl.FindControl("postID") as Label;

                string authorID = new CommonMethods().getAcc(postID.Text);
                string quote = new CommonMethods().getQuote(postID.Text, authorID);

                txtEditor.Text = "@ " + authorName.Text + ": " + quote + "\n\n";
                btnPost.Focus();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Panel pl = ((Button)sender).Parent as Panel;
            if (pl != null)
            {
                LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                Label postID = pl.FindControl("postID") as Label;

                string authorID = new CommonMethods().getAcc(postID.Text);

                if (votingChecker(authorName.Text))
                {

                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        Session["postID"] = postID.Text;
                        Session["author"] = authorName.Text;
                        Response.Redirect("~/Account/ReportPost.aspx");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The post is not reported.')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Why would you report your own post? O:');", true);
                    return;
                }
            }
        }

        //voting checker disallow users to like/dislike their own posts
        protected bool votingChecker(String username)
        {
            if (Session["username"].ToString() != username)
                return true;
            else
                return false;
        }

        protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            pageNum.Text = Convert.ToString(Convert.ToInt32(e.CommandArgument) - 1);
            getThreadPosts();
        }

        protected void getAuthor(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string authorName = lb.Text;
        }

        //vulgarity filter, loading the txt file of vulgarities.
        //and saving into list<string>
        protected List<string> getBadWordList()
        {
            List<string> badWords = new List<string>();
            //get the path to the WordList file
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

        
    }
}