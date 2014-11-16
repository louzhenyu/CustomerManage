<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" rel="stylesheet" />
    <script src="<%=StaticUrl+"/Framework/Javascript/Other/editor/kindeditor.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/CustomerBasicSetting/View/CustomerBasicSetting.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/CustomerBasicSetting/Controller/CustomerBasicSettingCtl.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/CustomerBasicSetting/Store/CustomerBasicSettingVMStore.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/CustomerBasicSetting/Model/CustomerBasicSettingVM.js?v=0.2"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/examples/jquery.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/kindeditor.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"%>"></script>
    <style type="text/css">
        .divllabl
        {
            font-size: 12px;
            color: #0E0E01;
            font-weight: 900;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="DivGridView" id="divmain">
                <%--<input type="button" id="uploadImageUrl" value=" 选择图片 " />
                <input type="button" id="uploadCusImageUrl" value=" 选择图片 " />--%>
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
