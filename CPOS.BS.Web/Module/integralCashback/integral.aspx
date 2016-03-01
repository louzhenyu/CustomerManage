<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>积分配置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/vipConfig.js?ver=0.3">
              <div class="queryTermArea" id="simpleQuery" style="display:none;width:100%;  background:#ebf1f4;">
                             <div class="listBtn" data-show="nav01" data-hide="nav02"  data-rewardstype="0"> 订单   </div>
                          <!--   <div class="listBtn" data-show="nav02" data-hide="nav01"  data-rewardstype="1"> 商品 <div class="on"></div></div>-->
                       </div>
                       <!-- 内容区域 -->
                       <div class="contentArea_vipquery">
                        <form></form>
                    <form id="optionForm">
                    <div class="nav01 mainTop">
                    <div class="checkBox" data-flag="EnableIntegral" data-name="r1" >
                                                         <em></em>
                                                         <span>启用积分（启用积分，会员才可以获得积分奖励）</span>
                                                     </div>
                     </div>
                       <div class="mainPanl" style="padding-top: 0; ">
                          <div class="lineT">
                             <div class="nav01" data-flag="EnableIntegral">

                                <div class="mainTitle">获取积分规则</div>
                                <div class="rowRline">会员消费回馈积分请在“会员卡-卡类型管理”中设置</div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">每单赠送积分上限：</em> <input type="text" name="PointsOrderUpLimit" value="" class="easyui-numberbox"  data-options="min:0,precision:0"> 积分  <em class="explain">（0或空代表不限制）</em></div>
                             </div>
                              <div class="mainTitle">使用积分规则</div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">积分抵扣比例：</em> <input type="text" value="" name="IntegralAmountPer" class="easyui-numberbox"  data-options="min:0,precision:0"> 积分抵扣1元 </div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt">账户积分累计满：</em> <input type="text" name="PointsRedeemLowestLimit" value="" class="easyui-numberbox"  data-options="min:0,precision:0"> 才可使用积分 <em class="explain">（积分数未达到此限制不允许抵扣，0或空代表不限制）</em></div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext"><em class="txt"> 每单可使用积分上限：</em> <input type="text" value="" name="PointsRedeemUpLimit" class="easyui-numberbox"  data-options="min:0,precision:0"> %  <em class="explain">（按订单实付金额比例设置）</em></div>
                             </div>
                             <div class="rowRline">
                                    <div class="linetext" style="float: left;"><em class="txt">积分有效期：</em> <input type="text" name="PointsValidPeriod" value="2" class="easyui-numberbox"  data-options="min:1,precision:0"> 年</div>  <em class="explain" style="color: #ff0000;float: left; margin: 6px;">例如：积分有效期为2年，则客户2018年度（2018/1/1 - 2018/12/31）<br/>
                                                                                                                                                                                                                                          累计的所有积分，次年年底（2019/12/31）积分到期</em>
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
