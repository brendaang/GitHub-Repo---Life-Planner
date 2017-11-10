using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Life_Planner.Data;

namespace Life_Planner.Account
{
    public partial class CreatePlanFromJCPOLY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }

        protected void radioSelectITEPolyJC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                PolyCoursesTable.Visible = false;

                DataTable ViewITEJCSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCSchTable);
                ITEJCPOLYTable.DataSource = ViewITEJCSchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                PolyCoursesTable.Visible = true;
                btnITEJCPOLYCont.Visible = false;
                btnITEJCPOLYSubmit.Visible = false;

                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                PolyCoursesTable.Visible = false;

                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%ITE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

        }

        protected void btnITEJCPOLYLocation(string area)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCPOLYTable);
                ITEJCPOLYTable.DataSource = ViewJCPOLYTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {

                DataTable ViewITEJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCPOLYTable);
                ITEJCPOLYTable.DataSource = ViewITEJCPOLYTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                DataTable ViewITEJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%ITE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCPOLYTable);
                ITEJCPOLYTable.DataSource = ViewITEJCPOLYTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

        }

        protected void btn_ITEJCPOLYNone(object sender, EventArgs e)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewITEJCSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCSchTable);
                ITEJCPOLYTable.DataSource = ViewITEJCSchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%ITE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }
        }

        protected void btn_ITEJCPOLYNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnITEJCPOLYLocation(area);
        }

        protected void btn_ITEJCPOLYSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnITEJCPOLYLocation(area);
        }

        protected void btn_ITEJCPOLYEast(object sender, EventArgs e)
        {
            string area = "East";
            btnITEJCPOLYLocation(area);
        }

        protected void btn_ITEJCPOLYWest(object sender, EventArgs e)
        {
            string area = "West";
            btnITEJCPOLYLocation(area);
        }

        protected void ITEJCPOLYGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnITEJCPOLYSubmit.Visible = true;
            btnITEJCPOLYCont.Visible = true;

            if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                PolyCoursesTable.Visible = true;
                btnITEJCPOLYCont.Visible = false;
                btnITEJCPOLYSubmit.Visible = false;

                DataTable PolyTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT DISTINCT OLevelCOP.school, OLevelCOP.course_name, Min(OLevelCOP.gceo_cut_off) AS gceo_cut_off FROM [CZ2006 - Life Planner].[dbo].[OLevelCOP] GROUP BY OLevelCOP.school, OLevelCOP.course_name;";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(PolyTable);
                PolyCoursesTable.DataSource = PolyTable;
                PolyCoursesTable.DataBind();
                con.Close();
            }

            string ITEJCPOLYName = (string)ITEJCPOLYTable.DataKeys[ITEJCPOLYTable.SelectedIndex].Value;

            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
                Session["JCName"] = ITEJCPOLYName;
            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
                Session["ITEName"] = ITEJCPOLYName;
            if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
                Session["POLYName"] = ITEJCPOLYName;
        }

        protected void btnITEJCPOLYSubmitPlan(object sender, EventArgs e)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                int priSchID1, secSchID1, jcID;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                    con.Open();
                    priSchID1 = (int)cmd2.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql3 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@schoolname", Session["secSchName"].ToString());

                    con.Open();
                    secSchID1 = (int)cmd3.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.Parameters.AddWithValue("@schoolname", Session["JCName"].ToString());

                    con.Open();
                    jcID = (int)cmd4.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, jcID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @jcID, @accountID);";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@NRIC", Session["newChild"].ToString());
                    cmd.Parameters.AddWithValue("@secSchID", secSchID1);
                    cmd.Parameters.AddWithValue("@priSchID", priSchID1);
                    cmd.Parameters.AddWithValue("@jcID", jcID);
                    cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
               
            }

            if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                int priSchID1, secSchID1, polyID;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                    con.Open();
                    priSchID1 = (int)cmd2.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql3 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@schoolname", Session["secSchName"].ToString());

                    con.Open();
                    secSchID1 = (int)cmd3.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.Parameters.AddWithValue("@schoolname", Session["POLYName"].ToString());

                    con.Open();
                    polyID = (int)cmd4.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, polyID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @polyID, @accountID);";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@NRIC", Session["newChild"].ToString());
                    cmd.Parameters.AddWithValue("@secSchID", secSchID1);
                    cmd.Parameters.AddWithValue("@priSchID", priSchID1);
                    cmd.Parameters.AddWithValue("@polyID", polyID);
                    cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }

            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                int priSchID1, secSchID1, iteID;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                    con.Open();
                    priSchID1 = (int)cmd2.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql3 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@schoolname", Session["secSchName"].ToString());

                    con.Open();
                    secSchID1 = (int)cmd3.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.Parameters.AddWithValue("@schoolname", Session["ITEName"].ToString());

                    con.Open();
                    iteID = (int)cmd4.ExecuteScalar();
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, polyID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @ITEID, @accountID);";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@NRIC", Session["newChild"].ToString());
                    cmd.Parameters.AddWithValue("@secSchID", secSchID1);
                    cmd.Parameters.AddWithValue("@priSchID", priSchID1);
                    cmd.Parameters.AddWithValue("@ITEID", iteID);
                    cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }


            //redirect to view plan
            Response.AddHeader("REFRESH", "3;URL=/Account/ChangePassword.aspx");
        }

        protected void btnITEJCPOLYContinuePlanning(object sender, EventArgs e)
        {
            //if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
                //Response.Redirect("CreatePlanFromJCPOLY.aspx");
            //else
                //Response.Redirect("CreatePlanFromUni.aspx");
        }

        protected void POLYCourseGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnITEJCPOLYSubmit2.Visible = true;
            btnITEJCPOLYCont2.Visible = true;

            string polyCourse = (string)PolyCoursesTable.DataKeys[PolyCoursesTable.SelectedIndex].Value;
            Session["PolyCourse"] = polyCourse;

        }

        protected void btnITEJCPOLYSubmitPlan2(object sender, EventArgs e)
        {
            
        }

        protected void btnITEJCPOLYContinuePlanning2(object sender, EventArgs e)
        {
            
        }
    }
}