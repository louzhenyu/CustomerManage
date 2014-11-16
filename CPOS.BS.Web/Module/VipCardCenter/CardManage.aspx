<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>卡管理</title>
        <link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
        <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
     <link href="../static/css/datepicker.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/cardManage">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	 <a href="javascript:void(0)" onclick='AddMid("AllBrowser.aspx")'>总览</a>
         <a href="javascript:void(0)" onclick='AddMid("MakeNewCard.aspx")'>制新卡</a>
        <a href="javascript:;"  class="on" >卡管理<em></em></a>
    </div>
    
    <div class="contentArea_vipCard">
    	<div class="commonTitWrap">
        	<span>卡管理</span>
        </div>
        <div class="mana-card">
            <div class="mana-card-t">
                <ul class="clearfix">
                    <li>
                        <div class="commonSelBox"> <span data-day="" id="sourceText">渠道</span>
                            <div class="selPulldown" id="source"> <span></span> <span></span> <span></span> </div>
                        </div>
                    </li>
                    <li>
                        <div class="num-card">
                            卡序号
                            <input type="text" placeholder="去除英文和后四位" id="idFrom" />
                            — 
                            <input type="text" placeholder="去除英文和后四位" id="idTo" />
                        </div>
                    </li>
                    <li>
                        <div class="commonSelBox"> <span id="useStatusText">使用状态</span>
                            <div class="selPulldown" id="useStatus"> <span data-status="">请选择</span> <span data-status="0">未激活</span> <span data-status="1">已激活</span> <span data-status="2">已消费</span></div>
                        </div>
                    </li>
                    <li>
                        <div class="commonSelBox"> <span id="cardStatusText">卡状态</span>
                            <div class="selPulldown" id="cardStatus"> <span data-status="">请选择</span> <span data-status="0">正常状态</span> <span data-status="1">置为冻结</span> <span data-status="2">置为废卡</span></div>
                        </div>
                    </li>
                    <li>
                        <input type="text" id="intoMoney" placeholder="充值金额">
                    </li>
                </ul>
                <ul class="clearfix">
                    <li><span class="minTit">制卡日期</span>
                        <div class="commonSelBox"> <span  id="dateTimeText" data-day="">请选择</span>
                            <div class="selPulldown" id="dateTime"> <span data-status="-1">昨天</span> <span data-status="1">本周</span> <span data-status="0">本月</span> <span data-status="0">自定义</span> </div>
                        </div>
                    </li>
                        
                    <li class="mt1 inputDate" id="timeBetwwen" style="display:none;">
            	        <input type="text" readonly date-format="yyyy-mm-dd" placeholder="开始日期"  id="date-begin" />
            	        <span>至</span>
                        <input type="text" readonly id="date-end" placeholder="结束日期" date-format="yyyy-mm-dd" />
                    </li>
                    <li><span class="btn-daochu" id="searchBtn">查询</span></li>
                    <li><a href="javascript:;" id="exportAll" class="btn-daochu">全部导出</a></li>
                </ul>
                <ul class="clearfix">
                    <li><a href="javascript:;" id="btnUpdateCards" class="btn-daochu">批量更新卡</a></li>
                </ul>
            </div>
            <div class="mana-card-m" id="appendPage">
                <table id="queryResult"  width="100%" border="0" cellspacing="0" cellpadding="0">
                   
                    <tr>
                        <th ><i id="selAll"></i></th>
                        <th >卡号</th>
                        <th >城市/门店</th>
                        <th >充值金额</th>
                        <th >赠送金额</th>
                        <th >已消费金额</th>
                        <th >消费券</th>
                        <th >使用状态</th>
                        <th >卡状态</th>
                    </tr>
                    <tbody id="content">
                        <tr >
                            <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="9" align="center"> <span><img src="images/loading.gif"></span></td>
                        </tr>
                    </tbody>
                </table>
                <div id="kkpager"></div>
            </div>
        <div id="popUpdateDiv" class="hide" style="position: fixed;top: 30%;left: 35%;z-index: 9999;">
            <div class="popup" id="popbox">
                <div class="popup-t ">批量更新卡</div>
                <div class="popup-m">
                    <div class="change-card">
                        <dl class="clearfix">
                            <dt>选中卡数量</dt>
                            <dd id="cardCount">5</dd>
                        </dl>
                        <dl class="clearfix">
                            <dt>批量更新卡</dt>
                            <dd>
                                <div class="commonSelBox"> <span id="setCardStatusText">卡状态</span>
                                    <div id="setCardStatus" class="selPulldown"> <span data-status="">请选择</span> <span data-status="0">正常状态</span> <span data-status="1">置为冻结</span> <span data-status="2">置为废卡</span></div>
                                </div>
                            </dd>
                        </dl>
                    </div>
                </div>
                <div class="popup-b"> <a href="javascript:;" id="sureUpdate">确定</a> <a href="javascript:;" id="sureBack" class="hintClose">返回</a> </div>
            </div>
        </div>
        
    </div>

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
        <tr>
            <td ><i class="toSel" data-cardid="<#=item.CardID#>"></i></td>
                        <td ><#=item.CardNo#></td>
                        <td ><#=item.ChannelTitle#></td>
                        <td ><#=item.Amount#></td>
                        <td ><#=item.Bonus#></td>
                        <td ><#=item.ConsumedAmount#></td>
                        <td>0</td>
                        <td >
                            <#
                                if(item.UseStatus==0){
                            #>
                                <#='未激活'#>
                            <#}else if(item.UseStatus==1){#>
                                <#='已激活'#>
                            <#}else if(item.UseStatus==2){#>
                                <#='已激活'#>
                            <#}#>
                        </td>
                        <td  class="last">
                            <a href="javascript:;">
                                <#
                                    if(item.CardStatus==0){
                                #>
                                    <#='正常状态'#>
                                <#}else if(item.CardStatus==1){#>
                                    <#='置为冻结'#>
                                <#}else if(item.CardStatus==2){#>
                                    <#='置为废卡'#>
                                <#}#>
                                
                            </a>
                            <dl class="set-layer"   data-itemId="<#=item.CardID#>">
                                <b></b>
                                <dt>设置为</dt>
                                
                                <dd><span data-status="0" data-text="正常状态" class="<#=item.CardStatus==0?'on':''#>"></span>正常状态</dd>
                                <dd><span data-status="1" data-text="置为冻结" class="<#=item.CardStatus==1?'on':''#>"></span>置为冻结</dd>
                                <dd><span data-status="2" data-text="置为废卡" class="<#=item.CardStatus==2?'on':''#>"></span>置为废卡</dd>
                            </dl>
                        </td>
        </tr>
    <#}#>
</script>
<script id="tpl_date" type="text/html">
    <span data-day="">请选择</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-day="<#=item.date#>"><#=item.dateName#></span>
    <#}#>
    <span data-day="999">自定义</span>
</script>
<script id="tpl_source" type="text/html">
    <span data-status="">全部</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-status="<#=item.ChannelID#>"><#=item.ChannelTitle#></span>
    <#}#>
</script>
<script type="text/javascript" src="/Module/static/js/plugin/datepicker.js" ></script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/VipCardCenter/js/main.js"></script>
</asp:Content>