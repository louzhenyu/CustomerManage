<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.CollegeNews.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea">
        <!--详情菜单-->
        <div class="subMenu">
            <ul class="clearfix">
                <li class="nav01 on">资讯内容</li>
                <li class="nav02">资讯分类</li>
                <li class="nav03">资讯标签</li>
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
                    <div class="commonSelectWrap">
                        <em class="tit">分类</em>
                        <div class="selectBox">
                            <span class="text">分类1</span>
                            <div class="selectList">
                                <p>
                                    分类1</p>
                                <p>
                                    分类2</p>
                            </div>
                        </div>
                    </div>
                    <div class="commonSelectWrap">
                        <em class="tit">关键字</em>
                        <label class="searchInput">
                            <input type="text" value=""></label>
                    </div>
                    <div class="commonSelectWrap selectDateBox">
                        <span class="tit">从</span>
                        <p>
                            <input type="text" value="2014-6-30" /><em class="dateIcon"></em></p>
                        <span class="tit">至</span>
                        <p>
                            <input type="text" value="2014-7-30" /><em class="dateIcon"></em></p>
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
                                操作1</p>
                            <p>
                                操作2</p>
                        </div>
                    </div>
                    <a href="javascript:;" class="commonBtn appInfoBtn">添加资讯</a>
                </div>
                <!-- 已确认名单表格 -->
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<img src="images/selectIcon02.png" alt="" /><div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                操作<img src="images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                日期<img src="images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                标题
                            </th>
                            <th>
                                分类<img src="images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                浏览数<img src="images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                赞数<img src="images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                分享数<img src="images/selectIcon02.png" alt="" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="checkBox">
                                <em></em>
                            </td>
                            <td class="operateWrap">
                                <span class="editIcon"></span><span class="delIcon"></span>
                            </td>
                            <td>
                                2014-5-17
                            </td>
                            <td>
                                “共饮一江水，畅享未来情”半年分享会
                            </td>
                            <td>
                                活动
                            </td>
                            <td>
                                1234
                            </td>
                            <td>
                                21
                            </td>
                            <td>
                                111
                            </td>
                        </tr>
                        <tr>
                            <td class="checkBox">
                                <em></em>
                            </td>
                            <td class="operateWrap">
                                <span class="editIcon"></span><span class="delIcon"></span>
                            </td>
                            <td>
                                2014-5-17
                            </td>
                            <td>
                                “共饮一江水，畅享未来情”半年分享会
                            </td>
                            <td>
                                活动
                            </td>
                            <td>
                                1234
                            </td>
                            <td>
                                21
                            </td>
                            <td>
                                111
                            </td>
                        </tr>
                        <tr>
                            <td class="checkBox">
                                <em></em>
                            </td>
                            <td class="operateWrap">
                                <span class="editIcon"></span><span class="delIcon"></span>
                            </td>
                            <td>
                                2014-5-17
                            </td>
                            <td>
                                “共饮一江水，畅享未来情”半年分享会
                            </td>
                            <td>
                                活动
                            </td>
                            <td>
                                1234
                            </td>
                            <td>
                                21
                            </td>
                            <td>
                                111
                            </td>
                        </tr>
                        <tr>
                            <td class="checkBox">
                                <em></em>
                            </td>
                            <td class="operateWrap">
                                <span class="editIcon"></span><span class="delIcon"></span>
                            </td>
                            <td>
                                2014-5-17
                            </td>
                            <td>
                                “共饮一江水，畅享未来情”半年分享会
                            </td>
                            <td>
                                活动
                            </td>
                            <td>
                                1234
                            </td>
                            <td>
                                21
                            </td>
                            <td>
                                111
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
