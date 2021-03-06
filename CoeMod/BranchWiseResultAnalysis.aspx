﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CoeMod/COESubSiteMaster.master" AutoEventWireup="true" CodeFile="BranchWiseResultAnalysis.aspx.cs" Inherits="BranchWiseResultAnalysis" %>

<%@ Register Assembly="FarPoint.Web.Spread" Namespace="FarPoint.Web.Spread" TagPrefix="FarPoint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:ScriptManager ID="scrptmngr" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .font
        {
            font-family: Book Antiqua;
            font-size: medium;
            font-weight: bold;
        }
        .style2
        {
            top: 227px;
            left: 20px;
            position: absolute;
            height: 21px;
            width: 147px;
        }
    </style>
    <br />
   <center>
        <asp:Label ID="Label7" runat="server" Text="Branch Wise Result Analysis" Font-Bold="True"
            Font-Names="Book Antiqua" Font-Size="Large" ForeColor="Green"></asp:Label></center>
   <br /><center>
    <table style="width:700px; height:70px; background-color:#0CA6CA;">
        <tr>
            <td>
                <asp:Label ID="lblMonth" runat="server" CssClass="font" Text="Exam Month" Width="100px"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="font" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblyear" runat="server" Text="Exam Year" Font-Bold="True" Font-Names="Book Antiqua"
                    Font-Size="Medium" Width="80px"> </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="font" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lbldegree" runat="server" Text="Degree" Font-Bold="True" Font-Names="Book Antiqua"
                    Font-Size="Medium"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddldegree" runat="server" AutoPostBack="True" Font-Bold="True"
                    Font-Names="Book Antiqua" Font-Size="Medium" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblbranch" runat="server" Text="Branch" Font-Bold="True" Font-Names="Book Antiqua"
                    Font-Size="Medium"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="True" Font-Bold="True"
                    Font-Names="Book Antiqua" Font-Size="Medium" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btngo" runat="server" Text="Go" Font-Bold="True" Font-Names="Book Antiqua"
                    Font-Size="Medium" OnClick="btngo_Click" />
            </td>
            <td>
                <asp:RadioButton ID="RadioRegular" runat="server" AutoPostBack="True" Font-Bold="True"
                    Font-Names="Book Antiqua" Font-Size="Medium" Text="Regular" GroupName="Subjectwise" Width="90px" />
                    </td>
                    <td>
                <asp:RadioButton ID="RadioArrear" runat="server" AutoPostBack="True" Font-Bold="True"
                    Font-Names="Book Antiqua" Font-Size="Medium" Text="Arrear" GroupName="Subjectwise" Width="80px" />
            </td>
            <td>
                <asp:CheckBox ID="chechbox1" runat="server" AutoPostBack="true" Font-Bold="True"
                    Font-Names="Book Antiqua" Font-Size="Medium" Text="Detailed" OnCheckedChanged="chechbox1_CheckedChanged"  Width="90px"/>
            </td>
        </tr>
    </table>
    </center>
    <br />
    <asp:Label ID="lblnorec" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
        Font-Size="Medium" ForeColor="#FF3300" Text="No Record(s) Found" Visible="False"
        CssClass="style2"></asp:Label>
    <br />
    <center>
        <FarPoint:FpSpread ID="spreadbind" runat="server" BorderColor="Black" BorderStyle="Solid"
            BorderWidth="1px" Height="200" HorizontalScrollBarPolicy="Never" VerticalScrollBarPolicy="Never">
            <CommandBar BackColor="Control" ButtonFaceColor="Control" ButtonHighlightColor="ControlLightLight"
                ButtonShadowColor="ControlDark" ButtonType="PushButton">
            </CommandBar>
            <Sheets>
                <FarPoint:SheetView SheetName="Sheet1">
                </FarPoint:SheetView>
            </Sheets>
        </FarPoint:FpSpread></center>
    <%--</ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>

