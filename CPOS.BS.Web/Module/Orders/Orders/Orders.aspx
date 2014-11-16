<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>花间堂订单</title>
    <script src="Controller/OrdersCtl.js" type="text/javascript"></script>
    <script src="Model/OrdersVM.js" type="text/javascript"></script>
    <script src="Store/OrdersVMStore.js" type="text/javascript"></script>
    <script src="View/OrdersView.js" type="text/javascript"></script>
    <style type="text/css">
        .x-form-display-field { padding-top: 0 !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='search_form_panel'></span>
                </div>
                <div class="view_Search2">
                    <span id='search_button_panel'></span>
                </div>
            </div>
            <div class="art-titbutton" style="margin: 0px; background: #E6E4E1;">
                <div class="view_Button" style="margin: 0px; margin-top: 10px; background: #E6E4E1;">
                    <div id='btn_export' style="float: left; width: 200px;">
                    </div>
                </div>
            </div>
            <div class="art-titbutton" style="margin: 0px; background: #E6E4E1;">
                <input id="hStatus" type="hidden" value="0" />
                <div class="view_Button" style="margin: 0px; background: #E6E4E1;">
                    <div id="tab0" class="z_posorder_head" onclick="fnSearchOrder('0')">
                        <div style="width: 100px; height: 20px;">
                            全部</div>
                        <div id="txtNum0" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab100" class="z_posorder_head" onclick="fnSearchOrder('100')">
                        <div style="width: 100px; height: 20px;">
                            待审核</div>
                        <div id="txtNum1" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab200" class="z_posorder_head" onclick="fnSearchOrder('200')">
                        <div style="width: 100px; height: 20px;">
                            订单已确认</div>
                        <div id="txtNum2" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab300" class="z_posorder_head" onclick="fnSearchOrder('300')">
                        <div style="width: 100px; height: 20px;">
                            完成</div>
                        <div id="txtNum3" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab400" class="z_posorder_head" onclick="fnSearchOrder('400')">
                        <div style="width: 100px; height: 20px;">
                            取消</div>
                        <div id="txtNum4" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab500" class="z_posorder_head" onclick="fnSearchOrder('500')">
                        <div style="width: 100px; height: 20px;">
                            审核不通过</div>
                        <div id="txtNum5" style="height: 24px;">
                            0</div>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
