<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动管理</title>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/EventType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/LEventSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/LEventsType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/Controller/EventsCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/Model/EventsVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/Store/EventsVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/View/EventsView.js?ver=0.0.1"%>" type="text/javascript"></script>
    <style type="text/css">
        td {
        vertical-align: middle; 
        }
    </style>
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
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
