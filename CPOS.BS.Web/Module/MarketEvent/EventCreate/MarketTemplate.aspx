<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动管理</title>
    
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/MarketSendType.js" type="text/javascript"></script>

    <script src="Controller/MarketTemplateCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/MarketTemplateView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section z_event">
     <div class="art-tit" style="background:#F6F6F6">
        <div class="z_event_step z_event_border" style="width:830px; border:0;">
            <div class="z_event_step_item pointer" onclick="fnGoto1()">
                <div class="z_event_step_item_icon z_event_step1"></div>
                <div class="z_event_step_item_text ">定义</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto2()">
                <div class="z_event_step_item_icon z_event_step2"></div>
                <div class="z_event_step_item_text ">时间</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto3()">
                <div class="z_event_step_item_icon z_event_step3"></div>
                <div class="z_event_step_item_text ">门店</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnPre()">
                <div class="z_event_step_item_icon z_event_step4"></div>
                <div class="z_event_step_item_text ">人群</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto5()">
                <div class="z_event_step_item_icon z_event_step5_h"></div>
                <div class="z_event_step_item_text ">邀约</div>
            </div>
        </div>
    </div>
        <div class="z_event_border" style="height:56px; 
            padding-left:10px;  background:rgb(241, 242, 245);">
            <div id="pnlType" style="padding-top:5px;"></div>
        </div>
        <div class="z_event_border" style="padding:0px; border-top:0px; padding-bottom:0px; height:540px;">
            
            <div style="width:100%; padding-left:0px; padding-right:0px; min-height:90px;">
                <div style="width:100%;">
                    <div style="float:left; width:100%;">
                        <div id="pnlTemplate" style="padding-top:5px;padding:10px;"></div>
                    </div>
                </div>
            </div>
            <div style="padding:10px; min-height:250px;background:;">
                <div style="padding:10px;">邀约内容：</div>
                <div style="padding:0px;">
                    <input type="checkbox" id="chkTemplateContent" disabled="disabled" style="margin-top:4px;" />
                    <label id="txtTemplateContent" for="chkTemplateContent" style="color:#000;">微信</label>
                    <div id="tbTemplateContent"></div>
                    
                    <input type="checkbox" id="chkTemplateContentAPP" style="margin-top:4px;" />
                    <label id="txtTemplateContentAPP" for="chkTemplateContentAPP" style="color:#000;">APP</label>
                    <div id="tbTemplateContentAPP"></div>

                    <input type="checkbox" id="chkTemplateContentSMS" style="margin-top:4px;" />
                    <label id="txtTemplateContentSMS" for="chkTemplateContentSMS" style="color:#000;">短信</label>
                    <div id="tbTemplateContentSMS"></div>

                    <div style="height:30px;">
                        <div style="width:80px;float:left;">
                            <font color="red">*</font>发送方式：
                        </div>
                        <div style="width:120px;float:left;">
                            <div id="txtSendType"></div>
                        </div>
                    </div>
                    <div style="color:red; padding-left:0px;clear:both;">*为保证话术内容的正确发送，请不要更改#内的英文文字。</div>
                </div>
            </div>

        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; clear:both; background:rgb(241, 242, 245);">
            <div id="btnStart" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnNext" style="float:right; margin-top:10px;"></div>
            <div id="btnPre" style="float:right; margin-top:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
    </div>

</asp:Content>
