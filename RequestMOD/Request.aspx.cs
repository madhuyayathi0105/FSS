﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using InsproDataAccess;
using wc = System.Web.UI.WebControls;

public partial class Request : System.Web.UI.Page
{
    string usercode = string.Empty;
    string collegecode1 = string.Empty;
    static string colleg = string.Empty;
    string collegecode = string.Empty;
    static string clgcode = string.Empty;
    string singleuser = string.Empty;
    string group_user = string.Empty;
    public static string rights = "";
    public static string ishostel = "";
    public static string vendcompname = "";
    public static string Item_Type = "";
    public static Int64 VendorFK;
    static string staff_per_count = "";
    static string deletevalue = "";
    static string semstatic;
    static string secstatci;
    Boolean alert = false;
    static string addleave = "";
    static string mulicollg = "";
    string user_id = "";
    string Password = "";
    string SenderID = "";
    string isst = "";
    string mobilenos = "";
    string strmsg = "";
    InsproDirectAccess dir = new InsproDirectAccess();
    DAccess2 d2 = new DAccess2();
    DataSet ds = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet ds1 = new DataSet();
    ReuasableMethods reuse = new ReuasableMethods();
    Hashtable hat = new Hashtable();
    Hashtable ht = new Hashtable();
    DataTable dt = new DataTable();
    DataTable dt2 = new DataTable();
    int row;
    Boolean gridclick = false;
    Hashtable ht_sch = new Hashtable();
    Hashtable ht_sdate = new Hashtable();
    Hashtable ht_bell = new Hashtable();
    Hashtable ht_period = new Hashtable();
    Hashtable totalleave = new Hashtable();
    Boolean flag = false;
    string Rollflag1 = string.Empty;
    string Regflag1 = string.Empty;
    DataRow dr;
    string college = "";
    string floor = "";
    string sqladd = "";
    string build = "";
    string build1 = "";
    static string pri_txt = "";
    static string con_txt = "";
    static string checknew = "";
    static string sms_req = "";
    static string sms_app = "";
    static string sms_exit = "";
    static string sms_reject = "";
    static string gatepass_staffdept = "";
    static string sms_mom = "";
    static string sms_dad = "";
    static string sms_stud = "";
    static string hrr = "";
    static string staffcodesession = "";
    static string requestpermissioncheck = "";
    static Hashtable depthash = new Hashtable();
    DAccess2 queryObject = new DAccess2();
    DAccess2 da = new DAccess2();
    static Hashtable newhash = new Hashtable();
    static Hashtable newhashtbl = new Hashtable();
    bool value_flage = false;
    string rollflag1 = string.Empty;
    DateTime Datehol;
    static string holidaydate = "";
    SqlConnection ssql = new SqlConnection(ConfigurationManager.AppSettings["LocalConn"].ToString());
    SqlConnection cona = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DSN"]);
    static string deg_batch_sem = "";
    static string alterrigths = "";
    int SchOrder = 0, nodays = 0;
    int intNHrs = 0;
    SqlCommand cmd;
    static string startdate1 = "";
    static int hourstaff = 0;
    static string stafcodealter = "";
    static string tblday = "";
    static string curday = "";
    static double totleavedays = 0;
    static int dd = 0;
    static string closedleave = "";
    Hashtable headerleavetype = new Hashtable();
    static string headerlev = "";
    static string h = "";
    static string hh = "";
    Boolean sun_check = false;
    Boolean holi_check = false;
    Boolean datecheck = false;
    static string leavetypecount = "";
    static int rowval = 0;
    static int colval = 0;
    static int ss = 0;
    static int bindleavecount = 0;
    static string rowgrid = "";
    static string colgrind = "";
    static int chkrelived = 0;
    // Boolean partl_check = false;
    //Boolean full_check = false;
    Boolean leavedayscheckcount = false;
    static int full_check = 0;
    static int partl_check = 0;
    static int saveclear = 0;
    static string checksstu = string.Empty;
    static string col_code = string.Empty;
    static string colgcode = string.Empty;//saran 30.11.2018
    Dictionary<string, string> DateMornOrEvenLeaveDic = new Dictionary<string, string>();
    DataTable data = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["collegecode"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        usercode = Session["usercode"].ToString();
        collegecode1 = Session["collegecode"].ToString();
        colleg = collegecode1;
        singleuser = Session["single_user"].ToString();
        group_user = Session["group_code"].ToString();
        collegecode1 = Session["collegecode"].ToString();
        clgcode = Session["collegecode"].ToString();
        staffcodesession = Session["Staff_Code"].ToString();
        timevalue1();
        if (Chkdesch.Checked == true)
            checksstu = "dayscholar";
        else if (Chkhostel.Checked == true)
            checksstu = "Hosteler";
        else
            checksstu = "ALL";
        CalendarExtender1.EndDate = DateTime.Now;
        if (!IsPostBack)
        {
            //CalendarExtender1.EndDate = DateTime.Now;
            //CalendarExtender9.EndDate = DateTime.Now;
            //CalendarExtender8.EndDate = DateTime.Now.AddDays(30);
            imagestaff.ImageUrl = "image/Gender Neutral User Filled-100(1).png";
            access();
            access1();
            TabAccess();
            staffcount();
            alternateRights();
            leaverequestsetting();
            leave_staff_login();
            ItemReqNo();
            loadhour();
            loadminits();
            btnstaffadd.Visible = false;
            divPopAlertContent.Visible = false;
            if (txt_frm.Text == txt_to.Text)
            {
                GV1.Visible = false;
            }
            if (alterrigths == "1")
            {
                batchbtn.Visible = true;
                lnk_AlterStaff.Visible = false;
            }
            else if (alterrigths == "2")
            {
                lnk_AlterStaff.Visible = true;
                batchbtn.Visible = false;
            }
            else
            {
                batchbtn.Visible = false;
                lnk_AlterStaff.Visible = false;
            }
            if (requestpermissioncheck.Trim() == "1")
            {
                Btn_Cancel.Visible = false;
            }
            txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_exdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_serexpdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_serreqdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_visitorreqdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_visitdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_time_rqstn_leave.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_time_rqstn_leave.Attributes.Add("readonly", "readonly");
            txt_compreqdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtapply.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtapply.Attributes.Add("readonly", "readonly");
            txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtfromdate.Attributes.Add("readonly", "readonly");
            txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Attributes.Add("readonly", "readonly");
            txt_applydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_applydate.Attributes.Add("readonly", "readonly");
            txt_frm.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_frm.Attributes.Add("readonly", "readonly");
            txt_to.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_to.Attributes.Add("readonly", "readonly");
            txt_reqtn_gate_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_reqtn_gate_date.Attributes.Add("readonly", "readonly");
            BindCollege();
            rdlist.Checked = true;
            //  txt_min_startdate.Enabled = false;
            //rindiv.Visible = true;
            lbl_headername.Visible = true;
            td_item.BgColor = "#c4c4c4";
            itemheader();
            itemmaster();
            griddiv.Visible = false;
            SelectdptGrid.Visible = false;
            Sergrid.Visible = false;
            loadhour();
            loadminits();
            timevalue();
            loadtimes();
            rbcheck();
            BindLeave();
            loaddepartment();
            loaddesignation();
            gatepass_stud.Visible = false;
            panelrollnopop.Visible = false;
            loadcomplaints();
            loadsuggestions();
            loadreason();
            //loadeventLOC();
            res();
            dept();
            loadleavereason();
            leav_res();
            loadreqby();
            resrequest();
            loadreqmode();
            req_mode();
            checkdept();
            checkdesg();
            bindbatch();
            binddeg();
            branch();
            bindhostelname();
            bindbuild1();
            bindfloor1();
            bindroom1();
            bindsem1();
            BindSectionDetail();
            loadstaffdep1(collegecode);
            bind_stafType1();
            bind_design1();
            loadsubheadername();
            txt_to.Visible = true;
            lbl_to.Visible = true;
            bindpop1college();
            Btn_Cancel.Visible = true;
            degree();
            if (ddlcollege.Items.Count > 0)
            {
                mulicollg = Convert.ToString(ddlcollege.SelectedItem.Value);
                col_code = mulicollg;
            }
            bindpop2batchyear();
            branch1();
            //panelrollnopop.Visible = true;
            gatepass_stud.Visible = true;
            //clgfloor(build);
            //alternate schedule changed based on settings
            if (alterrigths == "1")
                degbatchsem();
            else if (alterrigths == "2")
                afteralterSchedule();
            // degbatchsem();
            //   btn_go1stud_Click(sender, e);
            fproll.Sheets[0].AutoPostBack = false;
            fproll.Sheets[0].RowCount = 0;
            fproll.Visible = false;
            ddl_pop2sex.Items.Add(new ListItem("All", "0"));
            ddl_pop2sex.Items.Add(new ListItem("Male", "1"));
            ddl_pop2sex.Items.Add(new ListItem("Female", "2"));
            ddl_pop2sex.Items.Add(new ListItem("Transgender", "3"));
            fsstaff.Sheets[0].AutoPostBack = true;
            fsstaff.CommandBar.Visible = false;
            FarPoint.Web.Spread.StyleInfo darkstyle111 = new FarPoint.Web.Spread.StyleInfo();
            darkstyle111.BackColor = ColorTranslator.FromHtml("#0CA6CA");
            darkstyle111.ForeColor = Color.Black;
            darkstyle111.HorizontalAlign = HorizontalAlign.Center;
            fsstaff.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle111;
            fsstaff.Sheets[0].AllowTableCorner = true;
            fsstaff.Sheets[0].RowHeader.Visible = false;
            //fsstaff.Sheets[0].AutoPostBack = true;
            fsstaff.Sheets[0].ColumnCount = 3;
            fsstaff.Sheets[0].ColumnHeader.Columns[0].Label = "S.No";
            fsstaff.Sheets[0].ColumnHeader.Columns[1].Label = "Staff Name";
            fsstaff.Sheets[0].ColumnHeader.Columns[2].Label = "Staff Code";
            fsstaff.Sheets[0].Columns[0].Width = 80;
            fsstaff.Sheets[0].Columns[1].Width = 300;
            fsstaff.Sheets[0].Columns[2].Width = 200;
            fsstaff.Sheets[0].Columns[0].Locked = true;
            fsstaff.Sheets[0].Columns[1].Locked = true;
            fsstaff.Sheets[0].Columns[2].Locked = true;
            string Master1 = "select * from Master_Settings where usercode=" + Session["usercode"] + "";
            //string Master1 = "select value,settings from Master_Settings where settings='order_by'";
            ds = da.select_method_wo_parameter(Master1, "text");
            Session["Rollflag"] = "0";
            Session["Regflag"] = "0";
            Session["Studflag"] = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int hf = 0; hf < ds.Tables[0].Rows.Count; hf++)
                {
                    if (ds.Tables[0].Rows[hf]["settings"].ToString() == "Roll No" && ds.Tables[0].Rows[hf]["value"].ToString() == "1")
                    {
                        Session["Rollflag"] = "1";
                    }
                    if (ds.Tables[0].Rows[hf]["settings"].ToString() == "Register No" && ds.Tables[0].Rows[hf]["value"].ToString() == "1")
                    {
                        Session["Regflag"] = "1";
                    }
                    if (ds.Tables[0].Rows[hf]["settings"].ToString() == "Student_Type" && ds.Tables[0].Rows[hf]["value"].ToString() == "1")
                    {
                        Session["Studflag"] = "1";
                    }
                }
            }
            if (Session["back"] == "1")
            {
                txtleavereason.Text = Convert.ToString(Session["reason"]);
                txt_to.Text = Convert.ToString(Session["toDate"]);
                txt_to_TextChanged(sender, e);
                txt_frm.Text = Convert.ToString(Session["fromDate"]); //modified by prabha  on jan 25 2018
                txt_frm_TextChanged(sender, e);
                string ltype = Convert.ToString(Session["leaveType"]);
                //ddl_leave_type.SelectedValue = ltype;
                //ddl_leave_type.Items.FindByValue(ltype).Selected = true;
                imgbtn_leave_Click(sender, e);
                ddl_leave_type.SelectedIndex = ddl_leave_type.Items.IndexOf(ddl_leave_type.Items.FindByValue(ltype));//barath 29.01.18
                Session["toDate"] = null;
                Session["leaveType"] = null;
                Session["reason"] = null;
                //foreach (ListItem item in ddl_leave_type.Items) //modified by prabha  on jan 25 2018
                //{
                //    if (item.Value.ToString().ToLower().Trim() == ltype.ToLower().Trim())
                //        item.Selected = true;
                //}
            }
        }
    }

    public void loadtimes()
    {
        int i;
        for (i = 1; i <= 12; i++)
        {
            ddl_hrs.Items.Add(i.ToString());
        }
        for (i = 0; i < 60; i++)
        {
            if (i < 10)
                ddl_mins.Items.Add("0" + i.ToString());
            else if (i >= 10)
                ddl_mins.Items.Add(i.ToString());
        }
        ddl_ampm.Items.Add("AM");
        ddl_ampm.Items.Add("PM");
    }

    protected void lb3_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("default.aspx", false);
    }

    protected void btn_dept_Click(object sender, EventArgs e)
    {
        try
        {
            binddepartment();
            Newdiv.Visible = true;
        }
        catch
        {
        }
    }

    protected void btn_dept1_Click(object sender, EventArgs e)
    {
        try
        {
            binddepartment();
            Newdiv.Visible = true;
        }
        catch
        {
        }
    }

    protected void imagebtnpopclose1_Click(object sender, EventArgs e)
    {
        Newdiv.Visible = false;
    }

    protected void btndept_save(object sender, EventArgs e)
    {
        try
        {
            string strname = "";
            string deptcode = "";
            string departmentcode = "";
            foreach (GridViewRow row in dptgrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("cbcheck") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Label lbldeptcode = (row.Cells[3].FindControl("lbldeptcode") as Label);
                        Label lbldeptname = (row.Cells[3].FindControl("lbldeptname") as Label);
                        if (strname == "")
                        {
                            strname = lbldeptname.Text;
                            deptcode = lbldeptcode.Text;
                        }
                        else
                        {
                            strname = strname + "," + lbldeptname.Text;
                            deptcode = deptcode + "," + lbldeptcode.Text;
                        }
                    }
                }
            }
            Session["departmentcode"] = deptcode;
            txt_serdept.Text = strname;
            txt_dept.Text = strname;
            Newdiv.Visible = false;
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    protected void btndept_exit(object sender, EventArgs e)
    {
        try
        {
            Newdiv.Visible = false;
        }
        catch
        {
        }
    }

    protected void btn_itemsave1_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("ItemCode");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Measure");
            dt.Columns.Add("Quantity");
            if (Session["dt"] != null)
            {
                DataTable d1 = new DataTable();
                d1 = (DataTable)Session["dt"];
                if (d1.Rows.Count > 0)
                {
                    for (int r = 0; r < d1.Rows.Count; r++)
                    {
                        dr = dt.NewRow();
                        for (int c = 0; c < d1.Columns.Count; c++)
                        {
                            dr[c] = Convert.ToString(d1.Rows[r][c]);
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            int count = 0;
            string itemname = "";
            if (selectitemgrid.Rows.Count > 0)
            {
                for (int i = 0; i < selectitemgrid.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = Convert.ToString((selectitemgrid.Rows[i].FindControl("itemcodegv") as Label).Text);
                    dr[1] = Convert.ToString((selectitemgrid.Rows[i].FindControl("itemnamegv") as Label).Text);
                    dr[2] = Convert.ToString((selectitemgrid.Rows[i].FindControl("lbl_measureitem") as Label).Text);
                    // string noofperson = Convert.ToString(txt_noofperson.Text);
                    dr[3] = Convert.ToString("");
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                SelectdptGrid.Visible = true;
                SelectdptGrid.DataSource = dt;
                SelectdptGrid.DataBind();
                griddiv.Visible = true;
                SelectdptGrid.Visible = true;
                Sergrid.Visible = true;
                Sergrid.DataSource = dt;
                Sergrid.DataBind();
                sergriddiv.Visible = true;
                Session["dt"] = dt;
                popwindow1.Visible = false;
                btn_reqclear.Visible = true;
                btn_reqsave.Visible = true;
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    //Saranya devi 12.5.2018
    protected void btn_itemsave4_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt11 = new DataTable();
            DataRow dr;
            DataRow dr1;
            dt.Columns.Add("ItemCode");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Measure");
            dt.Columns.Add("Quantity");
            dt2.Columns.Add("ItemCode");
            dt2.Columns.Add("ItemName");
            dt2.Columns.Add("Measure");
            dt2.Columns.Add("Quantity");
            DataTable d1 = new DataTable();
            if (Session["dt"] != null)
            {
                //DataTable d1 = new DataTable();
                d1 = (DataTable)Session["dt"];
                if (d1.Rows.Count > 0)
                {
                    for (int r = 0; r < d1.Rows.Count; r++)
                    {
                        dr = dt.NewRow();
                        for (int c = 0; c < d1.Columns.Count; c++)
                        {
                            dr[c] = Convert.ToString(d1.Rows[r][c]);
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            //if (Session["dt"] != null)
            //{
            //    DataTable dd = new DataTable();
            //    dd = (DataTable)Session["dt"];
            //    if (dd.Rows.Count > 0)
            //    {
            //        for (int r = 0; r < dd.Rows.Count; r++)
            //        {
            //            dr1 = dd.NewRow();
            //            for (int c = 0; c < dd.Columns.Count; c++)
            //            {
            //                dr1[c] = Convert.ToString(dd.Rows[r][c]);
            //            }
            //            dd.Rows.Add(dr1);
            //        }
            //    }
            //}
            int count = 0;
            foreach (DataListItem gvrow in gvdatass.Items)
            {
                CheckBox chkSelect = (gvrow.FindControl("CheckBox2") as CheckBox);
                if (chkSelect.Checked)
                {
                    chkSelect.Enabled = false;
                }
            }
            if (selectitemgrid.Rows.Count > 0)
            {
                for (int i = 0; i < selectitemgrid.Rows.Count; i++)
                {
                    dr1 = dt2.NewRow();
                    dr1[0] = Convert.ToString((selectitemgrid.Rows[i].FindControl("itemcodegv") as Label).Text);
                    dr1[1] = Convert.ToString((selectitemgrid.Rows[i].FindControl("itemnamegv") as Label).Text);
                    dr1[2] = Convert.ToString((selectitemgrid.Rows[i].FindControl("lbl_measureitem") as Label).Text);
                    dr1[3] = Convert.ToString("");
                    dt2.Rows.Add(dr1);
                }
            }
            string ss = "0";
            DataTable dt3 = dt2.Clone();
            foreach (DataRow row1 in d1.Rows)
            {
                foreach (DataRow row2 in dt2.Rows)
                {
                    if (row1["ItemName"].ToString() == row2["ItemName"].ToString())
                    {
                        ss = "2";
                    }
                }
            }
            if (ss != "2")
            {
                if (selectitemgrid.Rows.Count > 0)
                {
                    for (int i = 0; i < selectitemgrid.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = Convert.ToString((selectitemgrid.Rows[i].FindControl("itemcodegv") as Label).Text);
                        dr[1] = Convert.ToString((selectitemgrid.Rows[i].FindControl("itemnamegv") as Label).Text);
                        dr[2] = Convert.ToString((selectitemgrid.Rows[i].FindControl("lbl_measureitem") as Label).Text);
                        dr[3] = Convert.ToString("");
                        dt.Rows.Add(dr);
                    }
                }
            }
            if (ss == "0")
            {
                if (dt.Rows.Count > 0)
                {
                    SelectdptGrid.Visible = true;
                    SelectdptGrid.DataSource = dt;
                    SelectdptGrid.DataBind();
                    griddiv.Visible = true;
                    SelectdptGrid.Visible = true;
                    Sergrid.Visible = true;
                    Sergrid.DataSource = dt;
                    Sergrid.DataBind();
                    sergriddiv.Visible = true;
                    Session["dt"] = dt;
                    popwindow1.Visible = false;
                    btn_reqclear.Visible = true;
                    btn_reqsave.Visible = true;
                }
            }
            if (ss == "2")
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "This Item Already Added";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    SelectdptGrid.Visible = true;
                    SelectdptGrid.DataSource = dt;
                    SelectdptGrid.DataBind();
                    griddiv.Visible = true;
                    SelectdptGrid.Visible = true;
                    Sergrid.Visible = true;
                    Sergrid.DataSource = dt;
                    Sergrid.DataBind();
                    sergriddiv.Visible = true;
                    Session["dt"] = dt;
                    popwindow1.Visible = false;
                    btn_reqclear.Visible = true;
                    btn_reqsave.Visible = true;
                }
                else
                {
                    SelectdptGrid.Visible = true;
                    SelectdptGrid.DataSource = dt2;
                    SelectdptGrid.DataBind();
                    griddiv.Visible = true;
                    SelectdptGrid.Visible = true;
                    Sergrid.Visible = true;
                    Sergrid.DataSource = dt2;
                    Sergrid.DataBind();
                    sergriddiv.Visible = true;
                    Session["dt"] = dt2;
                    popwindow1.Visible = false;
                    btn_reqclear.Visible = true;
                    btn_reqsave.Visible = true;
                }
            }
            btn_reqclear.Visible = true;
            btn_reqsave.Visible = true;
            if (count == 0)
            {
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public object sender { get; set; }

    public EventArgs e { get; set; }

    public void bindtable()
    {
        dt.Columns.Add("S.No");
        dt.Columns.Add("Item Name");
        dt.Columns.Add("ItemCode");
        dt.Columns.Add("Header Name");
        dt.Columns.Add("Header code");
        dt.Columns.Add("Item unit");
        dt.TableName = "selecteditems";
    }

    protected void selectedmenuchk(object sender, EventArgs e)
    {
        int count = 0;
        bindtable();
        string box = "";
        if (checknew == "s")
        {
            if (ViewState["sb"] != null)
            {
                DataTable dts = (DataTable)ViewState["sb"];
                DataView dv = new DataView(dts);
                dt = dv.ToTable();
                dr = null;
                // checknew = "";
            }
        }
        else
        {
        }
        dt.Clear();
        foreach (DataListItem gvrow in gvdatass.Items)
        {
            CheckBox chkSelect = (gvrow.FindControl("CheckBox2") as CheckBox);
            if (chkSelect.Checked)
            {
                //chkSelect.Enabled = false;
                count++;
                dr = dt.NewRow();
                string itemcode = "";
                string itemnamegv = "";
                string itemheadername = "";
                dr[0] = Convert.ToString(count);
                Label lbl_itemname = (Label)gvrow.FindControl("lbl_itemname");
                itemnamegv = lbl_itemname.Text;
                dr[1] = itemnamegv;
                Label lbl_itemcode = (Label)gvrow.FindControl("lbl_itemcode");
                itemcode = lbl_itemcode.Text;
                dr[2] = itemcode;
                Label lbl_headername = (Label)gvrow.FindControl("lblitemheadername");
                itemheadername = lbl_headername.Text;
                dr[3] = itemheadername;
                Label lbl_itemheadercode = (Label)gvrow.FindControl("lbl_itemheadercode");
                string itemheadercode = lbl_itemheadercode.Text;
                dr[4] = itemheadercode;
                Label lbl_measureitem = (Label)gvrow.FindControl("lbl_measureitem");
                string measureitem = lbl_measureitem.Text;
                //if(measureitem.Trim()!="")
                //{
                dr[5] = measureitem;
                //}
                if (SelectdptGrid.Visible == true)
                {
                    for (int i = 0; i < SelectdptGrid.Rows.Count; i++)
                    {
                        Label box1 = (Label)SelectdptGrid.Rows[i].Cells[1].FindControl("lbl_itemcode");
                        box = Convert.ToString(box1.Text);
                        if (box != itemcode)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataView d = new DataView(dt);
                                d.RowFilter = "ItemCode ='" + itemcode + "'";
                                if (d.Count == 0)
                                {
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                dt.Rows.Add(dr);
                            }
                            // dt = dt.DefaultView.ToTable(true, "Item Name", "Item Code", "Header Name", "Header code", "Item unit");
                            //ViewState["selecteditems"] = dt;
                            selectitemgrid.DataSource = dt;
                            selectitemgrid.DataBind();
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataView d = new DataView(dt);
                        d.RowFilter = "ItemCode ='" + itemcode + "'";
                        if (d.Count == 0)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                    }
                    selectitemgrid.DataSource = dt;
                    selectitemgrid.DataBind();
                }
            }
            else
            {
            }
        }
        selectitemgrid.DataSource = dt;
        selectitemgrid.DataBind();
        ViewState["selecteditems"] = dt;
        ViewState["selecteditems1"] = dt;
    }

    public void serbindtable()
    {
        dt.Columns.Add("S.No");
        dt.Columns.Add("Item Name");
        dt.Columns.Add("ItemCode");
        dt.Columns.Add("Header Name");
        dt.Columns.Add("Header code");
        dt.Columns.Add("Item unit");
        dt.TableName = "selecteditems1";
    }

    //Saran 30.11.2018
    protected void btn_additem_Click(object sender, EventArgs e)
    {
        ViewState["selecteditems"] = null;
        ViewState["selecteditem"] = null;
        selectitemgrid.DataSource = null;
        selectitemgrid.DataBind();
        //div2.Visible = false;
        //gvdatass.DataSource = null;
        //gvdatass.DataBind();
        data.Clear();
        popwindow1.Visible = true;
        btn_go1_Click(sender, e);
        txt_searchby.Visible = true;
        txt_searchitemcode.Visible = false;
        ddl_type.SelectedValue = "0";
        itemmaster();
        itemheader();
        loadsubheadername();
    }

    protected void btn_seradditem_Click(object sender, EventArgs e)
    {
        ViewState["selecteditems"] = null;
        Sergrid.DataSource = null;
        Sergrid.DataBind();
        ViewState["selecteditems1"] = null;
        selectitemgrid.DataSource = null;
        selectitemgrid.DataBind();
        div2.Visible = false;
        gvdatass.DataSource = null;
        gvdatass.DataBind();
        popwindow1.Visible = true;
        btn_go1_Click(sender, e);
        txt_searchby.Visible = true;
    }

    protected void btn_go1_Click(object sender, EventArgs e)
    {
        try
        {
            itemheader();
            rbcheck();
            if (div_itmreqst.Visible == true)
            {
                if (ViewState["selecteditems"] != null)
                {
                    DataTable dnew = (DataTable)ViewState["selecteditems"];
                    ViewState["sb"] = dnew;
                    checknew = "s";
                }
            }
            else if (div_service.Visible == true)
            {
                if (ViewState["selecteditems1"] != null)
                {
                    DataTable dnew = (DataTable)ViewState["selecteditems1"];
                    ViewState["sb"] = dnew;
                    checknew = "s";
                }
            }
            string itemheadercode = "";
            for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
            {
                if (cbl_itemheader3.Items[i].Selected == true)
                {
                    if (itemheadercode == "")
                    {
                        itemheadercode = "" + cbl_itemheader3.Items[i].Value.ToString() + "";
                    }
                    else
                    {
                        itemheadercode = itemheadercode + "'" + "," + "'" + cbl_itemheader3.Items[i].Value.ToString() + "";
                    }
                }
            }
            string itemheadercode1 = "";
            for (int i = 0; i < chklst_pop2itemtyp.Items.Count; i++)
            {
                if (chklst_pop2itemtyp.Items[i].Selected == true)
                {
                    if (itemheadercode1 == "")
                    {
                        itemheadercode1 = "" + chklst_pop2itemtyp.Items[i].Value.ToString() + "";
                    }
                    else
                    {
                        itemheadercode1 = itemheadercode1 + "'" + "," + "'" + chklst_pop2itemtyp.Items[i].Value.ToString() + "";
                    }
                }
            }
            string selectquery = "";
            string Item_Type = Session["rbvalue"].ToString();
            if (txt_searchby.Text.Trim() != "")
            {
                selectquery = "select itemheader_name,itemheader_code,item_code,item_name ,model_name,Size_name ,item_unit,description ,special_instru from Item_Master where item_name='" + txt_searchby.Text + "'  order by item_code";
            }
            else if (txt_searchitemcode.Text.Trim() != "")
            {
                selectquery = "select itemheader_name,itemheader_code,item_code,item_name ,model_name,Size_name ,item_unit,description ,special_instru from Item_Master where item_code='" + txt_searchitemcode.Text + "' order by item_code";
            }
            else if (txt_searchheadername.Text.Trim() != "")
            {
                selectquery = "select itemheader_name,itemheader_code,item_code,item_name ,model_name,Size_name ,item_unit,description ,special_instru from Item_Master where itemheader_name='" + txt_searchheadername.Text + "' order by item_code";
            }
            else if (itemheadercode.Trim() != "" && itemheadercode1.Trim() != "")
            {
                selectquery = "select distinct  item_code ,item_name , itemheader_code,itemheader_name,item_unit from item_master where itemheader_code in ('" + itemheadercode + "') and item_code in ('" + itemheadercode1 + "')  order by item_code ";
            }
            if (selectquery.Trim() != "")
            {
                ds.Clear();
                ds = d2.select_method_wo_parameter(selectquery, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvdatass.DataSource = ds.Tables[0];
                    gvdatass.DataBind();
                    gvdatass.Visible = true;
                    div2.Visible = true;
                }
            }
            //else
            //{
            //    gvdatass.DataSource = ds.Tables[0];
            //    gvdatass.DataBind();
            //    //imgdiv2.Visible = true;
            //    //lbl_alert.Text = "No Records Found";
            //}
            txt_searchby.Text = "";
            txt_searchitemcode.Text = "";
            txt_searchheadername.Text = "";
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    protected void cbselectAll_Change(object sender, EventArgs e)
    {
        try
        {
            if (cbselectall.Checked == true)
            {
                if (dptgrid.Rows.Count > 0)
                {
                    for (int i = 0; i < dptgrid.Rows.Count; i++)
                    {
                        (dptgrid.Rows[i].FindControl("cbcheck") as CheckBox).Checked = true;
                    }
                }
            }
            if (cbselectall.Checked == false)
            {
                if (dptgrid.Rows.Count > 0)
                {
                    for (int i = 0; i < dptgrid.Rows.Count; i++)
                    {
                        (dptgrid.Rows[i].FindControl("cbcheck") as CheckBox).Checked = false;
                    }
                }
            }
        }
        catch
        {
        }
    }

    public void binddepartment()
    {
        try
        {
            string deptquery = "";
            bool Is_Staff;
            string staff_code = "";
            string deptcode = "";
            string otherdeptcode = "";
            Is_Staff = Convert.ToBoolean(d2.GetFunction("select Is_Staff from UserMaster where User_Code='" + usercode + "' and college_code='" + collegecode1 + "'"));
            staff_code = d2.GetFunction("select staff_code from UserMaster where User_Code='" + usercode + "' and college_code='" + collegecode1 + "'");
            otherdeptcode = d2.GetFunction("select  dm.dept_code from desig_master dm,staff_appl_master sa,staffmaster sm,UserMaster um where sa.appl_no=sm.appl_no and sa.desig_code=dm.desig_code and sm.staff_code=um.staff_code and um.is_staff=1 and um.staff_code='" + staff_code + "' and um.college_code='" + collegecode1 + "'");
            string[] split = otherdeptcode.Split(';');
            for (int i = 0; i < split.Length; i++)
            {
                if (deptcode == "")
                {
                    deptcode = split[i];
                }
                else
                {
                    deptcode = deptcode + "'" + "," + "'" + split[i];
                }
            }
            CheckBox cbown = new CheckBox();
            CheckBox cbother = new CheckBox();
            if (div_itmreqst.Visible == true)
            {
                cbown = cb_own;
                cbother = cb_other;
            }
            else if (div_service.Visible == true)
            {
                cbown = cb_serown;
                cbother = cb_serother;
            }
            string sql = d2.GetFunction("");
            if (Is_Staff == true)
            {
                if (cbown.Checked == true)
                {
                    deptquery = "select distinct hr.dept_code as DeptCode,hr.Dept_Name as DeptName from hrdept_master hr,UserMaster um,staffmaster sm,staff_appl_master sa where um.staff_code=sm.staff_code and sm.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and um.is_staff=1 and um.staff_code='" + staff_code + "' and hr.college_code ='" + collegecode1 + "' order by hr.dept_code ";
                }
                else if (cbother.Checked == true)
                {
                    // deptquery = "select distinct hr.dept_code as DeptCode,hr.Dept_Name as DeptName from hrdept_master hr,UserMaster um,staffmaster sm,staff_appl_master sa,desig_master dm where sa.desig_code=dm.desig_code and um.staff_code=sm.staff_code and sm.appl_no=sa.appl_no   and um.is_staff=1 and um.staff_code='" + staff_code + "' and hr.dept_code   in('" + deptcode + "') and hr.college_code ='" + collegecode1 + "' order by hr.dept_code";
                    deptquery = "select distinct dept_code as DeptCode,dept_name as DeptName from hrdept_master where college_code='" + collegecode1 + "' order by Dept_Code";
                }
                else if (cbown.Checked == true && cbother.Checked == true)
                {
                    deptquery = "select distinct dept_code as DeptCode,dept_name as DeptName from hrdept_master where college_code='" + collegecode1 + "' order by Dept_Code";
                }
                else
                {
                    deptquery = "select distinct hr.dept_code as DeptCode,hr.Dept_Name as DeptName from hrdept_master hr,UserMaster um,staffmaster sm,staff_appl_master sa,desig_master dm where sa.desig_code=dm.desig_code and um.staff_code=sm.staff_code and sm.appl_no=sa.appl_no   and um.is_staff=1 and um.staff_code='" + staff_code + "' and hr.dept_code   in('" + deptcode + "') and hr.college_code ='" + collegecode1 + "' order by hr.dept_code";
                }
            }
            else if (Is_Staff == false)
            {
                deptquery = "select distinct dept_code as DeptCode,dept_name as DeptName from hrdept_master where college_code='" + collegecode1 + "' order by Dept_Code";
            }
            //select Dept_Code as DeptCode ,Dept_Name as DeptName from Department where college_code ='" + collegecode1 + "' order by Dept_Code ";
            ds.Clear();
            ds = d2.select_method_wo_parameter(deptquery, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                dptgrid.DataSource = ds;
                dptgrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void rbcheck()
    {
        string rbvalue = "";
        if (div_itmreqst.Visible == true)
        {
            Session["rbvalue"] = "0,1";
            Item_Type = "0,1";
        }
        else if (div_service.Visible == true)
        {
            Session["rbvalue"] = "1";
            Item_Type = "1";
        }
        else if (div_visitor.Visible == true)
        {
            btn_reqsave.Visible = true;
            btn_reqclear.Visible = true;
        }
        else if (div_complaints.Visible == true)
        {
            btn_reqsave.Visible = true;
            btn_reqclear.Visible = true;
        }
        // mainclear();
    }

    protected void SelectdptGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int row = Convert.ToInt32(e.CommandArgument);
            Session["rowvalue"] = Convert.ToString(row);
            if (e.CommandName == "instruction")
            {
                string itemcode = ((SelectdptGrid.Rows[row].FindControl("lbl_itemcode") as Label).Text);
                string itemname = ((SelectdptGrid.Rows[row].FindControl("lbl_itemname") as Label).Text);
                string qunatity = ((SelectdptGrid.Rows[row].FindControl("lblquantity") as Label).Text);
                // btn_additem2.Text = "Update";
                Session["itemnewcode"] = Convert.ToString(itemcode);
            }
        }
        catch
        {
        }
    }

    protected void typegrid_OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this.SelectdptGrid, "instruction$" + e.Row.RowIndex);
                //e.Row.Cells[1].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this.SelectdptGrid, "instruction$" + e.Row.RowIndex);
                //e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this.SelectdptGrid, "instruction$" + e.Row.RowIndex);
                //e.Row.Cells[3].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this.SelectdptGrid, "instruction$" + e.Row.RowIndex);
            }
        }
        catch
        {
        }
    }

    protected void serSelectdptGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int row = Convert.ToInt32(e.CommandArgument);
            Session["rowvalue"] = Convert.ToString(row);
            if (e.CommandName == "instruction")
            {
                string itemcode = ((Sergrid.Rows[row].FindControl("lbl_seritemcode") as Label).Text);
                string itemname = ((Sergrid.Rows[row].FindControl("lbl_seritemname") as Label).Text);
                string qunatity = ((Sergrid.Rows[row].FindControl("lblserquantity") as Label).Text);
                // btn_additem2.Text = "Update";
                Session["itemnewcode"] = Convert.ToString(itemcode);
            }
        }
        catch
        {
        }
    }

    protected void sertypegrid_OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = (DropDownList)e.Row.FindControl("gridddl_sugvendor");
                // int vendorID = Convert.ToInt32(e.Row.Cells[3].Text);
                string sql = "select id,vendor_name from vendor_details where vendor_type='Approved'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddl.DataSource = ds;
                ddl.DataTextField = "vendor_name";
                ddl.DataValueField = "id";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch
        {
        }
    }

    protected void imagebtnpopclose_Click(object sender, EventArgs e)
    {
        popwindow1.Visible = false;
    }

    protected void cb_subheadername_CheckedChange(object sender, EventArgs e)
    {
        try
        {
            if (cb_subheadername.Checked == true)
            {
                for (int i = 0; i < cbl_subheadername.Items.Count; i++)
                {
                    cbl_subheadername.Items[i].Selected = true;
                }
                txt_subheadername.Text = "Sub Header Name(" + (cbl_subheadername.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < cbl_subheadername.Items.Count; i++)
                {
                    cbl_subheadername.Items[i].Selected = false;
                }
                txt_subheadername.Text = "--Select--";
            }
            // loadsubheadername();
            itemmaster();
        }
        catch (Exception ex)
        {
        }
    }

    protected void cbl_subheadername_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txt_subheadername.Text = "--Select--";
            cb_subheadername.Checked = false;
            int commcount = 0;
            for (int i = 0; i < cbl_subheadername.Items.Count; i++)
            {
                if (cbl_subheadername.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            if (commcount > 0)
            {
                txt_subheadername.Text = "Sub Header Name(" + commcount.ToString() + ")";
                if (commcount == cbl_subheadername.Items.Count)
                {
                    cb_subheadername.Checked = true;
                }
            }
            itemmaster();
        }
        catch (Exception ex)
        {
        }
    }

    public void loadsubheadername()
    {
        try
        {
            cbl_subheadername.Items.Clear();
            string itemheader = "";
            for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
            {
                if (cbl_itemheader3.Items[i].Selected == true)
                {
                    if (itemheader == "")
                    {
                        itemheader = "" + cbl_itemheader3.Items[i].Value.ToString() + "";
                    }
                    else
                    {
                        itemheader = itemheader + "'" + "," + "" + "'" + cbl_itemheader3.Items[i].Value.ToString() + "";
                    }
                }
            }
            if (itemheader.Trim() != "")
            {
                string query = "";
                query = "select distinct t.MasterCode,t.MasterValue from CO_MasterValues  t,IM_ItemMaster i where t.MasterCode=i.subheader_code and ItemHeaderCode in ('" + itemheader + "')";
                ds.Clear();
                ds = d2.select_method_wo_parameter(query, "Text");
                // ds.Clear();
                // ds = d2.BindItemCodeAll(itemheader);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbl_subheadername.DataSource = ds;
                    cbl_subheadername.DataTextField = "MasterValue";
                    cbl_subheadername.DataValueField = "MasterCode";
                    cbl_subheadername.DataBind();
                    if (cbl_subheadername.Items.Count > 0)
                    {
                        for (int i = 0; i < cbl_subheadername.Items.Count; i++)
                        {
                            cbl_subheadername.Items[i].Selected = true;
                        }
                        txt_subheadername.Text = "Sub Header Name(" + cbl_subheadername.Items.Count + ")";
                    }
                    if (cbl_subheadername.Items.Count > 5)
                    {
                        Panel5.Width = 300;
                        Panel5.Height = 300;
                    }
                }
                else
                {
                    txt_subheadername.Text = "--Select--";
                }
            }
            else
            {
                txt_subheadername.Text = "--Select--";
            }
        }
        catch
        {
        }
    }

    protected void cb_itemheader3_CheckedChange(object sender, EventArgs e)
    {
        int cout = 0;
        txt_itemheader3.Text = "--Select--";
        if (cb_itemheader3.Checked == true)
        {
            cout++;
            for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
            {
                cbl_itemheader3.Items[i].Selected = true;
            }
            txt_itemheader3.Text = "Item Header(" + (cbl_itemheader3.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
            {
                cbl_itemheader3.Items[i].Selected = false;
            }
        }
        loadsubheadername();
        itemmaster();
    }

    protected void cbl_itemheader_SelectedIndexChange(object sender, EventArgs e)
    {
        int i = 0;
        int commcount = 0;
        txt_itemheader3.Text = "--Select--";
        for (i = 0; i < cbl_itemheader3.Items.Count; i++)
        {
            if (cbl_itemheader3.Items[i].Selected == true)
            {
                commcount = commcount + 1;
                cb_itemheader3.Checked = false;
            }
        }
        if (commcount > 0)
        {
            if (commcount == cbl_itemheader3.Items.Count)
            {
                cb_itemheader3.Checked = true;
            }
            txt_itemheader3.Text = "Item Header(" + commcount.ToString() + ")";
        }
        loadsubheadername();
        itemmaster();
    }

    protected void chklstitemtyp(object sender, EventArgs e)
    {
        int i = 0;
        chk_pop2itemtyp.Checked = false;
        int commcount = 0;
        txt_itemname3.Text = "--Select--";
        for (i = 0; i < chklst_pop2itemtyp.Items.Count; i++)
        {
            if (chklst_pop2itemtyp.Items[i].Selected == true)
            {
                commcount = commcount + 1;
            }
        }
        if (commcount > 0)
        {
            if (commcount == chklst_pop2itemtyp.Items.Count)
            {
                chk_pop2itemtyp.Checked = true;
            }
            txt_itemname3.Text = "Item Name(" + commcount.ToString() + ")";
        }
    }

    protected void chkitemtyp(object sender, EventArgs e)
    {
        int cout = 0;
        if (chk_pop2itemtyp.Checked == true)
        {
            cout++;
            for (int i = 0; i < chklst_pop2itemtyp.Items.Count; i++)
            {
                chklst_pop2itemtyp.Items[i].Selected = true;
            }
            txt_itemname3.Text = "Item Name(" + (chklst_pop2itemtyp.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < chklst_pop2itemtyp.Items.Count; i++)
            {
                chklst_pop2itemtyp.Items[i].Selected = false;
            }
            txt_itemname3.Text = "--Select--";
        }
    }

    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedValue == "0")
        {
            txt_searchby.Visible = true;
            txt_searchitemcode.Visible = false;
            txt_searchheadername.Visible = false;
            txt_searchheadername.Text = "";
            txt_searchitemcode.Text = "";
        }
        else if (ddl_type.SelectedValue == "1")
        {
            txt_searchby.Visible = false;
            txt_searchitemcode.Visible = true;
            txt_searchheadername.Visible = false;
            txt_searchby.Text = "";
            txt_searchheadername.Text = "";
        }
        else if (ddl_type.SelectedValue == "2")
        {
            txt_searchby.Visible = false;
            txt_searchitemcode.Visible = false;
            txt_searchheadername.Visible = true;
            txt_searchby.Text = "";
            txt_searchitemcode.Text = "";
        }
    }

    protected void btn_conexit1_Click(object sender, EventArgs e)
    {
        popwindow1.Visible = false;
    }

    protected void btn_errorclose_Click(object sender, EventArgs e)
    {
        try
        {
            imgdiv2.Visible = false;
            if (chkrelived == 1)
            {
                Response.Redirect("HierarchySettingReport.aspx");
            }
            chkrelived = 0;
            if (saveclear == 1)
            {
                // delsi 03.05.2018
                //Btn_Cancel_Click(sender, e); 
                Session["alters"] = "";
                Session["staconform"] = "";
                Session["alter_done"] = "";
                Session["conformleave"] = "";

            }
            saveclear = 0;
            ItemReqNo();


        }
        catch (Exception ex)
        {
        }
    }

    public void itemheader()
    {
        try
        {
            cbl_itemheader3.Items.Clear();
            string group_code = Session["group_code"].ToString();
            string columnfield = "";
            if (group_code.Contains(';'))
            {
                string[] group_semi = group_code.Split(';');
                group_code = group_semi[0].ToString();
            }
            if ((group_code.ToString().Trim() != "") && (Session["single_user"].ToString() != "1" && Session["single_user"].ToString() != "true" && Session["single_user"].ToString() != "TRUE" && Session["single_user"].ToString() != "True"))
            {
                columnfield = " and group_code='" + group_code + "'";
            }
            else
            {
                columnfield = " and usercode='" + Session["usercode"] + "'";
            }
            string maninvalue = "";
            string selectnewquery = d2.GetFunction("select value from Master_Settings where settings='ItemHeaderRights' " + columnfield + "");
            if (selectnewquery.Trim() != "" && selectnewquery.Trim() != "0")
            {
                string[] splitnew = selectnewquery.Split(',');
                if (splitnew.Length > 0)
                {
                    for (int row = 0; row <= splitnew.GetUpperBound(0); row++)
                    {
                        if (maninvalue == "")
                        {
                            maninvalue = Convert.ToString(splitnew[row]);
                        }
                        else
                        {
                            maninvalue = maninvalue + "'" + "," + "'" + Convert.ToString(splitnew[row]);
                        }
                    }
                }
            }
            //if (maninvalue.Trim() != "")
            //{
            ds.Clear();
            //  ds = d2.BindItemHeaderWithRights();
            string query = "";
            query = "select distinct ItemHeaderCode,ItemHeaderName  from IM_ItemMaster ";
            ds.Clear();
            ds = d2.select_method_wo_parameter(query, "Text");
            //}
            //else
            //{
            //    ds.Clear();
            //    ds = d2.BindItemHeaderWithOutRights();
            //}
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_itemheader3.DataSource = ds;
                cbl_itemheader3.DataTextField = "ItemHeaderName";
                cbl_itemheader3.DataValueField = "ItemHeaderCode";
                cbl_itemheader3.DataBind();
                //cbl_itm_hdrname.DataSource = ds;
                //cbl_itm_hdrname.DataTextField = "ItemHeaderName";
                //cbl_itm_hdrname.DataValueField = "ItemHeaderCode";
                //cbl_itm_hdrname.DataBind();
                if (cbl_itemheader3.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
                    {
                        cbl_itemheader3.Items[i].Selected = true;
                    }
                    txt_itemheader3.Text = "Item Header(" + cbl_itemheader3.Items.Count + ")";
                }
                //if (cbl_itm_hdrname.Items.Count > 0)
                //{
                //    for (int i = 0; i < cbl_itm_hdrname.Items.Count; i++)
                //    {
                //        cbl_itm_hdrname.Items[i].Selected = true;
                //    }
                //    txt_itn_hdr.Text = "Item Header(" + cbl_itm_hdrname.Items.Count + ")";
                //}
            }
            else
            {
                txt_itemheader3.Text = "--Select--";
            }
        }
        catch
        {
        }
    }

    public void itemmaster()
    {
        chklst_pop2itemtyp.Items.Clear();
        string itemheadercode = "";
        string subheader = "";
        for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
        {
            if (cbl_itemheader3.Items[i].Selected == true)
            {
                if (itemheadercode == "")
                {
                    itemheadercode = "" + cbl_itemheader3.Items[i].Value.ToString() + "";
                }
                else
                {
                    itemheadercode = itemheadercode + "'" + "," + "'" + cbl_itemheader3.Items[i].Value.ToString() + "";
                }
            }
        }
        for (int i = 0; i < cbl_subheadername.Items.Count; i++)
        {
            if (cbl_subheadername.Items[i].Selected == true)
            {
                if (subheader == "")
                {
                    subheader = "" + cbl_subheadername.Items[i].Value.ToString() + "";
                }
                else
                {
                    subheader = subheader + "'" + "," + "" + "'" + cbl_subheadername.Items[i].Value.ToString() + "";
                }
            }
        }
        if (itemheadercode.Trim() != "" && subheader.Trim() != "")
        {
            // ds.Clear();
            //  ds = d2.BindItemCodewithsubheader(itemheadercode, subheader);
            string query = "";
            query = "select distinct ItemCode,ItemName  from IM_ItemMaster where ItemHeaderCode in ('" + itemheadercode + "') and subheader_code in ('" + subheader + "')";
            ds.Clear();
            ds = d2.select_method_wo_parameter(query, "Text");
            chklst_pop2itemtyp.Items.Clear();
            //if (itemheadercode.Trim() != "")
            //{
            //    ds.Clear();
            //    ds = d2.BindItemCode(itemheadercode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                chklst_pop2itemtyp.DataSource = ds;
                chklst_pop2itemtyp.DataTextField = "ItemName";
                chklst_pop2itemtyp.DataValueField = "ItemCode";
                chklst_pop2itemtyp.DataBind();
                if (chklst_pop2itemtyp.Items.Count > 0)
                {
                    for (int i = 0; i < chklst_pop2itemtyp.Items.Count; i++)
                    {
                        chklst_pop2itemtyp.Items[i].Selected = true;
                    }
                    txt_itemname3.Text = "Item Name(" + chklst_pop2itemtyp.Items.Count + ")";
                }
            }
            else
            {
                txt_itemname3.Text = "--Select--";
            }
        }
        else
        {
            txt_itemname3.Text = "--Select--";
        }
    }

    protected void btn_reqsave_Click(object sender, EventArgs e)
    {
        try
        {
            string req_type = "";
            int RequestType = 0;
            string RequestCode = "";
            string query = "";
            Int64 ReqStaffAppNo = 0;
            Int64 ReqStaffDeptFK = 0;
            bool Is_Staff;
            int q = 0;
            int q1 = 0;
            string Remarks = "";
            dept();
            Is_Staff = Convert.ToBoolean(d2.GetFunction("select Is_Staff from UserMaster where User_Code='" + usercode + "' and college_code='" + collegecode1 + "'"));
            if (Is_Staff == true)
            {
                string staffcode = d2.GetFunction("select staff_code from UserMaster where User_Code='" + usercode + "'");
                if (staffcode.Trim() != "")
                {
                    ReqStaffAppNo = Convert.ToInt64(d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staffcode + "'"));
                }
            }
            else if (Is_Staff == false)
            {
                ReqStaffAppNo = Convert.ToInt64(usercode);
            }
            ReqStaffDeptFK = Convert.ToInt64(d2.GetFunction("select distinct hr.dept_code as DeptCode,hr.Dept_Name as DeptName from hrdept_master hr,UserMaster um,staffmaster sm,staff_appl_master sa where um.staff_code=sm.staff_code and sm.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and um.is_staff=1 and um.staff_code='" + usercode + "' and hr.college_code ='" + collegecode1 + "'"));
            if (div_itmreqst.Visible == true)
            {
                #region Item Request
                RequestType = 1;
                DateTime RequestDate = new DateTime();
                RequestDate = TextToDate(txt_date);
                DateTime ReqExpectedDate = new DateTime();
                ReqExpectedDate = TextToDate(txt_exdate);
                RequestCode = txt_reqno.Text;
                Remarks = txt_reqremarks.Text;
                if (cb_own.Checked == true && cb_other.Checked == true)
                {
                    req_type = "3";
                }
                else if (cb_own.Checked == true)
                {
                    req_type = "1";
                }
                else if (cb_other.Checked == true)
                {
                    req_type = "2";
                }
                if (txt_dept.Text != "")
                {
                    int cnt = 0;
                    for (int i = 0; i < SelectdptGrid.Rows.Count; i++)
                    {
                        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)SelectdptGrid.Rows[i].FindControl("cb_select");
                        if (chk.Checked == true)
                        {
                            cnt++;
                        }
                    }
                    if (cnt == 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Select Request Items";
                        return;
                    }

                    query = "insert into RQ_Requisition(RequestType,RequestCode,RequestDate,ReqExpectedDate,ReqAppNo,Remarks,ReqApproveStage,MemType,RequestBy) values('" + RequestType + "','" + RequestCode + "','" + RequestDate + "','" + ReqExpectedDate + "','" + ReqStaffAppNo + "','" + Remarks + "','0','2','" + req_type + "')";
                    q = d2.update_method_wo_parameter(query, "TEXT");
                    Int64 RequisitionFK = Convert.ToInt64(d2.GetFunction("select distinct top (1) RequisitionPK from RQ_Requisition where RequestCode='" + RequestCode + "' and RequestType='" + RequestType + "' order by RequisitionPK desc"));
                    string deptcode = Session["departmentcode"].ToString();
                    string[] split = deptcode.Split(',');
                    //Int64 itemsplit = Convert.ToInt64(split);
                    for (int code = 0; code < split.Length; code++)
                    {
                        Int64 DeptFK = Convert.ToInt64(split[code]);

                        for (int i = 0; i < SelectdptGrid.Rows.Count; i++)
                        {
                            System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)SelectdptGrid.Rows[i].FindControl("cb_select");
                            if (chk.Checked == true)
                            {
                                string itemname = Convert.ToString((SelectdptGrid.Rows[i].FindControl("lbl_itemname") as Label).Text);
                                string itemcode = Convert.ToString((SelectdptGrid.Rows[i].FindControl("lbl_itemcode") as Label).Text);
                                Int64 ItemFK = Convert.ToInt64(d2.GetFunction("select ItemPK from IM_ItemMaster where ItemCode='" + itemcode + "'"));
                                double ReqQty = 0;

                                double.TryParse((SelectdptGrid.Rows[i].FindControl("txt_quantity") as TextBox).Text, out ReqQty);
                                //  Int64 ReqQty =Convert.Int64((SelectdptGrid.Rows[i].FindControl("txt_quantity") as TextBox).Text);
                                if (ReqQty != null && ReqQty != 0.0)
                                {
                                    string updatequery = "INSERT INTO RQ_RequisitionDet(ItemFK,ReqQty,RequisitionFK,DeptFK,RequestQty) values ('" + ItemFK + "','" + ReqQty + "','" + RequisitionFK + "','" + DeptFK + "','" + ReqQty + "')";
                                    q1 = d2.update_method_wo_parameter(updatequery, "Text");
                                }
                                else
                                {
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "Please Fill The Quantity Value of" + itemname;
                                    return;
                                }

                            }
                        }

                    }
                    if (q != 0 && q1 != 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Saved Successfully";
                        ItemReqNo();
                        mainclear();
                        SelectdptGrid.DataSource = null;
                        SelectdptGrid.DataBind();
                        Session["dt"] = null;
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select any department";
                }
                #endregion
            }
            else if (div_service.Visible == true)
            {
                #region Service
                RequestType = 2;
                DateTime RequestDate = new DateTime();
                RequestDate = TextToDate(txt_serreqdate);
                DateTime ReqExpectedDate = new DateTime();
                ReqExpectedDate = TextToDate(txt_serexpdate);
                RequestCode = txt_serreqno.Text;
                Remarks = txt_serremarks.Text;
                if (cb_serown.Checked == true && cb_serother.Checked == true)
                {
                    req_type = "3";
                }
                else if (cb_serown.Checked == true)
                {
                    req_type = "1";
                }
                else if (cb_serother.Checked == true)
                {
                    req_type = "2";
                }
                // Int64 SugVendorFK = Convert.ToInt64(d2.GetFunction("select id from vendor_details where vendor_name like '" + txt_sersugvendor.Text + "'"));
                if (txt_serdept.Text != "")
                {
                    query = "insert into RQ_Requisition(RequestType,RequestCode,RequestDate,ReqExpectedDate,ReqAppNo,Remarks,MemType,ReqApproveStage,RequestBy) values('" + RequestType + "','" + RequestCode + "','" + RequestDate + "','" + ReqExpectedDate + "','" + ReqStaffAppNo + "','" + Remarks + "','2','0','" + req_type + "')";
                    q = d2.update_method_wo_parameter(query, "TEXT");
                    Int64 RequisitionFK = Convert.ToInt64(d2.GetFunction("select distinct top (1) RequisitionPK from RQ_Requisition where RequestCode='" + RequestCode + "' and RequestType='" + RequestType + "' order by RequisitionPK desc"));
                    string deptcode = Session["departmentcode"].ToString();
                    string[] split = deptcode.Split(',');
                    //Int64 itemsplit = Convert.ToInt64(split);
                    for (int code = 0; code < split.Length; code++)
                    {
                        Int64 DeptFK = Convert.ToInt64(split[code]);
                        for (int i = 0; i < Sergrid.Rows.Count; i++)
                        {
                            DropDownList ddlNew = (DropDownList)Sergrid.Rows[i].FindControl("gridddl_loc");
                            int SugServiceLoc = Convert.ToInt16(ddlNew.SelectedItem.Value);
                            DropDownList ddlvendor = (DropDownList)Sergrid.Rows[i].FindControl("gridddl_sugvendor");
                            Int64 SugVendorFK = Convert.ToInt64(ddlvendor.SelectedItem.Value);
                            string itemcode = Convert.ToString((Sergrid.Rows[i].FindControl("lbl_seritemcode") as Label).Text);
                            Int64 ItemFK = Convert.ToInt64(d2.GetFunction("select ItemPK from IM_ItemMaster where ItemCode='" + itemcode + "'"));
                            Int64 ReqQty = Convert.ToInt64((Sergrid.Rows[i].FindControl("txt_serquantity") as TextBox).Text);
                            if (ReqQty != null)
                            {
                                string updatequery = "INSERT INTO RQ_RequisitionDet(ItemFK,ReqQty,RequisitionFK,DeptFK,SugServiceLoc,SugVendorFK) values ('" + ItemFK + "','" + ReqQty + "','" + RequisitionFK + "','" + DeptFK + "'," + SugServiceLoc + "," + SugVendorFK + ")";
                                q1 = d2.update_method_wo_parameter(updatequery, "Text");
                            }
                            else
                            {
                                imgdiv2.Visible = true;
                                lbl_alert.Text = "Please Fill The Quantity Value";
                            }
                        }
                    }
                    if (q != 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Saved Successfully";
                        ItemReqNo();
                        mainclear();
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select any department";
                }
                #endregion
            }
            else if (div_visitor.Visible == true)
            {
                #region Visitor
                RequestType = 3;
                if (txt_name.Text != "" && txt_visitormob.Text != "")
                {
                    int moble = 0;
                    int.TryParse(txt_visitormob.Text, out moble);
                    if (txt_visitormob.Text.Length == 10)
                    {
                        DateTime RequestDate = new DateTime();
                        RequestDate = TextToDate(txt_visitorreqdate);
                        RequestCode = txt_visitorreqno.Text;
                        DateTime ReqExpectedDate = new DateTime();
                        ReqExpectedDate = TextToDate(txt_visitdate);
                        string vendoraddress = txt_address.Text;
                        string city = txt_cty.Text;
                        string state = txt_stat.Text;
                        string dis = txt_dis.Text;
                        state = d2.GetFunction("select mastercode from CO_MasterValues where mastercriteria='State' and mastervalue ='" + state + "'");
                        dis = d2.GetFunction("select mastercode from CO_MasterValues where mastercriteria='District' and mastervalue ='" + dis + "'");
                        string desti = string.Empty;
                        string destq = string.Empty;
                        if (ddl_designation.SelectedItem.Text != "--Select--")
                            desti = ddl_designation.SelectedItem.Text;
                        if (ddl_department.SelectedItem.Text != "--Select--")
                            destq = ddl_department.SelectedItem.Text;

                        string ReqExpectedTime = ddl_hrs.SelectedItem.Text + ":" + ddl_mins.SelectedItem.Text + " " + ddl_ampm.SelectedItem.Text;
                        string MeetDeptCode = "";
                        string MeetStaffAppNo = "";
                        string VendorCode = "";
                        //magesh 8.6.18
                        //string VenCode = d2.GetFunction("select VendorCode from CO_VendorMaster where VendorType=3 and VendorCompName like '" + txt_cname.Text + "%'");
                        string VenCode = d2.GetFunction("select VendorCode from CO_VendorMaster where VendorType=3 and VendorName like '" + txt_name.Text + "%' and VendorMobileNo like '" + txt_visitormob.Text + "%'");
                        if (VenCode != "" && VenCode != null && VenCode != "0")
                        {
                            VendorCode = VenCode;
                        }
                        else
                        {
                            VendorCodeGen();
                            VendorCode = Session["VendorCode"].ToString();
                            string venmst = "if exists (select * from CO_VendorMaster where VendorCode='" + VendorCode + "' and VendorName='" + txt_name.Text + "' and VendorMobileNo='" + txt_visitormob.Text + "' and VendorType='3') update CO_VendorMaster set VendorCode='" + VendorCode + "',VendorCompName='" + txt_cname.Text + "',VendorType='3',VendorAddress='" + vendoraddress + "',VendorCity='" + city + "',VendorDist='" + dis + "',VendorState='" + state + "' where VendorCode='" + VendorCode + "' and VendorName='" + txt_name.Text + "' and VendorMobileNo='" + txt_visitormob.Text + "' and VendorType='3' else insert into CO_VendorMaster(VendorCode,VendorCompName,VendorType,VendorAddress,VendorName,VendorMobileNo,VendorCity,VendorDist,VendorState) values('" + VendorCode + "','" + txt_cname.Text + "','3','" + vendoraddress + "','" + txt_name.Text + "','" + txt_visitormob.Text + "','" + city + "','" + dis + "','" + state + "')";
                            int vc = d2.update_method_wo_parameter(venmst, "TEXT");
                        }
                        Int64 VendorFK = Convert.ToInt64(d2.GetFunction("select VendorPK from CO_VendorMaster where VendorCode='" + VendorCode + "'"));
                        string vencmst = "if exists(select * from IM_VendorContactMaster where VendorFK='" + VendorFK + "' and VenContactName='" + txt_name.Text + "'  ) update IM_VendorContactMaster set VendorFK='" + VendorFK + "',VenContactName='" + txt_name.Text + "',VenContactDesig='" + desti + "',VenContactDept='" + destq + "',VendorMobileNo='" + txt_visitormob.Text + "',VendorPhoneNo='" + txt_visitorph.Text + "',VendorEmail='" + txt_visitoremail.Text + "' where VendorFK='" + VendorFK + "' and VenContactName='" + txt_name.Text + "'  else insert into IM_VendorContactMaster(VendorFK,VenContactName,VenContactDesig,VenContactDept,VendorMobileNo, VendorPhoneNo,VendorEmail) values('" + VendorFK + "','" + txt_name.Text + "','" + desti + "','" + destq + "','" + txt_visitormob.Text + "','" + txt_visitorph.Text + "','" + txt_visitoremail.Text + "')";//and VendorMobileNo='" + txt_visitormob.Text + "' and VenContactDesig='" + ddl_designation.SelectedItem.Text + "' and VenContactDept='" + ddl_department.SelectedItem.Text + "'
                        int vcm = d2.update_method_wo_parameter(vencmst, "TEXT");
                        Int64 VendorContactFK = Convert.ToInt64(d2.GetFunction("select VendorContactPK from IM_VendorContactMaster where VendorFk='" + VendorFK + "' and VenContactName='" + txt_name.Text + "' and VendorMobileNo='" + txt_visitormob.Text + "'"));//and VenContactDesig='" + ddl_designation.SelectedItem.Text + "'")
                        int MeetToStaff = 0;
                        int MeetToDept = 0;
                        if (cb_dept.Checked == true)
                        {
                            MeetToDept = 1;
                        }
                        if (cb_individual.Checked == true)
                        {
                            MeetToStaff = 1;
                        }
                        if (txt_name.Text != "")//magesh 7.6.18txt_cname.Text != "" && 
                        {
                            if (cb_dept.Checked != false || cb_individual.Checked != false)
                            {
                                //for Department Meet
                                if (txt_dept_to.Text != "")
                                {
                                    MeetDeptCode = d2.GetFunction("select Dept_Code from Department where Dept_Name ='" + txt_dept_to.Text + "'");
                                }
                                if (txt_to1.Text != "")
                                {
                                    MeetDeptCode = MeetDeptCode + "," + d2.GetFunction("select Dept_Code from Department where Dept_Name ='" + txt_to1.Text + "'");
                                }
                                if (txt_dept_cc.Text != "")
                                {
                                    MeetDeptCode = MeetDeptCode + "," + d2.GetFunction("select Dept_Code from Department where Dept_Name ='" + txt_dept_cc.Text + "'");
                                }
                                if (txt_cc1.Text != "")
                                {
                                    MeetDeptCode = MeetDeptCode + "," + d2.GetFunction("select Dept_Code from Department where Dept_Name ='" + txt_cc1.Text + "'");
                                }
                                // for Staff Meet 
                                if (txt_indiv.Text != "")
                                {
                                    string[] indivstaffname = txt_indiv.Text.Trim().Split('-');//28.03.17
                                    if (indivstaffname.Length > 1)
                                    {
                                        MeetStaffAppNo = d2.GetFunction("select appl_no from staffmaster where staff_name='" + Convert.ToString(indivstaffname[0]) + "'");
                                    }
                                    else
                                        MeetStaffAppNo = "0";
                                }
                                if (txt_indiv1.Text != "")
                                {
                                    string[] indivstaffname = txt_indiv1.Text.Trim().Split('-');//28.03.17
                                    if (indivstaffname.Length > 1)
                                    {
                                        MeetStaffAppNo += "," + d2.GetFunction("select appl_no from staffmaster where staff_name='" + Convert.ToString(indivstaffname[0]) + "'");
                                    }
                                    else
                                        MeetStaffAppNo = "0";
                                }
                                if (txt_indiv_cc.Text != "")
                                {
                                    //MeetStaffAppNo = MeetStaffAppNo + "," + d2.GetFunction("select appl_no from staffmaster where staff_name='" + txt_dept_cc.Text + "'");
                                    string[] indivstaffname = txt_indiv_cc.Text.Trim().Split('-');
                                    if (indivstaffname.Length > 1)
                                    {
                                        MeetStaffAppNo += "," + d2.GetFunction("select appl_no from staffmaster where staff_name='" + Convert.ToString(indivstaffname[0]) + "'");
                                    }
                                    else
                                        MeetStaffAppNo = "0";
                                }
                                if (txt_cc2.Text != "")
                                {
                                    string[] indivstaffname = txt_cc2.Text.Trim().Split('-');
                                    if (indivstaffname.Length > 1)
                                    {
                                        MeetStaffAppNo += "," + d2.GetFunction("select appl_no from staffmaster where staff_name='" + Convert.ToString(indivstaffname[0]) + "'");
                                    }
                                    else
                                        MeetStaffAppNo = "0";
                                }
                                int reques = 0;
                                int reqapp = 0;
                                string checkper = d2.GetFunction("select value from Master_Settings where settings='Leave Approval Permission' and usercode='" + usercode + "' ");
                                if (checkper == "3")
                                {
                                    reques = 1;
                                    reqapp = 1;
                                }
                                else
                                {
                                    reques = 0;
                                    reqapp = 0;
                                }
                                query = "insert into RQ_Requisition(RequestType,RequestCode,RequestDate,ReqExpectedDate,ReqExpectedTime,Remarks,MeetDeptCode,MeetStaffAppNo,VendorFk,VendorContactFK,MeetToStaff,MeetToDept,ReqAppNo,ReqApproveStage,MemType,ReqAppStatus) values('" + RequestType + "','" + RequestCode + "','" + RequestDate + "','" + ReqExpectedDate + "','" + ReqExpectedTime + "','" + txt_visitorpurpose.Text + "','" + MeetDeptCode + "','" + MeetStaffAppNo.TrimStart(',') + "','" + VendorFK + "','" + VendorContactFK + "'," + MeetToStaff + "," + MeetToDept + ",'" + ReqStaffAppNo + "','" + reqapp + "','4','" + reques + "')";//magesh 7.6.18 remove ReqApproveStage=0 add ReqApproveStage=1 and ReqAppStatus
                                q = d2.update_method_wo_parameter(query, "TEXT");
                                Int64 RequisitionFK = Convert.ToInt64(d2.GetFunction("select distinct top (1) RequisitionPK from RQ_Requisition where RequestCode='" + RequestCode + "' and RequestType='" + RequestType + "' order by RequisitionPK desc"));
                                string department_code1 = Convert.ToString(txt_dept_to.Text);
                                string department_code2 = Convert.ToString(txt_to1.Text);
                                string department_code3 = Convert.ToString(txt_dept_cc.Text);
                                string department_code4 = Convert.ToString(txt_cc1.Text);
                                string code = "";
                                string code1 = "";
                                string deptcode1 = Convert.ToString(depthash[Convert.ToString(department_code1)]);
                                if (deptcode1.Trim() != "")
                                {
                                    if (code == "")
                                    {
                                        code = deptcode1;
                                    }
                                    else
                                    {
                                        code = code + "," + deptcode1;
                                    }
                                }
                                string deptcode2 = Convert.ToString(depthash[Convert.ToString(department_code2)]);
                                if (deptcode2.Trim() != "")
                                {
                                    if (code == "")
                                    {
                                        code = deptcode2;
                                    }
                                    else
                                    {
                                        code = code + "," + deptcode2;
                                    }
                                }
                                string deptcode3 = Convert.ToString(depthash[Convert.ToString(department_code3)]);
                                if (deptcode3.Trim() != "")
                                {
                                    if (code == "")
                                    {
                                        code = deptcode3;
                                    }
                                    else
                                    {
                                        code = code + "," + deptcode3;
                                    }
                                }
                                string deptcode4 = Convert.ToString(depthash[Convert.ToString(department_code4)]);
                                if (deptcode4.Trim() != "")
                                {
                                    if (code == "")
                                    {
                                        code = deptcode4;
                                    }
                                    else
                                    {
                                        code = code + "," + deptcode4;
                                    }
                                }
                                string[] split = code.Split(',');
                                for (int code2 = 0; code2 < split.Length; code2++)
                                {
                                    Int64 DeptFK = 0;
                                    if (split.Length > 1)
                                    {
                                        DeptFK = Convert.ToInt64(split[code2]);
                                    }
                                    query = "INSERT INTO RQ_RequisitionDet(DeptFK,StaffAppNo,RequisitionFK) VALUES ('" + DeptFK + "','" + ReqStaffAppNo + "','" + RequisitionFK + "')";
                                    q = d2.update_method_wo_parameter(query, "TEXT");
                                }
                                if (q != 0)
                                {
                                    ItemReqNo();
                                    //mainclear();
                                    access();
                                    if (sms_req == "1")
                                    {
                                        string companynameandpersonname = Convert.ToString(txt_cname.Text + "-" + txt_name.Text);
                                        visitorsms(Convert.ToString(RequisitionFK), companynameandpersonname, txt_visitdate.Text, ReqExpectedTime, txt_visitorpurpose.Text.Trim());
                                    }
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "Saved Successfully";
                                    visitorclear();
                                }
                            }
                            else
                            {
                                imgdiv2.Visible = true;
                                lbl_alert.Text = "Select any option that you want to meet";
                            }
                        }
                        //magesh 7.6.18
                        //else if (txt_cname.Text == "")
                        //{
                        //    imgdiv2.Visible = true;
                        //    lbl_alert.Text = "Enter the visitor's company name";
                        //}
                        else if (txt_name.Text == "")
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Enter the visitor name";
                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Enter the visitor name & company name";
                        }
                    }
                    else
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Enter Valid 10 - Digits Mobile Numbers";

                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Enter the visitor name & Mobile Number";

                }
                #endregion
            }
            else if (div_complaints.Visible == true)
            {
                #region complaints
                RequestType = 4;
                DateTime RequestDate = new DateTime();
                RequestDate = TextToDate(txt_compreqdate);
                RequestCode = txt_compreqno.Text;
                Int64 ReqComplaints = new Int64();
                ReqComplaints = Convert.ToInt64(ddl_complaints.SelectedItem.Value.ToString());
                string ReqComplaintSub = txt_regards.Text;
                Int64 ReqSuggestions = new Int64();
                ReqSuggestions = Convert.ToInt64(ddl_suggestions.SelectedItem.Value.ToString());
                query = "insert into RQ_Requisition(RequestType,RequestCode,RequestDate,ReqComplaints,ReqComplaintSub,ReqSuggestions,ReqAppNo,ReqApproveStage,MemType,college_code) values('" + RequestType + "','" + RequestCode + "','" + RequestDate + "'," + ReqComplaints + ",'" + ReqComplaintSub + "'," + ReqSuggestions + ",'" + ReqStaffAppNo + "','0','2','" + Convert.ToString(collegecode1) + "')";
                q = d2.update_method_wo_parameter(query, "TEXT");
                if (q != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Saved Successfully";
                    ItemReqNo();
                    complaintsclear();
                    //mainclear();
                }
                #endregion
            }
            ItemReqNo();
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeButton", "HideLoadingDiv();", true);
        }
    }

    protected void btn_reqclear_Click(object sender, EventArgs e)
    {
        try
        {
            if (div_itmreqst.Visible == true)
            {
                mainclear();
            }
            else if (div_service.Visible == true)
            {
                mainclear();
            }
            else if (div_visitor.Visible == true)
            {
                visitorclear();
            }
            else if (div_complaints.Visible == true)
            {
                complaintsclear();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void ItemReqNo()
    {
        try
        {
            string newitemcode = "";
            string selectquery = "select ReqAcr,ReqSize,ReqStNo  from IM_CodeSettings order by StartDate desc";
            ds = d2.select_method_wo_parameter(selectquery, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string itemacronym = Convert.ToString(ds.Tables[0].Rows[0]["ReqAcr"]);
                string itemstarno = Convert.ToString(ds.Tables[0].Rows[0]["ReqStNo"]);
                string itemsize = Convert.ToString(ds.Tables[0].Rows[0]["ReqSize"]);
                selectquery = "select distinct top (1)  RequestCode,RequisitionPK   from RQ_Requisition where RequestCode like '" + Convert.ToString(itemacronym) + "%' order by RequisitionPK desc";
                ds.Clear();
                ds = d2.select_method_wo_parameter(selectquery, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string itemcode = Convert.ToString(ds.Tables[0].Rows[0]["RequestCode"]);
                    string itemacr = Convert.ToString(itemacronym);
                    int len = itemacr.Length;
                    itemcode = itemcode.Remove(0, len);
                    int len1 = Convert.ToString(itemcode).Length;
                    string newnumber = Convert.ToString((Convert.ToInt32(itemcode) + 1));
                    len = Convert.ToString(newnumber).Length;
                    len1 = Convert.ToInt32(itemsize) - len;
                    if (len1 == 2)
                    {
                        newitemcode = "00" + newnumber;
                    }
                    else if (len1 == 1)
                    {
                        newitemcode = "0" + newnumber;
                    }
                    else if (len1 == 4)
                    {
                        newitemcode = "0000" + newnumber;
                    }
                    else if (len1 == 3)
                    {
                        newitemcode = "000" + newnumber;
                    }
                    else if (len1 == 5)
                    {
                        newitemcode = "00000" + newnumber;
                    }
                    else if (len1 == 6)
                    {
                        newitemcode = "000000" + newnumber;
                    }
                    else
                    {
                        newitemcode = Convert.ToString(newnumber);
                    }
                    if (newitemcode.Trim() != "")
                    {
                        newitemcode = itemacr + "" + newitemcode;
                    }
                }
                else
                {
                    string itemacr = Convert.ToString(itemstarno);
                    int len = itemacr.Length;
                    string items = Convert.ToString(itemsize);
                    int len1 = Convert.ToInt32(items);
                    int size = len1 - len;
                    if (size == 2)
                    {
                        newitemcode = "00" + itemstarno;
                    }
                    else if (size == 1)
                    {
                        newitemcode = "0" + itemstarno;
                    }
                    else if (size == 4)
                    {
                        newitemcode = "0000" + itemstarno;
                    }
                    else if (size == 3)
                    {
                        newitemcode = "000" + itemstarno;
                    }
                    else if (len1 == 5)
                    {
                        newitemcode = "00000" + itemstarno;
                    }
                    else if (len1 == 6)
                    {
                        newitemcode = "000000" + itemstarno;
                    }
                    else
                    {
                        newitemcode = Convert.ToString(itemstarno);
                    }
                    newitemcode = Convert.ToString(itemacronym) + "" + Convert.ToString(newitemcode);
                }
                Session["requestcode"] = Convert.ToString(newitemcode);
                txt_reqno.Text = Convert.ToString(newitemcode);
                // else if (rb_service.Checked == true)
                txt_serreqno.Text = Convert.ToString(newitemcode);
                //  else if (rb_visitor.Checked == true)
                txt_visitorreqno.Text = Convert.ToString(newitemcode);
                // else if (rb_complaints.Checked == true)
                txt_compreqno.Text = Convert.ToString(newitemcode);
                txt_rqstn_leave.Text = Convert.ToString(newitemcode);
                txt_reqtn_gate.Text = Convert.ToString(newitemcode);
            }
        }
        catch
        { }
    }

    public void VendorCodeGen()
    {
        try
        {
            string newitemcode = "";
            string VendorCode = "";
            string selectquery = "select VenAcr,VenStNo,VenSize from IM_CodeSettings  order by StartDate desc";
            //select Requisition_Acr ,Requisition_Size,Requisition_StNo  from InvCode_Settings where Latestrec =1";
            ds = d2.select_method_wo_parameter(selectquery, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string itemacronym = Convert.ToString(ds.Tables[0].Rows[0]["VenAcr"]);
                string itemstarno = Convert.ToString(ds.Tables[0].Rows[0]["VenStNo"]);
                string itemsize = Convert.ToString(ds.Tables[0].Rows[0]["VenSize"]);
                if (itemacronym.Trim() != "" && itemstarno.Trim() != "")
                {
                    selectquery = "select distinct top (1)  VendorCode  from CO_VendorMaster where VendorCode like '" + Convert.ToString(itemacronym) + "%' order by VendorCode desc";
                    //select distinct top (1)  RequestCode  from RQ_Requisition where RequestCode like '" + Convert.ToString(itemacronym) + "%' order by RequestCode desc";
                    //select distinct top (1) item_code  from item_master where item_code like '" + Convert.ToString(itemacronym) + "%' order by item_code desc";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selectquery, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string itemcode = Convert.ToString(ds.Tables[0].Rows[0]["VendorCode"]);
                        string itemacr = Convert.ToString(itemacronym);
                        int len = itemacr.Length;
                        itemcode = itemcode.Remove(0, len);
                        int len1 = Convert.ToString(itemcode).Length;
                        string newnumber = Convert.ToString((Convert.ToInt32(itemcode) + 1));
                        len = Convert.ToString(newnumber).Length;
                        len1 = len1 - len;
                        if (len1 == 2)
                        {
                            newitemcode = "00" + newnumber;
                        }
                        else if (len1 == 1)
                        {
                            newitemcode = "0" + newnumber;
                        }
                        else if (len1 == 4)
                        {
                            newitemcode = "0000" + newnumber;
                        }
                        else if (len1 == 3)
                        {
                            newitemcode = "000" + newnumber;
                        }

                        else if (len1 == 5)
                        {
                            newitemcode = "00000" + newnumber;
                        }
                        else if (len1 == 6)
                        {
                            newitemcode = "000000" + newnumber;
                        }
                        else
                        {
                            newitemcode = Convert.ToString(newnumber);
                        }
                        if (newitemcode.Trim() != "")
                        {
                            newitemcode = itemacr + "" + newitemcode;
                        }
                    }
                    else
                    {
                        string itemacr = Convert.ToString(itemstarno);
                        int len = itemacr.Length;
                        string items = Convert.ToString(itemsize);
                        int len1 = Convert.ToInt32(items);
                        int size = len1 - len;
                        if (size == 2)
                        {
                            newitemcode = "00" + itemstarno;
                        }
                        else if (size == 1)
                        {
                            newitemcode = "0" + itemstarno;
                        }
                        else if (size == 3)
                        {
                            newitemcode = "000" + itemstarno;
                        }
                        else if (size == 4)
                        {
                            newitemcode = "0000" + itemstarno;
                        }
                        else if (size == 5)
                        {
                            newitemcode = "00000" + itemstarno;
                        }
                        else if (size == 6)
                        {
                            newitemcode = "000000" + itemstarno;
                        }
                        else
                        {
                            newitemcode = Convert.ToString(itemstarno);
                        }
                        newitemcode = Convert.ToString(itemacronym) + "" + Convert.ToString(newitemcode);
                    }
                    Session["VendorCode"] = newitemcode;
                    //  if (rb_item.Checked == true)
                    //txt_reqno.Text = Convert.ToString(newitemcode);
                    // else if (rb_service.Checked == true)
                    // txt_serreqno.Text = Convert.ToString(newitemcode);
                    //  else if (rb_visitor.Checked == true)
                    //  txt_visitorreqno.Text = Convert.ToString(newitemcode);
                    // else if (rb_complaints.Checked == true)
                    //  txt_compreqno.Text = Convert.ToString(newitemcode);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void mainclear()
    {
        try
        {
            cb_other.Checked = false;
            cb_own.Checked = false;
            txt_dept.Text = txt_serdept.Text = "";
            // SelectdptGrid.Columns.Clear();
            SelectdptGrid.DataSource = null;
            SelectdptGrid.DataBind();
            Sergrid.DataSource = null;
            Sergrid.DataBind();
            Session["dt"] = null;
            ViewState["selecteditems"] = null;
            ViewState["selecteditems1"] = null;
            selectitemgrid.DataSource = null;
            selectitemgrid.DataBind();
            griddiv.Visible = false;
            sergriddiv.Visible = false;
            btn_reqclear.Visible = false;
            btn_reqsave.Visible = false;
            txt_reqremarks.Text = "";
            txt_serremarks.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    public void complaintsclear()
    {
        try
        {
            ddl_complaints.SelectedIndex = 0;
            ddl_suggestions.SelectedIndex = 0;
            txt_regards.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    public void visitorclear()
    {
        try
        {
            txt_cname.Text = txt_name.Text = txt_visitorpurpose.Text = txt_visitorph.Text = txt_visitormob.Text = "";
            txt_visitoremail.Text = txt_to1.Text = txt_cc1.Text = txt_cc2.Text = "";
            txt_dept_cc.Text = txt_dept_to.Text = txt_indiv.Text = txt_indiv_cc.Text = txt_address.Text = "";
            ddl_department.SelectedIndex = 0;
            ddl_designation.SelectedIndex = 0;
            cb_dept.Checked = true;
            cb_individual.Checked = false;
            btn_stud_deptto_rmv_Click(sender, e);
            btn_stud_deptcc_remove_Click(sender, e);
        }
        catch (Exception ex)
        {
        }
    }

    public void loaddepartment()
    {
        ddl_department.Items.Clear();
        ds.Tables.Clear();
        string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='ReDep' and college_code ='" + collegecode1 + "'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_department.DataSource = ds;
            ddl_department.DataTextField = "TextVal";
            ddl_department.DataValueField = "TextCode";
            ddl_department.DataBind();
            ddl_department.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddl_department.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void btn_deptplus_Click(object sender, EventArgs e)
    {
        imgdiv5.Visible = true;
        panel_description2.Visible = true;
    }

    protected void btn_deptminus_Click(object sender, EventArgs e)
    {
        if (ddl_department.SelectedIndex == -1)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
        else if (ddl_department.SelectedIndex == 0)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "Select any record";
        }
        else if (ddl_department.SelectedIndex != 0)
        {
            imgdivcnfm2.Visible = true;
            lbl_alertconfm2.Visible = true;
        }
        //else if (ddl_department.SelectedIndex == -1)
        //{
        //    imgdiv2.Visible = true;
        //    lbl_erroralert.Text = "No records found";
        //}
        else
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
    }

    protected void btn_deptexitdesc1_Click(object sender, EventArgs e)
    {
        imgdiv5.Visible = false;
        panel_description2.Visible = false;
    }

    protected void btn_deptadddesc1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_department.Text != "")
            {
                string sql = "if exists ( select * from TextValTable where TextVal ='" + txt_department.Text + "' and TextCriteria ='ReDep' and college_code ='" + collegecode1 + "') update TextValTable set TextVal ='" + txt_department.Text + "' where TextVal ='" + txt_department.Text + "' and TextCriteria ='ReDep' and college_code ='" + collegecode1 + "' else insert into TextValTable (TextVal,TextCriteria,college_code) values ('" + txt_department.Text + "','ReDep','" + collegecode1 + "')";
                int insert = d2.update_method_wo_parameter(sql, "TEXT");
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Saved Successfully";
                    txt_department.Text = "";
                    imgdiv5.Visible = false;
                    panel_description2.Visible = false;
                }
                loaddepartment();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Enter the description";
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void loaddesignation()
    {
        ddl_designation.Items.Clear();
        ds.Tables.Clear();
        string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='ReDes' and college_code ='" + collegecode1 + "'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_designation.DataSource = ds;
            ddl_designation.DataTextField = "TextVal";
            ddl_designation.DataValueField = "TextCode";
            ddl_designation.DataBind();
            ddl_designation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddl_designation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void btn_desgplus_Click(object sender, EventArgs e)
    {
        imgdiv6.Visible = true;
        panel_description3.Visible = true;
    }

    protected void btn_desgminus_Click(object sender, EventArgs e)
    {
        if (ddl_designation.SelectedIndex == -1)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
        else if (ddl_designation.SelectedIndex == 0)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "Select any record";
        }
        else if (ddl_designation.SelectedIndex != 0)
        {
            imgdivcnfm.Visible = true;
            lbl_alertconfm.Visible = true;
        }
        //else if (ddl_designation.SelectedIndex == -1)
        //{
        //    imgdiv2.Visible = true;
        //    lbl_erroralert.Text = "No records found";
        //}
        else
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
    }

    protected void btn_desgexitdesc1_Click(object sender, EventArgs e)
    {
        imgdiv6.Visible = false;
        panel_description3.Visible = false;
    }

    protected void btn_desgadddesc1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_designation.Text != "")
            {
                string sql = "if exists ( select * from TextValTable where TextVal ='" + txt_designation.Text + "' and TextCriteria ='ReDes' and college_code ='" + collegecode1 + "') update TextValTable set TextVal ='" + txt_designation.Text + "' where TextVal ='" + txt_designation.Text + "' and TextCriteria ='ReDes' and college_code ='" + collegecode1 + "' else insert into TextValTable (TextVal,TextCriteria,college_code) values ('" + txt_designation.Text + "','ReDes','" + collegecode1 + "')";
                int insert = d2.update_method_wo_parameter(sql, "TEXT");
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Saved Successfully";
                    txt_designation.Text = "";
                    imgdiv6.Visible = false;
                    panel_description3.Visible = false;
                }
                loaddesignation();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Enter the description";
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void loadcomplaints()
    {
        ddl_complaints.Items.Clear();
        ds.Tables.Clear();
        string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='ReCom' and college_code ='" + collegecode1 + "'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_complaints.DataSource = ds;
            ddl_complaints.DataTextField = "TextVal";
            ddl_complaints.DataValueField = "TextCode";
            ddl_complaints.DataBind();
            ddl_complaints.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddl_complaints.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    public void loadleavereason()
    {
        //ds.Clear();
        //ddl_leavereason.Items.Clear();
        //string sql = "select MasterCode,MasterValue from CO_MasterValues where MasterCriteria ='HGRea'";
        //ds = d2.select_method_wo_parameter(sql, "TEXT");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddl_leavereason.DataSource = ds;
        //    ddl_leavereason.DataTextField = "MasterValue";
        //    ddl_leavereason.DataValueField = "MasterCode";
        //    ddl_leavereason.DataBind();
        //    // ddl_leavereason.Items.Insert(0, new ListItem("Select", "0"));
        //}
        //ddl_leavereason.Items.Insert(0, new ListItem("Select", "0"));
        //ddl_leavereason.Items.Insert(ddl_leavereason.Items.Count, "Others");
    }

    public void leav_res()
    {
        try
        {
        }
        catch
        {
        }
    }

    protected void btn_compplus_Click(object sender, EventArgs e)
    {
        txt_complaints.Text = string.Empty;
        imgdiv3.Visible = true;
        panel_description.Visible = true;
    }

    protected void btn_compminus_Click(object sender, EventArgs e)
    {
        if (ddl_complaints.SelectedIndex == -1)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
        else if (ddl_complaints.SelectedIndex == 0)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "Select any record";
        }
        else if (ddl_complaints.SelectedIndex != 0)
        {
            deletevalue = "1";
            imgdivcnfm3.Visible = true;
        }
        //else if (ddl_complaints.SelectedIndex == -1)
        //{
        //    imgdiv2.Visible = true;
        //    lbl_erroralert.Text = "No records found";
        //}
        else
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
    }

    protected void btn_compexitdesc1_Click(object sender, EventArgs e)
    {
        imgdiv3.Visible = false;
        panel_description.Visible = false;
    }

    protected void btn_compadddesc1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_complaints.Text != "")
            {
                string sql = "if exists ( select * from TextValTable where TextVal ='" + txt_complaints.Text + "' and TextCriteria ='ReCom' and college_code ='" + collegecode1 + "') update TextValTable set TextVal ='" + txt_complaints.Text + "' where TextVal ='" + txt_complaints.Text + "' and TextCriteria ='ReCom' and college_code ='" + collegecode1 + "' else insert into TextValTable (TextVal,TextCriteria,college_code) values ('" + txt_complaints.Text + "','ReCom','" + collegecode1 + "')";
                int insert = d2.update_method_wo_parameter(sql, "TEXT");
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Saved Successfully";
                    txt_complaints.Text = "";
                    imgdiv3.Visible = false;
                    panel_description.Visible = false;
                }
                loadcomplaints();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Enter the description";
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void loadsuggestions()
    {
        ddl_suggestions.Items.Clear();
        ds.Tables.Clear();
        string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='ReSug' and college_code ='" + collegecode1 + "'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_suggestions.DataSource = ds;
            ddl_suggestions.DataTextField = "TextVal";
            ddl_suggestions.DataValueField = "TextCode";
            ddl_suggestions.DataBind();
            ddl_suggestions.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddl_suggestions.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void btn_sugplus_Click(object sender, EventArgs e)
    {
        txt_sggestions.Text = string.Empty;
        imgdiv4.Visible = true;
        panel_description1.Visible = true;
    }

    protected void btn_sugminus_Click(object sender, EventArgs e)
    {
        if (ddl_suggestions.SelectedIndex == -1)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
        else if (ddl_suggestions.SelectedIndex == 0)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "Select any record";
        }
        else if (ddl_suggestions.SelectedIndex != 0)
        {
            deletevalue = "2";
            imgdivcnfm3.Visible = true;
        }
        //else if (ddl_suggestions.SelectedIndex == -1)
        //{
        //    imgdiv2.Visible = true;
        //    lbl_erroralert.Text = "No records found";
        //}
        else
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
    }

    protected void btn_sugexitdesc1_Click(object sender, EventArgs e)
    {
        imgdiv4.Visible = false;
        panel_description1.Visible = false;
    }

    protected void btn_sugadddesc1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_sggestions.Text != "")
            {
                string sql = "if exists ( select * from TextValTable where TextVal ='" + txt_sggestions.Text + "' and TextCriteria ='ReSug' and college_code ='" + collegecode1 + "') update TextValTable set TextVal ='" + txt_sggestions.Text + "' where TextVal ='" + txt_sggestions.Text + "' and TextCriteria ='ReSug' and college_code ='" + collegecode1 + "' else insert into TextValTable (TextVal,TextCriteria,college_code) values ('" + txt_sggestions.Text + "','ReSug','" + collegecode1 + "')";
                int insert = d2.update_method_wo_parameter(sql, "TEXT");
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Saved Successfully";
                    txt_sggestions.Text = "";
                    imgdiv4.Visible = false;
                    panel_description1.Visible = false;
                }
                loadsuggestions();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Enter the description";
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    protected void btn_visitormeetstaff_Click(object sender, EventArgs e)
    {
        try
        {
            ad();
        }
        catch (Exception ex)
        {
        }
    }

    static int j = 0;
    TextBox box;
    public void ad()
    {
        j++;
        for (int i = 0; i < j; i++)
        {
            box = new TextBox();
            box.ID = "textbox" + i.ToString();
            box.Height = Unit.Pixel(20);
            box.Width = Unit.Pixel(300);
            box.AutoPostBack = false;
            dynamictxt.Controls.Add(box);
            dynamictxt.Controls.Add(new LiteralControl("<BR><BR>"));
            AjaxControlToolkit.AutoCompleteExtender kit = new AjaxControlToolkit.AutoCompleteExtender();
            kit.ID = "AutoCompleteExtender1rrr" + i.ToString();
            kit.Enabled = true;
            kit.TargetControlID = box.ID;
            kit.CompletionListCssClass = "autocomplete_completionListElement";
            kit.CompletionListHighlightedItemCssClass = "autocomplete_highlightedListItem";
            kit.CompletionListItemCssClass = "txtsearchpan";
            kit.CompletionSetCount = 10;
            kit.MinimumPrefixLength = 0;
            kit.CompletionInterval = 100;
            kit.ServiceMethod = "GetCompName";
            // this.Form.Controls.Add(kit);
            dynamictxt.Controls.Add(kit);
            // dynamictxt.Controls.Add(autoCompleteExtender);
        }
    }

    protected void btn_visitormeetdept_Click(object sender, EventArgs e)
    {
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getitemheader(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        if (rights != "" && ishostel != "")
        {
            if (rights == "both" && ishostel == "both")
            {
                query = "select distinct itemheader_name from item_master WHERE itemheader_name like '" + prefixText + "%' and Item_Type in(" + Item_Type + ")";
            }
            else if (rights == "hostel" && ishostel == "0")
            {
                query = "select distinct itemheader_name from item_master WHERE itemheader_name like '" + prefixText + "%' and Is_Hostel=0 and Item_Type in(" + Item_Type + ")";
            }
            else if (rights == "invetory" && ishostel == "0")
            {
                query = "select distinct itemheader_name from item_master WHERE itemheader_name like '" + prefixText + "%' and Is_Hostel<>0 and Item_Type in(" + Item_Type + ")";
            }
            else
            {
                query = "";
            }
            dw = dn.select_method_wo_parameter(query, "Text");
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["itemheader_name"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getitemcode(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        if (rights != "" && ishostel != "")
        {
            if (rights == "both" && ishostel == "both")
            {
                query = "select distinct item_code from item_master WHERE item_code like '" + prefixText + "%' and Item_Type in(" + Item_Type + ") ";
            }
            else if (rights == "hostel" && ishostel == "0")
            {
                query = "select distinct item_code from item_master WHERE item_code like '" + prefixText + "%' and Is_Hostel=0 and Item_Type in(" + Item_Type + ")";
            }
            else if (rights == "invetory" && ishostel == "0")
            {
                query = "select distinct item_code from item_master WHERE item_code like '" + prefixText + "%' and Is_Hostel<>0 and Item_Type in(" + Item_Type + ")";
            }
            else
            {
                query = "";
            }
            dw = dn.select_method_wo_parameter(query, "Text");
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["item_code"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getname(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        if (rights != "" && ishostel != "")
        {
            if (rights == "both" && ishostel == "both")
            {
                query = "select distinct item_name from item_master WHERE item_name like '" + prefixText + "%' and Item_Type in(" + Item_Type + ")";
            }
            else if (rights == "hostel" && ishostel == "0")
            {
                query = "select distinct item_name from item_master WHERE item_name like '" + prefixText + "%' and Is_Hostel=0 and Item_Type in(" + Item_Type + ")";
            }
            else if (rights == "invetory" && ishostel == "0")
            {
                query = "select distinct item_name from item_master WHERE item_name like '" + prefixText + "%' and Is_Hostel<>0 and Item_Type in(" + Item_Type + ")";
            }
            dw = dn.select_method_wo_parameter(query, "Text");
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["item_name"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> GetVendorDet(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        query = "select distinct vendor_name from vendor_details where vendor_type='Approved' and vendor_name like '" + prefixText + "%' order by vendor_name";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["vendor_name"].ToString());
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> GetVendComp(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        query = "select VendorCompName from CO_VendorMaster where VendorType='3' and VendorCompName like '" + prefixText + "%' order by VendorCompName";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count != 0)
        {
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["VendorCompName"].ToString());
                }
            }
        }
        return name;
    }

    protected void txt_cname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            vendcompname = txt_cname.Text;
            VendorFK = Convert.ToInt64(d2.GetFunction("select VendorPK from CO_VendorMaster where VendorCompName='" + vendcompname + "'"));
        }
        catch (Exception ex)
        {
        }
    }

    protected void txt_name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string FetchData = "select VenContactName,VenContactDesig,VenContactDept,VendorPhoneNo,VendorMobileNo,VendorEmail from IM_VendorContactMaster where VenContactName='" + txt_name.Text + "' and VendorFK=" + VendorFK + "";
            string FetchData = "select vc.VenContactName,vc.VenContactDesig,vc.VenContactDept,vc.VendorPhoneNo,vc.VendorMobileNo, vc.VendorEmail,vm.VendorAddress from IM_VendorContactMaster vc,CO_VendorMaster vm where vc.VendorFK=vm.VendorPK and vc.VenContactName='" + txt_name.Text + "' and vc.VendorFK='" + VendorFK + "'";
            ds = da.select_method_wo_parameter(FetchData, "Text");
            txt_visitoremail.Text = ds.Tables[0].Rows[0]["VendorEmail"].ToString();
            txt_visitormob.Text = ds.Tables[0].Rows[0]["VendorMobileNo"].ToString();
            txt_visitorph.Text = ds.Tables[0].Rows[0]["VendorPhoneNo"].ToString();
            txt_address.Text = ds.Tables[0].Rows[0]["VendorAddress"].ToString();
            string dept = ds.Tables[0].Rows[0]["VenContactDept"].ToString();
            string desg = ds.Tables[0].Rows[0]["VenContactDesig"].ToString();
            ddl_department.SelectedIndex = ddl_department.Items.IndexOf(ddl_department.Items.FindByText(dept));
            ddl_designation.SelectedIndex = ddl_designation.Items.IndexOf(ddl_designation.Items.FindByText(desg));
            loaddepartment();
            loaddesignation();
        }
        catch (Exception ex)
        {
        }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> GetVendCompDet(string prefixText)
    {
        //  getvisitorcompdet();
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        query = "select VenContactName from IM_VendorContactMaster where VendorFK=" + VendorFK + " and VenContactName like '" + prefixText + "%' order by [VenContactName] asc";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["VenContactName"].ToString());
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> GetCompName(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "";
        query = "select company_name from company_details where company_name like '" + prefixText + "%' order by [company_name] asc";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["company_name"].ToString());
            }
        }
        return name;
    }

    [WebMethod]
    public static compdet[] getData1(string VenContactName)
    {
        string data = string.Empty;
        List<compdet> details = new List<compdet>();
        try
        {
            DataSet ds = new DataSet();
            DAccess2 dd = new DAccess2();
            Hashtable hat = new Hashtable();
            //string FetchData = "select VenContactName,VenContactDesig,VenContactDept,VendorPhoneNo,VendorMobileNo,VendorEmail from IM_VendorContactMaster where VenContactName='" + VenContactName + "' and VendorFK=" + VendorFK + "";
            string FetchData = " select vc.VenContactName,vc.VenContactDesig,vc.VenContactDept,vc.VendorPhoneNo,vc.VendorMobileNo, vc.VendorEmail,vm.VendorAddress from IM_VendorContactMaster vc,CO_VendorMaster vm where vc.VendorFK=vm.VendorPK and vc.VenContactName='" + VenContactName + "' and vc.VendorFK='" + VendorFK + "'";
            ds = dd.select_method_wo_parameter(FetchData, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    compdet s = new compdet();
                    s.VenContactName = ds.Tables[0].Rows[a]["VenContactName"].ToString();
                    s.VenContactDept = ds.Tables[0].Rows[a]["VenContactDept"].ToString();
                    s.VenContactDesig = ds.Tables[0].Rows[a]["VenContactDesig"].ToString();
                    s.VendorPhoneNo = ds.Tables[0].Rows[a]["VendorPhoneNo"].ToString();
                    s.VendorMobileNo = ds.Tables[0].Rows[a]["VendorMobileNo"].ToString();
                    s.VendorEmail = ds.Tables[0].Rows[a]["VendorEmail"].ToString();
                    details.Add(s);
                }
            }
            return details.ToArray();
        }
        catch
        {
            return details.ToArray();
        }
    }

    public class compdet
    {
        public string VenContactName { get; set; }
        public string VenContactDesig { get; set; }
        public string VenContactDept { get; set; }
        public string VendorPhoneNo { get; set; }
        public string VendorMobileNo { get; set; }
        public string VendorEmail { get; set; }
        public string VendorFK { get; set; }
    }
    //------------------------------for convert text to date-----------------------------
    public DateTime TextToDate(TextBox txt)
    {
        DateTime dt = new DateTime();
        string firstdate = Convert.ToString(txt.Text);
        string[] split = firstdate.Split('/');
        dt = Convert.ToDateTime(split[1] + "/" + split[0] + "/" + split[2]);
        return dt;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getdept(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string query = "select distinct Dept_Name as DeptName,Dept_Name from Department where Dept_Name like '" + prefixText + "%'";
        newhash = ws.Getnamevalue(query);
        if (newhash.Count > 0)
        {
            foreach (DictionaryEntry p in newhash)
            {
                string deptname = Convert.ToString(p.Key);
                name.Add(deptname);
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getstaff(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string query = "";
        //if (oldcurrentcheck == "O")
        //{
        //    query = "select distinct s.staff_name+'-'+dm.desig_name+'-'+hr.dept_name+'-'+ s.staff_code, s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=1 and resign =1 and s.staff_name like '" + prefixText + "%'";
        //}
        //else
        //{
        //query = "select distinct s.staff_name+'-'+dm.desig_name+'-'+hr.dept_name+'-'+ s.staff_code, s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and resign =0 and s.staff_name like '" + prefixText + "%'";
        query = "select distinct s.staff_name+'-'+dm.desig_name+'-'+hr.dept_name+'-'+ s.staff_code, s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm,stafftrans st where s.appl_no=sa.appl_no and st.dept_code=hr.dept_code and st.staff_code=s.staff_code and dm.desig_code=st.desig_code and settled=0 and resign =0 and st.latestrec=1 and s.staff_name like '%" + prefixText + "%' and isnull(st.staff_code,'')<>''";
        // }
        newhashtbl = ws.Getnamevalue(query);
        if (newhashtbl.Count > 0)
        {
            foreach (DictionaryEntry p in newhashtbl)
            {
                string staffname = Convert.ToString(p.Key);
                name.Add(staffname);
            }
        }
        // name = ws.Getname(query);
        return name;
    }

    public void checkdept()
    {
        if (cb_dept.Checked == true)
        {
            div_dept.Visible = true;
        }
        else
        {
            div_dept.Visible = false;
        }
    }

    public void checkdesg()
    {
        if (cb_individual.Checked == true)
        {
            div_indiv.Visible = true;
        }
        else
        {
            div_indiv.Visible = false;
        }
    }

    protected void cb_dept_CheckedChanged(object sender, EventArgs e)
    {
        checkdept();
        checkdesg();
    }

    protected void cb_individual_CheckedChanged(object sender, EventArgs e)
    {
        checkdept();
        checkdesg();
    }

    protected void btn_stud_deptto_add_Click(object sender, EventArgs e)
    {
        try
        {
            txt_to1.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_stud_deptto_rmv_Click(object sender, EventArgs e)
    {
        try
        {
            txt_to1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_stud_deptcc_add_Click(object sender, EventArgs e)
    {
        try
        {
            txt_cc1.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_stud_deptcc_remove_Click(object sender, EventArgs e)
    {
        try
        {
            txt_cc1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_dptadd_Click(object sender, EventArgs e)
    {
    }

    protected void btn_stud_indito_add_Click(object sender, EventArgs e)
    {
        try
        {
            txt_indiv1.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_stud_indito_rmv_Click(object sender, EventArgs e)
    {
        try
        {
            txt_indiv1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_stud_indicc_add_Click(object sender, EventArgs e)
    {
        try
        {
            txt_cc2.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_stud_indicc_rmv_Click(object sender, EventArgs e)
    {
        try
        {
            txt_cc2.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    public void imgbtn_item_Click(object sender, EventArgs e)
    {
        td_item.BgColor = "#c4c4c4";
        td_sev.BgColor = "white";
        td_vist.BgColor = "white";
        td_comp.BgColor = "white";
        td_lv.BgColor = "white";
        td_othr.BgColor = "white";
        td_event.BgColor = "white";
        itemheader();
        itemmaster();
        rbcheck();
        mainclear();
        div_itmreqst.Visible = true;
        div_visitor.Visible = false;
        div_complaints.Visible = false;
        div_service.Visible = false;
        div_leavereq.Visible = false;
        panelrollnopop.Visible = false;
        lbl_headername.Visible = true;
        lbl_headername.Text = "Item Request";
        leave_clear();
        div_gate_reqstn.Visible = false;
        div_save.Visible = true;
        cb_own.Checked = true;
        bindgrid_Item_approvalstaff();
    }

    public void imgbtn_service_Click(object sender, EventArgs e)
    {
        btn_reqsave.Visible = true;
        td_item.BgColor = "white";
        td_sev.BgColor = "#c4c4c4";
        td_vist.BgColor = "white";
        td_comp.BgColor = "white";
        td_lv.BgColor = "white";
        td_othr.BgColor = "white";
        td_event.BgColor = "white";
        itemheader();
        itemmaster();
        rbcheck();
        mainclear();
        div_service.Visible = true;
        div_save.Visible = true;
        div_itmreqst.Visible = false;
        div_visitor.Visible = false;
        div_complaints.Visible = false;
        div_leavereq.Visible = false;
        panelrollnopop.Visible = false;
        lbl_headername.Visible = true;
        lbl_headername.Text = "Service";
        div_gate_reqstn.Visible = false;
        leave_clear();
    }

    public void imgbtn_visitor_Click(object sender, EventArgs e)
    {
        td_item.BgColor = "white";
        td_sev.BgColor = "white";
        td_vist.BgColor = "#c4c4c4";
        td_comp.BgColor = "white";
        td_lv.BgColor = "white";
        td_othr.BgColor = "white";
        td_event.BgColor = "white";
        btn_reqclear.Visible = true;
        btn_reqsave.Visible = true;
        visitorclear();
        div_dept.Attributes.Add("style", "display:block");
        div_visitor.Visible = true;
        div_service.Visible = false;
        div_itmreqst.Visible = false;
        div_complaints.Visible = false;
        div_leavereq.Visible = false;
        panelrollnopop.Visible = false;
        sergriddiv.Visible = false;
        div_save.Visible = true;
        lbl_headername.Visible = true;
        lbl_headername.Text = "Visitor Appointment";
        divscrll.Visible = false;
        paneladd.Visible = false;
        div_gate_reqstn.Visible = false;
        fpcammarkstaff.Visible = false;
        leave_clear();
    }

    public void imgbtn_comp_Click(object sender, EventArgs e)
    {
        div_save.Visible = true;
        td_item.BgColor = "white";
        td_sev.BgColor = "white";
        td_vist.BgColor = "white";
        td_comp.BgColor = "#c4c4c4";
        td_lv.BgColor = "white";
        td_othr.BgColor = "white";
        td_event.BgColor = "white";
        div_complaints.Visible = true;
        div_service.Visible = false;
        div_itmreqst.Visible = false;
        div_visitor.Visible = false;
        loadcomplaints();
        loadsuggestions();
        complaintsclear();
        btn_reqclear.Visible = true;
        btn_reqsave.Visible = true;
        div_leavereq.Visible = false;
        panelrollnopop.Visible = false;
        lbl_headername.Visible = true;
        lbl_headername.Text = "Complaints";
        divscrll.Visible = false;
        paneladd.Visible = false;
        div_gate_reqstn.Visible = false;
        fpcammarkstaff.Visible = false;
        leave_clear();
    }

    public void imgbtn_leave_Click(object sender, EventArgs e)
    {
        #region magesh 8.3.18
        Session["alters"] = "conform";
        Session["staffalter"] = "altersch";
        if (Session["staconform"] == "yes")
        {
            if (Session["conformleave"] == "yes")
            {
                Session["leave"] = "Yes";
            }
            else
            {
                Session["leave"] = "NO";
            }
        }
        #endregion
        Session["back"] = null;
        td_item.BgColor = "white";
        td_sev.BgColor = "white";
        td_vist.BgColor = "white";
        td_comp.BgColor = "white";
        td_lv.BgColor = "#c4c4c4";
        td_othr.BgColor = "white";
        td_event.BgColor = "white";
        div_visitor.Visible = false;
        div_service.Visible = false;
        div_itmreqst.Visible = false;
        div_complaints.Visible = false;
        div_leavereq.Visible = true;
        panelrollnopop.Visible = false;
        div_save.Visible = false;
        lbl_headername.Visible = true;
        lbl_headername.Text = "Leave Request";
        divscrll.Visible = false;
        paneladd.Visible = false;
        div_gate_reqstn.Visible = false;
        fpcammarkstaff.Visible = false;
        sp_appstaff_Item.Visible = false;
        grid_Item_approvalstaff.Visible = false;
        if (requestpermissioncheck == "1")
        {
            txt_staff_code.ReadOnly = true;
            txt_staff_name.ReadOnly = true;
            txt_des.ReadOnly = true;
            txt_dep.ReadOnly = true;
        }
        leave_staff_login();
        if (requestpermissioncheck == "1")
        {
            BindGridview();
            bindgrid2();
            gridView2.Visible = true;
            bindgrid_approvalstaff();
        }
    }

    public void imbtn_othgr_Click(object sender, EventArgs e)
    {
        string gatepassrights = d2.GetFunction("select value from Master_Settings where settings='Request Gatepass Rights' and usercode='" + usercode + "'");
        if (gatepassrights.Trim() != "0")
        {
            div_save.Visible = false;
            panelrollnopop.Visible = true;
            div_gate_reqstn.Visible = true;
            td_item.BgColor = "white";
            td_sev.BgColor = "white";
            td_vist.BgColor = "white";
            td_comp.BgColor = "white";
            td_lv.BgColor = "white";
            td_event.BgColor = "white";
            td_othr.BgColor = "#c4c4c4";
            div_visitor.Visible = false;
            div_service.Visible = false;
            div_itmreqst.Visible = false;
            div_complaints.Visible = false;
            div_leavereq.Visible = false;
            lbl_headername.Visible = true;
            lbl_headername.Text = "GatePass Request";
            leave_clear();
            timevalue();
        }
        else
        {
            bb.Visible = true;
            Label17.Text = "Please Set Request Gatepass Rights";
            return;
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            panelrollnopop.Visible = false;
            //panel10.Visible = false;
        }
        catch
        {
        }
    }

    protected void ddlbatch1_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            binddeg();
            branch();
            bindsem1();
        }
        catch
        {
        }
    }

    protected void ddldegree1_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            branch();
            bindsem1();
        }
        catch
        {
        }
    }

    protected void ddldepart1_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            bindsem1();
            BindSectionDetail();
        }
        catch
        {
        }
    }

    protected void ddlsem1_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            BindSectionDetail();
        }
        catch
        {
        }
    }

    protected void ddlsec1_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlsec1.Enabled == true)
            {
                //secstatci = ddlsec1.SelectedValue;
            }
        }
        catch
        {
        }
    }

    protected void Checkhostel1_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            if (Checkhostel1.Checked == true)
            {
                for (int i = 0; i < ddlhostel1.Items.Count; i++)
                {
                    ddlhostel1.Items[i].Selected = true;
                }
                txthostgelname1.Text = "Hostel Name(" + (ddlhostel1.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < ddlhostel1.Items.Count; i++)
                {
                    ddlhostel1.Items[i].Selected = false;
                }
                txthostgelname1.Text = "--Select--";
            }
            bindbuild1();
            bindfloor1();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlhostel1_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            int commcount = 0;
            txthostgelname1.Text = "--Select--";
            Checkhostel1.Checked = false;
            for (int i = 0; i < ddlhostel1.Items.Count; i++)
            {
                if (ddlhostel1.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            if (commcount > 0)
            {
                txthostgelname1.Text = "Hostel Name(" + commcount.ToString() + ")";
                if (commcount == ddlhostel1.Items.Count)
                {
                    Checkhostel1.Checked = true;
                }
            }
            bindbuild1();
            bindfloor1();
        }
        catch (Exception ex)
        {
        }
    }

    protected void checkBuild_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            if (checkBuild.Checked == true)
            {
                for (int i = 0; i < cheklist_Build.Items.Count; i++)
                {
                    cheklist_Build.Items[i].Selected = true;
                }
                txt_Build.Text = "Build(" + (cheklist_Build.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < cheklist_Build.Items.Count; i++)
                {
                    cheklist_Build.Items[i].Selected = false;
                }
                txt_Build.Text = "--Select--";
            }
            bindfloor1();
        }
        catch (Exception ex)
        {
        }
    }

    protected void cheklist_Build_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            int commcount = 0;
            txt_Build.Text = "--Select--";
            checkBuild.Checked = false;
            for (int i = 0; i < cheklist_Build.Items.Count; i++)
            {
                if (cheklist_Build.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            if (commcount > 0)
            {
                txt_Build.Text = "Build(" + commcount.ToString() + ")";
                if (commcount == cheklist_Build.Items.Count)
                {
                    checkBuild.Checked = true;
                }
            }
            bindfloor1();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Checkfloor_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            if (Checkfloor.Checked == true)
            {
                for (int i = 0; i < ddlfloor.Items.Count; i++)
                {
                    ddlfloor.Items[i].Selected = true;
                }
                txtfloor.Text = "Floor(" + (ddlfloor.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < ddlfloor.Items.Count; i++)
                {
                    ddlfloor.Items[i].Selected = false;
                }
                txtfloor.Text = "--Select--";
            }
            bindroom1();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlfloor_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            int commcount = 0;
            txtfloor.Text = "--Select--";
            Checkfloor.Checked = false;
            for (int i = 0; i < ddlfloor.Items.Count; i++)
            {
                if (ddlfloor.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            if (commcount > 0)
            {
                txtfloor.Text = "Floor(" + commcount.ToString() + ")";
                if (commcount == ddlfloor.Items.Count)
                {
                    Checkfloor.Checked = true;
                }
            }
            bindroom1();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Checkroom_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (Checkroom.Checked == true)
                {
                    for (int i = 0; i < ddlroom.Items.Count; i++)
                    {
                        ddlroom.Items[i].Selected = true;
                        txtroomno.Text = "Room(" + (ddlroom.Items.Count) + ")";
                    }
                }
                else
                {
                    for (int i = 0; i < ddlroom.Items.Count; i++)
                    {
                        ddlroom.Items[i].Selected = false;
                        txtroomno.Text = "--Select--";
                    }
                }
                //  Button2.Focus();
            }
            catch (Exception ex)
            {
            }
        }
        catch
        {
        }
    }

    protected void ddlroom_selectedchanged(object sender, EventArgs e)
    {
        try
        {
            int seatcount = 0;
            Checkroom.Checked = false;
            for (int i = 0; i < ddlroom.Items.Count; i++)
            {
                if (ddlroom.Items[i].Selected == true)
                {
                    seatcount = seatcount + 1;
                }
            }
            if (seatcount == ddlroom.Items.Count)
            {
                txtroomno.Text = "Room(" + seatcount.ToString() + ")";
                Checkroom.Checked = true;
            }
            else if (seatcount == 0)
            {
                txtroomno.Text = "--Select--";
            }
            else
            {
                txtroomno.Text = "Room(" + seatcount.ToString() + ")";
            }
            //   Button2.Focus();
            if (txtroomno.Text != "--Select--")
            {
                for (int i = 0; i < ddlroom.Items.Count; i++)
                {
                    if (ddlroom.Items[i].Selected == true)
                    {
                        //room = ddlroom.Items[i].Text.ToString();
                        //if (roomstatic == "")
                        //{
                        //    roomstatic = room;
                        //}
                        //else
                        //{
                        //    roomstatic = roomstatic + "'" + "," + "'" + room;
                        //}
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            divscrll.Visible = true;
            paneladd.Visible = true;
            bindgrid();
            fpcammarkstaff.Visible = false;
            //bindspread();
            btnnew_click(sender, e);
            gatpasslogin();
        }
        catch
        {
        }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkup3");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }
        catch
        {
        }
    }

    protected void gridview1_OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
        }
        catch
        {
        }
    }

    protected void grd_commond(object sender, EventArgs e)
    {



    }

    protected void Redirect2_Click(object sender, EventArgs e)
    {
        try
        {
            view6();
        }
        catch
        {
        }
    }

    protected void fpcammarkstaff_CellClick(object sender, FarPoint.Web.Spread.SpreadCommandEventArgs e)
    {
        try
        {
            string activerow = fpcammarkstaff.ActiveSheetView.ActiveRow.ToString();
            string activecol = fpcammarkstaff.ActiveSheetView.ActiveColumn.ToString();
            //Cellclick = true;
            //panel8.Visible = true;
            //panel12.Visible = true;
        }
        catch
        {
        }
    }

    protected void fpcammarkstaff_UpdateCommand(object sender, FarPoint.Web.Spread.SpreadCommandEventArgs e)
    {
        try
        {
            string activerow = fpcammarkstaff.Sheets[0].ActiveRow.ToString();
            if (activerow == "0")
            {
                string selecttext = "";
                string actcol = "1";
                selecttext = e.EditValues[Convert.ToInt32(actcol)].ToString();
                for (int i = 1; i < fpcammarkstaff.Sheets[0].RowCount; i++)
                {
                    if (selecttext != "System Object")
                    {
                        fpcammarkstaff.Sheets[0].Cells[i, Convert.ToInt32(actcol)].Text = selecttext.ToString();
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void btngateplus_Click(object sender, EventArgs e)
    {
        try
        {
            pan_gatepass.Visible = true;
            txt_gatepass.Text = "";
            Capgatepass.InnerHtml = "Reason Gate Pass";
        }
        catch
        {
        }
    }

    protected void btngatemin_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch
        {
        }
    }

    protected void txtapply_TextChanged(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnrequestbyplus_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch
        {
        }
    }

    protected void btnrequestbymins_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch
        {
        }
    }

    protected void btnrequestmodeplus_Click(object sender, EventArgs e)
    {
        try
        {
            //pnl_requestmode.Visible = true;
            //txt_requestmode.Text = "";
            //Caption2.InnerHtml = "Request Mode";
        }
        catch
        {
        }
    }

    protected void btnrequestmodemins_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlrequestmode.Items.Count > 0)
            //{
            //    lblerror1.Visible = false;
            //    string collegecode = Session["collegecode"].ToString();
            //    string reason = ddlrequestmode.SelectedItem.ToString();
            //    string reason1 = ddlrequestmode.SelectedValue.ToString();
            //    if (reason != "---Select---")
            //    {
            //        if (reason.Trim().ToLower() != "all" && reason.Trim() != "")
            //        {
            //            string strquery = "if exists(select * from GatePass_Approval where requestmode='" + reason1 + "')select * from GatePass_Approval where requestmode='" + reason1 + "' else delete textvaltable where TextVal='" + reason + "' and TextCriteria='stgrm' and college_code='" + collegecode + "'";
            //            int d = da.update_method_wo_parameter(strquery, "Text");
            //            if (d == -1)
            //            {
            //                lblerror1.Text = "Can't Be Deleted";
            //                lblerror1.Visible = true;
            //            }
            //            //gaterequestmode();
            //        }
            //    }
            //    else
            //    {
            //        lblerror1.Text = "Select Request Mode Then Delete";
            //        lblerror1.Visible = true;
            //    }
            //}
        }
        catch
        {
        }
    }

    protected void btnstaff_click(object sender, EventArgs e)
    {
        try
        {
            Div1.Visible = true;
            fsstaff.Visible = true;
            fsstaff.Sheets[0].RowCount = 0;
            BindCollege();
            loadstaffdep1(collegecode);
            bind_stafType1();
            bind_design1();
            loadfsstaff();
        }
        catch
        {
        }
    }

    protected void BtnView_click(object sender, EventArgs e)
    {
        try
        {
            //Boolean checkflag = false;
            //string date23 = txtapply.Text.ToString();
            //string[] split223 = date23.Split(new Char[] { '/' });
            //string date323 = split223[2].ToString() + "-" + split223[1].ToString() + "-" + split223[0].ToString();
            //string dtime = DateTime.Now.ToShortTimeString().ToString();
            //foreach (GridViewRow gvrow in GridView1.Rows)
            //{
            //    CheckBox chkSelect = (gvrow.FindControl("chkup3") as CheckBox);
            //    if (chkSelect.Checked)
            //    {
            //        checkflag = true;
            //        Label name_active = (Label)gvrow.FindControl("lblrollno");
            //        Label hostel_code = (Label)gvrow.FindControl("lblhostelcode");
            //        Label gatepasscode = new Label();
            //        Label status = new Label();
            //        string strquery = "";
            //        if (btnsave.Text == "Update")
            //        {
            //            gatepasscode = (Label)gvrow.FindControl("lblgatepass");
            //            status = (Label)gvrow.FindControl("lblstatus1");
            //        }
            //        if (btnsave.Text == "Update")
            //        {
            //            strquery = "select file_upload,file_name,file_type,file_status from GatePass_Approval where  Roll_No='" + name_active.Text + "' and gatepassapproval_code='" + gatepasscode.Text + "' ";
            //        }
            //        else
            //        {
            //            strquery = "select file_upload,file_name,file_type,file_status from GatePass_Approval where  Roll_No='" + name_active.Text + "' and access_date='" + date323.ToString() + "' ";
            //        }
            //        ds.Dispose();
            //        ds.Reset();
            //        ds = da.select_method_wo_parameter(strquery, "Text");
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            if (ds.Tables[0].Rows[0]["file_status"].ToString() != null && ds.Tables[0].Rows[0]["file_status"].ToString() != "False")
            //            {
            //                Response.ContentType = ds.Tables[0].Rows[0]["file_type"].ToString();
            //                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ds.Tables[0].Rows[0]["file_name"].ToString() + "\"");
            //                Response.BinaryWrite((byte[])ds.Tables[0].Rows[0]["file_upload"]);
            //                Response.End();
            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('No Attachements')", true);
            //            }
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('No Attachements')", true);
            //        }
            //    }
            //}
        }
        catch
        {
        }
    }

    protected void btnnew_click(object sender, EventArgs e)
    {
        try
        {
            //  txtissueper.Text = "";
            txtapply.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string time = DateTime.Now.ToLongTimeString();
            //string[] split = time.Split(':', ' ');
            //ddlendhour.SelectedValue = split[0].ToString();
            //ddlendmin.SelectedValue = split[1].ToString();
            //ddlenssession.SelectedValue = split[3].ToString();
            //ddlhour.SelectedValue = split[0].ToString();
            //ddlmin.SelectedValue = split[1].ToString();
            //ddlsession.SelectedValue = split[3].ToString();
            //erroemesssagelbl.Visible = false;
            //lblview.Visible = false;
            //BtnView.Visible = false;
            //bindgrid();
        }
        catch
        {
        }
    }

    protected void btnsave_click(object sender, EventArgs e)
    {
        try
        {
            if (rdo_student.Checked == true)
            {
                gatepass();
            }
            else
            {
                gatepass_staff();
            }
        }
        catch
        {
        }
    }

    public void gatepass()
    {
        try
        {
            int s = 0;
            Int64 ReqStaffAppNo = 0;
            string reason = "";
            string mode = "";
            string by = "";
            string hr = Convert.ToString(ddlhour.SelectedItem.Text);
            string min = Convert.ToString(ddlmin.SelectedItem.Text);
            string day = Convert.ToString(ddlsession.SelectedItem.Text);
            string time_exit = hr + ":" + min + ":" + day;
            string nowtime = DateTime.Now.ToString();
            string[] spl = nowtime.Split();
            string[] Spl1 = spl[1].Split(':');

            string hr1 = Convert.ToString(ddlendhour.SelectedItem.Text);
            string min1 = Convert.ToString(ddlendmin.SelectedItem.Text);
            string day1 = Convert.ToString(ddlenssession.SelectedItem.Text);
            int iday = Convert.ToInt32(ddlmin.SelectedItem.Text);
            int iday1 = Convert.ToInt32(ddlendmin.SelectedItem.Text);
            int ihr = Convert.ToInt32(ddlhour.SelectedItem.Text);
            int ihr1 = Convert.ToInt32(ddlendhour.SelectedItem.Text);
            string time_entry = hr1 + ":" + min1 + ":" + day1;
            int starthrvalue = 0;
            int endhrvalue = 0;
            starthrvalue = Convert.ToInt32(hr);
            endhrvalue = Convert.ToInt32(hr1);
            DateTime RequestfromDate = new DateTime();
            RequestfromDate = TextToDate(txtfromdate);
            DateTime RequesttoDate = new DateTime();
            RequesttoDate = TextToDate(txttodate);
            DateTime RequestDate = new DateTime();
            RequestDate = TextToDate(txt_reqtn_gate_date);
            string dt = txtfromdate.Text;
            string[] Split = dt.Split('/');
            DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            string enddt = txttodate.Text;
            Split = enddt.Split('/');
            DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            if (fromdate < todate)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            //string timeconv = "16:23:01";
            //var result = Convert.ToDateTime(timeconv);
            //string test = result.ToString("hh:mm: tt", CultureInfo.CurrentCulture);
            string cuurentdate = DateTime.Now.ToString("dd/MM/yyyy");
            string fdate = Convert.ToString(txtfromdate.Text);
            int hrrr = Convert.ToInt32(hrr);
            string time = DateTime.Now.ToString("HH:mm:ss");
            string[] ay = time.Split(':');
            string val_min = ay[1].ToString();
            int v_min = Convert.ToInt32(val_min);
            string req_code = Convert.ToString(txt_reqtn_gate.Text);
            if (ddlgatepass.SelectedItem.Value != "Select")
            {
                if (ddlgatepass.SelectedItem.Value != "Others")
                {
                    reason = Convert.ToString(ddlgatepass.SelectedItem.Value);
                }
                else
                {
                    string txtreason = Convert.ToString(txt_ddlgatepassreson.Text);
                    reason = subjectcodenew("GRRea", txtreason);
                }
            }
            if (ddlrequest.SelectedItem.Value != "Select")
            {
                if (ddlrequest.SelectedItem.Value != "Others")
                {
                    by = Convert.ToString(ddlrequest.SelectedItem.Value);
                }
                else
                {
                    string txtby = Convert.ToString(txt_gatepassreq.Text);
                    by = subjectcode("GRTyp", txtby);
                }
            }
            if (ddlrequestmode.SelectedItem.Value != "Select")
            {
                if (ddlrequestmode.SelectedItem.Value != "Others")
                {
                    mode = Convert.ToString(ddlrequestmode.SelectedItem.Value);
                }
                else
                {
                    string txtreason = Convert.ToString(txt_gatepassreqmode.Text);
                    mode = subjectcode("GRMod", txtreason);
                }
            }
            string currentdate = DateTime.Now.ToString("dd/MM/yyyy");
            if (txtfromdate.Text == currentdate)
            {
                if (spl[2] == day)
                {
                    string hrs = Spl1[0];
                    string minss = Spl1[1];
                    int hour = 0;
                    int.TryParse(hrs, out hour);
                    int minsu = 0;
                    int.TryParse(minss, out minsu);
                    int hours = 0;
                    int.TryParse(hr, out hours);
                    int minsut = 0;
                    int.TryParse(min, out minsut);
                    if (hour == 12)
                    {
                        if (hours > hour)
                        {
                        }
                    }
                    else if (hours < hour)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Kindly Select The Valid Time";
                        return;
                    }
                    if (hours == hour)
                    {
                        if (minsut < minsu)
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Kindly Select The Valid Time";
                            return;
                        }
                    }
                }
                //magesh 18.7.18
                else if (spl[2] == "AM" && day == "PM")
                {

                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Kindly Select The Valid Time";
                    return;
                }
            }
            // validation for PM AM 
            if (RequestfromDate == RequesttoDate)
            {
                if ((RequestfromDate == RequesttoDate) && ddlsession.SelectedItem.Text == "PM" && ddlenssession.SelectedItem.Text == "AM")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Kindly Select The Valid Time";
                    return;
                }
                currentdate = DateTime.Now.ToString("dd/MM/yyyy");
                if (txtfromdate.Text == currentdate)
                {
                    if (spl[2] == day)
                    {
                        string hrs = Spl1[0];
                        string minss = Spl1[1];
                        int hour = 0;
                        int.TryParse(hrs, out hour);
                        int minsu = 0;
                        int.TryParse(minss, out minsu);
                        int hours = 0;
                        int.TryParse(hr, out hours);
                        int minsut = 0;
                        int.TryParse(min, out minsut);
                        if (hour == 12)
                        {
                            if (hours > hour)
                            {
                            }
                        }
                        else if (hours < hour)
                        {


                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Kindly Select The Valid Time";
                            return;

                        }
                        if (hours == hour)
                        {
                            if (minsut < minsu)
                            {
                                imgdiv2.Visible = true;
                                lbl_alert.Text = "Kindly Select The Valid Time";
                                return;
                            }
                        }
                    }
                    //magesh 21.6.18
                    else if (spl[2] == "AM" && day == "PM")
                    {

                    }
                    else
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Kindly Select The Valid Time";
                        return;
                    }
                }
                else
                {

                }
                //// Validation for mins
                if (ddlhour.SelectedItem.Text == ddlendhour.SelectedItem.Text && ddlsession.SelectedItem.Text == ddlenssession.SelectedItem.Text && iday >= iday1)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Kindly Select The Valid Time";
                    return;
                }
                // Validation for hr
                if (ddlsession.SelectedItem.Text == ddlsession.SelectedItem.Text && iday == iday1 && ihr >= ihr1)
                {
                    if (ddlsession.SelectedItem.Text == ddlenssession.SelectedItem.Text)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Kindly Select The Valid Time";
                        return;
                    }
                }
                if (cuurentdate == fdate && day == day1)
                {
                    //if ((hrrr > ihr) || (v_min > iday1) || (hrrr > ihr && v_min > iday1))
                    //{
                    currentdate = DateTime.Now.ToString("dd/MM/yyyy");
                    if (txtfromdate.Text == currentdate)
                    {
                        if (spl[2] == day)
                        {
                            string hrs = Spl1[0];
                            string minss = Spl1[1];
                            int hour = 0;
                            int.TryParse(hrs, out hour);
                            int minsu = 0;
                            int.TryParse(minss, out minsu);
                            int hours = 0;
                            int.TryParse(hr, out hours);
                            int minsut = 0;
                            int.TryParse(min, out minsut);
                            if (hour == 12)
                            {
                                if (hours > hour)
                                {
                                }
                            }
                            else if (hours < hour)
                            {


                                imgdiv2.Visible = true;
                                lbl_alert.Text = "Kindly Select The Valid Time";
                                return;

                            }
                            if (hours == hour)
                            {
                                if (minsut < minsu)
                                {
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "Kindly Select The Valid Time";
                                    return;
                                }
                            }
                        }
                        //magesh 21.6.18
                        else if (spl[2] == "AM" && day == "PM")
                        {

                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Kindly Select The Valid Time";
                            return;
                        }
                    }
                    else if (hrrr == 12)
                    {

                    }
                    else if (hrrr > ihr)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Kindly Select The Valid Time";
                        return;
                    }
                    else if (hrrr == ihr)
                    {
                        if (v_min > Convert.ToInt32(min))
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Kindly Select The Valid Time";
                            return;
                        }
                    }
                    //}
                }
            }
            string stud = "";
            string appno = "";
            string checkapp = "";
            int not = 0;
            string staff_name = Convert.ToString(txtissueper.Text);
            string[] split = staff_name.Split('-');
            string names = split[0].Trim();
            string staff_code = d2.GetFunction("select appl_no from staffmaster where staff_name='" + names + "'");
            if (staff_code == "0")
            {
                staff_code = Convert.ToString(TextBox1.Text);
            }
            if (staff_code.Trim() != "")
            {
                ReqStaffAppNo = Convert.ToInt64(d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and a.appl_no='" + staff_code + "'"));
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkItemHeader = (CheckBox)GridView1.Rows[i].FindControl("chkup3");
                if (chkItemHeader.Checked == true)
                {
                    Label rollno = (Label)GridView1.Rows[i].FindControl("lblrollno");
                    string appno1 = Convert.ToString(rollno.Text);
                    appno = d2.GetFunction("select app_no  from Registration where Roll_No ='" + appno1 + "'");
                    string namestud = d2.GetFunction("select stud_name from applyn where app_no='" + appno + "'");
                    string stu_type = d2.GetFunction("select Stud_Type  from Registration where Roll_No ='" + appno1 + "'");
                    if (appno1 != "")
                    {
                        string appstage = d2.GetFunction("select ReqAppStatus from RQ_Requisition where RequestType='6' and ReqAppNo='" + appno + "'");
                        // appno of not approve
                        string getstudappno = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=6 and ReqAppStatus='0' and ReqAppNo='" + appno + "'");
                        // totleave permission count
                        string totleavecount = string.Empty;
                        if (stu_type == "hostler" || stu_type == "Hostler" || stu_type == "HOSTLER")
                            totleavecount = d2.GetFunction("select hm.HostelGatePassPerCount from HT_HostelRegistration ht,HM_HostelMaster hm where app_no='" + appno + "'and hm.hostelmasterpk=ht.hostelmasterfk");
                        else
                            totleavecount = d2.GetFunction("select leavecount from gatepasscount  where college_code='" + collegecode1 + "'");
                        if (totleavecount == "")
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Kindly Set The GatePassCount";
                            s = 0;
                            return;
                        }
                        int tolcount = Convert.ToInt32(totleavecount);
                        string approvcount = string.Empty;
                        if (stu_type == "hostler" || stu_type == "Hostler" || stu_type == "HOSTLER")
                            approvcount = d2.GetFunction("select SUM(ReqAppStatus) as Approvecount from HM_HostelMaster hm,HT_HostelRegistration hr,RQ_Requisition req where hm.HostelMasterPK=hr.HostelMasterFK and hr.APP_No=req.ReqAppNo and hr.MemType='1' and ReqAppStatus='1' and hr.APP_No='" + appno + "' and MONTH(RequestDate)=MONTH(GateReqEntryDate) group by HostelGatePassPerCount");
                        else
                            approvcount = d2.GetFunction("select SUM(ReqAppStatus) as Approvecount from gatepasscount,RQ_Requisition req where  ReqAppStatus='1' and req.ReqAppNo='" + appno + "'");
                        string totdays = d2.GetFunction("SELECT DATEDIFF(day, GateReqExitDate, GateReqEntryDate) from RQ_Requisition where RequestType='6' and  ReqAppNo='" + appno + "'");
                        int dayys = Convert.ToInt32(totdays);
                        //int appcount = Convert.ToInt32(approvcount);
                        int appcount = 0;
                        int.TryParse(approvcount, out appcount);
                        string approvcountnot = d2.GetFunction("select count(ReqAppNo) as Approvecount from RQ_Requisition  where ReqAppNo='" + appno + "' and ReqAppStatus='0' and RequestType=6 and MONTH(RequestDate)=MONTH(GateReqEntryDate) ");
                        string getstudappno1 = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=6 and ReqAppStatus='0' and ReqAppNo='" + appno + "'");
                        int approvcountnot1 = Convert.ToInt32(approvcountnot);
                        if (getstudappno1 == appno)
                        {
                            string qur = "select GateReqEntryDate from RQ_Requisition where RequestType=6 and ReqAppNo='" + getstudappno1 + "' and MONTH(RequestDate)=MONTH(GateReqEntryDate) and (GateReqEntryTime >'" + time_exit + "') and GateReqEntryDate>='" + RequestfromDate + "'";
                            ds = d2.select_method_wo_parameter(qur, "Text");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                not = 1;
                            }
                        }
                        if (approvcountnot1 >= 1)
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "You Cannot Give More Than 1 Request Without Of Approval";
                            return;
                        }
                        if (appcount <= tolcount)
                        {
                            if (not != 0)
                            {
                                imgdiv2.Visible = true;
                                lbl_alert.Text = " Already You Are Requested In This Date/Time";
                                return;
                            }
                            else
                            {
                                if (((txtfromdate.Text == txttodate.Text || fromdate > todate) && ((hr == hr1 && min != min1) || (hr != hr1 && min == min1) || (hr != hr1 && min != min1) || (hr == hr1 && min == min1 && day != day1))) || ((txtfromdate.Text != txttodate.Text && fromdate > todate)))
                                {
                                    if (ddlgatepass.SelectedItem.Text != "Select")
                                    {
                                        int reques = 0;
                                        int reqapp = 0;
                                        string checkper = d2.GetFunction("select value from Master_Settings where settings='Leave Approval Permission' and usercode='" + usercode + "' ");
                                        if (checkper == "3")
                                        {
                                            reques = 1;
                                            reqapp = 1;
                                        }
                                        else
                                        {
                                            reques = 0;
                                            reqapp = 0;
                                        }
                                        string query = "insert into RQ_Requisition(RequestType,RequestMode,RequestBy,RequestCode,RequestDate,ReqAppNo,ReqStaffAppNo,ReqAppStaffAppNo,GateReqExitDate,GateReqExitTime,GateReqEntryDate,GateReqEntryTime,GateReqReason,MemType,ReqApproveStage,ReqAppStatus)values('6','" + mode + "','" + by + "','" + req_code + "','" + RequestDate + "','" + appno + "','" + ReqStaffAppNo + "','" + ReqStaffAppNo + "','" + RequestfromDate + "','" + time_exit + "','" + RequesttoDate + "','" + time_entry + "','" + reason + "','1','" + reqapp + "','" + reques + "')";
                                        s = d2.update_method_wo_parameter(query, "Text");
                                        if (sms_req == "1")
                                        {
                                            // sms();
                                        }
                                    }
                                    else
                                    {
                                        imgdiv2.Visible = true;
                                        lbl_alert.Text = "Kindly Select The Reason";
                                        s = 0;
                                        return;
                                    }
                                }
                                else
                                {
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "Kindly Select Correct Date And Time";
                                    s = 0;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "You Don't Have A Gate Pass Permission";
                            s = 0;
                            return;
                        }
                    }
                }
            }
            if (s != 0)
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Saved Successfully";
                ItemReqNo();
                gatepass_clear();
                //magesh 25.5.18
                divscrll.Visible = true;
                paneladd.Visible = true;
                GridView1.Visible = true;
                timevalue();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Please Select Any One Student";
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void gatepass_staff()
    {
        try
        {
            int s = 0;
            Int64 ReqStaffAppNo = 0;
            string reason = "";
            string mode = "";
            string by = "";
            string hr = Convert.ToString(ddlhour.SelectedItem.Text);
            string min = Convert.ToString(ddlmin.SelectedItem.Text);
            string day = Convert.ToString(ddlsession.SelectedItem.Text);
            string time_exit = hr + ":" + min + ":" + day;
            string hr1 = Convert.ToString(ddlendhour.SelectedItem.Text);
            string min1 = Convert.ToString(ddlendmin.SelectedItem.Text);
            string day1 = Convert.ToString(ddlenssession.SelectedItem.Text);
            int iday = Convert.ToInt32(ddlmin.SelectedItem.Text);
            int iday1 = Convert.ToInt32(ddlendmin.SelectedItem.Text);
            int ihr = Convert.ToInt32(ddlhour.SelectedItem.Text);
            int ihr1 = Convert.ToInt32(ddlendhour.SelectedItem.Text);
            string time_entry = hr1 + ":" + min1 + ":" + day1;
            int starthrvalue = 0;
            int endhrvalue = 0;
            starthrvalue = Convert.ToInt32(hr);
            endhrvalue = Convert.ToInt32(hr1);
            DateTime RequestfromDate = new DateTime();
            RequestfromDate = TextToDate(txtfromdate);
            DateTime RequesttoDate = new DateTime();
            RequesttoDate = TextToDate(txttodate);
            DateTime RequestDate = new DateTime();
            RequestDate = TextToDate(txt_reqtn_gate_date);
            string dt = txtfromdate.Text;
            string[] Split = dt.Split('/');
            DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            string enddt = txttodate.Text;
            Split = enddt.Split('/');
            DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            if (fromdate > todate)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            //string timeconv = "16:23:01";
            //var result = Convert.ToDateTime(timeconv);
            //string test = result.ToString("hh:mm: tt", CultureInfo.CurrentCulture);
            string cuurentdate = DateTime.Now.ToString("dd/MM/yyyy");
            string fdate = Convert.ToString(txtfromdate.Text);
            int hrrr = Convert.ToInt32(hrr);
            string time = DateTime.Now.ToString("HH:mm:ss");
            string[] ay = time.Split(':');
            string val_min = ay[1].ToString();
            int v_min = Convert.ToInt32(val_min);
            string req_code = Convert.ToString(txt_reqtn_gate.Text);
            if (ddlgatepass.SelectedItem.Value != "Select")
            {
                if (ddlgatepass.SelectedItem.Value != "Others")
                {
                    reason = Convert.ToString(ddlgatepass.SelectedItem.Value);
                }
                else
                {
                    string txtreason = Convert.ToString(txt_ddlgatepassreson.Text);
                    reason = subjectcodenew("GRRea", txtreason);
                }
            }
            if (ddlrequest.SelectedItem.Value != "Select")
            {
                if (ddlrequest.SelectedItem.Value != "Others")
                {
                    by = Convert.ToString(ddlrequest.SelectedItem.Value);
                }
                else
                {
                    string txtby = Convert.ToString(txt_gatepassreq.Text);
                    by = subjectcode("GRTyp", txtby);
                }
            }
            if (ddlrequestmode.SelectedItem.Value != "Select")
            {
                if (ddlrequestmode.SelectedItem.Value != "Others")
                {
                    mode = Convert.ToString(ddlrequestmode.SelectedItem.Value);
                }
                else
                {
                    string txtreason = Convert.ToString(txt_gatepassreqmode.Text);
                    mode = subjectcode("GRMod", txtreason);
                }
            }
            // validation for PM AM 
            if ((RequestfromDate == RequesttoDate) && ddlsession.SelectedItem.Text == "PM" && ddlenssession.SelectedItem.Text == "AM")
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Kindly Select The Valid Time";
                return;
            }
            // Validation for mins
            if (ddlhour.SelectedItem.Text == ddlendhour.SelectedItem.Text && ddlsession.SelectedItem.Text == ddlenssession.SelectedItem.Text && iday >= iday1)
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Kindly Select The Valid Time";
                return;
            }
            // Validation for hr
            if (ddlsession.SelectedItem.Text == ddlsession.SelectedItem.Text && iday == iday1 && ihr >= ihr1)
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Kindly Select The Valid Time";
                return;
            }
            if (cuurentdate == fdate && day == day1)
            {
                //if ((hrrr > ihr) || (v_min > iday1) || (hrrr > ihr && v_min > iday1))
                //{
                if (hrrr > ihr)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Kindly Select The Valid Time";
                    return;
                }
                else if (hrrr == ihr)
                {
                    if (v_min > Convert.ToInt32(min))
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Kindly Select The Valid Time";
                        return;
                    }
                }
                //}
            }
            string stud = "";
            string appno = "";
            string checkapp = "";
            int not = 0;
            string staff_code = Convert.ToString(txt_pop_search.Text);
            appno = d2.GetFunction("select appl_id from staffmaster s,staff_appl_master sm where s.staff_code='" + staff_code + "' and s.appl_no=sm.appl_no");
            if (appno != "")
            {
                staffcount();
                string appstage = d2.GetFunction("select ReqAppStatus from RQ_Requisition where RequestType='6' and MemType=2 and ReqAppNo='" + appno + "'");
                // appno of not approve
                string getstudappno = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=6 and  MemType=2 and ReqAppStatus='0' and ReqAppNo='" + appno + "'");
                // totleave permission count
                if (staff_per_count == "")
                {
                    staff_per_count = "0";
                }
                int tolcount = Convert.ToInt32(staff_per_count);
                string approvcount = d2.GetFunction("select count(ReqAppStatus) as Approvecount from RQ_Requisition  where  MemType='2' and ReqAppStatus='1' and ReqAppNo='" + appno + "' and MONTH(RequestDate)=MONTH(GateReqEntryDate) ");
                string totdays = d2.GetFunction("SELECT DATEDIFF(day, GateReqExitDate, GateReqEntryDate) from RQ_Requisition where MemType='2' and RequestType='6' and  ReqAppNo='" + appno + "'");
                int dayys = Convert.ToInt32(totdays);
                int appcount = Convert.ToInt32(approvcount);
                string approvcountnot = d2.GetFunction("select count(ReqAppNo) as Approvecount from RQ_Requisition  where ReqAppNo='" + appno + "' and MemType='2'  and ReqAppStatus='0' and RequestType=6 and MONTH(RequestDate)=MONTH(GateReqEntryDate) ");
                string getstudappno1 = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=6 and ReqAppStatus='0' and  MemType='2' and ReqAppNo='" + appno + "'");
                int approvcountnot1 = Convert.ToInt32(approvcountnot);
                if (getstudappno1 == appno)
                {
                    string qur = "select GateReqEntryDate from RQ_Requisition where RequestType=6 and  MemType=2 and ReqAppNo='" + getstudappno1 + "' and MONTH(RequestDate)=MONTH(GateReqEntryDate) and (GateReqEntryTime >'" + time_exit + "') and GateReqEntryDate>='" + RequestfromDate + "'";
                    ds = d2.select_method_wo_parameter(qur, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        not = 1;
                    }
                }
                if (approvcountnot1 >= 1)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "You Cannot Give More Than 1 Request Without Of Approval";
                    return;
                }
                if (appcount <= tolcount)
                {
                    if (not != 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = " Already You Are Requested In This Date/Time";
                        return;
                    }
                    else
                    {
                        if (((txtfromdate.Text == txttodate.Text || fromdate > todate) && ((hr == hr1 && min != min1) || (hr != hr1 && min == min1) || (hr != hr1 && min != min1) || (hr == hr1 && min == min1 && day != day1))) || ((txtfromdate.Text != txttodate.Text && fromdate > todate)))
                        {
                            if (ddlgatepass.SelectedItem.Text != "Select")
                            {
                                string query = "insert into RQ_Requisition(RequestType,RequestMode,RequestBy,RequestCode,RequestDate,ReqAppNo,ReqStaffAppNo,GateReqExitDate,GateReqExitTime,GateReqEntryDate,GateReqEntryTime,GateReqReason,MemType,ReqApproveStage)values('6','" + mode + "','" + by + "','" + req_code + "','" + RequestDate + "','" + appno + "','" + appno + "','" + RequestfromDate + "','" + time_exit + "','" + RequesttoDate + "','" + time_entry + "','" + reason + "','2','0')";
                                s = d2.update_method_wo_parameter(query, "Text");
                                //if (sms_req == "1")
                                //{
                                //    sms();
                                //}
                            }
                            else
                            {
                                imgdiv2.Visible = true;
                                lbl_alert.Text = "Kindly Select The Reason";
                                s = 0;
                                return;
                            }
                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Kindly Select Correct Date And Time";
                            s = 0;
                            return;
                        }
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "You Donnot Have A Leave Permission";
                    s = 0;
                    return;
                }
            }
            if (s != 0)
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Saved Successfully";
                ItemReqNo();
                gatepass_clear();
                timevalue();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Please Select Any One Student";
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void sms()
    {
        access1();
        user_id = d2.GetFunction("select SMS_User_ID from Track_Value where college_code='" + collegecode1 + "'");
        string getval = d2.GetUserapi(user_id);
        string[] spret = getval.Split('-');
        if (spret.GetUpperBound(0) == 1)
        {
            SenderID = spret[0].ToString();
            Password = spret[1].ToString();
            Session["api"] = user_id;
            Session["senderid"] = SenderID;
        }
        string rq_fk = d2.GetFunction("select RequisitionPK from RQ_Requisition where RequisitionPK=((select max(RequisitionPK) from RQ_Requisition where RequestType=6))");
        string reasongate = d2.GetFunction("select GateReqReason from RQ_Requisition where RequestType=6 and requisitionpk='" + rq_fk + "'");
        string reason = d2.GetFunction("select MasterValue from CO_MasterValues where MasterCriteria='GRRea' and MasterCode='" + reasongate + "'");
        string appnumber = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=6 and RequisitionPK='" + rq_fk + "'");
        string q = "select a.stud_name, r.Roll_no,r.Stud_Type,c.Course_Name,dt.Dept_Name,r.Current_Semester ,r.Sections, a.parent_name from applyn a,Registration r ,Degree d,course c,Department dt where   a.app_no=r.app_no   and  r.degree_code=d.Degree_Code and d.Course_Id=c.Course_Id and  d.Dept_Code=dt.Dept_Code and a.app_no='" + appnumber + "'";
        ds = d2.select_method_wo_parameter(q, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string name = Convert.ToString(ds.Tables[0].Rows[0]["stud_name"]);
            string course = Convert.ToString(ds.Tables[0].Rows[0]["Course_Name"]);
            string dept = Convert.ToString(ds.Tables[0].Rows[0]["Dept_Name"]);
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            // strmsg = "Your ward miss " + name + "-" + course + "-" + dept + " Requested from home on  " + exitdate + " to " + entrydate;
            strmsg = " Your ward Ms." + name + " requested to go for " + reason + " on " + date + ".";
        }
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chkItemHeader = (CheckBox)GridView1.Rows[i].FindControl("chkup3");
            if (chkItemHeader.Checked == true)
            {
                Label rollno = (Label)GridView1.Rows[i].FindControl("lblrollno");
                string appno1 = Convert.ToString(rollno.Text);
                string appno = d2.GetFunction("select app_no  from Registration where Roll_No ='" + appno1 + "'");
                if (sms_mom == "1")
                {
                    string momnum = d2.GetFunction("select parentM_Mobile from applyn where app_no='" + appno + "'");
                    mobilenos = momnum;
                    if (mobilenos != "")
                    {
                        string strpath = "http://dnd.airsmsmarketing.info/api/sendmsg.php?user=" + user_id + "&pass=" + Password + "&sender=" + SenderID + "&phone=" + mobilenos + "&text=" + strmsg + "&priority=ndnd&stype=normal";
                        //string strpath = "http://dnd.airsmsmarketing.info/api/sendmsg.php?user=" + user_id + "&pass=" + Password + "&sender=" + SenderID + "&phone=" + mobilenos + "&text=" + strmsg + "&priority=ndnd&stype=normal";
                        smsreport(strpath, isst);
                    }
                }
                if (sms_dad == "2")
                {
                    string fathernum = d2.GetFunction("select parentF_Mobile from applyn where app_no='" + appno + "'");
                    mobilenos = fathernum;
                    //mobilenos = "9585698019";
                    if (mobilenos != "")
                    {
                        string strpath = "http://dnd.airsmsmarketing.info/api/sendmsg.php?user=" + user_id + "&pass=" + Password + "&sender=" + SenderID + "&phone=" + mobilenos + "&text=" + strmsg + "&priority=ndnd&stype=normal";
                        smsreport(strpath, isst);
                    }
                }
                if (sms_stud == "3")
                {
                    string studnum = d2.GetFunction("select Student_Mobile from applyn where app_no='" + appno + "'");
                    mobilenos = studnum;
                    // mobilenos = "9585698019";
                    if (mobilenos != "")
                    {
                        string strpath = "http://dnd.airsmsmarketing.info/api/sendmsg.php?user=" + user_id + "&pass=" + Password + "&sender=" + SenderID + "&phone=" + mobilenos + "&text=" + strmsg + "&priority=ndnd&stype=normal";
                        smsreport(strpath, isst);
                    }
                }
            }
        }
    }

    public void smsreport(string uril, string isstaff)
    {
        try
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            WebRequest request = WebRequest.Create(uril);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader sr = new StreamReader(data);
            string strvel = sr.ReadToEnd();
            string groupmsgid = "";
            groupmsgid = strvel.Trim().ToString();
            int sms = 0;
            string smsreportinsert = "";
            string[] split_id = groupmsgid.Split(' ');
            string[] split_mobileno = mobilenos.Split(new Char[] { ',' });
            for (int icount = 0; icount <= split_mobileno.GetUpperBound(0); icount++)
            {
                string group_id = split_id[icount].ToString();
                smsreportinsert = "insert into smsdeliverytrackmaster (mobilenos,groupmessageid,message,college_code,isstaff,date )values( '" + split_mobileno[icount] + "','" + group_id + "','" + strmsg + "','" + collegecode1 + "','" + isstaff + "','" + date + "' )";
                sms = d2.insert_method(smsreportinsert, hat, "Text");
            }
        }
        catch
        {
        }
    }

    protected void gatepassadd_Click(object sender, EventArgs e)
    {
    }

    protected void gatepassexit_Click(object sender, EventArgs e)
    {
        try
        {
            pan_gatepass.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    public void bindhostelname()
    {
        try
        {
            ddlhostel1.Items.Clear();
            ds.Clear();
            //string query = "select hostelbuildingfk,HostelMasterPK,HostelName  from HM_HostelMaster  where CollegeCode in ('" + collegecode1 + "') order by HostelMasterPK ";
            ds = d2.BindHostel_inv();
            //ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlhostel1.DataSource = ds;
                ddlhostel1.DataTextField = "HostelName";
                ddlhostel1.DataValueField = "HostelMasterPK";
                ddlhostel1.DataBind();
                if (ddlhostel1.Items.Count > 0)
                {
                    for (int i = 0; i < ddlhostel1.Items.Count; i++)
                    {
                        ddlhostel1.Items[i].Selected = true;
                    }
                    txthostgelname1.Text = "Hostel Name(" + ddlhostel1.Items.Count + ")";
                    Checkhostel1.Checked = true;
                }
            }
            else
            {
                txthostgelname1.Text = "--Select--";
                Checkhostel1.Checked = false;
            }
        }
        catch
        {
        }
    }

    public void bindbatch()
    {
        try
        {
            ddlbatch1.Items.Clear();
            hat.Clear();
            // string sqlyear = "select distinct batch_year from Registration where batch_year<>'-1' and batch_year<>'' and cc=0 and delflag=0 and exam_flag<>'debar' order by batch_year desc";
            ds = d2.BindBatch();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbatch1.DataSource = ds;
                ddlbatch1.DataTextField = "batch_year";
                ddlbatch1.DataValueField = "batch_year";
                ddlbatch1.DataBind();
            }
        }
        catch
        {
        }
    }

    public void bindbuild1()
    {
        try
        {
            cheklist_Build.Items.Clear();
            //txt_building.Text = "---Select---";
            //cb_building.Checked = false;
            string build = "";
            if (ddlhostel1.Items.Count > 0)
            {
                for (int i = 0; i < ddlhostel1.Items.Count; i++)
                {
                    if (ddlhostel1.Items[i].Selected == true)
                    {
                        if (build == "")
                        {
                            build = Convert.ToString(ddlhostel1.Items[i].Value);
                        }
                        else
                        {
                            build = build + "'" + "," + "'" + Convert.ToString(ddlhostel1.Items[i].Value);
                        }
                    }
                }
            }
            string bul = "";
            if (build != "")
            {
                bul = d2.GetBuildingCode_inv(build);
                ds = d2.BindBuilding(bul);
                //ds = d2.select_method_wo_parameter(query, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cheklist_Build.DataSource = ds;
                    cheklist_Build.DataTextField = "Building_Name";
                    cheklist_Build.DataValueField = "code";
                    cheklist_Build.DataBind();
                    if (cheklist_Build.Items.Count > 0)
                    {
                        for (int i = 0; i < cheklist_Build.Items.Count; i++)
                        {
                            cheklist_Build.Items[i].Selected = true;
                        }
                        checkBuild.Checked = true;
                        txt_Build.Text = "Building Name(" + cheklist_Build.Items.Count + ")";
                    }
                }
            }
            else
            {
                checkBuild.Checked = false;
                txt_Build.Text = "--Select--";
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void bindfloor1()
    {
        try
        {
            string floorname = "";
            ddlfloor.Items.Clear();
            txtfloor.Text = "---Select---";
            Checkfloor.Checked = false;
            if (cheklist_Build.Items.Count > 0)
            {
                for (int i = 0; i < cheklist_Build.Items.Count; i++)
                {
                    if (cheklist_Build.Items[i].Selected == true)
                    {
                        if (floorname == "")
                        {
                            floorname = Convert.ToString(cheklist_Build.Items[i].Value);
                        }
                        else
                        {
                            floorname = floorname + "'" + "," + "'" + Convert.ToString(cheklist_Build.Items[i].Value);
                        }
                    }
                }
            }
            if (floorname != "")
            {
                //string qq = "select distinct Floor_Name,Floorpk  from Floor_Master where Building_Name in('" + buildingName + "')";
                //string qq = "select distinct floor_name,floorpk from HT_HostelRegistration h,Floor_Master fm where h.FloorFK=fm.Floorpk and fm.College_Code='" + collegecode1 + "' and BuildingFK in('" + floorname + "')";
                string qq = "select distinct floor_name,floorpk from HT_HostelRegistration h,Floor_Master fm where h.FloorFK=fm.Floorpk and BuildingFK in('" + floorname + "')";
                ds = d2.select_method_wo_parameter(qq, "Text");
                //ds = d2.BindFloor_new(floorname);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlfloor.DataSource = ds;
                    ddlfloor.DataTextField = "Floor_Name";
                    ddlfloor.DataValueField = "FloorPK";
                    ddlfloor.DataBind();
                    if (ddlfloor.Items.Count > 0)
                    {
                        for (int i = 0; i < ddlfloor.Items.Count; i++)
                        {
                            ddlfloor.Items[i].Selected = true;
                        }
                        txtfloor.Text = "Floor Name(" + ddlfloor.Items.Count + ")";
                        Checkfloor.Checked = true;
                    }
                }
            }
            else
            {
                Checkfloor.Checked = false;
                txtfloor.Text = "--Select--";
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void bindroom1()
    {
        try
        {
            ddlroom.Items.Clear();
            txtroomno.Text = "---Select---";
            Checkroom.Checked = false;
            string flooor = "";
            string room = "";
            if (cheklist_Build.Items.Count > 0)
            {
                for (int i = 0; i < cheklist_Build.Items.Count; i++)
                {
                    if (cheklist_Build.Items[i].Selected == true)
                    {
                        if (flooor == "")
                        {
                            flooor = Convert.ToString(cheklist_Build.Items[i].Text);
                        }
                        else
                        {
                            flooor = flooor + "'" + "," + "'" + Convert.ToString(cheklist_Build.Items[i].Text);
                        }
                    }
                }
            }
            if (ddlfloor.Items.Count > 0)
            {
                for (int i = 0; i < ddlfloor.Items.Count; i++)
                {
                    if (ddlfloor.Items[i].Selected == true)
                    {
                        if (room == "")
                        {
                            room = Convert.ToString(ddlfloor.Items[i].Text);
                        }
                        else
                        {
                            room = room + "'" + "," + "'" + Convert.ToString(ddlfloor.Items[i].Text);
                        }
                    }
                }
            }
            if (flooor != "" && room != "")
            {
                ds = d2.BindRoomtype(room, flooor);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlroom.DataSource = ds;
                    ddlroom.DataTextField = "room_type";
                    ddlroom.DataValueField = "room_type";
                    ddlroom.DataBind();
                    if (ddlroom.Items.Count > 0)
                    {
                        for (int row = 0; row < ddlroom.Items.Count; row++)
                        {
                            ddlroom.Items[row].Selected = true;
                        }
                        txtroomno.Text = "Room Name(" + ddlroom.Items.Count + ")";
                        Checkroom.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void branch()
    {
        try
        {
            string query1 = "";
            string buildvalue1 = "";
            string build1 = "";
            if (ddldegree1.Items.Count > 0)
            {
                for (int i = 0; i < ddldegree1.Items.Count; i++)
                {
                    build1 = ddldegree1.SelectedValue;
                    if (buildvalue1 == "")
                    {
                        buildvalue1 = build1;
                    }
                    else
                    {
                        buildvalue1 = buildvalue1 + "'" + "," + "'" + build1;
                    }
                }
                query1 = "select distinct degree.degree_code,department.dept_name,degree.Acronym  from degree,department,course,deptprivilages where course.course_id=degree.course_id  and department.dept_code=degree.dept_code and course.college_code = degree.college_code and department.college_code = degree.college_code and degree.course_id in('" + buildvalue1 + "') and  deptprivilages.Degree_code=degree.Degree_code";
                ds = d2.select_method(query1, hat, "Text");
                ddldepart1.DataSource = ds;
                ddldepart1.DataTextField = "dept_name";
                ddldepart1.DataValueField = "degree_code";
                ddldepart1.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void binddeg()
    {
        try
        {
            ds.Clear();
            string query = "";
            //if (usercode != "")
            //{
            //    query = "select distinct degree.course_id,course.course_name from degree,course,deptprivilages  where course.course_id=degree.course_id and course.college_code = degree.college_code  and degree.college_code='" + collegecode1 + "' and deptprivilages.Degree_code=degree.Degree_code and   user_code=" + usercode + "";
            //}
            //else
            //{
            query = "select distinct degree.course_id,course.course_name  from degree,course,deptprivilages where  course.course_id=degree.course_id and course.college_code = degree.college_code   and degree.college_code='" + ddl_gat_collegename.SelectedItem.Value + "' and deptprivilages.Degree_code=degree.Degree_code order by degree.course_id ";
            //}
            ds = d2.select_method_wo_parameter(query, "Text");
            ddldegree1.DataSource = ds;
            ddldegree1.DataTextField = "course_name";
            ddldegree1.DataValueField = "course_id";
            ddldegree1.DataBind();
        }
        catch
        {
        }
    }

    public void bindsem1()
    {
        try
        {
            DataSet ds3 = new DataSet();
            ddlsem1.Items.Clear();
            Boolean first_year;
            first_year = false;
            int duration = 0;
            int i = 0;
            string sqluery = "select distinct ndurations,first_year_nonsemester from ndegree where degree_code= (" + ddldepart1.SelectedValue + ") and batch_year  = (" + ddlbatch1.SelectedValue + ") and college_code=" + ddl_gat_collegename.SelectedItem.Value + "";
            ds3 = da.select_method_wo_parameter(sqluery, "text");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                first_year = Convert.ToBoolean(ds3.Tables[0].Rows[0]["first_year_nonsemester"]);
                duration = Convert.ToInt16(ds3.Tables[0].Rows[0]["ndurations"]);
                for (i = 1; i <= duration; i++)
                {
                    if (first_year == false)
                    {
                        ddlsem1.Items.Add(i.ToString());
                    }
                    else if (first_year == true && i != 2)
                    {
                        ddlsem1.Items.Add(i.ToString());
                    }
                }
                semstatic = ddlsem1.SelectedValue;
            }
            else
            {
                sqluery = "select distinct duration,first_year_nonsemester  from degree where degree_code in (" + ddldepart1.SelectedValue + ") and college_code=" + collegecode1 + "";
                ddlsem1.Items.Clear();
                ds3 = da.select_method_wo_parameter(sqluery, "text");
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    first_year = Convert.ToBoolean(ds3.Tables[0].Rows[0]["first_year_nonsemester"]);
                    duration = Convert.ToInt16(ds3.Tables[0].Rows[0]["duration"]);
                    for (i = 1; i <= duration; i++)
                    {
                        if (first_year == false)
                        {
                            ddlsem1.Items.Add(i.ToString());
                        }
                        else if (first_year == true && i != 2)
                        {
                            ddlsem1.Items.Add(i.ToString());
                        }
                    }
                    semstatic = ddlsem1.SelectedValue;
                }
            }
        }
        catch
        {
        }
    }

    public void BindSectionDetail()
    {
        try
        {
            ddlsec1.Items.Clear();
            if (ddlsem1.Text != "")
            {
                string branch = ddldepart1.SelectedValue.ToString();
                string batch = ddlbatch1.SelectedValue.ToString();
                string sqlquery = "select distinct sections from registration where batch_year=" + batch + " and degree_code=" + branch + " and sections<>'-1' and sections<>' ' and delflag=0 and exam_flag<>'Debar'";
                DataSet ds = new DataSet();
                ds = da.select_method_wo_parameter(sqlquery, "text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlsec1.DataSource = ds;
                    ddlsec1.DataTextField = "sections";
                    ddlsec1.DataValueField = "sections";
                    ddlsec1.DataBind();
                    secstatci = ddlsec1.SelectedValue;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["sections"].ToString() == "")
                    {
                        ddlsec1.Enabled = false;
                    }
                    else
                    {
                        ddlsec1.Enabled = true;
                    }
                }
                else
                {
                    ddlsec1.Enabled = false;
                }
            }
        }
        catch
        {
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Getname1(string prefixText)
    {
        DataSet dt = new DataSet();
        DAccess2 dsa = new DAccess2();
        List<string> CityNames = new List<string>();
        string strsql = "select distinct r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code,hs.Floor_Name,hs.Room_Name  from Hostel_StudentDetails hs,Registration r,Hostel_Details hd where hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and r.stud_name like '" + prefixText + "%'";
        dt = dsa.select_method_wo_parameter(strsql, "text");
        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
        {
            CityNames.Add(dt.Tables[0].Rows[i]["stud_name"].ToString());
        }
        return CityNames;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Getnamegatpass(string prefixText)
    {
        DataSet dt = new DataSet();
        DAccess2 dsa = new DAccess2();
        List<string> CityNames = new List<string>();
        string strsql = string.Empty;
        // string strsql = "select distinct r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code,hs.Floor_Name,hs.Room_Name  from Hostel_StudentDetails hs,Registration r,Hostel_Details hd where hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and  Stud_Type='Hostler' and r.stud_name like '" + prefixText + "%'";
        //magesh 24.5.18
        // string strsql = "select r.Stud_Name from HT_HostelRegistration h,Registration r where h.APP_No =r.App_No and ISNULL(IsVacated,'0')=0 and  isnull(IsDiscontinued,0) =0 and ISNULL( IsSuspend,0) =0 and  r.stud_name like '" + prefixText + "%'";
        //strsql = "select r.Stud_Name +'-'+r.Roll_No+'-'+ISNULL(  a.parent_name,'') as stud_name from Registration r,applyn a where a.app_no=r.app_no and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and r.stud_name like '" + prefixText + "%'";//magesh 24.5.18 and ISNULL(IsVacated,'0')=0
        if (checksstu == "dayscholar")
        {
            strsql = "select r.Stud_Name +'-'+r.Roll_No+'-'+ISNULL(  a.parent_name,'') as stud_name from Registration r,applyn a where a.app_no=r.app_no and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and r.app_no not in(select r.app_no from HT_HostelRegistration h,Registration r where h.APP_No =r.App_No and isnull(IsVacated,'0')=0 and  isnull(IsDiscontinued,0) =0 and ISNULL( IsSuspend,0) =0  )  and r.college_code='" + col_code + "' and r.stud_name like '" + prefixText + "%'";
        }
        else if (checksstu == "ALL")
            strsql = "select r.Stud_Name +'-'+r.Roll_No+'-'+ISNULL(  a.parent_name,'') as stud_name from Registration r,applyn a where a.app_no=r.app_no and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and r.college_code='" + col_code + "' and r.stud_name like '" + prefixText + "%'";//magesh 24.5.18 and ISNULL(IsVacated,'0')=0
        else
            strsql = "select  r.Stud_Name +'-'+r.Roll_No+'-'+ISNULL(  a.parent_name,'') as stud_name from HT_HostelRegistration h,Registration r,applyn a where  a.app_no=r.app_no and h.APP_No =r.App_No and ISNULL(IsVacated,'0')=0 and  isnull(IsDiscontinued,0) =0 and ISNULL( IsSuspend,0) =0 and r.college_code='" + col_code + "' and  r.stud_name like '" + prefixText + "%'";

        dt = dsa.select_method_wo_parameter(strsql, "text");
        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
        {
            CityNames.Add(dt.Tables[0].Rows[i]["stud_name"].ToString());
        }
        return CityNames;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Getroll(string prefixText)
    {
        DataSet dt = new DataSet();
        DAccess2 dsa = new DAccess2();
        List<string> CityNames = new List<string>();
        string strsql = string.Empty;
        //string strsql = "select r.Roll_No from HT_HostelRegistration h,Registration r where h.APP_No =r.App_No and isnull(IsVacated,'0')=0 and  isnull(IsDiscontinued,0) =0 and ISNULL( IsSuspend,0) =0  and r.roll_no like '" + prefixText + "%'";


        if (checksstu == "dayscholar")
        {
            //strsql = "select r.Roll_No +'-'+r.Stud_Name+'-'+ISNULL(  a.parent_name,'') as roll_no from Registration r,applyn a  where a.app_no=r.app_no and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and r.roll_no like '" + prefixText + "%'";
            strsql = "select r.Roll_No +'-'+r.Stud_Name+'-'+ISNULL(  a.parent_name,'') as roll_no from Registration r,applyn a  where a.app_no=r.app_no and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and r.app_no not in(select r.app_no from HT_HostelRegistration h,Registration r where h.APP_No =r.App_No and isnull(IsVacated,'0')=0 and  isnull(IsDiscontinued,0) =0 and ISNULL( IsSuspend,0) =0  ) and r.college_code='" + col_code + "' and r.roll_no like '" + prefixText + "%'";
        }
        else if (checksstu == "ALL")
            strsql = "select r.Roll_No +'-'+r.Stud_Name+'-'+ISNULL(  a.parent_name,'') as roll_no from Registration r,applyn a where a.app_no=r.app_no and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and r.college_code='" + col_code + "' and r.Roll_No like '" + prefixText + "%'";//magesh 24.5.18 and ISNULL(IsVacated,'0')=0
        else
            strsql = "select r.Roll_No +'-'+r.Stud_Name+'-'+ISNULL(  a.parent_name,'') as roll_no from HT_HostelRegistration h,Registration r,applyn a where a.app_no=r.app_no and h.APP_No =r.App_No and isnull(IsVacated,'0')=0 and  isnull(IsDiscontinued,0) =0 and ISNULL( IsSuspend,0) =0 and r.college_code='" + col_code + "'  and r.roll_no like '" + prefixText + "%'";
        dt = dsa.select_method_wo_parameter(strsql, "text");
        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
        {
            CityNames.Add(dt.Tables[0].Rows[i]["roll_no"].ToString());
        }
        return CityNames;
    }

    public void bindgrid()
    {
        try
        {
            if (Chkhostel.Checked == true)
            {
                string hostel = "";
                string hostel1 = "";
                string floor = "";
                string floor1 = "";
                string room = "";
                string room1 = "";
                string bulid = "";
                string bulid1 = "";
                for (int i = 0; i < ddlhostel1.Items.Count; i++)
                {
                    if (ddlhostel1.Items[i].Selected == true)
                    {
                        hostel = ddlhostel1.Items[i].Value.ToString();
                        if (hostel1 == "")
                        {
                            hostel1 = hostel;
                        }
                        else
                        {
                            hostel1 = hostel1 + "," + hostel;
                        }
                    }
                }
                if (hostel1 != "")
                {
                }
                else
                {
                    GridView1.Visible = false;
                    erroemesssagelbl.Visible = true;
                    erroemesssagelbl.Text = "Please Select Any Hostel";
                    fpcammarkstaff.Visible = false;
                    paneladd.Visible = false;
                    return;
                }
                for (int i = 0; i < cheklist_Build.Items.Count; i++)
                {
                    if (cheklist_Build.Items[i].Selected == true)
                    {
                        bulid = cheklist_Build.Items[i].Value.ToString();
                        if (bulid1 == "")
                        {
                            bulid1 = bulid;
                        }
                        else
                        {
                            bulid1 = bulid1 + "'" + "," + "'" + bulid;
                        }
                    }
                }
                if (bulid1 != "")
                {
                }
                else
                {
                    GridView1.Visible = false;
                    erroemesssagelbl.Visible = true;
                    erroemesssagelbl.Text = "Please Select Any Building";
                    fpcammarkstaff.Visible = false;
                    paneladd.Visible = false;
                    return;
                }
                for (int i = 0; i < ddlfloor.Items.Count; i++)
                {
                    if (ddlfloor.Items[i].Selected == true)
                    {
                        floor = ddlfloor.Items[i].Value.ToString();
                        if (floor1 == "")
                        {
                            floor1 = floor;
                        }
                        else
                        {
                            floor1 = floor1 + "'" + "," + "'" + floor;
                        }
                    }
                }
                if (floor1 != "")
                {
                }
                else
                {
                    GridView1.Visible = false;
                    erroemesssagelbl.Visible = true;
                    erroemesssagelbl.Text = "Please Select Any Floor";
                    fpcammarkstaff.Visible = false;
                    paneladd.Visible = false;
                    return;
                }
                for (int i = 0; i < ddlroom.Items.Count; i++)
                {
                    if (ddlroom.Items[i].Selected == true)
                    {
                        room = ddlroom.Items[i].Text.ToString();
                        if (room1 == "")
                        {
                            room1 = room;
                        }
                        else
                        {
                            room1 = room1 + "'" + "," + "'" + room;
                        }
                    }
                }
                //if (room1 != "")
                //{
                //}
                //else
                //{
                //    GridView1.Visible = false;
                //    erroemesssagelbl.Visible = true;
                //    erroemesssagelbl.Text = "Please Select Any Room";
                //    fpcammarkstaff.Visible = false;
                //    paneladd.Visible = false;
                //    return;
                //}
                string namestudent = "";
                string roll_no = "";
                if (TextBox5.Text != "")
                {
                    namestudent = " and r.stud_name='" + TextBox5.Text + "'";
                }
                else
                {
                    namestudent = "";
                }
                if (TextBox78.Text != "")
                {
                    roll_no = " and r.roll_no='" + TextBox78.Text + "'";
                }
                else
                {
                    roll_no = "";
                }
                string test = "";
                if (ddlsec1.Text == "")
                {
                    test = "";
                }
                else
                {
                    test = "and r.Sections='" + ddlsec1.SelectedValue + "'";
                }
                // string sqlquery = " select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code,hs.Floor_Name,hs.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections) as Degree  from Hostel_StudentDetails hs,Registration r,Hostel_Details hd,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and Stud_Type='Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ") and Vacated = 0 and Relived = 0 and Suspension = 0 and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester ='" + ddlsem1.SelectedValue + "'   and hs.Hostel_Code in(" + hostel1 + ") and hs.Building_Name in('" + bulid1 + "') and hs.Floor_Name in('" + floor1 + "') " + test + " and hs.Room_Type in('" + room1 + "') " + namestudent + " " + roll_no + "";           
                string sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections)as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ") and ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester='" + ddlsem1.SelectedValue + "'    and hs.HostelMasterFK in(" + hostel1 + ") and hs.BuildingFK in('" + bulid1 + "')and hs.FloorFK in('" + floor1 + "')";
                ds2 = da.select_method_wo_parameter(sqlquery, "text");
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
                GridView1.Columns[10].Visible = true;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    erroemesssagelbl.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = ds2;
                    GridView1.DataBind();
                    if (Session["Rollflag"].ToString() == "0")
                    {
                        GridView1.Columns[2].Visible = false;
                    }
                    if (Session["Regflag"].ToString() == "0")
                    {
                        GridView1.Columns[3].Visible = false;
                    }
                    //if (Session["Studflag"].ToString() == "0")
                    //{
                    //    GridView1.Columns[4].Visible = false;
                    //}
                }
                else
                {
                    erroemesssagelbl.Visible = true;
                    erroemesssagelbl.Text = "No Records Found";
                    GridView1.Visible = false;
                    paneladd.Visible = false;
                }
            }
            if (Chkdesch.Checked == true)
            {
                dayscholar();
            }
            if (Chkall.Checked == true)
            {
                dayscholar1();
            }
        }
        catch
        {
        }
    }

    public void ddlstaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstaff.SelectedItem.Value == "0")
        {
            txt_search.Visible = true;
            txt_search1.Visible = false;
            txt_search1.Text = "";
        }
        else
        {
            txt_search.Visible = false;
            txt_search1.Visible = true;
            txt_search.Text = "";
        }
        loadfsstaff();
    }

    public void txt_search_TextChanged(object sender, EventArgs e)
    {
        loadfsstaff();
    }

    public void txt_search1_TextChanged(object sender, EventArgs e)
    {
        loadfsstaff();
    }

    public void btnstaffadd_Click(object sender, EventArgs e)
    {
        try
        {
            string activerow = fsstaff.ActiveSheetView.ActiveRow.ToString();
            if (Convert.ToInt32(activerow.ToString()) > 0)
            {
                string name_active = fsstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Text;
                string des_active = fsstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 2].Text;
                txtissueper.Text = name_active.ToString();
                txt_staff_code.Text = des_active.ToString();
                txtstaff_co.Text = fsstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 2].Text;
                Div1.Visible = false;
                DataTable dt_get_staff = d2.select_method_wop_table("select  s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + ddlcollege.SelectedItem.Value + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.staff_code = '" + des_active + "' and d.collegeCode=s.college_code", "Text");
                // college code added by poomalar
                txt_staff_code.Text = des_active;
                txt_staff_name.Text = name_active;
                txt_dep.Text = dt_get_staff.Rows[0]["dept"].ToString();
                txt_des.Text = dt_get_staff.Rows[0]["design"].ToString();
                imagestaff.ImageUrl = "~/Handler/staffphoto.ashx?Staff_Code=" + txt_staff_code.Text;
                string photo = d2.GetFunction("select photo from staffphoto where staff_code='" + txt_staff_code.Text + "'");
                if (photo == "0")
                {
                    imagestaff.ImageUrl = "image/Gender Neutral User Filled-100(1).png";
                }
                bindgrid2();
                BindLeave();
                if (requestpermissioncheck != "3")
                {
                    bindgrid_approvalstaff();
                }
                BindGridview();
                gridView2.Visible = true;
                altersubject();
            }
            else
            {
                ermsg.Visible = true;
                ermsg.Text = "Please Select Staff";
            }
        }
        catch
        {
        }
    }

    public void exitpop_Click(object sender, EventArgs e)
    {
        Div1.Visible = false;
    }

    public void fsstaff_CellClick(object sender, EventArgs e)
    {
    }

    public void imagebtnpop1close_Click(object sender, EventArgs e)
    {
        Div1.Visible = false;
    }

    void BindCollege()
    {
        try
        {
            //   string srisql = "select collname,college_code from collinfo";
            string usertype = "";
            if (Session["group_code"] != null && (Convert.ToString(Session["group_code"]).Trim() != "") && (Convert.ToString(Session["group_code"]).Trim() != "0") && (Convert.ToString(Session["group_code"]).Trim() != "-1"))
            {
                string code = Convert.ToString(Session["group_code"]).Trim().Split(';')[0];
                usertype = " and group_code='" + code + "'";
                // usertype = " group_code='" + Convert.ToString(Session["group_code"]).Trim() + "'";
            }
            else if (Session["usercode"] != null)
            {
                usertype = " and user_code='" + Convert.ToString(Session["usercode"]).Trim() + "'";
            }
            string srisql = " select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where cp.college_code=cf.college_code " + usertype + "";
            ds.Clear();
            ds = da.select_method_wo_parameter(srisql, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlcollege.DataSource = ds;
                ddlcollege.DataTextField = "collname";
                ddlcollege.DataValueField = "college_code";
                ddlcollege.DataBind();
                ddl_gat_collegename.DataSource = ds;
                ddl_gat_collegename.DataTextField = "collname";
                ddl_gat_collegename.DataValueField = "college_code";
                ddl_gat_collegename.DataBind();
            }
        }
        catch
        {
        }
    }

    public void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        string collegcodee = Convert.ToString(ddlcollege.SelectedItem.Value);
        loadstaffdep1(collegcodee);
        bind_stafType1();
        bind_design1();
        fsstaff.Visible = false;
        btnstaffadd.Visible = false;
        lbl_totalstaffcount.Visible = false;
        mulicollg = Convert.ToString(ddlcollege.SelectedItem.Value);
    }

    public void ddl_gat_collegename_SelectedIndexChanged(object sender, EventArgs e)
    {
        col_code = Convert.ToString(ddl_gat_collegename.SelectedValue);
        binddeg();
        branch();
        bindsem1();
        bindhostelname();
        bindbuild1();
        bindfloor1();
        bindroom1();
    }

    public void loadstaffdep1(string collegecode)
    {
        try
        {
            string srisql = "select distinct dept_name,dept_code from hrdept_master where college_code=" + collegecode + "";
            ds.Clear();
            ds = da.select_method_wo_parameter(srisql, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_staff_dept11.DataSource = ds;
                cbl_staff_dept11.DataTextField = "dept_name";
                cbl_staff_dept11.DataValueField = "dept_code";
                cbl_staff_dept11.DataBind();
                if (cbl_staff_dept11.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_staff_dept11.Items.Count; i++)
                    {
                        cbl_staff_dept11.Items[i].Selected = true;
                    }
                    txt_staff_dept11.Text = "Dept(" + cbl_staff_dept11.Items.Count + ")";
                    cb_staff_dept11.Checked = true;
                }
            }
        }
        catch
        {
        }
    }

    void bind_stafType1()
    {
        try
        {
            string srisql = "SELECT DISTINCT StfType FROM StaffTrans T,HrDept_Master D WHERE T.Dept_Code = D.Dept_Code AND T.Latestrec = 1 and d.college_code='" + ddlcollege.SelectedItem.Value + "'";
            ds.Clear();
            ds = da.select_method_wo_parameter(srisql, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_staff_type111.DataSource = ds;
                cbl_staff_type111.DataTextField = "StfType";
                cbl_staff_type111.DataValueField = "StfType";
                cbl_staff_type111.DataBind();
                if (cbl_staff_type111.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_staff_type111.Items.Count; i++)
                    {
                        cbl_staff_type111.Items[i].Selected = true;
                    }
                    txt_staff_type11.Text = "Staff Type(" + cbl_staff_type111.Items.Count + ")";
                    cb_staff_type111.Checked = true;
                }
            }
        }
        catch
        {
        }
    }

    public void bind_design1()
    {
        try
        {
            string sql = string.Empty;
            string itemheader = "";
            for (int i = 0; i < cbl_staff_type111.Items.Count; i++)
            {
                if (cbl_staff_type111.Items[i].Selected == true)
                {
                    if (itemheader == "")
                    {
                        itemheader = "" + cbl_staff_type111.Items[i].Value.ToString() + "";
                    }
                    else
                    {
                        itemheader = itemheader + "'" + "," + "" + "'" + cbl_staff_type111.Items[i].Value.ToString() + "";
                    }
                }
            }
            sql = "SELECT distinct Desig_Name FROM StaffTrans T,staffmaster m,Desig_Master G WHERE t.staff_code = m.staff_code and T.Desig_Code = G.Desig_Code AND Latestrec = 1 and G.collegecode='" + ddlcollege.SelectedItem.Value + "' and stftype in('" + itemheader + "')";
            ds.Clear();
            ds = da.select_method_wo_parameter(sql, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_staff_desn11.DataSource = ds;
                cbl_staff_desn11.DataTextField = "Desig_Name";
                cbl_staff_desn11.DataValueField = "Desig_Name";
                cbl_staff_desn11.DataBind();
                if (cbl_staff_desn11.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_staff_desn11.Items.Count; i++)
                    {
                        cbl_staff_desn11.Items[i].Selected = true;
                    }
                    txt_staff_desg111.Text = "Designation(" + cbl_staff_desn11.Items.Count + ")";
                    cb_staff_desn11.Checked = true;
                }
            }
        }
        catch
        {
        }
    }

    protected void loadfsstaff()
    {
        try
        {
            btnstaffadd.Visible = false;
            btnexitpop.Visible = false;
            ermsg.Visible = false;
            string sql = "";
            fsstaff.Sheets[0].RowCount = 0;
            fsstaff.SaveChanges();
            fsstaff.Visible = true;
            FarPoint.Web.Spread.CheckBoxCellType chkcell1 = new FarPoint.Web.Spread.CheckBoxCellType();
            FarPoint.Web.Spread.CheckBoxCellType chkcell = new FarPoint.Web.Spread.CheckBoxCellType();
            fsstaff.Sheets[0].RowCount = fsstaff.Sheets[0].RowCount + 1;
            fsstaff.Sheets[0].SpanModel.Add(fsstaff.Sheets[0].RowCount - 1, 0, 1, 3);
            string bindspread = sql;
            string itemheader = "";
            string designation = "";
            string dept = "";
            if (cbl_staff_dept11.Items.Count > 0)
                dept = Convert.ToString(getCblSelectedValue(cbl_staff_dept11));
            if (cbl_staff_type111.Items.Count > 0)
                itemheader = Convert.ToString(getCblSelectedValue(cbl_staff_type111));
            if (cbl_staff_desn11.Items.Count > 0)
                designation = Convert.ToString(getCblSelectedValue(cbl_staff_desn11));
            if (txt_search.Text != "" || txt_search1.Text != "")
            {
                if (ddlstaff.SelectedIndex == 0)
                {
                    sql = "SELECT distinct staffmaster.staff_code, staffmaster.staff_name FROM staffmaster INNER JOIN stafftrans ON staffmaster.staff_code = stafftrans.staff_code INNER JOIN hrdept_master ON stafftrans.dept_code = hrdept_master.dept_code WHERE (stafftrans.latestrec <> 0) AND (staffmaster.resign = 0) and (staffmaster.settled = 0) and (staffmaster.staff_code like '" + txt_search.Text.Split('-')[1].Trim() + "%') and (staffmaster.college_code =hrdept_master.college_code)";
                }
                else if (ddlstaff.SelectedIndex == 1)
                {
                    sql = "SELECT distinct staffmaster.staff_code, staffmaster.staff_name FROM staffmaster INNER JOIN stafftrans ON staffmaster.staff_code = stafftrans.staff_code INNER JOIN hrdept_master ON stafftrans.dept_code = hrdept_master.dept_code WHERE (stafftrans.latestrec <> 0) AND (staffmaster.resign = 0) and (staffmaster.settled = 0) and (staffmaster.staff_code like '" + txt_search1.Text.Trim() + "%') and (staffmaster.college_code =hrdept_master.college_code)";
                }
                else if (ddlcollege.SelectedIndex != -1)
                {
                    sql = "select distinct staffmaster.staff_code, staff_name  from stafftrans,staffmaster where stafftrans.staff_code=staffmaster.staff_code and latestrec<>0 and resign=0 and settled=0 and staffmaster.college_code='" + ddlcollege.SelectedValue + "'";
                }
                else
                {
                    sql = "select distinct staffmaster.staff_code, staff_name from stafftrans,staffmaster,hrdept_master.dept_name where stafftrans.staff_code=staffmaster.staff_code and latestrec<>0 and resign=0";
                }
            }
            else
            {
                if (dept != "")
                {
                    if (itemheader != "")
                    {
                        if (designation != "")
                        {
                            sql = "select distinct s.staff_code,s.staff_name,appl_id,h.dept_name from staffmaster s,hrdept_master h,desig_master d,stafftrans st,staff_appl_master sm where s.staff_code=st.staff_code and st.Dept_Code = h.Dept_Code and d.desig_code=st.desig_code and s.college_code =  h.college_code and s.college_code = d.collegecode  and s.appl_no=sm.appl_no and h.dept_code in('" + dept + "')  and d.desig_name in('" + designation + "') and s.college_code='" + ddlcollege.SelectedValue.ToString() + "'   and stftype in('" + itemheader + "') and resign = 0 and settled = 0 and latestrec=1";
                            //string strfilterVal = " and st.dept_code in('" + dept + "') and st.desig_code in('" + desicode + "') and st.category_code in('" + stafcatg + "') and st.stftype in('" + itemheader + "')";
                            //string selQ = " select distinct (sm.staff_code+'-'+sm.staff_name) as staff,sm.staff_code from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' and st.staff_code=sm.staff_code and sm.resign='0' and sm.settled='0' " + strfilterVal + " order by sm.staff_code asc";
                        }
                        else
                        {
                            fsstaff.Visible = false;
                            ermsg.Visible = true;
                            ermsg.Text = "Select Any Designation";
                        }
                    }
                    else
                    {
                        fsstaff.Visible = false;
                        ermsg.Visible = true;
                        ermsg.Text = "Select Any Staff Type";
                    }
                }
                else
                {
                    fsstaff.Visible = false;
                    ermsg.Visible = true;
                    ermsg.Text = "Select Any Department";
                }
            }
            ds.Clear();
            ds = da.select_method_wo_parameter(sql, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                int sno = 0;
                lbl_totalstaffcount.Visible = true;
                lbl_totalstaffcount.Text = "Total Staff Count: " + Convert.ToString(ds.Tables[0].Rows.Count);
                for (int rolcount = 0; rolcount < ds.Tables[0].Rows.Count; rolcount++)
                {
                    sno++;
                    string name = ds.Tables[0].Rows[rolcount]["staff_name"].ToString();
                    string code = ds.Tables[0].Rows[rolcount]["staff_code"].ToString();
                    fsstaff.Sheets[0].RowCount = fsstaff.Sheets[0].RowCount + 1;
                    fsstaff.Sheets[0].Rows[fsstaff.Sheets[0].RowCount - 1].Font.Bold = false;
                    fsstaff.Sheets[0].Cells[fsstaff.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(sno);
                    fsstaff.Sheets[0].Cells[fsstaff.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                    fsstaff.Sheets[0].Cells[fsstaff.Sheets[0].RowCount - 1, 1].Text = name;
                    fsstaff.Sheets[0].Cells[fsstaff.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                    fsstaff.Sheets[0].Cells[fsstaff.Sheets[0].RowCount - 1, 2].Text = code;
                    fsstaff.Sheets[0].Cells[fsstaff.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                    fsstaff.Sheets[0].AutoPostBack = false;
                }
                int rowcount = fsstaff.Sheets[0].RowCount;
                btnstaffadd.Visible = true;
                btnexitpop.Visible = true;
                fsstaff.Sheets[0].PageSize = 25 + (rowcount * 20);
                fsstaff.SaveChanges();
            }
            else
            {
                fsstaff.Visible = false;
                ermsg.Visible = true;
                ermsg.Text = "No Records Found";
                lbl_totalstaffcount.Visible = false;
            }
            txt_search.Text = "";
            txt_search1.Text = "";
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void btn_gostaff_Click(object sender, EventArgs e)
    {
        loadfsstaff();
    }

    public void loadreason()
    {
        ds.Tables.Clear();
        string sql = "select MasterCode,MasterValue from CO_MasterValues where MasterCriteria ='GRRea'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgatepass.DataSource = ds;
            ddlgatepass.DataTextField = "MasterValue";
            ddlgatepass.DataValueField = "MasterCode";
            ddlgatepass.DataBind();
            ddlgatepass.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlgatepass.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    public void loadreqby()
    {
        ds.Tables.Clear();
        string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='GRTyp'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlrequest.DataSource = ds;
            ddlrequest.DataTextField = "TextVal";
            ddlrequest.DataValueField = "TextCode";
            ddlrequest.DataBind();
            ddlrequest.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlrequest.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    public void loadreqmode()
    {
        ds.Tables.Clear();
        string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='GRMod'";
        ds = d2.select_method_wo_parameter(sql, "TEXT");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlrequestmode.DataSource = ds;
            ddlrequestmode.DataTextField = "TextVal";
            ddlrequestmode.DataValueField = "TextCode";
            ddlrequestmode.DataBind();
            ddlrequestmode.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlrequestmode.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    public void res()
    {
        try
        {
            ddlgatepass.Items.Insert(ddlgatepass.Items.Count, "Others");
        }
        catch
        {
        }
    }

    public void resrequest()
    {
        try
        {
            ddlrequest.Items.Insert(ddlrequest.Items.Count, "Others");
        }
        catch
        {
        }
    }

    public void req_mode()
    {
        try
        {
            ddlrequestmode.Items.Insert(ddlrequestmode.Items.Count, "Others");
        }
        catch
        {
        }
    }

    public void loadhour()
    {
        try
        {
            ddlhour.Items.Clear();
            ddlendhour.Items.Clear();
            ddl_hrs.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlhour.Items.Add(Convert.ToString(i));
                ddlendhour.Items.Add(Convert.ToString(i));
                ddl_hrs.Items.Add(Convert.ToString(i));
                ddlhour.SelectedIndex = ddlhour.Items.Count - 1;
                ddlendhour.SelectedIndex = ddlendhour.Items.Count - 1;
                ddl_hrs.SelectedIndex = ddlendhour.Items.Count - 1;
            }
        }
        catch
        {
        }
    }

    public void loadminits()
    {
        ddlmin.Items.Clear();
        ddlendmin.Items.Clear();
        ddl_mins.Items.Clear();
        for (int i = 0; i <= 59; i++)
        {
            string value = Convert.ToString(i);
            if (value.Length == 1)
            {
                value = "0" + "" + value;
            }
            ddlmin.Items.Add(Convert.ToString(value));
            ddlendmin.Items.Add(Convert.ToString(value));
            ddl_mins.Items.Add(Convert.ToString(value));
        }
    }

    public void timevalue1()
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        //string time =Convert.ToString(txt_viewtime.Text);
        string[] ay = time.Split(':');
        string val_hr = ay[0].ToString();
        int hr = Convert.ToInt16(val_hr);
        if (val_hr == "01")
        {
            hrr = "1";
        }
        else if (val_hr == "02")
        {
            hrr = "2";
        }
        else if (val_hr == "03")
        {
            hrr = "3";
        }
        else if (val_hr == "04")
        {
            hrr = "4";
        }
        else if (val_hr == "05")
        {
            hrr = "5";
        }
        else if (val_hr == "06")
        {
            hrr = "6";
        }
        else if (val_hr == "07")
        {
            hrr = "7";
        }
        else if (val_hr == "08")
        {
            hrr = "8";
        }
        else if (val_hr == "09")
        {
            hrr = "9";
        }
        else if (val_hr == "13")
        {
            hrr = "1";
        }
        else if (val_hr == "14")
        {
            hrr = "2";
        }
        else if (val_hr == "15")
        {
            hrr = "3";
        }
        else if (val_hr == "16")
        {
            hrr = "4";
        }
        else if (val_hr == "17")
        {
            hrr = "5";
        }
        else if (val_hr == "18")
        {
            hrr = "6";
        }
        else if (val_hr == "19")
        {
            hrr = "7";
        }
        else if (val_hr == "20")
        {
            hrr = "8";
        }
        else if (val_hr == "21")
        {
            hrr = "9";
        }
        else if (val_hr == "22")
        {
            hrr = "10";
        }
        else if (val_hr == "23")
        {
            hrr = "11";
        }
        else if (val_hr == "24")
        {
            hrr = "12";
        }
    }

    public void timevalue()
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        //string time =Convert.ToString(txt_viewtime.Text);
        string[] ay = time.Split(':');
        string val_hr = ay[0].ToString();
        int hr = Convert.ToInt16(val_hr);
        if (val_hr == "01")
        {
            hrr = "1";
        }
        else if (val_hr == "02")
        {
            hrr = "2";
        }
        else if (val_hr == "03")
        {
            hrr = "3";
        }
        else if (val_hr == "04")
        {
            hrr = "4";
        }
        else if (val_hr == "05")
        {
            hrr = "5";
        }
        else if (val_hr == "06")
        {
            hrr = "6";
        }
        else if (val_hr == "07")
        {
            hrr = "7";
        }
        else if (val_hr == "08")
        {
            hrr = "8";
        }
        else if (val_hr == "09")
        {
            hrr = "9";
        }
        else if (val_hr == "13")
        {
            hrr = "1";
        }
        else if (val_hr == "14")
        {
            hrr = "2";
        }
        else if (val_hr == "15")
        {
            hrr = "3";
        }
        else if (val_hr == "16")
        {
            hrr = "4";
        }
        else if (val_hr == "17")
        {
            hrr = "5";
        }
        else if (val_hr == "18")
        {
            hrr = "6";
        }
        else if (val_hr == "19")
        {
            hrr = "7";
        }
        else if (val_hr == "20")
        {
            hrr = "8";
        }
        else if (val_hr == "21")
        {
            hrr = "9";
        }
        else if (val_hr == "22")
        {
            hrr = "10";
        }
        else if (val_hr == "23")
        {
            hrr = "11";
        }
        else if (val_hr == "24")
        {
            hrr = "12";
        }
        if (val_hr == "10" || val_hr == "11" || val_hr == "12")
        {
            hrr = val_hr;
            ddlhour.Text = val_hr;
            ddlmin.Text = ay[1].ToString();
            ddlendhour.Text = val_hr;
            ddlendmin.Text = ay[1].ToString();
            ddl_hrs.Text = val_hr;
            ddl_mins.Text = ay[1].ToString();
        }
        else
        {
            ddlhour.Text = hrr;
            ddlmin.Text = ay[1].ToString();
            ddlendhour.Text = hrr;
            ddlendmin.Text = ay[1].ToString();
            ddl_hrs.Text = hrr;
            ddl_mins.Text = ay[1].ToString();
        }
        if (val_hr == "12" || val_hr == "13" || val_hr == "14" || val_hr == "15" || val_hr == "16" || val_hr == "17" || val_hr == "18" || val_hr == "19" || val_hr == "20" || val_hr == "21" || val_hr == "22" || val_hr == "23" || val_hr == "24")
        {
            ddlsession.Text = "PM";
            ddlenssession.Text = "PM";
            ddl_ampm.Text = "PM";
        }
        else
        {
            ddlsession.Text = "AM";
            ddlenssession.Text = "AM";
            ddl_ampm.Text = "AM";
        }
    }

    public void Btn_Staff_Code_Click(object sender, EventArgs e)
    {
        try
        {
            Div1.Visible = true;
            fsstaff.Visible = true;
            panelrollnopop.Visible = false;
            fsstaff.Sheets[0].RowCount = 0;
            BindCollege();
            collegecode = Convert.ToString(ddlcollege.SelectedItem.Value);
            loadstaffdep1(collegecode);
            bind_stafType1();
            bind_design1();
            loadfsstaff();
        }
        catch
        {
        }
    }

    public void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getstaffcode(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string query = " select distinct sm.staff_code  from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' and st.staff_code=sm.staff_code and sm.resign='0' and sm.settled='0' and sm.staff_code like '" + prefixText + "%' and sm.college_code='" + clgcode + "' order by sm.staff_code asc";
        //  string query = "select distinct s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and s.college_code='" + mulicollg + "' and resign =0 and s.staff_code like '" + prefixText + "%'";
        // string query = "select staff_name  from staffmaster where resign =0 and settled =0 and staff_name like  '" + prefixText + "%'";
        name = ws.Getname(query);
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getstaffname(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        // query = "select distinct s.staff_name+'-'+dm.desig_name+'-'+hr.dept_name+'-'+ s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and resign =0 and s.staff_name like '" + prefixText + "%'";
        string query = "select distinct s.staff_name+'-'+dm.desig_name+'-'+hr.dept_name+'-'+ s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm,stafftrans st where s.appl_no=sa.appl_no and st.dept_code=hr.dept_code and st.staff_code=s.staff_code and dm.desig_code=st.desig_code and settled=0 and resign =0 and st.latestrec=1 and s.staff_name like '%" + prefixText + "%' and isnull(st.staff_code,'')<>''";
        // string query = "select staff_name  from staffmaster where resign =0 and settled =0 and staff_name like  '" + prefixText + "%'";
        name = ws.Getname(query);
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getstaffname1(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string query = " select distinct (sm.staff_name+'-'+ sm.staff_code) as staff,sm.staff_name from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' and st.staff_code=sm.staff_code and sm.resign='0' and sm.settled='0' and sm.staff_name like '" + prefixText + "%' and sm.college_code='" + clgcode + "' order by sm.staff_name asc";
        // string query = "select distinct (s.staff_name+'-'+s.staff_code)as staff from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and resign =0 and s.staff_name like '" + prefixText + "%'";
        // string query = "select staff_name  from staffmaster where resign =0 and settled =0 and staff_name like  '" + prefixText + "%'";
        name = ws.Getname(query);
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getdes(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string sql = "SELECT distinct Desig_Name FROM StaffTrans T,staffmaster m,Desig_Master G WHERE t.staff_code = m.staff_code and T.Desig_Code = G.Desig_Code AND Latestrec = 1 ";
        // string query = "select staff_name  from staffmaster where resign =0 and settled =0 and staff_name like  '" + prefixText + "%'";
        name = ws.Getname(sql);
        return name;
    }

    public void BindLeave()
    {
        try
        {
            string leave = "";
            string leaveadd = "";
            addleave = "";
            ddl_leave_type.Items.Clear();
            string leaveeee = "";
            string query = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + Session["collegecode"] + "'";
            ds1.Clear();
            ds1 = d2.select_method_wo_parameter(query, "Text");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string[] spl_type = ds1.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                for (int k = 0; k < ds1.Tables[0].Rows.Count; k++)
                {
                    int col = 0;
                    for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                    {
                        bindleavecount = Convert.ToInt32(spl_type.GetUpperBound(0));
                        if (spl_type[i].Trim() != "")
                        {
                            string[] split_leave = spl_type[i].Split(';');
                            leave = split_leave[0];
                            if (addleave == "")
                            {
                                addleave = leave;
                            }
                            else
                            {
                                addleave = addleave + "','" + leave;
                            }
                            //if (headerleavetype.Count > 0)
                            //{
                            //    string lvee = d2.GetFunction("select shortname from leave_category where college_code='" + ddlcollege.SelectedItem.Value + "' and category in('" + leave + "')");
                            //    string value = Convert.ToString(headerleavetype[lvee]);
                            //    string[] array = value.Split(',');
                            //    for (int j = 0; j < array.Length; j++)
                            //    {
                            //        string[] spl = array[j].Split('/');
                            //        string l = spl[0];
                            //        if (value != "")
                            //        {
                            //            if (leaveeee == "")
                            //            {
                            //                leaveeee = l;
                            //            }
                            //            else
                            //            {
                            //                leaveeee = leaveeee + "','" + l;
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            string strleave = "";
            if (txt_staff_code.Text != "")
            {
                strleave = "select * from leave_category where category in('" + addleave + "')  and college_code='" + Session["collegecode"] + "'";
                //and shortname not in('" + leaveeee + "')
            }
            else
            {
                strleave = "select * from leave_category WHERE college_code='" + Session["collegecode"] + "'";
            }
            ds2 = d2.select_method(strleave, hat, "Text");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddl_leave_type.DataSource = ds2;
                ddl_leave_type.DataTextField = "category";
                ddl_leave_type.DataValueField = "shortname";
                ddl_leave_type.DataBind();
                ddl_leave_type.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
        }
    }

    //public void ddl_leave_type_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}

    public void bindgrid2()
    {
        try
        {
            double llcount = 0;
            totalleave.Clear();
            double addtot = 0;
            string actual = "";
            string currentYear = DateTime.Now.Year.ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add("Month");
            DataRow dr = null;
            int tott = 0;
            string applid = d2.GetFunction("select appl_id from staff_appl_master a,staffmaster s where a.appl_no=s.appl_no and staff_code='" + txt_staff_code.Text + "'");
            string queryObject = "select * from hrpaymonths where college_code='" + Session["collegecode"] + "' and SelStatus='1'";
            ds = d2.select_method_wo_parameter(queryObject, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string query = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + Session["collegecode"] + "'";
                ds2.Clear();
                ds2 = d2.select_method_wo_parameter(query, "Text");
                if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    string[] spl_type = ds2.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                    int col_cnt = 0;
                    for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                    {
                        col_cnt++;
                        string[] split_leave = spl_type[i].Split(';');
                        string shortname = d2.GetFunction("select shortname from leave_category where category='" + split_leave[0] + "'");
                        if (Convert.ToString(shortname).Trim() != "0")
                            dt.Columns.Add(Convert.ToString(shortname));
                    }
                    int leavetypecount = spl_type.GetUpperBound(0);
                    if (leavetypecount == 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Set The leave Detail For This Staff";
                        return;
                    }
                    for (int k = 0; k <= ds.Tables[0].Rows.Count; k++)
                    {
                        dr = dt.NewRow();
                        if (k != ds.Tables[0].Rows.Count)
                            dr[0] = Convert.ToString(ds.Tables[0].Rows[k]["Paymonth"]);
                        else
                            dr[0] = Convert.ToString("Total");
                        dt.Rows.Add(dr);
                    }
                    double tot_leave = 0;
                    string leavefromdate = "";
                    string leavetodate = "";
                    string ishalfdate = "";
                    string halfdaydate = "";
                    int finaldate = 0;
                    string sleave = "";
                    string requestpk = string.Empty;
                    for (int k = 0; k <= ds.Tables[0].Rows.Count; k++)
                    {
                        int col = 0;
                        for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                        {
                            if (spl_type[i].Trim() != "")
                            {
                                col++;
                                tot_leave = 0;
                                string[] split_leave = spl_type[i].Split(';');
                                string leave = split_leave[0];
                                if (split_leave.Length >= 2)
                                {
                                    string s = Convert.ToString(split_leave[1]);
                                    if (s == "" || s.Trim() == "-")
                                        addtot = 0;
                                    else
                                        addtot = Convert.ToDouble(s);
                                }
                                if (k != ds.Tables[0].Rows.Count)
                                {
                                    string leavepk = d2.GetFunction("select LeaveMasterPK from leave_category where category='" + leave + "' and college_code='" + Session["collegecode"] + "'");
                                    //string dt_get_leave = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + ds.Tables[0].Rows[k]["From_Date"].ToString() + "' and LeaveTo<='" + ds.Tables[0].Rows[k]["To_Date"].ToString() + "' and ReqAppNo='" + applid + "' and ReqAppStatus='1' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' ";//delsi1611 added between
                                    string dt_get_leave = "select * from RQ_Requisition r,leave_category l where RequestType=5 and (LeaveFrom>='" + ds.Tables[0].Rows[k]["From_Date"].ToString() + "' and LeaveTo<='" + ds.Tables[0].Rows[k]["To_Date"].ToString() + "' or LeaveFrom between'" + ds.Tables[0].Rows[k]["From_Date"].ToString() + "' and '" + ds.Tables[0].Rows[k]["To_Date"].ToString() + "') and ReqAppNo='" + applid + "' and ReqAppStatus='1' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "'";
                                    ds1 = d2.select_method_wo_parameter(dt_get_leave, "Text");
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        for (int g = 0; g < ds1.Tables[0].Rows.Count; g++)
                                        {
                                            leavefromdate = Convert.ToString(ds1.Tables[0].Rows[g]["LeaveFrom"]);
                                            leavetodate = Convert.ToString(ds1.Tables[0].Rows[g]["LeaveTo"]);
                                            ishalfdate = Convert.ToString(ds1.Tables[0].Rows[g]["IsHalfDay"]);
                                            requestpk = Convert.ToString(ds1.Tables[0].Rows[g]["RequisitionPK"]);
                                            if (leavefromdate != "" && leavetodate != "")
                                            {
                                                string dtT = leavefromdate;
                                                string[] Split = dtT.Split('/');
                                                string enddt = leavetodate;
                                                Split = enddt.Split('/');
                                                DateTime fromdate = Convert.ToDateTime(dtT);
                                                DateTime todate = Convert.ToDateTime(enddt);
                                                TimeSpan days = todate - fromdate;
                                                string ndate = Convert.ToString(days);
                                                Split = ndate.Split('.');
                                                string getdate = Split[0];
                                                llcount = 0;
                                                if (fromdate != todate)
                                                {
                                                    for (; fromdate <= todate; )
                                                    {
                                                        string dayy = fromdate.ToString("dddd");
                                                        leavedayscheckcount = false;
                                                        if (dayy == "Sunday")
                                                        {
                                                            string qur1 = "select * from individual_leave_type where  staff_code='" + txt_staff_code.Text + "' and college_code=" + Session["collegecode"] + "";
                                                            ds2 = d2.select_method_wo_parameter(qur1, "Text");
                                                            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                                                            {
                                                                string[] spl_type1 = ds2.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                                                                for (int f = 0; spl_type.GetUpperBound(0) >= f; f++)
                                                                {
                                                                    string[] split_leave1 = spl_type1[f].Split(';');
                                                                    if (split_leave[3] == "0")
                                                                        leavedayscheckcount = true;
                                                                    else
                                                                        leavedayscheckcount = false;
                                                                }
                                                            }
                                                        }
                                                        //finaldate = Convert.ToInt32(getdate);
                                                        if (leavedayscheckcount == false)//saranyaref
                                                        {

                                                            // finaldate = Convert.ToInt32(getdate) + 1;
                                                            llcount++;

                                                            string checkval = "select * from staff_leave_dates where requestfk='" + requestpk + "' and LeaveDate='" + fromdate + "'";
                                                            DataSet checkdel = new DataSet();
                                                            checkdel = d2.select_method_wo_parameter(checkval, "text");
                                                            if (checkdel.Tables[0].Rows.Count > 0)
                                                            {
                                                                ishalfdate = "False";
                                                                string approvalcheck = Convert.ToString(checkdel.Tables[0].Rows[0]["isApproved"]);
                                                                string session = Convert.ToString(checkdel.Tables[0].Rows[0]["SessionType"]);
                                                                string delecheck = Convert.ToString(checkdel.Tables[0].Rows[0]["checkdel"]);
                                                                if (approvalcheck == "1")
                                                                {
                                                                    if (delecheck == "1")
                                                                    {
                                                                        if (session == "0")
                                                                        {
                                                                            llcount--;

                                                                        }
                                                                        else if (session == "1" || session == "2")
                                                                        {

                                                                            llcount = llcount - 0.5;


                                                                        }


                                                                    }

                                                                }


                                                            }
                                                        }
                                                        fromdate = fromdate.AddDays(1);
                                                    }
                                                }
                                                else
                                                {
                                                    llcount++;
                                                }
                                                if (ishalfdate == "True")
                                                {
                                                    halfdaydate = Convert.ToString(ds1.Tables[0].Rows[g]["HalfDate"]);
                                                    if (tot_leave == 0)
                                                    {
                                                        // tot_leave = Convert.ToDouble(finaldate + 1);
                                                        tot_leave = llcount;
                                                        tot_leave = tot_leave - 0.5;
                                                    }
                                                    else
                                                    {
                                                        //tot_leave = tot_leave + Convert.ToDouble(finaldate + 1);
                                                        tot_leave = tot_leave + llcount;
                                                        tot_leave = tot_leave - 0.5;
                                                    }
                                                    sleave = leave + "-" + tot_leave;
                                                }
                                                else
                                                {
                                                    if (tot_leave == 0)
                                                    {
                                                        // tot_leave = tot_leave + Convert.ToDouble(finaldate + 1);
                                                        tot_leave = tot_leave + llcount;
                                                    }
                                                    else
                                                    {
                                                        //tot_leave = tot_leave + Convert.ToDouble(finaldate + 1);
                                                        tot_leave = tot_leave + llcount;
                                                    }
                                                    sleave = leave + "-" + tot_leave;
                                                }
                                            }
                                        }
                                    }
                                    if (spl_type[i].Contains(";"))
                                    {
                                        string sp = split_leave[0].ToString();
                                        actual = split_leave[2].ToString();
                                        if (actual == "")
                                        {
                                            actual = "0";
                                        }
                                        string[] iii = sleave.Split('-');
                                        string sp1 = iii[0];
                                        if (sp != sp1)
                                        {
                                            tot_leave = 0;
                                        }
                                        //-------------------------------------------------------------------------------//

                                        //.................................................................................
                                        string tt = Convert.ToString(tot_leave + "/" + actual);
                                        if (!totalleave.Contains(Convert.ToString(leave)))
                                            totalleave.Add(Convert.ToString(leave), Convert.ToString(tt));
                                        else
                                        {
                                            string getvalue = Convert.ToString(totalleave[Convert.ToString(leave)]);
                                            if (getvalue.Trim() != "")
                                            {
                                                getvalue = getvalue + "," + tt;
                                                totalleave.Remove(Convert.ToString(leave));
                                                if (getvalue.Trim() != "")
                                                    totalleave.Add(Convert.ToString(leave), Convert.ToString(getvalue));
                                            }
                                        }
                                        //..........................................................................................................................
                                        dt.Rows[k][col] = Convert.ToString(tot_leave + "/" + actual);
                                    }
                                }
                                else
                                {
                                    double addlv = 0;
                                    double totvalue = 0;
                                    if (totalleave.Count > 0)
                                    {
                                        string value = Convert.ToString(totalleave[leave]);
                                        string[] array = value.Split(',');
                                        for (int j = 0; j < array.Length; j++)
                                        {
                                            string[] spl = array[j].Split('/');
                                            string lv = spl[0];
                                            string tot = spl[1];
                                            if (addlv == 0)
                                                addlv = Convert.ToDouble(lv);
                                            else
                                                addlv = addlv + Convert.ToDouble(lv);
                                        }
                                    }
                                    dt.Rows[k][col] = addlv + "/" + addtot;
                                }
                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        gridView2.DataSource = dt;
                        gridView2.DataBind();
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set The Leave Type For This Staff";
                    return;
                }
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Please Set HR Year";
                return;
            }
            //BindLeave();
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    protected void GridView2_databound(object sender, EventArgs e)
    {
        try
        {
            int ccouunt = 0;
            int ccouunt1 = 0;
            int sss = gridView2.Rows.Count - 1;
            headerleavetype.Clear();
            for (int i = gridView2.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow row1 = gridView2.Rows[i];
                for (int j = 0; j <= 5; j++)
                {
                    string lnlname = gridView2.Rows[i].Cells[1].Text;
                    if (lnlname == "Total")
                    {
                        row1.Cells[1].ColumnSpan = 2;
                        row1.Cells[0].Visible = false;
                    }
                }
                int ccc = 0;
                ccc = bindleavecount;
                //+2;
                for (int j = 1; j < ccc + 2; j++)
                {
                    string lnlname = gridView2.Rows[i].Cells[j].Text;
                    string[] spl = lnlname.Split('/');
                    if (spl.Length == 2)
                    {
                        string f = Convert.ToString(spl[0]);
                        string l = Convert.ToString(spl[1]);
                        // int div = Convert.ToInt32(l) / 2;

                        double div = Convert.ToDouble(l) / 2;//delsi 03/05/2018
                        //if (l != "0")
                        //{
                        if (f != "0")
                        {
                            if (f != l)
                            {
                                partl_check = 1;
                                gridView2.Rows[i].Cells[j].BackColor = ColorTranslator.FromHtml("#A4F9C9");
                                string con = Convert.ToString(i) + "-" + Convert.ToString(j);
                                if (rowgrid == "")
                                {
                                    rowgrid = con;
                                }
                                else
                                {
                                    rowgrid = rowgrid + "," + con;
                                }
                            }
                        }
                        if (f == l)
                        {
                            if (l != "0")
                            {
                                gridView2.Rows[i].Cells[j].BackColor = Color.Tomato;
                                full_check = 1;
                                string con = Convert.ToString(i) + "-" + Convert.ToString(j);
                                if (colgrind == "")
                                {
                                    colgrind = con;
                                }
                                else
                                {
                                    colgrind = colgrind + "," + con;
                                }
                                string header = gridView2.HeaderRow.Cells[j].Text;
                                if (!headerleavetype.Contains(Convert.ToString(header)))
                                {
                                    headerleavetype.Add(Convert.ToString(header), Convert.ToString(header));
                                }
                                else
                                {
                                    string getvalue = Convert.ToString(headerleavetype[Convert.ToString(header)]);
                                    if (getvalue.Trim() != "")
                                    {
                                        getvalue = getvalue + "," + header;
                                        headerleavetype.Remove(Convert.ToString(header));
                                        if (getvalue.Trim() != "")
                                        {
                                            headerleavetype.Add(Convert.ToString(header), Convert.ToString(getvalue));
                                        }
                                    }
                                }
                            }
                        }
                        if (l == "0")
                        {
                            string chklnlname = gridView2.Rows[13].Cells[j].Text;
                            if (chklnlname != "Total")
                            {
                                string[] spl1 = chklnlname.Split('/');
                                if (spl1.Length == 2)
                                {
                                    string f1 = spl1[0];
                                    string l1 = spl1[1];
                                    if (l1 == f1)
                                    {
                                        gridView2.Rows[13].Cells[j].BackColor = Color.Tomato;//refdelsi
                                        // gridView2.Rows[i].Cells[j].BackColor = Color.Tomato;
                                        string header = gridView2.HeaderRow.Cells[j].Text;
                                        if (!headerleavetype.Contains(Convert.ToString(header)))
                                        {
                                            headerleavetype.Add(Convert.ToString(header), Convert.ToString(header));
                                        }
                                        else
                                        {
                                            string getvalue = Convert.ToString(headerleavetype[Convert.ToString(header)]);
                                            if (getvalue.Trim() != "")
                                            {
                                                getvalue = getvalue + "," + header;
                                                headerleavetype.Remove(Convert.ToString(header));
                                                if (getvalue.Trim() != "")
                                                {
                                                    headerleavetype.Add(Convert.ToString(header), Convert.ToString(getvalue));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        gridView2.Rows[sss].Cells[0].Font.Bold = true;
                        gridView2.Rows[sss].Cells[1].Font.Bold = true;
                        gridView2.Rows[sss].Cells[j].Font.Bold = true;
                        ccouunt = ccc;
                        ccouunt1 = ccc + 1;
                    }
                }
            }
            gridView2.Rows[sss].Cells[ccouunt].Font.Bold = true;
            gridView2.Rows[sss].Cells[ccouunt1].Font.Bold = true;
            // BindLeave();
        }
        catch
        {
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int t = 2;
        int c = 0;
        leavetypecountt();
        c = Convert.ToInt32(leavetypecount);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            //e.Row.Cells[2].Width = 50;
            if (c == 1)
            {
                e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "2$" + e.Row.RowIndex);
            }
            else if (c == 2)
            {
                e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "2$" + e.Row.RowIndex);
                e.Row.Cells[3].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "3$" + e.Row.RowIndex);
            }
            else if (c == 3)
            {
                e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "2$" + e.Row.RowIndex);
                e.Row.Cells[3].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "3$" + e.Row.RowIndex);
                e.Row.Cells[4].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "4$" + e.Row.RowIndex);
            }
            else
            {
                e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "2$" + e.Row.RowIndex);
                if (c + 2 > t)
                {
                    e.Row.Cells[3].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "3$" + e.Row.RowIndex);
                }
                t = 3 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[4].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "4$" + e.Row.RowIndex);
                }
                t = 4 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "5$" + e.Row.RowIndex);
                }
                t = 5 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[6].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "6$" + e.Row.RowIndex);
                }
                t = 6 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[7].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "7$" + e.Row.RowIndex);
                }
                t = 7 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[8].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "8$" + e.Row.RowIndex);
                }
                t = 8 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[9].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "9$" + e.Row.RowIndex);
                }
                t = 9 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[10].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "10$" + e.Row.RowIndex);
                }
                t = 10 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[11].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "11$" + e.Row.RowIndex);
                }
                t = 11 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[12].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "12$" + e.Row.RowIndex);
                }
                t = 12 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[13].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "13$" + e.Row.RowIndex);
                }
                t = 13 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[15].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "14$" + e.Row.RowIndex);
                }
                t = 14 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[16].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "15$" + e.Row.RowIndex);
                }
                t = 15 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[17].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "16$" + e.Row.RowIndex);
                }
                t = 16 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[18].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "17$" + e.Row.RowIndex);
                }
                t = 17 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[19].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "18$" + e.Row.RowIndex);
                }
                t = 18 + 1;
                if (c + 2 > t)
                {
                    e.Row.Cells[20].Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gridView2, "19$" + e.Row.RowIndex);
                }
            }
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        foreach (GridViewRow row in gridView2.Rows)
        {
            row.ToolTip = "Click to select Leave.";
        }
        gridView2.Rows[rowval].Cells[colval].BackColor = ColorTranslator.FromHtml("#F0F0F0");
        leavetypecountt();
        string leaveheadername = "";
        string leavemonthh = "";
        string leavetext = "";
        int x = 0;
        int jj = Convert.ToInt32(e.CommandArgument);
        int cc = Convert.ToInt32(leavetypecount);
        cc = cc + 2;
        for (x = 2; x < cc; x++)
        {
            if (e.CommandName == Convert.ToString(x))
            {
                leavetext = gridView2.Rows[jj].Cells[x].Text;
                string[] t = leavetext.Split('/');
                string tot = t[0];
                leavemonthh = gridView2.Rows[jj].Cells[1].Text;
                rowval = jj;
                colval = x;
                leaveheadername = Convert.ToString(gridView2.HeaderRow.Cells[x].Text);
                gridView2.Rows[jj].Cells[x].BackColor = Color.OrangeRed;
                string[] spl = leavetext.Split('/');
                string f = spl[0];
                if (f != "0")
                {
                    divleavedis.Visible = true;
                    leavedetailgrid(leavemonthh, leaveheadername, tot);
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "You Are Doesn't Take A Leave On This Category Of " + leaveheadername;
                }
            }
        }
        int r = 0;
        int c = 0;
        if (partl_check == 1)
        {
            string[] sp = rowgrid.Split(',');
            for (int f = 0; f < sp.Length; f++)
            {
                string[] sp1 = sp[f].Split('-');
                r = Convert.ToInt32(sp1[0]);
                c = Convert.ToInt32(sp1[1]);
                gridView2.Rows[r].Cells[c].BackColor = ColorTranslator.FromHtml("#A4F9C9");
            }
        }
        if (full_check == 1)
        {
            string[] spsecond = colgrind.Split(',');
            for (int f = 0; f < spsecond.Length; f++)
            {
                string[] sp11 = spsecond[f].Split('-');
                r = Convert.ToInt32(sp11[0]);
                c = Convert.ToInt32(sp11[1]);
                gridView2.Rows[r].Cells[c].BackColor = Color.Tomato;
            }
        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gridView2.Rows)
        {
            if (row.RowIndex == gridView2.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";
            }
        }
    }

    public void leavetypecountt()
    {
        string queryObject = "select * from hrpaymonths where college_code='" + Session["collegecode"] + "'";
        ds = d2.select_method_wo_parameter(queryObject, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string query = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + Session["collegecode"] + "'";
            ds2.Clear();
            ds2 = d2.select_method_wo_parameter(query, "Text");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                string[] spl_type = ds2.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                int col_cnt = 0;
                leavetypecount = Convert.ToString(spl_type.GetUpperBound(0));
                for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                {
                    col_cnt++;
                    string[] split_leave = spl_type[i].Split(';');
                }
            }
        }
    }

    public void leavedetailgrid(string leavemonth, string leavetypename, string tot)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Dummy1");
        dt.Columns.Add("Dummy2");
        dt.Columns.Add("Dummy3");
        dt.Columns.Add("Dummy4");
        dt.Columns.Add("Dummy5");
        dt.Columns.Add("Dummy6");
        dt.Columns.Add("Dummy7");
        dt.Columns.Add("Dummy8");
        int y = 0;
        string leavepkval = d2.GetFunction("select LeaveMasterPK from leave_category where shortname='" + leavetypename + "' and college_code='" + ddlcollege.SelectedItem.Value + "'");
        string fromdate = "";
        string todate = "";
        string fromdatenew = "";
        string todatenew = "";
        string reason = "";
        string leavechage = "";
        string approvalstaff = "";
        string qur = "";
        string q = "";
        int llcount = 0;
        string appno = d2.GetFunction("select sm.appl_id from staff_appl_master sm ,staffmaster m where sm.appl_no=m.appl_no and m.staff_code='" + txt_staff_code.Text + "'");
        if (leavemonth != "Total")
        {
            qur = "select From_Date,to_date from hrpaymonths WHERE PayMonth='" + leavemonth + "' and College_Code='" + ddlcollege.SelectedItem.Value + "'";
            ds = d2.select_method_wo_parameter(qur, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromdate = Convert.ToString(ds.Tables[0].Rows[0]["From_Date"]);
                todate = Convert.ToString(ds.Tables[0].Rows[0]["to_date"]);
            }
            q = "select RequisitionPK,IsHalfDay, CASE WHEN IsHalfDay = 1 THEN 'Half Day' ELSE 'Full Day' END Leave,case when LeaveSession=2 then 'Morning' when LeaveSession=1 then 'Evening' else '-' end LeaveSession, LeaveFrom, LeaveTo,CONVERT(VARCHAR(11),HalfDate,103) as HalfDate,GateReqReason,leaveChangeReason,ReqAppStaffAppNo from RQ_Requisition where RequestType=5 and LeaveFrom>='" + fromdate + "' and LeaveTo<='" + todate + "' and ReqAppNo='" + appno + "' and LeaveMasterFK='" + leavepkval + "' and ReqAppStatus not in('2','0')";
            ds.Clear();
            ds = d2.select_method_wo_parameter(q, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (y = 0; y < ds.Tables[0].Rows.Count; y++)
                {
                    fromdatenew = Convert.ToString(ds.Tables[0].Rows[y]["LeaveFrom"]);
                    todatenew = Convert.ToString(ds.Tables[0].Rows[y]["LeaveTo"]);
                    string ishalfdate = Convert.ToString(ds.Tables[0].Rows[y]["IsHalfDay"]);
                    reason = d2.GetFunction("select MasterValue from CO_MasterValues where MasterCriteria='HGRea' and MasterCode='" + Convert.ToString(ds.Tables[0].Rows[y]["GateReqReason"]) + "'");
                    if (reason == "0")
                    {
                        reason = "";
                    }
                    approvalstaff = d2.GetFunction("select sm.appl_name from staff_appl_master sm ,staffmaster m where sm.appl_no=m.appl_no and sm.appl_id='" + Convert.ToString(ds.Tables[0].Rows[y]["ReqAppStaffAppNo"]) + "'");
                    if (approvalstaff == "0")
                    {
                        approvalstaff = "Direct Approval-Self";
                    }
                    string hlf = Convert.ToString(ds.Tables[0].Rows[y]["HalfDate"]);
                    if (hlf == "" || hlf == "0")
                    {
                        hlf = "";
                    }
                    string addleavechangereason = "";
                    string leavechagequery = d2.GetFunction("select ApproveReason from RQ_ReqApprovalDetails where ReqFK='" + Convert.ToString(ds.Tables[0].Rows[y]["RequisitionPK"]) + "' and ReqApproveStaffappNo='" + Convert.ToString(ds.Tables[0].Rows[y]["ReqAppStaffAppNo"]) + "' order by ReqApproveStaffStage,ApproveStaffPriority");
                    addleavechangereason = leavechagequery;
                    int finaldate = 0;
                    double t = 0;
                    string dtT = fromdatenew;
                    string[] Split = dtT.Split('/');
                    string enddt = todatenew;
                    Split = enddt.Split('/');
                    DateTime fromdatea = Convert.ToDateTime(dtT);
                    DateTime fromdatea1 = Convert.ToDateTime(dtT);
                    DateTime todatea = Convert.ToDateTime(enddt);
                    TimeSpan days = todatea - fromdatea;
                    string ndate = Convert.ToString(days);
                    Split = ndate.Split('.');
                    string getdate = Split[0];
                    if (fromdatea != todatea)
                    {
                        for (; fromdatea <= todatea; )
                        {
                            string dayy = fromdatea.ToString("dddd");
                            leavedayscheckcount = false;
                            if (dayy == "Sunday")
                            {
                                string qur1 = "select * from individual_leave_type where  staff_code='" + txt_staff_code.Text + "' and college_code=" + ddlcollege.SelectedItem.Value + "";
                                ds2 = d2.select_method_wo_parameter(qur1, "Text");
                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    string[] spl_type = ds2.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                                    for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                                    {
                                        string[] split_leave = spl_type[i].Split(';');
                                        string ll = d2.GetFunction("select category from leave_category where shortname='" + leavetypename + "' and college_code='" + ddlcollege.SelectedItem.Value + "'");
                                        if (split_leave[0].ToString() == ll)
                                        {
                                            if (split_leave[3] == "0")
                                            {
                                                leavedayscheckcount = true;
                                            }
                                            else
                                            {
                                                leavedayscheckcount = false;
                                            }
                                        }
                                    }
                                }
                            }
                            if (leavedayscheckcount == false)
                            {
                                // finaldate = Convert.ToInt32(getdate) + 1;
                                llcount++;
                            }
                            fromdatea = fromdatea.AddDays(1);
                            t = llcount;
                        }
                    }
                    else
                    {
                        finaldate = 1;
                        t = finaldate;
                    }
                    if (ishalfdate == "True")
                    {
                        t = Convert.ToDouble(finaldate) - 0.5;
                    }
                    dr = dt.NewRow();
                    dr[0] = fromdatea1.ToString("dd/MM/yyyy");
                    dr[1] = todatea.ToString("dd/MM/yyyy");
                    dr[2] = reason;
                    dr[3] = approvalstaff;
                    dr[4] = addleavechangereason;
                    dr[5] = t;
                    dr[6] = Convert.ToString(ds.Tables[0].Rows[y]["LeaveSession"]);
                    dr[7] = hlf;
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    gridView3.DataSource = dt;
                    gridView3.DataBind();
                }
                lbl_leavedis.Text = d2.GetFunction("select category from leave_category where shortname='" + leavetypename + "' and college_code='" + collegecode1 + "'") + "(" + leavetypename + ")";
            }
        }
        else
        {
            /// total- all month
            qur = "select From_Date,to_date from hrpaymonths WHERE College_Code='" + collegecode1 + "'";
            ds1 = d2.select_method_wo_parameter(qur, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int u = 0; u < ds1.Tables[0].Rows.Count; u++)
                {
                    fromdate = Convert.ToString(ds1.Tables[0].Rows[u]["From_Date"]);
                    todate = Convert.ToString(ds1.Tables[0].Rows[u]["to_date"]);
                    q = "select IsHalfDay, CASE WHEN IsHalfDay = 1 THEN 'Half Day' ELSE 'Full Day' END Leave,case when LeaveSession=2 then 'Morning' when LeaveSession=1 then 'Evening' else '-' end LeaveSession, LeaveFrom, LeaveTo,CONVERT(VARCHAR(11),HalfDate,103) as HalfDate,GateReqReason,leaveChangeReason,ReqAppStaffAppNo from RQ_Requisition where RequestType=5 and LeaveFrom>='" + fromdate + "' and LeaveTo<='" + todate + "' and ReqAppNo='" + appno + "' and LeaveMasterFK='" + leavepkval + "' and ReqAppStatus not in('2','0')";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(q, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (y = 0; y < ds.Tables[0].Rows.Count; y++)
                        {
                            fromdatenew = Convert.ToString(ds.Tables[0].Rows[y]["LeaveFrom"]);
                            todatenew = Convert.ToString(ds.Tables[0].Rows[y]["LeaveTo"]);
                            string ishalfdate = Convert.ToString(ds.Tables[0].Rows[y]["IsHalfDay"]);
                            reason = d2.GetFunction("select MasterValue from CO_MasterValues where MasterCriteria='HGRea' and MasterCode='" + Convert.ToString(ds.Tables[0].Rows[y]["GateReqReason"]) + "'");
                            if (reason == "0")
                            {
                                reason = "";
                            }
                            leavechage = Convert.ToString(ds.Tables[0].Rows[y]["leaveChangeReason"]);
                            approvalstaff = d2.GetFunction("select sm.appl_name from staff_appl_master sm ,staffmaster m where sm.appl_no=m.appl_no and sm.appl_id='" + Convert.ToString(ds.Tables[0].Rows[y]["ReqAppStaffAppNo"]) + "'");
                            if (approvalstaff == "0")
                            {
                                approvalstaff = "Direct Approval-Self";
                            }
                            string hlf = Convert.ToString(ds.Tables[0].Rows[y]["HalfDate"]);
                            if (hlf == "" || hlf == "0")
                            {
                                hlf = "";
                            }
                            int finaldate = 0;
                            double t = 0;
                            string dtT = fromdatenew;
                            string[] Split = dtT.Split('/');
                            string enddt = todatenew;
                            Split = enddt.Split('/');
                            DateTime fromdatea = Convert.ToDateTime(dtT);
                            DateTime fromdatea1 = Convert.ToDateTime(dtT);
                            DateTime todatea = Convert.ToDateTime(enddt);
                            TimeSpan days = todatea - fromdatea;
                            string ndate = Convert.ToString(days);
                            Split = ndate.Split('.');
                            string getdate = Split[0];
                            if (fromdatea != todatea)
                            {
                                for (; fromdatea <= todatea; )
                                {
                                    string dayy = fromdatea.ToString("dddd");
                                    leavedayscheckcount = false;
                                    if (dayy == "Sunday")
                                    {
                                        string qur1 = "select * from individual_leave_type where  staff_code='" + txt_staff_code.Text + "' and college_code=" + ddlcollege.SelectedItem.Value + "";
                                        ds2 = d2.select_method_wo_parameter(qur1, "Text");
                                        if (ds2.Tables[0].Rows.Count > 0)
                                        {
                                            string[] spl_type = ds2.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                                            for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                                            {
                                                string[] split_leave = spl_type[i].Split(';');
                                                string ll = d2.GetFunction("select category from leave_category where shortname='" + leavetypename + "' and college_code='" + ddlcollege.SelectedItem.Value + "'");
                                                if (split_leave[0].ToString() == ll)
                                                {
                                                    if (split_leave[3] == "0")
                                                    {
                                                        leavedayscheckcount = true;
                                                    }
                                                    else
                                                    {
                                                        leavedayscheckcount = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (leavedayscheckcount == false)
                                    {
                                        // finaldate = Convert.ToInt32(getdate) + 1;
                                        llcount++;
                                    }
                                    fromdatea = fromdatea.AddDays(1);
                                    t = llcount;
                                }
                            }
                            else
                            {
                                finaldate = 1;
                                t = finaldate;
                            }
                            if (ishalfdate == "True")
                            {
                                t = Convert.ToDouble(finaldate) - 0.5;
                            }
                            dr = dt.NewRow();
                            dr[0] = fromdatea1.ToString("dd/MM/yyyy");
                            dr[1] = todatea.ToString("dd/MM/yyyy");
                            dr[2] = reason;
                            dr[3] = approvalstaff;
                            dr[4] = leavechage;
                            dr[5] = t;
                            dr[6] = Convert.ToString(ds.Tables[0].Rows[y]["LeaveSession"]);
                            dr[7] = hlf;
                            dt.Rows.Add(dr);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            gridView3.DataSource = dt;
                            gridView3.DataBind();
                        }
                        lbl_leavedis.Text = d2.GetFunction("select category from leave_category where shortname='" + leavetypename + "' and college_code='" + collegecode1 + "'") + "(" + leavetypename + ")";
                    }
                }
            }
        }
    }

    public void btn_leavedisclose_Click(object sender, EventArgs e)
    {
        divleavedis.Visible = false;
    }

    public void altbatch_click(object sender, EventArgs e)
    {
        Session["toDate"] = txt_to.Text;
        Session["leaveType"] = ddl_leave_type.SelectedValue.ToString();
        Session["fromDate"] = txt_frm.Text;  //modified by prabha  on jan 25 2018
        if (!string.IsNullOrEmpty(txtleavereason.Text))
        {
            Session["reason"] = txtleavereason.Text;
        }
        string purchasevendorfk = "1";
        //magesh 13/2/18
        Session["forrequest"] = Session["Staff_Code"].ToString();
        Session["leavereqstatus"] = "LeaveRequest";
        Session["alterforrequest"] = Session["Staff_Code"].ToString();
        //Response.Redirect("~/ScheduleMOD/Alternatesched.aspx?@@barath$$=" + (purchasevendorfk + "@" + txt_frm.Text + "$" + txt_to.Text));
        batchbtn.Attributes.Add("href", "../ScheduleMOD/Alternatesched.aspx?@@BB$$=" + (purchasevendorfk + "@" + txt_frm.Text + "$" + txt_to.Text));
        //batchbtn.Attributes.Add("target", "_blank");
        //Response.Write("<script>window.open('~/ScheduleMOD/Alternatesched.aspx?@@barath$$=" + (purchasevendorfk) + "', '_newtab');</script>");
        //      Page.ClientScript.RegisterStartupScript(
        //this.GetType(), "OpenWindow", "window.open('~/ScheduleMOD/Alternatesched.aspx?@@barath$$=" + (purchasevendorfk) + "','_newtab');", true);
    }

    public void lnk_AlterStaff_click(object sender, EventArgs e)
    {
        alternatePeriodCheck();
        binddept();
        binddesignation();
        loadstafftype();
        loadcategory();
        loadStaffid();
    }

    protected void Btn_Apply_Leave_Click(object sender, EventArgs e)
    {
        try
        {
            //magesh 8.3.18
            string alter_status_new = Convert.ToString(Session["alter_done"]);
            if (alter_status_new == "1")
                Session["conformleave"] = "yes";//magesh 8.3.18
            chkrelived = 0;
            alternateRights();
            leaverequestsetting();
            int alchk = 0;
            Int64 ReqStaffAppNo = 0;
            string reqappno = "";
            string reason = "";
            string req_code = "";
            string halfday = "";
            string day = "0";
            string leavecount = "";
            string staff_code = "";
            string sub_staff_code = "";
            string[] Split;
            string staff_code1 = "";
            DateTime RequestfromDate = new DateTime();
            RequestfromDate = TextToDate(txt_frm);
            DateTime RequesttoDate = new DateTime();
            RequesttoDate = TextToDate(txt_to);
            string[] spl_frm_date = txt_frm.Text.Split('/');
            string frm_date = spl_frm_date[1] + "/" + spl_frm_date[0] + "/" + spl_frm_date[2];
            string[] spl_to_date = txt_to.Text.Split('/');
            string to_date = spl_to_date[1] + "/" + spl_to_date[0] + "/" + spl_to_date[2];
            collegecode = Convert.ToString(ddlcollege.SelectedItem.Value);
            if (datecheck == false)
            {
                int leavcount1 = 0;
                // int lv_days1 = 0; delsi03/05/2018
                double lv_days1 = 0;
                int ch = 0;
                int ch1 = 0;
                halfday = "0";
                abc();
                string chk = "";
                chk = d2.GetFunction("select distinct s.staff_code,sa.appl_no  from staffmaster s,staff_appl_master sa,stafftrans t,hrdept_master hr,desig_master dm where t.staff_code =s.staff_code and s.appl_no=sa.appl_no and t.dept_code=hr.dept_code and dm.desig_code=t.desig_code and settled=0 and resign =0 and latestrec ='1' and s.staff_code='" + txt_staff_code.Text + "'");
                if (chk == "0")
                {
                    txt_staff_code.Text = "";
                    Btn_Cancel_Click(sender, e);
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select The Correct Staff Code";
                    return;
                }
                if (RequestfromDate > RequesttoDate)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select The Correct Date";
                    return;
                }
                if (txt_staff_code.Text == "")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Staff Code Should Not Be Empty";
                    return;
                }
                if (ddl_leave_type.Items.Count == 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set Leave Details In Staff Appointment Screen After That Proceed";
                    return;
                }
                if (ddl_leave_type.SelectedItem.Value == "")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set Leave Details In Staff Appointment Screen After That Proceed";
                    return;
                }
                else if (ddl_leave_type.SelectedItem.Text == "--Select--")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select The Leave Type";
                    return;
                }
                string stfc = d2.GetFunction("select sm.appl_id from staffmaster s,staff_appl_master sm where s.appl_no=sm.appl_no and s.staff_code='" + staffcodesession + "'");
                string checkrequest = d2.GetFunction("select sm.appl_id from RQ_RequestHierarchy,staff_appl_master sm where ReqStaffAppNo=sm.appl_id and RequestType=5 and ReqStaffAppNo='" + stfc + "'");
                if (requestpermissioncheck == "1")
                {
                    staff_code = staffcodesession;
                    staff_code1 = staffcodesession;
                    sub_staff_code = staffcodesession;
                    if (checkrequest == "0")
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Set Hierarchy After That Proceed";
                        return;
                    }
                }
                else if (requestpermissioncheck == "2")
                {
                    if (checkrequest == "0")
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Set Hierarchy After That Proceed";
                        return;
                    }
                    staff_code = Convert.ToString(txt_staff_code.Text);
                    staff_code1 = staffcodesession;
                    sub_staff_code = staff_code;
                }
                else if (requestpermissioncheck == "3")
                {
                    staff_code = Convert.ToString(txt_staff_code.Text);
                    staff_code1 = staff_code;
                    sub_staff_code = staff_code;
                }
                else if (requestpermissioncheck == "")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set The Leave Approval Permission After That Proceed";
                    return;
                }
                else
                    staff_code = Convert.ToString(txt_staff_code.Text);
                string linkvaluequery = "";
                string linkvalue = "";
                string holidayquery = string.Empty;
                Hashtable HasHoliday = new Hashtable();
                linkvaluequery = d2.GetFunction("select value from Master_Settings where settings='HR_PanelSettings' and usercode='" + Convert.ToString(Session["usercode"]) + "'");
                if (linkvaluequery.Contains('3'))
                    linkvalue = "1";
                else
                    linkvalue = "0";
                if (linkvalue == "0")
                {
                    //holidayquery = "select distinct halforfull ,morning,evening,ltype,h.stftype,holiday_desc,convert (varchar(10), holiday_Date,101) as holiday_Date from holidayStaff h,stafftrans t  where h.category_code =t.category_code and college_code='" + ddlcollege.SelectedItem.Value + "' and t.staff_code ='" + staff_code + "'";

                    holidayquery = "select distinct halforfull ,morning,evening,ltype,h.stftype,holiday_desc,convert (varchar(10), holiday_Date,101) as holiday_Date from holidayStaff h,stafftrans t  where h.category_code =t.category_code and college_code='" + ddlcollege.SelectedItem.Value + "' and t.staff_code ='" + staff_code + "' and t.desig_code=h.dept_code";

                }
                if (linkvalue == "1")
                {
                    // holidayquery = "select distinct halforfull ,morning,evening,ltype,h.stftype,holiday_desc,convert (varchar(10), holiday_Date,101) as holiday_Date from holidayStaff h,stafftrans t  where h.stftype =t.stftype and college_code='" + ddlcollege.SelectedItem.Value + "' and t.staff_code ='" + staff_code + "'";

                    holidayquery = "select distinct halforfull ,morning,evening,ltype,h.stftype,holiday_desc,convert (varchar(10), holiday_Date,101) as holiday_Date from holidayStaff h,stafftrans t  where h.stftype =t.stftype and college_code='" + ddlcollege.SelectedItem.Value + "' and t.staff_code ='" + staff_code + "' and t.desig_code=h.dept_code";
                }
                if (holidayquery != "")
                {
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(holidayquery, "Text");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int intH = 0; intH < ds.Tables[0].Rows.Count; intH++)
                        {
                            if (!HasHoliday.ContainsKey(Convert.ToString(ds.Tables[0].Rows[intH]["holiday_Date"])))
                                HasHoliday.Add(Convert.ToString(ds.Tables[0].Rows[intH]["holiday_Date"]), Convert.ToString(ds.Tables[0].Rows[intH]["halforfull"]) + "-" + Convert.ToString(ds.Tables[0].Rows[intH]["morning"]) + "-" + Convert.ToString(ds.Tables[0].Rows[intH]["evening"]));
                        }
                    }
                }
                for (int i = 0; i < gridView2.Rows.Count; i++)
                {
                    if (gridView2.Rows[i].Cells[1].Text != con_txt)
                        continue;
                    string value = gridView2.Rows[i].Cells[2].Text;
                    string[] sp = value.Split('/');
                    string secnval = sp[1];
                    //  lv_days1 = Convert.ToInt32(secnval); delsi 03/05/2018
                    lv_days1 = Convert.ToDouble(secnval);
                    ArrayList addnew = new ArrayList();
                    DateTime fromdate = new DateTime();
                    fromdate = TextToDate(txt_frm);
                    DateTime todate = new DateTime();
                    todate = TextToDate(txt_to);
                    TimeSpan days = todate - fromdate;
                    string ndate = Convert.ToString(days);
                    if (fromdate == todate)
                        Split = ndate.Split(':');
                    else
                        Split = ndate.Split('.');
                    string getdate = Split[0];
                    int finaldate = Convert.ToInt32(getdate);
                    if (fromdate == todate)
                        leavecount = "1";
                    else
                        leavecount = Convert.ToString(finaldate + 1);
                    leavcount1 = Convert.ToInt32(leavecount);
                    leavcount1 = Convert.ToInt32(leavecount);
                    leavcount1 = leavcount1 + Convert.ToInt32(lv_days1);
                    if (alert == false)
                    {
                        if (txt_staff_code.Text != "" && txt_staff_name.Text != "" && txt_dep.Text != "" && txt_des.Text != "")
                        {
                            ReqStaffAppNo = Convert.ToInt64(d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staff_code + "'"));
                            reqappno = d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staff_code1 + "'");
                            string txtreason = Convert.ToString(txtleavereason.Text);
                            reason = subjectcodenew("HGRea", txtreason);
                            string fkvalue = Convert.ToString(ddl_leave_type.SelectedItem.Text);
                            string fk = d2.GetFunction("select LeaveMasterPK from leave_category where category='" + fkvalue + "' and college_code='" + Session["collegecode"] + "'");
                            req_code = Convert.ToString(txt_rqstn_leave.Text);
                            DateTime RequestDate = new DateTime();
                            RequestDate = TextToDate(txt_time_rqstn_leave);
                            string halfdate = "";
                            int cc = 0;
                            int ccc = 0;
                            int tt = 0;
                            int tt1 = 0;
                            double yrLeaveCount = 0;
                            double mnthLeaveCount = 0;
                            double mnthCaryLeaveCount = 0;
                            double Sundayinclude = 0;
                            double HolidayInclude = 0;
                            double yearlyCaryoverLeaveCount = 0;//delsi2106
                            string leaveText = string.Empty;
                            string qur = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + Session["collegecode"] + "'";
                            DataSet CheckLeaveStatus = d2.select_method_wo_parameter(qur, "Text");
                            if (CheckLeaveStatus.Tables.Count > 0 && CheckLeaveStatus.Tables[0].Rows.Count > 0)
                            {
                                string[] spl_type = CheckLeaveStatus.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                                for (int lve = 0; spl_type.GetUpperBound(0) >= lve; lve++)
                                {
                                    string[] split_leave = spl_type[lve].Split(';');
                                    if (split_leave[0].ToString().ToLower() == ddl_leave_type.SelectedItem.ToString().ToLower())//delsi1402 added tolower()
                                    {
                                        leaveText = Convert.ToString(split_leave[0]);
                                        double.TryParse(Convert.ToString(split_leave[1]), out yrLeaveCount);
                                        double.TryParse(Convert.ToString(split_leave[2]), out mnthLeaveCount);
                                        double.TryParse(Convert.ToString(split_leave[6]), out mnthCaryLeaveCount);
                                        double.TryParse(Convert.ToString(split_leave[4]), out Sundayinclude);
                                        double.TryParse(Convert.ToString(split_leave[5]), out HolidayInclude);
                                        double.TryParse(Convert.ToString(split_leave[7]), out yearlyCaryoverLeaveCount);//delsi2106
                                    }
                                }
                            }
                            #region New Total Leave Count
                            DataSet dshrm = dshrMonthDate(Convert.ToDateTime(frm_date), Convert.ToDateTime(to_date));
                            double totLeaveCnt = 0;
                            Dictionary<string, double> dtLveCnt = new Dictionary<string, double>();
                            Dictionary<string, double> dtTotLeaveCnt = new Dictionary<string, double>();
                            for (int j = 0; j < GV1.Rows.Count; j++)
                            {
                                double cnt = 0;
                                TextBox txtdt = (TextBox)GV1.Rows[j].FindControl("txtdate");
                                CheckBox chkmrng = (CheckBox)GV1.Rows[j].FindControl("chk_mrng");
                                CheckBox chkevng = (CheckBox)GV1.Rows[j].FindControl("chk_evng");
                                DateTime leaveDate = Convert.ToDateTime(txtdt.Text.Split('/')[1] + "/" + txtdt.Text.Split('/')[0] + "/" + txtdt.Text.Split('/')[2]);
                                string sunday = leaveDate.ToString("dddd");
                                if (chkmrng.Checked && chkevng.Checked) // Add Holiday include and Sunday Include 
                                {
                                    totLeaveCnt += 1;
                                    //halfday = "0";
                                    //day = "0";
                                    cnt = 1;
                                }
                                if (chkmrng.Checked && !chkevng.Checked)
                                {
                                    totLeaveCnt += 0.5;
                                    halfday = "1";
                                    day = "1";
                                    cnt = 0.5;
                                    halfdate = Convert.ToString(leaveDate);
                                }
                                if (!chkmrng.Checked && chkevng.Checked)
                                {
                                    totLeaveCnt += 0.5;
                                    halfday = "2";
                                    day = "2";
                                    cnt = 0.5;
                                    halfdate = Convert.ToString(leaveDate);
                                }
                                if (sunday.Trim() == "Sunday" && Sundayinclude == 1)
                                {
                                    totLeaveCnt += 1;
                                    cnt = 1;
                                }
                                if (sunday.Trim() != "Sunday" && HasHoliday.ContainsKey(leaveDate.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                {
                                    string Getkeyvalue = Convert.ToString(HasHoliday[leaveDate.ToString("MM/dd/yyyy")]);
                                    if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                    {
                                        totLeaveCnt += 0.5;
                                        cnt = 0.5;
                                    }
                                    else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                    {
                                        totLeaveCnt += 1;
                                        cnt = 1;
                                    }
                                }
                                //poo
                                string MornCheck = "0";
                                string EvennCheck = "0";
                                if (chkmrng.Checked)
                                    MornCheck = "1";
                                if (chkevng.Checked)
                                    EvennCheck = "1";
                                if (!DateMornOrEvenLeaveDic.ContainsKey(Convert.ToString(leaveDate.ToString("MM/dd/yyyy"))))
                                {
                                    DateMornOrEvenLeaveDic.Add(Convert.ToString(leaveDate.ToString("MM/dd/yyyy")), MornCheck + "$" + EvennCheck);
                                }
                                if (dshrm.Tables.Count > 0 && dshrm.Tables[0].Rows.Count > 0)
                                {
                                    for (int dvrow = 0; dvrow < dshrm.Tables[0].Rows.Count; dvrow++)
                                    {
                                        DateTime tempfrdt = Convert.ToDateTime(dshrm.Tables[0].Rows[dvrow]["from_date"]);
                                        DateTime temptodt = Convert.ToDateTime(dshrm.Tables[0].Rows[dvrow]["to_date"]);
                                        if (leaveDate >= tempfrdt && leaveDate <= temptodt)
                                        {
                                            if (!dtLveCnt.ContainsKey(Convert.ToString(tempfrdt + "-" + temptodt)))
                                            {
                                                dtLveCnt.Add(Convert.ToString(tempfrdt + "-" + temptodt), cnt);
                                            }
                                            else
                                            {
                                                double TempCnt = 0;
                                                double.TryParse(Convert.ToString(dtLveCnt[Convert.ToString(tempfrdt + "-" + temptodt)]), out TempCnt);
                                                TempCnt += cnt;
                                                dtLveCnt.Remove(Convert.ToString(tempfrdt + "-" + temptodt));
                                                dtLveCnt.Add(Convert.ToString(tempfrdt + "-" + temptodt), TempCnt);
                                            }
                                        }
                                    }
                                }
                            }
                            totleavedays = totLeaveCnt;
                            #endregion
                            #region CheckAlterSchedule
                            if (alterrigths == "1" || alterrigths == "2")
                            {
                                bool saveOrAlter = false;
                                Check_Status(sub_staff_code, frm_date, to_date, Convert.ToInt32(halfday), Convert.ToString(cc), day, saveOrAlter);
                                if (flag == true)
                                    return;
                            }
                            #endregion
                            #region CheckTodayLeave Apply
                            //  string q = "select * from RQ_Requisition where RequestType=5 and ReqAppNo='" + ReqStaffAppNo + "' and LeaveTo>='" + RequestfromDate + "' and MONTH(RequestDate)=MONTH(GETDATE())  and ReqAppStatus not in('2','3')";
                            string q = "select * from RQ_Requisition where RequestType=5 and ReqAppNo='" + ReqStaffAppNo + "' and LeaveTo>='" + RequestfromDate + "' and ReqAppStatus not in('2','3')";//delsi 1407
                            ds1 = d2.select_method_wo_parameter(q, "Text");
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                DateTime hhalf = new DateTime();
                                for (int u = 0; u < ds1.Tables[0].Rows.Count; u++)
                                {
                                    DateTime ll = Convert.ToDateTime(ds1.Tables[0].Rows[u]["LeaveTo"]);
                                    DateTime l = Convert.ToDateTime(ds1.Tables[0].Rows[u]["LeaveFrom"]);
                                    string Ishalfday = Convert.ToString(ds1.Tables[0].Rows[u]["ishalfday"]);
                                    string hhalfdate = Convert.ToString(ds1.Tables[0].Rows[u]["HalfDate"]);
                                    if (hhalfdate != "")
                                        hhalf = Convert.ToDateTime(ds1.Tables[0].Rows[u]["HalfDate"]);
                                    for (; l <= ll; )
                                    {
                                        DateTime fromdateee = new DateTime();
                                        fromdateee = TextToDate(txt_frm);
                                        DateTime todateeee = new DateTime();
                                        todateeee = TextToDate(txt_to);
                                        for (; fromdateee <= todateeee; )
                                        {
                                            if (Ishalfday.ToUpper() == "TRUE")
                                            {
                                                if (hhalfdate != "")
                                                {
                                                    if (hhalf == fromdateee)
                                                    {
                                                        string checksession = Convert.ToString(ds1.Tables[0].Rows[u]["LeaveSession"]);
                                                        if (checksession == day || day == "0")
                                                        {
                                                            datecheck = true;
                                                            imgdiv2.Visible = true;
                                                            lbl_alert.Text = "You Are Already Requested In This Same Date,Change The Session";
                                                            BindLeave();
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string startdate = fromdateee.ToString("dd/MM/yyyy");
                                                        string sdate = l.ToString("dd/MM/yyyy");
                                                        if (startdate == sdate)
                                                        {
                                                            datecheck = true;
                                                            imgdiv2.Visible = true;
                                                            lbl_alert.Text = "You Are Already Requested In This Same Date";
                                                            BindLeave();
                                                            return;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string startdate = fromdateee.ToString("dd/MM/yyyy");
                                                    string sdate = l.ToString("dd/MM/yyyy");
                                                    if (startdate == sdate)
                                                    {
                                                        datecheck = true;
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "You Are Already Requested In This Same Date";
                                                        BindLeave();
                                                        return;
                                                    }
                                                }
                                            }
                                            else //barath 12.38
                                            {
                                                string startdate = fromdateee.ToString("dd/MM/yyyy");
                                                string sdate = l.ToString("dd/MM/yyyy");
                                                if (startdate == sdate)
                                                {
                                                    datecheck = true;
                                                    imgdiv2.Visible = true;
                                                    lbl_alert.Text = "You Are Already Requested In This Same Date";
                                                    BindLeave();
                                                    return;
                                                }
                                            }
                                            fromdateee = fromdateee.AddDays(1);
                                        }
                                        l = l.AddDays(1);
                                    }
                                }
                            }
                            #endregion
                            Leave_Apply(totleavedays, dtLveCnt, yrLeaveCount, mnthLeaveCount, mnthCaryLeaveCount, Sundayinclude, HolidayInclude, yearlyCaryoverLeaveCount, leaveText, HasHoliday);
                            int s = 0;
                            string save_query = "";
                            if (alert == false)
                            {
                                ItemReqNo();
                                req_code = Convert.ToString(txt_rqstn_leave.Text);
                                //staff incharge to alternate for hod staff
                                double hodAlterApplid = 0;
                                if (ddlstfincharge.Items.Count > 0)
                                {
                                    string hodAlterStaff = string.Empty;
                                    hodAlterStaff = Convert.ToString(ddlstfincharge.SelectedItem.Value);
                                    double.TryParse(Convert.ToString(d2.GetFunction(" select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + hodAlterStaff + "'")), out hodAlterApplid);
                                }
                                if (halfday == "0")
                                {
                                    if (requestpermissioncheck == "3")//delsi1711
                                    { // ReqappStaffAppNo added by poo 07.11.17
                                        save_query = "insert into RQ_Requisition (RequestType,RequestCode,RequestDate,ReqAppNo,ReqStaffAppNo,LeaveMasterFK,LeaveFrom,LeaveTo,IsHalfDay,LeaveSession,GateReqReason,MemType,ReqApproveStage,ReqAppStatus,college_code,HodAlterInchargeAppNo,ReqappStaffAppNo)VALUES('5','" + req_code + "','" + RequestDate + "','" + ReqStaffAppNo + "','" + reqappno + "','" + fk + "','" + RequestfromDate + "','" + RequesttoDate + "','" + halfday + "','" + day + "','" + reason + "','2','1','1','" + Session["collegecode"] + "','" + hodAlterApplid + "','" + stfc + "')";
                                        s = d2.update_method_wo_parameter(save_query, "Text");
                                        string pk = d2.GetFunction("select RequisitionPK from RQ_Requisition where RequisitionPK=((select max(RequisitionPK) from RQ_Requisition)) and RequestType=5");
                                        foreach (GridViewRow gRow in GV1.Rows)
                                        {
                                            string type = string.Empty;
                                            string leavedate = string.Empty;
                                            CheckBox chk_mrng = (CheckBox)gRow.FindControl("chk_mrng");
                                            CheckBox chk_evng = (CheckBox)gRow.FindControl("chk_evng");
                                            TextBox txtdate = (TextBox)gRow.FindControl("txtdate");
                                            leavedate = TextToDate(txtdate).ToString("MM/dd/yyy");
                                            if (!chk_mrng.Checked)
                                            {
                                                type = "2";

                                            }
                                            else if (!chk_evng.Checked)
                                            {
                                                type = "1";

                                            }
                                            else
                                            {
                                                type = "0";
                                            }

                                            string insertqry = "insert into staff_leave_dates(requestfk,LeaveDate,SessionType,isApproved) values('" + pk + "','" + leavedate + "','" + type + "','1')";
                                            int saveqry = d2.update_method_wo_parameter(insertqry, "text");
                                        }
                                        add_attn(pk);
                                    }
                                    else
                                    {
                                        save_query = "insert into RQ_Requisition (RequestType,RequestCode,RequestDate,ReqAppNo,ReqStaffAppNo,LeaveMasterFK,LeaveFrom,LeaveTo,IsHalfDay,LeaveSession,GateReqReason,MemType,ReqApproveStage,college_code,HodAlterInchargeAppNo)VALUES('5','" + req_code + "','" + RequestDate + "','" + ReqStaffAppNo + "','" + reqappno + "','" + fk + "','" + RequestfromDate + "','" + RequesttoDate + "','" + halfday + "','" + day + "','" + reason + "','2','0','" + Session["collegecode"] + "','" + hodAlterApplid + "')";
                                        s = d2.update_method_wo_parameter(save_query, "Text");
                                        string pk = d2.GetFunction("select RequisitionPK from RQ_Requisition where RequisitionPK=((select max(RequisitionPK) from RQ_Requisition)) and RequestType=5");
                                        if (s > 0)
                                        {
                                            int smswhilerequest = 0;
                                            int.TryParse(Convert.ToString(d2.GetFunction(" select LinkValue from New_InsSettings where LinkName='smssendwhilerequest' and user_code ='" + usercode + "'")), out smswhilerequest);//and college_code ='" + 
                                            string leave_cateCode = d2.GetFunction("select category from leave_category where LeaveMasterPK in(select LeaveMasterFK from RQ_Requisition where RequisitionPK='" + pk + "')");
                                            string applid = d2.GetFunction("select ReqStaffAppNo from RQ_Requisition where RequisitionPK='" + pk + "'");
                                            string staffname = d2.GetFunction("select appl_name  from  staff_appl_master where appl_id='" + applid + "'");

                                            if (smswhilerequest == 1)
                                            {
                                                if (sms_app == "2")
                                                {
                                                    string text = staffname + "" + "Applied For" + "" + leave_cateCode;
                                                    access();
                                                    sms1(pk, text);
                                                }

                                            }

                                        }

                                        foreach (GridViewRow gRow in GV1.Rows)
                                        {
                                            string type = string.Empty;
                                            string leavedate = string.Empty;
                                            CheckBox chk_mrng = (CheckBox)gRow.FindControl("chk_mrng");
                                            CheckBox chk_evng = (CheckBox)gRow.FindControl("chk_evng");
                                            TextBox txtdate = (TextBox)gRow.FindControl("txtdate");
                                            leavedate = TextToDate(txtdate).ToString("MM/dd/yyy");
                                            if (!chk_mrng.Checked)
                                            {
                                                type = "2";

                                            }
                                            else if (!chk_evng.Checked)
                                            {
                                                type = "1";

                                            }
                                            else
                                            {
                                                type = "0";
                                            }

                                            string insertqry = "insert into staff_leave_dates(requestfk,LeaveDate,SessionType) values('" + pk + "','" + leavedate + "','" + type + "')";
                                            int saveqry = d2.update_method_wo_parameter(insertqry, "text");
                                        }
                                    }
                                }
                                else
                                {
                                    if (requestpermissioncheck == "3")
                                    { // ReqappStaffAppNo added by poo 07.11.17
                                        save_query = "insert into RQ_Requisition (RequestType,RequestCode,RequestDate,ReqAppNo,ReqStaffAppNo,LeaveMasterFK,LeaveFrom,LeaveTo,IsHalfDay,LeaveSession,GateReqReason,MemType,ReqApproveStage,HalfDate,ReqAppStatus,college_code,HodAlterInchargeAppNo,ReqappStaffAppNo)VALUES('5','" + req_code + "','" + RequestDate + "','" + ReqStaffAppNo + "','" + reqappno + "','" + fk + "','" + RequestfromDate + "','" + RequesttoDate + "','" + halfday + "','" + day + "','" + reason + "','2','1','" + halfdate + "','1','" + Session["collegecode"] + "','" + hodAlterApplid + "','" + stfc + "')";
                                        s = d2.update_method_wo_parameter(save_query, "Text");
                                        string pk = d2.GetFunction("select RequisitionPK from RQ_Requisition where RequisitionPK=((select max(RequisitionPK) from RQ_Requisition)) and RequestType=5");
                                        foreach (GridViewRow gRow in GV1.Rows)
                                        {
                                            string type = string.Empty;
                                            string leavedate = string.Empty;
                                            CheckBox chk_mrng = (CheckBox)gRow.FindControl("chk_mrng");
                                            CheckBox chk_evng = (CheckBox)gRow.FindControl("chk_evng");
                                            TextBox txtdate = (TextBox)gRow.FindControl("txtdate");
                                            leavedate = TextToDate(txtdate).ToString("MM/dd/yyy");
                                            if (!chk_mrng.Checked)
                                            {
                                                type = "2";

                                            }
                                            else if (!chk_evng.Checked)
                                            {
                                                type = "1";

                                            }
                                            else
                                            {
                                                type = "0";
                                            }

                                            string insertqry = "insert into staff_leave_dates(requestfk,LeaveDate,SessionType,isApproved) values('" + pk + "','" + leavedate + "','" + type + "','1')";
                                            int saveqry = d2.update_method_wo_parameter(insertqry, "text");
                                        }

                                        add_attn(pk);
                                    }
                                    else
                                    {
                                        save_query = "insert into RQ_Requisition (RequestType,RequestCode,RequestDate,ReqAppNo,ReqStaffAppNo,LeaveMasterFK,LeaveFrom,LeaveTo,IsHalfDay,LeaveSession,GateReqReason,MemType,ReqApproveStage,HalfDate,ReqAppStatus,college_code,HodAlterInchargeAppNo)VALUES('5','" + req_code + "','" + RequestDate + "','" + ReqStaffAppNo + "','" + reqappno + "','" + fk + "','" + RequestfromDate + "','" + RequesttoDate + "','" + halfday + "','" + day + "','" + reason + "','2','0','" + halfdate + "','0','" + Session["collegecode"] + "','" + hodAlterApplid + "')";
                                        s = d2.update_method_wo_parameter(save_query, "Text");//delsi1711
                                        string pk = d2.GetFunction("select RequisitionPK from RQ_Requisition where RequisitionPK=((select max(RequisitionPK) from RQ_Requisition)) and RequestType=5");
                                        foreach (GridViewRow gRow in GV1.Rows)
                                        {
                                            string type = string.Empty;
                                            string leavedate = string.Empty;
                                            CheckBox chk_mrng = (CheckBox)gRow.FindControl("chk_mrng");
                                            CheckBox chk_evng = (CheckBox)gRow.FindControl("chk_evng");
                                            TextBox txtdate = (TextBox)gRow.FindControl("txtdate");
                                            leavedate = TextToDate(txtdate).ToString("MM/dd/yyy");
                                            if (!chk_mrng.Checked)
                                            {
                                                type = "2";

                                            }
                                            else if (!chk_evng.Checked)
                                            {
                                                type = "1";

                                            }
                                            else
                                            {
                                                type = "0";
                                            }

                                            string insertqry = "insert into staff_leave_dates(requestfk,LeaveDate,SessionType) values('" + pk + "','" + leavedate + "','" + type + "')";
                                            int saveqry = d2.update_method_wo_parameter(insertqry, "text");
                                        }

                                    }
                                }
                            }
                            if (s == 1)
                            {
                                string max = d2.GetFunction("select RequisitionPK from RQ_Requisition where RequisitionPK=((select max(RequisitionPK) from RQ_Requisition))");
                                access();
                                if (sms_req == "1")
                                    sms1(max);
                                trincharge.Visible = false;
                                imgdiv2.Visible = true;
                                lbl_alert.Text = "Saved Successfully";
                                saveclear = 1;
                                ItemReqNo();
                            }
                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "Staff details should not be empty";
                        }
                    }
                }
            }
            BindLeave();
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
        finally { ScriptManager.RegisterStartupScript(this, GetType(), "InvokeButton", "HideLoadingDiv();", true); }
    }

    protected void alternatePeriodCheck()
    {
        try
        {
            chkrelived = 0;
            alternateRights();
            leaverequestsetting();
            int alchk = 0;
            Int64 ReqStaffAppNo = 0;
            string reqappno = "";
            string reason = "";
            string req_code = "";
            string halfday = "";
            string day = "";
            string leavecount = "";
            string staff_code = "";
            string sub_staff_code = "";
            string[] Split;
            string staff_code1 = "";
            DateTime RequestfromDate = new DateTime();
            RequestfromDate = TextToDate(txt_frm);
            DateTime RequesttoDate = new DateTime();
            RequesttoDate = TextToDate(txt_to);
            string[] spl_frm_date = txt_frm.Text.Split('/');
            string frm_date = spl_frm_date[1] + "/" + spl_frm_date[0] + "/" + spl_frm_date[2];
            string[] spl_to_date = txt_to.Text.Split('/');
            string to_date = spl_to_date[1] + "/" + spl_to_date[0] + "/" + spl_to_date[2];
            collegecode = Convert.ToString(ddlcollege.SelectedItem.Value);
            if (datecheck == false)
            {
                int leavcount1 = 0;
                int lv_days1 = 0;
                int ch = 0;
                int ch1 = 0;
                halfday = "0";
                abc();
                string chk = "";
                chk = d2.GetFunction("select distinct s.staff_code,sa.appl_no  from staffmaster s,staff_appl_master sa,stafftrans t,hrdept_master hr,desig_master dm where t.staff_code =s.staff_code and s.appl_no=sa.appl_no and t.dept_code=hr.dept_code and dm.desig_code=t.desig_code and settled=0 and resign =0 and latestrec ='1' and s.staff_code='" + txt_staff_code.Text + "'");
                if (chk == "0")
                {
                    txt_staff_code.Text = "";
                    Btn_Cancel_Click(sender, e);
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select The Correct Staff Code";
                    return;
                }
                if (RequestfromDate > RequesttoDate)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Select The Correct Date";
                    return;
                }
                if (txt_staff_code.Text == "")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Staff Code Should Not Be Empty";
                    return;
                }
                if (ddl_leave_type.Items.Count == 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set Leave Details In Staff Appointment Screen After That Proceed";
                    return;
                }
                if (ddl_leave_type.SelectedItem.Value == "")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set Leave Details In Staff Appointment Screen After That Proceed";
                    return;
                }
                //else if (ddl_leave_type.SelectedItem.Text == "--Select--")
                //{
                //    imgdiv2.Visible = true;
                //    lbl_alert.Text = "Select The Leave Type";
                //    return;
                //}
                string stfc = d2.GetFunction("select sm.appl_id from staffmaster s,staff_appl_master sm where s.appl_no=sm.appl_no and s.staff_code='" + staffcodesession + "'");
                string checkrequest = d2.GetFunction("select sm.appl_id from RQ_RequestHierarchy,staff_appl_master sm where ReqStaffAppNo=sm.appl_id and RequestType=5 and ReqStaffAppNo='" + stfc + "'");
                if (requestpermissioncheck == "1")
                {
                    staff_code = staffcodesession;
                    staff_code1 = staffcodesession;
                    sub_staff_code = staffcodesession;
                    if (checkrequest == "0")
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Set Hierarchy After That Proceed";
                        return;
                    }
                }
                else if (requestpermissioncheck == "2")
                {
                    if (checkrequest == "0")
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Set Hierarchy After That Proceed";
                        return;
                    }
                    staff_code = Convert.ToString(txt_staff_code.Text);
                    staff_code1 = staffcodesession;
                    sub_staff_code = staff_code;
                }
                else if (requestpermissioncheck == "3")
                {
                    staff_code = Convert.ToString(txt_staff_code.Text);
                    staff_code1 = staff_code;
                    sub_staff_code = staff_code;
                }
                else if (requestpermissioncheck == "")
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Set The Leave Approval Permission After That Proceed";
                    return;
                }
                else
                    staff_code = Convert.ToString(txt_staff_code.Text);
                for (int i = 0; i < gridView2.Rows.Count; i++)
                {
                    if (gridView2.Rows[i].Cells[1].Text == con_txt)
                    {
                        string value = gridView2.Rows[i].Cells[2].Text;
                        string[] sp = value.Split('/');
                        string secnval = sp[1];
                        lv_days1 = Convert.ToInt32(secnval);
                        ArrayList addnew = new ArrayList();
                        DateTime fromdate = new DateTime();
                        fromdate = TextToDate(txt_frm);
                        DateTime todate = new DateTime();
                        todate = TextToDate(txt_to);
                        TimeSpan days = todate - fromdate;
                        string ndate = Convert.ToString(days);
                        if (fromdate == todate)
                            Split = ndate.Split(':');
                        else
                            Split = ndate.Split('.');
                        string getdate = Split[0];
                        int finaldate = Convert.ToInt32(getdate);
                        if (fromdate == todate)
                            leavecount = "1";
                        else
                            leavecount = Convert.ToString(finaldate + 1);
                        leavcount1 = Convert.ToInt32(leavecount);
                        leavcount1 = Convert.ToInt32(leavecount);
                        leavcount1 = leavcount1 + lv_days1;
                        if (alert == false)
                        {
                            if (txt_staff_code.Text != "" && txt_staff_name.Text != "" && txt_dep.Text != "" && txt_des.Text != "")
                            {
                                ReqStaffAppNo = Convert.ToInt64(d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staff_code + "'"));
                                reqappno = d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staff_code1 + "'");
                                string txtreason = Convert.ToString(txtleavereason.Text);
                                reason = subjectcodenew("HGRea", txtreason);
                                string fkvalue = Convert.ToString(ddl_leave_type.SelectedItem.Text);
                                string fk = d2.GetFunction("select LeaveMasterPK from leave_category where category='" + fkvalue + "' and college_code='" + ddlcollege.SelectedItem.Value + "'");
                                req_code = Convert.ToString(txt_rqstn_leave.Text);
                                DateTime RequestDate = new DateTime();
                                RequestDate = TextToDate(txt_time_rqstn_leave);
                                string halfdate = "";
                                int cc = 0;
                                int ccc = 0;
                                int tt = 0;
                                int tt1 = 0;
                                #region New Total Leave Count
                                // bool checkbol = false;
                                // DataSet dshr = validateHrYear(collegecode, Convert.ToDateTime(frm_date), Convert.ToDateTime(to_date), ref  checkbol);                               
                                DataSet dshrm = dshrMonthDate(Convert.ToDateTime(frm_date), Convert.ToDateTime(to_date));
                                double totLeaveCnt = 0;
                                Dictionary<string, double> dtLveCnt = new Dictionary<string, double>();
                                for (int j = 0; j < GV1.Rows.Count; j++)
                                {
                                    double cnt = 0;
                                    TextBox txtdt = (TextBox)GV1.Rows[j].FindControl("txtdate");
                                    CheckBox chkmrng = (CheckBox)GV1.Rows[j].FindControl("chk_mrng");
                                    CheckBox chkevng = (CheckBox)GV1.Rows[j].FindControl("chk_evng");
                                    DateTime leaveDate = Convert.ToDateTime(txtdt.Text.Split('/')[1] + "/" + txtdt.Text.Split('/')[0] + "/" + txtdt.Text.Split('/')[2]);
                                    if (chkmrng.Checked && chkevng.Checked)
                                    {
                                        totLeaveCnt += 1;
                                        halfday = "0";
                                        day = "2";
                                        cnt = 1;
                                    }
                                    if (chkmrng.Checked && !chkevng.Checked)
                                    {
                                        totLeaveCnt += 0.5;
                                        halfday = "1";
                                        day = "1";
                                        cnt = 0.5;
                                        halfdate = Convert.ToString(leaveDate);
                                    }
                                    if (!chkmrng.Checked && chkevng.Checked)
                                    {
                                        totLeaveCnt += 0.5;
                                        halfday = "2";
                                        day = "2";
                                        cnt = 0.5;
                                        halfdate = Convert.ToString(leaveDate);
                                    }
                                    if (dshrm.Tables.Count > 0 && dshrm.Tables[0].Rows.Count > 0)
                                    {
                                        for (int dvrow = 0; dvrow < dshrm.Tables[0].Rows.Count; dvrow++)
                                        {
                                            DateTime tempfrdt = Convert.ToDateTime(dshrm.Tables[0].Rows[dvrow]["from_date"]);
                                            DateTime temptodt = Convert.ToDateTime(dshrm.Tables[0].Rows[dvrow]["to_date"]);
                                            if (leaveDate >= tempfrdt && leaveDate <= temptodt && !dtLveCnt.ContainsKey(Convert.ToString(tempfrdt + "-" + temptodt)))
                                            {
                                                dtLveCnt.Add(Convert.ToString(tempfrdt + "-" + temptodt), cnt);
                                            }
                                        }
                                    }
                                }
                                totleavedays = totLeaveCnt;
                                #endregion
                                bool saveOrAlter = true;
                                Check_Status(sub_staff_code, frm_date, to_date, Convert.ToInt32(halfday), Convert.ToString(cc), day, saveOrAlter);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void sms1(string req)
    {
        user_id = d2.GetFunction("select SMS_User_ID from Track_Value where college_code='" + collegecode1 + "'");
        string getval = d2.GetUserapi(user_id);
        string[] spret = getval.Split('-');
        if (spret.GetUpperBound(0) == 1)
        {
            SenderID = spret[0].ToString();
            Password = spret[1].ToString();
            Session["api"] = user_id;
            Session["senderid"] = SenderID;
        }
        string appno = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=5 and RequisitionPK='" + req + "'");
        strmsg = "Requested Successfully!";
        string staffnum = d2.GetFunction("select per_mobileno from staff_appl_master where appl_id='" + appno + "'");
        mobilenos = staffnum;
        if (mobilenos != "")
        {
            //string strpath = "http://dnd.airsmsmarketing.info/api/sendmsg.php?user=" + user_id + "&pass=" + Password + "&sender=" + SenderID + "&phone=" + mobilenos + "&text=" + strmsg + "&priority=ndnd&stype=normal";
            //smsreport(strpath, isst);
            isst = "1";
            int smsdet = d2.send_sms(user_id, collegecode1, usercode, mobilenos, strmsg, isst);
        }
    }

    public void dcheck()
    {
    }

    public void add_attn(string pkcodevalue)
    {
        string fromdate = "";
        string todate = "";
        string day = "";
        string month = "";
        string year = "";
        string halfday = "";
        string halfdate = "";
        string leavesession = "";
        string leavetype = "";
        string leavemasterfk = "";
        leavemasterfk = d2.GetFunction("select LeaveMasterFK from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
        string leavecat = d2.GetFunction("select category from leave_category where leavemasterpk='" + leavemasterfk + "'");
        fromdate = d2.GetFunction("select LeaveFrom from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
        todate = d2.GetFunction("select LeaveTo from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
        halfday = d2.GetFunction("select IsHalfDay from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
        leavesession = d2.GetFunction("select LeaveSession from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
        string qur = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + collegecode1 + "'";
        ds = d2.select_method_wo_parameter(qur, "Text");
        DateTime fdate = Convert.ToDateTime(fromdate);
        DateTime tdate = Convert.ToDateTime(todate);
        if (halfday == "True")
        {
            halfdate = d2.GetFunction("select HalfDate from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            string[] spl_type = ds.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
            for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
            {
                string[] split_leave = spl_type[i].Split(';');
                if (leavecat == split_leave[0])
                {
                    if (split_leave[4] == "1")
                    {
                        sun_check = true; // Include Sunday
                    }
                    else
                    {
                        sun_check = false;
                    }
                    if (split_leave[5] == "1")
                    {
                        holi_check = true; // Include Holiday
                    }
                    else
                    {
                        holi_check = false;
                    }
                }
            }
        }
        if (fdate == tdate)
        {
            for (; fdate <= tdate; )
            {
                fromdate = Convert.ToString(fdate);
                string[] sp = fromdate.Split('/');
                day = sp[1].TrimStart('0');//barath 29.12.17
                month = sp[0].TrimStart('0');//barath 29.12.17 
                string year1 = sp[2];
                string[] sp1 = year1.Split(' ');
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(sp[0] + "/" + sp[1] + "/" + sp[2]);
                string sunday = dt.ToString("dddd");
                year = sp1[0];
                string mothyear = month + "/" + year;
                string shortname = d2.GetFunction("select l.shortname from leave_category l,RQ_Requisition r where r.LeaveMasterFK=l.LeaveMasterPK and r.RequisitionPK='" + pkcodevalue + "'");
                string code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");

                string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806


                string get = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and  holiday_date='" + dt + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
                if (halfdate == fromdate)
                {
                    if (leavesession == "1")
                    {
                        string firsthalf = string.Empty;//delsi11/05/2018
                        string secondhalf = string.Empty;
                        string getpresentapp = d2.GetFunction(" select [" + day + "] from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'");
                        if (getpresentapp != null && getpresentapp != "" && getpresentapp != "0")
                        {
                            if (getpresentapp.Contains('-'))
                            {
                                string[] splitval = getpresentapp.Split('-');
                                firsthalf = Convert.ToString(splitval[0]);
                                secondhalf = Convert.ToString(splitval[1]);
                            }


                        }
                        leavetype = shortname + "-" + secondhalf;
                    }
                    else if (leavesession == "2")
                    {
                        string firsthalf = string.Empty;
                        string secondhalf = string.Empty;
                        string getpresentapp = d2.GetFunction(" select [" + day + "] from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'");
                        if (getpresentapp != null && getpresentapp != "" && getpresentapp != "0")
                        {
                            if (getpresentapp.Contains('-'))
                            {
                                string[] splitval = getpresentapp.Split('-');
                                firsthalf = Convert.ToString(splitval[0]);
                                secondhalf = Convert.ToString(splitval[1]);
                            }

                        }
                        leavetype = firsthalf + "-" + shortname;
                    }
                }
                else
                {
                    leavetype = shortname + "-" + shortname;//delsiref1711


                }
                bool CheckFalg = false;
                if (sun_check == true)
                {
                    if (sunday == "Sunday")
                    {
                        CheckFalg = true;
                    }
                }
                if (sunday != "Sunday")
                {
                    if (holi_check == true)
                    {
                        CheckFalg = true;
                    }
                    else if (holi_check == false)
                    {
                        if (get == "0")
                        {
                            CheckFalg = true;
                        }
                    }
                }
                if (CheckFalg == true)
                {
                    string attn_query = "if exists (select * from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "')update staff_attnd set [" + day + "]='" + leavetype + "' where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'else insert into staff_attnd (staff_code,mon_year,[" + day + "])values ('" + txt_staff_code.Text + "','" + mothyear + "','" + leavetype + "')";
                    int val = d2.update_method_wo_parameter(attn_query, "Text");
                }
                fdate = fdate.AddDays(1);
            }
        }
        else
        {
            DataSet alldateds = new DataSet();
            for (; fdate <= tdate; )
            {
                alldateds.Clear();
                fromdate = Convert.ToString(fdate);
                string[] sp = fromdate.Split('/');
                day = sp[1].TrimStart('0');//barath 29.12.17
                month = sp[0].TrimStart('0');//barath 29.12.17 
                string year1 = sp[2];
                string[] sp1 = year1.Split(' ');
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(sp[0] + "/" + sp[1] + "/" + sp[2]);
                string sunday = dt.ToString("dddd");
                year = sp1[0];
                string mothyear = month + "/" + year;
                string shortname = d2.GetFunction("select l.shortname from leave_category l,RQ_Requisition r where r.LeaveMasterFK=l.LeaveMasterPK and r.RequisitionPK='" + pkcodevalue + "'");
                string code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");

                string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806


                string get = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and  holiday_date='" + dt + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");

                string quer = "select * from staff_leave_dates where requestfk='" + pkcodevalue + "' and LeaveDate='" + fromdate + "'";
                alldateds = d2.select_method_wo_parameter(quer, "text");

                if (alldateds.Tables[0].Rows.Count > 0)
                {
                    string session = Convert.ToString(alldateds.Tables[0].Rows[0]["SessionType"]);
                    if (session == "0")
                    {
                        leavetype = shortname + "-" + shortname;


                    }
                    if (session == "1")
                    {
                        string firsthalf = string.Empty;//delsi11/05/2018
                        string secondhalf = string.Empty;
                        string getpresentapp = d2.GetFunction(" select [" + day + "] from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'");
                        if (getpresentapp != null && getpresentapp != "" && getpresentapp != "0")
                        {
                            if (getpresentapp.Contains('-'))
                            {
                                string[] splitval = getpresentapp.Split('-');
                                firsthalf = Convert.ToString(splitval[0]);
                                secondhalf = Convert.ToString(splitval[1]);
                            }


                        }
                        leavetype = shortname + "-" + secondhalf;

                    }
                    if (session == "2")
                    {

                        string firsthalf = string.Empty;
                        string secondhalf = string.Empty;
                        string getpresentapp = d2.GetFunction(" select [" + day + "] from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'");
                        if (getpresentapp != null && getpresentapp != "" && getpresentapp != "0")
                        {
                            if (getpresentapp.Contains('-'))
                            {
                                string[] splitval = getpresentapp.Split('-');
                                firsthalf = Convert.ToString(splitval[0]);
                                secondhalf = Convert.ToString(splitval[1]);
                            }

                        }
                        leavetype = firsthalf + "-" + shortname;

                    }
                }
                bool CheckFalg = false;
                if (sun_check == true)
                {
                    if (sunday == "Sunday")
                    {
                        CheckFalg = true;
                    }
                }
                if (sunday != "Sunday")
                {
                    if (holi_check == true)
                    {
                        CheckFalg = true;
                    }
                    else if (holi_check == false)
                    {
                        if (get == "0")
                        {
                            CheckFalg = true;
                        }
                    }
                }
                if (CheckFalg == true)
                {
                    string attn_query = "if exists (select * from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "')update staff_attnd set [" + day + "]='" + leavetype + "' where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'else insert into staff_attnd (staff_code,mon_year,[" + day + "])values ('" + txt_staff_code.Text + "','" + mothyear + "','" + leavetype + "')";
                    int val = d2.update_method_wo_parameter(attn_query, "Text");
                }

                fdate = fdate.AddDays(1);
            }
        }
    }

    //public void add_attn(string pkcodevalue)
    //{
    //    string fromdate = "";
    //    string todate = "";
    //    string day = "";
    //    string month = "";
    //    string year = "";
    //    string halfday = "";
    //    string halfdate = "";
    //    string leavesession = "";
    //    string leavetype = "";
    //    string leavemasterfk = "";
    //    leavemasterfk = d2.GetFunction("select LeaveMasterFK from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
    //    string leavecat = d2.GetFunction("select category from leave_category where leavemasterpk='" + leavemasterfk + "'");
    //    fromdate = d2.GetFunction("select LeaveFrom from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
    //    todate = d2.GetFunction("select LeaveTo from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
    //    halfday = d2.GetFunction("select IsHalfDay from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
    //    leavesession = d2.GetFunction("select LeaveSession from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
    //    string qur = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + collegecode1 + "'";
    //    ds = d2.select_method_wo_parameter(qur, "Text");
    //    DateTime fdate = Convert.ToDateTime(fromdate);
    //    DateTime tdate = Convert.ToDateTime(todate);
    //    if (halfday == "True")
    //    {
    //        halfdate = d2.GetFunction("select HalfDate from RQ_Requisition where RequestType=5 and RequisitionPK='" + pkcodevalue + "'");
    //    }
    //    for (; fdate <= tdate; )
    //    {
    //        fromdate = Convert.ToString(fdate);
    //        string[] sp = fromdate.Split('/');
    //        day = sp[1];
    //        month = sp[0];
    //        string year1 = sp[2];
    //        string[] sp1 = year1.Split(' ');
    //        DateTime dt = new DateTime();
    //        dt = Convert.ToDateTime(sp[0] + "/" + sp[1] + "/" + sp[2]);
    //        string sunday = dt.ToString("dddd");
    //        year = sp1[0];
    //        string mothyear = month + "/" + year;
    //        string shortname = d2.GetFunction("select l.shortname from leave_category l,RQ_Requisition r where r.LeaveMasterFK=l.LeaveMasterPK and r.RequisitionPK='" + pkcodevalue + "'");
    //        string code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
    //        string get = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and  holiday_date='" + dt + "' and college_code ='" + collegecode1 + "'");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            string[] spl_type = ds.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
    //            for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
    //            {
    //                string[] split_leave = spl_type[i].Split(';');
    //                if (leavecat == split_leave[0])
    //                {
    //                    if (split_leave[3] == "1")
    //                    {
    //                        sun_check = false;
    //                    }
    //                    else
    //                    {
    //                        sun_check = true;
    //                    }
    //                    if (sunday != "Sunday")
    //                    {
    //                        if (split_leave[4] == "1")
    //                        {
    //                            holi_check = false;
    //                        }
    //                        else
    //                        {
    //                            holi_check = true;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        if (halfdate == fromdate)
    //        {
    //            if (leavesession == "2")
    //            {
    //                leavetype = shortname + "-";
    //            }
    //            else if (leavesession == "1")
    //            {
    //                leavetype = "-" + shortname;
    //            }
    //        }
    //        else
    //        {
    //            leavetype = shortname + "-" + shortname;
    //        }
    //        if (sun_check == true)
    //        {
    //            if (sunday == "Sunday")
    //            {
    //                sun_check = true;
    //            }
    //            else
    //            {
    //                sun_check = false;
    //            }
    //        }
    //        if (sunday != "Sunday")
    //        {
    //            if (holi_check == true)
    //            {
    //                if (get == "0")
    //                {
    //                    holi_check = true;
    //                }
    //                else
    //                {
    //                    holi_check = false;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            holi_check = true;
    //        }
    //        if (sun_check == false && holi_check == false)
    //        {
    //            string attn_query = "if exists (select * from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "')update staff_attnd set [" + day + "]='" + leavetype + "' where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'else insert into staff_attnd (staff_code,mon_year,[" + day + "])values ('" + txt_staff_code.Text + "','" + mothyear + "','" + leavetype + "')";
    //            int val = d2.update_method_wo_parameter(attn_query, "Text");
    //        }
    //        else if (sun_check == false)
    //        {
    //            string attn_query = "if exists (select * from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "')update staff_attnd set [" + day + "]='" + leavetype + "' where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'else insert into staff_attnd (staff_code,mon_year,[" + day + "])values ('" + txt_staff_code.Text + "','" + mothyear + "','" + leavetype + "')";
    //            int val = d2.update_method_wo_parameter(attn_query, "Text");
    //        }
    //        else if (holi_check == false)
    //        {
    //            string attn_query = "if exists (select * from staff_attnd where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "')update staff_attnd set [" + day + "]='" + leavetype + "' where staff_code ='" + txt_staff_code.Text + "' and mon_year ='" + mothyear + "'else insert into staff_attnd (staff_code,mon_year,[" + day + "])values ('" + txt_staff_code.Text + "','" + mothyear + "','" + leavetype + "')";
    //            int val = d2.update_method_wo_parameter(attn_query, "Text");
    //        }
    //        fdate = fdate.AddDays(1);
    //    }
    //}

    protected void Leave_Apply(double totleaveday, Dictionary<string, double> dtLveCnt, double yrLeaveCount, double mnthLeaveCount, double mnthCaryLeaveCount, double Sundayinclude, double HolidayInclude, double yearlyCaryoverLeaveCount, string leaveText, Hashtable HolidayHash)
    {
        try
        {
            double ccc = 0;
            double cccc = 0;
            chkrelived = 0;

            DateTime fromdate1 = new DateTime();
            fromdate1 = TextToDate(txt_frm);
            string srtMnth = fromdate1.ToString("dd/MM/yyyy").Split('/')[1];
            string srtYear = fromdate1.ToString("dd/MM/yyyy").Split('/')[2];
            DateTime todate1 = new DateTime();
            todate1 = TextToDate(txt_to);
            string endMnth = todate1.ToString("dd/MM/yyyy").Split('/')[1];
            string endYear = todate1.ToString("dd/MM/yyyy").Split('/')[2];
            string staffCode = Convert.ToString(txt_staff_code.Text);
            collegecode = Convert.ToString(Session["collegecode"]);
            DateTime from_dateS = new DateTime();
            DateTime todateS = new DateTime();
            double totalyearleave = 0;
            string queryhr = "select * from hrpaymonths where SelStatus='1' and College_Code='" + collegecode + "'";
            DataSet hrmonthds = d2.select_method_wo_parameter(queryhr, "text");
            string getyear = string.Empty;
            if (hrmonthds.Tables[0].Rows.Count > 0)
            {
                getyear = Convert.ToString(hrmonthds.Tables[0].Rows[0]["PayYear"]);
            }
            string quryear = "select * from hrpaymonths where College_Code='" + collegecode + "' and SelStatus='0' and PayYear<='" + getyear + "' order by PayYear asc ";
            // Dictionary<string, string> hrDate = getHrDate(collegecode);
            DataSet ds_yearSelect = d2.select_method_wo_parameter(quryear, "text");
            if (ds_yearSelect.Tables.Count > 0 && ds_yearSelect.Tables[0].Rows.Count > 0)
            {
                for (int val = 0; val < ds_yearSelect.Tables[0].Rows.Count; val++)
                {
                    totalyearleave = totalyearleave + mnthLeaveCount;
                }
                from_dateS = Convert.ToDateTime(ds_yearSelect.Tables[0].Rows[0]["from_date"]);
                todateS = Convert.ToDateTime(ds_yearSelect.Tables[0].Rows[ds_yearSelect.Tables[0].Rows.Count - 1]["to_date"]);
            }
            if (yearlyCaryoverLeaveCount == 1)
            {
                yrLeaveCount = yrLeaveCount + totalyearleave;
            }
            if (yrLeaveCount != 0)
            {
                if (yrLeaveCount >= totleaveday)//delsi25/05
                {
                    //hr pay month
                    bool checkbol = false;
                    DataSet dt_paymonths = validateHrYear(collegecode, fromdate1, todate1, ref  checkbol);
                    if (dt_paymonths.Tables.Count > 0 && dt_paymonths.Tables[0].Rows.Count > 0)
                    {
                        #region
                        if (checkbol)
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "The Year of Apply of leave is not matching the current HR Year.";
                            alert = true;
                            return;
                        }
                        DateTime from_date = Convert.ToDateTime(dt_paymonths.Tables[0].Rows[0]["from_date"]);
                        DateTime todate = Convert.ToDateTime(dt_paymonths.Tables[0].Rows[dt_paymonths.Tables[0].Rows.Count - 1]["to_date"]);
                        double taken_leave = 0;
                        double yeartaken_leave = 0;
                        string leavepk = d2.GetFunction("select LeaveMasterPK from leave_category where category='" + leaveText + "' and college_code='" + collegecode + "'");
                        string appno = d2.GetFunction("select sm.appl_id from staff_appl_master sm,staffmaster s where sm.appl_no=s.appl_no and s.staff_code='" + staffCode + "'");
                        string qu2 = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + from_date + "' and LeaveTo<='" + todate + "' and ReqAppNo='" + appno + "' and( ReqAppStatus='1' or ReqAppStatus='0') and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + collegecode + "' ";
                        DataSet dt_monthleave_count = d2.select_method_wo_parameter(qu2, "text");
                        if (dt_monthleave_count.Tables.Count > 0 && dt_monthleave_count.Tables[0].Rows.Count == 0)
                        {
                            qu2 = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + from_date + "' and LeaveTo<='" + todate + "' and ReqAppNo='" + appno + "' and ReqAppStatus='0' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + ddlcollege.SelectedItem.Value + "'";
                            dt_monthleave_count = d2.select_method_wo_parameter(qu2, "text");
                        }
                        string queryy = string.Empty;
                        DataSet dt_yearleave_count = new DataSet();
                        if (ds_yearSelect.Tables.Count > 0 && ds_yearSelect.Tables[0].Rows.Count > 0)
                        {
                            queryy = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + from_dateS + "' and LeaveTo<='" + todateS + "' and ReqAppNo='" + appno + "' and( ReqAppStatus='1' or ReqAppStatus='0') and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + collegecode + "' ";
                            dt_yearleave_count = d2.select_method_wo_parameter(queryy, "text");
                        }
                        #endregion

                        string leavefromdate = string.Empty;
                        string leavetodate = string.Empty;
                        string ishalfdate = string.Empty;
                        string halfdaydate = string.Empty;
                        int finaldate11 = 0;
                        double tot_leave = 0;
                        string yearleavefromdate = string.Empty;
                        string yearleavetodate = string.Empty;
                        string yearishalfdate = string.Empty;
                        string yearhalfdaydate = string.Empty;
                        string requestpk = string.Empty;
                        int yearfinaldate11 = 0;
                        double yeartot_leave = 0;
                        if (dt_monthleave_count.Tables.Count > 0 && dt_monthleave_count.Tables[0].Rows.Count > 0)
                        {
                            #region leave count check
                            for (int k = 0; k < dt_monthleave_count.Tables[0].Rows.Count; k++)
                            {
                                leavefromdate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["LeaveFrom"]);
                                leavetodate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["LeaveTo"]);
                                ishalfdate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["IsHalfDay"]);
                                requestpk = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["RequisitionPK"]);
                                if (!string.IsNullOrEmpty(leavefromdate) && !string.IsNullOrEmpty(leavetodate))
                                {
                                    string dtT = leavefromdate; // Add Holiday include and Sunday Include 
                                    string[] Split = dtT.Split('/');
                                    string enddt = leavetodate;
                                    Split = enddt.Split('/');
                                    DateTime fromdatenew = Convert.ToDateTime(dtT);
                                    DateTime todatenew = Convert.ToDateTime(enddt);
                                    TimeSpan days = todatenew - fromdatenew;
                                    string ndate = Convert.ToString(days);
                                    Split = ndate.Split('.');
                                    string getdate = Split[0];
                                    string sunday = fromdatenew.ToString("dddd");
                                    for (; fromdatenew <= todatenew; )
                                    {

                                        sunday = fromdatenew.ToString("dddd");
                                        if (!HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")))
                                        {
                                            if (sunday != "Sunday")
                                            {
                                                ccc++;

                                                string checkval = "select * from staff_leave_dates where requestfk='" + requestpk + "' and LeaveDate='" + fromdatenew + "'";
                                                DataSet checkdel = new DataSet();
                                                checkdel = d2.select_method_wo_parameter(checkval, "text");
                                                if (checkdel.Tables[0].Rows.Count > 0)
                                                {
                                                    ishalfdate = "False";
                                                    string approvalcheck = Convert.ToString(checkdel.Tables[0].Rows[0]["isApproved"]);
                                                    string session = Convert.ToString(checkdel.Tables[0].Rows[0]["SessionType"]);
                                                    string delecheck = Convert.ToString(checkdel.Tables[0].Rows[0]["checkdel"]);
                                                    if (approvalcheck == "1")
                                                    {
                                                        if (delecheck == "1")
                                                        {
                                                            if (session == "0")
                                                            {
                                                                ccc--;

                                                            }
                                                            else if (session == "1" || session == "2")
                                                            {

                                                                ccc = ccc - 0.5;

                                                            }


                                                        }

                                                    }


                                                }
                                                tot_leave = tot_leave + Convert.ToDouble(ccc);
                                                taken_leave = tot_leave;
                                            }
                                            //  else if (sunday == "Sunday" && Sundayinclude == 1)
                                            else if (sunday == "Sunday" && Sundayinclude == 0)
                                            {
                                                ccc++;
                                                tot_leave = tot_leave + Convert.ToDouble(ccc);
                                                taken_leave = tot_leave;
                                            }
                                        }
                                        else if (HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                        {
                                            string Getkeyvalue = Convert.ToString(HolidayHash[fromdatenew.ToString("MM/dd/yyyy")]);
                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                            {
                                                ccc += 0.5;
                                                tot_leave = tot_leave + Convert.ToDouble(ccc);
                                                taken_leave = tot_leave;
                                            }
                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                            {
                                                ccc++;
                                                tot_leave = tot_leave + Convert.ToDouble(ccc);
                                                taken_leave = tot_leave;
                                            }
                                        }
                                        fromdatenew = fromdatenew.AddDays(1);
                                    }
                                    if (ishalfdate == "True")
                                    {
                                        halfdaydate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["HalfDate"]);
                                        ccc = Convert.ToDouble(ccc) - 0.5;
                                    }
                                }
                            }
                            #endregion
                        }
                        taken_leave = ccc;
                        double all_leave = totleaveday + taken_leave;
                        if (yrLeaveCount < all_leave)//delsi2206
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "no leave available.";
                            alert = true;
                            return;
                        }
                        //delsi2106
                        if (dt_yearleave_count.Tables.Count > 0 && dt_yearleave_count.Tables[0].Rows.Count > 0)
                        {
                            #region year leave count check
                            for (int yr = 0; yr < dt_yearleave_count.Tables[0].Rows.Count; yr++)
                            {
                                yearleavefromdate = Convert.ToString(dt_yearleave_count.Tables[0].Rows[yr]["LeaveFrom"]);
                                yearleavetodate = Convert.ToString(dt_yearleave_count.Tables[0].Rows[yr]["LeaveTo"]);
                                yearishalfdate = Convert.ToString(dt_yearleave_count.Tables[0].Rows[yr]["IsHalfDay"]);
                                if (!string.IsNullOrEmpty(yearleavefromdate) && !string.IsNullOrEmpty(yearleavetodate))
                                {
                                    string dtT = yearleavefromdate; // Add Holiday include and Sunday Include 
                                    string[] Split = dtT.Split('/');
                                    string enddt = yearleavetodate;
                                    Split = enddt.Split('/');
                                    DateTime fromdatenew = Convert.ToDateTime(dtT);
                                    DateTime todatenew = Convert.ToDateTime(enddt);
                                    TimeSpan days = todatenew - fromdatenew;
                                    string ndate = Convert.ToString(days);
                                    Split = ndate.Split('.');
                                    string getdate = Split[0];
                                    string sunday = fromdatenew.ToString("dddd");
                                    for (; fromdatenew <= todatenew; )
                                    {
                                        sunday = fromdatenew.ToString("dddd");
                                        if (!HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")))
                                        {
                                            if (sunday != "Sunday")
                                            {
                                                cccc++;
                                                yeartot_leave = yeartot_leave + Convert.ToDouble(cccc);
                                                yeartaken_leave = yeartot_leave;
                                            }
                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                            {
                                                cccc++;
                                                yeartot_leave = yeartot_leave + Convert.ToDouble(cccc);
                                                yeartaken_leave = yeartot_leave;
                                            }
                                        }
                                        else if (HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                        {
                                            string Getkeyvalue = Convert.ToString(HolidayHash[fromdatenew.ToString("MM/dd/yyyy")]);
                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                            {
                                                cccc += 0.5;
                                                yeartot_leave = yeartot_leave + Convert.ToDouble(cccc);
                                                yeartaken_leave = yeartot_leave;
                                            }
                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                            {
                                                cccc++;
                                                yeartot_leave = yeartot_leave + Convert.ToDouble(cccc);
                                                yeartaken_leave = yeartot_leave;
                                            }
                                        }
                                        fromdatenew = fromdatenew.AddDays(1);
                                    }
                                    if (yearishalfdate == "True")
                                    {
                                        yearhalfdaydate = Convert.ToString(dt_yearleave_count.Tables[0].Rows[yr]["HalfDate"]);
                                        cccc = Convert.ToInt32(Convert.ToDouble(cccc) - 0.5);
                                    }
                                }
                            }
                            #endregion
                        }
                        yeartaken_leave = cccc;
                        double previousLeaveAvailable = 0;
                        previousLeaveAvailable = totalyearleave - yeartaken_leave;
                        double pre_leave = previousLeaveAvailable;
                        //  double yearall_leave = totleaveday + yeartaken_leave + taken_leave;//yearly taken include month taken include applies leave
                        //delsi 2106
                        if (all_leave <= yrLeaveCount)
                        {
                            #region monthwise leave allot
                            if (mnthLeaveCount != 0) //all_leave > mnthLeaveCount &&
                            {
                                if (mnthCaryLeaveCount == 1 && all_leave > yrLeaveCount)
                                {
                                    double difference = mnthLeaveCount - taken_leave;
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "Leave only available for " + difference.ToString() + " day(s).";
                                    alert = true;
                                    return;
                                }
                            }
                            #endregion

                            if (mnthCaryLeaveCount == 0 && yearlyCaryoverLeaveCount == 1) // Modify by jairam 19-07-2017 SVS College
                            {
                                #region  yearly Carry over Leave Check
                                string sql_query = "select * from hrpaymonths where college_code='" + collegecode + "' and from_date <='" + fromdate1 + "' and selstatus='1'";
                                DataSet dt_months = d2.select_method_wo_parameter(sql_query, "Text");
                                if (dt_months.Tables.Count > 0 && dt_months.Tables[0].Rows.Count > 0)
                                {
                                    tot_leave = 0;
                                    //totleaveday = 0;
                                    for (int s = 0; s < dt_months.Tables[0].Rows.Count; s++)
                                    {
                                        double monthly_total = 0;
                                        double tempCnt = 0;
                                        double.TryParse(Convert.ToString(dt_months.Tables[0].Rows.Count), out tempCnt);
                                        monthly_total = mnthLeaveCount * tempCnt;
                                        string SQlQuery = string.Empty;
                                        SQlQuery = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + dt_months.Tables[0].Rows[s]["From_date"].ToString() + "' and LeaveTo<='" + dt_months.Tables[0].Rows[s]["to_date"].ToString() + "' and ReqAppNo='" + appno + "' and( ReqAppStatus='1' or ReqAppStatus='0') and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + collegecode + "' ";
                                        DataSet dt_count = d2.select_method_wo_parameter(SQlQuery, "Text");
                                        if (dt_count.Tables.Count > 0 && dt_count.Tables[0].Rows.Count > 0)
                                        {
                                            for (int k = 0; k < dt_count.Tables[0].Rows.Count; k++)
                                            {
                                                leavefromdate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveFrom"]);
                                                leavetodate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveTo"]);
                                                ishalfdate = Convert.ToString(dt_count.Tables[0].Rows[k]["IsHalfDay"]);
                                                if (leavefromdate != "" && leavetodate != "")
                                                {
                                                    string dtT = leavefromdate;
                                                    string[] Split = dtT.Split('/');
                                                    string enddt = leavetodate;
                                                    Split = enddt.Split('/');
                                                    DateTime fromdatenew = Convert.ToDateTime(dtT);
                                                    DateTime todatenew = Convert.ToDateTime(enddt);
                                                    TimeSpan days = fromdatenew - todatenew;
                                                    string ndate = Convert.ToString(days);
                                                    Split = ndate.Split('.');
                                                    string getdate = Split[0];
                                                    finaldate11 = 0;
                                                    string sunday = fromdatenew.ToString("dddd");
                                                    for (; fromdatenew <= todatenew; )
                                                    {
                                                        sunday = fromdatenew.ToString("dddd");
                                                        if (!HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")))
                                                        {
                                                            if (sunday != "Sunday")
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                        }
                                                        else if (HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                                        {
                                                            string Getkeyvalue = Convert.ToString(HolidayHash[fromdatenew.ToString("MM/dd/yyyy")]);
                                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                                            {
                                                                tot_leave += 0.5;
                                                            }
                                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                        }
                                                        fromdatenew = fromdatenew.AddDays(1);
                                                    }
                                                    if (ishalfdate == "True")
                                                    {
                                                        halfdaydate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["HalfDate"]);
                                                        tot_leave -= 0.5;
                                                    }
                                                }
                                                // previousLeaveAvailable = previousLeaveAvailable - (tot_leave - mnthLeaveCount);
                                                monthly_total = monthly_total + previousLeaveAvailable;
                                            }
                                            totleaveday += tot_leave;
                                            tot_leave = 0;
                                            if (yrLeaveCount >= totleaveday)//barath 17.01.18
                                            {
                                                if (yearlyCaryoverLeaveCount != 0 && mnthLeaveCount != 0)
                                                {

                                                    if (totleaveday > monthly_total)
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "No leave available.";
                                                        alert = true;
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                imgdiv2.Visible = true;
                                                lbl_alert.Text = "No leave available.";
                                                alert = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (dt_months.Tables.Count > 0 && dt_months.Tables[0].Rows.Count > 0)//delsi2206
                                            {
                                                double monthly_totals = 0;
                                                double tempCnts = 0;
                                                double.TryParse(Convert.ToString(dt_months.Tables[0].Rows.Count), out tempCnts);
                                                monthly_totals = mnthLeaveCount * tempCnt;
                                                monthly_totals = monthly_totals + previousLeaveAvailable;

                                                if (yrLeaveCount >= totleaveday)
                                                {
                                                    if (yearlyCaryoverLeaveCount != 0)
                                                    {
                                                        if (totleaveday > monthly_totals)
                                                        {
                                                            imgdiv2.Visible = true;
                                                            lbl_alert.Text = "No leave available.";
                                                            alert = true;
                                                            return;
                                                        }
                                                    }
                                                }

                                            }


                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (mnthLeaveCount != 0 && yearlyCaryoverLeaveCount != 1 && mnthCaryLeaveCount != 1)//doubt//modifided by saranya on 27/8/2018 mnthCaryLeaveCount != 0 changed tomnthCaryLeaveCount != 1
                            {
                                #region MonthWise

                                DataSet dshrm = dshrMonthDate(Convert.ToDateTime(fromdate1), Convert.ToDateTime(todate1));
                                if (dshrm.Tables.Count > 0 && dshrm.Tables[0].Rows.Count > 0)
                                {
                                    string AlreadyRequestDateDetails = string.Empty;
                                    for (int dvrow = 0; dvrow < dshrm.Tables[0].Rows.Count; dvrow++)
                                    {
                                        string tempfrdt = Convert.ToString(dshrm.Tables[0].Rows[dvrow]["from_date"]);
                                        string temptodt = Convert.ToString(dshrm.Tables[0].Rows[dvrow]["to_date"]);
                                        dt_monthleave_count.Tables[0].DefaultView.RowFilter = "(LeaveFrom>=#" + Convert.ToDateTime(tempfrdt).ToString("MM/dd/yyyy") + "# and LeaveTo<=#" + Convert.ToDateTime(temptodt).ToString("MM/dd/yyyy") + "# )";
                                        DataView dvlv = dt_monthleave_count.Tables[0].DefaultView;
                                        double temptotleaveday = 0;
                                        double.TryParse(Convert.ToString(dtLveCnt[tempfrdt + "-" + temptodt]), out temptotleaveday);
                                        if (dt_monthleave_count.Tables[0].Rows.Count > 0)
                                        {
                                            if (dvlv.Count > 0)
                                            {
                                                double oldlveCnt = 0;
                                                for (int rval = 0; rval < dvlv.Count; rval++)
                                                {
                                                    DateTime dtfr;
                                                    DateTime dtto;
                                                    DateTime.TryParse(Convert.ToString(dvlv[rval]["LeaveFrom"]), out dtfr);
                                                    DateTime.TryParse(Convert.ToString(dvlv[rval]["LeaveTo"]), out dtto);
                                                    TimeSpan totalDays = dtto - dtfr;
                                                    double oldcnt = 0;
                                                    for (; dtfr <= dtto; )
                                                    {
                                                        string sunday = dtfr.ToString("dddd");
                                                        if (!HolidayHash.ContainsKey(dtfr.ToString("MM/dd/yyyy")))
                                                        {
                                                            if (sunday != "Sunday")
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                        }
                                                        else if (HolidayHash.ContainsKey(dtfr.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                                        {
                                                            string Getkeyvalue = Convert.ToString(HolidayHash[dtfr.ToString("MM/dd/yyyy")]);
                                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                                            {
                                                                oldcnt += 0.5;
                                                            }
                                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                        }
                                                        string dayofabsent = string.Empty;//barath 29.01.18
                                                        if (ishalfdate == "True")
                                                            dayofabsent = " Half Day";
                                                        else
                                                            dayofabsent = " Full Day";
                                                        if (Convert.ToString(dvlv[rval]["ReqAppStatus"]) == "0" || Convert.ToString(dvlv[rval]["ReqAppStatus"]) == "False")
                                                            AlreadyRequestDateDetails += " " + dtfr.ToString("dd/MM/yyyy") + " - " + dayofabsent;
                                                        dtfr = dtfr.AddDays(1);
                                                    }
                                                    if (ishalfdate == "True")
                                                    {
                                                        halfdaydate = Convert.ToString(dvlv[rval]["HalfDate"]);
                                                        oldcnt -= 0.5;
                                                    }
                                                    //double.TryParse(Convert.ToString(totalDays.TotalDays), out oldcnt);
                                                    oldlveCnt += oldcnt;
                                                    //   double.TryParse(Convert.ToString(dvlv.Count), out oldlveCnt);                                                                
                                                }
                                                oldlveCnt += temptotleaveday;
                                                if (mnthLeaveCount < oldlveCnt)
                                                {
                                                    if (!string.IsNullOrEmpty(AlreadyRequestDateDetails))//barath 29.01.18
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "Already Applied " + AlreadyRequestDateDetails;
                                                        alert = true;
                                                    }
                                                    else
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "No leave available in this month";
                                                        alert = true;
                                                    }
                                                    return;
                                                }
                                            }
                                            else
                                                datasetEmpty(mnthLeaveCount, temptotleaveday, yrLeaveCount);
                                        }
                                        else
                                            datasetEmpty(mnthLeaveCount, temptotleaveday, yrLeaveCount);
                                    }
                                }
                                #endregion
                            }

                            if (mnthCaryLeaveCount == 1 && yearlyCaryoverLeaveCount == 1) // Modify by jairam 19-07-2017 SVS College
                            {
                                #region Month and yearly Carry over Leave Check
                                string sql_query = "select * from hrpaymonths where college_code='" + collegecode + "' and from_date <='" + fromdate1 + "' and selstatus='1'";
                                DataSet dt_months = d2.select_method_wo_parameter(sql_query, "Text");
                                if (dt_months.Tables.Count > 0 && dt_months.Tables[0].Rows.Count > 0)
                                {
                                    tot_leave = 0;
                                    //totleaveday = 0;
                                    for (int s = 0; s < dt_months.Tables[0].Rows.Count; s++)
                                    {
                                        double monthly_total = 0;
                                        double tempCnt = 0;
                                        double.TryParse(Convert.ToString(dt_months.Tables[0].Rows.Count), out tempCnt);
                                        monthly_total = mnthLeaveCount * tempCnt;
                                        string SQlQuery = string.Empty;
                                        SQlQuery = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + dt_months.Tables[0].Rows[s]["From_date"].ToString() + "' and LeaveTo<='" + dt_months.Tables[0].Rows[s]["to_date"].ToString() + "' and ReqAppNo='" + appno + "' and( ReqAppStatus='1' or ReqAppStatus='0') and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + collegecode + "' ";
                                        DataSet dt_count = d2.select_method_wo_parameter(SQlQuery, "Text");
                                        if (dt_count.Tables.Count > 0 && dt_count.Tables[0].Rows.Count > 0)
                                        {
                                            for (int k = 0; k < dt_count.Tables[0].Rows.Count; k++)
                                            {
                                                leavefromdate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveFrom"]);
                                                leavetodate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveTo"]);
                                                ishalfdate = Convert.ToString(dt_count.Tables[0].Rows[k]["IsHalfDay"]);
                                                if (leavefromdate != "" && leavetodate != "")
                                                {
                                                    string dtT = leavefromdate;
                                                    string[] Split = dtT.Split('/');
                                                    string enddt = leavetodate;
                                                    Split = enddt.Split('/');
                                                    DateTime fromdatenew = Convert.ToDateTime(dtT);
                                                    DateTime todatenew = Convert.ToDateTime(enddt);
                                                    TimeSpan days = fromdatenew - todatenew;
                                                    string ndate = Convert.ToString(days);
                                                    Split = ndate.Split('.');
                                                    string getdate = Split[0];
                                                    finaldate11 = 0;
                                                    string sunday = fromdatenew.ToString("dddd");
                                                    for (; fromdatenew <= todatenew; )
                                                    {
                                                        sunday = fromdatenew.ToString("dddd");
                                                        if (!HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")))
                                                        {
                                                            if (sunday != "Sunday")
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                        }
                                                        else if (HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                                        {
                                                            string Getkeyvalue = Convert.ToString(HolidayHash[fromdatenew.ToString("MM/dd/yyyy")]);
                                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                                            {
                                                                tot_leave += 0.5;
                                                            }
                                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                        }
                                                        fromdatenew = fromdatenew.AddDays(1);
                                                    }
                                                    if (ishalfdate == "True")
                                                    {
                                                        halfdaydate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["HalfDate"]);
                                                        tot_leave -= 0.5;
                                                    }
                                                }
                                                // previousLeaveAvailable = previousLeaveAvailable - (tot_leave - mnthLeaveCount);
                                                monthly_total = monthly_total + previousLeaveAvailable;
                                            }
                                            totleaveday += tot_leave;
                                            tot_leave = 0;
                                            if (yrLeaveCount >= totleaveday)//barath 17.01.18
                                            {
                                                if (yearlyCaryoverLeaveCount != 0 && mnthLeaveCount != 0)
                                                {

                                                    if (totleaveday > monthly_total)
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "No leave available.";
                                                        alert = true;
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                imgdiv2.Visible = true;
                                                lbl_alert.Text = "No leave available.";
                                                alert = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (dt_months.Tables.Count > 0 && dt_months.Tables[0].Rows.Count > 0)//delsi2206
                                            {
                                                double monthly_totals = 0;
                                                double tempCnts = 0;
                                                double.TryParse(Convert.ToString(dt_months.Tables[0].Rows.Count), out tempCnts);
                                                monthly_totals = mnthLeaveCount * tempCnt;
                                                monthly_totals = monthly_totals + previousLeaveAvailable;

                                                if (yrLeaveCount >= totleaveday)
                                                {
                                                    if (yearlyCaryoverLeaveCount != 0)
                                                    {
                                                        if (totleaveday > monthly_totals)
                                                        {
                                                            imgdiv2.Visible = true;
                                                            lbl_alert.Text = "No leave available.";
                                                            alert = true;
                                                            return;
                                                        }
                                                    }
                                                }

                                            }


                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (mnthLeaveCount != 0 && yearlyCaryoverLeaveCount != 1 && mnthCaryLeaveCount != 1)//doubt
                            {
                                #region MonthWise
                                DataSet dshrm = dshrMonthDate(Convert.ToDateTime(fromdate1), Convert.ToDateTime(todate1));
                                if (dshrm.Tables.Count > 0 && dshrm.Tables[0].Rows.Count > 0)
                                {
                                    string AlreadyRequestDateDetails = string.Empty;
                                    for (int dvrow = 0; dvrow < dshrm.Tables[0].Rows.Count; dvrow++)
                                    {
                                        string tempfrdt = Convert.ToString(dshrm.Tables[0].Rows[dvrow]["from_date"]);
                                        string temptodt = Convert.ToString(dshrm.Tables[0].Rows[dvrow]["to_date"]);
                                        dt_monthleave_count.Tables[0].DefaultView.RowFilter = "(LeaveFrom>=#" + Convert.ToDateTime(tempfrdt).ToString("MM/dd/yyyy") + "# and LeaveTo<=#" + Convert.ToDateTime(temptodt).ToString("MM/dd/yyyy") + "# )";
                                        DataView dvlv = dt_monthleave_count.Tables[0].DefaultView;
                                        double temptotleaveday = 0;
                                        double.TryParse(Convert.ToString(dtLveCnt[tempfrdt + "-" + temptodt]), out temptotleaveday);
                                        if (dt_monthleave_count.Tables[0].Rows.Count > 0)
                                        {
                                            if (dvlv.Count > 0)
                                            {
                                                double oldlveCnt = 0;
                                                for (int rval = 0; rval < dvlv.Count; rval++)
                                                {
                                                    DateTime dtfr;
                                                    DateTime dtto;
                                                    DateTime.TryParse(Convert.ToString(dvlv[rval]["LeaveFrom"]), out dtfr);
                                                    DateTime.TryParse(Convert.ToString(dvlv[rval]["LeaveTo"]), out dtto);
                                                    TimeSpan totalDays = dtto - dtfr;
                                                    double oldcnt = 0;
                                                    for (; dtfr <= dtto; )
                                                    {
                                                        string sunday = dtfr.ToString("dddd");
                                                        if (!HolidayHash.ContainsKey(dtfr.ToString("MM/dd/yyyy")))
                                                        {
                                                            if (sunday != "Sunday")
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                        }
                                                        else if (HolidayHash.ContainsKey(dtfr.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                                        {
                                                            string Getkeyvalue = Convert.ToString(HolidayHash[dtfr.ToString("MM/dd/yyyy")]);
                                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                                            {
                                                                oldcnt += 0.5;
                                                            }
                                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                        }
                                                        string dayofabsent = string.Empty;//barath 29.01.18
                                                        if (ishalfdate == "True")
                                                            dayofabsent = " Half Day";
                                                        else
                                                            dayofabsent = " Full Day";
                                                        if (Convert.ToString(dvlv[rval]["ReqAppStatus"]) == "0" || Convert.ToString(dvlv[rval]["ReqAppStatus"]) == "False")
                                                            AlreadyRequestDateDetails += " " + dtfr.ToString("dd/MM/yyyy") + " - " + dayofabsent;
                                                        dtfr = dtfr.AddDays(1);
                                                    }
                                                    if (ishalfdate == "True")
                                                    {
                                                        halfdaydate = Convert.ToString(dvlv[rval]["HalfDate"]);
                                                        oldcnt -= 0.5;
                                                    }
                                                    //double.TryParse(Convert.ToString(totalDays.TotalDays), out oldcnt);
                                                    oldlveCnt += oldcnt;
                                                    //   double.TryParse(Convert.ToString(dvlv.Count), out oldlveCnt);                                                                
                                                }
                                                oldlveCnt += temptotleaveday;
                                                if (mnthLeaveCount < oldlveCnt)
                                                {
                                                    if (!string.IsNullOrEmpty(AlreadyRequestDateDetails))//barath 29.01.18
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "Already Applied " + AlreadyRequestDateDetails;
                                                        alert = true;
                                                    }
                                                    else
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "No leave available in this month";
                                                        alert = true;
                                                    }
                                                    return;
                                                }
                                            }
                                            else
                                                datasetEmpty(mnthLeaveCount, temptotleaveday, yrLeaveCount);
                                        }
                                        else
                                            datasetEmpty(mnthLeaveCount, temptotleaveday, yrLeaveCount);
                                    }
                                }
                                #endregion
                            }

                            if (mnthCaryLeaveCount == 1 && yearlyCaryoverLeaveCount != 1) // Modify by jairam 19-07-2017 SVS College
                            {
                                #region Month Carry over Leave Check
                                string sql_query = "select * from hrpaymonths where college_code='" + collegecode + "' and from_date <='" + fromdate1 + "' and selstatus='1'";
                                DataSet dt_months = d2.select_method_wo_parameter(sql_query, "Text");
                                if (dt_months.Tables.Count > 0 && dt_months.Tables[0].Rows.Count > 0)
                                {
                                    tot_leave = 0;
                                    //totleaveday = 0;
                                    for (int s = 0; s < dt_months.Tables[0].Rows.Count; s++)
                                    {
                                        double monthly_total = 0;
                                        double tempCnt = 0;
                                        double.TryParse(Convert.ToString(dt_months.Tables[0].Rows.Count), out tempCnt);
                                        monthly_total = mnthLeaveCount * tempCnt;
                                        string SQlQuery = string.Empty;
                                        SQlQuery = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + dt_months.Tables[0].Rows[s]["From_date"].ToString() + "' and LeaveTo<='" + dt_months.Tables[0].Rows[s]["to_date"].ToString() + "' and ReqAppNo='" + appno + "' and( ReqAppStatus='1' or ReqAppStatus='0') and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + collegecode + "' ";
                                        DataSet dt_count = d2.select_method_wo_parameter(SQlQuery, "Text");
                                        if (dt_count.Tables.Count > 0 && dt_count.Tables[0].Rows.Count > 0)
                                        {
                                            for (int k = 0; k < dt_count.Tables[0].Rows.Count; k++)
                                            {
                                                leavefromdate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveFrom"]);
                                                leavetodate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveTo"]);
                                                ishalfdate = Convert.ToString(dt_count.Tables[0].Rows[k]["IsHalfDay"]);
                                                if (leavefromdate != "" && leavetodate != "")
                                                {
                                                    string dtT = leavefromdate;
                                                    string[] Split = dtT.Split('/');
                                                    string enddt = leavetodate;
                                                    Split = enddt.Split('/');
                                                    DateTime fromdatenew = Convert.ToDateTime(dtT);
                                                    DateTime todatenew = Convert.ToDateTime(enddt);
                                                    TimeSpan days = fromdatenew - todatenew;
                                                    string ndate = Convert.ToString(days);
                                                    Split = ndate.Split('.');
                                                    string getdate = Split[0];
                                                    finaldate11 = 0;
                                                    string sunday = fromdatenew.ToString("dddd");
                                                    for (; fromdatenew <= todatenew; )
                                                    {
                                                        sunday = fromdatenew.ToString("dddd");
                                                        if (!HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")))
                                                        {
                                                            if (sunday != "Sunday")
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                        }
                                                        else if (HolidayHash.ContainsKey(fromdatenew.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                                        {
                                                            string Getkeyvalue = Convert.ToString(HolidayHash[fromdatenew.ToString("MM/dd/yyyy")]);
                                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                                            {
                                                                tot_leave += 0.5;
                                                            }
                                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                                            {
                                                                tot_leave += 1;
                                                            }
                                                        }
                                                        fromdatenew = fromdatenew.AddDays(1);
                                                    }
                                                    if (ishalfdate == "True")
                                                    {
                                                        halfdaydate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["HalfDate"]);
                                                        tot_leave -= 0.5;
                                                    }
                                                }
                                            }
                                            totleaveday += tot_leave;
                                            tot_leave = 0;
                                            if (yrLeaveCount >= totleaveday)//barath 17.01.18
                                            {
                                                if (mnthLeaveCount != 0)
                                                {
                                                    if (totleaveday > monthly_total)
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "No leave available.";
                                                        alert = true;
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                imgdiv2.Visible = true;
                                                lbl_alert.Text = "No leave available.";
                                                alert = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (dt_months.Tables.Count > 0 && dt_months.Tables[0].Rows.Count > 0)//delsi2206
                                            {
                                                double monthly_totals = 0;
                                                double tempCnts = 0;
                                                double.TryParse(Convert.ToString(dt_months.Tables[0].Rows.Count), out tempCnts);
                                                monthly_totals = mnthLeaveCount * tempCnt;

                                                if (yrLeaveCount >= totleaveday)//barath 17.01.18
                                                {
                                                    if (mnthLeaveCount != 0)
                                                    {
                                                        if (totleaveday > monthly_totals)
                                                        {
                                                            imgdiv2.Visible = true;
                                                            lbl_alert.Text = "No leave available.";
                                                            alert = true;
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion


                            }
                            else if (mnthLeaveCount != 0 && yearlyCaryoverLeaveCount != 1 && mnthCaryLeaveCount == 1)
                            {
                                #region MonthWise
                                DataSet dshrm = dshrMonthDate(Convert.ToDateTime(fromdate1), Convert.ToDateTime(todate1));
                                if (dshrm.Tables.Count > 0 && dshrm.Tables[0].Rows.Count > 0)
                                {
                                    string AlreadyRequestDateDetails = string.Empty;
                                    for (int dvrow = 0; dvrow < dshrm.Tables[0].Rows.Count; dvrow++)
                                    {
                                        string tempfrdt = Convert.ToString(dshrm.Tables[0].Rows[dvrow]["from_date"]);
                                        string temptodt = Convert.ToString(dshrm.Tables[0].Rows[dvrow]["to_date"]);
                                        dt_monthleave_count.Tables[0].DefaultView.RowFilter = "(LeaveFrom>=#" + Convert.ToDateTime(tempfrdt).ToString("MM/dd/yyyy") + "# and LeaveTo<=#" + Convert.ToDateTime(temptodt).ToString("MM/dd/yyyy") + "# )";
                                        DataView dvlv = dt_monthleave_count.Tables[0].DefaultView;
                                        double temptotleaveday = 0;
                                        double.TryParse(Convert.ToString(dtLveCnt[tempfrdt + "-" + temptodt]), out temptotleaveday);
                                        if (dt_monthleave_count.Tables[0].Rows.Count > 0)
                                        {
                                            if (dvlv.Count > 0)
                                            {
                                                double oldlveCnt = 0;
                                                for (int rval = 0; rval < dvlv.Count; rval++)
                                                {
                                                    DateTime dtfr;
                                                    DateTime dtto;
                                                    DateTime.TryParse(Convert.ToString(dvlv[rval]["LeaveFrom"]), out dtfr);
                                                    DateTime.TryParse(Convert.ToString(dvlv[rval]["LeaveTo"]), out dtto);
                                                    TimeSpan totalDays = dtto - dtfr;
                                                    double oldcnt = 0;
                                                    for (; dtfr <= dtto; )
                                                    {
                                                        string sunday = dtfr.ToString("dddd");
                                                        if (!HolidayHash.ContainsKey(dtfr.ToString("MM/dd/yyyy")))
                                                        {
                                                            if (sunday != "Sunday")
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                            else if (sunday == "Sunday" && Sundayinclude == 1)
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                        }
                                                        else if (HolidayHash.ContainsKey(dtfr.ToString("MM/dd/yyyy")) && HolidayInclude == 1)
                                                        {
                                                            string Getkeyvalue = Convert.ToString(HolidayHash[dtfr.ToString("MM/dd/yyyy")]);
                                                            if (Getkeyvalue.Trim().Split('-')[0] == "True")
                                                            {
                                                                oldcnt += 0.5;
                                                            }
                                                            else if (Getkeyvalue.Trim().Split('-')[0] == "False")
                                                            {
                                                                oldcnt += 1;
                                                            }
                                                        }
                                                        string dayofabsent = string.Empty;//barath 29.01.18
                                                        if (ishalfdate == "True")
                                                            dayofabsent = " Half Day";
                                                        else
                                                            dayofabsent = " Full Day";
                                                        if (Convert.ToString(dvlv[rval]["ReqAppStatus"]) == "0" || Convert.ToString(dvlv[rval]["ReqAppStatus"]) == "False")
                                                            AlreadyRequestDateDetails += " " + dtfr.ToString("dd/MM/yyyy") + " - " + dayofabsent;
                                                        dtfr = dtfr.AddDays(1);
                                                    }
                                                    if (ishalfdate == "True")
                                                    {
                                                        halfdaydate = Convert.ToString(dvlv[rval]["HalfDate"]);
                                                        oldcnt -= 0.5;
                                                    }
                                                    //double.TryParse(Convert.ToString(totalDays.TotalDays), out oldcnt);
                                                    oldlveCnt += oldcnt;
                                                    //   double.TryParse(Convert.ToString(dvlv.Count), out oldlveCnt);                                                                
                                                }
                                                oldlveCnt += temptotleaveday;
                                                if (mnthLeaveCount < oldlveCnt)
                                                {
                                                    if (!string.IsNullOrEmpty(AlreadyRequestDateDetails))//barath 29.01.18
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "Already Applied " + AlreadyRequestDateDetails;
                                                        alert = true;
                                                    }
                                                    else
                                                    {
                                                        imgdiv2.Visible = true;
                                                        lbl_alert.Text = "No leave available in this month";
                                                        alert = true;
                                                    }
                                                    return;
                                                }
                                            }
                                            else
                                                datasetEmpty(mnthLeaveCount, temptotleaveday, yrLeaveCount);
                                        }
                                        else
                                            datasetEmpty(mnthLeaveCount, temptotleaveday, yrLeaveCount);
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region Yearly Wise
                                if (all_leave > yrLeaveCount)
                                {
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "No leave available in current year";
                                    alert = true;
                                    return;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "No leave available in current year";
                            alert = true;
                            return;
                        }
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "No long leave available in current year for " + totleaveday + " day(s).";
                    alert = true;
                    return;
                }
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "No leave available in current year.";
                alert = true;
                return;
            }
            //        }
            //    }
            //}
            //else
            //{
            //    imgdiv2.Visible = true;
            //    lbl_alert.Text = "Can't Apply This LeaveType, Leave Details not Entered";
            //    alert = true;
            //    return;
            //}
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
            return;
        }
    }

    protected DataSet validateHrYear(string collegecode, DateTime fromdate1, DateTime todate1, ref bool checkbol)
    {
        string qur1 = "select * from hrpaymonths where College_Code='" + collegecode + "' and SelStatus='1'";
        Dictionary<string, string> hrDate = getHrDate(collegecode);
        DataSet dt_paymonths = d2.select_method_wo_parameter(qur1, "text");
        if (dt_paymonths.Tables.Count > 0 && dt_paymonths.Tables[0].Rows.Count > 0)
        {
            DateTime from_date = Convert.ToDateTime(dt_paymonths.Tables[0].Rows[0]["from_date"]);
            DateTime todate = Convert.ToDateTime(dt_paymonths.Tables[0].Rows[dt_paymonths.Tables[0].Rows.Count - 1]["to_date"]);
            if (from_date > fromdate1 || todate < fromdate1 || from_date > todate1 || todate < todate1)
                checkbol = true;
        }
        return dt_paymonths;
    }

    protected DataSet dshrMonthDate(DateTime from, DateTime to)
    {
        string selQ = " select * from hrpaymonths where ( '" + from + "' between  from_date  and  to_date  or '" + to + "' between  from_date  and  to_date) and college_code='" + Session["collegecode"] + "'";//delsi1212
        DataSet dshrmn = d2.select_method_wo_parameter(selQ, "Text");
        return dshrmn;
    }

    protected void datasetEmpty(double mnthLeaveCount, double taken_leave, double yrLeaveCount)
    {
        if (mnthLeaveCount != 0 && taken_leave > mnthLeaveCount)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No leave available in this month";
            alert = true;
            return;
        }
        if (taken_leave > yrLeaveCount)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No leave available in this month";
            alert = true;
            return;
        }
    }

    protected Dictionary<string, string> getHrDate(string collegecode)
    {
        Dictionary<string, string> hrDate = new Dictionary<string, string>();
        string SelQ = " select paymonthnum,payyear,convert(varchar(20),from_date,103) as from_date,convert(varchar(20),to_date,103) as to_date from hrpaymonths where College_Code='" + collegecode + "' and SelStatus='1'";
        DataSet dshr = d2.select_method_wo_parameter(SelQ, "Text");
        if (dshr.Tables.Count > 0 && dshr.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < dshr.Tables[0].Rows.Count; row++)
            {
                string mnth = Convert.ToString(dshr.Tables[0].Rows[row]["paymonthnum"]);
                string year = Convert.ToString(dshr.Tables[0].Rows[row]["payyear"]);
                string fromYear = Convert.ToString(dshr.Tables[0].Rows[row]["from_date"]);
                string toYear = Convert.ToString(dshr.Tables[0].Rows[row]["to_date"]);
                string fnlStr = mnth + "-" + year;
                if (!hrDate.ContainsKey(fnlStr))
                    hrDate.Add(fnlStr, fromYear + "-" + toYear);
            }
        }
        return hrDate;
    }

    #region old
    //void Leave_Apply(double totleaveday)
    //{
    //    try
    //    {
    //        int ccc = 0;
    //        chkrelived = 0;
    //        DateTime fromdate1 = new DateTime();
    //        fromdate1 = TextToDate(txt_frm);
    //        DateTime todate1 = new DateTime();
    //        todate1 = TextToDate(txt_to);
    //        DataSet dt_leave_type = new DataSet();
    //        string qur = "select * from individual_leave_type where  staff_code='" + txt_staff_code.Text + "' and college_code=" + ddlcollege.SelectedItem.Value + "";
    //        dt_leave_type = d2.select_method_wo_parameter(qur, "Text");
    //        if (dt_leave_type.Tables[0].Rows.Count > 0)
    //        {
    //            double tot_days = 0;
    //            tot_days = Convert.ToDouble(totleaveday);
    //            string[] spl_type = dt_leave_type.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
    //            for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
    //            {
    //                string[] split_leave = spl_type[i].Split(';');
    //                if (split_leave[0].ToString() == ddl_leave_type.SelectedItem.ToString())
    //                {
    //                    if (split_leave[1].ToString() == "" || split_leave[1].ToString() == "0")
    //                    {
    //                        imgdiv2.Visible = true;
    //                        lbl_alert.Text = "No leave available in current year.";
    //                        alert = true;
    //                        return;
    //                    }
    //                    if (split_leave[1].ToString() != "")
    //                    {
    //                        if (Convert.ToDouble(split_leave[1].ToString()) < tot_days)
    //                        {
    //                            imgdiv2.Visible = true;
    //                            lbl_alert.Text = "No long leave available in current year for " + tot_days.ToString() + " day(s).";
    //                            alert = true;
    //                            return;
    //                        }
    //                        DataSet dt_paymonths = new DataSet();
    //                        string qur1 = "select * from hrpaymonths where College_Code='" + ddlcollege.SelectedItem.Value + "' and SelStatus='1'";
    //                        dt_paymonths = d2.select_method_wo_parameter(qur1, "text");
    //                        if (dt_paymonths.Tables[0].Rows.Count > 0)
    //                        {
    //                            string from_date = dt_paymonths.Tables[0].Rows[0]["from_date"].ToString();
    //                            string todate = dt_paymonths.Tables[0].Rows[dt_paymonths.Tables[0].Rows.Count - 1]["to_date"].ToString();
    //                            if (Convert.ToDateTime(from_date) > Convert.ToDateTime(fromdate1) || Convert.ToDateTime(todate) < Convert.ToDateTime(fromdate1) || Convert.ToDateTime(from_date) > Convert.ToDateTime(todate1) || Convert.ToDateTime(todate) < Convert.ToDateTime(todate1))
    //                            {
    //                                imgdiv2.Visible = true;
    //                                lbl_alert.Text = "The Year of Apply of leave is not matching the current HR Year.";
    //                                alert = true;
    //                                return;
    //                            }
    //                            double taken_leave = 0;
    //                            string leave = split_leave[0];
    //                            string leavepk = d2.GetFunction("select LeaveMasterPK from leave_category where category='" + leave + "' and college_code='" + ddlcollege.SelectedItem.Value + "'");
    //                            string appno = d2.GetFunction("select sm.appl_id from staff_appl_master sm,staffmaster s where sm.appl_no=s.appl_no and s.staff_code='" + txt_staff_code.Text + "'");
    //                            string qu2 = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + from_date + "' and LeaveTo<='" + todate + "' and ReqAppNo='" + appno + "' and ReqAppStatus='1' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + ddlcollege.SelectedItem.Value + "' ";
    //                            DataSet dt_monthleave_count = new DataSet();
    //                            dt_monthleave_count = d2.select_method_wo_parameter(qu2, "text");
    //                            if (dt_monthleave_count.Tables[0].Rows.Count == 0)
    //                            {
    //                                qu2 = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + from_date + "' and LeaveTo<='" + todate + "' and ReqAppNo='" + appno + "' and ReqAppStatus='0' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + ddlcollege.SelectedItem.Value + "'";
    //                                dt_monthleave_count = d2.select_method_wo_parameter(qu2, "text");
    //                            }
    //                            string leavefromdate = "";
    //                            string leavetodate = "";
    //                            string ishalfdate = "";
    //                            string halfdaydate = "";
    //                            int finaldate11 = 0;
    //                            double tot_leave = 0;
    //                            if (dt_monthleave_count.Tables[0].Rows.Count > 0)
    //                            {
    //                                #region leave count check
    //                                for (int k = 0; k < dt_monthleave_count.Tables[0].Rows.Count; k++)
    //                                {
    //                                    leavefromdate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["LeaveFrom"]);
    //                                    leavetodate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["LeaveTo"]);
    //                                    ishalfdate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["IsHalfDay"]);
    //                                    if (leavefromdate != "" && leavetodate != "")
    //                                    {
    //                                        string dtT = leavefromdate;
    //                                        string[] Split = dtT.Split('/');
    //                                        string enddt = leavetodate;
    //                                        Split = enddt.Split('/');
    //                                        DateTime fromdatenew = Convert.ToDateTime(dtT);
    //                                        DateTime todatenew = Convert.ToDateTime(enddt);
    //                                        TimeSpan days = todatenew - fromdatenew;
    //                                        string ndate = Convert.ToString(days);
    //                                        Split = ndate.Split('.');
    //                                        string getdate = Split[0];
    //                                        string sunday = fromdatenew.ToString("dddd");
    //                                        for (; fromdatenew <= todatenew; )
    //                                        {
    //                                            sunday = fromdatenew.ToString("dddd");
    //                                            if (sunday != "Sunday" && split_leave[3].ToString() != "1")
    //                                            {
    //                                                ccc++;
    //                                                tot_leave = tot_leave + Convert.ToDouble(ccc);
    //                                                taken_leave = tot_leave;
    //                                                if (ishalfdate == "True")
    //                                                {
    //                                                    halfdaydate = Convert.ToString(dt_monthleave_count.Tables[0].Rows[k]["HalfDate"]);
    //                                                    ccc = Convert.ToInt32(Convert.ToDouble(ccc) - 0.5);
    //                                                }
    //                                            }
    //                                            fromdatenew = fromdatenew.AddDays(1);
    //                                        }
    //                                    }
    //                                }
    //                                #endregion
    //                            }
    //                            taken_leave = ccc;
    //                            if (split_leave[0].ToString() != "")
    //                            {
    //                                if (taken_leave > Convert.ToDouble(split_leave[1].ToString()))
    //                                {
    //                                    imgdiv2.Visible = true;
    //                                    lbl_alert.Text = "No leave available in current year";
    //                                    alert = true;
    //                                    return;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                imgdiv2.Visible = true;
    //                                lbl_alert.Text = "No leave available in current year";
    //                                alert = true;
    //                                return;
    //                            }
    //                            ///.............. if monthly leave allot 0...............zzz
    //                            double all_leave = tot_days + taken_leave;
    //                            if (split_leave[2].ToString() != "0")
    //                            {
    //                                double temptwo = 0;
    //                                double.TryParse(Convert.ToString(split_leave[2]), out temptwo);
    //                                if (all_leave > temptwo && Convert.ToDouble(split_leave[6].ToString()) == 1)
    //                                {
    //                                    if (taken_leave >= Convert.ToDouble(split_leave[1].ToString()))
    //                                    {
    //                                        double difference = Convert.ToDouble(split_leave[2].ToString()) - taken_leave;
    //                                        imgdiv2.Visible = true;
    //                                        lbl_alert.Text = "Leave only available for " + difference.ToString() + " day(s).";
    //                                        alert = true;
    //                                        return;
    //                                    }
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (all_leave > Convert.ToDouble(split_leave[1].ToString()))
    //                                {
    //                                    double difference = Convert.ToDouble(split_leave[1].ToString()) - taken_leave;
    //                                    imgdiv2.Visible = true;
    //                                    lbl_alert.Text = "Leave only available for " + difference.ToString() + " day(s).";
    //                                    alert = true;
    //                                    return;
    //                                }
    //                            }
    //                            //....................IF monthly carry true.......................zzz
    //                            if (split_leave[6].ToString() == "1")
    //                            {
    //                                #region index 6
    //                                string monthly_leave = "";
    //                                string yearly_leave = "";
    //                                yearly_leave = Convert.ToString(split_leave[1]);
    //                                monthly_leave = Convert.ToString(split_leave[2]);
    //                                string sql_query = "select * from hrpaymonths where college_code='" + ddlcollege.SelectedItem.Value + "' and from_date <='" + fromdate1 + "'";
    //                                DataSet dt_months = d2.select_method_wo_parameter(sql_query, "Text");
    //                                if (dt_months.Tables[0].Rows.Count > 0)
    //                                {
    //                                    for (int s = 0; s < dt_months.Tables[0].Rows.Count; s++)
    //                                    {
    //                                        double monthly_total = 0;
    //                                        monthly_total = Convert.ToDouble(monthly_leave) * Convert.ToDouble(dt_months.Tables[0].Rows.Count);
    //                                        string SQlQuery = string.Empty;
    //                                        SQlQuery = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + dt_months.Tables[0].Rows[s]["From_date"].ToString() + "' and LeaveTo<='" + dt_months.Tables[0].Rows[s]["to_date"].ToString() + "' and ReqAppNo='" + appno + "' and ReqAppStatus='1' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' and l.college_code='" + ddlcollege.SelectedItem.Value + "' ";
    //                                        DataSet dt_count = d2.select_method_wo_parameter(SQlQuery, "Text");
    //                                        if (dt_count.Tables[0].Rows.Count > 0)
    //                                        {
    //                                            for (int k = 0; k < dt_count.Tables[0].Rows.Count; k++)
    //                                            {
    //                                                leavefromdate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveFrom"]);
    //                                                leavetodate = Convert.ToString(dt_count.Tables[0].Rows[k]["LeaveTo"]);
    //                                                ishalfdate = Convert.ToString(dt_count.Tables[0].Rows[k]["IsHalfDay"]);
    //                                                if (leavefromdate != "" && leavetodate != "")
    //                                                {
    //                                                    string dtT = leavefromdate;
    //                                                    string[] Split = dtT.Split('/');
    //                                                    string enddt = leavetodate;
    //                                                    Split = enddt.Split('/');
    //                                                    DateTime fromdatenew = Convert.ToDateTime(dtT);
    //                                                    DateTime todatenew = Convert.ToDateTime(enddt);
    //                                                    TimeSpan days = fromdatenew - todatenew;
    //                                                    string ndate = Convert.ToString(days);
    //                                                    Split = ndate.Split('.');
    //                                                    string getdate = Split[0];
    //                                                    if (fromdatenew != todatenew)
    //                                                        finaldate11 = Convert.ToInt32(getdate);
    //                                                    else
    //                                                        finaldate11 = 0;
    //                                                    if (ishalfdate == "True")
    //                                                    {
    //                                                        halfdaydate = Convert.ToString(dt_count.Tables[0].Rows[k]["HalfDate"]);
    //                                                        tot_leave = Convert.ToDouble(finaldate11 + 1);
    //                                                        tot_leave = tot_leave - 0.5;
    //                                                        tot_days = tot_leave;
    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        tot_leave = tot_leave + Convert.ToDouble(finaldate11 + 1);
    //                                                        tot_days = tot_leave;
    //                                                    }
    //                                                }
    //                                                if (Convert.ToDouble(yearly_leave) > tot_days)
    //                                                {
    //                                                }
    //                                                else
    //                                                {
    //                                                    imgdiv2.Visible = true;
    //                                                    lbl_alert.Text = "No leave available.";
    //                                                    alert = true;
    //                                                    return;
    //                                                }
    //                                                if (dt_leave_type.Tables[0].Rows.Count > 0)
    //                                                {
    //                                                    string leave_cate = dt_leave_type.Tables[0].Rows[0]["leavetype"].ToString();
    //                                                    string[] spl_leave_cate = leave_cate.Split('\\');
    //                                                    for (int r = 0; spl_leave_cate.GetUpperBound(0) >= r; r++)
    //                                                    {
    //                                                        string[] spl_lv_cat = spl_leave_cate[r].ToString().Split(';');
    //                                                        if (spl_lv_cat[0].ToString() == ddl_leave_type.SelectedItem.ToString())
    //                                                        {
    //                                                            if (spl_lv_cat[6].ToString() == "0")
    //                                                            {
    //                                                                double month_leave = 0;
    //                                                                if (spl_lv_cat[6].ToString() != "")
    //                                                                {
    //                                                                    month_leave = Convert.ToDouble(spl_lv_cat[2].ToString());
    //                                                                }
    //                                                                if (tot_days > month_leave)
    //                                                                {
    //                                                                    imgdiv2.Visible = true;
    //                                                                    lbl_alert.Text = "No leave available.";
    //                                                                    alert = true;
    //                                                                    return;
    //                                                                }
    //                                                            }
    //                                                            else
    //                                                            {
    //                                                                break;
    //                                                            }
    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                                #endregion
    //                            }
    //                            else
    //                            {
    //                                #region index not 6
    //                                if (dt_monthleave_count.Tables[0].Rows.Count > 0)
    //                                {
    //                                    for (int k = 0; k < dt_monthleave_count.Tables[0].Rows.Count; k++)
    //                                    {
    //                                        if (split_leave[2].ToString() != "0")
    //                                        {
    //                                            double temptwo = 0;
    //                                            double.TryParse(Convert.ToString(split_leave[2]), out temptwo);
    //                                            if (taken_leave > temptwo)
    //                                            {
    //                                                imgdiv2.Visible = true;
    //                                                lbl_alert.Text = "No leave available in this month";
    //                                                alert = true;
    //                                                return;
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            if (taken_leave > Convert.ToDouble(split_leave[1]))
    //                                            {
    //                                                imgdiv2.Visible = true;
    //                                                lbl_alert.Text = "No leave available in this month";
    //                                                alert = true;
    //                                                return;
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    if (split_leave.Length > 2)
    //                                    {
    //                                        string splval2 = Convert.ToString(split_leave[2]);
    //                                        if (splval2 != "")
    //                                        {
    //                                            if (split_leave[2].ToString() != "0")
    //                                            {
    //                                                if (taken_leave > Convert.ToDouble(splval2))
    //                                                {
    //                                                    imgdiv2.Visible = true;
    //                                                    lbl_alert.Text = "No leave available in this month";
    //                                                    alert = true;
    //                                                    return;
    //                                                }
    //                                                else
    //                                                {
    //                                                    if (taken_leave > Convert.ToDouble(split_leave[2].ToString()))
    //                                                    {
    //                                                        string l = Convert.ToString(split_leave[2].ToString());
    //                                                        imgdiv2.Visible = true;
    //                                                        lbl_alert.Text = "You Can't Apply More Than of " + l + "Days";
    //                                                        alert = true;
    //                                                        return;
    //                                                    }
    //                                                }
    //                                            }
    //                                            if (taken_leave > Convert.ToDouble(split_leave[1]))
    //                                            {
    //                                                imgdiv2.Visible = true;
    //                                                lbl_alert.Text = "No leave available in this month";
    //                                                alert = true;
    //                                                return;
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            if (taken_leave > Convert.ToDouble(split_leave[1]))
    //                                            {
    //                                                imgdiv2.Visible = true;
    //                                                lbl_alert.Text = "No leave available in this month";
    //                                                alert = true;
    //                                                return;
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                                #endregion
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        imgdiv2.Visible = true;
    //                        lbl_alert.Text = "Can't Apply This LeaveType, Leave Details not Entered";
    //                        alert = true;
    //                        return;
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            imgdiv2.Visible = true;
    //            lbl_alert.Text = "Can't Apply This LeaveType, Leave Details not Entered";
    //            alert = true;
    //            return;
    //        }
    //    }
    //    catch { }
    //}
    #endregion

    public void Check_Status(string staffcode, string fdate, string tdate, int chkhalf, string hlf, string d, bool saveOrAlter)
    {
        try
        {
            DataTable dtgrid = new DataTable();

            DataTable dt = new DataTable();
            dt.Columns.Add("Dummy0");
            dt.Columns.Add("Dummy1");
            dt.Columns.Add("Dummy2");
            dt.Columns.Add("Dummy3");
            dt.Columns.Add("Dummy4");
            int sno = 0;

            for (int j = 0; j < GV1.Rows.Count; j++)
            {
                sno++;
                DataRow dr = dt.NewRow();
                string morningcheck = string.Empty;
                string eveningcheck = string.Empty;
                TextBox txtdt = (TextBox)GV1.Rows[j].FindControl("txtdate");
                CheckBox chkmrng = (CheckBox)GV1.Rows[j].FindControl("chk_mrng");
                CheckBox chkevng = (CheckBox)GV1.Rows[j].FindControl("chk_evng");
                CheckBoxList atttype = (CheckBoxList)GV1.Rows[j].FindControl("cblhr");
                string getdatetime = Convert.ToString(txtdt.Text);

                if (getdatetime.Contains('/'))
                {
                    string[] splitdates = getdatetime.Split('/');
                    getdatetime = splitdates[1] + "/" + splitdates[0] + "/" + splitdates[2];

                }
                if (chkmrng.Checked)
                {
                    morningcheck = "True";
                }
                else
                {
                    morningcheck = "False";
                }
                if (chkevng.Checked)
                {
                    eveningcheck = "True";

                }
                else
                {
                    eveningcheck = "False";
                }
                string selectedItem = string.Empty;
                if (atttype.Items.Count > 0)
                {


                    for (int cb = 0; cb < atttype.Items.Count; cb++)
                    {

                        if (atttype.Items[cb].Selected)
                        {
                            string cbVal = Convert.ToString(atttype.Items[cb].Value);
                            if (selectedItem == "")
                            {
                                selectedItem = cbVal;
                            }
                            else
                            {
                                selectedItem = selectedItem + "," + cbVal;
                            }
                        }
                    }
                }
                dr["Dummy0"] = sno;
                dr["Dummy1"] = Convert.ToDateTime(getdatetime);
                dr["Dummy2"] = morningcheck;
                dr["Dummy3"] = eveningcheck;
                dr["Dummy4"] = selectedItem;


                dt.Rows.Add(dr);
            }
            chkrelived = 0;
            string degcode = "";
            string[] Days = new string[7] { "mon", "tue", "wed", "thu", "fri", "sat", "sun" };
            DataSet dsgetvalue = new DataSet();
            DataSet dsalterperiod = new DataSet();
            DataSet dsstuatt = new DataSet();
            Hashtable hatvalue = new Hashtable();
            Hashtable hatsublab = new Hashtable();
            string degree_var = "";
            string cur_camprevar = "";
            string tmp_datevalue = "";
            string tmp_camprevar = "";
            string strsction = "";
            string strday = "";
            string start_datesem = "";
            string start_dayorder = "";
            string noofdays = "";
            string Strsql = "";
            string SqlFinal = "";
            string sql1 = "";
            string tmp_varstr = "";
            string vari = "";
            string stafftakenclass = "";
            string staffdegreeclass = "";
            string Day_Order = "";
            int noofhrs = 0;
            string MorningDayandEvening = string.Empty;
            Dictionary<string, string> dtSubType = dictSubType();
            DataSet ds_attndmaster = new DataSet();
            ht_sch.Clear();
            hat.Clear();
            hat.Add("college_code", Session["collegecode"].ToString());
            string sql_stringvar = "sp_select_details_staff";
            ds_attndmaster.Dispose();
            ds_attndmaster.Reset();
            ds_attndmaster = d2.select_method(sql_stringvar, hat, "sp");
            if (ds_attndmaster.Tables[0].Rows.Count > 0)
            {
                for (int pcont = 0; pcont < ds_attndmaster.Tables[0].Rows.Count; pcont++)
                {
                    degree_var = Convert.ToString(ds_attndmaster.Tables[0].Rows[pcont]["degree_code"]) + "-" + Convert.ToString(ds_attndmaster.Tables[0].Rows[pcont]["semester"]);
                    if (!ht_sch.Contains(Convert.ToString(degree_var)))
                    {
                        vari = ds_attndmaster.Tables[0].Rows[pcont]["SchOrder"] + "," + ds_attndmaster.Tables[0].Rows[pcont]["nodays"];
                        ht_sch.Add(degree_var, Convert.ToString(vari));
                    }
                }
            }
            ht_sdate.Clear();
            if (ds_attndmaster.Tables[1].Rows.Count > 0)
            {
                for (int pcont = 0; pcont < ds_attndmaster.Tables[1].Rows.Count; pcont++)
                {
                    degree_var = Convert.ToString(ds_attndmaster.Tables[1].Rows[pcont]["batch_year"]) + "-" + Convert.ToString(ds_attndmaster.Tables[1].Rows[pcont]["degree_code"]) + "-" + Convert.ToString(ds_attndmaster.Tables[1].Rows[pcont]["semester"]);
                    if (!ht_sdate.Contains(Convert.ToString(degree_var)))
                    {
                        vari = ds_attndmaster.Tables[1].Rows[pcont]["sdate"] + "," + ds_attndmaster.Tables[1].Rows[pcont]["starting_dayorder"];
                        ht_sdate.Add(degree_var, Convert.ToString(vari));
                    }
                }
            }
            ht_bell.Clear();
            if (ds_attndmaster.Tables[2].Rows.Count > 0)
            {
                for (int pcont = 0; pcont < ds_attndmaster.Tables[2].Rows.Count; pcont++)
                {
                    degree_var = Convert.ToString(ds_attndmaster.Tables[2].Rows[pcont]["batch_year"]) + "-" + Convert.ToString(ds_attndmaster.Tables[2].Rows[pcont]["degree_code"]) + "-" + Convert.ToString(ds_attndmaster.Tables[2].Rows[pcont]["semester"]) + "-" + Convert.ToString(ds_attndmaster.Tables[2].Rows[pcont]["period1"]);
                    if (!ht_bell.Contains(Convert.ToString(degree_var)))
                    {
                        vari = ds_attndmaster.Tables[2].Rows[pcont]["start_time"] + "," + ds_attndmaster.Tables[2].Rows[pcont]["end_time"];
                        ht_bell.Add(degree_var, Convert.ToString(vari));
                    }
                }
            }
            ht_period.Clear();
            if (ds_attndmaster.Tables[3].Rows.Count > 0)
            {
                for (int pcont = 0; pcont < ds_attndmaster.Tables[3].Rows.Count; pcont++)
                {
                    degree_var = Convert.ToString(ds_attndmaster.Tables[3].Rows[pcont]["lock_hr"]);
                    if (!ht_period.Contains(Convert.ToString(degree_var)))
                    {
                        vari = ds_attndmaster.Tables[3].Rows[pcont]["markatt_from"] + "," + ds_attndmaster.Tables[3].Rows[pcont]["markatt_to"];
                        ht_period.Add(degree_var, Convert.ToString(vari));
                    }
                }
            }
            string degreename = "";
            Hashtable hatdegreename = new Hashtable();
            for (int i = 0; i < ds_attndmaster.Tables[5].Rows.Count; i++)
            {
                if (!hatdegreename.Contains(ds_attndmaster.Tables[5].Rows[i]["Degree_Code"].ToString()))
                {
                    hatdegreename.Add(ds_attndmaster.Tables[5].Rows[i]["Degree_Code"].ToString(), ds_attndmaster.Tables[5].Rows[i]["course"].ToString() + '$' + ds_attndmaster.Tables[5].Rows[i]["dept_acronym"].ToString());
                }
            }
            DateTime dtf = Convert.ToDateTime(fdate);
            DateTime dtt = Convert.ToDateTime(tdate);
            if (dtf <= dtt)
            {
                long days = -1;
                DateTime dt1 = DateTime.Now.AddDays(-6);
                DateTime dt2 = DateTime.Now;
                try
                {
                    dt1 = Convert.ToDateTime(fdate);
                    dt2 = Convert.ToDateTime(tdate);
                    TimeSpan t = dt2.Subtract(dt1);
                    days = t.Days;
                }
                catch
                {
                    try
                    {
                        dt1 = Convert.ToDateTime(fdate);
                        dt2 = Convert.ToDateTime(tdate);
                        TimeSpan t = dt2.Subtract(dt1);
                        days = t.Days;
                    }
                    catch
                    {
                    }
                }
                if (days >= 0)
                {
                    string[] differdays = new string[days];
                    noofhrs = 0;
                    if (ds_attndmaster.Tables[6].Rows.Count > 0)
                    {
                        if (ds_attndmaster.Tables[6].Rows[0]["noofhours"].ToString().Trim() != "" && ds_attndmaster.Tables[6].Rows[0]["noofhours"].ToString().Trim() != null && ds_attndmaster.Tables[6].Rows[0]["noofhours"].ToString().Trim() != "0")
                        {
                            noofhrs = Convert.ToInt32(ds_attndmaster.Tables[6].Rows[0]["noofhours"].ToString());
                        }
                    }
                    sql1 = "";
                    Strsql = "";
                    SqlFinal = "";
                    string stafcode = staffcode;
                    for (int day_lp = 0; day_lp < 7; day_lp++)
                    {
                        strday = Days[day_lp].ToString();
                        sql1 = sql1 + "(";
                        tmp_varstr = "";
                        for (int i_loop = 1; i_loop <= noofhrs; i_loop++)
                        {
                            Strsql = Strsql + strday + Convert.ToString(i_loop) + ",";
                            if (tmp_varstr == "")
                            {
                                tmp_varstr = tmp_varstr + strday + Convert.ToString(i_loop) + " like '%" + stafcode + "%'";
                            }
                            else
                            {
                                tmp_varstr = tmp_varstr + " or " + strday + Convert.ToString(i_loop) + " like '%" + stafcode + "%'";
                            }
                        }
                        if (day_lp != 6)
                            tmp_varstr = tmp_varstr + ") or ";
                        else
                            tmp_varstr = tmp_varstr + ")";
                        sql1 = sql1 + tmp_varstr.ToString();
                    }
                    SqlFinal = " select distinct r.Batch_Year,r.degree_code,sy.semester,r.Sections,si.end_date from staff_selector ss,Registration r,";
                    SqlFinal = SqlFinal + " subject s,sub_sem sm,syllabus_master sy,seminfo si where sy.Batch_Year=r.Batch_Year and sy.degree_code=r.degree_code";
                    SqlFinal = SqlFinal + " and sy.semester=r.Current_Semester and sy.syll_code=sm.syll_code and sm.subType_no=s.subType_no ";
                    SqlFinal = SqlFinal + " and s.subject_no=ss.subject_no and isnull(r.sections,'')=isnull(ss.sections,'') and ss.batch_year=r.Batch_Year";
                    SqlFinal = SqlFinal + " and si.Batch_Year=r.Batch_Year and si.degree_code=r.degree_code and si.semester=r.Current_Semester and ";
                    SqlFinal = SqlFinal + " si.Batch_Year=sy.Batch_Year and sy.degree_code=r.degree_code and si.semester=sy.Semester and r.CC=0 and r.Exam_Flag<>'debar'";
                    SqlFinal = SqlFinal + " and r.DelFlag=0 and ss.staff_code='" + stafcode + "'";
                    DataView dvalternaet = new DataView();
                    DataView dvsemster = new DataView();
                    DataView dvholiday = new DataView();
                    DataView dvdaily = new DataView();
                    DataView dvsubject = new DataView();
                    DataView dvsublab = new DataView();
                    string getalldetails = "select * from Alternate_Schedule where FromDate between '" + fdate + "' and '" + tdate + "' ; ";
                    getalldetails = getalldetails + " select * from Semester_Schedule order by FromDate desc; ";
                    getalldetails = getalldetails + " Select * from holidaystudents where holiday_date between '" + fdate + "' and '" + tdate + "' ; ";
                    getalldetails = getalldetails + " select staff_name,staff_code from staffmaster ; ";
                    getalldetails = getalldetails + " select s.subject_no,s.subject_name,s.subject_code,sy.Batch_Year,sy.degree_code,sy.semester,ss.Lab from Registration r,syllabus_master sy,sub_sem ss,subject s where r.Batch_Year=sy.Batch_Year and r.degree_code=sy.degree_code and r.Current_Semester=sy.semester and sy.syll_code=ss.syll_code and sy.syll_code=s.syll_code and ss.subType_no=ss.subType_no and r.cc=0 and r.delflag=0 and r.exam_flag<>'debar' and r.degree_code='2012' and r.Batch_Year='1052' and r.Current_Semester='3';";
                    getalldetails = getalldetails + " select distinct Current_Semester,Batch_Year,degree_code from Registration where cc=0 and delflag=0 and exam_flag<>'debar'; ";
                    getalldetails = getalldetails + " select no_of_hrs_I_half_day as mor,no_of_hrs_I_half_day as eve,degree_code,semester from periodattndschedule";
                    getalldetails = getalldetails + " select * from tbl_consider_day_order";
                    DataSet dsall = d2.select_method_wo_parameter(getalldetails, "Text");
                    Hashtable hatholiday = new Hashtable();
                    DataSet dsperiod = d2.select_method(SqlFinal, hat, "Text");

                    DataSet dsperiodNew = d2.select_method_wo_parameter("select distinct s.batch_year,r.degree_code,s.semester,s.sections from registration r, Semester_Schedule s where s.batch_year=r.batch_year and s.degree_code=r.degree_code and r.current_semester=s.semester and ltrim(rtrim(isnull(r.sections,'')))=ltrim(rtrim(isnull(s.sections,''))) and r.CC=0 and r.Exam_Flag<>'debar' and r.DelFlag=0  order by s.batch_year desc", "text");

                    string SemInfo = "select batch_year, degree_code ,semester,CONVERT(nvarchar(20),end_date,101) end_date,CONVERT(nvarchar(20),start_date,101) start_date from seminfo";
                    DataTable dtSem = dir.selectDataTable(SemInfo);

                    if (dsperiodNew.Tables.Count > 0 && dsperiodNew.Tables[0].Rows.Count > 0)
                    {
                        for (int pre = 0; pre < dsperiodNew.Tables[0].Rows.Count; pre++)
                        {
                            staffdegreeclass = "";
                            cur_camprevar = Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["batch_year"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["degree_code"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["semester"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]);
                            deg_batch_sem = cur_camprevar;
                            string getdate = "";
                            if (Convert.ToString(tmp_camprevar.Trim()) != Convert.ToString(cur_camprevar.Trim()))
                            {
                                strsction = "";
                                if ((Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) != "") && (Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) != "-1"))
                                {
                                    strsction = " and sections='" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) + "'";
                                }
                                string strsubstucount = " select count(distinct r.Roll_No) as stucount,r.Batch_Year,r.degree_code,r.Current_Semester,isnull(r.Sections,'')Sections,s.subject_no,r.adm_date from registration r,subjectchooser s where  r.roll_no=s.roll_no and  r.current_semester=s.semester";
                                strsubstucount = strsubstucount + " and batch_year='" + dsperiodNew.Tables[0].Rows[pre]["batch_year"] + "' and  degree_code='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "'  and current_semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "'  and cc=0 and delflag=0 and exam_flag<>'debar' " + strsction + "  group by r.Batch_Year,r.degree_code,r.Current_Semester,isnull(r.Sections,''),s.subject_no,r.adm_date";
                                DataSet dssubstucount = d2.select_method_wo_parameter(strsubstucount, "Text");
                                DataView dvsubstucount = new DataView();
                                hatholiday.Clear();
                                dsall.Tables[2].DefaultView.RowFilter = " degree_code=" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + " and semester=" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + " ";
                                DataView duholiday = dsall.Tables[2].DefaultView;
                                for (int i = 0; i < duholiday.Count; i++)
                                {
                                    if (!hatholiday.Contains(duholiday[i]["holiday_date"].ToString()))
                                    {
                                        hatholiday.Add(duholiday[i]["holiday_date"].ToString(), duholiday[i]["holiday_desc"].ToString());
                                    }
                                }
                                string subjectquery = "select distinct s.subject_no,s.subject_name,s.subject_code,sy.Batch_Year,sy.degree_code,sy.semester,ss.Lab,ss.subject_type from Registration r,syllabus_master sy,sub_sem ss,subject s where r.Batch_Year=sy.Batch_Year and r.degree_code=sy.degree_code and r.Current_Semester=sy.semester and sy.syll_code=ss.syll_code and sy.syll_code=s.syll_code and ss.subType_no=s.subType_no and r.cc=0 and r.delflag=0 and r.exam_flag<>'debar' and r.batch_year='" + dsperiodNew.Tables[0].Rows[pre]["batch_year"] + "' and  r.degree_code='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "'  and r.current_semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "' order by s.subject_no";
                                DataSet dssubject = d2.select_method_wo_parameter(subjectquery, "Text");
                                int frshlf = 0, schlf = 0;
                                dsall.Tables[6].DefaultView.RowFilter = " degree_code ='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "' and  semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "'";
                                DataView dvperiod = dsall.Tables[6].DefaultView;
                                if (dvperiod.Count > 0)
                                {
                                    string morhr = dvperiod[0]["mor"].ToString();
                                    string evehr = dvperiod[0]["eve"].ToString(); // poo
                                    MorningDayandEvening = morhr + "$" + evehr;
                                    if (morhr != null && morhr.Trim() != "")
                                        frshlf = Convert.ToInt32(morhr);
                                    if (evehr != null && evehr.Trim() != "")
                                        schlf = Convert.ToInt32(evehr);
                                }
                                string getcurrent_sem = "";
                                dsall.Tables[5].DefaultView.RowFilter = "degree_code ='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "'  and batch_year = '" + dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + "'";
                                DataView dvcurrsem = dsall.Tables[5].DefaultView;
                                if (dvcurrsem.Count > 0)
                                {
                                    getcurrent_sem = dvcurrsem[0]["current_semester"].ToString();
                                }
                                if (Convert.ToString(getcurrent_sem) == Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["semester"]))
                                {
                                    //string semenddate = dsperiodNew.Tables[0].Rows[pre]["end_date"].ToString();
                                    string semenddate = string.Empty;
                                    DataView dvSeminfo = new DataView();
                                    if (dtSem.Rows.Count > 0)
                                    {
                                        dtSem.DefaultView.RowFilter = "degree_code ='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "'  and batch_year = '" + dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + "' and semester=" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + " ";
                                        dvSeminfo = dtSem.DefaultView;
                                        semenddate = Convert.ToString(dvSeminfo[0]["end_date"]);
                                    }
                                    //string semenddate = dsperiodNew.Tables[0].Rows[pre]["end_date"].ToString();
                                    string altersetion = "";
                                    if (dsperiodNew.Tables[0].Rows[pre]["sections"].ToString() != "-1" && dsperiodNew.Tables[0].Rows[pre]["sections"].ToString() != null && dsperiodNew.Tables[0].Rows[pre]["sections"].ToString().Trim() != "")
                                    {
                                        altersetion = "and Sections='" + dsperiodNew.Tables[0].Rows[pre]["sections"].ToString() + "'";
                                    }
                                    Hashtable hatdc = new Hashtable();
                                    dsall.Tables[7].DefaultView.RowFilter = "degree_code ='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "'  and batch_year = '" + dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + "' and semester=" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "  ";
                                    DataView dvdayorderchanged = dsall.Tables[7].DefaultView;
                                    for (int dc = 0; dc < dvdayorderchanged.Count; dc++)
                                    {
                                        DateTime dtdcf = Convert.ToDateTime(dvdayorderchanged[dc]["from_date"].ToString());
                                        DateTime dtdct = Convert.ToDateTime(dvdayorderchanged[dc]["to_date"].ToString());
                                        for (DateTime dtc = dtdcf; dtc <= dtdct; dtc = dtc.AddDays(1))
                                        {
                                            if (!hatdc.Contains(dtc))
                                            {
                                                hatdc.Add(dtc, dtc);
                                            }
                                        }
                                    }
                                    for (int row_inc = 0; row_inc <= days; row_inc++)
                                    {
                                        string setdate = "";
                                        if (hatdegreename.Contains(dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString()))
                                        {
                                            degreename = Convert.ToString(hatdegreename[dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString()]).ToString();
                                        }
                                        DateTime cur_day = new DateTime();
                                        cur_day = dt2.AddDays(-row_inc);
                                        if (!hatdc.Contains(cur_day))
                                        {
                                            tmp_datevalue = Convert.ToString(cur_day);
                                            degree_var = Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["degree_code"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["semester"]);
                                            string SchOrder = "";
                                            string day_from = cur_day.ToString("yyyy-MM-dd");
                                            DateTime schfromdate = cur_day;
                                            dsall.Tables[1].DefaultView.RowFilter = "batch_year='" + dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + "' and degree_code='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "' and semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "' " + altersetion + " and FromDate<='" + cur_day.ToString() + "'";
                                            dvsemster = dsall.Tables[1].DefaultView;
                                            if (dvsemster.Count > 0)
                                                getdate = dvsemster[0]["FromDate"].ToString();
                                            if (Convert.ToString(getdate) != "" && Convert.ToString(getdate).Trim() != "0" && Convert.ToString(getdate).Trim() != null)
                                            {
                                                DateTime getsche = Convert.ToDateTime(getdate);
                                                if (Convert.ToDateTime(schfromdate) == Convert.ToDateTime(getsche) || Convert.ToDateTime(schfromdate) != Convert.ToDateTime(getsche))
                                                {
                                                    if (ht_sch.Contains(Convert.ToString(degree_var)))
                                                    {
                                                        string contvar = Convert.ToString(ht_sch[Convert.ToString(degree_var)]);
                                                        string[] sp_rd_semi = contvar.Split(',');
                                                        if (sp_rd_semi.GetUpperBound(0) >= 1)
                                                        {
                                                            SchOrder = sp_rd_semi[0].ToString();
                                                            noofdays = sp_rd_semi[1].ToString();
                                                        }
                                                    }
                                                    degree_var = Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["batch_year"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["degree_code"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["semester"]);
                                                    if (ht_sdate.Contains(Convert.ToString(degree_var)))
                                                    {
                                                        string contvar = Convert.ToString(ht_sdate[Convert.ToString(degree_var)]);
                                                        string[] sp_rd_semi = contvar.Split(',');
                                                        if (sp_rd_semi.GetUpperBound(0) >= 1)
                                                        {
                                                            start_datesem = sp_rd_semi[0].ToString();
                                                            start_dayorder = sp_rd_semi[1].ToString();
                                                        }
                                                    }
                                                    if (noofdays.ToString().Trim() == "")
                                                        goto lb1;
                                                    Day_Order = "";
                                                    if (SchOrder == "1")
                                                    {
                                                        strday = cur_day.ToString("ddd");
                                                        Day_Order = "0-" + Convert.ToString(strday);
                                                    }
                                                    else
                                                    {
                                                        string[] sps = dt2.ToString().Split('/');
                                                        string curdate = sps[0] + '/' + sps[1] + '/' + sps[2];
                                                        strday = d2.findday(cur_day.ToString(), dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString(), dsperiodNew.Tables[0].Rows[pre]["semester"].ToString(), dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString(), start_datesem.ToString(), noofdays.ToString(), start_dayorder);
                                                        Day_Order = "0-" + Convert.ToString(strday);
                                                    }
                                                    if (strday.ToString().Trim() == "")
                                                        goto lb1;
                                                    string reasonsun = "";
                                                    if (!hatholiday.Contains(cur_day.ToString()) || reasonsun.Trim().ToLower() != "sunday")
                                                    {
                                                        string str_day = strday;
                                                        string Atmonth = cur_day.Month.ToString();
                                                        string Atyear = cur_day.Year.ToString();
                                                        long strdate = (Convert.ToInt32(Atmonth) + Convert.ToInt32(Atyear) * 12);
                                                        sql1 = "";
                                                        Strsql = "";
                                                        DataView dv = new DataView();
                                                        if (dt.Rows.Count > 0)
                                                        {


                                                            dt.DefaultView.RowFilter = "Dummy1='" + cur_day + "'";
                                                            dv = dt.DefaultView;

                                                        }
                                                        for (int i_loop = 1; i_loop <= noofhrs; i_loop++)//delsijref
                                                        {
                                                            if (dv.Count > 0)
                                                            {
                                                                string getselected = Convert.ToString(dv[0]["Dummy4"]);
                                                                string lopval = Convert.ToString(i_loop);
                                                                if (getselected.Contains(lopval))
                                                                {

                                                                    Strsql = Strsql + strday + Convert.ToString(i_loop) + ",";
                                                                    if (sql1 == "")
                                                                    {
                                                                        sql1 = sql1 + strday + Convert.ToString(i_loop) + " like '%" + stafcode + "%'";//Modified by Manikandan 14/08/2013 from above comment line
                                                                    }
                                                                    else
                                                                    {
                                                                        sql1 = sql1 + " or " + strday + Convert.ToString(i_loop) + " like '%" + stafcode + "%'";//Modified by Manikandan 14/08/2013 from above comment line
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        string day_aten = cur_day.Day.ToString();
                                                        string strsectionvar = "";
                                                        string labsection = "";
                                                        if (Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) != "" && Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) != "-1")
                                                        {
                                                            strsectionvar = " and sections='" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) + "'";
                                                            labsection = " and sections='" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]) + "'";
                                                        }
                                                        sql1 = " and (" + sql1 + ")";
                                                        //zzz
                                                        dsall.Tables[0].DefaultView.RowFilter = "degree_code=" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + " and batch_year=" + dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + " and semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "' " + altersetion + " and fromdate='" + day_from + "'";
                                                        dvalternaet = dsall.Tables[0].DefaultView;
                                                        string text_temp = "";
                                                        int temp = 0;
                                                        text_temp = "";
                                                        string alterSubType = string.Empty;
                                                        string getcolumnfield = "";
                                                        Boolean moringleav = false;
                                                        Boolean evenleave = false;
                                                        //zzz                                                        
                                                        dsall.Tables[2].DefaultView.RowFilter = "holiday_date='" + cur_day.ToString("MM/dd/yyyy") + "' and degree_code=" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + " and semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "'";
                                                        dvholiday = dsall.Tables[2].DefaultView;
                                                        if (dvholiday.Count > 0)
                                                        {
                                                            if (!hatholiday.Contains(cur_day.ToString()))
                                                                hatholiday.Add(cur_day.ToString(), dvholiday[0]["holiday_desc"].ToString());
                                                            if (dvholiday[0]["morning"].ToString() == "1" || dvholiday[0]["morning"].ToString().Trim().ToLower() == "true")
                                                                moringleav = true;
                                                            if (dvholiday[0]["evening"].ToString() == "1" || dvholiday[0]["evening"].ToString().Trim().ToLower() == "true")
                                                                evenleave = true;
                                                            if (dvholiday[0]["halforfull"].ToString() == "0" || dvholiday[0]["halforfull"].ToString().Trim().ToLower() == "false")
                                                            {
                                                                evenleave = true;
                                                                moringleav = true;
                                                            }
                                                        }
                                                        if (hlf != "0")
                                                        {
                                                            string hr = "";
                                                            if (d != "1")
                                                            {
                                                                hr = d2.GetFunction("select no_of_hrs_II_half_day from PeriodAttndSchedule where degree_code='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "' and semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "'");
                                                            }
                                                            else if (d == "1")
                                                            {
                                                                hr = d2.GetFunction("select no_of_hrs_I_half_day from PeriodAttndSchedule where degree_code='" + dsperiodNew.Tables[0].Rows[pre]["degree_code"].ToString() + "' and semester='" + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + "'");
                                                            }
                                                            noofhrs = Convert.ToInt32(hr);
                                                        }

                                                        for (temp = 1; temp <= noofhrs; temp++)
                                                        {
                                                            if (dv.Count > 0)//delsi1203
                                                            {
                                                                string getselected = Convert.ToString(dv[0]["Dummy4"]);
                                                                string lopval = Convert.ToString(temp);
                                                                if (getselected.Contains(lopval))
                                                                {
                                                                    string sp_rd = "";
                                                                    Boolean altfalg = false;
                                                                    if (dvalternaet.Count > 0)
                                                                    {
                                                                        sp_rd = dvalternaet[0]["" + strday.Trim() + temp + ""].ToString();
                                                                        if (hatdegreename.Contains(dvalternaet[0]["degree_code"].ToString()))
                                                                        {
                                                                            degreename = Convert.ToString(hatdegreename[dvalternaet[0]["degree_code"].ToString()]);
                                                                            degcode = dvalternaet[0]["degree_code"].ToString();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        sp_rd = "";
                                                                    }
                                                                    if (sp_rd.Trim() != "" && sp_rd.Trim() != "0" && sp_rd != null)
                                                                    {
                                                                        #region
                                                                        altfalg = true;
                                                                        string[] sp_rd_split = sp_rd.Split(';');
                                                                        for (int index = 0; index <= sp_rd_split.GetUpperBound(0); index++)
                                                                        {
                                                                            string[] sp2 = sp_rd_split[index].Split(new Char[] { '-' });
                                                                            if (sp2.GetUpperBound(0) >= 1)
                                                                            {
                                                                                int upperbound = sp2.GetUpperBound(0);
                                                                                for (int multi_staff = 1; multi_staff < sp2.GetUpperBound(0); multi_staff++)
                                                                                {
                                                                                    if (sp2[multi_staff] == stafcode)
                                                                                    {
                                                                                        string sect = dsperiodNew.Tables[0].Rows[pre]["sections"].ToString();
                                                                                        if (sect != "-1" && sect != null && sect.Trim() != "")
                                                                                        {
                                                                                            sect = "-Section " + sect + "";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            sect = "$Section ";
                                                                                        }
                                                                                        if (semenddate.Trim() != "" && semenddate.Trim() != null && semenddate.Trim() != "0")
                                                                                        {
                                                                                            if (cur_day <= (Convert.ToDateTime(semenddate)))
                                                                                            {
                                                                                                double Num;
                                                                                                bool isNum = double.TryParse(sp2[0].ToString(), out Num);
                                                                                                if (isNum)
                                                                                                {
                                                                                                    dssubject.Tables[0].DefaultView.RowFilter = " subject_no=" + sp2[0] + "";
                                                                                                    dvsubject = dssubject.Tables[0].DefaultView;
                                                                                                    if (dvsubject.Count > 0)
                                                                                                    {
                                                                                                        string hourset = "";
                                                                                                        if (setdate == "")
                                                                                                        {
                                                                                                            setdate = cur_day.ToString("dd/MM/yyyy");
                                                                                                            hourset = " Date :" + setdate + " -Hour " + temp.ToString();
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            hourset = " * Hour " + temp.ToString();
                                                                                                        }
                                                                                                        text_temp = hourset + " " + dvsubject[0]["subject_name"].ToString();
                                                                                                        //29.12.2016
                                                                                                        alterSubType = Convert.ToString(dvsubject[0]["subject_type"]);
                                                                                                        //  alterSubType = Convert.ToString(d2.GetFunction(" select distinct  sm.subject_type from sub_sem sm,subject s where sm.subType_no=s.subType_No and s.subject_name ='" + dvsubject[0]["subject_name"] + "'"));
                                                                                                    }
                                                                                                    Boolean allowleave = false;
                                                                                                    if (hatholiday.Contains(cur_day.ToString()))
                                                                                                    {
                                                                                                        if (moringleav == true)
                                                                                                        {
                                                                                                            if (frshlf >= temp)
                                                                                                            {
                                                                                                                allowleave = true;
                                                                                                            }
                                                                                                        }
                                                                                                        if (evenleave == true)
                                                                                                        {
                                                                                                            if (temp > frshlf)
                                                                                                            {
                                                                                                                allowleave = true;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                    if (chkhalf == 1)
                                                                                                    {
                                                                                                        if (temp > frshlf)
                                                                                                        {
                                                                                                            allowleave = true;
                                                                                                        }
                                                                                                    }
                                                                                                    else if (chkhalf == 2)
                                                                                                    {
                                                                                                        if (frshlf >= temp)
                                                                                                        {
                                                                                                            allowleave = true;
                                                                                                        }
                                                                                                    }
                                                                                                    if (allowleave == false)
                                                                                                    {
                                                                                                        if (!dtSubType.ContainsKey(alterSubType.Trim().ToUpper()))
                                                                                                        {
                                                                                                            if (staffdegreeclass.Trim() == "")
                                                                                                            {
                                                                                                                staffdegreeclass = dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + "-" + degreename + "-Sem " + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + sect + "- " + text_temp;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                staffdegreeclass = staffdegreeclass + text_temp;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        #endregion
                                                                    }
                                                                    if (altfalg == false)
                                                                    {
                                                                        #region
                                                                        getcolumnfield = Convert.ToString(strday + temp);
                                                                        if (dvsemster.Count > 0)
                                                                        {
                                                                            if (dvsemster[0][getcolumnfield].ToString() != "" && dvsemster[0][getcolumnfield].ToString() != null && dvsemster[0][getcolumnfield].ToString() != "\0")
                                                                            {
                                                                                string timetable = "";
                                                                                string name = dvsemster[0]["ttname"].ToString();
                                                                                if (name != null && name.Trim() != "")
                                                                                {
                                                                                    timetable = name;
                                                                                }
                                                                                sp_rd = dvsemster[0][getcolumnfield].ToString();
                                                                                string[] sp_rd_semi = sp_rd.Split(';');
                                                                                for (int semi = 0; semi <= sp_rd_semi.GetUpperBound(0); semi++)
                                                                                {
                                                                                    string[] sp2 = sp_rd_semi[semi].Split(new Char[] { '-' });
                                                                                    if (sp2.GetUpperBound(0) >= 1)
                                                                                    {
                                                                                        int upperbound = sp2.GetUpperBound(0);
                                                                                        for (int multi_staff = 1; multi_staff < sp2.GetUpperBound(0); multi_staff++)
                                                                                        {
                                                                                            if (sp2[multi_staff] == stafcode)
                                                                                            {
                                                                                                string sect = dsperiodNew.Tables[0].Rows[pre]["sections"].ToString();
                                                                                                if (sect == "-1" || sect == null || sect.Trim() == "")
                                                                                                {
                                                                                                    sect = "$Section ";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    sect = "$Section " + sect + "";
                                                                                                }
                                                                                                if (semenddate.Trim() != "" && semenddate.Trim() != null && semenddate.Trim() != "0")
                                                                                                {
                                                                                                    if (cur_day <= (Convert.ToDateTime(semenddate)))
                                                                                                    {
                                                                                                        Boolean allowleave = false;
                                                                                                        if (hatholiday.Contains(cur_day.ToString()))
                                                                                                        {
                                                                                                            if (moringleav == true)
                                                                                                            {
                                                                                                                if (frshlf >= temp)
                                                                                                                {
                                                                                                                    allowleave = true;
                                                                                                                }
                                                                                                            }
                                                                                                            if (evenleave == true)
                                                                                                            {
                                                                                                                if (temp > frshlf)
                                                                                                                {
                                                                                                                    allowleave = true;
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        if (chkhalf == 1)
                                                                                                        {
                                                                                                            if (temp > frshlf)
                                                                                                            {
                                                                                                                allowleave = true;
                                                                                                            }
                                                                                                        }
                                                                                                        else if (chkhalf == 2)
                                                                                                        {
                                                                                                            if (frshlf >= temp)
                                                                                                            {
                                                                                                                allowleave = true;
                                                                                                            }
                                                                                                        }
                                                                                                        if (allowleave == false)
                                                                                                        {
                                                                                                            double Num;
                                                                                                            bool isNum = double.TryParse(sp2[0].ToString(), out Num);
                                                                                                            if (isNum)
                                                                                                            {
                                                                                                                dssubject.Tables[0].DefaultView.RowFilter = " subject_no=" + sp2[0] + "";
                                                                                                                dvsubject = dssubject.Tables[0].DefaultView;
                                                                                                                if (dvsubject.Count > 0)
                                                                                                                {
                                                                                                                    string hourset = "";
                                                                                                                    //poo
                                                                                                                    bool MornLeave = false;
                                                                                                                    bool EvenLeave = false;
                                                                                                                    if (DateMornOrEvenLeaveDic.Count > 0)
                                                                                                                    {
                                                                                                                        if (DateMornOrEvenLeaveDic.ContainsKey(cur_day.ToString("MM/dd/yyyy")))
                                                                                                                        {
                                                                                                                            if (Convert.ToString(DateMornOrEvenLeaveDic[cur_day.ToString("MM/dd/yyyy")]).Contains('$'))
                                                                                                                            {
                                                                                                                                string[] MornOrEven = Convert.ToString(DateMornOrEvenLeaveDic[cur_day.ToString("MM/dd/yyyy")]).Split('$');
                                                                                                                                if (MornOrEven.Length > 1)
                                                                                                                                {
                                                                                                                                    MornLeave = Convert.ToString(MornOrEven[0]) == "1" ? true : false;
                                                                                                                                    EvenLeave = Convert.ToString(MornOrEven[1]) == "1" ? true : false;
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        MornLeave = true;
                                                                                                                        EvenLeave = true;
                                                                                                                    }
                                                                                                                    if (Convert.ToInt32(temp) <= frshlf && MornLeave == true)
                                                                                                                    {
                                                                                                                        //if (setdate == "")barath added 04.12.17
                                                                                                                        //{
                                                                                                                        setdate = cur_day.ToString("dd/MM/yyyy");
                                                                                                                        hourset = " Date :" + setdate + "$ Hour " + temp.ToString();
                                                                                                                        //}
                                                                                                                        //else
                                                                                                                        //{
                                                                                                                        //    hourset = " * Hour " + temp.ToString();
                                                                                                                        //}
                                                                                                                        text_temp = hourset + " #Subject : " + dvsubject[0]["subject_name"].ToString() + "+" + dvsubject[0]["subject_no"];
                                                                                                                        alterSubType = Convert.ToString(dvsubject[0]["subject_type"]);
                                                                                                                    }
                                                                                                                    else if (Convert.ToInt32(temp) > schlf && EvenLeave == true)//delsi3001
                                                                                                                    {
                                                                                                                        //if (setdate == "")barath added 04.12.17
                                                                                                                        //{
                                                                                                                        setdate = cur_day.ToString("dd/MM/yyyy");
                                                                                                                        hourset = " Date :" + setdate + "$ Hour " + temp.ToString();
                                                                                                                        //}
                                                                                                                        //else
                                                                                                                        //{
                                                                                                                        //    hourset = " * Hour " + temp.ToString();
                                                                                                                        //}
                                                                                                                        text_temp = hourset + " #Subject : " + dvsubject[0]["subject_name"].ToString() + "+" + dvsubject[0]["subject_no"];
                                                                                                                        alterSubType = Convert.ToString(dvsubject[0]["subject_type"]);
                                                                                                                    }
                                                                                                                    //$ symbol 04/12/17
                                                                                                                    //poo
                                                                                                                    //if (setdate == "")
                                                                                                                    //{
                                                                                                                    //    setdate = cur_day.ToString("dd/MM/yyyy");
                                                                                                                    //    hourset = " Date :" + setdate + "$ Hour " + temp.ToString();
                                                                                                                    //}
                                                                                                                    //else
                                                                                                                    //{
                                                                                                                    //    hourset = " * Hour " + temp.ToString();
                                                                                                                    //}
                                                                                                                    //text_temp = hourset + " #Subject : " + dvsubject[0]["subject_name"].ToString() + "+" + dvsubject[0]["subject_no"];
                                                                                                                    //29.12.2019
                                                                                                                    //alterSubType = Convert.ToString(dvsubject[0]["subject_type"]);
                                                                                                                    //alterSubType = Convert.ToString(d2.GetFunction(" select distinct  sm.subject_type from sub_sem sm,subject s where sm.subType_no=s.subType_No and s.subject_name ='" + dvsubject[0]["subject_name"] + "'"));
                                                                                                                }
                                                                                                            }
                                                                                                            if (!string.IsNullOrEmpty(alterSubType))
                                                                                                            {
                                                                                                                if (!dtSubType.ContainsKey(alterSubType.Trim().ToUpper()))
                                                                                                                {
                                                                                                                    if (staffdegreeclass.Trim() == "")
                                                                                                                        staffdegreeclass = dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + " $ " + degreename + "$Sem " + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + sect + " $ " + text_temp;
                                                                                                                    else
                                                                                                                        staffdegreeclass = staffdegreeclass + "@" + dsperiodNew.Tables[0].Rows[pre]["batch_year"].ToString() + " $ " + degreename + "$Sem " + dsperiodNew.Tables[0].Rows[pre]["semester"].ToString() + sect + " $ " + text_temp;
                                                                                                                    //  staffdegreeclass = staffdegreeclass + text_temp; added Barath 04.12.17
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (staffdegreeclass.Trim() != "")
                            {
                                if (stafftakenclass == "")
                                    stafftakenclass = staffdegreeclass;
                                else
                                    stafftakenclass = stafftakenclass + " @ " + staffdegreeclass;
                            }
                        lb1: tmp_camprevar = Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["batch_year"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["degree_code"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["semester"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]);
                        }
                    }
                }
            }
            if (stafftakenclass.Trim() != "" && alterrigths == "1")
            {
                // loadGridAlterStaff(ref  stafftakenclass, staffcode);
                Div3.Visible = true;
                Label12.Text = "Please allot alternate schedule for this staff. " + stafftakenclass;
                flag = true;
            }
            else
            {
                if (stafftakenclass.Trim() != "")
                {
                    string sd = MorningDayandEvening;
                    //int newhour = 0;
                    //var chkeve = (CheckBox)GV1.Rows[0].FindControl("chk_evng");
                    //string sp1 = stafftakenclass.Split('#')[0]; // poo
                    //string sp2 = sp1.Split('$')[6]; //poo
                    //string hour = sp2.Split(' ')[2];
                    //if (chkeve.Checked == true)
                    //{
                    //    int.TryParse(hour, out newhour);
                    //    if (newhour > 5)
                    //    {
                    //loadGridAlterStaff(ref  stafftakenclass);
                    bool checkval = afteralterSchedule();
                    if (saveOrAlter)
                        loadGridAlterStaff(ref  stafftakenclass, staffcode, fdate, tdate, saveOrAlter);
                    if (!checkval)
                    {
                        //Div3.Visible = true;
                        //Label12.Text = "Please allot alternate schedule for this staff. " + stafftakenclass; 
                        bool validt = loadGridAlterStaff(ref  stafftakenclass, staffcode, fdate, tdate, saveOrAlter);
                        if (validt)
                        {
                            binddept();
                            binddesignation();
                            loadstafftype();
                            loadcategory();
                            loadStaffid();
                            flag = true;
                        }
                        else
                            flag = false;
                    }
                }
            }
        }
        //    }
        //}
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    protected Dictionary<string, string> dictSubType()
    {
        Dictionary<string, string> dictSub = new Dictionary<string, string>();
        try
        {
            string SelSubType = " select value from Master_Settings where settings='AlternateSubjectRights'  and usercode='" + usercode + "' and isnull(value,'')<>''";
            DataSet dssub = d2.select_method_wo_parameter(SelSubType, "Text");
            if (dssub.Tables.Count > 0 && dssub.Tables[0].Rows.Count > 0)
            {
                string SubValue = Convert.ToString(dssub.Tables[0].Rows[0]["value"]);
                string[] splSubValue = SubValue.Split(',');
                if (!string.IsNullOrEmpty(SubValue))
                {
                    for (int i = 0; i < splSubValue.Length; i++)
                    {
                        dictSub.Add(Convert.ToString(splSubValue[i].Trim().ToUpper()), Convert.ToString(i));
                    }
                }
            }
        }
        catch { dictSub.Clear(); }
        return dictSub;
    }

    public void txt_staff_code_TextChanged(object sender, EventArgs e)
    {
        try
        {
            chkrelived = 0;
            string staffcode = Convert.ToString(txt_staff_code.Text);
            string chk = "";
            chk = d2.GetFunction("select distinct s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and resign =0 and s.staff_code='" + txt_staff_code.Text + "'");
            if (chk == "0")
            {
                txt_staff_code.Text = "";
                imgdiv2.Visible = true;
                lbl_alert.Text = "Select The Correct Staff Code";
                return;
            }
            string query = "select appl_name, s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.staff_code = '" + staffcode + "' and d.collegeCode=m.college_code";
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_staff_name.Text = Convert.ToString(ds.Tables[0].Rows[i]["appl_name"]);
                    txt_dep.Text = Convert.ToString(ds.Tables[0].Rows[i]["dept"]);
                    txt_des.Text = Convert.ToString(ds.Tables[0].Rows[i]["design"]);
                    txt_staff_code.Text = staffcode;
                    imagestaff.ImageUrl = "~/Handler/staffphoto.ashx?Staff_Code=" + txt_staff_code.Text;
                    string photo = d2.GetFunction("select photo from staffphoto where staff_code='" + txt_staff_code.Text + "'");
                    if (photo == "0")
                    {
                        imagestaff.ImageUrl = "image/Gender Neutral User Filled-100(1).png";
                    }
                }
                bindgrid2();
                BindLeave();
                if (requestpermissioncheck != "3")
                {
                    bindgrid_approvalstaff();
                }
                BindGridview();
                gridView2.Visible = true;
                //  altersubject();
                //alternate schedule changed based on settings
                if (alterrigths == "1")
                    altersubject();
                else if (alterrigths == "2")
                    afteralterSchedule();
                //if hod staff available to mention alternate incharge staff
                getInchargeStaff(staffcode);
            }
        }
        catch
        {
        }
    }

    public void txt_staff_name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            chkrelived = 0;
            string staffname = Convert.ToString(txt_staff_name.Text).Split('-')[0].Trim();
            string chk = "";
            chk = d2.GetFunction("select distinct s.staff_name from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and resign =0 and s.staff_name='" + staffname + "'");
            if (chk == "0")
            {
                txt_staff_name.Text = "";
                imgdiv2.Visible = true;
                lbl_alert.Text = "Select The Correct Staff Name";
                return;
            }
            string query = "select m.staff_code, s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.staff_name = '" + staffname + "'";
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_staff_name.Text = staffname;
                    txt_dep.Text = Convert.ToString(ds.Tables[0].Rows[i]["dept"]);
                    txt_des.Text = Convert.ToString(ds.Tables[0].Rows[i]["design"]);
                    txt_staff_code.Text = Convert.ToString(ds.Tables[0].Rows[i]["staff_code"]);
                    imagestaff.ImageUrl = "~/Handler/staffphoto.ashx?Staff_Code=" + txt_staff_code.Text;
                    string photo = d2.GetFunction("select photo from staffphoto where staff_code='" + txt_staff_code.Text + "'");
                    if (photo == "0")
                    {
                        imagestaff.ImageUrl = "image/Gender Neutral User Filled-100(1).png";
                    }
                    bindgrid2();
                    BindLeave();
                    if (requestpermissioncheck != "3")
                    {
                        bindgrid_approvalstaff();
                    }
                    BindGridview();
                    gridView2.Visible = true;
                    //alternate schedule changed based on settings
                    if (alterrigths == "1")
                        altersubject();
                    else if (alterrigths == "2")
                        afteralterSchedule();
                    //  altersubject();
                }
            }
        }
        catch
        {
        }
    }

    public void leave_clear()
    {
        txt_staff_code.Text = "";
        txt_staff_name.Text = "";
        txt_dep.Text = "";
        txt_des.Text = "";
        ddl_leave_type.Items.Clear();
        //txt_reason.Text = "";
        txtleavereason.Text = "";
        gridView2.Visible = false;
    }

    public string subjectcode(string textcri, string subjename)
    {
        string subjec_no = "";
        try
        {
            string select_subno = "select TextCode from textvaltable where TextCriteria='" + textcri + "' and college_code =" + collegecode1 + " and TextVal='" + subjename + "'";
            ds.Clear();
            ds = d2.select_method_wo_parameter(select_subno, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                subjec_no = Convert.ToString(ds.Tables[0].Rows[0]["TextCode"]);
            }
            else
            {
                string insertquery = "insert into textvaltable(TextCriteria,TextVal,college_code) values('" + textcri + "','" + subjename + "','" + collegecode1 + "')";
                int result = d2.update_method_wo_parameter(insertquery, "Text");
                if (result != 0)
                {
                    string select_subno1 = "select TextCode from textvaltable where TextCriteria='" + textcri + "' and college_code =" + collegecode1 + " and TextVal='" + subjename + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(select_subno1, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        subjec_no = Convert.ToString(ds.Tables[0].Rows[0]["TextCode"]);
                    }
                }
            }
        }
        catch
        {
        }
        return subjec_no;
    }

    //Added by Sraranyadevi
    protected void btn_go3_Click(object sender, EventArgs e)
    {
        try
        {
            ds.Clear();
            if (ViewState["selecteditems"] != null)
            {
                DataTable dnew = (DataTable)ViewState["selecteditems"];
                ViewState["sb"] = dnew;
                checknew = "s";
            }

            string itemheadercode = "";
            for (int i = 0; i < cbl_itemheader3.Items.Count; i++)
            {
                if (cbl_itemheader3.Items[i].Selected == true)
                {
                    if (itemheadercode == "")
                    {
                        itemheadercode = "" + cbl_itemheader3.Items[i].Value.ToString() + "";
                    }
                    else
                    {
                        itemheadercode = itemheadercode + "'" + "," + "'" + cbl_itemheader3.Items[i].Value.ToString() + "";
                    }
                }
            }
            string itemheadercode1 = "";
            for (int i = 0; i < chklst_pop2itemtyp.Items.Count; i++)
            {
                if (chklst_pop2itemtyp.Items[i].Selected == true)
                {
                    if (itemheadercode1 == "")
                    {
                        itemheadercode1 = "" + chklst_pop2itemtyp.Items[i].Value.ToString() + "";
                    }
                    else
                    {
                        itemheadercode1 = itemheadercode1 + "'" + "," + "'" + chklst_pop2itemtyp.Items[i].Value.ToString() + "";
                    }
                }
            }


            string box = "";
            string selectquery = "";
            string qryitem = "";
            if (gvdatass.Visible == true)
            {
                string itcode1 = "";
                string selecquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemCode='" + txt_searchitemcode.Text + "' or ItemName='" + txt_searchby.Text + "' or ItemHeaderName='" + txt_searchheadername.Text + "' order by ItemCode";
                DataSet dtitem = d2.select_method_wo_parameter(selecquery, "Text");
                if (dtitem.Tables.Count > 0 && dtitem.Tables[0].Rows.Count > 0)
                    itcode1 = Convert.ToString(dtitem.Tables[0].Rows[0]["ItemCode"]);
                for (int i = 0; i < gvdatass.Items.Count; i++)
                {
                    Label box1 = (Label)gvdatass.Items[i].FindControl("lbl_itemcode");
                    box = Convert.ToString(box1.Text);
                    if (lbl_error3.Visible == false)
                    {
                        if (box.Trim() != itcode1)
                        {
                            selectquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemCode='" + txt_searchitemcode.Text + "' or ItemName='" + txt_searchby.Text + "' or ItemHeaderName='" + txt_searchheadername.Text + "' order by ItemCode";
                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "This Item Already Added";
                            txt_searchitemcode.Text = "";
                            txt_searchby.Text = "";
                            txt_searchheadername.Text = "";
                            return;
                        }
                    }
                }

            }
            if (txt_searchby.Text.Trim() != "")
            {
                if (box.Trim() != txt_searchby.Text)
                {
                    //selectquery = "select itemheader_name,itemheader_code,item_code,item_name ,model_name,Size_name ,item_unit,description ,special_instru from Item_Master where item_name='" + txt_searchby.Text + "' order by item_code";
                    selectquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemName='" + txt_searchby.Text + "' order by ItemCode";

                }
            }
            else if (txt_searchitemcode.Text.Trim() != "")
            {
                selectquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemCode='" + txt_searchitemcode.Text + "' order by ItemCode";


            }
            else if (txt_searchheadername.Text.Trim() != "")
            {
                // selectquery = "select itemheader_name,itemheader_code,item_code,item_name ,model_name,Size_name ,item_unit,description ,special_instru from Item_Master where itemheader_name='" + txt_searchheadername.Text + "' order by item_code";
                selectquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemHeaderName='" + txt_searchheadername.Text + "' order by ItemCode";

            }
            else if (itemheadercode.Trim() != "" && itemheadercode1.Trim() != "")
            {
                //selectquery = "select distinct  item_code ,item_name , itemheader_code,itemheader_name,item_unit from item_master where itemheader_code in ('" + itemheadercode + "') and item_code in ('" + itemheadercode1 + "') order by item_code ";
                selectquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemHeaderCode in ('" + itemheadercode + "') and ItemCode in ('" + itemheadercode1 + "') order by ItemCode ";
            }

            if (txt_itemheader3.Text.Trim() != "--Select--" && txt_itemname3.Text.Trim() != "--Select--" || txt_searchby.Text.Trim() != "" || txt_searchitemcode.Text.Trim() != "" || txt_searchheadername.Text.Trim() != "")
            {
                if (selectquery.Trim() != "")
                {

                    DataRow dr = null;
                    if (ViewState["selecteditem"] == null)
                    {
                        data.Columns.Add("ItemName");
                        data.Columns.Add("ItemCode");
                        data.Columns.Add("ItemHeaderName");
                        data.Columns.Add("ItemHeaderCode");
                        data.Columns.Add("ItemUnit");
                        ds.Clear();
                        ds = d2.select_method_wo_parameter(selectquery, "Text");
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                dr = data.NewRow();
                                dr[0] = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]);
                                dr[1] = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                                data.Rows.Add(dr);
                            }
                        }
                    }
                    else
                    {
                        data = (DataTable)ViewState["selecteditem"];
                        ds.Clear();
                        ds = d2.select_method_wo_parameter(selectquery, "Text");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                dr = data.NewRow();
                                dr[0] = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]);
                                dr[1] = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                                data.Rows.Add(dr);
                            }
                        }

                    }
                    if (data.Rows.Count > 0)
                    {
                        gvdatass.DataSource = data;
                        gvdatass.DataBind();
                        gvdatass.Visible = true;
                        ViewState["selecteditem"] = data;
                        div2.Visible = true;
                        btn_itemsave4.Visible = true;
                        btn_conexist4.Visible = true;
                        lbl_error3.Visible = false;
                        selectedmenuchk(sender, e);
                    }
                }
            }
            else
            {
                lbl_error3.Visible = true;
                lbl_error3.Text = "Please select all fields";
                div2.Visible = false;
                btn_itemsave4.Visible = false;
                btn_conexist4.Visible = false;
            }
            //foreach (DataListItem gvrow in gvdatass.Items)
            //{
            //    CheckBox chkSelect = (gvrow.FindControl("CheckBox2") as CheckBox);
            //        chkSelect.Enabled = false;
            //}
            txt_searchby.Text = "";
            txt_searchitemcode.Text = "";
            txt_searchheadername.Text = "";
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    protected void btn_conexit4_Click(object sender, EventArgs e)
    {
        gvdatass.Visible = false;
        popwindow1.Visible = false;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getitemheader1(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct ItemHeaderName from IM_ItemMaster WHERE ItemHeaderName like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["ItemHeaderName"].ToString());
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getitemcode1(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct ItemCode from IM_ItemMaster WHERE ItemCode like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["ItemCode"].ToString());
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getnamemm(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct ItemName from IM_ItemMaster WHERE ItemName like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["ItemName"].ToString());
            }
        }
        return name;
    }

    public void dept()
    {
        string query = "select Dept_Name as DeptName,Dept_Code from Department ";
        ds = d2.select_method_wo_parameter(query, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string dd = Convert.ToString(ds.Tables[0].Rows[i]["DeptName"]);
                string ddd = Convert.ToString(ds.Tables[0].Rows[i]["Dept_Code"]);
                if (!depthash.Contains(Convert.ToString(dd)))
                {
                    depthash.Add(Convert.ToString(dd), Convert.ToString(ddd));
                }
            }
        }
    }

    public void imgbtn_event_Click(object sender, EventArgs e)
    {
        try
        {
            td_item.BgColor = "white";
            td_sev.BgColor = "white";
            td_vist.BgColor = "white";
            td_comp.BgColor = "white";
            td_lv.BgColor = "white";
            td_othr.BgColor = "white";
            td_event.BgColor = "#c4c4c4";
            Response.Redirect("EventRequest.aspx", false);
        }
        catch
        {
        }
    }

    public void imgbtn2close_Click(object sender, EventArgs e)
    {
        popwindow2.Visible = false;
    }

    protected void bindpop1college()
    {
        string clgname = "select college_code,collname from collinfo ";
        if (clgname != "")
        {
            ds = d2.select_method(clgname, hat, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_pop2collgname.DataSource = ds;
                ddl_pop2collgname.DataTextField = "collname";
                ddl_pop2collgname.DataValueField = "college_code";
                ddl_pop2collgname.DataBind();
            }
        }
    }

    public void branch1()
    {
        try
        {
            string query1 = "";
            string buildvalue1 = "";
            string build1 = "";
            if (ddl_pop2degre.Items.Count > 0)
            {
                for (int i = 0; i < ddl_pop2degre.Items.Count; i++)
                {
                    build1 = ddl_pop2degre.SelectedValue;
                    if (buildvalue1 == "")
                    {
                        buildvalue1 = build1;
                    }
                    else
                    {
                        buildvalue1 = buildvalue1 + "'" + "," + "'" + build1;
                    }
                }
                query1 = "select distinct degree.degree_code,department.dept_name,degree.Acronym  from degree,department,course,deptprivilages where course.course_id=degree.course_id  and department.dept_code=degree.dept_code and course.college_code = degree.college_code and department.college_code = degree.college_code and degree.course_id in('" + buildvalue1 + "') and degree.college_code='" + ddl_pop2collgname.SelectedValue + "' and deptprivilages.Degree_code=degree.Degree_code";
                ds = d2.select_method(query1, hat, "Text");
                ddl_pop2branch.DataSource = ds;
                ddl_pop2branch.DataTextField = "dept_name";
                ddl_pop2branch.DataValueField = "degree_code";
                ddl_pop2branch.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void ddl_pop2degre_SelectedIndexChanged(object sender, EventArgs e)
    {
        branch1();
    }

    protected void bindpop2batchyear()
    {
        try
        {
            //ddlpop2batchyr.Items.Clear();
            hat.Clear();
            string sqlyear = "select distinct batch_year from Registration where batch_year<>'-1' and batch_year<>'' and cc=0 and delflag=0 and exam_flag<>'debar' order by batch_year desc";
            ds = d2.select_method(sqlyear, hat, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_pop2batchyear.DataSource = ds;
                ddl_pop2batchyear.DataTextField = "batch_year";
                ddl_pop2batchyear.DataValueField = "batch_year";
                ddl_pop2batchyear.DataBind();
            }
        }
        catch
        {
        }
    }

    public void btn_pop2go_Click(object sender, EventArgs e)
    {
        loadroll();
        //lblpop2error.Visible = true;
        //lblpop2error.Text = "Please select Required field";
    }

    public void loadroll()
    {
        try
        {
            if (ddl_pop2sex.SelectedItem.Text == "All")
            {
                sqladd = "select roll_no,r.stud_name,course_name+'-'+dept_name branch,roll_admit from Registration r,applyn a,Degree g,course c,Department d where cc=0 and delflag=0 and exam_flag!='debar'and r.degree_code = g.Degree_Code and r.App_No = a.app_no and g.Course_Id = c.Course_Id and g.college_code = c.college_code and g.Dept_Code = d.Dept_Code  and g.college_code = d.college_code and r.Batch_Year ='" + ddl_pop2batchyear.SelectedItem + "' and roll_no not in (select roll_no from Hostel_StudentDetails where ISNULL(roll_no,'')<>'')";
                ldroll();
            }
            else if (ddl_pop2sex.SelectedItem.Text == "Male")
            {
                sqladd = "select roll_no,r.stud_name,course_name+'-'+dept_name branch,roll_admit from Registration r,applyn a,Degree g,course c,Department d where cc=0 and delflag=0 and exam_flag!='debar'and r.degree_code = g.Degree_Code and r.App_No = a.app_no and g.Course_Id = c.Course_Id and g.college_code = c.college_code and g.Dept_Code = d.Dept_Code and a.sex ='0' and g.college_code = d.college_code and r.Batch_Year ='" + ddl_pop2batchyear.SelectedItem + "' and roll_no not in (select roll_no from Hostel_StudentDetails where ISNULL(roll_no,'')<>'')";
                ldroll();
            }
            else if (ddl_pop2sex.SelectedItem.Text == "Female")
            {
                sqladd = "select roll_no,r.stud_name,course_name+'-'+dept_name branch,roll_admit from Registration r,applyn a,Degree g,course c,Department d where cc=0 and delflag=0 and exam_flag!='debar'and r.degree_code = g.Degree_Code and r.App_No = a.app_no and g.Course_Id = c.Course_Id and g.college_code = c.college_code and g.Dept_Code = d.Dept_Code and a.sex ='1' and g.college_code = d.college_code and r.Batch_Year ='" + ddl_pop2batchyear.SelectedItem + "' and roll_no not in (select roll_no from Hostel_StudentDetails where ISNULL(roll_no,'')<>'')";
                ldroll();
            }
            else
            {
                sqladd = "select roll_no,r.stud_name,course_name+'-'+dept_name branch,roll_admit from Registration r,applyn a,Degree g,course c,Department d where cc=0 and delflag=0 and exam_flag!='debar'and r.degree_code = g.Degree_Code and r.App_No = a.app_no and g.Course_Id = c.Course_Id and g.college_code = c.college_code and g.Dept_Code = d.Dept_Code and a.sex ='2' and g.college_code = d.college_code and r.Batch_Year ='" + ddl_pop2batchyear.SelectedItem + "' and roll_no not in (select roll_no from Hostel_StudentDetails where ISNULL(roll_no,'')<>'')";
                ldroll();
            }
        }
        catch (Exception ex)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = ex.ToString();
        }
    }

    public void ldroll()
    {
        try
        {
            if (ddl_pop2branch.Items.Count > 0)
            {
                string buildvalue1 = "";
                string build1 = "";
                build1 = ddl_pop2branch.SelectedValue;
                if (buildvalue1 == "")
                {
                    buildvalue1 = build1;
                }
                else
                {
                    buildvalue1 = buildvalue1 + "'" + "," + "'" + build1;
                }
                if (buildvalue1 != "")
                {
                    sqladd = sqladd + " AND g.degree_code in ('" + buildvalue1 + "')";
                }
                else
                {
                    sqladd = sqladd + "";
                }
            }
            if (ddl_pop2studenttype.SelectedItem.Text != "")
            {
                string buildvalue2 = "";
                string build2 = "";
                build2 = ddl_pop2studenttype.SelectedValue.ToString();
                if (buildvalue2 == "")
                {
                    buildvalue2 = build2;
                }
                else
                {
                    buildvalue2 = buildvalue2 + "'" + "," + "'" + build2;
                }
                if (buildvalue2 != "")
                {
                    sqladd = sqladd + " AND r.stud_type in ('" + buildvalue2 + "')";
                }
                else
                {
                    sqladd = sqladd + "";
                }
            }
            // fproll.Columns[1].Visible = true;
            // fproll.Columns[2].Visible = false;
            //roll1 = Convert.ToString(Session["Rollflag"].ToString());
            if (Rollflag1 == "1")
            {
                fproll.Columns[1].Visible = true;
                fproll.Width = 787;
            }
            else
            {
                fproll.Columns[1].Visible = false;
                fproll.Width = 666;
            }
            string strorderby = d2.GetFunction("select value from Master_Settings where settings='order_by'");
            if (strorderby == "")
            {
                strorderby = "";
            }
            else
            {
                if (strorderby == "0")
                {
                    strorderby = "ORDER BY r.Roll_No";
                }
                else if (strorderby == "2")
                {
                    strorderby = "ORDER BY r.Stud_Name";
                }
                else if (strorderby == "0,2")
                {
                    strorderby = "ORDER BY r.Roll_No,r.Stud_Name";
                }
                else
                {
                    strorderby = "";
                }
            }
            fproll.Sheets[0].RowCount = 0;
            fproll.Sheets[0].RowHeader.Visible = false;
            fproll.SaveChanges();
            fproll.Sheets[0].AutoPostBack = false;
            ds.Clear();
            string q = sqladd + strorderby;
            ds = d2.select_method_wo_parameter(q, "Text");
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //div2.Visible = false;
                fproll.Visible = false;
                lblcounttxt.Visible = false;
                lblcount.Visible = false;
                lblpop2error.Visible = true;
                lblpop2error.Text = "No Students Found Or Roll numbers might not be generated";
                btn_pop2ok.Visible = false;
                btn_pop2exit.Visible = false;
            }
            else
            {
                FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                darkstyle.ForeColor = Color.White;
                fproll.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fproll.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;
                    fproll.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;
                    fproll.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;
                    fproll.Sheets[0].ColumnHeader.Cells[0, 3].HorizontalAlign = HorizontalAlign.Center;
                    fproll.Sheets[0].ColumnHeader.Cells[0, 4].HorizontalAlign = HorizontalAlign.Center;
                    //fproll.Sheets[0].ColumnHeader.Cells[0, 5].HorizontalAlign = HorizontalAlign.Center;
                    //fproll.Sheets[0].ColumnHeader.Cells[0, 6].HorizontalAlign = HorizontalAlign.Center;
                    int sno = 0;
                    lblpop2error.Visible = false;
                    lblcounttxt.Visible = true;
                    lblcounttxt.Text = "No of student:";
                    lblcount.Visible = true;
                    lblcount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                    fproll.Visible = true;
                    //div2.Visible = true;
                    fproll.CommandBar.Visible = false;
                    btn_pop2ok.Visible = true;
                    btn_pop2exit.Visible = true;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sno++;
                        string roll = ds.Tables[0].Rows[i]["roll_no"].ToString();
                        string name = ds.Tables[0].Rows[i]["stud_name"].ToString();
                        string dept = ds.Tables[0].Rows[i]["branch"].ToString();
                        string admin = ds.Tables[0].Rows[i]["roll_admit"].ToString();
                        //comm = ds.Tables[0].Rows[i]["textval"].ToString();
                        fproll.Sheets[0].RowCount = fproll.Sheets[0].RowCount + 1;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 0].Font.Bold = false;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(sno);
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 1].Text = roll;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 1].Font.Bold = false;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 2].Text = admin;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 2].Font.Bold = false;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 3].Text = name;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 3].Font.Bold = false;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 4].Text = dept;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 4].Font.Bold = false;
                        fproll.Sheets[0].Cells[fproll.Sheets[0].RowCount - 1, 4].HorizontalAlign = HorizontalAlign.Left;
                    }
                    //fproll.Sheets[0].SetColumnMerge(4, FarPoint.Web.Spread.Model.MergePolicy.Always);
                    int rowcount = fproll.Sheets[0].RowCount;
                    fproll.Height = 270;
                    fproll.Sheets[0].PageSize = 15 + (rowcount * 5);
                    fproll.SaveChanges();
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void btn_pop2exit_Click(object sender, EventArgs e)
    {
        popwindow2.Visible = false;
    }

    public void btn_pop2ok_Click(object sender, EventArgs e)
    {
    }

    public void degree()
    {
        try
        {
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
            ds = d2.select_method("bind_degree", hat, "sp");
            int count1 = ds.Tables[0].Rows.Count;
            if (count1 > 0)
            {
                ddl_pop2degre.DataSource = ds;
                ddl_pop2degre.DataTextField = "course_name";
                ddl_pop2degre.DataValueField = "course_id";
                ddl_pop2degre.DataBind();
                //cbl_degree.DataSource = ds;
                //cbl_degree.DataTextField = "course_name";
                //cbl_degree.DataValueField = "course_id";
                //cbl_degree.DataBind();
                //if (cbl_degree.Items.Count > 0)
                //{
                //    for (int i = 0; i < cbl_degree.Items.Count; i++)
                //    {
                //        cbl_degree.Items[i].Selected = true;
                //    }
                //    txt_degree.Text = "Degree(" + cbl_degree.Items.Count + ")";
                //}
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void gv5_Bound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // string ddlvalue = ddlmarkgradeug.SelectedItem.Text;
            if (e.Row.RowIndex == 0)
            {
                e.Row.Cells[6].Visible = false;
            }
            else
            {
                e.Row.Cells[6].Visible = true;
            }
        }
    }

    protected void gv6_Bound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // string ddlvalue = ddlmarkgradeug.SelectedItem.Text;
            if (e.Row.RowIndex == 0)
            {
                e.Row.Cells[5].Visible = false;
            }
            else
            {
                e.Row.Cells[5].Visible = true;
            }
        }
    }

    public void pop_cataddclose_Click(object sender, EventArgs e)
    {
        // pop_catadd.Visible = false;
    }

    public void btndescpopadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_description11.Text != "")
            {
                string sql = "if exists ( select * from CO_MasterValues where MasterValue ='" + txt_description11.Text + "' and MasterCriteria ='Action' and CollegeCode ='" + collegecode1 + "') update CO_MasterValues set MasterValue ='" + txt_description11.Text + "' where MasterValue ='" + txt_description11.Text + "' and MasterCriteria ='Action' and CollegeCode ='" + collegecode1 + "' else insert into CO_MasterValues (MasterValue,MasterCriteria,CollegeCode) values ('" + txt_description11.Text + "','Action','" + collegecode1 + "')";
                int insert = d2.update_method_wo_parameter(sql, "TEXT");
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    pnl2.Visible = true;
                    lbl_alert.Text = "Added sucessfully";
                    txt_description11.Text = "";
                    imgdiv3.Visible = false;
                    panel_description.Visible = false;
                }
            }
            else
            {
                imgdiv2.Visible = true;
                pnl2.Visible = true;
                lbl_alert.Text = "Enter the description";
            }
        }
        catch (Exception ex)
        {
        }
        //pop_add_staff_stud_othr.Visible = true;
        //imgdiv33.Visible = false;
        //panel_description11.Visible = false;
    }

    public void btndescpopexit_Click(object sender, EventArgs e)
    {
        imgdiv33.Visible = false;
        panel_description11.Visible = false;
    }

    public string subjectcodenew(string textcri, string subjename)
    {
        string subjec_no = "";
        try
        {
            string select_subno = "select MasterCode from CO_MasterValues where MasterCriteria='" + textcri + "' and CollegeCode =" + collegecode1 + " and MasterValue='" + subjename + "'";
            ds.Clear();
            ds = d2.select_method_wo_parameter(select_subno, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                subjec_no = Convert.ToString(ds.Tables[0].Rows[0]["MasterCode"]);
            }
            else
            {
                string insertquery = "insert into CO_MasterValues(MasterCriteria,MasterValue,CollegeCode) values('" + textcri + "','" + subjename + "','" + collegecode1 + "')";
                int result = d2.update_method_wo_parameter(insertquery, "Text");
                if (result != 0)
                {
                    string select_subno1 = "select MasterCode from CO_MasterValues where MasterCriteria='" + textcri + "' and CollegeCode =" + collegecode1 + " and MasterValue='" + subjename + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(select_subno1, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        subjec_no = Convert.ToString(ds.Tables[0].Rows[0]["MasterCode"]);
                    }
                }
            }
        }
        catch
        {
        }
        return subjec_no;
    }

    public void access()
    {
        try
        {
            string query = "";
            string Master1 = "";
            sms_req = string.Empty;
            sms_app = string.Empty;
            sms_exit = string.Empty;
            sms_reject = string.Empty;
            if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
            {
                string group = Session["group_code"].ToString();
                if (group.Contains(';'))
                {
                    string[] group_semi = group.Split(';');
                    Master1 = group_semi[0].ToString();
                    query = "select * from Master_Settings where settings ='SMS Rights' and group_code ='" + Master1 + "'"; // poo 12.12.17
                }
                else
                    query = "select * from Master_Settings where settings ='SMS Rights' and group_code ='" + group + "'"; // poo 12.12.17
            }
            else
            {
                Master1 = Session["usercode"].ToString();
                query = "select * from Master_Settings where settings ='SMS Rights' and usercode ='" + Master1 + "'";
            }
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string val = Convert.ToString(ds.Tables[0].Rows[i]["value"]);
                    string[] split1 = val.Split('-');
                    string[] split = split1[0].Split(',');
                    for (int u = 0; u < split.Length; u++)
                    {
                        if (split[u] == "1")
                            sms_req = "1";
                        if (split[u] == "2")
                            sms_app = "2";
                        if (split[u] == "3")
                            sms_exit = "3";
                        if (split[u] == "4")
                            sms_reject = "4";
                    }
                }
            }
        }
        catch
        {
        }
    }

    public void access1()
    {
        try
        {
            string query = "";
            string Master1 = "";
            string stud = "";
            string values = "";
            string sms = "";
            string sms1 = "";
            string sms2 = "";
            if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
            {
                string group = Session["group_code"].ToString();
                if (group.Contains(';'))
                {
                    string[] group_semi = group.Split(';');
                    Master1 = group_semi[0].ToString();
                    query = "select * from Master_Settings where settings ='SMS Mobile Rights' and group_code ='" + Master1 + "'";
                }
                else
                    query = "select * from Master_Settings where settings ='SMS Mobile Rights' and group_code ='" + group + "'";
            }
            else
            {
                Master1 = Session["usercode"].ToString();
                query = "select * from Master_Settings where settings ='SMS Mobile Rights' and usercode ='" + Master1 + "'";
            }
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string val = Convert.ToString(ds.Tables[0].Rows[i]["value"]);
                    string[] split = val.Split(',');
                    if (split.Length == 1)
                    {
                        sms = split[0];
                        if (sms == "1")
                        {
                            sms_mom = sms;
                        }
                        else if (sms == "2")
                        {
                            sms_dad = sms;
                        }
                        else if (sms == "3")
                        {
                            sms_stud = sms;
                        }
                    }
                    else if (split.Length == 2)
                    {
                        sms = split[0];
                        sms1 = split[1];
                        if (sms == "1")
                        {
                            sms_mom = sms;
                        }
                        else if (sms == "2")
                        {
                            sms_dad = sms;
                        }
                        else if (sms == "3")
                        {
                            sms_stud = sms;
                        }
                        if (sms1 == "1")
                        {
                            sms_mom = sms1;
                        }
                        else if (sms1 == "2")
                        {
                            sms_dad = sms1;
                        }
                        else if (sms1 == "3")
                        {
                            sms_stud = sms1;
                        }
                    }
                    else
                    {
                        sms = split[0];
                        sms1 = split[1];
                        sms2 = split[2];
                        sms_mom = "1";
                        sms_dad = "2";
                        sms_stud = "3";
                    }
                }
            }
        }
        catch
        {
        }
    }

    public void leave_staff_login()
    {
        leaverequestsetting();
        Int64 ReqStaffAppNo = 0;
        Int64 ReqStaffDeptFK = 0;
        bool Is_Staff;
        if (requestpermissioncheck == "1")
        {
            // Is_Staff = Convert.ToBoolean();
            bool.TryParse(d2.GetFunction("select Is_Staff from UserMaster where User_Code='" + usercode + "' and college_code='" + Session["collegecode"] + "'"), out Is_Staff);
            //Is_Staff = d2.GetFunction("select Is_Staff from UserMaster where User_Code='" + usercode + "' and college_code='" + Session["collegecode"] + "'");
            if (Is_Staff == true)
            {
                string staffcode = d2.GetFunction("select staff_code  from UserMaster where User_Code='" + usercode + "'");
                if (staffcode.Trim() != "")
                {
                    ReqStaffAppNo = Convert.ToInt64(d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staffcode + "'"));
                    Btn_Staff_Code.Visible = false;
                    string query = "select  s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.staff_code = '" + staffcode + "' and m.college_code=d.collegeCode";// added m.college_code=d.collegeCode 18/06
                    ds = d2.select_method_wo_parameter(query, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_staff_name.Text = Convert.ToString(ds.Tables[0].Rows[0]["appl_name"]);
                        txtissueper.Text = Convert.ToString(ds.Tables[0].Rows[0]["appl_name"]);
                        txt_dep.Text = Convert.ToString(ds.Tables[0].Rows[0]["dept"]);
                        txt_des.Text = Convert.ToString(ds.Tables[0].Rows[0]["design"]);
                        txt_staff_code.Text = staffcode;
                        imagestaff.ImageUrl = "~/Handler/staffphoto.ashx?Staff_Code=" + txt_staff_code.Text;
                        BindLeave();
                        bindgrid2();
                        //if hod staff available to mention alternate incharge staff
                        getInchargeStaff(staffcode);
                    }
                    string photo = d2.GetFunction("select photo from staffphoto where staff_code='" + txt_staff_code.Text + "'");
                    if (photo == "0")
                    {
                        imagestaff.ImageUrl = "image/Gender Neutral User Filled-100(1).png";
                    }
                }
            }
            else if (Is_Staff == false)
            {
                ReqStaffAppNo = Convert.ToInt64(usercode);
                Btn_Staff_Code.Visible = false;
            }
        }
    }

    public void abc()
    {
        ArrayList addnew = new ArrayList();
        DateTime fromdate = new DateTime();
        fromdate = TextToDate(txt_frm);
        DateTime todate = new DateTime();
        todate = TextToDate(txt_to);
        string fromdate1 = Convert.ToString(txt_frm.Text);
        // GV1.Visible = true;
        TimeSpan c = fromdate - todate;
        string[] ay = txt_frm.Text.Split('/');
        pri_txt = ay[1].ToString();
        if (pri_txt == "01")
        {
            con_txt = "January";
        }
        if (pri_txt == "02")
        {
            con_txt = "February";
        }
        if (pri_txt == "03")
        {
            con_txt = "March";
        }
        if (pri_txt == "04")
        {
            con_txt = "April";
        }
        if (pri_txt == "05")
        {
            con_txt = "May";
        }
        if (pri_txt == "06")
        {
            con_txt = "June";
        }
        if (pri_txt == "07")
        {
            con_txt = "July";
        }
        if (pri_txt == "08")
        {
            con_txt = "August";
        }
        if (pri_txt == "09")
        {
            con_txt = "September";
        }
        if (pri_txt == "10")
        {
            con_txt = "October";
        }
        if (pri_txt == "11")
        {
            con_txt = "November";
        }
        if (pri_txt == "12")
        {
            con_txt = "December";
        }
    }

    public void abc1()
    {
        if (pri_txt == "1")
        {
            con_txt = "A";
        }
        if (pri_txt == "2")
        {
            con_txt = "B";
        }
        if (pri_txt == "3")
        {
            con_txt = "C";
        }
        if (pri_txt == "4")
        {
            con_txt = "D";
        }
        if (pri_txt == "5")
        {
            con_txt = "E";
        }
        if (pri_txt == "6")
        {
            con_txt = "F";
        }
        if (pri_txt == "7")
        {
            con_txt = "G";
        }
        if (pri_txt == "8")
        {
            con_txt = "H";
        }
        if (pri_txt == "9")
        {
            con_txt = "I";
        }
        if (pri_txt == "10")
        {
            con_txt = "J";
        }
        if (pri_txt == "11")
        {
            con_txt = "K";
        }
        if (pri_txt == "12")
        {
            con_txt = "L";
        }
        if (pri_txt == "13")
        {
            con_txt = "M";
        }
        if (pri_txt == "14")
        {
            con_txt = "N";
        }
        if (pri_txt == "15")
        {
            con_txt = "O";
        }
        if (pri_txt == "16")
        {
            con_txt = "P";
        }
        if (pri_txt == "17")
        {
            con_txt = "Q";
        }
        if (pri_txt == "18")
        {
            con_txt = "R";
        }
        if (pri_txt == "19")
        {
            con_txt = "S";
        }
        if (pri_txt == "20")
        {
            con_txt = "T";
        }
        if (pri_txt == "21")
        {
            con_txt = "U";
        }
        if (pri_txt == "22")
        {
            con_txt = "V";
        }
        if (pri_txt == "23")
        {
            con_txt = "W";
        }
        if (pri_txt == "24")
        {
            con_txt = "X";
        }
        if (pri_txt == "25")
        {
            con_txt = "Y";
        }
        if (pri_txt == "26")
        {
            con_txt = "Z";
        }
    }

    public void cb_own_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_own.Checked == true)
        {
            cb_other.Checked = false;
        }
        else
        {
            cb_other.Checked = true;
        }
    }

    public void cb_other_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_other.Checked == true)
        {
            cb_own.Checked = false;
        }
        else
        {
            cb_own.Checked = true;
        }
    }

    public void cb_serown_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_serown.Checked == true)
        {
            cb_serother.Checked = false;
        }
        else
        {
            cb_serother.Checked = true;
        }
    }

    public void cb_serother_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_serother.Checked == true)
        {
            cb_serown.Checked = false;
        }
        else
        {
            cb_serown.Checked = true;
        }
    }

    public void txt_exdate_TextChanged(object sender, EventArgs e)
    {
        string dt = txt_exdate.Text;
        string[] Split = dt.Split('/');
        DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt = DateTime.Now.ToString("dd/MM/yyyy");
        Split = enddt.Split('/');
        DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        TimeSpan days = fromdate - todate;
        string ndate = Convert.ToString(days);
        Split = ndate.Split('.');
        string getdate = Split[0];
        int finaldate = Convert.ToInt32(getdate);
        if (fromdate > todate)
        {
            txt_exdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    public void txt_serexpdate_TextChanged(object sender, EventArgs e)
    {
        string dt = txt_serexpdate.Text;
        string[] Split = dt.Split('/');
        DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt = DateTime.Now.ToString("dd/MM/yyyy");
        Split = enddt.Split('/');
        DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        if (fromdate > todate)
        {
            txt_serexpdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    public void txt_visitdate_TextChanged(object sender, EventArgs e)
    {
        string dt = txt_visitdate.Text;
        string[] Split = dt.Split('/');
        DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt = DateTime.Now.ToString("dd/MM/yyyy");
        Split = enddt.Split('/');
        DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        if (fromdate > todate)
        {
            txt_visitdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    public void gatepass_clear()
    {

        divscrll.Visible = false;
        paneladd.Visible = false;
        btnnew_click(sender, e);
        loadreason();
        loadreqby();
        loadreqmode();
        res();
        resrequest();
        req_mode();
        timevalue();
        //GridView1.DataSource = null;
        //GridView1.DataBind();
        TextBox78.Text = "";
        TextBox5.Text = "";
        txt_pop_search.Text = "";
        txt_staffnamegate.Text = "";
        txt_staffdeptgate.Text = "";
        txt_staffdesgngate.Text = "";

    }

    public void btn_errorclose_cnfm_Click(object sender, EventArgs e)
    {
        imgdivcnfm.Visible = false;
        string sql = "delete from textvaltable where TextCode='" + ddl_designation.SelectedItem.Value.ToString() + "' and TextCriteria='ReDes' and college_code='" + collegecode1 + "' ";
        int delete = d2.update_method_wo_parameter(sql, "TEXT");
        if (delete != 0)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "Deleted Successfully";
        }
        else
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
        loaddesignation();
    }

    public void btn_errorclose_cnfm2_Click(object sender, EventArgs e)
    {
        imgdivcnfm2.Visible = false;
        string sql = "delete from textvaltable where TextCode='" + ddl_department.SelectedItem.Value.ToString() + "' and TextCriteria='ReDep' and college_code='" + collegecode1 + "' ";
        int delete = d2.update_method_wo_parameter(sql, "TEXT");
        if (delete != 0)
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "Deleted Successfully";
        }
        else
        {
            imgdiv2.Visible = true;
            lbl_alert.Text = "No records found";
        }
        loaddepartment();
    }

    public void btn_errorclose_cncl2_Click(object sender, EventArgs e)
    {
        imgdivcnfm2.Visible = false;
    }

    public void btn_errorclose_cncl_Click(object sender, EventArgs e)
    {
        imgdivcnfm.Visible = false;
    }

    public void btn_errorclose_cnfm3_Click(object sender, EventArgs e)
    {
        imgdivcnfm3.Visible = false;
        if (deletevalue == "1")
        {
            string sql = "delete from textvaltable where TextCode='" + ddl_complaints.SelectedItem.Value.ToString() + "' and TextCriteria='ReCom' and college_code='" + collegecode1 + "' ";
            int delete = d2.update_method_wo_parameter(sql, "TEXT");
            if (delete != 0)
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Deleted Successfully";
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "No records found";
            }
            loadcomplaints();
        }
        else if (deletevalue == "2")
        {
            string sql = "delete from textvaltable where TextCode='" + ddl_suggestions.SelectedItem.Value.ToString() + "' and TextCriteria='ReSug' and college_code='" + collegecode1 + "' ";
            int delete = d2.update_method_wo_parameter(sql, "TEXT");
            if (delete != 0)
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "Deleted Successfully";
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Text = "No records found";
            }
            loadsuggestions();
        }
    }

    public void btn_errorclose_cncl3_Click(object sender, EventArgs e)
    {
        imgdivcnfm3.Visible = false;
    }

    public void TextBox5_TextChanged(object sender, EventArgs e)
    {
        if (TextBox5.Text != "")
        {
            //magesh 24.5.18
            string studname = Convert.ToString(TextBox5.Text);
            string[] spl = studname.Split('-');
            string roll = string.Empty;
            if (spl.Length >= 2)
                roll = spl[1];
            studname = spl[0];
            string sqlquery = string.Empty;//magesh 24.5.18
            //string query = "select distinct  hd.Gate_PerCount,hd.Gate_AllowUnApprove,r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code, hs.Floor_Name,hs.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections) as Degree  from Hostel_StudentDetails hs,Registration r,Hostel_Details hd,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and Stud_Type='Hostler' and r.stud_name = '" + studname + "'";
            string stustype = d2.GetFunction("select Stud_Type from Registration where  Roll_No = '" + roll + "'");
            if (Chkall.Checked == true)
            {
                if (stustype == "hostler" || stustype == "Hostler" || stustype.ToUpper() == "HOSTLER")
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and  ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.stud_name = '" + studname + "'";
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = true;
                    GridView1.Columns[10].Visible = true;

                }

                else
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,r.Batch_Year as HostelName,r.Roll_No HostelMasterPK,r.Roll_No Floor_Name,r.Roll_No Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,leavecount as HostelGatePassPerCount from Registration r,gatepasscount g,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id  and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR'   and r.stud_name = '" + studname + "' and  r.Roll_No='" + roll + "' and r.college_code=g.college_code";
                    //magesh 24.5.18
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;

                }
            }
            else
            {

                if (stustype == "hostler" || stustype == "Hostler" || stustype.ToUpper() == "HOSTLER")
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and  ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.stud_name = '" + studname + "'";
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = true;
                    GridView1.Columns[10].Visible = true;

                }

                else
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,r.Batch_Year as HostelName,r.Roll_No HostelMasterPK,r.Roll_No Floor_Name,r.Roll_No Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,leavecount as HostelGatePassPerCount from Registration r,gatepasscount g,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id  and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR'   and r.stud_name = '" + studname + "' and  r.Roll_No='" + roll + " ' and r.college_code=g.college_code";
                    //magesh 24.5.18
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;

                }
            }
            ds2 = da.select_method_wo_parameter(sqlquery, "text");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                erroemesssagelbl.Visible = false;
                GridView1.Visible = true;
                GridView1.DataSource = ds2;
                GridView1.DataBind();
                //magesh 24.5.18
                CheckBox chkLeaveCode = new CheckBox();
                chkLeaveCode = (CheckBox)GridView1.Rows[0].FindControl("chkup3");
                chkLeaveCode.Checked = true;
                //magesh 24.5.18

                if (Session["Rollflag"].ToString() == "0")
                {
                    GridView1.Columns[2].Visible = false;
                }
                if (Session["Regflag"].ToString() == "0")
                {
                    GridView1.Columns[3].Visible = false;
                }
                //if (Session["Studflag"].ToString() == "0")
                //{
                //    GridView1.Columns[4].Visible = false;
                //}
            }
            else
            {
                erroemesssagelbl.Visible = true;
                erroemesssagelbl.Text = "No Records Found";
                GridView1.Visible = false;
                paneladd.Visible = false;
            }
            divscrll.Visible = true;
            paneladd.Visible = true;
            fpcammarkstaff.Visible = false;
            btnnew_click(sender, e);
            gatpasslogin();
            TextBox78.Text = "";
        }
        else
        {
        }
    }

    public void TextBox78_TextChanged(object sender, EventArgs e)
    {
        if (TextBox78.Text != "")
        {
            string studno = Convert.ToString(TextBox78.Text);
            //magesh 24.5.18
            string[] spl = studno.Split('-');
            studno = spl[0];
            string sqlquery = string.Empty;//magesh 24.5.18
            //string query = "  select distinct  hd.Gate_PerCount,hd.Gate_AllowUnApprove,r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code, hs.Floor_Name,hs.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections) as Degree  from  Hostel_StudentDetails hs,Registration r,Hostel_Details hd,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and Stud_Type='Hostler' and r.Roll_No = '" + studno + "'";
            //magesh 24.5.18
            //   if (Chkhostel.Checked == true)

            string stustype = d2.GetFunction("select Stud_Type from Registration where  Roll_No = '" + studno + "'");
            if (Chkall.Checked == true)
            {
                if (stustype == "hostler" || stustype == "Hostler" || stustype.ToUpper() == "HOSTLER")
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and  ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.Roll_No = '" + studno + "'";
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = true;
                    GridView1.Columns[10].Visible = true;
                }
                if (stustype.ToUpper() == "DAY SCHOLAR" || stustype.ToUpper() == "DAYSCHOLAR" || stustype == "Day Scholar")
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,r.Batch_Year as HostelName,r.Roll_No HostelMasterPK,r.Roll_No Floor_Name,r.Roll_No Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,leavecount as HostelGatePassPerCount from Registration r,gatepasscount g,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id  and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR'   and r.Roll_No = '" + studno + "'  and r.college_code=g.college_code";


                    //magesh 24.5.18
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;

                }
            }
            else
            {
                if (stustype == "hostler" || stustype == "Hostler" || stustype.ToUpper() == "HOSTLER")
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and  ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.Roll_No = '" + studno + "'";
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = true;
                    GridView1.Columns[10].Visible = true;
                }
                if (stustype.ToUpper() == "DAY SCHOLAR" || stustype.ToUpper() == "DAYSCHOLAR" || stustype == "Day Scholar")
                {
                    sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,r.Batch_Year as HostelName,r.Roll_No HostelMasterPK,r.Roll_No Floor_Name,r.Roll_No Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+(ISNULL(Sections,'')))as Degree,leavecount as HostelGatePassPerCount from Registration r,gatepasscount g,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id  and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR'   and r.Roll_No = '" + studno + "'  and r.college_code=g.college_code";


                    //magesh 24.5.18
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;

                }
            }


            ds2 = da.select_method_wo_parameter(sqlquery, "text");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                erroemesssagelbl.Visible = false;
                GridView1.Visible = true;
                GridView1.DataSource = ds2;
                GridView1.DataBind();
                //magesh 24.5.18
                CheckBox chkLeaveCode = new CheckBox();
                chkLeaveCode = (CheckBox)GridView1.Rows[0].FindControl("chkup3");
                chkLeaveCode.Checked = true;
                //magesh 24.5.18

                if (Session["Rollflag"].ToString() == "0")
                {
                    //magesh 24.5.18
                    // GridView1.Columns[2].Visible = false;
                    GridView1.Columns[3].Visible = false;
                }
                if (Session["Regflag"].ToString() == "0")
                {
                    //magesh 24.5.18
                    // GridView1.Columns[3].Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                //if (Session["Studflag"].ToString() == "0")
                //{
                //    GridView1.Columns[4].Visible = false;
                //}
            }
            else
            {
                erroemesssagelbl.Visible = true;
                erroemesssagelbl.Text = "No Records Found";
                GridView1.Visible = false;
                paneladd.Visible = false;
            }
            divscrll.Visible = true;
            paneladd.Visible = true;
            fpcammarkstaff.Visible = false;
            btnnew_click(sender, e);
            gatpasslogin();
            TextBox5.Text = "";
        }
    }

    public void reload()
    {
        loadcomplaints();
        loadsuggestions();
        loadreason();
        //loadeventLOC();
        res();
        dept();
        loadleavereason();
        leav_res();
        loadreqby();
        resrequest();
        loadreqmode();
        req_mode();
    }

    protected void txtfromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dt = txtfromdate.Text;
            string[] Split = dt.Split('/');
            DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            string enddt = DateTime.Now.ToString("dd/MM/yyyy");
            Split = enddt.Split('/');
            DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            if (fromdate > todate)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dt = txttodate.Text;
            string dt1 = txtfromdate.Text;
            string[] Split = dt.Split('/');
            DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            string enddt = DateTime.Now.ToString("dd/MM/yyyy");
            Split = enddt.Split('/');
            DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
            if (fromdate > todate)
            {
                // txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = fromdate.ToString("dd/MM/yyyy");
            }
            compls_workingday();
        }
        catch (Exception ex)
        {
        }
    }

    public void txt_time_rqstn_leave_TextChanged(object sender, EventArgs e)
    {
        string dt = txt_time_rqstn_leave.Text;
        string[] Split = dt.Split('/');
        DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt = DateTime.Now.ToString("dd/MM/yyyy");
        Split = enddt.Split('/');
        DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        if (fromdate < todate)
        {
            txt_time_rqstn_leave.Text = DateTime.Now.ToString("dd/MM/yyyy");
            imgdiv2.Visible = true;
            lbl_alert.Text = "Kindly Select Valid Date";
            return;
        }
    }

    public void txt_frm_TextChanged(object sender, EventArgs e)
    {

        //============Added by saranya on 28/8/2018 to lock the previous HRmonth date============//
        string getusercode = string.Empty;

        getusercode = Session["usercode"].ToString();
        singleuser = Session["single_user"].ToString();
        group_user = Session["group_code"].ToString();//delsi1611
        if (singleuser == "False")
        {
            getusercode = Session["group_code"].ToString();
            if (getusercode.Contains(';'))
            {
                string[] splits = getusercode.Split(';');
                getusercode = Convert.ToString(splits[0]);
            }
        }

        string linkValueForLeaveApply = d2.GetFunction("select value from Master_Settings where settings='StaffLeaveApplyForPastDt' and usercode='" + getusercode + "'");
        if (linkValueForLeaveApply == "0")
        {
            DataSet dsHryearMonth = new DataSet();
            string serverDate = d2.GetFunction("select convert(varchar(20),GETDATE(),101) as date");
            DateTime DtserverDate = Convert.ToDateTime(serverDate);
            string ChkServerMonth = DtserverDate.ToString("dd/MM/yyyy").Split('/')[1];
            string ChkServerYear = DtserverDate.ToString("dd/MM/yyyy").Split('/')[2];
            string hrYear = "select * from hrpaymonths where SelStatus='1' and College_Code='" + Session["collegecode"].ToString() + "' and PayMonthNum='" + ChkServerMonth + "' and PayYear='" + ChkServerYear + "'";
            dsHryearMonth = d2.select_method_wo_parameter(hrYear, "Text");
            string HrYearFromDt = "";
            string HrYearToDt = "";
            if (dsHryearMonth.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsHryearMonth.Tables[0].Rows.Count; i++)
                {
                    HrYearFromDt = Convert.ToString(dsHryearMonth.Tables[0].Rows[i]["From_Date"]);

                    HrYearToDt = Convert.ToString(dsHryearMonth.Tables[0].Rows[i]["To_Date"]);
                }

                if (!string.IsNullOrEmpty(txt_frm.Text) && !string.IsNullOrEmpty(txt_to.Text))
                {
                    string SelectedDt = Convert.ToString(txt_frm.Text);

                    DateTime dtHrYearFromDt = Convert.ToDateTime(HrYearFromDt);
                    string[] dtDATE = SelectedDt.Split('/');
                    if (dtDATE.Length == 3)
                        SelectedDt = dtDATE[1].ToString() + "/" + dtDATE[0].ToString() + "/" + dtDATE[2].ToString();

                    DateTime dtSelectedDt = Convert.ToDateTime(SelectedDt);

                    if (dtSelectedDt < dtHrYearFromDt)
                    {
                        txt_frm.Enabled = false;
                        txt_to.Enabled = false;
                        div_GV1.Visible = false;
                        return;
                    }
                }
            }
        }
        //====================================================================================//

        h = "";
        hh = "";
        string dt = txt_frm.Text;
        string getdate = "";
        string[] Split = dt.Split('/');
        DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt = DateTime.Now.ToString("dd/MM/yyyy");
        Split = enddt.Split('/');
        DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt1 = txt_to.Text;
        Split = enddt1.Split('/');
        DateTime todate1 = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        TimeSpan days = todate1 - todate;
        string ndate = Convert.ToString(days);
        if (ndate != "")
        {
            Split = ndate.Split('.');
            getdate = Split[0];
            if (getdate == "00:00:00")
            {
                getdate = "0";
            }
        }
        //if (fromdate > todate)
        //{
        //    txt_frm.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    imgdiv2.Visible = true;
        //    lbl_alert.Text = "Kindly Select Valid Date";
        //    //;
        //}
        //if (fromdate > todate1)
        //{
        //    txt_frm.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    imgdiv2.Visible = true;
        //    lbl_alert.Text = "Kindly Select Valid Date";
        //    // return;
        //}
        //todate = todate.AddDays(30);
        //CalendarExtender8.EndDate = todate;
        //txt_to.Text = todate.ToString("dd/MM/yyyy");
        //if (Convert.ToInt32(getdate) + 1 < 31)
        //{
        compls_workingday();
        bindleavedatechange();
        //}
        //else
        //{
        //    txt_frm.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    imgdiv2.Visible = true;
        //    lbl_alert.Text = "You Cann't Select More Than Of 30 Days";
        //}


    }

    public void txt_to_TextChanged(object sender, EventArgs e)
    {
        h = "";
        hh = "";
        string getdate = "";
        string dt = txt_frm.Text;
        //int finaldate = 0;
        string[] Split = dt.Split('/');
        DateTime todate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        string enddt = txt_to.Text;
        Split = enddt.Split('/');
        DateTime fromdate = Convert.ToDateTime(Split[1] + "/" + Split[0] + "/" + Split[2]);
        TimeSpan days = fromdate - todate;
        string ndate = Convert.ToString(days);
        if (ndate != "")
        {
            Split = ndate.Split('.');
            getdate = Split[0];
            if (getdate == "00:00:00")
            {
                getdate = "0";
            }
        }
        //if (fromdate != todate)
        //{
        //    if (fromdate < todate)
        //    {
        //        txt_to.Text = txt_frm.Text;
        //        //txt_to.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //        GV1.Visible = false;
        //        imgdiv2.Visible = true;
        //        lbl_alert.Text = "Kindly Select Valid Date";
        //        // return;
        //    }
        //}
        if (Convert.ToInt32(getdate) + 1 <= 31)//delsi0506
        {
            compls_workingday();
        }
        else
        {
            txt_to.Text = txt_frm.Text;
            //txt_to.Text = DateTime.Now.ToString("dd/MM/yyyy");
            imgdiv2.Visible = true;
            lbl_alert.Text = "You Cann't Select More Than Of 30 Days";
        }
    }

    public void compls_workingday()
    {
        string CompWorkType = "";
        string CompWorkDateDay = "";
        DateTime fromdatenew = new DateTime();
        fromdatenew = TextToDate(txt_frm);
        DateTime todatenew = new DateTime();
        todatenew = TextToDate(txt_to);
        string qur = "select * from individual_leave_type where  staff_code='" + txt_staff_code.Text + "' and college_code=" + ddlcollege.SelectedItem.Value + "";
        ds1 = d2.select_method_wo_parameter(qur, "Text");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            string[] spl_type = ds1.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
            for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
            {
                string[] split_leave = spl_type[i].Split(';');
                if (split_leave[0].ToString() == ddl_leave_type.SelectedItem.ToString())
                {
                    if (split_leave[8] != "" && split_leave[8] != "0")
                    {
                        CompWorkDateDay = Convert.ToString(split_leave[8]);
                        //string catg_code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'  and latestrec='1'");
                        //string catg_shift = d2.GetFunction("select shift from stafftrans where staff_code='" + txt_staff_code.Text + "'  and latestrec='1'");
                        //string compl_wrkng_days = d2.GetFunction("select IsCompWork from in_out_time where category_code='" + catg_code + "' and shift='" + catg_shift + "' and ISNULL(IsCompWork,0)!=0");
                        //if (compl_wrkng_days == "True")
                        //{
                        //    CompWorkType = d2.GetFunction("select CompWorkType from in_out_time where category_code='" + catg_code + "' and shift='" + catg_shift + "' and ISNULL(IsCompWork,0)!=0");
                        //if (CompWorkType == "0")
                        //{
                        //    CompWorkDateDay = d2.GetFunction("select CompWorkDate from in_out_time where category_code='" + catg_code + "' and shift='" + catg_shift + "' and ISNULL(IsCompWork,0)!=0");
                        //}
                        //else if (CompWorkType == "1")
                        //{
                        //    CompWorkDateDay = d2.GetFunction("select CompWorkDay from in_out_time where category_code='" + catg_code + "' and shift='" + catg_shift + "' and ISNULL(IsCompWork,0)!=0");
                        //}
                        for (; fromdatenew <= todatenew; )
                        {
                            startdate1 = fromdatenew.ToString("dd/MM/yyyy");
                            DateTime dtnew = new DateTime();
                            string[] spnew = startdate1.Split('/');
                            dtnew = Convert.ToDateTime(spnew[1] + "/" + spnew[0] + "/" + spnew[2]);
                            string dayvalue = dtnew.ToString("dddd");
                            //if (CompWorkType == "0")
                            //{
                            //    DateTime fdate = Convert.ToDateTime(CompWorkDateDay);
                            //    if (fromdatenew == fdate)
                            //    {
                            //        imgdiv2.Visible = true;
                            //        lbl_alert.Text = fromdatenew + "-CompulsoryWorkingDate,You Cannot Take a Leave on This Date";
                            //        return;
                            //    }
                            //}
                            //else if (CompWorkType == "1")
                            //{
                            CompWorkDateDay = DAY(CompWorkDateDay);
                            if (dayvalue == CompWorkDateDay)
                            {
                                imgdiv2.Visible = true;
                                lbl_alert.Text = dayvalue + "-CompulsoryWorkingDay,You Cannot Take a Leave on This Day";
                                return;
                            }
                            // }
                            fromdatenew = fromdatenew.AddDays(1);
                        }
                        //}
                        //else
                        //{
                        //    imgdiv2.Visible = true;
                        //    lbl_alert.Text = "Set The Master Setting of CompulsoryWorkingDay";
                        //    return;
                        //}
                    }
                }
            }
        }
        DataTable dt = new DataTable();
        grid_altersub.DataSource = dt;
        grid_altersub.DataBind();
        ss = 0;
        BindGridview();
        // degbatchsem();
        //alternate schedule changed based on settings
        if (alterrigths == "1")
            degbatchsem();
        //  altersubject();
        else if (alterrigths == "2")
            afteralterSchedule();
        // altersubject();
    }

    public string DAY(string D)
    {
        string dd = "";
        if (D == "Mon")
        {
            dd = "Monday";
        }
        if (D == "Tue")
        {
            dd = "Tuesday";
        }
        if (D == "Wed")
        {
            dd = "Wednesday";
        }
        if (D == "Thus")
        {
            dd = "Thursday";
        }
        if (D == "Fri")
        {
            dd = "Friday";
        }
        if (D == "Sat")
        {
            dd = "Saturday";
        }
        if (D == "Sun")
        {
            dd = "Sunday";
        }
        return dd;
    }

    private bool IsPredefinedHoliday(DateTime date)
    {
        if (date.Day % 3 == 0)
        {
            holidaydate = "1";
            return true;
        }
        else
        {
            holidaydate = "2";
            return true;
        }
    }

    public void degbatchsem()//delsi 0907
    {

        string[] ay = txt_frm.Text.Split('/');
        string[] ay1 = txt_to.Text.Split('/');
        //string currdate = DateTime.Now.ToString("dd/MM/yyyy");        
        DateTime dt = new DateTime();
        DateTime dt1 = new DateTime();
        dt = Convert.ToDateTime(ay[1] + "/" + ay[0] + "/" + ay[2]);
        dt1 = Convert.ToDateTime(ay1[1] + "/" + ay1[0] + "/" + ay1[2]);
        string SqlFinal = " select distinct r.Batch_Year,r.degree_code,sy.semester,ltrim(rtrim(isnull(r.sections,''))) as sections,si.end_date from staff_selector ss,Registration r,";//magesh
        SqlFinal = SqlFinal + " subject s,sub_sem sm,syllabus_master sy,seminfo si where sy.Batch_Year=r.Batch_Year and sy.degree_code=r.degree_code";
        SqlFinal = SqlFinal + " and sy.semester=r.Current_Semester and sy.syll_code=sm.syll_code and sm.subType_no=s.subType_no ";
        SqlFinal = SqlFinal + " and s.subject_no=ss.subject_no and ltrim(rtrim(isnull(r.sections,'')))=ltrim(rtrim(isnull(ss.sections,''))) and ss.batch_year=r.Batch_Year";
        SqlFinal = SqlFinal + " and si.Batch_Year=r.Batch_Year and si.degree_code=r.degree_code and si.semester=r.Current_Semester and ";
        SqlFinal = SqlFinal + " si.Batch_Year=sy.Batch_Year and sy.degree_code=r.degree_code and si.semester=sy.Semester and r.CC=0 and r.Exam_Flag<>'debar'";
        SqlFinal = SqlFinal + " and r.DelFlag=0 and ss.staff_code='" + txt_staff_code.Text + "'";
        DataView dvalternaet = new DataView();
        DataView dvsemster = new DataView();
        DataView dvholiday = new DataView();
        DataView dvdaily = new DataView();
        DataView dvsubject = new DataView();
        DataView dvsublab = new DataView();
        string getalldetails = "";
        getalldetails = "select * from Alternate_Schedule where FromDate between '" + dt.ToString("MM/dd/yyyy") + "' and '" + dt1.ToString("MM/dd/yyyy") + "'";
        getalldetails = getalldetails + " select * from Semester_Schedule order by FromDate desc; ";
        getalldetails = getalldetails + " Select * from holidaystudents where holiday_date between '" + dt.ToString("MM/dd/yyyy") + "' and '" + dt.ToString("MM/dd/yyyy") + "' ";
        getalldetails = getalldetails + " select staff_name,staff_code from staffmaster ; ";
        getalldetails = getalldetails + " select s.subject_no,s.subject_name,s.subject_code,sy.Batch_Year,sy.degree_code,sy.semester,ss.Lab from Registration r,syllabus_master sy,sub_sem ss,subject s where r.Batch_Year=sy.Batch_Year and r.degree_code=sy.degree_code and r.Current_Semester=sy.semester and sy.syll_code=ss.syll_code and sy.syll_code=s.syll_code and ss.subType_no=ss.subType_no and r.cc=0 and r.delflag=0 and r.exam_flag<>'debar' and r.degree_code='2012' and r.Batch_Year='1052' and r.Current_Semester='3';";
        getalldetails = getalldetails + " select distinct Current_Semester,Batch_Year,degree_code from Registration where cc=0 and delflag=0 and exam_flag<>'debar'; ";
        getalldetails = getalldetails + " select no_of_hrs_I_half_day as mor,no_of_hrs_I_half_day as eve,degree_code,semester from periodattndschedule";
        getalldetails = getalldetails + " select * from tbl_consider_day_order";
        DataSet dsall = d2.select_method_wo_parameter(getalldetails, "Text");
        Hashtable hatholiday = new Hashtable();
        DataSet dsperiod = d2.select_method(SqlFinal, hat, "Text");
        DataSet dsperiodNew = d2.select_method_wo_parameter("select distinct s.batch_year,r.degree_code,s.semester,s.sections from registration r, Semester_Schedule s where s.batch_year=r.batch_year and s.degree_code=r.degree_code and r.current_semester=s.semester and ltrim(rtrim(isnull(r.sections,'')))=ltrim(rtrim(isnull(s.sections,''))) and r.cc=0 and r.delflag=0 and r.exam_flag<>'debar'  order by s.batch_year desc", "text");

        if (dsperiodNew.Tables.Count > 0 && dsperiodNew.Tables[0].Rows.Count > 0)
        {
            for (int pre = 0; pre < dsperiodNew.Tables[0].Rows.Count; pre++)
            {
                string cur_camprevar = Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["batch_year"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["degree_code"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["semester"]) + "-" + Convert.ToString(dsperiodNew.Tables[0].Rows[pre]["sections"]);
                deg_batch_sem = cur_camprevar;
                altersubject();
            }
        }
        else
        {
            deg_batch_sem = "";
        }
    }

    public void BindGridview()
    {
        ArrayList addnew = new ArrayList();
        DateTime fromdate = new DateTime();
        fromdate = TextToDate(txt_frm);
        DateTime todate = new DateTime();
        todate = TextToDate(txt_to);
        GV1.Visible = true;
        TimeSpan c = fromdate - todate;
        DataTable dt = new DataTable();
        dt.Columns.Add("Dummy");
        dt.Columns.Add("Dummy1");
        dt.Columns.Add("Dummy2");
        dt.Columns.Add("Dummy3");

        dt.Columns.Add("Dummy4");//delsi0312
        DataRow dr;

        if (fromdate != todate)
        {
            for (; fromdate <= todate; )
            {
                string to = Convert.ToString(txt_frm.Text);
                string from = Convert.ToString(txt_to.Text);
                dr = dt.NewRow();
                dr[0] = "1";
                dr[1] = fromdate.ToString("dd/MM/yyyy");
                dt.Rows.Add(dr);
                fromdate = fromdate.AddDays(1);
            }
        }
        else
        {
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = fromdate.ToString("dd/MM/yyyy");
            dt.Rows.Add(dr);
            fromdate = fromdate.AddDays(1);
        }
        if (dt.Rows.Count > 0)
        {
            GV1.DataSource = dt;
            GV1.DataBind();
            div_GV1.Visible = true;
        }
    }

    protected void OnRowDataBound_gv1(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (alterrigths == "1")
            {
                e.Row.Cells[4].Visible = true;
            }
            else
            {

                e.Row.Cells[4].Visible = false;
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string colg = ddlcollege.SelectedItem.Value;
            int cellvalue = e.Row.Cells.Count;
            string datecheck;
            string code = "";
            string deptCode = string.Empty;
            string holidate = "";
            var dateholy = (TextBox)e.Row.FindControl("txtdate");
            datecheck = Convert.ToString(dateholy.Text);
            string[] ay = datecheck.Split('/');
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(ay[1] + "/" + ay[0] + "/" + ay[2]);
            string type = d2.GetFunction("select LinkValue  from InsSettings where LinkName like 'Staff Holiday By Staff Type%' and college_code='" + ddlcollege.SelectedItem.Value + "'");
            if (type == "0")
            {
                code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                holidate = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and holiday_date='" + dt.ToString("MM/dd/yyyy") + "' and college_code ='" + ddlcollege.SelectedItem.Value + "' and dept_code='" + deptCode + "'");
                string sunday = dt.ToString("dddd");
                if (sunday == "Sunday" || holidate != "0")
                {
                    ss = 1;
                    e.Row.BackColor = Color.Aqua;
                    if (sunday == "Sunday")
                    {
                        if (hh == "")
                        {
                            hh = dateholy.Text;
                        }
                        else
                        {
                            hh = hh + "," + dateholy.Text;
                        }
                    }
                    else
                    {
                        if (h == "")
                        {
                            h = dateholy.Text;
                        }
                        else
                        {
                            h = h + "," + dateholy.Text;
                        }
                    }
                    //lbl_holidayalert.Visible = true; // poo 07.11.17
                    if (hh != "" && h != "")
                    {
                        lbl_holidayalert.Text = h + "-Holiday," + hh + "-Sunday";
                    }
                    else if (h != "" && hh == "")
                    {
                        lbl_holidayalert.Text = h + "-Holiday";
                    }
                    else
                    {
                        lbl_holidayalert.Text = hh + "-Sunday";
                    }
                    var chkmrng = (CheckBox)e.Row.FindControl("chk_mrng");
                    //chkmrng.Enabled = false; // poo 07.11.17
                    //chkmrng.Checked = false;// poo 07.11.17
                    var chkeve = (CheckBox)e.Row.FindControl("chk_evng");
                    //chkeve.Enabled = false;// poo 07.11.17
                    //chkeve.Checked = false;// poo 07.11.17
                    if (alterrigths == "1")
                    {
                        DataTable dt1 = new DataTable();
                        string hrs = d2.GetFunction("select max(No_of_hrs_per_day) as noofhours from PeriodAttndSchedule");
                        int totfrs = Convert.ToInt32(hrs);
                        if (totfrs > 0)
                        {
                            DataColumn dc1 = new DataColumn();
                            DataRow dr1;
                            dc1.ColumnName = "hrs";
                            dt1.Columns.Add(dc1);
                            dc1 = new DataColumn();
                            dc1.ColumnName = "hrsval";
                            dt1.Columns.Add(dc1);
                            for (int totalhr = 1; totalhr <= totfrs; totalhr++)
                            {
                                dr1 = dt1.NewRow();
                                dr1[0] = Convert.ToString(totalhr);
                                dr1[1] = Convert.ToString(totalhr);
                                dt1.Rows.Add(dr1);
                            }

                        }
                        if (dt1.Rows.Count > 0)
                        {
                            //for (int i = 0; i < dt1.Rows.Count; i++)
                            //{
                            string cblist = "cblhr";
                            string ckBox = "chkhr";
                            string txt = "txthour";
                            CheckBoxList atttype = (e.Row.FindControl(cblist) as CheckBoxList);
                            CheckBox chk = (e.Row.FindControl(ckBox) as CheckBox);
                            TextBox txtBox = (e.Row.FindControl(txt) as TextBox);
                            atttype.Items.Clear();
                            atttype.DataSource = dt1;
                            atttype.DataTextField = "hrs";
                            atttype.DataValueField = "hrsval";
                            atttype.DataBind();
                            //checkBoxListselectOrDeselect(atttype, false);
                            checkBoxListselectOrDeselect(atttype, true);
                            CallCheckboxListChange(chk, atttype, txtBox, "Hours", "--Select--");
                            //}
                        }


                        e.Row.Cells[4].Visible = true;


                    }
                    else
                    {
                        e.Row.Cells[4].Visible = false;

                    }
                }
                else
                {
                    var chkmrng = (CheckBox)e.Row.FindControl("chk_mrng");
                    chkmrng.Enabled = true;
                    var chkeve = (CheckBox)e.Row.FindControl("chk_evng");
                    chkeve.Enabled = true;
                    if (alterrigths == "1")
                    {
                        DataTable dt1 = new DataTable();
                        string hrs = d2.GetFunction("select max(No_of_hrs_per_day) as noofhours from PeriodAttndSchedule");
                        int totfrs = Convert.ToInt32(hrs);
                        if (totfrs > 0)
                        {
                            DataColumn dc1 = new DataColumn();
                            DataRow dr1;
                            dc1.ColumnName = "hrs";
                            dt1.Columns.Add(dc1);
                            dc1 = new DataColumn();
                            dc1.ColumnName = "hrsval";
                            dt1.Columns.Add(dc1);
                            for (int totalhr = 1; totalhr <= totfrs; totalhr++)
                            {
                                dr1 = dt1.NewRow();
                                dr1[0] = Convert.ToString(totalhr);
                                dr1[1] = Convert.ToString(totalhr);
                                dt1.Rows.Add(dr1);
                            }

                        }
                        if (dt1.Rows.Count > 0)
                        {
                            //for (int i = 0; i < dt1.Rows.Count; i++)
                            //{
                            string cblist = "cblhr";
                            string ckBox = "chkhr";
                            string txt = "txthour";
                            CheckBoxList atttype = (e.Row.FindControl(cblist) as CheckBoxList);
                            CheckBox chk = (e.Row.FindControl(ckBox) as CheckBox);
                            TextBox txtBox = (e.Row.FindControl(txt) as TextBox);
                            atttype.Items.Clear();
                            atttype.DataSource = dt1;
                            atttype.DataTextField = "hrs";
                            atttype.DataValueField = "hrsval";
                            atttype.DataBind();
                            //checkBoxListselectOrDeselect(atttype, false);
                            checkBoxListselectOrDeselect(atttype, true);
                            CallCheckboxListChange(chk, atttype, txtBox, "Hours", "--Select--");
                            //}
                        }


                        e.Row.Cells[4].Visible = true;


                    }
                    else
                    {
                        e.Row.Cells[4].Visible = false;

                    }

                }
            }
            else
            {
                code = d2.GetFunction("select stftype from stafftrans where staff_code='" + txt_staff_code.Text + "'");

                string deptCodes = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806

                holidate = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and holiday_date='" + dt.ToString("MM/dd/yyyy") + "' and college_code ='" + collegecode1 + "'and dept_code='" + deptCodes + "'");
                string sunday = dt.ToString("dddd");
                if (sunday == "Sunday" || holidate != "0")
                {
                    ss = 1;
                    e.Row.BackColor = Color.Aqua;
                    if (sunday == "Sunday")
                    {
                        if (hh == "")
                        {
                            hh = dateholy.Text;
                        }
                        else
                        {
                            hh = hh + "," + dateholy.Text;
                        }
                    }
                    else
                    {
                        if (h == "")
                        {
                            h = dateholy.Text;
                        }
                        else
                        {
                            h = h + "," + dateholy.Text;
                        }
                    }
                    lbl_holidayalert.Visible = true;
                    if (hh != "" && h != "")
                    {
                        lbl_holidayalert.Text = h + "-Holiday," + hh + "-Sunday";
                    }
                    else if (h != "" && hh == "")
                    {
                        lbl_holidayalert.Text = h + "-Holiday";
                    }
                    else
                    {
                        lbl_holidayalert.Text = hh + "-Sunday";
                    }
                    var chkmrng = (CheckBox)e.Row.FindControl("chk_mrng");
                    chkmrng.Enabled = false;
                    chkmrng.Checked = false;
                    var chkeve = (CheckBox)e.Row.FindControl("chk_evng");
                    chkeve.Enabled = false;
                    chkeve.Checked = false;
                }
                else
                {
                    var chkmrng = (CheckBox)e.Row.FindControl("chk_mrng");
                    chkmrng.Enabled = true;
                    var chkeve = (CheckBox)e.Row.FindControl("chk_evng");
                    chkeve.Enabled = true;
                }
            }
        }
        if (ss == 0)
        {
            lbl_holidayalert.Visible = false;
        }
    }

    public void gatepassrights()
    {
        try
        {
            string query = "";
            string Master1 = "";
            string stud = "";
            string values = "";
            string sms = "";
            string sms1 = "";
            string sms2 = "";
            if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
            {
                string group = Session["group_code"].ToString();
                if (group.Contains(';'))
                {
                    string[] group_semi = group.Split(';');
                    Master1 = group_semi[0].ToString();
                    query = "select * from Master_Settings where settings ='Request Gatepass Rights' and group_code ='" + Master1 + "'";
                }
                else
                    query = "select * from Master_Settings where settings ='Request Gatepass Rights' and group_code ='" + group + "'"; // poo 12.12.17
            }
            else
            {
                Master1 = Session["usercode"].ToString();
                query = "select * from Master_Settings where settings ='Request Gatepass Rights' and usercode ='" + Master1 + "'";
            }
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string val = Convert.ToString(ds.Tables[0].Rows[i]["value"]);
                    if (val == "1")
                    {
                        gatepass_staffdept = "1";
                    }
                    else if (val == "2")
                    {
                        gatepass_staffdept = "2";
                    }
                }
            }
        }
        catch
        {
        }
    }

    public void gatpasslogin()
    {
        Int64 ReqStaffAppNo = 0;
        Int64 ReqStaffDeptFK = 0;
        bool Is_Staff;
        Is_Staff = Convert.ToBoolean(d2.GetFunction("select Is_Staff from UserMaster where User_Code='" + usercode + "' and college_code='" + collegecode1 + "'"));
        if (Is_Staff == true)
        {
            string staffcode = d2.GetFunction("select staff_code  from UserMaster where User_Code='" + usercode + "'");
            if (staffcode.Trim() != "")
            {
                ReqStaffAppNo = Convert.ToInt64(d2.GetFunction("select appl_id  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staffcode + "'"));
                Btn_Staff_Code.Visible = false;
                string query = "select  s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = 13 and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.staff_code = '" + staffcode + "'";
                ds = d2.select_method_wo_parameter(query, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtissueper.Text = Convert.ToString(ds.Tables[0].Rows[0]["appl_name"]);
                    TextBox1.Text = Convert.ToString(ds.Tables[0].Rows[0]["appl_no"]);
                }
                else
                {
                    string ReqStaffAppname = d2.GetFunction("select appl_name  from staff_appl_master a, staffmaster s where a.appl_no=s.appl_no and staff_code='" + staffcode + "'");
                    string applnnno = d2.GetFunction("select appl_no from staffmaster where staff_code='" + staffcode + "'");
                    txtissueper.Text = ReqStaffAppname;
                    TextBox1.Text = applnnno;
                }
            }
        }
    }

    public void TabAccess()
    {
        try
        {
            string query = "";
            string Master1 = "";
            string stud = "";
            string values = "";
            if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
            {
                string group = Session["group_code"].ToString();
                if (group.Contains(';'))
                {
                    string[] group_semi = group.Split(';');
                    Master1 = group_semi[0].ToString();
                    query = "select * from Master_Settings where settings ='Request Tap Rights' and group_code ='" + Master1 + "'"; // poo 11.12.17
                }
                else
                    query = "select * from Master_Settings where settings ='Request Tap Rights' and group_code ='" + group + "'"; // poo 11.12.17
            }
            else
            {
                Master1 = Session["usercode"].ToString();
                query = "select * from Master_Settings where settings ='Request Tap Rights' and usercode ='" + Master1 + "'";
            }
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string val = Convert.ToString(ds.Tables[0].Rows[i]["value"]);
                    string[] split = val.Split(',');
                    string len = split.Length.ToString();
                    if (len == "1")
                    {
                        values = val;
                        if (val == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                            imgbtn_item.Visible = false;
                            lbl_itemreq.Visible = false;
                        }
                        if (val == "2")
                        {
                            td_sev.Visible = true;
                            imgbtn_service.Visible = true;
                            lbl_sevice.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                            imgbtn_service.Visible = false;
                            lbl_sevice.Visible = false;
                        }
                        if (val == "3")
                        {
                            td_vist.Visible = true;
                            imgbtn_visitor.Visible = true;
                            lbl_visitor.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                            imgbtn_visitor.Visible = false;
                            lbl_visitor.Visible = false;
                        }
                        if (val == "4")
                        {
                            td_comp.Visible = true;
                            imgbtn_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = false;
                        }
                        if (val == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (val == "6")
                        {
                            td_othr.Visible = true;
                            panelrollnopop.Visible = true;
                            div_gate_reqstn.Visible = true;
                            lbl_headername.Text = "GatePass Request ";
                        }
                        else
                        {
                            td_othr.Visible = false;
                            panelrollnopop.Visible = false;
                            div_gate_reqstn.Visible = false;
                        }
                        if (val == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                    // ******************** length 2**************
                    if (len == "2")
                    {
                        string sp1 = (split[0]);
                        string sp2 = (split[1]);
                        if (sp1 == "1" || sp2 == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                        }
                        if (sp1 == "2" || sp2 == "2")
                        {
                            td_sev.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                        }
                        if (sp1 == "3" || sp2 == "3")
                        {
                            td_vist.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                        }
                        if (sp1 == "4" || sp2 == "4")
                        {
                            td_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = false;
                        }
                        if (sp1 == "5" || sp2 == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (sp1 == "6" || sp2 == "6")
                        {
                            td_othr.Visible = true;
                        }
                        else
                        {
                            td_othr.Visible = false;
                        }
                        if (sp1 == "7" || sp2 == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                    //  *************************** length 3*****************
                    else if (len == "3")
                    {
                        string sp1 = (split[0]);
                        string sp2 = (split[1]);
                        string sp3 = (split[2]);
                        if (sp1 == "1" || sp2 == "1" || sp3 == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                        }
                        if (sp1 == "2" || sp2 == "2" || sp3 == "2")
                        {
                            td_sev.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                        }
                        if (sp1 == "3" || sp2 == "3" || sp3 == "3")
                        {
                            td_vist.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                        }
                        if (sp1 == "4" || sp2 == "4" || sp3 == "4")
                        {
                            td_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = false;
                        }
                        if (sp1 == "5" || sp2 == "5" || sp3 == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (sp1 == "6" || sp2 == "6" || sp3 == "6")
                        {
                            td_othr.Visible = true;
                        }
                        else
                        {
                            td_othr.Visible = false;
                        }
                        if (sp1 == "7" || sp2 == "7" || sp3 == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                    //    *********************** length 4*****************
                    else if (len == "4")
                    {
                        string sp1 = (split[0]);
                        string sp2 = (split[1]);
                        string sp3 = (split[2]);
                        string sp4 = (split[3]);
                        if (sp1 == "1" || sp2 == "1" || sp3 == "1" || sp4 == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                        }
                        if (sp1 == "2" || sp2 == "2" || sp3 == "2" || sp4 == "2")
                        {
                            td_sev.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                        }
                        if (sp1 == "3" || sp2 == "3" || sp3 == "3" || sp4 == "3")
                        {
                            td_vist.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                        }
                        if (sp1 == "4" || sp2 == "4" || sp3 == "4" || sp4 == "4")
                        {
                            td_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = false;
                        }
                        if (sp1 == "5" || sp2 == "5" || sp3 == "5" || sp4 == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (sp1 == "6" || sp2 == "6" || sp3 == "6" || sp4 == "6")
                        {
                            td_othr.Visible = true;
                        }
                        else
                        {
                            td_othr.Visible = false;
                        }
                        if (sp1 == "7" || sp2 == "7" || sp3 == "7" || sp4 == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                    else if (len == "5")
                    {
                        string sp1 = (split[0]);
                        string sp2 = (split[1]);
                        string sp3 = (split[2]);
                        string sp4 = (split[3]);
                        string sp5 = (split[4]);
                        if (sp1 == "1" || sp2 == "1" || sp3 == "1" || sp4 == "1" || sp5 == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                        }
                        if (sp1 == "2" || sp2 == "2" || sp3 == "2" || sp4 == "2" || sp5 == "2")
                        {
                            td_sev.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                        }
                        if (sp1 == "3" || sp2 == "3" || sp3 == "3" || sp4 == "3" || sp5 == "3")
                        {
                            td_vist.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                        }
                        if (sp1 == "4" || sp2 == "4" || sp3 == "4" || sp4 == "4" || sp5 == "4")
                        {
                            td_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = false;
                        }
                        if (sp1 == "5" || sp2 == "5" || sp3 == "5" || sp4 == "5" || sp5 == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (sp1 == "6" || sp2 == "6" || sp3 == "6" || sp4 == "6" || sp5 == "6")
                        {
                            td_othr.Visible = true;
                        }
                        else
                        {
                            td_othr.Visible = false;
                        }
                        if (sp1 == "7" || sp2 == "7" || sp3 == "7" || sp4 == "7" || sp5 == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                    else if (len == "6")
                    {
                        string sp1 = (split[0]);
                        string sp2 = (split[1]);
                        string sp3 = (split[2]);
                        string sp4 = (split[3]);
                        string sp5 = (split[4]);
                        string sp6 = (split[5]);
                        if (sp1 == "1" || sp2 == "1" || sp3 == "1" || sp4 == "1" || sp5 == "1" || sp6 == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                        }
                        if (sp1 == "2" || sp2 == "2" || sp3 == "2" || sp4 == "2" || sp5 == "2" || sp6 == "2")
                        {
                            td_sev.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                        }
                        if (sp1 == "3" || sp2 == "3" || sp3 == "3" || sp4 == "3" || sp5 == "3" || sp6 == "3")
                        {
                            td_vist.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                        }
                        if (sp1 == "4" || sp2 == "4" || sp3 == "4" || sp4 == "4" || sp5 == "4" || sp6 == "4")
                        {
                            td_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = true;
                        }
                        if (sp1 == "5" || sp2 == "5" || sp3 == "5" || sp4 == "5" || sp5 == "5" || sp6 == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (sp1 == "6" || sp2 == "6" || sp3 == "6" || sp4 == "6" || sp5 == "6" || sp6 == "6")
                        {
                            td_othr.Visible = true;
                        }
                        else
                        {
                            td_othr.Visible = false;
                        }
                        if (sp1 == "7" || sp2 == "7" || sp3 == "7" || sp4 == "7" || sp5 == "7" || sp6 == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                    else if (len == "7")
                    {
                        string sp1 = (split[0]);
                        string sp2 = (split[1]);
                        string sp3 = (split[2]);
                        string sp4 = (split[3]);
                        string sp5 = (split[4]);
                        string sp6 = (split[5]);
                        string sp7 = (split[6]);
                        if (sp1 == "1" || sp2 == "1" || sp3 == "1" || sp4 == "1" || sp5 == "1" || sp6 == "1" || sp7 == "1")
                        {
                            td_item.Visible = true;
                        }
                        else
                        {
                            td_item.Visible = false;
                        }
                        if (sp1 == "2" || sp2 == "2" || sp3 == "2" || sp4 == "2" || sp5 == "2" || sp6 == "2" || sp7 == "2")
                        {
                            td_sev.Visible = true;
                        }
                        else
                        {
                            td_sev.Visible = false;
                        }
                        if (sp1 == "3" || sp2 == "3" || sp3 == "3" || sp4 == "3" || sp5 == "3" || sp6 == "3" || sp7 == "3")
                        {
                            td_vist.Visible = true;
                        }
                        else
                        {
                            td_vist.Visible = false;
                        }
                        if (sp1 == "4" || sp2 == "4" || sp3 == "4" || sp4 == "4" || sp5 == "4" || sp6 == "4" || sp7 == "4")
                        {
                            td_comp.Visible = true;
                        }
                        else
                        {
                            td_comp.Visible = false;
                        }
                        if (sp1 == "5" || sp2 == "5" || sp3 == "5" || sp4 == "5" || sp5 == "5" || sp6 == "5" || sp7 == "5")
                        {
                            td_lv.Visible = true;
                        }
                        else
                        {
                            td_lv.Visible = false;
                        }
                        if (sp1 == "6" || sp2 == "6" || sp3 == "6" || sp4 == "6" || sp5 == "6" || sp6 == "6" || sp7 == "6")
                        {
                            td_othr.Visible = true;
                        }
                        else
                        {
                            td_othr.Visible = false;
                        }
                        if (sp1 == "7" || sp2 == "7" || sp3 == "7" || sp4 == "7" || sp5 == "7" || sp6 == "7" || sp7 == "7")
                        {
                            td_event.Visible = true;
                        }
                        else
                        {
                            td_event.Visible = false;
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }

    public void rdo_student_CheckedChanged(object sender, EventArgs e)
    {
        panelrollnopop.Visible = true;
        gatepass_stud.Visible = true;
        div_staff.Visible = false;
        btnstaff.Visible = true;
        txtissueper.ReadOnly = false;
        paneladd.Visible = false;
        //if (rdo_staff.Checked)
        //{
        //    txt_pop_search.Text = Convert.ToString(staffcodesession);
        //    rdo_staff_CheckedChanged(sender, e);
        //}
        //else
        //    txt_pop_search.Text = "";
    }

    public void rdo_staff_CheckedChanged(object sender, EventArgs e)
    {
        panelrollnopop.Visible = true;
        gatepass_stud.Visible = false;
        div_staff.Visible = true;
        txtissueper.Text = "Self";
        btnstaff.Visible = false;
        txtissueper.ReadOnly = true;
        paneladd.Visible = false;
        //if (rdo_staff.Checked)
        //{
        txt_pop_search.Text = Convert.ToString(staffcodesession);
        txt_pop_search_TextChanged(sender, e);
        //}
        //else
        //    txt_pop_search.Text = "";
    }

    public void txt_pop_search_TextChanged(object sender, EventArgs e)
    {
        string staffcode = Convert.ToString(txt_staff_code.Text);
        string query = "select  s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = 13 and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.staff_code = '" + txt_pop_search.Text + "'";
        ds = d2.select_method_wo_parameter(query, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                txt_staffnamegate.Text = Convert.ToString(ds.Tables[0].Rows[i]["appl_name"]);
                txt_staffdeptgate.Text = Convert.ToString(ds.Tables[0].Rows[i]["dept"]);
                txt_staffdesgngate.Text = Convert.ToString(ds.Tables[0].Rows[i]["design"]);
                txt_staff_code.Text = staffcode;
            }
            paneladd.Visible = true;
        }
    }

    public void txt_staffnamegate_TextChanged(object sender, EventArgs e)
    {
        string staffname = Convert.ToString(txt_staffnamegate.Text);
        string query = "select m.Staff_code, s.*,h.dept_name as dept,d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = 13 and t.latestrec = 1 and m.resign = 0 and settled = 0 and s.appl_name = '" + txt_staffnamegate.Text + "'";
        ds = d2.select_method_wo_parameter(query, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                txt_staffnamegate.Text = staffname;
                txt_staffdeptgate.Text = Convert.ToString(ds.Tables[0].Rows[i]["dept"]);
                txt_staffdesgngate.Text = Convert.ToString(ds.Tables[0].Rows[i]["design"]);
                txt_pop_search.Text = Convert.ToString(ds.Tables[0].Rows[i]["Staff_code"]); ;
            }
            paneladd.Visible = true;
        }
    }

    public void staffcount()
    {
        try
        {
            string query = "";
            string Master1 = "";
            string stud = "";
            string values = "";
            if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
            {
                string group = Session["group_code"].ToString();
                if (group.Contains(';'))
                {
                    string[] group_semi = group.Split(';');
                    Master1 = group_semi[0].ToString();
                    query = "select * from Master_Settings where settings ='Gatepass Staff Permission' and group_code ='" + Master1 + "'"; //poo 12.12.17
                }
                else
                    query = "select * from Master_Settings where settings ='Gatepass Staff Permission' and group_code ='" + group + "'"; //poo 12.12.17
            }
            else
            {
                Master1 = Session["usercode"].ToString();
                query = "select * from Master_Settings where settings ='Gatepass Staff Permission' and usercode ='" + Master1 + "'";
            }
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string val = Convert.ToString(ds.Tables[0].Rows[i]["value"]);
                    staff_per_count = val;
                }
            }
        }
        catch
        {
        }
    }

    public void alternateRights()
    {
        try
        {
            alterrigths = string.Empty;
            string query = "";
            query = " select * from Master_Settings where settings ='Include Alternate Schedule' ";
            query += " select * from Master_Settings where settings ='Leave Apply in Alternate Schedule'";
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int rightVal = 0;
                int.TryParse(Convert.ToString(ds.Tables[0].Rows[0]["value"]), out rightVal);
                if (rightVal == 1)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string val = Convert.ToString(ds.Tables[1].Rows[i]["value"]);
                        alterrigths = val;
                    }
                }
            }
        }
        catch
        {
        }
    }

    public void bt_closedalter_Clik(object sender, EventArgs e)
    {
        Div3.Visible = false;
    }

    public void leaverequestsetting()
    {
        try
        {
            string query = "";
            string Master1 = "";
            string stud = "";
            string values = "";
            if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
            {
                string group = Session["group_code"].ToString();
                if (group.Contains(';'))
                {
                    string[] group_semi = group.Split(';');
                    Master1 = group_semi[0].ToString();
                    query = "select * from Master_Settings where settings ='Leave Approval Permission' and group_code ='" + Master1 + "'";
                }
                else
                    query = "select * from Master_Settings where settings ='Leave Approval Permission' and group_code ='" + group + "'"; // poo 11.12.17
            }
            else
            {
                query = "select * from Master_Settings where settings ='Leave Approval Permission' AND usercode='" + usercode + "'";
            }
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string val = Convert.ToString(ds.Tables[0].Rows[i]["value"]);
                    requestpermissioncheck = val;
                }
            }
        }
        catch
        {
        }
    }

    public void altersubject()
    {
        collegecode1 = Convert.ToString(Session["collegecode"]);
        sp_appsub.Visible = false;
        DataTable dt = new DataTable();
        Hashtable hatalt = new Hashtable();
        // degbatchsem();
        string dayord = "";
        string Altdayord = "";
        string batch = "";
        string deg = "";
        string sem = "";
        string sec = "";
        string day_value = "", srt_day = "", dt1 = "", dt2 = "", strsec = "";
        string[] sp = deg_batch_sem.Split('-');
        if (deg_batch_sem != "")
        {
            batch = sp[0];
            deg = sp[1];
            sem = sp[2];
            sec = sp[3];
            strsec = " and sections in('" + sec + "')";
        }
        else
        {
            grid_altersub.Visible = false;
            sp_appsub.Visible = false;
            return;
        }
        stafcodealter = Convert.ToString(txt_staff_code.Text);
        dt.Columns.Add("Dummy0");
        dt.Columns.Add("Dummy");
        dt.Columns.Add("Dummy5");
        dt.Columns.Add("Dummy3");
        dt.Columns.Add("Dummy4");
        dt.Columns.Add("Dummy6");
        dt.Columns.Add("Dummy2");
        dt.Columns.Add("Dummy1");
        DataRow dr1;
        DateTime fromdate1 = new DateTime();
        fromdate1 = TextToDate(txt_frm);
        DateTime todate1 = new DateTime();
        todate1 = TextToDate(txt_to);
        string dayy = "";
        for (; fromdate1 <= todate1; )
        {
            startdate1 = fromdate1.ToString("dd/MM/yyyy");
            dayy = fromdate1.ToString("dddd");
            string[] dt1_split = startdate1.Split('/');
            dt1 = dt1_split[1] + "-" + dt1_split[0] + "-" + dt1_split[2];
            DateTime dtnew = new DateTime();
            dtnew = Convert.ToDateTime(dt1_split[1] + "/" + dt1_split[0] + "/" + dt1_split[2]);
            DateTime startdate = Convert.ToDateTime(d2.GetFunction("select start_date from seminfo where degree_code='" + deg + "' and semester='" + sem + "' and batch_year='" + batch + "' "));
            string start_dayorder = d2.GetFunction("select starting_dayorder from seminfo where degree_code='" + deg + "' and semester='" + sem + "' and batch_year='" + batch + "'");
            if (startdate.ToString() != "" && startdate.ToString() != null)
                day_value = startdate.ToString("ddd");
            con.Close();
            con.Open();
            SqlDataReader dr;
            cmd = new SqlCommand("Select No_of_hrs_per_day,schorder,nodays from periodattndschedule where degree_code='" + deg + "' and semester = '" + sem + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows == true)
            {
                if ((dr["No_of_hrs_per_day"].ToString()) != "")
                {
                    intNHrs = Convert.ToInt32(dr["No_of_hrs_per_day"]);
                    SchOrder = Convert.ToInt32(dr["schorder"]);
                    nodays = Convert.ToInt32(dr["nodays"]);
                }
            }
            if (intNHrs > 0)
            {
                if (SchOrder != 0)
                    srt_day = (Convert.ToDateTime(dt1)).ToString("ddd");
                else
                {
                    string[] sps = dtnew.ToString().Split('/');
                    string curdate = sps[0] + '/' + sps[1] + '/' + sps[2];//mag
                    srt_day = d2.findday(curdate.ToString(), deg, sem, batch, startdate.ToString(), nodays.ToString(), start_dayorder.ToString());
                }
            }
            string sql1 = "";
            string Strsql = "";
            for (int i_loop = 1; i_loop <= intNHrs; i_loop++)
            {
                if (sql1 == "")
                    sql1 = sql1 + srt_day + Convert.ToString(i_loop);
                else
                    sql1 = sql1 + " - " + srt_day + Convert.ToString(i_loop);
            }
            if (srt_day != "")
            {
                string altersech = "";
                string dayclu = srt_day;
                string value = "";
                string note = "";
                string day = "";
                string altday = "";
                hourstaff = 0;
                string[] array = sql1.Split('-');
                string add = "";
                for (int j = 0; j < array.Length; j++)
                {
                    dayord = d2.GetFunction("select " + array[j] + " from semester_schedule where degree_code='" + deg + "'and semester='" + sem + "' and batch_year='" + batch + "' and sections='" + sec + "'");
                    string[] dsplt = dayord.Split('-');
                    for (int k = 0; k < dsplt.Length; k++)
                    {
                        if (dsplt[k] == stafcodealter)
                        {
                            if (day == "")
                                day = array[j];
                            else
                                day = day + "-" + array[j];
                        }
                    }
                }

                //Rajkumar for second Alter-----------------------------------------
                for (int j = 0; j < array.Length; j++)
                {
                    Altdayord = d2.GetFunction("select " + array[j] + " from semester_schedule where degree_code='" + deg + "'and semester='" + sem + "' and batch_year='" + batch + "' and sections='" + sec + "'");
                    string[] dsplt = Altdayord.Split('-');
                    for (int k = 0; k < 1; k++)
                    {
                        //  if (dsplt[k] == stafcodealter)//mas
                        {
                            if (altday == "")
                                altday = array[j];
                            else
                                altday = altday + "-" + array[j];
                        }
                    }
                }



                string[] daysp = day.Split('-');
                for (int j = 0; j < array.Length; j++)
                {
                    //  for (int mns = 0; mns < daysp.Length; mns++)
                    //{
                    string ss = "";
                    // string qur = d2.GetFunction("select " + array[j] + " from Alternate_Schedule where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' and fromdate='" + dt1 + "' ");
                    string qur = d2.GetFunction("select " + array[j] + " from tbl_alter_schedule_Details where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' and fromdate='" + dt1 + "' and No_of_Alter='1' ");
                    string sub = "";
                    string sub1 = "";
                    string qur1 = "";
                    string stf1 = "";
                    string stf = "";
                    string stfcode = "";
                    string staf1code = "";
                    if ((qur != ""))
                    {
                        if ((qur != "0"))
                        {
                            string[] q = qur.Split('-');
                            qur1 = d2.GetFunction("select " + array[j] + " from semester_schedule where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' ");
                            string[] qq = qur1.Split('-');

                            string[] splq1 = qur1.Split(';');

                            bool isfalse = false;
                            for (int i = 0; i < splq1.Length; i++)
                            {
                                string[] q1 = splq1[i].Split('-');
                                //multiple staff time table alter
                                if (q1.Length > 1)
                                {
                                    for (int row = 1; row < q1.Length - 1; row++)
                                    {
                                        if (q1[row] == stafcodealter)
                                        {
                                            sub = d2.GetFunction("select subject_name from subject where subject_no='" + q[0] + "'");
                                            sub1 = d2.GetFunction("select subject_name from subject where subject_no='" + qq[0] + "'");
                                            if (q1.Length >= 2)
                                            {
                                                stf = d2.GetFunction("select staff_name from staffmaster where staff_code='" + q[1] + "'");
                                                stfcode = Convert.ToString(txt_staff_code.Text);
                                                staf1code = Convert.ToString(q[1]);
                                            }
                                            stf1 = d2.GetFunction("select staff_name from staffmaster where staff_code='" + txt_staff_code.Text + "'");
                                            ss = array[j];
                                            tblday = array[j];
                                            dayfuction();
                                            ss = curday;
                                            isfalse = true;
                                        }
                                    }
                                }
                            }
                            if (!isfalse)
                            {
                                string[] splq2 = qur.Split(';');
                                for (int ii = 0; ii < splq2.Length; ii++)
                                {
                                    string[] q2 = splq2[ii].Split('-');
                                    //multiple staff time table alter
                                    if (q2.Length > 1)
                                    {
                                        for (int row = 1; row < q2.Length - 1; row++)
                                        {
                                            if (q2[row] == stafcodealter)
                                            {
                                                sub = d2.GetFunction("select subject_name from subject where subject_no='" + qq[0] + "'");
                                                sub1 = d2.GetFunction("select subject_name from subject where subject_no='" + q[0] + "'");
                                                if (q2.Length >= 2)
                                                {
                                                    stf = d2.GetFunction("select staff_name from staffmaster where staff_code='" + qq[1] + "'");
                                                    stfcode = Convert.ToString(txt_staff_code.Text);
                                                    staf1code = Convert.ToString(qq[1]);
                                                }
                                                stf1 = d2.GetFunction("select staff_name from staffmaster where staff_code='" + txt_staff_code.Text + "'");
                                                ss = array[j];
                                                tblday = array[j];
                                                dayfuction();
                                                ss = curday;
                                            }
                                        }
                                    }
                                }
                            }
                            //}
                        }
                        else
                        {
                            string tyyype = d2.GetFunction("select LinkValue  from InsSettings where LinkName like 'Staff Holiday By Staff Type%' and college_code='" + collegecode1 + "'");
                            if (tyyype == "0")
                            {
                                string code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                                string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806
                                string get = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
                                string sunday = dtnew.ToString("dddd");
                                string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
                                if (get != "0" || sunday == "Sunday")
                                    ss = dayy + "-Holiday";
                                else if (get1 != "0")
                                    ss = dayy + "-No Classes";
                                else
                                    ss = dayy;
                                stf = "-";
                                sub = "-";
                                stf1 = "-";
                                sub1 = "-";
                                stfcode = "-";
                                staf1code = "-";
                            }
                            else
                            {
                                string code = d2.GetFunction("select StfType from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                                string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806
                                string get = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "'and dept_code='" + deptCode + "'");
                                string sunday = dtnew.ToString("dddd");
                                string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
                                if (get != "0" || sunday == "Sunday")
                                    ss = dayy + "-Holiday";
                                else if (get1 != "0")
                                    ss = dayy + "-No Classes";
                                else
                                    ss = dayy;
                                stf = "-";
                                sub = "-";
                                stf1 = "-";
                                sub1 = "-";
                                stfcode = "-";
                                staf1code = "-";
                            }
                        }
                    }
                    else
                    {
                        string code = d2.GetFunction("select StfType from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                        string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                        string get = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
                        string sunday = dtnew.ToString("dddd");
                        string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
                        if (get != "0" || sunday == "Sunday")
                            ss = dayy + "-Holiday";
                        else if (get1 != "0")
                            ss = dayy + "-No Classes";
                        else
                            ss = dayy;
                        ss = array[j];
                        tblday = array[j];
                        dayfuction();
                        ss = curday;
                        stf = "-";
                        sub = "-";
                        stf1 = "-";
                        sub1 = "-";
                        stfcode = "-";
                        staf1code = "-";
                    }
                    if (!string.IsNullOrEmpty(stf) && stf != "-" && sub != "-" && !string.IsNullOrEmpty(sub) && stf1 != "-" && !string.IsNullOrEmpty(stf1) && sub1 != "-" && !string.IsNullOrEmpty(sub1) && stfcode != "-" && !string.IsNullOrEmpty(stfcode) && staf1code != "-" && !string.IsNullOrEmpty(staf1code))
                    {
                        if (grid_altersub.Rows.Count > 0)
                        {
                            foreach (GridViewRow item in grid_altersub.Rows)
                            {
                                Label date = (Label)item.FindControl("txtday0");
                                Label dayhr = (Label)item.FindControl("txtday1");
                                Label stafcode = (Label)item.FindControl("txtday22");
                                Label staff = (Label)item.FindControl("txtday2");
                                Label subj = (Label)item.FindControl("txtday3");
                                Label alterstafcode = (Label)item.FindControl("txtday44");
                                Label alterstaf = (Label)item.FindControl("txtday4");
                                Label altersub = (Label)item.FindControl("txtday5");
                                dr1 = dt.NewRow();
                                dr1[0] = Convert.ToString(date.Text);
                                dr1[1] = Convert.ToString(dayhr.Text);
                                dr1[2] = Convert.ToString(stafcode.Text);
                                dr1[3] = Convert.ToString(staff.Text);
                                dr1[4] = Convert.ToString(subj.Text);
                                dr1[5] = Convert.ToString(alterstafcode.Text);
                                dr1[6] = Convert.ToString(alterstaf.Text);
                                dr1[7] = Convert.ToString(altersub.Text);
                                dt.Rows.Add(dr1);
                            }
                        }
                        dr1 = dt.NewRow();
                        dr1[0] = startdate1;
                        dr1[1] = ss;
                        dr1[2] = stfcode;
                        dr1[3] = stf1;
                        dr1[4] = sub1;
                        dr1[5] = staf1code;
                        dr1[6] = stf;
                        dr1[7] = sub;
                        dt.Rows.Add(dr1);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        grid_altersub.DataSource = dt;
                        grid_altersub.DataBind();
                        grid_altersub.Visible = true;
                        sp_appsub.Visible = true;
                    }
                    else
                        sp_appsub.Visible = false;
                    dt.Clear();
                }




                DataTable dtNoalt = dir.selectDataTable("select * from tbl_alter_schedule_Details where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' and fromdate='" + dt1 + "'");
                if (dtNoalt.Rows.Count > 0)
                {
                    int noaltCount = dtNoalt.Rows.Count;
                    if (noaltCount > 1)
                    {
                        //for (int ii = 1; ii < noaltCount; ii++)
                        //{
                        //    int noalt = ii;
                        if (!string.IsNullOrEmpty(altday))
                        {
                            string[] altdaysp = altday.Split('-');
                            for (int j = 0; j < array.Length; j++)
                            {
                                string ss = "";
                                string qur = d2.GetFunction("select " + array[j] + " from tbl_alter_schedule_Details where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' and fromdate='" + dt1 + "' and No_of_Alter='2'");//and No_of_Alter='" + noalt + "'
                                string sub = "";
                                string sub1 = "";
                                string qur1 = "";
                                string stf1 = "";
                                string stf = "";
                                string stfcode = "";
                                string staf1code = "";

                                if ((qur != ""))
                                {
                                    //if (!hatalt.ContainsKey(dt1 + "-" + array[j]))
                                    {
                                        // hatalt.Add()
                                        if ((qur != "0"))
                                        {
                                            string[] q = qur.Split('-');
                                            qur1 = d2.GetFunction("select " + array[j] + " from tbl_alter_schedule_Details where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' and fromdate='" + dt1 + "' and No_of_Alter='1'");
                                            string[] splq1 = qur1.Split(';');
                                            for (int i = 0; i < splq1.Length; i++)
                                            {
                                                string[] q1 = splq1[i].Split('-');
                                                //multiple staff time table alter
                                                if (q1.Length > 1)
                                                {
                                                    for (int row = 1; row < q1.Length - 1; row++)
                                                    {
                                                        if (q1[row] == stafcodealter)
                                                        {
                                                            sub = d2.GetFunction("select subject_name from subject where subject_no='" + q[0] + "'");
                                                            sub1 = d2.GetFunction("select subject_name from subject where subject_no='" + q1[0] + "'");
                                                            if (q1.Length >= 2)
                                                            {
                                                                stf = d2.GetFunction("select staff_name from staffmaster where staff_code='" + q[1] + "'");
                                                                stfcode = Convert.ToString(txt_staff_code.Text);
                                                                staf1code = Convert.ToString(q[1]);
                                                            }
                                                            stf1 = d2.GetFunction("select staff_name from staffmaster where staff_code='" + txt_staff_code.Text + "'");
                                                            ss = array[j];
                                                            tblday = array[j];
                                                            dayfuction();
                                                            ss = curday;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string tyyype = d2.GetFunction("select LinkValue  from InsSettings where LinkName like 'Staff Holiday By Staff Type%' and college_code='" + collegecode1 + "'");
                                            if (tyyype == "0")
                                            {
                                                string code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                                                string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806
                                                string get = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
                                                string sunday = dtnew.ToString("dddd");
                                                string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
                                                if (get != "0" || sunday == "Sunday")
                                                    ss = dayy + "-Holiday";
                                                else if (get1 != "0")
                                                    ss = dayy + "-No Classes";
                                                else
                                                    ss = dayy;
                                                stf = "-";
                                                sub = "-";
                                                stf1 = "-";
                                                sub1 = "-";
                                                stfcode = "-";
                                                staf1code = "-";
                                            }
                                            else
                                            {
                                                string code = d2.GetFunction("select StfType from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                                                string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806
                                                string get = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "'and dept_code='" + deptCode + "'");
                                                string sunday = dtnew.ToString("dddd");
                                                string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
                                                if (get != "0" || sunday == "Sunday")
                                                    ss = dayy + "-Holiday";
                                                else if (get1 != "0")
                                                    ss = dayy + "-No Classes";
                                                else
                                                    ss = dayy;
                                                stf = "-";
                                                sub = "-";
                                                stf1 = "-";
                                                sub1 = "-";
                                                stfcode = "-";
                                                staf1code = "-";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string code = d2.GetFunction("select StfType from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                                    string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
                                    string get = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
                                    string sunday = dtnew.ToString("dddd");
                                    string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
                                    if (get != "0" || sunday == "Sunday")
                                        ss = dayy + "-Holiday";
                                    else if (get1 != "0")
                                        ss = dayy + "-No Classes";
                                    else
                                        ss = dayy;
                                    ss = altdaysp[j];
                                    tblday = altdaysp[j];
                                    dayfuction();
                                    ss = curday;
                                    stf = "-";
                                    sub = "-";
                                    stf1 = "-";
                                    sub1 = "-";
                                    stfcode = "-";
                                    staf1code = "-";
                                }
                                if (!string.IsNullOrEmpty(stf) && stf != "-" && sub != "-" && !string.IsNullOrEmpty(sub) && stf1 != "-" && !string.IsNullOrEmpty(stf1) && sub1 != "-" && !string.IsNullOrEmpty(sub1) && stfcode != "-" && !string.IsNullOrEmpty(stfcode) && staf1code != "-" && !string.IsNullOrEmpty(staf1code))
                                {
                                    if (grid_altersub.Rows.Count > 0)
                                    {
                                        foreach (GridViewRow item in grid_altersub.Rows)
                                        {
                                            Label date = (Label)item.FindControl("txtday0");
                                            Label dayhr = (Label)item.FindControl("txtday1");
                                            Label stafcode = (Label)item.FindControl("txtday22");
                                            Label staff = (Label)item.FindControl("txtday2");
                                            Label subj = (Label)item.FindControl("txtday3");
                                            Label alterstafcode = (Label)item.FindControl("txtday44");
                                            Label alterstaf = (Label)item.FindControl("txtday4");
                                            Label altersub = (Label)item.FindControl("txtday5");
                                            dr1 = dt.NewRow();
                                            dr1[0] = Convert.ToString(date.Text);
                                            dr1[1] = Convert.ToString(dayhr.Text);
                                            dr1[2] = Convert.ToString(stafcode.Text);
                                            dr1[3] = Convert.ToString(staff.Text);
                                            dr1[4] = Convert.ToString(subj.Text);
                                            dr1[5] = Convert.ToString(alterstafcode.Text);
                                            dr1[6] = Convert.ToString(alterstaf.Text);
                                            dr1[7] = Convert.ToString(altersub.Text);
                                            dt.Rows.Add(dr1);
                                        }
                                    }
                                    dr1 = dt.NewRow();
                                    dr1[0] = startdate1;
                                    dr1[1] = ss;
                                    dr1[2] = stfcode;
                                    dr1[3] = stf1;
                                    dr1[4] = sub1;
                                    dr1[5] = staf1code;
                                    dr1[6] = stf;
                                    dr1[7] = sub;
                                    dt.Rows.Add(dr1);
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    grid_altersub.DataSource = dt;
                                    grid_altersub.DataBind();
                                    grid_altersub.Visible = true;
                                    sp_appsub.Visible = true;
                                }
                                else
                                    sp_appsub.Visible = false;
                                dt.Clear();
                                dt.Clear();

                            }
                        }
                        //}
                    }
                }
                //---------------------------------------------------


            }
            fromdate1 = fromdate1.AddDays(1);
        }
    }

    //public void altersubject()
    //{
    //    collegecode1 = Convert.ToString(Session["collegecode"]);
    //    sp_appsub.Visible = false;
    //    DataTable dt = new DataTable();
    //    // degbatchsem();
    //    string dayord = "";
    //    string batch = "";
    //    string deg = "";
    //    string sem = "";
    //    string sec = "";
    //    string day_value = "", srt_day = "", dt1 = "", dt2 = "", strsec = "";
    //    string[] sp = deg_batch_sem.Split('-');
    //    if (deg_batch_sem != "")
    //    {
    //        batch = sp[0];
    //        deg = sp[1];
    //        sem = sp[2];
    //        sec = sp[3];
    //        strsec = " and sections in('" + sec + "')";
    //    }
    //    else
    //    {
    //        grid_altersub.Visible = false;
    //        sp_appsub.Visible = false;
    //        return;
    //    }
    //    stafcodealter = Convert.ToString(txt_staff_code.Text);
    //    dt.Columns.Add("Dummy0");
    //    dt.Columns.Add("Dummy");
    //    dt.Columns.Add("Dummy5");
    //    dt.Columns.Add("Dummy3");
    //    dt.Columns.Add("Dummy4");
    //    dt.Columns.Add("Dummy6");
    //    dt.Columns.Add("Dummy2");
    //    dt.Columns.Add("Dummy1");
    //    DataRow dr1;
    //    DateTime fromdate1 = new DateTime();
    //    fromdate1 = TextToDate(txt_frm);
    //    DateTime todate1 = new DateTime();
    //    todate1 = TextToDate(txt_to);
    //    string dayy = "";
    //    for (; fromdate1 <= todate1; )
    //    {
    //        startdate1 = fromdate1.ToString("dd/MM/yyyy");
    //        dayy = fromdate1.ToString("dddd");
    //        string[] dt1_split = startdate1.Split('/');
    //        dt1 = dt1_split[1] + "-" + dt1_split[0] + "-" + dt1_split[2];
    //        DateTime dtnew = new DateTime();
    //        dtnew = Convert.ToDateTime(dt1_split[1] + "/" + dt1_split[0] + "/" + dt1_split[2]);
    //        DateTime startdate = Convert.ToDateTime(d2.GetFunction("select start_date from seminfo where degree_code='" + deg + "' and semester='" + sem + "' and batch_year='" + batch + "' "));
    //        string start_dayorder = d2.GetFunction("select starting_dayorder from seminfo where degree_code='" + deg + "' and semester='" + sem + "' and batch_year='" + batch + "'");
    //        if (startdate.ToString() != "" && startdate.ToString() != null)
    //            day_value = startdate.ToString("ddd");
    //        con.Close();
    //        con.Open();
    //        SqlDataReader dr;
    //        cmd = new SqlCommand("Select No_of_hrs_per_day,schorder,nodays from periodattndschedule where degree_code='" + deg + "' and semester = '" + sem + "'", con);
    //        dr = cmd.ExecuteReader();
    //        dr.Read();
    //        if (dr.HasRows == true)
    //        {
    //            if ((dr["No_of_hrs_per_day"].ToString()) != "")
    //            {
    //                intNHrs = Convert.ToInt32(dr["No_of_hrs_per_day"]);
    //                SchOrder = Convert.ToInt32(dr["schorder"]);
    //                nodays = Convert.ToInt32(dr["nodays"]);
    //            }
    //        }
    //        if (intNHrs > 0)
    //        {
    //            if (SchOrder != 0)
    //                srt_day = (Convert.ToDateTime(dt1)).ToString("ddd");
    //            else
    //            {
    //                string[] sps = dtnew.ToString().Split('/');
    //                string curdate = sps[0] + '/' + sps[1] + '/' + sps[2];//mag
    //                srt_day = d2.findday(curdate.ToString(), deg, sem, batch, startdate.ToString(), nodays.ToString(), start_dayorder.ToString());
    //            }
    //        }
    //        string sql1 = "";
    //        string Strsql = "";
    //        for (int i_loop = 1; i_loop <= intNHrs; i_loop++)
    //        {
    //            if (sql1 == "")
    //                sql1 = sql1 + srt_day + Convert.ToString(i_loop);
    //            else
    //                sql1 = sql1 + " - " + srt_day + Convert.ToString(i_loop);
    //        }
    //        if (srt_day != "")
    //        {
    //            string altersech = "";
    //            string dayclu = srt_day;
    //            string value = "";
    //            string note = "";
    //            string day = "";
    //            hourstaff = 0;
    //            string[] array = sql1.Split('-');
    //            string add = "";
    //            for (int j = 0; j < array.Length; j++)
    //            {
    //                dayord = d2.GetFunction("select " + array[j] + " from semester_schedule where degree_code='" + deg + "'and semester='" + sem + "' and batch_year='" + batch + "' and sections='" + sec + "'");
    //                string[] dsplt = dayord.Split('-');
    //                for (int k = 0; k < dsplt.Length; k++)
    //                {
    //                    if (dsplt[k] == stafcodealter)
    //                    {
    //                        if (day == "")
    //                            day = array[j];
    //                        else
    //                            day = day + "-" + array[j];
    //                    }
    //                }
    //            }
    //            string[] daysp = day.Split('-');
    //            for (int j = 0; j < daysp.Length; j++)
    //            {
    //                string ss = "";
    //                string qur = d2.GetFunction("select " + daysp[j] + " from Alternate_Schedule where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' and fromdate='" + dt1 + "' ");
    //                string sub = "";
    //                string sub1 = "";
    //                string qur1 = "";
    //                string stf1 = "";
    //                string stf = "";
    //                string stfcode = "";
    //                string staf1code = "";
    //                if ((qur != ""))
    //                {
    //                    if ((qur != "0"))
    //                    {
    //                        string[] q = qur.Split('-');
    //                        qur1 = d2.GetFunction("select " + daysp[j] + " from semester_schedule where  degree_code='" + deg + "' and batch_year='" + batch + "' " + strsec + " and semester='" + sem + "' ");
    //                        string[] splq1 = qur1.Split(';');
    //                        for (int i = 0; i < splq1.Length; i++)
    //                        {
    //                            string[] q1 = splq1[i].Split('-');
    //                            //multiple staff time table alter
    //                            if (q1.Length > 1)
    //                            {
    //                                for (int row = 1; row < q1.Length - 1; row++)
    //                                {
    //                                    if (q1[row] == stafcodealter)
    //                                    {
    //                                        sub = d2.GetFunction("select subject_name from subject where subject_no='" + q[0] + "'");
    //                                        sub1 = d2.GetFunction("select subject_name from subject where subject_no='" + q1[0] + "'");
    //                                        if (q1.Length >= 2)
    //                                        {
    //                                            stf = d2.GetFunction("select staff_name from staffmaster where staff_code='" + q[1] + "'");
    //                                            stfcode = Convert.ToString(txt_staff_code.Text);
    //                                            staf1code = Convert.ToString(q[1]);
    //                                        }
    //                                        stf1 = d2.GetFunction("select staff_name from staffmaster where staff_code='" + txt_staff_code.Text + "'");
    //                                        ss = daysp[j];
    //                                        tblday = daysp[j];
    //                                        dayfuction();
    //                                        ss = curday;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        string tyyype = d2.GetFunction("select LinkValue  from InsSettings where LinkName like 'Staff Holiday By Staff Type%' and college_code='" + collegecode1 + "'");
    //                        if (tyyype == "0")
    //                        {
    //                            string code = d2.GetFunction("select category_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
    //                            string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806
    //                            string get = d2.GetFunction("select holiday_date from holidayStaff where category_code ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
    //                            string sunday = dtnew.ToString("dddd");
    //                            string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
    //                            if (get != "0" || sunday == "Sunday")
    //                                ss = dayy + "-Holiday";
    //                            else if (get1 != "0")
    //                                ss = dayy + "-No Classes";
    //                            else
    //                                ss = dayy;
    //                            stf = "-";
    //                            sub = "-";
    //                            stf1 = "-";
    //                            sub1 = "-";
    //                            stfcode = "-";
    //                            staf1code = "-";
    //                        }
    //                        else
    //                        {
    //                            string code = d2.GetFunction("select StfType from stafftrans where staff_code='" + txt_staff_code.Text + "'");
    //                            string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");//delsi1806
    //                            string get = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "'and dept_code='" + deptCode + "'");
    //                            string sunday = dtnew.ToString("dddd");
    //                            string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
    //                            if (get != "0" || sunday == "Sunday")
    //                                ss = dayy + "-Holiday";
    //                            else if (get1 != "0")
    //                                ss = dayy + "-No Classes";
    //                            else
    //                                ss = dayy;
    //                            stf = "-";
    //                            sub = "-";
    //                            stf1 = "-";
    //                            sub1 = "-";
    //                            stfcode = "-";
    //                            staf1code = "-";
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    string code = d2.GetFunction("select StfType from stafftrans where staff_code='" + txt_staff_code.Text + "'");
    //                    string deptCode = d2.GetFunction("select dept_code from stafftrans where staff_code='" + txt_staff_code.Text + "'");
    //                    string get = d2.GetFunction("select holiday_date from holidayStaff where StfType ='" + code + "' and  holiday_date='" + dt1 + "' and college_code ='" + collegecode1 + "' and dept_code='" + deptCode + "'");
    //                    string sunday = dtnew.ToString("dddd");
    //                    string get1 = d2.GetFunction("select holiday_date from holidayStudents where semester='" + sem + "' and degree_code='" + deg + "' and holiday_date='" + dt1 + "'");
    //                    if (get != "0" || sunday == "Sunday")
    //                        ss = dayy + "-Holiday";
    //                    else if (get1 != "0")
    //                        ss = dayy + "-No Classes";
    //                    else
    //                        ss = dayy;
    //                    ss = daysp[j];
    //                    tblday = daysp[j];
    //                    dayfuction();
    //                    ss = curday;
    //                    stf = "-";
    //                    sub = "-";
    //                    stf1 = "-";
    //                    sub1 = "-";
    //                    stfcode = "-";
    //                    staf1code = "-";
    //                }
    //                if (!string.IsNullOrEmpty(stf) && stf != "-" && sub != "-" && !string.IsNullOrEmpty(sub) && stf1 != "-" && !string.IsNullOrEmpty(stf1) && sub1 != "-" && !string.IsNullOrEmpty(sub1) && stfcode != "-" && !string.IsNullOrEmpty(stfcode) && staf1code != "-" && !string.IsNullOrEmpty(staf1code))
    //                {
    //                    if (grid_altersub.Rows.Count > 0)
    //                    {
    //                        foreach (GridViewRow item in grid_altersub.Rows)
    //                        {
    //                            Label date = (Label)item.FindControl("txtday0");
    //                            Label dayhr = (Label)item.FindControl("txtday1");
    //                            Label stafcode = (Label)item.FindControl("txtday22");
    //                            Label staff = (Label)item.FindControl("txtday2");
    //                            Label subj = (Label)item.FindControl("txtday3");
    //                            Label alterstafcode = (Label)item.FindControl("txtday44");
    //                            Label alterstaf = (Label)item.FindControl("txtday4");
    //                            Label altersub = (Label)item.FindControl("txtday5");
    //                            dr1 = dt.NewRow();
    //                            dr1[0] = Convert.ToString(date.Text);
    //                            dr1[1] = Convert.ToString(dayhr.Text);
    //                            dr1[2] = Convert.ToString(stafcode.Text);
    //                            dr1[3] = Convert.ToString(staff.Text);
    //                            dr1[4] = Convert.ToString(subj.Text);
    //                            dr1[5] = Convert.ToString(alterstafcode.Text);
    //                            dr1[6] = Convert.ToString(alterstaf.Text);
    //                            dr1[7] = Convert.ToString(altersub.Text);
    //                            dt.Rows.Add(dr1);
    //                        }
    //                    }
    //                    dr1 = dt.NewRow();
    //                    dr1[0] = startdate1;
    //                    dr1[1] = ss;
    //                    dr1[2] = stfcode;
    //                    dr1[3] = stf1;
    //                    dr1[4] = sub1;
    //                    dr1[5] = staf1code;
    //                    dr1[6] = stf;
    //                    dr1[7] = sub;
    //                    dt.Rows.Add(dr1);
    //                }
    //                if (dt.Rows.Count > 0)
    //                {
    //                    grid_altersub.DataSource = dt;
    //                    grid_altersub.DataBind();
    //                    grid_altersub.Visible = true;
    //                    sp_appsub.Visible = true;
    //                }
    //                else
    //                    sp_appsub.Visible = false;
    //                dt.Clear();
    //            }
    //        }
    //        fromdate1 = fromdate1.AddDays(1);
    //    }
    //}

    protected void grid_altersub_databound(object sender, EventArgs e)
    {
        try
        {
            int curindx = 0;
            int curLength = 0;
            string datecheck = "";
            for (int i = grid_altersub.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = grid_altersub.Rows[i];
                GridViewRow previousRow = grid_altersub.Rows[i - 1];
                for (int j = 0; j <= 7; j++)
                {
                    Label lnldate = (Label)row.FindControl("txtday0");
                    Label lnlname = (Label)row.FindControl("txtday1");
                    Label lnlname1 = (Label)previousRow.FindControl("txtday1");
                    if (lnlname.Text == lnlname1.Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                    if (lnlname.Text == "Sunday-Holiday")
                    {
                        row.Cells[1].ColumnSpan = 8;
                        row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        row.Cells[1].ForeColor = Color.Red;
                        row.Cells[1].BorderColor = Color.Black;
                        row.Cells[2].Visible = false;
                        row.Cells[3].Visible = false;
                        row.Cells[4].Visible = false;
                        row.Cells[5].Visible = false;
                        row.Cells[6].Visible = false;
                        row.Cells[7].Visible = false;
                    }
                    DateTime dt = new DateTime();
                    string datee = Convert.ToString(lnldate.Text);
                    string[] dt1_split = datee.Split('/');
                    dt = Convert.ToDateTime(dt1_split[1] + "/" + dt1_split[0] + "/" + dt1_split[2]);
                    string day = dt.ToString("dddd");
                    string dayy = day + "-No Classes";
                    if (lnlname.Text == dayy)
                    {
                        row.Cells[1].ColumnSpan = 8;
                        row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        row.Cells[1].ForeColor = Color.Red;
                        row.Cells[1].BorderColor = Color.Black;
                        row.Cells[2].Visible = false;
                        row.Cells[3].Visible = false;
                        row.Cells[4].Visible = false;
                        row.Cells[5].Visible = false;
                        row.Cells[6].Visible = false;
                        row.Cells[7].Visible = false;
                    }
                    dayy = day + "-Holiday";
                    if (lnlname.Text == dayy)
                    {
                        row.Cells[1].ColumnSpan = 8;
                        row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        row.Cells[1].ForeColor = Color.Red;
                        row.Cells[1].BorderColor = Color.Black;
                        row.Cells[2].Visible = false;
                        row.Cells[3].Visible = false;
                        row.Cells[4].Visible = false;
                        row.Cells[5].Visible = false;
                        row.Cells[6].Visible = false;
                        row.Cells[7].Visible = false;
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void grid_altersub_rowdatabound(object sender, GridViewRowEventArgs e)
    {
    }

    public void dayfuction()
    {
        ArrayList a = new ArrayList();
        a.Add("Mon");
        a.Add("Tue");
        a.Add("Wed");
        a.Add("Thu");
        a.Add("Fri");
        a.Add("Sat");
        a.Add("Sun");
        string val = "";
        for (int i = 0; i < a.Count; i++)
        {
            for (int j = 1; j <= 9; j++)//magesh 16.8.18  for (int j = 1; j <9; j++) for licet
            {
                val = a[i] + "" + j;
                if (tblday.Trim().ToUpper() == val.ToUpper())
                {
                    if (a[i] == "Mon")
                    {
                        curday = "Monday-" + j + "hour";
                    }
                    if (a[i] == "Tue")
                    {
                        curday = "Tuesday-" + j + "hour";
                    }
                    if (a[i] == "Wed")
                    {
                        curday = "Wednesday-" + j + "hour";
                    }
                    if (a[i] == "Thu")
                    {
                        curday = "Thursday-" + j + "hour";
                    }
                    if (a[i] == "Fri")
                    {
                        curday = "Friday-" + j + "hour";
                    }
                    if (a[i] == "Sat")
                    {
                        curday = "Saturday-" + j + "hour";
                    }
                    if (a[i] == "Sun")
                    {
                        curday = "Sunday-" + j + "hour";
                    }
                }
            }
        }
    }

    public void bindgrid_approvalstaff()
    {
        string applid = "";


        leave_staff_login();
        if (requestpermissioncheck == "1")
        {
            applid = d2.GetFunction("select appl_id  from staff_appl_master sm, staffmaster s where sm.appl_no=s.appl_no and s.staff_code='" + txt_staff_code.Text + "' and s.resign=0 and s.settled=0");
        }
        else
        {
            applid = d2.GetFunction("select appl_id  from staff_appl_master sm, staffmaster s where sm.appl_no=s.appl_no and s.staff_code='" + staffcodesession + "' and s.resign=0 and s.settled=0");
        }
        if (applid != "0")
        {
            string query = "select * from RQ_RequestHierarchy where ReqStaffAppNo='" + applid + "' and RequestType=5 order by ReqStaffAppNo,ReqApproveStage,ReqAppPriority";
            ds = d2.select_method_wo_parameter(query, "Text");
            DataTable dt = new DataTable();
            dt.Columns.Add("Dummy");
            dt.Columns.Add("Dummy1");
            dt.Columns.Add("Dummy2");
            dt.Columns.Add("Dummy3");
            dt.Columns.Add("Dummy0");
            DataRow dr;
            chkrelived = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkrelived = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string staffcode = d2.GetFunction("select staff_code from staffmaster s,staff_appl_master sm where s.appl_no=sm.appl_no and s.resign=0 and s.settled=0 and sm.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                    if (staffcode == "0")
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "The Approval Staff Was Relived,Change The Hierarchy Setting";
                        chkrelived = 1;
                    }
                    else
                    {
                        chkrelived = 0;
                        string staffname = d2.GetFunction("select appl_name from staff_appl_master where appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                        //string dept = d2.GetFunction("select s.dept_name as dept from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and s.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                        //string desg = d2.GetFunction("select d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and s.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                        string dept = d2.GetFunction("select s.dept_name as dept from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0  and m.college_code=h.college_code and m.college_code=d.collegeCode  and s.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                        string desg = d2.GetFunction("select d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.college_code=h.college_code and m.college_code=d.collegeCode  and s.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                        string stagecount = Convert.ToString(ds.Tables[0].Rows[i]["ReqApproveStage"]);
                        pri_txt = Convert.ToString(ds.Tables[0].Rows[i]["ReqAppPriority"]);
                        abc1();
                        string stage = stagecount + "-" + con_txt;
                        dr = dt.NewRow();
                        dr[0] = staffname;
                        dr[1] = dept;
                        dr[2] = desg;
                        dr[3] = stage;
                        dr[4] = staffcode;
                        dt.Rows.Add(dr);
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                sp_appstaf.Visible = true;
                grid_approvalstaff.DataSource = dt;
                grid_approvalstaff.DataBind();
                grid_approvalstaff.Visible = true;
            }
            else
            {
                grid_approvalstaff.Visible = false;
            }
        }
    }

    protected void OnRowDataBound_grid_approvalstaff(object sender, GridViewRowEventArgs e)
    {
        string stafname = "";
        string approvestage = "";
        string qur = "";
        string appstaffnum = "";
        string staffapplid = d2.GetFunction("select sm.appl_id from staff_appl_master sm,staffmaster s where sm.appl_no=s.appl_no and staff_code='" + txt_staff_code.Text + "'");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var staffname = (Label)e.Row.FindControl("lblname");
            stafname = Convert.ToString(staffname.Text);
            string name = d2.GetFunction("select staff_name from staffmaster where staff_code='" + staffcodesession + "'");
        }
    }
    //Added By SaranyaDevi 13.4.2018

    public void bindgrid_Item_approvalstaff()
    {
        try
        {
            string applid = "";

            applid = d2.GetFunction("select appl_id  from staff_appl_master sm, staffmaster s where sm.appl_no=s.appl_no and s.staff_code='" + staffcodesession + "' and s.resign=0 and s.settled=0");

            if (applid != "0")
            {
                string query = "select * from RQ_RequestHierarchy where ReqStaffAppNo='" + applid + "' and RequestType=1 order by ReqStaffAppNo,ReqApproveStage,ReqAppPriority";
                ds.Clear();
                ds = d2.select_method_wo_parameter(query, "Text");
                DataTable dtitem = new DataTable();
                dtitem.Columns.Add("StaffName");
                dtitem.Columns.Add("Department");
                dtitem.Columns.Add("Designation");
                dtitem.Columns.Add("Stage");
                dtitem.Columns.Add("StaffCode");
                DataRow drow;
                chkrelived = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkrelived = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string staffcode = d2.GetFunction("select staff_code from staffmaster s,staff_appl_master sm where s.appl_no=sm.appl_no and s.resign=0 and s.settled=0 and sm.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                        if (staffcode == "0")
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Text = "The Approval Staff Was Relived,Change The Hierarchy Setting";
                            chkrelived = 1;
                        }
                        else
                        {
                            chkrelived = 0;
                            string staffname = d2.GetFunction("select appl_name from staff_appl_master where appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                            string dept = d2.GetFunction("select s.dept_name as dept from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0  and m.college_code=h.college_code and m.college_code=d.collegeCode  and s.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                            string desg = d2.GetFunction("select d.desig_name as design from staff_appl_master s,staffmaster m,stafftrans t,hrdept_master h,desig_master d where s.appl_no = m.appl_no and m.staff_code = t.staff_code and t.dept_code = h.dept_code and t.desig_code = d.desig_code and m.college_code = '" + Session["collegecode"] + "' and t.latestrec = 1 and m.resign = 0 and settled = 0 and m.college_code=h.college_code and m.college_code=d.collegeCode  and s.appl_id='" + Convert.ToString(ds.Tables[0].Rows[i]["ReqAppStaffAppNo"]) + "'");
                            string stagecount = Convert.ToString(ds.Tables[0].Rows[i]["ReqApproveStage"]);
                            pri_txt = Convert.ToString(ds.Tables[0].Rows[i]["ReqAppPriority"]);
                            abc1();
                            string stage = stagecount + "-" + con_txt;
                            drow = dtitem.NewRow();
                            drow[0] = staffname;
                            drow[1] = dept;
                            drow[2] = desg;
                            drow[3] = stage;
                            drow[4] = staffcode;
                            dtitem.Rows.Add(drow);
                        }
                    }
                }
                if (dtitem.Rows.Count > 0)
                {
                    sp_appstaff_Item.Visible = true;
                    grid_Item_approvalstaff.DataSource = dtitem;
                    grid_Item_approvalstaff.DataBind();
                    grid_Item_approvalstaff.Visible = true;
                }
                else
                {
                    grid_Item_approvalstaff.Visible = false;
                }
            }
        }
        catch
        {


        }
    }

    protected void OnRowDataBound_grid_Item_approvalstaff(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string itemstaffname = "";

            string staffapplid = d2.GetFunction("select sm.appl_id from staff_appl_master sm,staffmaster s where sm.appl_no=s.appl_no and staff_code='" + staffcodesession + "'");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var staffname1 = (Label)e.Row.FindControl("lblname1");
                itemstaffname = Convert.ToString(staffname1.Text);
                string name = d2.GetFunction("select staff_name from staffmaster where staff_code='" + staffcodesession + "'");
            }
        }

        catch
        {
        }


    }

    //End By SaranyaDevi 13.4.2018

    public void cb_staff_dept11_CheckedChanged(object sender, EventArgs e)
    {
        int cout = 0;
        txt_staff_dept11.Text = "--Select--";
        if (cb_staff_dept11.Checked == true)
        {
            cout++;
            for (int i = 0; i < cbl_staff_dept11.Items.Count; i++)
            {
                cbl_staff_dept11.Items[i].Selected = true;
            }
            txt_staff_dept11.Text = "Dept(" + (cbl_staff_dept11.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < cbl_staff_dept11.Items.Count; i++)
            {
                cbl_staff_dept11.Items[i].Selected = false;
            }
        }
    }

    public void cbl_staff_dept11_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        int commcount = 0;
        txt_staff_dept11.Text = "--Select--";
        for (i = 0; i < cbl_staff_dept11.Items.Count; i++)
        {
            if (cbl_staff_dept11.Items[i].Selected == true)
            {
                commcount = commcount + 1;
                cb_staff_dept11.Checked = false;
            }
        }
        if (commcount > 0)
        {
            if (commcount == cbl_staff_dept11.Items.Count)
            {
                cb_staff_dept11.Checked = true;
            }
            txt_staff_dept11.Text = "Dept(" + commcount.ToString() + ")";
        }
    }

    public void cb_staff_type111_CheckedChanged(object sender, EventArgs e)
    {
        int cout = 0;
        txt_staff_type11.Text = "--Select--";
        if (cb_staff_type111.Checked == true)
        {
            cout++;
            for (int i = 0; i < cbl_staff_type111.Items.Count; i++)
            {
                cbl_staff_type111.Items[i].Selected = true;
            }
            txt_staff_type11.Text = "Staff Type(" + (cbl_staff_type111.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < cbl_staff_type111.Items.Count; i++)
            {
                cbl_staff_type111.Items[i].Selected = false;
            }
        }
        bind_design1();
    }

    public void cb_staff_type111_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        int commcount = 0;
        txt_staff_type11.Text = "--Select--";
        for (i = 0; i < cbl_staff_type111.Items.Count; i++)
        {
            if (cbl_staff_type111.Items[i].Selected == true)
            {
                commcount = commcount + 1;
                cb_staff_type111.Checked = false;
            }
        }
        if (commcount > 0)
        {
            if (commcount == cbl_staff_type111.Items.Count)
            {
                cb_staff_type111.Checked = true;
            }
            txt_staff_type11.Text = "Staff Type(" + commcount.ToString() + ")";
        }
        bind_design1();
    }

    public void cb_staff_desn11_CheckedChanged(object sender, EventArgs e)
    {
        int cout = 0;
        txt_staff_desg111.Text = "--Select--";
        if (cb_staff_desn11.Checked == true)
        {
            cout++;
            for (int i = 0; i < cbl_staff_desn11.Items.Count; i++)
            {
                cbl_staff_desn11.Items[i].Selected = true;
            }
            txt_staff_desg111.Text = "Designation(" + (cbl_staff_desn11.Items.Count) + ")";
        }
        else
        {
            for (int i = 0; i < cbl_staff_desn11.Items.Count; i++)
            {
                cbl_staff_desn11.Items[i].Selected = false;
            }
        }
    }

    public void cbl_staff_desn11_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        int commcount = 0;
        txt_staff_desg111.Text = "--Select--";
        for (i = 0; i < cbl_staff_desn11.Items.Count; i++)
        {
            if (cbl_staff_desn11.Items[i].Selected == true)
            {
                commcount = commcount + 1;
                cb_staff_desn11.Checked = false;
            }
        }
        if (commcount > 0)
        {
            if (commcount == cbl_staff_desn11.Items.Count)
            {
                cb_staff_desn11.Checked = true;
            }
            txt_staff_desg111.Text = "Designation(" + commcount.ToString() + ")";
        }
    }

    [WebMethod]
    public static string Checkstaffcode(string code)
    {
        string returnValue = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            DAccess2 dd = new DAccess2();
            bool flage = false;
            if (code != "")
            {
                string query = "";
                query = "select distinct s.staff_code from staffmaster s,staff_appl_master sa,hrdept_master hr,desig_master dm where s.appl_no=sa.appl_no and sa.dept_code=hr.dept_code and dm.desig_code=sa.desig_code and settled=0 and resign =0 and s.staff_code='" + code + "'";
                ds = dd.select_method_wo_parameter(query, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                }
                else
                {
                    flage = true;
                }
                if (flage == true)
                {
                    returnValue = "0";
                }
                else
                {
                    returnValue = "1";
                }
            }
            else
            {
                returnValue = "0";
            }
        }
        catch (SqlException ex)
        {
            returnValue = "error" + ex.ToString();
        }
        return returnValue;
    }

    public void img_divleavedis_Click(object sender, EventArgs e)
    {
        divleavedis.Visible = false;
    }

    public void imgalterstafcls_Click(object sender, EventArgs e)
    {
        divalterstaff.Visible = false;
        afteralterSchedule();
    }

    public void bindleavedatechange()
    {
        try
        {
            double llcount = 0;
            totalleave.Clear();
            double addtot = 0;
            string actual = "";
            string currentYear = DateTime.Now.Year.ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add("Month");
            DataRow dr = null;
            int tott = 0;
            string applid = d2.GetFunction("select appl_id from staff_appl_master a,staffmaster s where a.appl_no=s.appl_no and staff_code='" + txt_staff_code.Text + "'");
            string queryObject = "select * from hrpaymonths where college_code='" + ddlcollege.SelectedItem.Value + "'";
            ds = d2.select_method_wo_parameter(queryObject, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "select * from individual_leave_type where staff_code='" + txt_staff_code.Text + "' and college_code='" + ddlcollege.SelectedItem.Value + "'";
                ds2.Clear();
                ds2 = d2.select_method_wo_parameter(query, "Text");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    string[] spl_type = ds2.Tables[0].Rows[0]["leavetype"].ToString().Split(new Char[] { '\\' });
                    int col_cnt = 0;
                    int leavetypecount = spl_type.GetUpperBound(0);
                    if (leavetypecount == 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Text = "Please Set The leave Detail For This Staff";
                        return;
                    }
                    double tot_leave = 0;
                    string leavefromdate = "";
                    string leavetodate = "";
                    string ishalfdate = "";
                    string halfdaydate = "";
                    int finaldate = 0;
                    string sleave = "";
                    for (int k = 0; k <= ds.Tables[0].Rows.Count; k++)
                    {
                        int col = 0;
                        for (int i = 0; spl_type.GetUpperBound(0) >= i; i++)
                        {
                            if (spl_type[i].Trim() != "")
                            {
                                col++;
                                tot_leave = 0;
                                string[] split_leave = spl_type[i].Split(';');
                                string leave = split_leave[0];
                                if (split_leave.Length >= 2)
                                {
                                    string s = Convert.ToString(split_leave[1]);
                                    if (s == "")
                                    {
                                        addtot = 0;
                                    }
                                    else
                                    {
                                        addtot = Convert.ToDouble(s);
                                    }
                                }
                                if (k != ds.Tables[0].Rows.Count)
                                {
                                    int dd = 0;
                                    string leavepk = d2.GetFunction("select LeaveMasterPK from leave_category where category='" + leave + "' and college_code='" + ddlcollege.SelectedItem.Value + "'");
                                    string dt_get_leave = "select * from RQ_Requisition r,leave_category l where RequestType=5 and LeaveFrom>='" + ds.Tables[0].Rows[k]["From_Date"].ToString() + "' and LeaveTo<='" + ds.Tables[0].Rows[k]["To_Date"].ToString() + "' and ReqAppNo='" + applid + "' and ReqAppStatus='1' or ReqAppStatus='0' and l.LeaveMasterPK=r.LeaveMasterFK and r.LeaveMasterFK='" + leavepk + "' ";
                                    ds1 = d2.select_method_wo_parameter(dt_get_leave, "Text");
                                    //string q = "select * from RQ_Requisition where RequestType=5 and ReqAppNo='" + ReqStaffAppNo + "' and LeaveTo>='" + RequestfromDate + "' and MONTH(RequestDate)=MONTH(GETDATE())  and ReqAppStatus not in('2')";
                                    //ds1 = d2.select_method_wo_parameter(q, "Text");
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        DateTime hhalf = new DateTime();
                                        for (int u = 0; u < ds1.Tables[0].Rows.Count; u++)
                                        {
                                            DateTime ll = Convert.ToDateTime(ds1.Tables[0].Rows[u]["LeaveTo"]);
                                            DateTime l = Convert.ToDateTime(ds1.Tables[0].Rows[u]["LeaveFrom"]);
                                            string hhalfdate = Convert.ToString(ds1.Tables[0].Rows[u]["HalfDate"]);
                                            if (hhalfdate != "")
                                            {
                                                hhalf = Convert.ToDateTime(ds1.Tables[0].Rows[u]["HalfDate"]);
                                            }
                                            for (; l <= ll; )
                                            {
                                                DateTime fromdateee = new DateTime();
                                                fromdateee = TextToDate(txt_frm);
                                                DateTime todateeee = new DateTime();
                                                todateeee = TextToDate(txt_to);
                                                for (; fromdateee <= todateeee; )
                                                {
                                                    if (hhalfdate != "")
                                                    {
                                                        if (hhalf == fromdateee)
                                                        {
                                                            string checksession = Convert.ToString(ds1.Tables[0].Rows[u]["LeaveSession"]);
                                                            dd++;
                                                        }
                                                        else
                                                        {
                                                            string startdate = fromdateee.ToString("dd/MM/yyyy");
                                                            string sdate = l.ToString("dd/MM/yyyy");
                                                            if (startdate == sdate)
                                                            {
                                                                dd++;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string startdate = fromdateee.ToString("dd/MM/yyyy");
                                                        string sdate = l.ToString("dd/MM/yyyy");
                                                        if (startdate == sdate)
                                                        {
                                                            dd++;
                                                        }
                                                    }
                                                    fromdateee = fromdateee.AddDays(1);
                                                }
                                                l = l.AddDays(1);
                                            }
                                        }
                                    }
                                    //
                                }
                            }
                        }
                    }
                }
            }
            //BindLeave();
        }
        catch
        {
        }
    }

    protected void bb_close_Clik(object sender, EventArgs e)
    {
        bb.Visible = false;
    }

    //alter subject staff added by sudhagar 06.02.2017

    protected void gdalterStaff_databound(object sender, EventArgs e)
    {
    }

    protected void gdalterStaff_rowdatabound(object sender, EventArgs e)
    {
    }

    protected bool loadGridAlterStaff(ref string alterSubject, string staffcode, string fdate, string tdate, bool saveOrAlter)
    {
        bool boolcheck = false;
        try
        {
            if (!string.IsNullOrEmpty(alterSubject))
            {
                bool datacheck = false;
                string stafname = Convert.ToString(txt_staff_name.Text);
                string selq = " select * from RQ_AlterStaffLeaveRequest where ActStaffID='" + staffcode + "' and AlterDate between '" + fdate + "' and '" + tdate + "'";
                selq += " select d.Degree_Code,c.Course_Name,dt.dept_acronym , dt.Dept_Name,(c.Course_Name +'-'+dt.dept_acronym)as dept_acronym from Degree d,Department dt,Course c where d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and d.college_code ='" + collegecode1 + "'";
                selq += " select distinct (sm.staff_code+'-'+sm.staff_name) as staff,sm.staff_code from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' order by sm.staff_code asc";
                DataSet dsv = d2.select_method_wo_parameter(selq, "Text");
                if (dsv.Tables.Count > 0 && dsv.Tables[0].Rows.Count > 0)
                    datacheck = true;
                DataTable dtalter = new DataTable();
                dtalter.Columns.Add("Sno");
                //select
                dtalter.Columns.Add("sfyear");
                dtalter.Columns.Add("sfdepts");
                dtalter.Columns.Add("sfsemester");
                dtalter.Columns.Add("sfsection");
                dtalter.Columns.Add("sfdate");
                dtalter.Columns.Add("sfhour");
                dtalter.Columns.Add("sfcode");
                // dtalter.Columns.Add("sfname");
                dtalter.Columns.Add("sfsubject");
                dtalter.Columns.Add("sfsubjectcode");
                dtalter.Columns.Add("altsfcode");
                // dtalter.Columns.Add("altsfname");
                DataRow dralter;
                string[] splsub = alterSubject.Split('@');
                if (splsub.Length > 0)
                {
                    int sno = 0;
                    for (int i = 0; i < splsub.Length; i++)
                    {
                        string[] spldetail = splsub[i].Split('$');
                        if (spldetail.Length > 0)
                        {
                            for (int j = 6; j < spldetail.Length; j++) // poo 01.12.17
                            {
                                //subject multiple subject
                                if (spldetail[j].Length > 1)
                                {
                                    string[] splmulsub = spldetail[j].Split('*');
                                    if (splmulsub.Length > 0)
                                    {
                                        for (int sp = 0; sp < splmulsub.Length; sp++)
                                        {
                                            sno++;
                                            dralter = dtalter.NewRow();
                                            dralter["Sno"] = Convert.ToString(sno);
                                            dralter["sfyear"] = Convert.ToString(spldetail[0]);
                                            dralter["sfdepts"] = Convert.ToString(spldetail[1] + "-" + spldetail[2]);
                                            dralter["sfsemester"] = Convert.ToString(spldetail[3].Split(' ')[1]);
                                            dralter["sfsection"] = Convert.ToString(spldetail[4].Split(' ')[1]);
                                            dralter["sfdate"] = Convert.ToString(spldetail[5].Split(':')[1]);
                                            //dralter["sfdate"] = Convert.ToString(spldetail[j].Split(':')[1]);
                                            //if (j - 1 == 5) // poo 01.12.17
                                            //dralter["sfdate"] = Convert.ToString(spldetail[j].Split(':')[1]);
                                            // else if (j < spldetail.Length - 1) // poo 01.12.17
                                            //{
                                            //    string[] spl = spldetail[j].Split(':'); int length = spl.Length;
                                            //    dralter["sfdate"] = Convert.ToString(spldetail[j].Split(':')[length - 1]);//poo
                                            //}
                                            dralter["sfhour"] = Convert.ToString(splmulsub[sp].Split('#')[0].Split(' ')[2]);
                                            dralter["sfcode"] = Convert.ToString(staffcode + "-" + stafname);
                                            // dralter["sfname"] = Convert.ToString(stafname);
                                            dralter["sfsubject"] = Convert.ToString(splmulsub[sp].Split('#')[1].Split(':')[1]);
                                            dralter["sfsubjectcode"] = Convert.ToString(splmulsub[sp].Split('#')[1].Split(':')[1].Split('+')[1]);
                                            string alterstaff = string.Empty;
                                            if (datacheck)
                                            {
                                                string degreecode = string.Empty;
                                                if (dsv.Tables[1].Rows.Count > 0)
                                                {
                                                    dsv.Tables[1].DefaultView.RowFilter = "Course_Name='" + spldetail[1].TrimStart(' ') + "' and dept_acronym='" + spldetail[2] + "'";
                                                    DataView dv = dsv.Tables[1].DefaultView;
                                                    if (dv.Count > 0)
                                                        degreecode = Convert.ToString(dv[0]["Degree_Code"]);
                                                }
                                                string date = Convert.ToString(spldetail[5].Split(':')[1]);
                                                DateTime dt = Convert.ToDateTime(date.Split('/')[1] + "/" + date.Split('/')[0] + "/" + date.Split('/')[2]);
                                                dsv.Tables[0].DefaultView.RowFilter = "ActStaffID='" + staffcode + "' and AlterDate='" + dt + "' and BatchYear='" + spldetail[0] + "' and DegreeFK='" + degreecode + "' and Sem='" + spldetail[3].Split(' ')[1] + "'and Sec='" + spldetail[4].Split(' ')[1] + "'and ActSubjectNo='" + splmulsub[sp].Split('#')[1].Split(':')[1].Split('+')[1] + "' and Hour='" + splmulsub[sp].Split('#')[0].Split(' ')[2] + "'";
                                                DataView dvs = dsv.Tables[0].DefaultView;
                                                if (dvs.Count > 0)
                                                    alterstaff = Convert.ToString(dvs[0]["AlterStaffID"]);
                                                if (!string.IsNullOrEmpty(alterstaff) && dsv.Tables[2].Rows.Count > 0)
                                                {
                                                    dsv.Tables[2].DefaultView.RowFilter = "staff_code='" + alterstaff + "'";
                                                    DataView dvsfname = dsv.Tables[2].DefaultView;
                                                    if (dvs.Count > 0)
                                                        alterstaff = Convert.ToString(dvsfname[0]["staff"]);
                                                }
                                            }
                                            dralter["altsfcode"] = alterstaff;
                                            //  dralter["altsfname"] = Convert.ToString("");
                                            dtalter.Rows.Add(dralter);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (dtalter.Rows.Count > 0)
                {
                    gdalterStaff.DataSource = dtalter;
                    gdalterStaff.DataBind();
                    gdalterStaff.Visible = true;
                    lblaltmsg.Text = string.Empty;
                    lblaltmsg.Visible = false;
                    divalterstaff.Visible = true;
                    divdept.Visible = true;
                    boolcheck = true;
                }
            }
            else if (saveOrAlter)
            {
                gdalterStaff.Visible = false;
                // lblaltmsg.Text = "There Is No Alter Schedule!";
                //lblaltmsg.Visible = true;
                divaltalert.Visible = true;
                lblaltalertmsg.Text = "There Is No Alter Schedule!";
                divalterstaff.Visible = false;
                divdept.Visible = false;
            }
        }
        catch { }
        return boolcheck;
    }

    //ok button

    protected void btnstaffok_Click(object sender, EventArgs e)
    {
        string staffid = string.Empty;
        if (ddlstaffid.Items.Count > 0 && ddlstaffid.SelectedItem.Text != "Select")
            staffid = Convert.ToString(ddlstaffid.SelectedItem.Text);
        else
            staffid = Convert.ToString(txt_staffid.Text);
        if (!string.IsNullOrEmpty(staffid) && validateGrid())
        {
            foreach (GridViewRow gdrow in gdalterStaff.Rows)
            {
                CheckBox cb = (CheckBox)gdrow.Cells[1].FindControl("cbselect");
                if (cb.Checked)
                {
                    Label lblaltname = (Label)gdrow.FindControl("lblaltstf");
                    lblaltname.Text = Convert.ToString(staffid);
                }
            }
        }
        else
        {
            divaltalert.Visible = true;
            lblaltalertmsg.Text = "Please Provide Values";
        }
    }

    //save button

    protected void btnaltstfSave_Click(object sender, EventArgs e)
    {
        bool check = false;
        bool save = false;
        string selq = " select d.Degree_Code,c.Course_Name,dt.dept_acronym , dt.Dept_Name,(c.Course_Name +'-'+dt.dept_acronym)as dept_acronym from Degree d,Department dt,Course c where d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and d.college_code ='" + collegecode1 + "'";
        DataSet dsd = d2.select_method_wo_parameter(selq, "Text");
        string actstaffid = string.Empty;
        string alterstaffid = string.Empty;
        DateTime dt = new DateTime();
        ArrayList ardate = new ArrayList();
        foreach (GridViewRow gdrow in gdalterStaff.Rows)
        {
            CheckBox cb = (CheckBox)gdrow.Cells[1].FindControl("cbselect");
            if (cb.Checked)
            {
                Label lblyear = (Label)gdrow.FindControl("lblyear");
                Label lbldept = (Label)gdrow.FindControl("lbldepts");
                Label lblsem = (Label)gdrow.FindControl("lblsemester");
                Label lblsec = (Label)gdrow.FindControl("lblsection");
                Label lbldate = (Label)gdrow.FindControl("lbldate");
                Label lblhour = (Label)gdrow.FindControl("lblhr");
                Label lblaltstfname = (Label)gdrow.FindControl("lblstfcode");
                Label subject = (Label)gdrow.FindControl("lblsub");
                Label alterstaff = (Label)gdrow.FindControl("lblaltstf");
                actstaffid = Convert.ToString(lblaltstfname.Text).Split('-')[0];
                alterstaffid = Convert.ToString(alterstaff.Text).Split('-')[0];
                string subjcode = Convert.ToString(subject.Text).Split('+')[1];
                dt = Convert.ToDateTime(lbldate.Text.Split('/')[1] + "/" + lbldate.Text.Split('/')[0] + "/" + lbldate.Text.Split('/')[2]);
                if (!ardate.Contains(dt))
                {
                    ardate.Add(dt);
                }
                string degreecode = string.Empty;
                if (dsd.Tables.Count > 0 && dsd.Tables[0].Rows.Count > 0)
                {
                    dsd.Tables[0].DefaultView.RowFilter = "Course_Name='" + lbldept.Text.Split('-')[0].TrimStart(' ') + "' and dept_acronym='" + lbldept.Text.Split('-')[1] + "'";
                    DataView dv = dsd.Tables[0].DefaultView;
                    if (dv.Count > 0)
                        degreecode = Convert.ToString(dv[0]["Degree_Code"]);
                }
                if (!string.IsNullOrEmpty(alterstaff.Text) && !string.IsNullOrEmpty(degreecode))
                {
                    string InsQ = " if exists(select * from RQ_AlterStaffLeaveRequest where ActStaffID='" + actstaffid + "' and AlterDate='" + dt + "' and BatchYear='" + lblyear.Text + "' and DegreeFK='" + degreecode + "' and Sem='" + lblsem.Text + "'and Sec='" + lblsec.Text + "'and ActSubjectNo='" + subjcode + "' and Hour='" + lblhour.Text + "')update RQ_AlterStaffLeaveRequest set AlterStaffID='" + alterstaffid + "' where ActStaffID='" + actstaffid + "' and AlterDate='" + dt + "' and BatchYear='" + lblyear.Text + "' and DegreeFK='" + degreecode + "' and Sem='" + lblsem.Text + "'and Sec='" + lblsec.Text + "'and ActSubjectNo='" + subjcode + "' and Hour='" + lblhour.Text + "' else insert into RQ_AlterStaffLeaveRequest(BatchYear,DegreeFK,Sem,Sec,AlterDate,Hour,ActStaffID,ActSubjectNo,AlterStaffID) values('" + lblyear.Text + "','" + degreecode + "','" + lblsem.Text + "','" + lblsec.Text + "','" + dt + "','" + lblhour.Text + "','" + actstaffid + "','" + subjcode + "','" + alterstaffid + "')";
                    int upd = d2.update_method_wo_parameter(InsQ, "Text");
                    save = true;
                }
                else
                    check = true;
            }
            else
                check = true;
        }
        if (check)
        {
            divaltalert.Visible = true;
            lblaltalertmsg.Text = "Please Provide Details!";
        }
        if (save && !check)
        {
            divaltalert.Visible = true;
            lblaltalertmsg.Text = "Staff Alter Successfully!";
            divalterstaff.Visible = false;
            afteralterSchedule();
            //alterstaffid
        }
    }

    protected bool validateGrid()
    {
        bool boolgrid = false;
        foreach (GridViewRow gdrow in gdalterStaff.Rows)
        {
            CheckBox cb = (CheckBox)gdrow.FindControl("cbselect");
            if (cb.Checked)
                boolgrid = true;
        }
        return boolgrid;
    }

    protected bool afteralterSchedule()
    {
        bool check = false;
        try
        {
            collegecode1 = Convert.ToString(Session["collegecode"]);
            string actstaffid = Convert.ToString(txt_staff_code.Text);
            string[] spl_frm_date = txt_frm.Text.Split('/');
            string from = spl_frm_date[1] + "/" + spl_frm_date[0] + "/" + spl_frm_date[2];
            string[] spl_to_date = txt_to.Text.Split('/');
            string to = spl_to_date[1] + "/" + spl_to_date[0] + "/" + spl_to_date[2];
            string selq = " select BatchYear,DegreeFK,Sem,Sec,convert(varchar(10),AlterDate,103) as AlterDate,Hour,ActStaffID,ActSubjectNo,AlterStaffID from RQ_AlterStaffLeaveRequest where ActStaffID='" + actstaffid + "' and AlterDate between '" + from + "' and '" + to + "' ";
            //and AlterStaffID='" + alterstaffid + "'
            selq += " select d.Degree_Code,c.Course_Name,dt.dept_acronym , dt.Dept_Name,(c.Course_Name +'-'+dt.dept_acronym)as dept_acronym from Degree d,Department dt,Course c where d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and d.college_code ='" + collegecode1 + "'";
            selq += " select distinct (sm.staff_code+'-'+sm.staff_name) as staff,sm.staff_code,sm.staff_name from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' order by sm.staff_code asc";
            selq += " select distinct s.subject_no,s.subject_name,s.subject_code,sy.Batch_Year,sy.degree_code,sy.semester,ss.Lab,ss.subject_type,current_semester from Registration r,syllabus_master sy,sub_sem ss,subject s where r.Batch_Year=sy.Batch_Year and r.degree_code=sy.degree_code and r.Current_Semester=sy.semester and sy.syll_code=ss.syll_code and sy.syll_code=s.syll_code and ss.subType_no=s.subType_no and r.cc=0 and r.delflag=0 and r.exam_flag<>'debar'";
            DataSet dsdet = d2.select_method_wo_parameter(selq, "Text");
            if (dsdet.Tables.Count > 0 && dsdet.Tables[0].Rows.Count > 0)
            {
                DataTable dts = new DataTable();
                dts.Columns.Add("Dummy0");
                dts.Columns.Add("Dummy");
                dts.Columns.Add("Dummy5");
                dts.Columns.Add("Dummy3");
                dts.Columns.Add("Dummy4");
                dts.Columns.Add("Dummy6");
                dts.Columns.Add("Dummy2");
                dts.Columns.Add("Dummy1");
                DataRow drs;
                for (int i = 0; i < dsdet.Tables[0].Rows.Count; i++)
                {
                    drs = dts.NewRow();
                    drs[0] = Convert.ToString(dsdet.Tables[0].Rows[i]["AlterDate"]);
                    drs[1] = Convert.ToString(dsdet.Tables[0].Rows[i]["Hour"]);
                    drs[2] = Convert.ToString(dsdet.Tables[0].Rows[i]["ActStaffID"]);
                    string stafname = string.Empty;
                    if (dsdet.Tables[2].Rows.Count > 0)
                    {
                        dsdet.Tables[2].DefaultView.RowFilter = "staff_code='" + dsdet.Tables[0].Rows[i]["ActStaffID"] + "'";
                        DataView dvsfname = dsdet.Tables[2].DefaultView;
                        if (dvsfname.Count > 0)
                            stafname = Convert.ToString(dvsfname[0]["staff_name"]);
                    }
                    drs[3] = stafname;
                    string subname = string.Empty;
                    if (dsdet.Tables[3].Rows.Count > 0)
                    {
                        dsdet.Tables[3].DefaultView.RowFilter = "batch_year='" + dsdet.Tables[0].Rows[i]["BatchYear"] + "' and  degree_code='" + dsdet.Tables[0].Rows[i]["DegreeFK"] + "'  and current_semester='" + dsdet.Tables[0].Rows[i]["Sem"] + "' and subject_no='" + dsdet.Tables[0].Rows[i]["ActSubjectNo"] + " '";
                        DataView dvsub = dsdet.Tables[3].DefaultView;
                        if (dvsub.Count > 0)
                            subname = Convert.ToString(dvsub[0]["subject_name"]);
                    }
                    drs[4] = subname;
                    drs[5] = Convert.ToString(dsdet.Tables[0].Rows[i]["AlterStaffID"]);
                    stafname = string.Empty;
                    if (dsdet.Tables[2].Rows.Count > 0)
                    {
                        dsdet.Tables[2].DefaultView.RowFilter = "staff_code='" + dsdet.Tables[0].Rows[i]["AlterStaffID"] + "'";
                        DataView dvsfname = dsdet.Tables[2].DefaultView;
                        if (dvsfname.Count > 0)
                            stafname = Convert.ToString(dvsfname[0]["staff_name"]);
                    }
                    drs[6] = Convert.ToString(stafname);
                    drs[7] = Convert.ToString("");
                    dts.Rows.Add(drs);
                }
                if (dts.Rows.Count > 0)
                {
                    grid_altersub.DataSource = dts;
                    grid_altersub.DataBind();
                    grid_altersub.Visible = true;
                    sp_appsub.Visible = true;
                    check = true;
                }
                else
                {
                    sp_appsub.Visible = false;
                }
            }
        }
        catch { }
        return check;
    }

    protected void ddlstaffid_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    //department 

    #region
    protected void cbl_depts_CheckedChanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxChangedEvent(cbl_depts, cb_depts, txt_depts, "Department");
        loadStaffid();
    }
    protected void cbl_depts_selectedchanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxListChangedEvent(cbl_depts, cb_depts, txt_depts, "Department");
        loadStaffid();
    }
    protected void cb_desig_CheckedChanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxChangedEvent(cbl_desig, cb_desig, txt_desig, "Designation");
        loadStaffid();
    }
    protected void cbl_desig_selectedchanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxListChangedEvent(cbl_desig, cb_desig, txt_desig, "Designation");
        loadStaffid();
    }
    protected void cb_staffcat_CheckedChanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxChangedEvent(cbl_staffcat, cb_staffcat, txt_staffcat, "Category");
        loadStaffid();
    }
    protected void cbl_staffcat_selectedchanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxListChangedEvent(cbl_staffcat, cb_staffcat, txt_staffcat, "Category");
        loadStaffid();
    }
    protected void cb_stafftyp_CheckedChanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxChangedEvent(cbl_stafftyp, cb_stafftyp, txt_stafftyp, "StaffType");
        loadStaffid();
    }
    protected void cbl_stafftyp_selectedchanged(object sender, EventArgs e)
    {
        reuse.CallCheckBoxListChangedEvent(cbl_stafftyp, cb_stafftyp, txt_stafftyp, "StaffType");
        loadStaffid();
    }
    protected void binddept()
    {
        try
        {
            //if (ddl_rpt_collge.Items.Count > 0)
            //   collegecode1 = Convert.ToString(ddl_rpt_collge.SelectedItem.Value);
            cbl_depts.Items.Clear();
            ds.Clear();
            string group_user = "";
            string cmd = "";
            string singleuser = Session["single_user"].ToString();
            if (singleuser == "True")
            {
                cmd = "SELECT DISTINCT hp.dept_code,dept_name from hr_privilege hp,hrdept_master hr  where  hr.dept_code=hp.dept_code  and hp.dept_code in (select dept_code from hrdept_master where college_code='" + collegecode1 + "') order by dept_name";
            }
            else
            {
                group_user = Session["group_code"].ToString();
                if (group_user.Contains(';'))
                {
                    string[] group_semi = group_user.Split(';');
                    group_user = group_semi[0].ToString();
                }
                cmd = "SELECT DISTINCT hp.dept_code,dept_name from hr_privilege hp,hrdept_master hr  where  hr.dept_code=hp.dept_code  and hp.dept_code in (select dept_code from hrdept_master where college_code='" + collegecode1 + "') order by dept_name";
            }
            ds = d2.select_method_wo_parameter(cmd, "Text");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbl_depts.DataSource = ds.Tables[0];
                    cbl_depts.DataTextField = "dept_name";
                    cbl_depts.DataValueField = "dept_code";
                    cbl_depts.DataBind();
                    if (cbl_depts.Items.Count > 0)
                    {
                        for (int i = 0; i < cbl_depts.Items.Count; i++)
                        {
                            cbl_depts.Items[i].Selected = true;
                        }
                        txt_depts.Text = "Department(" + cbl_depts.Items.Count + ")";
                        cb_depts.Checked = true;
                    }
                }
                else
                {
                    txt_depts.Text = "--Select--";
                    cb_depts.Checked = false;
                }
            }
        }
        catch { }
    }
    protected void binddesignation()
    {
        //if (ddl_rpt_collge.Items.Count > 0)
        //  collegecode1 = Convert.ToString(ddl_rpt_collge.SelectedItem.Value);
        ds.Clear();
        cbl_desig.Items.Clear();
        string statequery = "select desig_code,desig_name from desig_master where collegeCode='" + collegecode1 + "' order by desig_name";
        ds = d2.select_method_wo_parameter(statequery, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            cbl_desig.DataSource = ds;
            cbl_desig.DataTextField = "desig_name";
            cbl_desig.DataValueField = "desig_code";
            cbl_desig.DataBind();
            cbl_desig.Visible = true;
            if (cbl_desig.Items.Count > 0)
            {
                for (int i = 0; i < cbl_desig.Items.Count; i++)
                {
                    cbl_desig.Items[i].Selected = true;
                }
                txt_desig.Text = "Designation(" + cbl_desig.Items.Count + ")";
                cb_desig.Checked = true;
            }
        }
        else
        {
            txt_desig.Text = "--Select--";
            cb_desig.Checked = false;
        }
    }
    protected void loadstafftype()
    {
        try
        {
            //if (ddl_rpt_collge.Items.Count > 0)
            //   collegecode1 = Convert.ToString(ddl_rpt_collge.SelectedItem.Value);
            ds.Clear();
            cbl_stafftyp.Items.Clear();
            string item = "select distinct stftype from stafftrans t ,staffmaster m where m.staff_code = t.staff_code and college_code = '" + collegecode1 + "'";
            ds = d2.select_method_wo_parameter(item, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_stafftyp.DataSource = ds;
                cbl_stafftyp.DataTextField = "stftype";
                cbl_stafftyp.DataBind();
                if (cbl_stafftyp.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_stafftyp.Items.Count; i++)
                    {
                        cbl_stafftyp.Items[i].Selected = true;
                    }
                    txt_stafftyp.Text = "StaffType (" + cbl_stafftyp.Items.Count + ")";
                    cb_stafftyp.Checked = true;
                }
            }
            else
            {
                txt_stafftyp.Text = "--Select--";
                cb_stafftyp.Checked = false;
            }
        }
        catch { }
    }
    protected void loadcategory()
    {
        // if (ddl_rpt_collge.Items.Count > 0)
        // collegecode1 = Convert.ToString(ddl_rpt_collge.SelectedItem.Value);
        ds.Clear();
        cbl_staffcat.Items.Clear();
        string statequery = "select category_code,category_Name from staffcategorizer where college_code = '" + collegecode1 + "' ";
        ds = d2.select_method_wo_parameter(statequery, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            cbl_staffcat.DataSource = ds;
            cbl_staffcat.DataTextField = "category_Name";
            cbl_staffcat.DataValueField = "category_code";
            cbl_staffcat.DataBind();
            cbl_staffcat.Visible = true;
            if (cbl_staffcat.Items.Count > 0)
            {
                for (int i = 0; i < cbl_staffcat.Items.Count; i++)
                {
                    cbl_staffcat.Items[i].Selected = true;
                }
                txt_staffcat.Text = "Category(" + cbl_staffcat.Items.Count + ")";
                cb_staffcat.Checked = true;
            }
        }
        else
        {
            txt_staffcat.Text = "--Select--";
            cb_staffcat.Checked = false;
        }
    }
    protected void loadStaffid()
    {
        string deptcode = Convert.ToString(getCblSelectedValue(cbl_depts));
        string desicode = Convert.ToString(getCblSelectedValue(cbl_desig));
        string stafcatg = Convert.ToString(getCblSelectedValue(cbl_staffcat));
        string stftype = Convert.ToString(getCblSelectedValue(cbl_stafftyp));
        //strtblname = " ,stafftrans st";
        //strvalue = " and st.latestrec='1'";
        ddlstaffid.Items.Clear();
        if (!string.IsNullOrEmpty(deptcode) && !string.IsNullOrEmpty(desicode) && !string.IsNullOrEmpty(stafcatg) && !string.IsNullOrEmpty(stftype))
        {
            string strfilterVal = " and st.dept_code in('" + deptcode + "') and st.desig_code in('" + desicode + "') and st.category_code in('" + stafcatg + "') and st.stftype in('" + stftype + "')";
            string selQ = " select distinct (sm.staff_code+'-'+sm.staff_name) as staff,sm.staff_code from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' and st.staff_code=sm.staff_code and sm.resign='0' and sm.settled='0' " + strfilterVal + " order by sm.staff_code asc";
            DataSet dsval = d2.select_method_wo_parameter(selQ, "Text");
            if (dsval.Tables.Count > 0 && dsval.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsval.Tables[0].Rows.Count; i++)
                {
                    ddlstaffid.Items.Add(new ListItem(Convert.ToString(dsval.Tables[0].Rows[i]["staff"]), Convert.ToString(dsval.Tables[0].Rows[i]["staff_code"])));
                }
                if (ddlstaffid.Items.Count > 0)
                    ddlstaffid.Items.Insert(0, "Select");
            }
        }
    }
    private string getCblSelectedValue(CheckBoxList cblSelected)
    {
        System.Text.StringBuilder selectedvalue = new System.Text.StringBuilder();
        try
        {
            for (int sel = 0; sel < cblSelected.Items.Count; sel++)
            {
                if (cblSelected.Items[sel].Selected == true)
                {
                    if (selectedvalue.Length == 0)
                    {
                        selectedvalue.Append(Convert.ToString(cblSelected.Items[sel].Value));
                    }
                    else
                    {
                        selectedvalue.Append("','" + Convert.ToString(cblSelected.Items[sel].Value));
                    }
                }
            }
        }
        catch { cblSelected.Items.Clear(); }
        return selectedvalue.ToString();
    }
    #endregion

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getrno(string prefixText)
    {
        List<string> name = new List<string>();
        try
        {
            string query = "";
            WebService ws = new WebService();

            //   query = "select top 100 Roll_No from Registration where CC=0 and DelFlag =0 and Exam_Flag <>'DEBAR' and Roll_No like '" + prefixText + "%' and college_code='" + collegecode1 + "' order by Roll_No asc";
            query = " select (sm.staff_code+'-'+sm.staff_name) as staff,sm.staff_code from staffmaster sm,stafftrans st where sm.staff_code=st.staff_code and st.latestrec='1' and college_code='" + clgcode + "' and sm.staff_code like '" + prefixText + "%'  order by sm.staff_code asc ";
            name = ws.Getname(query);
            return name;
        }
        catch { return name; }
    }

    protected void btnaltalert_Clik(object sender, EventArgs e)
    {
        divaltalert.Visible = false;
    }

    protected void getInchargeStaff(string staffcode)
    {
        try
        {
            //bool check = false;
            ddlstfincharge.Items.Clear();
            string getstaff = "select head_of_dept,dept_code from department where head_of_dept='" + staffcode + "'";
            DataSet dsDet = d2.select_method_wo_parameter(getstaff, "Text");
            if (dsDet.Tables.Count > 0 && dsDet.Tables[0].Rows.Count > 0)
            {
                string selQ = " select distinct (sm.staff_code+'-'+sm.staff_name) as staff,sm.staff_code,st.stftype from staffmaster sm,staff_appl_master sa,stafftrans st where sm.appl_no=sa.appl_no and st.latestrec='1' and st.staff_code=sm.staff_code and sm.resign='0' and sm.settled='0'  and st.dept_code in('" + dsDet.Tables[0].Rows[0]["dept_code"] + "')  and st.stftype in('TEACHING') and sm.staff_code<>'" + staffcode + "' order by sm.staff_code asc";
                DataSet dsval = d2.select_method_wo_parameter(selQ, "Text");
                if (dsval.Tables.Count > 0 && dsval.Tables[0].Rows.Count > 0)
                {
                    ddlstfincharge.DataSource = dsval;
                    ddlstfincharge.DataTextField = "staff";
                    ddlstfincharge.DataValueField = "staff_code";
                    ddlstfincharge.DataBind();
                    trincharge.Visible = true;
                }
                else
                    trincharge.Visible = false;
            }
            else
                trincharge.Visible = false;
        }
        catch { }
    }

    public void visitorsms(string RequisitionFK, string companyandperson, string date, string time, string reason)
    {
        user_id = d2.GetFunction("select SMS_User_ID from Track_Value where college_code='" + collegecode1 + "'");
        string getval = d2.GetUserapi(user_id);
        string[] spret = getval.Split('-');
        if (spret.GetUpperBound(0) == 1)
        {
            SenderID = spret[0].ToString();
            Password = spret[1].ToString();
            Session["api"] = user_id;
            Session["senderid"] = SenderID;
        }
        string appno = d2.GetFunction("select MeetStaffAppNo from RQ_Requisition where RequestType=3 and RequisitionPK='" + RequisitionFK + "'");
        strmsg = "Your ward Ms." + companyandperson + " request to Visit for " + reason + " on " + date + " at " + time;
        string staffnum = d2.GetFunction("select per_mobileno from staff_appl_master where appl_no in('" + appno.Replace(",", "','") + "')");
        mobilenos = staffnum;
        if (mobilenos != "")
        {
            isst = "1";
            int smsdet = d2.send_sms(user_id, collegecode1, usercode, mobilenos, strmsg, isst);
        }
    }

    public void Btn_Cancel_Click(object sender, EventArgs e)
    {
        string alter_status = Convert.ToString(Session["alter_done"]);
        // if (alter_status == "1")
        // {
        divPopAlertNEW.Visible = true;
        divPopAlertContent.Visible = true;
        lblAlertMsgNEW.Text = "Alteration of hours has been Saved.Do You Want to delete the Altered hour(s)?";
        // }

    }

    protected void btn_delete_Alter_Hour_Click(object sender, EventArgs e)
    {
        try
        {
            string altertableqry = string.Empty;
            string altertablesched = string.Empty;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["tbl_alter_qry"])))
            {
                //altertableqry = Convert.ToString(Session["tbl_alter_qry"]);
                // string degree = Convert.ToString(Session["deg"]);
                // string seme=Convert.ToString(Session["sem"] );
                // string batchyear=Convert.ToString(Session["batch_year"]);
                // string date=Convert.ToString(Session["fromdates"]);
                // string sec=Convert.ToString(Session["sections"]);
                // string alt=Convert.ToString(Session["No_of_Alter"]);
                //string day=Convert.ToString(Session["getday"]);
                //string dys = Convert.ToString(Session["getdays"]);
                //string arra =Convert.ToString(Session["has"]);
                Hashtable ht = Session["has"] as Hashtable;
                Hashtable htvval = Session["hasvalues"] as Hashtable;
                Hashtable htvval1 = Session["hasvalues1"] as Hashtable;
                Hashtable htvval2 = Session["hasvalues2"] as Hashtable;
                Hashtable htvval3 = Session["hasvalues3"] as Hashtable;
                Hashtable htvval4 = Session["hasvalues4"] as Hashtable;
                Hashtable htvval5 = Session["hasvalues5"] as Hashtable;
                Hashtable htvval6 = Session["hasvalues6"] as Hashtable;
                int m = 0;
                for (int j = 0; j < ht.Count; j++)
                {
                    m = j + 1;
                    string sm = ht[m].ToString();
                    string smt = htvval[m].ToString();
                    string degree = htvval1[m].ToString();
                    string seme = htvval2[m].ToString();
                    string batchyear = htvval3[m].ToString();
                    string date = htvval4[m].ToString();
                    string sec = htvval6[m].ToString();
                    string alt = htvval5[m].ToString();
                    string[] spl1 = sm.Split(',');

                    string[] spl2 = smt.Split(',');
                    for (int mon = 0; mon < spl2.Count(); mon++)
                    {
                        if (spl2[mon] != "''")
                        {
                            string mday = spl1[mon].ToString();
                            string dayno = spl2[mon].ToString();

                            altertableqry = " update tbl_alter_schedule_Details set " + mday + "=''  where batch_year=" + batchyear + " and degree_code = " + degree + " and semester = " + seme + " and FromDate ='" + date + "'and sections='" + sec + "' and lastrec='0' and No_of_Alter='" + alt + "' and " + mday + "=" + dayno + "";
                            int i = 0;
                            if (!string.IsNullOrEmpty(altertableqry))// !string.IsNullOrEmpty(altertablesched))
                                i = d2.update_method_wo_parameter(altertableqry, "text");
                            if (i == 0)
                            {
                                lblpopupAlertMsg.Text = "Error deleting the Altered Hour(s)";
                                divPopupAlert.Visible = true;
                            }
                            else
                            {
                                divok.Visible = true;
                                Lblok.Text = "Deleted Succesfully";
                                txt_frm_TextChanged(sender, e);
                                txt_to_TextChanged(sender, e);

                                chkrelived = 0;
                                leaverequestsetting();
                                if (requestpermissioncheck != "1")
                                {
                                    txt_staff_code.Text = "";
                                    txt_staff_name.Text = "";
                                    txt_dep.Text = "";
                                    txt_des.Text = "";
                                    lbl_holidayalert.Text = "";
                                    gridView2.Visible = false;
                                    grid_altersub.Visible = false;
                                    sp_appsub.Visible = false;
                                    sp_appstaf.Visible = false;
                                    grid_approvalstaff.Visible = false;
                                    imagestaff.ImageUrl = "~/image/Gender Neutral User Filled-100(1).png";
                                }
                                rdlist.Checked = true;
                                txt_applydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                txt_frm.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                txt_to.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                //txt_reason.Text = "";
                                txtleavereason.Text = "";
                                loadleavereason();
                                leav_res();
                                BindGridview();
                            }
                        }
                    }

                }
            }
            //if (!string.IsNullOrEmpty(Convert.ToString(Session["tbl_alter_qry"])))
            //{
            //    altertablesched = Convert.ToString(Session["tbl_alter_qry"]);
            //   // string codevai = Convert.ToString(Session["code_val"]);
            //}
            //int i = 0;
            //if (!string.IsNullOrEmpty(altertableqry) )// !string.IsNullOrEmpty(altertablesched))
            //    i = d2.update_method_wo_parameter(altertableqry, "text");
            //if (i == 0)
            //{
            //    lblpopupAlertMsg.Text = "Error deleting the Altered Hour(s)";
            //    divPopupAlert.Visible = true;
            //}
            //else
            //{
            //    divok.Visible = true;
            //     Lblok.Text = "Deleted Succesfully";
            //     txt_frm_TextChanged(sender, e);
            //     txt_to_TextChanged(sender,e);

            //    chkrelived = 0;
            //    leaverequestsetting();
            //    if (requestpermissioncheck != "1")
            //    {
            //        txt_staff_code.Text = "";
            //        txt_staff_name.Text = "";
            //        txt_dep.Text = "";
            //        txt_des.Text = "";
            //        lbl_holidayalert.Text = "";
            //        gridView2.Visible = false;
            //        grid_altersub.Visible = false;
            //        sp_appsub.Visible = false;
            //        sp_appstaf.Visible = false;
            //        grid_approvalstaff.Visible = false;
            //        imagestaff.ImageUrl = "~/image/Gender Neutral User Filled-100(1).png";
            //    }
            //    rdlist.Checked = true;
            //    txt_applydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    txt_frm.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    txt_to.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    //txt_reason.Text = "";
            //    txtleavereason.Text = "";
            //    loadleavereason();
            //    leav_res();
            //    BindGridview();
            //}
        }
        catch (Exception ex)
        {

        }
    }

    protected void btn_close_alter_alert_Click(object sender, EventArgs e)
    {
        divPopAlertContent.Visible = false;
        lblAlertMsgNEW.Text = string.Empty;
        divPopAlertNEW.Visible = false;
    }

    protected void btnpopupAlertMsgCloseNEW_Click(object sender, EventArgs e)
    {
        //    try
        //    {

        //         string altertableqry = string.Empty;
        //        string altertablesched = string.Empty;
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["tbl_alter_qry"])))
        //        {
        //            altertableqry = Convert.ToString(Session["tbl_alter_qry"]);
        //        }
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["tbl_alter_qry"])))
        //        {
        //            altertablesched = Convert.ToString(Session["tbl_alter_qry"]);
        //        }
        //        int i = 0;
        //        if (!string.IsNullOrEmpty(altertableqry) && !string.IsNullOrEmpty(altertablesched))
        //            i = d2.update_method_wo_parameter(altertableqry + ";" + "" + altertablesched + ";", "text");
        //        if (i == 0)
        //        {
        //            lblpopupAlertMsg.Text = "Error deleting the Altered Hour(s)";
        //            divPopupAlert.Visible = true;
        //        }
        //        else
        //        {
        //            Lblok.Text = "Deleted Succesfully";
        //            divok.Visible = true;
        //        }
        //      //  lblpopupAlertMsg.Text = string.Empty;
        //        //divPopupAlert.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
    }

    protected void Btnok_Click(object sender, EventArgs e)
    {
        try
        {
            divPopAlertNEW.Visible = false;
            divok.Visible = false;
            Session["alters"] = "";
            Session["staconform"] = "";
            Session["alter_done"] = "";
            Session["conformleave"] = "";


        }
        catch (Exception ex)
        {

        }
    }

    protected void btnpopup_No_Click(object sender, EventArgs e)
    {
        try
        {
            lblpopupAlertMsg.Text = string.Empty;
            divPopupAlert.Visible = false;

            Session["alters"] = "";
            Session["staconform"] = "";
            Session["alter_done"] = "";
            Session["conformleave"] = "";
        }
        catch (Exception ex)
        {

        }
    }

    //magesh 24.5.18
    public void Chkdesch_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            Label7.Visible = false;
            txthostgelname1.Visible = false;
            Label16.Visible = false;
            txt_Build.Visible = false;
            Label8.Visible = false;
            txtfloor.Visible = false;
            Label9.Visible = false;
            txtroomno.Visible = false;
            Panel9.Visible = false;
            PBuild.Visible = false;
            panel14.Visible = false;
            panel15.Visible = false;
            checksstu = "dayscholar";
            TextBox78.Text = "";
            TextBox5.Text = "";
            //magesh 21.6.18
            if (ddl_gat_collegename.Items.Count > 0)
            {

                col_code = Convert.ToString(ddl_gat_collegename.SelectedItem.Value);
            }
        }
        catch
        {

        }
    }

    public void Chkhostel_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            Label7.Visible = true;
            txthostgelname1.Visible = true;
            Label16.Visible = true;
            txt_Build.Visible = true;
            Label8.Visible = true;
            txtfloor.Visible = true;
            Label9.Visible = true;
            txtroomno.Visible = true;
            Panel9.Visible = true;
            PBuild.Visible = true;
            panel14.Visible = true;
            panel15.Visible = true;
            checksstu = "Hosteler";
            TextBox78.Text = "";
            TextBox5.Text = "";
        }
        catch
        {

        }
    }

    public void dayscholar()
    {
        try
        {
            string namestudent = "";
            string roll_no = "";
            if (TextBox5.Text != "")
            {
                namestudent = " and r.stud_name='" + TextBox5.Text + "'";
            }
            else
            {
                namestudent = "";
            }
            if (TextBox78.Text != "")
            {
                roll_no = " and r.roll_no='" + TextBox78.Text + "'";
            }
            else
            {
                roll_no = "";
            }
            string test = "";
            if (ddlsec1.Text == "")
            {
                test = "";
            }
            else
            {
                test = "and r.Sections='" + ddlsec1.SelectedValue + "'";
            }
            // string sqlquery = " select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code,hs.Floor_Name,hs.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections) as Degree  from Hostel_StudentDetails hs,Registration r,Hostel_Details hd,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and Stud_Type='Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ") and Vacated = 0 and Relived = 0 and Suspension = 0 and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester ='" + ddlsem1.SelectedValue + "'   and hs.Hostel_Code in(" + hostel1 + ") and hs.Building_Name in('" + bulid1 + "') and hs.Floor_Name in('" + floor1 + "') " + test + " and hs.Room_Type in('" + room1 + "') " + namestudent + " " + roll_no + "";           
            //string sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections)as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ") and ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester='" + ddlsem1.SelectedValue + "'" ;  
            //magesh 24.5.18
            string sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,r.Batch_Year as HostelName,r.Roll_No HostelMasterPK,r.Roll_No Floor_Name,r.Roll_No Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections)as Degree,leavecount as HostelGatePassPerCount  from Registration r,gatepasscount g,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and  Stud_Type<>'Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ")  and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester='" + ddlsem1.SelectedValue + "' and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and d.Course_Id='" + ddldegree1.SelectedValue + "' and r.college_code='" + Convert.ToString(ddl_gat_collegename.SelectedValue) + "' and r.college_code=g.college_code ";

            ds2 = da.select_method_wo_parameter(sqlquery, "text");

            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            GridView1.Columns[10].Visible = false;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                erroemesssagelbl.Visible = false;
                GridView1.Visible = true;
                GridView1.DataSource = ds2;
                GridView1.DataBind();
                if (Session["Rollflag"].ToString() == "0")
                {
                    GridView1.Columns[2].Visible = false;
                }
                if (Session["Regflag"].ToString() == "0")
                {
                    GridView1.Columns[3].Visible = false;
                }
                //if (Session["Studflag"].ToString() == "0")
                //{
                //    GridView1.Columns[4].Visible = false;
                //}
            }
            else
            {
                erroemesssagelbl.Visible = true;
                erroemesssagelbl.Text = "No Records Found";
                GridView1.Visible = false;
                paneladd.Visible = false;
            }
        }
        catch
        {

        }
    }

    public void dayscholar1()
    {
        try
        {
            string namestudent = "";
            string roll_no = "";
            if (TextBox5.Text != "")
            {
                namestudent = " and r.stud_name='" + TextBox5.Text + "'";
            }
            else
            {
                namestudent = "";
            }
            if (TextBox78.Text != "")
            {
                roll_no = " and r.roll_no='" + TextBox78.Text + "'";
            }
            else
            {
                roll_no = "";
            }
            string test = "";
            if (ddlsec1.Text == "")
            {
                test = "";
            }
            else
            {
                test = "and r.Sections='" + ddlsec1.SelectedValue + "'";
            }
            // string sqlquery = " select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.Hostel_Name,hd.Hostel_code,hs.Floor_Name,hs.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections) as Degree  from Hostel_StudentDetails hs,Registration r,Hostel_Details hd,Degree d,Department dt,Course c where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.Hostel_code=hs.Hostel_Code and hs.Roll_Admit=r.Roll_Admit and Stud_Type='Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ") and Vacated = 0 and Relived = 0 and Suspension = 0 and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester ='" + ddlsem1.SelectedValue + "'   and hs.Hostel_Code in(" + hostel1 + ") and hs.Building_Name in('" + bulid1 + "') and hs.Floor_Name in('" + floor1 + "') " + test + " and hs.Room_Type in('" + room1 + "') " + namestudent + " " + roll_no + "";           
            //string sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type,hd.HostelName,hd.HostelMasterPK,f.Floor_Name,rm.Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections)as Degree,hd.HostelGatePassPerCount,hd.IsAllowUnApproveStud  from HT_HostelRegistration hs,Registration r,HM_HostelMaster hd,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id and hd.HostelMasterPK=hs.HostelMasterFK and r.App_No=hs.APP_No and hs.MemType='1' and hs.FloorFK=f.Floorpk and rm.Roompk=hs.Roomfk  and Stud_Type='Hostler' and r.degree_code =(" + ddldepart1.SelectedValue + ") and ISNULL(IsVacated,0) = 0 and ISNULL(IsDiscontinued,0) = 0 and ISNULL(IsSuspend,0) = 0 and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester='" + ddlsem1.SelectedValue + "'" ;  
            //magesh 24.5.18
            string sqlquery = "select distinct  r.Batch_Year,r.Roll_No,r.Reg_No,r.stud_name,r.Stud_Type as HostelName,r.Batch_Year ,r.Roll_No HostelMasterPK,r.Roll_No Floor_Name,r.Roll_No Room_Name,r.Batch_Year,(c.Course_Name +'-'+dt.Dept_Name+'-'+CONVERT(varchar(10), r.Current_Semester)+'-'+Sections)as Degree,leavecount as HostelGatePassPerCount  from Registration r,gatepasscount g,Degree d,Department dt,Course c,Floor_Master f,Room_Detail rm where d.Degree_Code =r.degree_code and d.Dept_Code =dt.Dept_Code and c.Course_Id =d.Course_Id  and r.degree_code =(" + ddldepart1.SelectedValue + ")  and r.Batch_Year ='" + ddlbatch1.SelectedValue + "' and r.Current_Semester='" + ddlsem1.SelectedValue + "' and r.CC=0 and r.DelFlag =0 and r.Exam_Flag <>'DEBAR' and d.Course_Id='" + ddldegree1.SelectedValue + "' and r.college_code='" + Convert.ToString(ddl_gat_collegename.SelectedValue) + "' and r.college_code=g.college_code";

            ds2 = da.select_method_wo_parameter(sqlquery, "text");

            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            GridView1.Columns[10].Visible = false;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                erroemesssagelbl.Visible = false;
                GridView1.Visible = true;
                GridView1.DataSource = ds2;
                GridView1.DataBind();
                if (Session["Rollflag"].ToString() == "0")
                {
                    GridView1.Columns[2].Visible = false;
                }
                if (Session["Regflag"].ToString() == "0")
                {
                    GridView1.Columns[3].Visible = false;
                }
                //if (Session["Studflag"].ToString() == "0")
                //{
                //    GridView1.Columns[4].Visible = false;
                //}
            }
            else
            {
                erroemesssagelbl.Visible = true;
                erroemesssagelbl.Text = "No Records Found";
                GridView1.Visible = false;
                paneladd.Visible = false;
            }
        }
        catch
        {

        }
    }

    public void view6()
    {
        try
        {
            //div_gate_reqstn.Visible = true;
            //paneladd.Visible = true;
            // bindgrid();
            Div5.Visible = true;
            div_gatestudview.Visible = true;
            string activerow = "";
            string req_date = "";
            string exitdate = "";
            string entrydate = "";
            string exittime = "";
            string entrytime = "";
            string Stud_Name = "";
            string rollno = "";
            string query = "";
            string Staff_Code = "";
            string rows = hid.Value;
            int row = 0;
            int.TryParse(rows, out row);

            Label roll = (Label)GridView1.Rows[row].FindControl("lblrollno");
            Label reg = (Label)GridView1.Rows[row].FindControl("lblreg");
            Label batch = (Label)GridView1.Rows[row].FindControl("lblstudtype");
            Label degree = (Label)GridView1.Rows[row].FindControl("lblstuddeg");
            Label name = (Label)GridView1.Rows[row].FindControl("lblname");
            Label count = (Label)GridView1.Rows[row].FindControl("lblper_count");
            Label hos = (Label)GridView1.Rows[row].FindControl("lblhostel");
            Label floor = (Label)GridView1.Rows[row].FindControl("lblfloorno");
            Label room = (Label)GridView1.Rows[row].FindControl("lblroomno");
            string stustype = d2.GetFunction("select Stud_Type from Registration where  Roll_No = '" + roll.Text + "'");
            if (stustype == "hostler" || stustype == "Hostler" || stustype == "HOSTLER")
            {
                hos2.Visible = true;
                // hos3.Visible = true;
                hos4.Visible = true;
                hos2.Visible = true;
                hos5.Visible = true;
                txt_gatehostel.Text = hos.Text;
                // txt_gatebuli.Text = hos.Text;
                txt_gateflr.Text = floor.Text;
                txt_gatermname.Text = room.Text;
            }
            else
            {
                hos2.Visible = false;
                // hos3.Visible = false;
                hos4.Visible = false;
                hos2.Visible = false;
                hos5.Visible = false;
            }


            string app = d2.GetFunction("select App_No from Registration where Roll_No='" + roll.Text + "'");
            string req = "select RequestCode,GateReqExitDate,RequisitionPK from RQ_Requisition where ReqAppNo='" + app + "' order by RequestCode desc";
            string reqnumb = string.Empty;
            DataSet gates = d2.select_method_wo_parameter(req, "text");
            if (gates.Tables[0].Rows.Count > 0 && gates.Tables.Count > 0)
            {
                TextBox2.Text = Convert.ToString(gates.Tables[0].Rows[0]["RequestCode"]);

                reqnumb = Convert.ToString(gates.Tables[0].Rows[0]["RequisitionPK"]);
            }
            else
            {
                TextBox2.Text = "";
                reqnumb = "";
            }
            string req_no = TextBox2.Text;
            string deg = degree.Text;
            string[] spl = deg.Split('-');
            txt_rollreq.Text = roll.Text;
            txt_namereq.Text = name.Text;
            txt_gatebatch.Text = batch.Text;
            if (spl.Length > 0)
                txt_degreq.Text = spl[0];
            if (spl.Length > 2)
                txt_deptreq.Text = spl[1];
            if (spl.Length > 3)
                txt_semreq.Text = spl[2];
            if (spl.Length > 4)
                txt_semreq.Text = spl[3];




            string query1 = "SELECT CASE WHEN RequestType = 6 THEN 'Gate Pass' END RequestType,RequestCode,(Select MasterValue FROM CO_MasterValues T WHERE R.GateReqReason = T.MasterCode) GateReqReason,ReqStaffAppNo,CONVERT(VARCHAR(11),GateReqExitDate,103) as GateReqExitDate,GateReqExitTime,CONVERT(VARCHAR(11),GateReqEntryDate,103) as GateReqEntryDate,GateReqEntryTime,CONVERT(VARCHAR(11),RequestDate,103) as RequestDate,(Select TextVal FROM TextValTable T WHERE R.RequestBy = T.TextCode) RequestBy,(Select TextVal FROM TextValTable T WHERE R.RequestMode = T.TextCode) RequestMode,Roll_No,Stud_Name,case when MemType=2 then  'Staff' when MemType =1 then 'Stuedent' end as memtype,r.RequestBy FROM RQ_Requisition R,Registration S    WHERE R.ReqAppNo = S.App_No  and  RequestType=6 and RequestCode='" + req_no + "' and RequisitionPK='" + reqnumb + "'";
            ds = d2.select_method_wo_parameter(query1, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                req_date = Convert.ToString(ds.Tables[0].Rows[0]["RequestDate"]);
                exitdate = Convert.ToString(ds.Tables[0].Rows[0]["GateReqExitDate"]);
                entrydate = Convert.ToString(ds.Tables[0].Rows[0]["GateReqEntryDate"]);
                string[] ay = req_date.Split('/');
                string[] ay1 = exitdate.Split('/');
                string[] ay2 = entrydate.Split('/');

                TextBox4.Text = ay[0].ToString() + "/" + ay[1].ToString() + "/" + ay[2].ToString();
                TextBox4.Enabled = false;
                txt_appldatereq.Text = ay[0].ToString() + "/" + ay[1].ToString() + "/" + ay[2].ToString();




                TextBox6.Text = ay[0].ToString() + "/" + ay[1].ToString() + "/" + ay[2].ToString();
                TextBox6.Enabled = false;
                txtfromdate1.Text = ay1[0].ToString() + "/" + ay1[1].ToString() + "/" + ay1[2].ToString();
                txtfromdate1.Enabled = false;
                txttodate1.Text = ay2[0].ToString() + "/" + ay2[1].ToString() + "/" + ay2[2].ToString();
                txttodate1.Enabled = false;
                string res = Convert.ToString(ds.Tables[0].Rows[0]["GateReqReason"]);
                DropDownList1.Enabled = false;
                DropDownList1.Items.Insert(0, res);
                DropDownList2.Items.Insert(0, Convert.ToString(ds.Tables[0].Rows[0]["RequestBy"]));
                DropDownList2.Enabled = false;

                exittime = ds.Tables[0].Rows[0]["GateReqExitTime"].ToString();
                entrytime = ds.Tables[0].Rows[0]["GateReqEntryTime"].ToString();
                string[] split1 = exittime.Split(':');

                ddlhour1.Items.Insert(0, (split1[0]));
                ddlmin1.Items.Insert(0, (split1[1]));
                ddlsession1.Items.Insert(0, (split1[2]));
                ddlhour1.Enabled = false;
                ddlmin1.Enabled = false;
                ddlsession1.Enabled = false;
                string[] split3 = entrytime.Split(':');

                ddlendhour1.Items.Insert(0, (split3[0]));
                ddlendmin1.Items.Insert(0, (split3[1]));
                ddlenssession1.Items.Insert(0, (split3[2]));

                ddlendhour1.Enabled = false;
                ddlendmin1.Enabled = false;
                ddlenssession1.Enabled = false;
            }
            else
            {
                TextBox4.Text = "";
                ddlendmin1.Items.Clear();
                ddlendhour1.Items.Clear();
                ddlenssession1.Items.Clear();
                ddlhour1.Items.Clear();
                ddlendhour1.Items.Clear();
                ddlsession1.Items.Clear();
                ddlmin1.Items.Clear();
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                txt_appldatereq.Text = "";
                TextBox6.Text = "";
                txtfromdate1.Text = "";
                txttodate1.Text = "";
                DropDownList1.Enabled = false;
                ddlendhour1.Enabled = false;
                ddlendmin1.Enabled = false;
                ddlenssession1.Enabled = false;
                ddlhour1.Enabled = false;
                ddlmin1.Enabled = false;
                ddlsession1.Enabled = false;
                TextBox6.Enabled = false;
                DropDownList2.Enabled = false;
                txttodate1.Enabled = false;
                txtfromdate1.Enabled = false;
            }

            bindgridleave();

        }
        catch
        {
        }
    }

    public void bindgridleave()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Month");
            dt.Columns.Add("allleave");
            dt.Columns.Add("grantleave");
            dt.Columns.Add("balleave");
            Hashtable monthlst = new Hashtable();
            monthlst.Clear();
            monthlst.Add(01, "January");
            monthlst.Add(02, "Feburary");
            monthlst.Add(03, "March");
            monthlst.Add(04, "April");
            monthlst.Add(05, "May");
            monthlst.Add(06, "June");
            monthlst.Add(07, "July");
            monthlst.Add(08, "August");
            monthlst.Add(09, "September");
            monthlst.Add(10, "October");
            monthlst.Add(11, "November");
            monthlst.Add(12, "December");
            DataRow dr = null;
            string activerow = "";
            string req_no = "";
            //if (div_all.Visible == true)
            //{
            //    activerow = Fpspread9.ActiveSheetView.ActiveRow.ToString();
            //    req_no = Convert.ToString(Fpspread9.Sheets[0].Cells[Convert.ToInt32(activerow), 0].Tag);
            //}
            //else
            //{
            //    activerow = Fpspread6.ActiveSheetView.ActiveRow.ToString();
            //    req_no = Convert.ToString(Fpspread6.Sheets[0].Cells[Convert.ToInt32(activerow), 0].Tag);
            //}
            string rows = hid.Value;
            int row = 0;
            int.TryParse(rows, out row);
            Label reg = (Label)GridView1.Rows[row].FindControl("lblreg");
            string regno = reg.Text;
            string appno = d2.GetFunction("select App_no from Registration where reg_no='" + regno + "'");
            //string appno = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=6 and RequisitionPK='" + req_no + "'");

            string stu_type = d2.GetFunction("select Stud_Type  from Registration where App_no ='" + appno + "'");
            string app1 = string.Empty;
            string gatecount = string.Empty;
            if (stu_type == "hostler" || stu_type == "Hostler" || stu_type == "HOSTLER")
            {
                app1 = d2.GetFunction("select HostelMasterFK from HT_HostelRegistration where app_no='" + appno + "'");

                gatecount = d2.GetFunction("select HostelGatePassPerCount from HM_HostelMaster where HostelMasterPK='" + app1 + "'");
            }
            else
                gatecount = d2.GetFunction("select leavecount from gatepasscount  where college_code='" + collegecode1 + "'");

            string leave = d2.GetFunction("select COUNT(ReqAppStatus) as count from RQ_Requisition where ReqAppNo='" + appno + "' and RequestType=6 and ReqAppStatus='1'");
            string alllev = "";
            string allotleave = string.Empty;
            if (stu_type == "hostler" || stu_type == "Hostler" || stu_type == "HOSTLER")
            {
                allotleave = d2.GetFunction("select HostelGatePassPerCount from HM_HostelMaster hm,HT_HostelRegistration hr where hm.HostelmasterPK=hr.HostelMasterFK and App_No='" + appno + "' and MemType='1'");
            }
            else
                allotleave = d2.GetFunction("select leavecount from gatepasscount  where college_code='" + collegecode1 + "'");
            if (allotleave.Trim() != "0")
            {
                alllev = allotleave;
            }
            else
            {
                alllev = "0";
            }
            for (int i = 1; i < 12; i++)
            {
                dr = dt.NewRow();
                dr["month"] = Convert.ToString(monthlst[i]);
                dr["allleave"] = Convert.ToString(alllev);
                string val = Convert.ToString(i);
                if (val.Length == 1)
                {
                    val = "0" + val;
                }
                else
                {
                    val = Convert.ToString(i);
                }
                string selquery = string.Empty;
                if (stu_type == "hostler" || stu_type == "Hostler" || stu_type == "HOSTLER")
                {
                    selquery = "select ISNULL(HostelGatePassPerCount,0)-SUM(ISNULL(ReqAppStatus,0)) as Gatepasscount,SUM(ReqAppStatus) as Approvecount from HM_HostelMaster hm,HT_HostelRegistration hr,RQ_Requisition req where hm.HostelMasterPK=hr.HostelMasterFK and hr.APP_No=req.ReqAppNo and hr.MemType='1' and ReqAppStatus='1' and hr.APP_No='" + appno + "' and MONTH(RequestDate)=MONTH(GateReqEntryDate) and SUBSTRING(CONVERT(varchar(10), RequestDate, 105), 4, 2)='" + val + "' group by HostelGatePassPerCount";
                }
                else
                    selquery = "select ISNULL(leavecount,0)-SUM(ISNULL(ReqAppStatus,0)) as Gatepasscount,SUM(ReqAppStatus) as Approvecount from gatepasscount,registration r,RQ_Requisition req where r.APP_No=req.ReqAppNo  and ReqAppStatus='1' and r.APP_No='" + appno + "' and MONTH(RequestDate)=MONTH(GateReqEntryDate) and SUBSTRING(CONVERT(varchar(10), RequestDate, 105), 4, 2)='" + val + "' group by leavecount";


                ds.Clear();
                ds = d2.select_method_wo_parameter(selquery, "Text");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dr["grantleave"] = Convert.ToString(ds.Tables[0].Rows[0]["Approvecount"]);
                        dr["balleave"] = Convert.ToString(ds.Tables[0].Rows[0]["Gatepasscount"]);
                    }
                    else
                    {
                        dr["grantleave"] = 0;
                        dr["balleave"] = Convert.ToInt32(alllev) - Convert.ToInt32(dr["grantleave"]);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                grdshow.DataSource = dt;
                grdshow.DataBind();
                grdshow.Visible = true;
                divright.Visible = true;
            }
        }
        catch
        {
        }
    }

    public void btn_popclose_Click(object sender, EventArgs e)
    {
        Div5.Visible = false;
    }

    //magesh 9.6.18
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> getstat(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string query = "select mastervalue,mastercode from CO_MasterValues where mastercriteria='State' and CollegeCode='" + colleg + "' and mastervalue like '" + prefixText + "%' order by MasterValue  ";
        name = ws.Getname(query);
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> getdis(string prefixText)
    {
        WebService ws = new WebService();
        List<string> name = new List<string>();
        string query = "select mastervalue,mastercode from CO_MasterValues where mastercriteria='District' and CollegeCode='" + colleg + "' and mastervalue like '" + prefixText + "%' order by MasterValue  ";

        name = ws.Getname(query);
        return name;
    }
    public void sms1(string req, string strmsg)
    {
        user_id = d2.GetFunction("select SMS_User_ID from Track_Value where college_code='" + collegecode1 + "'");
        string getval = d2.GetUserapi(user_id);
        string[] spret = getval.Split('-');
        if (spret.GetUpperBound(0) == 1)
        {
            SenderID = spret[0].ToString();
            Password = spret[1].ToString();
            Session["api"] = user_id;
            Session["senderid"] = SenderID;
        }
        string appno = d2.GetFunction("select ReqAppNo from RQ_Requisition where RequestType=5 and RequisitionPK='" + req + "'");
        string queryhier = "select * from RQ_RequestHierarchy where ReqStaffAppNo='" + appno + "'";
        DataSet smsds = new DataSet();
        smsds.Clear();
        smsds = d2.select_method_wo_parameter(queryhier, "text");
        if (smsds.Tables[0].Rows.Count > 0)
        {
            for (int sm = 0; sm < smsds.Tables[0].Rows.Count; sm++)
            {
                string reqapprovestaffapplid = Convert.ToString(smsds.Tables[0].Rows[sm]["ReqAppStaffAppNo"]);
                string staffnum = d2.GetFunction("select per_mobileno from staff_appl_master where appl_id='" + reqapprovestaffapplid + "'");
                mobilenos = staffnum;
                if (mobilenos != "")
                {
                    //string strpath = "http://dnd.airsmsmarketing.info/api/sendmsg.php?user=" + user_id + "&pass=" + Password + "&sender=" + SenderID + "&phone=" + mobilenos + "&text=" + strmsg + "&priority=ndnd&stype=normal";
                    //smsreport(strpath, isst);
                    isst = "1";
                    int smsdet = d2.send_sms(user_id, collegecode1, usercode, mobilenos, strmsg, isst);
                }
            }
        }

        // strmsg = "Approved Successfully!";

    }

    protected void chkhr_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox ddlLabTest = (CheckBox)sender;
            var row = ddlLabTest.NamingContainer;
            string rowIndxS = ddlLabTest.UniqueID.ToString().Split('$')[3].Replace("ctl", string.Empty);
            int rowIndx = Convert.ToInt32(rowIndxS) - 2;
            string colIndxS = ddlLabTest.UniqueID.ToString().Split('$')[4].Replace("chkhr", string.Empty);
            int colIndx = Convert.ToInt32("4");
            CheckBox ddlAddLabTestShortName = (CheckBox)row.FindControl("chkhr");
            CheckBoxList cbl = (CheckBoxList)row.FindControl("cblhr");
            TextBox txtB = (TextBox)row.FindControl("txthour");
            CallCheckboxChange(ddlAddLabTestShortName, cbl, txtB, "Hours", "--Select--");
        }
        catch (Exception ex)
        {
        }
    }

    private void CallCheckboxChange(CheckBox cb, CheckBoxList cbl, TextBox txt, string dispst, string deft)
    {
        try
        {
            int sel = 0;
            string name = string.Empty;
            txt.Text = deft;
            if (cb.Checked == true)
            {
                for (sel = 0; sel < cbl.Items.Count; sel++)
                {
                    cbl.Items[sel].Selected = true;
                    name = Convert.ToString(cbl.Items[sel].Text);
                }
                if (cbl.Items.Count == 1)
                {
                    txt.Text = "" + name + "";
                }
                else
                {
                    txt.Text = dispst + "(" + cbl.Items.Count + ")";
                }
            }
            else
            {
                for (sel = 0; sel < cbl.Items.Count; sel++)
                {
                    cbl.Items[sel].Selected = false;
                }
                txt.Text = deft;
            }
        }
        catch { }
    }
    private void CallCheckboxListChange(CheckBox cb, CheckBoxList cbl, TextBox txt, string dipst, string deft)
    {
        try
        {
            int sel = 0;
            int count = 0;
            string name = string.Empty;
            cb.Checked = false;
            txt.Text = deft;
            for (sel = 0; sel < cbl.Items.Count; sel++)
            {
                if (cbl.Items[sel].Selected == true)
                {
                    count++;
                    name = Convert.ToString(cbl.Items[sel].Text);
                }
            }
            if (count > 0)
            {
                if (count == 1)
                {
                    txt.Text = "" + name + "";
                }
                else
                {
                    txt.Text = dipst + "(" + count + ")";
                }
                if (cbl.Items.Count == count)
                {
                    cb.Checked = true;
                }
            }
        }
        catch { }
    }

    private void checkBoxListselectOrDeselect(CheckBoxList cbl, bool selected = true)
    {
        try
        {
            foreach (wc.ListItem li in cbl.Items)
            {
                li.Selected = selected;
            }
        }
        catch
        {
        }
    }



    //Added By Saranyadevi 29.11.2018
    protected void btn_newitem_Click(object sender, EventArgs e)
    {
        try
        {
            plusdiv.Visible = true;
            panel_addgroup.Visible = true;
            Newitem();
        }
        catch
        {

        }

    }


    public void Newitem()
    {
        try
        {
            lbl_itemcod.Text = "";
            string newitemcode = "";
            string selectquery = "select ItemAcr,ItemStNo,ItemSize  from IM_CodeSettings order by startdate desc";//where Latestrec =1"
            ds = d2.select_method_wo_parameter(selectquery, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string itemacronym = Convert.ToString(ds.Tables[0].Rows[0]["ItemAcr"]);
                string itemstarno = Convert.ToString(ds.Tables[0].Rows[0]["ItemStNo"]);
                string itemsize = Convert.ToString(ds.Tables[0].Rows[0]["ItemSize"]);
                if (itemacronym.Trim() != "" && itemstarno.Trim() != "")
                {
                    selectquery = " select distinct top (1) ItemCode  from IM_ItemMaster where ItemCode like '" + Convert.ToString(itemacronym) + "%' order by ItemCode desc";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selectquery, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string itemcode = Convert.ToString(ds.Tables[0].Rows[0]["ItemCode"]);
                        string itemacr = Convert.ToString(itemacronym);
                        int len = itemacr.Length;
                        itemcode = itemcode.Remove(0, len);
                        int len1 = Convert.ToString(itemcode).Length;
                        string newnumber = Convert.ToString((Convert.ToInt32(itemcode) + 1));
                        len = Convert.ToString(newnumber).Length;
                        len1 = len1 - len;
                        if (len1 == 2)
                        {
                            newitemcode = "00" + newnumber;
                        }
                        else if (len1 == 1)
                        {
                            newitemcode = "0" + newnumber;
                        }
                        else if (len1 == 3)
                        {
                            newitemcode = "000" + newnumber;
                        }
                        else if (len1 == 4)
                        {
                            newitemcode = "0000" + newnumber;
                        }
                        else if (len1 == 5)
                        {
                            newitemcode = "00000" + newnumber;
                        }
                        else if (len1 == 6)
                        {
                            newitemcode = "000000" + newnumber;
                        }
                        else
                        {
                            newitemcode = Convert.ToString(newnumber);
                        }
                        if (newitemcode.Trim() != "")
                        {
                            newitemcode = itemacr + "" + newitemcode;
                        }
                    }
                    else
                    {
                        string itemacr = Convert.ToString(itemstarno);
                        int len = itemacr.Length;
                        string items = Convert.ToString(itemsize);
                        int len1 = Convert.ToInt32(items);
                        int size = len1 - len;
                        if (size == 2)
                        {
                            newitemcode = "00" + itemstarno;
                        }
                        else if (size == 1)
                        {
                            newitemcode = "0" + itemstarno;
                        }
                        else if (size == 3)
                        {
                            newitemcode = "000" + itemstarno;
                        }
                        else if (size == 4)
                        {
                            newitemcode = "0000" + itemstarno;
                        }
                        else if (size == 5)
                        {
                            newitemcode = "00000" + itemstarno;
                        }
                        else if (size == 6)
                        {
                            newitemcode = "000000" + itemstarno;
                        }
                        else
                        {
                            newitemcode = Convert.ToString(itemstarno);
                        }
                        newitemcode = Convert.ToString(itemacronym) + "" + Convert.ToString(newitemcode);
                    }
                    lbl_itemcod.Text = Convert.ToString(newitemcode);

                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Please Update Code Master";
                }
            }
        }
        catch
        {

        }
    }

    protected void btn_addgroup_Click(object sender, EventArgs e)
    {
        try
        {

            int insert = 0;
            if (txt_addnewitem.Text != "")
            {
                string subheader_code = "";
                string itemheadername = "No Header";
                string itemheadercode = "";
                string itemcode = "";
                string itemname = "";
                string box = "";
                int code = 0;
                string dtaccessdate = DateTime.Now.ToString();
                string dtaccesstime = DateTime.Now.ToLongTimeString();
                string itcode1 = "";

                itemheadername = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(itemheadername);
                if (itemheadername.Trim() != "")
                {

                    itemheadername = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(itemheadername);
                    if (itemheadername.Trim() != "")
                    {
                        string selectquer = d2.GetFunction("select ItemHeaderCode from IM_ItemMaster where ItemHeaderName='No Header'   order by CONVERT(numeric,ItemHeaderCode) desc");
                        if (selectquer != "" && selectquer != "0")
                            itemheadercode = Convert.ToString(selectquer);
                        else
                        {
                            string selectquery = d2.GetFunction("select ItemHeaderCode from IM_ItemMaster   order by CONVERT(numeric,ItemHeaderCode) desc");
                            if (selectquery.Trim() != "")
                            {
                                if (int.TryParse(selectquery, out code))
                                {
                                    code = code + 1;
                                    itemheadercode = Convert.ToString(code);
                                }
                                else
                                {
                                    string applno = selectquery.Remove(0, 3);
                                    int len = applno.Length;
                                    int codevalue = Convert.ToInt32(applno);
                                    codevalue = codevalue + 1;
                                    int len1 = Convert.ToString(codevalue).Length;
                                    len = len - len1;
                                    if (len == 2)
                                    {
                                        itemheadercode = "00" + codevalue;
                                    }
                                    else if (len == 1)
                                    {
                                        itemheadercode = "0" + codevalue;
                                    }
                                    else
                                    {
                                        itemheadercode = Convert.ToString(codevalue);
                                    }
                                    if (itemheadercode.Trim() != "")
                                    {
                                        itemheadercode = "ITH" + itemheadercode;
                                    }
                                }
                            }
                            else
                            {
                                code = 1;
                                itemheadercode = Convert.ToString(code);
                            }
                        }
                    }
                }

                itemcode = Convert.ToString(lbl_itemcod.Text);
                itemname = Convert.ToString(txt_addnewitem.Text);
                itemname = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(itemname);

                string subheadername = "No Sub Header";
                string isgrp = "Subheader";
                subheader_code = subheadercode(isgrp, subheadername);


                Boolean gvdatassvis = false;
                if (subheader_code.Trim() != "")
                {

                    if (gvdatass.Visible == true)
                    {
                        gvdatassvis = true;
                        string selecquery = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where  ItemName='" + txt_addnewitem.Text + "' and ItemHeaderName='No Header' order by ItemCode";
                        DataSet dtitem = d2.select_method_wo_parameter(selecquery, "Text");
                        if (dtitem.Tables.Count > 0 && dtitem.Tables[0].Rows.Count > 0)
                            itcode1 = Convert.ToString(dtitem.Tables[0].Rows[0]["ItemCode"]);
                        for (int i = 0; i < gvdatass.Items.Count; i++)
                        {
                            Label box1 = (Label)gvdatass.Items[i].FindControl("lbl_itemcode");
                            box = Convert.ToString(box1.Text);
                            if (lbl_error3.Visible == false)
                            {
                                if (box.Trim() == itcode1)
                                {
                                    imgdiv2.Visible = true;
                                    lbl_alert.Text = "This Item Already Added";
                                    txt_addnewitem.Text = "";
                                    return;
                                }
                                else
                                {

                                    gvdatassvis = false;
                                }
                            }

                        }

                    }


                    if (!gvdatassvis)
                    {

                        string insertquery = "";
                        string itmcod2 = d2.GetFunction("select ItemCode  from IM_ItemMaster where ItemName='" + txt_addnewitem.Text + "' and ItemHeaderName='" + itemheadername + "'");
                        if (itmcod2 != "" && itmcod2 != "0")
                        {
                            insertquery = "if not exists(select * from IM_ItemMaster where ItemHeaderCode='" + itemheadercode + "' and ItemHeaderName='" + itemheadername + "' and subheader_code='" + subheader_code + "' and ItemCode='" + itmcod2 + "') insert into IM_ItemMaster (ItemCode,ItemName,ItemHeaderCode,ItemHeaderName,subheader_code) values ('" + itemcode + "','" + itemname + "','" + itemheadercode + "','" + itemheadername + "','" + subheader_code + "') else update IM_ItemMaster set ItemName='" + itemname + "' where ItemHeaderCode='" + itemheadercode + "' and ItemHeaderName='" + itemheadername + "' and subheader_code='" + subheader_code + "' and ItemCode='" + itmcod2 + "' ";

                        }
                        else
                        {
                            insertquery = " insert into IM_ItemMaster (ItemCode,ItemName,ItemHeaderCode,ItemHeaderName,subheader_code) values ('" + itemcode + "','" + itemname + "','" + itemheadercode + "','" + itemheadername + "','" + subheader_code + "') ";
                        }
                        ds.Clear();
                        insert = d2.update_method_wo_parameter(insertquery, "Text");
                        if (insert != 0)
                        {
                            itemmaster();
                            itemheader();
                            loadsubheadername();
                            string selectquery1 = "select ItemHeaderName,ItemHeaderCode,ItemCode,ItemName ,ItemModel,ItemSize ,ItemUnit  from IM_ItemMaster where ItemName='" + itemname + "' and ItemHeaderName='" + itemheadername + "' and ItemHeaderCode='" + itemheadercode + "' order by ItemCode";

                            if (selectquery1.Trim() != "")
                            {

                                DataRow dr = null;
                                if (ViewState["selecteditem"] == null)
                                {
                                    data.Columns.Add("ItemName");
                                    data.Columns.Add("ItemCode");
                                    data.Columns.Add("ItemHeaderName");
                                    data.Columns.Add("ItemHeaderCode");
                                    data.Columns.Add("ItemUnit");
                                    ds.Clear();
                                    ds = d2.select_method_wo_parameter(selectquery1, "Text");
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {

                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            dr = data.NewRow();
                                            dr[0] = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]);
                                            dr[1] = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                                            data.Rows.Add(dr);
                                        }
                                    }
                                }
                                else
                                {
                                    data = (DataTable)ViewState["selecteditem"];
                                    ds.Clear();
                                    ds = d2.select_method_wo_parameter(selectquery1, "Text");
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            dr = data.NewRow();
                                            dr[0] = Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]);
                                            dr[1] = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                                            data.Rows.Add(dr);
                                        }
                                    }

                                }
                                if (data.Rows.Count > 0)
                                {
                                    gvdatass.DataSource = data;
                                    gvdatass.DataBind();
                                    gvdatass.Visible = true;
                                    ViewState["selecteditem"] = data;
                                    div2.Visible = true;
                                    btn_itemsave4.Visible = true;
                                    btn_conexist4.Visible = true;
                                    lbl_error3.Visible = false;
                                    selectedmenuchk(sender, e);

                                }
                            }
                        }
                    }

                }
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Text = "Item Added Successfully";
                    txt_addnewitem.Text = "";
                    plusdiv.Visible = false;
                    panel_addgroup.Visible = false;
                }

            }

            else
            {
                plusdiv.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "Enter the Item Name";
            }
        }
        catch
        {

        }
    }
    protected void btn_exitaddgroup_Click(object sender, EventArgs e)
    {
        plusdiv.Visible = false;
        panel_addgroup.Visible = false;
        txt_addnewitem.Text = "";
    }


    public string subheadercode(string textcri, string subjename)
    {
        string subjec_no = "";
        try
        {
            // subjename = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(subjename);
            string select_subno = "select MasterCode from CO_MasterValues where MasterCriteria='" + textcri + "' and CollegeCode =" + collegecode1 + " and MasterValue='" + subjename + "'";
            ds.Clear();
            ds = d2.select_method_wo_parameter(select_subno, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                subjec_no = Convert.ToString(ds.Tables[0].Rows[0]["MasterCode"]);
            }
            else
            {
                string insertquery = "insert into CO_MasterValues(MasterCriteria,MasterValue,CollegeCode) values('" + textcri + "','" + subjename + "','" + collegecode1 + "')";
                int result = d2.update_method_wo_parameter(insertquery, "Text");
                if (result != 0)
                {
                    string select_subno1 = "select MasterCode from CO_MasterValues where MasterCriteria='" + textcri + "' and CollegeCode =" + collegecode1 + " and MasterValue='" + subjename + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(select_subno1, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        subjec_no = Convert.ToString(ds.Tables[0].Rows[0]["MasterCode"]);
                    }
                }
            }
        }
        catch
        {

        }
        return subjec_no;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getitemname(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct ItemName from IM_ItemMaster WHERE ItemHeaderName='No Header' and ItemName like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
            {
                name.Add(dw.Tables[0].Rows[i]["ItemName"].ToString());
            }
        }
        return name;
    }
}