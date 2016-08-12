<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>华硕订单</title>
    <script src="/Framework/javascript/Biz/CustomerUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SupplierUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/OrderStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/Warehouse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/biz/OrderNo.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Biz/DeliveryUnit.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Biz/Delivery.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Biz/DefrayType.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Biz/TUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="Controller/OrdersEditCtl.js" type="text/javascript"></script>
    <script src="Model/OrdersEditVM.js" type="text/javascript"></script>
    <script src="Store/OrdersEditVMStore.js" type="text/javascript"></script>
    <script src="View/OrdersEditView.js" type="text/javascript"></script>
    <!--begin fancybox-v2-->
    <link rel="stylesheet" type="text/css" href="/Framework/Javascript/Other/fancybox-v2/source/jquery.fancybox.css?v=2.1.4"
        media="screen" />
    <link rel="stylesheet" type="text/css" href="/Framework/Javascript/Other/fancybox-v2/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" />
    <script type="text/javascript" src="/Framework/Javascript/Other/fancybox-v2/lib/jquery.mousewheel-3.0.6.pack.js"></script>
    <script type="text/javascript" src="/Framework/Javascript/Other/fancybox-v2/source/jquery.fancybox.js?v=2.1.4"
        charset="gb2312"></script>
    <script type="text/javascript" src="/Framework/Javascript/Other/fancybox-v2/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>
    <!--end fancybox-v2-->
    <style type="text/css">
        .a_b_aa { height: 200px; padding: 2px; border: 1px solid #D7D7D7; }
        .a_b_aa a { display: block; width: 136px; height: 136px; overflow: hidden; float: left; margin-right: 20px; margin-top:10px; }
        .a_b_aa img { width: 136px; margin-bottom:10px }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="z_event_border" style="font-weight: bold; height: 46px; line-height: 46px;
                background: rgb(241, 242, 245);" id="divOperation">
            </div>
            <div class="DivGridView" id="divMain">
            </div>
            <div class="DivGridView" id="divDetail">
                <div class="a_b_aa">
                </div>
                <ul>
                    <li></li>
                </ul>
            </div>
            <div class="DivGridView" id="divpay">
            </div>
            <div class="DivGridView" id="divvip">
            </div>
            <div class="DivGridView" id="divvipMessage">
            </div>
            <div class="DivGridView" id="divDetail1">
            </div>
            <div id="grid">
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
