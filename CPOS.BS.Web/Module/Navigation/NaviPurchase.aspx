<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="NaviPurchase.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Navigation.NaviPurchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>采购管理_导航</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="z_event_border" style="font-weight: bold; height: 72px; line-height: 72px;
                padding-left: 10px; background: rgb(241, 242, 245);font-size:22px;">
                采购管理</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;height:400px">
                <table class="z_main_tb">
                    <tr><td colspan="6" style="height:40px">&nbsp;</td></tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td2" style="width:50px;height:150px"">&nbsp;</td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button1" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"  onclick="window.location.href='/module/order/orders/PurchaseOrder.aspx?mid=3D10006BE0EF43588D59D92AB7130850'">
                            <span id="jitbutton-1014-btnInnerEl" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">采购单据</span>
                            <span id="jitbutton-1014-btnIconEl" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button2" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/PurchaseInOrder.aspx?mid=3D10006BE0EF43588D59D92AB7130850'">
                            <span id="Span1" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">采购入库</span>
                            <span id="Span2" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button3" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/orders/PurchaseReturnOrder.aspx?mid=3D10006BE0EF43588D59D92AB7130850'">
                            <span id="Span3" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">采购退货</span>
                            <span id="Span4" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button4" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/order/inoutorders/PurchaseReturnOutOrder.aspx?mid=3D10006BE0EF43588D59D92AB7130850'">
                            <span id="Span5" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">采购出库</span>
                            <span id="Span6" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="width: 138px">&nbsp;</td>
                    </tr>         
                    <%--<tr><td class="z_main_tb_td" style="text-align:center">门店分布</td>
                    <td class="z_main_tb_td2" style="text-align:center">门店列表</td></tr>--%>
                    <tr><td colspan="6" >&nbsp;</td></tr>           
                </table>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 130px; line-height: 130px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                功能说明：管理门店采购流程。
                </div>        
</div>
</asp:Content>
