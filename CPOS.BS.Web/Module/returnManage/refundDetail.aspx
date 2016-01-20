<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>退款详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/returnManage/css/detail.css?v=0.4"%>" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/refundDetail.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                     <div class="optionBtn " style="display: none"  data-opttype="staus">
                     <div class="commonBtn icon"  data-showstaus="1" >确认退款</div>
                     <!-- <div class="commonBtn" data-status="2" data-showstaus="1,3,4,5"  >取消申请</div>-->




               </div>
                </div>

            </div>
             <div class="tablewrap">
             <form></form>
             <form id="salesReturnInfo">
                <div class="panlDiv  panlDivhead">
                          <div class="panlText panlTexthead">
                                   <div class="commonSelectWrap">
                                      <em class="tit">退款单号:</em>
                                      <div class="searchInput">
                                       <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="RefundNo"/>
                                      </div>
                                   </div>
                                   <div class="commonSelectWrap">
                                      <em class="tit">退款状态:</em>
                                      <div class="searchInput" >
                                       <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="StatusName"/>
                                      </div>
                                   </div>



                              </div><!--clearfix END-->
                </div><!--panlDiv   END-->
                <div class="panlDiv">
                 <div class="title">退款信息</div>
                 <div class="panlText">
                     <div class="panlL">
                      <div class="commonSelectWrap">
                         <em class="tit">原订单编号:</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="OrderNo"/>
                         </div>
                      </div>
                        <div class="commonSelectWrap">
                                               <em class="tit">会员:</em>
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
                         <em class="tit">应退金额(元):</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="ConfirmAmount"/>
                         </div>
                      </div>
                      <div class="commonSelectWrap">
                         <em class="tit">实退金额(元):</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="ActualRefundAmount"/>
                         </div>
                      </div>
                      <div class="commonSelectWrap">
                         <em class="tit">退款方式:</em>
                         <div class="searchInput">
                          <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="PayTypeName"/>
                         </div>
                      </div>

                      <div class="commonSelectWrap">
                                               <em class="tit">商户订单号:</em>
                                               <div class="searchInput">
                                                <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="PayOrderID"/>
                                               </div>
                                               <p class="fontColor">(*请在支付宝或者微信支付中输入此单号查询完成退款)</p>
                                            </div>
                      <div class="commonSelectWrap textlist">
                                               <em class="tit">余额(元):</em>
                                               <div class="searchInput">
                                                <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="Amount"/>

                                               </div>
                                                 <div class="searchInput" style="width: auto">
                                                  <em style="width: 36px;">积分:</em>
                                                   <input type="text" style="width:auto"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="Points"/>
                                                </div>
                                                 <div class="searchInput">
                                                  <em >返现(元):</em>
                                                   <input type="text"   readonly="readonly" class="easyui-validatebox" data-options="disabled:true" name="ReturnAmount"/>

                                                </div>
                                                <p class="fontColor">(*请在支付宝或者微信支付中输入此单号查询完成退款)</p>
                                            </div>


                     </div> <!--panl   END-->

                 </div><!--clearfix END-->
                 </div><!--panlDiv订单信息  END-->

                <div class="panlDiv">
                 <div class="title">商品信息
                 </div>
                 <div class="panlText ">

                      <div class="commonSelectWrap showItem" style="width: 100%; min-width: 300px;">
                         <em class="tit" style="min-width:60px;">商品名称:</em>
                         <div class="searchInput" style="width: 80%;">
                          <input type="text"  class="easyui-validatebox" data-options="disabled:true"  readonly="readonly" name="ItemName"/>
                         </div>
                      </div>
                                       <div class="commonSelectWrap hideItem">
                                          <em class="tit">该订单商品详情</em>
                                          <div class="searchInput" id="hrefOrderDetail">

                                          </div>
                                       </div>
                      <div class="skuList showItem" id="skuList" style="width: 100%; float:left" >


                      </div>

                 </div><!--clearfix END-->
                 </div><!--panlDiv 商品信息  END-->

            </form>


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
          <div class="commonSelectWrap" >
         <# if(i==0){ #>
          <em class="tit " ><#=item.skuName#>: </em>
         <# } else{  #>
              <em class="tit wh30" ><#=item.skuName#>: </em>
         <#  } #>

                                  <div class="searchInput wh30">
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

                          <textarea type="text"   class="easyui-validatebox"   name="Desc"> </textarea>
                </div>
                </div>
               </form>
      </script>


       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
