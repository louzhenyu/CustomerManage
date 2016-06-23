<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商品数据分析</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/superRetailStore/css/goodAnalyze.css?v=0.6"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/goodAnalyze.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                  <div class="listBtn r"> <a class="commonBtn w110 l orderList">查看分销订单</a>  <a href="/module/superRetailStore/QueryList.aspx" target="_blank"  class="commonBtn w110 r">分销商品管理</a></div><!--listBtn-->

   <div class="mainPanel">

             <div class="onePanelDiv borderAll">
             <div class="panelTitle"><p>近30天关键数据</p></div>
             <div class="oneLi">
             <p>商品分享数量 <b data-filed="SharedRTProduct.Day30SharedRTProductCount" data-value="">0</b>个</p>
                <div  class="onePanel">
                    <div class="ricePanel">
                    <div class="rice bg1"></div>
                    <div id="container" class="hCharts" ></div>

                    </div><!--ricePanel-->

                   <div class="bottom">
                         <div class="l">
                             <div class="centerDiv">
                             <p><em class="color0"></em>已分享商品</p>
                              <p class="red"><i data-filed="SharedRTProduct.Day30SharedRTProductCount" data-value="30000">0</i></p>
                              </div>
                         </div>
                          <div class="r">
                           <div class="centerDiv">
                             <p><em class="color0Hover"></em>未分享商品</p>
                             <p class="red"><i data-filed="SharedRTProduct.Day30NoSharedRTProductCount" data-value="3000">0</i></p>
                             </div>
                          </div>
                   </div>
               </div>

             </div>  <!--oneLi-->
             <div class="oneLi">
               <p>商品销售数量<b data-value="" data-filed="SalesRTProduct.count">0</b> 个</p>
                <div class="onePanel">
                    <div class="ricePanel">
                     <div class="rice bg2"></div>
                            <div id="container1"  class="hCharts"  ></div>


                    </div><!--ricePanel-->
                   <div class="bottom">
                         <div class="l">
                          <div class="centerDiv">
                             <p><em class="color1"></em>线上已销售商品</p>
                              <p class="red">￥<i data-filed="SalesRTProduct.Day30ShareSalesRTProductCount" data-separator=true data-value="30000">0</i></p> </div>
                         </div>
                          <div class="r">
                           <div class="centerDiv">
                             <p><em class="color1Hover"></em>线下已销售商品</p>
                             <p class="red">￥<i data-filed="SalesRTProduct.Day30F2FSalesRTProductCount" data-separator=true data-value="3000">0</i></p></div>
                          </div>
                   </div>
                </div>
             </div>  <!--oneLi-->
             <div class="oneLi">
             <p>近7天商品销售转化率 <b><em data-filed="RTProductCRate.Day7RTProductCRate" data-value="">0</em><i class="hidden">%</i></b>
              <span >近28天转化率趋势</span>

              </p>
                <div class="onePanel" style="position: relative;" >
                     <div class="panelCol">
                       <div class="col1" data-filed="RTProductCRate.Last3Day7RTProductCRate" data-value=""></div>
                       <div class="col2" data-filed="RTProductCRate.Last2Day7RTProductCRate" data-value=""></div>
                       <div class="col3" data-filed="RTProductCRate.LastDay7RTProductCRate" data-value=""></div>
                       <div class="col4" data-filed="RTProductCRate.Day7RTProductCRate" data-value=""></div>
</div> <!--panelCol-->
                     <p id="dataDay"></p>
                     <div class="noData" id="RTProductCRate">无数据</div>
                </div> <!--onePanel-->
             </div>  <!--oneLi-->
 </div>  <!--one-->

 <div class="threePanelDiv">
    <div class="tableDiv borderAll">
            <div class="panelTitle "><p class="titleP">分享次数最多的5种商品</p></div>
            <div data-filed="ShareMoreItemsList" ><p class="line200">无数据</p></div>
       </div>
    <div class="tableDiv borderAll">
            <div class="panelTitle"><p class="titleP">销量最高的5种商品</p></div>
             <div data-filed="SalesMoreItemesList" data-show="true"><p class="line200">无数据</p></div>

       </div>
    <div class="tableDiv borderAll ">
            <div class="panelTitle bg"><p class="titleP1">分享次数最少的5种商品</p></div>
             <div data-filed="ShareLessItemsList" ><p class="line200">无数据</p></div>
       </div>
    <div class="tableDiv borderAll" style="margin-right: 0;">
            <div class="panelTitle bg"><p class="titleP1">销量最低的5种商品</p></div>
             <div data-filed="SalesLessItemesList" data-show="true"><p class="line200">无数据</p></div>
       </div>
 </div> <!--three-->

       <div class="zsy"></div>
 </div> <!--mainPanel-->
            </div>  <!--contentArea_vipquery-->
        </div>
       <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">
      				 <img src="images/orderInfo.png" width="442" height="267">
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style=" height:70px;text-align:center;padding:5px 0 0;">
      			   <div class="listBtn">
      				 <a class="easyui-linkbutton commonBtn cancelBtn l" style="display: block"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >关闭</a>
      				<a class="easyui-linkbutton commonBtn saveBtn r" href="/module/chainCloudOrder/querylist.aspx" target="_blank" >去查看订单</a>
                   </div>  <!--listBtn-->
      			</div>
      		</div>

      	</div>
      </div>


</asp:Content>
