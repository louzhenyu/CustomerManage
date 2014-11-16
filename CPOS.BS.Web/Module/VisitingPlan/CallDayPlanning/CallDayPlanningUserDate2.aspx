<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>

<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>

<script src="Controller/CallDayPlanningUserDateCtl.js" type="text/javascript"></script>
<script src="Model/CallDayPlanningUserDateVM.js" type="text/javascript"></script>
<script src="Store/CallDayPlanningUserDateVMStore.js" type="text/javascript"></script>
<script src="View/CallDayPlanningUserDateView.js" type="text/javascript"></script>--%>

<script type="text/javascript">
    Ext.Loader.setConfig({
        enabled: true,
        paths: {
            'Ext.calendar': 'calendar/src'
        }
    });
    Ext.require([
            'Ext.calendar.App'
        ]);
    Ext.onReady(function () {
        // launch the app:
        Ext.create('Ext.calendar.App');

        // update the header logo date:

    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="m10 article">
            <div class="art-tit">
            <div class="view_Search">
                <span id='span_panel'></span>
             </div>
            </div>
            <div class="art-titbutton">
             <div class="view_Button">
                    <span id='span_create'></span>
                    <span id='span_cancel'></span>
               </div>
            </div>
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>

    

    <div style="display:none;">
    <div id="app-header-content">
        <div id="app-logo">
            <div class="logo-top">&nbsp;</div>
            <div id="logo-body">&nbsp;</div>
            <div class="logo-bottom">&nbsp;</div>
        </div>
        <h1>Ext JS Calendar</h1>
        <span id="app-msg" class="x-hidden"></span>
    </div>
    </div>
</asp:Content>