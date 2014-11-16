<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>票务详情</title>
    <script src="/Framework/javascript/Biz/LEventSelectTree.js" type="text/javascript"></script>

    <script src="Controller/TicketEditCtl.js" type="text/javascript"></script>
    <script src="Model/TicketVM.js" type="text/javascript"></script>
    <script src="Store/TicketVMStore.js" type="text/javascript"></script>
    <script src="View/TicketEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section1">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 250px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="height: 200px;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>票务名称
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtTicketName" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>活动标题
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtEventID" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>价格
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtTicketPrice" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>数量
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtTicketNum" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    <font color="red">*</font>排序
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtTicketSort" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    票务备注
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;padding-bottom:10px;">
                                    <div id="txtTicketRemark">
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
