<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员积分返现配置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/integralCashback/css/style.css?v=0.7"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #contentArea{background:#f4f8fa;}
	.checkBox{padding-bottom:30px;}
	.checkBox span{font-size:15px;color:#666;}
	.lienT .rowRline{padding:0 0 30px 0;}
	.lineT .rowRline .linetext{height:32px;line-height:32px;font-size:15px;font-family:"黑体";color:#999;}
	.tipTextBox{line-height:20px;padding:8px 0 0 169px;font-size:15px;color:#f33;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/vipConfig.js?ver=0.3">
              <div class="queryTermArea" id="simpleQuery" style="display:inline-block;width:100%;  background:#ebf1f4;">
                             <div class="listBtn" data-show="nav01" data-hide="nav02"  data-rewardstype="0"> 订单   </div>
                          <!--   <div class="listBtn" data-show="nav02" data-hide="nav01"  data-rewardstype="1"> 商品 <div class="on"></div></div>-->
                       </div>
                       <!-- 内容区域 -->
                       <div class="contentArea_vipquery">
                       <div class="mainPanl">
                   <form></form>
           <form id="optionForm">
                          <div class="lineT">
                             <div class="nav01"  data-flag="EnableIntegral">
                              <div class="checkBox" data-flag="EnableIntegral" data-name="r1" style="width: 100px;margin-bottom: 40px;">
                                  <em></em>
                                  <span>启用积分</span>
                              </div>
                             
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">每单赠送积分上限：</em> <input type="text" name="PointsOrderUpLimit" value="" class="easyui-numberbox"  data-options="min:0,precision:0"> 积分  <em class="explain">（0或空代表不限制）</em></div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">积分抵扣比例：</em> <input type="text" value="" name="RewardPointsPer" class="easyui-numberbox"  data-options="min:0,precision:0"> 积分抵扣1元 </div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">账户积分累计满：</em> <input type="text" name="PointsRedeemLowestLimit" value="" class="easyui-numberbox"  data-options="min:0,precision:0"> 才可使用积分 <em class="explain">（积分数未达到此限制不允许抵扣，0或空代表不限制）</em></div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt"> 每单可使用积分上限：</em> <input type="text" value="" name="PointsRedeemUpLimit" class="easyui-numberbox"  data-options="min:0,precision:0"> %  <em class="explain">（按订单实付金额比例设置）</em></div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">积分有效期：</em> <input type="text" name="PointsValidPeriod" value="2" class="easyui-numberbox"  data-options="min:1,precision:0"> 年 </div>
                             </div>
                             <div class="tipTextBox">
                             		例如：积分有效期为2年，则客户2013年度（2013/1/1 - 2013/12/31）<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;累计的所有积分，次年年底（2014/12/31）积分到期。
                             </div>
                             
                            
                            </div>
                             <div class="nav02">
                               <div class="rowRline">
                                <p class="tit">积分</p>
                                <p>商品获得和使用返现，请在商品中涉及。</p>
                               </div>
                             </div>
                          </div>

                          <div class="lineT">
                              <div class="nav01" data-flag="EnableRewardCash">
                              <div class="checkBox" data-flag="EnableRewardCash" data-name="r1"  style="height:50px;"><em></em> <span>启用返现 </span></div>
								<!--
                                <div class="rowRline">
                                        <div class="linetext"><em class="txt">  获得返现按金额比例计算：</em>  <input type="text" class="easyui-numberbox"  data-options="min:0,precision:0" value="" name="RewardCashPer"> % </div>
                                </div>
                                -->
                                <div class="rowRline">
                                        <div class="linetext"><em class="txt">  每单赠送返现上限:：</em> <input type="text" name="CashOrderUpLimit" class="easyui-numberbox"  data-options="min:0,precision:2" value=""> 元  <em class="explain">（0或空代表不限制）</em></div>
                                 </div>
                                <div class="rowRline">
                                        <div class="linetext"><em class="txt"> 返现最低使用限制：</em> <input type="text" name="CashRedeemLowestLimit" class="easyui-numberbox"  data-options="min:0,precision:2"  value=""> 元 <em class="explain">（返现数未达到此限制不允许抵扣，0或空代表不限制）</em></div>
                                 </div>
                                <div class="rowRline">
                                        <div class="linetext"> <em class="txt"> 每单可使用返现上限：</em>  <input type="text" value="" name="CashRedeemUpLimit" class="easyui-numberbox"  data-options="min:0,precision:0"> %  <em class="explain">（按订单实付金额比例设置）</em></div>
                                </div>
                                <div class="rowRline">
                                        <div class="linetext"><em class="txt">返现有效期:</em> <input type="text" name="CashValidPeriod" value="2" class="easyui-numberbox"  data-options="min:1,precision:0"> 年 </div>
                                </div>
                                <div class="tipTextBox">
                             		例如：返现有效期为2年，则客户2013年度（2013/1/1 - 2013/12/31）<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;累计的所有返现，次年年底（2014/12/31）返现到期。
                             </div>
                                </div>
                              <div class="nav02">
                               <div class="rowRline">
                                <p class="tit">返现</p>
                                <p>商品获得和使用返现，请在商品中涉及。</p>
                               </div>
                               </div>
                          </div>

            </form>


                         <div class="commonBtn submit"> 提交</div>

                       </div>



                       </div>
        </div>
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
