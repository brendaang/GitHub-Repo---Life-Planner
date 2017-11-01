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


namespace Life_Planner.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
 

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                getProfileToEdit();
            }
        }
        
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string accountID = Session["accountID"].ToString();
            string fname = tb_fName.Text;
            string lname = tb_lName.Text;
            string email = tb_email.Text;
            string dob = tb_datepicker.Text;
            string gender = rbl_gender.SelectedItem.Value;
            int gen = int.Parse(gender);
            editProfile(accountID, fname, lname, email, dob, gen);
                //Updated.
                alert_placeholder.Visible = true;
                alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                alertText.Text = "User account updated successfully created! You will be redirected to the home page shortly.";

                Response.AddHeader("REFRESH", "3;URL=/Default.aspx");
            


        }

        protected void getProfileToEdit()
        {
            string accountID = Session["accountID"].ToString();
            SqlConnection con = new DBManager().getConnection();

            string sql="SELECT * FROM[CZ2006 - Life Planner].[dbo].[Account] a " +
                "INNER JOIN[CZ2006 - Life Planner].[dbo].[AccCreds] ac ON a.accountID = ac.accountID " +
                "WHERE ac.accountID = @accountID;";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();

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
            con.Close();
        }

        protected void editProfile(string accountID,string fname,string lname,string email,string dob, int gen)
        {
                SqlConnection con = new DBManager().getConnection();
                
                string sql = "UPDATE  [CZ2006 - Life Planner].[dbo].[Account] SET  fname=@fname, lname=@lname, email=@email, birthdate=@birthdate, gender=@gender WHERE accountID=@accountID";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@accountID", accountID);
                cmd.Parameters.AddWithValue("@fName", fname);
                cmd.Parameters.AddWithValue("@lName", lname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@birthdate", dob);
                cmd.Parameters.AddWithValue("@gender", gen);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
        }
    }
}




