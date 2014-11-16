<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
    <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/generate">
	<span class="packup"></span>
	<div class="commonNav_coupon">
    	<%--<a href="AllBrowser.aspx" >总览</a>--%>
        <a href="javascript:;" class="on">生成优惠券</a>
       <a href="javascript:void(0)" onclick='AddMid("CouponManage.aspx")'>优惠券管理</a>
    </div>
    <div class="contentArea_coupon">
    	<div class="commonTitWrap">
        	<span>生成优惠券</span>
        </div>
        <div class="create-card">
          <dl class="clearfix">
            <dt>优惠券类型</dt>
            <dd>
              <div class="commonSelBox"> <span data-value="" id="couponTypeText">优惠券类型</span>
                <div class="selPulldown" id="couponType"> <span></span> <span></span> <span></span> </div>
              </div>
            </dd>
            <dd>
          </dl>
          <dl class="clearfix">
            <dt>优惠券名称</dt>
            <dd>
              <input type="text" id="couponName" class="inpTxt" />
            </dd>
          </dl>
          <dl class="clearfix">
            <dt>有效期</dt>
            <dd>
            	<input type="text" readonly date-format="yyyy-mm-dd" placeholder="开始日期，不填则即时生效"  id="beginTime" style="width:180px;" />
            	<span>至</span>
                <input type="text" readonly id="endTime" placeholder="结束日期，不填则永久有效" date-format="yyyy-mm-dd" style="width:180px;" />
            </dd>
          </dl>
          <dl class="clearfix">
            <dt>优惠券描述</dt>
            <dd>
              <textarea rows="6" cols="100" id="description" class="inpTxt" style="width: 680px; height: 300px;"></textarea>
            </dd>
          </dl>
          <dl class="clearfix">
            <dt>生成数量</dt>
            <dd>
              <input type="text" id="nums" class="inpTxt" />
            </dd>
          </dl>
          <dl class="clearfix">
            <dt>&nbsp;</dt>
            <dd>
              <input type="button" id="generateCoupon" value="确 定" class="inpBtn" />
            </dd>
          </dl>
        </div>
        <div id="tips"  class="hide" style="position: fixed;z-index: 9999;left: 35%;top: 35%;">
            <div class="popup">
                <div class="popup-t">操作完成</div>
                <div class="popup-m">
                    <div class="popup-mes">本次共生成优惠券0张</div>
                </div>
                <div class="popup-b">
                    <%--<a href="javascript:;" id="exportOut">导出</a>--%>
                    <a href="javascript:;" id="complet">完成</a>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- 提示弹层 -->
<div id="ui-mask" class="ui-mask"></div>
<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-orderPayId="<#=item.OrderPayId#>">
            <td><#=item.OrderNo#></td>
            <td><#=item.SerialPay#></td>
            <td><#=item.PayAmount+"元"#></td>
            <td><#=item.VipSourceName#></td>
        </tr>
    <#}#>
</script>
<script id="tpl_couponType" type="text/html">
    <span data-status=""></span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-value="<#=item.CouponTypeID#>"><#=item.OriginalCouponTypeName#></span>
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
        <span data-value="<#=item.OptionValue#>"><#=item.OptionText#></span>
    <#}#>
</script>

<script type="text/javascript" src="/Module/static/js/plugin/datepicker.js" ></script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/Coupon/js/main.js"></script>

</asp:Content>