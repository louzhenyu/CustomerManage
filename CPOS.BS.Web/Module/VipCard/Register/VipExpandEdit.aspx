<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>车信息</title>
    <script src="/Framework/javascript/Biz/CarBrand.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CarModels.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CompartmentsForm.js" type="text/javascript"></script>
    <script src="Controller/VipExpandEditCtl.js" type="text/javascript"></script>
    <script src="Model/RegisterVM.js" type="text/javascript"></script>
    <script src="Store/RegisterVMStore.js" type="text/javascript"></script>
    <script src="View/VipExpandEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 321px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>车牌号
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtLicensePlateNo" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    车架号
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtChassisNumber" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>车品牌
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCarBrand" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>车型
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtCarModels" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    车厢形式
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCompartmentsForm" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    购买时间
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtPurchaseTime" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    备注
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="5">
                                    <div id="txtRemark" style="margin-top: 5px;">
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
