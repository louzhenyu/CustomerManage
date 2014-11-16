<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.CollegeNews.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="/module/static/css/datepicker.css" rel="stylesheet" type="text/css" />
    <title>资讯管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="/Module/College/News/js/NewsList">
        <!--详情菜单-->
        <div class="subMenu">
            <ul class="clearfix">
                <li class="nav01 on"><a href="NewsList.aspx">资讯内容</a></li>
                <li class="nav02"><a href="NewsType.aspx">资讯分类</a></li>
                <!--<li class="nav03">资讯标签</li>-->
            </ul>
        </div>
        <div class="subMenu-content">
            <!--个别查询-->
            <div class="queryTermArea">
                <div class="item">
                    <div class="moreQueryWrap">
                        <a href="javascript:;" class="commonBtn queryBtn">查询</a> <a href="javascript:;" class="more">
                            更多查询条件</a>
                    </div>
                    <div class="commonSelectWrap type-con">
                        <em class="tit">分类</em>
                        <div id="newsTypeSelect" class="selectBox">
                        </div>
                    </div>
                    <div class="commonSelectWrap">
                        <em class="tit">关键字</em>
                        <label class="searchInput">
                            <input id="ipkw" type="text" value="">
                        </label>
                    </div>                    
                    <div class="commonSelectWrap">
                            <em class="tit">开始时间</em>
                            <label class="searchInput"><input type="text" id="starTime" name="BeginTime" /></label>
                        </div>
                        <div class="commonSelectWrap">
                            <em class="tit">结束时间</em>
                            <label class="searchInput"><input type="text" id="endTime" name="EndTime" /></label>
                        </div>
                </div>
            </div>
            <!--表格操作按钮-->
            <div class="tableWrap">
                <div class="tablehandle">
                    <!--<div class="selectBox fl">-->
                    <!--<span class="text">操作</span>-->
                    <!--<div class="selectList">-->
                    <!--<p>-->
                    <!--操作1</p>-->
                    <!--<p>-->
                    <!--操作2</p>-->
                    <!--</div>-->
                    <!--</div>-->
                    <a href="NewsEdit.aspx" class="commonBtn appInfoBtn">添加资讯</a>
                </div>
                <!-- 已确认名单表格 -->
                <table id="table" class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<img src="../commres/images/selectIcon02.png" alt="" /><div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p data-val="select">
                                        全选本页</p>
                                    <p data-val="cancel">
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                操作<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                日期<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                标题
                            </th>
                            <th>
                                分类<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                浏览数<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                赞数<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                分享数<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="9">
                                <span>
                                    <img src="../CommRes/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div id="kkpager" style="text-align: center;">
                </div>
            </div>
        </div>
    </div>
    <script id="tableTemp" type="text/html">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <tr id="<#=idata.NewsId #>">
                <td class="checkBox" data-id="<#=idata.NewsId #>">
                 <em></em>
                </td>
                <td class="operateWrap">
                     <span data-id="<#=idata.NewsId #>" class="editIcon editBtn"></span>
                     <span data-id="<#=idata.NewsId #>" class="delBtn delIcon"></span>
                </td>
                <td>
                 <#:dd=idata.PublishTime #>
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
    <script id="selectTemp" type="text/html">
        <span id="stype" class="selected text" data-val=""></span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.id #>"><#=idata.name #></p>
        <#}#>
        </div>
    </script>
</asp:Content>
