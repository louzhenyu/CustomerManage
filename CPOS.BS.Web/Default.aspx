<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JIT.CPOS.BS.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section" style="display:;">
    
    </div>
    <script type="text/javascript">
        //$(function () {

        //    $("#commonNav li").eq(0).find("a").trigger("click");
        //})
        location.href = "module/Index/IndexPage.aspx?CustomerId=<%=this.CurrentUserInfo.CurrentLoggingManager.Customer_Id.ToString()%>";
    </script>
</asp:Content>
