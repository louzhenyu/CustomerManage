<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>会员卡查询</title>
    
<%--    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipSource.js" type="text/javascript"></script>--%>

    <script src="Model/VipCardVM.js" type="text/javascript"></script>
    <script src="Store/VipCardSearchVMStore.js" type="text/javascript"></script>
    <script src="View/VipCardSearchView.js" type="text/javascript"></script>
    <script src="Controller/VipCardSearchCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div>
                <div id="tabsMain" style="width:100%; height:70px;"></div>
                <div id="tabInfo" style="height:61px; background:rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb" style="width:100%;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">会员卡号：</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtSearchVipCardCode" style="margin-top:5px;font-weight:;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">会员姓名：</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtSearchVipName" style="margin-top:5px;font-weight:;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">车牌号：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtSearchCarCode" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                    <div id="btnSearch" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="btnReset" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
            <div style="margin-top:10px;">
                <div id="tabsMain2" style="width:100%; height:135px;"></div>
                <div id="tabInfo2" style="height:106px; background:rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb" style="width:100%;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">会员卡号：</td>
                                <td class="z_main_tb_td2" style="width:130px;">
                                    <div id="txtVipCardCode" style="margin-top:0px;font-weight:bold;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">会员姓名：</td>
                                <td class="z_main_tb_td2" style="width:130px;">
                                    <div id="txtVipName" style="margin-top:0px;font-weight:bold;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">联系电话：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;width:130px;">
                                    <div id="txtPhone" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">开卡时间：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;width:130px;">
                                    <div id="txtBeginDate" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">有效期：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;width:130px;">
                                    <div id="txtEndDate" style="margin-top:0px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">会籍店：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtUnitName" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">账户余额：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtBalanceAmount" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">可用积分：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtIntegration" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">卡等级：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtVipCardGrade" style="margin-top:0px;font-weight:bold;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">卡状态：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtVipCardStatus" style="margin-top:0px;font-weight:bold;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:120px;">最近消费时间：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtLastSalesTime" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:120px;">累计下单数：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtPurchaseTotalCount" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:120px;">累计消费金额：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtSalesTotalAmount" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:120px;">累计充值金额：</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    <div id="txtPurchaseTotalAmount" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:22px;">
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
            <div style="margin-top:10px; clear:both;">
                <div id="tabsMain3" style="width:100%; height:250px;"></div>
                <div id="tabInfo3" style="height:231px; background:rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div class="DivGridView" id="gridVipCardSales">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_2" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div class="DivGridView" id="gridVipCardRechargeRecord">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_3" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div class="DivGridView" id="gridVipCardGradeChangeLog">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_4" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div class="DivGridView" id="gridVipCardStatusChangeLog">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_5" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div class="DivGridView" id="gridVipExpand">
                        </div>
                    </div>
                </div>
            </div>

            <div style="height:48px; background:rgb(241, 242, 245); border:1px solid #ddd; 
                padding-top:10px; padding-right:10px; margin-top:10px;">
                <div id="btnOp1" style="float:left;"></div>
                <div id="btnOp2" style="float:left;"></div>
                <div id="btnOp3" style="float:left;"></div>
                <div id="btnOp8" style="float:right;"></div>
                <div id="btnOp7" style="float:right;"></div>
                <div id="btnOp6" style="float:right;"></div>
                <div id="btnOp5" style="float:right;"></div>
                <div id="btnOp4" style="float:right;"></div>
            </div>

        </div>
    </div>

</asp:Content>
