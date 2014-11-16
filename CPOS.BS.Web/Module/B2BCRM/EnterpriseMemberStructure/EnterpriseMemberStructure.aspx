<%@ Page Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/Javascript/Biz/EnterpriseMemberStructureSelectTree.js" type="text/javascript"></script>
    <script src="Controller/EnterpriseMemberStructureCtl.js" type="text/javascript"></script>
    <script src="Model/EnterpriseMemberStructureVM.js" type="text/javascript"></script>
    <script src="Store/EnterpriseMemberStructureVMStore.js" type="text/javascript"></script>
    <script src="View/EnterpriseMemberStructureView.js" type="text/javascript"></script>
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
            <div class="view_Search2">
                    <span id='span_panel3'></span>
                </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
