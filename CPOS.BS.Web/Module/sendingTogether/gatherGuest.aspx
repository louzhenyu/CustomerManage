<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>奖励模板设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/gatherGuest.css?v=0.4" rel="stylesheet"type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/gatherGuest.js?ver=0.1" >
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
                    <!--引流-->
                    <div class="titleBar">
                    	<p id="drainageTag" class="checkBoxTag"><em></em><span>引流</span></p>
                    </div>
                    
                    <div style="padding-left:30px;">
                        <div class="panlDiv" id="simpleQuery" style="display: inline-block; width: 100%;">
                          <div class="listBtn" data-cooperatetype="TwoWay" data-hide="oneway" data-show="twoway" >互相集客(资源共享)<div class="on"></div></div>
                          <div class="listBtn" data-cooperatetype="OneWay"data-show="oneway" data-hide="twoway" >单向集客(赚取佣金)<div class="on"></div></div>
                        </div>
                        <form></form>
                        <form id="addOneWay" >
                        <input name="RetailRewardRuleID" type="text" style="display:none">
                           <div class="tableWrap oneway">
                            <div class="panlDiv">
                            <p  class="title">首次关注奖励</p>
                            <div class="commonSelectWrap">
                                   <em class="tit">销售员：</em>
                                    <label class="searchInput" >
                                         <input name="FirstAttention_RetailRewardRuleID" type="text" style="display:none">
                                         <input  data-text="销售员" class="easyui-numberbox" data-options="precision:2,required:true,min:0,width:160,height:32" value="0" data-flag="FirstAttention_SellUserReward" name="FirstAttention_SellUserReward" type="text" >
                                    </label>元
                            </div>
                            <div class="commonSelectWrap" >
                                <em class="tit">分销商：</em>
                                    <label class="searchInput" >
                                       <input  data-text="分销商" class="easyui-numberbox" data-options="precision:2,required:true,min:0,width:160,height:32" value="0" data-flag="FirstAttention_RetailTraderReward" name="FirstAttention_RetailTraderReward" type="text" >
                                    </label>元
                           </div>
    
                            </div><!--panlDivEnd-->
                            <div class="panlDiv">
                            <p  class="title">会员首笔消费奖励比例，按每笔成交总额</p>
                            <div class="commonSelectWrap">
                                <em class="tit">销售员：</em>
                                    <label class="searchInput" >
                                        <input name="FirstTrade_RetailRewardRuleID" type="text" style="display:none">
                                         <input  data-text="销售员" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="FirstTrade_SellUserReward" name="FirstTrade_SellUserReward" type="text" >
                                    </label>%
                            </div>
                            <div class="commonSelectWrap" >
                                <em class="tit">分销商：</em>
                                    <label class="searchInput" >
                                       <input  data-text="分销商" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="FirstTrade_RetailTraderReward" name="FirstTrade_RetailTraderReward" type="text" >
                                    </label>%
                           </div>
    
                            </div><!--panlDivEnd-->
                            <div class="panlDiv ">
                            <p  class="title lineH30">会员3个月内消费奖励比例，按每笔成交总额</p>
                            <p  class="title explain lineH30">(说明：设置“会员首笔消费奖励后”，此项设置不包括首笔的奖励规则；如不设置则此项设置包括首笔奖励规则)</p>
                            <div class="commonSelectWrap">
                                <em class="tit">销售员：</em>
                                    <label class="searchInput" >
                                               <input name="AttentThreeMonth_RetailRewardRuleID" type="text" style="display:none">
                                         <input  data-text="销售员" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="AttentThreeMonth_SellUserReward" name="AttentThreeMonth_SellUserReward" type="text" >
                                    </label>%
                            </div>
                            <div class="commonSelectWrap" >
                                <em class="tit">分销商：</em>
                                    <label class="searchInput" >
                                       <input  data-text="分销商" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="AttentThreeMonth_RetailTraderReward" name="AttentThreeMonth_RetailTraderReward" type="text" >
                                    </label>%
                           </div>
    
                            </div><!--panlDivEnd-->
    
                        </div>
                        </form>
                        <form id="addTwoWay" class="">
                           <div class="tableWrap twoway">
                            <div class="panlDiv">
                            <p  class="title">首次关注奖励</p>
                            <div class="commonSelectWrap">
                                <em class="tit">销售员：</em>
                                    <label class="searchInput" >
                                      <input name="FirstAttention_RetailRewardRuleID" type="text" style="display:none">
                                         <input  data-text="销售员" class="easyui-numberbox" data-options="precision:2,required:true,min:0,width:160,height:32" value="0" data-flag="FirstAttention_SellUserReward" name="FirstAttention_SellUserReward" type="text" >
                                    </label>元
                            </div>
                            <div class="commonSelectWrap" >
                                <em class="tit">分销商：</em>
                                    <label class="searchInput" >
                                       <input  data-text="分销商" class="easyui-numberbox" data-options="precision:2,required:true,min:0,width:160,height:32" value="0" data-flag="FirstAttention_RetailTraderReward" name="FirstAttention_RetailTraderReward" type="text" >
                                    </label>元
                           </div>
    
                            </div><!--panlDivEnd-->
                            <div class="panlDiv">
                            <p  class="title">会员首笔消费奖励比例，按每笔成交总额</p>
                            <div class="commonSelectWrap">
                                <em class="tit">销售员：</em>
                                    <label class="searchInput" >
                                    <input name="FirstTrade_RetailRewardRuleID" type="text" style="display:none">
                                         <input  data-text="销售员" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="FirstTrade_SellUserReward" name="FirstTrade_SellUserReward" type="text" >
                                    </label>%
                            </div>
                            <div class="commonSelectWrap" >
                                <em class="tit">分销商：</em>
                                    <label class="searchInput" >
                                       <input  data-text="优惠券面值" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="FirstTrade_RetailTraderReward" name="FirstTrade_RetailTraderReward" type="text" >
                                    </label>%
                           </div>
    
                            </div><!--panlDivEnd-->
                            <div class="panlDiv ">
                            <p  class="title lineH30">会员3个月内消费奖励比例，按每笔成交总额</p>
                            <p  class="title explain lineH30">(说明：设置“会员首笔消费奖励后”，此项设置不包括首笔的奖励规则；如不设置则此项设置包括首笔奖励规则)</p>
                            <div class="commonSelectWrap">
                                <em class="tit">销售员：</em>
                                    <label class="searchInput" >
                                    <input name="AttentThreeMonth_RetailRewardRuleID" type="text" style="display:none">
                                         <input  data-text="销售员" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="AttentThreeMonth_SellUserReward" name="AttentThreeMonth_SellUserReward" type="text" >
                                    </label>%
                            </div>
                            <div class="commonSelectWrap" >
                                <em class="tit">分销商：</em>
                                    <label class="searchInput" >
                                       <input  data-text="分销商" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="AttentThreeMonth_RetailTraderReward" name="AttentThreeMonth_RetailTraderReward" type="text" >
                                    </label>%
                           </div>
                           
    
                            </div><!--panlDivEnd-->
                        </div>
                          </form>
                      
                    </div>  
                      
                    <!--销售-->
                    <div class="titleBar">
                        <p id="marketTag" class="checkBoxTag"><em></em><span>销售</span></p>
                    </div>
                    <div class="salesWrapArea">
                        <div class="panlDiv" style="display:inline-block;width:100%;">
                        	<form id="addSalseWay" class="">
                                <div class="panlDiv ">
                                	<div style="display:inline-block;width:100%;">
                                        <div class="commonSelectWrap" style="position:relative;left:-61px;">
                                            <em class="tit">商品分销价比例：</em>
                                                <label class="searchInput" >
                                                     <input  data-text="商品分销价比例" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="" name="ItemSalesPriceRate" type="text" >
                                                </label>%
                                        </div>
                                    </div>
                                    <p class="explain" style="clear:left;">分销价=商品价格*百分比</p>
                                    <div class="commonSelectWrap">
                                        <em class="tit">销售员：</em>
                                            <label class="searchInput">
                                                 <input  data-text="销售员" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="" name="SellUserReward" type="text" >
                                            </label>%
                                    </div>
                                    <div class="commonSelectWrap" >
                                        <em class="tit">分销商：</em>
                                            <label class="searchInput" >
                                               <input  data-text="分销商" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="" name="RetailTraderReward" type="text" >
                                            </label>%
                                    </div>
                                    
                                </div>
                            </form>
                        </div>
                    </div>
                    
                    
                    <div class="panlDiv bordernone">  <div class="commonBtn submitBtn">保存</div> </div>

                </div>
    </div>
    <div style="display: none">
        <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="padding: 10px;">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                    text-align: center; padding: 5px 0 0;">
                    <a class="easyui-linkbutton commonBtn saveBtn">确定</a> <a class="easyui-linkbutton commonBtn cancelBtn"
                        href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                </div>
            </div>
        </div>
    </div>
    <!--收款-->
    <script id="tpl_AddUnitList" type="text/html">
            <form id="payOrder">


            <div class="optionclass">
               <div class="commonSelectWrap">
               <em class="tit" style="width: 30px;"></em>
                                <div class="bordernone searchInput"  style="width: 222px">
                                 <input  id="unitTree" class="easyui-combotree" data-options=" width:130,height:32,"/>
                               </div>
                </div>
               <div class="commonSelectWrap">
                                <div class=" searchInput" >
                                 <input  name="RetailTraderName" placeholder="分销商名称"/>
                               </div>
                </div>
                <div class="commonBtn searchBtn"> 搜索</div>
            </div>
            <div class="zsy"> </div>
            <div id="searchGrid" class="searchGrid"></div>
       </form>
    </script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/sendingTogether/js/main.js"%>"></script>
</asp:Content>
