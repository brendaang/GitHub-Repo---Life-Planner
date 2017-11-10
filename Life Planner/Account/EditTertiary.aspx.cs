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

        protected void radioSelectPolyJC_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTrue();
            resetView();
        }

        protected void btnJCPOLYLocation(string area)
        {
            if (radioSelectPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCPOLYTable);
                JCPOLYTable.DataSource = ViewJCPOLYTable;
                JCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectPolyJC.SelectedItem.Text == "Polytechnic")
            {
                DataTable ViewJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCPOLYTable);
                JCPOLYTable.DataSource = ViewJCPOLYTable;
                JCPOLYTable.DataBind();
                con.Close();
            }

        }

        protected void btn_JCPOLYNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnJCPOLYLocation(area);
        }

        protected void btn_JCPOLYSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnJCPOLYLocation(area);
        }

        protected void btn_JCPOLYEast(object sender, EventArgs e)
        {
            string area = "East";
            btnJCPOLYLocation(area);
        }

        protected void btn_JCPOLYWest(object sender, EventArgs e)
        {
            string area = "West";
            btnJCPOLYLocation(area);
        }

        protected void JCPOLYGridView_SelectedIndexChanging(object sender, EventArgs e)
        {

            string JCPOLYName = (string)JCPOLYTable.DataKeys[JCPOLYTable.SelectedIndex].Value;
            Session["JCPOLYName"] = JCPOLYName;
        }

        protected void btnJCPOLYSubmitPlan(object sender, EventArgs e)
        {

        }

        protected void btnJCPOLYContinuePlanning(object sender, EventArgs e)
        {
            //Response.Redirect("CreatePlanFromUniversity.aspx");
        }

        protected void btn_JCPOLYNone(object sender, EventArgs e)
        {
            resetView();
        }

        protected void btn_updateTertiary_Click(object sender, EventArgs e)
        {
            EditPlanDAO ep = new EditPlanDAO();
            SqlConnection con = new DBManager().getConnection();
            int count;
            string id = ep.getSchID(Session["JCPOLYName"].ToString());
            if(radioSelectPolyJC.SelectedItem.Text == "Junior College")
            {
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
            else //selected is poly
            {//not yet
                //string sql = "UPDATE dbo.PathPlan SET polyID=@polyID WHERE accountID=@accountID";
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.Parameters.AddWithValue("@polyID", id);
                //cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                //con.Open();
                //count = Convert.ToInt32(cmd.ExecuteScalar());
                ////update: count == 0
                //if (count == 0)
                //{
                //    alert_placeholder.Visible = true;
                //    alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                //    alertText.Text = "Successfully updated! Redirecting to View Own Plan page...";
                //    Response.AddHeader("REFRESH", "2;URL=ViewOwnPlan.aspx");
                //    con.Close();
                //    con.Dispose();
                //}
                //else
                //{
                //    con.Close();
                //    con.Dispose();
                //    Response.Redirect("Error.aspx");

                //}
            }
        }

        protected void resetView()
        {
            // to load secsch gridview with sec schools after setting it to visible
            //*************to store priSchName into db

            if (radioSelectPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewJCSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCSchTable);
                JCPOLYTable.DataSource = ViewJCSchTable;
                JCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectPolyJC.SelectedItem.Text == "Polytechnic")
            {
                DataTable ViewPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewPolySchTable);
                JCPOLYTable.DataSource = ViewPolySchTable;
                JCPOLYTable.DataBind();
                con.Close();
            }
        }

        protected void setTrue()
        {
            btnJCPOLYNorth.Visible = true;
            btnJCPOLYWest.Visible = true;
            btnJCPOLYEast.Visible = true;
            btnJCPOLYSouth.Visible = true;
            btnJCPOLYNone.Visible = true;
            btn_updateTertiary.Visible = true;
        }

        protected void setFalse()
        {
            btnJCPOLYNorth.Visible = false;
            btnJCPOLYWest.Visible = false;
            btnJCPOLYEast.Visible = false;
            btnJCPOLYSouth.Visible = false;
            btnJCPOLYNone.Visible = false;
            btn_updateTertiary.Visible = false;
        }
    }
}