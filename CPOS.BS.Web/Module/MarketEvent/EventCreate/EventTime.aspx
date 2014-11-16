<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动管理</title>
    
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>

    <script src="Controller/EventTimeCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/EventTimeView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="section z_event">
     <div class="art-tit" style="background:#F6F6F6">
        <div class="z_event_step z_event_border" style="width:830px; border:0;">
            <div class="z_event_step_item pointer" onclick="fnPre()">
                <div class="z_event_step_item_icon z_event_step1"></div>
                <div class="z_event_step_item_text ">定义</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto2()">
                <div class="z_event_step_item_icon z_event_step2_h"></div>
                <div class="z_event_step_item_text ">时间</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnSave()">
                <div class="z_event_step_item_icon z_event_step3"></div>
                <div class="z_event_step_item_text ">门店</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto4()">
                <div class="z_event_step_item_icon z_event_step4"></div>
                <div class="z_event_step_item_text ">人群</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto5()">
                <div class="z_event_step_item_icon z_event_step5"></div>
                <div class="z_event_step_item_text ">邀约</div>
            </div>
        </div>
        </div>
        <%--<div class="z_event_border" style="font-weight:bold; height:36px; line-height:36px; 
            padding-left:10px; border-top:0px;">定义11</div>--%>
        <div class="z_event_border" style="
            padding:10px; padding-bottom:0px;">
            
            <div id="tabsMain" style="width:100%; height:450px;"></div>
            <div id="tabInfo" style="height:441px; /*background:rgb(241, 242, 245);*/">
                <div class="z_detail_tb" style="">
                    <div style="height:5px;"></div>
                    <table class="z_main_tb" style="width:400px;">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:62px; padding-top:5px; width:100px;">
                                计划开始时间
                            </td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:62px;">
                                <div id="txtBeginDate" style="margin-top:25px; margin-left:10px;"></div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:62px;">
                                计划结束时间
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:62px;">
                                <div id="txtEndDate" style="margin-top:20px; margin-left:10px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div id="tabWave" style="height:1px; overflow:hidden;">
                <div style="width:100%; padding-left:10px; padding-right:0px;">
                    <div style="width:100%;">
                        <div style="float:left; width:180px; background:;">
                            <div style="line-height:100px; height:100px;">
                                文件模板：<a href="#"><font color="blue">下载</font></a>
                            </div>
                            <div style="height:100px;">
                                文件上传：
                                <div id="fileUpload" style="margin-top:10px;"></div>
                                <div id="btnUpload" style="margin-top:10px;"></div>
                            </div>
                        </div>
                        <div style="float:right; width:70%;">
                            <div id="gridWave"></div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; background:rgb(241, 242, 245);">
            <div id="btnNext" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnPre" style="float:right; margin-top:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
    </div>

</asp:Content>
