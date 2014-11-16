<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>标签管理</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/TagsStatus.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/TagsType.js"%>"  type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/Tags/Controller/TagsCtl.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Tags/Model/TagsVM.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Tags/Store/TagsVMStore.js"%>"  type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Tags/View/TagsView.js"%>"  type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height:44px;">
                    <div id='span_panel' style="float:left; width:620px;"></div>
                    <div id='btn_panel' style="float:left; width:200px;"></div>
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
