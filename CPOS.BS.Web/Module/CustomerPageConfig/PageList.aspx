<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>页面列表</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <link href="<%=StaticUrl+"/module/static/css/pagination.css"%>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%=StaticUrl+"/module/CustomerPageConfig/css/style3.css?v=0.2"%>" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section"  class="commonOutArea" data-js="CustomerPageConfig/pageList">
        <div class="pageUserConfigArea">
            <div class="queryWrap clearfix">
                <span id="publicBtn" class="queryBtn">发布</span>
            </div>
		    <div class="queryWrap clearfix">
        	    <span class="tit">页面KEY</span>
                <input id="keyInput" type="text" class="inputKey">
                <span class="tit">页面名</span>
                <input id="nameInput" type="text" class="inputName">
                <span id="searchBtn" class="queryBtn">查询</span>
            </div>
        
            <div class="queryListWrap">
        	    <table id="pageList" style="width: 100%" border="1">
                    <thead>
                        <tr>
                            <th>操作</th>
                            <th>Key</th>
                            <th>页面名</th>
                            <th>版本号</th>
                            <th>更新时间</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="pageWrap" style="display:none;">
                <div class="pagination">
                    <a href="javascript:;" class="first" data-action="first">&laquo;</a>
                    <a href="javascript:;" class="previous" data-action="previous">&lsaquo;</a>
                    <input type="text" readonly="readonly" />
                    <a href="javascript:;" class="next" data-action="next">&rsaquo;</a>
                    <a href="javascript:;" class="last" data-action="last">&raquo;</a>
                </div>
            </div>
        </div>    
    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/static/js/main"%>"></script>
</asp:Content>
