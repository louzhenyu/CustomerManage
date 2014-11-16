<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>角色管理</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/AppSys.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/role/Controller/RoleCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/role/Model/RoleVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/role/Store/RoleVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/role/View/RoleView.js"%>" type="text/javascript"></script>
      <style type="text/css">
        td {
        vertical-align: middle; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='span_panel'></span>
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
