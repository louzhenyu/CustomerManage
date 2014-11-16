<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动人员</title>
    <script src="Model/EventsVM.js" type="text/javascript"></script>   
    <script src="Store/EventsUserListVMStore.js" type="text/javascript"></script>
    <script src="View/EventsUserListView2.js" type="text/javascript"></script>
    <script src="Controller/EventsUserListCtl2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section1">
        <div class="m10 article" >         
            <div class="DivGridView" id="wdivBtn">
            </div>
        </div>
    </div>
</asp:Content>
