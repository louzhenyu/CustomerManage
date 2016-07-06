<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>员工小店</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/socializeConfig.js?ver=0.6">
             <div class="queryTermArea" id="simpleQuery" style="display: none; width: 100%;">
                  <div class="listBtn" data-show="nav01" data-hide="nav02"  data-socialsalestype="0"> 订单   </div>
                 <!-- <div class="listBtn" data-show="nav02" data-hide="nav01" data-socialsalestype="1"> 商品 <div class="on"></div></div>-->
            </div>
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                                    <form></form>
                                <form id="optionForm">
                                <div class="nav01 mainTop">
                                <div class="checkBox" data-flag="EnableEmployeeSales"><em></em> <span>员工分销设置 </span></div>
                                 </div>
            <div class="mainPanl">
               <div class="lineT">
                  <div class="nav01" data-flag="EnableEmployeeSales">
                      <div class="rowRline">
                       <div class="linetext"> 商品分销折扣：<input type="text" value="" name="EDistributionPricePer" class="easyui-numberbox"  data-options="min:0,precision:2"> %  <em class="explain">（商品原价x商品分销折扣=商品分销售价）</em></div>
                     </div>
                     <div class="tipSecretText"> <em>秘籍</em>设置合理折扣，帮助员工为会员提供更多优惠！0或不填代表无折扣</div>
                     <div class="rowRline">
                             <div class="linetext"> 销售提成比例：<input type="text" value="" name="EOrderCommissionPer" class="easyui-numberbox"  data-options="min:0,precision:2"> % <em class="explain">（订单金额x销售提成比例=销售提成）</em></div>
                     </div>
                        <div class="tipSecretText"> <em>秘籍</em>适当的提成，激发员工销售热情！0或不填代表无提成</div>
                 </div>

               </div>

      </div>
 </form>


              <div class="commonBtn submit"> 确定</div>

            </div>

                <div style="padding-top: 20px; text-align: center; padding-right:40px;">
                                 <img src="images/yuangonxd.png" width="1120" height="320">
                        </div>

            </div>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
