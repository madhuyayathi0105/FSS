﻿<%@ Page Title="" Language="C#" MasterPageFile="~/HRMOD/HRSubSiteMaster.master" AutoEventWireup="true"
    CodeFile="SalaryBill.aspx.cs" Inherits="HRMOD_SalaryBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="~/Styles/css/Commoncss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function display() {
            document.getElementById('<%=lblerror.ClientID %>').innerHTML = "";
        }
        function display1() {
            document.getElementById('<%=Label3.ClientID %>').innerHTML = "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <center>
                <div>
                    <span class="fontstyleheader" style="color: Green;">Salary Comparative Report</span>
                </div>
                <br />
            </center>
            <table class="maintablestyle">
                <tr>
                    <td>
                        <asp:Label ID="lbl_college" runat="server" Text="College Name" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtclg" runat="server" Style="width: 190px;" ReadOnly="true" CssClass="textbox textbox1">--Select--</asp:TextBox>
                                <asp:Panel ID="pnlclg" runat="server" CssClass="multxtpanel multxtpanleheight" Style="width: 350px;
                                    height: 200px;">
                                    <asp:CheckBox ID="cbclg" runat="server" Width="100px" Text="Select All" AutoPostBack="True"
                                        OnCheckedChanged="cbclg_OnCheckedChanged" />
                                    <asp:CheckBoxList ID="cblclg" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblclg_OnSelectedIndexChanged">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender11" runat="server" TargetControlID="txtclg"
                                    PopupControlID="pnlclg" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Label ID="lbl_dept" runat="server" Text="Department"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_dept" runat="server" ReadOnly="true" CssClass="textbox textbox1 txtheight1"
                                    Style="width: 120px;">--Select--</asp:TextBox>
                                <asp:Panel ID="p1" runat="server" CssClass="multxtpanel" Height="200px">
                                    <asp:CheckBox ID="cb_dept" runat="server" Text="Select All" OnCheckedChanged="cb_dept_CheckedChange"
                                        AutoPostBack="true" />
                                    <asp:CheckBoxList ID="cbl_dept" runat="server" OnSelectedIndexChanged="cbl_dept_SelectedIndexChange"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txt_dept"
                                    PopupControlID="p1" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Label ID="lbl_desig" runat="server" Text="Designation"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_desig" runat="server" ReadOnly="true" CssClass="textbox textbox1 txtheight1"
                                    Style="width: 120px;">--Select--</asp:TextBox>
                                <asp:Panel ID="P2" runat="server" CssClass="multxtpanel" Height="200px">
                                    <asp:CheckBox ID="cb_desig" runat="server" Text="Select All" OnCheckedChanged="cb_desig_CheckedChange"
                                        AutoPostBack="true" />
                                    <asp:CheckBoxList ID="cbl_desig" runat="server" OnSelectedIndexChanged="cbl_desig_SelectedIndexChange"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txt_desig"
                                    PopupControlID="P2" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_staffc" runat="server" Text="Staff Category"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_staffc" runat="server" ReadOnly="true" CssClass="textbox textbox1 txtheight1"
                                    Style="width: 190px;">--Select--</asp:TextBox>
                                <asp:Panel ID="P3" runat="server" CssClass="multxtpanel" Height="200px" Width="196px">
                                    <asp:CheckBox ID="cb_staffc" runat="server" Text="Select All" OnCheckedChanged="cb_staffc_CheckedChange"
                                        AutoPostBack="true" />
                                    <asp:CheckBoxList ID="cbl_staffc" runat="server" OnSelectedIndexChanged="cbl_staffc_SelectedIndexChange"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txt_staffc"
                                    PopupControlID="P3" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Label ID="lbl_stype" runat="server" Text="Staff Type"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_stype" runat="server" ReadOnly="true" CssClass="textbox textbox1 txtheight1"
                                    Style="width: 120px;">--Select--</asp:TextBox>
                                <asp:Panel ID="P4" runat="server" CssClass="multxtpanel" Height="200px">
                                    <asp:CheckBox ID="cb_stype" runat="server" Text="Select All" OnCheckedChanged="cb_stype_CheckedChange"
                                        AutoPostBack="true" />
                                    <asp:CheckBoxList ID="cbl_stype" runat="server" OnSelectedIndexChanged="cbl_stype_SelectedIndexChange"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="txt_stype"
                                    PopupControlID="P4" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        Year
                        <asp:DropDownList ID="ddl_year" CssClass="textbox1 ddlheight" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td colspan='2'>
                        Month
                        <asp:DropDownList ID="ddl_month" CssClass="textbox1 ddlheight" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="display: none;">
                        <asp:Label ID="lbl_stat" runat="server" Text="Staff Status" Style="font-family: book antiqua;
                            font-size: medium;"></asp:Label>
                    </td>
                    <td style="display: none;">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_stat" runat="server" ReadOnly="true" CssClass="textbox textbox1 txtheight1"
                                    Style="width: 120px;">--Select--</asp:TextBox>
                                <asp:Panel ID="P5" runat="server" CssClass="multxtpanel" Height="200px">
                                    <asp:CheckBox ID="cb_stat" runat="server" Text="Select All" OnCheckedChanged="cb_stat_CheckedChange"
                                        AutoPostBack="true" />
                                    <asp:CheckBoxList ID="cbl_stat" runat="server" OnSelectedIndexChanged="cbl_stat_SelectedIndexChange"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txt_stat"
                                    PopupControlID="P5" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_sname" runat="server" Text="Staff Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_sname" runat="server" CssClass="textbox textbox1" MaxLength="50"
                            placeholder="Search Staff Name" Style="width: 190px;"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                            Enabled="True" ServiceMethod="GetStaffName" MinimumPrefixLength="0" CompletionInterval="100"
                            EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="txt_sname"
                            CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            CompletionListItemCssClass="txtsearchpan">
                        </asp:AutoCompleteExtender>
                    </td>
                    <td>
                        <asp:Label ID="lbl_scode" runat="server" Text="Staff Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_scode" runat="server" CssClass="textbox textbox1" MaxLength="10"
                            Style="width: 120px;" placeholder="Search Staff Code"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters=""
                            Enabled="True" ServiceMethod="GetStaffCode" MinimumPrefixLength="0" CompletionInterval="100"
                            EnableCaching="false" CompletionSetCount="10" ServicePath="" TargetControlID="txt_scode"
                            CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            CompletionListItemCssClass="txtsearchpan">
                        </asp:AutoCompleteExtender>
                    </td>
                    <td>
                        <asp:Button ID="btn_go" runat="server" Text="Go" OnClick="btn_go_Click" CssClass="textbox textbox1 btn1" />
                    </td>
                </tr>
            </table>
            <br />
            <center>
                <asp:Label ID="lbl_alert" runat="server" Visible="false" Style="color: red;"></asp:Label>
            </center>
            <div id="sp_div" runat="server" visible="false">
                <FarPoint:FpSpread ID="FpSpread" runat="server" BorderColor="Black" BorderStyle="Solid"
                    BorderWidth="1px" Width="980px" Style="margin-left: 2px;" class="spreadborder"
                    ShowHeaderSelection="false">
                    <Sheets>
                        <FarPoint:SheetView SheetName="Sheet1">
                        </FarPoint:SheetView>
                    </Sheets>
                </FarPoint:FpSpread>
                <br />
                <asp:Label ID="lblerror" Text="Please Enter Your Report Name" Visible="false" ForeColor="Red"
                    runat="server"></asp:Label>
                <asp:Label ID="lblexcel" runat="server" Text="Report Name"></asp:Label>
                <asp:TextBox ID="txtexcel" onkeypress="display()" CssClass="textbox textbox1" runat="server"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server" TargetControlID="txtexcel"
                    FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="!@$%^&()_+}{][';,.">
                </asp:FilteredTextBoxExtender>
                <asp:Button ID="btnexcel" runat="server" CssClass="textbox textbox1 btn2" Text="Export Excel"
                    OnClick="btnexcel_Click" />
                <asp:Button ID="btnprintmaster" runat="server" Text="Print" OnClick="btnprintmaster_Click"
                    CssClass="textbox textbox1 btn1" />
                <Insproplus:printmaster runat="server" ID="Printcontrol" Visible="false" />
            </div>
        </center>
        <br />
        <div id="Deduction" runat="server" visible="false">
            <FarPoint:FpSpread ID="DeductionDetSp" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Width="980px" Style="margin-left: 2px;" class="spreadborder"
                ShowHeaderSelection="false">
                <Sheets>
                    <FarPoint:SheetView SheetName="Sheet1">
                    </FarPoint:SheetView>
                </Sheets>
            </FarPoint:FpSpread>
            <br />
            <center>
                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Please Enter Your Report Name"
                    Visible="false"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Report Name"></asp:Label>
                <asp:TextBox ID="txtexcel1" CssClass="textbox textbox1" runat="server" onkeypress="display1()"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtexcel1"
                    FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="!@$%^&()_+}{][';,. ">
                </asp:FilteredTextBoxExtender>
                <asp:Button ID="Button1" runat="server" CssClass="textbox btn2" Text="Export Excel"
                    OnClick="btnExcel_Click1" />
                <asp:Button ID="Button2" runat="server" Text="Print" CssClass="textbox btn1" OnClick="btnprintmaster_Click1" />
                <Insproplus:printmaster runat="server" ID="Printmaster1" Visible="false" />
            </center>
        </div>
        <br />
        </center>
    </div>
</asp:Content>
