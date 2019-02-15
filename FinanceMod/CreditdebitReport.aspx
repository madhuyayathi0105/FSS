﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Financemod/FinanceSubSiteMaster.master"
    AutoEventWireup="true" CodeFile="CreditdebitReport.aspx.cs" Inherits="CreditdebitReport" %>

<%@ Register Src="~/Usercontrols/NewPrintMaster.ascx" TagName="NEWPrintMater" TagPrefix="NEW" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <link href="Styles/css/Registration.css" rel="stylesheet" type="text/css" />
    <link href="Styles/css/Commoncss.css" rel="Stylesheet" type="text/css" />
    <body>
        <script type="text/javascript">
            function myFunction(x) {
                x.style.borderColor = "#c4c4c4";
            }
            function display1() {
                document.getElementById('<%=lbl_norec1.ClientID %>').innerHTML = "";
            }
            function display2() {
                document.getElementById('<%=lbl_norec2.ClientID %>').innerHTML = "";
            }
        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <div>
                <center>
                    <div>
                        <span class="fontstyleheader" style="color: Green">Credit/Debit Report</span></div>
                </center>
            </div>
        </center>
        <center>
            <div class="maindivstyle maindivstylesize" style="width: 1000px; height: 121px;">
                <br />
                <center>
                    <table class="maintablestyle" width="800px" height="80px">
                        <tr>
                            <td>
                                <asp:Label ID="lblclg" runat="server" Text="College"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_collegename" runat="server" CssClass="textbox ddlstyle ddlheight3"
                                    OnSelectedIndexChanged="ddl_collegename_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbl_header" runat="server" Text="Header"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="Updp_header" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_header" Width=" 90px" runat="server" CssClass="textbox  txtheight2"
                                            ReadOnly="true">--Select--</asp:TextBox>
                                        <asp:Panel ID="Panel_header" runat="server" CssClass="multxtpanel" Height="200px"
                                            Width="200px">
                                            <asp:CheckBox ID="cb_header" runat="server" Text="Select All" AutoPostBack="True"
                                                OnCheckedChanged="cb_header_CheckedChanged" />
                                            <asp:CheckBoxList ID="cbl_header" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_header_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <asp:PopupControlExtender ID="PopupControlExtender8" runat="server" TargetControlID="txt_header"
                                            PopupControlID="Panel_header" Position="Bottom">
                                        </asp:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Label ID="lbl_ledger" Width="50px" runat="server" Text="Ledger"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="Updp_ledger" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_ledger" Width=" 90px" runat="server" CssClass="textbox  txtheight2"
                                            ReadOnly="true">--Select--</asp:TextBox>
                                        <asp:Panel ID="Panel_ledger" runat="server" CssClass="multxtpanel" Height="200px"
                                            Width="200px">
                                            <asp:CheckBox ID="cb_ledger" runat="server" Text="Select All" AutoPostBack="True"
                                                OnCheckedChanged="cb_ledger_CheckedChanged" />
                                            <asp:CheckBoxList ID="cbl_ledger" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_ledger_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <asp:PopupControlExtender ID="PopupControlExtender9" runat="server" TargetControlID="txt_ledger"
                                            PopupControlID="Panel_ledger" Position="Bottom">
                                        </asp:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:RadioButton ID="rb_date" Width="88px" runat="server" GroupName="same" Text="DateWise"
                                    OnCheckedChanged="rb_date_CheckedChanged" AutoPostBack="true" Checked="true">
                                </asp:RadioButton>
                            </td>
                            <td>
                                <asp:RadioButton ID="rb_month" runat="server" Width="100px" GroupName="same" Text="MonthWise"
                                    OnCheckedChanged="rb_month_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                            </td>
                            <%--Added By saranya on 23/12/2017--%>
                            <td>
                                <asp:RadioButton ID="rb_header" runat="server" Width="100px" GroupName="same" Text="HeaderWise"
                                    OnCheckedChanged="rb_header_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                            </td>
                            <td>
                                <asp:RadioButton ID="rb_ledger" runat="server" Width="100px" GroupName="same" Text="LedgerWise"
                                    OnCheckedChanged="rb_ledger_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                            </td>
                            <%-- --------------------------------%>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_fromdate" runat="server" Width="75px" Text="From Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_fromdate" Width=" 90px" runat="server" CssClass="textbox  txtheight2"
                                    AutoPostBack="true" OnTextChanged="txt_fromdate_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_fromdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1 ajax__calendar_active">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:Label ID="lbl_todate" runat="server" Text="To Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_todate" runat="server" CssClass="textbox  txtheight2" AutoPostBack="true"
                                    OnTextChanged="txt_todate_TextChanged" Width=" 90px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_todate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1 ajax__calendar_active">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                PayMode
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upd_paid" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_paid" runat="server" Style="height: 20px; width: 100px;" ReadOnly="true">--Select--</asp:TextBox>
                                        <asp:Panel ID="pnl_paid" runat="server" CssClass="multxtpanel multxtpanleheight"
                                            Style="width: 126px; height: 160px;">
                                            <asp:CheckBox ID="chk_paid" runat="server" Width="100px" Text="Select All" AutoPostBack="True"
                                                OnCheckedChanged="chk_paid_OnCheckedChanged" />
                                            <asp:CheckBoxList ID="chkl_paid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkl_paid_OnSelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <asp:PopupControlExtender ID="PopupControlExtender6" runat="server" TargetControlID="txt_paid"
                                            PopupControlID="pnl_paid" Position="Bottom">
                                        </asp:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btn_search" runat="server" CssClass="textbox btn2" Text="Search"
                                    OnClick="btnsearch_Click" />
                            </td>
                        </tr>
                        <tr id="trrow" runat="server" visible="false">
                            <td colspan="3">
                                <asp:Label ID="lbl_Staff" runat="server" Width="80px" Text="Staff Name"></asp:Label>
                                <asp:TextBox ID="Txt_Staff" runat="server" Width="150px" CssClass="textbox  txtheight2"
                                    OnTextChanged="Txt_Staff_TextChanged"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="Txt_Staff"
                                    FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" ValidChars=" .">
                                </asp:FilteredTextBoxExtender>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="Getstaff" MinimumPrefixLength="0" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_Staff"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="panelbackground">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lbl_student" runat="server" Width="85px" Text="StudentName"></asp:Label>
                                <asp:TextBox ID="Txt_student" runat="server" Width="180px" CssClass="textbox  txtheight2"
                                    OnTextChanged="Txt_student_TextChanged"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_student"
                                    FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" ValidChars=" .">
                                </asp:FilteredTextBoxExtender>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="Getname" MinimumPrefixLength="0" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_student"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="panelbackground">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lbl_vendore" runat="server" Width="85px" Text="VendorName"></asp:Label>
                                <asp:TextBox ID="Txt_vendor" Width="150px" runat="server" CssClass="textbox  txtheight2"
                                    OnTextChanged="Txt_vendore_TextChanged"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_vendor"
                                    FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" ValidChars=" .">
                                </asp:FilteredTextBoxExtender>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="Getname3" MinimumPrefixLength="0" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_vendor"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="panelbackground">
                                </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <br />
            <center>
                <%-- <div id="yeardiv" runat="server">--%>
                <%--     <div id="div1" visible="false" runat="server" class="spreadborder" style="width: 918px;
                    height: 345px; overflow: auto; background-color: White; border-radius: 10px;">
                --%>
                <div id="div1" visible="false" runat="server" style="background-color: White; overflow: auto;
                    height: 250px; width: 880px; border: 5px; border-radius: 10px;">
                    <br />
                    <asp:HiddenField ID="SelectedGridCellIndex" runat="server" Value="-1" />
                    <asp:GridView ID="grdCreditReport" Width="850px" runat="server" ShowFooter="false"
                        AutoGenerateColumns="true" Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false"
                        OnRowDataBound="grdCreditReport_RowDataBound" OnRowCreated="grdCreditReport_OnRowCreated"
                        OnSelectedIndexChanged="grdCreditReport_SelectedIndexChanged">
                        <%-- --%>
                        <HeaderStyle BackColor="#0CA6CA" ForeColor="Black" />
                    </asp:GridView>
                </div>
                <br />
                <div id="divCellClickDate" visible="false" runat="server" style="background-color: White;
                    overflow: auto; height: 250px; width: 900px; border: 5px; border-radius: 10px;">
                    <asp:GridView ID="GrdDateWise" Width="850px" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                        Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false" OnRowDataBound="GrdDateWise_RowDataBound">
                    </asp:GridView>
                </div>
            </center>
            <br />
            <div id="monthdiv" visible="false" runat="server" style="background-color: White;
                height: 250px; width: 900px; border: 5px; border-radius: 10px;" class="spreadborder">
                <br />
                <div id="maindiv" runat="server" visible="false">
                    <table class="maintablestyle" width="600px" height="80px">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_header1" runat="server" Width="60px" Text="Header"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="Updp_header1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_header1" runat="server" CssClass="textbox  txtheight2" ReadOnly="true">--Select--</asp:TextBox>
                                        <asp:Panel ID="Panel_header1" runat="server" CssClass="multxtpanel" Height="200px"
                                            Width="200px">
                                            <asp:CheckBox ID="cb_header1" runat="server" Text="Select All" AutoPostBack="True"
                                                OnCheckedChanged="cb_header1_CheckedChanged" />
                                            <asp:CheckBoxList ID="cbl_header1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_header1_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <asp:PopupControlExtender ID="PopupControlExtender20" runat="server" TargetControlID="txt_header1"
                                            PopupControlID="Panel_header1" Position="Bottom">
                                        </asp:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Label ID="lbl_ledger1" runat="server" Text="Ledger"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="Updp_ledger1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_ledger1" runat="server" CssClass="textbox  txtheight2" ReadOnly="true">--Select--</asp:TextBox>
                                        <asp:Panel ID="Panel_ledger1" runat="server" CssClass="multxtpanel" Height="200px"
                                            Width="200px">
                                            <asp:CheckBox ID="cb_ledger1" runat="server" Text="Select All" AutoPostBack="True"
                                                OnCheckedChanged="cb_ledger1_CheckedChanged" />
                                            <asp:CheckBoxList ID="cbl_ledger1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_ledger1_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <asp:PopupControlExtender ID="PopupControlExtender21" runat="server" TargetControlID="txt_ledger1"
                                            PopupControlID="Panel_ledger1" Position="Bottom">
                                        </asp:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Label ID="lbl_fromdate1" runat="server" Width="75px" Text="From Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_fromdate1" runat="server" CssClass="textbox  txtheight2" AutoPostBack="true"
                                    OnTextChanged="txt_fromdate1_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txt_fromdate1" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1 ajax__calendar_active">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:Label ID="lbl_todate1" runat="server" Text="To Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_todate1" runat="server" CssClass="textbox  txtheight2" AutoPostBack="true"
                                    OnTextChanged="txt_todate1_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txt_todate1" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1 ajax__calendar_active">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_Staff1" runat="server" Width="85px" Text="Staff Name"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="Txt_Staff1" runat="server" Width="160px" CssClass="textbox  txtheight2"></asp:TextBox>
                                <%--    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="Txt_Staff"
                                                FilterType="uppercaseletters,lowercaseletters,custom" ValidChars=" .">
                                            </asp:FilteredTextBoxExtender> --%>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="Getstaff" MinimumPrefixLength="0" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_Staff1"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="panelbackground">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:Label ID="lbl_student1" runat="server" Width="85px" Text="StudentName"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="Txt_student1" runat="server" Width="160px" CssClass="textbox  txtheight2"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender15" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="Getname" MinimumPrefixLength="0" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_student1"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="panelbackground">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:Label ID="lbl_vendor1" runat="server" Width="85px" Text="VendorName"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="Txt_vendor1" Width="150px" runat="server" CssClass="textbox  txtheight2"
                                    OnTextChanged="Txt_vendore_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="Getname3" MinimumPrefixLength="0" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_vendor1"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="panelbackground">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:Button ID="btn_search1" runat="server" CssClass="textbox btn2" Text="Search"
                                    OnClick="btnsearch2_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="HiddenFieldMonth" runat="server" Value="-1" />
                <asp:GridView ID="GrdMonth" Width="900px" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                    Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false" OnRowDataBound="GrdMonth_RowDataBound"
                    OnRowCreated="GrdMonth_OnRowCreated" OnSelectedIndexChanged="GrdMonth_SelectedIndexChanged">
                    <HeaderStyle BackColor="#0CA6CA" ForeColor="Black" />
                </asp:GridView>
            </div>
            <br />
            <div id="Div2" visible="false" runat="server" style="background-color: White; height: 150px;
                width: 880px; border: 5px; border-radius: 10px;" class="spreadborder">
                <asp:HiddenField ID="HiddenFieldMonth2" runat="server" Value="-1" />
                <asp:GridView ID="GrdMonthCellClick" Width="900px" runat="server" ShowFooter="false"
                    AutoGenerateColumns="true" Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false"
                    OnRowDataBound="GrdMonthCellClick_RowDataBound" OnRowCreated="GrdMonthCellClick_OnRowCreated"
                    OnSelectedIndexChanged="GrdMonthCellClick_SelectedIndexChanged">
                    <HeaderStyle BackColor="#0CA6CA" ForeColor="Black" />
                </asp:GridView>
            </div>
            <br />
            <%----------------------------------------Added by saranya on 23/12/2017----------------------------------------%>
            <div id="Div3" visible="false" runat="server" style="background-color: White; width: 880px;
                height: 500px; border: 5px; margin-top: -60px; border-radius: 10px;" class="spreadborder">
                <asp:GridView ID="GrdHeaderWise" Width="900px" runat="server" ShowFooter="false"
                    AutoGenerateColumns="true" Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false"
                    OnRowDataBound="GrdHeaderWise_RowDataBound">
                    <HeaderStyle BackColor="#0CA6CA" ForeColor="Black" />
                </asp:GridView>
            </div>
            <br />
            <div id="Div4" visible="false" runat="server" style="background-color: White; width: 880px;
                height: 500px; border: 100px; margin-top: -60px; border-radius: 10px;" class="spreadborder">                
                 <asp:GridView ID="GrdLedger" Width="900px" runat="server" ShowFooter="false"
                    AutoGenerateColumns="true" Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false"
                    OnRowDataBound="GrdLedger_RowDataBound">
                    <HeaderStyle BackColor="#0CA6CA" ForeColor="Black" />
                </asp:GridView>
            </div>
            <br />
            <%-----------------------------------------------------------------------------------------------------------------%>            
            <div id="divMonth3" visible="false" runat="server" style="background-color: White;
                overflow: auto; height: 250px; width: 900px; border: 5px; border-radius: 10px;">
                <asp:GridView ID="GrdMonth3CellClick" Width="900px" runat="server" ShowFooter="false"
                    AutoGenerateColumns="true" Font-Names="Book Antiqua" ShowHeader="false" toGenerateColumns="false"
                    OnRowDataBound="GrdMonth3CellClick_RowDataBound">
                    <HeaderStyle BackColor="#0CA6CA" ForeColor="Black" />
                </asp:GridView>
            </div>
            <%-- </div>--%>
            <br />
            <div>
                <div id="rptprint1" runat="server" visible="false">
                    <asp:Label ID="lbl_norec1" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" ForeColor="Red" Text="Please Enter Your Report Name" Visible="false"></asp:Label>
                    <asp:Label ID="lblrptname1" runat="server" Font-Names="Book Antiqua" Font-Size="Medium"
                        Text="Report Name"></asp:Label>
                    <asp:TextBox ID="txtexcelname1" runat="server" CssClass="textbox textbox1" Height="20px"
                        Width="180px" Style="font-family: 'Book Antiqua'" Font-Bold="True" Font-Names="Book Antiqua"
                        onkeypress="display1()" Font-Size="Medium"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtexcelname1"
                        FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="(),.[]_"
                        InvalidChars="/\">
                    </asp:FilteredTextBoxExtender>
                    <asp:Button ID="btnExcel1" runat="server" Font-Names="Book Antiqua" OnClick="btnExcel1_Click"
                        Font-Size="Medium" Text="Export To Excel" Width="127px" Height="35px" CssClass="textbox textbox1" />
                    <asp:Button ID="btnprintmaster1" runat="server" Text="Print" OnClick="btnprintmaster1_Click"
                        Font-Names="Book Antiqua" Font-Size="Medium" Width="60px" Height="35px" CssClass="textbox textbox1" />
                    <NEW:NEWPrintMater runat="server" ID="Printcontrol1" Visible="false" />
                </div>
            </div>
            <div id="popwindow1" runat="server" visible="false" class="popupstyle popupheight1">
                <asp:ImageButton ID="ImageButton1" runat="server" Width="40px" Height="40px" ImageUrl="~/images/close.png"
                    Style="height: 30px; width: 30px; position: absolute; margin-top: 100px; margin-left: 472px;"
                    OnClick="imagebtnpopclose2_Click" />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <div id="monthdate1" visible="false" runat="server" style="background-color: White;
                    height: 536px; width: 974px; border: 5px solid #0CA6CA; border-top: 30px solid #0CA6CA;
                    border-radius: 10px;" class="spreadborder">
                    <br />
                    <div id="maindiv2" runat="server" visible="false">
                        <table class="maintablestyle" width="600px" height="80px">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_header2" runat="server" Width="72px" Text="Header"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="Updp_header2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_header2" runat="server" CssClass="textbox  txtheight2" ReadOnly="true">--Select--</asp:TextBox>
                                            <asp:Panel ID="Panel_header2" runat="server" CssClass="multxtpanel" Height="200px"
                                                Width="200px">
                                                <asp:CheckBox ID="cb_header2" runat="server" Text="Select All" AutoPostBack="True"
                                                    OnCheckedChanged="cb_header2_CheckedChanged" />
                                                <asp:CheckBoxList ID="cbl_header2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_header2_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="PopupControlExtender30" runat="server" TargetControlID="txt_header2"
                                                PopupControlID="Panel_header2" Position="Bottom">
                                            </asp:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_ledger2" runat="server" Text="Ledger"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="Updp_ledger2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txt_ledger2" runat="server" CssClass="textbox  txtheight2" ReadOnly="true">--Select--</asp:TextBox>
                                            <asp:Panel ID="Panel_ledger2" runat="server" CssClass="multxtpanel" Height="200px"
                                                Width="200px">
                                                <asp:CheckBox ID="cb_ledger2" runat="server" Text="Select All" AutoPostBack="True"
                                                    OnCheckedChanged="cb_ledger2_CheckedChanged" />
                                                <asp:CheckBoxList ID="cbl_ledger2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_ledger2_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="PopupControlExtender31" runat="server" TargetControlID="txt_ledger2"
                                                PopupControlID="Panel_ledger2" Position="Bottom">
                                            </asp:PopupControlExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_fromdate2" runat="server" Width="75px" Text="From Date"></asp:Label>
                                    <asp:TextBox ID="txt_fromdate2" runat="server" CssClass="textbox  txtheight2" AutoPostBack="true"
                                        OnTextChanged="txt_fromdate2_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender6" TargetControlID="txt_fromdate2" runat="server"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme1 ajax__calendar_active">
                                    </asp:CalendarExtender>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lbl_todate2" runat="server" Text="To Date"></asp:Label>
                                    <asp:TextBox ID="txt_todate2" runat="server" CssClass="textbox  txtheight2" AutoPostBack="true"
                                        OnTextChanged="txt_todate2_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender7" TargetControlID="txt_todate2" runat="server"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme1 ajax__calendar_active">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Staff2" runat="server" Width="85px" Text="Staff Name"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="Txt_Staff2" runat="server" Width="170px" CssClass="textbox  txtheight2"></asp:TextBox>
                                    <%--    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="Txt_Staff"
                                                FilterType="uppercaseletters,lowercaseletters,custom" ValidChars=" .">
                                            </asp:FilteredTextBoxExtender>
                                    --%>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                        Enabled="True" ServiceMethod="Getstaff" MinimumPrefixLength="0" CompletionInterval="100"
                                        EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_Staff2"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="panelbackground">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_student2" runat="server" Width="85px" Text="StudentName"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="Txt_student2" runat="server" Width="170px" CssClass="textbox  txtheight2"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                        Enabled="True" ServiceMethod="Getname" MinimumPrefixLength="0" CompletionInterval="100"
                                        EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_student2"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="panelbackground">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_vendor2" runat="server" Width="85px" Text="VendorName"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="Txt_vendor2" Width="165px" runat="server" CssClass="textbox  txtheight2"
                                        OnTextChanged="Txt_vendore_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters=""
                                        Enabled="True" ServiceMethod="Getname3" MinimumPrefixLength="0" CompletionInterval="100"
                                        EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="Txt_vendor2"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="panelbackground">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btn_search2" runat="server" CssClass="textbox btn2" Text="Search"
                                        OnClick="btnsearch3_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                    <div id="rptprint2" runat="server" visible="false">
                        <asp:Label ID="lbl_norec2" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="Red" Text="Please Enter Your Report Name" Visible="false"></asp:Label>
                        <asp:Label ID="lblrptname2" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Report Name"></asp:Label>
                        <asp:TextBox ID="txtexcelname2" runat="server" CssClass="textbox textbox1" Height="20px"
                            Width="180px" Style="font-family: 'Book Antiqua'" Font-Bold="True" Font-Names="Book Antiqua"
                            onkeypress="display2()" Font-Size="Medium"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtexcelname1"
                            FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="(),.[]_"
                            InvalidChars="/\">
                        </asp:FilteredTextBoxExtender>
                        <asp:Button ID="btnExcel2" runat="server" Font-Bold="true" Font-Names="Book Antiqua"
                            OnClick="btnExcel2_Click" Font-Size="Medium" Text="Export To Excel" Width="127px"
                            Height="35px" CssClass="textbox textbox1" />
                        <asp:Button ID="btnprintmaster2" runat="server" Text="Print" OnClick="btnprintmaster2_Click"
                            Font-Names="Book Antiqua" Font-Size="Medium" Font-Bold="true" Width="60px" Height="35px"
                            CssClass="textbox textbox1" />
                        <Insproplus:printmaster runat="server" ID="Printcontrol2" Visible="false" />
                    </div>
                    <br />
                    <br />
                </div>
            </div>
            <%--******* date ********--%>
            <center>
                <div id="imgdiv2" runat="server" visible="false" style="height: 100%; z-index: 1000;
                    width: 100%; background-color: rgba(54, 25, 25, .2); position: absolute; top: 0;
                    left: 0px;">
                    <center>
                        <div id="pnl2" runat="server" class="table" style="background-color: White; height: 120px;
                            width: 238px; border: 5px solid #0CA6CA; border-top: 25px solid #0CA6CA; margin-top: 200px;
                            border-radius: 10px;">
                            <center>
                                <table style="height: 100px; width: 100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_alert1" runat="server" Text="" Style="color: Red;" Font-Bold="true"
                                                Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <center>
                                                <asp:Button ID="btn_errorclose" CssClass=" textbox btn1 comm" Style="height: 28px;
                                                    width: 65px;" OnClick="btn_errorclose_Click" Text="ok" runat="server" />
                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </center>
                </div>
            </center>
            <%-- ******** date **********--%>
            </div>
        </center>
    </body>
    </html>
</asp:Content>
