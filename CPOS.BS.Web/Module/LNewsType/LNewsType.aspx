<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>资讯类型设置</title>
    <script src="<%=StaticUrl+"/Framework/Javascript/Biz/LNewsTypeSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Options.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/LnewsType/Controller/LNewsTypeCtl.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/LnewsType/Model/LNewsTypeVM.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/LnewsType/Store/LNewsVMStore.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/LnewsType/View/LNewsTypeView.js?v=0.2"%>" type="text/javascript"></script>
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
                    <span id='search_form_panel'></span>
                </div>
                <div class="view_Search2">
                    <span id='search_button_panel'></span>
                </div>
            
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='dvWork'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
