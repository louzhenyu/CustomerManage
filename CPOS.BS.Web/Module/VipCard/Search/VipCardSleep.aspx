﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
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
    <script src="View/VipCardSleepView.js" type="text/javascript"></script>
    <script src="Controller/VipCardSleepCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div style="width:100%;">
                    <div class="" style="">
                        <div class="z_card_row" style="">
                            <div style="float:left;">会员卡号：</div>
                            <div style="float:left; min-width:200px;">
                                <div id="txtVipCardCode" style="font-weight:bold; line-height:28px;"></div>
                            </div>
                            <div style="float:left;">卡等级：</div>
                            <div style="float:left;">
                                <div id="txtVipCardGrade" style="font-weight:bold; line-height:30px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left;">账户余额：</div>
                            <div style="float:left;">
                                <div id="txtTotalAmount" style="float:left; color:; min-width:200px;font-weight:bold; line-height:28px;"></div>
                            </div>
                            <div style="float:left;">卡状态：</div>
                            <div style="float:left;">
                                <div id="txtVipCardStatus" style="font-weight:bold; line-height:30px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="border-bottom:1px solid #ddd;">
                            <div style="float:left;">卡有效期：</div>
                            <div style="float:left;">
                                <div id="txtVipBeginDate" style="font-weight:; line-height:30px;float:left;"></div>
                                <div id="" style="line-height:30px;float:left;padding-left:4px;padding-right:4px;">到</div>
                                <div id="txtVipEndDate" style="font-weight:; line-height:30px;float:left;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left;">当前日期：</div>
                            <div style="float:left;">
                                <div id="txtToday" style=" line-height:30px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left;">休眠期限：</div>
                            <div style="float:left;">
                                <div id="txtDormancyTime" style="margin-top:5px;"></div>
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
