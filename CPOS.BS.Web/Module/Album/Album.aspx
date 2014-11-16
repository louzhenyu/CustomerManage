<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>相册管理</title>
    <script src="/Framework/javascript/Biz/AlbumModuleType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/AlbumType.js" type="text/javascript"></script>
    <script src="Controller/AlbumCtl.js" type="text/javascript"></script>
    <script src="Model/AlbumVM.js" type="text/javascript"></script>
    <script src="Store/AlbumVMStore.js" type="text/javascript"></script>
    <script src="View/AlbumView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 44px;">
                    <div id='span_panel' style="float: left; width: 820px; overflow: hidden;">
                    </div>
                    <div id='btn_panel' style="float: left; width: 200px;">
                    </div>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_create'></span>
                </div>
            </div>
            <div class="DivGridView" id="gridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
