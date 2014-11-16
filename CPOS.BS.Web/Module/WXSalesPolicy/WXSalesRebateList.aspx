<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Framework/MasterPage/CPOS.Master" CodeBehind="WXSalesRebateList.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.WXSalesPolicy.WXSalesPolicyRateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/css/common-layout.css" rel="stylesheet" type="text/css" />
    <link href="../styles/css/reset-pc.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body cache></body>
    <div id="section" data-js="scripts/salesRebateList" class="queryTermArea" style="display:inline-block;">
        <div class="item clearfix">
            <div class="commonSelectWrap" style="float:right;">
                <a href="javascript:;" class="commonBtn queryBtn">查询</a>
            </div>
            <div class="commonSelectWrap" style="position:relative;">
                <em class="tit">门店：</em>
                <label class="searchInput unit"><input id="txtUnit"   type="text" /></label>
                <ul id="ztreeUnit" class="ztree" style="display:none;position: absolute;left: 120px;background:#FFF;margin-top: 31px;width:173px;z-index:100;"></ul>
            </div>
             <div class="commonSelectWrap">
                <em class="tit">操作员：</em>
                <label class="searchInput"><input  id="txtOperator" type="text"/></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">日期：</em>
                <label class="searchInput"><input class="datepicker" id="dtBegin" type="text" /></label>
                <span class="separator">~</span>
                <label class="searchInput"><input  class="datepicker" id="dtEnd" type="text" /></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">交易金额：</em>
                <label class="searchInput"><input id="amountBegin"  type="text"/></label>
                <span class="separator">~</span>
                <label class="searchInput"><input id="amountEnd" type="text" /></label>
            </div>
        </div>
        <div class="tips" style="padding:0;"></div>
         <div id="menuItems" class="tableHandleBox">
            <span style="margin-right:40px;" class="commonBtn _export">全部导出</span>
        </div> 
        <div class="tableWrap">
            <div class="tablehandle">
                <h3 class="count">共查询到<span id="resultCount">0</span>条数据</h3>  
                 <!--
                <div class="selectBox fl">
                    <span class="text">操作</span>
                    <div class="selectList">
                        <p>操作1</p>
                        <p>操作2</p>
                    </div>
                </div>
                
                <div class="selectBox filterIcon fl">
                    <span class="text">筛选</span>
                    <div class="selectList">
                        <p>筛选1</p>
                        <p>筛选2</p>
                    </div>
                </div>
                -->
            </div>
            <table class="dataTable" style="display:inline-table;">
                <thead  id="thead">
                    <tr class="title">
                        <%--<th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>--%>
                        <th>时间</th>
                        <th>门店</th>
                        <th>操作员</th>
                        <th>交易金额</th>
                        <th>返利</th>
                        <th>会员昵称</th>
                        <th>订单编号</th>
                    </tr>
                </thead>
                <tbody id="content">
                </tbody>
            </table>
            <div id="pageContianer">
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>
    </div>
    <script id="tpl_body" type="text/html">
        <#var len=list.length;#>
        <#if(len>0){#>
        <#for(var i=0;i<list.length;i++){ var o=list[i];#>
            <tr data-id="<#=o.Id#>">
                <td><#=o.HandleTime#></td>
                <td><#=o.UnitName#></td>
                <td><#=o.Operator#></td>
                <td><#=o.SalesAmount#></td>
                <td><#=o.ReturnAmount#></td>
                <td><#=o.VipName#></td>
                <td><#=o.OrderNo#></td>
            </tr>
        <#}#>
        <#} else {#>
        <tr><td colspan="7">无查询结果</td></tr>
        <#}#>
    </script>
    <script type="text/javascript" src="/Module/static/js/lib/require.min.js" data-main="scripts/main"></script>
</asp:Content>
