<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>用户管理</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UserGender.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/user/Controller/UserCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Model/UserVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Store/UserVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/View/UserView.js"%>" type="text/javascript"></script>
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
