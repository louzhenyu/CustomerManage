<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>订单管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/chainCloudOrder/css/style.css?v=0.3"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.4">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">订单编号：</em>
                                                      <label class="searchInput">
                                                          <input data-text="订单编号" data-flag="order_no" name="order_no" type="text" value="" placeholder="请输入">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">会员信息：</em>
                                                      <div class="searchInput">
                                                                <input  name="vip_no" data-text="会员" data-flag="vip_no" type="text" value=""  placeholder="请输入">
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">商户单号：</em>
                                                      <div class="searchInput">
                                                                <input  name="paymentcenter_id" data-text="会员" data-flag="vip_no" type="text" value=""  placeholder="请输入">
                                                      </div>
                                                  </div>
                                                 <div class="moreQueryWrap">
                                                     <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit" >支付方式：</em>
                                                      <div class="selectBox">
                                                               <select id="txtDataFromID" name="Field11"></select>
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">付款状态：</em>
                                                      <div class="selectBox">
                                                               <select id="payment" name="Field1" class="easyui-combobox" data-options="width:200" ></select>
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                  <em class="tit">订单渠道：</em>
                                                   <div class="selectBox">
                                                   <select id="txtDataFromIDs" class="easyui-combobox" name="data_from_id" data-options="width:200" ></select>
</div>
</div>

													<div class="unfoldMoreBtn">展开更多</div>
                                                    
                                                    <div class="unfoldBox">
                                                    <div class="commonSelectWrap">
                                                          <em class="tit">门店查询：</em>
                                                          <div class="selectBox">
                                                                   <select id="storeList" name="UnitId" class="easyui-combobox" data-options="width:200,height:30" ></select>
                                                          </div>
                                                    </div>
                                                  
                                                  	<div class="commonSelectWrap">
                                                          <em class="tit">会员卡等级：</em>
                                                          <div class="selectBox">
                                                                   <select id="vipCardGrade" name="VipCardTypeID" class="easyui-combobox" data-options="width:200" ></select>
                                                          </div>
                                                    </div>
                                                    
                                                    
                                                    <div class="commonSelectWrap">
                                                       <em class="tit">提货日期：</em>
                                                       <div class="searchInput bordernone whauto"  style="width: 202px; background: none;">
                                                           <div class="line">
                                                                 <input id="reserveDateBegin"  name="ReserveDateBegin" class="easyui-datebox"  data-options="width:100,height:32" /><span>至</span><input id="reserveDateEnd" name="ReserveDateEnd" class="easyui-datebox" data-options="width:100,height:32" validType="compareDate[$('#reserveDateBegin').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/>
                                                           </div>
                                                     	</div>
                                                     </div>
                                                    
                                                   <div class="commonSelectWrap">
                                                       <em class="tit">下单日期：</em>
                         <div class="searchInput bordernone whauto"  style="width: 202px; background: none;">
                           <div class="line">
                                 <input id="startDate"  name="order_date_begin" class="easyui-datebox"  data-options="width:100,height:32" /><span>至</span> <input id="expireDate" name="order_date_end" class="easyui-datebox" data-options="width:100,height:32" validType="compareDate[$('#startDate').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/></div>
                              </div>
                              </div>
                              					   
                                                   <div class="packupBtn">收起</div>
                              
                              
                              						</div>
                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="optionBtn">
                                     <div class="commonBtn w100 batch optionCheck" style="position: relative" data-show="0,100,500" data-flag="batch"   >批量操作
                                     <div class="panelTab" >
                                     <p class="optionCheck check01" data-optiontype="audit" data-show="0,100" >批量审核</p>
                                   <!--  <p class="optionCheck check02" data-optiontype="shipments" data-show="0,500">批量发货</p>-->
                                     <p class="optionCheck check03" data-optiontype="cancel" data-show="0,占位">批量取消</p>
                </div>
                                     </div>
                                    <div class="commonBtn sales icon w80 icon_export r" data-flag="export" >导出</div>
                               <div class="commonBtn sales" id="optBtnss" data-flag="add" style="display: none">配发货门店</div>
                                  </div>
                <div class="tableWrap" id="tableWrap">

                <div class="tableList" id="opt">
                   <ul><li class="on" data-Field7="0"><em > 全部 </em></li>
                        <li data-Field7="100"><em>未审核 </em></li>
                        <li data-Field7="900"><em>审核不通过</em></li>
                        <!--<li data-Field7="410"><em>备货中 </em></li>-->
                        <li data-Field7="500"><em>未发货</em></li>
                        <li data-Field7="510"><em>未提货 </em></li>
                        <li data-Field7="600"><em>已发货 </em></li>
                        <li data-Field7="610"><em>已提货 </em></li>
                        <li data-Field7="700"><em>已完成 </em></li>
                        <li data-Field7="800"><em>已取消 </em></li>
                         <li data-Field7="1234567890"><em>未选择门店</em></li>
                   </ul>

                </div>


                   <div class="dataTable" id="gridTable">
                                            <div  class="loading" style="padding-top: 50px;">
                                                     <span>
                                                   <img src="../static/images/loading.gif"></span>
                                              </div>

</div>  </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager" >
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
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>
      
      
      
      
      
      
      <!--  快速上手 -->
      <div style="display:none;">
            <div id="winQuickly" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
                    <div data-options="region:'center'" style="padding:10px;">
                        <div class="quicklyBox">
                            <div><img src="images/index-order.png" alt="" /></div>
                        </div>
                        <div class="quicklyBtnBox">
                            <a href="http://help.chainclouds.cn/?p=763" style="text-indent:55px;" target="_blank"><span>操作指引</span></a>
                            <a href="http://help.chainclouds.cn/?p=772" style="text-indent:15px;" target="_blank"><span>操作指引</span></a>
                            <a href="http://help.chainclouds.cn/?p=836" style="text-indent:-5px;" target="_blank"><span>操作指引</span></a>
                        </div>
                        <p class="nextNotShow"><span>下次不再显示</span></p>
                    </div>
                </div>
            </div>  
        </div>
      
       <!-- 取消订单-->
       <script id="tpl_OrderCancel" type="text/html">
            <form id="payOrder">
           <div class="commonSelectWrap">
                 <em class="tit">备注：</em>
                <div  class="searchInput">
                   <input type="text" name="Remark" />
               </div>
           </div>

           <p class="winfont">你确认取消此笔订单吗？</p>
           </form>
       </script>
        <!--收款-->
         <script id="tpl_OrderPayMent" type="text/html">
            <form id="payOrder">


            <div class="optionclass">
               <div class="commonSelectWrap">
                             <em class="tit">订单金额:</em>
                                <div class="borderNone" >
                                 <input id="Amount" class=" easyui-numberbox " name="Amount" readonly="readonly" style="border:none"/>
                               </div>
                </div>
                  <div  class="commonSelectWrap">
                                                              <em class="tit">电子优惠券抵扣:</em>
                                                              <div class="selectBox bodernone">
                                                                 <input id="coupon" class="easyui-combogrid" data-options="width:200,height:30,validType:'selectIndex'"  name="CouponID" />
                                                             </div>
                                        </div>
               <div class="commonSelectWrap">
                             <em class="tit">纸质优惠券抵扣:</em>
                             <div class="searchInput" >
                                    <input id="Deduction" class="easyui-numberbox" name="Deduction" value="" data-options="width:200,height:30,precision:0,groupSeparator:','" /><span style="float: right; margin-right: -24px;margin-top: -30px;font-size: 14px;">元</span>
                            </div>
                            </div>
               <div class="commonSelectWrap">
                             <em class="tit">实付金额：</em>
                             <input id="ActualAmount" class="searchInput bodernone" name="ActualAmount" readonly="readonly" />
                            </div>
                <div class="commonSelectWrap">
                                        <em class="tit">付款方式：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="pay"  class="easyui-combobox" data-options="width:200,height:32,required:true"  name="PayID" />
                                       </div>
                                       </div>
                <div class="commonSelectWrap" id="ValueCard">
                            <em class="tit">实体储值卡号：</em>
                                  <div class="searchInput bodernone">
                                      <input  class="easyui-validatebox" data-options="width:200,height:32,validType:['englishCheckSub','length[5,12]']"  name="ValueCard" />
                                  </div>
                            </div>
               </div>
                </form>
                </script>
        <!--头部名称-->
        <script id="tpl_setUnit" type="text/html">
          <form id="payOrder">

          <div class="commonSelectWrap">
                                        <em class="tit">门店：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="setUnit"  class="easyui-combotree" data-options="width:200,height:32,required:true"  name="unitID" />
                                       </div>
                                       </div>
           </form>
      </script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
