<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员信息</title>
    
    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipStatus.js" type="text/javascript"></script>

    <script src="Model/VipCardVM.js" type="text/javascript"></script>
    <%--<script src="Store/VipVMStore.js" type="text/javascript"></script>
    <script src="Store/VipEditVMStore.js" type="text/javascript"></script>--%>
    <script src="View/VipCardDisableView.js" type="text/javascript"></script>
    <script src="Controller/VipCardDisableCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div style="width:100%;">
                    <div class="" style="">
                        <div class="z_card_row" style="">
                            <div style="float:left;">会员卡号：</div>
                            <div style="float:left;">
                                <div id="txtVipCardCode" style="font-weight:bold; line-height:28px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left;">账户余额：</div>
                            <div style="float:left;">
                                <div id="txtTotalAmount" style="float:left; color:red; min-width:200px;font-weight:bold; line-height:28px;"></div>
                                <div style="float:right; color:red; width:170px;">警告：余额非0，建议先退款！</div>
                            </div>
                        </div>
                        <div class="" style="border:1px solid #ddd; height:; clear:both; margin-top:5px; 
                            padding:10px; padding-right:10px;">
                            <div class="z_card_row" style="border-bottom:1px solid #ddd;">
                                <div style="float:left;">应退金额：</div>
                                <div style="float:left;">
                                    <div id="txtReturnAmount" style=" line-height:28px;"></div>
                                </div>
                            </div>
                            <div class="z_card_row" style="margin-top:5px;">
                                <div style="float:left;">现金实退：</div>
                                <div style="float:left;">
                                    <div id="txtReturnCash" style="margin-top:5px;"></div>
                                </div>
                            </div>
                            <div class="z_card_row" style="">
                                <div style="float:left;">卡实退：</div>
                                <div style="float:left;">
                                    <div id="txtReturnCard" style="margin-top:5px; margin-left:12px;"></div>
                                </div>
                                <div style="float:left; margin-left:40px;">退单号：</div>
                                <div style="float:left;">
                                    <div id="txtReturnOrder" style="margin-top:5px;"></div>
                                </div>
                            </div>
                        </div>
                        <div style="height:30px; line-height:30px; padding-top:10px;">
                            <div id="btnClose" style="float:right;"></div>
                            <div id="btnSave" style="float:right;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
