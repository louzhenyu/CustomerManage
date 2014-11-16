<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" CodeBehind="NewsType.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.College.News.NewsType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="/Module/static/css/artDialog.css" rel="stylesheet" type="text/css" />
    <title>资讯分类管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="section" class="contentArea" data-js="/Module/College/News/js/NewsType">
        <!--详情菜单-->
        <div class="subMenu">
            <ul class="clearfix">
                <li class="nav01"><a href="NewsList.aspx">资讯内容</a></li>
                <li class="nav02 on"><a href="NewsType.aspx">资讯分类</a></li>
                <!--<li class="nav03">资讯标签</li>-->
            </ul>
        </div>
        <div class="subMenu-content">
            <!--表格操作按钮-->
            <div class="tableWrap">
                <div class="tablehandle">
                    <div class="selectBox fl">
                        <span class="text">操作</span>
                        <div class="selectList">
                            <p>批量删除</p>
                        </div>
                    </div>
                    <a href="javascript:;" class="commonBtn appInfoBtn">添加分类</a>
                </div>
                <!-- 已确认名单表格 -->
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<img src="../commres/images/selectIcon02.png" alt="" /><div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p data-val="select">全选本页</p>
                                    <p data-val="cancel">取消选择</p>
                                </div>
                            </th>
                            <th>
                                操作<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                日期<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                类型名称
                            </th>
                            <th>
                                级别<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                父类型名称<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot id="footer" style="display: none;">
                        <tr>
                            <td colspan="9" class="tfooter">
                                <span>
                                    <img src="../CommRes/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tfoot>
                </table>
                <div id="kkpager" style="text-align: center;">
                </div>
            </div>
        </div>
    </div>
    <div id="opertip" style="display:none;">
    <div class="commdialog dig-newstype">
        <div class="row">
            <em class="tit">父类型</em>
            <div id="pre-typeList" class="selectBox w120">
            </div>
        </div>
        <div class="row">
            <em class="tit"><span class="fontRed">*</span> 类型名称</em>
            <p class="input"><input id="typename" type="text" value="" /></p>
        </div>
        <div class="btns">
        	<a class="commonBtn addBtn" href="javascript:;">保存</a>
          <a class="commonBtn cancelBtn" href="javascript:;">取消</a>
        </div>
    </div>
    </div>
    <script id="tableTemp" type="text/html">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <tr id="<#=idata.NewsTypeId #>">
                <td class="checkBox" data-id="<#=idata.NewsTypeId #>">
                 <em></em>
                </td>
                <td class="operateWrap">
                     <span data-id="<#=idata.NewsTypeId #>" class="editIcon"></span>
                     <span data-id="<#=idata.NewsTypeId #>" class="delBtn delIcon"></span>
                </td>
                <td>
                 <#=idata.CreateTime.replace(/T\d{2}:\d{2}:\d{2}.\d{0,3}$/, "") #>
                </td>
                <td>
                 <#=idata.NewsTypeName #>
                </td>
                <td>
                 <#=idata.TypeLevel #>
                </td>
                <td>
                 <#=idata.ParentTypeName #>
                </td>               
            </tr>
        <#} #>
    </script>
    <script id="selType" type="text/html">
        <span class="selected text" data-val="<#=list[0].id #>"><#=list[0].name #></span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.NewsTypeId #>" data-level="<#=idata.TypeLevel #>"><#=idata.NewsTypeName #></p>
        <#}#>
        </div>
    </script>
</asp:Content>
