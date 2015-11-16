<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>活动列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/activityList/css/activityDetail.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/activiDetail.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                    <div  class="title">
                           <ul id="optPanel">
                           <li data-flag="#nav01" class="on one">基本信息</li>
                           <li data-flag="#nav02">活动规则</li>
                           <li data-flag="#nav03">活动配置</li>
                           </ul>
                    </div>
                    <form></form>
                    <form id="nav0_1">
                   <div class="panelDiv" id="nav01" data-index="0">
                             <div class="commonSelectWrap">
                                                      <em class="tit">活动名称：</em>
                                                      <label class="searchInput" style="width: 446px;">
                                                          <input data-text="活动名称" data-flag="item_name" name="item_name" type="text"
                                                              value="">
                                                      </label>
                                                  </div>
                              <div class="commonSelectWrap">
                                                      <em class="tit">活动时间：</em>
                                                      <div class="selectBox" style="width: 200px">
                                                            <input type="text" id="startDate"  required="true" class="easyui-datebox"  data-options="width:88,height:32"/>至<input  required="true" type="text" class="easyui-datebox" validType="compareDate[$('#startDate').datebox('getText'),'前面选择的时间必须晚于该时间']" data-options="width:88,height:32"/>
                                                      </div>
                                                  </div>
                      <div class="commonSelectWrap">
                          <em class="tit">活动对象:</em>
                          <label class="searchInput">
                            <input data-text="活动对象" id="Category" data-options="required:true" data-flag="Item_Category_Id" name="Item_Category_Id" type="text" value="">
                          </label>
                      </div>
                      <div class="commonSelectWrap">
                          <em class="tit">活动类型：</em>
                          <label class="searchInput">
                            <input data-text="活动类型" id="ItemCategoryId"    data-flag="ItemCategoryId" name="ItemCategoryId" type="text" value="">
                          </label>
                      </div>
                       <div class="commonSelectWrap">
                           <em class="tit">活动方式：</em>
                           <label class="searchInput">
                             <input data-text="活动方式" id="ItemCategoryId1"    data-flag="ItemCategoryId" name="ItemCategoryId" type="text" value="">
                           </label>
                       </div>

                   </div>
                   </form>
                   <!--商品详情-->
                   <div class="panelDiv" id="nav02" data-index="1">
                          <form id="nav0_2" >
                           <div class="commonSelectWrap">
                                                <em class="tit">参与游戏次数:</em>
                                                <label class="selectBox">
                                                  <input data-text="参与游戏次数"  class="easyui-numberbox" data-options="required:true,width:160,height:32,min:0,precision:0"  type="text" value="">
                                                </label>
                                            </div>
                                            <div class="commonSelectWrap">
                                                <em class="tit">参与游戏所需积分:</em>
                                                <label class="selectBox">
                                                  <input data-text="参与游戏所需积分"  class="easyui-numberbox" data-options="required:true,width:160,height:32,min:0,precision:0"  type="text" value="">
                                                </label>
                                            </div>

                      </form>
                   </div>
                    <!--商品详情End-->
                   <!--销售信息-->
                   <div class="panelDiv" id="nav03" data-index="2">
                      <div  data-state="商品基本信息" id="dataState">
                        <div class="commonSelectWrap">
                          <em class="tit">商品条码：</em>
                          <label class="searchInput">
                            <input data-text="商品条码" data-flag="price" name="barcode" class="easyui-validatebox" data-options="validType:'englishCheckSub'" type="text" value="">
                          </label>
                      </div>
                        <div class="textList" id="textList">
                      <div class="commonSelectWrap">
                          <em class="tit">价格：</em>
                          <label class="searchInput">
                            <input data-text="价格" data-flag="CarNumber" name="CarNumber" type="text" value="">
                          </label>
                      </div>
                      <div class="commonSelectWrap">
                          <em class="tit">原价：</em>
                          <label class="searchInput">
                            <input data-text="原价" data-flag="CarNumber" name="CarNumber" type="text" value="">
                          </label>
                      </div>
                      <div class="commonSelectWrap">
                          <em class="tit">库存：</em>
                          <label class="searchInput">
                            <input data-text="库存" data-flag="CarNumber" name="CarNumber" type="text" value="">
                          </label>
                      </div>
                   </div>
                        <div class="zsy"> </div>
                      </div>

                       <div class="SKU" id="sku">
                       <!--   <div class="skuList">

                           <div class="pro">
                               <div class="commonSelectWrap">
                                      <label class="selectBox">
                                          <input   class="easyui-combobox proList" name="CarNumber"  type="text" value="">
                                      </label>
                                </div>
                              <div class="icon"></div>
                           </div>
                           <div class="proDetailList">
                               <div class="proValue">
                                <div class="btn">白色 <em class="icon"></em></div>
                                <div class="btn">黑色 <em class="icon"></em></div>
                                <div class="btn">白色 <em class="icon"></em></div>
                                <div class="btn">白色 <em class="icon"></em></div>
                                </div>
                                <div class="fontC" data-type="add"> +添加</div>

                           </div>
                          </div>-->
                        <div class="commonBtn addSKU">添加规格</div>
                       </div>

                     <div class="skuTable">  <div id="skuTable" class="easyui-datagrid" ></div>

                        <div id="batch">
                                                                               <div class="commonSelectWrap">
                                                                                   <em class="tit">批量操作：</em>
                                                                                   <label class="searchInput borderNone" id="option">

                                                                                   </label>
                                                                               </div>
                                                                               <div class="commonSelectWrap">
                                                                                   <em class="tit">总库存：</em>
                                                                                   <label class="searchInput borderNone">
                                                                                       <b id="countRepertory">0</b>
                                                                                   </label>
                                                                               </div>


                                                                              </div>
                     </div>



                   </div>
                     <!--销售信息End-->
                  <div class="zsy"></div>
                </div>
               <div class="btnopt commonBtn" id="submitBtn" data-flag="#nav02">下一步</div>
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






  <!--动态生成销售信息的类型-->
  <script id="tpl_commoditySellForm" type="text/html">
      <#var subRoot=Data.ItemPriceTypeList;subRoot=subRoot?subRoot:[];#>
          <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];#>
                  <#if(item.item_price_type_code=="销量"){#>
           <div class="commonSelectWrap load" style="display: none">
                                                                                   <em class="tit"><#=item.item_price_type_name#>:</em>
                                                                                   <label class="searchInput " style="border: none">
                                                                                     <input data-text="<#=item.item_price_type_name#>" data-type="price" data-flagInfo="<#=JSON.stringify(item)#>" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:32,disabled:true" data-flag="price" name="price" type="text" value="">
                                                                                   </label>
                                                              </div>

                  <#}else{#>

                                                          <div class="commonSelectWrap load">
                                                              <em class="tit"><#=item.item_price_type_name#>:</em>
                                                              <label class="searchInput " style="border: none">
                                                              <#if(item.item_price_type_code=="库存"){#>
                                                                <input data-text="<#=item.item_price_type_name#>" data-type="price" data-flagInfo="<#=JSON.stringify(item)#>" class="easyui-numberbox"  data-options="min:0,precision:0,width:160,height:32" data-flag="price" name="price" type="text" value="">
                                                            <#}else{#>
                                                             <input data-text="<#=item.item_price_type_name#>" data-type="price" data-flagInfo="<#=JSON.stringify(item)#>" class="easyui-numberbox"  data-options="min:0,precision:2,width:160,height:32" data-flag="price" name="price" type="text" value="">

                                                            <#}#>
                                                              </label>
                                                           </div>
                                    <#}#>



          <#}#>


  </script>


<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>" ></script>
</asp:Content>
