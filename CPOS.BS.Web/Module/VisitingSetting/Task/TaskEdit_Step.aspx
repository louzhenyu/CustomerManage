<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Controller/TaskEdit_StepCtl.js" type="text/javascript"></script>
    <script src="Model/TaskEdit_StepVM.js" type="text/javascript"></script>
    <script src="Store/TaskEdit_StepVMStore.js" type="text/javascript"></script>
    <script src="View/TaskEdit_StepView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <%--by zhongbao.xiao 2013.5.24 用户返回拜访任务列表页--%>
        <div id="navi"><a href="javascript:;"></a></div>
        <div class="process">
            <ul class="proce-list">
                <li><a href="javascript:;" onclick="window.location='TaskEdit.aspx'+urlparams;">
                    <img src="/framework/image/flowimg/waiting/1.png" style="float: left" />
                    <span style="padding-top: 10px;">基本信息</span> </a>
                    <img src="/framework/image/flowimg/next.png" style="float: right" /></li>
                <li><a id="a1" href="javascript:;">
                    <img src="/framework/image/flowimg/waiting/2.png" style="float: left" />
                    <span style="padding-top: 10px;">终端选择</span> </a>
                    <img src="/framework/image/flowimg/next.png" style="float: right" /></li>
                <li><a href="javascript:;" onclick="window.location=window.location;">
                    <img src="/framework/image/flowimg/finish/3.png" style="float: left" />
                    <span style="padding-top: 10px;">拜访步骤</span> </a></li>
            </ul>
        </div>
        <div class="m10 article">
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_create'></span><span id='span_cancel'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <div style="display: block; height: 0px; overflow: hidden;">
        <iframe id="tab1" frameborder="0" src=""></iframe>
        <iframe id="tab2" frameborder="0" src=""></iframe>
        <iframe id="tab3" frameborder="0" src=""></iframe>
    </div>
</asp:Content>
