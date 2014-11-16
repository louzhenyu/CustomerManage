<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>奖品</title>
    <script src="/Framework/javascript/Biz/EventType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventRange.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventCheckinType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/QuestionnaireType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WeiXinPublic.js" type="text/javascript"></script>
    <script src="Controller/PrizesEditCtl.js" type="text/javascript"></script>
    <script src="Model/PrizesVM.js" type="text/javascript"></script>
    <script src="Store/EventsPrizesListVMStore.js" type="text/javascript"></script>
    <script src="View/PrizesEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 481px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width: 100%;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb" style="width: 100%;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>类型
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtPrizeType" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" id="lblCouponType" style="vertical-align: top; line-height: 22px;
                                    display: none">
                                    <font color="red">*</font>优惠劵
                                </td>
                                <td class="z_main_tb_td2" id="tCouponType" style="vertical-align: top; line-height: 22px;
                                    display: none">
                                    <div id="txtCouponType" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" id="lblPoint" style="vertical-align: top; line-height: 22px;
                                    display: none">
                                    <font color="red">*</font>积分
                                </td>
                                <td class="z_main_tb_td2" id="tPoint" style="vertical-align: top; line-height: 22px;
                                    display: none">
                                    <div id="txtPoint" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" id="lblMoney" style="vertical-align: top; line-height: 22px;
                                    display: none">
                                    <font color="red">*</font>现金
                                </td>
                                <td class="z_main_tb_td2" id="tMoney" style="vertical-align: top; line-height: 22px;
                                    padding-left: 15px; display: none">
                                    <div id="txtMoney" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width: 100px;
                                    min-width: 100px;">
                                    <font color="red">*</font>奖品名称
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width: 100px;">
                                    <div id="txtPrizeName" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; min-width: 100px;
                                    width: 100px;">
                                    奖品缩写
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width: 100px;">
                                    <div id="txtPrizeShortDesc" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; min-width: 100px;
                                    width: 100px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width: 100px;">
                                </td>
                            </tr>
                              <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    价格
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtPrice" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtDisplayIndex" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; min-width: 100px;
                                    width: 100px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width: 100px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>总数量
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtCountTotal" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    没中奖品数量
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtCountLeft" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; min-width: 100px;
                                    width: 100px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width: 100px;">
                                </td>
                            </tr>
                                <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    是否自动补充奖品
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="">
                                    <div id="txtIsAutoPrizes">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    奖品描述
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="">
                                    <div id="txtPrizeDesc">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    Logo超链接
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtLogoURL" style="">
                                    </div>
                                    <div style="position: absolute; left: 480px; top: 288px;">
                                        <input type="button" id="uploadLogo" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    图片超链接
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtImageUrl">
                                    </div>
                                    <div style="position: absolute; left: 480px; top: 320px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    内容文本
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtContentText" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    内容链接
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtContentUrl" style="">
                                    </div>
                                </td>
                            </tr>
                          
                        </table>
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
