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
    public partial class ViewOwnPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accID = Session["accountID"].ToString();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT * FROM dbo.PathPlan WHERE accountID=@accID";
            string[] info = new string[7];

            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@accID", accID);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                info[0] = dr["priSchID"].ToString();
                info[1] = dr["secSchID"].ToString();
                info[2] = dr["jcID"].ToString();
                info[3] = dr["polyID"].ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("polyCourse")))
                    info[4] =(string)dr["polyCourse"];
                info[5] = dr["uniID"].ToString();
                info[6] = dr["ITEID"].ToString();
            }
            dr.Close();
            con.Close();
            con.Dispose();

            //display of plan info
            displayPlan(info);
            //if no plan created, inform user they have no plan and redirect them to create plan page
            checkFields();
        }

        protected void btn_editPlan_Click(object sender, EventArgs e)
        {
            btn_editPri.Visible = true;
            btn_editSec.Visible = true;
            btn_editTertiary.Visible = true;
            btn_doneEdit.Visible = true;
            btn_editUni.Visible = true;
            btn_editPlan.Visible = false;
        }

        protected void btn_deletePlan_Click(object sender, EventArgs e)
        {
            SqlConnection con = new DBManager().getConnection();
            string nric = getNRIC();

            //delete plan logic here

            string sql = "DELETE FROM dbo.PathPlan WHERE accountID=@accountID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();

            //delete child in childplan
            deleteChild(nric);

            setNA();
            //reset shortest/longest plan
            resetSL();
            btn_deletePlan.Visible = false;
            btn_editPlan.Visible = false;
            alert_placeholder.Visible = true;
            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
            alertText.Text = "Successfully Deleted Plan! Redirecting to create plan page..";
            Response.AddHeader("REFRESH", "0;URL=CreatePlan.aspx");


        }

        protected void displayPlan(string[] info)
        {
            //display in form
            tb_priName.Text = getSchName(info[0]);
            tb_secName.Text = getSchName(info[1]);
            tb_jcName.Text = getSchName(info[2]);
            tb_polyName.Text = getSchName(info[3]);
            if(tb_polyName.Text == "Not Available")
            {
                tb_polyCourse.Text = "Not Available";
            }
            else
            {
                tb_polyCourse.Text = info[4];
            }
            
            tb_uniName.Text = getSchName(info[5]);
            tb_iteName.Text = getSchName(info[6]);

            //copy info[] to edlvl[] for looping 
            string[] edlvl = new string[6];
            edlvl[0] = info[0]; //primary
            edlvl[1] = info[1]; //secondary
            edlvl[2] = info[2]; //jc
            edlvl[3] = info[3]; //poly
            edlvl[4] = info[5]; //uni
            edlvl[5] = info[6]; //ite
            
       

			int currentEdLevel = 0; //0 primary, 1 secondary, 2 jc, 3 poly, 4 uni, 5 ite
            for(int i=0; i<6; i++)
            {
                if(edlvl[i] == "")
                {
                    currentEdLevel = i;
                    break;
                }
                if(i==5 && edlvl[i] != "") //ite not null
                {
                    currentEdLevel = i;
                }
            }
            
			int shortest = getShortestPath(edlvl, currentEdLevel); //years
			int longest = getLongestPath(edlvl, currentEdLevel); //years



			//not including kindergarten
			tb_shortestTime.Text = shortest + " year(s)";
			tb_longestTime.Text = longest + " year(s)";
		}

		protected int getShortestPath(string[] info, int curr) {
			SqlConnection con = new DBManager().getConnection();
			string sql = "SELECT shortest FROM dbo.Module WHERE moduleID <= @curr ORDER BY moduleID";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@curr", curr+1);
			con.Open();

			SqlDataReader dr = cmd.ExecuteReader();
			int[] shortest = new int[7];
			int shortestPath = 0;
			int i = 0;
			while (dr.Read()) {
				//shortest[i] = (int) dr["shortest"];
				shortestPath += (int)dr["shortest"];
                i++;
			}
            

            if (info[3] != "" && i<4)//poly
            {
				shortestPath += 3;
			}

            if(info[4] != "" && i<5)//uni
            {
                shortestPath += 4;
            }

            if(info[5] != "" && i <= 6)//ite
            {
                shortestPath += 2;
            }
			shortestPath -= 2; //for kindergarten (since we do not show, we offset -2)



			con.Close();
			con.Dispose();
			return shortestPath;
		}

        protected int getLongestPath(string[] info, int curr) {
			SqlConnection con = new DBManager().getConnection();
			string sql = "SELECT longest FROM dbo.Module WHERE moduleID <= @curr ORDER BY moduleID";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@curr", curr+1);
			con.Open();

			SqlDataReader dr = cmd.ExecuteReader();
			int[] longest = new int[7];
			int longestPath = 0;
			int i = 0;
			while (dr.Read()) {
				//shortest[i] = (int) dr["shortest"];
				longestPath += (int)dr["longest"];
				i++;
			}
			if (info[3] != "" && i<4)//poly
            {
				longestPath += 5;
			}
			if (info[4] != "" && i<5)//uni
            {
				longestPath += 6;
			}
            if(info[5] != "" && i <= 6)//ite
            {
                longestPath += 3;
            }

			longestPath -= 3; //for kindergarten (since we do not show, we offset -3)



			con.Close();
			con.Dispose();
			return longestPath;
		}

		protected string getSchName(string schID)
        {
            if(!string.IsNullOrEmpty(schID))    
            {
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name FROM dbo.Schools WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", schID);

                con.Open();
                string schName = cmd.ExecuteScalar().ToString();
                con.Close();
                con.Dispose();
                return schName;
            }
            return "Not Available";
        }

        protected void checkFields()
        {
            if(tb_priName.Text == tb_secName.Text)
                if(tb_secName.Text == tb_jcName.Text)
                    if(tb_jcName.Text == tb_polyName.Text)
                        if(tb_polyName.Text == tb_polyCourse.Text)
                            if(tb_polyCourse.Text == tb_iteName.Text)
                                if(tb_iteName.Text == tb_uniName.Text)
                                    if(tb_uniName.Text == "Not Available")
                                    { //no plans
                                      //reset shortest/longest plan
                                        resetSL();
                                        btn_deletePlan.Visible = false;
                                        btn_editPlan.Visible = false;
                                        alert_placeholder.Visible = true;
                                        alert_placeholder.Attributes["class"] = "alert alert-warning alert-dismissable";
                                        alertText.Text = "No Existing Plan! Redirecting to Create Plan page...";
                                        Response.AddHeader("REFRESH", "0;URL=CreatePlan.aspx");
                                    }
        }

        protected void btn_editPri_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPrimarySch.aspx");
        }

        protected void btn_editSec_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditSecSch.aspx");
        }

        protected void btn_editTertiary_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditTertiary.aspx");
        }

        protected void btn_editUni_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditUni.aspx");
        }
        protected void btn_doneEdit_Click(object sender, EventArgs e)
        {
            btn_editPri.Visible = false;
            btn_editSec.Visible = false;
            btn_editTertiary.Visible = false;
            btn_doneEdit.Visible = false;
            btn_editPlan.Visible = true;
            btn_editUni.Visible = false;
        }

        protected void setNA()
        {
            tb_priName.Text = "";
            tb_secName.Text = "";
            tb_jcName.Text = "";
            tb_iteName.Text = "";
            tb_polyName.Text = "";
            tb_polyCourse.Text = "";
            tb_uniName.Text = "";
            tb_shortestTime.Text = "";
            tb_longestTime.Text = "";
        }

        protected void resetSL()
        {
            tb_shortestTime.Text = "Not Available";
            tb_longestTime.Text = "Not Available";
        }

        protected void deleteChild(string nric)
        {
            //delete child in childplan
            SqlConnection con = new DBManager().getConnection();
            string sql = "DELETE FROM dbo.Child WHERE NRIC=@NRIC";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@NRIC", nric);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }

        protected string getNRIC()
        {
            SqlConnection con = new DBManager().getConnection();
            //get nric of child plan
            string sql = "SELECT NRIC FROM dbo.PathPlan WHERE accountID=@accountID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
            con.Open();
            string nric = cmd.ExecuteScalar().ToString();
            con.Close();
            cmd.Dispose();
            return nric;
        }
    }
}