<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title></title>
    <script src="Controller/EventVipCtl.js" type="text/javascript"></script>
    <script src="Model/EventVipVM.js" type="text/javascript"></script>
    <script src="Store/EventVipVMStore.js" type="text/javascript"></script>
    <script src="View/EventVipView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section" style="min-height: 0px; height: auto; border: 0;">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id="span_panel"></span>
                </div>
                <div class="view_Search2">
                    <span id="span_panel2"></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id="span_create"></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
                <div class="cb">
                </div>
            </div>
        </div>
    </div>
</asp:Content>