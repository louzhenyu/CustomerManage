<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
 Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>结算凭证</title>

    <script src="/Framework/javascript/Biz/CustomerUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/Warehouse.js" type="text/javascript"></script>

    <script src="Controller/VoucherOrderCtl.js" type="text/javascript"></script>
    <script src="Model/InoutOrderEntity.js" type="text/javascript"></script>
    <script src="Store/VoucherOrderVMStore.js" type="text/javascript"></script>
    <script src="View/VoucherOrderView.js" type="text/javascript"></script>
    

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
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_create'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>