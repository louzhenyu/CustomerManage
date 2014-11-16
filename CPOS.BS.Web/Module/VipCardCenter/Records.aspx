<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>交易记录</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
     <link href="../static/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
     <link href="../static/css/datepicker.css" rel="stylesheet" type="text/css" />
     <link href="../static/css/pagination.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/withdrawRecord">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	 <a href="javascript:void(0)" onclick='AddMid("Recharge.aspx")'>充值</a>
         <a href="javascript:void(0)" onclick='AddMid("Consumption.aspx")' >消费</a>
        <a href="javascript:;"  class="on" >交易记录<em></em></a>
    </div>
    
    <div class="contentArea_vipCard">
    	<div class="commonTitWrap">
        	<span>交易记录</span>
        </div>
        
        <div class="searchArea clearfix">
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
        </div>
        
        <!-- 表格展示 -->
        <div id="list1" class="dataTableShow">
            <table id="queryResult" >
                <thead>
                    <tr class="title">
                        <td >流水号</td>
                        <td >提现申请时间</td>
                        <td >企业名称</td>
                        <td >提现银行</td>
                        <td >收款银行账号</td>
                        <td >提现金额</td>
                        <td >状态</td>
                        <td >操作</td>
                        <td >备注</td>
                    </tr>
                </thead>
                <tbody id="content">
                    
                
                    
                </tbody>
            </table>
        </div>
        
    </div>

</div>
<!-- 弹层入账列表 -->
<div id="listDiv" class="popUpArea" style="display:none">
	<h2 class="titBox"><span>入账列表</span><em class="hintClose"></em></h2>
    <!-- 表格展示 -->
    <div id="list2" class="dataTableShow"  >
        <table  id="popTableResult" class="display" cellspacing="0" width="100%">
            <thead>
                    <tr class="title">
                    <td >订单号</td>
                    <td >支付流水号</td>
                    <td >订单支付金额</td>
                    <td >订单来源</td>
                    <td >支付方式</td>
                    <td >付款时间</td>
                    <td >费率</td>
                    <td >可提现金额</td>
                    <td >状态</td>
                </tr>
            </thead>
            <tbody id="popContent">
                 
            </tbody>
        </table>
        <div id="kkpager2"></div>
    </div>
</div>

<div id="loading" class="m_loading">
    <span><img src="images/loading.gif"></span>
    <div>正在加载...</div>
    
</div>
<!-- 提示弹层 -->
<div id="ui-mask" class="ui-mask"></div>
<script id="tpl_popContent" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-orderPayId="<#=item.OrderPayId#>">
            <td><#=item.OrderNo#></td>
            <td><#=item.SerialPay#></td>
            <td><#=item.PayAmount+"元"#></td>
            <td><#=item.VipSourceName#></td>
            <td><#=item.PaymentName#></td>
            <td><#=item.PayTime#></td>
            <td><#=item.AladingRate*100+"%"#></td>
            <td><#=item.WithdrawalAmount+"元"#></td>
            <td><#=item.OrderPayStatusName#></td>
        </tr>
    <#}#>
</script>
<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-orderPayId="<#=item.OrderPayId#>">
            <td><#=item.SerialNo#></td>
            <td><#=item.WithdrawalTime#></td>
            <td><#=item.CustomerName#></td>
            <td><#=item.ReceivingBank#></td>
            <td><#=item.BankAccount#></td>
            <td><#="￥"+item.WithdrawalAmount+"元"#></td>
            <td class="<#=((item.WithdrawalStatus.indexOf("成功"))>=0||(item.WithdrawalStatus.indexOf("已提现"))>=0)?'status':'status failure'#>"><span><#=item.WithdrawalStatus#></span></td>
            <td data-id="<#=item.WithdrawalId#>" class="handle"></td>
            <td><#=item.FailureReason#></td>
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
<script id="tpl_date" type="text/html">
    <span data-day="-1">请选择</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-day="<#=item.date#>"><#=item.dateName#></span>
    <#}#>
    <span data-day="0">自定义</span>
</script>
<script type="text/javascript" src="/Module/static/js/plugin/datepicker.js" ></script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/Withdraw/js/main"></script>
</asp:Content>