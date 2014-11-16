<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>门店管理</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/CitySelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/Module/Basic/Unit/Controller/UnitCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Model/UnitVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Store/UnitVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/View/UnitView.js?v=0.3"%>" type="text/javascript"></script>
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
                <div id="view_Search" class="view_Search" style="">
                    <div id='span_panel' style="float:; width:820px;"></div>
                    <div id='btn_panel' style="float:; width:200px;"></div>
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
