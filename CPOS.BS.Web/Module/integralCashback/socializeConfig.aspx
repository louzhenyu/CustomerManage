<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>社会化销售</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/integralCashback/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/socializeConfig.js?ver=0.6">
             <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                  <div class="listBtn" data-show="nav01" data-hide="nav02"  data-socialsalestype="0"> 订单   <div class="on"></div></div>
                 <!-- <div class="listBtn" data-show="nav02" data-hide="nav01" data-socialsalestype="1"> 商品 <div class="on"></div></div>-->
            </div>
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
            <div class="mainPanl">
        <form></form>
<form id="optionForm">
               <div class="lineT">
                  <div class="nav01">
                   <div class="checkBox" data-flag="EnableEmployeeSales"><em></em> <span>员工分销设置 </span></div>
                      <div class="rowRline">
                       <div class="linetext  pl1_8em"> 商品分销价比例：<input type="text" value="" name="EDistributionPricePer" class="easyui-numberbox"  data-options="min:0,precision:0"> %  <em class="explain">（分销价=商品价格*百分比）</em></div>
                     </div>
                     <div class="rowRline">
                             <div class="linetext"> 订单金额分成比例：<input type="text" value="" name="EOrderCommissionPer" class="easyui-numberbox"  data-options="min:0,precision:0"> % </div>
                     </div>
                     <div class="rowRline">
                                                  <div class="linetext"> 集客订单分成比例：<input type="text" value="" name="GetVipUserOrderPer" class="easyui-numberbox"  data-options="min:0,precision:2"> % </div>
                                          </div>
                 </div>
                  <div class="nav02">
                    <div class="rowRline">
                     <p class="tit">员工分销设置</p>
                     <p>按商品设置分成金额。</p>
                     <p>说明：请在商品管理中设置！</p>
                    </div>
                  </div>
               </div>

               <div class="lineT">
                   <div class="nav01">
                   <div class="checkBox" data-flag="EnableVipSales"><em></em> <span>会员分销设置 </span></div>
                      <div class="rowRline">
                       <div class="linetext  pl1_8em"> 商品分销价比例：<input type="text" name="VDistributionPricePer" value="" class="easyui-numberbox"  data-options="min:0,precision:0"> %  <em class="explain">（分销价=商品价格*百分比）</em></div>
                     </div>
                     <div class="rowRline">
                             <div class="linetext"> 订单金额分成比例：<input type="text" value="" name="VOrderCommissionPer" class="easyui-numberbox"  data-options="min:0,precision:0"> % </div>
                     </div>
                     
                     <div class="rowRline" style="padding-left:6px">
                             <div class="linetext"> 邀请小伙伴获得积分：<input type="text" value="" name="InvitePartnersPoints" class="easyui-numberbox"  data-options="min:0,precision:0"> 积分 </div>
                     </div>
                     
                     </div>
                   <div class="nav02">
                    <div class="rowRline">
                     <p class="tit">会员分销设置</p>
                     <p>按商品设置分成金额。</p>
                     <p>说明：请在商品管理中设置！</p>
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
