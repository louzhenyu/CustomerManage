<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Role.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
    <%--<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>--%>
    <script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
   <%-- <script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>--%>
    <script src="Controller/TaskDataCtl.js" type="text/javascript"></script>
    <script src="Model/TaskDataVM.js" type="text/javascript"></script>
    <script src="Store/TaskDataVMStore.js" type="text/javascript"></script>
    <script src="View/TaskDataView.js" type="text/javascript"></script>
    <style type="text/css">
        .x-grid-back-100 .x-grid-cell
        {
        }
        .x-grid-back-above80 .x-grid-cell
        {
            background-color: Orange !important;
        }
        .x-grid-back-below80 .x-grid-cell
        {
            background-color: Red !important;
            color: White;
            font-weight: bold;
        }
        
        /*合计行的样式 begin*/
        .x-grid-row-summary .x-grid-cell-inner
        {
            color: Red;
            font-weight: bold;
            border-bottom: 1px solid #c6c6c6;
        }
        /*合计行的样式 end*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='span_panel'></span>
                </div>
                <div class="view_Search2">
                    <span id='span_panel2'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
