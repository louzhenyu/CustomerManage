<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>最受关注</title>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="Controller/EventStatsCtl.js" type="text/javascript"></script>
    <script src="Model/EventStatsVM.js" type="text/javascript"></script>
    <script src="Store/EventStatsStore.js" type="text/javascript"></script>
    <script src="View/EventStatsView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='search_form_panel'></span>
                </div>
                <div class="view_Search2">
                    <span id='search_button_panel'></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='dvWork'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
