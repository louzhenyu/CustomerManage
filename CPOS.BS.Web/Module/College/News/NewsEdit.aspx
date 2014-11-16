<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.Module.CollegeNews.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="/module/static/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()" />
    <title>资讯管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="/Module/College/News/js/NewsEdit">
        <div class="commonTitleBox">
            <a href="javascript:history.go(-1);" class="getBack">返回</a>
            <h2 class="commonTitle">添加资讯</h2>
        </div>
        <div class="mainContent">
            <div class="addInfoArea">
                <form action="" name="frm" id="frm" method="post">
                <div class="item-line">
                    <div class="commonSelectWrap classInput">
                        <em class="tit"><span class="fontRed">*</span> 分类</em>
                        <div id="newsTypeSelect" class="selectBox">
                        </div>
                    </div>
                    <div class="commonSelectWrap authorInput">
                        <em class="tit">作者</em>
                        <p class="searchInput">
                            <input id="Author" name="Author" type="text" value="" /></p>
                    </div>
                </div>
                <div class="item-line">
                    <div class="commonSelectWrap">
                        <em class="tit"><span class="fontRed">*</span> 标题</em>
                        <p class="searchInput  w-596">
                            <input id="NewsTitle" name="NewsTitle" type="text" value="" /></p>
                    </div>
                </div>
                <div class="item-line desc">
                    <div class="commonSelectWrap">
                        <em class="tit">描述</em>
                        <p class="textarea w-596"><textarea id="Intro" name="Intro"></textarea></p>
                    </div>
                </div>
                <div class="item-line theme richtext">
                    <div class="commonSelectWrap">
                        <em class="tit"><span class="fontRed">*</span> 内容</em>
                        <p class="textarea w-596"><textarea id="editor_id" name="Content" style="width: 680px; height: 300px;"></textarea></p>
                    </div>
                </div>
                <div class="item-line thumb">
                    <div class="commonSelectWrap">
                        <em class="tit">缩略图</em>
                        <div class="thumbBox">
                            <div class="uploadBtn">
                            </div>
                            <div class="relateWrap">
                                <p id="IsRelMicro" class="radioBox"><em></em><span>关联微刊</span></p>
                                <div class="commonSelectWrap classInput" style="display: none;">
                                    <em class="tit"><span class="fontRed">*</span> 刊号</em>
                                    <div id="microNumsSelect" class="selectBox">
                                </div>
                                </div>
                                <div class="commonSelectWrap classInput" style="display: none;">
                                    <em class="tit"><span class="fontRed">*</span> 版块</em>
                                    <div id="microTypeSelect" class="selectBox">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item-line">
                    <div class="commonSelectWrap selectDateBox">
                        <span class="tit">发布时间</span>
                        <p><input id="PublishTime" name="PublishTime" type="text" value=""/></p>
                    </div>
                </div>
                <div class="btnWrap">
                    <a id="saveBtn" href="javascript:;" class="commonBtn">保存</a><a id="cancelBtn" href="javascript:;" class="commonBtn cancelBtn">取消</a>
                </div>
                </form>
            </div>
        </div>
    </div>
    <script id="tableTemp" type="text/html">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <tr>
                <td class="checkBox">
                 <em></em>
                </td>
                <td class="operateWrap">
                     <span id="<#=idata.NewsId #>" class="editIcon"></span>
                     <span id="<#=idata.NewsId #>" class="delIcon"></span>
                </td>
                <td>
                 <#=idata.PublishTime #>
                </td>
                <td>
                 <#=idata.NewsTitle #>
                </td>
                <td>
                 <#=idata.NewsTypeName #>
                </td>
                <td>
                 <#=idata.BrowseCount #>
                </td>
                <td>
                 <#=idata.PraiseCount #>
                </td>
                <td>
                 <#=idata.CollCount #>
                </td>
            </tr>
        <#} #>
    </script>
    <script id="selType" type="text/html">
        <span class="selected text" data-val="<#=list[0].id #>"><#=list[0].name #></span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.NewsTypeId #>"><#=idata.NewsTypeName #></p>
        <#}#>
        </div>
    </script>
    <script id="selNumber" type="text/html">
        <span class="selected text" data-val="<#=list[0].id #>"><#=list[0].name #></span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.MicroNumberID #>"><#=idata.MicroNumber #></p>
        <#}#>
        </div>
    </script>
    <script id="selMicroType" type="text/html">
        <span class="selected text" data-val="<#=list[0].id #>"><#=list[0].name #></span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.MicroTypeID #>"><#=idata.MicroTypeName  #></p>
        <#}#>
        </div>
    </script>
    <script type="text/javascript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/mainNew.js"></script>
</asp:Content>
