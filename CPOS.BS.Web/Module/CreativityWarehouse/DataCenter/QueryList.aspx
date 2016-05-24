<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>数据中心</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/CreativityWarehouse/DataCenter/css/queryList.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js">
           
           
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">

                <div class="ActivityGroups">
                    <div id="Product"  data-code="Product" class="ActivityGroupName hotbordercolor graycolor">
                        <div class="ActivityGroupImg hotcolor "><img src="../MyActivity/images/ActivityGroup01.png" /></div>
                        <div class="ActivityGroupstatus">
                            <div class="running">在进行<span class="runningNum num">0</span></div>
                            <div class="unreleased">未发布<span class="unreleasedNum num">0</span></div>
                            <div class="invalid">到期<span class="invalidNum num">0</span></div>
                        </div>
                    </div>

                    <div id="Holiday"   data-code="Holiday"  class="ActivityGroupName holidaybordercolor graycolor">
                        <div class="ActivityGroupImg holidaycolor"><img src="../MyActivity/images/ActivityGroup02.png" /></div>
                        <div class="ActivityGroupstatus">
                            <div class="running">在进行<span class="runningNum num">0</span></div>
                            <div class="unreleased">未发布<span class="unreleasedNum num">0</span></div>
                            <div class="invalid">到期<span class="invalidNum num">0</span></div>
                        </div>
                    </div>

                    <div id="Unit"  data-code="Unit" class="ActivityGroupName stockbordercolor graycolor">
                        <div class="ActivityGroupImg stockcolor"><img src="../MyActivity/images/ActivityGroup03.png" /></div>
                        <div class="ActivityGroupstatus">
                            <div class="running">在进行<span class="runningNum num">0</span></div>
                            <div class="unreleased">未发布<span class="unreleasedNum num">0</span></div>
                            <div class="invalid">到期<span class="invalidNum num">0</span></div>
                        </div>
                    </div>
                </div>

                <!--信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                   <em class="tit">活动名称：</em>
                                  <div class="searchInput">
                                       <input id="Text2" name="EventName" type="text"  placeholder="请输入活动名称"    data-options="width:200,height:30" />
                                  </div>
                              </div>
                              <div class="commonSelectWrap">
                                   <em class="tit">活动状态：</em>
                                  <div class="selectBox">
                                       <input id="Status" name="EventStatus"  class="easyui-combobox"  data-options="width:200,height:30,prompt:'请选择'" />
                                  </div>
                              </div>
                              <div class="moreQueryWrap">
                                   <a href="javascript:;" class="commonBtn queryBtn">查询</a>

                                </div>
                               <div style="clear:both;"></div>
                              <div class="commonSelectWrap">
                                   <em class="tit">开始时间：</em>
                                  <div class="selectBox">
                                       <input id="EventName" class="easyui-datebox" name="BeginTime" type="text"     data-options="width:200,height:30,prompt:'请选择'" />
                                  </div>
                              </div>
                               <div class="commonSelectWrap">
                                   <em class="tit">结束时间：</em>
                                  <div class="selectBox">
                                       <input id="Text1" class="easyui-datebox" name="EndTime" type="text"      data-options="width:200,height:30,prompt:'请选择'" />
                                  </div>
                              </div>
                              
                              
                            
                        </form>
                        </div>
                    <!--<h2 class="commonTitle">会员查询</h2>-->
                </div>

                
                <div class="tableWrap" id="tableWrap" style="display:inline-block;width:100%;">

                   <div class="dataTable" id="gridTable">
                         <!-- <div  class="loading">
                                   <span>
                                 <img src="../../static/images/loading.gif"></span>
                            </div>-->
                   </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager">
                        </div>
                    </div>
                </div>
              
            </div>

            <div>


            </div>
   
        </div>
      <div style="display:none;">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="panlconent">
           <div data-options="region:'center'" style="overflow-x:hidden">
              <div class="ActivityOperation">
              <div class="ActivityDateLayer">活动日期：<span class="ActivityDate">2016/4/4-2016/4/5</span></div><a  href="javascript:;"  download="清单导出.xls" href="javascript:;" class="commonBtn exportlist">导出清单</a>
                  </div>
            <div class="tableWrap" id="tableWrap3" style="display:inline-block;width:100%;">
                    
                   <div class="dataTable" id="gridTable3">
                          
                   </div>
                    <div id="pageContianer3">
                    <div class="dataMessage3" >没有符合条件的查询记录</div>
                        <div id="kkpager3">
                        </div>
                    </div>
                </div>
              </div>
             <div class="btnWrap" id="Div1" data-options="region:'south',border:false">
      				<a  class="easyui-linkbutton commonBtn submit" >确定</a>
      			</div>

        </div>
    
        </div>  

      </div>

     <div style="display:none;">
      <div id="windefer" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="panlconent1">
            <div class="lineform"><span>开始时间：</span><span class="_startdate lineinput"></span></div>
            <div class="lineform"><span>结束时间：</span> <input id="_endTime"  class="easyui-datebox" name="_endTime" type="text"     data-options="width:200,height:30,prompt:'请选择'" /></div>

               <div class="btnWrap" id="Div2" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn submit deferbtn" >确定</a>
      			</div>
        </div>
    
        </div>  

      </div>

    <div style="display:none;">
      <div id="winadd" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="panlconent2">
            <div class="lineform"  style="  margin-top: 50px;"><span>数量：</span><input id="addPrize" class="easyui-numberbox" data-options="min:1,max:10000,width:260,height:30"   type="text"  placeholder="请输入"    /></div>

               <div class="btnWrap" id="Div3" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn submit addbtn" >确定</a>
      			</div>
        </div>
    
        </div>  

      </div>
   

    <!--数据详情部分-->
<script id="tpl_gameEventDataDetail" type="text/html">
    <tr class="tr_DataDetail"><td colspan='7' >
    <div class="Activitydesc">
              <div class="EventDataOperation">
                  <div class="EDO_opt">
                      <# if(Status!="40") {#>
                        <a href="javascript:;" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>" class="commonBtn defer">活动延期</a>
                      <a href="javascript:;" class="commonBtn endbtn">结束活动</a>
                      <#}#>
                      <a href="javascript:;" class="commonBtn returnbtn">返回</a></div>
                  <div class="EDO_data">
                      <div class="prizecount">
                          <div class="prizedata">
                              <div class="num"><#=LeventsStats.PrizeCount #></div>
                              <div class="desc">奖品发放总数（份）</div>
                          </div>
                          <div class="parizetype">
                              <div class="integral parizecontainer">
                                  <div class="title">积分</div>
                                  <div class="value"><span class="integralvalue"><#=LeventsStats.PointSum #></span>分</div>
                              </div>
                              <div class="verticalLine"></div>
                              <div class="Vouchers parizecontainer">
                                  <div class="title">代金券</div>
                                  <div class="value"><span class="Vouchersvalue"><#=LeventsStats.CASHCouponValue #></span>元</div></div>
                              
                              <div class="verticalLine"></div>
                              <div class="GiftCard parizecontainer">
                                  <div class="title">兑换券</div>
                                  <div class="value"><span class="GiftCardvalue"><#=LeventsStats.GIFTCouponValue #></span>份</div></div>
                          </div>
                      </div>
                      <div class="fansmember">
                          <div class="fansdata">
                              <div class="fansnum"><#=LeventsStats.FocusVipCount #></div>
                              <div class="fansdesc">新增公众号粉丝（人）</div>
                          </div>
                          <div class="memberdata">
                              <div class="membernum"><#=LeventsStats.RegVipCount #></div>
                              <div class="memberdesc">新增会员（人）</div>
                          </div>
                      </div>
                  </div>
                  <div class="EDO_export"><div class="prizeExtend">奖品发放</div><a class="viewlist" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>" href="javascript:void(0)" data-id="<#=EventId #>" data-interactiontype="<#=InteractionType #>">查看发放清单</a><a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=GameAwardsListExport" download="清单导出.xls" class="exportbtn commonBtn">导出清单</a></div>
              </div>
              <div class="PrizeList">
                   <div class="tableWrap tableWrap2"  style="display:inline-block;width:100%;">

                   <div class="dataTable gridTable2" >
                          
                   </div>
                    <div id="pageContianer2">
                    <div class="dataMessage2" >没有符合条件的查询记录</div>
                        <div id="kkpager2">
                        </div>
                    </div>
                </div>

              </div>
            <div class="EventDataCount">
                  <div class="fans">
                      <div class="title">公众号粉丝增长数量<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=GameVipAddExport" download="公众号粉丝增长数量.xls" class="fansexportbtn commonBtn">导出清单</a></div>
                      <div id="fansContainer" class="content "></div>
                  </div>
                  <div class="register">
                      <div class="title">注册会员增长数量<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=PromotionVipAddExport" download="注册会员增长数量.xls"  class="registerexportbtn commonBtn">导出清单</a></div>
                      <div  id="registerContainer" class="content"></div>
                  </div>
              </div>
              
          </div>
        </td></tr>
</script>

    <!--数据详情部分-->
<script id="tpl_EventDataDetail" type="text/html">
    <tr class="tr_DataDetail"><td colspan='7' >
    <div class="Activitydesc">
              <div class="EventDataOperation">
                  <div class="EDO_opt">
                      <# if(Status!="40") {#>
                        <a href="javascript:;" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>" class="commonBtn defer">活动延期</a>
                      <a href="javascript:;" class="commonBtn endbtn">结束活动</a>
                      <#}#>
                      <a href="javascript:;" class="commonBtn .returnbtn">返回</a></div>
                  <div class="EDO_data">
                      <div class="prizecount">
                          <div class="Sales">
                              <div class="num"><#=LeventsStats.OrderActualAmount #></div>
                              <div class="desc">活动总销量</div>
                          </div>
                          <div class="parizetype">
                              <div class="integral parizecontainer  w280">
                                  <div class="title">订单数</div>
                                  <div class="value"><span class="integralvalue"><#=LeventsStats.OrderCount #></span>个</div>
                              </div>
                              <div class="verticalLine"></div>
                              <div class="Vouchers parizecontainer w280">
                                  <div class="title">客单价</div>
                                  <div class="value"><span class="Vouchersvalue"><#=LeventsStats.CustSinglePrice #></span>元</div></div>
                              
                            
                          </div>
                      </div>
                      <div class="fansmember">
                          <div class="fansdata">
                              <div class="fansnum"><#=LeventsStats.FocusVipCount #></div>
                              <div class="fansdesc">新增公众号粉丝（人）</div>
                          </div>
                          <div class="memberdata">
                              <div class="membernum"><#=LeventsStats.RegVipCount #></div>
                              <div class="memberdesc">新增会员（人）</div>
                          </div>
                      </div>
                  </div>
                  <div class="EDO_export"><div class="prizeExtend">商品销量</div><a class="viewlist" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>" href="javascript:void(0)" data-id="<#=EventId #>" data-interactiontype="<#=InteractionType #>" >查看销售清单</a><a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=SalesItemsListExport" download="清单导出.xls" class="exportbtn commonBtn">导出清单</a></div>
              </div>
              <div class="PrizeList">
                   <div class="tableWrap tableWrap2"  style="display:inline-block;width:100%;">

                   <table class="dataTable gridTable2" >
                          
                   </table>
                    <div id="pageContianer2" style="padding-right: 15px">
                    <div class="dataMessage2" >没有符合条件的查询记录</div>
                        <div id="kkpager2">
                        </div>
                    </div>
                </div>

              </div>
              

            <div class="EventDataCount">
                  <div class="ActivitySales">
                      <div class="title">活动每日销量<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=OrderMoneyExport" download="活动每日销量.xls" class="fansexportbtn commonBtn">导出清单</a></div>
                      <div id="ActivitySales" class="content "></div>
                  </div>
                  <div class="ActivityOrder">
                      <div class="title">活动每日订单<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=OrderCountExport" download="活动每日订单.xls" class="registerexportbtn commonBtn">导出清单</a></div>
                      <div  id="ActivityOrder" class="content"></div>
                  </div>
                  <div class="fans">
                      <div class="title">公众号粉丝增长数量<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=GameVipAddExport" download="公众号粉丝增长数量.xls" class="fansexportbtn commonBtn">导出清单</a></div>
                      <div id="fansContainer" class="content "></div>
                  </div>
                  <div class="register">
                      <div class="title">注册会员增长数量<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=PromotionVipAddExport" download="注册会员增长数量.xls" class="registerexportbtn commonBtn">导出清单</a></div>
                      <div  id="registerContainer" class="content"></div>
                  </div>
                  
              </div>
          </div>
        </td></tr>
</script>



</asp:Content>

