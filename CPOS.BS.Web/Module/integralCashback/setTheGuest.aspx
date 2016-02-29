<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>集客奖励</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/socializeConfig.js?ver=0.6">
             <div class="queryTermArea" id="simpleQuery" style="display:none; width: 100%;">
                  <div class="listBtn" data-show="nav01" data-hide="nav02"  data-socialsalestype="0"> 订单   </div>
                 <!-- <div class="listBtn" data-show="nav02" data-hide="nav01" data-socialsalestype="1"> 商品 <div class="on"></div></div>-->
            </div>
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
            <div class="mainPanl">
        <form></form>
<form id="optionForm">
               <div class="lineT">
                  <div class="nav01">
                     <div class="rowRline">
                       <div class="linetext"> 订单分成比例：<input type="text" value="" name="GetVipUserOrderPer" class="easyui-numberbox"  data-options="min:0,precision:2"/> % <em class="explain">（订单金额x订单分成比例=集客提成金额）</em></div>
                    </div>
                     <div class="tipSecretText"> <em>秘籍</em>激励员工积极拓展会员，分享会员的消费贡献，0或不填代表无分成</div>
                 </div>

               </div>



 </form>


              <div class="commonBtn submit"> 确定</div>

            </div>



            </div>
        <div style="padding-top: 20px; text-align: center; padding-right:40px;">
         <img src="images/yuangonjl.png" width="1120" height="320">
</div>
        </div>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
