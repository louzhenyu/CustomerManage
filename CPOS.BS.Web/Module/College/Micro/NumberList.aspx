<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" CodeBehind="NumberList.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.College.Micro.NumberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="/Module/static/css/artDialog.css" rel="stylesheet" type="text/css" />
    <title>期刊管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="/Module/College/Micro/js/NumberList">
        <div class="subMenu-content">
            <!--个别查询-->
            <div class="queryTermArea">
                <div class="item">
                    <div class="moreQueryWrap">
                        <a href="javascript:;" class="commonBtn queryBtn">查询</a> <a href="javascript:;" class="more">
                            更多查询条件</a>
                    </div>
                    <div class="commonSelectWrap issuesBox">
                        <em class="tit">刊号</em>
                        <div id="microNumsSelect" class="selectBox">
                        </div>
                    </div>
                    <div class="commonSelectWrap">
                        <label class="searchInput">
                            <input id="inKeyword" type="text" placeholder="输入关键字"></label>
                    </div>
                </div>
            </div>
            <!--表格操作按钮-->
            <div class="tableWrap">
                <div class="tablehandle">
                    <div class="selectBox fl">
                        <span class="text">操作</span>
                        <div class="selectList">
                            <p>
                                批量删除</p>
                        </div>
                    </div>
                    <a href="javascript:;" class="commonBtn appInfoBtn">创建期刊</a>
                </div>
                <!-- 已确认名单表格 -->
                <table id="table" class="dataTable journalTable" style="display: inline-table;">
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
                                刊号<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th width="180px">
                                期刊主题
                            </th>
                            <th>
                                期刊封面
                            </th>
                            <th width="160px">
                                期刊链接
                            </th>
                            <th>
                                版块与资讯
                            </th>
                            <th>
                                创建时间
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
    <div id="dvMicroNumer" style="display: none;">
        <div class="commdialog dig-number">
            <div class="row">
                <em class="tit"><span class="fontRed">*</span>刊号</em>
                <p class="input">
                    <input id="ipNum" type="text" /></p>
            </div>
            <div class="row">
                <em class="tit"><span class="fontRed">*</span>主题</em>
                <p class="input">
                    <input id="ipTitle" type="text" class="w520" /></p>
            </div>
            <div class="row thumbrow">
                <em class="tit"><span class="fontRed">*</span>封面</em>
                <div class="thumbBox">
                    <div class="uploadBtn">
                    </div>
                </div>
            </div>
            <div class="btns">
                <a class="commonBtn addBtn" href="javascript:;">保存</a> <a class="commonBtn cancelBtn"
                    href="javascript:;">取消</a>
            </div>
        </div>
    </div>
    <script id="tableTemp" type="text/html">
        <$ for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
            <tr id="<$=idata.NumberId $>">
                <td class="checkBox" data-id="<$=idata.NumberId $>">
                 <em></em>
                </td>
                <td class="operateWrap">
                     <span data-id="<$=idata.NumberId $>" class="editIcon editBtn"></span>
                     <span data-id="<$=idata.NumberId $>" class="delBtn delIcon"></span>
                </td>
                <td>
                 <$=idata.Number $>
                </td>
                <td class="al">
                 <$=idata.NumberName $>
                </td>
                <td>
                 <img class="tableImg" src="<$=idata.CoverPath $>" alt="" />
                </td>
                <td class="al">
                 <$=idata.NumberUrl $>
                </td>
                <td>
                 <$for(var j=0,chdata;j<idata.TypeList.length;j++){chdata=idata.TypeList[j]; $>
                 <a data-id="<$=chdata.TypeId $>" class="links" href="NumberNewMap.aspx?TypeId=<$=chdata.TypeId $>&NumberId=<$=idata.NumberId $>"><$=chdata.TypeName $>(<$=chdata.TypeCount $>)</a><br/>
                 <$}$>
                </td>
                <td>
                <$=idata.CreateTime.replace(/T\d{2}:\d{2}:\d{2}.\d{0,3}$/, "") $>
                </td>
            </tr>
        <$} $>
    </script>
    <script id="selectTemp" type="text/html">
        <span id="microNumsText" class="selected text" data-val="">全部</span>
        <div id="microNums" class="selectList">
        <$ for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
            <p class="option" data-val="<$=idata.id $>"><$=idata.name $></p>
        <$}$>
        </div>
    </script>
</asp:Content>
