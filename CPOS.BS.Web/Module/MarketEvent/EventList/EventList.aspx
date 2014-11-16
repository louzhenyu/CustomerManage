<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动列表</title>
    <script src="Controller/EventListCtl.js" type="text/javascript"></script>
    <script src="Model/EventListVM.js" type="text/javascript"></script>
    <script src="Store/EventListVMStore.js" type="text/javascript"></script>
    <script src="View/EventListView.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <%--<div class="art-tit">
                <div id="view_Search" class="view_Search" >
                      <span id='span_panel'></span>
                </div>
            </div>--%>
            <div class="art-titbutton" >
                <div class="view_Button" >
                    <span id='span_create'><H3>活动列表</H3></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

    <%--<div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='span1'></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span2'></span>
                </div>
            </div>
            <div class="DivGridView" id="Div1">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
--%>
</asp:Content>
