<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" 
CodeBehind="VipLevel.aspx.cs" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>会员等级</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipLevel.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipSource.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Tags.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/TagsGroup.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/Vip/VipLevel/Model/VipLevelVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipLevel/Store/VipLevelVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipLevel/View/VipLevelView.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipLevel/Controller/VipLevelCtl.js"%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="section">
        <div class="m10 article">
           
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>
