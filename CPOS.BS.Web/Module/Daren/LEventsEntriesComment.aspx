<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>评论列表</title>
    <script src="Controller/LEventsEntriesCommentCtl.js" type="text/javascript"></script>
    <script src="Model/LEventsEntriesVM.js" type="text/javascript"></script>
    <script src="Store/LEventsEntriesVMStore.js" type="text/javascript"></script>
    <script src="View/LEventsEntriesCommentView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="Div1" style="height: 441px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <div style="height: 35px; padding-top: 3px;">
                            <span style="font-weight: bold;">评论列表</span>
                        </div>
                    </div>
                    <div id="gridComment">
                    </div>
                </div>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 46px; line-height: 46px;
                padding-left: 10px; border-top: 0px; clear: both; background: rgb(241, 242, 245);">
                <div id="btnClose" style="float: left; margin-top: 10px; margin-right: 10px;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
