using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranscriptForUniben
{
    public partial class viewtranscript : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clssettings x = new clssettings();
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

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            clssettings x = new clssettings();
            try
            {
                int inttranscriptid = Convert.ToInt32(ddltranscripts.SelectedValue);
                System.Data.DataTable dtgettranscript = new System.Data.DataTable();
                dtgettranscript = x.getdatabase("Select * From transcript Where transcriptid=" + inttranscriptid);
                if (dtgettranscript.Rows.Count > 0)
                {
                    Session["matnumber"] = dtgettranscript.Rows[0]["matnumber"].ToString();
                    Session["startsession"] = dtgettranscript.Rows[0]["startsession"].ToString();
                    Session["endsession"] = dtgettranscript.Rows[0]["endsession"].ToString();
                    Session["institution"] = dtgettranscript.Rows[0]["institution"].ToString();
                    Response.Redirect("generatetranscript.aspx");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                clssettings x = new clssettings();
                //search for users
                if (!string.IsNullOrEmpty(txtmatnumber.Text))
                {
                    System.Data.DataTable dtsearch = new System.Data.DataTable();
                    dtsearch = x.getdatabase("Select * From transcript Where matnumber Like '%" + txtmatnumber.Text + "%' Or fullname Like '%" + txtmatnumber.Text + "%' Order By fullname;");
                    ddltranscripts.Items.Clear();

                    if (dtsearch.Rows.Count > 0)
                    {
                        ListItem lstitem = new ListItem();
                        for (var i = 0; i < dtsearch.Rows.Count; i++)
                        {
                            lstitem = new ListItem();
                            lstitem.Text = dtsearch.Rows[i]["matnumber"].ToString() + " " + dtsearch.Rows[i]["fullname"].ToString() + " " + dtsearch.Rows[i]["transcriptstatus"].ToString() +
                                " (" + dtsearch.Rows[i]["department"].ToString() + ")";
                            lstitem.Value = dtsearch.Rows[i]["transcriptid"].ToString();
                            ddltranscripts.Items.Add(lstitem);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}