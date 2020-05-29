using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranscriptForUniben
{
    public partial class transcript : System.Web.UI.Page
    {
        clssettings x = new clssettings();
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dtgetsession = new System.Data.DataTable();
            System.Data.DataTable dtgetstudent = new System.Data.DataTable();
            System.Data.DataTable dtgetcourse = new System.Data.DataTable();
            dtgetstudent = x.getdatabase("Select * From students");
            dtgetsession = x.getdatabase("select * from session");

            if (dtgetsession.Rows.Count > 0)
            {

                for (var i = 0; i < dtgetsession.Rows.Count; i++)
                {

                    ddlstartsession.Items.Add(dtgetsession.Rows[i]["sessionname"].ToString());
                    ddlendsession.Items.Add(dtgetsession.Rows[i]["sessionname"].ToString());
                }

            }

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmatnumber.Text))
            {
                if (string.IsNullOrEmpty(txtinstitution.Text))
                {
                    lbmsg.Text = "Please, enter requesting institution";
                    txtinstitution.Focus();
                    return;

                }
                Session["matnumber"] = txtmatnumber.Text;
                Session["startsession"] = ddlstartsession.Text;
                Session["endsession"] = ddlendsession.Text;
                Session["institution"] = txtinstitution.Text;
                //check if the transcript has been generated
                System.Data.DataTable dtchecktranscript = new System.Data.DataTable();
                dtchecktranscript = x.getdatabase("Select * From transcript Where matnumber='" + txtmatnumber.Text + "';");
                if (dtchecktranscript.Rows.Count == 0)
                {
                    //generate transcript
                    //get admin name
                    string stradmin = "";
                    System.Data.DataTable dtadmin = new System.Data.DataTable();
                    int intadminid = Convert.ToInt32(Session["adminid"]);
                    dtadmin = x.getdatabase("Select userid,username From admin Where adminid=" + intadminid);
                    if (dtadmin.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtadmin.Rows[0]["userid"]) == 0)
                        {
                            stradmin = dtadmin.Rows[0]["username"].ToString();
                        }
                        else
                        {
                            System.Data.DataTable dtgetadminname = new System.Data.DataTable();
                            dtgetadminname = x.getdatabase("Select surname,othernames From user Where userid=" + Convert.ToInt32(dtadmin.Rows[0]["userid"]));
                            stradmin = dtgetadminname.Rows[0]["surname"].ToString() + " " + dtgetadminname.Rows[0]["othernames"].ToString();
                        }
                    }
                    else
                    {
                        lbmsg.Text = "Invalid Credentials to generate transcript";
                    }
                    //get student department
                    System.Data.DataTable dtgetdepartment = new System.Data.DataTable();
                    dtgetdepartment = x.getdatabase("Select department,lastname,othernames From students Where matnumber='" + txtmatnumber.Text + "'");
                    string strinsert = "Insert Into transcript(matnumber,department,fullname,generatedby,approvalstatus,transcriptstatus,institution,generateddate,startsession,endsession,details) Values('" + txtmatnumber.Text + "','" + dtgetdepartment.Rows[0]["department"].ToString() + "','" + dtgetdepartment.Rows[0]["lastname"].ToString() + " " + dtgetdepartment.Rows[0]["othernames"].ToString() + "','" + stradmin + "','Not Approved','Transcript Generated','" + txtinstitution.Text + "','" + DateTime.Now.ToString() + "','" + ddlstartsession.Text + "','" + ddlendsession.Text + "','" + txtdetails.Text + "');";
                    x.AddData(strinsert);
                    x.audit(stradmin, "transcript", "all", "Transcript Generated", Convert.ToInt32(Session["adminid"]));
                }
                Response.Redirect("generatetranscript.aspx");
            }
            else
            {
                lbmsg.Text = "Please Enter The MAT Number of The Requesting The Transcript";
            }

        }
    }
}