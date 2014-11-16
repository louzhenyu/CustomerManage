<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true"  Inherits="JIT.CPOS.BS.Web.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>终端信息管理</title>
 <script type="text/javascript">
    var __clientid =<%=CurrentUserInfo.ClientID %>;
    var __clientdistributorid =<%=CurrentUserInfo.ClientDistributorID%>;
    var __photoType = "1";
</script>
 <link rel="Stylesheet" type="text/css" href="/Lib/Javascript/Ext4.1.0/ux/css/CheckHeader.css" />

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
    <script src="/Framework/javascript/Biz/MapSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/AutoNumber.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Photo.js" type="text/javascript"></script>
    <script src="View/StoreDef.js" type="text/javascript"></script>
    <script src="View/StoreView.js" type="text/javascript"></script>
    <script src="Controller/StoreCtl.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='dvSearch'></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                         <span id='dvWork'></span><span id='dvExport'></span>
                </div>
            </div>
            <div class="DivGridView" id="dvGrid">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
 
  <script language="javascript" type="text/javascript">
      Ext.Loader.setConfig({ enabled: true });
      Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
      Ext.require([
     'Ext.data.*',
     'Ext.util.*',
     'Ext.view.View',
    'Ext.selection.CellModel' , 
      'Ext.ux.CheckColumn'


]);
    pClientDistributorID=<%=CurrentUserInfo.ClientDistributorID %>;
  Ext.onReady(function () {
          InitView();


      });
    </script>
</asp:Content>
