<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>连锁云掌柜-O2O云店总览</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/pandect/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js">
         <div class="mainPanel">

            <div class="onePanelDiv borderAll">
            <div class="panelTitle"><p>7天经营数据</p></div>
            <div class="oneLi">
            <p>访客</p>
               <div  class="onePanel">
                   <div class="ricePanel">
                      <div class="rice color0" ></div>
                      <div class="minRice bg01" data-filed="UVPercent" data-value="22"></div>

                      <div class="holdL hold">
                          <div class="pieL color0Hover"></div>
                      </div>
                      <div class="holdR hold">
                      <div class="pieR color0Hover"></div>
                      </div>
                   </div><!--ricePanel-->

                  <div class="bottom">
                        <div class="l">
                            <div class="centerDiv">
                            <p><em class="color0"></em>云店访客</p>
                             <p class="red"><i data-filed="WxUV" data-value="30000">0</i>人</p>
                             </div>
                        </div>
                         <div class="r">
                          <div class="centerDiv">
                            <p><em class="color0Hover"></em>门店访客</p>
                            <p class="red"><i data-filed="OfflineUV" data-value="3000">0</i>人</p>
                            </div>
                         </div>
                  </div>

               </div>
            </div>  <!--oneLi-->
            <div class="oneLi">
              <p>成交订单笔数</p>
               <div class="onePanel">
                   <div class="ricePanel">
                      <div class="rice color1"></div>
                      <div class="minRice bg02" data-filed="PayCountPercent" data-value="0"></div>

                      <div class="holdL hold">
                          <div class="pieL color1Hover"></div>
                      </div>
                      <div class="holdR hold">
                      <div class="pieR color1Hover"></div>
                      </div>
                   </div><!--ricePanel-->
                  <div class="bottom">
                        <div class="l">
                         <div class="centerDiv">
                            <p><em class="color1"></em>云店成交笔数</p>
                             <p class="red"><i data-filed="WxOrderPayCount" data-value="30000">0</i></p> </div>
                        </div>
                         <div class="r">
                          <div class="centerDiv">
                            <p><em class="color1Hover"></em>门店成交笔数</p>
                            <p class="red"><i data-filed="OfflineOrderPayCount" data-value="3000">0</i></p></div>
                         </div>
                  </div>
               </div>
            </div>  <!--oneLi-->
            <div class="oneLi">
            <p>成交金额</p>
               <div class="onePanel">
                   <div class="ricePanel">
                      <div class="rice color2"></div>
                      <div class="minRice bg03" data-filed="PayMoneyPercent" data-value="68"></div>

                      <div class="holdL hold">
                          <div class="pieL color2Hover"></div>
                      </div>
                      <div class="holdR hold">
                      <div class="pieR color2Hover"></div>
                      </div>
                   </div><!--ricePanel-->
                  <div class="bottom">
                        <div class="l">
                         <div class="centerDiv">
                            <p><em class="color2"></em>云店成交金额</p>
                             <p class="red">￥<i data-filed="WxOrderPayMoney" data-separator=true data-value="30000">0</i></p>
                             </div>
                        </div>
                         <div class="r">
                          <div class="centerDiv">
                            <p><em class="color2Hover"></em>门店成交金额</p>
                            <p class="red">￥<i data-filed="OfflineOrderPayMoney" data-separator=true data-value="3000">0</i></p>
                            </div>
                         </div>
                  </div>
               </div>
            </div>  <!--oneLi-->
            <div class="oneLi">
            <p>客单价</p>
               <div class="onePanel" style="border: none">
                   <div class="textPanel ">

                   <p><em class="tit" >云店</em><span class="Rectangle_0"></span></p>
                   <p><em class="tit"> 门店</em><span class="Rectangle_1"></span></p>

                   </div><!--ricePanel-->
                  <div class="bottom">
                        <div class="l">
                         <div class="centerDiv">
                            <p><em class="color3"></em>云店客单价</p>
                             <p class="red">￥<i data-filed="WxOrderAVG"  data-separator=true  data-value="30000">0</i></p>
                             </div>
                        </div>
                         <div class="r">
                          <div class="centerDiv">
                            <p><em class="color3Hover"></em>门店客单价</p>
                            <p class="red">￥<i data-filed="OfflineOrderAVG" data-separator=true data-value="3000">0</i></p>
                            </div>
                         </div>
                  </div>
               </div>
            </div>  <!--oneLi-->
</div>  <!--one-->
     <div class="zsy"></div>
            <div class="twoPanelDiv">
             <div class="leftDiv borderAll">
             <div class="panelTitle"><p>云店转化（近30天数据对比）</p></div>
             <div class="percent">
                     <div class="rice_1"><em style="font-size: 14px;">转化率</em><p style="padding-top: 3px;"><span data-filed="Rate_OrderVipPayCount_UV" data-value="10">0</span><i>%</i></p></div>
                     <div class="rice_2"><em>转化率</em><p><span data-filed="Rate_OrderVipCount_UV" data-value="30">0</span><i>%</i></p></div>
                     <div class="rice_3"><em>转化率</em><p><span data-filed="Rate_OrderVipPayCount_OrderVipCount" data-value="50">0</span><i>%</i></p></div>
</div> <!--percent-->
               <div class="panelData">
                <div class="line">
                                 <div class="data w180"><em>访客人数</em><p>
                                 <span data-filed="WxUV" data-value="99999">99999</span>
                                 <i class="up" data-filed="Rate_UV_Last" data-value="100">0%</i>
                                 </p></div>
                                  <span class="data"><em>页面浏览</em><p>
                                  <span data-filed="WxPV" data-value="99999">99999</span>
                                  <i class="down" data-filed="Rate_PV_Last" data-value="100">0%</i>
                                  </p></span>
               </div><!--line-->
                <div class="line">
                                 <div class="data w180"><em>下单人数</em>
                                 <p>
                                 <span data-filed="WxOrderVipCount" data-value="99999">99999</span>
                                 <i class="down" data-filed="Rate_OrderVipCount_Last" data-value="99999">0%</i>

                                 </p>
                                 </div>
                                    <div class="data"><em>下单笔数</em>  <p>
                                       <span data-filed="WxOrderCount" data-value="99999">0</span>
                                      <i class="down" data-filed="Rate_OrderCount_Last" data-value="99999">0%</i>

                                    </p></div>
                                       <div class="data w200"><em>下单金额</em><p><b>￥</b>

                                          <span data-filed="WxOrderMoney" data-separator=true data-value="99999">0</span>
                                          <i class="down" data-filed="Rate_OrderMoney_Last" data-value="99999">0%</i>

                                       </p></div>
               </div><!--line-->
                <div class="line" style="margin-bottom: 0">
                                 <div class="data w180"><em>成交人数</em><p>
                                  <span data-filed="WxOrderVipPayCount" data-value="99999">99999</span>
                                  <i class="down" data-filed="Rate_OrderVipPayCount_Last" data-value="99999">0%</i>

                                 </p></div>   <!--data-->
                                  <div class="data"><em>成交笔数</em><p>
                                  <span data-filed="WxOrderPayCount" data-value="99999">99999</span>
                                  <i class="down" data-filed="Rate_OrderPayCount_Last" data-value="99999">0%</i>

                                  </p></div>    <!--data-->
                                 <div class="data w200"><em>成交金额</em><p><b>￥</b>
                                  <span data-filed="WxOrderPayMoney" data-separator=true data-value="99999">0</span>
                                  <i class="down" data-filed="Rate_OrderPayMoney_Last" data-value="99999">0%</i>
                                 </div>    <!--data-->
                                    <div class="data w130"><em>客单价</em><p><b>￥</b>
                                  <span data-filed="WxOrderAVG" data-separator=true data-value="99999">0</span>
                                  <i class="down" data-filed="Rate_OrderAVG_Last" data-value="99999">0%</i>


                                    </p></div>   <!--data-->
               </div><!--line-->

</div>

             </div><!--leftDiv-->
              <div class="rightDiv borderAll">
                     <div class="panelTitle"><p class="titleP2">您可以</p></div>

                     <div class="btnList">
                     <p data-type="tsfks">提升访客数</p>
                     <p data-type="tskdj">提升客单价</p>
                     <p data-type="tgzhl">提高转化率</p>
</div>   <!--btnList-->
</div>  <!--rightDiv-->
</div> <!--two-->
<div class="threePanelDiv">
   <div class="tableDiv borderAll">
           <div class="panelTitle "><p class="titleP">浏览次数最多的10种商品</p></div>
           <div data-filed="Top10Views"></div>
      </div>
   <div class="tableDiv borderAll">
           <div class="panelTitle"><p class="titleP">销量最高的10种商品</p></div>
            <div data-filed="Top10Sales"></div>

      </div>
   <div class="tableDiv borderAll ">
           <div class="panelTitle bg"><p class="titleP1">浏览次数最少的10种商品</p></div>
            <div data-filed="Least10Views"></div>
      </div>
   <div class="tableDiv borderAll">
           <div class="panelTitle bg"><p class="titleP1">销量最低的10种商品</p></div>
            <div data-filed="Least10Sales"></div>
      </div>
                    <div class="rightDiv borderAll">
                           <div class="panelTitle"><p class="titleP2">您可以</p></div>

                           <div class="btnList">
                           <p  data-type="dzbk">打造爆款</p>
                           <p  data-type="tgzxp">推广滞销品</p>
      </div>   <!--btnList-->
      </div>  <!--rightDiv-->
</div> <!--three-->

         <div class="zsy"></div>
</div> <!--mainPanel-->


        </div>
      <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'">
      				指定的模板添加内容
      			</div>

      		</div>

      	</div>
      </div>
       <!-- 取消订单-->
       <script id="tpl_tsfks" type="text/html">
        <div class="imgPanel">
           <img src ="image/tsfks.png" alt="提升访客数" usemap ="#planetmap" />

           <map name="planetmap">
               <area shape="rect" coords="106,308,216,345" href="/Module/SetOffManage/Action.aspx"  target="_blank" title="发布集客行动" />
               <area shape="rect" coords="458,308,568,345" href="/Module/WMaterialText/WMaterialText.aspx"  target="_blank" title="设置微信图文">
               <area shape="rect" coords="106,660,216,696" href="/module/couponManage/querylist.aspx"  target="_blank" title="新增优惠券"/>
               <area shape="rect" coords="458,660,568,696" href="/module/CreativityWarehouse/CreativeWarehouseView/QueryList.aspx"  target="_blank" title="发起营销活动"/>
            </map>
            </div>
       </script>
        <!--收款-->
         <script id="tpl_tskdj" type="text/html">
          <div class="imgPanel">
            <img src ="image/tskdj.png" alt="提升客单价" usemap ="#planetmap" />
            <map name="planetmap">
                <area shape="rect" coords="232,300,342,336" href="/module/couponManage/querylist.aspx"  target="_blank" title="新增优惠券"/>
                <area shape="rect" coords="232,625,342,661" href="/module/commodity/release.aspx"  target="_blank" title="添加商品"/>
            </map>
            </div>
      </script>
          <script id="tpl_tgzhl" type="text/html">
           <div class="imgPanel">
          <img src ="image/tgzhl.png" alt="提高转化率" usemap ="#planetmap" />

          <map name="planetmap">
              <area shape="rect" coords="232,300,342,336" href="/module/GroupBuy/GroupList.aspx?pageType=2&pageName=抢购"  target="_blank" title="新建团购"/>
              <area shape="rect" coords="232,625,342,661" href="/module/AppConfig/querylist.aspx"  target="_blank" title="优化商城模板"/>
          </map>
          </div>
          </script>
          <script id="tpl_dzbk" type="text/html">
           <div class="imgPanel">
          <img src ="image/dzbk.png" alt="打造爆款" usemap ="#planetmap" />

          <map name="planetmap">
              <area shape="rect" coords="232,300,342,336" href="/module/CreativityWarehouse/CreativeWarehouseView/QueryList.aspx"  target="_blank" title="发起营销活动"/>
              <area shape="rect" coords="232,625,342,661" href="/module/AppConfig/querylist.aspx"  target="_blank" title="添加商城推广"/>
          </map>
           </div>
          </script>
          <script id="tpl_tgzxp" type="text/html">
           <div class="imgPanel">
           <img src ="image/tgzxp.png" alt="推广滞销" usemap ="#planetmap" />

                    <map name="planetmap">
                        <area shape="rect" coords="232,300,342,336" href="/module/CreativityWarehouse/CreativeWarehouseView/QueryList.aspx"  target="_blank" title="发起营销活动"/>
                        <area shape="rect" coords="232,625,342,661" href="/module/AppConfig/querylist.aspx"  target="_blank" title="添加商城推广"/>
                    </map>
                    </div>
          </script>
</asp:Content>
