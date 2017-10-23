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
            //if (Session["username"] == null)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You must be a registered user to be able to post.');", true);
            //    return;
            //}
            //else
            //{
                //checking if textbox is empty.
                //if there is no post content, do not allow user to proceed.
                if (txtEditor.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please type a post or reply before clicking the 'Post' button.');", true);
                    return;
                    //lblWarningMsg.Text = "Please type a post or reply before clicking the 'Post' button.";
                    //Editor1.Focus();
                }

                //pass content from rich textbox through the vulgarity checking method.
                //if post contains vulgarities, do not allow user to proceed.
                else if (messageChecker(txtEditor.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Please check your post contents. No vulgarities please.');", true);
                    return;
                }

                else
                {
                    string postText = txtEditor.Text;
                    string threadID = (string)(Session["ThreadID"]);
                    string accID = getAccID();
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
            //}

        }

        //method to get userID by the username that was saved in the session
        protected string getAccID()
        {
            //hardcoded for now
            String acctName = "tingle";
            //String acctName = Session["username"].ToString();

            String accID;
            SqlConnection con4 = new DBManager().getConnection();
            string sql4 = "SELECT [accountID] FROM [CZ2006 - Life Planner].[dbo].[AccCreds] WHERE username = @userName;";
            SqlCommand cmd4 = new SqlCommand(sql4, con4);
            cmd4.Parameters.AddWithValue("@userName", acctName);
            con4.Open();
            accID = cmd4.ExecuteScalar().ToString();
            cmd4.ExecuteNonQuery();
            con4.Close();

            return accID;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Posts.aspx");
        }

        //method to get the userID of a post's author
        protected string getAcc(string postID)
        {
            string authorID;

            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT [accID] FROM [CZ2006 - Life Planner].[dbo].[Posts] WHERE postID = @postID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@postID", postID);
            con.Open();
            authorID = cmd.ExecuteScalar().ToString();
            cmd.ExecuteNonQuery();
            con.Close();

            return authorID;
        }
        //voting system
        protected void btnLikeOnClick(object sender, EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You must be a registered user to be able to like a post.');", true);
            //    return;
            //}
            //else
            //{
                Panel pl = ((Button)sender).Parent as Panel;
                if (pl != null)
                {
                    LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                    Label postID = pl.FindControl("postID") as Label;

                    string authorID = getAcc(postID.Text);

                    if (votingChecker(authorName.Text))
                    {

                        //int pointBalance;
                        //string pointsBalance;
                        string accID = getAcc(postID.Text);

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

                        int numLikesBalance;

                        //get numLikes balance
                        SqlConnection con9 = new DBManager().getConnection();
                        string sql9 = "SELECT [numLikes]  FROM [CZ2006 - Life Planner].[dbo].[Posts]  WHERE postID = @postID;";
                        SqlCommand cmd9 = new SqlCommand(sql9, con9);
                        cmd9.Parameters.AddWithValue("@postID", postID.Text);
                        con9.Open();
                        string stringNumLikesBalance = cmd9.ExecuteScalar().ToString();
                        numLikesBalance = Convert.ToInt32(stringNumLikesBalance);
                        con9.Close();

                        int uponVote = 1;
                        int newNumLikesBalance = numLikesBalance + uponVote;

                        SqlConnection con10 = new DBManager().getConnection();
                        string sql10 = "UPDATE [CZ2006 - Life Planner].[dbo].[Posts] SET numLikes = @numLikes WHERE postID = @postID;";
                        SqlCommand cmd10 = new SqlCommand(sql10, con10);
                        cmd10.Parameters.AddWithValue("@postID", postID.Text);
                        cmd10.Parameters.AddWithValue("@numLikes", newNumLikesBalance);
                        con10.Open();
                        cmd10.ExecuteNonQuery();
                        con10.Close();


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
            //}
        }

        protected void btnDislikeOnClick(object sender, EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You must be a registered user to be able to dislike a post.');", true);
            //    return;
            //}
            //else
            //{
                Panel pl = ((Button)sender).Parent as Panel;
                if (pl != null)
                {
                    LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                    Label postID = pl.FindControl("postID") as Label;

                    string authorID = getAcc(postID.Text);

                    if (votingChecker(authorName.Text))
                    {
                        string accID = getAcc(postID.Text);

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

                        int numDislikesBalance;

                        //get numLikes balance
                        SqlConnection con9 = new DBManager().getConnection();
                        string sql9 = "SELECT [numDislikes]  FROM [CZ2006 - Life Planner].[dbo].[Posts]  WHERE postID = @postID;";
                        SqlCommand cmd9 = new SqlCommand(sql9, con9);
                        cmd9.Parameters.AddWithValue("@postID", postID.Text);
                        con9.Open();
                        string stringNumDislikesBalance = cmd9.ExecuteScalar().ToString();
                        numDislikesBalance = Convert.ToInt32(stringNumDislikesBalance);
                        con9.Close();

                        int uponVote = 1;
                        int newNumDislikesBalance = numDislikesBalance + uponVote;

                        SqlConnection con10 = new DBManager().getConnection();
                        string sql10 = "UPDATE [CZ2006 - Life Planner].[dbo].[Posts] SET numDislikes = @numDislikes WHERE postID = @postID;";
                        SqlCommand cmd10 = new SqlCommand(sql10, con10);
                        cmd10.Parameters.AddWithValue("@postID", postID.Text);
                        cmd10.Parameters.AddWithValue("@numDislikes", newNumDislikesBalance);
                        con10.Open();
                        cmd10.ExecuteNonQuery();
                        con10.Close();

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
            //}
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You must be logged in to edit your own posts!);", true);
            //    return;
            //}
            //else
            //{
                Panel pl = ((Button)sender).Parent as Panel;
                if (pl != null)
                {
                    LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                    Label postID = pl.FindControl("postID") as Label;

                    string authorID = getAcc(postID.Text);

                    if (votingChecker(authorName.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You cannot edit posts by other users!');", true);
                        return;
                    }
                    else
                    {
                        Session["postID"] = postID.Text;
                        Response.Redirect("Editing-Posts.aspx");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You cannot vote on your own post.');", true);
                        //return;
                    }
                }
            //}
        }

        protected void btnQuote_Click(object sender, EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You must be logged in to quote a post!);", true);
            //    return;
            //}
            //else
            //{
                Panel pl = ((Button)sender).Parent as Panel;
                if (pl != null)
                {
                    LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                    Label postID = pl.FindControl("postID") as Label;

                    string authorID = getAcc(postID.Text);
                    string quote = getQuote(postID.Text, authorID);

                    txtEditor.Text = "@ " + authorName.Text + "<br />" + "<span style=\"background-color: #c5c3c6;\">" + quote + "</span><br /><br /><br />";
                    btnPost.Focus();
                }
            //}
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('You must be logged in to report a post!);", true);
            //    return;
            //}
            //else
            //{
                Panel pl = ((Button)sender).Parent as Panel;
                if (pl != null)
                {
                    LinkButton authorName = pl.FindControl("authorLinkButton") as LinkButton;
                    Label postID = pl.FindControl("postID") as Label;

                    string authorID = getAcc(postID.Text);

                    if (votingChecker(authorName.Text))
                    {

                        string confirmValue = Request.Form["confirm_value"];
                        if (confirmValue == "Yes")
                        {
                            int reportCount = 1;
                            string reporter = Session["username"].ToString();
                            DateTime datetimeNow = DateTime.Now;
                            string dateTimeReported = datetimeNow.ToString("yyyy-MM-dd HH:mm:ss");
                            string windowPeriodToNextReport = datetimeNow.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss");

                            SqlConnection con1 = new DBManager().getConnection();
                            string sql1 = "SELECT top 1 [windowPeriodToNextReport]FROM [CZ2006 - Life Planner].[dbo].[Reporting] WHERE reporter = @reporter ORDER BY dateTimeReported;";
                            SqlCommand cmd1 = new SqlCommand(sql1, con1);
                            cmd1.Parameters.AddWithValue("@reporter", reporter);
                            con1.Open();
                            SqlDataReader dr = cmd1.ExecuteReader();
                            //checker for one-time reporting
                            if (dr.Read())
                            {
                                string dtr = dr["windowPeriodToNextReport"].ToString();
                                if (DateTime.Now < DateTime.Parse(dtr))
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert!", "alert('Sorry! You have reported a post once before in the past 24 hours!');", true);
                                    return;
                                }
                            }

                            SqlConnection con2 = new DBManager().getConnection();
                            string sql2 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[Reporting] (postID, reporter, author, reportCount, dateTimeReported, windowPeriodToNextReport) VALUES (@postID, @reporter, @author, @reportCount, @dateTimeReported, @windowPeriodToNextReport);";
                            SqlCommand cmd2 = new SqlCommand(sql2, con2);
                            cmd2.Parameters.AddWithValue("@postID", postID.Text);
                            cmd2.Parameters.AddWithValue("@reporter", Session["username"].ToString());
                            cmd2.Parameters.AddWithValue("@author", authorName.Text);
                            cmd2.Parameters.AddWithValue("@reportCount", reportCount);
                            cmd2.Parameters.AddWithValue("@dateTimeReported", dateTimeReported);
                            cmd2.Parameters.AddWithValue("@windowPeriodToNextReport", windowPeriodToNextReport);
                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

                            //get numReported balance
                            SqlConnection con3 = new DBManager().getConnection();
                            string sql3 = "SELECT [numReported] FROM [CZ2006 - Life Planner].[dbo].[Posts] WHERE [postID] = @postID;";
                            SqlCommand cmd3 = new SqlCommand(sql3, con3);
                            cmd3.Parameters.AddWithValue("@postID", postID.Text);
                            con3.Open();
                            string stringReportCountBalance = cmd3.ExecuteScalar().ToString();
                            int reportCountBalance = Convert.ToInt32(stringReportCountBalance);
                            con3.Close();

                            int uponReport = 1;
                            int newNumReportCountBalance = reportCountBalance + uponReport;

                            SqlConnection con4 = new DBManager().getConnection();
                            string sql4 = "UPDATE [CZ2006 - Life Planner].[dbo].[Posts] SET numReported = @numReported WHERE postID = @postID;";
                            SqlCommand cmd4 = new SqlCommand(sql4, con4);
                            cmd4.Parameters.AddWithValue("@postID", postID.Text);
                            cmd4.Parameters.AddWithValue("@numReported", newNumReportCountBalance);
                            con4.Open();
                            cmd4.ExecuteNonQuery();
                            con4.Close();

                            Session["postID"] = postID.Text;
                            Response.Redirect("~/ReportReasons.aspx");
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
            //}
        }

        //voting checker disallow users to like/dislike their own posts
        protected bool votingChecker(String username)
        {
            //if (Session["username"].ToString() != username)
            //    return true;
            //else
            //    return false;
            return true;
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
                        //System.Diagnostics.Debug.WriteLine("Inside2" );
                        //System.Diagnostics.Debug.WriteLine(i);
                        //System.Diagnostics.Debug.WriteLine(regexItem1);
                        //System.Diagnostics.Debug.WriteLine(badWords.Count());
                        //System.Diagnostics.Debug.WriteLine(words[i]);
                        return true;
                    }
                }
            }
            return false;
        }
        
        protected string getQuote(string postID, string authorID)
        {
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT [postText] FROM [CZ2006 - Life Planner].[dbo].[Posts] WHERE [postID] = @postID AND [accID] = @accID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@postID", postID);
            cmd.Parameters.AddWithValue("@accID", authorID);
            con.Open();
            string quote = cmd.ExecuteScalar().ToString();
            con.Close();

            return quote;

        }
    }
}