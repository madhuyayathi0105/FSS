﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Hostelmod/hostelsite.master" AutoEventWireup="true"
    CodeFile="HM_GuestAttendance.aspx.cs" Inherits="HM_GuestAttendance" %>

    <%@ register src="~/Usercontrols/PrintMaster.ascx" tagname="printmaster" tagprefix="Insproplus" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <%@ register assembly="FarPoint.Web.Spread" namespace="FarPoint.Web.Spread" tagprefix="FarPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1">
        <title></title>
        <link href="~/Styles/css/Commoncss.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .maindivstylesize
            {
                height: 830px;
                width: 1000px;
            }
        </style>
    </head>
    <body>
        <script type="text/javascript">
            function display() {
                document.getElementById('<%=lblvalidation1.ClientID %>').innerHTML = "";
            }
        </script>
        <form id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <center>
            <span style="color: green" class="fontstyleheader">Guest Attendance</span>
            <br />
            <br />
        </center>
        <center>
            <div>
                <div class="maindivstyle maindivstylesize">
                    <br />
                    <center>
                        <table style="margin-left: 11px; height: 40px;" class="maintablestyle">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_hostelname" Text="Hostel Name" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_hostelname" runat="server" Visible="false" AutoPostBack="True"
                                        CssClass="textbox ddlheight4">
                                    </asp:DropDownList>
                                    <asp:UpdatePanel ID="upp_hostelname" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_hostelname" runat="server" CssClass="textbox textbox1 txtheight2"
                                                ReadOnly="true" onfocus="return myFunction1(this)">--Select--</asp:TextBox>
                                            <asp:Panel ID="panel_hostelname" runat="server" BorderStyle="Solid" BorderWidth="2px"
                                                CssClass="multxtpanel" Style="position: absolute; height: 200px; width: 180px;">
                                                <asp:CheckBox ID="cb_hostelname" runat="server" Text="Select All" AutoPostBack="True"
                                                    OnCheckedChanged="cb_hostelname_CheckedChanged" />
                                                <asp:CheckBoxList ID="cbl_hostelname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_hostelname_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="popupext_hostelname" runat="server" TargetControlID="txt_hostelname"
                                                PopupControlID="panel_hostelname" Position="Bottom">
                                            </asp:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_building" runat="server" Text="Building Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upp_building" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_buildingname" runat="server" CssClass="textbox textbox1 txtheight2"
                                                ReadOnly="true">-- Select--</asp:TextBox>
                                            <asp:Panel ID="panel_building" runat="server" CssClass="multxtpanel" Style="height: 200px;
                                                width: 180px;">
                                                <asp:CheckBox ID="cb_buildingname" runat="server" Text="Select All" AutoPostBack="true"
                                                    OnCheckedChanged="cbbuildname_CheckedChange" />
                                                <asp:CheckBoxList ID="cbl_buildingname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblbuildname_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="popupext_buildingname" runat="server" TargetControlID="txt_buildingname"
                                                PopupControlID="panel_building" Position="Bottom">
                                            </asp:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_floorname" runat="server" Text="Floor Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upp_floorname" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_floorname" runat="server" CssClass="textbox textbox1 txtheight2"
                                                Height="20px" ReadOnly="true">-- Select--</asp:TextBox>
                                            <asp:Panel ID="panel_floorname" runat="server" CssClass="multxtpanel" Style="height: 200px;
                                                width: 180px;">
                                                <asp:CheckBox ID="cb_floorname" runat="server" Text="Select All" AutoPostBack="true"
                                                    OnCheckedChanged="cbfloorname_CheckedChanged" />
                                                <asp:CheckBoxList ID="cbl_floorname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblfloorname_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="popupext_floorname" runat="server" TargetControlID="txt_floorname"
                                                PopupControlID="panel_floorname" Position="Bottom">
                                            </asp:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_roomname" runat="server" Text="Room Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upp_roomname" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_roomname" runat="server" CssClass="textbox textbox1 txtheight2"
                                                ReadOnly="true">-- Select--</asp:TextBox>
                                            <asp:Panel ID="panel_roomname" runat="server" CssClass="multxtpanel multxtpanleheight"
                                                Style="height: 200px; width: 180px;">
                                                <asp:CheckBox ID="cb_roomname" runat="server" Text="Select All" AutoPostBack="true"
                                                    OnCheckedChanged="cbroomname_CheckedChanged" />
                                                <asp:CheckBoxList ID="cbl_roomname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblroomname_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="popupext_roomname" runat="server" TargetControlID="txt_roomname"
                                                PopupControlID="panel_roomname" Position="Bottom">
                                            </asp:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_fromdate" runat="server" Text="From Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fromdate" runat="server" CssClass="textbox textbox1 txtheight"
                                        AutoPostBack="true" OnTextChanged="txt_fromdate_Textchanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="calfromdate" TargetControlID="txt_fromdate" runat="server"
                                        Format="dd/MM/yyyy">
                                        <%--CssClass="cal_Theme1 ajax__calendar_active"--%>
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_todate" runat="server" Text="To Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_todate" runat="server" CssClass="textbox textbox1 txtheight"
                                        AutoPostBack="true" OnTextChanged="txt_todate_Textchanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="caltodate" TargetControlID="txt_todate" runat="server"
                                        Format="dd/MM/yyyy">
                                        <%--CssClass="cal_Theme1 ajax__calendar_active"--%>
                                    </asp:CalendarExtender>
                                </td>
                                <%-- </tr>
                        <tr>--%>
                                <td>
                                    <asp:Button ID="btn_go" Text="Go" runat="server" CssClass="textbox btn1" OnClick="btn_go_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtn_present" runat="server" OnClick="imgbtn_presentclick"
                                        Visible="true" ImageUrl="~/gv images/Tick.png" />
                                    <asp:Label ID="lbl_pre" runat="server" Text="Present"></asp:Label>
                                    <asp:ImageButton ID="imgbtn_abst" runat="server" Visible="true" OnClick="imgbtn_abstclick"
                                        ImageUrl="~/gv images/Tick.png" />
                                    <asp:Label ID="lblabst" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_abs" runat="server" Text="Absent"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <center>
                            <div>
                                <asp:Label ID="lbl_error1" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl_error" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl_holiday" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl_err" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </center>
                        <div>
                            <center>
                                <asp:Panel ID="pheaderfilter" runat="server" CssClass="maintablestyle" Height="22px"
                                    Width="889px">
                                    <%--&nbsp;Filter your Search here&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:Label ID="Labelfilter" Text="Column Order" runat="server" Font-Size="Medium"
                                        Font-Bold="True" Font-Names="Book Antiqua" Style="margin-left: 0%;" />
                                    <asp:Image ID="Imagefilter" runat="server" CssClass="cpimage" ImageUrl="~/images/right.jpeg"
                                        ImageAlign="Right" />
                                </asp:Panel>
                            </center>
                        </div>
                        <br />
                        <center>
                            <asp:Panel ID="pcolumnorder" runat="server" CssClass="maintablestyle" Width="890px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cb_column" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                                                Font-Size="Medium" Text="Select All" AutoPostBack="true" OnCheckedChanged="cb_column_CheckedChanged" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnk_columnorder" runat="server" Font-Size="X-Small" Height="16px"
                                                Style="font-family: 'Book Antiqua'; font-weight: 700; font-size: small; margin-left: -477px;"
                                                Visible="false" Width="111px" OnClick="lb_Click">Remove  All</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="tborder" Visible="false" Width="867px" TextMode="MultiLine" CssClass="style1"
                                                AutoPostBack="true" runat="server" Enabled="false">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="cblcolumnorder" runat="server" Height="43px" AutoPostBack="true"
                                                Width="850px" Style="font-family: 'Book Antiqua'; font-weight: 700; font-size: medium;"
                                                RepeatColumns="6" RepeatDirection="Horizontal" OnSelectedIndexChanged="cbl_columnorder_SelectedIndexChanged">
                                                <asp:ListItem Value="Hostel_Name">Hostel Name</asp:ListItem>
                                                   <asp:ListItem Value="id">Guest Id</asp:ListItem>
                                                <asp:ListItem Value="Guest_Name">Guest Name</asp:ListItem>
                                              
                                                <asp:ListItem Value="Guest_Address">Guest Address</asp:ListItem>
                                                <asp:ListItem Value="MobileNo">Mobile No</asp:ListItem>
                                                <asp:ListItem Value="From_Company">From Company</asp:ListItem>
                                                <asp:ListItem Value="Floor_Name">Floor Name</asp:ListItem>
                                                <asp:ListItem Value="Room_Name">Room Name</asp:ListItem>
                                                <%-- <asp:ListItem Value="Hostel_Code">Hostel Code</asp:ListItem>--%>
                                                <%--<asp:ListItem Value="Admission_Date">Admission_Date</asp:ListItem>--%>
                                                <asp:ListItem Value="Building_Name">Building Name</asp:ListItem>
                                                <%-- <asp:ListItem Value="Floor_Name">Floor Name</asp:ListItem>
                                            <asp:ListItem Value="Room_Name">Room No</asp:ListItem>--%>
                                                <asp:ListItem Value="Guest_Street">Guest Street</asp:ListItem>
                                                <asp:ListItem Value="Guest_City">Guest City</asp:ListItem>
                                                <asp:ListItem Value="Guest_PinCode">Guest Pincode</asp:ListItem>
                                                <%--<asp:ListItem Value="Purpose">Purpose</asp:ListItem>--%>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </center>
                        <asp:CollapsiblePanelExtender ID="cpecolumnorder" runat="server" TargetControlID="pcolumnorder"
                            CollapseControlID="pheaderfilter" ExpandControlID="pheaderfilter" Collapsed="true"
                            TextLabelID="Labelfilter" CollapsedSize="0" ImageControlID="Imagefilter" CollapsedImage="~/images/right.jpeg"
                            ExpandedImage="~/images/down.jpeg">
                        </asp:CollapsiblePanelExtender>
                        <%--end column order--%>
                    </center>
                    <p style="width: 691px;" align="right">
                        <asp:Label ID="lbl_errorsearch1" runat="server" Visible="false" Font-Bold="true"
                            ForeColor="Red"></asp:Label>
                    </p>
                    <br />
                    <center>
                        <div id="dat" visible="false" runat="server" style="width: 852px; overflow: auto;
                            height: 332px;" class="reportdivstyle table">
                            <asp:UpdatePanel ID="upd" runat="server">
                                <ContentTemplate>
                                    <FarPoint:FpSpread ID="FpSpread1" runat="server" Visible="false" Width="850px" Height="330px"
                                        class="spreadborder table" OnUpdateCommand="FpSpread1_Command" ShowHeaderSelection="false">
                                        <Sheets>
                                            <FarPoint:SheetView SheetName="Sheet1">
                                            </FarPoint:SheetView>
                                        </Sheets>
                                    </FarPoint:FpSpread>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </center>
                    <br />
                    <center>
                        <div>
                            <asp:Button ID="btn_save" Text="Save" Visible="false" runat="server" CssClass="textbox btn2"
                                OnClick="btn_save_Click" />
                            <asp:Button ID="btn_update" Text="Update" Visible="false" runat="server" CssClass="textbox btn2"
                                OnClick="btn_update_Click" />
                            <asp:Button ID="btn_reset" Text="Reset" Visible="false" runat="server" CssClass="textbox btn2"
                                OnClick="btn_reset_Click" />
                        </div>
                    </center>
                    <br />
                    <center>
                        <div id="rptprint" runat="server" visible="false">
                            <asp:Label ID="lblvalidation1" runat="server" ForeColor="Red" Text="Please Enter Your Report Name"
                                Visible="false"></asp:Label>
                            <asp:Label ID="lblrptname" runat="server" Text="Report Name"></asp:Label>
                            <asp:TextBox ID="txtexcelname" runat="server" Width="180px" Height="20px" onkeypress="display()"
                                CssClass="textbox textbox1"></asp:TextBox>
                            <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Export To Excel"
                                Width="127px" CssClass="textbox btn1" />
                            <asp:Button ID="btnprintmaster" runat="server" Text="Print" OnClick="btnprintmaster_Click"
                                CssClass="textbox btn1" Width="60px" />
                            <Insproplus:printmaster runat="server" ID="Printcontrol" Visible="false" />
                        </div>
                    </center>
                    <center>
                        <div id="alertmessage" runat="server" visible="false" style="height: 100%; z-index: 1000;
                            width: 100%; background-color: rgba(54, 25, 25, .2); position: absolute; top: 0;
                            left: 0px;">
                            <center>
                                <div id="pnl2" runat="server" class="table" style="background-color: White; height: 120px;
                                    width: 238px; border: 5px solid #0CA6CA; border-top: 25px solid #0CA6CA; margin-top: 200px;
                                    border-radius: 10px;">
                                    <center>
                                        <br />
                                        <table style="height: 100px; width: 100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lbl_alerterror" Visible="false" runat="server" Text="" Style="color: Red;"
                                                        Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <center>
                                                        <asp:Button ID="btn_errorclose" CssClass=" textbox btn1 comm" Style="height: 28px;
                                                            width: 65px;" OnClick="btn_errorclose_Click" Text="OK" runat="server" />
                                                    </center>
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </div>
                            </center>
                        </div>
                    </center>
                </div>
            </div>
        </center>
        </form>
    </body>
    </html>
</asp:Content>
