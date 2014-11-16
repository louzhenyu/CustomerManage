<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
        <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
    <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/makeNewCard">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	 <a href="javascript:void(0)" onclick='AddMid("AllBrowser.aspx")' >总览</a>
        <a href="javascript:;" class="on">制新卡</a>
         <a href="javascript:void(0)" onclick='AddMid("CardManage.aspx")'>卡管理<em></em></a>
    </div>
    
    <div class="contentArea_vipCard">
    	<div class="commonTitWrap">
        	<span>制新卡</span>
        </div>
        
        
        <div class="create-card">
          <dl class="clearfix">
            <dt>渠道</dt>
            <dd>
              <div class="commonSelBox"> <span data-status="" id="sourceText">渠道</span>
                <div class="selPulldown" id="source"> <span></span> <span></span> <span></span> </div>
              </div>
            </dd>
            <dd><!--<a href="#">管理</a>--></dd>
          </dl>
          <dl class="clearfix">
            <dt>充值金额</dt>
            <dd>
              <input type="text" id="intoMoney" class="inpTxt" />
            </dd>
          </dl>
          <dl class="clearfix">
            <dt>赠送金额</dt>
            <dd>
              <input type="text" id="otherMoney" class="inpTxt" />
            </dd>
          </dl>
          <!--
          <dl class="clearfix">
            <dt>消费券</dt>
            <dd>
              <div class="commonSelBox"> <span data-status="" id="statusText">请选择</span>
                <div class="selPulldown" id="status"> <span>AAA</span> <span>BBB</span> <span>CCC</span> </div>
              </div>
            </dd>
          </dl>-->
          <dl class="clearfix">
            <dt>制作数量</dt>
            <dd>
              <input type="text" id="nums" class="inpTxt" />
            </dd>
          </dl>
          <dl class="clearfix">
            <dt>&nbsp;</dt>
            <dd>
              <input type="button" id="makeCard" value="确 定" class="inpBtn" />
            </dd>
          </dl>
        </div>
        <div id="tips"  class="hide" style="position: fixed;z-index: 9999;left: 35%;top: 35%;">
            <div class="popup">
                <div class="popup-t">操作完成</div>
                <div class="popup-m">
                    <div class="popup-mes">本次共生成新卡0张</div>
                </div>
                <div class="popup-b"> <a href="javascript:;" id="exportOut">导出</a> <a href="javascript:;" id="complet">完成</a> </div>
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
<script id="tpl_source" type="text/html">
    <span data-status="">全部</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-status="<#=item.ChannelID#>"><#=item.ChannelTitle#></span>
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
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/Withdraw/js/main.js"></script>
</asp:Content>