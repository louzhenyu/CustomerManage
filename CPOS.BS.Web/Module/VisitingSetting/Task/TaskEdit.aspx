<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Role.js" type="text/javascript"></script>
    <%--<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>--%>
    <script src="Controller/TaskEditCtl.js" type="text/javascript"></script>
    <script src="Model/TaskVM.js" type="text/javascript"></script>
    <script src="Store/TaskEditVMStore.js" type="text/javascript"></script>
    <script src="View/TaskEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <%--by zhongbao.xiao 2013.5.24 用户返回拜访任务列表页--%>
        <div id="navi"><a href="javascript:;"></a></div>
        <div class="process">
            <ul class="proce-list">
                <li><a href="javascript:;" onclick="window.location=window.location;">
                    <img src="/framework/image/flowimg/finish/1.png" style="float: left" />
                    <span style="padding-top: 10px;">基本信息</span> </a>
                    <img src="/framework/image/flowimg/next.png" style="float: right" /></li>
                <li><a id="a1" href="javascript:;">
                    <img src="/framework/image/flowimg/waiting/2.png" style="float: left" />
                    <span style="padding-top: 10px;">终端选择</span> </a>
                    <img src="/framework/image/flowimg/next.png" style="float: right" /></li>
                <li><a id="a2" href="javascript:;">
                    <img src="/framework/image/flowimg/waiting/3.png" style="float: left" />
                    <span style="padding-top: 10px;">拜访步骤</span> </a></li>
            </ul>
        </div>
        <div class="m10 article">
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
