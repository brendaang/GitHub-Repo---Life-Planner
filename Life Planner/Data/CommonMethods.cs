using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

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
    }
}