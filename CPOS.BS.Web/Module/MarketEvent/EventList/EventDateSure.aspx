<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>时间验证</title>
    <script type ="text/javascript">
        var eventId = "<%=Request.QueryString["id"] %>"; 
    </script>
    <script src="Controller/EventDateSureCtl.js" type="text/javascript"></script>
    <script src="Model/EventDateSureVM.js" type="text/javascript"></script>
    <script src="Store/EventDateSureVMStore.js" type="text/javascript"></script>
    <script src="View/EventDateSureView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="m10 article">
   
   <div class="art-tit">
    <div class="z_event_step z_event_border">
            <div class="z_event_step_item pointer" onclick="javascript: location.href = 'EventRun.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=<%=Request.QueryString["id"] %>&page=1;'">
                <div class="z_event_step_item_icon z_event_step1"></div>
                <div class="z_event_step_item_text ">启动</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item" style=" width:120px; cursor:pointer;" >
                <div class="z_event_step_item_icon z_event_step2_h" ></div>
                <div class="z_event_step_item_text " style=" width:60px;">时间校验</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="javascript: location.href = 'ResponsePersonList.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=<%=Request.QueryString["id"] %>&page=1;'">
                <div class="z_event_step_item_icon z_event_step3"></div>
                <div class="z_event_step_item_text ">响应</div>
            </div>
            
        </div>
    </div>
    <div class="Eventconnext" id="Eventconnext">
       
           <div class="DivGridView" id="EventDateSure">
            </div>
       <div style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; background:rgb(241, 242, 245); " class="z_event_border" id="sendBtnAtt">
            <div style="float:right; margin-top:10px; margin-right:10px;" id="SubmitDataSure"><div style="color: rgb(0, 0, 0); margin: 0px 0px 0px 10px; border-width: 1px; height: 31px;" class="x-btn x-btn-default-small x-noicon x-btn-noicon x-btn-default-small-noicon" id="jitbutton-1012"><em id="jitbutton-1012-btnWrap"><button autocomplete="off" role="button" hidefocus="true" class="x-btn-center" type="button" id="jitbutton-1012-btnEl" style="width: 120px; height: 26px;"><span style="width: 120px; height: 26px; line-height: 26px;" class="x-btn-inner">保存</span><span class="x-btn-icon " ></span></button></em></div></div>
            <div style="float:right; margin-top:10px;" id="ResetDataSure" ><div style="color: rgb(0, 0, 0); margin: 0px 0px 0px 10px; border-width: 1px; height: 31px;" class="x-btn x-btn-default-small x-noicon x-btn-noicon x-btn-default-small-noicon" id="jitbutton-1009"><em id="jitbutton-1009-btnWrap"><button autocomplete="off" role="button" hidefocus="true" class="x-btn-center" type="button" id="jitbutton-1009-btnEl" style="width: 120px; height: 26px;"><span style="width: 120px; height: 26px; line-height: 26px;" class="x-btn-inner">清空</span><span class="x-btn-icon " ></span></button></em></div></div>
        </div>
    </div>
</div>

</asp:Content>
