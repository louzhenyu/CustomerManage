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
                       <div class="checkBox" data-flag="EnableRewardCash" data-name="r1" ><em></em> <span>启用返现（启用返现，会员才可以获得返现奖励） </span></div>
                       </div>
                       <div class="mainPanl" style="padding-top: 0">



                          <div class="lineT">
                              <div class="nav01" data-flag="EnableRewardCash">

                                 <div class="mainTitle">获取返现规则</div>
                                 <div class="rowRline"> 会员消费获得返现请在“会员卡-卡类型管理”中设置 </div>
                                <div class="rowRline">
                                        <div class="linetext"><em class="txt">  每单赠送返现上限:：</em> <input type="text" name="CashOrderUpLimit" class="easyui-numberbox"  data-options="min:0,precision:2" value=""> 元  <em class="explain">（0或空代表不限制）</em></div>
                                 </div>
                                 <div class="mainTitle">使用返现规则</div>
                                <div class="rowRline">
                                        <div class="linetext"><em class="txt"> 返现最低使用限制：</em> <input type="text" name="CashRedeemLowestLimit" class="easyui-numberbox"  data-options="min:0,precision:2"  value=""> 元 <em class="explain">（返现数未达到此限制不允许抵扣，0或空代表不限制）</em></div>
                                 </div>
                                <div class="rowRline">
                                        <div class="linetext"> <em class="txt"> 每单可使用返现上限：</em>  <input type="text" value="" name="CashRedeemUpLimit" class="easyui-numberbox"  data-options="min:0,precision:0"> %  <em class="explain">（按订单实付金额比例设置）</em></div>
                                </div>
                                <div class="rowRline">
                                        <div class="linetext" style="float: left;"><em class="txt">返现有效期：</em> <input type="text" name="CashValidPeriod" value="2" class="easyui-numberbox"  data-options="min:1,precision:0"> 年 </div>  <em style="color: #ff0000;float: left; margin: 6px;">例如：返现有效期为2年，则客户2018年度（2018/1/1 - 2018/12/31）<br />
                                                                                                                                                                                                                       累计的所有返现，次年年底（2019/12/31）返现到期。</em>
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
