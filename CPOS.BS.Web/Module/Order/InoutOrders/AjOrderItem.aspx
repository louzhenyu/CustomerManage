<%@ Page Title="调整商品" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" AutoEventWireup="true" 
Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>调整商品</title>

    <script src="/Framework/javascript/Biz/SupplierUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ItemStockBalanceNo.js" type="text/javascript"></script>

    <script src="Controller/AjOrderItemCtl.js" type="text/javascript"></script>
    <script src="Model/InoutOrderDetailItemVM.js" type="text/javascript"></script>
    <script src="Model/InoutOrderEntity.js" type="text/javascript"></script>
    <script src="Store/PurchaseInOrderItemVMStore.js" type="text/javascript"></script>
    <script src="View/AjOrderItemView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">

            <div style="width:100%; padding:10px; padding-bottom:0px; 
                background:rgb(241, 242, 245); border:1px solid #d0d0d0;">
                <div style="width:100%; padding-left:10px; padding-right:10px;">
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>商品代码</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtItemCode" style="margin-top:0px;"></div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">商品名称</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtItemName"></div>
                            </td>
                        </tr>
                        <tr id="rowSkuProp1" class="z_main_tb_tr" style="display:none;">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                <span id="lblSkuProp1" style="display:none;"></span>
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtSkuProp1"></div>
                            </td>
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                <span id="lblSkuProp2" style="display:none;"></span>
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtSkuProp2"></div>
                            </td>
                        </tr>
                        <tr id="rowSkuProp2" class="z_main_tb_tr" style="display:none;">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                <span id="lblSkuProp3" style="display:none;"></span>
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtSkuProp3"></div>
                            </td>
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                <span id="lblSkuProp4" style="display:none;"></span>
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtSkuProp4"></div>
                            </td>
                        </tr>
                        <tr id="rowSkuProp3" class="z_main_tb_tr" style="display:none;">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                <span id="lblSkuProp5" style="display:none;"></span>
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtSkuProp5"></div>
                            </td>
                            <td class="z_main_tb_td">

                            </td>
                            <td>

                            </td>
                        </tr>
                       
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>库存数量</td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtRequestNum"></div>
                              
                            </td>
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>调整数</td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                <div id="txtInNum"></div>
                                
                            </td>
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">

                            </td>
                        </tr>
                     
                    </table>
                </div>
            </div>
            <div style="width:200px; line-height:22px; margin:10px;">
                <div style="float:left; width:80px; line-height:32px; margin-left:0px;">
                    <div id="btnSave" class="button" style="float:left; padding:0px;"></div>
                </div>
                <div style="float:left; width:80px; line-height:32px; margin-left:10px;">
                    <div id="btnClose" class="button" style="float:left; margin-left:0px; padding:0px;"></div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
