using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Life_Planner.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public class validateUser
    {
        //public methods
        public int UsernameValid(string username)
        {
            return UsernameValid1(username);
        }

        public int EmailValid(string email)
        {
            return EmailValid1(email);
        }
        public int retrieveAccID(string email)
        {
            return retrieveAccID1(email);
        }

        public string RetrieveSalt(string username)
        {
            return RetrieveSalt1(username);
        }

        public bool AuthenticateUser(string username, string password, string salt)
        {
            return AuthenticateUser1(username, password, salt);
        }

        public bool AuthenticateUsername(string username)
        {
            return AuthenticateUsername1(username);
        }

        public int GetAccountID(string username, string password, string salt)
        {
            return GetAccountID1(username, password, salt);
        }

        public bool AuthenticateEmail(string email)
        {
            return AuthenticateEmail1(email);
        }

        public string getEmail(string accID)
        {
            return getEmail1(accID);
        }

        public string getUsername(string accID)
        {
            return getUsername1(accID);
        }

        public string[] getCreds(string username)
        {
            return getCredsByUsername(username);
        }

        public string[] getCreds1(string email)
        {
            return getCredsByEmail(email);
        }

        public bool updatePasswordByAccID(string password, string accID, string salt)
        {
            return updatePasswordByAccID1(password, accID, salt);
        }
        public bool updatePasswordByUsername(string password, string username, string salt)
        {
            return updatePasswordByUsername1(password, username, salt);
        }

        public bool AuthenticatePassword(string password, string salt)
        {
            return AuthenticatePassword1(password, salt);
        }

        public void Register(string fn, string ln, string email, string birthdate, string gender, string password, string username)
        {
            DBManager dm = new DBManager();
            SqlConnection con = dm.getConnection();
            //dm = new DBManager();

            SqlParameter[] sqlParameters = new SqlParameter[5];

            string sql = "INSERT INTO dbo.Account(fName, lName, email, birthdate, gender, role) VALUES (@fName, @lName, @email, @date, @gender, 0);";
            sqlParameters[0] = new SqlParameter("@fName", SqlDbType.NVarChar);
            sqlParameters[0].Value = fn;
            sqlParameters[1] = new SqlParameter("@lName", SqlDbType.NVarChar);
            sqlParameters[1].Value = ln;
            sqlParameters[2] = new SqlParameter("@email", SqlDbType.NVarChar);
            sqlParameters[2].Value = email;
            sqlParameters[3] = new SqlParameter("@date", SqlDbType.Date);
            sqlParameters[3].Value = birthdate;
            sqlParameters[4] = new SqlParameter("@gender", SqlDbType.TinyInt);
            sqlParameters[4].Value = gender;

            dm.executeInsertQuery(sql, sqlParameters);

            //SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.Parameters.AddWithValue("@fName", tb_fName.Text);
            //    cmd.Parameters.AddWithValue("@lName", tb_lName.Text);
            //    cmd.Parameters.AddWithValue("@email", tb_email.Text);
            //    cmd.Parameters.AddWithValue("@date", tb_datepicker.Text);
            //    cmd.Parameters.AddWithValue("@gender", rbl_gender.Text);

            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    con.Close();

            // Generate random salt
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] tokenData = new byte[4];
            rng.GetBytes(tokenData);
            string salt = Convert.ToBase64String(tokenData);

            // Generate hash from salted password
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password));
            string hashedPwd = Convert.ToBase64String(hash);

            string sql2 = "INSERT INTO dbo.AccCreds(username, passwordSalt, passwordHash, accountID) VALUES (@Username, @PasswordSalt, @PasswordHash, @accountID);";
            SqlParameter[] sqlParameters2 = new SqlParameter[4];
            sqlParameters2[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
            sqlParameters2[0].Value = username;
            sqlParameters2[1] = new SqlParameter("@PasswordSalt", SqlDbType.NVarChar);
            sqlParameters2[1].Value = salt;
            sqlParameters2[2] = new SqlParameter("@PasswordHash", SqlDbType.NVarChar);
            sqlParameters2[2].Value = hashedPwd;
            sqlParameters2[3] = new SqlParameter("@accountID", SqlDbType.Int);
            sqlParameters2[3].Value = retrieveAccID(email);

            dm.executeInsertQuery(sql2, sqlParameters2);
        }

        // private methods
        private static bool AuthenticatePassword1(string password, string salt)
        {
            bool isValid = false;
            //Generate hash from salted password
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password));
            string hashedPwd = Convert.ToBase64String(hash);

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT COUNT(*) FROM dbo.AccCreds WHERE passwordHash=@password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@password", hashedPwd);

                con.Open();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                if (result == 1)
                {
                    isValid = true;
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    isValid = false;
                    con.Close();
                    con.Dispose();
                }
            //}

            return isValid;
        }

        private static bool updatePasswordByAccID1(string password, string accID, string salt)
        {
            bool isValid = false;
            //Generate hash from salted password
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password));
            string hashedPwd = Convert.ToBase64String(hash);

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string sql = "UPDATE dbo.AccCreds SET passwordHash=@passwordHash WHERE accountID=@accID";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@passwordHash", hashedPwd);
                cmd.Parameters.AddWithValue("@accID", accID);

                con.Open();

                int count;
                count = Convert.ToInt32(cmd.ExecuteScalar());
                // updated: count == 0
                if (count == 0)
                {
                    isValid = true;
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    isValid = false;
                    con.Close();
                    con.Dispose();
                }
            //}

            return isValid;
        }

        private string[] getCredsByEmail(string email)
        {
            //get creds by email
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string query = "SELECT * FROM dbo.Account WHERE email=@email";
                string[] creds = new string[2];

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader dr = cmd.ExecuteReader();

                //save creds
                while (dr.Read())
                {
                    creds[0] = (string)dr["email"];
                    creds[1] = dr["accountID"].ToString();
                }

                dr.Close();
                con.Close();
                con.Dispose();

                return creds;
            //}
        }
        private string[] getCredsByUsername(string username)
        {
            //get credentials by username
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string query = "SELECT * FROM dbo.AccCreds WHERE username=@username";
                string[] creds = new string[3];

                con.Open();
                SqlCommand cmd1 = new SqlCommand(query, con);
                cmd1.Parameters.AddWithValue("@username", username);
                SqlDataReader dr = cmd1.ExecuteReader();

                //save creds to array
                while (dr.Read())
                {
                    creds[0] = (string)dr["username"];
                    creds[1] = dr["accountID"].ToString();
                    creds[2] = (string)dr["passwordHash"];
                }
                dr.Close();
                con.Close();
                con.Dispose();

                return creds;
            //}
        }

        private static string getUsername1(string accID)
        {
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string query = "SELECT * FROM dbo.AccCreds WHERE accountID=@accID";
                string[] username = new string[1];

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@accID", accID);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    username[0] = (string)dr["username"];
                }
                string name = username[0].ToString();

                con.Close();
                con.Dispose();
                return name;
           // }

        }
        private static string getEmail1(string accID)
        {
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string query = "SELECT * FROM dbo.Account WHERE accountID=@accID";
                string[] email = new string[1];

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@accID", accID);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    email[0] = (string)dr["email"];
                }

                string add = email[0].ToString();

                con.Close();
                con.Dispose();
                return add;
            //}

        }

        private static bool updatePasswordByUsername1(string password, string username, string salt)
        {
            bool isValid = false;
            //Generate hash from salted password
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password));
            string hashedPwd = Convert.ToBase64String(hash);

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string sql = "UPDATE dbo.AccCreds SET passwordHash=@passwordHash WHERE username=@username";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@passwordHash", hashedPwd);
                cmd.Parameters.AddWithValue("@username", username);

                con.Open();

                int count;
                count = Convert.ToInt32(cmd.ExecuteScalar());
                // updated: count == 0
                if (count == 0)
                {
                    isValid = true;
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    isValid = false;
                    con.Close();
                    con.Dispose();
                }
           // }

            return isValid;
        }

        private static bool AuthenticateEmail1(string email)
        {
            bool isValid = false;
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT COUNT(*) FROM dbo.Account WHERE email=@email";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@email", email);

                con.Open();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                if (result == 1)
                {
                    isValid = true;
                    con.Close();
                    con.Dispose();
                }

                else
                {
                    isValid = false;
                    con.Close();
                    con.Dispose();
                }
            //}
            return isValid;
        }

        private static int GetAccountID1(string username, string password, string salt)
        {
            int accountID = 0;
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            //Generate hash from salted password
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password));
                string hashedPwd = Convert.ToBase64String(hash);

                string sql = "SELECT accountID FROM dbo.AccCreds WHERE username=@username AND passwordHash=@passwordHash";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@passwordHash", hashedPwd);

                con.Open();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                if (result != null)
                    accountID = result;
                else
                    accountID = 0;
           // }
            return accountID;
        }

        private static bool AuthenticateUser1(string username, string password, string salt)
        {
            bool isValid = false;
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();

            //Generate hash from salted password
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password));
                string hashedPwd = Convert.ToBase64String(hash);

                string sql = "SELECT COUNT(*) FROM dbo.AccCreds WHERE username=@username AND passwordHash=@passwordHash";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@passwordHash", hashedPwd);

                con.Open();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                if (result == 1)
                    isValid = true;
                else
                    isValid = false;
           // }
            return isValid;
        }

        private static bool AuthenticateUsername1(string username)
        {
            bool isValid = false;
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT COUNT(*) FROM dbo.AccCreds WHERE username=@username";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", username);

                con.Open();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                if (result == 1)
                {
                    isValid = true;
                    con.Close();
                    con.Dispose();
                }

                else
                {
                    isValid = false;
                    con.Close();
                    con.Dispose();
                }

            //}

            return isValid;
        }

        private static string RetrieveSalt1(string username)
        {
            string salt = "";
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT passwordSalt FROM dbo.AccCreds WHERE username=@username";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", username);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result == null)
                    salt = "-1";
                else
                    salt = result.ToString();
           // }
            return salt;
        }
        private static int retrieveAccID1(string email)
        {
            int result = new int();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
           // SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT accountID FROM dbo.Account WHERE email=@email";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@email", email);

                con.Open();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return result;
        }

        private static int UsernameValid1(string username)
        {
            int valid = 0;

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.AccCreds WHERE username=@username", con);
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();

                int result = int.Parse(cmd.ExecuteScalar().ToString());

                if (result == 0)
                    valid = 1;
                else
                    valid = 0;

                con.Close();
                con.Dispose();
                cmd.Dispose();
           // }
            return valid;
        }

        private static int EmailValid1(string email)
        {
            int valid = 0;

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            //{
            SqlConnection con = new DBManager().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Account WHERE email=@email", con);
                cmd.Parameters.AddWithValue("@email", email);
                con.Open();

                int result = int.Parse(cmd.ExecuteScalar().ToString());

                if (result == 0)
                    valid = 2;
                else
                    valid = 0;

                con.Close();
                con.Dispose();
                cmd.Dispose();
            //}
            return valid;
        }

    }
}