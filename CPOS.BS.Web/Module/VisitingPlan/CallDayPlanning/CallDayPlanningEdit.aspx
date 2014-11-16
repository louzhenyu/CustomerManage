<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<!--人员-->
<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script><script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script><script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script><script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>

<!--终端-->
<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Channel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Chain.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Category.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/HierarchyItem.js" type="text/javascript"></script>
<link rel="Stylesheet" type="text/css" href="/Lib/Javascript/Ext4.1.0/ux/css/CheckHeader.css" />
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>

<script src="Controller/CallDayPlanningEditCtl.js" type="text/javascript"></script>
<script src="Model/CallDayPlanningEditVM.js" type="text/javascript"></script>
<script src="Store/CallDayPlanningEditVMStore.js" type="text/javascript"></script>
<script src="View/CallDayPlanningEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="m10 article">
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>