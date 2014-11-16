<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>密码管理</title>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="Controller/ClientUserCtl.js" type="text/javascript"></script>
    <script src="Model/ClientUserVM.js" type="text/javascript"></script>
    <script src="Store/ClientUserVMStore.js" type="text/javascript"></script>
    <script src="View/ClientUserView.js" type="text/javascript"></script>
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
