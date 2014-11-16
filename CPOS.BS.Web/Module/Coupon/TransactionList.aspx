<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>交易记录</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
     <link href="../static/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
     <link href="../static/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/transactionList">
	<span class="packup"></span>
	<div class="commonNav">
    	<a href="Recharge.aspx">充值</a>
        <a href="Consumption.aspx" >消费</a>
        <a href="javascript:;"  class="on" >交易记录<em></em></a>
    </div>
    
    <div class="contentArea">
    	<div class="commonTitWrap">
        	<span>交易记录</span>
        </div>
        
        <%--<div class="searchArea clearfix">
            <label class="mt1">
            	<div class="commonSelBox">
                	<span id="Span1" data-day="">请选择</span>
                	<div id="Div1" class="selPulldown">
                    	<span>AAA</span>
                        <span>BBB</span>
                        <span>CCC</span>
                    </div>
                </div>
            </label>
        	<label><input type="text" id="serialNo" placeholder="流水号" /></label>
            <label class="mt1">
            	<span class="minTit">提现申请时间</span>
            	<div class="commonSelBox">
                	<span id="dateTimeText" data-day="">请选择</span>
                	<div id="dateTime" class="selPulldown">
                    	
                    </div>
                </div>
            </label>
            
            <label class="mt1 inputDate">
            	<input type="text" date-format="yyyy-mm-dd"  id="date-begin" />
            	<span>至</span>
                <input type="text" id="date-end" date-format="yyyy-mm-dd" />
            </label>
            
            
            <label class="mt1">
            	<span class="minTit">状态</span>
            	<div class="commonSelBox">
                	<span id="statusText" data-status="-1">全部</span>
                	<div id="status" class="selPulldown">
                        <span data-status="-1">全部</span>
                        <span data-status="1">成功</span>
                        <span data-status="0">失败</span>
                    </div>
                </div>
            </label>
            
            <label class="mt1"><span id="searchBtn" class="searchBtn">查询</span></label>
        </div>--%>
        
        <!-- 表格展示 -->
        <div id="list1" class="dataTableShow">
            <table id="queryResult" >
                <thead>
                    <tr class="title">
                        <td >日期时间</td>
                        <td >门店</td>
                        <td >房型</td>
                        <td >会员编号</td>
                        <td >会员名</td>
                        <td >微信昵称</td>
                        <td >手机号</td>
                        <td >身份证号</td>
                        <td >交易类型</td>
                        <td >发生金额</td>
                    </tr>
                </thead>
                <tbody id="content">
                    <tr>
                        <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="9" align="center">
                            <span>
                            <img src="../static/images/loading.gif" alt="loading..." />
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="pageContianer">
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>
    </div>
</div>

<div id="loading" class="m_loading">
    <span><img src="images/loading.gif"></span>
    <div>正在加载...</div>
    
</div>
<!-- 提示弹层 -->
<div id="ui-mask" class="ui-mask"></div>

<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ 
         var item=list[i]; 
         var remark = item.RoomType.split("~", 2); 
         if(remark.length > 1 && remark[1])
            item.RoomType = remark[1];
         else
            item.RoomType = "";
         #>
        <tr data-orderPayId="">
            <td><#=item.CreateTime#></td>
            <td><#=item.UnitName#></td>
            <td><#=item.RoomType#></td>
            <td><#=item.VipCode#></td>
            <td><#=item.VipRealName#></td>
            <td><#=item.VipName#></td>
            <td><#=item.Phone#></td>
            <td><#=item.IDCard#></td>
            <td><#=item.AmountSource#></td>
            <td><#=item.Amount#></td>
        </tr>
    <#}#>
</script>

<script id="pageTmpl" type="text/html">
    <div class="pagination">
      <a href="#" class="first" data-action="first">&laquo;</a>
      <a href="#" class="previous" data-action="previous">&lsaquo;</a>
      <input type="text" readonly="readonly" data-max-page="40" />
      <a href="#" class="next" data-action="next">&rsaquo;</a>
      <a href="#" class="last" data-action="last">&raquo;</a>
    </div>
</script>

<script type="text/javascript" src="/Module/static/js/plugin/datepicker.js" ></script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/Withdraw/js/main"></script>
</asp:Content>