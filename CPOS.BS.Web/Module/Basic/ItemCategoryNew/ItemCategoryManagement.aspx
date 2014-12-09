<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" CodeBehind="ItemCategoryManagement.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module2.BaseData.ItemCategory.ItemCategoryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Biz/Status.js"%>"></script>
    <script language="javascript" type="text/javascript" src="<%=StaticUrl+"/module/basic/ItemCategoryNew/Model/ItemCategoryVM.js"%>"></script>
    <script language="javascript" type="text/javascript" src="<%=StaticUrl+"/module/basic/ItemCategoryNew/Store/ItemCategoryVMStore.js"%>"></script>
    <script language="javascript" type="text/javascript" src="<%=StaticUrl+"/module/basic/ItemCategoryNew/View/ItemCategoryView.js?v=1.3"%>"></script>
    <script language="javascript" type="text/javascript" src="<%=StaticUrl+"/module/basic/ItemCategoryNew/Controller/ItemCategoryManagementCtl.js?v=1.3"%>"></script>
    <%--图片上传控件--%>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/examples/jquery.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/kindeditor.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"%>"></script>
  
    <style type="text/css">
        .picmargin
        {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPlaceholder">
    </div>
</asp:Content>
