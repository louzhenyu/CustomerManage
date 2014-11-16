<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="NaviShopVisiting.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Navigation.NaviShopVisiting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>门店拜访_导航</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="z_event_border" style="font-weight: bold; height: 72px; line-height: 72px;
                padding-left: 10px; background: rgb(241, 242, 245);font-size:22px;">
                门店拜访</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;height:400px">
                <table class="z_main_tb">
                    <tr><td colspan="6" style="height:40px">&nbsp;</td></tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td2" style="width:50px;height:150px"">&nbsp;</td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button4" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"  onclick="window.location.href='/module/visitingdata/data/TaskDataView_List.aspx?mid=0B1585A7AA42486EAB4A628471AF4AA1'">
                            <span id="Span5" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访反馈</span>
                            <span id="Span6" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button1" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;"   onclick="window.location.href='/module/visitingdata/file/taskdatafile.aspx?mid=0B1585A7AA42486EAB4A628471AF4AA1'">
                            <span id="jitbutton-1014-btnInnerEl" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访照片</span>
                            <span id="jitbutton-1014-btnIconEl" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                                <button id="Button2" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/visitingSetting/Task/Task.aspx?mid=0B1585A7AA42486EAB4A628471AF4AA1'">
                            <span id="Span1" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访任务</span>
                            <span id="Span2" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                        <td class="z_main_tb_td2" style="">
                            <button id="Button3" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/visitingSetting/VisitingObject/VisitingObject.aspx?mid=0B1585A7AA42486EAB4A628471AF4AA1'">
                            <span id="Span3" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">自定义对象</span>
                            <span id="Span4" class="x-btn-icon " style=""></span>
                            </button>
                        </td>
                          <td class="z_main_tb_td2" style="">
                            <button id="Button3" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/visitingSetting/Parameter/Parameter.aspx?mid=0B1585A7AA42486EAB4A628471AF4AA1'">
                            <span id="Span3" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访参数</span>
                            <span id="Span4" class="x-btn-icon " style=""></span>
                            </button>
                             <td class="z_main_tb_td2" style="">
                            <button id="Button5" type="button" class="x-btn-center" hidefocus="true" role="button" autocomplete="off" style="width: 138px; height: 138px;" onclick="window.location.href='/module/visitingSetting/ParameterOption/ParameterOption.aspx?mid=0B1585A7AA42486EAB4A628471AF4AA1'">
                            <span id="Span7" class="x-btn-inner" style="width: 138px; height: 138px; line-height: 138px;font-size:22px; font-weight:bold">拜访参数选项</span>
                            <span id="Span8" class="x-btn-icon " style=""></span>
                            </button>
                        </td><td class="z_main_tb_td2" style="width: 138px">&nbsp;</td>
                    </tr>         
                    <%--<tr><td class="z_main_tb_td" style="text-align:center">门店分布</td>
                    <td class="z_main_tb_td2" style="text-align:center">门店列表</td></tr>--%>
                    <tr><td colspan="6" >&nbsp;</td></tr>           
                </table>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 130px; line-height: 130px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                功能说明：管理促销人员拜访工作，自动分配拜访任务，智能记录拜访情况。
                </div>        
</div>
</asp:Content>
