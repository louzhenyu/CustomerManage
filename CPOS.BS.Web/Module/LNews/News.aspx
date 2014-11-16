<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>资讯管理</title>
    <script src="/Framework/javascript/Biz/NewsType.js" type="text/javascript"></script>
    <script src="Controller/NewsCtl.js" type="text/javascript"></script>
    <script src="Model/NewsVM.js" type="text/javascript"></script>
    <script src="Store/NewsVMStore.js" type="text/javascript"></script>
    <script src="View/NewsView.js?ver=0.0.1" type="text/javascript"></script>
    <script src="../../Framework/Javascript/Biz/LNewsTypeSelectTree.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search">
                    <%--<div id='span_panel' style="float: left; width: 100%; overflow: hidden;">
                    </div>--%>
                    <span id='span_panel'></span>
                    <%-- <div id='btn_panel' style="float: left; width: 1000px;">
                    </div>--%>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_create'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <%--<div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='span1'></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span2'></span>
                </div>
            </div>
            <div class="DivGridView" id="Div1">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
    --%>
</asp:Content>
