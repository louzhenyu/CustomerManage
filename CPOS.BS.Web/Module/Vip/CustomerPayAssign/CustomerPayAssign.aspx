<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>支付分成列表</title>
    
    <script src="/Framework/javascript/Biz/PaymentType.js" type="text/javascript"></script>

    <script src="Model/CustomerPayAssignVM.js" type="text/javascript"></script>
    <script src="Store/CustomerPayAssignVMStore.js" type="text/javascript"></script>
    <script src="View/CustomerPayAssignView.js" type="text/javascript"></script>
    <script src="Controller/CustomerPayAssignCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search">
                    <div id='span_panel' style="float:left"></div>
                    
                    <div id='btn_panel' style=" float:left; width:220px;">
                    </div>
                    <span style="clear:both; height:1px; overflow:hidden; display:block"></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                        <div id="btnAdd"></div>
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
