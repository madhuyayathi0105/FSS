<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attendanceduplicateentryremove.aspx.cs"
    Inherits="Attendanceduplicateentryremove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btn_update" runat="server" Text="Missing Attendance update Step 1"
            OnClick="btn_update_click" />
        <asp:Button ID="btn_update2" runat="server" Text="Missing Attendance update Step 2"
            OnClick="btn_update2_click" />
        <asp:Button ID="Button1" runat="server" Visible="false" Text="Holiday Delete" OnClick="Button1_click" />
        <asp:Button ID="Button2" runat="server" Text="Delete CAM " OnClick="Button2_click" />
        <br />
        <asp:Label ID="lbl_error" Font-Bold="true" runat="server" Font-Size="Medium"></asp:Label>
        <asp:Button ID="Button4" runat="server" Text="Remove SubjectChooser" OnClick="Button4_click" />
    </div>
    </form>
</body>
</html>
