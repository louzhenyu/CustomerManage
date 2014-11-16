<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>抽奖日志</title>
    <script src="Model/EventsVM.js" type="text/javascript"></script>
    <script src="Model/LotteryLogVM.js" type="text/javascript"></script>
    <script src="Store/EventsLotteryLogListVMStore.js" type="text/javascript"></script>
    <script src="View/EventsLotteryLogListView.js" type="text/javascript"></script>
    <script src="Controller/EventsLotteryLogListCtl.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
