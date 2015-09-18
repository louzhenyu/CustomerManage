<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>新增优惠券</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/couponManage/css/addCoupon.css?v=0.4"%>" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/addCoupon.js?ver=0.3" >
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
              <div class="listBtn" data-coupontype="0" data-show="panltext">现金券<div class="on"></div></div>
              <div class="listBtn" data-coupontype="1" data-hide="panltext">兑换券<div class="on"></div></div>
            </div>
            <form></form>
            <form id="addCoupon">
               <div class="tableWrap">
                <div class="panlDiv">
                <div class="commonSelectWrap">
                    <em class="tit">优惠券名称：</em>
                        <label class="searchInput" style="width: 487px;">
                             <input data-text="优惠券名称" data-flag="CouponTypeName" class="easyui-validatebox textbox" data-options="required:true,validType:'length[3,100]'" name="CouponTypeName" type="text" value="">
                        </label>
                </div>
                <div class="commonSelectWrap" style="height: 80px;">
                    <em class="tit">使用规则：</em>
                    <label class="searchInput " style="width: 487px; height: 80px;">
                        <textarea name="CouponTypeDesc" class="easyui-validatebox" data-options="required:true,validType:'length[3,100]'"></textarea>
                    </label>
               </div>
                <div class="commonSelectWrap panltext">
                    <em class="tit">优惠券面值：</em>
                        <label class="searchInput bordernone">
                             <input id="ParValue" data-text="优惠券面值" class="easyui-numberbox" data-options="min:0,precision:2,required:true,min:0,width:160,height:32" value="0" data-flag="ParValue" name="ParValue" type="text" >
                        </label>
                </div>
                <div class="commonSelectWrap">
                    <em class="tit">优惠券数量：</em>
                        <label class="searchInput bordernone">
                             <input data-text="优惠券数量" class="easyui-numberbox" data-options="min:1,precision:0,required:true,width:160,height:32" data-flag="IssuedQty" name="IssuedQty" type="text" value="0">
                        </label>
                </div>
                </div><!--panlDivEnd-->
                <div class="panlDiv">
                <div class="commonSelectWrap">
                    <em class="tit">适用范围：</em>
                        <label class="searchInput bordernone" style="width: 238px;">
                              <div class="radio on" data-name="r1" data-UsableRange="1"><em></em><span>仅限在线商城使用</span></div>
                              <div class="radio" data-name="r1"  data-UsableRange="2"><em></em><span>仅限到店使用</span></div>
                        </label>
                </div>


                </div> <!--panlDivEnd-->
                <div class="panlDiv">
                <div class="commonSelectWrap" style="height:86px">
                    <em class="tit">有效期：</em>
                        <div class="searchInput bordernone whauto">
                          <div class="line"> <div class="radio" data-name="r2" data-validity="time"><em></em></div>
                               <div class="linetext">固定时间：<input id="startDate"  name="BeginTime" class="easyui-datebox"  data-options="width:160,height:32" /><span>至</span> <input id="expireDate" name="EndTime" class="easyui-datebox" data-options="width:160,height:32" validType="compareDate[$('#startDate').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/></div>
                             </div>
                           <div class="line">
                            <div class="radio on" data-name="r2" data-validity="day"><em></em></div>
                                 <div class="linetext">领券当日起顺延X天：<input name="ServiceLife" class="easyui-numberbox"  data-options="min:0,precision:0,width:160,height:32" /><span>天</span></div>
                            </div>
                        </div>
                </div>
                </div> <!--panlDivEnd-->
                <div class="panlDiv panltext">
                <div class="commonSelectWrap">
                    <div class="tit"><div class="checkBox" data-flag="ConditionValue" ><em></em><span>使用限制:</span></div></div>

                </div>
               <div class="commonSelectWrap">
                         <div class="searchInput bordernone whauto pl_45">
                             <div class="linetext">购买商品满<input id="ConditionValue" name="ConditionValue" class="easyui-numberbox"  data-options="min:0,precision:0,width:160,height:32" value="0" /><span>元 可使用</span></div>
                         </div>
                </div>

                </div> <!--panlDivEnd-->
                <div class="panlDiv bordernone">

                   <div class="commonSelectWrap">
                           <em class="tit">适用类型:</em>
                           <div class="searchInput bordernone whauto">
                              <select id="applicationType" class="easyui-combobox" data-options="width:160,height:32"  style="width:160px;"></select>

                             <div class="checkBox r" data-flag="SuitableForStore" data-toggleClass="toggClass" ><em></em><span>适用所有</span></div>
                           </div>
                   </div>
                   <div class="listItem toggClass" id="listItembtn">
                 <!--    <div  id="unitList" class="btnlist">
                       <div class="unitBtn"> 黄陂南路店 <em class="icon" ></em></div>
                       <div class="unitBtn"> 黄埔店 <em class="icon" ></em></div>

                      </div>-->

                      <div class="addunitBtn " id="addUnit">+添加</div>
                        <div style="display: none">
                                              <div id="Tooltip">
                                                      <div class="treeNode" ></div>

                                                    <div class="btnList">
                                                      <div class="commonBtn opts l"  data-flag="sales"> 确定 </div>
                                                       <div class="commonBtn opts r"  data-flag="cannel">取消</div>
                                                     </div>
                                              </div>

                        </div>

                   </div>

                   <div class=" listTable toggClass">
                     <div class="easyui-datagrid" style="width:640px;height:250px" data-options="title:'已选门店',fitColumns:true,singleSelect:true"></div>
                   </div>


                   <div class="zsy" > </div>

                </div><!--panlDivEnd-->
            </div>
            </form>
            <div class="commonBtn submitBtn">保存</div>
        </div>
    </div>
    <div style="display: none">
        <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="padding: 10px;">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                    text-align: center; padding: 5px 0 0;">
                    <a class="easyui-linkbutton commonBtn saveBtn">确定</a> <a class="easyui-linkbutton commonBtn cancelBtn"
                        href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                </div>
            </div>
        </div>
    </div>
    <!--收款-->
    <script id="tpl_AddUnitList" type="text/html">
            <form id="payOrder">


            <div class="optionclass">
               <div class="commonSelectWrap">
               <em class="tit" style="width: 30px;"></em>
                                <div class="bordernone searchInput"  style="width: 222px">
                                 <input  id="unitTree" class="easyui-combotree" data-options=" width:130,height:32,"/>
                               </div>
                </div>
               <div class="commonSelectWrap">
                                <div class=" searchInput" >
                                 <input  name="RetailTraderName" placeholder="分销商名称"/>
                               </div>
                </div>
                <div class="commonBtn searchBtn"> 搜索</div>
            </div>
            <div class="zsy"> </div>
            <div id="searchGrid" class="searchGrid"></div>
       </form>
    </script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/couponManage/js/main.js"%>"></script>
</asp:Content>
