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
    <div class="allPage" id="section" data-js="js/couponManage">
	<span class="packup"></span>
	<div class="commonNav_coupon">
    	<%--<a href="AllBrowser.aspx">总览</a>--%>
         <a href="javascript:void(0)" onclick='AddMid("Generate.aspx")'>生成优惠券</a>
        <a href="javascript:;"  class="on" >优惠券管理<em></em></a>
    </div>
    
    <div class="contentArea_coupon">
    	<div class="commonTitWrap">
        	<span>优惠券管理</span>
        </div>
        <div class="mana-card">
            <div class="mana-card-t">
                <ul class="clearfix">
                    <li>
                        <div class="commonSelBox"> <span data-value="" id="couponTypeText">优惠券类型</span>
                            <div class="selPulldown" id="couponType"> <span></span> <span></span> <span></span> </div>
                        </div>
                    </li>
                    <li>
                        <input type="text" id="couponName" placeholder="优惠券名称">
                    </li>
                    <li>
                        <input type="text" id="couponCode" placeholder="优惠券编号">
                    </li>
                    <li>
                        <div class="commonSelBox"> <span id="useStatusText" data-value="0">未发放</span>
                            <div class="selPulldown" id="useStatus"> <span data-value="">请选择</span> <span data-value="0">未发放</span> <span data-value="1">已发放</span> <span data-value="2">已使用</span></div>
                        </div>
                    </li>
                    <li>
                        <div class="commonSelBox"> <span id="cardStatusText"  data-status="">优惠券状态</span>
                            <div class="selPulldown" id="cardStatus"> <span data-status="">请选择</span> <span data-status="0">正常状态</span> <%--<span data-status="1">置为冻结</span> --%><span data-status="1">废除状态</span></div>
                        </div>
                    </li>
<%-- 有效期注释 2014-9-26 <li><span class="minTit">有效期</span>
                        <div class="commonSelBox"> <span  id="dateTimeText" data-day="">请选择</span>
                            <div class="selPulldown" id="dateTime"> <span data-status="-1">昨天</span> <span data-status="1">本周</span> <span data-status="0">本月</span> <span data-status="0">自定义</span> </div>
                        </div>
                    </li>                        
                    <li class="mt1 inputDate" id="timeBetwwen" style="display:none;">
            	        <input type="text" readonly date-format="yyyy-mm-dd" placeholder="开始日期"  id="date-begin" />
            	        <span>至</span>
                        <input type="text" readonly id="date-end" placeholder="结束日期" date-format="yyyy-mm-dd" />
                    </li>--%>
                    <li><span class="btn-daochu" id="searchBtn">查询</span></li>
                    <%--<li><a href="javascript:;" id="exportAll" class="btn-daochu">全部导出</a></li>--%>
                </ul>
               <%-- <ul class="clearfix">
                    <li><a href="javascript:;" id="btnUpdateCards" class="btn-daochu">批量更新卡</a></li>
                </ul>--%>
            </div>
            <div class="mana-card-m" id="appendPage">
                <table id="queryResult"  width="95%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th ><i id="selAll"></i></th>
                        <th>操作</th>
                        <th>优惠券类型</th>
                        <th >优惠券名称</th>
                        <th >优惠券编号</th>
                        <th >使用状态</th>
                        <th >有效时间</th>
                        <th >优惠券状态</th>
                    </tr>
                    <tbody id="content">
                        <tr >
                            <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="9" align="center"><span><img src="images/loading.gif"></span></td>
                        </tr>
                    </tbody>
                </table>
                <div id="kkpager"></div>
            </div>
        <div id="popUpdateDiv" class="hide" style="position: fixed;top: 30%;left: 35%;z-index: 9999;">
            <div class="popup" id="popbox">
                <div class="popup-t ">修改优惠券编号</div>
                <div class="popup-m">
                    <div class="change-card">
<%--                        <dl class="clearfix">
                            <dt>选中卡数量</dt>
                            <dd id="cardCount">5</dd>
                        </dl>--%>
                        <dl class="clearfix">
                            <dt>优惠券编号</dt>
                            <dd>
                                <div class="commonSelBox"> 
                                    <input type="text" id="updateCouponCodeText" placeholder="优惠券编号" value="" />
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
            <td ><i class="toSel" data-cardid="<#=item.CouponID#>"></i></td>
            <td ><a href="javascript:void(0)" class="updateCode" data-couponid="<#=item.CouponID#>" data-couponcode="<#=item.CouponCode#>">修改编号</a></td>
            <td ><#=item.CouponTypeName#></td>
            <td ><#=item.CouponName#></td>
            <td ><#=item.CouponCode#></td>
            <td >
                <#if(item.CouponUseStatus==0){#>
                    <#='未发放'#>
                <#}else if(item.CouponUseStatus==1){#>
                    <#='已发放'#>
                <#}else if(item.CouponUseStatus==2){#>
                    <#='已使用'#>
                <#}#>
            </td>
            <td ><#=item.BeginTime#>--<#=item.EndTime#></td>
            <# if(item.CouponUseStatus!=2){ #>
            <td  class="last">             
                    <a href="javascript:;">
                        <#
                            if(item.IsDelete==0){                           
                        #> 
                            <#='正常状态'#>
                        <#}else if(item.IsDelete==1){#>
                            <#='废除状态'#>
                        <#}#>
                    </a>
               
                <dl class="set-layer"   data-itemId="<#=item.CouponID#>"   data-cardid="-1"  id="popUpdateStatus">
                    <b></b>
                    <dt>设置为</dt>
                    <dd><span data-status="0" data-text="正常状态" class="<#=item.IsDelete==0?'on':''#>"></span>正常状态</dd>          
                    <dd><span data-status="1" data-text="废除状态" class="<#=item.IsDelete==1?'on':''#>"></span>置为废除</dd>
                </dl>
            </td>
            <# } else{#>
            <td></td>
            <# }#>
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
<script id="tpl_couponType" type="text/html">
    <span data-status=""></span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-value="<#=item.CouponTypeID#>"><#=item.OriginalCouponTypeName#></span>
    <#}#>
</script>
<script type="text/javascript" src="/Module/static/js/plugin/datepicker.js" ></script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/Coupon/js/main.js"></script>
</asp:Content>