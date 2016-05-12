<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商品发布</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/commodity/css/release.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/static/css/kkpager.css"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/release.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                    <div  class="title">
                           <ul id="optPanel">
                           <li data-flag="#nav01" class="on one">基础信息</li>
                           <li data-flag="#nav02">商品详情</li>
                           <li data-flag="#nav03">销售信息</li>
                           </ul>
                    </div>
                    <form></form>
                    <form id="nav0_1">
                   <div class="panelDiv" id="nav01" data-index="0">
                        <div class="item"  >
                          <div class="commonSelectWrap">
                              <em class="tit">商品类型：</em>
                              <div class="selectBox" style="width: 302px;">
                              <div class="radio line14 on" data-name="ifservice" data-value="0"><em></em><span>实物商品<br/>（物流发货）</span></div>
                              <div class="radio line14 " data-name="ifservice" data-value="1" ><em></em><span>虚拟商品<br/> （无需物流）</span> </div>
                              </div>
                          </div>
                         </div>
                         
                         
                         <div class="item" id="bulkBox">
                          <div class="commonSelectWrap">
                              <em class="tit"></em>
                              <div class="selectBox" style="width:302px;line-height:16px;">
                              <div class="checkBox line14" data-name="isBulk" data-value="1"><em></em><span>散装商品<br/>（称重结算）</span></div>
                              </div>
                          </div>
                         </div>
                      
                      
                        <div class="item" data-flag="ifservice">
                            <div class="commonSelectWrap">
                                                 <em class="tit">选择种类：</em>
                                                 <label class="selectBox">
                                                   <input data-text="选择种类" id="itemType" data-flag="Item_Name" class="easyui-combobox" data-options="required:true,width:200,height:30" name="VirtualItemTypeId" type="text" value="">
                                                 </label>
                                             </div>
                            <div class="commonSelectWrap">
                                                 <em class="tit">关联：</em>
                                                 <label class="selectBox">
                                                   <input data-text="商品名称" id="ObjecetType" data-flag="Item_Name"  class="easyui-combobox" data-options="required:true,width:200,height:30" name="ObjecetTypeId" type="text" value="请选择">
                                                 </label>
                                             </div>
                     </div>
                    <div class="left">

                      <div class="commonSelectWrap">
                          <em class="tit">商品名称：</em>
                          <label class="searchInput" >
                            <input data-text="商品名称" data-flag="Item_Name" class="easyui-validatebox" data-options="required:true" name="Item_Name" type="text" value="">
                          </label>
                      </div>
                      <div class="commonSelectWrap">
                          <em class="tit">商品编码：</em>
                          <label class="searchInput">
                            <input data-text="商品编码" data-flag="Item_Code" class="easyui-validatebox" data-options="required:true,validType:'stringCheck'" name="Item_Code" type="text" value="">
                          </label>
                      </div>
                      <div class="commonSelectWrap">
                          <em class="tit">商品品类：</em>
                          <label class="selectBox">
                            <input data-text="商品品类" id="Category" data-options="required:true" data-flag="Item_Category_Id" name="Item_Category_Id" type="text" value="">
                          </label>
                      </div>
                      <div class="commonSelectWrap">
                          <em class="tit">商品分组：</em>
                          <label class="selectBox">
                            <input data-text="商品分组" id="ItemCategoryId"    data-flag="ItemCategoryId" name="ItemCategoryId" type="text" value="">
                          </label>
                      </div>
                    </div>

                    <div class="right">
                      <div class="commonSelectWrap">
                          <em class="tit">商品图片：</em>
                         <div class="handleLayer" id="editLayer">
                             <div class="jsAreaItem">
                              <div class="wrapPic">
                                 <div class="imglist"></div>
                                 <span class="uploadBtn"><input class="uploadImgBtn" type="file" /></span>
                                 <div class="imgPanl"><img src="" style="display: none" > <div class="btnPanel"><div class="bg"></div><p><a data-flag="default" class="setDefault commonBtn">默认</a> <a data-flag="del" class="setDel commonBtn">删除</a></p> </div></div>
                               </div>
                               <div  class="txt">图片尺寸640*640</div>
                          </div>
                         </div>
                      </div>

                    </div>
                   </div>
                   </form>
                   <!--商品详情-->
                   <div class="panelDiv" id="nav02" data-index="1">
                   <div class="textList">
                       <textarea name="content"  class="info" style="width: 100%;"></textarea>
                   </div>
                   <div class="textList">
                      <div class="commonSelectWrap">
                          <em class="tit">库存：</em>
                          <label class="searchInput">
                            <input data-text="库存" data-flag="CarNumber" name="CarNumber" type="text" value="">
                          </label>
                      </div>
                   </div>

                   </div>
                    <!--商品详情End-->
                   <!--销售信息-->
                   <div class="panelDiv" id="nav03" data-index="2">
                   <form></form>
                      <form id="SKUForm">
                      <div  data-state="商品基本信息" id="dataState">
                        <div class="commonSelectWrap">
                          <em class="tit">商品条码：</em>
                          <label class="searchInput">
                            <input data-text="商品条码" data-flag="price" name="barcode" class="easyui-validatebox" data-options="validType:['englishCheckSub','length[4,20]']" type="text" value="">
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


                    </form>
                   </div>
                     <!--销售信息End-->
                  <div class="zsy"></div>
                </div>
               <div class="btnopt commonBtn nextStepBtn" id="submitBtn" data-flag="#nav02">下一步</div>
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
       <script id="tpl_OrderCancel" type="text/html">
            <form id="payOrder">
           <div class="commonSelectWrap">
                 <em class="tit">备注：</em>
                <div class="searchInput">
                   <input type="text" name="Remark" />
               </div>
           </div>

           <p class="winfont">你确认取消此笔订单吗？</p>
           </form>
       </script>
      <script id="tpl_uploadimg" type="text/html">
              <div class="jsAreaItem">
                              <div class="wrapPic">
                                 <span class="uploadBtn"><input class="uploadImgBtn input" type="file" /></span>
                                 <div><img src=""></div>

                               </div>

                       </div>


      </script>
      <script id="tpl_AddPro"  type="text/html">
                        <div class="skuList">

                           <div class="pro">
                               <div class="commonSelectWrap">
                               <em class="tit">商品规格：</em>
                                      <label class="selectBox">
                                          <input   class="easyui-combobox proList" name="CarNumber"  type="text" value="">
                                      </label>
                                </div>
                              <div class="icon"></div>
                           </div>
                           <div class="proDetailList">
                              <div class="proValue">
                              </div>
                               <div class="fontC" data-type="add"> +添加</div>
                           </div>
                          </div>


      </script>
      <script id="tpl_AddProDetail" type="text/html">
  <div class="commonSelectWrap">
      <div class="selectBox">
         <input   id="proDetail"  type="text" value="" />
      </div>
     <div class="commonBtn proDetailSave" > 确定</div>
     <div class="commonBtn proDetailCancel" > 取消</div>
  </div>
      </script>
      <script id="tpl_AddBtn" type="text/html">
          <div class="btn" data-id="<#=id#>" data-name="<#=name#>"><#=name#> <em class="icon"></em></div>
      </script>
 <!--动态生成 商品详情字段-->
 <script id="tpl_commodityForm" type="text/html">
     <#var subRoot=Children;subRoot=subRoot?subRoot:[];#>
         <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];#>
                 <#if(item.Prop_Input_Flag=="text"){#>
                 <div class="textList">
                                       <div class="commonSelectWrap" style="display: none;">
                                           <em class="tit"><#=item.Prop_Name#>:</em>
                                           <label class="searchInput">
                                             <input data-text="<#=item.Prop_Name#>" data-type="text"  data-flagInfo="<#=JSON.stringify(item)#>" data-flag="details" type="text"  readonly="readonly"  value="0">
                                           </label>
                      </div>
                 </div>
                 <#}#>

                 <#if(item.Prop_Input_Flag=="htmltextarea"){ #>
                   <div class="textList">
                       <textarea data-text="<#=item.Prop_Name#>" class="info" data-type="htmltextarea"  data-flagInfo="<#=JSON.stringify(item)#>" data-flag="details" style="width: 100%;"></textarea>
                   </div>
                 <#}#>


         <#}#>


 </script>


  <!--动态生成销售信息的类型-->
  <script id="tpl_commoditySellForm" type="text/html">
      <#var subRoot=Data.ItemPriceTypeList;subRoot=subRoot?subRoot:[];#>
          <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];#>
                  <#if(item.item_price_type_code=="销量"){#>
           <div class="commonSelectWrap load" style="display: none">
                                                                                   <em class="tit"><#=item.item_price_type_name#>:</em>
                                                                                   <label class="searchInput " style="border: none">
                                                                                     <input data-text="<#=item.item_price_type_name#>" data-type="price" data-flagInfo="<#=JSON.stringify(item)#>" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30,disabled:true" data-flag="price" name="price" type="text" value="0">
                                                                                   </label>
                                                              </div>

                  <#}else{#>

                                                          <div class="commonSelectWrap load">
                                                              <em class="tit"><#=item.item_price_type_name#>:</em>
                                                              <label class="searchInput " style="border: none">
                                                              <#if(item.item_price_type_code=="库存"){#>
                                                                <input data-text="<#=item.item_price_type_name#>" data-type="price" data-flagInfo="<#=JSON.stringify(item)#>" class="easyui-numberbox"  data-options="required:true,min:0,precision:0,width:200,height:30" data-flag="price" name="price" type="text" value="">
                                                            <#}else{#>
                                                             <input data-text="<#=item.item_price_type_name#>" data-type="price" data-flagInfo="<#=JSON.stringify(item)#>" class="easyui-numberbox"  data-options="required:true,min:0,precision:2,width:200,height:30" data-flag="price" name="price" type="text" value="">

                                                            <#}#>
                                                              </label>
                                                           </div>
                                    <#}#>



          <#}#>


  </script>

 <script id="tpl_AddBatch" type="text/html">
 <div class="mainpanl" title="<#=name#>" data-item="<#=JSON.stringify(obj)#>">
    <div class="searchInput">
              <input type="text" value="" placeholder="请输入<#=name#>"/>
           </div>
           <div class="commonBtn" data-type="save" >确定</div>
           <div class="commonBtn" data-type="cancel">取消</div>
 </div>


       </script>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>" ></script>
</asp:Content>
