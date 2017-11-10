using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Life_Planner.Data
{
    public class CommonMethods
    {
        public string getAccID(String acctName)
        {
            String accID;
            SqlConnection con1 = new DBManager().getConnection();
            string sql1 = "SELECT [accountID] FROM[CZ2006 - Life Planner].[dbo].[AccCreds] WHERE username = @userName; ";
            SqlCommand cmd1 = new SqlCommand(sql1, con1);
            cmd1.Parameters.AddWithValue("@userName", acctName);
            con1.Open();
            accID = cmd1.ExecuteScalar().ToString();
            cmd1.ExecuteNonQuery();
            con1.Close();

            return accID;
        }

        //method to get the userID of a post's author
        public string getAcc(string postID)
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
        public string getQuote(string postID, string authorID)
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

        public int getNumLikesBalance(string postID)
        {
            int numLikesBalance;

            //get numLikes balance
            SqlConnection con9 = new DBManager().getConnection();
            string sql9 = "SELECT [numLikes]  FROM [CZ2006 - Life Planner].[dbo].[Posts]  WHERE postID = @postID;";
            SqlCommand cmd9 = new SqlCommand(sql9, con9);
            cmd9.Parameters.AddWithValue("@postID", postID);
            con9.Open();
            string stringNumLikesBalance = cmd9.ExecuteScalar().ToString();
            numLikesBalance = Convert.ToInt32(stringNumLikesBalance);
            con9.Close();

            return numLikesBalance;
        }

        public void updateNumLikesBalance(string postID, int newNumLikesBalance)
        {

            SqlConnection con10 = new DBManager().getConnection();
            string sql10 = "UPDATE [CZ2006 - Life Planner].[dbo].[Posts] SET numLikes = @numLikes WHERE postID = @postID;";
            SqlCommand cmd10 = new SqlCommand(sql10, con10);
            cmd10.Parameters.AddWithValue("@postID", postID);
            cmd10.Parameters.AddWithValue("@numLikes", newNumLikesBalance);
            con10.Open();
            cmd10.ExecuteNonQuery();
            con10.Close();
        }

        public int getNumDislikesBalance(string postID)
        {

            int numDislikesBalance;
            //get numDislikes balance
            SqlConnection con9 = new DBManager().getConnection();
            string sql9 = "SELECT [numDislikes]  FROM [CZ2006 - Life Planner].[dbo].[Posts]  WHERE postID = @postID;";
            SqlCommand cmd9 = new SqlCommand(sql9, con9);
            cmd9.Parameters.AddWithValue("@postID", postID);
            con9.Open();
            string stringNumDislikesBalance = cmd9.ExecuteScalar().ToString();
            numDislikesBalance = Convert.ToInt32(stringNumDislikesBalance);
            con9.Close();

            return numDislikesBalance;
        }

        public void updateNumDislikesBalance(string postID, int newNumDislikesBalance)
        {
            SqlConnection con10 = new DBManager().getConnection();
            string sql10 = "UPDATE [CZ2006 - Life Planner].[dbo].[Posts] SET numDislikes = @numDislikes WHERE postID = @postID;";
            SqlCommand cmd10 = new SqlCommand(sql10, con10);
            cmd10.Parameters.AddWithValue("@postID", postID);
            cmd10.Parameters.AddWithValue("@numDislikes", newNumDislikesBalance);
            con10.Open();
            cmd10.ExecuteNonQuery();
            con10.Close();
        }

        //splitting the post word by word and reading through the array, comparing the words.
        public bool messageChecker(string post, List<string> BadWordList)
        {


            char delimiter = ' ';
            string[] words = post.Split(delimiter);
            string[] badWords = BadWordList.ToArray();

            foreach (string word in words)
            {
                if (BadWordList.Contains(word.ToLower()))
                {
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