<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="UnitManagement.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Basic.UnitNew.UnitManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Framework/Javascript/Biz/Status.js"></script>
    <script language="javascript" type="text/javascript" src="Model/UnitVM.js"></script>
    <script language="javascript" type="text/javascript" src="Store/UnitVMStore.js"></script>
    <script language="javascript" type="text/javascript" src="View/UnitView.js"></script>
    <script language="javascript" type="text/javascript" src="Controller/UnitManagementCtl.js"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <%--图片上传控件--%>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPlaceholder"></div>
</asp:Content>
