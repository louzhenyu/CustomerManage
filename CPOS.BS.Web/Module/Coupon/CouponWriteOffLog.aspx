<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>优惠券核销</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
        <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/datepicker.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/CouponWriteOffLog">
	<span class="packup"></span>
	<div class="commonNav_coupon">
    	 <a href="javascript:void(0)" onclick='AddMid("couponWriteOff.aspx")'>优惠券核销</a>
        <a href="javascript:;" class="on" >使用记录</a>
    </div>
    
    <div class="contentArea_coupon">
    	<div class="commonTitWrap">
        	<span>使用记录</span>
        </div>
        <div class="mana-card">
            <div class="mana-card-t">
                <ul class="clearfix">
                    <li>
                        <div class="commonSelBox"> <span data-value="" id="couponTypeText">优惠券类型</span>
                            <div class="selPulldown" id="couponType"> <span></span> <span></span> <span></span> </div>
                        </div>
                    </li><%--
                    <li>
                        <input type="text" id="couponName" placeholder="优惠券名称">
                    </li>--%>
                    <li>
                        <input type="text" id="couponCode" placeholder="优惠券编号">
                    </li>
                    
                    <li>
                        <input type="text" id="Comment" placeholder="备注">
                    </li>
                    <li>
                        <input type="text" id="VipName" placeholder="核销人">
                    </li>
                    <li  class="mt1 inputDate" id="timeBetwwen" >
            	       <%--<input type="text" readonly date-format="yyyy-mm-dd" placeholder="使用日期"  id="beginTime" />--%>
                       <input type="text"  date-format="yyyy-mm-dd" id="UseTime" placeholder="核销起始日期"/>
                       <input type="text"  date-format="yyyy-mm-dd" id="UseEndTime" placeholder="核销结束日期"/>
                    </li>
                    <li><span class="btn-daochu" id="searchBtn">查询</span></li>
                    <li><span class="btn-daochu" id="exportBtn">导出</span></li>
                    <%--<li><a href="javascript:;" id="exportAll" class="btn-daochu">全部导出</a></li>--%>
                </ul>
               <%-- <ul class="clearfix">
                    <li><a href="javascript:;" id="btnUpdateCards" class="btn-daochu">批量更新卡</a></li>
                </ul>--%>
            </div>
            <div class="mana-card-m" id="appendPage">
                <table id="queryResult"  width="95%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th align="center" scope="col">优惠券类型</th>
                        <th align="center" scope="col">优惠券号</th>
                        <th align="center" scope="col">优惠券名称</th>
                        <th align="center" scope="col">核销日期</th>
                        <th align="center" scope="col">备注</th>
                        <th align="center" scope="col">核销人</th>
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
        <tr data-item="<#=JSON.stringify(item)#>">
            <td><#=item.CouponTypeName#></td>           
            <td><#=item.CouponCode#></td>
            <td><#=item.CouponName#></td>
            <td><#=item.UseTime#></td>
            <td><#=item.Comment#></td>
            <td><#=item.CreateByName#></td>
        </tr>
    <#}#>
</script>

<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr>
            <td><#=item.CouponTypeName#></td>
            <td><#=item.CouponCode#></td>
            <td><#=item.CouponName#></td>
            <td><#=item.UseTime#></td>
            <td><#=item.Comment#></td>
            <td><#=item.CreateByName#></td>
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