<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>论坛与活动管理</title>
    <script src="/Framework/javascript/Biz/ZCourse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ZForumType.js" type="text/javascript"></script>
    <script src="Controller/EventsCtl.js" type="text/javascript"></script>
    <script src="Model/EventsVM.js" type="text/javascript"></script>
    <script src="Store/EventsVMStore.js" type="text/javascript"></script>
    <script src="View/EventsView.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 86px;">
                    <div id='span_panel' style="float:left; width: 820px; overflow: hidden;">
                    </div>
                    <div id='btn_panel2' style="float:left; width: 200px;  ">
                    </div>
                    <div id='btn_panel' style="clear:both; width: 220px;">
                    </div>
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
</asp:Content>
