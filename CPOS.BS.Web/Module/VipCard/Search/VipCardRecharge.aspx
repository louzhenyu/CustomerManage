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
    <script src="View/VipCardRechargeView.js" type="text/javascript"></script>
    <script src="Controller/VipCardRechargeCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div style="width:100%;">
                    <div class="" style="">
                        <div class="z_card_row" style="">
                            <div id="chkAmount1" style="float:left;width:30px;"></div>
                            <div class="z_card_td" style="float:left;min-width:70px;">现金实收：</div>
                            <div style="float:left;">
                                <div id="txtCashAmount" style="margin-top:0px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="border-bottom:1px solid #ddd;">
                            <div id="chkAmount2" style="float:left;width:30px;"></div>
                            <div class="z_card_td" style="float:left;min-width:70px;">刷卡实收：</div>
                            <div style="float:left;">
                                <div id="txtCardAmount" style="float:left; min-width:200px;margin-top:0px;"></div>
                            </div>
                            <div class="z_card_td" style="float:left;">小票号：</div>
                            <div style="float:left;">
                                <div id="txtOrderNo" style="font-weight:bold; line-height:30px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="font-weight:bold;border-bottom:1px solid #ddd; margin-top:7px;">
                            <div class="z_card_td" style="float:left; font-size:14px;">充值金额：</div>
                            <div style="float:left;">
                                <div id="txtRechargeAmount" style="font-weight:bold; line-height:30px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style=" margin-top:0px; height:100px;">
                            <div class="z_card_td" style="float:left; line-height:32px;">备注：</div>
                            <div style="float:left;">
                                <div id="txtRemark" style=""></div>
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
