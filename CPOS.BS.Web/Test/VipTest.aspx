<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VipTest.aspx.cs" Inherits="JIT.CPOS.BS.Web.Test.VipTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lb1" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server"> </asp:GridView> 
        <br />
             <asp:GridView ID="GridView2" runat="server"> 
        </asp:GridView> 
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
