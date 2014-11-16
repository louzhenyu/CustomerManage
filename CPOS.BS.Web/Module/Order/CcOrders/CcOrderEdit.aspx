<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>库存盘点</title>
    <script src="/Framework/javascript/Biz/Warehouse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>
    <script src="Controller/CcOrderEditCtl.js" type="text/javascript"></script>
    <script src="Model/CcOrderVM.js" type="text/javascript"></script>
    <script src="Model/CcOrderDetailItemVM.js" type="text/javascript"></script>
    <script src="Store/CcOrderEditVMStore.js" type="text/javascript"></script>
    <script src="View/CcOrderEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="DivGridView" id="divMain">
            </div>
            <div class="DivGridView" id="divDetail">
                <div style="float: left; width: 100%; margin-left: 0px; background: rgb(241, 242, 245);
                    border: 1px solid #d0d0d0; border-top: 0px;">
                    <div style="float: left; width: 90px; margin-left: 0px; padding-right: 0px; text-align: right;
                        vertical-align: top;">
                        商品明细：
                    </div>
                    <div style="float: left; width: 80px; line-height: 32px; margin-left: 0px;">
                        <div id="btnGetItems" class="button" style="float: left; padding: 0px;">
                        </div>
                    </div>
                    <div style="float: left; width: 80px; line-height: 32px; margin-left: 10px;">
                        <div id="btnAddItem" class="button" style="float: left; margin-left: 0px; padding: 0px;">
                        </div>
                    </div>
                    <div style="clear: both; width: 100%; padding-left: 10px; padding-right: 10px; padding-top: 10px;
                        padding-bottom: 10px;">
                        <div id="grid">
                        </div>
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
