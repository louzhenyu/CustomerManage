<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>会员列表</title>
    
    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Tags.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/TagsGroup.js" type="text/javascript"></script>

    <script src="Model/VipVM.js" type="text/javascript"></script>
    <script src="Store/VipSearchVMStore.js" type="text/javascript"></script>
    <script src="View/VipSearchView.js" type="text/javascript"></script>
    <script src="Controller/VipSearchCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search">
                    <div id='span_panel' style="float:left"></div>
                    
                    <div id='btn_panel' style=" float:left; width:220px;"></div>
                    <span style="clear:both; height:1px; overflow:hidden; display:block"></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div style="float:left; height:32px; clear:both; visibility:hidden;" id="divTags">
                    <div style="float:left; margin-left:20px; margin-top:10px; width:60px;">已选：</div>
                    <div style="float:left; width:570px;">
                        <div id="txtAddedTags" style="float:left; margin-top:8px; margin-left:0px; 
                        min-width:550px;max-width:550px; min-height:26px; max-height:42px; overflow:auto;
                        border:1px solid #d0d0d0; line-height:24px; padding-left:4px; padding-right:4px;">
                        </div>
                    </div>
                    <div id="btnCancel" style="float:right;margin-top:8px; "> </div>
                </div>
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
