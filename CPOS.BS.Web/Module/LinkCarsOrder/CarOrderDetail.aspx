<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="<%=StaticUrl+"/module/static/css/easyui.css"%>" rel="stylesheet" type="text/css" />
        <link href="css/reset-pc.css" rel="stylesheet" type="text/css" />
        <link href="css/orderDetail.css?v=0.4" rel="stylesheet" type="text/css" />
        <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
          <style media="print">
                     .Noprint,.commonHeader,.subMenu, .subMenu,.commonMenu
                     {
                         display: none;
                     }
                     .PageNext
                     {
                         page-break-after: always;
                     }
                     .print{
                           display: block;
                     }
                 </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <body></body>--%>
<div class="allPage" id="section" data-js="js/CarOrderDetail">
    <!-- 内容区域 -->
    <div class="contentArea" style="margin-left:10px;">

        
        <!--会员详情菜单-->
        <div class="subMenu vipDetailInfo" data-panl="orderinfo" >
            <ul class="clearfix">
                <li data-id="nav01"  class="nav01 on">订单详情</li>
                <li data-id="nav02" class="nav02">结算清单</li>
                <li data-id="nav03" class="nav03">车况报告</li>
                    <!--<li data-id="nav05" class="nav05">实体储值卡</li>
             <li data-id="nav04" class="nav04">帐内余额</li>
                <li data-id="nav06" class="nav06">上线与下线</li>
                <li data-id="nav07" class="nav07">客服记录</li>
                <li data-id="nav08" class="nav08">变更记录</li>-->
            </ul>
            	<div class="item">
                                <div class="itemBox">
                                    <em class="tit">订单号：</em>
                                    <p class="itemText" data-flag="OrderNo">--</p>
                                </div>
                                <div class="itemBox">
                                    <em class="tit">下单时间：</em>
                                    <p class="itemText" id="CreateTime" data-flag="CreateTime">--</p>
                                </div>

                            </div>
        </div>
        <div id="nav08" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div>
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>时间</th>
                        <th>操作人</th>
                        <th>操作事项</th>
                    </tr>
                </thead>
                <tbody id="tblLogs">
                    <tr>
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="4" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
                    </tr>
                </tbody>
            </table>
            <script id="tpl_logs" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.logid#>"><em></em></td>
                        <td><#=item.createtime#></td>
                        <td><#=item.cu_name#></td>
                        <td><#=item.action#></td>
                    </tr>
                <#}#>
            </script>
        </div>
        <!--上线与下线-->
        <div id="nav06" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div>
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>会员编号</th>
                        <th>微信昵称</th>
                        <th>姓名</th>
                        <th>等级</th>
                        <th>积分</th>
                        <th>下线数</th>
                        <th>入会时间</th>
                    </tr>
                </thead>
                <tbody id="tblOnline">
                    <tr>
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="7" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
                    </tr>
                </tbody>
            </table>
            <script id="tpl_online" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipId#>"><em></em></td>
                        <td><#=item.VipCode#></td>
                        <td><#=item.VipName#></td>
                        <td><#=item.VipRealName#></td>
                        <td><#=item.VipCardGradeName#></td>
                        <td><#=item.EndIntegral#></td>
                        <td><#=item.OfflineCount#></td>
                        <td><#=item.CreateTime#></td>
                    </tr>
                <#}#>
            </script>
        </div>

          <div id="nav01">
             <div class="panltitle">
                <div class="title">服务信息</div>
                <div id="paymentyBtn"><a class="fontC" data-flag="Status">已派工</a></div>
             </div>
             <div class="vipDetailInfo" data-panl="orderinfo">

                   <div class="item">
                        <div class="itemBox">
                         <em class="tit">项目名称：</em>
                         <p class="itemText" data-flag="ServiceItemsName">--</p>
                        </div>
                        <div class="itemBox">
                         <em class="tit">服务方式：</em>
                         <p class="itemText" data-flag="ServiceMode">--</p>
                        </div>
                        <div class="itemBox">
                         <em class="tit">服务价格：</em>
                         <p class="itemText" data-flag="Amount"  data-prefix="￥">--</p>
                        </div>
                   </div>
                   <div class="item">
                        <div class="itemBox">
                         <em class="tit">洗车时间：</em>
                         <p class="itemText" data-flag="ServiceTime">--</p>
                        </div>
                   </div>
             </div>
              <div class="panltitle">
                                                             <div class="title">支付信息</div>
                                                             <div id="paymentBtn"><a  class="commonBtn">收款</a></div>
                                                          </div>
              <div class="vipDetailInfo"  data-panl="orderinfo">

                                                                <div class="item">
                                                                     <div class="itemBox">
                                                                      <em class="tit">订单金额：</em>
                                                                      <p class="itemText" data-flag="Amount" data-prefix="￥" >--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">优惠券抵扣：</em>
                                                                      <p class="itemText"  data-flag="DeductionAmount"  data-prefix="￥" >--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">支付金额：</em>
                                                                      <p class="itemText" data-flag="ActualAmount"  data-prefix="￥">--</p>
                                                                     </div>
                                                                </div>
                                                                <div class="item">
                                                                     <div class="itemBox">
                                                                      <em class="tit">支付方式：</em>
                                                                      <p class="itemText" data-flag="PaymentTypeName">--</p>
                                                                     </div>
                                                                </div>
                                                          </div>
              <div class="panltitle">
                                                             <div class="title">会员及车信息</div>

                                                          </div>
              <div class="vipDetailInfo" data-panl="orderinfo">

                                                                <div class="item">
                                                                     <div class="itemBox">
                                                                      <em class="tit">微信昵称：</em>
                                                                      <p class="itemText" data-flag="VipName">--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">姓名：</em>
                                                                      <p class="itemText" data-flag="VipRealName">--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">手机号码：</em>
                                                                      <p class="itemText" data-flag="Phone">--</p>
                                                                     </div>
                                                                </div>
                                                                <div class="item">
                                                                     <div class="itemBox">
                                                                      <em class="tit">车牌号：</em>
                                                                      <p class="itemText" data-flag="CarNumber">--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">车型：</em>
                                                                      <p class="itemText" data-flag="CarInfo">--</p>
                                                                     </div>
                                                                </div>
                                                          </div>
              <div class="panltitle">
                 <div class="title">评价信息</div>

               </div>
              <div class="vipDetailInfo" data-panl="vipinfo">

                                                                <div class="item">
                                                                     <div class="itemBox">
                                                                      <em class="tit">总体评价：</em>
                                                                      <p class="itemText" data-flag="StarLevel" data-unit="分" >--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">服务态度：</em>
                                                                      <p class="itemText" data-unit="星"data-flag="StarLevel3"  >--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">干净指数：</em>
                                                                      <p class="itemText" data-unit="星" data-flag="StarLevel1"  >--</p>
                                                                     </div>
                                                                     <div class="itemBox">
                                                                      <em class="tit">技师能力：</em>
                                                                      <p class="itemText" data-unit="星"  data-flag="StarLevel2">--</p>
                                                                     </div>
                                                                </div>
                                                                <div class="item">
                                                                     <div class="itemBox">
                                                                      <em class="tit">评论内容：</em>
                                                                      <p class="itemText" data-flag="Content" >--</p>
                                                                     </div>
                                                                </div>
                                                          </div>
              <div class="panltitle">
                     <div class="title">服务跟踪</div>

              </div>
              <div class="tableWrap" id="tableWrap">
                                  <table class="dataTable" id="gridTable">

                                  </table>
                                  <div id="pageContianer">
                                      <div id="kkpager" style="text-align: center;">
                                      </div>
                                  </div>
                              </div>
          </div>
        <!--积分明细-->
        <div id="nav03" style="display:none;">
<div class="panltitle">
                 <div class="title">检测报告</div>

               </div>
<div class="vipDetailInfo" data-panl="carinfo">
                  <div class="item">
                        <div class="itemBox">
                                                                      <em class="tit">车型信息：</em>
                                                                      <p class="itemText" data-flag="CarInfo" >--</p>
                                                                     </div>
                         <div class="itemBox">
                                                                      <em class="tit">车牌号：</em>
                                                                      <p class="itemText"   data-flag="CarNumber">--</p>
                                                                     </div>
                         <div class="itemBox">
                         <em class="tit">是否二手车：</em>
                         <p class="itemText" data-flag="IsUsedCar" >--</p>
                         </div>
                         <div class="itemBox">
                               <em class="tit">上路时间：</em>
                               <p class="itemText" data-flag="RoadDate" >--</p>
                               </div>
                  </div>
                  <div class="item">
                         <div class="itemBox" >
                               <em class="tit">健康指数：</em>
                                <p class="itemText" data-flag="HealthIndex" >--</p>
                           </div>
                       <div class="itemBox">
                         <em class="tit">检测项目名称：</em>
                         <p class="itemText" data-flag="DetectItem"  >--</p>
                        </div>
                       <div class="itemBox">
                          <em class="tit">检测时间：</em>
                          <p class="itemText" data-flag="DetectTime"  >--</p>
                       </div>
                         <div class="itemBox">
                         <em class="tit">下次保养建议时间：</em>
                         <p class="itemText" data-flag="NextSuggestDate" >--</p>
                         </div>




                  </div>


                  <div class="item">
                          <div class="itemBox">
                                                  <em class="tit">技师建议：</em>
                                                  <p class="itemText" data-flag="Remark" >--</p>
                                                  </div>

                 </div>


               </div>
 <div class="panltitle">
                  <div class="title">检测项目</div>

                </div>
 <div class="vipDetailInfo" data-panl="carinfo">
                   <div class="item">
                      <div class="itemBox">
                                                                       <em class="tit">机油：</em>
                                                                       <span class="itemText" data-flag="EngineOil" >--</span>
                                                                       <span class="itemText" data-flag="EngineOilExt"  >--</span>
                                                                      </div>

                      <div class="itemBox">
                                                                       <em class="tit">助力转向油：</em>
                                                                       <span class="itemText" data-flag="SteeringHose"  >--</span>
                                                                       <span class="itemText"   data-flag="SteeringHoseExt">--</span>
                                                                      </div>

                          <div class="itemBox">
                                <em class="tit">变速油箱：</em>
                                 <span class="itemText" data-flag="TransmissionOil" >--</span>
                                 <span class="itemText" data-flag="TransmissionOilExt" >--</span>
                          </div>

                          <div class="itemBox">
                          <em class="tit">制动液：</em>
                          <span class="itemText" data-flag="BrakeFluid" >--</span>
                           <span class="itemText" data-flag="BrakeFluidExt" >--</span>
                          </div>

                  </div>
                   <div class="item">
                        <div class="itemBox">
                                <em class="tit">防冻液：</em>
                                <span class="itemText" data-flag="Antifreeze" >--</span>
                                <span class="itemText" data-flag="AntifreezeExt" >--</span>
                         </div>
                         <div class="itemBox">
                                <em class="tit">玻璃水：</em>
                                <span class="itemText" data-flag="GlassyWater" >--</span>
                                <span class="itemText" data-flag="GlassyWaterExt" >--</span>
                         </div>

                  </div>
                   <div class="item"></div>

                    <div class="item">

                                               <div class="itemBox">
                                                  <em class="tit">空气滤芯：</em>
                                                  <p class="itemText" data-flag="AirCleaner" >--</p>
                                               </div>
                                               <div class="itemBox">
                                                  <em class="tit">空气滤芯备注：</em>
                                                  <p class="itemText" data-flag="AirCleanerDesc" >--</p>
                                               </div>

                                                                   <div class="itemBox">
                                                                      <em class="tit">空调滤芯：</em>
                                                                      <p class="itemText" data-flag="AirFilter" >--</p>
                                                                   </div>
                                                                     <div class="itemBox">
                                                                        <em class="tit">空调滤芯备注：</em>
                                                                        <p class="itemText" data-flag="AirFilterDesc" >--</p>
                                                                     </div>

                                                          </div>

                    <div class="item">
                      <div class="itemBox">
                                                                      <em class="tit">车辆外观：</em>
                                                                      <p class="itemText" data-flag="CarFacade" >--</p>
                                                                   </div>
                      <div class="itemBox">
                                                                        <em class="tit">车辆外观备注：</em>
                                                                        <p class="itemText" data-flag="CarFacadeDesc" >--</p>
                                                                     </div>


                        <div class="itemBox">
                                                                          <em class="tit">车辆内部清洁：</em>
                                                                          <p class="itemText" data-flag="CarInside" >--</p>
                                                                       </div>
                       <div class="itemBox">
                                                                         <em class="tit">车辆内部清洁备注：</em>
                                                                         <p class="itemText" data-flag="CarInsideDesc" >--</p>
                                                                      </div>

                    </div>
                    <div class="item"></div>

                    <div class="item">
                       <div class="itemBox">
                                                                        <em class="tit">雨刮片：</em>
                                                                        <p class="itemText" data-flag="WiperBlade" >--</p>
                        </div>
                       <div class="itemBox">
                                                                        <em class="tit">雨刮片备注：</em>
                                                                        <p class="itemText" data-flag="WiperBladeDesc" >--</p>
                        </div>


                      <div class="itemBox">
                            <em class="tit">车灯：</em>
                          <p class="itemText" data-flag="CarLight" >--</p>
                        </div>
                      <div class="itemBox">
                                                                        <em class="tit">车灯备注：</em>
                                                                        <p class="itemText" data-flag="CarLightDesc" >--</p>
                                                                     </div>
                      </div>

                      <div class="item">
                      <div class="itemBox">
                                                                       <em class="tit">备用轮胎：</em>
                                                                       <p class="itemText" data-flag="SpareTire" >--</p>
                                                                    </div>
                      <div class="itemBox">
                                                                       <em class="tit">备用轮胎备注：</em>
                                                                       <p class="itemText" data-flag="SpareTireDesc" >--</p>
                                                                    </div>

                       <div class="itemBox">
                        <em class="tit">轮胎：</em>
                         <p class="itemText" data-flag="AirCleaner" >--</p>
                       </div>
                         <div class="itemBox">
                          <em class="tit">轮胎备注：</em>
                           <p class="itemText" data-flag="TireDesc" >--</p>
                         </div>
                     </div>
                      <div class="item">

                        <div class="itemBox">
                         <em class="tit">刹车片：</em>
                          <p class="itemText" data-flag="BrakePad" >--</p>
                        </div>
                        <div class="itemBox">
                         <em class="tit">刹车片备注：</em>
                          <p class="itemText" data-flag="BrakePadDesc" >--</p>
                        </div>

                        <div class="itemBox">
                         <em class="tit">电瓶：</em>
                          <p class="itemText" data-flag="Battery" >--</p>
                        </div>
                          <div class="itemBox">
                           <em class="tit">电瓶备注：</em>
                            <p class="itemText" data-flag="BatteryDesc" >--</p>
                          </div>
                      </div>
                    <div class="item"></div>

                    <div class="item">

                        <div class="itemBox">
                         <em class="tit">发动机机舱清洁：</em>
                          <p class="itemText" data-flag="EngineClean" >--</p>
                        </div>
                        <div class="itemBox">
                         <em class="tit">发动机机舱清洁备注：</em>
                          <p class="itemText" data-flag="EngineCleanDesc" >--</p>
                        </div>

                       <div class="itemBox">
                        <em class="tit">轮胎上光：</em>
                         <p class="itemText" data-flag="TireGlazing" >--</p>
                       </div>
                         <div class="itemBox">
                          <em class="tit">轮胎上光备注：</em>
                           <p class="itemText" data-flag="TireGlazingDesc" >--</p>
                         </div>


                    </div>
                   <div class="item">

                    <div class="itemBox">
                                            <em class="tit">轮胎胎压：</em>
                                             <span class="itemText" data-flag="TirePressure" >--</span>
                                              <span class="itemText" data-flag="TirePressureExt" >--</span>
                                           </div>
                   </div>
 </div>
        </div>
        <!--帐内余额-->
        <div id="nav04" style="display:none;">

            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>时间</th>
                        <th>余额</th>
                        <th>变更类型</th>
                        <th>备注</th>
                    </tr>
                </thead>
                <tbody id="tblAmount">
                    <tr >
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
                    </tr>
                </tbody>
            </table>
            <script id="tpl_amount" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipAmountId#>"><em></em></td>
                        <td><#=item.Date#></td>
                        <td><#=item.Amount#>元</td>
                        <td><#=item.VipAmountSource#></td>
                        <td><#=item.Remark#></td>
                    </tr>
                <#}#>
            </script>
        </div>
        <!--消费卡-->
        <div id="nav05" style="display:none;">
        <div class="panlDiv">
              <div class="panl" >



              <div class="commonSelectWrap">
                                       <em class="tit">实体储值卡卡号：</em>
                                       <div class="searchInput borderNone" >
                                              <input class="easyui-numberbox" value="" data-options="width:220,height:32,validType:'maxLength[18]'">
                                      </div>
                                      </div>
              <div class="commonSelectWrap">
              <em class="tit"></em>
                 <div class="searchInput borderNone" >
                     <a href="javascript:;" class="commonBtn saveBtn">保存</a>
                 </div>
              </div>

               </div>
        </div>
        </div>

        <div id="nav02" style="display:none;">
 <div id="btnDiv" style="text-align: left;position: relative;z-index: 999; margin-top: 20px; margin-left: 10px" class="Noprint">
              <input type="button" value="打印" id="printBtn" class="commonBtn"  style="width: 80px;float: right" />


          </div>
  <div id="printWord" class="wordDIv print">
    <h1 id="UnitName">兰博士深度养护连锁(凯马店)结算清单</h1>
    <form></form>
    <form id="word">
  <!--  rowspan合并行，colspan 合并列-->
  <div class="panl">
      <div class="title">

      </div>
        <table>
          <tr >

                           <td   class="txtL" colspan="4"  style="border: none">订单号：<input class="bodernone" name="OrderNo" value=""/></td>
                           <td class="txtL" colspan="4"  style="border: none"> 结算日期:<input class="bodernone" name="StatementDate" style="width: auto" value=""/></td>

                           <td colspan="4"   class="txtL" style="border: none">打印日期：<input class="bodernone" name="NewDate" value=""/></td>
                </tr>
        </table>
       <table>

            <tr>

            <td colspan="3">车主姓名</td>
            <td colspan="3" ><input class="bodernone" name="OwnerName" value="鼎鼎大名"/></td>
            <td colspan="3">联系电话</td>
            <td colspan="3" ><input class="bodernone" name="Phone" value="18516098067"/></td>

            </tr>
            <tr>

            <td  colspan="3">车牌号码</td>
            <td colspan="3"><input class="bodernone" name="CarNumber" value="苏B82536"/></td>
            <td  colspan="3">服务类别</td>
            <td  colspan="3"><input class="bodernone" name="ServiceItemsName" value="洗车"/></td>


            </tr>
             <tr>
            <td  colspan="3">服务价格</td>
            <td colspan="3"><input  class="easyui-numberbox bodernone" name="Amount" data-options="min:0,precision:2,prefix:'￥',height:42,width:231" /></td>
            <td  colspan="3">优惠方式</td>
            <td  colspan="3"><input   class="bodernone" name="CouponMode" value=""/></td>
            </tr>
             <tr>

            <td  colspan="3">实收金额</td>
            <td  colspan="3"><input class="easyui-numberbox bodernone"  name="ActualAmount" data-options="min:0,precision:2,prefix:'￥',height:42,width:231" /></td>
            <td  colspan="3">支付方式</td>
            <td  colspan="3"><input class="bodernone" name="PaymentTypeName" value="在线支付"/></td>
             </tr>

             <tr><td colspan="9" style="height:100px;border:0px"></td>
             <td class="txtR" style="border:0px;line-height: 166px; padding-right: 25px;">客户签名:__________________</td></tr>
       </table>
 </div>
    </form>

  </div>


        </div>


    </div>

      <div id="win" class="easyui-window" style=" width:600px; height: 460px;">
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

            <!--收款-->


</div>
<!--百度模板渲染模板 数据部分-->
   <script id="tpl_OrderPayMent" type="text/html">
            <form id="payOrder">


            <div class="optionclass">
               <div class="commonSelectWrap">
                             <em class="tit">订单金额:</em>
                             <div class="borderNone" >
                             <input id="Amount" class="easyui-numberbox" name="Amount" readonly="readonly" style="border:none" >
                            </div>
                            </div>
                  <div class="commonSelectWrap">
                                                              <em class="tit">电子优惠券抵扣:</em>
                                                              <div class="selectBox bodernone">
                                                                 <input id="coupon" class="easyui-combogrid" data-options="width:160,height:32,validType:'selectIndex'"  name="CouponID">
                                                             </div>
                                        </div>
               <div class="commonSelectWrap">
                             <em class="tit">纸质优惠券抵扣:</em>
                             <div class="searchInput" >
                                    <input id="Deduction" class="easyui-numberbox" name="Deduction" value="" data-options="width:160,height:32,precision:0,groupSeparator:','"><span style="
    float: right;
    margin-right: -24px;
    margin-top: -30px;
    font-size: 14px;
">元</span>
                            </div>
                            </div>
               <div class="commonSelectWrap">
                             <em class="tit">实付金额：</em>
                             <input id="ActualAmount" class="searchInput bodernone" name="ActualAmount" readonly="readonly" >
                            </div>
                <div class="commonSelectWrap">
                                        <em class="tit">付款方式：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="pay"  class="easyui-combobox" data-options="width:160,height:32,required:true"  name="PayID">
                                       </div>
                                       </div>
                <div class="commonSelectWrap" id="ValueCard">
                            <em class="tit">实体储值卡号：</em>
                                  <div class="searchInput bodernone">
                                      <input  class="easyui-validatebox" data-options="width:160,height:32,validType:['englishCheckSub','length[5,12]']"  name="ValueCard">
                                  </div>
                            </div>
               </div>
                </form>
            </script>

<script id="tpl_content" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){ var item=list[i]; #>
        <tr>
            <td class="checkBox"><em></em></td>
            <td class="seeIcon" data-orderid="<#=item.OrderId#>"></td>
            <td class="fontC"><#=item.OrderNo#></td>
            <td><#=item.CreateTime#></td>
            <td></td>
            <td><#=item.PayUnitName#></td>
            <td><#=item.PayAmount#></td>
            <td><#=item.PayStatus#></td>
            <td class="fontF"><#=item.PayType#></td>
            <td class="fontF"><#=item.OrderStatus#></td>
        </tr>
<#} #>
</script>
<!--会员标签-->
<script id="tpl_vipTag" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){  var item=list[i];#>
<span><#=item.TagName#></span>
<#}#>
</script>
<!--table没数据的提示-->
<script id="tpl_noContent" type="text/html">
<tr >
    <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><#=tips#></span></td>
</tr>
</script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="js/main.js"></script>
</asp:Content>
