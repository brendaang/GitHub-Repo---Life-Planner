﻿using Microsoft.AspNet.Identity;
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

namespace Life_Planner.Account
{
    public partial class Register : Page
    {
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            // Resets all form fields
            tb_username.Text = String.Empty;
            tb_fName.Text = String.Empty;
            tb_lName.Text = String.Empty;
            tb_email.Text = String.Empty;
            tb_password.Text = String.Empty;
            tb_rePassword.Text = String.Empty;
            tb_datepicker.Text = String.Empty;
            rbl_gender.SelectedIndex = 0;
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ValidateRC())
                {
                    /*
                 * Explamation behind logic:
                 * 
                 * If usernameValid and emailValid are both true, then result = 3 (1 + 2)
                 * If only usernameValid is true, result = 1
                 * If only emailValid is true, result = 2
                 * 
                 * React accordingly to results
                 */

                    int usernameValid = UsernameValid(tb_username.Text);
                    int emailValid = EmailValid(tb_email.Text);

                    // If both checks are true
                    if (usernameValid + emailValid == 3)
                    {
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                        {

                            string sql = "INSERT INTO dbo.Account(fName, lName, email, birthdate, gender, role) VALUES (@fName, @lName, @email, @date, @gender, 0);";

                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.Parameters.AddWithValue("@fName", tb_fName.Text);
                            cmd.Parameters.AddWithValue("@lName", tb_lName.Text);
                            cmd.Parameters.AddWithValue("@email", tb_email.Text);
                            cmd.Parameters.AddWithValue("@date", tb_datepicker.Text);
                            cmd.Parameters.AddWithValue("@gender", rbl_gender.Text);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Generate random salt
                            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
                            byte[] tokenData = new byte[4];
                            rng.GetBytes(tokenData);
                            string salt = Convert.ToBase64String(tokenData);

                            // Generate hash from salted password
                            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + tb_password.Text));
                            string hashedPwd = Convert.ToBase64String(hash);

                            string sql2 = "INSERT INTO dbo.AccCreds(username, passwordSalt, passwordHash, accountID) VALUES (@Username, @PasswordSalt, @PasswordHash, @accountID);";
                            cmd = new SqlCommand(sql2, con);

                            cmd.Parameters.AddWithValue("@Username", tb_username.Text);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@PasswordHash", hashedPwd);
                            cmd.Parameters.AddWithValue("@accountID", retrieveAccID(tb_email.Text));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con.Dispose();
                            cmd.Dispose();

                            // Resets all form fields
                            tb_username.Text = String.Empty;
                            tb_fName.Text = String.Empty;
                            tb_lName.Text = String.Empty;
                            tb_email.Text = String.Empty;
                            tb_password.Text = String.Empty;
                            tb_rePassword.Text = String.Empty;
                            tb_datepicker.Text = String.Empty;
                            rbl_gender.SelectedIndex = 0;

                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                            alertText.Text = "User account successfully created! You will be redirected to the login page shortly.";

                            Response.AddHeader("REFRESH", "3;URL=/Account/Login.aspx");
                        }
                    }
                    else if (usernameValid + emailValid == 2)
                    {
                        alert_placeholder.Visible = true;
                        alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                        alertText.Text = "Username already taken. Please use another one.";
                    }
                    else if (usernameValid + emailValid == 1)
                    {
                        alert_placeholder.Visible = true;
                        alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                        alertText.Text = "Email already taken. Please use another one.";
                    }
                    else
                    {
                        alert_placeholder.Visible = true;
                        alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                        alertText.Text = "Username or email already taken. Please use another one.";
                    }
                }
                else
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Please check recaptcha.";
                }
                
            }
        }


        private static int UsernameValid(string username)
        {
            int valid = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
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
            }
            return valid;
        }

        private static int EmailValid(string email)
        {
            int valid = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
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
            }
            return valid;
        }

        private static int retrieveAccID(string email)
        {
            int result = new int();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
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

        public bool ValidateRC()
        {
            string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
            bool Valid = false;
            //Request to Google Server
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            (" https://www.google.com/recaptcha/api/siteverify?secret=6LfqzDUUAAAAAFJXrBM_3CaIYAI5qelOedMcGihR&response=" + Response);
            try
            {
                //Google recaptcha Response
                using (WebResponse wResponse = req.GetResponse())
                {

                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                        Valid = Convert.ToBoolean(data.success);
                    }
                }

                return Valid;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public class MyObject
        {
            public string success { get; set; }
        }
    }
}