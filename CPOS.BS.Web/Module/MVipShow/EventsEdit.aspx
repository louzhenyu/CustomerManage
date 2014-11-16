<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员秀内容</title>
    <script src="/Framework/javascript/Biz/EventType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/LEventSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventRange.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventCheckinType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/QuestionnaireType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WeiXinPublic.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ItemSelect.js" type="text/javascript"></script>
    <script src="Controller/EventsEditCtl.js" type="text/javascript"></script>
    <script src="Model/EventsVM.js" type="text/javascript"></script>
    <script src="Store/EventsEditVMStore.js" type="text/javascript"></script>
    <script src="View/EventsEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 421px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    会员名
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtVipName" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    赞的数量
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtPraiseCount">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    提交时间
                                </td>
                                <td class="z_main_tb_td2" style=" " colspan="3">
                                    <div id="txtCreateTime" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    审核状态
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtPass">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    获取的抽奖号码
                                </td>
                                <td class="z_main_tb_td2" style=" " colspan="3">
                                    <div id="txtLotteryCode" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    商品
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtItemCode">
                                    </div>
                                    <input type="hidden" id="hItemId" value="" />
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    心得
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;padding-bottom:10px; 
                                    padding-left:15px;">
                                    <div id="txtContent">
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
