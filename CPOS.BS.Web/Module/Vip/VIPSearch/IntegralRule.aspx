<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>积分规则列表</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SysIntegralSource.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Model/IntegralRuleVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Store/IntegralRuleVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/View/IntegralRuleView.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Controller/IntegralRuleCtl.js"%>" type="text/javascript"></script>
      <style type="text/css">
        td {
        vertical-align: middle; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit" style="height: 50px;">
                <div id="view_Search" class="view_Search">
                    <div id='span_panel' style="float:left"></div>
                    
                    <div id='btn_panel' style=" float:left; width:220px;"></div>
                    <span style="clear:both; height:1px; overflow:hidden; display:block"> </span>
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
