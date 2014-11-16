<%@ Page Title="活动分析" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动分析</title>
    <%--<script src="Controller/EventAnalysisCtl.js" type="text/javascript"></script>
    <script src="Model/EventAnalysisVM.js" type="text/javascript"></script>
    <script src="Store/EventAnalysisVMStore.js" type="text/javascript"></script>
    <script src="View/EventAnalysisView.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        fnChangeSize = function(height) {
            get("frmRep").style.height = parseInt(height)+100+"px";
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <iframe id="frmRep" src="http://o2oapi.aladingyidong.com/wap/Event/MarketEvent20131109/c.htm?marketEventId=<%=Request.QueryString["id"] %>&customerId=<%=CurrentUserInfo.CurrentUser.customer_id %>" 
                style="width:100%;min-width:800px;height:2800px;border:0px;margin:0px;padding:0px;overflow:hidden;"></iframe>
       

</asp:Content>
