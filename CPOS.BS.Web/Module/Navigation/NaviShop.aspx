<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="NaviShop.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Navigation.NaviShop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>门店基本信息_导航</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="z_event_border" style="font-weight: bold; height: 72px; line-height: 72px;
                padding-left: 10px; background: rgb(241, 242, 245);font-size:22px;">
                门店基本信息</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;height:400px">
                <table class="z_main_tb">
                    <tr><td colspan="6" style="height:40px">&nbsp;</td></tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td2" style="width:70px;height:150px"">&nbsp;</td>
                        <td class="z_main_tb_td2" style="padding-right:30px;">
                            <button id="Button1" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/bi/WebMap.aspx?mid=599C078C751A47019A8E14F94FA15C7F'">
                            <span id="jitbutton-1014-btnInnerEl" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px; font-size:22px; font-weight:bold">门店分布</span>
                            <span id="jitbutton-1014-btnIconEl" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="padding-left:30px">
                                <button id="Button2" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/basic/unit/Unit.aspx?mid=599C078C751A47019A8E14F94FA15C7F'">
                            <span id="Span1" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">门店列表</span>
                            <span id="Span2" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="width:212px">&nbsp;</td>
                        <td class="z_main_tb_td2" style="width:212px">&nbsp;</td>
                        <td class="z_main_tb_td2" style="width:212px">&nbsp;</td>
                    </tr>         
                    <%--<tr><td class="z_main_tb_td" style="text-align:center">门店分布</td>
                    <td class="z_main_tb_td2" style="text-align:center">门店列表</td></tr>--%>
                    <tr><td colspan="6" >&nbsp;</td></tr>           
                </table>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 130px; line-height: 130px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                功能说明：通过动态的地图展示所有门店的分布情况及基本信息，动态添加和删除门店。
                </div>        
</div>
</asp:Content>
