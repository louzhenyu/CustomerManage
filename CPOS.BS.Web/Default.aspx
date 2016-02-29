<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master" 
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JIT.CPOS.BS.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        #leftMenu{ display: none;}   /*隐藏左边菜单栏*/
        #contentArea{ margin: 0; border: none}   /*取消右边边距和右边边框*/
        #commonNav ul{ display: none;}   /*隐藏头部菜单项*/
        #leftsead{ display: none;}/*隐藏页面右侧浮动导航*/

    </style>
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
