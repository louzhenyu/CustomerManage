<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>菜单管理</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/AppSys.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/MenuSelectTree.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/menu/Controller/MenuCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/menu/Model/MenuVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/menu/Store/MenuVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/menu/View/MenuView.js"%>" type="text/javascript"></script>
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
