<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>我的活动</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    

    <link href="<%=StaticUrl+"/module/CreativityWarehouse/MyActivity/css/queryList.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.4">
           
           
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <div class="selectBox">
                                       <input id="Status" name="Status"  class="easyui-combobox" placeholder="按照活动状态查询"   data-options="width:200,height:30,prompt:'按照活动状态查询'" />
                                  </div>
                              </div>
                              <div class="commonSelectWrap">
                                  <div class="selectBox">
                                       <input id="ActivityGroupCode" name="ActivityGroupCode"  class="easyui-combobox"  placeholder="按照活动类型查询"    data-options="width:200,height:30,prompt:'按照活动类型查询'" />
                                  </div>
                              </div>
                              <div class="commonSelectWrap">
                                  <div class="searchInput">
                                       <input id="EventName" name="EventName" type="text"  placeholder="录入活动关键字查询"    data-options="width:200,height:30" />
                                  </div>
                              </div>
                              <div class="moreQueryWrap">
                                   <a href="javascript:;" class="commonBtn queryBtn">查询</a>

                                </div>
                              
                             <div class="moreQueryWrap">
                                 <a href="javascript:;" class="commonBtn addActivityBtn">发起新活动</a>
                              </div>
                              <div class="moreQueryWrap">
                                 <a href="javascript:;" class="viewall">查看全部活动 ></a>
                              </div>
                        </form>
                        </div>
                    <!--<h2 class="commonTitle">会员查询</h2>-->
                </div>

                <div class="ActivityGroups">
                    <div id="Product"  data-code="Product" class="ActivityGroupName hotbordercolor ">
                        <div class="ActivityGroupImg hotcolor "><img src="../MyActivity/images/ActivityGroup01.png" /></div>
                        <div class="ActivityGroupstatus">
                            <div class="running">在进行<span class="runningNum num">0</span></div>
                            <div class="unreleased">未发布<span class="unreleasedNum num">0</span></div>
                            <div class="invalid">到期<span class="invalidNum num">0</span></div>
                        </div>
                    </div>

                    <div id="Holiday"   data-code="Holiday"  class="ActivityGroupName holidaybordercolor ">
                        <div class="ActivityGroupImg holidaycolor"><img src="../MyActivity/images/ActivityGroup02.png" /></div>
                        <div class="ActivityGroupstatus">
                            <div class="running">在进行<span class="runningNum num">0</span></div>
                            <div class="unreleased">未发布<span class="unreleasedNum num">0</span></div>
                            <div class="invalid">到期<span class="invalidNum num">0</span></div>
                        </div>
                    </div>

                    <div id="Unit"  data-code="Unit" class="ActivityGroupName stockbordercolor ">
                        <div class="ActivityGroupImg stockcolor"><img src="../MyActivity/images/ActivityGroup03.png" /></div>
                        <div class="ActivityGroupstatus">
                            <div class="running">在进行<span class="runningNum num">0</span></div>
                            <div class="unreleased">未发布<span class="unreleasedNum num">0</span></div>
                            <div class="invalid">到期<span class="invalidNum num">0</span></div>
                        </div>
                    </div>
                </div>

                <div class="listdata">
                   

                </div>

            </div>

          
   
        </div>
    

     <div style="display:none;">
    <div id="winrelease" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true" >
        <div class="easyui-layout" data-options="fit:true" id="Div1">
            <div data-options="region:'center'" style="padding:10px;">
               <div class="QRcode OnlineQRCodeId">
                   <div class="title">线上活动</div>
                   <div class="desc">完整版活动，活跃线上用户，提升忠诚度</div>
                   <div class="codeimg "><img src="" /></div>
                   <div class="downbtn "><a class="commonBtn downaddress sales icon icon_downLoad"  download="线上活动二维码" href="javascript:void(0)">下载</a></div>
                   <div class="address"><input   type="text" class="addressinput" /><div style="position:relative;"><a  href="#none"    class="commonBtn copybtn addrcopy">复制</a></div></div>
               </div>
               <div class="QRcode OnfflineQRCodeId">
                   <div class="title">门店应用</div>
                   <div class="desc">精简版活动，活跃终端客户，提升好感度</div>
                   <div class="codeimg "><img src="" /></div>
                   <div class="downbtn "><a class="commonBtn downaddress  sales icon icon_downLoad" download="门店应用二维码" href="javascript:void(0)">下载</a></div>
                   <div class="address"><input type="text" class="addressinput" /><div style="position:relative;"><a  href="#none"      class="commonBtn copybtn addrcopy">复制</a></div></div>
               </div>
            </div>
            <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn release" >直接发布</a>
      				<a class="easyui-linkbutton commonBtn closeBtn"  href="javascript:void(0)" onclick="javascript:$('#winrelease').window('close')" >完成</a>
      			</div>
        </div>
    </div>  
</div>  


     <div style="display:none;">
    <div id="winReleaseSuccess" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true" >
        <div class="easyui-layout" data-options="fit:true" id="Div10">
            <div data-options="region:'center'" style="padding:10px;">
                <div class="ReleaseSuccess">
                您的活动即将发布，发布后，您的活动将无法进行界面和奖励的修改。
                    </div>
                <div class="btnWrap" id="Div9" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn ReleaseSuccessbtn" >确定</a>
      			</div>
               </div>
        </div>
    </div>  
</div>  


        <!--数据详情部分-->
<script id="tpl_Activity" type="text/html">
     <# for(i=0;i<MyActivityList.length;i++){ _data=MyActivityList[i]; #>
    <div class="Activity">
            <div class="ActivityContent">
                <div class="Activityimg"><img src="<#=_data.ImageURL #>" /></div>
                <div class="ActivityStatus <# if(_data.Status=="10"){ #>redBG<#}#> <# if(_data.Status=="20"){ #>blueBG<#}#> <# if(_data.Status=="30"||_data.Status=="40"){ #>blackBG<#}#>"><#=_data.StatusName #></div>
                <div class="ActivityQRcode"><img src="<#=_data.QRCodeImageUrlForOnline #>" /></div>
                <div class="ActivityDesc"><span><#=_data.Name #></span>
                    <# if(_data.Status=="10"){ #>
                    <span class="edit" data-eventid="<#=_data.CTWEventId #>"  data-id="<#=_data.TemplateId #>" ></span>
                    <#}#>
                </div>
            </div>
            <div class="ActivityOpeartion"><div class="Opeartiondesc"><div class="viewdata" data-startdate="<#=_data.StartDate #>" data-enddate="<#=_data.EndDate #>" data-interactiontype="<#=_data.InteractionType #>" data-status="<#=_data.Status #>" data-eventid="<#= _data.CTWEventId #>" data-id="<#=_data.LeventId #>">查看活动数据</div>
               <# if(_data.EventInfo){#>
                <div class="date"><#=(_data.StartDate.substring(0,_data.StartDate.indexOf(" "))+"-"+_data.EndDate.substring(5,_data.EndDate.indexOf(" "))) #></div>
                 <#} #>
                                           </div><div class="releasebtn <#=_data.Status=="10"?"":"view" #>" data-onfflineqrcode="<#=_data.QRCodeImageUrlForUnit #>" data-onlineqrcode="<#=_data.QRCodeImageUrlForOnline #>" data-onlineredirecturl="<#=_data.OnLineRedirectUrl #>" data-offlineredirecturl="<#=_data.OffLineRedirectUrl #>" data-eventid="<#= _data.CTWEventId #>" data-status="<#=_data.Status #>"  ><#=_data.Status=="10"?"发布":"预览" #></div></div>
        </div>
          <#} #>

    </script>

   <div style="display:none;">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,draggable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="Div3">
              <div class="ActivityOperation">
              <div class="ActivityDateLayer">活动日期：<span class="ActivityDate">2016/4/4-2016/4/5</span></div><a   download="清单导出.xls"  href="javascript:;" class="commonBtn exportlist">导出清单</a>
                  </div>
            <div class="tableWrap" id="tableWrap3" style="display:inline-block;width:100%;">

                   <table class="dataTable" id="gridTable3">
                          
                   </table>
                    <div id="pageContianer3">
                    <div class="dataMessage3" >没有符合条件的查询记录</div>
                        <div id="kkpager3">
                        </div>
                    </div>
                </div>

             
               <div class="btnWrap" id="Div2" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a  class="easyui-linkbutton commonBtn submit" >确定</a>
      			</div>
        </div>
    
        </div>  

      </div>

      <div style="display:none;">
      <div id="windesc" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="panlconent">

             

        </div>
    
        </div>  

      </div>

     <div style="display:none;">
      <div id="windefer" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,draggable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="panlconent1">
            <div class="lineform"><span>开始时间：</span><span class="_startdate lineinput"></span></div>
            <div class="lineform"><span>结束时间：</span> <input id="_endTime"  class="easyui-datebox" name="_endTime" type="text"     data-options="width:200,height:30,prompt:'请选择'" /></div>

               <div class="btnWrap" id="Div4" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn submit deferbtn" >确定</a>
      			</div>
        </div>
    
        </div>  

      </div>

    <div style="display:none;">
      <div id="winadd" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,draggable:false,maximizable:false,closed:true,closable:true" >
          <div class="easyui-layout" data-options="fit:true" id="panlconent2">
            <div class="lineform" style="  margin-top: 50px;"><span>数量：</span><input id="addPrize" class="easyui-numberbox" data-options="min:1,max:10000,width:260,height:30"  type="text"  placeholder="请输入"    /></div>

               <div class="btnWrap" id="Div5" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn submit addbtn" >确定</a>
      			</div>
        </div>
    
        </div>  

      </div>
   

    <!--数据详情部分-->
<script id="tpl_gameEventDataDetail" type="text/html">
    <div class="Activitydesc Activitydatadesc">
              <div class="EventDataOperation">
                  <div class="EDO_opt">
                      <# if(Status!="40"){#>
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
                                  <div class="title">礼品券</div>
                                  <div class="value"><span class="GiftCardvalue"><#=LeventsStats.PrizeCount #></span>份</div></div>
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
                  <div class="EDO_export"><div class="prizeExtend">奖品发放</div><a class="viewlist" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>"  href="javascript:void(0)" data-id="<#=EventId #>" data-interactiontype="<#=InteractionType #>">查看发放清单</a><a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?LeventId=<#=CTWEventId #>&method=GivingOutAwardsListExport" download="奖品发放导出.xls" class="exportbtn commonBtn">导出清单</a></div>
              </div>
              <div class="PrizeList">
                   <div class="tableWrap" id="tableWrap2" style="display:inline-block;width:100%;">

                   <table class="dataTable" id="gridTable2">
                          
                   </table>
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
       
</script>

    <!--数据详情部分-->
<script id="tpl_EventDataDetail" type="text/html">
    <div class="Activitydesc Activitydatadesc">
              <div class="EventDataOperation">
                  <div class="EDO_opt">
                      <# if(Status!="40"){#>
                        <a href="javascript:;" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>" class="commonBtn defer">活动延期</a>
                      <a href="javascript:;" class="commonBtn endbtn">结束活动</a>
                      <#}#>
                      
                      <a href="javascript:;" class="commonBtn returnbtn">返回</a></div>
                  <div class="EDO_data">
                      <div class="prizecount">
                          <div class="Sales">
                              <div class="num"><#=LeventsStats.OrderActualAmount #></div>
                              <div class="desc">活动总销量</div>
                          </div>
                          <div class="parizetype">
                              <div class="integral parizecontainer">
                                  <div class="title">订单数</div>
                                  <div class="value"><span class="integralvalue"><#=LeventsStats.OrderCount #></span>个</div>
                              </div>
                              <div class="verticalLine"></div>
                              <div class="Vouchers parizecontainer">
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
                  <div class="EDO_export"><div class="prizeExtend">奖品发放</div><a class="viewlist" data-startdate="<#=startdate #>" data-enddate="<#=enddate #>"  href="javascript:void(0)" data-id="<#=EventId #>" data-interactiontype="<#=InteractionType #>" >查看发放清单</a><a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?LeventId=<#=CTWEventId #>&method=SalesListExport" download="销售清单导出.xls" class="exportbtn commonBtn">导出清单</a></div>
              </div>
              <div class="PrizeList">
                   <div class="tableWrap" id="Div6" style="display:inline-block;width:100%;">

                   <table class="dataTable" id="Table1">
                          
                   </table>
                    <div id="Div7">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="Div8">
                        </div>
                    </div>
                </div>

              </div>
              <div class="EventDataCount">
                  <div class="ActivitySales">
                      <div class="title">活动每日销量<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=PromotionVipAddExport" download="活动每日销量.xls" class="fansexportbtn commonBtn">导出清单</a></div>
                      <div id="ActivitySales" class="content "></div>
                  </div>
                  <div class="ActivityOrder">
                      <div class="title">活动每日订单<a href="/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?ctweventId=<#=CTWEventId #>&method=PromotionVipAddExport" download="活动每日订单.xls" class="registerexportbtn commonBtn">导出清单</a></div>
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
        
</script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>

