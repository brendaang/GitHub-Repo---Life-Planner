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
    public partial class Register : Page
    {
        validateUser vu = new validateUser();
        //private DBManager dm;
        
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            // Resets all form fields
            resetField();
            
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

                    int usernameValid = vu.UsernameValid(tb_username.Text);
                    int emailValid = vu.EmailValid(tb_email.Text);
                    
                    // If both checks are true
                    if (usernameValid + emailValid == 3)
                    {
                        vu.Register(tb_fName.Text, tb_lName.Text, tb_email.Text, tb_datepicker.Text, rbl_gender.Text, tb_password.Text, tb_username.Text);

                        // Resets all form fields
                        resetField();

                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                            alertText.Text = "User account successfully created! You will be redirected to the login page shortly.";

                            Response.AddHeader("REFRESH", "3;URL=/Account/Login.aspx");
                        //}
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

        public void resetField()
        {
            tb_username.Text = String.Empty;
            tb_fName.Text = String.Empty;
            tb_lName.Text = String.Empty;
            tb_email.Text = String.Empty;
            tb_password.Text = String.Empty;
            tb_rePassword.Text = String.Empty;
            tb_datepicker.Text = String.Empty;
            rbl_gender.SelectedIndex = 0;
        }
    }
}