<%@ Page Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" 
Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>积分订单管理</title>

    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Model/IntegralOrderVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Store/IntegralOrderVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/View/IntegralOrderView.js"%>" type="text/javascript"></script> 
    <script src="<%=StaticUrl+"/Module/Order/InoutOrders/Controller/IntegralOrderCtl.js"%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                 <div id="view_Search" class="view_Search" style="height:44px;">
                    <div id='span_panel' style="float:left; width:820px; overflow:hidden;"></div>
                    <div id='btn_panel' style="float:left; width:200px;"></div>
                </div>
            </div>
            <div class="art-titbutton" style="margin:0px; background:#E6E4E1;">
                <div class="view_Button" style="margin:0px; margin-top:10px; background:#E6E4E1;">
                    <div id='btn_excel' style="float:left; width:200px;"></div>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>


