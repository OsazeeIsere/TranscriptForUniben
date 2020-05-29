using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranscriptForUniben
{
    public partial class courses : System.Web.UI.Page
    {
        private clssettings x = new clssettings();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dtgetdepartment = new System.Data.DataTable();
            dtgetdepartment = getdatabase("Select * From department");
            if (dtgetdepartment.Rows.Count > 0)
            {
                for (var i = 0; i < dtgetdepartment.Rows.Count; i++)
                {
                    ddldepartment.Items.Add(dtgetdepartment.Rows[i]["department"].ToString());
                }
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

        protected void btnsbmit_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dtgetdepartment = new System.Data.DataTable();
                System.Data.DataTable dtgetstudent = new System.Data.DataTable();


                dtgetdepartment = getdatabase("Select * From department");
                if (string.IsNullOrEmpty(txtcoursecode.Text))
                {
                    lbmsg.Text = "Please Enter the Course Code";
                }
                else if (string.IsNullOrEmpty(txtcoursetitle.Text))
                {
                    lbmsg.Text = "Please Enter the Course Title";
                }
                else if (string.IsNullOrEmpty(ddllevel.Text))
                {
                    lbmsg.Text = "Please Select the Curent Level";
                }
                else if (string.IsNullOrEmpty(ddlsemester.Text))
                {
                    lbmsg.Text = "Please Select the Curent Session";
                }
                else
                {
                    adddata("Insert Into courses(department,semester,coursecode,coursetitle,level,courseunit) Values('" + ddldepartment.Text + "','" + ddlsemester.Text + "','" + txtcoursecode.Text + "','" + txtcoursetitle.Text + "','" + ddllevel.Text + "','" + double.Parse(txtunit.Text) + "')");
                    x.audit(Convert.ToString(Session["username"]), "courses", "all", "Course Creation", Convert.ToInt32(Session["adminid"]));
                    txtcoursecode.Text = "";
                    txtcoursetitle.Text = "";
                    txtcoursecode.Text = "";
                    lbmsg.Text = "The Course is Registered";
                }
            }
            catch (Exception ex)
            {
                lbmsg.Text = ex.ToString();
            }

        }


    }
}
