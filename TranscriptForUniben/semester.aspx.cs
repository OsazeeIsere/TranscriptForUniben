using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranscriptForUniben
{
    public partial class semester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {

                System.Data.DataTable dtgetadmin = new System.Data.DataTable();

                dtgetadmin = getdatabase("Select * From semester");
                if (string.IsNullOrEmpty(txtsemester.Text))
                {
                    lbmsg.Text = "Please Enter The Semester";
                }
                else if (dtgetadmin.Rows.Count < 2)
                {
                    adddata("Insert Into semester(semester) Values('" + txtsemester.Text + "')");

                    txtsemester.Text = "";
                    lbmsg.Text = "The Semester is Registered";
                }
                else
                {
                    lbmsg.Text = "Please will already have The Maximum Semester";
                }
            }
            catch (Exception ex)
            {
                lbmsg.Text = ex.ToString();
            }
        }

        private System.Data.DataTable getdatabase(string strcommand)
        {
            System.Data.DataTable tempgetdatabase = null;
            tempgetdatabase = new System.Data.DataTable();
            MySql.Data.MySqlClient.MySqlConnection cn = new MySql.Data.MySqlClient.MySqlConnection();
            MySql.Data.MySqlClient.MySqlDataAdapter ad = new MySql.Data.MySqlClient.MySqlDataAdapter();
            MySql.Data.MySqlClient.MySqlCommand cm = new MySql.Data.MySqlClient.MySqlCommand();
            string strconnection = "";
            strconnection = "Server=localhost;Port=3306;Database=studentsrecord;Uid=root;Pwd=prayer;";
            cn.ConnectionString = strconnection;
            cn.Open();
            cm.CommandText = strcommand;
            ad.SelectCommand = cm;
            cm.Connection = cn;
            System.Data.DataTable dt = new System.Data.DataTable();
            ad.Fill(dt);
            tempgetdatabase = dt;
            cn.Close();
            return tempgetdatabase;
        }
        public void adddata(string strinsert)
        {
            MySql.Data.MySqlClient.MySqlConnection cn = new MySql.Data.MySqlClient.MySqlConnection();
            MySql.Data.MySqlClient.MySqlDataAdapter ad = new MySql.Data.MySqlClient.MySqlDataAdapter();
            MySql.Data.MySqlClient.MySqlCommand cm = new MySql.Data.MySqlClient.MySqlCommand();

            string strconnection = "";
            strconnection = "server= localhost;port=3306;database=studentsrecord;uid=root;pwd=prayer";
            cn.ConnectionString = strconnection;
            cn.Open();
            cm.CommandText = strinsert;
            cm.Connection = cn;
            cm.ExecuteNonQuery();
            cn.Close();
        }

    }
}