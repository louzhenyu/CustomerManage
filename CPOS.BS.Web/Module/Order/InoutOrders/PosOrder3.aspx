<%@ Page Title="POS小票" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.Module.Order.InoutOrders.PosOrder3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>订单管理</title>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SupplierUnit.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/OrderStatus.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SkuPropCfg.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SkuSelect.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/biz/Warehouse.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/biz/PosPayType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/biz/PosSendType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Options.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipSource.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Controller/PosOrder3Ctl.js?v=0.43"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Model/InoutOrderEntity.js?v=0.3"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Store/SalesOutOrderVMStore.js?v=0.3"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/View/PosOrder3View.js?v=0.3"%>" type="text/javascript"></script>
      <style type="text/css">
        td {
        vertical-align: middle; 
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 44px;">
                    <div id='span_panel' style="float: left; width: 820px; overflow: hidden;">
                    </div>
                    <div id='btn_panel' style="float: left; width: 200px;">
                    </div>
                </div>
            </div>
            <div class="art-titbutton" style="margin: 0px; background: #E6E4E1;">
                <div class="view_Button" style="margin: 0px; margin-top: 10px; background: #E6E4E1;">
                    <div id='btn_Create' style="float: left; width: 100px;">
                    </div>
                    <div id='btn_excel' style="float: left; width: 100px;">
                    </div>
                    <div id='btn_SetUnit' style="float: left; width: 230px;">
                    </div>
                </div>
            </div>
            <div class="art-titbutton" style="margin: 0px; background: #E6E4E1;">
                <input id="hType" type="hidden" value="0" />
                <div class="view_Button" style="margin: 0px; background: #E6E4E1;">
                    <div id="tab0" class="z_posorder_head" onclick="fnGridSearch('0')">
                        <div style="width: 100px; height: 20px;">
                            全部</div>
                        <div id="txtNum0" style="height: 24px;">
                            0</div>
                    </div>
                    <div id='tablist'></div>

                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
            <div style="height: 48px; background: rgb(241, 242, 245); border: 1px solid #ddd;
                padding-top: 10px; padding-right: 10px; margin-top: 10px; display: none;">
                <div id="btnOp1" style="float: right;">
                </div>
                <div id="btnOp2" style="float: right;">
                </div>
                <div id="btnOp3" style="float: right;">
                </div>
                <div id="btnOp4" style="float: right;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
