<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>会员奖励</title>

    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipLevel.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipSource.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Tags.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/TagsGroup.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SysIntegralSource.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Model/VipVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Store/VipSearchVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/View/VipRewardView.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Controller/VipRewardCtl.js"%>" type="text/javascript"></script>
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
                <div id="view_Search" class="view_Search">
                    <div style="min-height: 45px;">
                        <div id="btnMore" class="z_btn3 opentwo" style="float: right; width: 80px;margin-right:200px;" onclick="fnMoreSearchView()">更多</div>
                        <div id='span_panel' style="margin-right: 200px;"></div>
                    </div>
                <div id='btn_panel2'></div>

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
