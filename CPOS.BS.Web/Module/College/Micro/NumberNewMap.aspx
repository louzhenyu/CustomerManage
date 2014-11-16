<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" CodeBehind="NumberNewMap.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.College.Micro.NumberNewMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="/Module/static/css/artDialog.css" rel="stylesheet" type="text/css" />
    <link href="/module/static/css/datepicker.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #dig-pager{clear:both;height:30px;line-height:30px;margin-top:20px;color:#999;font-size:14px;}
    #dig-pager a{font-size:12px;border:1px solid #DFDFDF;background-color:#FFF;color:#9d9d9d;text-decoration:none;margin:10px 3px;padding:4px 8px;}
    #dig-pager span{font-size:14px;}
    #dig-pager span.disabled{font-size:12px;border:1px solid #DFDFDF;background-color:#FFF;color:#DFDFDF;margin:10px 3px;padding:4px 8px;}
    #dig-pager span.curr{font-size:12px;border:1px solid #F60;background-color:#F60;color:#FFF;margin:10px 3px;padding:4px 8px;}
    #dig-pager a:hover{background-color:#FFEEE5;border:1px solid #F60;}
    #dig-pager span.normalsize{font-size:12px;}
    #dig-pager_gopage_wrap{display:inline-block;width:44px;height:18px;border:1px solid #DFDFDF;position:relative;left:0;top:5px;margin:0 1px;padding:0;}
    #dig-pager_btn_go{width:44px;height:20px;line-height:20px;font-family:arial,宋体,sans-serif;text-align:center;border:0;background-color:#0063DC;color:#FFF;position:absolute;left:0;top:-1px;display:none;padding:0;}
    #dig-pager_btn_go_input{width:30px;height:16px;text-align:center;border:0;position:absolute;left:0;top:0;outline:none;}    
    </style>
    <title>期刊关联资讯</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" class="contentArea" data-js="js/NumberNewMap">
        <div class="topIssuesInfo">
            <div class="operateWrap">
                <p class="getBack" onclick="history.back();">返回</p>
            </div>
            <p class="picWrap"><img id="micro_cover" src="../commres/images/tableImg.png" alt="期刊封面"/></p>
            <div class="infoWrap">
                <h2 class="num">刊号：<label id="micro_num"></label></h2>
                <p class="caption">标题：<label id="micro_title"></label></p>
                <p class="creationDate">创建日期：<label id="micro_time"></label></p>
            </div>
        </div>
        <div class="subMenu-content">
            <!--表格操作按钮-->
            <div id="tabMenu" class="tableMenu">
            </div>
            <div class="tableWrap  modifyIssuesTable">
                <div class="tablehandle">
                    <div class="selectBox fl">
                        <span class="text">操作</span>
                        <div class="selectList">
                            <p>批量删除</p>
                        </div>
                    </div>
                    <a href="javascript:;" class="commonBtn appInfoBtn">添加资讯</a>
                </div>
                <!-- 已确认名单表格 -->
                <table id="table" class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                <span>选择</span><img src="../commres/images/selectIcon02.png" />
                            </th>
                            <th width="80px">
                            </th>
                            <th>
                                日期<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                标题
                            </th>
                            <th>
                                版块
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
                            <th>
                                排序
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="9">
                                <span><img src="../CommRes/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div id="pageContianer"></div>
            </div>
        </div>
    </div>
    <script id="tableTemp" type="text/html">
        <# for(var i=0,data;i<list.length;i++){ data=list[i] ;#>
            <tr>
                <td class="checkBox" data-id="<#=data.MappingId #>">
                    <em></em>
                </td>
                <td class="operateWrap">
                    <span class="minusIcon" data-id="<#=data.MappingId #>"></span>
                </td>
                <td>
                    <#:dd=data.PublishTime #>
                </td>
                <td>
                    <#=data.NewsTitle #>
                </td>
                <td>
                    <#=data.MicroTypeName #>
                </td>
                <td>
                    <#=data.BrowseCount #>
                </td>
                <td>
                    <#=data.PraiseCount #>
                </td>
                <td>
                    <#=data.CollCount #>
                </td>
                <td>
                    <input type="text" value="" />
                </td>
            </tr>
        <#} #>
    </script>
    <script id="dialogNews" type="text/html">
    <# for(var i=0,data;i<list.length;i++){ data=list[i] ;#>
        <tr>
            <td class="checkBox" data-val="<#=data.NewsId #>">
                <em></em>
            </td>
            <td>
                <#=data.NewsTitle#>
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
    <div id="dvNewsMicro" style="display:none;">
    <div class="commdialog dia-newsmicro">
        <div class="queryTermArea">
            <div class="moreQueryWrap">
                <a href="javascript:;" class="commonBtn queryBtn">查询</a>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">分类</em>
                <div id="sel_newstype" class="selectBox">
                </div>
            </div>
            <div class="commonSelectWrap">
                <label class="searchInput keyInput"><input id="keywords" type="text" placeholder="输入关键字"></label>
            </div>
            <div class="commonSelectWrap selectDateBox">
                <p><input type="text" value="" id="sdate"></p>
                <p><input type="text" value="" id="edate"></p>
            </div>
        </div>
        <div class="tableTopline">
            <table class="dataTable" style="display: inline-table;">
                <thead>
                    <tr class="title">
                        <th width="80px"></th>
                        <th>标题</th>
                    </tr>
                </thead>
                <tbody id="dig-body">
                    <tr>
                        <td class="checkBox">
                            <em></em>
                        </td>
                        <td>
                            暂时没有查询到任何数据
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="dig-pg-con"></div>
        </div>
        <div class="btns">
            <a href="javascript:;" class="commonBtn addBtn">保存</a> <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
    </div>
</asp:Content>
