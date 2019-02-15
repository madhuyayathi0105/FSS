<%@ Page Title="" Language="C#" MasterPageFile="~/AttendanceMOD/AttendanceSubSiteMaster.master"
    AutoEventWireup="true" CodeFile="Periodwiseattendancereport.aspx.cs" Inherits="Periodwiseattendancereport"
    EnableEventValidation="false" %>

<%@ Register Src="~/Usercontrols/PrintMaster.ascx" TagName="printmaster" TagPrefix="Insproplus" %>
<%@ Register Src="~/Usercontrols/NewPrintMaster.ascx" TagName="NEWPrintMater" TagPrefix="NEW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <link href="~/Styles/css/Registration.css" rel="stylesheet" type="text/css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link href="~/Styles/css/Commoncss.css" rel="Stylesheet" type="text/css" />
    <body>
        <script type="text/javascript">
            function display() {
                document.getElementById('MainContent_errmsg').innerHTML = "";
            }
            function PrintPanel() {
                var panel = document.getElementById("<%=divMainContents.ClientID %>");
                var printWindow = window.open('', '', 'height=842,width=1191');
                printWindow.document.write('<html');
                printWindow.document.write('<head>');
                printWindow.document.write('</head><body >');
                printWindow.document.write('<form>');
                printWindow.document.write(panel.innerHTML);
                printWindow.document.write(' </form>');
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                setTimeout(function () {
                    printWindow.print();
                }, 500);
                return false;
            }
        </script>
        <style type="text/css">
            .cpHeader
            {
                color: white;
                background-color: #719DDB;
                font-size: 12px;
                cursor: pointer;
                padding: 1px;
                font-style: normal;
                font-variant: normal;
                font-weight: bold;
                line-height: normal;
                font-family: "auto Trebuchet MS" , Verdana;
            }
            .cpBody
            {
                background-color: #DCE4F9;
                font: normal 11px auto Verdana, Arial;
                border: 1px gray;
                padding-top: 7px;
                padding-left: 4px;
                padding-right: 4px;
                padding-bottom: 4px;
            }
            
            .cpimage
            {
                float: right;
                vertical-align: middle;
                background-color: transparent;
            }
            
            
            .printclass
            {
                display: none;
            }
            .marginSet
            {
                margin: 0px;
                padding: 0px;
            }
            .headerDisp
            {
                font-size: 25px;
                font-weight: bold;
            }
            .headerDisp1
            {
                font-family: Book Antiqua;
                font-size: medium;
            }
            @media print
            {
                #divMainContents
                {
                    display: block;
                }
                .printclass
                {
                    display: block;
                    font-family: Book Antiqua;
                }
                .noprint
                {
                    display: none;
                }
            }
            @media screen,print
            {
            
            }
            @page
            {
                size: A4;
            }
        </style>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <center>
            <span class="fontstyleheader" style="color: Green;">Absentees Report</span>
        </center>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel2" runat="server" Height="95px" BackColor="#0CA6CA" BorderColor="Black"
                    BorderStyle="None" ClientIDMode="Static" Style="border: 1px solid #000; margin-bottom: 0;
                    margin-left: 0px; margin-top: 0px; width: 100%;">
                    <asp:Label ID="lblbatch" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" Text="Batch" Style="top: 150px; left: 35px; position: absolute;"></asp:Label>
                    <asp:TextBox ID="txt_batch" runat="server" Font-Bold="True" ReadOnly="true" CssClass="Dropdown_Txt_Box"
                        Font-Names="Book Antiqua" Font-Size="Medium" Text="--Select--" Style="top: 148px;
                        left: 87px; position: absolute; height: 20px; width: 100px; margin-right: 15px;">---Select---</asp:TextBox>
                    <asp:Panel ID="pbat" runat="server" CssClass="multxtpanel" Height="200" Width="125">
                        <asp:CheckBox ID="chk_batch" runat="server" Text="SelectAll" AutoPostBack="true"
                            OnCheckedChanged="chk_batch_ChekedChanged" Font-Bold="True" ForeColor="Black"
                            Font-Names="Book Antiqua" Font-Size="Medium" />
                        <asp:CheckBoxList ID="chklst_batch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chklst_batch_SelectedIndexChanged"
                            Font-Bold="True" Font-Size="Medium" ForeColor="Black" Font-Names="Book Antiqua"
                            Width="62px" Height="37px">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txt_batch"
                        PopupControlID="pbat" Position="Bottom">
                    </asp:PopupControlExtender>
                    <asp:Label ID="lbldeg" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" Text="Degree" Style="font-family: Book Antiqua; font-size: medium;
                        font-weight: bold; left: 196px; position: absolute; top: 150px;"></asp:Label>
                    <asp:TextBox ID="txt_degree" runat="server" CssClass="Dropdown_Txt_Box" ReadOnly="true"
                        Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium" Style="top: 148px;
                        left: 289px; position: absolute; height: 20px; width: 100px; margin-right: 15px;">---Select---</asp:TextBox>
                    <asp:Panel ID="Pdeg" runat="server" CssClass="multxtpanel" Height="200" Width="125">
                        <asp:CheckBox ID="chk_degree" runat="server" Text="SelectAll" AutoPostBack="true"
                            OnCheckedChanged="chk_degree_ChekedChanged" Font-Bold="True" ForeColor="Black"
                            Font-Names="Book Antiqua" Font-Size="Medium" />
                        <asp:CheckBoxList ID="chklst_degree" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chklst_degree_SelectedIndexChanged"
                            Font-Bold="True" ForeColor="Black" Font-Names="Book Antiqua" Font-Size="Medium"
                            Width="98px">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txt_degree"
                        PopupControlID="Pdeg" Position="Bottom">
                    </asp:PopupControlExtender>
                    <asp:Label ID="lblbranch" runat="server" Width="90px" Font-Bold="True" ForeColor="Black"
                        Font-Names="Book Antiqua" Font-Size="Medium" Style="display: inline-block; color: Black;
                        font-family: Book Antiqua; font-size: medium; font-weight: bold; width: 95px;
                        left: 400px; position: absolute; top: 150px;"></asp:Label>
                    <asp:TextBox ID="txt_branch" runat="server" Height="20px" CssClass="Dropdown_Txt_Box"
                        ReadOnly="true" Width="180px" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium"
                        Style="font-size: medium; font-weight: bold; height: 20px; width: 180px; font-family: 'Book Antiqua';
                        top: 148px; position: absolute; left: 492px;">---Select---</asp:TextBox>
                    <asp:Panel ID="pbranch" runat="server" Width="400px" CssClass="multxtpanel" Height="250px">
                        <asp:CheckBox ID="chk_branch" runat="server" Font-Bold="True" Width="150px" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Select All" AutoPostBack="True" OnCheckedChanged="chk_branch_ChekedChanged" />
                        <asp:CheckBoxList ID="chklst_branch" runat="server" Font-Size="Medium" AutoPostBack="True"
                            OnSelectedIndexChanged="chklst_branch_SelectedIndexChanged" Style="font-family: 'Book Antiqua'"
                            Font-Bold="True" Font-Names="Book Antiqua" Height="58px">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txt_branch"
                        PopupControlID="pbranch" Position="Bottom">
                    </asp:PopupControlExtender>
                    <asp:Label ID="lblSec" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" ForeColor="White" Text="Sec" Style="display: inline-block;
                        color: Black; font-family: Book Antiqua; font-size: medium; font-weight: bold;
                        width: 90px; left: 706px; position: absolute; top: 150px;">
                    </asp:Label>
                    <asp:TextBox ID="txtsection" runat="server" CssClass="Dropdown_Txt_Box" Font-Size="Medium"
                        Height="20px" Font-Bold="true" Font-Names="Book Antiqua" ReadOnly="true" Width="100px"
                        Style="top: 148px; position: absolute; left: 742px;">---Select---</asp:TextBox>
                    <asp:Panel ID="psection" runat="server" Height="200px" CssClass="multxtpanel" Width="120px">
                        <asp:CheckBox ID="chksection" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Select All" OnCheckedChanged="chksection_CheckedChanged"
                            AutoPostBack="True" />
                        <asp:CheckBoxList ID="chklstsection" runat="server" Font-Size="Medium" AutoPostBack="True"
                            Style="font-family: 'Book Antiqua'" OnSelectedIndexChanged="chklstsection_SelectedIndexChanged"
                            Font-Bold="True" Font-Names="Book Antiqua">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <asp:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txtsection"
                        PopupControlID="psection" Position="Bottom">
                    </asp:PopupControlExtender>
                    <asp:Label ID="lblFromdate" runat="server" Text="From Date" Font-Bold="True" Font-Names="Book Antiqua"
                        Width="80px" Font-Size="Medium" Style="display: inline-block; color: Black; width: 90px;
                        left: 35px; position: absolute; top: 193px;"></asp:Label>
                    <asp:TextBox ID="txtFromDate" CssClass="txt" runat="server" OnTextChanged="txtFromDate_TextChanged"
                        Height="26px" Width="74px" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium"
                        AutoPostBack="True" Style="font-size: medium; font-weight: bold; top: 188px;
                        position: absolute; left: 125px;"></asp:TextBox>
                    <asp:CalendarExtender ID="calfromdate" TargetControlID="txtFromDate" Format="d/MM/yyyy"
                        runat="server">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label3" runat="server" Text="Period" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" Style="color: Black; width: 155px; left: 224px; position: absolute;
                        top: 193px;">        </asp:Label>
                    <asp:DropDownList ID="ddlhour" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" AutoPostBack="true" OnSelectedIndexChanged="ddlhour_SelectedIndexChanged"
                        Style="color: Black; width: 80px; left: 289px; position: absolute; top: 191px;">
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkalldept" runat="server" Checked="false" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" Style="color: Black; left: 380px; position: absolute; top: 191px;" />
                    <asp:CheckBox ID="chkconso" runat="server" Checked="false" Text="Consolidate" Font-Bold="True"
                        Font-Names="Book Antiqua" Font-Size="Medium" Style="color: Black; left: 580px;
                        position: absolute; top: 191px;" />
                    <asp:UpdatePanel ID="UpdatePanel_go" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnGo" runat="server" Text="Go" Width="41px" Font-Bold="True" Font-Names="Book Antiqua"
                                Font-Size="Medium" Style="color: Black; left: 780px; position: absolute; top: 191px;"
                                OnClientClick="return txttbbat()" OnClick="btnGo_Click"></asp:Button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <br />
                <asp:Panel ID="pheaderfilter" runat="server" CssClass="cpHeader" BackColor="#719DDB"
                    Width="959px">
                    <asp:Label ID="Labelfilter" Text="Column Order" runat="server" Font-Size="Medium"
                        Font-Bold="True" Font-Names="Book Antiqua" />
                    <asp:Image ID="Imagefilter" runat="server" CssClass="cpimage" ImageUrl="../images/right.jpeg"
                        ImageAlign="Right" />
                </asp:Panel>
                <asp:Panel ID="pbodyfilter" runat="server" CssClass="cpBody" Width="952px">
                    <asp:CheckBox ID="Cbcolumn" runat="server" AutoPostBack="true" Font-Bold="True" Width="164px"
                        Font-Names="Book Antiqua" Font-Size="Medium" OnCheckedChanged="Cbcolumn_CheckedChanged"
                        Text="Select All" />
                    <asp:CheckBoxList ID="cblsearch" runat="server" Height="43px" Width="850px" AutoPostBack="true"
                        Style="font-family: 'Book Antiqua'; font-weight: 700; font-size: medium;" RepeatColumns="5"
                        RepeatDirection="Horizontal" OnSelectedIndexChanged="cblsearch_SelectedIndexChanged">
                        <asp:ListItem Text="S.No"></asp:ListItem>
                        <asp:ListItem Text="Degree Details"></asp:ListItem>
                        <asp:ListItem Text="Admission No"></asp:ListItem>
                        <asp:ListItem Text="Roll No"></asp:ListItem>
                        <asp:ListItem Text="Register No"></asp:ListItem>
                        <asp:ListItem Text="Student Name"></asp:ListItem>
                        <asp:ListItem Text="Student Type"></asp:ListItem>
                        <asp:ListItem Text="Gender"></asp:ListItem>
                        <asp:ListItem Text="Father Name"></asp:ListItem>
                        <asp:ListItem Text="Mobile Number"></asp:ListItem>
                        <asp:ListItem Text="Date"></asp:ListItem>
                        <asp:ListItem Text="Actual Class Strength"></asp:ListItem>
                        <asp:ListItem Text="Total Strength"></asp:ListItem>
                        <asp:ListItem Text="No of Student Present"></asp:ListItem>
                        <asp:ListItem Text="No of Student Absent"></asp:ListItem>
                        <asp:ListItem Text="Remarks"></asp:ListItem>
                    </asp:CheckBoxList>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpecolumnorder" runat="server" TargetControlID="pbodyfilter"
                    CollapseControlID="pheaderfilter" ExpandControlID="pheaderfilter" Collapsed="true"
                    TextLabelID="Labelfilter" CollapsedSize="0" ImageControlID="Imagefilter" CollapsedImage="../images/right.jpeg"
                    ExpandedImage="../images/down.jpeg">
                </asp:CollapsiblePanelExtender>
                <center>
                    <br />
                    <asp:Label ID="errmsg" runat="server" Font-Bold="true" Font-Size="Medium" Font-Names="Book Antiqua"
                        ForeColor="Red"></asp:Label>
                    <br />
                    <div id="divMainContents" runat="server" style="display: table; margin: 0px; margin-bottom: 20px;
                        margin-top: 10px; position: relative; text-align: left;">
                        <table class="printclass" style="width: 98%; height: auto; margin: 0px; padding: 0px;">
                            <tr>
                                <td rowspan="5" style="width: 100px; margin: 0px; border: 0px;">
                                    <asp:Image ID="imgLeftLogo2" runat="server" AlternateText="" ImageUrl="~/college/Left_Logo.jpeg"
                                        Width="100px" Height="100px" />
                                </td>
                                <th class="marginSet" align="center" colspan="6">
                                    <span id="spCollegeName" class="headerDisp" runat="server"></span>
                                </th>
                            </tr>
                            <tr>
                                <th class="marginSet" align="center" colspan="6">
                                    <span id="spAddr" class="headerDisp1" runat="server"></span>
                                </th>
                            </tr>
                            <tr>
                                <th class="marginSet" align="center" colspan="6">
                                    <span id="spReportName" class="headerDisp1" runat="server"></span>
                                </th>
                            </tr>
                            <tr>
                                <td class="marginSet" colspan="3" align="center">
                                    <span id="spDegreeName" class="headerDisp1" runat="server"></span>
                                </td>
                                <td class="marginSet" colspan="3" align="right">
                                    <span id="spSem" class="headerDisp1" runat="server"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="marginSet" colspan="3" align="left">
                                    <span id="spProgremme" class="headerDisp1" runat="server"></span>
                                </td>
                                <td class="marginSet" colspan="3" align="right">
                                    <span id="spSection" class="headerDisp1" runat="server"></span>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="Showgrid" runat="server" Visible="false" AutoGenerateColumns="true"
                            HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="#0CA6CA" Font-Names="Book Antiqua"
                            OnRowDataBound="Showgrid_OnRowDataBound">
                        </asp:GridView>
                    </div>
                    <br />
                    <asp:Label ID="lblrptname" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" Text="Report Name"></asp:Label>
                    <asp:TextBox ID="txtexcelname" runat="server" Height="20px" Width="180px" Style="font-family: 'Book Antiqua'"
                        Font-Bold="True" onkeypress="display()" Font-Names="Book Antiqua" Font-Size="Medium"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtexcelname"
                        FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="()}{][ ,">
                    </asp:FilteredTextBoxExtender>
                    <asp:Button ID="btnxl" runat="server" Text="Export Excel" Font-Bold="True" Font-Names="Book Antiqua"
                        Font-Size="Medium" OnClick="btnxl_Click" />
                    <asp:Button ID="btnprint" runat="server" Text="Print" Font-Names="Book Antiqua" Font-Size="Medium"
                        Font-Bold="true" OnClick="btnprint1_Click" />
                    <NEW:NEWPrintMater runat="server" ID="Printcontrol" Visible="false" />
                    <asp:Button ID="btnPrint1" runat="server" Visible="false" Text="Direct Print" OnClientClick="return PrintPanel();"
                        Font-Names="Book Antiqua" Font-Size="Medium" Font-Bold="true" Height="35px" CssClass="textbox textbox1" />
                    <br />
                </center>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnprint" />
                <asp:PostBackTrigger ControlID="btnxl" />
            </Triggers>
        </asp:UpdatePanel>
        <%--progressBar for go--%>
        <center>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel_go">
                <ProgressTemplate>
                    <center>
                        <div style="height: 40px; width: 150px;">
                            <img src="../gv images/cloud_loading_256.gif" style="height: 150px;" />
                            <br />
                            <span style="font-family: Book Antiqua; font-size: medium; font-weight: bold; color: Black;">
                                Processing Please Wait...</span>
                        </div>
                    </center>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="UpdateProgress1"
                PopupControlID="UpdateProgress1">
            </asp:ModalPopupExtender>
        </center>
    </body>
    </html>
</asp:Content>
