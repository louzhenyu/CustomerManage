<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>中欧会员</title>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Other/editor/kindeditor.js" type="text/javascript"></script>
    <script src="Controller/SendMessageCtl.js" type="text/javascript"></script>
    <script src="Model/SendMessageVM.js" type="text/javascript"></script>
    <script src="Store/SendMessageVMStore.js" type="text/javascript"></script>
    <script src="View/SendMessageView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section" id="section" style="min-height: 0px; height: auto; border: 0;">
        <div class="m10 article">
            <div class="view_Search">
                <span id="basic_panel"></span>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='dvWork'></span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
