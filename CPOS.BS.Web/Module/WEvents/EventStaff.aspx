<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动人员管理</title>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/LEventSelectTree.js" type="text/javascript"></script>
    <script src="Controller/EventVipCtl.js" type="text/javascript"></script>
    <script src="Model/EventsVM.js" type="text/javascript"></script>
    <script src="Store/EventsVMStore.js" type="text/javascript"></script>
    <script src="View/EventVipView.js" type="text/javascript"></script>
    <style type="text/css">
        .contentArea {
    margin-left:0px;
    float: left;
}
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search">
                    <div id='span_panel' style="float:left;">
                    </div>
                    <div id='btn_panel' style="float:left; width: 20%;">
                    </div>
                    <span style="clear:both; height:1px; overflow:hidden; display:block"></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_operation'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>
