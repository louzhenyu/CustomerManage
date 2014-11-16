<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="ReportChange.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Reports.ReportChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<form id="Form1" runat="server">--%>
<div>&nbsp;</div>
<div><p>&nbsp;&nbsp;&nbsp;
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
        RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="201206">&nbsp;2012年6月&nbsp;&nbsp;&nbsp;</asp:ListItem>
        <asp:ListItem Value="201207">&nbsp;2012年7月&nbsp;&nbsp;&nbsp;</asp:ListItem>
        <asp:ListItem Value="201208">&nbsp;2012年8月&nbsp;&nbsp;&nbsp;</asp:ListItem>
        <asp:ListItem Value="201209">&nbsp;2012年9月&nbsp;&nbsp;&nbsp;</asp:ListItem>
        <asp:ListItem Value="201210">&nbsp;2012年10月&nbsp;&nbsp;&nbsp;</asp:ListItem>
    </asp:CheckBoxList>
    <asp:HiddenField ID="HiddenField1" runat="server" /> 
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="查询" Width="80px" onclick="Button1_Click" />
    </p>
</div>
<asp:Label ID="lblReport" runat="server" Text="" />
<%--</form>--%>
</asp:Content>
