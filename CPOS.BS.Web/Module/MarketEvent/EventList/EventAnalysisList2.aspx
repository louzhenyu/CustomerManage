<%@ Page Title="活动分析" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动分析</title>
    <script src="Controller/EventAnalysisCtl.js" type="text/javascript"></script>
    <script src="Model/EventAnalysisVM.js" type="text/javascript"></script>
    <script src="Store/EventAnalysisVMStore.js" type="text/javascript"></script>
    <script src="View/EventAnalysisView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <div class="m10 article">
            
            <div class="art-titbutton" >
                <div class="view_Button" >
                    <span id='span_create'><H3>活动整体分析表</H3></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <br />
           <div style="border:0px solid red;text-align:center;padding-top:0px; padding-left:30px;" class="cb" id="divPanel" style=" vertical-align:middle; "></div>
        </div>
    </div>
    <%--<script type="text/javascript">
        top.fnChangeSize(1000,1400);
    </script>--%>
</asp:Content>
