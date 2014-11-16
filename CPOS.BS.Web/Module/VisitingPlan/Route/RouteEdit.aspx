<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<%@ Register src="/Framework/WebControl/HeadRel.ascx" tagname="HeadRel" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<uc1:HeadRel ID="HeadRel1" runat="server" />

<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>

<!--for user control-->
<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script><script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script><script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script><script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>
<!--for user control-->

<!--for store control-->
<script src="/Framework/javascript/Biz/CheckboxModel.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITDynamicGrid.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Channel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Chain.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Category.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/HierarchyItem.js" type="text/javascript"></script>
<!--for store control-->

<script src="Controller/RouteEditCtl.js" type="text/javascript"></script>
<script src="Model/RouteEditVM.js" type="text/javascript"></script>
<script src="Store/RouteEditVMStore.js" type="text/javascript"></script>
<script src="View/RouteEditView.js" type="text/javascript"></script>

</head>
<body>
<div class="section" style="min-height:0px;height:auto;border:0;">
        <div id="DivGridView">
                </div>
    </div>
</body>
</html>