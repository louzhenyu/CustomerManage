<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>社会化销售</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       .lineT .rowRline,.tipSecretText{ padding-left: 40px;}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/socializeConfig.js?ver=0.6">
             <div class="queryTermArea" id="simpleQuery" style="display:none; width: 100%;">
                  <div class="listBtn" data-show="nav01" data-hide="nav02"  data-socialsalestype="0"> 订单   </div>
                 <!-- <div class="listBtn" data-show="nav02" data-hide="nav01" data-socialsalestype="1"> 商品 <div class="on"></div></div>-->
            </div>
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                                    <form></form>
                                <form id="optionForm">
                                <div class="nav01 mainTop">
                                <div class="checkBox" data-flag="EnableVipSales"><em></em> <span>会员分销设置 </span></div>
                                 </div>
            <div class="mainPanl">



               <div class="lineT">

                   <div class="nav01" style="padding-left: 10px" data-flag="EnableVipSales">
                      <div class="rowRline">
                       <div class="linetext"> 商品分销折扣：<input type="text" name="VDistributionPricePer" value="" class="easyui-numberbox"  data-options="min:0,precision:2"> %  <em class="explain">（商品原价x商品分销折扣=商品分销售价）</em></div>
                     </div>
                      <div class="tipSecretText"> <em>秘籍</em>设置合理折扣，为会员创造更诱人的分享理由,0或不填代表无折扣</div>
                     <div class="rowRline">
                             <div class="linetext">销售提成比例：<input type="text" value="" name="VOrderCommissionPer" class="easyui-numberbox"  data-options="min:0,precision:2"> % <em class="explain">（订单金额x销售提成比例=销售提成）</em></div>
                     </div>
                     <div class="tipSecretText"> <em>秘籍</em>适当的分成，让会员成为我们的事业伙伴！0或不填代表无提成</div>
                     <div class="rowRline" style="margin-left: -40px">
                             <div class="linetext"> 邀请小伙伴获得积分：<input type="text" value="" name="InvitePartnersPoints" class="easyui-numberbox"  data-options="min:0,precision:0"> 积分 </div>
                     </div>
                       <div class="tipSecretText"> <em>秘籍</em>邀请小伙伴获得积分，激励会员拓展我们的粉丝群！</div>
                     </div>

               </div>
             </div>
 </form>


              <div class="commonBtn submit"> 确定</div>

            </div>

                   <div style="padding-top: 20px; text-align: center; padding-right:40px;">
                    <img src="images/huiyuanxd.png" width="1120" height="320">
           </div>

            </div>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
