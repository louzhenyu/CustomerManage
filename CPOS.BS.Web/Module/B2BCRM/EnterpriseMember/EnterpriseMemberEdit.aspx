<%@ Page Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="Controller/EnterpriseMemberEditCtl.js" type="text/javascript"></script>
    <script src="Model/EnterpriseMemberVM.js" type="text/javascript"></script>
    <script src="Store/EnterpriseMemberVMStore.js" type="text/javascript"></script>
    <script src="View/EnterpriseMemberEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
