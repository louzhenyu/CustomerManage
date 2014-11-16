<%@ Page Title="活动响应" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" 
Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>活动响应</title>
    <script src="Controller/ResponsePersonCtl.js" type="text/javascript"></script>
    <script src="Model/ResponsePersonVM.js" type="text/javascript"></script>
    <script src="Store/ResponsePersonVMStore.js" type="text/javascript"></script>
    <script src="View/ResponsePersonView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <%--<div id="view_Search" class="view_Search" >
                      
                      <img id="topImg" width="100%" height="70px;" alt="图例" src="../../../Framework/Image/ResponsePerson.png"/> 
                     
                </div>--%>

                <div class="z_event_step z_event_border">
            <div class="z_event_step_item pointer" onclick="javascript: location.href = 'EventRun.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=<%=Request.QueryString["id"] %>&page=1;'">
                <div class="z_event_step_item_icon z_event_step1"></div>
                <div class="z_event_step_item_text ">启动</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item" style=" width:120px; cursor:pointer;" onclick="javascript: location.href = 'EventDateSure.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=<%=Request.QueryString["id"] %>&page=1;'">
                <div class="z_event_step_item_icon z_event_step2" ></div>
                <div class="z_event_step_item_text " style=" width:60px;">时间校验</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" >
                <div class="z_event_step_item_icon z_event_step3_h"></div>
                <div class="z_event_step_item_text ">响应</div>
            </div>
            
        </div>
            </div>
            <div class="art-titbutton" >
                <div class="view_Button" >
                    <span id='span_create'><H3>响应人群</H3></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb" id="divPanel"></div>
        </div>
    </div>

</asp:Content>
