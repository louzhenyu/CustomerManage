<%@ Page Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master" AutoEventWireup="true" CodeBehind="IndexPage.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Index.IndexPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>连锁掌柜-首页</title>
    <link href="css/IndexPage.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="allPage" id="section" data-js="js/IndexPage.js?ver=0.1">
    <div class="contentmodule">
        <div class="modules">
            <div class="module " data-menucode="yd">
             <a class="menusrc" href="JavaSctipy:void(0)">
                <div class="modulehead"  >

                 <div class="title"><img src="images/shop.png" /><span>云店O2O</span></div>
                 <div class="gomore"><img src="images/gomore.png" /></div>
                 </div>
                 </a>
                <div class="modulecontent">
                    <div class="count">
                        <div class="numtitle">门店数量</div>
                        <div  class="num UnitCount">0家</div>

                    </div>
                    <div class="achievement">
                         <div class="achievementtitle">当日总业绩</div>
                        <div  class="achievementnum"><span class="num UnitCurrentDayOrderAmount">0</span>（万元）<img src="images/drop1.png"  class="rise"/><span class="UnitCurrentDayOrderAmountDToD">0%</span></div>

                    </div>
                </div>
            </div>
            <div class="module"  data-menucode="dzhh">
                 <a class="menusrc" href="JavaScript:void(0)">
                <div class="modulehead" >

                 <div class="title"><img src="images/partner.png" /><span>店长合伙</span></div>
                 <div class="gomore"><img src="images/gomore.png" /></div>

               </div> </a>
                <div class="modulecontent">
                    <div class="count">
                        <div class="numtitle">店长人数</div>
                        <div  class="num UnitMangerCount">0人</div>

                    </div>
                    <div class="achievement">
                         <div class="achievementtitle">当日店均业绩</div>
                        <div  class="achievementnum"><span class="num UnitCurrentDayAvgOrderAmount">0</span>（万元）<img src="images/drop1.png"  class="rise"/><span class="UnitCurrentDayAvgOrderAmountDToD">0%</span></div>

                    </div>
                </div>
            </div>
            <div class="module" data-menucode="dyjl">
            <a class="menusrc" href="JavaScript:void(0)">
                <div class="modulehead" data-menucode="dyjl">

                    <div class="title"><img src="images/reward.png" /><span>店员激励</span></div>
                     <div class="gomore"><img src="images/gomore.png" /></div>


               </div>
               </a>
                <div class="modulecontent">
                    <div class="count">
                        <div class="numtitle">店员人数</div>
                        <div  class="num UnitUserCount">0人</div>

                    </div>
                    <div class="achievement">
                         <div class="achievementtitle">当日人均业绩</div>
                        <div  class="achievementnum"><span class="num UserCurrentDayAvgOrderAmount">0人</span><img src="images/drop1.png"  class="rise"/><span class="UserCurrentDayAvgOrderAmountDToD">0%</span></div>

                    </div>
                </div>
            </div>
            <div class="module" data-menucode="qdcx">
            <a class="menusrc" href="JavaScript:void(0)">
                <div class="modulehead" >

                 <div class="title"><img src="images/money.png" /><span>创新渠道</span></div>
                 <div class="gomore" ><img src="images/gomore.png" /></div>

               </div>
               </a>
                <div class="modulecontent">
                    <div class="count">
                        <div class="numtitle">分销商数量</div>
                        <div  class="num RetailTraderCount">0家</div>

                    </div>
                    <div class="achievement">
                         <div class="achievementtitle">当日分销业绩</div>
                        <div  class="achievementnum"><span class="num CurrentDayRetailTraderOrderAmount">0</span><img src="images/drop1.png"  class="rise"/><span class="CurrentDayRetailTraderOrderAmountDToD">0%</span></div>

                    </div>
                </div>
            </div>
            <div class="module"data-menucode="hyjk">
                      <a class="menusrc" href="JavaScript:void(0)">
                <div class="modulehead"  >

                <div class="title"><img src="images/member.png" /><span>会员金矿</span></div>
                <div class="gomore">
                <img src="images/gomore.png" />

                </div></div>   </a>
                <div class="modulecontent">
                    <div class="count">
                        <div class="numtitle">会员总数</div>
                        <div  class="num VipCount">0人</div>

                    </div>
                    <div class="achievement">
                         <div class="achievementtitle">当日新增会员</div>
                        <div  class="achievementnum"><span class="num NewVipCount">0人</span></div>

                    </div>
                </div>
            </div>
            <div class="module" data-menucode="yxlq">
             <a class="menusrc" href="JavaScript:void(0)">
                <div class="modulehead" >

                <div class="title"><img src="images/arms.png" /><span>营销利器</span></div>
                <div class="gomore">
                <img src="images/gomore.png" /></div>

                </div>
                  </a>
                <div class="modulecontent">
                    <div class="count">
                        <div class="numtitle">当前活动数</div>
                        <div  class="num EventsCount">0个</div>

                    </div>
                    <div class="achievement">
                         <div class="achievementtitle">活动参与人数</div>
                        <div  class="achievementnum"><span class="num EventJoinCount">0人</span></div>

                    </div>
                </div>
            </div>

            
            
        </div>
        <div class="Ranking">
            <div class="RankingList">
                <div class="Rankinghead"><div class="title"><img src="images/riselist.png" /><span>业绩月排名</span></div><div class="desc">前五名<img src="images/rise.png" /></div></div>
                <div class="Rankingcontent">
                    <ul class="PerformanceTop">
                        
                    </ul>
                </div>
            </div>
            <div class="RankingList">
                <div class="Rankinghead"><div class="title"><img src="images/droplist.png" /><span>业绩月排名</span></div><div class="desc">后五名<img src="images/drop.png" /></div></div>
                <div class="Rankingcontent">
                    <ul class="PerformanceLower">
                        
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="Statistic">
        <div class="Statisticlist">
            <div class="achievementarea">
            <div class="achievementmodule">
                <div class="achievementmodulehead"><div class="title"><img src="images/list.png" /><span>业绩</span></div></div>
                <div class="achievementmodulecontent">
                   <div class="Charts">
                       <div class="Chart">
                           <div class="ring MonthArchivePectring">
                               <div class="bigcircle"></div>
                               <div class="Smallcircle"></div>
                            </div>
                           <div class="movering MonthArchivePectmovering">
                               <div class="bigcircle"></div>
                               <div class="Smallcircle"></div>
                            </div>
                           <div class="leftring">
                               <div class="bigcircle"></div>
                               <div class="Smallcircle"></div>
                            </div>
                    	   <div class="numvalue " ><span class="Pectvalue MonthArchivePect">0%</span><span>月度业绩达成</span></div>
                           <div class="leftcover"></div>
                       </div>

                       <div class="Chart">
                           <div class="ring VipContributePectring">
                               <div class="bigcircle"></div>
                               <div class="Smallcircle"></div>
                            </div>
                           <div class="movering VipContributePectmovering">
                               <div class="bigcircle"></div>
                               <div class="Smallcircle"></div>
                            </div>
                           <div class="leftring">
                               <div class="bigcircle"></div>
                               <div class="Smallcircle"></div>
                            </div>
                    	   <div class="numvalue" ><span class="Pectvalue VipContributePect">0%</span><span>会员贡献率</span></div>
                           <div class="leftcover"></div>
                       </div>
                   </div>
                   <div  class="Chartdesc">
                       <div class="optiondesc"><em>单店月均客流：</em><span class="CurrentMonthSingleUnitAvgTranCount">0</span>人次</div>
                       <div class="optiondesc"><em>门店月均单价：</em><span class="CurrentMonthUnitAvgCustPrice">0</span>元/人次</div>
                       <div class="optiondesc"><em>单店月均业绩：</em><span class="CurrentMonthSingleUnitAvgTranAmount">0</span>万元</div>
                       <div class="optiondesc"><em>门店月均总业绩：</em><span class="CurrentMonthTranAmount">0</span>万元</div>

                   </div>
                </div>
            </div>
                </div>
            <div class="ordersarea">
            <div class="orders">
                <div class="orderhead"><div class="title"><img src="images/order.png" /><span>订单</span></div></div>
                <div class="ordercontent">
                    <div class="order">
                        <div class="ordervalue PreAuditOrder">0</div>
                        <div class="orderdesc">待审核订单</div>
                    </div>
                    <div class="order">
                        <div class="ordervalue PreSendOrder">0</div>
                        <div class="orderdesc">待发货订单</div>
                    </div>
                    <div class="order">
                        <div class="ordervalue PreTakeOrder">0</div>
                        <div class="orderdesc">门店待提货订单</div>
                    </div>
                    <div class="order">
                        <div class="ordervalue PreRefund">0</div>
                        <div class="orderdesc ">待退货</div>
                    </div>
                    <div class="order">
                        <div class="ordervalue PreReturnCash">0</div>
                        <div class="orderdesc">待退款</div>
                    </div>
                </div>
            </div>
                </div>
        </div>
    </div>
           </div>
    <script type="text/JavaScript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/main"></script>
</asp:Content>
