<%@ Page Title="POS小票" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>POS小票</title>
    <style type="text/css">
        .txtDisable {
           background:Transparent; border:0px; border-bottom:dotted 1px #000;
        }
        .headerOver {
            cursor:pointer;
        }
    </style>

    <script src="<%=StaticUrl+"/Framework/javascript/Biz/CustomerUnit.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SupplierUnit.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/OrderStatus.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SkuPropCfg.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SkuSelect.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/biz/Warehouse.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/biz/OrderNo.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/Javascript/Biz/DeliveryUnit.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/Javascript/Biz/Delivery.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/Javascript/Biz/DefrayType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/Javascript/Biz/TUnit.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Options.js"%>" type="text/javascript"></script>


    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Controller/PosOrderEdit3Ctl.js?v=0.8"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Model/InoutOrderEntity.js?v=0.7"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Model/InoutOrderDetailItemVM.js?v=0.7"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Store/SalesOutOrderEditVMStore.js?v=0.7"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/View/PosOrderEdit3View.js?v=0.8"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Store/PosOrderEdit3VMStore.js?v=0.7"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Model/PosOrderEdit3Entity.js?v=0.7"%>" type="text/javascript"></script>

<%--    <script type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/jquery-1.9.0.min.js"%>" ></script>--%>
    <link rel="stylesheet" type="text/css" href="<%=StaticUrl+"/Framework/Javascript/Other/fancybox-v2/source/jquery.fancybox.css?v=2.1.4"%>" media="screen" />
    <link rel="stylesheet" type="text/css" href="<%=StaticUrl+"/Framework/Javascript/Other/fancybox-v2/source/helpers/jquery.fancybox-buttons.css?v=1.0.5"%>" />
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/fancybox-v2/lib/jquery.mousewheel-3.0.6.pack.js"%>" ></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/fancybox-v2/source/jquery.fancybox.js?v=2.1.4"%>" charset="gb2312"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/fancybox-v2/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"%>" ></script>
    
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
        <div class="z_event_border" style="font-weight: bold; height: 46px; line-height: 46px;
                 background: rgb(241, 242, 245);" id="divOperation" > 
            </div>
            <!--
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                订单主信息</div>
                -->
            <div class="DivGridView" id="divMain">
            </div>
            <!--
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                配送信息</div>
            -->
            <div class="DivGridView" id="divDetail">
            </div>
            <!--
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                支付信息</div>
            -->
            <div class="DivGridView" id="divpay">
            </div>
            <div class="DivGridView" id="divvip">
            </div>
            <div class="DivGridView" id="divvipMessage">
            </div>
            <div class="DivGridView" id="divDetail1">
                <!--<div style="float: left; width: 100%; margin-left: 0px; background: rgb(241, 242, 245);
                    border: 1px solid #d0d0d0; border-top: 0px;">
                    <div style="clear: both; width: 100%; padding-left: 10px; padding-right: 10px; padding-top: 10px;
                        padding-bottom: 10px;">
                        <div id="grid">
                        </div>
                    </div>
                </div>-->
            </div>
            <div id="grid"></div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
