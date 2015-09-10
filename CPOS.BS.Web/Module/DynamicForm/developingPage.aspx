<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>正在开发中</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/global.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/styleAttr.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="section" class="m10">
    <div class="allPage">
       <img src="images/developing.jpg" />
    </div>
</div>



<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/DynamicForm/js/main.js"%>"></script>
</asp:Content>