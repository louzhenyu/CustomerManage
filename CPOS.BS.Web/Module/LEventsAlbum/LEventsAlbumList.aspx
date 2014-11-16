<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>资讯管理</title>
    <script src="/Framework/javascript/Biz/NewsType.js" type="text/javascript"></script>
    <script src="Controller/LEventsAlbumCtl.js" type="text/javascript"></script>
    <script src="Model/LEventsAlbumVM.js" type="text/javascript"></script>
    <script src="Store/LEventsAlbumVMStore.js" type="text/javascript"></script>
    <script src="View/LEventsAlbumView.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" >                   
                      <span id='span_panel'></span>
                 </div>
            </div>
            <div class="art-titbutton" >
                <div class="view_Button" >
                    <span id='span_create'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
