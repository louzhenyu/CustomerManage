<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>表单管理</title>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" data-js="FormConfig/formList" class="section m10">
        
        <div class="showContentWrap clearfix">
            <div class="createdArea">
	            <span id="addFormBtn" class="btn">创建新表单</span>
            </div>

            <div id="formList" class="createdContentArea">
	            
            </div>            
        </div>        
        <div class="ui-mask"></div>

    </div>
    <script type="text/javascript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/main"></script>
</asp:Content>
