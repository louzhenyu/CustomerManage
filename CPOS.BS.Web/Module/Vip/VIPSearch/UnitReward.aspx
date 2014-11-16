<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>门店奖励</title>
    
    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Tags.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/TagsGroup.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SysIntegralSource.js" type="text/javascript"></script>

    <script src="Model/VipVM.js" type="text/javascript"></script>
    <script src="Store/VipSearchVMStore.js" type="text/javascript"></script>
    <script src="View/UnitRewardView.js" type="text/javascript"></script>
    <script src="Controller/UnitRewardCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search">
                    <div id='span_panel' style="float:left"></div>
                    <%--<div id="btnMore" class="z_btn3 opentwo" style="float:left; width:80px;" onclick="fnMoreSearchView()">更多</div>--%>
                    <div id='btn_panel2' style=" clear:both; width:220px;"></div>
                    <span style="clear:both; height:1px; overflow:hidden; display:block"></span>
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
