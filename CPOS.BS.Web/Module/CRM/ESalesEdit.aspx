<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>销售线索</title>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EIndustry.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EScale.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipEnterpriseExpandStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UserGender.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EPolicyDecisionRole.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ECCustomerSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesProduct.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesChargeVip.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesStage2.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesVisitVip.js" type="text/javascript"></script>
    <script src="Controller/ESalesEditCtl.js" type="text/javascript"></script>
    <script src="Model/EEnterpriseCustomersVM.js" type="text/javascript"></script>
    <script src="Store/EEnterpriseCustomersVMStore.js" type="text/javascript"></script>
    <script src="View/ESalesEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
    <style type="text/css">
.ywBox{ border:1px solid #d7d6d6; background-color:#f7f9f9; width:850px;padding: 10px;}
.ywBoxList{ padding: 0px; position:relative; /*border-bottom:1px solid #d7d6d6;*/}
.ywBoxH2{ line-height:20px; margin:0; padding:0; color:#2a77bc; font-size:14px; padding-right:40px; font-family:Tahoma, Geneva, sans-serif;}
.ywBoxp{ line-height:22px; font-size:14px; color:#4f4f4f; padding:5px 0; padding-left:20px; padding-right:40px; word-wrap:break-word;}
.ywBoxDiv a,.ywBoxDiv a:hover{ color:#56a9f4; text-decoration:underline; line-height:24px; font-family:Tahoma, Geneva, sans-serif}
.ywBoxDiv{ padding:10px 0 0 0 ; font-size:14px;border-bottom:1px solid #d7d6d6; padding-bottom:5px; margin-bottom:5px; }
.showIcn{ background:url('/Framework/Image/ks.png') no-repeat left top; width:24px; height:25px; display:block; position:absolute; right:20px; top:7px; cursor:pointer}
.HideIcn{background:url('/Framework/Image/ks.png') no-repeat left -30px; width:24px; height:24px; display:block; position:absolute; right:20px; top:7px; cursor:pointer}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 291px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width:700px;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width:120px;">
                                    <font color="red">*</font>销售线索名称
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtSalesName" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>销售产品
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtSalesProductId" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>结束日期
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtEndDate" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    <font color="red">*</font>所属客户
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtEnterpriseCustomerId">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <%--<font color="red">*</font>联系人--%>
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="1">
                                    <%--<div id="txtSalesPerson" style=""></div>--%>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    销售负责人
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="1">
                                    <div id="txtSalesVipId" style=""></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>可能性(%)
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="vertical-align: top; line-height: 22px;">
                                    
                                    <div id="txtPossibility" style=""></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>阶段
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtStageId" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>业务机会来源
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtECSourceId" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    预计的金额
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtForecastAmount" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    备注
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtRemark">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px; text-align:right; padding-left:380px; padding-top:10px;">
                                    <div class="" id="divBtn" style="">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="margin-top:10px;">
                <div id="pnlVisitList" class="ywBox">
                </div>
            </div>
        </div>
    </div>
    
    <div style="position:absolute; left: 244px; top:54px;">
        <img src="../../Framework/Image/search.png" style="height:22px;width:22px; cursor:pointer;"
            onclick="fnECShowSearch()" />
        <input id="hECCustomerId" type="hidden" value="" />
    </div>
    <div id="cusECSearch" style="border:1px solid #666; width:400px; height:320px; display:none;
        position:absolute; left: 140px; top:80px; z-index:10000; background:#fff;">
        <div style="background:#1b8cf2; color:#fff; height:30px; line-height:30px; padding-left:10px; font-weight:bold;">
            <div style="float:left;width:200px;">搜索客户</div>
            <div style="float:right;width:30px; padding-top:3px;">
                <img src="../../Framework/Image/close.png" style="height:24px;width:24px; cursor:pointer;"
                    onclick="fnECCloseSearch()" />
            </div>
        </div>
        <div style="height: 40px; padding-top:10px;">
            <div style="float:left; width:190px;">
                <div id="tbECSearchCustomerName"></div>
            </div>
            <div style="float:left; width:70px;">
                <div id="tbECSearchCustomerGo"></div>
            </div>
            <div style="float:left; width:70px; padding-left:10px;">
                <div id="tbECSearchCustomerClear"></div>
            </div>
        </div>
        <div style="height:20px; padding-left:10px; color:#d0d0d0; clear:both; width:350px;">
            可使用"*"作为通配符跟在其它字符后面以提高搜索效率。
        </div>
        <div id="pnlECSearchCustomer" style="height:200px; clear:both; width:380px; height:200px; margin:10px; overflow:auto;">
        </div>
    </div>

</asp:Content>
