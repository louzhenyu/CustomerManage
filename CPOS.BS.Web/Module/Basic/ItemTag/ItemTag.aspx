<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script language="javascript" type="text/javascript" src="../../../Framework/Javascript/Biz/Status.js"></script>
    <script language="javascript" type="text/javascript" src="Model/ItemTagVM.js"></script>
    <script language="javascript" type="text/javascript" src="Store/ItemTagVMStore.js"></script>
    <script language="javascript" type="text/javascript" src="View/ItemTagView.js"></script>
    <script language="javascript" type="text/javascript" src="Controller/ItemTagManagementCtl.js?v=1"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="dvPlaceholder"></div>
</asp:Content>
