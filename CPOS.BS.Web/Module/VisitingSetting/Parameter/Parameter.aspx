<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>

<script src="Controller/ParameterCtl.js" type="text/javascript"></script>
<script src="Model/ParameterVM.js" type="text/javascript"></script>
<script src="Store/ParameterVMStore.js" type="text/javascript"></script>
<script src="View/ParameterView.js" type="text/javascript"></script>

<script src="View/OptionSelectView.js" type="text/javascript"></script>
<script src="Model/OptionSelectVM.js" type="text/javascript"></script>
<script src="Store/OptionSelectVMStore.js" type="text/javascript"></script>
<script src="Controller/OptionSelectCtl.js" type="text/javascript"></script>

<script src="Controller/ParameterEditCtl.js" type="text/javascript"></script>
<script src="Store/ParameterEditVMStore.js" type="text/javascript"></script>
<script src="View/ParameterEditView.js" type="text/javascript"></script>

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
