using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranscriptForUniben
{
    public partial class updateuser : System.Web.UI.Page
    {
        private clssettings x = new clssettings();
        public void getdepartments()
        {
            try
            {

                System.Data.DataTable dtgetdepartments = new System.Data.DataTable();
                dtgetdepartments = x.getdatabase("Select * From department");

                ddldepartment.Items.Clear();
                if (dtgetdepartments.Rows.Count > 0)
                {
                    ListItem itm = new ListItem();

                    for (var i = 0; i < dtgetdepartments.Rows.Count; i++)
                    {
                        itm = new ListItem();
                        itm.Value = Convert.ToString(dtgetdepartments.Rows[i]["department"]);
                        itm.Text = dtgetdepartments.Rows[i]["department"].ToString() + " (" + dtgetdepartments.Rows[i]["faculty"].ToString() + ")";
                        ddldepartment.Items.Add(itm);
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void getuserroles()
        {
            try
            {

                System.Data.DataTable dtgetdepartments = new System.Data.DataTable();
                dtgetdepartments = x.getdatabase("Select * From userrole");

                ddluserrole.Items.Clear();
                if (dtgetdepartments.Rows.Count > 0)
                {
                    ListItem itm = new ListItem();

                    for (var i = 0; i < dtgetdepartments.Rows.Count; i++)
                    {
                        itm = new ListItem();
                        itm.Value = Convert.ToString(dtgetdepartments.Rows[i]["role"]);
                        itm.Text = dtgetdepartments.Rows[i]["role"].ToString();
                        ddluserrole.Items.Add(itm);
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["username"] == null))
            {
                Response.Redirect("login.aspx");
            }
            else
            {

                System.Data.DataTable dtgetadmin = new System.Data.DataTable();
                dtgetadmin = x.getdatabase("Select * From admin Where username='" + Session["username"].ToString() + "' And password='" + Session["password"].ToString() + "'");

                lbusername.Text = Convert.ToString(Session["username"]);
            }
            if (!IsPostBack)
            {
                getdepartments();
                getuserroles();
            }
        }

        protected void btnenter_Click(object sender, EventArgs e)
        {
            try
            {

                System.Data.DataTable dtgetuser = new System.Data.DataTable();
                int intuserid = int.Parse(ddlnames.SelectedValue);

                if (string.IsNullOrEmpty(txtusername.Text))
                {
                    lbmsg.Text = "Please Enter the user Name";
                }
                else if (string.IsNullOrEmpty(txtsurname.Text))
                {
                    lbmsg.Text = "Please Enter the Surname";
                }
                else if (string.IsNullOrEmpty(ddldepartment.Text))
                {
                    lbmsg.Text = "Please Select the Department";

                }
                else if (string.IsNullOrEmpty(txtemail.Text))
                {
                    lbmsg.Text = "Please Enter The Email";
                }
                else if (string.IsNullOrEmpty(lbusername.Text))
                {
                    lbmsg.Text = "Please Who Are You?";

                }
                else
                {


                    x.AddData("Update user Set surname='" + txtsurname.Text + "', othernames='" + txtothernames.Text + "', email='" + txtemail.Text + "', department='" + ddldepartment.Text + "', userrole='" + ddluserrole.Text + "', createdby='" + lbusername.Text + "' Where userid=" + intuserid + ";");



                    string strinsert = "";
                    if (string.IsNullOrEmpty(txtpassword.Text))
                    {
                        strinsert = "Update admin Set userrole='" + ddluserrole.Text +
                            "',username='" + txtusername.Text +
                            "',createdby='" + lbusername.Text + "' Where userid=" + intuserid + ";";
                    }
                    else
                    {
                        strinsert = "Update admin Set userrole='" + ddluserrole.Text +
                       "',username='" + txtusername.Text +
                       "',password='" + txtpassword.Text +
                       "',createdby='" + lbusername.Text + "' Where userid=" + intuserid + ";";
                    }

                    x.AddData(strinsert);
                    txtusername.Text = "";
                    txtsurname.Text = "";
                    txtothernames.Text = "";
                    txtemail.Text = "";
                    // ddldepartment.Text = ""
                    ddldepartment.Text = "None";
                    ddluserrole.Text = "None";
                    // ddldepartment.Items.Clear()

                    lbmsg.Text = "The User is Updated";
                }
            }
            catch (Exception ex)
            {
                lbmsg.Text = ex.ToString();
            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //search for users
                if (!string.IsNullOrEmpty(txtsearch.Text))
                {
                    System.Data.DataTable dtsearch = new System.Data.DataTable();
                    dtsearch = x.getdatabase("Select * From user Where surname Like '%" + txtsearch.Text + "%' Or othernames Like '%" + txtsearch.Text + "%' Order By surname;");
                    ddlnames.Items.Clear();

                    if (dtsearch.Rows.Count > 0)
                    {
                        ListItem lstitem = new ListItem();
                        for (var i = 0; i < dtsearch.Rows.Count; i++)
                        {
                            lstitem = new ListItem();
                            lstitem.Text = dtsearch.Rows[i]["surname"].ToString() + " " + dtsearch.Rows[i]["othernames"].ToString() +
                                " (" + dtsearch.Rows[i]["department"].ToString() + ")";
                            lstitem.Value = dtsearch.Rows[i]["userid"].ToString();
                            ddlnames.Items.Add(lstitem);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }

        }
        private void getuser()
        {
            try
            {
                int intuserid = Convert.ToInt32(ddlnames.SelectedValue);
                System.Data.DataTable dtgetuser = new System.Data.DataTable();
                System.Data.DataTable dtgetadmin = new System.Data.DataTable();

                dtgetuser = x.getdatabase("Select * From user Where userid=" + intuserid);
                if (dtgetuser.Rows.Count > 0)
                {
                    txtemail.Text = dtgetuser.Rows[0]["email"].ToString();
                    txtothernames.Text = dtgetuser.Rows[0]["othernames"].ToString();
                    txtsurname.Text = dtgetuser.Rows[0]["surname"].ToString();
                    ddluserrole.Text = dtgetuser.Rows[0]["userrole"].ToString();
                    ddldepartment.Text = dtgetuser.Rows[0]["department"].ToString();


                }
                dtgetadmin = x.getdatabase("Select * From admin Where userid=" + intuserid);
                if (dtgetadmin.Rows.Count > 0)
                {
                    txtusername.Text = dtgetadmin.Rows[0]["username"].ToString();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void ddlnames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}