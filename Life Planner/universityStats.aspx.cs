using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Life_Planner.Data;
namespace Life_Planner
{


    public partial class universityStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("http://www.tablebuilder.singstat.gov.sg/publicfacing/api/json/title/15207.json").Result;


            if (response.IsSuccessStatusCode)
            {

                string JSON = response.Content.ReadAsStringAsync().Result;
                //Deserialize to strongly typed class i.e., RootObject
                RootObject obj = JsonConvert.DeserializeObject<RootObject>(JSON);

                //loop through the list and insert into database
                foreach (Level1 currencyItem in obj.Level1)
                {
                    Console.WriteLine(currencyItem.year + "-" + currencyItem.level_1 + "-" + currencyItem.value);
                    SqlConnection con3 = new DBManager().getConnection();

                    string sql3 =
                        "INSERT INTO [CZ2006 - Life Planner].[dbo].[Table1] (year, level_1, value) VALUES (@year, @level_1, @value);";
                    SqlCommand cmd3 = new SqlCommand(sql3, con3);
                    cmd3.Parameters.AddWithValue("@year", currencyItem.year);
                    cmd3.Parameters.AddWithValue("@level_1", currencyItem.level_1);
                    cmd3.Parameters.AddWithValue("@value", currencyItem.value);

                    con3.Open();
                    cmd3.ExecuteNonQuery();
                    con3.Close();



                }
            }
        }

        public class Level1
        {
            public string year { get; set; }
            public string level_1 { get; set; }
            public string value { get; set; }
        }

        public class Level2
        {
            public string year { get; set; }
            public string level_1 { get; set; }
            public string level_2 { get; set; }
            public string value { get; set; }
        }

        public class RootObject
        {
            public List<Level1> Level1 { get; set; }
            public List<Level2> Level2 { get; set; }
            public string Note { get; set; }
            public List<string> VariableFootNotes { get; set; }
            public string Unit { get; set; }
            public string Datesource { get; set; }
            public string GeneratedBy { get; set; }
            public string DateGenerated { get; set; }
            public string Contact { get; set; }
        }
   



        //public class Value
        //{
        //    public string Code { get; set; }
        //    public string Description { get; set; }
        //    public double Buy { get; set; }
        //    public double Sell { get; set; }
        //}

        //public class CurrencyList
        //{
        //    public string Key { get; set; }
        //    public Value Value { get; set; }
        //}

        //public class RootObject
        //{
        //    public List<CurrencyList> CurrencyList { get; set; }
        //}


    }
}