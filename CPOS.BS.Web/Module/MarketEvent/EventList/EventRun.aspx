<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>活动详情</title>
    <script type ="text/javascript">
        var eventId = "<%=Request.QueryString["id"] %>"; 
    </script>
    <script src="Controller/EventRunCtl.js" type="text/javascript"></script>
   
    <style type="text/css">
    .Eventconnext{ margin-top:10px;}
    .eventRunDetailContext{ float:left; width:48%; padding-left:1%; height:36px; line-height:36px;}
    .eventRunDetailText{ clear:both; line-height:24px; padding-left:1%; margin-top:10px;}
    .EventConnextTabel{ border:1px solid #C2C3C8; border-top:0;border-bottom:0; padding:10px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="m10 article">
<div class="art-tit">
      <div class="z_event_step z_event_border">
            <div class="z_event_step_item pointer" >
                <div class="z_event_step_item_icon z_event_step1_h"></div>
                <div class="z_event_step_item_text ">启动</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item" style=" width:120px; cursor:pointer;" onclick="javascript: location.href = 'EventDateSure.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=<%=Request.QueryString["id"] %>&page=1;'">
                <div class="z_event_step_item_icon z_event_step2" ></div>
                <div class="z_event_step_item_text " style=" width:60px;">时间校验</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="javascript: location.href = 'ResponsePersonList.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=<%=Request.QueryString["id"] %>&page=1;'">
                <div class="z_event_step_item_icon z_event_step3"></div>
                <div class="z_event_step_item_text ">响应</div>
            </div>
            
        </div>
        </div>

    <div class="Eventconnext">
        <div class="art-titbutton" style="padding-left:16px; line-height:46px; font-weight:bold;">活动基本信息</div>
       
        <div class="EventConnextTabel" id="EventRunDetail">
        </div>
       <div style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; background:rgb(241, 242, 245); display:none;" class="z_event_border" id="sendBtnAtt">
            <div style="float:right; margin-top:10px; margin-right:10px; display:none;" id="runSendGray"><div style="color: rgb(0, 0, 0); border-color:#D7D5D5; margin: 0px 0px 0px 10px; border-width: 1px; height: 31px;" class="x-btn x-btn-default-small x-noicon x-btn-noicon x-btn-default-small-noicon" ><em id="Em1"><button autocomplete="off" role="button" hidefocus="true" class="x-btn-center" type="button" disabled="disabled" style="width: 120px; height: 26px; cursor:inherit;"><span style="width: 120px; height: 26px; line-height: 26px; color:Gray;" class="x-btn-inner">启动发送<span id="SendPersonNumGray"></span>人</span><span class="x-btn-icon " ></span></button></em></div></div>
            <div style="float:right; margin-top:10px; margin-right:10px;" id="runSend"><div style="color: rgb(0, 0, 0); margin: 0px 0px 0px 10px; border-width: 1px; height: 31px;" class="x-btn x-btn-default-small x-noicon x-btn-noicon x-btn-default-small-noicon" id="jitbutton-1012"><em id="jitbutton-1012-btnWrap"><button autocomplete="off" role="button" hidefocus="true" class="x-btn-center" type="button" id="jitbutton-1012-btnEl" style="width: 120px; height: 26px;"><span style="width: 120px; height: 26px; line-height: 26px;" class="x-btn-inner">启动发送<span id="SendPersonNum"></span>人</span><span class="x-btn-icon " ></span></button></em></div></div>
            <div style="float:right; margin-top:10px;" id="testSend" ><div style="color: rgb(0, 0, 0); margin: 0px 0px 0px 10px; border-width: 1px; height: 31px;" class="x-btn x-btn-default-small x-noicon x-btn-noicon x-btn-default-small-noicon" id="jitbutton-1009"><em id="jitbutton-1009-btnWrap"><button autocomplete="off" role="button" hidefocus="true" class="x-btn-center" type="button" id="jitbutton-1009-btnEl" style="width: 120px; height: 26px;"><span style="width: 120px; height: 26px; line-height: 26px;" class="x-btn-inner">测试发送</span><span class="x-btn-icon " ></span></button></em></div></div>
        </div>
    </div>
</div>

</asp:Content>
