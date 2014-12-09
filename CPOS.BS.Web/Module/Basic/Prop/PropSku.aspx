<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>规格管理</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropDomain.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropInputFlag.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropSelectTree.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/Module/basic/prop/Controller/PropSkuCtl.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Model/PropVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Store/PropVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/View/PropSkuView.js?v=1"%>" type="text/javascript"></script>
      <style type="text/css">
        td {
        vertical-align: middle; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   

    <div class="section">
        <%--<div class="m10 article" style="margin-bottom:0px;">
            <div class="art-titbutton">
                 <input type="hidden" id="hAppId" value="SKU" />
                <input type="hidden" id="tree_prop_type" value="" />
                <div class="view_Button" style="margin:0px;">
                    <div id='span_panel2' style="float:left; width:220px;"></div>
                    <div id='btn_panel2' style="float:left; width:200px;"></div>
                </div>
            </div>
        </div>--%>

        <%--属性域--%>
        <input type="hidden" id="hAppId" value="SKU" />
        <%--查询类型--%>
        <input type="hidden" id="tree_prop_type" value="1" />
        <%--点击树节点时的查询信息--%>
        <input id="tree_selected" type="hidden" />
        <%--点击树节点时，改树类型--%>
        <input id="propInputFlag" type ="hidden">

        <div style="float:left; width:20%;">
            <div style="background:rgb(241, 242, 245); margin:10px;">               
                <div class="art-titbutton">
                    <div class="view_Button">
                        <span id='span_createsku'></span>
                    </div>
                </div>
                 <div style="padding-left:4px; border:1px solid #c2c3c8; border-bottom:0px; cursor:pointer; color:blue;"
                    onclick="fnSearch2('')">回到根节点</div>
                <div id="span_tree"></div>                
            </div>
        </div>
        <div class="m10 article" style="float:left; width:77%;">
            <div class="art-tit" style=" display:none;">
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
