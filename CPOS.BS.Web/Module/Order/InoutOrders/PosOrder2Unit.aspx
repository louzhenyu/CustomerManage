<%@ Page Title="POS小票" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title></title>
    <script src="/Framework/javascript/Biz/CustomerUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SupplierUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/Warehouse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/OrderNo.js" type="text/javascript"></script>
    <script src="Controller/PosOrder2UnitCtl.js" type="text/javascript"></script>
    <script src="Model/InoutOrderEntity.js" type="text/javascript"></script>
    <script src="Model/InoutOrderDetailItemVM.js" type="text/javascript"></script>
    <script src="Store/SalesOutOrderEditVMStore.js" type="text/javascript"></script>
    <script src="View/PosOrder2UnitView.js" type="text/javascript"></script>

    <style type="text/css">
*{ margin:0; padding:0; list-style:none; font-family:Arial, Helvetica, sans-serif}
.dttba td{ border-bottom:1px solid #cccccc; padding:10px 5px; line-height:20px; font-size:12px;}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <%--<div class="z_event_border" style="font-weight: ; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                <div style="float:left; width:40px;">门店</div>
                <div class="" id="txtUnit" style="float:left; width:40px; margin-top:6px;">
                </div>
            </div>--%>
            <div class="DivGridView" id="divBtn" style="margin-left:400px;margin-bottom:10px;">
            </div>
            <div style="">
                <table style="border:1px solid #ddd; width:600px;">
                    <tr>
                        <td colspan="6" class="z_su_1">订单信息</td>
                    </tr>
                    <tr>
                        <td class="z_su_2">订单编号:</td>
                        <td class="z_su_2"><div id="txtOrderNo"></div></td>
                        <td class="z_su_2">金额:</td>
                        <td class="z_su_2"><div id="txtOrderAmount"></div></td>
                        <td class="z_su_2"></td>
                        <td class="z_su_2"></td>
                    </tr>
                    <tr>
                        <td class="z_su_2">会员名:</td>
                        <td class="z_su_2"><div id="txtVipName"></div></td>
                        <td class="z_su_2">手机号:</td>
                        <td class="z_su_2"><div id="txtPhone"></div></td>
                        <td class="z_su_2">配送时间:</td>
                        <td class="z_su_2"><div id="txtSendTime"></div></td>
                    </tr>
                    <tr>
                        <td class="z_su_2">地址:</td>
                        <td class="z_su_2" colspan="5"><div id="txtAddress"></div></td>
                    </tr>
                </table>
            </div>
            <div class="z_su_1" style="border:1px solid #ddd; width:600px;border-top:0px solid #ddd; height:30px; line-height:30px;">
                配送门店智能匹配
            </div>
            <div id="txtList" style="width:600px;"></div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>
