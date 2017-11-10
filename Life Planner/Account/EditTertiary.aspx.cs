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
    public partial class EditJCPoly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            setFalse();
        }

        protected void radioSelectITEPolyJC_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTrue();
            resetView();
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
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%ITE COLLEGE%';";
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
            resetView();
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
            if(radioSelectITEPolyJC.SelectedItem.Text == "ITE" || radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                btn_updateTertiary1.Visible = true;
                btn_updateTertiary2.Visible = false;
                PolyCoursesTable.Visible = false;
            }

            if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                btn_updateTertiary1.Visible = false;
                btn_updateTertiary2.Visible = true;
                PolyCoursesTable.Visible = true;

                DataTable PolyTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT DISTINCT school, course_name, gceo_cut_off FROM dbo.OLevelCOP WHERE academic_year = (SELECT MAX(academic_year) FROM OLevelCOP)";
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

        protected void POLYCourseGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            string polyCourse = (string)PolyCoursesTable.DataKeys[PolyCoursesTable.SelectedIndex].Value;
            Session["PolyCourse"] = polyCourse;

        }


        protected void btn_updateTertiary_Click(object sender, EventArgs e)
        {
            EditPlanDAO ep = new EditPlanDAO();
            SqlConnection con = new DBManager().getConnection();
            int count;
            if(radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                string id = ep.getSchID(Session["JCName"].ToString());
                string sql = "UPDATE dbo.PathPlan SET jcID=@jcID WHERE accountID=@accountID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@jcID", id);
                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                con.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                //update: count == 0
                if(count == 0)
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                    alertText.Text = "Successfully updated! Redirecting to View Own Plan page...";
                    Response.AddHeader("REFRESH", "2;URL=ViewOwnPlan.aspx");
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    con.Close();
                    con.Dispose();
                    Response.Redirect("Error.aspx");

                }
            }
            else if(radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")//selected is poly
            {
                string id = ep.getSchID(Session["POLYName"].ToString());
                string course = Session["PolyCourse"].ToString();
                string sql = "UPDATE dbo.PathPlan SET polyID=@polyID, polyCourse=@polyCourse WHERE accountID=@accountID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@polyID", id);
                cmd.Parameters.AddWithValue("@polyCourse", course);
                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                con.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                //update: count == 0
                if (count == 0)
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                    alertText.Text = "Successfully updated! Redirecting to View Own Plan page...";
                    Response.AddHeader("REFRESH", "2;URL=ViewOwnPlan.aspx");
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    con.Close();
                    con.Dispose();
                    Response.Redirect("Error.aspx");

                }
            }
            else //for ite
            {
                string id = ep.getSchID(Session["ITEName"].ToString());
                string sql = "UPDATE dbo.PathPlan SET ITEID=@ITEID WHERE accountID=@accountID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ITEID", id);
                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                con.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                //update: count == 0
                if (count == 0)
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                    alertText.Text = "Successfully updated! Redirecting to View Own Plan page...";
                    Response.AddHeader("REFRESH", "2;URL=ViewOwnPlan.aspx");
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    con.Close();
                    con.Dispose();
                    Response.Redirect("Error.aspx");

                }
            }
        }

        protected void resetView()
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE" || radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                btn_updateTertiary1.Visible = true;
                btn_updateTertiary2.Visible = false;
                PolyCoursesTable.Visible = false;
            }

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
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%ITE COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }
        }

        protected void setTrue()
        {
            btnITEJCPOLYNorth.Visible = true;
            btnITEJCPOLYWest.Visible = true;
            btnITEJCPOLYEast.Visible = true;
            btnITEJCPOLYSouth.Visible = true;
            btnITEJCPOLYNone.Visible = true;
            btn_updateTertiary1.Visible = true;
        }

        protected void setFalse()
        {
            btnITEJCPOLYNorth.Visible = false;
            btnITEJCPOLYWest.Visible = false;
            btnITEJCPOLYEast.Visible = false;
            btnITEJCPOLYSouth.Visible = false;
            btnITEJCPOLYNone.Visible = false;
            btn_updateTertiary1.Visible = false;
            btn_updateTertiary2.Visible = false;
        }
    }
}