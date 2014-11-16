<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />

     <link href="../static/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
     <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <title>入账查询</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/entryQuery">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	<a href="javascript:void(0)" onclick='AddMid("ApplyWithdraw.aspx")'>申请提现</a>
        <a href="javascript:;" class="on">入账查询</a>
        <a href="javascript:void(0)" onclick='AddMid("WithdrawRecord.aspx")'>提现记录查询<em></em></a>
    </div>
    
    <div class="contentArea_withdraw">
    	<div class="commonTitWrap">
        	<span>入账查询</span>
        </div>
        
        <div class="searchArea clearfix">
        	<label><input type="text" id="orderNo" placeholder="订单编号" /></label>
            <label class="mt1">
                <span class="minTit">订单来源</span>
            	<div class="commonSelBox">
                	<span id="orderSource">请选择</span>
                	<div id="source" class="selPulldown">
                        
                    </div>
                </div>
            </label>
            
            <label class="mt1">
            	<span class="minTit">状态</span>
            	<div class="commonSelBox">
                	<span id="statusText">请选择</span>
                	<div id="status" class="selPulldown">
                       
                    </div>
                </div>
            </label>
            
            <label class="mt1"><span id="search" class="searchBtn">查询</span></label>
        </div>
        
        <!-- 表格展示 -->
        <div class="dataTableShow"  >
            <table  id="queryResult" class="display" cellspacing="0" width="100%">
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
                <tbody id="content">
                    
                </tbody>
                <tfoot id="footer" style="display:none;">
                     <tr>
                        <td colspan="9" class="tfooter">
                            <span><img src="images/loading.gif"></span>
                        </td>
                     </tr>
                </tfoot>
            </table>
            <div id="kkpager"></div>
        </div>
        
    </div>

</div>
<script id="tpl_content" type="text/html">
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
<script id="tpl_source" type="text/html">
    <span data-status="">全部</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-status="<#=item.VipSourceID#>"><#=item.VipSourceName#></span>
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
<script id="tpl_status" type="text/html">
    <span data-status="">全部</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-status="<#=item.OptionValue#>"><#=item.OptionText#></span>
    <#}#>
</script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/Withdraw/js/main"></script>
</asp:Content>