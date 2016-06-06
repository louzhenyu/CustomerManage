<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>集客内容</title>
    <link href="<%=StaticUrl+"/module/SetOffManage/css/content.css?v=0.1"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/content.js" >
        <div class="contentArea_vipquery">
            <!-- 内容区域 -->
            <!--集客数据方块展示-->
            <div class="contentTotal">
                <!--集客来源-->
                <div class="contentTitle borderRadiu">
                   <span>内容贡献分析</span>
                   <a href="javascript:;" class="checkDay" id="checkDThi">近30天</a>
                   <a href="javascript:;" class="checkDay" id="checkDSeven">近7天</a>
                </div>
                <div class="contentData ">
                    <div class="sourcebase borderRadiu">
                       <div class="totalTitle">
                           <img src="images/2.1_08.png" class="incos" />
                           <span>总计</span>
                       </div>
                       <div class="total bordergrey" >
                            <div class="numTitle">
                                <span>分享总计</span>
                            </div>
                            <div class="numcenter colorred font24" style="margin:5px auto 50px" id="sharetotal">
                                <span></span>
                            </div>
                            <div class="hrbottom"></div>
                            <div class="numTitle"><span>新增会员总数</span></div>
                            <div class="numcenter colorred font24" id="setofftotal" >
                                <span></span>
                                <img src="" class="incoB" />
                                <span class="colorgrey font18"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase" >
                           <div class="title borderRadiu">
                               <img src="images/3.1_03.png" class="incos" />
                               <span>活动</span>
                           </div>
                            <div class="bordergrey total2 borderRadiu" style="margin-bottom:10px">
                                <div class="col2">
                                    <div class="numTitle"><span>活动分享</span></div>
                                    <div class="numleft font24 colorblue" id="activityshare"><span></span></div>
                                </div>
                                <div class="hrright"></div>
                                <div class="col2">
                                    <div class="numTitle"><span>新增会员</span></div>
                                    <div class="numleft font24 colorblue" id="activitysetoff">
                                        <span></span>
                                        <img src="" class="incoB" />
                                        <span class="colorgrey font18"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="title borderRadiu">
                               <img src="images/3.1_09.png" class="incos" />
                               <span>集客海报</span>
                           </div>
                            <div class="bordergrey total2 borderRadiu" >
                                <div class="col2">
                                    <div class="numTitle"><span>集客海报分享</span></div>
                                    <div class="numleft font24 colorblue" id="sharePoster"><span></span></div>
                                </div>
                                <div class="hrright"></div>
                                <div class="col2">
                                    <div class="numTitle"><span>新增会员</span></div>
                                    <div class="numleft font24 colorblue" id="SetoffPoster">
                                        <span></span>
                                        <img src="" class="incoB" />
                                        <span class="colorgrey font18"></span>
                                    </div>
                                </div>
                            </div>
                     </div>
                    <div class="sourcebase" style="width:33.3%;">
                            <div class="title borderRadiu">
                               <img src="" class="incos" />
                               <span>优惠券</span>
                           </div>
                            <div class="bordergrey total2 borderRadiu" style="margin-bottom:10px">
                                <div class="col2">
                                    <div class="numTitle"><span>优惠券分享</span></div>
                                    <div class="numleft font24 colorblue" id="Couponshare"><span></span></div>
                                </div>
                                <div class="hrright"></div>
                                <div class="col2">
                                    <div class="numTitle"><span>新增会员</span></div>
                                    <div class="numleft font24 colorblue" id="Couponsetoff">
                                        <span></span>
                                        <img src="" class="incoB" />
                                        <span class="colorgrey font18"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="title borderRadiu">
                               <img src="" class="incos" />
                               <span>商品</span>
                           </div>
                            <div class="bordergrey total2 borderRadiu">
                                <div class="col2">
                                    <div class="numTitle"><span>商品分享</span></div>
                                    <div class="numleft font24 colorblue" id="Goodshare"><span></span></div>
                                </div>
                                <div class="hrright"></div>
                                <div class="col2">
                                    <div class="numTitle"><span>新增会员</span></div>
                                    <div class="numleft font24 colorblue" id="Goodssetoff">
                                        <span></span>
                                        <img src="" class="incoB" />
                                        <span class="colorgrey font18"></span>
                                    </div>
                                </div>
                            </div>
                     </div>
                </div>
                <!--集客趋势图表-->
                <div class="soruceChart" style="margin-bottom:20px" >
                    <div class="chartTitle">
                        <img src="images/2.1_30.png" class="incos" />
                        <span>最近5天新增会员趋势</span>
                    </div>
                    <div id="staffCharts" style="height:300px;width:100%;"></div>
                </div>
            </div>
            <!--查询表格数据-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                <div class="moreQueryWrap">
                   <form></form>
                   <form id="seach">
                          <div class="commonSelectWrap" ">
                               <em class="tit">类型筛选：</em>
                               <label class=" selectBox">
                                    <input id="filterSource" class="easyui-combobox" data-options="width:200,height:32" name="source" >
                               </label>
                           </div>
                         <div class="commonSelectWrap" >
                             <em class="tit">开始时间：</em>
                             <div class="selectBox" >
                                  <input id="reserveDateBegin"  name="ReserveDateBegin" class="easyui-datebox"  data-options="width:200,height:32" />
                              </div>
                         </div>
                         <div class="commonSelectWrap">
                               <em class="tit">结束时间：</em>
                               <div class="selectBox">
                                     <input id="reserveDateEnd" name="ReserveDateEnd" class="easyui-datebox"data-options="width:200,height:32" validType="compareDate[$('#reserveDateBegin').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/>
                               </div>
                         </div>
                         <div class="moreQueryWrap">
                             <a href="javascript:;" class="commonBtn queryBtn select" >查询</a>
                         </div>
                    </form>
                </div>
            </div>
            <!--发送通知-->
             <!--表格无数据显示内容-->
            <div class="tableWrap" id="tableWrap">
                    <div class="dataTable" id="gridTable">
                         <div  class="loading">
                               <span><img src="../static/images/loading.gif"></span>
                          </div>
                    </div>
                    <div id="pageContianer">
                        <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager" ></div>
                    </div>
                </div>
         </div>
    </div>
</asp:Content>
