<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>品牌编辑</title>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>
    <script src="Controller/BrandEditCtl.js" type="text/javascript"></script>
    <script src="Model/BrandVM.js" type="text/javascript"></script>
    <script src="Store/BrandEditVMStore.js" type="text/javascript"></script>
    <script src="View/BrandEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
