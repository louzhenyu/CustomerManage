<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>菜单管理</title>
    
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WMenuSelectTree.js" type="text/javascript"></script>

    <script src="Controller/WMenuCtl.js" type="text/javascript"></script>
    <script src="Model/WMenuVM.js" type="text/javascript"></script>
    <script src="Store/WMenuVMStore.js" type="text/javascript"></script>
    <script src="View/WMenuView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article" style="margin-bottom:0px;">
            <div class="art-titbutton">
                <input type="hidden" id="hAppId" value="" />
                <div class="view_Button" style="margin:0px;">
                    <div id='span_panel2' style="float:left; width:220px;"></div>
                    <div id='btn_panel2' style="float:left; width:200px;"></div>
                </div>
            </div>
        </div>
        <div style="float:left; width:20%;">
            <div style="background:rgb(241, 242, 245); margin:10px;">
                <div style="padding-left:4px; border:1px solid #c2c3c8; border-bottom:0px; cursor:pointer; color:blue;"
                    onclick="fnSearch2('')">回到根节点</div>
                <div id="span_tree"></div>
                <input id="tree_selected" type="hidden" />
            </div>
        </div>
        <div class="m10 article" style="float:left; width:77%;">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height:44px;">
                    <div id='span_panel' style="float:left; width:220px;"></div>
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
