<%@ Page Title="POS小票" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" 
Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>订单管理</title>

    <script src="/Framework/javascript/Biz/SupplierUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/Warehouse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/PosPayType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/PosSendType.js" type="text/javascript"></script>

    <script src="Controller/PosOrder2Ctl.js" type="text/javascript"></script>
    <script src="Model/InoutOrderEntity.js" type="text/javascript"></script>
    <script src="Store/SalesOutOrderVMStore.js" type="text/javascript"></script>
    <script src="View/PosOrder2View.js" type="text/javascript"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                 <div id="view_Search" class="view_Search" style="height:44px;">
                    <div id='span_panel' style="float:left; width:820px; overflow:hidden;"></div>
                    <div id='btn_panel' style="float:left; width:200px;"></div>
                </div>
            </div>
            <div class="art-titbutton" style="margin:0px; background:#E6E4E1;">
                <div class="view_Button" style="margin:0px; margin-top:10px; background:#E6E4E1;">
                    <div id='btn_excel' style="float:left; width:200px;"></div>
                </div>
            </div>
            <div class="art-titbutton" style="margin:0px; background:#E6E4E1;">
                
                <input id="hType" type="hidden" value="1" />
                <div class="view_Button" style="margin:0px; background:#E6E4E1;">
                    <div id="tab1" class="z_posorder_head" onclick="fnSearch1()">
                        <div style="width:100px;height:20px;">未付款</div>
                        <div id="txtNum1" style="height:24px;">0</div>
                    </div>
                    <div id="tab2" class="z_posorder_head" onclick="fnSearch2()">
                        <div style="width:100px;height:20px;">待处理</div>
                        <div id="txtNum2" style="height:24px;">0</div>
                    </div>
                    <div id="tab3" class="z_posorder_head" onclick="fnSearch3()">
                        <div style="width:100px;height:20px;">已发货</div>
                        <div id="txtNum3" style="height:24px;">0</div>
                    </div>
                    <div id="tab4" class="z_posorder_head" onclick="fnSearch4()">
                        <div style="width:100px;height:20px;">已取消</div>
                        <div id="txtNum4" style="height:24px;">0</div>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView1">
            </div>
            <div class="DivGridView" id="DivGridView2" style="display:none;">
            </div>
            <div class="DivGridView" id="DivGridView3" style="display:none;">
            </div>
            <div class="DivGridView" id="DivGridView4" style="display:none;">
            </div>
            <div class="cb">
            </div>
            <div style="height:48px; background:rgb(241, 242, 245); border:1px solid #ddd; 
                padding-top:10px; padding-right:10px; margin-top:10px; display:none;">
                <div id="btnOp1" style="float:right;"></div>
                <div id="btnOp2" style="float:right;"></div>
                <div id="btnOp3" style="float:right;"></div>
                <div id="btnOp4" style="float:right;"></div>
            </div>
        </div>
    </div>

</asp:Content>


