<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="NaviSales.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Navigation.NaviSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>销售管理_导航</title>
<style type="text/css">
    button {
        background:#ccc;border:1px solid #999;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="z_event_border" style="font-weight: bold; height: 72px; line-height: 72px;
                padding-left: 10px; background: rgb(241, 242, 245);font-size:22px;">
                销售管理</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;height:400px">
                <table class="z_main_tb">
                    <tr><td colspan="6" style="height:40px">&nbsp;</td></tr>
                    <tr class="z_main_tb_tr" style="vertical-align:top">
                        <td class="z_main_tb_td2" style="width:50px; height:120px">&nbsp;</td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button1" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"  onclick="window.location.href='/module/order/orders/SalesOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="jitbutton-1014-btnInnerEl" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">销售单</span>
                            <span id="jitbutton-1014-btnIconEl" class="x-btn-icon " style=""></span>
                            </button>
                        </td>                        
                        <td class="z_main_tb_td2" style="">
                            <button id="Button3" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/SalesOutOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span3" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">销售单出库</span>
                            <span id="Span4" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button2" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/SalesReturnInOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span1" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">销售单入库</span>
                            <span id="Span2" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button4" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/orders/SalesReturnOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span5" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">销售退货</span>
                            <span id="Span6" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="width: 138px">
                            <button id="Button5" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/deliveryorder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span7" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">配送单</span>
                            <span id="Span8" class="x-btn-icon " style=""></span>
                        </td>
                    </tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td2" style="width:50px;height:160px">&nbsp;</td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button6" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"   onclick="window.location.href='/module/order/inoutorders/BatchOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span9" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">批发单</span>
                            <span id="Span10" class="x-btn-icon " style=""></span>
                            </button>
                        </td>                        
                        <td class="z_main_tb_td2" style="">
                            <button id="Button7" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"  onclick="window.location.href='/module/order/inoutorders/BatchReturnOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span11" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">批发退货单</span>
                            <span id="Span12" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button8" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"  onclick="window.location.href='/module/order/inoutorders/RetailOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span13" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">零售单</span>
                            <span id="Span14" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button9" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/RetailReturnOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span15" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">零售退货单</span>
                            <span id="Span16" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button10" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/PosOrder.aspx?mid=3E6B1C1EE70D4459BD0393F84E82AD56'">
                            <span id="Span17" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">POS小票</span>
                            <span id="Span18" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                    </tr>
                    <%--<tr><td class="z_main_tb_td" style="text-align:center">门店分布</td>
                    <td class="z_main_tb_td2" style="text-align:center">门店列表</td></tr>--%>
                    <tr><td colspan="6" >&nbsp;</td></tr>           
                </table>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 130px; line-height: 130px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                功能说明：配置门店参数，管理门店销售数据，管理批发和退货流程。
                </div>        
</div>
</asp:Content>
