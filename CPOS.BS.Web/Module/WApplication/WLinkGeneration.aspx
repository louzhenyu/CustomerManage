<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="WLinkGeneration.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.WApplication.WLinkGeneration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>这是一个测试</div>
    <div>
        <script language ="javascript" type="text/javascript">
            var customerid ='<%=CustomerID %>';
            alert(customerid);
        </script>
        <span>选择要查询的页面</span>
    </div>
</asp:Content>
