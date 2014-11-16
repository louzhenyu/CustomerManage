<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title></title>
    <script src="/Framework/javascript/Biz/WModel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WQRCodeType.js" type="text/javascript"></script>
    <script src="Controller/WModelListEditCtl.js" type="text/javascript"></script>
    <script src="Model/WModelListVM.js" type="text/javascript"></script>
    <script src="Store/WModelListVMStore.js" type="text/javascript"></script>
    <script src="View/WModelListEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
                <div id="tabsMain" style="width: 100%; height: 460px;">
                </div>
                <div id="tabInfo" style="height: 451px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>微信账号
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtApplicationId" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>分类号码
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtModelCode">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>分类名称
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtModelName">
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
