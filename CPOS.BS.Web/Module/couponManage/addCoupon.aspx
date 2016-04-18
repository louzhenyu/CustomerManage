<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>新增优惠券</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/couponManage/css/addCoupon.css?v=0.5"%>" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/addCoupon.js?ver=0.5" >
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" style="width: 100%;">
              <div class="listBtn" data-coupontype="0" data-show="panltext">代金券<div class="on"></div></div>
              <div class="listBtn" data-coupontype="1" data-hide="panltext">兑换券<div class="on"></div></div>
            </div>
            <form></form>
            <form id="addCoupon">
               <div class="tableWrap">

                <div class="panelDiv">
                 <p class="title">基础信息设置</p>
                <div class="commonSelectWrap">
                    <em class="tit w120">券名称：</em>
                        <label class="searchInput">
                             <input data-text="优惠券名称" data-flag="CouponTypeName" class="easyui-validatebox textbox" data-options="required:true,validType:'length[3,100]'" name="CouponTypeName" type="text" value="">
                        </label>
                </div>
                <div class="commonSelectWrap" style="height: 80px;">
                    <em class="tit w120">使用说明：</em>
                    <label class="searchInput " style="width: 487px; height: 80px;">
                        <textarea name="CouponTypeDesc" class="easyui-validatebox" data-options="required:true,validType:'length[6,200]'"></textarea>
                    </label>
                </div>

                <div class="commonSelectWrap">
                    <em class="tit w120">发放数量：</em>
                        <label class="searchInput bordernone">
                             <input data-text="优惠券数量" class="easyui-numberbox" data-options="min:1,precision:0,max:10000,required:true,width:160,height:32" data-flag="IssuedQty" name="IssuedQty" type="text" value="0">
                        </label>
                </div>
                 <div class="commonSelectWrap panltext">
                                    <em class="tit w120">面值设置：</em>
                                        <label class="searchInput bordernone">
                                             <input id="ParValue" data-text="优惠券面值" class="easyui-numberbox" data-options="min:0,precision:2,required:true,min:0,width:160,height:32" value="0" data-flag="ParValue" name="ParValue" type="text" >
                                         元
                                        </label>

                                </div>
                <div class="commonSelectWrap">
                    <em class="tit w120">使用范围：</em>
                        <label class="searchInput bordernone whauto">
                              <div class="radio on" data-name="r1" data-UsableRange="1"><em></em><span>仅云店使用</span></div>
                              <div class="radio" data-name="r1"  data-UsableRange="2"><em></em><span>仅门店使用</span></div>
                              <div class="radio" data-name="r1"  data-UsableRange="3"><em></em><span>云店门店通用</span></div>
                        </label>
                </div>

                <div class="commonSelectWrap" style="height:86px">
                    <em class="tit w120">使用期限：</em>
                        <div class="searchInput bordernone whauto">
                          <div class="line"><div class="radio" data-name="r2" data-validity="time"><em></em></div>
                               <div class="linetext">固定时间：<input id="startDate"  name="BeginTime" class="easyui-datebox"  data-options="width:120,height:32" /><span>至</span> <input id="expireDate" name="EndTime" class="easyui-datebox" data-options="width:120,height:32" validType="compareEqualityDate[$('#startDate').datebox('getText'),'当前选择的时间不能大于前面选择的时间']"/></div>
                             </div>
                           <div class="line">
                            <div class="radio on" data-name="r2" data-validity="day"><em></em></div>
                                 <div class="linetext">领券后有效天数：<input name="ServiceLife" class="easyui-numberbox"  data-options="min:0,precision:0,width:160,height:32" />
                                 	<span>天</span>
                                    <span class="tipBox">1天表示领券当天有效</span>
                                 </div>
                            </div>
                        </div>
                </div>
                </div> <!--panelDivEnd-->
                <div class="panelDiv ">
                 <p class="title">使用条件设置</p>
                <div class="commonSelectWrap panltext">
                    <em class="tit w120">使用限制：</em>

                     <div class="searchInput bordernone whauto">
                            <div class="checkBox l" data-flag="ConditionValue" ><em></em></div> <div class="linetext">购买商品满<input id="ConditionValue" name="ConditionValue" class="easyui-numberbox"  data-options="min:0,precision:0,width:160,height:32" value="0" /><span>元 可使用</span></div>
                         </div>

                </div>
                          <!-- <div class="commonSelectWrap panltext" >
                                 <em class="tit w120">适用商品：</em>
                                   <div class="searchInput bordernone whauto">
                                         <div class="radio on" data-name="product" data-valuetype="all"><em></em><span>所有商品适用</span></div>
                                         <div class="radio" data-name="product"  data-valuetype="portion"><em></em><span>部分商品适用</span></div>
                                          <div class="commonBtn w100 radioBtn" data-name="product">查看和修改</div>
                                   </div>
                           </div>-->
                             <div class="listTable toggClass" data-name="product" >
                                               <p >已选择门店</p>
                                                <div class="productGrid" ></div>
                                              </div>
                             <div class="commonSelectWrap">
                                   <em class="tit w120">适用门店：</em>
                                     <div class="searchInput bordernone whauto">
                                           <div class="radio on" data-name="unit" data-valuetype="all"><em></em><span>所有门店适用</span></div>
                                           <div class="radio" data-name="unit"  data-valuetype="portion"><em></em><span>部分门店适用</span></div>
                                            <div class="commonBtn w100 radioBtn r" data-name="unit">查看和修改</div>
                                     </div>
                             </div>

					
                   <div class="listTable toggClass" data-name="unit" >
                    <p >已选择门店</p>
                     <div class="datagrid" ></div>
                   </div>


                   <div class="zsy"></div>

                </div><!--panelDivEnd-->
            </div>
            </form>
            <div class="commonBtn submitBtn saveBtn">保存</div>
        </div>
    </div>
    <div style="display:none">
        <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="padding: 10px;">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;text-align: center; padding: 5px 0 0;">
                    <a class="easyui-linkbutton commonBtn saveBtn">确定</a> <a class="easyui-linkbutton commonBtn cancelBtn" href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                </div>
            </div>
        </div>
    </div>

      <script id="tpl_AddUnitList" type="text/html">
                  <form id="payOrder">


                  <div class="optionclass">
                     <div class="commonSelectWrap">
                     <em class="tit w120" style="width: 30px;"></em>
      					<div class="bordernone searchInput"  style="width: 222px">
      					 <input  id="unitTree" class="easyui-combotree" data-options=" width:130,height:32"/>
      				   </div>
                      </div>
                     <div class="commonSelectWrap">
      				   <div class=" searchInput">
      					 <input  name="RetailTraderName" placeholder="分销商名称"/>
      				   </div>
                      </div>
                      <div class="commonBtn searchBtn"> 搜索</div>
                  </div>
                  <div class="zsy"> </div>
                  <div id="searchGrid" class="searchGrid"></div>
             </form>
          </script>
      <script id="tpl_setUnitList" type="text/html">
        <div class="optionPanel">
         <div class="contentDiv" style="width: 200px;">
         <p class="explain">选择门店上级组织</p>
         <div class="content">
         <div id="unitParentTree"></div>
</div><!--content-->
</div> <!--contentDiv-->
          <div class="contentDiv center">
            <div class="explain"><em>所属组织</em><span id="unitName"></span>
            <div class="commonBtn searchBtn"> 搜索</div>
                                   <div class="commonSelectWrap r">
                    				   <div class=" searchInput">
                    					 <input  name="unit_name" id="unit_name" type="text" value="" placeholder="请输入店名">
                    				   </div>
                                    </div>


             </div>
                      <div class="content"  >
                       <div id="unitGrid"></div>
             </div><!--content-->
</div> <!--contentDiv-->
           <div class="contentDiv" style="width: 330px;">
                      <div class="explain"><em>已选择门店</em> <a class="optBtn">批量删除</a></div>
                      <div class="content">
                      <div id="unitTreeSelect"></div>
             </div><!--content-->
</div> <!--contentDiv-->
</div>



</script>
      <script id="tpl_setProduct" type="text/html">
      <div class="lineText">
                   <div class="commonSelectWrap">
                       <em class="tit w120">选择查看类型:</em>
                           <div class="searchInput bordernone">
                            <input  class="easyui-combobox" id="selectType" data-options="width:160,height:32,valueField: 'label',
                         textField: 'value',
                         data: [{
                                    label: '1',
                                    value: '商品品类',
                                    selected:true
                                 },{
                                    label: '2',
                                    value: '商品分组类'
                                 }]"  name="BatId" type="text" value="0"/>
                           </div>
                   </div>
           </div>
           </div>
        <div class="optionPanel">
             <div class="contentDiv" style="width: 200px;">
             <p class="explain">选择分组</p>
             <div class="content">
             <div id="unitParentTree"></div>
    </div><!--content-->
    </div> <!--contentDiv-->
              <div class="contentDiv center">
                <div class="explain"><em>所属分组</em><span id="unitName"></span>
                <div class="commonBtn searchBtn"> 搜索</div>
                                       <div class="commonSelectWrap r">
                        				   <div class=" searchInput">
                        					 <input  name="ItemName" id="ItemName" type="text" value="" placeholder="请输入商品名称">
                        				   </div>
                                        </div>


                 </div>
                          <div class="content"  >
                           <div id="unitGrid"></div>
                 </div><!--content-->
    </div> <!--contentDiv-->
               <div class="contentDiv" style="width: 330px;">
                          <div class="explain"><em>已选择门店</em> <a class="optBtn">批量删除</a></div>
                          <div class="content">
                          <div id="unitTreeSelect"></div>
                 </div><!--content-->
    </div> <!--contentDiv-->
    </div>
</script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/couponManage/js/main.js"%>"></script>
</asp:Content>
