using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner {

	public class OLevelModule {
		public string academic_year { get; set; }
		public string jae_course_code { get; set; }
		public string course_name { get; set; }
		public string school { get; set; }
		public int gceo_cut_off { get; set; }
		public string jae_cluster { get; set; }
		public int _id { get; set; }
	}

	public partial class OLevelCOP : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			//Load JSON to BUILD SQL QUERY

			//Initialise SQL Query
			string sql = "";
			sql += "INSERT INTO dbo.OLevelCOP(";

			//Fetch WEBDATA
			System.Net.WebClient webFetcher = new System.Net.WebClient();
			string jsonforOLevelCOP = "https://data.gov.sg/api/action/datastore_search?resource_id=642c5bba-9902-45ed-a637-51a491359017&limit=142";
			string json = webFetcher.DownloadString("https://data.gov.sg/api/action/datastore_search?resource_id=642c5bba-9902-45ed-a637-51a491359017&limit=142");
			//parse into object
			JObject jObject = JObject.Parse(json);
			JToken jFields = jObject["result"]["fields"];
			//Dynamically enumerating "field" property (ignoring the first field, id) into List field
			List<string> fields = new List<string>();
			for (int i = 1; i < jFields.Count() - 1; i++) {
				sql += jFields[i]["id"].ToString() + ", ";
				fields.Add(jFields[i]["id"].ToString());
			}
			//add the last one manually to remove the last ", "
			sql += jFields[jFields.Count() - 1]["id"].ToString();
			fields.Add(jFields[jFields.Count() - 1]["id"].ToString());
			sql += ") VALUES (";
			//sqli
			for (int i = 0; i < fields.Count() - 1; i++) {
				sql += "@" + fields[i] + ", ";
			}
			sql += "@" + fields[fields.Count() - 1];
			sql += ");";
			//sql query now built, TODO: run for every record

			//get all records
			JToken jRecords = jObject["result"]["records"];
			List<string[]> secsch = new List<string[]>();

			//to populate data on table
			//table headers
			TableHeaderRow tableHeader = new TableHeaderRow();
			Table1.Rows.Add(tableHeader);
			foreach (string field in fields) {
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				tableHeaderCell.Text = field;
				tableHeader.Cells.Add(tableHeaderCell);
			}


			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString)) {
				con.Open();
				SqlCommand cmd;

				//DEBUG
				//delete all data in table
				cmd = new SqlCommand("DELETE FROM dbo.OLevelCOP", con);
				cmd.ExecuteNonQuery();


				//loop through all records
				foreach (JToken record in jRecords) {
					//poupulate data
					TableRow tableRow = new TableRow();
					Table1.Rows.Add(tableRow);

					cmd = new SqlCommand(sql, con);
					//populate the values into @placeholders
					foreach (string field in fields) {
						cmd.Parameters.AddWithValue("@" + field, record[field].ToString());

						//for <table>
						TableCell tableCell = new TableCell();
						tableCell.Text = (record[field].ToString().Length > 50) ? record[field].ToString().Substring(0, 47) + "..." : record[field].ToString();
						tableCell.Height = 12;
						tableRow.Cells.Add(tableCell);
					}
					//execute
					cmd.ExecuteNonQuery();
				}
				con.Close();
				//Response.AddHeader("REFRESH", "0;URL=/Account/Login.aspx");
			}

			//done
		}

		protected void Button1_Click(object sender, EventArgs e) {
			TableRow tableRow = new TableRow();
            //clear table data
            Table1.Rows.Clear();
			Table1.Rows.Add(tableRow);
			TableCell aa = new TableCell();
			aa.Text = "Cut off point > " + TextBox1.Text;
			tableRow.Cells.Add(aa);
			tableRow = new TableRow();
			Table1.Rows.Add(tableRow);
			//get which one is lesser
			OLevelModule[] modules = new OLevelModule[256]; // how to get count of query??
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString)) {
				con.Open();
				SqlCommand cmd;

				//onclikc
				String OLevelPoints = TextBox1.Text;

				string query = "SELECT * FROM dbo.OLevelCOP WHERE gceo_cut_off >= @OLevelPoints ORDER BY gceo_cut_off ASC";
				//OLevelModule[] modules = new OLevelModule[256]; // how to get count of query??

				cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@OLevelPoints", OLevelPoints);
				SqlDataReader dr = cmd.ExecuteReader();

				int i = 0;
				//save creds to array
				while (dr.Read()) {
					OLevelModule temp = new OLevelModule();
					temp.academic_year = (string)dr["academic_year"];
					temp.school = (string)dr["school"];
					temp.course_name = (string)dr["course_name"];
					temp.jae_cluster = (string)dr["jae_cluster"];
					temp.jae_course_code = (string)dr["jae_course_code"];
					temp.gceo_cut_off = Convert.ToInt32((byte)dr["gceo_cut_off"]);
					modules[i] = temp;
					
					tableRow = new TableRow();
					Table1.Rows.Add(tableRow);
					//for <table>
					TableCell tableCell = new TableCell();
					tableCell.Text = modules[i].academic_year.ToString();
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = modules[i].school.ToString();
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = modules[i].course_name.ToString();
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = modules[i].jae_cluster.ToString();
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = modules[i].jae_course_code.ToString();
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = modules[i].gceo_cut_off.ToString();
					tableRow.Cells.Add(tableCell);
					tableCell.Height = 12;
					tableRow.Cells.Add(tableCell);

					i++;
				}
				dr.Close();
				con.Close();
				con.Dispose();
			};

			//modules[0].academic_year;
		}
	}
}