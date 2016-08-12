<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>礼品卡设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/openGiftCard/css/activityDetail.css?v=0.6"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/activiDetail.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->

                <div class="queryTermArea" id="simpleQuery" style="display: inline-block;">
                <div class="panelList">
                    <div  class="title"  style="display: none;">
                           <ul id="optPanel">
                               <li data-flag="#nav01" class="on one">卡类型</li>
                               <li data-flag="#nav02" class="borderNone">特殊日期</li>
                           </ul>
                    </div>
                    <form></form>
                    <form id="nav0_1">
                   <div class="panelDiv" id="nav01" data-index="0">
                   	  <div class="line" style="border-top:none; display: none">
                      		<div class="commonSelectWrap vipTypeCard">
                                  <em class="tit">卡类型：</em>
                                  <div class="radio" data-name="r2" data-category=0><em></em>会员卡</div>
                                  <div class="radio" data-name="r2" data-category=1><em></em>储值卡</div>
                                  <div class="radio on" data-name="r2" data-category=2><em></em>消费卡</div>
                            </div>
                      </div>

                      <div class="line" style="border-top:none;">
                      <!--
                      <div class="commonSelectWrap">
                          <em class="tit">编码：</em>
                          <label class="searchInput" style="width: 303px;" >
                            <input data-text="编码" data-flag="VipCardTypeCode" class="easyui-validatebox" data-options="required:true,tipPosition:'top',validType:['englishCheckSub','maxLength[5]']" name="VipCardTypeCode"  type="text" value="">
                          </label>
                      </div>
                      -->


                      <div class="commonSelectWrap">
                          <em class="tit" style="width:92px">卡类型名称：</em>
                          <label class="searchInput" style="width:200px;" >
                            <input data-text="卡类型名称" data-flag="VipCardTypeName" class="easyui-validatebox" data-options="required:true,tipPosition:'top'" name="VipCardTypeName" type="text" value="">
                          </label>
                          <div class="checkBox" id="IsPassword" data-flag="IsPassword" style="margin:5px 0 0 20px;"><em></em> <span>启用密码 </span></div>
                      </div>

                      <div class="commonSelectWrap" id="vipCardLevelBox" style="clear:left; display: none;">
                          <em class="tit" style="width:92px">卡等级值：</em>
                          <label class="searchInput" style="width:200px;border:1px solid #fff;" >
                            <input data-text="卡等级值" class="easyui-combobox" id="VipCardLevel" name="VipCardLevel" data-options="tipPosition:'top'"  type="text" value="">
                          </label>
                          <span class="textF33" style="padding:8px 0 0 20px;float:left;">数值越大，等级越高</span>
                      </div>


                      <div class="commonSelectWrap numberBox-100" style="clear:left">
                          <em class="tit" style="width:92px">售卡金额：</em>
                          <label class="selectBox" style="width:100px;" >
                            <input data-text="售卡金额" data-flag="" class="easyui-numberbox" data-options="height:30,width:100,min:0,precision:2,max:10000,tipPosition:'top'" name="Prices" type="text" value="">
                          </label>
                          <span class="monadBox">元</span>
                      </div>


                      <div class="commonSelectWrap numberBox-100" style="margin-left:35px;">
                          <em class="tit" style="width:92px">积分换卡：</em>
                          <label class="selectBox" style="width:100px;" >
                            <input data-text="积分兑换" data-flag="ExchangeIntegral" class="easyui-numberbox" data-options="height:30,width:100,min:0,precision:0,max:10000,tipPosition:'top'" name="ExchangeIntegral" type="text" value="">
                          </label>
                          <span class="monadBox">积分</span>
                      </div>
                   <div class="commonSelectWrap numberBox-100">
                              <div class="checkBox" data-flag="isextramoney"><em></em> <span>可补差价 </span></div>
                              <a href="javascript:void(0)" class="tip easyui-tooltip" title="提示文字可补差价，原卡价格100
                                                                                             元" ></a>
                     </div>



                  </div>

                      <div class="line">
                      <div class="commonSelectWrap" style="height: 158px;">
                                    <em class="tit" style="width: 49px;"></em>
                                    <em class="tit" style="width: 49px;"></em>
                         <div class="handleLayer" id="editLayer">
                             <div class="jsAreaItem">

                              <div class="wrapPic">
                                 <span class="uploadBtn"> <div> 上传图片</div><input class="uploadImgBtn" type="file" /></span>
                                 <div class="imgPanl"><img src="images/deflaut.png" width="270" height="135"> </div>
                               </div>
                               <div  class="txt">图片尺寸580*290</div>
                          </div>
                         </div>
                      </div>
                     </div>

                       <div class="line" style="padding-bottom:20px; display: none">
                       	 <div class="linetext" id="cardDiscountBox">
                            <div class="checkBox"  data-name="r1" data-flag="CardDiscount"><em></em> <span>折扣 </span></div>
                            <div class="inputDiv">&nbsp;&nbsp;&nbsp;消费&nbsp;&nbsp;<input type="text" class="easyui-numberbox"  name="CardDiscount" data-options="min:0,precision:2,max:10,width:120,height:32"> 折 <span class="textF33">提示：98折应输入9.8</span>
                            </div>
                         </div>

                         <div class="linetext" id="paidGivePointsBox">
                            <div class="checkBox"  data-name="r1" data-flag="PaidGivePoints"><em></em> <span>积分 </span></div>
                            <div class="inputDiv">&nbsp;&nbsp;&nbsp;消费&nbsp;&nbsp;<input type="text" name="PaidGivePoints" class="easyui-numberbox" data-options="min:0,precision:2,max:10000,width:120,height:32"> 元 回馈1积分
                       		</div>
                        </div>

                        <div class="linetext" id="chargeGiveBox">
                            <div class="checkBox" data-flag="EnableRewardCash" data-name="r1"><em></em> <span>充值 </span></div>
                            <div class="inputDiv">充值满 <input type="text" class="easyui-numberbox" name="ChargeFull" data-options="min:0,precision:0,max:10000,width:120,height:32">&nbsp;送&nbsp;<input type="text"  name="ChargeGive"  class="easyui-numberbox" data-options="min:0,precision:0,max:1000,width:120,height:32"></div>
                        </div>


                        <div class="linetext" id="returnAmountPerBox">
                            <div class="checkBox" data-flag="ReturnAmountPer" data-name="r1"><em></em> <span>返现</span></div>
                            <div class="inputDiv"><input type="text" name="ReturnAmountPer" class="easyui-numberbox" data-options="min:0,precision:2,max:100,width:120,height:32"> % （获得返现的订单金额比例）
                       		</div>
                        </div>


                       </div>




                       <div class="line" id="autoUpdateBox" style="padding-bottom:20px;display: none;">
                             <div class="linetext" style="width:100%">
                                <div class="checkBox" data-flag="UpgradeAmount"><em></em> <span>累计消费金额满 </span></div>
                                <div class="inputDiv" style="margin-left:8px;"><input type="text" class="easyui-numberbox"  name="UpgradeAmount" data-options="min:0,precision:1,max:10000,width:120,height:32"> 自动升级
                                </div>
                             </div>

                             <div class="linetext" style="width:100%">
                                <div class="checkBox" data-flag="UpgradePoint"><em></em> <span>累计获得积分满 </span></div>
                                <div class="inputDiv" style="margin-left:8px;"><input type="text" class="easyui-numberbox"  name="UpgradePoint" data-options="min:0,precision:0,max:10000,width:120,height:32"> 自动升级
                                </div>
                             </div>


                             <div class="linetext" style="width:100%">
                                <div class="checkBox" data-flag="UpgradeOnceAmount"><em></em> <span>单次消费金额满 </span></div>
                                <div class="inputDiv" style="margin-left:8px;"><input type="text" class="easyui-numberbox"  name="UpgradeOnceAmount" data-options="min:0,precision:1,max:10000,width:120,height:32"> 自动升级
                                </div>
                             </div>

                             <div class="expText">
                                 <p>1.如允许会员卡自动升级，可以选择一个或多个升级条件；满足条件后，系统将按会员卡等级值升级；</p>
                                 <p>2.如不允许会员卡自动升级，则不要勾选任何升级条件；</p>
                                 <p>3.会员可以通过在门店或云店购买会员卡实现升级</p>
                             </div>


                        </div>
                   </div>
                   </form>
                   <!--商品详情-->
                   <div class="panelDiv cursorDef" id="nav02" data-index="1" style="display: none;">
                    <div class="optBtnPanel">
                         <div class="commonBtn icon w80 icon_add r" id="addDatetime">新增</div>
                    </div>
                   <div  id="gridTable" class="gridLoading">
                         <div  class="loading">
                                  <span>
                                <img src="../static/images/loading.gif"></span>
                           </div>
                   </div>


                                             <div id="pageContianer">
                                             <div class="dataMessage" >该卡类型没有特殊日期</div>

                                             </div>

                   </div>
                   </div>
                    <!--商品详情End-->

                     <!--销售信息End-->
                  <div class="zsy"></div>

                <div class="btnopt" data-falg="nav01">
                 <div class=" commonBtn bgWhite prevStepBtn l"   data-flag="#nav01">上一步</div>
                <div class=" commonBtn nextStepBtn r"  id="submitBtn" data-flag="#nav02">确定</div>
                 <!--<div class="commonBtn bgCcc"  data-flag="#nav02">取消</div>-->
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
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
       </div>
       <!-- 取消订单-->
       <script id="tpl_addDatetime" type="text/html">
            <form id="payOrder">
           <div class="commonSelectWrap">
                 <em class="tit">假日选择：</em>
                <div class="selectBox">
                   <input type="text" name="HolidayID" id="Holiday" class="easyui-combobox" data-options="width:160,height:32,required:true,validType:'selectIndex'"  />
               </div>
           </div>
            <div class="commonSelectWrap" style="width: 400px;height: 100px;">
                            <em class="tit">规则：</em>
                           <div class="checkBoxList">

                      <div class="listBtn show" data-flag="NoAvailablePoints" >不可用积分<div class="on"></div></div>
                      <div class="listBtn" data-flag="NoAvailableDiscount" >不可用折扣<div class="on"></div></div>
                       <div class="listBtn"  data-flag="NoRewardPoints" >不可用回馈积分<div class="on"></div></div>
                          </div>
                      </div>
					  <p class="ruleTipText">规则必须最少选择一项</P>

           </form>
       </script>

</asp:Content>
