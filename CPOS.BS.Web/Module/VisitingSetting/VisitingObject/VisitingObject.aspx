<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>

<script src="Controller/VisitingObjectCtl.js" type="text/javascript"></script>
<script src="Model/VisitingObjectVM.js" type="text/javascript"></script>
<script src="Store/VisitingObjectVMStore.js" type="text/javascript"></script>
<script src="View/VisitingObjectView.js" type="text/javascript"></script>
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
    <div style="display:block;height:0px;overflow:hidden;">
    <iframe id="tab1" frameborder="0" src=""></iframe>
    <iframe id="tab2" frameborder="0" src=""></iframe>
    </div>
</asp:Content>