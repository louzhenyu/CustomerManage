<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>退货详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/returnManage/css/detail.css?v=0.4"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/salesReturnDetail.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                     <div class="optionBtn " style="display: none"  data-opttype="staus">
                     <div class="commonBtn icon" data-status="1"  data-showstaus="1,3" >审核通过</div>
                     <div class="commonBtn icon close" data-status="2"  data-showstaus="1,3" >审核不通过</div>
                      <div class="commonBtn icon" data-status="3" data-showstaus="4,5">确认收货</div>
                      <div class="commonBtn icon close" data-status="4" data-showstaus="4,5">拒绝收货</div>
                     <!-- <div class="commonBtn" data-status="2" data-showstaus="1,3,4,5"  >取消申请</div>-->




               </div>
                </div>

            </div>
             <div class="tablewrap">
             <form></form>
             <form id="salesReturnInfo">
                <div class="panlDiv">
                          <div class="panlText">
                                   <div class="commonSelectWrap">
                                      <em class="tit">退货单号:</em>
                                      <div class="searchInput">
                                       <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="SalesReturnNo"/>
                                      </div>
                                   </div>
                                   <div class="commonSelectWrap">
                                      <em class="tit">退货状态:</em>
                                      <div class="searchInput rowStatusStyle" >
                                       <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="StatusName"/>
                                      </div>
                                   </div>



                              </div><!--clearfix END-->
                </div><!--panlDiv   END-->
                <div class="panlDiv">
                 <div class="title">联系人信息
                                  <div class="updateBtn" data-opttype="ServicesType">
                                     <div class="commonBtn" data-flag="update" data-type="gropupdate">修改</div>
                                     <div class="commonBtn" data-flag="save"   data-type="groupsubmit">保存</div>
                                     <div class="commonBtn" data-flag="cancel" data-type="groupsubmit">取消</div>
                                  </div>

                 </div>
                 <div class="panlText">
                     <div class="panlL">
                      <div class="commonSelectWrap">
                         <em class="tit">原订单编号:</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="OrderNo"/>
                         </div>
                      </div>
                     
                      <div class="commonSelectWrap">
                         <em class="tit">联系人:</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="Contacts"/>
                         </div>
                      </div>
                      <div class="commonSelectWrap">
                         <em class="tit">手机号:</em>
                         <div class="searchInput">
                          <input type="text"    readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="Phone"/>
                         </div>
                      </div>
                      <div class="commonSelectWrap">
                         <em class="tit">地址:</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="Address"/>
                         </div>
                      </div>
                      <div class="commonSelectWrap">
                         <em class="tit">配送方式:</em>
                         <div class="searchInput rowStatusStyle">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="DeliveryTypeName"/>
                         </div>
                      </div>
                      <div class="commonSelectWrap" data-type="gropupdate">
                         <em class="tit">服务类型:</em>
                         <div class="searchInput rowStatusStyle ">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="ServicesTypeName"/>
                         </div>
                      </div>
                      <div id="ServicesTypePanl">


                     </div>

                      <div class="commonSelectWrap">
                                               <em class="tit">退货原因:</em>
                                               <div class="searchInput">
                                                <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="Reason"/>
                                               </div>
                                            </div>
                     </div> <!--panl   END-->

                 </div><!--clearfix END-->
                 </div><!--panlDiv订单信息  END-->

                <div class="panlDiv">
                 <div class="title">商品信息
                                  <div class="updateBtn" data-opttype="ActualQty">
                                     <div class="commonBtn" data-flag="update" data-type="gropupdate">修改</div>
                                     <div class="commonBtn" data-flag="save"   data-type="groupsubmit">保存</div>
                                     <div class="commonBtn" data-flag="cancel" data-type="groupsubmit">取消</div>
                                  </div>

                 </div>
                 <div class="panlText">

                      <div class="commonSelectWrap">
                         <em class="tit">商品名称:</em>
                         <div class="searchInput">
                          <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="ItemName"/>
                         </div>
                      </div>
                       <div class="commonSelectWrap">
                                               <em class="tit ">商品单价（元）:</em>
                                               <div class="searchInput wh80">
                                                <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="SalesPrice"/>
                                               </div>
                        </div>
                      <div class="commonSelectWrap" >
                         <em class="tit wh80">申请数量:</em>
                         <div class="searchInput wh80">
                          <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="applyQty"/>
                         </div>
                      </div>


                                            <div class="commonSelectWrap" data-type="gropupdate">
                                               <em class="tit">确认退货数量:</em>
                                               <div class="searchInput">
                                                <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="ActualQty"/>
                                               </div>
                                            </div>
                                         <div id="ActualQtyPanl">
                                           <!-- <form> </form>
                                                 <form id="ActualQty" class="optionForm" data-type="groupsubmit">
                                                                                      <div class="commonSelectWrap">
                                                                                         <em class="tit">申请数量:</em>
                                                                                         <div class="searchInput" >
                                                                                          <input type="text"  class="easyui-numberbox" data-options=" width:160,height:32,min:0,precision:0" name="ActualQty"/>
                                                                                         </div>
                                                                                      </div>

                                             </form>-->
                                             </div>
                      <div class="skuList" id="skuList" style="width: 100%; float:left" >


                      </div>

                 </div><!--clearfix END-->
                 </div><!--panlDiv 商品信息  END-->
                <div class="panlDiv">
                                 <div class="title">支付信息
                                  <div class="updateBtn" data-opttype="ConfirmAmount" id="ConfirmUpdate">
                                     <div class="commonBtn" data-flag="update" data-type="gropupdate">修改</div>
                                     <div class="commonBtn" data-flag="save"   data-type="groupsubmit">保存</div>
                                     <div class="commonBtn" data-flag="cancel" data-type="groupsubmit">取消</div>
                                  </div>
                                 </div>
                                 <div class="panlText">

                                      <div class="commonSelectWrap">
                                         <em class="tit">支付方式:</em>
                                         <div class="searchInput rowStatusStyle" >
                                          <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="PayTypeName"/>
                                         </div>
                                      </div>
                                      <div class="commonSelectWrap">
                                         <em class="tit">应退金额:</em>
                                         <div class="searchInput wh80" >
                                          <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="RefundAmount"/>
                                         </div>
                                      </div>
                                       <div class="commonSelectWrap" data-type="gropupdate">
                                          <em class="tit">确认退款金额(元):</em>
                                          <div class="searchInput wh80" >
                                           <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="ConfirmAmount"/>
                                          </div>
                                       </div>
                                       <div id="ConfirmAmountPanl">
                                        </div>


                                 </div><!--clearfix END-->
                                 </div><!--panlDiv  支付信息  END-->
            </form>

            <div class="panlDiv">
                 <div class="title">操作记录</div>
                 <div class="panlText" id="tableWrap">
                  <div class="table" id="commodity"></div>

                 </div>
                </div>  <!--panlDiv 操作记录  END-->
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

        <!--收款-->
       <script type="text/html" id="suklistValue">
        <#for(var i=0;i<list.length;i++){var item=list[i];#>
          <div class="commonSelectWrap">
                                  <em class="tit"><#=item.skuName#>:</em>
                                  <div class="searchInput">
                                   <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" value="<#=item.skuValue#>"/>
                                  </div>
                               </div>


       <#}#>

       </script>

       <script type="text/html" id="tpl_salesReturnOption">
                     <form id="orderOption">
                <div class="commonSelectWrap">
                      <em class="tit">备注: </em>
                <div class="searchInput" style="width:457px; height: 100px;">

                          <textarea type="text"   class="easyui-validatebox" data-options="required:true"  name="Desc"> </textarea>
                </div>
                </div>
               </form>
      </script>
        <script type="text/html" id="tpl_ActualQtyForm">
          <form> </form>
         <form id="ActualQty" class="optionForm" data-type="groupsubmit">
                                                                     <div class="commonSelectWrap">
                                                                        <em class="tit">确认退货数量:</em>
                                                                        <div class="searchInput" >
                                                                         <input type="text"  class="easyui-numberbox" data-options=" width:160,height:32,min:0,precision:0" name="ActualQty"/>
                                                                        </div>
                                                                     </div>

         </form>

        </script>
        <script type="text/html" id="tpl_ServicesTypeForm">
        <form> </form>
                        <form id="ServicesType" class="optionForm" data-type="groupsubmit" style="display: none">
                                                             <div class="commonSelectWrap">
                                                                <em class="tit">服务类型:</em>
                                                                <div class="searchInput" >
                                                                 <input type="text" id="ServicesTypeText"  class="easyui-combobox" data-options=" width:160,height:32,min:0,precision:0" name="ServicesType"/>
                                                                </div>
                                                             </div>
                         </form>


        </script>
        <script type="text/html" id="tpl_ConfirmAmountForm">

                                                 <form> </form>
                                               <form id="ConfirmAmount" class="optionForm" data-type="groupsubmit">
                                               <div class="commonSelectWrap">
                                                  <em class="tit">确认退款金额(元):</em>
                                                  <div class="searchInput" >
                                                   <input type="text"  class="easyui-numberbox" data-options=" width:160,height:32,min:0,precision:2" name="ConfirmAmount"/>
                                                  </div>
                                               </div>


                                               </form>

        </script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
