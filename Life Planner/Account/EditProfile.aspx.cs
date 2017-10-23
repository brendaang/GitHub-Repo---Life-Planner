using Life_Planner.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accountID = Session["accountID"].ToString();

            //Response.Write(accountID);

            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT * FROM[CZ2006 - Life Planner].[dbo].[Account] a INNER JOIN[CZ2006 - Life Planner].[dbo].[AccCreds] ac ON a.accountID = ac.accountID WHERE ac.accountID = @accountID;";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            tb_username.Text = reader["username"].ToString();
            tb_fName.Text = reader["fName"].ToString();
            tb_lName.Text = reader["lName"].ToString();
            tb_email.Text = reader["email"].ToString();
            tb_datepicker.Text = reader["birthdate"].ToString();
            reader.Close();
            con.Close();

            if(!IsPostBack)
            {
                btn_submit_Click();
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {


            SqlConnection con = new DBManager().getConnection();

            string accountID = Session["accountID"].ToString();
            string sql = "UPDATE [CZ2006 - Life Planner].[dbo].[Account] " +
                    "SET fname = @fName, lName = @lName, email = @email, birthdate = @date, role = '0' " +
                    "WHERE accountID = @accountID; ";

                    SqlCommand cmd = new SqlCommand(sql, con);
            
                 

                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    cmd.Parameters.AddWithValue("@fName", tb_fName.Text);
                    cmd.Parameters.AddWithValue("@lName", tb_lName.Text);
                    cmd.Parameters.AddWithValue("@email", tb_email.Text);
                    cmd.Parameters.AddWithValue("@date", tb_datepicker.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

                   

                }
            }




        }
