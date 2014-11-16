<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarketEvent.aspx.cs" Inherits="JIT.CPOS.Web.Test.MarketEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lbCode" runat="server"></asp:Label>
    <asp:Label ID="lbDesc" runat="server"></asp:Label>
    <asp:Label ID="lbCount" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView2" runat="server"> 
        </asp:GridView> <br />
        <asp:GridView ID="GridView1" runat="server"> 
        </asp:GridView> 
    </div>
    </form>
</body>
</html>
