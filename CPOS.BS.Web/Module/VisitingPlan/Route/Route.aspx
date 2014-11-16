<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>

<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>

<script src="Controller/RouteCtl.js" type="text/javascript"></script>
<script src="Model/RouteVM.js" type="text/javascript"></script>
<script src="Store/RouteVMStore.js" type="text/javascript"></script>
<script src="View/RouteView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
            <div class="view_Search">
                <span id='span_panel'></span>   
             </div>  
			    <div class="view_Search2">
                <span id='span_panel2'></span>   
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
