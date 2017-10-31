using Life_Planner.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Life_Planner.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
 

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            string accountID = Session["accountID"].ToString();


            try
            {
                if (!string.IsNullOrEmpty(Session["accountID"].ToString()))
                {
                    if (!Page.IsPostBack)
                    {
                        showData();

                    }

                }
                else
                {
                    ShowMessage("Please log in!");

                }
               
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }





        }

       
        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language = 'javascript' > alert('" + msg + "');</ script > ");
        }
        private void showData()
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string accountID = Session["accountID"].ToString();

                SqlCommand cmd = new SqlCommand("SELECT * FROM[CZ2006 - Life Planner].[dbo].[Account] a " +
                    "INNER JOIN[CZ2006 - Life Planner].[dbo].[AccCreds] ac ON a.accountID = ac.accountID " +
                    "WHERE ac.accountID = @accountID;", conn);


                cmd.Parameters.AddWithValue("@accountID", accountID);


                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                tb_username.Text = reader["username"].ToString();
                tb_fName.Text = reader["fName"].ToString();
                tb_lName.Text = reader["lName"].ToString();
                tb_email.Text = reader["email"].ToString();

                string datetime = reader["birthdate"].ToString();
                string b = datetime.Split(' ')[0];
                tb_datepicker.Text = b;
              
                string gender = reader["gender"].ToString();

      
                if (gender == "1")
                {
                    rbl_gender.Items.FindByValue("1").Selected = true;
                
                }
                else
                {
                    rbl_gender.Items.FindByValue("0").Selected = true;
                }
            

           

                reader.Close();
            }
            catch (SqlException ex)
            {
                ShowMessage(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string accountID = Session["accountID"].ToString();
                int gender = int.Parse(rbl_gender.SelectedItem.Value);
             
               // string sql = "UPDATE dbo.Account SET fname=@fname, lname=@lname, email=@email, birthdate=@birthdate, gender=@gender WHERE accountID=@accountID";
                //SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd = new SqlCommand("UPDATE [CZ2006 - Life Planner].[dbo].[Account] " +
               "SET fname =@fName , lName =@lName, email = @email, birthdate = @date, gender= @gender, role = '0' " +
                  "WHERE accountID = @accountID ", conn);



                cmd.Parameters.AddWithValue("@accountID", accountID);
                cmd.Parameters.AddWithValue("@fName", tb_fName.Text);
                cmd.Parameters.AddWithValue("@lName", tb_lName.Text);
                cmd.Parameters.AddWithValue("@email", tb_email.Text);
                cmd.Parameters.AddWithValue("@date", tb_datepicker.Text);


                cmd.Parameters.AddWithValue("@gender", gender);

                //Response.Write("TEST1");
                //int a = cmd.ExecuteNonQuery();
              int a=  cmd.ExecuteNonQuery();
               // Response.Write("TEST2");
               // Response.Write(gender);
          
                if (a == 0)
                {
                   // Response.Write("TEST3");
                    //Not updated.

                    alert_placeholder.Visible = false;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Updated failed, Please try again later. ";

                    Response.AddHeader("REFRESH", "3;URL=/Account/ViewMyProfile.aspx");
                }

                else
                {
                    //Response.Write("TEST4");
                    //Updated.
                    alert_placeholder.Visible = true;
                       alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                       alertText.Text = "User account updated successfully created! You will be redirected to the home page shortly.";

                      Response.AddHeader("REFRESH", "3;URL=/Default.aspx");
                }
                





            }
            catch (SqlException ex)
            {
                ShowMessage(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}




