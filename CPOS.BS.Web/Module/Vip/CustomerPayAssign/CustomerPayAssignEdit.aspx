<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>信息</title>
    
    <script src="/Framework/javascript/Biz/PaymentType.js" type="text/javascript"></script>

    <script src="Model/CustomerPayAssignVM.js" type="text/javascript"></script>
    <script src="Store/CustomerPayAssignVMStore.js" type="text/javascript"></script>
    <script src="View/CustomerPayAssignEditView.js" type="text/javascript"></script>
    <script src="Controller/CustomerPayAssignEditCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div id="tabsMain" style="width:100%; height:460px;"></div>
                <div id="tabInfo" style="height:451px; background:rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                    <font color="red">*</font>支付方式</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtPaymentTypeId" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                    <font color="red">*</font>客户帐号</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtCustomerAccountNumber" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"></td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;width: 120px;">
                                    <font color="red">*</font>客户分成比例</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtCustomerProportion" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width: 120px;">
                                    <font color="red">*</font>杰亦特截留比例</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtJITProportion"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">

                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">说明备注</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;" colspan="5">
                                    <div id="txtRemark"></div>
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
