<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>分销商提现管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/sendingTogether.css?v2.0"/>
    <style type="text/css">
      .datagrid-btable tr{height:45px;}
	  .datagrid-row td:first-of-type div{padding-left:0;}
	  td[field="RetailTraderID"] em{margin-right:15px;}
    </style>
</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<body cache>
<div class="allPage" id="section" data-js="js/distributor.js?ver=0.2">
    <!-- 内容区域 -->
    <div class="contentArea_vipquery">
        <!--个别信息查询-->
        <div class="queryTermArea" id="simpleQuery" style="display:inline-block;width: 100%; " >
             <form></form>
            <form id="queryFrom">
            <div class="item">
            <div class="commonSelectWrap">
                <em class="tit">商家名称：</em>
                <label class="searchInput"><input  name="RetailTraderName" type="text" value=""></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">商家地址：</em>
                <label class="searchInput"><input   name="RetailTraderAddress" type="text" value=""></label>
            </div>
            <div class="commonSelectWrap">
                            <em class="tit">联系人：</em>
                            <label class="searchInput"><input   name="RetailTraderMan" type="text" value=""></label>
                        </div>
                        <div class="moreQueryWrap">
                         <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                         </div>
           </div>
           <div class="item">
             <div class="commonSelectWrap">
                             <em class="tit">门店：</em>
                             <div class="selectBox bordernone">
                               <input id="unitTree" name="UnitID"  class="easyui-combotree" style="width:200px;height: 30px;"   />
                             </div>
                         </div>
            <div class="commonSelectWrap">
                            <em class="tit">活动类型：</em>
                            <div class="selectBox bordernone">
                              <input id="Way" class="easyui-combobox"  style="width:200px;height: 30px;"  name="CooperateType"  />


                            </div>
                        </div>
            <div class="commonSelectWrap">
                <em class="tit">状态：</em>
                <div class="selectBox bordernone">
                  <input id="cc" class="easyui-combobox"  style="width:200px;height: 30px;" name="Status"  />
                </div>
            </div>
            </div>
             </form>
            

        </div>

        <!--表格操作按钮-->
        <div class="tableWrap">
            <table class="dataTable"  id="dataTable">
                   <p class="loading" style="width:32px;height:32px;left:50%;margin-left:-16px;"><img src="../static/images/loading.gif"></p>
            </table>
            <div id="pageContianer">
             <div class="dataMessage" >没有符合条件的查询记录</div>
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>
    </div>
</div>
    <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onClick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>
      <iframe name="I1" style="display:none"></iframe>
      
      
<script type="text/html" id="tpl_setReward">
	<!--引流 title-->
	<div class="titleBar">
		<p id="drainageTag" class="checkBoxTag"><em></em><span>引流</span></p>
	</div>
					
	<div class="panlDiv">
		<div class="radio" data-cooperatetype="TwoWay"  data-name="r1" data-hide="oneway" data-show="twoway">
			<em></em>
			<span>互相集客(资源共享)</span>
		</div>
		<div class="radio" data-cooperatetype="OneWay"  data-name="r1"  data-hide="twoway" data-show="oneway">
			<em></em>
			<span>单向集客(赚取佣金)</span>
		</div>
	</div>
	<form id="addOneWay" >
	<input name="RetailRewardRuleID" type="text" style="display:none">
	   <div class="Wrap oneway"  style="display: none">
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
		<p  class="explain">设置“会员首次消费奖励后”此项设置不包括首次的奖励规则；如不设置则此项设置包括首次奖励规则</p>
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
	   <div class="Wrap twoway" style="display: none">
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
		<p  class="explain">设置“会员首次消费奖励后”此项设置不包括首次的奖励规则；如不设置则此项设置包括首次奖励规则</p>
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
	  
	  
	<!--销售-->
	<div class="titleBar">
		<p id="marketTag" class="checkBoxTag"><em></em><span>销售</span></p>
	</div>
	<div class="salesWrapArea">
		<div class="panlDiv" style="display:inline-block;width:100%;">
			<form id="addSalseWay" class="">
				<div class="">
					<div style="display:inline-block;width:100%;">
						<div class="commonSelectWrap" style="position:relative;left:-61px;">
							<em class="tit">商品分销价比例：</em>
								<label class="searchInput" >
									 <input  data-text="商品分销价比例" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="" name="ItemSalsePriceRate" type="text" >
								</label>%
						</div>
					</div>
					<p class="explain" style="clear:left;">分销价=商品价格*百分比</p>
					<div class="commonSelectWrap">
						<em class="tit">销售员:</em>
							<label class="searchInput">
								 <input  data-text="销售员" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="" name="SellUserReward" type="text" >
							</label>%
					</div>
					<div class="commonSelectWrap" >
						<em class="tit">分销商:</em>
							<label class="searchInput" >
							   <input  data-text="分销商" class="easyui-numberbox" data-options="precision:0,required:true,min:0,width:160,height:32" value="0" data-flag="" name="RetailTraderReward" type="text" >
							</label>%
					</div>
					
				</div>
			</form>
		</div>
	</div>  
    <p class="tipsBottomBox">引流与销售至少选中一个哦！</p>
</script>

<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async data-main="<%=StaticUrl+"/module/sendingTogether/js/main.js"%>" ></script>
    </body>
</asp:Content>