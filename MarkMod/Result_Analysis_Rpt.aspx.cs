﻿using System;//---------------modified on 27/6/12 ,28/6/12(change prgm->dept,course->subj),complete modification)3/7/12(change query fr tot stud and get staff name)
//----------------------------------20/7/12(multi iso, logo rights)
//==========modified on 20.07.12 & 21.07.12 by mythili(added rdbtn for internal,external) display internal report complete coding
//========added printmaster setting condition based on mastersetting in pageload on 21.07.12 by mythili
//=======corrected all pass by sec wise on 27.0712 mythili
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BalAccess;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using FarPoint.Web.Spread;
using System.Text;
public partial class Result_Analysis_Rpt : System.Web.UI.Page
{
    #region declaration

    [Serializable()]
    public class MyImg : ImageCellType
    {
        public override Control PaintCell(String id, TableCell parent, FarPoint.Web.Spread.Appearance style, FarPoint.Web.Spread.Inset margin, object val, bool ul)
        {
            //''----------strudent photo
            System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
            img1.ImageUrl = this.ImageUrl; //base.ImageUrl;  
            img1.Width = Unit.Percentage(70);
            img1.Height = Unit.Percentage(70);
            return img1;
            //''------------clg logo
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
            img.ImageUrl = this.ImageUrl; //base.ImageUrl;  
            img.Width = Unit.Percentage(105);
            img.Height = Unit.Percentage(70);
            return img;
            //'-------------coe sign
            System.Web.UI.WebControls.Image img2 = new System.Web.UI.WebControls.Image();
            img2.ImageUrl = this.ImageUrl; //base.ImageUrl;  
            img2.Width = Unit.Percentage(75);
            img2.Height = Unit.Percentage(70);
            return img2;
        }
    }

    SqlCommand cmd;
    SqlDataReader dr_exam;
    SqlDataReader dr_mnthyr;
    SqlDataReader dr_convert;
    string grade_setting = string.Empty;
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_sem2 = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Photo = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection setcon = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Load = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Inssetting = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Getfunc = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Examcode = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_loadSubject = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Grade = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Stud = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Grade1 = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_mrkentry = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_currsem = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_getdetail = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_daters = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_course = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_exam = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_secrs = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_new = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_grademas = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_credit = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_option = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_sem = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_result = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_convertgrade = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_subcrd = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_rs = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_Grade_flag = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con_fun = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    Hashtable has = new Hashtable();
    DAccess2 d2 = new DAccess2();
    DataSet ds_has = new DataSet();
    DAccess2 dacces2 = new DAccess2();
    int else_tot_pass = 0;
    //'---------------------------new
    string address = string.Empty;
    string Phoneno = string.Empty;
    string Faxno = string.Empty;
    string phnfax = string.Empty;
    int serialno = 0;
    int exam_code_new = 0;
    int qpassstu = 0;
    //'------------------------------
    int subjectcount = 0;
    string district = string.Empty;
    string email = string.Empty;
    string website = string.Empty;
    string strsec = string.Empty;
    int semdec = 0;
    string sections = string.Empty;
    string funcgrade = string.Empty;
    string mark = string.Empty;
    bool markflag = false;
    string rol_no = string.Empty;
    string courseid = string.Empty;
    string atten = string.Empty;
    string Master = string.Empty;
    string regularflag = string.Empty;
    string genderflag = string.Empty;
    string strdayflag = string.Empty;
    string fromdate = string.Empty;
    bool InsFlag;
    bool flag;
    int IntExamCode = 0;
    int column_count = 0;
    string degree_code = string.Empty;
    string current_sem = string.Empty;
    string batch_year = string.Empty;
    string getgradeflag = string.Empty;
    string exam_month = string.Empty;
    string exam_year = string.Empty;
    string getsubno = string.Empty;
    string getsubtype = string.Empty;
    int rcnt;
    int ExamCode = 0;
    string strmnthyear = string.Empty;
    string strexam = string.Empty;
    int overallcredit = 0;
    string grade = string.Empty;
    string funcsubno = string.Empty;
    string funcsubname = string.Empty;
    string funcsubcode = string.Empty;
    string funcresult = string.Empty;
    string funcsemester = string.Empty;
    string funccredit = string.Empty;
    string EarnedVal = string.Empty;
    double cgpa2 = 0;
    string semesterddl = string.Empty;
    int cou = 0;
    Hashtable hat = new Hashtable();
    DataSet ds_load = new DataSet();
    DAccess2 daccess = new DAccess2();
    string collegecode = string.Empty;
    string usercode = string.Empty;
    string singleuser = string.Empty;
    string group_user = string.Empty;
    int sl_no1 = 1;
    int allpass_tot_cnt = 0;
    string degree = string.Empty;
    int qtot_stu = 0;
    string[] string_session_values;
    DataRow drow;
    DataTable data = new DataTable();
    ArrayList arrColHdrNames1 = new ArrayList();
    ArrayList arrColHdrNames2 = new ArrayList();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["collegecode"] == null) //Aruna For Back Button
        {
            Response.Redirect("~/Default.aspx");
        }
        lblerr.Visible = false;
        if (!IsPostBack)
        {
            rbappear.Checked = true;
            btnxl.Visible = false;
            //if (Convert.ToString(Session["value"]) == "1")
            //{
            //    LinkButtonb1.Visible = false;
            //    LinkButtonb2.Visible = true;
            //}
            //else
            //{
            //    LinkButtonb1.Visible = true;
            //    LinkButtonb2.Visible = false;
            //}
            //'--------------------------------------
            collegecode = Session["collegecode"].ToString();
            usercode = Session["usercode"].ToString();
            singleuser = Session["single_user"].ToString();
            group_user = Session["group_code"].ToString();

            string getbranch = ddlBranch.Text.ToString();
            Showgrid.Visible = false;
            btnmasterprint.Visible = false;
            btn_dirtprint.Visible = false;
            //Added By Srinath 
            btnxl.Visible = false;
            lblrptname.Visible = false;
            txtexcelname.Visible = false;
            if (Session["usercode"] != "")
            {
                Master = "select * from Master_Settings where usercode=" + Session["usercode"] + "";
                setcon.Close();
                setcon.Open();
                SqlDataReader mtrdr;
                SqlCommand mtcmd = new SqlCommand(Master, setcon);
                mtrdr = mtcmd.ExecuteReader();
                Session["strvar"] = string.Empty;
                Session["Rollflag"] = "0";
                Session["Regflag"] = "0";
                Session["Studflag"] = "0";
                if (mtrdr.HasRows)
                {
                    while (mtrdr.Read())
                    {
                        if (mtrdr["settings"].ToString() == "Roll No" && mtrdr["value"].ToString() == "1")
                        {
                            Session["Rollflag"] = "1";
                        }
                        if (mtrdr["settings"].ToString() == "Register No" && mtrdr["value"].ToString() == "1")
                        {
                            Session["Regflag"] = "1";
                        }
                        if (mtrdr["settings"].ToString() == "Student_Type" && mtrdr["value"].ToString() == "1")
                        {
                            Session["Studflag"] = "1";
                        }
                        if (mtrdr["settings"].ToString() == "Days Scholor" && mtrdr["value"].ToString() == "1")
                        {
                            strdayflag = " and (Stud_Type='Day Scholar'";
                        }
                        if (mtrdr["settings"].ToString() == "Hostel" && mtrdr["value"].ToString() == "1")
                        {
                            if (strdayflag != "" && strdayflag != "\0")
                            {
                                strdayflag = strdayflag + " or Stud_Type='Hostler'";
                            }
                            else
                            {
                                strdayflag = " and (Stud_Type='Hostler'";
                            }
                        }
                        if (mtrdr["settings"].ToString() == "Regular")
                        {
                            regularflag = "and ((registration.mode=1)";
                        }
                        if (mtrdr["settings"].ToString() == "Lateral")
                        {
                            if (regularflag != "")
                            {
                                regularflag = regularflag + " or (registration.mode=3)";
                            }
                            else
                            {
                                regularflag = regularflag + " and ((registration.mode=3)";
                            }
                        }
                        if (mtrdr["settings"].ToString() == "Transfer")
                        {
                            if (regularflag != "")
                            {
                                regularflag = regularflag + " or (registration.mode=2)";
                            }
                            else
                            {
                                regularflag = regularflag + " and ((registration.mode=2)";
                            }
                        }
                        if (mtrdr["settings"].ToString() == "Male" && mtrdr["value"].ToString() == "1")
                        {
                            genderflag = " and (sex='0'";
                        }
                        if (mtrdr["settings"].ToString() == "Female" && mtrdr["value"].ToString() == "1")
                        {
                            if (genderflag != "" && genderflag != "\0")
                            {
                                genderflag = genderflag + " or sex='1'";
                            }
                            else
                            {
                                genderflag = " and (sex='1'";
                            }
                        }
                        //=========== hide the printmaster setting button based on print master setting mythili on 21.07.12
                        if (mtrdr["settings"].ToString() == "print_master_setting" && mtrdr["value"].ToString() == "1")
                        {
                            btnPrint.Visible = false;// true;
                        }
                        else
                        {
                            btnPrint.Visible = false;
                        }
                        //============================================
                    }
                }
                if (strdayflag != "")
                {
                    strdayflag = strdayflag + ")";
                }
                Session["strvar"] = strdayflag;
                if (regularflag != "")
                {
                    regularflag = regularflag + ")";
                }
                Session["strvar"] = Session["strvar"] + regularflag;
                if (genderflag != "")
                {
                    genderflag = genderflag + ")";
                }
                Session["strvar"] = Session["strvar"] + regularflag + genderflag;
            }
            ddlMonth.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Jan", "1"));
            ddlMonth.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Feb", "2"));
            ddlMonth.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Mar", "3"));
            ddlMonth.Items.Insert(3, new System.Web.UI.WebControls.ListItem("Apr", "4"));
            ddlMonth.Items.Insert(4, new System.Web.UI.WebControls.ListItem("May", "5"));
            ddlMonth.Items.Insert(5, new System.Web.UI.WebControls.ListItem("Jun", "6"));
            ddlMonth.Items.Insert(6, new System.Web.UI.WebControls.ListItem("Jul", "7"));
            ddlMonth.Items.Insert(7, new System.Web.UI.WebControls.ListItem("Aug", "8"));
            ddlMonth.Items.Insert(8, new System.Web.UI.WebControls.ListItem("Sep", "9"));
            ddlMonth.Items.Insert(9, new System.Web.UI.WebControls.ListItem("Oct", "10"));
            ddlMonth.Items.Insert(10, new System.Web.UI.WebControls.ListItem("Nov", "11"));
            ddlMonth.Items.Insert(11, new System.Web.UI.WebControls.ListItem("Dec", "12"));
            int year;
            year = Convert.ToInt16(DateTime.Today.Year);
            ddlYear.Items.Clear();
            for (int l = 0; l <= 20; l++)
            {
                ddlYear.Items.Add(Convert.ToString(year - l));
            }
            ////============== added on 20.07.12 mythili
            lblTest.Visible = false;
            ddlTest.Visible = false;
            Lbl_Gender.Visible = false;
            UpdatePanelGender.Visible = false;
            Columnorder.Visible = false;
            //==========================================
            if (Request.QueryString["val"] == null)
            {
                bindbatch();//-----------------call bind functions
                binddegree();
                loadGender();
                if (ddlDegree.Items.Count >= 1)
                {
                    bindbranch();
                    bindsem();
                    bindsec();
                }
                else
                {
                    lblnorec.Visible = true;
                    lblnorec.Text = "Select degree rights for staff";
                    ddlBatch.Items.Clear();
                }
            }
            else
            {
                //=======================page redirect from master print setting
                try
                {
                    string_session_values = Request.QueryString["val"].Split(',');
                    if (string_session_values.GetUpperBound(0) == 9)
                    {
                        bindbatch();
                        ddlBatch.SelectedIndex = Convert.ToInt16(string_session_values[0]);
                        binddegree();
                        if (ddlDegree.Items.Count >= 1)
                        {
                            ddlDegree.SelectedIndex = Convert.ToInt16(string_session_values[1]);
                            bindbranch();
                            if (ddlBranch.Enabled == true)
                            {
                                ddlBranch.SelectedIndex = Convert.ToInt16(string_session_values[2].ToString());
                            }
                            bindsem();
                            if (ddlSemYr.Enabled == true)
                            {
                                ddlSemYr.SelectedIndex = Convert.ToInt16(string_session_values[3].ToString());
                            }
                            bindsec();
                            if (ddlSec.Enabled == true)
                            {
                                ddlSec.SelectedIndex = Convert.ToInt16(string_session_values[4].ToString());
                            }
                            if (ddlMonth.Enabled == true)
                            {
                                ddlMonth.SelectedIndex = Convert.ToInt16(string_session_values[5].ToString());
                            }
                            if (ddlYear.Enabled == true)
                            {
                                ddlYear.SelectedIndex = Convert.ToInt16(string_session_values[6].ToString());
                            }
                            if (string_session_values[7].ToString() == "true" || string_session_values[7].ToString() == "TRUE" || string_session_values[7].ToString() == "True" || string_session_values[7].ToString() == "1")
                            {
                                rdinternal.Checked = true;
                                lblTest.Visible = true;
                                ddlTest.Visible = true;
                                Columnorder.Visible = true;
                                Lbl_Gender.Visible = true;
                                UpdatePanelGender.Visible = true;
                                GetTest();
                                ddlTest.SelectedIndex = Convert.ToInt32(string_session_values[9].ToString());
                            }
                            else
                            {
                                rdinternal.Checked = false;
                                lblTest.Visible = false;
                                Lbl_Gender.Visible = false;
                                Columnorder.Visible = false;
                                UpdatePanelGender.Visible = false;
                            }
                            if (string_session_values[8].ToString() == "true" || string_session_values[8].ToString() == "TRUE" || string_session_values[8].ToString() == "True" || string_session_values[8].ToString() == "1")
                            {
                                rdexternal.Checked = true;
                                ddlTest.Visible = false;
                                lblTest.Visible = false;
                                Lbl_Gender.Visible = false;
                                UpdatePanelGender.Visible = false;
                                Columnorder.Visible = false;
                            }
                            else
                            {
                                // ddlTest.Visible = false;
                                rdexternal.Checked = false;
                            }
                            btnGo_Click(sender, e);
                        }
                    }
                }
                catch
                {
                }
                //===================================
            }
        }
    }

    public void bindbatch()
    {
        ////batch
        ddlBatch.Items.Clear();
        string sqlstring = string.Empty;
        int max_bat = 0;
        con.Close();
        con.Open();


        cmd = new SqlCommand(" select distinct batch_year from Registration where batch_year<>'-1' and batch_year<>''  and delflag=0 and exam_flag<>'debar' order by batch_year", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        ddlBatch.DataSource = ds1;
        ddlBatch.DataValueField = "batch_year";
        ddlBatch.DataBind();
        //----------------display max year value 
        sqlstring = "select max(batch_year) from Registration where batch_year<>'-1' and batch_year<>'' and delflag=0 and exam_flag<>'debar' ";
        max_bat = Convert.ToInt32(GetFunction(sqlstring));
        ddlBatch.SelectedValue = max_bat.ToString();
        con.Close();
        //binddegree();
    }

    public void bindbranch()
    {
        ddlBranch.Items.Clear();
        hat.Clear();
        usercode = Session["usercode"].ToString();
        collegecode = Session["collegecode"].ToString();
        singleuser = Session["single_user"].ToString();
        group_user = Session["group_code"].ToString();
        if (group_user.Contains(';'))
        {
            string[] group_semi = group_user.Split(';');
            group_user = group_semi[0].ToString();
        }
        hat.Add("single_user", singleuser.ToString());
        hat.Add("group_code", group_user);
        hat.Add("course_id", ddlDegree.SelectedValue);
        hat.Add("college_code", collegecode);
        hat.Add("user_code", usercode);
        ds_load.Clear();
        ds_load = daccess.select_method("bind_branch", hat, "sp");
        if (ds_load.Tables.Count > 0 && ds_load.Tables[0].Rows.Count > 0)
        {
            int count2 = ds_load.Tables[0].Rows.Count;
            if (count2 > 0)
            {
                ddlBranch.DataSource = ds_load;
                ddlBranch.DataTextField = "dept_name";
                ddlBranch.DataValueField = "degree_code";
                ddlBranch.DataBind();
            }
        }
    }

    public void binddegree()
    {
        ddlDegree.Items.Clear();
        usercode = Session["usercode"].ToString();
        collegecode = Session["collegecode"].ToString();
        singleuser = Session["single_user"].ToString();
        group_user = Session["group_code"].ToString();
        if (group_user.Contains(';'))
        {
            string[] group_semi = group_user.Split(';');
            group_user = group_semi[0].ToString();
        }
        hat.Clear();
        hat.Add("single_user", singleuser.ToString());
        hat.Add("group_code", group_user);
        hat.Add("college_code", collegecode);
        hat.Add("user_code", usercode);
        ds_load.Clear();
        ds_load = daccess.select_method("bind_degree", hat, "sp");
        if (ds_load.Tables.Count > 0 && ds_load.Tables[0].Rows.Count > 0)
        {
            int count1 = ds_load.Tables[0].Rows.Count;
            if (count1 > 0)
            {
                ddlDegree.DataSource = ds_load;
                ddlDegree.DataTextField = "course_name";
                ddlDegree.DataValueField = "course_id";
                ddlDegree.DataBind();
            }
        }
    }

    public void bindsec()
    {
        ddlSec.Items.Clear();
        ds_load.Clear();
        hat.Clear();
        hat.Add("batch_year", ddlBatch.SelectedValue.ToString());
        hat.Add("degree_code", ddlBranch.SelectedValue.ToString());
        ds_load = daccess.select_method("bind_sec", hat, "sp");
        if (ds_load.Tables.Count > 0 && ds_load.Tables[0].Rows.Count > 0)
        {
            int count5 = ds_load.Tables[0].Rows.Count;
            if (count5 > 0)
            {
                ddlSec.DataSource = ds_load;
                ddlSec.DataTextField = "sections";
                ddlSec.DataValueField = "sections";
                ddlSec.DataBind();
                ddlSec.Enabled = true;
            }
            else
            {
                ddlSec.Enabled = false;
            }
        }
        else
        {
            ddlSec.Enabled = false;
        }
    }

    //public void BindBatch()
    //{
    //    ddlBatch.Items.Clear();
    //    string sqlstr =string.Empty;
    //    int max_bat = 0;
    //    DataSet ds = ClsAttendanceAccess.GetBatchDetail();
    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlBatch.DataSource = ds;
    //        ddlBatch.DataTextField = "batch_year";
    //        ddlBatch.DataValueField = "batch_year";
    //        ddlBatch.DataBind();
    //        sqlstr = "select max(batch_year) from Registration where batch_year<>'-1' and batch_year<>'' and delflag=0 and exam_flag<>'debar' ";
    //        max_bat = Convert.ToInt32(GetFunction(sqlstr));
    //        ddlBatch.SelectedValue = max_bat.ToString();
    //        // ddlBatch.Items.Insert(0, new ListItem("- -Select- -", "-1"));
    //    }
    //}

    public void BindDegree()
    {
        ddlDegree.Items.Clear();
        collegecode = Session["collegecode"].ToString();
        DataSet ds = ClsAttendanceAccess.GetDegreeDetail(collegecode.ToString());
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDegree.DataSource = ds;
            ddlDegree.DataValueField = "Course_Id";
            ddlDegree.DataTextField = "Course_Name";
            ddlDegree.DataBind();
            //ddlDegree.Items.Insert(0, new ListItem("- -Select- -", "-1"));
        }
    }

    public void BindSectionDetail()
    {
        string branch = ddlBranch.SelectedValue.ToString();
        string batch = ddlBatch.SelectedValue.ToString();
        con_Load.Close();
        con_Load.Open();
        cmd = new SqlCommand("select distinct sections from registration where batch_year=" + ddlBatch.SelectedValue.ToString() + " and degree_code=" + ddlBranch.SelectedValue.ToString() + " and sections<>'-1' and sections<>' ' and delflag=0 and exam_flag<>'Debar'", con_Load);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlSec.DataSource = ds;
        ddlSec.DataTextField = "sections";
        ddlSec.DataValueField = "sections";
        ddlSec.DataBind();
        //  ddlSec.Items.Insert(0, new ListItem("--Select--", "-1"));
        SqlDataReader dr_sec;
        dr_sec = cmd.ExecuteReader();
        dr_sec.Read();
        if (dr_sec.HasRows == true)
        {
            if (dr_sec["sections"].ToString() == "")
            {
                ddlSec.Enabled = false;
                //  RequiredFieldValidator5.Visible = false;
            }
            else
            {
                ddlSec.Enabled = true;
                //   RequiredFieldValidator5.Visible = true;
            }
        }
        else
        {
            ddlSec.Enabled = false;
            //   RequiredFieldValidator5.Visible = false;
        }
    }

    public void Get_Semester()
    {
        bool first_year;
        first_year = false;
        int duration = 0;
        string batch_calcode_degree;
        //int typeval = 4;
        string batch = ddlBatch.SelectedValue.ToString();
        collegecode = Session["collegecode"].ToString();
        string degree = ddlBranch.SelectedValue.ToString();
        batch_calcode_degree = batch.ToString() + "/" + collegecode.ToString() + "/" + degree.ToString();
        //Session["collegecode"].ToString();
        DataSet ds = ClsAttendanceAccess.Getsemster_Detail(batch_calcode_degree.ToString());
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            first_year = Convert.ToBoolean(ds.Tables[0].Rows[0][1].ToString());
            duration = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            for (int i = 1; i <= duration; i++)
            {
                if (first_year == false)
                {
                    ddlSemYr.Items.Add(i.ToString());
                }
                else if (first_year == true && i != 2)
                {
                    ddlSemYr.Items.Add(i.ToString());
                }
            }
            //ddlSemYr.Items.Insert(0, new ListItem("- -Select- -", "-1"));
        }
    }

    public void bindsem()
    {
        ddlSemYr.Items.Clear();
        bool first_year;
        first_year = false;
        int duration = 0;
        int i = 0;
        con.Open();
        SqlDataReader dr;
        cmd = new SqlCommand("select distinct ndurations,first_year_nonsemester from ndegree where degree_code=" + ddlBranch.Text.ToString() + " and batch_year=" + ddlBatch.Text.ToString() + " and college_code=" + Session["collegecode"] + "", con);
        dr = cmd.ExecuteReader();
        dr.Read();
        if (dr.HasRows == true)
        {
            first_year = Convert.ToBoolean(dr[1].ToString());
            duration = Convert.ToInt16(dr[0].ToString());
            for (i = 1; i <= duration; i++)
            {
                if (first_year == false)
                {
                    ddlSemYr.Items.Add(i.ToString());
                }
                else if (first_year == true && i != 2)
                {
                    ddlSemYr.Items.Add(i.ToString());
                }
            }
        }
        else
        {
            dr.Close();
            SqlDataReader dr1;
            cmd = new SqlCommand("select distinct duration,first_year_nonsemester  from degree where degree_code=" + ddlBranch.Text.ToString() + " and college_code=" + Session["collegecode"] + "", con);
            //     ddlSemYr.Items.Clear();
            dr1 = cmd.ExecuteReader();
            dr1.Read();
            if (dr1.HasRows == true)
            {
                first_year = Convert.ToBoolean(dr1[1].ToString());
                duration = Convert.ToInt16(dr1[0].ToString());
                for (i = 1; i <= duration; i++)
                {
                    if (first_year == false)
                    {
                        ddlSemYr.Items.Add(i.ToString());
                    }
                    else if (first_year == true && i != 2)
                    {
                        ddlSemYr.Items.Add(i.ToString());
                    }
                }
            }
            dr1.Close();
        }
        con.Close();
    }

    public void GetTest()
    {
        try
        {
            con.Close();
            con.Open();
            string SyllabusYr;
            string SyllabusQry;
            SyllabusQry = "select syllabus_year from syllabus_master where degree_code=" + ddlBranch.SelectedValue.ToString() + " and semester =" + ddlSemYr.SelectedValue.ToString() + " and batch_year=" + ddlBatch.SelectedValue.ToString() + "";
            SyllabusYr = GetFunction(SyllabusQry.ToString());
            string Sqlstr;
            Sqlstr = string.Empty;
            Sqlstr = "select criteria,criteria_no from criteriaforinternal,syllabus_master where criteriaforinternal.syll_code=syllabus_master.syll_code and degree_code=" + ddlBranch.SelectedValue.ToString() + " and semester=" + ddlSemYr.SelectedValue.ToString() + " and syllabus_year=" + SyllabusYr.ToString() + " and batch_year=" + ddlBatch.SelectedValue.ToString() + " order by criteria";
            SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(Sqlstr, con);
            DataSet titles = new DataSet();
            con.Close();
            con.Open();
            sqlAdapter1.Fill(titles);
            if (titles.Tables.Count > 0 && titles.Tables[0].Rows.Count > 0)
            {
                ddlTest.DataSource = titles;
                ddlTest.DataValueField = "Criteria_No";
                ddlTest.DataTextField = "Criteria";
                ddlTest.DataBind();
                ddlTest.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
            }
            else
            {
                ddlTest.Items.Clear();
            }
        }
        catch
        {
        }
    }

    public void clear()
    {
        ddlSemYr.Items.Clear();
        ddlSec.Items.Clear();
    }

    public string GetFunction(string sqlQuery)
    {
        string sqlstr;
        sqlstr = sqlQuery;
        con_Getfunc.Close();
        con_Getfunc.Open();
        SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sqlstr, con_Getfunc);
        SqlDataReader drnew;
        SqlCommand funcmd = new SqlCommand(sqlstr);
        funcmd.Connection = con_Getfunc;
        drnew = funcmd.ExecuteReader();
        drnew.Read();
        if (drnew.HasRows == true)
        {
            return drnew[0].ToString();
        }
        else
        {
            return "0";
        }
    }

    protected void Chkbxcou_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((ddlDegree.SelectedIndex != 0) && (ddlBranch.SelectedIndex != 0))
        {
            ddlSemYr.Items.Clear();
            Get_Semester();
            GetTest();
        }
        ddlSec.SelectedIndex = -1;
        binddegree();
        bindbranch();
        bindsem();
        bindsec();
        GetTest();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        string course_id = ddlDegree.SelectedValue.ToString();
        collegecode = Session["collegecode"].ToString();
        usercode = Session["UserCode"].ToString();//Session["UserCode"].ToString();
        DataSet ds = ClsAttendanceAccess.GetBranchDetail(course_id.ToString(), collegecode.ToString());
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds;
            ddlBranch.DataTextField = "Dept_Name";
            ddlBranch.DataValueField = "degree_code";
            ddlBranch.DataBind();
        }
        bindsec();
        GetTest();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (!Page.IsPostBack == false)
        {
        }
        try
        {
            if ((ddlBranch.SelectedIndex != 0) || (ddlBranch.SelectedIndex > 0) || (ddlBranch.SelectedIndex == 0))
            {
                //Get_Semester();
                bindsem();
                //    BindSectionDetail();
                bindsec();
            }
        }
        catch (Exception ex)
        {
            string s = ex.ToString();
            Response.Write(s);
        }
        GetTest();
    }

    protected void ddlSemYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!Page.IsPostBack == false)
        {
            ddlSec.Items.Clear();
        }
        bindsec();
        GetTest();
    }

    protected void ddlSec_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTest();
    }

    protected void DropDownListpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBoxother.Text = string.Empty;
        if (DropDownListpage.Text == "Others")
        {
            LabelE.Visible = false;
            TextBoxother.Visible = true;
            TextBoxother.Focus();
        }
        else
        {
            LabelE.Visible = false;
            TextBoxother.Visible = false;
            Showgrid.Visible = true;
            btnmasterprint.Visible = true;
            btn_dirtprint.Visible = true;
            //Added By Srinath 
            btnxl.Visible = true;
            lblrptname.Visible = true;
            txtexcelname.Visible = true;
            Showgrid.PageSize = Convert.ToInt16(DropDownListpage.Text.ToString());
            CalculateTotalPages();
        }

    }

    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        Showgrid.Visible = false;
        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        //Added By Srinath 
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
        lblnorec.Visible = false;
    }

    protected void rdinternal_CheckedChanged(object sender, EventArgs e)
    {
        GetTest();
        cblsearch.Items.Clear();
        addcloumnitems();
        Cbcolumn_CheckedChanged(sender, e);
        Columnorder.Visible = true;
        lblTest.Visible = true;
        ddlTest.Visible = true;
        lblTest.Enabled = true;
        ddlTest.Enabled = true;
        Lbl_Gender.Visible = true;
        UpdatePanelGender.Visible = true;
        Showgrid.Visible = false;
        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        //Added By Srinath 
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
    }

    //=====func added on 20.07.12 
    protected void rdexternal_CheckedChanged(object sender, EventArgs e)
    {
        //============== hide the label,ddl test when external btn is enable
        if (rdexternal.Checked == true)
        {
            Lbl_Gender.Visible = false;
            UpdatePanelGender.Visible = false;
            lblTest.Visible = false;
            ddlTest.Visible = false;
            Columnorder.Visible = false;
            cblsearch.Items.Clear();
        }
        else
        {
            Columnorder.Visible = true;
            Lbl_Gender.Visible = true;
            UpdatePanelGender.Visible = true;
            lblTest.Visible = true;
            ddlTest.Visible = true;
        }
        //===================
        Showgrid.Visible = false;
        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        //Added By Srinath 
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
    }

    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        cblsearch.Items.Clear();
        addcloumnitems();
        Cbcolumn_CheckedChanged(sender, e);
        Showgrid.Visible = false;
        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
        lblnorec.Visible = false;
    }

    protected void rdMark_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void rdGrade_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void TextBoxother_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TextBoxother.Text != "")
            {
                Showgrid.PageSize = Convert.ToInt16(TextBoxother.Text.ToString());
                CalculateTotalPages();
            }
        }
        catch
        {
            TextBoxother.Text = string.Empty;
        }
    }

    protected void TextBoxpage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TextBoxpage.Text.Trim() != "")
            {
                if (Convert.ToInt16(TextBoxpage.Text) > Convert.ToInt16(Session["totalPages"]))
                {
                    LabelE.Visible = true;
                    LabelE.Text = "Exceed The Page Limit";
                    Showgrid.Visible = true;
                    btnmasterprint.Visible = true;
                    btn_dirtprint.Visible = true;
                    //Added By Srinath 
                    btnxl.Visible = true;
                    lblrptname.Visible = true;
                    txtexcelname.Visible = true;
                    TextBoxpage.Text = string.Empty;
                }
                else if (Convert.ToInt32(TextBoxpage.Text) == 0)
                {
                    LabelE.Visible = true;
                    LabelE.Text = "Search should be greater than zero";
                    TextBoxpage.Text = string.Empty;
                }
                else
                {
                    LabelE.Visible = false;
                    Showgrid.Visible = true;
                    btnmasterprint.Visible = true;
                    btn_dirtprint.Visible = true;
                    //Added By Srinath 
                    btnxl.Visible = true;
                    lblrptname.Visible = true;
                    txtexcelname.Visible = true;
                }
            }
        }
        catch
        {
            TextBoxpage.Text = string.Empty;
        }
    }

    protected void btnmasterprint_Click(object sender, EventArgs e)
    {
        //Session["column_header_row_count"] = FpExternal.Sheets[0].ColumnHeader.RowCount;
        string sections = ddlSec.SelectedValue.ToString();
        if (sections.ToString() == "All" || sections.ToString() == string.Empty || sections.ToString() == "-1")
        {
            sections = string.Empty;
        }
        DateTime date_today = DateTime.Now;
        int yr_now = Convert.ToInt32(date_today.ToString("yyyy"));
        string academyear = (yr_now.ToString() + "-" + (yr_now + 1).ToString());
        string academicfromtoyear = GetFunction("select value from master_settings where settings='Academic year'");
        if (academicfromtoyear != "")
        {
            string[] fromtoyear = academicfromtoyear.Split(',');
            string acefromyear = fromtoyear[0].ToString();
            string acetoyear = fromtoyear[1].ToString();
            academyear = acefromyear + "-" + acetoyear;
        }
        string degreedetails = string.Empty;
        if (rdinternal.Checked == true)
        {
            degreedetails = "TEST & EXAMINATION RESULT-CONSOLIDATED REPORT" + '@' + "Degree :" + ddlBatch.SelectedItem.ToString() + '-' + ddlDegree.SelectedItem.ToString() + '[' + ddlBranch.SelectedItem.ToString() + ']' + '-' + "Sem-" + ddlSemYr.SelectedItem.ToString() + '@' + "Academic Year:" + academyear + '@' + "Test/Exam Name:" + ddlTest.SelectedItem.ToString();
        }
        else
        {
            degreedetails = "TEST & EXAMINATION RESULT-CONSOLIDATED REPORT" + '@' + "Degree :" + ddlBatch.SelectedItem.ToString() + '-' + ddlDegree.SelectedItem.ToString() + '[' + ddlBranch.SelectedItem.ToString() + ']' + '-' + "Sem-" + ddlSemYr.SelectedItem.ToString() + '@' + "Academic Year:" + academyear + '@' + "Month & Year of exam :" + ddlMonth.SelectedItem.ToString() + ' ' + ddlYear.SelectedItem.ToString();
        }
        string ss = null;
        string pagename = "Result_Analysis_Rpt.aspx";
        Printcontrol.loadspreaddetails(Showgrid, pagename, degreedetails, 0, ss);
        Printcontrol.Visible = true;
    }

    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bool recflag = false;
    //        if (ddlDegree.Text == "")
    //        {
    //            return;
    //        }
    //        long tot_stud_str = 0;
    //        long nofopass_str = 0;
    //        double noofperc = 0;
    //        string strsec = string.Empty;
    //        string secvar = string.Empty;
    //        string sec_str = string.Empty;
    //        double aclass_perc1 = 0;
    //        //decimal aclass_perc1 = 0;
    //        int totvl = 0;
    //        lblnorec.Visible = false;
    //        if (rdexternal.Checked == false && rdinternal.Checked == false)
    //        {
    //            lblnorec.Text = "Kindly select Report";
    //            lblnorec.Visible = true;
    //        }
    //        else
    //        {
    //            if (rdexternal.Checked == false && rdinternal.Checked == true)
    //            {
    //                if (ddlTest.Items.Count > 0)
    //                {
    //                    if (ddlTest.SelectedItem.ToString() == "--Select--")
    //                    {
    //                        lblnorec.Text = "Please Select The Test";
    //                        lblnorec.Visible = true;
    //                        return;
    //                    }
    //                }
    //                //Added By Mlang Raja on  Feb 7 2017
    //                //else
    //                //{
    //                //    lblnorec.Text = "Test is Not Conducted";
    //                //    lblnorec.Visible = true;
    //                //    return;
    //                //}
    //            }
    //            lblnorec.Visible = false;
    //            lblnorec.Text = string.Empty;
    //            if (rdexternal.Checked == true)
    //            {
    //                lblTest.Visible = false;
    //                ddlTest.Visible = false;
    //            }
    //            else
    //            {
    //                lblTest.Visible = true;
    //                ddlTest.Visible = true;
    //            }
    //            FpExternal.Sheets[0].DefaultStyle.Font.Name = "Book Antiqua";
    //            int yr = 0;
    //            int sem_new = 0;
    //            string sem_fun = string.Empty;
    //            string exam_month_fun = "", exam_year_fun = string.Empty;
    //            string subjects_fun = string.Empty;
    //            int examcode_fun = 0;
    //            int tot_stu = 0, tot_stu1 = 0, allpascnt = 0;
    //            int no_of_passA = 0, no_of_passB = 0;
    //            batch_year = ddlBatch.SelectedValue.ToString();
    //            degree = ddlDegree.SelectedValue.ToString();
    //            degree_code = ddlBranch.SelectedValue.ToString();
    //            semesterddl = ddlSemYr.SelectedValue.ToString();
    //            sections = ddlSec.SelectedValue.ToString();
    //            exam_year = ddlYear.SelectedValue.ToString();
    //            exam_month = ddlMonth.SelectedValue.ToString();
    //            int exam_code = 0;// Convert.ToInt32(GetFunction("select exam_code from exam_details where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + ""));
    //            for (int seccnt = 0; seccnt < ddlSec.Items.Count; seccnt++)
    //            {
    //                if (sec_str == "")
    //                {
    //                    sec_str = ddlSec.Items[seccnt].ToString();
    //                }
    //                else
    //                {
    //                    sec_str = sec_str + "," + ddlSec.Items[seccnt].ToString();
    //                }
    //            }
    //            FpExternal.Sheets[0].RowCount = 0;
    //            if (rdinternal.Checked == true)
    //            {
    //                FpExternal.Sheets[0].ColumnHeader.RowCount = 2;
    //            }
    //            else
    //            {
    //                FpExternal.Sheets[0].ColumnHeader.RowCount = 1;
    //            }
    //            FpExternal.Sheets[0].AllowTableCorner = false;
    //            FpExternal.Sheets[0].RowHeader.Visible = false;
    //            //FpExternal.Sheets[0].ColumnHeader.Visible = false;
    //            FpExternal.Sheets[0].ColumnCount = 10;
    //            FpExternal.Sheets[0].SheetName = " ";
    //            int tot_sem = 0;
    //            yr = 0;
    //            con.Close();
    //            cmd = new SqlCommand("select ndurations from ndegree where batch_year=" + ddlBatch.SelectedValue + "  and degree_code=" + ddlBranch.SelectedValue + "", con);
    //            SqlDataReader no_on_sem_dr;
    //            con.Open();
    //            no_on_sem_dr = cmd.ExecuteReader();
    //            if (no_on_sem_dr.HasRows)
    //            {
    //                while (no_on_sem_dr.Read())
    //                {
    //                    tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
    //                    yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
    //                }
    //            }
    //            else
    //            {
    //                cmd = new SqlCommand("select duration from degree where degree_code=" + ddlBranch.SelectedValue + "", con);
    //                con.Close();
    //                con.Open();
    //                no_on_sem_dr = cmd.ExecuteReader();
    //                if (no_on_sem_dr.HasRows)
    //                {
    //                    while (no_on_sem_dr.Read())
    //                    {
    //                        tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
    //                        yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
    //                    }
    //                }
    //            }
    //            //-----------------------------------------------------------
    //            FpExternal.Sheets[0].Columns[0].Width = 30;
    //            FpExternal.Width = 900;
    //            FpExternal.Sheets[0].Columns[1].Width = 250;
    //            FpExternal.Sheets[0].SpanModel.Add(0, 7, 1, 2);
    //            FpExternal.Sheets[0].Columns[7].Width = 130;
    //            int sl_no = 1;
    //            FpExternal.Sheets[0].Columns[0].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[1].HorizontalAlign = HorizontalAlign.Left;
    //            FpExternal.Sheets[0].Columns[2].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[3].HorizontalAlign = HorizontalAlign.Left;
    //            FpExternal.Sheets[0].Columns[4].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[5].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[6].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[7].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[8].HorizontalAlign = HorizontalAlign.Center;
    //            FpExternal.Sheets[0].Columns[9].HorizontalAlign = HorizontalAlign.Center;
    //            //FpExternal.Sheets[0].Columns[7].HorizontalAlign = HorizontalAlign.Left;
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Subject code and name";
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Section";
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 3].Text = "Subject Teacher";
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 4].Text = "No. Appeared";
    //            FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 0, 2, 1);
    //            FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 1, 2, 1);
    //            FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 2, 2, 1);
    //            FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 3, 2, 1);
    //            FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 4, 2, 1);
    //            if (rbappear.Checked == false)
    //            {
    //                FpExternal.Sheets[0].ColumnHeader.Cells[0, 4].Text = "No. Strength";
    //            }
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 5].Text = "No. passed";
    //            FpExternal.Sheets[0].ColumnHeader.Cells[0, 6].Text = "Sectionwise Pass %";
    //            if (rdinternal.Checked == true)
    //            {
    //                FpExternal.Sheets[0].ColumnHeader.Cells[0, 7].Text = "Overall";
    //                FpExternal.Sheets[0].ColumnHeader.Cells[1, 7].Text = "Appeared";
    //                FpExternal.Sheets[0].ColumnHeader.Cells[1, 8].Text = "Pass";
    //                FpExternal.Columns[7].Visible = true;
    //                FpExternal.Columns[8].Visible = true;
    //            }
    //            else
    //            {
    //                //FpExternal.Columns[7].Visible = false;
    //                //FpExternal.Columns[8].Visible = false;
    //            }
    //            if (rdinternal.Checked == true)
    //            {
    //                FpExternal.Sheets[0].ColumnHeader.Cells[1, 9].Text = "pass %";
    //                FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 5, 2, 1);
    //                FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 6, 2, 1);
    //                FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 7, 1, 3);
    //                FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 5, 2, 1);
    //                FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 6, 2, 1);
    //                FpExternal.Sheets[0].SetColumnMerge(7, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                FpExternal.Sheets[0].SetColumnMerge(8, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                FpExternal.Sheets[0].SetColumnMerge(9, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //            }
    //            else
    //            {
    //                //  FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 9, 2, 1);
    //                FpExternal.Sheets[0].ColumnHeader.Cells[0, 9].Text = "Subjectwise pass %";
    //                // FpExternal.Sheets[0].ColumnHeader.Cells[0, 9].Text = "Subjectwise pass %";
    //                FpExternal.Columns[7].Visible = false;
    //                FpExternal.Columns[8].Visible = false;
    //            }
    //            // FpExternal.Sheets[0].ColumnHeader.Cells[0, 7].Text = "Subject Teacher";
    //            FpExternal.Sheets[0].Columns[0].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[1].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[2].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[3].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[4].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[5].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[6].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[7].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[8].VerticalAlign = VerticalAlign.Middle;
    //            FpExternal.Sheets[0].Columns[9].VerticalAlign = VerticalAlign.Middle;
    //            FarPoint.Web.Spread.LabelCellType chkdd_cell = new FarPoint.Web.Spread.LabelCellType();
    //            FpExternal.Sheets[0].Columns[5].CellType = chkdd_cell;
    //            FpExternal.Sheets[0].Columns[6].CellType = chkdd_cell;
    //            FpExternal.Sheets[0].RowCount = 0;
    //            double passstu1 = 0;
    //            //decimal  passstu1 = 0;
    //            exam_month_fun = ddlMonth.SelectedValue.ToString();
    //            exam_year_fun = ddlYear.SelectedValue.ToString();
    //            sem_new = Convert.ToInt32(ddlSemYr.SelectedValue.ToString());
    //            sem_fun = GetSemester_AsNumber(Convert.ToInt32(sem_new)).ToString();
    //            examcode_fun = int.Parse(d2.GetFunction("select distinct exam_code from exam_details where degree_code='" + degree_code + "' and batch_year=" + batch_year + " and exam_month=" + exam_month_fun + " and exam_year=" + exam_year_fun + ""));
    //            Session["examcode"] = examcode_fun;
    //            string criteria_no = ddlTest.SelectedValue.ToString();
    //            string in_examcode = string.Empty;
    //            if (rdexternal.Checked == true)
    //            {
    //                has.Clear();
    //                has.Add("sem_fun", sem_fun);
    //                has.Add("degree_code", degree_code);
    //                has.Add("batch_year", batch_year);
    //                has.Add("examcode_fun", examcode_fun);
    //                ds_has = d2.select_method("get_subject", has, "sp");
    //            }
    //            else if (rdinternal.Checked == true) //from [PROC_STUD_ALL_SUBMARK]
    //            {
    //                if (ddlTest.Items.Count == 0)
    //                {
    //                    lblnorec.Visible = true;
    //                    lblnorec.Text = "Kindly select Test";
    //                    FpExternal.Visible = false;
    //                    lblrptname.Visible = false;
    //                    txtexcelname.Visible = false;
    //                    btnxl.Visible = false;
    //                    btnmasterprint.Visible = false;
    //                    return;
    //                }
    //                else
    //                {
    //                    lblnorec.Text = string.Empty;
    //                    lblnorec.Visible = false;
    //                    FpExternal.Visible = true;
    //                    lblrptname.Visible = true;
    //                    txtexcelname.Visible = true;
    //                    btnxl.Visible = true;
    //                    btnmasterprint.Visible = true;
    //                    // string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " order by s.subject_no ";
    //                    string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e where e.subject_no=s.subject_no and e.criteria_no=" + criteria_no + " order by s.subject_no ";
    //                    ds_has.Dispose();
    //                    ds_has = d2.select_method_wo_parameter(test_subj, "Text");
    //                }
    //            }
    //            //======================================
    //            string tmpvar = string.Empty;
    //            int dsv = ddlSec.Items.Count;
    //            if (dsv == 0)
    //                dsv = 1;
    //            if (ds_has.Tables.Count > 0 && 0 < ds_has.Tables[0].Rows.Count)
    //            {
    //                sl_no = 1;
    //                int spancolumns = 0;
    //                int rowsp = 0;
    //                double overallAppear = 0;
    //                double overallPass = 0;
    //                double overallSubjects = 0;
    //                for (int i = 0; i < ds_has.Tables[0].Rows.Count; i++)
    //                {
    //                    spancolumns = 0;
    //                    if (i == 0)
    //                    {
    //                        sl_no = 1;
    //                        rowsp = FpExternal.Sheets[0].RowCount;
    //                    }
    //                    else
    //                    {
    //                        if (Convert.ToString(ds_has.Tables[0].Rows[i]["subject_name"]) != Convert.ToString(ds_has.Tables[0].Rows[i - 1]["subject_name"]))
    //                        {
    //                            sl_no++;
    //                            rowsp = FpExternal.Sheets[0].RowCount;
    //                        }
    //                    }
    //                    if (sl_no > 1)
    //                    {
    //                        FpExternal.Sheets[0].RowCount++;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = ".";
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].ForeColor = Color.White;
    //                        FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, FpExternal.Sheets[0].ColumnCount);
    //                        // FpExternal.Sheets[0].Rows[FpExternal.Sheets[0].RowCount - 1].Visible = false;
    //                    }
    //                    FpExternal.Sheets[0].SetColumnMerge(0, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                    FpExternal.Sheets[0].SetColumnMerge(1, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                    //FpExternal.Sheets[0].SetColumnMerge(7, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                    //FpExternal.Sheets[0].SetColumnMerge(8, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                    FpExternal.Sheets[0].SetColumnMerge(9, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                    for (int seccount = 1; seccount <= dsv; seccount++)
    //                    {
    //                        spancolumns++;
    //                        FpExternal.Sheets[0].RowCount++;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = sl_no.ToString();
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 1].Text = ds_has.Tables[0].Rows[i]["subject_code"] + " - " + ds_has.Tables[0].Rows[i]["subject_name"];
    //                    }
    //                    //New
    //                    double totalAppear = 0;
    //                    double totalPass = 0;
    //                    //New END
    //                    Double getappear = 0;
    //                    Double getpass = 0;
    //                    passstu1 = 0;
    //                    int totalcheck = 0;
    //                    overallSubjects++;
    //                    for (int sec_temp = 0; sec_temp < dsv; sec_temp++)
    //                    {
    //                        if ((ddlSec.Items.Count > 1) && (ddlSec.Items[sec_temp].ToString() == "All" || ddlSec.Items[sec_temp].ToString() == string.Empty || ddlSec.Items[sec_temp].ToString() == "-1"))
    //                        {
    //                            strsec = string.Empty;
    //                            secvar = string.Empty;
    //                        }
    //                        else
    //                        {
    //                            if (ddlSec.Items.Count > 0)
    //                            {
    //                                strsec = " and sections='" + ddlSec.Items[sec_temp].ToString() + "'";
    //                                secvar = ddlSec.Items[sec_temp].ToString();
    //                            }
    //                        }
    //                        if (rdinternal.Checked == true) //20.07.12 mythili
    //                        {
    //                            //========query for getting the exam_code,min,max,staffcode,duration,exam,entrydate for a particular subj for a particular sec
    //                            string strexam = "select distinct staff_code,duration,convert(varchar(10),exam_date,103)as exam_date,convert(varchar(10),entry_date,103)as entry_date,max_mark,min_mark,r.exam_code,s.subject_no,e.sections from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " and s.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " " + strsec + "";
    //                            DataSet ds_exam = d2.select_method_wo_parameter(strexam, "text");
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 2].Text = secvar;
    //                            //===============================================
    //                            if (ds_exam.Tables.Count > 0 && ds_exam.Tables[0].Rows.Count > 0)
    //                            {
    //                                recflag = true;
    //                                if (in_examcode == "")
    //                                {
    //                                    in_examcode = ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
    //                                }
    //                                else
    //                                {
    //                                    in_examcode = in_examcode + "," + ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
    //                                }
    //                                string sect = ds_exam.Tables[0].Rows[0]["sections"].ToString();
    //                                string sectval = string.Empty;
    //                                if (sect.Trim() != "" && sect.Trim() != "-1" && sect.Trim() != null)
    //                                {
    //                                    sectval = " and rt.sections='" + sect + "'";
    //                                }
    //                                else
    //                                {
    //                                    sectval = string.Empty;
    //                                    sect = string.Empty;
    //                                }
    //                                string strincludePassedout = string.Empty;
    //                                string includePassedout = string.Empty;
    //                                if (!chkincludepastout.Checked)
    //                                {
    //                                    strincludePassedout = "and rt.cc=0";
    //                                    includePassedout = "and reg.cc=0";

    //                                }
    //                                //Modified By Srinath 2/4/2013
    //                                //string totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt where r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3') and r.roll_no=rt.roll_no and rt.cc=0 and rt.exam_flag <>'DEBAR' and rt.delflag=0 and rt.RollNo_Flag<>0 ";
    //                                string totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt,exam_type e,subjectchooser sc where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 "+strincludePassedout+" and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no  and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + "";
    //                                if (rbappear.Checked == false)
    //                                {
    //                                    totstu = "select count(rt.roll_no) as 'PRESENT_COUNT' from registration rt,subjectchooser sc where rt.roll_no=sc.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 "+strincludePassedout+" and rt.RollNo_Flag<>0 "+strincludePassedout+" and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + "";
    //                                }
    //                                int gtotstu = int.Parse(d2.GetFunction(totstu));
    //                                FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 4].Text = gtotstu.ToString();
    //                                totalAppear += gtotstu;
    //                                overallAppear += gtotstu;
    //                                //Modified By Srinath 2/4/2013
    //                                // string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result where exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2')";
    //                                string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result r,registration reg,exam_type e,subjectchooser sc where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no and reg.delflag=0 and reg.exam_flag<>'debar' "+includePassedout+" and reg.RollNo_Flag<>0 ";
    //                                int gpassstud = int.Parse(d2.GetFunction(passstud));
    //                                FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = gpassstud.ToString();
    //                                totalPass += gpassstud;
    //                                overallPass += gpassstud;
    //                                if (FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text != "")
    //                                {
    //                                    totalcheck++;
    //                                }
    //                                getappear = getappear + gtotstu;
    //                                getpass = getpass + gpassstud;
    //                                double jh1 = Convert.ToDouble(gpassstud);
    //                                double hf1 = Convert.ToDouble(gtotstu);
    //                                double aclass1 = Convert.ToDouble(jh1 / hf1) * 100;
    //                                aclass_perc1 = 0;
    //                                aclass_perc1 = Math.Round(aclass1, 2);
    //                                if (aclass_perc1.ToString() == "NaN" || aclass_perc1.ToString() == "Infinity")
    //                                {
    //                                    aclass_perc1 = 0;
    //                                }
    //                                string ddval = aclass_perc1.ToString();
    //                                string[] spval = ddval.Split(new char[] { '.' });
    //                                if (spval.GetUpperBound(0) == 1)
    //                                {
    //                                    int dec = spval[1].Length;
    //                                    if (dec == 1)
    //                                    {
    //                                        ddval = spval[0] + "." + spval[1] + "0";
    //                                    }
    //                                    else if (spval[1] == "00")
    //                                    {
    //                                        ddval = spval[0] + ".00";
    //                                    }
    //                                    else
    //                                    {
    //                                        ddval = spval[0] + "." + spval[1];
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    ddval = spval[0] + ".00";
    //                                }
    //                                FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 6].Text = ddval.ToString();
    //                                //  FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = aclass_perc1.ToString();
    //                                passstu1 = passstu1 + aclass_perc1;
    //                            }
    //                        }
    //                        else if (rdexternal.Checked == true)
    //                        {
    //                            recflag = true;
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 2].Text = secvar;

    //                            #region existing which shows wrong value on elective paper since it does not matches the subject chooser

    //                            //string ssd = "select count(distinct m.roll_no) from mark_entry m,registration r where m.roll_no=r.roll_no and r.delflag<>1 and m.attempts = 1  and subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " and (result='pass' or result='fail' or result='S') and m.exam_code = " + examcode_fun + "  and degree_code=" + degree_code + " and batch_year=" + batch_year + " " + strsec + ""; 

    //                            #endregion

    //                            //added by prabha  on jan 18 2018
    //                            //no of students elective subject chooser  differed with no of students mark entered
    //                            #region matching qry with subject chooser

    //                            string ssd = "select count(distinct m.roll_no) from mark_entry m,registration r,subjectChooser sc where m.roll_no=r.roll_no and r.delflag<>1 and m.attempts = 1 and sc.subject_no=m.subject_no and m.roll_no=sc.roll_no  and m.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " and (result='pass' or result='fail' or result='S') and m.exam_code = " + examcode_fun + "  and degree_code=" + degree_code + " and batch_year=" + batch_year + " " + strsec + "";

    //                            #endregion

    //                            // select count(ea.roll_no) from exam_application ea,exam_appl_details ead,registration r where ea.appl_no=ead.appl_no and subject_no= " + ds_has.Tables[0].Rows[i]["subject_no"] + " and degree_code=" + degree_code + "  and batch_year=" + batch_year + "  " + strsec + " and ea.roll_no=r.roll_no and exam_code=" + examcode_fun + "";
    //                            tot_stu = int.Parse(d2.GetFunction(ssd));
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 4].Text = tot_stu.ToString();
    //                            string pm = "select count(distinct m.roll_no) from mark_entry m,registration r where m.roll_no=r.roll_no  " + strsec + "  and m.result = 'Pass' and  subject_no =  " + ds_has.Tables[0].Rows[i]["subject_no"] + " and r.delflag<>1 and m.attempts = 1 and ltrim(rtrim(type))='' and m.exam_code=" + examcode_fun + "";
    //                            int passstu = int.Parse(d2.GetFunction(pm));
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = passstu.ToString();
    //                            no_of_passA = no_of_passA + passstu;
    //                            getappear = getappear + tot_stu;
    //                            getpass = getpass + passstu;
    //                            double jh = Convert.ToDouble(passstu);
    //                            double hf = Convert.ToDouble(tot_stu);
    //                            double aclass = Convert.ToDouble((jh * 100) / hf);
    //                            double aclass_perc = Math.Round(aclass, 2);
    //                            //decimal aclass_perc =Convert.ToDecimal(Math.Round(aclass, 2));
    //                            if (aclass_perc.ToString() == "NaN" || aclass_perc.ToString() == "Infinity")
    //                            {
    //                                aclass_perc = 0;
    //                            }
    //                            string ddval = aclass_perc.ToString();
    //                            string[] spval = ddval.Split(new char[] { '.' });
    //                            if (spval.GetUpperBound(0) == 1)
    //                            {
    //                                int dec = spval[1].Length;
    //                                if (dec == 1)
    //                                {
    //                                    ddval = spval[0] + "." + spval[1] + "0";
    //                                }
    //                                else if (spval[1] == "00")
    //                                {
    //                                    ddval = spval[0] + ".00";
    //                                }
    //                                else
    //                                {
    //                                    ddval = spval[0] + "." + spval[1];
    //                                }
    //                            }
    //                            else
    //                            {
    //                                ddval = spval[0] + ".00";
    //                            }
    //                            // FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = aclass_perc.ToString();
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 6].Text = ddval.ToString();
    //                            FpExternal.Sheets[0].SetColumnMerge(6, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                            passstu1 = passstu1 + aclass_perc;
    //                        }
    //                        string io = ds_has.Tables[0].Rows[i]["subject_no"].ToString();
    //                        string staff_name = d2.GetFunction("select staff_name from staffmaster where staff_code in (select staff_code from staff_selector where subject_no = " + io + " and batch_year=" + batch_year + "  " + strsec + ")");
    //                        // FpExternal.Sheets[0].SpanModel.Add((FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 3, 1, 2);
    //                        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 3].Text = staff_name.ToString();
    //                        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 3].HorizontalAlign = HorizontalAlign.Left;
    //                    }
    //                    //FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - dsv, 6, dsv, 1);
    //                    // FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - dsv, 6, dsv, 1);
    //                    double tot_secwise_per = 0;
    //                    //decimal tot_secwise_per = 0;
    //                    double math_tot = 0;
    //                    // decimal math_tot = 0;
    //                    if (Convert.ToInt16(ddlSec.Items.Count) >= 2)
    //                    {
    //                        if (rdinternal.Checked)
    //                        {
    //                            // tot_secwise_per = passstu1 / Convert.ToInt16(totalcheck);
    //                            tot_secwise_per = getpass / getappear * 100;
    //                            math_tot = Math.Round(tot_secwise_per, 2);
    //                            totalcheck = 0;
    //                            getappear = 0;
    //                            getpass = 0;
    //                        }
    //                        else
    //                        {
    //                            ///tot_secwise_per = passstu1 / Convert.ToInt16(ddlSec.Items.Count);
    //                            tot_secwise_per = getpass / getappear * 100;
    //                            math_tot = Math.Round(tot_secwise_per, 2);
    //                            getappear = 0;
    //                            getpass = 0;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        math_tot = passstu1;
    //                    }
    //                    if (math_tot.ToString() == "NaN" || math_tot.ToString() == "Infinity")
    //                    {
    //                        math_tot = 0;
    //                    }
    //                    //added by gowtham
    //                    //-----------
    //                    for (int seccount = 0; seccount < dsv; seccount++)
    //                    {
    //                        string ddval = math_tot.ToString();
    //                        string[] spval = ddval.Split(new char[] { '.' });
    //                        if (spval.GetUpperBound(0) == 1)
    //                        {
    //                            int dec = spval[1].Length;
    //                            if (dec == 1)
    //                            {
    //                                ddval = spval[0] + "." + spval[1] + "0";
    //                            }
    //                            else if (spval[1] == "00")
    //                            {
    //                                ddval = spval[0] + ".00";
    //                            }
    //                            else
    //                            {
    //                                ddval = spval[0] + "." + spval[1];
    //                            }
    //                        }
    //                        else
    //                        {
    //                            ddval = spval[0] + ".00";
    //                        }
    //                        //   FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 6].Text = math_tot.ToString();
    //                        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 7].Text = totalAppear.ToString();
    //                        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 7].VerticalAlign = VerticalAlign.Middle;
    //                        FpExternal.Sheets[0].AddSpanCell((FpExternal.Sheets[0].RowCount - dsv) + seccount, 7, dsv, 1);
    //                        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 8].Text = totalPass.ToString();
    //                        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 8].VerticalAlign = VerticalAlign.Middle;
    //                        FpExternal.Sheets[0].AddSpanCell((FpExternal.Sheets[0].RowCount - dsv) + seccount, 8, dsv, 1);
    //                        if (rdexternal.Checked == true)
    //                        {
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].Text = ddval.ToString();
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].VerticalAlign = VerticalAlign.Middle;
    //                            //FpExternal.Sheets[0].AddSpanCell((FpExternal.Sheets[0].RowCount - dsv) + seccount, 9, dsv, 1);
    //                        }
    //                        else
    //                        {
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].Text = ddval.ToString();
    //                            FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].VerticalAlign = VerticalAlign.Middle;
    //                            FpExternal.Sheets[0].AddSpanCell((FpExternal.Sheets[0].RowCount - dsv) + seccount, 9, dsv, 1);
    //                        }
    //                    }
    //                    //  FpExternal.Sheets[0].SpanModel.Add(rowsp, 7, spancolumns, 1);//Hiiden By Srinath For Print
    //                    //  FpExternal.Sheets[0].SetColumnMerge(6, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                    //------------
    //                }
    //                int rowcount = 0;
    //                if (FpExternal.Sheets[0].RowCount > 0)
    //                {
    //                    if (sl_no > 1)
    //                    {
    //                        FpExternal.Sheets[0].RowCount++;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = ".";
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].ForeColor = Color.White;
    //                        FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, FpExternal.Sheets[0].ColumnCount);
    //                    }
    //                    if (overallSubjects != 0)
    //                    {
    //                        overallAppear /= overallSubjects;
    //                        overallAppear = Math.Round(overallAppear, 2);
    //                        overallPass /= overallSubjects;
    //                        overallPass = Math.Round(overallPass, 2);
    //                    }
    //                    if (rdinternal.Checked == true)
    //                    {
    //                        FpExternal.Sheets[0].RowCount++;
    //                        rowcount = FpExternal.Sheets[0].RowCount;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "OVERALL";
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].VerticalAlign = VerticalAlign.Middle;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";
    //                        FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 0, 1, 7);
    //                    }
    //                }
    //                //======================20.07.12
    //                if (in_examcode != "")
    //                {
    //                    in_examcode = "in(" + in_examcode + ")";//all examcode
    //                }
    //                //==================20.07.12
    //                double tot_per_all_pass = 0;
    //                int tot_stud_overall = 0;
    //                double test_minmark = 0;
    //                double secper = 0;
    //                for (int sec_temp = 0; sec_temp < dsv; sec_temp++)
    //                {
    //                    if ((ddlSec.Items.Count > 1) && (ddlSec.Items[sec_temp].ToString() == "All" || ddlSec.Items[sec_temp].ToString() == string.Empty || ddlSec.Items[sec_temp].ToString() == "-1"))
    //                    {
    //                        strsec = string.Empty;
    //                        secvar = string.Empty;
    //                    }
    //                    else
    //                    {
    //                        if (ddlSec.Items.Count > 0)
    //                        {
    //                            strsec = " and sections='" + ddlSec.Items[sec_temp].ToString() + "'";
    //                            secvar = ddlSec.Items[sec_temp].ToString();
    //                        }
    //                    }
    //                    FpExternal.Sheets[0].RowCount++;
    //                    string in_sec_examcode = string.Empty;
    //                    if (rdinternal.Checked == true)
    //                    {
    //                        //string sec_examcode = "select distinct r.exam_code as exam_code from exam_type e,subject s,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + ddlTest.SelectedValue.ToString() + "  " + strsec + "  ";
    //                        string sec_examcode = "select distinct e.exam_code as exam_code from exam_type e,subject s where e.subject_no=s.subject_no and e.criteria_no=" + ddlTest.SelectedValue.ToString() + "  " + strsec + "  ";
    //                        DataSet ds_sec_exmcode = d2.select_method_wo_parameter(sec_examcode, "text");
    //                        if (ds_sec_exmcode.Tables.Count > 0 && ds_sec_exmcode.Tables[0].Rows.Count > 0)
    //                        {
    //                            for (int scexm = 0; scexm < ds_sec_exmcode.Tables[0].Rows.Count; scexm++)
    //                            {
    //                                if (in_sec_examcode == "")
    //                                {
    //                                    in_sec_examcode = ds_sec_exmcode.Tables[0].Rows[scexm]["exam_code"].ToString();
    //                                }
    //                                else
    //                                {
    //                                    in_sec_examcode = in_sec_examcode + "," + ds_sec_exmcode.Tables[0].Rows[scexm]["exam_code"].ToString();
    //                                }
    //                            }
    //                        }
    //                        if (in_sec_examcode != "")
    //                        {
    //                            in_sec_examcode = "in(" + in_sec_examcode + ")";
    //                        }
    //                    }
    //                    string strincludePassedout = string.Empty;
    //                    string includePassedout = string.Empty;
    //                    if (!chkincludepastout.Checked)
    //                    {
    //                        strincludePassedout = "and rt.cc=0";
    //                        includePassedout = "and r.cc=0";

    //                    }
    //                    string ssd = "", ssd1 = string.Empty;
    //                    if (rdexternal.Checked == true)
    //                    {
    //                        ssd = "select count(*) from registration where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + "  " + strsec + "";
    //                        //ssd1 = "select COUNT(distinct m.roll_no) as Attended, r.degree_code from mark_entry m,Exam_Details e,Registration r where  e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' " + strsec + " and r.cc=0 and  r.exam_flag <>'DEBAR' and r.delflag=0 and m.attempts=1 and m.roll_no not in (select distinct r.roll_no from mark_entry m,Exam_Details e,Registration r where e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' and r.cc=0 and  r.exam_flag <>'DEBAR' and r.delflag=0 " + strsec + " and result='AAA' and m.attempts=1)  group by r.degree_code";
    //                        ssd1 = "select COUNT(distinct m.roll_no) as Attended, r.degree_code from mark_entry m,Exam_Details e,Registration r where  e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' " + strsec + " and  r.exam_flag <>'DEBAR' and r.delflag=0 and m.attempts=1 and m.result<>'AAA' and m.result<>'WHD' group by r.degree_code";
    //                    }
    //                    else if ((rdinternal.Checked == true) && (in_sec_examcode.ToString() != "")) //no of students appeared based on pass all examcode
    //                    {
    //                        ssd = "select isnull(count(distinct rt.roll_no),0) as 'allpass_count' from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3'or marks_obtained='-1')  and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 "+strincludePassedout+" and rt.RollNo_Flag<>0 " + strsec + " ";
    //                        //ssd = ssd + "and rt.roll_no not in(select distinct rt.roll_no from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and marks_obtained='-1'  and r.roll_no=rt.roll_no and rt.cc=0 and rt.exam_flag <>'DEBAR' and rt.delflag=0 and rt.RollNo_Flag<>0 " + strsec + " )";
    //                        ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear' from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3')  and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 "+strincludePassedout+" and rt.RollNo_Flag<>0 " + strsec + " ";
    //                        if (rbappear.Checked == false)
    //                        {
    //                            ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear' from subjectChooser sc,registration rt where sc.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 "+strincludePassedout+" and rt.RollNo_Flag<>0 and rt.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.semester='" + ddlSemYr.SelectedValue.ToString() + "' " + strsec + "";
    //                        }
    //                    }
    //                    if (ssd.ToString().Trim() != "")
    //                        allpascnt = int.Parse(d2.GetFunction(ssd));
    //                    if (ssd1.ToString().Trim() != "")
    //                        tot_stu = int.Parse(d2.GetFunction(ssd1));
    //                    tot_stud_overall = tot_stud_overall + tot_stu;
    //                    //Modified by srinath 22/8/2013
    //                    //  FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 2);
    //                    //  FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "No. of Students Appeared For Tests in Section " + secvar;
    //                    // FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Text = tot_stu.ToString();
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Font.Bold = true;
    //                    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 3);
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "No. of Students Appeared For Tests in Section " + secvar + " : " + tot_stu.ToString();
    //                    if (rbappear.Checked == false)
    //                    {
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "No. of Students Strength For Tests in Section " + secvar + " : " + tot_stu.ToString();
    //                    }
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
    //                    tot_stud_str = tot_stud_str + Convert.ToInt32(tot_stu);
    //                    //---------------------find tot no of student pass in all subject
    //                    double b4 = 0;
    //                    double b3 = 0;
    //                    if (rdexternal.Checked == true)
    //                    {
    //                        int fail_stud_atleast_one = 0;
    //                        DataSet ds = new DataSet();
    //                        //cmd = new SqlCommand("select count(distinct(mark_entry.roll_no)) from mark_entry,registration where exam_code=" + examcode_fun + " and mark_entry.Attempts = 1 and result='pass'  and passorfail=1 and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + " and mark_entry.roll_no not in(select distinct(mark_entry.roll_no) from mark_entry,registration where exam_code=" + examcode_fun + "  and mark_entry.Attempts = 1 and (result='Fail' or result='AAA')  and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and   batch_year=" + ddlBatch.SelectedItem.ToString() + "" + strsec + " )", con);
    //                        ds = d2.select_method_wo_parameter("select count(distinct(m.roll_no)) from mark_entry m,registration r,subject s,syllabus_master sy where exam_code=" + examcode_fun + " and m.Attempts = 1 and s.subject_no=m.subject_no and sy.degree_code=r.degree_code and r.Batch_Year=sy.Batch_Year and s.syll_code=sy.syll_code and result='pass'  and passorfail=1 and m.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy.semester=" + ddlSemYr.SelectedItem.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + " and m.roll_no not in(select distinct(m1.roll_no) from mark_entry m1,registration r1,subject s1,syllabus_master sy1 where s1.subject_no=m1.subject_no and sy1.degree_code=r1.degree_code and r1.Batch_Year=sy1.Batch_Year and s1.syll_code=sy1.syll_code and m1.exam_code=" + examcode_fun + "  and m1.Attempts = 1 and (result='Fail' or result='AAA')  and m1.roll_no=r1.roll_no and r1.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy1.semester=" + ddlSemYr.SelectedItem.ToString() + " and   r1.batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + ")", "Text");
    //                        // Select count(distinct(mark_entry.roll_no)) from mark_entry,registration where exam_code=" + examcode_fun + "  and mark_entry.Attempts = 1 and (result='Fail' or result='AAA')  and passorfail=0 and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + ""
    //                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                        {
    //                            fail_stud_atleast_one = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
    //                        }
    //                        //--------------------------
    //                        //FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 3);
    //                        FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 4);
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Text = "No. of Students Passed In All Subject In Section " + secvar + " " + fail_stud_atleast_one.ToString();
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
    //                        //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (allpascnt - fail_stud_atleast_one).ToString();
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Font.Bold = true;
    //                        b3 = (Convert.ToDouble(fail_stud_atleast_one / Convert.ToDouble(tot_stu))) * 100;
    //                        b4 = Math.Round(b3, 2);
    //                        if (b4.ToString() == "NaN" || b4.ToString() == "Infinity")
    //                        {
    //                            b4 = 0;
    //                        }
    //                        if (secper == 0)
    //                        {
    //                            secper = b4;
    //                        }
    //                        else
    //                        {
    //                            secper = secper + b4;
    //                        }
    //                        //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Text = b4.ToString();
    //                        //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Font.Bold = true;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Text = "All Pass % In Section " + secvar + " " + b4;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].HorizontalAlign = HorizontalAlign.Left;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Bold = true;
    //                        FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 9, 1, 3);
    //                    }
    //                    else if ((rdinternal.Checked == true) && (in_sec_examcode.ToString() != ""))
    //                    {
    //                        //==========find minmark for particular test
    //                        int fail_in_allsubj = 0;
    //                        //test_minmark = Convert.ToInt32(GetFunction("select min_mark from criteriaforinternal where criteria_no=" + ddlTest.SelectedValue.ToString() + ""));  //by malang raja
    //                        string minmrk = d2.GetFunction("select min_mark from criteriaforinternal where criteria_no=" + ddlTest.SelectedValue.ToString() + "");
    //                        double.TryParse(minmrk, out test_minmark);
    //                        DataSet ds = new DataSet();
    //                        ssd = "select isnull(count(distinct rt.roll_no),0) from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and (rt.marks_obtained<" + test_minmark + " and rt.marks_obtained<>'-3' and rt.marks_obtained<>'-2' and rt.marks_obtained<>'-18') and r.exam_flag <>'DEBAR' and r.delflag=0 "+includePassedout+" and r.RollNo_Flag<>0  ";
    //                        //Modified by srinath 3/9/2013
    //                        // ssd = ssd + " and rt.roll_no not in (select distinct rt.roll_no from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and rt.marks_obtained='-1')";
    //                        // ssd = ssd + " and rt.roll_no not in (select distinct rt.roll_no from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and rt.marks_obtained='-1' and rt.marks_obtained=0)";
    //                        ds = d2.select_method_wo_parameter(ssd, "text");
    //                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                        {
    //                            fail_in_allsubj = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
    //                        }
    //                        // FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 3);
    //                        FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 4);
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Text = "No. of Students Passed In All Subject In Section " + secvar + " :" + (allpascnt - fail_in_allsubj).ToString();
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
    //                        // FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (allpascnt - fail_in_allsubj).ToString();//subtract from tot-failcount for getting pass count
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Font.Bold = true;
    //                        nofopass_str = Convert.ToInt32(nofopass_str + ((allpascnt - fail_in_allsubj)));
    //                        totvl = ((allpascnt - fail_in_allsubj));
    //                        //b3 = tot_per_all_pass + allpascnt;
    //                        b3 = (Convert.ToDouble(totvl) / Convert.ToDouble(tot_stu)) * 100;
    //                        noofperc = noofperc + Math.Round(b3);
    //                        b4 = Math.Round(b3, 2);
    //                        if (b4.ToString() == "NaN" || b4.ToString() == "Infinity")
    //                        {
    //                            b4 = 0;
    //                        }
    //                        if (secper == 0)
    //                        {
    //                            secper = b4;
    //                        }
    //                        else
    //                        {
    //                            secper = secper + b4;
    //                        }
    //                        //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Text = b4.ToString();
    //                        //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Font.Bold = true;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Text = "All Pass % In Section " + secvar + " " + b4;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].HorizontalAlign = HorizontalAlign.Left;
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Font.Bold = true;
    //                        FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 7, 1, 3);
    //                    }
    //                    tot_per_all_pass = tot_per_all_pass + b4;
    //                }///////////////////////////28/6/12 PRABHA
    //                if ((rdinternal.Checked == true))
    //                {
    //                    FpExternal.Sheets[0].RowCount++;
    //                    //--------------------------------------------------------------------------------------------------------
    //                    //Modified by Srinath 22/8/2013 
    //                    //FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 2);
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "Total No of Students Appeared For Tests In All Sections "; //"Total No of Students Appeared For Tests In Section " + sec_str ;
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Text = tot_stud_str.ToString();
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Font.Bold = true;
    //                    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 3);
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "Total No of Students Appeared For Tests In All Sections : " + tot_stud_str.ToString() + ""; //"Total No of Students Appeared For Tests In Section " + sec_str ;
    //                    if (rbappear.Checked == false)
    //                    {
    //                        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "Total No of Students Strength For Tests In All Sections : " + tot_stud_str.ToString() + ""; //"Total No of Students Appeared For Tests In Section " + sec_str ;
    //                    }
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
    //                    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 4);
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Text = "Total No of Students Passed In All Subject In All Sections: " + nofopass_str.ToString() + ""; //"Total No of Students Passed In All Subject In Section" + sec_str;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = nofopass_str.ToString();
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Font.Bold = true;
    //                    //FpExternal.Sheets[0].Columns[3].Width = 180;
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Text = Math.Round((Convert.ToDouble(nofopass_str.ToString()) / Convert.ToDouble(tot_stud_str.ToString())) * 100,2).ToString(); //noofperc.ToString();
    //                    string calc = Math.Round((Convert.ToDouble(nofopass_str.ToString()) / Convert.ToDouble(tot_stud_str.ToString())) * 100, 2).ToString(); //noofperc.ToString();
    //                    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Font.Bold = true;
    //                    //FpExternal.Sheets[0].Columns[7].Width = 170;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Text = "All Pass % In All Sections :" + " " + calc; //"All Pass % In Section " + sec_str ;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].HorizontalAlign = HorizontalAlign.Left;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Font.Bold = true;
    //                    FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 7, 1, 3);
    //                    FpExternal.Sheets[0].AutoPostBack = true;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Text = tot_stud_str.ToString();
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 7].VerticalAlign = VerticalAlign.Middle;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Font.Bold = true;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Font.Size = FontUnit.Medium;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Font.Name = "Book Antiqua";
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Text = (nofopass_str).ToString();
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 8].VerticalAlign = VerticalAlign.Middle;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Font.Bold = true;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Font.Size = FontUnit.Medium;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Font.Name = "Book Antiqua";
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Text = calc.ToString();
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 9].VerticalAlign = VerticalAlign.Middle;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Font.Bold = true;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Font.Size = FontUnit.Medium;
    //                    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Font.Name = "Book Antiqua";
    //                    //--------------------------------------------------------------------------------------------------------
    //                }
    //                else if (rdexternal.Checked == true)
    //                {
    //                    FpExternal.Sheets[0].RowCount++;
    //                    FpExternal.Sheets[0].Rows[FpExternal.Sheets[0].RowCount - 1].Height = 40;
    //                    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 7);
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "All PASS % In This Test/Semester";
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].VerticalAlign = VerticalAlign.Middle;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Size = 13;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
    //                    //double bb3 = (tot_per_all_pass) / ddlSec.Items.Count;
    //                    //double bb4 = Math.Round(bb3, 2);
    //                    double bb4 = 0;
    //                    if (ddlSec.Items.Count > 0)
    //                    {
    //                        bb4 = secper / ddlSec.Items.Count;
    //                    }
    //                    else
    //                    {
    //                        bb4 = secper;
    //                    }
    //                    //double bb3 = 0;
    //                    //bb3 = (Convert.ToDouble(allpascnt) / Convert.ToDouble(tot_stu));
    //                    //bb4 = bb4 + Math.Round(bb3);
    //                    //bb4 = Math.Round(bb3, 2);
    //                    ////bb3 = Math.Round((Convert.ToDouble(tot_per_all_pass.ToString()) / Convert.ToDouble(ddlSec.ToString())) * 100, 2).ToString();
    //                    ////string bb4 = Math.Round(bb3, 2);
    //                    if (bb4.ToString() == "NaN" || bb4.ToString() == "Infinity")
    //                    {
    //                        bb4 = 0;
    //                    }
    //                    bb4 = Math.Round(bb4, 2, MidpointRounding.AwayFromZero);
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Text = bb4.ToString();
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Bold = true;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].HorizontalAlign = HorizontalAlign.Center;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].VerticalAlign = VerticalAlign.Middle;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Size = 12;
    //                    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Bold = true;
    //                    FpExternal.Sheets[0].SetColumnMerge(9, FarPoint.Web.Spread.Model.MergePolicy.Always);
    //                }
    //                //--------------------------------------------27/6/12 PRABHA
    //                if (FpExternal.Sheets[0].PageSize > 0)
    //                {
    //                    if (rdexternal.Checked == true)
    //                    {
    //                        DateTime todate = DateTime.Now;
    //                        if (yr.ToString() == todate.ToString("yyyy"))
    //                        {
    //                            FpExternal.Sheets[0].RowCount++;
    //                            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "% of students qualifying for degree(for final year final semester only)";
    //                            FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 6);
    //                            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
    //                            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].VerticalAlign = VerticalAlign.Middle;
    //                            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Size = 12;
    //                            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
    //                            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Font.Bold = true;
    //                            if (rdexternal.Checked == true)
    //                            {
    //                                int all_padd_get_degree = Convert.ToInt32(d2.GetFunction("select count(distinct m.roll_no )from mark_entry m,subject s,syllabus_master sy,subjectchooser sc  where s.subject_no=m.subject_no and m.subject_no=sc.subject_no and s.syll_code=sy.syll_code and sy.semester=sc.semester and sy.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy.batch_year=" + ddlBatch.SelectedItem.ToString() + " and sy.semester<=" + ddlSemYr.SelectedItem.ToString() + " and (result='Fail' or result='AAA')  and passorfail=0 "));
    //                                FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (Math.Round(((Convert.ToDouble(tot_stud_overall - all_padd_get_degree) / Convert.ToDouble(tot_stud_overall)) * 100), 2)).ToString();
    //                            }
    //                            else if (rdinternal.Checked == true)
    //                            {
    //                                int all_qualify_stud = Convert.ToInt32(d2.GetFunction("select count(distinct rt.roll_no) from result rt,registration r where rt.exam_Code " + in_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + " and rt.marks_obtained>0 and rt.marks_obtained>=" + test_minmark + ""));
    //                                FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (Math.Round(((Convert.ToDouble(tot_stud_overall - all_qualify_stud) / Convert.ToDouble(tot_stud_overall)) * 100), 2)).ToString();
    //                            }
    //                        }
    //                    }
    //                }
    //                //==============================================================
    //                FpExternal.Visible = true;
    //                btnmasterprint.Visible = true;
    //                //Added By Srinath 28/2/2013
    //                btnxl.Visible = true;//Added By Srinath 
    //                lblrptname.Visible = true;
    //                txtexcelname.Visible = true;
    //                lblnorec.Visible = false;
    //            }
    //            if (recflag == false)
    //            {
    //                if (rdinternal.Checked == true)
    //                {
    //                    lblnorec.Visible = true;
    //                    FpExternal.Visible = false;
    //                    btnmasterprint.Visible = false;
    //                    //Added By Srinath 
    //                    btnxl.Visible = false;
    //                    lblrptname.Visible = false;
    //                    txtexcelname.Visible = false;
    //                    lblnorec.Text = "No Records Found";
    //                }
    //                else if (rdexternal.Checked == true)
    //                {
    //                    lblnorec.Visible = true;
    //                    FpExternal.Visible = false;
    //                    btnmasterprint.Visible = false;
    //                    //Added By Srinath 
    //                    btnxl.Visible = false;
    //                    lblrptname.Visible = false;
    //                    txtexcelname.Visible = false;
    //                    lblnorec.Text = "No Records Found";
    //                }
    //            }
    //            // logoset();
    //            FpExternal.Sheets[0].PageSize = FpExternal.Sheets[0].RowCount;
    //            func_multi_iso();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblnorec.Text = ex.ToString();
    //        lblnorec.Visible = true;
    //    }
    //}


    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            btnPrint11();
            #region external checked
            if (rdexternal.Checked == true)
            {

                bool recflag = false;
                if (ddlDegree.Text == "")
                {
                    return;
                }
                long tot_stud_str = 0;
                long nofopass_str = 0;
                double noofperc = 0;
                string strsec = string.Empty;
                string secvar = string.Empty;
                string sec_str = string.Empty;
                double aclass_perc1 = 0;
                //decimal aclass_perc1 = 0;
                int totvl = 0;
                lblnorec.Visible = false;
                if (rdexternal.Checked == false && rdinternal.Checked == false)
                {
                    lblnorec.Text = "Kindly select Report";
                    lblnorec.Visible = true;
                }
                else
                {
                    if (rdexternal.Checked == false && rdinternal.Checked == true)
                    {
                        if (ddlTest.Items.Count > 0)
                        {
                            if (ddlTest.SelectedItem.ToString() == "--Select--")
                            {
                                lblnorec.Text = "Please Select The Test";
                                lblnorec.Visible = true;
                                return;
                            }
                        }
                        //Added By Mlang Raja on  Feb 7 2017
                        //else
                        //{
                        //    lblnorec.Text = "Test is Not Conducted";
                        //    lblnorec.Visible = true;
                        //    return;
                        //}
                    }

                    lblnorec.Visible = false;
                    lblnorec.Text = string.Empty;
                    if (rdexternal.Checked == true)
                    {
                        Lbl_Gender.Visible = false;
                        UpdatePanelGender.Visible = false;
                        lblTest.Visible = false;
                        ddlTest.Visible = false;
                        Columnorder.Visible = false;
                    }
                    else
                    {
                        Columnorder.Visible = true;
                        Lbl_Gender.Visible = true;
                        UpdatePanelGender.Visible = true;
                        lblTest.Visible = true;
                        ddlTest.Visible = true;
                    }


                    int yr = 0;
                    int sem_new = 0;
                    string sem_fun = string.Empty;
                    string exam_month_fun = "", exam_year_fun = string.Empty;
                    string subjects_fun = string.Empty;
                    int examcode_fun = 0;
                    int tot_stu = 0, tot_stu1 = 0, allpascnt = 0;
                    int no_of_passA = 0, no_of_passB = 0;
                    int sl_no = 0;
                    batch_year = ddlBatch.SelectedValue.ToString();
                    degree = ddlDegree.SelectedValue.ToString();
                    degree_code = ddlBranch.SelectedValue.ToString();
                    semesterddl = ddlSemYr.SelectedValue.ToString();
                    sections = ddlSec.SelectedValue.ToString();
                    exam_year = ddlYear.SelectedValue.ToString();
                    exam_month = ddlMonth.SelectedValue.ToString();
                    Dictionary<int, string> dicexcolspan = new Dictionary<int, string>();
                    int exam_code = 0;// Convert.ToInt32(GetFunction("select exam_code from exam_details where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + ""));
                    for (int seccnt = 0; seccnt < ddlSec.Items.Count; seccnt++)
                    {
                        if (sec_str == "")
                        {
                            sec_str = ddlSec.Items[seccnt].ToString();
                        }
                        else
                        {
                            sec_str = sec_str + "," + ddlSec.Items[seccnt].ToString();
                        }
                    }

                    if (rdinternal.Checked == true)
                    {
                        // FpExternal.Sheets[0].ColumnHeader.RowCount = 2;
                    }
                    else
                    {
                        //FpExternal.Sheets[0].ColumnHeader.RowCount = 1;
                    }

                    int tot_sem = 0;
                    yr = 0;
                    con.Close();
                    cmd = new SqlCommand("select ndurations from ndegree where batch_year=" + ddlBatch.SelectedValue + "  and degree_code=" + ddlBranch.SelectedValue + "", con);
                    SqlDataReader no_on_sem_dr;
                    con.Open();
                    no_on_sem_dr = cmd.ExecuteReader();
                    if (no_on_sem_dr.HasRows)
                    {
                        while (no_on_sem_dr.Read())
                        {
                            tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
                            yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("select duration from degree where degree_code=" + ddlBranch.SelectedValue + "", con);
                        con.Close();
                        con.Open();
                        no_on_sem_dr = cmd.ExecuteReader();
                        if (no_on_sem_dr.HasRows)
                        {
                            while (no_on_sem_dr.Read())
                            {
                                tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
                                yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
                            }
                        }
                    }
                    //-----------------------------------------------------------

                    sl_no = 1;

                    arrColHdrNames1.Add("S.No");
                    arrColHdrNames1.Add("Subject code and name");
                    arrColHdrNames1.Add("Section");
                    arrColHdrNames1.Add("Subject Teacher");



                    data.Columns.Add("SNo", typeof(string));
                    data.Columns.Add("Subject code and name", typeof(string));
                    data.Columns.Add("Section", typeof(string));
                    data.Columns.Add("Subject Teacher", typeof(string));
                    if (rbappear.Checked == false)
                    {
                        arrColHdrNames1.Add("No. Strength");

                        data.Columns.Add("No. Strength", typeof(string));
                    }
                    else
                    {
                        arrColHdrNames1.Add("No. Appeared");

                        data.Columns.Add("No. Appeared", typeof(string));
                    }
                    arrColHdrNames1.Add("No. passed");
                    arrColHdrNames1.Add("Sectionwise pass %");
                    arrColHdrNames1.Add("Subjectwise pass %");

                    data.Columns.Add("No. passed", typeof(string));
                    data.Columns.Add("Sectionwise Pass %", typeof(string));
                    data.Columns.Add("Subjectwise pass %", typeof(string));


                    DataRow drHdr1 = data.NewRow();

                    for (int grCol = 0; grCol < data.Columns.Count; grCol++)
                        drHdr1[grCol] = arrColHdrNames1[grCol];


                    data.Rows.Add(drHdr1);

                    //if (rdinternal.Checked == true)
                    //{
                    //    FpExternal.Sheets[0].ColumnHeader.Cells[0, 7].Text = "Overall";
                    //    FpExternal.Sheets[0].ColumnHeader.Cells[1, 7].Text = "Appeared";
                    //    FpExternal.Sheets[0].ColumnHeader.Cells[1, 8].Text = "Pass";
                    //    FpExternal.Columns[7].Visible = true;
                    //    FpExternal.Columns[8].Visible = true;
                    //}
                    //else
                    //{
                    //    //FpExternal.Columns[7].Visible = false;
                    //    //FpExternal.Columns[8].Visible = false;
                    //}
                    //if (rdinternal.Checked == true)
                    //{
                    //    FpExternal.Sheets[0].ColumnHeader.Cells[1, 9].Text = "pass %";
                    //    FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 5, 2, 1);
                    //    FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 6, 2, 1);
                    //    FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 7, 1, 3);
                    //    FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 5, 2, 1);
                    //    FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 6, 2, 1);
                    //    FpExternal.Sheets[0].SetColumnMerge(7, FarPoint.Web.Spread.Model.MergePolicy.Always);
                    //    FpExternal.Sheets[0].SetColumnMerge(8, FarPoint.Web.Spread.Model.MergePolicy.Always);
                    //    FpExternal.Sheets[0].SetColumnMerge(9, FarPoint.Web.Spread.Model.MergePolicy.Always);
                    //}
                    //else
                    //{
                    //    //  FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(0, 9, 2, 1);
                    //    FpExternal.Sheets[0].ColumnHeader.Cells[0, 9].Text = "Subjectwise pass %";
                    //    // FpExternal.Sheets[0].ColumnHeader.Cells[0, 9].Text = "Subjectwise pass %";
                    //    FpExternal.Columns[7].Visible = false;
                    //    FpExternal.Columns[8].Visible = false;
                    //}

                    double passstu1 = 0;
                    //decimal  passstu1 = 0;
                    exam_month_fun = ddlMonth.SelectedValue.ToString();
                    exam_year_fun = ddlYear.SelectedValue.ToString();
                    sem_new = Convert.ToInt32(ddlSemYr.SelectedValue.ToString());
                    sem_fun = GetSemester_AsNumber(Convert.ToInt32(sem_new)).ToString();
                    examcode_fun = int.Parse(d2.GetFunction("select distinct exam_code from exam_details where degree_code='" + degree_code + "' and batch_year=" + batch_year + " and exam_month=" + exam_month_fun + " and exam_year=" + exam_year_fun + ""));
                    Session["examcode"] = examcode_fun;
                    string criteria_no = ddlTest.SelectedValue.ToString();
                    string in_examcode = string.Empty;
                    if (rdexternal.Checked == true)
                    {
                        has.Clear();
                        has.Add("sem_fun", sem_fun);
                        has.Add("degree_code", degree_code);
                        has.Add("batch_year", batch_year);
                        has.Add("examcode_fun", examcode_fun);
                        ds_has = d2.select_method("get_subject", has, "sp");
                    }
                    //Internal
                    //else if (rdinternal.Checked == true) //from [PROC_STUD_ALL_SUBMARK]
                    //{
                    //    if (ddlTest.Items.Count == 0)
                    //    {
                    //        lblnorec.Visible = true;
                    //        lblnorec.Text = "Kindly select Test";
                    //        Showgrid.Visible = false;
                    //        lblrptname.Visible = false;
                    //        txtexcelname.Visible = false;
                    //        btnxl.Visible = false;
                    //        btnmasterprint.Visible = false;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        lblnorec.Text = string.Empty;
                    //        lblnorec.Visible = false;
                    //        Showgrid.Visible = true;
                    //        lblrptname.Visible = true;
                    //        txtexcelname.Visible = true;
                    //        btnxl.Visible = true;
                    //        btnmasterprint.Visible = true;
                    //        // string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " order by s.subject_no ";
                    //        string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e where e.subject_no=s.subject_no and e.criteria_no=" + criteria_no + " order by s.subject_no ";
                    //        ds_has.Dispose();
                    //        ds_has = d2.select_method_wo_parameter(test_subj, "Text");
                    //    }
                    //}
                    //======================================
                    string tmpvar = string.Empty;
                    int dsv = ddlSec.Items.Count;
                    if (dsv == 0)
                        dsv = 1;
                    if (ds_has.Tables.Count > 0 && 0 < ds_has.Tables[0].Rows.Count)
                    {
                        sl_no = 1;
                        int spancolumns = 0;
                        int rowsp = 0;
                        double overallAppear = 0;
                        double overallPass = 0;
                        double overallSubjects = 0;
                        for (int i = 0; i < ds_has.Tables[0].Rows.Count; i++)
                        {
                            spancolumns = 0;
                            if (i == 0)
                            {
                                sl_no = 1;
                                //rowsp = FpExternal.Sheets[0].RowCount;
                            }
                            else
                            {
                                if (Convert.ToString(ds_has.Tables[0].Rows[i]["subject_name"]) != Convert.ToString(ds_has.Tables[0].Rows[i - 1]["subject_name"]))
                                {
                                    sl_no++;
                                    // rowsp = FpExternal.Sheets[0].RowCount;
                                }
                            }
                            if (sl_no > 1)
                            {
                                rowsp++;
                                drow = data.NewRow();
                                drow["SNo"] = "";
                                data.Rows.Add(drow);
                                dicexcolspan.Add(rowsp, "");
                            }
                            for (int seccount = 1; seccount <= dsv; seccount++)
                            {
                                rowsp++;
                                spancolumns++;
                                drow = data.NewRow();
                                drow["SNo"] = sl_no.ToString();
                                drow["Subject code and name"] = ds_has.Tables[0].Rows[i]["subject_code"] + " - " + ds_has.Tables[0].Rows[i]["subject_name"];
                                data.Rows.Add(drow);
                                dicexcolspan.Add(rowsp, ds_has.Tables[0].Rows[i]["subject_code"].ToString());
                            }
                            //New
                            double totalAppear = 0;
                            double totalPass = 0;
                            //New END
                            Double getappear = 0;
                            Double getpass = 0;
                            passstu1 = 0;
                            int totalcheck = 0;
                            overallSubjects++;
                            for (int sec_temp = 0; sec_temp < dsv; sec_temp++)
                            {
                                if ((ddlSec.Items.Count > 1) && (ddlSec.Items[sec_temp].ToString() == "All" || ddlSec.Items[sec_temp].ToString() == string.Empty || ddlSec.Items[sec_temp].ToString() == "-1"))
                                {
                                    strsec = string.Empty;
                                    secvar = string.Empty;
                                }
                                else
                                {
                                    if (ddlSec.Items.Count > 0)
                                    {
                                        strsec = " and sections='" + ddlSec.Items[sec_temp].ToString() + "'";
                                        secvar = ddlSec.Items[sec_temp].ToString();
                                    }
                                }
                                #region Internal
                                //if (rdinternal.Checked == true) //20.07.12 mythili
                                //{
                                //    //========query for getting the exam_code,min,max,staffcode,duration,exam,entrydate for a particular subj for a particular sec
                                //    string strexam = "select distinct staff_code,duration,convert(varchar(10),exam_date,103)as exam_date,convert(varchar(10),entry_date,103)as entry_date,max_mark,min_mark,r.exam_code,s.subject_no,e.sections from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " and s.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " " + strsec + "";
                                //    DataSet ds_exam = d2.select_method_wo_parameter(strexam, "text");
                                //    FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 2].Text = secvar;
                                //    //===============================================
                                //    if (ds_exam.Tables.Count > 0 && ds_exam.Tables[0].Rows.Count > 0)
                                //    {
                                //        recflag = true;
                                //        if (in_examcode == "")
                                //        {
                                //            in_examcode = ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
                                //        }
                                //        else
                                //        {
                                //            in_examcode = in_examcode + "," + ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
                                //        }
                                //        string sect = ds_exam.Tables[0].Rows[0]["sections"].ToString();
                                //        string sectval = string.Empty;
                                //        if (sect.Trim() != "" && sect.Trim() != "-1" && sect.Trim() != null)
                                //        {
                                //            sectval = " and rt.sections='" + sect + "'";
                                //        }
                                //        else
                                //        {
                                //            sectval = string.Empty;
                                //            sect = string.Empty;
                                //        }
                                //        string strincludePassedout = string.Empty;
                                //        string includePassedout = string.Empty;
                                //        if (!chkincludepastout.Checked)
                                //        {
                                //            strincludePassedout = "and rt.cc=0";
                                //            includePassedout = "and reg.cc=0";

                                //        }
                                //        //Modified By Srinath 2/4/2013
                                //        //string totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt where r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3') and r.roll_no=rt.roll_no and rt.cc=0 and rt.exam_flag <>'DEBAR' and rt.delflag=0 and rt.RollNo_Flag<>0 ";
                                //        string totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt,exam_type e,subjectchooser sc where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no  and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + "";
                                //        if (rbappear.Checked == false)
                                //        {
                                //            totstu = "select count(rt.roll_no) as 'PRESENT_COUNT' from registration rt,subjectchooser sc where rt.roll_no=sc.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + "";
                                //        }
                                //        int gtotstu = int.Parse(d2.GetFunction(totstu));
                                //        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 4].Text = gtotstu.ToString();
                                //        totalAppear += gtotstu;
                                //        overallAppear += gtotstu;
                                //        //Modified By Srinath 2/4/2013
                                //        // string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result where exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2')";
                                //        string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result r,registration reg,exam_type e,subjectchooser sc where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + " and reg.RollNo_Flag<>0 ";
                                //        int gpassstud = int.Parse(d2.GetFunction(passstud));
                                //        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = gpassstud.ToString();
                                //        totalPass += gpassstud;
                                //        overallPass += gpassstud;
                                //        if (FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text != "")
                                //        {
                                //            totalcheck++;
                                //        }
                                //        getappear = getappear + gtotstu;
                                //        getpass = getpass + gpassstud;
                                //        double jh1 = Convert.ToDouble(gpassstud);
                                //        double hf1 = Convert.ToDouble(gtotstu);
                                //        double aclass1 = Convert.ToDouble(jh1 / hf1) * 100;
                                //        aclass_perc1 = 0;
                                //        aclass_perc1 = Math.Round(aclass1, 2);
                                //        if (aclass_perc1.ToString() == "NaN" || aclass_perc1.ToString() == "Infinity")
                                //        {
                                //            aclass_perc1 = 0;
                                //        }
                                //        string ddval = aclass_perc1.ToString();
                                //        string[] spval = ddval.Split(new char[] { '.' });
                                //        if (spval.GetUpperBound(0) == 1)
                                //        {
                                //            int dec = spval[1].Length;
                                //            if (dec == 1)
                                //            {
                                //                ddval = spval[0] + "." + spval[1] + "0";
                                //            }
                                //            else if (spval[1] == "00")
                                //            {
                                //                ddval = spval[0] + ".00";
                                //            }
                                //            else
                                //            {
                                //                ddval = spval[0] + "." + spval[1];
                                //            }
                                //        }
                                //        else
                                //        {
                                //            ddval = spval[0] + ".00";
                                //        }
                                //        FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 6].Text = ddval.ToString();
                                //        //  FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = aclass_perc1.ToString();
                                //        passstu1 = passstu1 + aclass_perc1;
                                //    }
                                //}
                                #endregion
                                if (rdexternal.Checked == true)
                                {
                                    recflag = true;

                                    data.Rows[(data.Rows.Count - dsv) + sec_temp]["Section"] = secvar.ToString();
                                    //drow["Section"] = secvar.ToString();
                                    #region existing which shows wrong value on elective paper since it does not matches the subject chooser

                                    //string ssd = "select count(distinct m.roll_no) from mark_entry m,registration r where m.roll_no=r.roll_no and r.delflag<>1 and m.attempts = 1  and subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " and (result='pass' or result='fail' or result='S') and m.exam_code = " + examcode_fun + "  and degree_code=" + degree_code + " and batch_year=" + batch_year + " " + strsec + ""; 

                                    #endregion

                                    //added by prabha  on jan 18 2018
                                    //no of students elective subject chooser  differed with no of students mark entered
                                    #region matching qry with subject chooser

                                    string ssd = "select count(distinct m.roll_no) from mark_entry m,registration r,subjectChooser sc where m.roll_no=r.roll_no and r.delflag<>1 and m.attempts = 1 and sc.subject_no=m.subject_no and m.roll_no=sc.roll_no  and m.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " and (result='pass' or result='fail' or result='S') and m.exam_code = " + examcode_fun + "  and degree_code=" + degree_code + " and batch_year=" + batch_year + " " + strsec + "";

                                    #endregion

                                    // select count(ea.roll_no) from exam_application ea,exam_appl_details ead,registration r where ea.appl_no=ead.appl_no and subject_no= " + ds_has.Tables[0].Rows[i]["subject_no"] + " and degree_code=" + degree_code + "  and batch_year=" + batch_year + "  " + strsec + " and ea.roll_no=r.roll_no and exam_code=" + examcode_fun + "";
                                    tot_stu = int.Parse(d2.GetFunction(ssd));
                                    if (rbappear.Checked == false)
                                        data.Rows[(data.Rows.Count - dsv) + sec_temp]["No. Strength"] = tot_stu.ToString();

                                    else
                                        data.Rows[(data.Rows.Count - dsv) + sec_temp]["No. Appeared"] = tot_stu.ToString();


                                    //magesh 17.9.18
                                    //string pm = "select count(distinct m.roll_no) from mark_entry m,registration r where m.roll_no=r.roll_no  " + strsec + "  and m.result = 'Pass' and  subject_no =  " + ds_has.Tables[0].Rows[i]["subject_no"] + " and r.delflag<>1 and m.attempts = 1 and ltrim(rtrim(type))='' and m.exam_code=" + examcode_fun + "";
                                    string pm = "select count(distinct m.roll_no) from mark_entry m,registration r,subjectChooser sc  where m.roll_no=r.roll_no  " + strsec + "  and m.result = 'Pass' and  m.subject_no =  " + ds_has.Tables[0].Rows[i]["subject_no"] + " and r.delflag<>1 and m.attempts = 1 and ltrim(rtrim(type))='' and m.exam_code=" + examcode_fun + " and sc.subject_no=m.subject_no and m.roll_no=sc.roll_no";   //magesh 17.9.18
                                    int passstu = int.Parse(d2.GetFunction(pm));

                                    data.Rows[(data.Rows.Count - dsv) + sec_temp]["No. passed"] = passstu.ToString();
                                    no_of_passA = no_of_passA + passstu;
                                    getappear = getappear + tot_stu;
                                    getpass = getpass + passstu;
                                    double jh = Convert.ToDouble(passstu);
                                    double hf = Convert.ToDouble(tot_stu);
                                    double aclass = Convert.ToDouble((jh * 100) / hf);
                                    double aclass_perc = Math.Round(aclass, 2);
                                    //decimal aclass_perc =Convert.ToDecimal(Math.Round(aclass, 2));
                                    if (aclass_perc.ToString() == "NaN" || aclass_perc.ToString() == "Infinity")
                                    {
                                        aclass_perc = 0;
                                    }
                                    string ddval = aclass_perc.ToString();
                                    string[] spval = ddval.Split(new char[] { '.' });
                                    if (spval.GetUpperBound(0) == 1)
                                    {
                                        int dec = spval[1].Length;
                                        if (dec == 1)
                                        {
                                            ddval = spval[0] + "." + spval[1] + "0";
                                        }
                                        else if (spval[1] == "00")
                                        {
                                            ddval = spval[0] + ".00";
                                        }
                                        else
                                        {
                                            ddval = spval[0] + "." + spval[1];
                                        }
                                    }
                                    else
                                    {
                                        ddval = spval[0] + ".00";
                                    }

                                    data.Rows[(data.Rows.Count - dsv) + sec_temp]["Sectionwise Pass %"] = ddval.ToString();

                                    passstu1 = passstu1 + aclass_perc;
                                }
                                string io = ds_has.Tables[0].Rows[i]["subject_no"].ToString();

                                string staff_name = "select staff_name,staff_code from staffmaster where staff_code in (select staff_code from staff_selector where subject_no = " + io + " and batch_year=" + batch_year + "  " + strsec + ")";
                                DataSet dsstf = d2.select_method_wo_parameter(staff_name, "text"); //added by Mullai
                                if (dsstf.Tables.Count > 0 && dsstf.Tables[0].Rows.Count > 0)
                                {

                                    if (chklstaff.SelectedIndex == 0)
                                    {
                                        data.Rows[(data.Rows.Count - dsv) + sec_temp]["Subject Teacher"] = Convert.ToString(dsstf.Tables[0].Rows[0]["staff_name"]);
                                    }
                                    if (chklstaff.SelectedIndex == 1)
                                    {
                                        data.Rows[(data.Rows.Count - dsv) + sec_temp]["Subject Teacher"] = Convert.ToString(dsstf.Tables[0].Rows[0]["staff_code"]);
                                    }
                                    if (chklstaff.SelectedIndex == 2)
                                    {
                                        data.Rows[(data.Rows.Count - dsv) + sec_temp]["Subject Teacher"] = Convert.ToString(dsstf.Tables[0].Rows[0]["staff_code"]) + "-" + Convert.ToString(dsstf.Tables[0].Rows[0]["staff_name"]);

                                    }

                                }

                                // data.Rows[(data.Rows.Count - dsv) + sec_temp]["Subject Teacher"] = staff_name.ToString();
                            }
                            //FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - dsv, 6, dsv, 1);
                            // FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - dsv, 6, dsv, 1);
                            double tot_secwise_per = 0;
                            //decimal tot_secwise_per = 0;
                            double math_tot = 0;
                            // decimal math_tot = 0;
                            if (Convert.ToInt16(ddlSec.Items.Count) >= 2)
                            {
                                if (rdinternal.Checked)
                                {
                                    // tot_secwise_per = passstu1 / Convert.ToInt16(totalcheck);
                                    tot_secwise_per = getpass / getappear * 100;
                                    math_tot = Math.Round(tot_secwise_per, 2);
                                    totalcheck = 0;
                                    getappear = 0;
                                    getpass = 0;
                                }
                                else
                                {
                                    ///tot_secwise_per = passstu1 / Convert.ToInt16(ddlSec.Items.Count);
                                    tot_secwise_per = getpass / getappear * 100;
                                    math_tot = Math.Round(tot_secwise_per, 2);
                                    getappear = 0;
                                    getpass = 0;
                                }
                            }
                            else
                            {
                                math_tot = passstu1;
                            }
                            if (math_tot.ToString() == "NaN" || math_tot.ToString() == "Infinity")
                            {
                                math_tot = 0;
                            }
                            //added by gowtham
                            //-----------
                            for (int seccount = 0; seccount < dsv; seccount++)
                            {
                                string ddval = math_tot.ToString();
                                string[] spval = ddval.Split(new char[] { '.' });
                                if (spval.GetUpperBound(0) == 1)
                                {
                                    int dec = spval[1].Length;
                                    if (dec == 1)
                                    {
                                        ddval = spval[0] + "." + spval[1] + "0";
                                    }
                                    else if (spval[1] == "00")
                                    {
                                        ddval = spval[0] + ".00";
                                    }
                                    else
                                    {
                                        ddval = spval[0] + "." + spval[1];
                                    }
                                }
                                else
                                {
                                    ddval = spval[0] + ".00";
                                }
                                data.Rows[(data.Rows.Count - dsv) + seccount]["Subjectwise pass %"] = ddval.ToString();


                                //if (rdexternal.Checked == true)
                                //{
                                //    FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].Text = ddval.ToString();
                                //    FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].VerticalAlign = VerticalAlign.Middle;
                                //    //FpExternal.Sheets[0].AddSpanCell((FpExternal.Sheets[0].RowCount - dsv) + seccount, 9, dsv, 1);
                                //}
                                //else
                                //{
                                //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].Text = ddval.ToString();
                                //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + seccount, 9].VerticalAlign = VerticalAlign.Middle;
                                //FpExternal.Sheets[0].AddSpanCell((FpExternal.Sheets[0].RowCount - dsv) + seccount, 9, dsv, 1);
                                //}
                            }
                            //  FpExternal.Sheets[0].SpanModel.Add(rowsp, 7, spancolumns, 1);//Hiiden By Srinath For Print
                            //  FpExternal.Sheets[0].SetColumnMerge(6, FarPoint.Web.Spread.Model.MergePolicy.Always);
                            //------------
                        }
                        int rowcount = data.Rows.Count;
                        if (data.Columns.Count > 0 && data.Rows.Count > 0)
                        {

                            rowcount++;
                            if (sl_no > 1)
                            {

                                drow = data.NewRow();
                                drow["SNo"] = "";
                                data.Rows.Add(drow);
                                dicexcolspan.Add(rowcount - 1, "");
                            }
                            if (overallSubjects != 0)
                            {
                                overallAppear /= overallSubjects;
                                overallAppear = Math.Round(overallAppear, 2);
                                overallPass /= overallSubjects;
                                overallPass = Math.Round(overallPass, 2);
                            }
                            //if (rdinternal.Checked == true)
                            //{
                            //    FpExternal.Sheets[0].RowCount++;
                            //    rowcount = FpExternal.Sheets[0].RowCount;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "OVERALL";
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].VerticalAlign = VerticalAlign.Middle;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";
                            //    FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 0, 1, 7);
                            //}
                        }
                        //======================20.07.12
                        if (in_examcode != "")
                        {
                            in_examcode = "in(" + in_examcode + ")";//all examcode
                        }
                        //==================20.07.12
                        double tot_per_all_pass = 0;
                        int tot_stud_overall = 0;
                        double test_minmark = 0;
                        double secper = 0;
                        for (int sec_temp = 0; sec_temp < dsv; sec_temp++)
                        {
                            rowcount++;
                            if ((ddlSec.Items.Count > 1) && (ddlSec.Items[sec_temp].ToString() == "All" || ddlSec.Items[sec_temp].ToString() == string.Empty || ddlSec.Items[sec_temp].ToString() == "-1"))
                            {
                                strsec = string.Empty;
                                secvar = string.Empty;
                            }
                            else
                            {
                                if (ddlSec.Items.Count > 0)
                                {
                                    strsec = " and sections='" + ddlSec.Items[sec_temp].ToString() + "'";
                                    secvar = ddlSec.Items[sec_temp].ToString();
                                }
                            }

                            string in_sec_examcode = string.Empty;
                            //if (rdinternal.Checked == true)
                            //{
                            //    //string sec_examcode = "select distinct r.exam_code as exam_code from exam_type e,subject s,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + ddlTest.SelectedValue.ToString() + "  " + strsec + "  ";
                            //    string sec_examcode = "select distinct e.exam_code as exam_code from exam_type e,subject s where e.subject_no=s.subject_no and e.criteria_no=" + ddlTest.SelectedValue.ToString() + "  " + strsec + "  ";
                            //    DataSet ds_sec_exmcode = d2.select_method_wo_parameter(sec_examcode, "text");
                            //    if (ds_sec_exmcode.Tables.Count > 0 && ds_sec_exmcode.Tables[0].Rows.Count > 0)
                            //    {
                            //        for (int scexm = 0; scexm < ds_sec_exmcode.Tables[0].Rows.Count; scexm++)
                            //        {
                            //            if (in_sec_examcode == "")
                            //            {
                            //                in_sec_examcode = ds_sec_exmcode.Tables[0].Rows[scexm]["exam_code"].ToString();
                            //            }
                            //            else
                            //            {
                            //                in_sec_examcode = in_sec_examcode + "," + ds_sec_exmcode.Tables[0].Rows[scexm]["exam_code"].ToString();
                            //            }
                            //        }
                            //    }
                            //    if (in_sec_examcode != "")
                            //    {
                            //        in_sec_examcode = "in(" + in_sec_examcode + ")";
                            //    }
                            //}
                            string strincludePassedout = string.Empty;
                            string includePassedout = string.Empty;
                            if (!chkincludepastout.Checked)
                            {
                                strincludePassedout = "and rt.cc=0";
                                includePassedout = "and r.cc=0";

                            }
                            string ssd = "", ssd1 = string.Empty;
                            if (rdexternal.Checked == true)
                            {
                                ssd = "select count(*) from registration where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + "  " + strsec + "";
                                //ssd1 = "select COUNT(distinct m.roll_no) as Attended, r.degree_code from mark_entry m,Exam_Details e,Registration r where  e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' " + strsec + " and r.cc=0 and  r.exam_flag <>'DEBAR' and r.delflag=0 and m.attempts=1 and m.roll_no not in (select distinct r.roll_no from mark_entry m,Exam_Details e,Registration r where e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' and r.cc=0 and  r.exam_flag <>'DEBAR' and r.delflag=0 " + strsec + " and result='AAA' and m.attempts=1)  group by r.degree_code";
                                ssd1 = "select COUNT(distinct m.roll_no) as Attended, r.degree_code from mark_entry m,Exam_Details e,Registration r where  e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' " + strsec + " and  r.exam_flag <>'DEBAR' and r.delflag=0 and m.attempts=1 and m.result<>'AAA' and m.result<>'WHD' group by r.degree_code";
                            }
                            //Internal
                            //else if ((rdinternal.Checked == true) && (in_sec_examcode.ToString() != "")) //no of students appeared based on pass all examcode
                            //{
                            //    ssd = "select isnull(count(distinct rt.roll_no),0) as 'allpass_count' from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3'or marks_obtained='-1')  and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strsec + " ";
                            //    //ssd = ssd + "and rt.roll_no not in(select distinct rt.roll_no from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and marks_obtained='-1'  and r.roll_no=rt.roll_no and rt.cc=0 and rt.exam_flag <>'DEBAR' and rt.delflag=0 and rt.RollNo_Flag<>0 " + strsec + " )";
                            //    ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear' from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3')  and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strsec + " ";
                            //    if (rbappear.Checked == false)
                            //    {
                            //        ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear' from subjectChooser sc,registration rt where sc.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.semester='" + ddlSemYr.SelectedValue.ToString() + "' " + strsec + "";
                            //    }
                            //}
                            if (ssd.ToString().Trim() != "")
                                allpascnt = int.Parse(d2.GetFunction(ssd));
                            if (ssd1.ToString().Trim() != "")
                                tot_stu = int.Parse(d2.GetFunction(ssd1));
                            tot_stud_overall = tot_stud_overall + tot_stu;
                            //Modified by srinath 22/8/2013
                            //  FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 2);
                            //  FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "No. of Students Appeared For Tests in Section " + secvar;
                            // FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Text = tot_stu.ToString();
                            //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Font.Bold = true;
                            drow = data.NewRow();



                            if (rbappear.Checked == false)
                                drow["SNo"] = "No. of Students Strength For Tests in Section " + secvar + " : " + tot_stu.ToString();
                            else
                                drow["SNo"] = "No. of Students Appeared For Tests in Section " + secvar + " : " + tot_stu.ToString();


                            tot_stud_str = tot_stud_str + Convert.ToInt32(tot_stu);
                            //---------------------find tot no of student pass in all subject
                            double b4 = 0;
                            double b3 = 0;
                            if (rdexternal.Checked == true)
                            {
                                int fail_stud_atleast_one = 0;
                                DataSet ds = new DataSet();
                                //cmd = new SqlCommand("select count(distinct(mark_entry.roll_no)) from mark_entry,registration where exam_code=" + examcode_fun + " and mark_entry.Attempts = 1 and result='pass'  and passorfail=1 and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + " and mark_entry.roll_no not in(select distinct(mark_entry.roll_no) from mark_entry,registration where exam_code=" + examcode_fun + "  and mark_entry.Attempts = 1 and (result='Fail' or result='AAA')  and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and   batch_year=" + ddlBatch.SelectedItem.ToString() + "" + strsec + " )", con);
                                ds = d2.select_method_wo_parameter("select count(distinct(m.roll_no)) from mark_entry m,registration r,subject s,syllabus_master sy where exam_code=" + examcode_fun + " and m.Attempts = 1 and s.subject_no=m.subject_no and sy.degree_code=r.degree_code and r.Batch_Year=sy.Batch_Year and s.syll_code=sy.syll_code and result='pass'  and passorfail=1 and m.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy.semester=" + ddlSemYr.SelectedItem.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + " and m.roll_no not in(select distinct(m1.roll_no) from mark_entry m1,registration r1,subject s1,syllabus_master sy1 where s1.subject_no=m1.subject_no and sy1.degree_code=r1.degree_code and r1.Batch_Year=sy1.Batch_Year and s1.syll_code=sy1.syll_code and m1.exam_code=" + examcode_fun + "  and m1.Attempts = 1 and (result='Fail' or result='AAA')  and m1.roll_no=r1.roll_no and r1.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy1.semester=" + ddlSemYr.SelectedItem.ToString() + " and   r1.batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + ")", "Text");
                                // Select count(distinct(mark_entry.roll_no)) from mark_entry,registration where exam_code=" + examcode_fun + "  and mark_entry.Attempts = 1 and (result='Fail' or result='AAA')  and passorfail=0 and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + ""
                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    fail_stud_atleast_one = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                                }
                                //--------------------------
                                //FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 3);

                                drow["Subject Teacher"] = "No. of Students Passed In All Subject In Section " + secvar + " " + fail_stud_atleast_one.ToString();

                                b3 = (Convert.ToDouble(fail_stud_atleast_one / Convert.ToDouble(tot_stu))) * 100;
                                b4 = Math.Round(b3, 2);
                                if (b4.ToString() == "NaN" || b4.ToString() == "Infinity")
                                {
                                    b4 = 0;
                                }
                                if (secper == 0)
                                {
                                    secper = b4;
                                }
                                else
                                {
                                    secper = secper + b4;
                                }
                                drow["Subjectwise pass %"] = "All Pass % In Section " + secvar + " " + b4;

                                data.Rows.Add(drow);
                                dicexcolspan.Add(rowcount - 1, "1");
                            }

                            #region Internal
                            //else if ((rdinternal.Checked == true) && (in_sec_examcode.ToString() != ""))
                            //{
                            //    //==========find minmark for particular test
                            //    int fail_in_allsubj = 0;
                            //    //test_minmark = Convert.ToInt32(GetFunction("select min_mark from criteriaforinternal where criteria_no=" + ddlTest.SelectedValue.ToString() + ""));  //by malang raja
                            //    string minmrk = d2.GetFunction("select min_mark from criteriaforinternal where criteria_no=" + ddlTest.SelectedValue.ToString() + "");
                            //    double.TryParse(minmrk, out test_minmark);
                            //    DataSet ds = new DataSet();
                            //    ssd = "select isnull(count(distinct rt.roll_no),0) from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and (rt.marks_obtained<" + test_minmark + " and rt.marks_obtained<>'-3' and rt.marks_obtained<>'-2' and rt.marks_obtained<>'-18') and r.exam_flag <>'DEBAR' and r.delflag=0 " + includePassedout + " and r.RollNo_Flag<>0  ";
                            //    //Modified by srinath 3/9/2013
                            //    // ssd = ssd + " and rt.roll_no not in (select distinct rt.roll_no from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and rt.marks_obtained='-1')";
                            //    // ssd = ssd + " and rt.roll_no not in (select distinct rt.roll_no from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and rt.marks_obtained='-1' and rt.marks_obtained=0)";
                            //    ds = d2.select_method_wo_parameter(ssd, "text");
                            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            //    {
                            //        fail_in_allsubj = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                            //    }
                            //    // FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 3);
                            //    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 4);
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Text = "No. of Students Passed In All Subject In Section " + secvar + " :" + (allpascnt - fail_in_allsubj).ToString();
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
                            //    // FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (allpascnt - fail_in_allsubj).ToString();//subtract from tot-failcount for getting pass count
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Font.Bold = true;
                            //    nofopass_str = Convert.ToInt32(nofopass_str + ((allpascnt - fail_in_allsubj)));
                            //    totvl = ((allpascnt - fail_in_allsubj));
                            //    //b3 = tot_per_all_pass + allpascnt;
                            //    b3 = (Convert.ToDouble(totvl) / Convert.ToDouble(tot_stu)) * 100;
                            //    noofperc = noofperc + Math.Round(b3);
                            //    b4 = Math.Round(b3, 2);
                            //    if (b4.ToString() == "NaN" || b4.ToString() == "Infinity")
                            //    {
                            //        b4 = 0;
                            //    }
                            //    if (secper == 0)
                            //    {
                            //        secper = b4;
                            //    }
                            //    else
                            //    {
                            //        secper = secper + b4;
                            //    }
                            //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Text = b4.ToString();
                            //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Font.Bold = true;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Text = "All Pass % In Section " + secvar + " " + b4;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].HorizontalAlign = HorizontalAlign.Left;
                            //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Font.Bold = true;
                            //    FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 7, 1, 3);
                            //}
                            #endregion
                            tot_per_all_pass = tot_per_all_pass + b4;
                        }///////////////////////////28/6/12 PRABHA

                        #region Internal
                        //if ((rdinternal.Checked == true))
                        //{
                        //    FpExternal.Sheets[0].RowCount++;
                        //    //--------------------------------------------------------------------------------------------------------
                        //    //Modified by Srinath 22/8/2013 
                        //    //FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 2);
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "Total No of Students Appeared For Tests In All Sections "; //"Total No of Students Appeared For Tests In Section " + sec_str ;
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Text = tot_stud_str.ToString();
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 2].Font.Bold = true;
                        //    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 3);
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "Total No of Students Appeared For Tests In All Sections : " + tot_stud_str.ToString() + ""; //"Total No of Students Appeared For Tests In Section " + sec_str ;
                        //    if (rbappear.Checked == false)
                        //    {
                        //        FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "Total No of Students Strength For Tests In All Sections : " + tot_stud_str.ToString() + ""; //"Total No of Students Appeared For Tests In Section " + sec_str ;
                        //    }
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
                        //    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 3, 1, 4);
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Text = "Total No of Students Passed In All Subject In All Sections: " + nofopass_str.ToString() + ""; //"Total No of Students Passed In All Subject In Section" + sec_str;
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = nofopass_str.ToString();
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Font.Bold = true;
                        //    //FpExternal.Sheets[0].Columns[3].Width = 180;
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Text = Math.Round((Convert.ToDouble(nofopass_str.ToString()) / Convert.ToDouble(tot_stud_str.ToString())) * 100,2).ToString(); //noofperc.ToString();
                        //    string calc = Math.Round((Convert.ToDouble(nofopass_str.ToString()) / Convert.ToDouble(tot_stud_str.ToString())) * 100, 2).ToString(); //noofperc.ToString();
                        //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Font.Bold = true;
                        //    //FpExternal.Sheets[0].Columns[7].Width = 170;
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Text = "All Pass % In All Sections :" + " " + calc; //"All Pass % In Section " + sec_str ;
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].HorizontalAlign = HorizontalAlign.Left;
                        //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 7].Font.Bold = true;
                        //    FpExternal.Sheets[0].AddSpanCell(FpExternal.Sheets[0].RowCount - 1, 7, 1, 3);
                        //    FpExternal.Sheets[0].AutoPostBack = true;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Text = tot_stud_str.ToString();
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 7].VerticalAlign = VerticalAlign.Middle;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Font.Bold = true;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Font.Size = FontUnit.Medium;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 7].Font.Name = "Book Antiqua";
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Text = (nofopass_str).ToString();
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 8].VerticalAlign = VerticalAlign.Middle;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Font.Bold = true;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Font.Size = FontUnit.Medium;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 8].Font.Name = "Book Antiqua";
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Text = calc.ToString();
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 9].VerticalAlign = VerticalAlign.Middle;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Font.Bold = true;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Font.Size = FontUnit.Medium;
                        //    FpExternal.Sheets[0].Cells[rowcount - 1, 9].Font.Name = "Book Antiqua";
                        //    //--------------------------------------------------------------------------------------------------------
                        //}
                        #endregion
                        if (rdexternal.Checked == true)
                        {
                            drow = data.NewRow();
                            drow["SNo"] = "All PASS % In This Test/Semester";


                            //double bb3 = (tot_per_all_pass) / ddlSec.Items.Count;
                            //double bb4 = Math.Round(bb3, 2);
                            double bb4 = 0;
                            if (ddlSec.Items.Count > 0)
                            {
                                bb4 = secper / ddlSec.Items.Count;
                            }
                            else
                            {
                                bb4 = secper;
                            }
                            //double bb3 = 0;
                            //bb3 = (Convert.ToDouble(allpascnt) / Convert.ToDouble(tot_stu));
                            //bb4 = bb4 + Math.Round(bb3);
                            //bb4 = Math.Round(bb3, 2);
                            ////bb3 = Math.Round((Convert.ToDouble(tot_per_all_pass.ToString()) / Convert.ToDouble(ddlSec.ToString())) * 100, 2).ToString();
                            ////string bb4 = Math.Round(bb3, 2);
                            if (bb4.ToString() == "NaN" || bb4.ToString() == "Infinity")
                            {
                                bb4 = 0;
                            }
                            bb4 = Math.Round(bb4, 2, MidpointRounding.AwayFromZero);
                            drow["Subjectwise pass %"] = bb4.ToString();
                            data.Rows.Add(drow);
                            dicexcolspan.Add(data.Rows.Count - 1, "2");
                        }
                        //--------------------------------------------27/6/12 PRABHA
                        if (Showgrid.PageSize > 0)
                        {
                            if (rdexternal.Checked == true)
                            {
                                DateTime todate = DateTime.Now;
                                if (yr.ToString() == todate.ToString("yyyy"))
                                {
                                    drow = data.NewRow();
                                    drow["SNo"] = "% of students qualifying for degree(for final year final semester only)";


                                    if (rdexternal.Checked == true)
                                    {
                                        int all_padd_get_degree = Convert.ToInt32(d2.GetFunction("select count(distinct m.roll_no )from mark_entry m,subject s,syllabus_master sy,subjectchooser sc  where s.subject_no=m.subject_no and m.subject_no=sc.subject_no and s.syll_code=sy.syll_code and sy.semester=sc.semester and sy.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy.batch_year=" + ddlBatch.SelectedItem.ToString() + " and sy.semester<=" + ddlSemYr.SelectedItem.ToString() + " and (result='Fail' or result='AAA')  and passorfail=0 "));
                                        drow["Sectionwise pass %"] = (Math.Round(((Convert.ToDouble(tot_stud_overall - all_padd_get_degree) / Convert.ToDouble(tot_stud_overall)) * 100), 2)).ToString();

                                    }
                                    data.Rows.Add(drow);
                                    dicexcolspan.Add(data.Rows.Count - 1, "3");
                                }
                            }
                        }
                        //==============================================================

                        if (data.Columns.Count > 0 && data.Rows.Count > 1)
                        {

                            divMainContents.Visible = true;
                            Showgrid.DataSource = data;
                            Showgrid.DataBind();
                            Showgrid.Visible = true;

                            Showgrid.Rows[0].BackColor = ColorTranslator.FromHtml("#0CA6CA");
                            Showgrid.Rows[0].Font.Bold = true;
                            Showgrid.Rows[0].HorizontalAlign = HorizontalAlign.Center;

                            int d = Convert.ToInt32(data.Columns.Count);
                            for (int g = 0; g < data.Columns.Count; g++)
                            {
                                if (g != 1 && g != 3)
                                {

                                    for (int j = 1; j < Showgrid.Rows.Count; j++)
                                    {
                                        string value = dicexcolspan[j];
                                        if (value.Trim() != "1" && value.Trim() != "2" && value.Trim() != "3" && value.Trim() != "")
                                            Showgrid.Rows[j].Cells[g].HorizontalAlign = HorizontalAlign.Center;
                                    }
                                }
                            }

                            if (dicexcolspan.Count > 0)
                            {
                                //ColumnSpan
                                foreach (KeyValuePair<int, string> dr in dicexcolspan)
                                {
                                    int k = dr.Key;
                                    string hrhead = dr.Value;


                                    if (hrhead == "" || hrhead == "3")
                                    {
                                        Showgrid.Rows[k].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                                        Showgrid.Rows[k].Cells[0].Font.Bold = true;
                                        Showgrid.Rows[k].Cells[0].ColumnSpan = d;
                                        for (int a = 1; a < d; a++)
                                            Showgrid.Rows[k].Cells[a].Visible = false;

                                    }
                                    else if (hrhead == "1")
                                    {
                                        Showgrid.Rows[k].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                                        Showgrid.Rows[k].Cells[0].Font.Bold = true;
                                        Showgrid.Rows[k].Cells[0].ColumnSpan = 3;
                                        Showgrid.Rows[k].Cells[1].Visible = false;
                                        Showgrid.Rows[k].Cells[2].Visible = false;
                                        Showgrid.Rows[k].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                                        Showgrid.Rows[k].Cells[3].Font.Bold = true;
                                        Showgrid.Rows[k].Cells[3].ColumnSpan = 4;
                                        Showgrid.Rows[k].Cells[4].Visible = false;
                                        Showgrid.Rows[k].Cells[5].Visible = false;
                                        Showgrid.Rows[k].Cells[6].Visible = false;

                                    }
                                    else if (hrhead == "2")
                                    {
                                        Showgrid.Rows[k].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                                        Showgrid.Rows[k].Cells[0].Font.Bold = true;
                                        Showgrid.Rows[k].Cells[0].ColumnSpan = 7;
                                        for (int a = 1; a < d - 1; a++)
                                            Showgrid.Rows[k].Cells[a].Visible = false;

                                    }

                                }

                                //Rowspan
                                //for (int rowIndex = Showgrid.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                                //{
                                //    GridViewRow row = Showgrid.Rows[rowIndex];
                                //    GridViewRow previousRow = Showgrid.Rows[rowIndex + 1];

                                //    string value = dicexcolspan[rowIndex];
                                //    string value1 = dicexcolspan[rowIndex + 1];
                                //    if (value.Trim() != "1" && value.Trim() != "2" && value.Trim() != "3" && value.Trim() != "" &&
                                //        value1.Trim() != "1" && value1.Trim() != "2" && value1.Trim() != "3" && value1.Trim() != "")
                                //    {
                                //        for (int j = 0; j < row.Cells.Count; j++)
                                //        {

                                //            if (row.Cells[j].Text == previousRow.Cells[j].Text)
                                //            {
                                //                if (previousRow.Cells[j].RowSpan == 0)
                                //                {
                                //                    row.Cells[j].RowSpan = previousRow.Cells[j].RowSpan < 2 ? 2 :
                                //                   previousRow.Cells[j].RowSpan + 1;
                                //                    previousRow.Cells[j].Visible = false;
                                //                }
                                //            }
                                //        }
                                //    }

                                //}

                                for (int rowIndex = Showgrid.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                                {
                                    GridViewRow row = Showgrid.Rows[rowIndex];
                                    GridViewRow previousRow = Showgrid.Rows[rowIndex + 1];

                                    for (int i = 0; i < row.Cells.Count; i++)
                                    {
                                        if (i != 4 && i != 5)
                                        {
                                            if (row.Cells[i].Text == previousRow.Cells[i].Text)
                                            {

                                                row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                                       previousRow.Cells[i].RowSpan + 1;
                                                previousRow.Cells[i].Visible = false;
                                            }
                                        }
                                    }

                                }
                            }
                            btnmasterprint.Visible = true;
                            btn_dirtprint.Visible = true;
                            //Added By Srinath 28/2/2013
                            btnxl.Visible = true;//Added By Srinath 
                            lblrptname.Visible = true;
                            txtexcelname.Visible = true;
                            lblnorec.Visible = false;
                        }
                    }
                    if (recflag == false)
                    {
                        if (rdinternal.Checked == true)
                        {
                            lblnorec.Visible = true;
                            Showgrid.Visible = false;
                            btnmasterprint.Visible = false;
                            btn_dirtprint.Visible = false;
                            //Added By Srinath 
                            divMainContents.Visible = false;
                            btnxl.Visible = false;
                            lblrptname.Visible = false;
                            txtexcelname.Visible = false;
                            lblnorec.Text = "No Records Found";
                        }
                        else if (rdexternal.Checked == true)
                        {
                            lblnorec.Visible = true;
                            Showgrid.Visible = false;
                            btnmasterprint.Visible = false;
                            btn_dirtprint.Visible = false;
                            //Added By Srinath 
                            divMainContents.Visible = false;
                            btnxl.Visible = false;
                            lblrptname.Visible = false;
                            txtexcelname.Visible = false;
                            lblnorec.Text = "No Records Found";
                        }
                    }
                    // logoset();

                    func_multi_iso();
                }
            }
            #endregion

            #region Internal_Checked
            else
            {
                Dictionary<int, string> dicstdtype = new Dictionary<int, string>();
                dicstdtype.Add(0, "Day Scholar");
                dicstdtype.Add(1, "Hosteller");
                dicstdtype.Add(2, "Lateral Entry");
                Dictionary<int, string> dicstudtype = new Dictionary<int, string>();
                dicstudtype.Add(0, "Day Scholar");
                dicstudtype.Add(1, "Hostler");
                dicstudtype.Add(2, "Lateral Entry");
                string Exam_code_val = string.Empty;
                bool recflag = false;
                if (ddlDegree.Text == "")
                {
                    return;
                }
                long tot_stud_str = 0;
                long nofopass_str = 0;
                double noofperc = 0;
                string strsec = string.Empty;
                string secvar = string.Empty;
                string sec_str = string.Empty;
                double aclass_perc1 = 0;
                //decimal aclass_perc1 = 0;
                int totvl = 0;
                lblnorec.Visible = false;
                if (rdexternal.Checked == false && rdinternal.Checked == false)
                {
                    lblnorec.Text = "Kindly select Report";
                    lblnorec.Visible = true;
                }
                else
                {
                    if (rdexternal.Checked == false && rdinternal.Checked == true)
                    {
                        if (ddlTest.Items.Count > 0)
                        {
                            if (ddlTest.SelectedItem.ToString() == "--Select--")
                            {
                                lblnorec.Text = "Please Select The Test";
                                lblnorec.Visible = true;
                                return;
                            }
                        }
                    }
                    lblnorec.Visible = false;
                    lblnorec.Text = string.Empty;
                    if (rdexternal.Checked == true)
                    {
                        lblTest.Visible = false;
                        ddlTest.Visible = false;
                        Lbl_Gender.Visible = false;
                        UpdatePanelGender.Visible = false;
                        Columnorder.Visible = false;
                    }
                    else
                    {
                        Lbl_Gender.Visible = true;
                        UpdatePanelGender.Visible = true;
                        lblTest.Visible = true;
                        ddlTest.Visible = true;
                        Columnorder.Visible = true;
                    }

                    int yr = 0;
                    int sem_new = 0;
                    string sem_fun = string.Empty;
                    string exam_month_fun = "", exam_year_fun = string.Empty;
                    string subjects_fun = string.Empty;
                    int examcode_fun = 0;
                    int tot_stu = 0, tot_stu1 = 0, allpascnt = 0;
                    int no_of_passA = 0, no_of_passB = 0;
                    int sl_no = 0;
                    batch_year = ddlBatch.SelectedValue.ToString();
                    degree = ddlDegree.SelectedValue.ToString();
                    degree_code = ddlBranch.SelectedValue.ToString();
                    semesterddl = ddlSemYr.SelectedValue.ToString();
                    sections = ddlSec.SelectedValue.ToString();
                    exam_year = ddlYear.SelectedValue.ToString();
                    exam_month = ddlMonth.SelectedValue.ToString();
                    int exam_code = 0;// Convert.ToInt32(GetFunction("select exam_code from exam_details where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + ""));
                    for (int seccnt = 0; seccnt < ddlSec.Items.Count; seccnt++)
                    {
                        if (sec_str == "")
                        {
                            sec_str = ddlSec.Items[seccnt].ToString();
                        }
                        else
                        {
                            sec_str = sec_str + "," + ddlSec.Items[seccnt].ToString();
                        }
                    }


                    int tot_sem = 0;
                    yr = 0;
                    con.Close();
                    cmd = new SqlCommand("select ndurations from ndegree where batch_year=" + ddlBatch.SelectedValue + "  and degree_code=" + ddlBranch.SelectedValue + "", con);
                    SqlDataReader no_on_sem_dr;
                    con.Open();
                    no_on_sem_dr = cmd.ExecuteReader();
                    if (no_on_sem_dr.HasRows)
                    {
                        while (no_on_sem_dr.Read())
                        {
                            tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
                            yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("select duration from degree where degree_code=" + ddlBranch.SelectedValue + "", con);
                        con.Close();
                        con.Open();
                        no_on_sem_dr = cmd.ExecuteReader();
                        if (no_on_sem_dr.HasRows)
                        {
                            while (no_on_sem_dr.Read())
                            {
                                tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
                                yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
                            }
                        }
                    }
                    string subject_No = string.Empty;
                    //-----------------------------------------------------------

                    arrColHdrNames1.Add("S.No");
                    arrColHdrNames1.Add("Subject Code and Name");
                    arrColHdrNames1.Add("Section");
                    arrColHdrNames1.Add("Subject Teacher");

                    arrColHdrNames2.Add("S.No");
                    arrColHdrNames2.Add("Subject Code and Name");
                    arrColHdrNames2.Add("Section");
                    arrColHdrNames2.Add("Subject Teacher");

                    data.Columns.Add("SNo", typeof(string));
                    data.Columns.Add("Subject code and name", typeof(string));
                    data.Columns.Add("Section", typeof(string));
                    data.Columns.Add("Subject Teacher", typeof(string));

                    Boolean colvis = false;
                    if (CkLGender.Items.Count > 0)
                    {
                        for (int st = 0; st < 3; st++)
                        {
                            string stype = dicstdtype[st];
                            if (cblsearch.Items[st + 3].Selected == true)
                            {
                                colvis = true;
                                //string colname = cblsearch.Items[st + 4].Text;
                                for (int gen = 0; gen < CkLGender.Items.Count; gen++)
                                {
                                    if (CkLGender.Items[gen].Selected == true)
                                    {
                                        string gender = CkLGender.Items[gen].Text;
                                        arrColHdrNames1.Add(stype);
                                        arrColHdrNames2.Add(gender);
                                        System.Text.StringBuilder gende = new System.Text.StringBuilder(gender);
                                        AddTableColumn(data, gende);
                                        arrColHdrNames1.Add(stype);
                                        arrColHdrNames2.Add("No.Of Passed");
                                        System.Text.StringBuilder pass = new System.Text.StringBuilder("No.Of Passed");

                                        AddTableColumn(data, pass);
                                    }
                                }
                            }
                        }
                    }

                    if (rbappear.Checked == false)
                    {
                        arrColHdrNames1.Add("No. Strength");
                        arrColHdrNames2.Add("No. Strength");
                        data.Columns.Add("No. Strength", typeof(string));
                    }
                    else
                    {
                        arrColHdrNames1.Add("No. Appeared");
                        arrColHdrNames2.Add("No. Appeared");
                        data.Columns.Add("No. Appeared", typeof(string));
                    }
                    arrColHdrNames1.Add("No. Passed");
                    arrColHdrNames2.Add("No. Passed");
                    arrColHdrNames1.Add("Sectionwise Pass %");
                    arrColHdrNames2.Add("Sectionwise Pass %");
                    data.Columns.Add("No. passed", typeof(string));
                    data.Columns.Add("Sectionwise Pass %", typeof(string));


                    arrColHdrNames1.Add("Overall");
                    arrColHdrNames1.Add("Overall");
                    arrColHdrNames1.Add("Overall");
                    arrColHdrNames2.Add("Appeared");
                    arrColHdrNames2.Add("Pass");
                    arrColHdrNames2.Add("Pass %");
                    data.Columns.Add("Appeared", typeof(string));
                    data.Columns.Add("Pass", typeof(string));
                    data.Columns.Add("Pass %", typeof(string));


                    DataRow drHdr1 = data.NewRow();
                    DataRow drHdr2 = data.NewRow();
                    for (int grCol = 0; grCol < data.Columns.Count; grCol++)
                    {
                        drHdr1[grCol] = arrColHdrNames1[grCol];
                        drHdr2[grCol] = arrColHdrNames2[grCol];
                    }

                    data.Rows.Add(drHdr1);
                    data.Rows.Add(drHdr2);


                    double passstu1 = 0;
                    //decimal  passstu1 = 0;
                    exam_month_fun = ddlMonth.SelectedValue.ToString();
                    exam_year_fun = ddlYear.SelectedValue.ToString();
                    sem_new = Convert.ToInt32(ddlSemYr.SelectedValue.ToString());
                    sem_fun = GetSemester_AsNumber(Convert.ToInt32(sem_new)).ToString();
                    examcode_fun = int.Parse(d2.GetFunction("select distinct exam_code from exam_details where degree_code='" + degree_code + "' and batch_year=" + batch_year + " and exam_month=" + exam_month_fun + " and exam_year=" + exam_year_fun + ""));
                    Session["examcode"] = examcode_fun;
                    string criteria_no = ddlTest.SelectedValue.ToString();
                    string in_examcode = string.Empty;
                    if (rdinternal.Checked == true) //from [PROC_STUD_ALL_SUBMARK]
                    {
                        if (ddlTest.Items.Count == 0)
                        {
                            lblnorec.Visible = true;
                            lblnorec.Text = "Kindly select Test";
                            Showgrid.Visible = false;
                            lblrptname.Visible = false;
                            txtexcelname.Visible = false;
                            btnxl.Visible = false;
                            btnmasterprint.Visible = false;
                            btn_dirtprint.Visible = false;

                            return;
                        }
                        else
                        {
                            lblnorec.Text = string.Empty;
                            lblnorec.Visible = false;
                            Showgrid.Visible = true;
                            lblrptname.Visible = true;
                            txtexcelname.Visible = true;
                            btnxl.Visible = true;
                            btnmasterprint.Visible = true;
                            btn_dirtprint.Visible = true;
                            // string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " order by s.subject_no ";
                            //magesh 14.8.18
                            string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e,syllabus_master ss where e.subject_no=s.subject_no and e.criteria_no=" + criteria_no + " and s.syll_code=ss.syll_code and degree_code='" + degree_code + "' and semester='" + semesterddl + "' and e.batch_year='" + batch_year + "' order by s.subject_no";

                            //   string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e where e.subject_no=s.subject_no and e.criteria_no=" + criteria_no + " order by s.subject_no ";

                            //magesh 14.8.18
                            ds_has.Dispose();
                            ds_has = d2.select_method_wo_parameter(test_subj, "Text");
                        }
                    }
                    if (ds_has.Tables.Count > 0 && ds_has.Tables[0].Rows.Count > 0)
                    {  //subject_No
                        for (int i = 0; i < ds_has.Tables[0].Rows.Count; i++)
                        {
                            if (string.IsNullOrEmpty(subject_No))
                                subject_No = "'" + Convert.ToString(ds_has.Tables[0].Rows[i]["subject_no"]) + "'";
                            else
                                subject_No += ",'" + Convert.ToString(ds_has.Tables[0].Rows[i]["subject_no"]) + "'";
                        }
                    }
                    string tmpvar = string.Empty;
                    int dsv = ddlSec.Items.Count;
                    if (dsv == 0)
                        dsv = 1;
                    double total_OverAllAppear = 0;
                    double total_OverAllPass = 0;
                    Dictionary<string, int> dictotalappeared = new Dictionary<string, int>();
                    Dictionary<string, int> dictotalpass = new Dictionary<string, int>();
                    Dictionary<int, string> dicrowspan = new Dictionary<int, string>();
                    if (ds_has.Tables.Count > 0 && 0 < ds_has.Tables[0].Rows.Count)
                    {
                        sl_no = 1;
                        int spancolumns = 0;
                        int rowsp = 0;
                        int Row_Span_Value = 0;
                        int start_Row = 0;
                        double overallAppear = 0;
                        double overallPass = 0;
                        double overallpercentage = 0;

                        int count = 0;
                        int rocnt = 1;
                        int rcount = 0;

                        string staff_qry1 = "select staff_name,sm.staff_code,subject_no,sections from staffmaster sm,staff_selector ss where sm.staff_code=ss.staff_code and  batch_year=" + batch_year + "";
                        DataSet dsstaff1 = d2.select_method_wo_parameter(staff_qry1, "text");

                        string strexam1 = "select distinct staff_code,duration,convert(varchar(10),exam_date,103)as exam_date,convert(varchar(10),entry_date,103)as entry_date,max_mark,min_mark,r.exam_code,s.subject_no,e.sections,criteria_no from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code ";
                        DataSet ds_exam1 = d2.select_method_wo_parameter(strexam1, "text");




                        for (int i = 0; i < ds_has.Tables[0].Rows.Count; i++)
                        {
                            spancolumns = 0;
                            if (i == 0)
                            {
                                sl_no = 1;

                            }
                            else
                            {
                                if (Convert.ToString(ds_has.Tables[0].Rows[i]["subject_name"]) != Convert.ToString(ds_has.Tables[0].Rows[i - 1]["subject_name"]))
                                {
                                    sl_no++;

                                }
                            }
                            if (sl_no > 1)
                            {
                                rocnt++;
                                drow = data.NewRow();
                                drow["SNo"] = "";
                                data.Rows.Add(drow);
                                dicrowspan.Add(rocnt, "");
                            }

                            start_Row = data.Rows.Count;

                            for (int seccount = 1; seccount <= dsv; seccount++)
                            {

                                if ((ddlSec.Items.Count > 1) && (ddlSec.Items[seccount - 1].ToString() == "All" || ddlSec.Items[seccount - 1].ToString() == string.Empty || ddlSec.Items[seccount - 1].ToString() == "-1"))
                                {
                                    strsec = string.Empty;
                                    secvar = string.Empty;
                                }
                                else
                                {
                                    if (ddlSec.Items.Count > 0)
                                    {
                                        strsec = " and sections='" + ddlSec.Items[seccount - 1].ToString() + "'";
                                        secvar = ddlSec.Items[seccount - 1].ToString();
                                    }
                                }

                                dsstaff1.Tables[0].DefaultView.RowFilter = "subject_no = " + Convert.ToString(ds_has.Tables[0].Rows[i]["subject_no"]) + "  " + strsec + "";
                                DataTable dsstaf = dsstaff1.Tables[0].DefaultView.ToTable();
                                DataSet dsstaff = new DataSet();
                                dsstaff.Tables.Add(dsstaf);
                                //string staff_qry = "select staff_name,staff_code from staffmaster where staff_code in (select staff_code from staff_selector where subject_no = " + Convert.ToString(ds_has.Tables[0].Rows[i]["subject_no"]) + " and batch_year=" + batch_year + "  " + strsec + ")";
                                //DataSet dsstaff = d2.select_method_wo_parameter(staff_qry, "text");
                                if (dsstaff.Tables.Count > 0 && dsstaff.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow row in dsstaff.Tables[0].Rows)
                                    {
                                        rocnt++;
                                        Row_Span_Value++;
                                        spancolumns++;
                                        drow = data.NewRow();
                                        drow["SNo"] = sl_no.ToString();
                                        drow["Subject code and name"] = ds_has.Tables[0].Rows[i]["subject_code"] + " - " + ds_has.Tables[0].Rows[i]["subject_name"];
                                        dicrowspan.Add(rocnt, ds_has.Tables[0].Rows[i]["subject_code"].ToString());

                                        if (chklstaff.SelectedIndex == 0)
                                        {
                                            drow["Subject Teacher"] = Convert.ToString(row["staff_name"]);


                                        }
                                        if (chklstaff.SelectedIndex == 1)
                                        {
                                            drow["Subject Teacher"] = ds_has.Tables[0].Rows[i]["subject_code"];

                                            //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 3].Tag = Convert.ToString(row["staff_code"]);
                                        }
                                        if (chklstaff.SelectedIndex == 2)
                                        {
                                            drow["Subject Teacher"] = Convert.ToString(row["staff_code"]) + '-' + Convert.ToString(row["staff_name"]);


                                        }
                                        //*******

                                        double totalAppear = 0;
                                        double totalPass = 0;
                                        //New END
                                        Double getappear = 0;
                                        Double getpass = 0;
                                        passstu1 = 0;
                                        int totalcheck = 0;

                                        #region security settings
                                        Session["StaffSelector"] = "0";
                                        bool strstaffselector = false;   //Session["collegecode"].ToString()
                                        string staffbatchyear = dacces2.GetFunction("select LinkValue from New_InsSettings where LinkName='Studnet Staff Selector' and college_code='" + Session["collegecode"].ToString() + "'");
                                        string[] splitminimumabsentsms = staffbatchyear.Split('-');
                                        if (splitminimumabsentsms.Length == 2)
                                        {
                                            int batchyearsetting = Convert.ToInt32(splitminimumabsentsms[1].ToString());
                                            if (splitminimumabsentsms[0].ToString() == "1")
                                            {
                                                if (Convert.ToInt32(batch_year) >= batchyearsetting)
                                                {
                                                    Session["StaffSelector"] = "1";
                                                }
                                            }
                                        }
                                        if (Session["StaffSelector"].ToString() == "1")
                                        {
                                            strstaffselector = true;
                                        }
                                        #endregion


                                        ds_exam1.Tables[0].DefaultView.RowFilter = "criteria_no=" + criteria_no + " and subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " " + strsec + "";
                                        DataTable dtexam = ds_exam1.Tables[0].DefaultView.ToTable();
                                        DataSet ds_exam = new DataSet();
                                        ds_exam.Tables.Add(dtexam);
                                        //string strexam = "select distinct staff_code,duration,convert(varchar(10),exam_date,103)as exam_date,convert(varchar(10),entry_date,103)as entry_date,max_mark,min_mark,r.exam_code,s.subject_no,e.sections from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " and s.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " " + strsec + "";
                                        //DataSet ds_exam = d2.select_method_wo_parameter(strexam, "text");
                                        drow["Section"] = secvar;


                                        if (ds_exam.Tables.Count > 0 && ds_exam.Tables[0].Rows.Count > 0)
                                        {
                                            recflag = true;
                                            if (in_examcode == "")
                                            {
                                                in_examcode = ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
                                            }
                                            else
                                            {
                                                in_examcode = in_examcode + "," + ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
                                            }
                                            string sect = ds_exam.Tables[0].Rows[0]["sections"].ToString();
                                            string sectval = string.Empty;
                                            string sectionval = string.Empty;// Added by madhumathi
                                            if (sect.Trim() != "" && sect.Trim() != "-1" && sect.Trim() != null)
                                            {
                                                sectval = " and rt.sections='" + sect + "'";
                                                sectionval = "and reg.sections = '" + sect + "'";// Added by madhumathi
                                            }
                                            else
                                            {
                                                sectval = string.Empty;
                                                sect = string.Empty;
                                                sectionval = string.Empty;// Added by madhumathi
                                            }
                                            string strincludePassedout = string.Empty;
                                            string includePassedout = string.Empty;
                                            if (!chkincludepastout.Checked)
                                            {
                                                strincludePassedout = "and rt.cc=0";
                                                includePassedout = "and reg.cc=0";
                                            }
                                            string str_staff_qry = string.Empty;
                                            if (strstaffselector == true)
                                            {
                                                str_staff_qry = "and sc.staffCode='" + Convert.ToString(row["staff_code"]) + "'";
                                            }
                                            string totstu = "";
                                            string totstudcnt = "";
                                            int gtotstu = 0;
                                            int gpassstud = 0;

                                            DataSet dsstdcnt = new DataSet();
                                            if (rbappear.Checked == true)
                                            {
                                                totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt,exam_type e,subjectchooser sc where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no  and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + "" + str_staff_qry;


                                                totstudcnt = "select count(marks_obtained) as 'PRESENT_COUNT',isnull(rt.Stud_Type,'Day Scholar') as Stud_Type,al.sex,rt.mode from result r,registration rt,exam_type e,subjectchooser sc,Applyn al where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no and al.app_no=rt.App_No  and rt.RollNo_Flag<>0  and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + str_staff_qry + " group by rt.Stud_Type ,al.sex,rt.mode";
                                                dsstdcnt.Clear();
                                                dsstdcnt = d2.select_method_wo_parameter(totstudcnt, "Text");
                                            }
                                            else
                                            {
                                                totstu = "select count(rt.roll_no) as 'PRESENT_COUNT' from registration rt,subjectchooser sc where rt.roll_no=sc.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and  sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + "" + str_staff_qry;
                                                totstudcnt = "select count(rt.roll_no) as 'PRESENT_COUNT',isnull(rt.Stud_Type,'Day Scholar') as Stud_Type,al.sex,rt.mode  from registration rt,subjectchooser sc,Applyn al where rt.roll_no=sc.roll_no and al.app_no=rt.App_No and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and  sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + str_staff_qry + " group by rt.Stud_Type,al.sex,rt.mode";
                                                dsstdcnt.Clear();
                                                dsstdcnt = d2.select_method_wo_parameter(totstudcnt, "Text");
                                            }
                                            if (!colvis)
                                            {
                                                gtotstu = int.Parse(d2.GetFunction(totstu));
                                                if (rbappear.Checked == false)
                                                    drow["No. Strength"] = gtotstu.ToString();
                                                else
                                                    drow["No. Appeared"] = gtotstu.ToString();

                                               // totalAppear += gtotstu;
                                               // overallAppear += gtotstu;
                                            }
                                            //string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result r,registration reg,exam_type e,subjectchooser sc where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + "and reg.RollNo_Flag<>0 " + "" + str_staff_qry; 


                                            string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result r,registration reg,exam_type e,subjectchooser sc where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + " " + sectionval + " and reg.RollNo_Flag<>0 " + "" + str_staff_qry;//altered by madhumathi
                                            if (!colvis)
                                            {
                                                gpassstud = int.Parse(d2.GetFunction(passstud));

                                               // totalPass += gpassstud;
                                              //  overallPass += gpassstud;
                                                if (gpassstud.ToString() != "")
                                                {
                                                    totalcheck++;
                                                }
                                               // getappear = getappear + gtotstu;
                                                //getpass = getpass + gpassstud;
                                            }
                                            string passstudcnt = "select count(marks_obtained) as 'PASS_COUNT',reg.Stud_Type,al.sex,reg.mode from result r,registration reg,exam_type e,subjectchooser sc,Applyn al where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no  and al.app_no=reg.App_No and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + " " + sectionval + " and reg.RollNo_Flag<>0 " + str_staff_qry + "  group by reg.Stud_Type ,al.sex,reg.mode ";
                                            DataSet dsstdpsct = new DataSet();
                                            dsstdpsct = d2.select_method_wo_parameter(passstudcnt, "Text");
                                            int colcnt = 3;
                                            DataView dvstcnt = new DataView();
                                            DataView dvstpscnt = new DataView();

                                            Boolean studcnt = false;
                                            string mode = "1";
                                            if (CkLGender.Items.Count > 0)
                                            {
                                                for (int st = 0; st < 3; st++)
                                                {
                                                    string stype = dicstudtype[st];
                                                    if (st == 2)
                                                        mode = "3";
                                                    if (cblsearch.Items[st + 3].Selected == true)
                                                    {

                                                        for (int gen = 0; gen < CkLGender.Items.Count; gen++)
                                                        {
                                                            if (CkLGender.Items[gen].Selected == true)
                                                            {
                                                                //Strength
                                                                dsstdcnt.Tables[0].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and mode='" + mode + "'";
                                                                dvstcnt = dsstdcnt.Tables[0].DefaultView;
                                                                //PassCnt
                                                                dsstdpsct.Tables[0].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and mode='" + mode + "'";
                                                                dvstpscnt = dsstdpsct.Tables[0].DefaultView;



                                                                colcnt++;
                                                                string colname = data.Columns[colcnt].ColumnName;
                                                                if (dvstcnt.Count > 0)
                                                                {
                                                                    drow[colname] = dvstcnt[0]["PRESENT_COUNT"].ToString();
                                                                    gtotstu = gtotstu + int.Parse(dvstcnt[0]["PRESENT_COUNT"].ToString());
                                                                }
                                                                else
                                                                    drow[colname] = "-";
                                                                colcnt++;
                                                                string colname1 = data.Columns[colcnt].ColumnName;
                                                                if (dvstpscnt.Count > 0)
                                                                {
                                                                    drow[colname1] = dvstpscnt[0]["PASS_COUNT"].ToString();
                                                                    gpassstud = gpassstud + int.Parse(dvstpscnt[0]["PASS_COUNT"].ToString());
                                                                }
                                                                else
                                                                    drow[colname1] = "-";
                                                            }
                                                        }


                                                    }
                                                }
                                                if (rbappear.Checked == false)
                                                    drow["No. Strength"] = gtotstu.ToString();
                                                else
                                                    drow["No. Appeared"] = gtotstu.ToString();

                                                totalAppear += gtotstu;
                                                overallAppear += gtotstu;


                                                totalPass += gpassstud;
                                                overallPass += gpassstud;
                                                if (gpassstud.ToString() != "")
                                                {
                                                    totalcheck++;
                                                }
                                                getappear = getappear + gtotstu;
                                                getpass = getpass + gpassstud;

                                            }



                                            if (string.IsNullOrEmpty(Exam_code_val))
                                            {
                                                Exam_code_val = "'" + Convert.ToString(ds_exam.Tables[0].Rows[0]["exam_code"]) + "'";
                                            }
                                            else
                                            {
                                                Exam_code_val += ",'" + Convert.ToString(ds_exam.Tables[0].Rows[0]["exam_code"]) + "'";
                                            }
                                            drow["No. passed"] = gpassstud.ToString();


                                            double jh1 = Convert.ToDouble(gpassstud);
                                            double hf1 = Convert.ToDouble(gtotstu);
                                            double aclass1 = Convert.ToDouble(jh1 / hf1) * 100;
                                            aclass_perc1 = 0;
                                            aclass_perc1 = Math.Round(aclass1, 2);
                                            if (aclass_perc1.ToString() == "NaN" || aclass_perc1.ToString() == "Infinity")
                                            {
                                                aclass_perc1 = 0;
                                            }
                                            string ddval = aclass_perc1.ToString();
                                            string[] spval = ddval.Split(new char[] { '.' });
                                            if (spval.GetUpperBound(0) == 1)
                                            {
                                                int dec = spval[1].Length;
                                                if (dec == 1)
                                                {
                                                    ddval = spval[0] + "." + spval[1] + "0";
                                                }
                                                else if (spval[1] == "00")
                                                {
                                                    ddval = spval[0] + ".00";
                                                }
                                                else
                                                {
                                                    ddval = spval[0] + "." + spval[1];
                                                }
                                            }
                                            else
                                            {
                                                ddval = spval[0] + ".00";
                                            }
                                            drow["Sectionwise Pass %"] = ddval.ToString();


                                            overallpercentage += Convert.ToDouble(ddval);
                                            count++;
                                            passstu1 = passstu1 + aclass_perc1;

                                        }
                                        data.Rows.Add(drow);
                                    }
                                }

                            }

                            int spreadbindrow = start_Row;
                            for (int spanrow = 0; spanrow < Row_Span_Value; spanrow++)
                            {
                                if (start_Row == 0)
                                {

                                    data.Rows[spreadbindrow]["Appeared"] = overallAppear.ToString();
                                    data.Rows[spreadbindrow]["Pass"] = overallPass.ToString();
                                    data.Rows[spreadbindrow]["Pass %"] = Math.Round((overallpercentage / count), 2, MidpointRounding.AwayFromZero).ToString();

                                }
                                else
                                {
                                    data.Rows[spreadbindrow]["Appeared"] = overallAppear.ToString();
                                    data.Rows[spreadbindrow]["Pass"] = overallPass.ToString();
                                    data.Rows[spreadbindrow]["Pass %"] = Math.Round((overallpercentage / count), 2, MidpointRounding.AwayFromZero).ToString();
                                }

                                spreadbindrow++;
                            }

                            overallAppear = 0;
                            overallPass = 0;
                            count = 0;
                            overallpercentage = 0;
                            Row_Span_Value = 0;
                        }
                        int rowcount = 0;
                        #region   to get the bottom content

                        recflag = false;
                        if (ddlDegree.Text == "")
                        {
                            return;
                        }
                        tot_stud_str = 0;
                        nofopass_str = 0;
                        noofperc = 0;
                        strsec = string.Empty;
                        secvar = string.Empty;
                        sec_str = string.Empty;
                        aclass_perc1 = 0;
                        //decimal aclass_perc1 = 0;
                        totvl = 0;
                        lblnorec.Visible = false;
                        if (rdexternal.Checked == false && rdinternal.Checked == false)
                        {
                            lblnorec.Text = "Kindly select Report";
                            lblnorec.Visible = true;
                        }
                        else
                        {
                            if (rdexternal.Checked == false && rdinternal.Checked == true)
                            {
                                if (ddlTest.Items.Count > 0)
                                {
                                    if (ddlTest.SelectedItem.ToString() == "--Select--")
                                    {
                                        lblnorec.Text = "Please Select The Test";
                                        lblnorec.Visible = true;
                                        return;
                                    }
                                }
                                //Added By Mlang Raja on  Feb 7 2017
                                //else
                                //{
                                //    lblnorec.Text = "Test is Not Conducted";
                                //    lblnorec.Visible = true;
                                //    return;
                                //}
                            }
                            lblnorec.Visible = false;
                            lblnorec.Text = string.Empty;
                            if (rdexternal.Checked == true)
                            {
                                Lbl_Gender.Visible = false;
                                UpdatePanelGender.Visible = false;
                                lblTest.Visible = false;
                                ddlTest.Visible = false;
                                Columnorder.Visible = false;
                            }
                            else
                            {
                                Lbl_Gender.Visible = true;
                                UpdatePanelGender.Visible = true;
                                lblTest.Visible = true;
                                ddlTest.Visible = true;
                                Columnorder.Visible = true;
                            }

                            yr = 0;
                            sem_new = 0;
                            sem_fun = string.Empty;
                            exam_month_fun = ""; exam_year_fun = string.Empty;
                            subjects_fun = string.Empty;
                            examcode_fun = 0;
                            tot_stu = 0; tot_stu1 = 0; allpascnt = 0;
                            no_of_passA = 0; no_of_passB = 0;
                            batch_year = ddlBatch.SelectedValue.ToString();
                            degree = ddlDegree.SelectedValue.ToString();
                            degree_code = ddlBranch.SelectedValue.ToString();
                            semesterddl = ddlSemYr.SelectedValue.ToString();
                            sections = ddlSec.SelectedValue.ToString();
                            exam_year = ddlYear.SelectedValue.ToString();
                            exam_month = ddlMonth.SelectedValue.ToString();
                            exam_code = 0;// Convert.ToInt32(GetFunction("select exam_code from exam_details where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + ""));
                            for (int seccnt = 0; seccnt < ddlSec.Items.Count; seccnt++)
                            {
                                if (sec_str == "")
                                {
                                    sec_str = ddlSec.Items[seccnt].ToString();
                                }
                                else
                                {
                                    sec_str = sec_str + "," + ddlSec.Items[seccnt].ToString();
                                }
                            }

                            tot_sem = 0;
                            yr = 0;
                            con.Close();
                            cmd = new SqlCommand("select ndurations from ndegree where batch_year=" + ddlBatch.SelectedValue + "  and degree_code=" + ddlBranch.SelectedValue + "", con);

                            con.Open();
                            no_on_sem_dr = cmd.ExecuteReader();
                            if (no_on_sem_dr.HasRows)
                            {
                                while (no_on_sem_dr.Read())
                                {
                                    tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
                                    yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
                                }
                            }
                            else
                            {
                                cmd = new SqlCommand("select duration from degree where degree_code=" + ddlBranch.SelectedValue + "", con);
                                con.Close();
                                con.Open();
                                no_on_sem_dr = cmd.ExecuteReader();
                                if (no_on_sem_dr.HasRows)
                                {
                                    while (no_on_sem_dr.Read())
                                    {
                                        tot_sem = Convert.ToInt32(no_on_sem_dr[0].ToString());
                                        yr = Convert.ToInt32(ddlBatch.SelectedValue.ToString()) + (tot_sem / 2);
                                    }
                                }
                            }
                            //-----------------------------------------------------------

                            passstu1 = 0;
                            //decimal  passstu1 = 0;
                            exam_month_fun = ddlMonth.SelectedValue.ToString();
                            exam_year_fun = ddlYear.SelectedValue.ToString();
                            sem_new = Convert.ToInt32(ddlSemYr.SelectedValue.ToString());
                            sem_fun = GetSemester_AsNumber(Convert.ToInt32(sem_new)).ToString();
                            examcode_fun = int.Parse(d2.GetFunction("select distinct exam_code from exam_details where degree_code='" + degree_code + "' and batch_year=" + batch_year + " and exam_month=" + exam_month_fun + " and exam_year=" + exam_year_fun + ""));
                            Session["examcode"] = examcode_fun;
                            criteria_no = ddlTest.SelectedValue.ToString();
                            in_examcode = string.Empty;
                            if (rdexternal.Checked == true)
                            {
                                has.Clear();
                                has.Add("sem_fun", sem_fun);
                                has.Add("degree_code", degree_code);
                                has.Add("batch_year", batch_year);
                                has.Add("examcode_fun", examcode_fun);
                                ds_has = d2.select_method("get_subject", has, "sp");
                            }
                            else if (rdinternal.Checked == true) //from [PROC_STUD_ALL_SUBMARK]
                            {
                                if (ddlTest.Items.Count == 0)
                                {
                                    lblnorec.Visible = true;
                                    lblnorec.Text = "Kindly select Test";
                                    Showgrid.Visible = false;
                                    lblrptname.Visible = false;
                                    txtexcelname.Visible = false;
                                    btnxl.Visible = false;
                                    btnmasterprint.Visible = false;
                                    btn_dirtprint.Visible = false;
                                    return;
                                }
                                else
                                {
                                    lblnorec.Text = string.Empty;
                                    lblnorec.Visible = false;
                                    Showgrid.Visible = true;
                                    lblrptname.Visible = true;
                                    txtexcelname.Visible = true;
                                    btnxl.Visible = true;
                                    btnmasterprint.Visible = true;
                                    btn_dirtprint.Visible = true;
                                    // string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " order by s.subject_no ";
                                    string test_subj = "select distinct s.subject_no,s.subject_name,s.acronym,s.subject_code from subject s,exam_type e where e.subject_no=s.subject_no and e.criteria_no=" + criteria_no + " order by s.subject_no ";
                                    ds_has.Dispose();
                                    ds_has = d2.select_method_wo_parameter(test_subj, "Text");
                                }
                            }
                            //======================================
                            tmpvar = string.Empty;
                            dsv = ddlSec.Items.Count;
                            if (dsv == 0)
                                dsv = 1;
                            if (ds_has.Tables.Count > 0 && 0 < ds_has.Tables[0].Rows.Count)
                            {
                                sl_no = 1;
                                spancolumns = 0;
                                rowsp = 0;
                                overallAppear = 0;
                                overallPass = 0;
                                double overallSubjects = 0;
                                string mode = "1";
                                for (int i = 0; i < ds_has.Tables[0].Rows.Count; i++)
                                {
                                    spancolumns = 0;
                                    if (i == 0)
                                    {
                                        sl_no = 1;
                                        //rowsp = FpExternal.Sheets[0].RowCount;
                                    }
                                    else
                                    {
                                        if (Convert.ToString(ds_has.Tables[0].Rows[i]["subject_name"]) != Convert.ToString(ds_has.Tables[0].Rows[i - 1]["subject_name"]))
                                        {
                                            sl_no++;
                                            // rowsp = FpExternal.Sheets[0].RowCount;
                                        }
                                    }

                                    //New

                                    double totalAppear = 0;
                                    double totalPass = 0;
                                    //New END
                                    Double getappear = 0;
                                    Double getpass = 0;
                                    passstu1 = 0;
                                    int totalcheck = 0;
                                    overallSubjects++;
                                    for (int sec_temp = 0; sec_temp < dsv; sec_temp++)
                                    {
                                        if ((ddlSec.Items.Count > 1) && (ddlSec.Items[sec_temp].ToString() == "All" || ddlSec.Items[sec_temp].ToString() == string.Empty || ddlSec.Items[sec_temp].ToString() == "-1"))
                                        {
                                            strsec = string.Empty;
                                            secvar = string.Empty;
                                        }
                                        else
                                        {
                                            if (ddlSec.Items.Count > 0)
                                            {
                                                strsec = " and sections='" + ddlSec.Items[sec_temp].ToString() + "'";
                                                secvar = ddlSec.Items[sec_temp].ToString();
                                            }
                                        }
                                        if (rdinternal.Checked == true) //20.07.12 mythili
                                        {
                                            //========query for getting the exam_code,min,max,staffcode,duration,exam,entrydate for a particular subj for a particular sec
                                            ds_exam1.Tables[0].DefaultView.RowFilter = "criteria_no=" + criteria_no + " and subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " " + strsec + "";
                                            DataTable dtexam = ds_exam1.Tables[0].DefaultView.ToTable();
                                            DataSet ds_exam = new DataSet();
                                            ds_exam.Tables.Add(dtexam);


                                            //string strexam = "select distinct staff_code,duration,convert(varchar(10),exam_date,103)as exam_date,convert(varchar(10),entry_date,103)as entry_date,max_mark,min_mark,r.exam_code,s.subject_no,e.sections from subject s,exam_type e,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + criteria_no + " and s.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " " + strsec + "";
                                            //DataSet ds_exam = d2.select_method_wo_parameter(strexam, "text");
                                            //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 2].Text = secvar;
                                            //===============================================
                                            if (ds_exam.Tables.Count > 0 && ds_exam.Tables[0].Rows.Count > 0)
                                            {
                                                recflag = true;
                                                if (in_examcode == "")
                                                {
                                                    in_examcode = ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
                                                }
                                                else
                                                {
                                                    in_examcode = in_examcode + "," + ds_exam.Tables[0].Rows[0]["exam_code"].ToString();
                                                }
                                                string sect = ds_exam.Tables[0].Rows[0]["sections"].ToString();
                                                string sectval = string.Empty;
                                                string sectionval = string.Empty;// Added by madhumathi
                                                if (sect.Trim() != "" && sect.Trim() != "-1" && sect.Trim() != null)
                                                {
                                                    sectval = " and rt.sections='" + sect + "'";
                                                    sectionval = " and reg.sections = '" + sect + "'";// Added by madhumathi
                                                }
                                                else
                                                {
                                                    sectval = string.Empty;
                                                    sect = string.Empty;
                                                    sectionval = string.Empty;// Added by madhumathi
                                                }
                                                string strincludePassedout = string.Empty;
                                                string includePassedout = string.Empty;
                                                if (!chkincludepastout.Checked)
                                                {
                                                    strincludePassedout = "and rt.cc=0";
                                                    includePassedout = "and reg.cc=0";

                                                }
                                                //Modified By Srinath 2/4/2013
                                                //string totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt where r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3') and r.roll_no=rt.roll_no and rt.cc=0 and rt.exam_flag <>'DEBAR' and rt.delflag=0 and rt.RollNo_Flag<>0 ";
                                                //string totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt,exam_type e,subjectchooser sc where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no  and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + "";
                                                //if (rbappear.Checked == false)
                                                //{
                                                //    totstu = "select count(rt.roll_no) as 'PRESENT_COUNT' from registration rt,subjectchooser sc where rt.roll_no=sc.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + "";
                                                //}
                                                //int gtotstu = int.Parse(d2.GetFunction(totstu));

                                                //totalAppear += gtotstu;
                                                //overallAppear += gtotstu;

                                                string totstu = "";
                                                string totstudcnt = "";
                                                int gtotstu = 0;
                                                int gpassstud = 0;
                                                DataSet dsstdcnt = new DataSet();
                                                if (rbappear.Checked == true)
                                                {
                                                    totstu = "select count(marks_obtained) as 'PRESENT_COUNT' from result r,registration rt,exam_type e,subjectchooser sc where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no  and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + " ";


                                                    totstudcnt = "select count(marks_obtained) as 'PRESENT_COUNT',rt.Stud_Type,al.sex,rt.mode from result r,registration rt,exam_type e,subjectchooser sc,Applyn al where r.roll_no=sc.roll_no and e.subject_no=sc.subject_no and r.exam_code=e.exam_code and r.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3' ) and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0  and r.roll_no=rt.roll_no and al.app_no=rt.App_No  and rt.RollNo_Flag<>0  and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' " + sectval + " group by rt.Stud_Type ,al.sex,rt.mode";
                                                    dsstdcnt.Clear();
                                                    dsstdcnt = d2.select_method_wo_parameter(totstudcnt, "Text");
                                                }
                                                else
                                                {
                                                    totstu = "select count(rt.roll_no) as 'PRESENT_COUNT' from registration rt,subjectchooser sc where rt.roll_no=sc.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and  sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + " ";
                                                    totstudcnt = "select count(rt.roll_no) as 'PRESENT_COUNT',rt.Stud_Type,al.sex,rt.mode  from registration rt,subjectchooser sc,Applyn al where rt.roll_no=sc.roll_no and al.app_no=rt.App_No and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.batch_year='" + ddlBatch.SelectedItem.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and  sc.subject_no='" + ds_exam.Tables[0].Rows[0]["subject_no"] + "' " + sectval + " group by rt.Stud_Type,al.sex,rt.mode";
                                                    dsstdcnt.Clear();
                                                    dsstdcnt = d2.select_method_wo_parameter(totstudcnt, "Text");
                                                }
                                                if (!colvis)
                                                {
                                                    gtotstu = int.Parse(d2.GetFunction(totstu));
                                                    if (rbappear.Checked == false)
                                                        drow["No. Strength"] = gtotstu.ToString();
                                                    else
                                                        drow["No. Appeared"] = gtotstu.ToString();

                                                    totalAppear += gtotstu;
                                                    overallAppear += gtotstu;
                                                }

                                                string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result r,registration reg,exam_type e,subjectchooser sc where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + " " + sectionval + " and reg.RollNo_Flag<>0 ";
                                                if (!colvis)
                                                {
                                                    gpassstud = int.Parse(d2.GetFunction(passstud));

                                                    totalPass += gpassstud;
                                                    overallPass += gpassstud;
                                                    if (gpassstud.ToString() != "")
                                                    {
                                                        totalcheck++;
                                                    }
                                                    getappear = getappear + gtotstu;
                                                    getpass = getpass + gpassstud;
                                                }

                                                string passstudcnt = "select count(marks_obtained) as 'PASS_COUNT',reg.Stud_Type,al.sex,reg.mode from result r,registration reg,exam_type e,subjectchooser sc,Applyn al where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no  and al.app_no=reg.App_No and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + " " + sectionval + " and reg.RollNo_Flag<>0   group by reg.Stud_Type ,al.sex,reg.mode ";
                                                DataSet dsstdpsct = new DataSet();
                                                dsstdpsct = d2.select_method_wo_parameter(passstudcnt, "Text");
                                                int colcnt = 3;
                                                DataView dvstcnt = new DataView();
                                                DataView dvstpscnt = new DataView();


                                                if (CkLGender.Items.Count > 0)
                                                {
                                                    mode = "1";
                                                    for (int st = 0; st < 3; st++)
                                                    {
                                                        if (st == 2)
                                                            mode = "3";
                                                        string stype = dicstudtype[st];
                                                        if (cblsearch.Items[st + 3].Selected == true)
                                                        {

                                                            for (int gen = 0; gen < CkLGender.Items.Count; gen++)
                                                            {
                                                                if (CkLGender.Items[gen].Selected == true)
                                                                {
                                                                    //Strength
                                                                    dsstdcnt.Tables[0].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and  mode='" + mode + "' ";
                                                                    dvstcnt = dsstdcnt.Tables[0].DefaultView;
                                                                    //PassCnt
                                                                    dsstdpsct.Tables[0].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and mode='" + mode + "' ";
                                                                    dvstpscnt = dsstdpsct.Tables[0].DefaultView;

                                                                    if (dvstcnt.Count > 0)
                                                                        gtotstu = gtotstu + int.Parse(dvstcnt[0]["PRESENT_COUNT"].ToString());
                                                                    if (dvstpscnt.Count > 0)
                                                                        gpassstud = gpassstud + int.Parse(dvstpscnt[0]["PASS_COUNT"].ToString());

                                                                }
                                                            }




                                                        }
                                                    }
                                                    totalAppear += gtotstu;
                                                    overallAppear += gtotstu;


                                                    totalPass += gpassstud;
                                                    overallPass += gpassstud;
                                                    if (gpassstud.ToString() != "")
                                                    {
                                                        totalcheck++;
                                                    }
                                                    getappear = getappear + gtotstu;
                                                    getpass = getpass + gpassstud;

                                                }

                                                //string passstud = "select count(marks_obtained) as 'PASS_COUNT' from result r,registration reg,exam_type e,subjectchooser sc where e.subject_no=sc.subject_no and r.roll_no=sc.roll_no and r.exam_code=e.exam_code and e.exam_code=" + ds_exam.Tables[0].Rows[0]["exam_code"] + " and (marks_obtained>=" + ds_exam.Tables[0].Rows[0]["min_mark"] + " or marks_obtained='-3' or marks_obtained='-2') and reg.roll_no=r.roll_no and reg.delflag=0 and reg.exam_flag<>'debar' " + includePassedout + " " + sectionval + " and reg.RollNo_Flag<>0 ";
                                                // gpassstud = int.Parse(d2.GetFunction(passstud));

                                                //totalPass += gpassstud;
                                                //overallPass += gpassstud;

                                                //getappear = getappear + gtotstu;
                                                //getpass = getpass + gpassstud;
                                                double jh1 = Convert.ToDouble(gpassstud);
                                                double hf1 = Convert.ToDouble(gtotstu);
                                                double aclass1 = Convert.ToDouble(jh1 / hf1) * 100;
                                                aclass_perc1 = 0;
                                                aclass_perc1 = Math.Round(aclass1, 2);
                                                if (aclass_perc1.ToString() == "NaN" || aclass_perc1.ToString() == "Infinity")
                                                {
                                                    aclass_perc1 = 0;
                                                }
                                                string ddval = aclass_perc1.ToString();
                                                string[] spval = ddval.Split(new char[] { '.' });
                                                if (spval.GetUpperBound(0) == 1)
                                                {
                                                    int dec = spval[1].Length;
                                                    if (dec == 1)
                                                    {
                                                        ddval = spval[0] + "." + spval[1] + "0";
                                                    }
                                                    else if (spval[1] == "00")
                                                    {
                                                        ddval = spval[0] + ".00";
                                                    }
                                                    else
                                                    {
                                                        ddval = spval[0] + "." + spval[1];
                                                    }
                                                }
                                                else
                                                {
                                                    ddval = spval[0] + ".00";
                                                }

                                                passstu1 = passstu1 + aclass_perc1;
                                            }
                                        }

                                        #region External
                                        //else if (rdexternal.Checked == true)
                                        //{
                                        //    recflag = true;
                                        //    //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 2].Text = secvar;

                                        //    #region existing which shows wrong value on elective paper since it does not matches the subject chooser

                                        //    //string ssd = "select count(distinct m.roll_no) from mark_entry m,registration r where m.roll_no=r.roll_no and r.delflag<>1 and m.attempts = 1  and subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " and (result='pass' or result='fail' or result='S') and m.exam_code = " + examcode_fun + "  and degree_code=" + degree_code + " and batch_year=" + batch_year + " " + strsec + ""; 

                                        //    #endregion

                                        //    //added by prabha  on jan 18 2018
                                        //    //no of students elective subject chooser  differed with no of students mark entered
                                        //    #region matching qry with subject chooser

                                        //    string ssd = "select count(distinct m.roll_no) from mark_entry m,registration r,subjectChooser sc where m.roll_no=r.roll_no and r.delflag<>1 and m.attempts = 1 and sc.subject_no=m.subject_no and m.roll_no=sc.roll_no  and m.subject_no=" + ds_has.Tables[0].Rows[i]["subject_no"] + " and (result='pass' or result='fail' or result='S') and m.exam_code = " + examcode_fun + "  and degree_code=" + degree_code + " and batch_year=" + batch_year + " " + strsec + "";

                                        //    #endregion

                                        //    // select count(ea.roll_no) from exam_application ea,exam_appl_details ead,registration r where ea.appl_no=ead.appl_no and subject_no= " + ds_has.Tables[0].Rows[i]["subject_no"] + " and degree_code=" + degree_code + "  and batch_year=" + batch_year + "  " + strsec + " and ea.roll_no=r.roll_no and exam_code=" + examcode_fun + "";
                                        //    tot_stu = int.Parse(d2.GetFunction(ssd));
                                        //    //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 4].Text = tot_stu.ToString();
                                        //    string pm = "select count(distinct m.roll_no) from mark_entry m,registration r where m.roll_no=r.roll_no  " + strsec + "  and m.result = 'Pass' and  subject_no =  " + ds_has.Tables[0].Rows[i]["subject_no"] + " and r.delflag<>1 and m.attempts = 1 and ltrim(rtrim(type))='' and m.exam_code=" + examcode_fun + "";
                                        //    int passstu = int.Parse(d2.GetFunction(pm));
                                        //    //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = passstu.ToString();
                                        //    no_of_passA = no_of_passA + passstu;
                                        //    getappear = getappear + tot_stu;
                                        //    getpass = getpass + passstu;
                                        //    double jh = Convert.ToDouble(passstu);
                                        //    double hf = Convert.ToDouble(tot_stu);
                                        //    double aclass = Convert.ToDouble((jh * 100) / hf);
                                        //    double aclass_perc = Math.Round(aclass, 2);
                                        //    //decimal aclass_perc =Convert.ToDecimal(Math.Round(aclass, 2));
                                        //    if (aclass_perc.ToString() == "NaN" || aclass_perc.ToString() == "Infinity")
                                        //    {
                                        //        aclass_perc = 0;
                                        //    }
                                        //    string ddval = aclass_perc.ToString();
                                        //    string[] spval = ddval.Split(new char[] { '.' });
                                        //    if (spval.GetUpperBound(0) == 1)
                                        //    {
                                        //        int dec = spval[1].Length;
                                        //        if (dec == 1)
                                        //        {
                                        //            ddval = spval[0] + "." + spval[1] + "0";
                                        //        }
                                        //        else if (spval[1] == "00")
                                        //        {
                                        //            ddval = spval[0] + ".00";
                                        //        }
                                        //        else
                                        //        {
                                        //            ddval = spval[0] + "." + spval[1];
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        ddval = spval[0] + ".00";
                                        //    }
                                        //    // FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 5].Text = aclass_perc.ToString();
                                        //    //FpExternal.Sheets[0].Cells[(FpExternal.Sheets[0].RowCount - dsv) + sec_temp, 6].Text = ddval.ToString();

                                        //    passstu1 = passstu1 + aclass_perc;
                                        //}
                                        #endregion
                                    }
                                    double tot_secwise_per = 0;
                                    //decimal tot_secwise_per = 0;
                                    double math_tot = 0;
                                    // decimal math_tot = 0;
                                    if (Convert.ToInt16(ddlSec.Items.Count) >= 2)
                                    {
                                        if (rdinternal.Checked)
                                        {
                                            // tot_secwise_per = passstu1 / Convert.ToInt16(totalcheck);
                                            tot_secwise_per = getpass / getappear * 100;
                                            math_tot = Math.Round(tot_secwise_per, 2);
                                            totalcheck = 0;
                                            getappear = 0;
                                            getpass = 0;
                                        }
                                        else
                                        {
                                            ///tot_secwise_per = passstu1 / Convert.ToInt16(ddlSec.Items.Count);
                                            tot_secwise_per = getpass / getappear * 100;
                                            math_tot = Math.Round(tot_secwise_per, 2);
                                            getappear = 0;
                                            getpass = 0;
                                        }
                                    }
                                    else
                                    {
                                        math_tot = passstu1;
                                    }
                                    if (math_tot.ToString() == "NaN" || math_tot.ToString() == "Infinity")
                                    {
                                        math_tot = 0;
                                    }
                                    for (int seccount = 0; seccount < dsv; seccount++)
                                    {
                                        string ddval = math_tot.ToString();
                                        string[] spval = ddval.Split(new char[] { '.' });
                                        if (spval.GetUpperBound(0) == 1)
                                        {
                                            int dec = spval[1].Length;
                                            if (dec == 1)
                                            {
                                                ddval = spval[0] + "." + spval[1] + "0";
                                            }
                                            else if (spval[1] == "00")
                                            {
                                                ddval = spval[0] + ".00";
                                            }
                                            else
                                            {
                                                ddval = spval[0] + "." + spval[1];
                                            }
                                        }
                                        else
                                        {
                                            ddval = spval[0] + ".00";
                                        }

                                    }
                                }
                                rowcount = 0;
                                if (data.Rows.Count > 0)
                                {
                                    if (sl_no > 1)
                                    {
                                        drow = data.NewRow();
                                        drow["SNo"] = "";
                                        data.Rows.Add(drow);
                                        rowcount = data.Rows.Count;
                                        dicrowspan.Add(rowcount - 1, "");
                                    }
                                    if (overallSubjects != 0)
                                    {
                                        overallAppear /= overallSubjects;
                                        overallAppear = Math.Round(overallAppear, 2);
                                        overallPass /= overallSubjects;
                                        overallPass = Math.Round(overallPass, 2);
                                    }
                                    if (rdinternal.Checked == true)
                                    {
                                        //Saran
                                        drow = data.NewRow();
                                        drow["Sno"] = "OVERALL";
                                        data.Rows.Add(drow);

                                        rowcount = data.Rows.Count;
                                        dicrowspan.Add(rowcount - 1, "OVERALL");
                                    }
                                }
                                //======================20.07.12
                                if (in_examcode != "")
                                {
                                    in_examcode = "in(" + in_examcode + ")";//all examcode
                                }
                                //==================20.07.12
                                double tot_per_all_pass = 0;
                                int tot_stud_overall = 0;
                                double test_minmark = 0;
                                double secper = 0;
                                int spanrow = data.Rows.Count;

                                for (int sec_temp = 0; sec_temp < dsv; sec_temp++)
                                {
                                    tot_stu = 0;
                                    allpascnt = 0;
                                    spanrow++;
                                    if ((ddlSec.Items.Count > 1) && (ddlSec.Items[sec_temp].ToString() == "All" || ddlSec.Items[sec_temp].ToString() == string.Empty || ddlSec.Items[sec_temp].ToString() == "-1"))
                                    {
                                        strsec = string.Empty;
                                        secvar = string.Empty;
                                    }
                                    else
                                    {
                                        if (ddlSec.Items.Count > 0)
                                        {
                                            strsec = " and sections='" + ddlSec.Items[sec_temp].ToString() + "'";
                                            secvar = ddlSec.Items[sec_temp].ToString();
                                        }
                                    }

                                    string in_sec_examcode = string.Empty;
                                    if (rdinternal.Checked == true)
                                    {
                                        //string sec_examcode = "select distinct r.exam_code as exam_code from exam_type e,subject s,result r where e.subject_no=s.subject_no and e.exam_code= r.exam_code and criteria_no=" + ddlTest.SelectedValue.ToString() + "  " + strsec + "  ";
                                        string sec_examcode = "select distinct e.exam_code as exam_code from exam_type e,subject s where e.subject_no=s.subject_no and e.criteria_no=" + ddlTest.SelectedValue.ToString() + "  " + strsec + "  ";
                                        DataSet ds_sec_exmcode = d2.select_method_wo_parameter(sec_examcode, "text");
                                        if (ds_sec_exmcode.Tables.Count > 0 && ds_sec_exmcode.Tables[0].Rows.Count > 0)
                                        {
                                            for (int scexm = 0; scexm < ds_sec_exmcode.Tables[0].Rows.Count; scexm++)
                                            {
                                                if (in_sec_examcode == "")
                                                {
                                                    in_sec_examcode = ds_sec_exmcode.Tables[0].Rows[scexm]["exam_code"].ToString();
                                                }
                                                else
                                                {
                                                    in_sec_examcode = in_sec_examcode + "," + ds_sec_exmcode.Tables[0].Rows[scexm]["exam_code"].ToString();
                                                }
                                            }
                                        }
                                        if (in_sec_examcode != "")
                                        {
                                            in_sec_examcode = "in(" + in_sec_examcode + ")";
                                        }
                                    }
                                    string strincludePassedout = string.Empty;
                                    string includePassedout = string.Empty;
                                    if (!chkincludepastout.Checked)
                                    {
                                        strincludePassedout = "and rt.cc=0";
                                        includePassedout = "and r.cc=0";

                                    }
                                    string ssd = "", ssd1 = string.Empty;
                                    if (rdexternal.Checked == true)
                                    {
                                        ssd = "select count(*) from registration where degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + "  " + strsec + "";
                                        //ssd1 = "select COUNT(distinct m.roll_no) as Attended, r.degree_code from mark_entry m,Exam_Details e,Registration r where  e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' " + strsec + " and r.cc=0 and  r.exam_flag <>'DEBAR' and r.delflag=0 and m.attempts=1 and m.roll_no not in (select distinct r.roll_no from mark_entry m,Exam_Details e,Registration r where e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' and r.cc=0 and  r.exam_flag <>'DEBAR' and r.delflag=0 " + strsec + " and result='AAA' and m.attempts=1)  group by r.degree_code";
                                        ssd1 = "select COUNT(distinct m.roll_no) as Attended, r.degree_code from mark_entry m,Exam_Details e,Registration r where  e.exam_code=m.exam_code and m.roll_no=r.Roll_No and e.batch_year=r.Batch_Year and r.degree_code=e.degree_code and r.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and e.current_semester='" + ddlSemYr.SelectedValue.ToString() + "' and r.degree_code='" + ddlBranch.SelectedValue + "' " + strsec + " and  r.exam_flag <>'DEBAR' and r.delflag=0 and m.attempts=1 and m.result<>'AAA' and m.result<>'WHD' group by r.degree_code";
                                    }
                                    else if ((rdinternal.Checked == true) && (in_sec_examcode.ToString() != "")) //no of students appeared based on pass all examcode
                                    {
                                        if (!colvis)
                                        {
                                            ssd = "select isnull(count(distinct rt.roll_no),0) as 'allpass_count' from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3'or marks_obtained='-1')  and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strsec + " ";

                                            ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear' from result r,registration rt where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3')  and r.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strsec + " ";
                                            if (rbappear.Checked == false)
                                            {
                                                ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear' from subjectChooser sc,registration rt where sc.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and rt.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.semester='" + ddlSemYr.SelectedValue.ToString() + "' " + strsec + "";
                                            }

                                            if (ssd.ToString().Trim() != "")
                                                allpascnt = int.Parse(d2.GetFunction(ssd));
                                            if (ssd1.ToString().Trim() != "")
                                                tot_stu = int.Parse(d2.GetFunction(ssd1));
                                            tot_stud_overall = tot_stud_overall + tot_stu;
                                        }
                                        else
                                        {
                                            DataSet dsstd = new DataSet();
                                            DataSet dsstd1 = new DataSet();
                                            ssd = "select isnull(count(distinct rt.roll_no),0) as 'allpass_count',rt.Stud_Type,al.sex,rt.mode from result r,registration rt,Applyn al  where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3'or marks_obtained='-1')  and r.roll_no=rt.roll_no and al.app_no=rt.App_No and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strsec + "  group by rt.Stud_Type,al.sex,rt.mode ";
                                            dsstd.Clear();
                                            dsstd = d2.select_method_wo_parameter(ssd, "Text");

                                            ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear',rt.Stud_Type,al.sex,rt.mode from result r,registration rt,Applyn al  where r.exam_code " + in_sec_examcode.ToString() + "  and (marks_obtained>=0 or marks_obtained='-2' or marks_obtained='-3')  and r.roll_no=rt.roll_no and al.app_no=rt.App_No and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 " + strsec + "  group by rt.Stud_Type,al.sex,rt.mode ";
                                            if (rbappear.Checked == false)
                                            {
                                                ssd1 = "select isnull(count(distinct rt.roll_no),0) as 'appear',rt.Stud_Type,al.sex,rt.mode from subjectChooser sc,registration rt,Applyn al where sc.roll_no=rt.roll_no and rt.exam_flag <>'DEBAR' and rt.delflag=0 " + strincludePassedout + " and rt.RollNo_Flag<>0 and al.app_no=rt.App_No and rt.Batch_Year='" + ddlBatch.SelectedValue.ToString() + "' and rt.degree_code='" + ddlBranch.SelectedValue.ToString() + "' and sc.semester='" + ddlSemYr.SelectedValue.ToString() + "' " + strsec + "  group by rt.Stud_Type,al.sex,rt.mode";
                                            }
                                            dsstd1.Clear();
                                            dsstd1 = d2.select_method_wo_parameter(ssd1, "Text");

                                            DataView dvstcn = new DataView();
                                            DataView dvstpscn = new DataView();
                                            int gtotst = 0;
                                            int gpassstd = 0;

                                            if (CkLGender.Items.Count > 0)
                                            {
                                                mode = "1";
                                                for (int st = 0; st < 3; st++)
                                                {
                                                    if (st == 2)
                                                        mode = "3";
                                                    string stype = dicstudtype[st];
                                                    if (cblsearch.Items[st + 3].Selected == true)
                                                    {

                                                        for (int gen = 0; gen < CkLGender.Items.Count; gen++)
                                                        {
                                                            if (CkLGender.Items[gen].Selected == true)
                                                            {
                                                                //Strength
                                                                dsstd.Tables[0].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and mode='" + mode + "' ";
                                                                dvstcn = dsstd.Tables[0].DefaultView;
                                                                //PassCnt
                                                                dsstd1.Tables[0].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and mode='" + mode + "' ";
                                                                dvstpscn = dsstd1.Tables[0].DefaultView;

                                                                if (dvstcn.Count > 0)
                                                                    allpascnt = allpascnt + int.Parse(dvstcn[0]["allpass_count"].ToString());
                                                                if (dvstpscn.Count > 0)
                                                                    tot_stu = tot_stu + int.Parse(dvstpscn[0]["appear"].ToString());
                                                                tot_stud_overall = tot_stud_overall + tot_stu;

                                                            }
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }
                                    drow = data.NewRow();

                                    tot_stud_str = tot_stud_str + Convert.ToInt32(tot_stu);
                                    //---------------------find tot no of student pass in all subject
                                    double b4 = 0;
                                    double b3 = 0;

                                    #region External

                                    //if (rdexternal.Checked == true)
                                    //{
                                    //    int fail_stud_atleast_one = 0;
                                    //    DataSet ds = new DataSet();
                                    //    //cmd = new SqlCommand("select count(distinct(mark_entry.roll_no)) from mark_entry,registration where exam_code=" + examcode_fun + " and mark_entry.Attempts = 1 and result='pass'  and passorfail=1 and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + " and mark_entry.roll_no not in(select distinct(mark_entry.roll_no) from mark_entry,registration where exam_code=" + examcode_fun + "  and mark_entry.Attempts = 1 and (result='Fail' or result='AAA')  and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and   batch_year=" + ddlBatch.SelectedItem.ToString() + "" + strsec + " )", con);
                                    //    ds = d2.select_method_wo_parameter("select count(distinct(m.roll_no)) from mark_entry m,registration r,subject s,syllabus_master sy where exam_code=" + examcode_fun + " and m.Attempts = 1 and s.subject_no=m.subject_no and sy.degree_code=r.degree_code and r.Batch_Year=sy.Batch_Year and s.syll_code=sy.syll_code and result='pass'  and passorfail=1 and m.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy.semester=" + ddlSemYr.SelectedItem.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + " and m.roll_no not in(select distinct(m1.roll_no) from mark_entry m1,registration r1,subject s1,syllabus_master sy1 where s1.subject_no=m1.subject_no and sy1.degree_code=r1.degree_code and r1.Batch_Year=sy1.Batch_Year and s1.syll_code=sy1.syll_code and m1.exam_code=" + examcode_fun + "  and m1.Attempts = 1 and (result='Fail' or result='AAA')  and m1.roll_no=r1.roll_no and r1.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy1.semester=" + ddlSemYr.SelectedItem.ToString() + " and   r1.batch_year=" + ddlBatch.SelectedItem.ToString() + " " + strsec + ")", "Text");
                                    //    // Select count(distinct(mark_entry.roll_no)) from mark_entry,registration where exam_code=" + examcode_fun + "  and mark_entry.Attempts = 1 and (result='Fail' or result='AAA')  and passorfail=0 and mark_entry.roll_no=registration.roll_no and degree_code=" + ddlBranch.SelectedValue.ToString() + " and current_semester=" + ddlSemYr.SelectedItem.ToString() + " and batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + ""
                                    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    //    {
                                    //        fail_stud_atleast_one = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                                    //    }
                                    //    //--------------------------
                                    //    drow["Subject Teacher"] = "No. of Students Passed In All Subject In Section " + secvar + " " + fail_stud_atleast_one.ToString();

                                    //    b3 = (Convert.ToDouble(fail_stud_atleast_one / Convert.ToDouble(tot_stu))) * 100;
                                    //    b4 = Math.Round(b3, 2);
                                    //    if (b4.ToString() == "NaN" || b4.ToString() == "Infinity")
                                    //    {
                                    //        b4 = 0;
                                    //    }
                                    //    if (secper == 0)
                                    //    {
                                    //        secper = b4;
                                    //    }
                                    //    else
                                    //    {
                                    //        secper = secper + b4;
                                    //    }
                                    //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Text = b4.ToString();
                                    //    //FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 8].Font.Bold = true;

                                    //    drow["Appeared"] = "All Pass % In Section " + secvar + " " + b4;

                                    //}
                                    #endregion

                                    if ((rdinternal.Checked == true) && (in_sec_examcode.ToString() != ""))
                                    {
                                        //==========find minmark for particular test
                                        int fail_in_allsubj = 0;
                                        //test_minmark = Convert.ToInt32(GetFunction("select min_mark from criteriaforinternal where criteria_no=" + ddlTest.SelectedValue.ToString() + ""));  //by malang raja
                                        string minmrk = d2.GetFunction("select min_mark from criteriaforinternal where criteria_no=" + ddlTest.SelectedValue.ToString() + "");
                                        double.TryParse(minmrk, out test_minmark);
                                        DataSet ds = new DataSet();
                                        ssd = "select isnull(count(distinct rt.roll_no),0) from result rt,registration r where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and (rt.marks_obtained<" + test_minmark + " and rt.marks_obtained<>'-3' and rt.marks_obtained<>'-2' and rt.marks_obtained<>'-18') and r.exam_flag <>'DEBAR' and r.delflag=0 " + includePassedout + " and r.RollNo_Flag<>0  ";
                                        ssd = ssd + "select isnull(count(distinct rt.roll_no),0),r.Stud_Type,al.sex,r.mode from result rt,registration r,Applyn al where rt.exam_Code " + in_sec_examcode.ToString() + " and rt.roll_no=r.roll_no and al.app_no=r.App_No and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + "  " + strsec + " and (rt.marks_obtained<" + test_minmark + " and rt.marks_obtained<>'-3' and rt.marks_obtained<>'-2' and rt.marks_obtained<>'-18') and r.exam_flag <>'DEBAR' and r.delflag=0 " + includePassedout + " and r.RollNo_Flag<>0  group by r.Stud_Type,al.sex,r.mode ";
                                        ds = d2.select_method_wo_parameter(ssd, "text");
                                        if (ds.Tables.Count > 0)
                                        {
                                            if (!colvis)
                                            {
                                                if (ds.Tables[0].Rows.Count > 0)
                                                    fail_in_allsubj = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                                            }
                                            else
                                            {
                                                if (ds.Tables[1].Rows.Count > 0)
                                                {
                                                    mode = "1";
                                                    if (CkLGender.Items.Count > 0)
                                                    {
                                                        for (int st = 0; st < 3; st++)
                                                        {
                                                            if (st == 2)
                                                                mode = "3";
                                                            string stype = dicstudtype[st];
                                                            if (cblsearch.Items[st + 3].Selected == true)
                                                            {

                                                                for (int gen = 0; gen < CkLGender.Items.Count; gen++)
                                                                {
                                                                    if (CkLGender.Items[gen].Selected == true)
                                                                    {
                                                                        //Strength
                                                                        ds.Tables[1].DefaultView.RowFilter = "Stud_Type='" + stype + "'  and sex='" + CkLGender.Items[gen].Value + "' and mode='" + mode + "' ";
                                                                        DataView dvscnt = ds.Tables[1].DefaultView;

                                                                        if (dvscnt.Count > 0)

                                                                            fail_in_allsubj = fail_in_allsubj + int.Parse(dvscnt[0][0].ToString());
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }



                                        nofopass_str = Convert.ToInt32(nofopass_str + ((allpascnt - fail_in_allsubj)));
                                        totvl = ((allpascnt - fail_in_allsubj));
                                        //b3 = tot_per_all_pass + allpascnt;
                                        b3 = (Convert.ToDouble(totvl) / Convert.ToDouble(tot_stu)) * 100;
                                        noofperc = noofperc + Math.Round(b3);
                                        b4 = Math.Round(b3, 2);
                                        if (b4.ToString() == "NaN" || b4.ToString() == "Infinity")
                                        {
                                            b4 = 0;
                                        }
                                        if (secper == 0)
                                        {
                                            secper = b4;
                                        }
                                        else
                                        {
                                            secper = secper + b4;
                                        }


                                        string value = "";
                                        if (rbappear.Checked == false)
                                            value = "No. of Students Strength For Tests in Section " + secvar + " : " + tot_stu.ToString() + " ,  " + " No. of Students Passed In All Subject In Section " + secvar + " :" + (allpascnt - fail_in_allsubj).ToString() + "  , " + "   All Pass % In Section " + secvar + " " + b4;
                                        else
                                            value = "No. of Students Appeared For Tests in Section " + secvar + " : " + tot_stu.ToString() + " ,  " + "  No. of Students Passed In All Subject In Section " + secvar + " :" + (allpascnt - fail_in_allsubj).ToString() + " ,  " + "  All Pass % In Section " + secvar + " " + b4;

                                        drow["SNo"] = value;
                                        data.Rows.Add(drow);
                                        dicrowspan.Add(spanrow - 1, "1");
                                    }
                                    tot_per_all_pass = tot_per_all_pass + b4;
                                }///////////////////////////28/6/12 PRABHA
                                int t1 = 0;
                                if ((rdinternal.Checked == true))
                                {
                                    spanrow++;
                                    drow = data.NewRow();

                                    string calc = Math.Round((Convert.ToDouble(nofopass_str.ToString()) / Convert.ToDouble(tot_stud_str.ToString())) * 100, 2).ToString(); //noofperc.ToString();


                                    dicrowspan.Add(spanrow - 1, "1");
                                    //--------------------------------------------------------------------------------------------------------
                                    if (rbappear.Checked == false)
                                        drow["SNo"] = "Total No of Students Strength For Tests In All Sections : " + tot_stud_str.ToString() + "  ,  " + "Total No of Students Passed In All Subject In All Sections: " + nofopass_str.ToString() + " ,  " + "All Pass % In All Sections :" + " " + calc;
                                    else
                                        drow["SNo"] = "Total No of Students Strength For Tests In All Sections : " + tot_stud_str.ToString() + " ,  " + "Total No of Students Passed In All Subject In All Sections: " + nofopass_str.ToString() + " ,  " + "All Pass % In All Sections :" + " " + calc;



                                    data.Rows[rowcount - 1]["Appeared"] = tot_stud_str.ToString();
                                    data.Rows[rowcount - 1]["Pass"] = (nofopass_str).ToString();
                                    data.Rows[rowcount - 1]["Pass %"] = calc.ToString();

                                    data.Rows.Add(drow);

                                }
                                //else if (rdexternal.Checked == true)
                                //{
                                //    FpExternal.Sheets[0].RowCount++;
                                //    FpExternal.Sheets[0].Rows[FpExternal.Sheets[0].RowCount - 1].Height = 40;
                                //    FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 7);
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "All PASS % In This Test/Semester";
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].VerticalAlign = VerticalAlign.Middle;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Size = 13;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
                                //    //double bb3 = (tot_per_all_pass) / ddlSec.Items.Count;
                                //    //double bb4 = Math.Round(bb3, 2);
                                //    double bb4 = 0;
                                //    if (ddlSec.Items.Count > 0)
                                //    {
                                //        bb4 = secper / ddlSec.Items.Count;
                                //    }
                                //    else
                                //    {
                                //        bb4 = secper;
                                //    }
                                //    //double bb3 = 0;
                                //    //bb3 = (Convert.ToDouble(allpascnt) / Convert.ToDouble(tot_stu));
                                //    //bb4 = bb4 + Math.Round(bb3);
                                //    //bb4 = Math.Round(bb3, 2);
                                //    ////bb3 = Math.Round((Convert.ToDouble(tot_per_all_pass.ToString()) / Convert.ToDouble(ddlSec.ToString())) * 100, 2).ToString();
                                //    ////string bb4 = Math.Round(bb3, 2);
                                //    if (bb4.ToString() == "NaN" || bb4.ToString() == "Infinity")
                                //    {
                                //        bb4 = 0;
                                //    }
                                //    bb4 = Math.Round(bb4, 2, MidpointRounding.AwayFromZero);
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Text = bb4.ToString();
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Bold = true;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].HorizontalAlign = HorizontalAlign.Center;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].VerticalAlign = VerticalAlign.Middle;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Size = 12;
                                //    FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 9].Font.Bold = true;
                                //    FpExternal.Sheets[0].SetColumnMerge(9, FarPoint.Web.Spread.Model.MergePolicy.Always);
                                //}
                                //--------------------------------------------27/6/12 PRABHA
                                //if (Showgrid.PageSize > 0)
                                //{
                                //    if (rdexternal.Checked == true)
                                //    {
                                //        DateTime todate = DateTime.Now;
                                //        if (yr.ToString() == todate.ToString("yyyy"))
                                //        {
                                //            FpExternal.Sheets[0].RowCount++;
                                //            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Text = "% of students qualifying for degree(for final year final semester only)";
                                //            FpExternal.Sheets[0].SpanModel.Add(FpExternal.Sheets[0].RowCount - 1, 0, 1, 6);
                                //            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Left;
                                //            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].VerticalAlign = VerticalAlign.Middle;
                                //            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Size = 12;
                                //            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 0].Font.Bold = true;
                                //            FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Font.Bold = true;
                                //            if (rdexternal.Checked == true)
                                //            {
                                //                int all_padd_get_degree = Convert.ToInt32(d2.GetFunction("select count(distinct m.roll_no )from mark_entry m,subject s,syllabus_master sy,subjectchooser sc  where s.subject_no=m.subject_no and m.subject_no=sc.subject_no and s.syll_code=sy.syll_code and sy.semester=sc.semester and sy.degree_code=" + ddlBranch.SelectedValue.ToString() + " and sy.batch_year=" + ddlBatch.SelectedItem.ToString() + " and sy.semester<=" + ddlSemYr.SelectedItem.ToString() + " and (result='Fail' or result='AAA')  and passorfail=0 "));
                                //                FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (Math.Round(((Convert.ToDouble(tot_stud_overall - all_padd_get_degree) / Convert.ToDouble(tot_stud_overall)) * 100), 2)).ToString();
                                //            }
                                //            else if (rdinternal.Checked == true)
                                //            {
                                //                int all_qualify_stud = Convert.ToInt32(d2.GetFunction("select count(distinct rt.roll_no) from result rt,registration r where rt.exam_Code " + in_examcode.ToString() + " and rt.roll_no=r.roll_no and r.degree_code=" + ddlBranch.SelectedValue.ToString() + " and r.batch_year=" + ddlBatch.SelectedItem.ToString() + " and rt.marks_obtained>0 and rt.marks_obtained>=" + test_minmark + ""));
                                //                FpExternal.Sheets[0].Cells[FpExternal.Sheets[0].RowCount - 1, 6].Text = (Math.Round(((Convert.ToDouble(tot_stud_overall - all_qualify_stud) / Convert.ToDouble(tot_stud_overall)) * 100), 2)).ToString();
                                //            }
                                //        }
                                //    }
                                //}
                                //==============================================================

                                if (data.Columns.Count > 0 && data.Rows.Count > 0)
                                {
                                    divMainContents.Visible = true;
                                    Showgrid.DataSource = data;
                                    Showgrid.DataBind();
                                    Showgrid.Visible = true;

                                    Showgrid.Rows[0].BackColor = ColorTranslator.FromHtml("#0CA6CA");
                                    Showgrid.Rows[0].Font.Bold = true;
                                    Showgrid.Rows[0].HorizontalAlign = HorizontalAlign.Center;

                                    Showgrid.Rows[1].BackColor = ColorTranslator.FromHtml("#0CA6CA");
                                    Showgrid.Rows[1].Font.Bold = true;
                                    Showgrid.Rows[1].HorizontalAlign = HorizontalAlign.Center;

                                    int d = Convert.ToInt32(data.Columns.Count);
                                    for (int g = 0; g < data.Columns.Count; g++)
                                    {
                                        if (g != 1 && g != 3)
                                        {
                                            for (int j = 2; j < Showgrid.Rows.Count - 3; j++)
                                            {
                                                string value = dicrowspan[j];
                                                if (g == 7 && g == 8 && g == 9)
                                                {
                                                    if (value.ToUpper().Trim() == "OVERALL")
                                                        Showgrid.Rows[j].Cells[g].HorizontalAlign = HorizontalAlign.Right;
                                                }
                                                else if (value.Trim() != "1" && value.Trim() != "3" && value.Trim() != "")
                                                    Showgrid.Rows[j].Cells[g].HorizontalAlign = HorizontalAlign.Center;
                                            }
                                        }

                                    }

                                    int col = 0;
                                    int colcnt = 0;
                                    for (int c = 0; c < cblsearch.Items.Count; c++)
                                    {
                                        if (cblsearch.Items[c].Selected == false)
                                        {
                                            string colname = cblsearch.Items[c].Text;

                                            for (int g = 0; g < arrColHdrNames1.Count; g++)
                                            {
                                                string columname = arrColHdrNames1[g].ToString();
                                                if (colname == columname)
                                                {
                                                    for (int r = 0; r < data.Rows.Count; r++)
                                                        Showgrid.Rows[r].Cells[g].Visible = false;


                                                    if (colname == "Overall")
                                                        col++;
                                                    else
                                                        colcnt++;
                                                }

                                            }

                                        }
                                    }



                                    //Rowspan

                                    for (int rowIndex = Showgrid.Rows.Count - 6; rowIndex >= 0; rowIndex--)
                                    {
                                        GridViewRow row = Showgrid.Rows[rowIndex];
                                        GridViewRow previousRow = Showgrid.Rows[rowIndex + 1];

                                        for (int i = 0; i < row.Cells.Count; i++)
                                        {
                                            if (row.RowIndex == 0 && previousRow.RowIndex == 1)
                                            {
                                                if (row.Cells[i].Text == previousRow.Cells[i].Text)
                                                {

                                                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                                           previousRow.Cells[i].RowSpan + 1;
                                                    previousRow.Cells[i].Visible = false;
                                                }

                                            }
                                            else
                                            {
                                                if (Showgrid.HeaderRow.Cells[i].Text == "SNo" || Showgrid.HeaderRow.Cells[i].Text == "Subject code and name" || Showgrid.HeaderRow.Cells[i].Text == "Section" || Showgrid.HeaderRow.Cells[i].Text == "Subject Teacher" || Showgrid.HeaderRow.Cells[i].Text == "Appeared" || Showgrid.HeaderRow.Cells[i].Text == "Pass" || Showgrid.HeaderRow.Cells[i].Text == "Pass %")
                                                {
                                                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                                                    {

                                                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                                               previousRow.Cells[i].RowSpan + 1;
                                                        previousRow.Cells[i].Visible = false;
                                                    }
                                                }

                                            }
                                        }

                                    }
                                    int rowcnt = Showgrid.Rows.Count - 2;
                                    int rowcnt1 = Showgrid.Rows.Count - 4;
                                    //ColumnSpan
                                    for (int rowIndex = Showgrid.Rows.Count - rowcnt - 2; rowIndex >= 0; rowIndex--)
                                    {
                                        for (int cell = Showgrid.Rows[rowIndex].Cells.Count - 1; cell > 0; cell--)
                                        {
                                            TableCell colum = Showgrid.Rows[rowIndex].Cells[cell];
                                            TableCell previouscol = Showgrid.Rows[rowIndex].Cells[cell - 1];
                                            if (colum.Text == previouscol.Text)
                                            {
                                                if (previouscol.ColumnSpan == 0)
                                                {
                                                    if (colum.ColumnSpan == 0)
                                                    {
                                                        previouscol.ColumnSpan += 2;

                                                    }
                                                    else
                                                    {
                                                        previouscol.ColumnSpan += colum.ColumnSpan + 1;

                                                    }

                                                    colum.Visible = false;

                                                }
                                            }
                                        }

                                    }
                                    int col2 = col;
                                    if (col == 0)
                                        col = 3;



                                    for (int rowIndex = Showgrid.Rows.Count - 1; rowIndex > rowcnt1 - 1; rowIndex--)
                                    {
                                        Showgrid.Rows[rowIndex].Font.Bold = true;


                                        if (rowIndex == rowcnt1)
                                        {
                                            Showgrid.Rows[rowIndex].Cells[0].HorizontalAlign = HorizontalAlign.Right;
                                            Showgrid.Rows[rowIndex].Cells[0].ColumnSpan = d - (col + colcnt);
                                            for (int h = 1; h < d - (col); h++)
                                                Showgrid.Rows[rowIndex].Cells[h].Visible = false;

                                        }
                                        else
                                        {
                                            Showgrid.Rows[rowIndex].HorizontalAlign = HorizontalAlign.Left;
                                            Showgrid.Rows[rowIndex].Cells[0].ColumnSpan = d - (col2);
                                            for (int h = 1; h < d - (col2); h++)
                                                Showgrid.Rows[rowIndex].Cells[h].Visible = false;
                                        }
                                    }

                                    btnmasterprint.Visible = true;
                                    btn_dirtprint.Visible = true;
                                    btnxl.Visible = true;//Added By Srinath 
                                    lblrptname.Visible = true;
                                    txtexcelname.Visible = true;
                                    lblnorec.Visible = false;
                                }


                            }
                            if (recflag == false)
                            {
                                if (rdinternal.Checked == true)
                                {
                                    lblnorec.Visible = true;
                                    Showgrid.Visible = false;
                                    btnmasterprint.Visible = false;
                                    btn_dirtprint.Visible = false;
                                    //Added By Srinath 
                                    btnxl.Visible = false;
                                    divMainContents.Visible = false;
                                    lblrptname.Visible = false;
                                    txtexcelname.Visible = false;
                                    lblnorec.Text = "No Records Found";
                                }
                                else if (rdexternal.Checked == true)
                                {
                                    lblnorec.Visible = true;
                                    Showgrid.Visible = false;
                                    btnmasterprint.Visible = false;
                                    btn_dirtprint.Visible = false;
                                    //Added By Srinath 
                                    btnxl.Visible = false;
                                    lblrptname.Visible = false;
                                    divMainContents.Visible = false;
                                    txtexcelname.Visible = false;
                                    lblnorec.Text = "No Records Found";
                                }
                            }
                            // logoset();
                            //FpExternal.Sheets[0].PageSize = FpExternal.Sheets[0].RowCount;
                            //func_multi_iso();
                        }

                        #endregion
                    }
                    else
                    {
                        if (rdinternal.Checked == true)
                        {
                            divMainContents.Visible = false;
                            lblnorec.Visible = true;
                            Showgrid.Visible = false;
                            btnmasterprint.Visible = false;
                            //Added By Srinath 
                            btnxl.Visible = false;
                            lblrptname.Visible = false;
                            txtexcelname.Visible = false;
                            btn_dirtprint.Visible = false;
                            lblnorec.Text = "No Records Found";
                        }
                        else if (rdexternal.Checked == true)
                        {
                            divMainContents.Visible = false;
                            lblnorec.Visible = true;
                            Showgrid.Visible = false;
                            btnmasterprint.Visible = false;
                            //Added By Srinath 
                            btnxl.Visible = false;
                            lblrptname.Visible = false;
                            txtexcelname.Visible = false;
                            btn_dirtprint.Visible = false;
                            lblnorec.Text = "No Records Found";
                        }
                    }
                }

                func_multi_iso();
            }
            #endregion

        }
        catch (Exception ex)
        {
            lblnorec.Text = ex.ToString();
            lblnorec.Visible = true;
        }
    }

    public int GetSemester_AsNumber(int IpValue)
    {
        InsFlag = false;
        string strinssetting = string.Empty;
        string VarProcessValue = string.Empty;
        int GetSemesterAsNumber = 0;
        strinssetting = "select * from inssettings where college_code=" + Session["collegecode"] + " and LinkName='Semester Display'";
        con_Inssetting.Close();
        con_Inssetting.Open();
        SqlCommand cmd_ins = new SqlCommand(strinssetting, con_Inssetting);
        SqlDataReader dr_ins;
        dr_ins = cmd_ins.ExecuteReader();
        while (dr_ins.Read())
        {
            if (dr_ins.HasRows == true)
            {
                if (dr_ins["LinkName"].ToString() == "Semester Display")
                {
                    InsFlag = true;
                }
                if (Convert.ToInt32(dr_ins["LinkValue"]) == 0)
                {
                    GetSemesterAsNumber = IpValue;
                }
                else if (Convert.ToInt32(dr_ins["LinkValue"]) == 1)
                {
                    VarProcessValue = Convert.ToString(IpValue).Trim();
                }
            }
        }
        return IpValue;
    }



    void CalculateTotalPages()
    {
        Double totalRows = 0;
        totalRows = Convert.ToInt32(Showgrid.Rows.Count);
        Session["totalPages"] = (int)Math.Ceiling(totalRows / Showgrid.PageSize);
        Buttontotal.Text = "Records : " + totalRows + "          Pages : " + Session["totalPages"];
        Buttontotal.Visible = true;
    }

    public void convertgrade(string roll, string subj)
    {
        strexam = "Select subject_name,subject_code,total,result,cp,mark_entry.subject_no from Mark_Entry,Subject,sub_sem where Mark_Entry.Subject_No = Subject.Subject_No and subject.subtype_no= sub_sem.subtype_no and  Exam_Code = " + IntExamCode + "  and roll_no='" + roll + "' and subject.subject_no=" + subj + "";
        SqlCommand cmd_exam1 = new SqlCommand(strexam, con_convertgrade);
        con_convertgrade.Close();
        con_convertgrade.Open();
        dr_convert = cmd_exam1.ExecuteReader();
        while (dr_convert.Read())
        {
            funcsubname = dr_convert["subject_name"].ToString();
            funcsubno = dr_convert["subject_no"].ToString();
            funcsubcode = dr_convert["subject_code"].ToString();
            funcresult = dr_convert["result"].ToString();
            funccredit = dr_convert["cp"].ToString();
            mark = dr_convert["total"].ToString();
            string strgrade = string.Empty;
            if (dr_convert["total"].ToString() != string.Empty)
            {
                strgrade = "select mark_grade from grade_master where degree_code=" + degree_code + " and batch_year=" + batch_year + " and college_code=" + Session["collegecode"] + " and " + dr_convert["total"] + " between frange and trange";
            }
            else
            {
                strgrade = "select mark_grade from grade_master where degree_code=" + degree_code + " and batch_year=" + batch_year + " and college_code=" + Session["collegecode"] + " and credit_points between frange and trange";
            }
            SqlCommand cmd_grade = new SqlCommand(strgrade, con_Grade);
            con_Grade.Close();
            con_Grade.Open();
            SqlDataReader dr_grade;
            dr_grade = cmd_grade.ExecuteReader();
            while (dr_grade.Read())
            {
                funcgrade = dr_grade["mark_grade"].ToString();
            }
        }
    }

    protected void tamilbutton_Click(object sender, EventArgs e)
    {
    }

    public string findroman(string sem)
    {
        string sem3 = string.Empty;
        if (sem == "1")
            sem3 = "I";
        else if (sem == "2")
            sem3 = "II";
        else if (sem == "3")
            sem3 = "III";
        else if (sem == "4")
            sem3 = "IV";
        else if (sem == "5")
            sem3 = "V";
        else if (sem == "6")
            sem3 = "VI";
        else if (sem == "7")
            sem3 = "VII";
        else if (sem == "8")
            sem3 = "VIII";
        else if (sem == "9")
            sem3 = "IX";
        else if (sem == "10")
            sem3 = "X";
        return sem3;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Saran
        //string batch = "", sections = "", semester = "", degreecode = "", subcolumntext = "", strsec = string.Empty;
        //bool child_flag = false;
        //int sec_index = 0, sem_index = 0;
        //batch = ddlBatch.SelectedValue.ToString();
        //sections = ddlSec.SelectedValue.ToString();
        //semester = ddlSemYr.SelectedValue.ToString();
        //degreecode = ddlBranch.SelectedValue.ToString();
        //if (ddlSec.Text == "")
        //{
        //    strsec = string.Empty;
        //}
        //else
        //{
        //    if (ddlSec.SelectedItem.ToString() == "")
        //    {
        //        strsec = string.Empty;
        //    }
        //    else
        //    {
        //        strsec = " - " + ddlSec.SelectedItem.ToString();
        //    }
        //}
        //if (ddlSec.Enabled == false)
        //{
        //    sec_index = -1;
        //}
        //else
        //{
        //    sec_index = ddlSec.SelectedIndex;
        //}
        //if (ddlSemYr.Enabled == false)
        //{
        //    sem_index = -1;
        //}
        //else
        //{
        //    sem_index = ddlSemYr.SelectedIndex;
        //}
        //Session["page_redirect_value"] = ddlBatch.SelectedIndex + "," + ddlDegree.SelectedIndex + "," + ddlBranch.SelectedIndex + "," + sem_index + "," + sec_index + "," + ddlMonth.SelectedIndex + "," + ddlYear.SelectedIndex + "," + rdinternal.Checked + "," + rdexternal.Checked + "," + ddlTest.SelectedIndex;
        //// first_btngo();
        //btnGo_Click(sender, e);
        ////lblpages.Visible = true;
        ////ddlpage.Visible = true;
        //string clmnheadrname = string.Empty;
        //int total_clmn_count = FpExternal.Sheets[0].ColumnCount;
        //Response.Redirect("Print_Master_Setting_new.aspx?ID=" + clmnheadrname.ToString() + ":" + "result_analysis_rpt.aspx" + ":" + ddlBatch.SelectedItem.ToString() + " Batch - " + ddlDegree.SelectedItem.ToString() + "-" + ddlBranch.SelectedItem.ToString() + "[ " + sem_roman(Convert.ToInt16(ddlSemYr.SelectedItem.ToString())) + "  Semester ] " + strsec + " :" + "Branchwise Result Analysis");
    }

    public string sem_roman(int sem)
    {
        string sql = string.Empty;
        string sem_roman = string.Empty;
        SqlDataReader rsChkSet;
        con1.Close();
        con1.Open();
        sql = "select * from inssettings where college_code=" + Session["collegecode"] + " and LinkName ='Semester Display'";
        SqlCommand cmd1 = new SqlCommand(sql, con1);
        rsChkSet = cmd1.ExecuteReader();
        rsChkSet.Read();
        if (rsChkSet.HasRows == true)
        {
            if (rsChkSet["linkvalue"].ToString() == "1")
            {
                switch (sem)
                {
                    case 1:
                        sem_roman = "1";
                        break;
                    case 2:
                        sem_roman = "1-II";
                        break;
                    case 3:
                        sem_roman = "2-I";
                        break;
                    case 4:
                        sem_roman = "2-II";
                        break;
                    case 5:
                        sem_roman = "3-I";
                        break;
                    case 6:
                        sem_roman = "3-II";
                        break;
                    case 7:
                        sem_roman = "4-I";
                        break;
                    case 8:
                        sem_roman = "4-II";
                        break;
                    default:
                        sem_roman = " ";
                        break;
                }
            }
            else
            {
                switch (sem)
                {
                    case 1:
                        sem_roman = "I";
                        break;
                    case 2:
                        sem_roman = "II";
                        break;
                    case 3:
                        sem_roman = "III";
                        break;
                    case 4:
                        sem_roman = "IV";
                        break;
                    case 5:
                        sem_roman = "V";
                        break;
                    case 6:
                        sem_roman = "VI";
                        break;
                    case 7:
                        sem_roman = "VII";
                        break;
                    case 8:
                        sem_roman = "VIII";
                        break;
                    case 9:
                        sem_roman = "IX";
                        break;
                    case 10:
                        sem_roman = "X";
                        break;
                    default:
                        sem_roman = " ";
                        break;
                }
            }
        }
        return sem_roman;
    }

    protected void btnxl_Click(object sender, EventArgs e)
    {
        //Modified by Srinath 27/2/2013
        string reportname = txtexcelname.Text.ToString().Trim();
        if (reportname != "")
        {
            dacces2.printexcelreportgrid(Showgrid, reportname);
            lblerr.Visible = false;
        }
        else
        {
            lblerr.Text = "Please Enter Your Report Name";
            lblerr.Visible = true;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    { }

    //=====================func for load the test

    public void func_multi_iso()
    {
        string MultiISO = string.Empty;
        DataSet dsprint = new DataSet();
        try
        {
            hat.Clear();
            hat.Add("college_code", Session["collegecode"].ToString());
            hat.Add("form_name", "Result_Analysis_Rpt.aspx");
            dsprint = dacces2.select_method("PROC_PRINT_MASTER_SETTINGS", hat, "sp");
            if (dsprint.Tables.Count > 0 && dsprint.Tables[0].Rows.Count > 0)
            {
                MultiISO = dsprint.Tables[0].Rows[0]["MultiISOCode"].ToString();
            }
            //==================================================
            if (MultiISO != "")
            {
                string[] spl_iso = MultiISO.Split(',');
                int c = 0;
                int isocount = 0;
                int rowcount = 0;
                isocount = spl_iso.GetUpperBound(0) + 1;
                if (spl_iso.GetUpperBound(0) > 0)
                {
                    //for (int iso = 0; iso < FpExternal.Sheets[0].ColumnHeader.RowCount; iso++)
                    //{
                    //    if (FpExternal.Sheets[0].ColumnHeader.Rows[iso].Visible == true)
                    //    {
                    //        rowcount++;
                    //        FpExternal.Sheets[0].ColumnHeader.Cells[iso, FpExternal.Sheets[0].ColumnCount - 1].Text = spl_iso[c].ToString();
                    //        FpExternal.Sheets[0].ColumnHeader.Cells[iso, FpExternal.Sheets[0].ColumnCount - 1].HorizontalAlign = HorizontalAlign.Left;
                    //        FpExternal.Sheets[0].ColumnHeader.Cells[iso, FpExternal.Sheets[0].ColumnCount - 1].Border.BorderColorRight = Color.White;
                    //        FpExternal.Sheets[0].ColumnHeader.Cells[iso, FpExternal.Sheets[0].ColumnCount - 1].Border.BorderColorBottom = Color.White;
                    //        FpExternal.Sheets[0].ColumnHeader.Cells[iso, FpExternal.Sheets[0].ColumnCount - 1].Border.BorderColorTop = Color.White;
                    //        FpExternal.Sheets[0].ColumnHeader.Cells[0, FpExternal.Sheets[0].ColumnCount - 1].Border.BorderColorTop = Color.Black;
                    //        c++;
                    //    }
                    //}
                }
                int remain_rowcount = isocount - rowcount;
                if (remain_rowcount != 0)
                {
                    //  FpExternal.Sheets[0].ColumnHeader.RowCount += remain_rowcount;
                    for (int iso1 = c; iso1 < isocount; iso1++)
                    {
                        //    FpExternal.Sheets[0].ColumnHeader.RowCount++;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 1].Text = spl_iso[c].ToString();
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 1].HorizontalAlign = HorizontalAlign.Left;
                        //    FpExternal.Sheets[0].ColumnHeaderSpanModel.Add(FpExternal.Sheets[0].ColumnHeader.RowCount - 1, 0, 1, FpExternal.Sheets[0].ColumnCount - 3);
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, 0].Text = " ";
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 2].Text = " ";
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 3].Text = " ";
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 4].Text = " ";
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 5].Text = " ";
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 6].Text = " ";
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, 0].Border.BorderColorRight = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, 0].Border.BorderColorBottom = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, 0].Border.BorderColorTop = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 2].Border.BorderColorRight = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 2].Border.BorderColorBottom = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 2].Border.BorderColorTop = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 3].Border.BorderColorRight = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 3].Border.BorderColorBottom = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 3].Border.BorderColorTop = Color.White;
                        //    FpExternal.Sheets[0].ColumnHeader.Cells[FpExternal.Sheets[0].ColumnHeader.RowCount - 1, FpExternal.Sheets[0].ColumnCount - 1].Border.BorderColorBottom = Color.White;
                        //    c++;
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void Showgrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)

                for (int grCol = 0; grCol < data.Columns.Count; grCol++)
                    e.Row.Cells[grCol].Visible = false;



        }
        catch
        {


        }

    }



    protected void includepastout_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chklstaff_SelectedIndexChanged(object sender, EventArgs e)
    {

        chkstaff.Checked = false;
        txtstaff.Text = "---Select---";
        int corcount = 0;
        string value = string.Empty;
        string code = string.Empty;
        for (int i = 0; i < chklstaff.Items.Count; i++)
        {
            if (chklstaff.Items[i].Selected == true)
            {
                value = chklstaff.Items[i].Text;
                code = chklstaff.Items[i].Value.ToString();
                corcount = corcount + 1;
            }
        }
        if (corcount > 0)
        {
            txtstaff.Text = "Staff Type(" + corcount.ToString() + ")";
            if (corcount == chklstaff.Items.Count)
            {
                chkstaff.Checked = true;
            }
        }

        Showgrid.Visible = false;
        divMainContents.Visible = false;

        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        Printcontrol.Visible = false;
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
        txtexcelname.Text = "";

    }
    protected void chkstaff_CheckedChanged(object sender, EventArgs e)
    {


        if (chkstaff.Checked == true)
        {
            for (int i = 0; i < chklstaff.Items.Count; i++)
            {
                chklstaff.Items[i].Selected = true;
            }
            txtstaff.Text = "Staff Type(" + (chklstaff.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < chklstaff.Items.Count; i++)
            {
                chklstaff.Items[i].Selected = false;
            }
            txtstaff.Text = "---Select---";
        }
        Showgrid.Visible = false;
        divMainContents.Visible = false;
        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        Printcontrol.Visible = false;
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
        txtexcelname.Text = "";
    }


    public void btnPrint11()
    {
        DAccess2 ddd2 = new DAccess2();
        string college_code = Convert.ToString(Session["collegecode"].ToString());
        string colQ = "select * from collinfo where college_code='" + college_code + "'";
        DataSet dsCol = new DataSet();
        dsCol = ddd2.select_method_wo_parameter(colQ, "Text");
        string collegeName = string.Empty;
        string collegeCateg = string.Empty;
        string collegeAff = string.Empty;
        string collegeAdd = string.Empty;
        string collegePhone = string.Empty;
        string collegeFax = string.Empty;
        string collegeWeb = string.Empty;
        string collegeEmai = string.Empty;
        string collegePin = string.Empty;
        string acr = string.Empty;
        string City = string.Empty;
        if (dsCol.Tables.Count > 0 && dsCol.Tables[0].Rows.Count > 0)
        {
            collegeName = Convert.ToString(dsCol.Tables[0].Rows[0]["Collname"]);
            City = Convert.ToString(dsCol.Tables[0].Rows[0]["address3"]);
            collegeAff = "(Affiliated to " + Convert.ToString(dsCol.Tables[0].Rows[0]["university"]) + ")";
            collegeAdd = Convert.ToString(dsCol.Tables[0].Rows[0]["address1"]) + " , " + Convert.ToString(dsCol.Tables[0].Rows[0]["address2"]) + " , " + Convert.ToString(dsCol.Tables[0].Rows[0]["district"]) + " - " + Convert.ToString(dsCol.Tables[0].Rows[0]["pincode"]);
            collegePin = Convert.ToString(dsCol.Tables[0].Rows[0]["pincode"]);
            collegePhone = "OFFICE: " + Convert.ToString(dsCol.Tables[0].Rows[0]["phoneno"]);
            collegeFax = "FAX: " + Convert.ToString(dsCol.Tables[0].Rows[0]["faxno"]);
            collegeWeb = "Website: " + Convert.ToString(dsCol.Tables[0].Rows[0]["website"]);
            collegeEmai = "E-Mail: " + Convert.ToString(dsCol.Tables[0].Rows[0]["email"]);
            collegeCateg = "(" + Convert.ToString(dsCol.Tables[0].Rows[0]["category"]) + ")";
        }
        DateTime dt = DateTime.Now;
        int year = dt.Year;
        spCollegeName.InnerHtml = collegeName;
        spAddr.InnerHtml = collegeAdd;
        spDegreeName.InnerHtml = acr;
        spReportName.InnerHtml = "Branchwise Result Analysis";
        // spSection.InnerHtml ="Satff: "+ Convert.ToString(ddlSearchOption.SelectedItem.Text);


    }


    //Added By SaranyaDevi 27.11.2018

    private static void AddTableColumn(DataTable resultsTable, StringBuilder ColumnName)
    {
        try
        {
            DataColumn tableCol = new DataColumn(ColumnName.ToString());
            resultsTable.Columns.Add(tableCol);
        }
        catch (System.Data.DuplicateNameException)
        {
            ColumnName.Append(" ");
            AddTableColumn(resultsTable, ColumnName);
        }
    }


    #region Gender
    public void loadGender()
    {
        try
        {
            TextGender.Text = "--Select--";
            CheckGender.Checked = false;
            CkLGender.Items.Clear();
            DataSet dsgender = new DataSet();
            string Gender = string.Empty;

            Gender = "select distinct isnull(sex,'10') Gen_no, case when sex='0' then 'Male' when sex='1' then 'Female' when sex='2' then 'Transgender' end Gender from Applyn  where isnull(sex,'10')<>'10'  order by Gen_no";
            dsgender.Clear();
            dsgender = d2.select_method_wo_parameter(Gender, "Text");
            if (dsgender.Tables.Count > 0 && dsgender.Tables[0].Rows.Count > 0)
            {
                CkLGender.DataSource = dsgender;
                CkLGender.DataValueField = "Gen_no";
                CkLGender.DataTextField = "Gender";
                CkLGender.DataBind();
            }

            if (CkLGender.Items.Count > 0)
            {
                for (int row = 0; row < CkLGender.Items.Count; row++)
                {
                    CkLGender.Items[row].Selected = true;
                    CheckGender.Checked = true;
                }
                TextGender.Text = "Gender(" + CkLGender.Items.Count + ")";
            }
            else
            {
                TextGender.Text = "--Select--";
            }


        }
        catch
        {

        }
    }

    protected void CheckGender_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckGender.Checked == true)
        {
            for (int i = 0; i < CkLGender.Items.Count; i++)
            {
                CkLGender.Items[i].Selected = true;
            }
            TextGender.Text = "Gender(" + (CkLGender.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < CkLGender.Items.Count; i++)
            {
                CkLGender.Items[i].Selected = false;
            }
            TextGender.Text = "---Select---";
        }
        Showgrid.Visible = false;
        divMainContents.Visible = false;

    }

    protected void CkLGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        PGender.Focus();
        CheckGender.Checked = false;
        TextGender.Text = "---Select---";
        int corcount = 0;
        string value = string.Empty;
        string code = string.Empty;
        for (int i = 0; i < CkLGender.Items.Count; i++)
        {
            if (CkLGender.Items[i].Selected == true)
            {
                value = CkLGender.Items[i].Text;
                code = CkLGender.Items[i].Value.ToString();
                corcount = corcount + 1;
            }
        }
        if (corcount > 0)
        {
            TextGender.Text = "Gender(" + corcount.ToString() + ")";
            if (corcount == CkLGender.Items.Count)
            {
                CheckGender.Checked = true;
            }
        }

        Showgrid.Visible = false;
        divMainContents.Visible = false;

    }

    #endregion

    //Column Order
    public void addcloumnitems()
    {
        try
        {

            cblsearch.Items.Add("Subject Code and Name");
            cblsearch.Items.Add("Section");
            cblsearch.Items.Add("Subject Teacher");
            cblsearch.Items.Add("Day Scholar");
            cblsearch.Items.Add("Hosteller");
            cblsearch.Items.Add("Lateral Entry");
            if (rbappear.Checked == false)
                cblsearch.Items.Add("No. Strength");
            else
                cblsearch.Items.Add("No. Appeared");
            cblsearch.Items.Add("No. Passed");
            cblsearch.Items.Add("Sectionwise Pass %");
            cblsearch.Items.Add("Overall");
        }
        catch
        {

        }

    }

    protected void cblsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear1();
        Cbcolumn.Checked = false;
    }

    protected void Cbcolumn_CheckedChanged(object sender, EventArgs e)
    {
        clear1();
        if (Cbcolumn.Checked == true)
        {
            for (int c = 0; c < cblsearch.Items.Count; c++)
            {
                cblsearch.Items[c].Selected = true;
            }
        }
        else
        {
            for (int c = 0; c < cblsearch.Items.Count; c++)
            {
                cblsearch.Items[c].Selected = false;
            }
        }
    }

    public void clear1()
    {
        Showgrid.Visible = false;
        divMainContents.Visible = false;
        btnmasterprint.Visible = false;
        btn_dirtprint.Visible = false;
        Printcontrol.Visible = false;
        btnxl.Visible = false;
        lblrptname.Visible = false;
        txtexcelname.Visible = false;
        txtexcelname.Text = "";
    }
}