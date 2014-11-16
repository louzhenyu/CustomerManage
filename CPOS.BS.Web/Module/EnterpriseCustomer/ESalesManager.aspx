<%@ Page Title="销售线索" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" 
Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>销售线索</title>
    <script src="Controller/ESalesManagerCtl.js" type="text/javascript"></script>
    <script src="Model/ESalesManagerVM.js" type="text/javascript"></script>
    <script src="Store/ESalesManagerVMStore.js" type="text/javascript"></script>
    <script src="View/ESalesManagerView.js" type="text/javascript"></script>

    <script src="/Framework/javascript/Biz/ESalesProduct.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesStage.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesChargeVip.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ECCustomerSelect.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 86px;">
                    <div id='span_panel' style="float:left; width: 820px; overflow: hidden;">
                    </div>
                    <div id='btn_panel2' style="float:left; width: 200px;  ">
                    </div>
                    <div id='btn_panel' style="clear:both; width: 220px;">
                    </div>
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
    <div style="position:absolute; left: 622px; top:83px;">
        <img src="../../Framework/Image/search.png" style="height:22px;width:22px; cursor:pointer;"
            onclick="fnECShowSearch()" />
        <input id="hECCustomerId" type="hidden" value="" />
    </div>
    <div id="cusECSearch" style="border:1px solid #666; width:400px; height:320px; display:none;
        position:absolute; left: 519px; top:110px; z-index:10000; background:#fff;">
        <div style="background:#1b8cf2; color:#fff; height:30px; line-height:30px; padding-left:10px; font-weight:bold;">
            <div style="float:left;width:200px;">搜索客户</div>
            <div style="float:right;width:30px; padding-top:3px;">
                <img src="../../Framework/Image/close.png" style="height:24px;width:24px; cursor:pointer;"
                    onclick="fnECCloseSearch()" />
            </div>
        </div>
        <div style="height: 40px; padding-top:10px;">
            <div style="float:left; width:190px;">
                <div id="tbECSearchCustomerName"></div>
            </div>
            <div style="float:left; width:70px;">
                <div id="tbECSearchCustomerGo"></div>
            </div>
            <div style="float:left; width:70px; padding-left:10px;">
                <div id="tbECSearchCustomerClear"></div>
            </div>
        </div>
        <div style="height:20px; padding-left:10px; color:#d0d0d0; clear:both; width:350px;">
            可使用"*"作为通配符跟在其它字符后面以提高搜索效率。
        </div>
        <div id="pnlECSearchCustomer" style="height:200px; clear:both; width:380px; height:200px; margin:10px; overflow:auto;">
        </div>
    </div>
</asp:Content>
