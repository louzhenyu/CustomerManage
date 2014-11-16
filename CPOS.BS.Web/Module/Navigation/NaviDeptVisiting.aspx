<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="NaviDeptVisiting.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Navigation.NaviDeptVisiting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>单位拜访_导航</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="z_event_border" style="font-weight: bold; height: 72px; line-height: 72px;
                padding-left: 10px; background: rgb(241, 242, 245);font-size:22px;">
                客户拜访</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;height:400px">
                <table class="z_main_tb">
                    <tr><td colspan="6" style="height:40px">&nbsp;</td></tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td2" style="width:50px;height:150px"">&nbsp;</td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button4" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" >
                            <span id="Span5" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访反馈</span>
                            <span id="Span6" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button1" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" >
                            <span id="jitbutton-1014-btnInnerEl" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访任务</span>
                            <span id="jitbutton-1014-btnIconEl" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button2" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;">
                            <span id="Span1" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访路线</span>
                            <span id="Span2" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button3" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;">
                            <span id="Span3" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访追踪</span>
                            <span id="Span4" class="x-btn-icon " style=""></span>
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
                功能说明：管理人员拜访工作，自动分配拜访任务，智能记录拜访情况。
                </div>        
</div>
</asp:Content>
