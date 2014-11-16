<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员查询</title>
    <script src="Controller/VipSearchCtl.js" type="text/javascript"></script>
    <script src="Model/RegisterVM.js" type="text/javascript"></script>
    <script src="Store/RegisterVMStore.js" type="text/javascript"></script>
    <script src="View/VipSearchView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <div class="z_event_border" style="padding: 0px; border-top: 0px; padding-bottom: 0px;
            height: 390px;">
            <div style="width: 100%; padding-left: 0px; padding-right: 0px;">
                <div style="width: 100%;">
                    <div style="float: right; width: 100%;">
                        <div id="gridVipSearch">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="z_event_border" style="font-weight: bold; height: 46px; line-height: 46px;
            padding-left: 10px; border-top: 0px; clear: both; background: rgb(241, 242, 245);">
            <div id="btnNext" style="float: right; margin-top: 10px; margin-right: 10px;">
            </div>
        </div>
    </div>
</asp:Content>
