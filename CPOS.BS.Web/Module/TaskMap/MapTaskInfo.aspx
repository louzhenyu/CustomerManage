<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title></title>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="Model/MapTaskInfoVM.js" type="text/javascript"></script>
    <script src="Store/MapTaskInfoVMStore.js" type="text/javascript"></script>
    <script src="View/MapTaskInfoView.js" type="text/javascript"></script>
    <script src="Controller/MapTaskInfoCtl.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div id="btnClose" onclick="fnClose()" style="position:absolute; top:5px; left:465px; cursor:pointer;">关闭</div>--%>
    <div id="btnClose" onclick="fnClose()" style="position:absolute; top:0px; left:168px; cursor:pointer;">关闭</div>
<div id="pnl" style="padding:14px; height:130px; padding-top:16px; ">
    <div style="height:17px;line-height:17px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:4px; clear:both;">
        <div style="float:left; width:70px;text-align:left; padding-right:0px;">取衣地址：</div>
        <div id="txtAddress" style="float:left; width:100px;overflow:hidden;margin-top:-1px; max-width:160px;"></div>
    </div>
    <%--<div style="height:17px; line-height:17px; font-size:13px; color:#000; font-weight:; padding-top:2px; clear:both; padding-left:3px;">
    </div>--%>
    <%--<div class="z_tk_1" style="height:5px;"></div>--%>
    <div style="height:17px; line-height:17px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:7px; clear:both;">
        <div style="float:left; width:50px;text-align:left; padding-right:0px;">时间：</div>
        <div id="txtTime" style="float:left; width:;margin-top:-2px;"></div>
    </div>
    <div style="height:17px; line-height:17px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:7px; clear:both;">
        <div style="float:left; width:50px;text-align:left; padding-right:0px;">件数：</div>
        <div id="txtOrderQty2" style="float:left; width:;margin-top:-2px;"></div>
    </div>
    <div style="height:17px; line-height:17px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:7px; clear:both;">
        <div style="float:left; width:50px;text-align:left; padding-right:0px;">手机：</div>
        <div id="txtPhone" style="float:left; width:;margin-top:-2px;"></div>
    </div>
    <div style="height:17px; line-height:17px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:7px; clear:both;">
        <div style="float:left; width:50px;text-align:left; padding-right:0px;">人员：</div>
        <div id="txtUser" style="float:left; width:;margin-top:-2px;"></div>
    </div>
    <%--<div style="height:17px; line-height:17px; font-size:13px; color:#000; font-weight:; padding-top:2px; clear:both; padding-left:3px;">
    </div>--%>
    <div style="height:37px; padding-top:20px;">
        <div id="btnSend"></div>
    </div>
</div>
<div id="pnl2" style="padding:14px; display:none;">
    <div style="height:25px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:2px;">
        <div style="float:left; width:80px;text-align:right; height:25px; padding-right:6px;">订单号：</div>
        <div id="txtOrderNo" style="float:left; width:150px; height:23px;margin-top:-2px;">&nbsp;</div>
        <div style="float:left; width:80px;text-align:right; height:25px; padding-right:6px;">衣物数量：</div>
        <div id="txtOrderQty" style="float:left; width:100px; height:23px;margin-top:-1px;">&nbsp;</div>
    </div>
    <div style="height:20px; font-size:13px; color:#1566ca; font-weight:bold; padding-top:2px; vertical-align:top;">
        <div style="float:left; width:80px;text-align:right; height:20px; padding-right:6px;">取衣地址：</div>
        <div id="txtAddress2" style="float:left; width:200px; height:23px;overflow:hidden;margin-top:-1px;">&nbsp;</div>
    </div>
    <div style="font-size:13px; color:#000; font-weight:bold; padding-top:15px;">
        <div id="txtList" style="width:100%;"></div>
    </div>
    <div style="height:37px; padding-top:20px;">
        <div id="btnSend2" style="float:left;"></div>
        <div id="btnBack" style="float:left;"></div>
    </div>
</div>
</asp:Content>
