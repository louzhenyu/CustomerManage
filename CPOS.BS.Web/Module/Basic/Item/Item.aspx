<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>商品管理</title>
   
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/YesNoStatus.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/ItemCategorySelectTree.js"%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Lib/Javascript/Jit/form/field/ComboTree.js?v=1.2"%>"></script>
    <script src="<%=StaticUrl+"/module/basic/Item/Controller/ItemCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Item/Model/ItemVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Item/Store/ItemVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Item/View/ItemView.js"%>" type="text/javascript"></script>
    <style type="text/css">
        .x-grid-back-belowIsPause
        {
            background-color: red !important;
            color: red;
        }
        .x-grid-back-belowIsItemCategory
        {
            background-color: orange !important;
            color: orange;
        }
      
        td {
        vertical-align: middle; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit" >
                <div id="view_Search" class="view_Search" style="width: 95%;">
                    <div id='span_panel' style="width: 80%; float: left;">
                    </div>
                    <div id='btn_panel' style="float: left; width: 20%;">
                    </div>
                </div>
            </div>
                <div style="height: 15px;"> <%--clear:both--%>
                    &nbsp;</div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_create'></span><span id='span_enable'></span><span id='span_disable'>
                    </span><span><span style="width: 20px; height: 20px; background-color: red; margin-left: 10px">
                        &nbsp&nbsp&nbsp&nbsp</span> </span><span>表示已过期/过期且父类被停用</span> <span style="width: 20px;
                            height: 20px; background-color: orange; margin-left: 10px">&nbsp&nbsp&nbsp&nbsp&nbsp</span>
                    <span>表示父类被停用</span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
