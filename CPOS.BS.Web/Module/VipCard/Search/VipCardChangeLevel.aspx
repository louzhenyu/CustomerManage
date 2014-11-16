<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员信息</title>
    
    <script src="/Framework/javascript/Biz/VipCardExtensionYear.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipCardGrade.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipStatus.js" type="text/javascript"></script>

    <script src="Model/VipCardVM.js" type="text/javascript"></script>
    <%--<script src="Store/VipVMStore.js" type="text/javascript"></script>
    <script src="Store/VipEditVMStore.js" type="text/javascript"></script>--%>
    <script src="View/VipCardChangeLevelView.js" type="text/javascript"></script>
    <script src="Controller/VipCardChangeLevelCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div style="width:100%;">
                    <div class="" style="">
                        <div class="z_card_row" style="">
                            <div class="z_card_td" style="float:left;">会员卡号：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtVipCardCode" style="font-weight:bold; min-width:150px;"></div>
                            </div>
                            <div class="z_card_td" style="float:left;">卡等级：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtVipCardGrade" style="font-weight:bold;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div class="z_card_td" style="float:left;">账户余额：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtTotalAmount" style="float:left; color:; min-width:150px;font-weight:bold;"></div>
                            </div>
                            <div class="z_card_td" style="float:left;">卡状态：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtVipCardStatus" style="font-weight:bold;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="border-bottom:1px solid #ddd;">
                            <div class="z_card_td" style="float:left;">卡有效期：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtVipBeginDate" style="float:left;"></div>
                                <div id="" style="float:left;padding-left:4px;padding-right:4px;">到</div>
                                <div id="txtVipEndDate" style="float:left;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="margin-top:10px;">
                            <div class="z_card_td" style="float:left;"><font color="red">*</font>等级调整：</div>
                            <div class="z_card_td2" style="float:left;width:150px;">
                                <div id="txtExtensionYear" style="font-weight:bold;"></div>
                            </div>
                            <div class="z_card_td" style="float:left;">当前日期：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtToday" style="font-weight:bold;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div class="z_card_td" style="float:left;width:100px;">&nbsp;</div>
                            <div class="z_card_td2" style="float:left;width:150px;">
                                &nbsp;
                            </div>
                            <div class="z_card_td" style="float:left;">延期后有效期：</div>
                            <div class="z_card_td2" style="float:left;">
                                <div id="txtExtensionDay" style="font-weight:bold;"></div>
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
