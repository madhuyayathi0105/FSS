<%@ Page Title="Subject Wise Test Mark Report" Language="C#" MasterPageFile="~/MarkMod/CAMSubSiteMaster.master"
    AutoEventWireup="true" CodeFile="SubjectWiseTestMark.aspx.cs" Inherits="MarkMod_SubjectWiseTestMark" %>

    <%@ Register Src="~/Usercontrols/NewPrintMaster.ascx" TagName="NEWPrintMater" TagPrefix="NEW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="~/Styles/css/Commoncss.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .fontStyle
        {
            font-size: medium;
            font-weight: bolder;
            font-style: oblique;
            padding: 5px;
        }
        .fontStyle1
        {
            font-size: medium;
            font-style: oblique;
            padding: 3px;
            color: Blue;
        }
        .commonHeaderFont
        {
            font-size: medium;
            color: Black;
            font-family: 'Book Antiqua';
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function display1() {
            document.getElementById('<%#lblExcelErr.ClientID %>').innerHTML = "";
        }
    </script>
      <script type="text/javascript">
          function printTTOutput() {
              var panel = document.getElementById("<%=printdiv.ClientID %>");
              var printWindow = window.open('', '', 'height=816,width=980');
              printWindow.document.write('<html><head>');
              printWindow.document.write('</head><body >');
              printWindow.document.write(panel.innerHTML);
              printWindow.document.write('</body></html>');
              printWindow.document.close();
              setTimeout(function () {
                  printWindow.print();
              }, 500);
              return false;
          }
    </script>
    <style tyle="text/css">
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
            #printdiv
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div>
            <span id="spPageHeading" runat="server" class="fontstyleheader" style="color: Green;
                margin: 0px; margin-bottom: 10px; margin-top: 10px; position: relative;">Subject
                Wise Test Mark Report</span>
        </div>
        <div id="divSearch" runat="server" visible="true" class="maindivstyle" style="width: 100%;
            height: auto; margin: 0px; margin-bottom: 20px; margin-top: 10px; padding: 5px;
            position: relative;">
            <table class="maintablestyle" style="height: auto; margin-left: 0px; margin-top: 10px;
                margin-bottom: 10px; padding: 6px;">
                <tr>
                    <td>
                        <asp:Label ID="lblCollege" runat="server" Text="College" CssClass="commonHeaderFont"
                            AssociatedControlID="ddlCollege"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="dropdown commonHeaderFont"
                            Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblBatch" runat="server" Text="Batch" CssClass="commonHeaderFont"
                            AssociatedControlID="ddlBatch"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBatch" runat="server" CssClass="commonHeaderFont" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"
                            AutoPostBack="True" Width="80px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblDegree" runat="server" CssClass="commonHeaderFont" Text="Degree"
                            AssociatedControlID="ddlDegree"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="commonHeaderFont" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                            AutoPostBack="True" Width="80px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblBranch" runat="server" CssClass="commonHeaderFont" Text="Branch"
                            AssociatedControlID="ddlBranch"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="commonHeaderFont" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                            AutoPostBack="True" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblSem" runat="server" CssClass="commonHeaderFont" Text="Sem" AssociatedControlID="ddlSem"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSem" runat="server" CssClass="commonHeaderFont" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"
                            AutoPostBack="True" Width="40px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSec" runat="server" Text="Section" CssClass="commonHeaderFont"
                            AssociatedControlID="ddlSec"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSec" runat="server" CssClass="commonHeaderFont" OnSelectedIndexChanged="ddlSec_SelectedIndexChanged"
                            AutoPostBack="True" Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblTest" runat="server" Text="Test" CssClass="commonHeaderFont" AssociatedControlID="ddlTest"></asp:Label>
                    </td>
                    <td>
                        <div style="position: relative;">
                            <asp:UpdatePanel ID="upnlTest" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtTest" Visible="false" Width="76px" runat="server" CssClass="textbox  txtheight2 commonHeaderFont"
                                        ReadOnly="true">-- Select --</asp:TextBox>
                                    <asp:Panel ID="pnlTest" Visible="false" runat="server" CssClass="multxtpanel" Height="200px"
                                        Width="280px">
                                        <asp:CheckBox ID="chkTest" CssClass="commonHeaderFont" runat="server" Text="Select All"
                                            AutoPostBack="True" OnCheckedChanged="chkTest_CheckedChanged" />
                                        <asp:CheckBoxList ID="cblTest" CssClass="commonHeaderFont" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cblTest_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="popExtTest" runat="server" TargetControlID="txtTest"
                                        PopupControlID="pnlTest" Position="Bottom">
                                    </asp:PopupControlExtender>
                                    <asp:DropDownList ID="ddlTest" runat="server" Visible="true" CssClass="commonHeaderFont"
                                        OnSelectedIndexChanged="ddlTest_SelectedIndexChanged" AutoPostBack="True" Width="80px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblSubject" AssociatedControlID="ddlSubject" runat="server" Text="Subject"
                            CssClass="commonHeaderFont"></asp:Label>
                    </td>
                    <td>
                        <div style="position: relative;">
                            <asp:UpdatePanel ID="UpnlSubject" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSubject" Visible="false" Width="76px" runat="server" CssClass="textbox  txtheight2 commonHeaderFont"
                                        ReadOnly="true">-- Select --</asp:TextBox>
                                    <asp:Panel ID="pnlSubject" Visible="false" runat="server" CssClass="multxtpanel"
                                        Height="200px" Width="280px">
                                        <asp:CheckBox ID="chkSubject" CssClass="commonHeaderFont" runat="server" Text="Select All"
                                            AutoPostBack="True" OnCheckedChanged="chkSubject_CheckedChanged" />
                                        <asp:CheckBoxList ID="cblSubject" CssClass="commonHeaderFont" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cblSubject_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="popExtSubject" runat="server" TargetControlID="txtSubject"
                                        PopupControlID="pnlSubject" Position="Bottom">
                                    </asp:PopupControlExtender>
                                    <asp:DropDownList ID="ddlSubject" Width="152px" Visible="true" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" CssClass="commonHeaderFont">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td>
                    <asp:UpdatePanel ID="btngoUpdatePanel" runat="server">
                                <ContentTemplate>
                        <asp:Button ID="btnGetMarks" CssClass="textbox textbox1 commonHeaderFont" runat="server"
                            OnClick="btnGetMarks_Click" Text="Get Marks" Style="width: auto; height: auto;" />

                             </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkConvertedTo" Checked="false" runat="server" CssClass="commonHeaderFont"
                                        Text="Include Converted Mark" AutoPostBack="true" OnCheckedChanged="chkConvertedTo_CheckedChanged" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConvertedMaxMark" runat="server" CssClass="commonHeaderFont"
                                        Text="" Width="50px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="filterConvert" runat="server" TargetControlID="txtConvertedMaxMark"
                                        FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIncludeGrade" runat="server" Checked="false" CssClass="commonHeaderFont"
                                        Text="Include Grade" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkRoundOffMarks" runat="server" Checked="false" CssClass="commonHeaderFont"
                                        Text="Round off Marks" />
                                </td>
                                <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="false" CssClass="commonHeaderFont"
                                        Text="Without Range" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblErrSearch" runat="server" Text="" ForeColor="Red" Visible="False"
                Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium" Style="margin: 0px;
                margin-bottom: 15px; margin-top: 10px;"></asp:Label>
            <div id="divMainContents" runat="server" visible="false" style="margin: 0px; margin-bottom: 15px;
                margin-top: 10px; position: relative;">
                <div id="divPrint1" runat="server" style="margin: 0px; margin-top: 20px;">
                    <table>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblExcelErr" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                                    Font-Size="Medium" ForeColor="Red" Text="Please Enter Your Report Name" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblExcelReportName" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                                    Font-Size="Medium" Text="Report Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExcelName" runat="server" CssClass="textbox textbox1" Height="20px"
                                    Width="180px" Style="font-family: 'Book Antiqua'" Font-Bold="True" Font-Names="Book Antiqua"
                                    onkeypress="display1()" Font-Size="Medium"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtExcelName"
                                    FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="(),.[]_"
                                    InvalidChars="/\@#$%^&*()-=+!~`<>?|:;'">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnExportExcel" runat="server" Font-Bold="true" Font-Names="Book Antiqua"
                                    OnClick="btnExportExcel_Click" Font-Size="Medium" Style="width: auto; height: auto;"
                                    Text="Export To Excel" CssClass="textbox textbox1" />
                            </td>
                            <td>
                                <asp:Button ID="btnPrintPDF" runat="server" Text="Print" OnClick="btnPrintPDF_Click"
                                    Font-Names="Book Antiqua" Font-Size="Medium" Font-Bold="true" Style="width: auto;
                                    height: auto;" CssClass="textbox textbox1" />
                                    <NEW:NEWPrintMater runat="server" ID="Printcontrol" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnDirectPrint" Visible="false" CssClass="textbox textbox1" runat="server"
                                    Font-Bold="True" Font-Size="Medium" Font-Names="Book Antiqua" Style="width: auto;
                                    height: auto;" Text="Direct Print" OnClientClick="return PrintPanel();" />
                                    <button id="btnPrint" runat="server"  height="29px" width="62px" onclick="return printTTOutput();"
            style=" font-weight: bold; font-size: medium; font-family: Book Antiqua;">
            Direct Print
        </button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                    </table>
                </div>
               
                 <center>
                         <div id="printdiv" runat="server">
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
      <center>
                          
                            <asp:GridView ID="grdover" runat="server" Width="500px" BorderStyle="Double" Font-Bold="true"
                            Font-Names="Book Antiqua" Font-Size="Medium" GridLines="Both" CellPadding="4"  
                             ShowFooter="false" ShowHeader="true">
                           
                        </asp:GridView>
                        </center>
            <table class="printclass" style="width: 98%; height: auto; margin-top: 100px; padding: 0px;">
                <tr>
                    <td>
                        
                    </td>
                    <td style="text-align: right">
                        
                    </td>
                </tr>
            </table>
        </div>
                           
                    
                </center>
            </div>
            <Insproplus:printmaster runat="server" ID="printCommonPdf" Visible="false" />
        </div>
    </center>
    <%-- Confirmation --%>
    <center>
        <div id="divConfirmBox" runat="server" visible="false" style="height: 550em; z-index: 1000;
            width: 100%; background-color: rgba(54, 25, 25, .2); position: absolute; top: 0;
            left: 0px;">
            <center>
                <div id="divConfirm" runat="server" class="table" style="background-color: White;
                    height: auto; width: 38%; border: 5px solid #0CA6CA; border-top: 25px solid #0CA6CA;
                    left: 30%; right: 30%; top: 40%; position: fixed; border-radius: 10px;">
                    <center>
                        <table style="height: auto; width: 100%; padding: 3px;">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblConfirmMsg" runat="server" Text="Do You Want To Delete All Subject Remarks?"
                                        Style="color: Red;" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <asp:Button ID="btnYes" CssClass=" textbox btn1 textbox1" Style="height: 28px; width: 65px;"
                                            OnClick="btnYes_Click" Text="Yes" runat="server" />
                                        <asp:Button ID="btnNo" CssClass=" textbox btn1 textbox1" Style="height: 28px; width: 65px;"
                                            OnClick="btnNo_Click" Text="No" runat="server" />
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
            </center>
        </div>
    </center>
    <%-- Alert Box --%>
    <center>
        <div id="divPopAlert" runat="server" visible="false" style="height: 550em; z-index: 2000;
            width: 100%; background-color: rgba(54, 25, 25, .2); position: absolute; top: 0%;
            left: 0%;">
            <center>
                <div id="divPopAlertContent" runat="server" class="table" style="background-color: White;
                    height: 120px; width: 23%; border: 5px solid #0CA6CA; border-top: 25px solid #0CA6CA;
                    left: 39%; right: 39%; top: 35%; padding: 5px; position: fixed; border-radius: 10px;">
                    <center>
                        <table style="height: 100px; width: 100%; padding: 5px;">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblAlertMsg" runat="server" Style="color: Red;" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <asp:Button ID="btnPopAlertClose" Font-Bold="true" Font-Size="Medium" Font-Names="Book Antiqua"
                                            CssClass="textbox textbox1" Style="height: auto; width: auto;" OnClick="btnPopAlertClose_Click"
                                            Text="Ok" runat="server" />
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
            </center>
        </div>
    </center>   

    </ContentTemplate>
                                <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportExcel" />
                                </Triggers>
                             </asp:UpdatePanel>

                             <center>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="btngoUpdatePanel">
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
    
    
</asp:Content>
