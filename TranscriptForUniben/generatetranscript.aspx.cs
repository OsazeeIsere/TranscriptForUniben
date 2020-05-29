using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace TranscriptForUniben
{
    public partial class generatetranscript : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            clssettings x = new clssettings();
            try
            {
                imgSignature.Visible = false;

                if ((Session["adminid"] == null))
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //check the admin rights
                    int intadminid = Convert.ToInt32(Session["adminid"]);
                    System.Data.DataTable dtcheckadmin = new System.Data.DataTable();
                    dtcheckadmin = x.getdatabase("Select userrole From admin Where adminid=" + intadminid);
                    if (dtcheckadmin.Rows[0]["userrole"].ToString() == "Verification Officer")
                    {
                        divcoment.Visible = true;
                        btnapprovalcoment.Visible = false;
                        btnverify.Visible = true;
                        btnapprove.Visible = false;
                        divprint.Visible = false;
                    }
                    else if (dtcheckadmin.Rows[0]["userrole"].ToString() == "Transcript Approval Officer")
                    {
                        divcoment.Visible = true;
                        btncoment.Visible = false;
                        btnverify.Visible = false;
                        btnapprove.Visible = true;
                        divprint.Visible = false;
                    }
                    else if (dtcheckadmin.Rows[0]["userrole"].ToString() == "Transcript Officer")
                    {
                        btnverify.Visible = false;
                        btnapprove.Visible = false;
                        btnviewappovedtranscript.Visible = true;
                        //btnapprove.Text = "View The Approved Transcript";
                        divprint.Visible = true;

                    }
                }

                gettranscript();

            }
            catch (Exception ex)
            {
                ex.ToString();

            }
        }
        public void gettranscript()
        {
            //create literal for the panel
            Literal litliteral = new Literal();
            Literal litliteral1 = new Literal();
            Literal litliteral2 = new Literal();
            //clear the panel
            pnltranscript.Controls.Clear();

            //retrieve the sessions
            string strmatnumber = Session["matnumber"].ToString();
            string strstartsession = Session["startsession"].ToString();
            string strendsession = Session["endsession"].ToString();
            int totalunit = 0;
            double totagrade = 0;
            double gpa = 0;
            double cgpa = 0;


            //get transcript information
            System.Data.DataTable dtgettranscript = new System.Data.DataTable();
            dtgettranscript = getdatabase("Select generatedby,approvalstatus,approvedby,transcriptstatus,generateddate,altereddate From transcript Where matnumber='" + strmatnumber + "';");
            if (dtgettranscript.Rows.Count > 0)
            {
                //get student information
                System.Data.DataTable dtgetstudent = new System.Data.DataTable();
                dtgetstudent = getdatabase("Select studentid,lastname,othernames,matnumber,gender,department From students Where matnumber='" + strmatnumber + "';");
                if (dtgetstudent.Rows.Count > 0)
                {
                    int intstudentid = Convert.ToInt32(dtgetstudent.Rows[0]["studentid"]);
                    string strname = "<b><span style='font-size:larger'>" + dtgetstudent.Rows[0]["lastname"].ToString() + "</span></b> " + dtgetstudent.Rows[0]["othernames"].ToString();
                    string strdepartment = dtgetstudent.Rows[0]["department"].ToString();

                    //get the faculty
                    System.Data.DataTable dtgetfaculty = new System.Data.DataTable();
                    dtgetfaculty = getdatabase("select faculty from department Where department='" + strdepartment + "';");
                    string strfaculty = "";
                    if (dtgetfaculty.Rows.Count > 0)
                    {
                        strfaculty = dtgetfaculty.Rows[0]["faculty"].ToString();
                    }

                    //output the name, faculty and department
                    litliteral = new Literal();
                    litliteral.ID = "litstart";
                    litliteral.Text = "<div style='width:400px;margin:0px auto;'><h1 style='text-align:centre'>UNIVERSITY OF BENIN</h1><h2 style='text-align:centre>STUDENT TRANSCRIPT</h2><h2 style='text-align:centre'>" + strname + "</h2><h3 style='text-align:centre'>DEPARTMENT OF " + strdepartment + ", FACULTY OF " + strfaculty + "</h3></div>";
                    pnltranscript.Controls.Add(litliteral);


                    litliteral = new Literal();
                    litliteral.ID = "litstartsession";
                    litliteral.Text = "<div style='padding-top:20px; padding-left:40px;'>";
                    pnltranscript.Controls.Add(litliteral);


                    //get sessions
                    string strsessionname = "";
                    int intsessionid = 0;
                    System.Data.DataTable dtgetsession = new System.Data.DataTable();
                    dtgetsession = getdatabase("Select sessionid,sessionname From session Where sessionname>='" + strstartsession + "' And sessionname <='" + strendsession + "' Order By sessionname;");
                    if (dtgetsession.Rows.Count > 0)
                    {
                        for (var i = 0; i < dtgetsession.Rows.Count; i++)
                        {
                            strsessionname = dtgetsession.Rows[i]["sessionname"].ToString();
                            intsessionid = Convert.ToInt32(dtgetsession.Rows[i]["sessionid"]);
                            //display sessions
                            litliteral = new Literal();
                            litliteral.ID = "litsession_" + intsessionid;
                            litliteral.Text = "<h3>" + strsessionname + " SESSION</h3>";
                            pnltranscript.Controls.Add(litliteral);

                            //get semester
                            string strsemester = "";

                            System.Data.DataTable dtgetsemester = new System.Data.DataTable();
                            dtgetsemester = getdatabase("Select semesterid,semester From semester;");
                            if (dtgetsemester.Rows.Count > 0)
                            {
                                for (var j = 0; j < dtgetsemester.Rows.Count; j++)
                                {
                                    strsemester = dtgetsemester.Rows[j]["semester"].ToString();

                                    //display semester
                                    litliteral = new Literal();
                                    litliteral.ID = "litsession_" + intsessionid + "_" + dtgetsemester.Rows[j]["semesterid"].ToString();
                                    litliteral.Text = "<h3 style='padding-left:20px;'>" + strsemester + " SEMESTER</h3>";
                                    pnltranscript.Controls.Add(litliteral);

                                    //create the scores table
                                    litliteral = new Literal();
                                    litliteral.ID = "littable_" + intsessionid + "_" + dtgetsemester.Rows[j]["semesterid"].ToString();
                                    litliteral.Text = "<div style='padding-left:50px;'><table class='table table-default'><tr><th>Level</th><th>Course TiTle</th><th>Course Code</th><th>Score</th><th>Grade</th></tr>";
                                    pnltranscript.Controls.Add(litliteral);

                                    //display scores and save scores for computation of CGPA
                                    System.Data.DataTable dtscores = new System.Data.DataTable();
                                    dtscores = getdatabase("Select scoreid,level,courseid,score,grade,gradepoint From scores Where sessionid=" + intsessionid + " And semester='" + strsemester + "' And studentid=" + intstudentid);
                                    if (dtscores.Rows.Count > 0)
                                    {
                                        for (var k = 0; k < dtscores.Rows.Count; k++)
                                        {
                                            //create the scores table

                                            litliteral = new Literal();
                                            litliteral1 = new Literal();
                                            litliteral1.ID = "lit1start";
                                            litliteral2.ID = "litstart";
                                            litliteral.ID = "littable_" + intsessionid + "_" + dtgetsemester.Rows[j]["semesterid"].ToString() + "_" + dtscores.Rows[k]["scoreid"].ToString();
                                            litliteral.Text = "<tr><td>" + dtscores.Rows[k]["level"].ToString() +
                                                                "</td><td>" + getcourse(Convert.ToInt32(dtscores.Rows[k]["courseid"])) +
                                                                "</td><td>" + getcoursecode(Convert.ToInt32(dtscores.Rows[k]["courseid"])) + "</td><td>" + dtscores.Rows[k]["score"].ToString() +
                                                                "</td><td>" + dtscores.Rows[k]["grade"].ToString() + "</td></tr>";
                                            pnltranscript.Controls.Add(litliteral);
                                            totalunit = totalunit + getcourseunit(Convert.ToInt32(dtscores.Rows[k]["courseid"].ToString()));
                                            totagrade = totagrade + double.Parse(dtscores.Rows[k]["gradepoint"].ToString());
                                            gpa = totagrade / totalunit;
                                        }
                                    }

                                    //close the scores table
                                    litliteral = new Literal();
                                    litliteral.ID = "littableend_" + intsessionid + "_" + dtgetsemester.Rows[j]["semesterid"].ToString();
                                    litliteral.Text = "</table></div>";

                                    pnltranscript.Controls.Add(litliteral);
                                }
                                litliteral1 = new Literal();
                                litliteral2 = new Literal();
                                cgpa = cgpa + gpa;
                                litliteral1.Text = "The Student GPA =" + decimal.Round((decimal)gpa, 2);
                                litliteral2.Text = "The Student CGPA =" + decimal.Round((decimal)cgpa, 2);
                                pnltranscript.Controls.Add(litliteral1);
                                pnltranscript.Controls.Add(litliteral2);


                            }
                        }
                        litliteral1 = new Literal();
                        litliteral2 = new Literal();

                        litliteral1.Text = "<br><br><b>" + dtgettranscript.Rows[0]["transcriptstatus"].ToString() + "<br>" + dtgettranscript.Rows[0]["approvalstatus"].ToString();
                        litliteral2.Text = "<br>Generated Date: " + dtgettranscript.Rows[0]["generateddate"].ToString() + "  <br>Approved By: " + dtgettranscript.Rows[0]["approvedby"].ToString() + "  <br>Approval Date: " + dtgettranscript.Rows[0]["altereddate"].ToString() + "/<b>";
                        pnltranscript.Controls.Add(litliteral1);
                        pnltranscript.Controls.Add(litliteral2);
                    }

                    //end sessions
                    litliteral = new Literal();
                    litliteral.ID = "litendsession";
                    litliteral.Text = "</div>";
                    pnltranscript.Controls.Add(litliteral);
                }
                else
                {
                    litliteral = new Literal();
                    litliteral.ID = "literror";
                    litliteral.Text = "Student does not exist";
                    pnltranscript.Controls.Add(litliteral);
                }
            }
            else
            {
                litliteral = new Literal();
                litliteral.ID = "literror";
                litliteral.Text = "Transcript does not exist";
                pnltranscript.Controls.Add(litliteral);
            }

        }
        public string getcoursecode(int courseid)
        {
            string tempgetcoursecode = null;
            try
            {
                tempgetcoursecode = "";
                System.Data.DataTable dtgetcourse = new System.Data.DataTable();
                dtgetcourse = getdatabase("Select * From courses Where courseid=" + courseid);
                string strcourse = "";
                if (dtgetcourse.Rows.Count > 0)
                {
                    strcourse = dtgetcourse.Rows[0]["coursecode"].ToString();
                    tempgetcoursecode = strcourse;
                }

                return tempgetcoursecode;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return tempgetcoursecode;
        }
        public int getcourseunit(int courseid)
        {
            int tempgetcourseunit = 0;
            try
            {
                
                System.Data.DataTable dtgetcourseunit = new System.Data.DataTable();
                dtgetcourseunit = getdatabase("Select * From courses Where courseid=" + courseid);
                int strcourse = 0;
                if (dtgetcourseunit.Rows.Count > 0)
                {
                    strcourse = Convert.ToInt32( dtgetcourseunit.Rows[0]["courseunit"].ToString());
                    tempgetcourseunit = strcourse;
                }

                return tempgetcourseunit;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return tempgetcourseunit;
        }
        public string getcourse(int courseid)
        {
            string tempgetcourse = null;
            try
            {
                tempgetcourse = "";
                System.Data.DataTable dtgetcourse = new System.Data.DataTable();
                dtgetcourse = getdatabase("Select * From courses Where courseid=" + courseid);
                string strcourse = "";
                if (dtgetcourse.Rows.Count > 0)
                {
                    strcourse = dtgetcourse.Rows[0]["coursetitle"].ToString();
                    tempgetcourse = strcourse;
                }

                return tempgetcourse;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return tempgetcourse;
        }
        public void adddata(string strinsert)
        {
            MySqlConnection cn = new MySqlConnection();
            MySqlDataAdapter ad = new MySqlDataAdapter();
          MySqlCommand cm = new MySqlCommand();

            string strconnection = "";
            strconnection = "server= localhost;port=3306;database=studentsrecord;uid=root;pwd=prayer";
            cn.ConnectionString = strconnection;
            cn.Open();
            cm.CommandText = strinsert;
            cm.Connection = cn;
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public System.Data.DataTable getdatabase(string strcommand)
        {
            System.Data.DataTable tempgetdatabase = null;
            tempgetdatabase = new System.Data.DataTable();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlCommand cm = new MySqlCommand();
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

        protected void btnverify_Click(object sender, EventArgs e)
        {
            try
            {

                string stradmin = "";
                if ((Session["adminid"] == null))
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //check the admin rights
                    int intadminid = Convert.ToInt32(Session["adminid"]);
                    System.Data.DataTable dtcheckadmin = new System.Data.DataTable();
                    dtcheckadmin = getdatabase("Select userid,username From admin Where adminid=" + intadminid);
                    stradmin = dtcheckadmin.Rows[0]["username"].ToString();
                    if (dtcheckadmin.Rows.Count > 0)
                    {
                        System.Data.DataTable dtgetadmin = new System.Data.DataTable();
                        dtgetadmin = getdatabase("Select surname,othernames From user Where userid=" + dtcheckadmin.Rows[0]["userid"].ToString());
                        stradmin = dtgetadmin.Rows[0]["surname"].ToString() + " " + dtgetadmin.Rows[0]["othernames"].ToString();

                    }
                }

                if ((Session["matnumber"] == null))
                {
                    Response.Redirect("transcript.aspx");
                }
                string strmatnumber = Session["matnumber"].ToString();
                //check approval
                System.Data.DataTable dtchecktranscript = new System.Data.DataTable();
                dtchecktranscript = getdatabase("Select * From transcript Where matnumber='" + strmatnumber + "';");
                if (!string.IsNullOrEmpty(dtchecktranscript.Rows[0]["approvedby"].ToString()))
                {
                    return;
                }
                clssettings x = new clssettings();
                adddata("Update transcript Set approvalstatus='Verified Awaiting Approval',transcriptstatus='Transcript Verified' Where matnumber ='" + strmatnumber + "';");
                x.audit(stradmin, "transcript", "transcriptstatus", "Transcript Verified", Convert.ToInt32(Session["adminid"].ToString()));
                gettranscript();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            try
            {
                string stradmin = "";
                if ((Session["adminid"] == null))
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //check the admin rights
                    int intadminid = Convert.ToInt32(Session["adminid"]);
                    System.Data.DataTable dtcheckadmin = new System.Data.DataTable();
                    dtcheckadmin = getdatabase("Select userid,username From admin Where adminid=" + intadminid);
                    stradmin = dtcheckadmin.Rows[0]["username"].ToString();
                    if (dtcheckadmin.Rows.Count > 0)
                    {
                        System.Data.DataTable dtgetadmin = new System.Data.DataTable();
                        dtgetadmin = getdatabase("Select surname,othernames From user Where userid=" + dtcheckadmin.Rows[0]["userid"].ToString());
                        stradmin = dtgetadmin.Rows[0]["surname"].ToString() + " " + dtgetadmin.Rows[0]["othernames"].ToString();

                    }
                }

                if ((Session["matnumber"] == null))
                {
                    Response.Redirect("transcript.aspx");
                }
                string strmatnumber = Session["matnumber"].ToString();
                //check approval
                System.Data.DataTable dtchecktranscript = new System.Data.DataTable();
                dtchecktranscript = getdatabase("Select * From transcript Where matnumber='" + strmatnumber + "';");

                if (string.IsNullOrEmpty(dtchecktranscript.Rows[0]["approvedby"].ToString()))
                {
                    if (dtchecktranscript.Rows[0]["transcriptstatus"].ToString() == "Transcript Verified")
                    {
                        clssettings x = new clssettings();
                        adddata("Update transcript Set approvalstatus='Approval Successful',transcriptstatus='Transcript Approved',approvedby='" + stradmin + "' Where matnumber ='" + strmatnumber + "';");
                        x.audit(stradmin, "transcript", "approvalstatus", "Transcript Approval", Convert.ToInt32(Session["adminid"]));
                    }

                }

                gettranscript();
                imgSignature.Visible = true;
            }
            catch (Exception ex)
            {
                ex.ToString();

            }

        }

        protected void btncoment_Click(object sender, EventArgs e)
        {
            try
            {
                string stradmin = "";
                if ((Session["adminid"] == null))
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //check the admin rights
                    int intadminid = Convert.ToInt32(Session["adminid"]);
                    System.Data.DataTable dtcheckadmin = new System.Data.DataTable();
                    dtcheckadmin = getdatabase("Select userid,username From admin Where adminid=" + intadminid);
                    stradmin = dtcheckadmin.Rows[0]["username"].ToString();
                    if (dtcheckadmin.Rows.Count > 0)
                    {
                        System.Data.DataTable dtgetadmin = new System.Data.DataTable();
                        dtgetadmin = getdatabase("Select surname,othernames From user Where userid=" + dtcheckadmin.Rows[0]["userid"].ToString());
                        stradmin = dtgetadmin.Rows[0]["surname"].ToString() + " " + dtgetadmin.Rows[0]["othernames"].ToString();

                    }
                }

                if ((Session["matnumber"] == null))
                {
                    Response.Redirect("transcript.aspx");
                }
                string strmatnumber = Session["matnumber"].ToString();
                //check approval
                System.Data.DataTable dtchecktranscript = new System.Data.DataTable();
                dtchecktranscript = getdatabase("Select * From transcript Where matnumber='" + strmatnumber + "';");
                if (!string.IsNullOrEmpty(dtchecktranscript.Rows[0]["approvedby"].ToString()))
                {
                    return;
                }
                clssettings x = new clssettings();
                adddata("Update transcript Set details='" + txtcoment.Text+"' Where matnumber ='" + strmatnumber + "';");
                x.audit(stradmin, "transcript", "transcriptstatus", "Transcript Verified", Convert.ToInt32(Session["adminid"].ToString()));
                gettranscript();
                lbfeedback.Text = "Ok! Your Comment Has Been Sent";
                txtcoment.Text = null;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string stradmin = "";
                if ((Session["adminid"] == null))
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //check the admin rights
                    int intadminid = Convert.ToInt32(Session["adminid"]);
                    System.Data.DataTable dtcheckadmin = new System.Data.DataTable();
                    dtcheckadmin = getdatabase("Select userid,username From admin Where adminid=" + intadminid);
                    stradmin = dtcheckadmin.Rows[0]["username"].ToString();
                    if (dtcheckadmin.Rows.Count > 0)
                    {
                        System.Data.DataTable dtgetadmin = new System.Data.DataTable();
                        dtgetadmin = getdatabase("Select surname,othernames From user Where userid=" + dtcheckadmin.Rows[0]["userid"].ToString());
                        stradmin = dtgetadmin.Rows[0]["surname"].ToString() + " " + dtgetadmin.Rows[0]["othernames"].ToString();

                    }
                }

                if ((Session["matnumber"] == null))
                {
                    Response.Redirect("transcript.aspx");
                }
                string strmatnumber = Session["matnumber"].ToString();
                //check approval
                System.Data.DataTable dtchecktranscript = new System.Data.DataTable();
                dtchecktranscript = getdatabase("Select * From transcript Where matnumber='" + strmatnumber + "';");

                if (string.IsNullOrEmpty(dtchecktranscript.Rows[0]["approvedby"].ToString()))
                {
                    if (dtchecktranscript.Rows[0]["transcriptstatus"].ToString() == "Transcript Verified")
                    {
                        clssettings x = new clssettings();
                        adddata("Update transcript Set details='"+txtcoment.Text+"' Where matnumber ='" + strmatnumber + "';");
                        x.audit(stradmin, "transcript", "approvalstatus", "Transcript Approval", Convert.ToInt32(Session["adminid"]));
                    }

                }
                lbfeedback.Text = "Ok! Your Comment Has Been Sent";
                txtcoment.Text = null;
                gettranscript();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        protected void btnviewappovedtranscript_Click(object sender, EventArgs e)
        {
            string stradmin = "";
            string strmatnumber = Session["matnumber"].ToString();

            if ((Session["adminid"] == null))
            {
                Response.Redirect("login.aspx");
            }
            else
            {

            
                clssettings x = new clssettings();
                string adminnow = "John Udoka";
            adddata("Update transcript Set approvalstatus='Approval Successful',transcriptstatus='Transcript Approved',approvedby='"+ adminnow +"' Where matnumber ='" + strmatnumber + "';");
            //x.audit(stradmin, "transcript", "approvalstatus", "Transcript Approval", Convert.ToInt32(Session["adminid"]));
            gettranscript();
            imgSignature.Visible = true;
                btnviewappovedtranscript.Visible = false;
            }
        }
    }
}
