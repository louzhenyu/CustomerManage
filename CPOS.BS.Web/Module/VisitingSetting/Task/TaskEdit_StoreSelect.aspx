<%@ Page Title="拜访任务管理" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/javascript/Biz/CheckboxModel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITDynamicGrid.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JTIPagePannel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
   <%-- <script src="/Framework/javascript/Biz/Channel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Chain.js" type="text/javascript"></script>--%>
    <script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>
  <%--  <script src="/Framework/javascript/Biz/Category.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>--%>
    <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/HierarchyItem.js" type="text/javascript"></script>
<%--    <script src="/Framework/Javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Biz/District.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Biz/Province.js" type="text/javascript"></script>--%>
    <%--<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>--%>
    <script src="View/TaskEdit_StoreSelectView.js" type="text/javascript"></script>
    <script src="Controller/TaskEdit_StoreSelectCtl.js" type="text/javascript"></script>
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
                <li><a href="javascript:;" onclick="window.location=window.location">
                    <img src="/framework/image/flowimg/finish/2.png" style="float: left" />
                    <span style="padding-top: 10px;">终端选择</span> </a>
                    <img src="/framework/image/flowimg/next.png" style="float: right" /></li>
                <li><a href="javascript:;" onclick="window.location='TaskEdit_Step.aspx'+urlparams;">
                    <img src="/framework/image/flowimg/waiting/3.png" style="float: left" />
                    <span style="padding-top: 10px;">拜访步骤</span> </a></li>
            </ul>
        </div>
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='dvSearch'></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='dvWork'></span><span id='dvAutoFill'></span>
                </div>
            </div>
            <div class="DivGridView" id="dvGrid">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <div style="display: block; height: 0px; overflow: hidden;">
        <iframe id="tab1" height="530px" width="1044px" frameborder="0" src=""></iframe>
    </div>
</asp:Content>
