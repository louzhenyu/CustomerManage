<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<%@ Register src="/Framework/WebControl/HeadRel.ascx" tagname="HeadRel" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<uc1:HeadRel ID="HeadRel1" runat="server" />

<!--for store control-->
<script src="/Framework/Javascript/pub/JITStoreFrmWindow.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Channel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Chain.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/HierarchyItem.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
    <link rel="Stylesheet" type="text/css" href="/Lib/Javascript/Ext4.1.0/ux/css/CheckHeader.css" />
<!--for store control-->

<script src="/Module/VisitingPlan/Route/Controller/RoutePOPMap_DistributorCtl.js" type="text/javascript"></script>
<script src="/Module/VisitingPlan/Route/Model/RoutePOPMap_DistributorVM.js" type="text/javascript"></script>
<script src="/Module/VisitingPlan/Route/Store/RoutePOPMap_DistributorVMStore.js" type="text/javascript"></script>
<script src="/Module/VisitingPlan/Route/View/RoutePOPMap_DistributorView.js" type="text/javascript"></script>

</head>
<body>
<div class="section" style="min-height:0px;height:auto;border:0;">
        <div class="m10 article">
            <div class="art-tit">
            <div class="view_Search">
                <span id='dvSearch'></span>
             </div>                
            </div>
            <div class="art-titbutton">
             <div class="view_Button">
                    <span id='tab2_span_set'></span>
                    <span id='tab2_span_save'></span>
               </div>
            </div>
                <div class="DivGridView" id="tab2_DivGridView">
                <div style="width:18%; float:left; border:1px solid #dbdbdb;" id="dvGrid"></div>
                <div style="width:80%; float:left; border:1px solid #dbdbdb;"><iframe  height="500px"  marginheight="0" marginwidth="0" frameborder="0" id="frmFlashMap"  width="100%" src="/Lib/MapFlash/VisitingPlan_routPOPIndex.html"></iframe></div>
                
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        Ext.onReady(function () {
            id = JITMethod.getUrlParam("ClientUserID") + "|" + JITMethod.getUrlParam("CallDate"); //searchcondition
            editMethod = "EditRoutePOPMap_Distributor";
            callBack.saveCallBack = function () {
                parent.tab2State = false;
            }
            JITPage.HandlerUrl.setValue("/Module/VisitingPlan/CallDayPlanning/Handler/CallDayPlanning_DistributorHandler.ashx?mid=" + __mid);

            InitVE();
            InitStore();
            InitView();

            btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();
        });
    </script>
</body>
</html>