<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动管理</title>
    
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/MarketBrand.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventMode.js" type="text/javascript"></script>

    <script src="Controller/EventCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/EventView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section z_event">
        <div style="display:none;">
            <a href="Event.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">Event</a>
            <a href="EventTime.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">EventTime</a>
            <a href="MarketStore.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">MarketStore</a>
            <a href="MarketPerson.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">MarketPerson</a>
            <a href="MarketPersonAdd.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">MarketPersonAdd</a>
            <a href="MarketPersonImport.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">MarketPersonImport</a>
            <a href="MarketTemplate.aspx?MarketEventID=087A78E03DDD4A8081865C7EF1A7BC95&mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA">MarketTemplate</a>
        </div>
        <div class="art-tit" style="background:#F6F6F6">
        <div class="z_event_step z_event_border" style="width:830px; border:0;">
            <div class="z_event_step_item">
                <div class="z_event_step_item_icon z_event_step1_h"></div>
                <div class="z_event_step_item_text ">定义</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnSave()">
                <div class="z_event_step_item_icon z_event_step2"></div>
                <div class="z_event_step_item_text ">时间</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto3()">
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
        <div class="z_event_border" style="font-weight:bold; height:36px; line-height:36px; 
            padding-left:10px; background:rgb(241, 242, 245);">定义</div>
        <div class="z_event_border" style="
            padding-left:10px; border-top:0px;height:425px;">
            <table class="z_main_tb">
                <tr class="z_main_tb_tr">
                    <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; padding-top:5px;">
                        活动代码
                    </td>
                    <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px;">
                        <div id="txtCode" style="margin-top:5px; margin-left:10px;"></div>
                    </td>
                </tr>
                <tr class="z_main_tb_tr">
                    <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                        品牌
                    </td>
                    <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                        <div id="txtBrand" style="margin-top:5px;"></div>
                    </td>
                    <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                        <div style="float:right;">预算支出总金额</div>
                        <div id="chkAmount1" style="float:right;"></div>
                    </td>
                    <td class="z_main_tb_td2" style="">
                        <div id="txtAmount1" style="margin-top:5px;"></div>
                    </td>
                </tr>
                <tr class="z_main_tb_tr">
                    <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                        <%--<font color="red">*</font>活动类型--%>
                        活动方式
                    </td>
                    <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                        <%--<div id="txtEventType" style="margin-top:5px;"></div>--%>
                        <div id="txtEventMode" style="margin-top:5px;"></div>
                    </td>
                    <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                        <div style="float:right; margin-left:12px;">人均基数金额</div>
                        <div id="chkAmount2" style="float:right;"></div>
                    </td>
                    <td class="z_main_tb_td2" style="">
                        <div id="txtAmount2" style="margin-top:5px;"></div>
                    </td>
                </tr>
                <tr class="z_main_tb_tr">
                    <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                        活动描述
                    </td>
                    <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px;">
                        <div id="txtEventDesc" style="margin-top:-5px;"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; background:rgb(241, 242, 245);">
            <div id="btnNext" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
    </div>

</asp:Content>
