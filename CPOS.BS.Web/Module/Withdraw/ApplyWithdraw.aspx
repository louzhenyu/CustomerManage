<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>申请提现</title>
        <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/applyWithdraw">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	<a href="javascript:;" class="on" >申请提现</a>
        <a href="javascript:void(0)" onclick='AddMid("EntryQuery.aspx")'>入账查询</a>
        <a href="javascript:void(0)" onclick='AddMid("WithdrawRecord.aspx")'  >提现记录查询<em></em></a>
    </div>
   
    <div class="contentArea_withdraw">
    	<div class="commonTitWrap">
        	<span>申请提现</span>
        </div>
        <div class="withdrawInfo clearfix">
        	<div class="leftBox">
            	<p>
                    <span class="tit">企业名称</span>
                    <span id="company">暂无</span>
                </p>
                <p>
                    <span class="tit">提现总额</span>
                    <span id="totalMoney">0</span>
                </p>
                <p>
                    <span class="tit">已到账总额</span>
                    <span id="intoMoney">0</span>
                </p>
                
                <p>
                    <span class="tit">收款银行</span>
                    <span id="bank">暂无</span>
                </p>
                <p>
                    <span class="tit">收款账号</span>
                    <span id="cardId">暂无</span>
                </p>
                <p  class="mb2">
                    <span class="tit">开户行</span>
                    <span id="area">暂无</span>
                </p>
            </div>
            <div class="rightBox">
            	<div class="fl">
                    <p>
                        <span class="tit">可提现金额</span>
                        <span><strong id="canGetMoney">0</strong>元</span>
                    </p>
                    
                    <p>
                        <span class="tit">等待出账金额</span>
                        <span><strong id="waitMoney">0</strong>元</span>
                    </p>
                    
                    <p>
                        <span class="tit">上次提现时间</span>
                        <span id="lastTime">2014-05-28</span>
                    </p>
                </div>
                
                <div class="fl">
                    <span id="wishMoneyBtn" class="applyWithdrawBtn">申请提现</span>
                    <span id="changePassBtn" class="modifyWithdrawBtn">修改提现密码</span>
                </div>
            
            </div>
        </div>
        
        
        <div class="commonTitWrap">
        	<span>结算信息</span>
        </div>
        <div class="clearingInfo">
        	<h2>结算银行信息</h2>
            <div class="clearingContent withdrawInfo">
                <p>
                    <span class="tit">支付宝 汇率</span>
                    <span id="alipayPercent">2.0%</span>
                </p>
                <p>
                    <span class="tit">银联 汇率</span>
                    <span id="yinlianPercent">2.0%</span>
                </p>
                <p>
                    <span class="tit">结算周期</span>
                    <span id="weekDay">T+1</span>
                </p>
                <p class="mb2">
                    <span class="tit">最低结算费用</span>
                    <span><strong id="lessMoney">1000</strong>元人民币</span>
                </p>
            </div>
            <div id="textCon" class="clearingExplain">
            </div>
        </div>
    </div>

</div>


<!-- 提示弹层 -->
<div id="ui-mask" class="ui-mask"></div>
<div class="commonHintArea" id="tips" style="display:none">
	<h2 class="titBox"><span>提示</span><em class="hintClose"></em></h2>
    <div class="hintText">
    	<p id="text">不允许提现。</p>
    </div>
    <div>
    	<span id="sureOk" class="hintBtn">确定</span>
    </div>
</div>

<!-- 修改提现密码 -->
<div id="changePassDiv" class="commonHintArea" style="display:none">
	<h2 class="titBox"><span>修改提现密码</span><em class="hintClose"></em></h2>
    <div class="hintForm">
    	<p><input type="password" id="currentPass" placeholder="输入当前密码" /></p>
        <p><input type="password" id="newPass" placeholder="输入新密码" /></p>
        <p><input type="password" id="againPass" placeholder="再次输入" /></p>
    </div>
    <div>
    	<span id="sureChangePass" class="hintBtn">确定</span>
    </div>
</div>

<!-- 提现申请-弹层 -->
<div id="getMoneyDiv" class="commonHintArea" style="display:none">
	<h2 class="titBox"><span>提现申请</span><em class="hintClose"></em></h2>
    <div class="hintForm">
    	<p class="applyInput"><input type="password" id="getMoneyPass" placeholder="输入您的密码" /></p>
    </div>
    <div>
    	<span id="sureGet" class="hintBtn">确定</span>
    </div>
</div>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/Withdraw/js/main"%>"></script>
</asp:Content>