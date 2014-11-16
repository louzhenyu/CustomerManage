<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>微信推送</title>
    <script src="/Framework/javascript/Biz/WModel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="Controller/EventVipPushWeixinCtl.js" type="text/javascript"></script>
    <%--    <script src="Model/EventVipPushWeixinVM.js" type="text/javascript"></script>
    <script src="Store/EventVipPushWeixinVMStore.js" type="text/javascript"></script>--%>
    <script src="View/EventVipPushWeixinView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div>
                <div id="tabsMain" style="width: 100%; height: 70px;">
                </div>
                <div id="tabInfo" style="height: 61px; background: rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb" style="width: 700px;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width: 100px;
                                    text-align: right;">
                                    微信账号：
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtApplicationId" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width: 100px;
                                    text-align: right;">
                                    <%--模块：--%>&nbsp
                                </td>
                                <td class="z_main_tb_td2">
                                    <%--    <div id="txtWModel" style="margin-top: 5px;">
                                    </div>--%>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <div id="btnSearch" style="margin-top: 5px;">
                                    </div>
                                    <input id="MaterialId" type="hidden" value="" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width: 80px;">
                                <font color="red">*</font>文本
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align: top; line-height: 32px;">
                                <div id="txtContent" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
