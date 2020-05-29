using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranscriptForUniben
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["adminid"] == null))
            {
                lnklogin.Visible = false;
                lnklogout.Visible = true;
                lnkdashboard.Visible = true;

            }
            else
            {
                lnklogin.Visible = true;
                lnklogout.Visible = false;
                lnkdashboard.Visible = false;

            }

        }


        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");

        }

        protected void lnklogin_Click(object sender, EventArgs e)
        {

        }
    }
}