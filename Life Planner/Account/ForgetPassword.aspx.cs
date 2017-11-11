using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Life_Planner.Account
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        validateUser vu = new validateUser();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            //alert_placeholder.Visible = true;
            //alert_placeholder.Attributes["class"] = "alert alert-info alert-dismissable";
            //alertText.Text = "Please wait..";

            //get new random password
            string newPass = randomString();

            if (tb_UsernameOrEmail.Text != null && tb_UsernameOrEmail.Text != "")
            {
                //have input
                //check for recaptcha validation
                //string EncodedResponse = Request.Form["g-Recaptcha-Response"];
                //bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "True" ? true : false);

                if (ValidateRC())
                {
                    //Valid Request
                    if (vu.AuthenticateUsername(tb_UsernameOrEmail.Text))
                    {
                        //valid username
                        //retrieve salt
                        string salt = vu.RetrieveSalt(tb_UsernameOrEmail.Text);

                        //get creds of user
                        string[] creds = vu.getCreds(tb_UsernameOrEmail.Text);

                        //get email address of user, creds[1] = accountID
                        string email = vu.getEmail(creds[1]);

                        if (vu.updatePasswordByUsername(newPass, tb_UsernameOrEmail.Text, salt))
                        {
                            //sent email if valid
                            sentMail(newPass, email, tb_UsernameOrEmail.Text);

                            //accID
                            Session["accountID"] = creds[1];

                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                            alertText.Text = "Please check email for new password.";
                            tb_UsernameOrEmail.Text = "";
                            Response.AddHeader("REFRESH", "1;URL=ResetPassword.aspx");
                            //Response.Write("Go to change pw");

                        }
                        else
                        {
                            Session.RemoveAll();
                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                            alertText.Text = "ERROR! Please try again later.";
                            tb_UsernameOrEmail.Text = "";
                        }



                    }
                    else if (vu.AuthenticateEmail(tb_UsernameOrEmail.Text))
                    {
                        //valid email

                        //get creds of user
                        string[] creds = vu.getCreds1(tb_UsernameOrEmail.Text);

                        //get username, creds[1] = accID
                        string username = vu.getUsername(creds[1]);

                        //get accID
                        string accID = creds[1];

                        //retrieve salt
                        string salt = vu.RetrieveSalt(username);


                        if (vu.updatePasswordByUsername(newPass, username, salt))
                        {
                            //sent mail if valid
                            sentMail(newPass, tb_UsernameOrEmail.Text, username);

                            //accID
                            Session["accountID"] = creds[1];

                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                            alertText.Text = "Please check email for new password.";
                            tb_UsernameOrEmail.Text = "";
                            Response.AddHeader("REFRESH", "1;URL=ResetPassword.aspx");
                            //Response.Write("Go to change pw");

                        }
                        else
                        {
                            Session.RemoveAll();
                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                            alertText.Text = "ERROR! Please try again later.";
                            tb_UsernameOrEmail.Text = "";
                        }

                    }
                    else
                    {
                        //invalid
                        alert_placeholder.Visible = true;
                        alert_placeholder.Attributes["class"] = "alert alert-warning alert-dismissable";
                        alertText.Text = "Invalid username or email address.";
                        tb_UsernameOrEmail.Text = "";
                    }
                }
                else
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Please check recaptcha.";
                }

            }
            else
            {
                //no input
                alert_placeholder.Visible = true;
                alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                alertText.Text = "Please enter username or email address";
                tb_UsernameOrEmail.Text = "";
            }
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

        private string randomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }

        private static void sentMail(string newPass, string email, string username)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(email);
            message.Subject = "Reset Password";
            message.From = new System.Net.Mail.MailAddress("forprojectonly1@hotmail.com");
            message.Body = "Hi " + username + ", \n" + "Your new random password is: " + newPass + "\n" + "Please use this password to change your current password.";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.live.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("forprojectonly1@hotmail.com", "4pr0ject0nly1");
            smtp.Send(message);
        }
    }
}