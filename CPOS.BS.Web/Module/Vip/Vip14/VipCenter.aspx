<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="VipCenter.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Vip.Vip14.VipCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>页面详情页</title>
	<link rel="stylesheet" href="css/global.css" />
    <link rel="stylesheet" href="css/style.css?v=0.2" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div data-js="/Module/static/js/Vip14/page.js?ver=0.1" id="section">
    <div class="followDataArea">
        <div class="userFollow clearfix">
        <div class="commonShow orderArea">
           <div class="orderContent">
           		<span class="tit"></span>
                <div class="orderInfo">
                	<div><p>当日已发货订单数 </p><span id="sentCount" class="num">0</span></div>
                    <div><p>已支付待发货订单数</p><span id="paidNotSentCount" class="num">0</span></div>
                    <div><p>货到付款待发货订单数</p><span id="cashOnDeliveryCount" class="num">0</span></div>
                </div>
           </div>
        </div>
        <div class="commonShowLeft">
            <div class="goodArea">
            	<div class="goodContent">
                	<span class="tit"></span>
                    <div class="goodInfo">
                        <div><p>上架商品数 </p><span id="upShelfCount" class="num">0</span></div>
                        <div><p>下架商品数 </p><span id="offShelfCount" class="num">0</span></div>
                    </div>
                </div>
            </div>
            
            <div class="storeNumArea">
            	<div class="storeNumContent">
                	<span class="tit"></span>
                    <div class="storeNumInfo">
                        <div><p>上线门店数 </p><span id="onlineUnitCount" class="num">0</span></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>    <div class="followTrend">
        <div class="commonMenu_vip">
            <em class="setIcon"></em>
            <span class="on one"></span>
            <div class="textTag">今日新增 <strong id="todayVipCount">0</strong>，环比 <strong id="todayVipPercent">--</strong>。本月新增 <strong id="totalVipCount">0</strong>，环比 <strong id="MonthVipPercent">--</strong>。</di>
        </div>
        <div class="chart" id="chartSection">
            
        </div>
    </div>
    <%--<div class="activityAnalyze">
        <div class="commonMenu">
            <span class="on one"><em>活动分析</em></span>
            <span><em>替换文字</em></span>
        </div>

        <div class="tableShow">
            <table>
                <tr class="title" >
                    <td>活动名称</td>
                    <td>活动时间</td>
                    <td>扫码参与</td>
                    <td>转发数</td>
                    <td>转发带来关注</td>
                    <td>会员数</td>
                    <td>有购买会员数</td>
                    <td>销售数</td>
                </tr>

                <tr class="heightSpace" id="tr_eventlist"></tr>

            </table>
        </div>
    </div>--%>
<%--	<script type="text/template" id="tpl_table_item">
		<tr>
			<td>{{Title}}</td>
			<td>{{EventTime}}</td>
			<td>{{DecodeNo}}</td>
			<td>{{ForwardingNo}}</td>
			<td>{{ForwardingSignNo}}</td>
			<td>{{VipNo}}</td>
			<td>{{SalesVipNo}}</td>
			<td>{{SalesNo}}</td>
		</tr>
	</script>--%>
</div>
<script type="text/javascript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/main"></script>
</asp:Content>
