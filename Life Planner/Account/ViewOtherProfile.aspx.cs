using Life_Planner.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner.Account
{
    public partial class ViewOtherProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string authorName = Session["AuthorName"].ToString();
          

            //Response.Write(accountID);

            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT * FROM[CZ2006 - Life Planner].[dbo].[Account] a INNER JOIN[CZ2006 - Life Planner].[dbo].[AccCreds] ac ON a.accountID = ac.accountID WHERE ac.username = @authorName;";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@authorName", authorName);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            uname.Text = reader["username"].ToString();
            tbFirstName.Text = reader["fName"].ToString();
            //reader.Read();
            tbLname.Text = reader["lName"].ToString();
            //reader.Read();
            tbEmail.Text = reader["email"].ToString();

            string datetime = reader["birthdate"].ToString();
            string b = datetime.Split(' ')[0];
            tbBirthDate.Text = b;
        

            string gender = reader["gender"].ToString();
            if (gender == "1")
            {
                tbGender.Text = "Male";
            }
            else
            {
                tbGender.Text = "Female";
            }





            reader.Close();
            con.Close();

        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forum.aspx");
        }
    }
}