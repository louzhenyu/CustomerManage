<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员卡管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/vipCardManage/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" >

                        <div class="item">
                          <form></form>
                          <form id="seach">

                                                  <div class="commonSelectWrap">
                                                      <em class="tit">卡号：</em>
                                                      <div class="searchInput">
                                                          <input data-forminfo="" data-text="卡号" data-flag="VipCardCode" name="VipCardCode" type="text"
                                                              value="">
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">手机号：</em>
                                                      <div class="searchInput">
                                                          <input data-forminfo="" data-text="手机号" data-flag="Phone" name="Phone" type="text"
                                                              value="">
                                                      </div>
                                                  </div>
                                            <div class="commonSelectWrap">
                                               <em class="tit">卡类型：</em>
                                                <div class="selectBox">
                                                      <input data-text="卡类型" id="VipCardTypeCodeID"  name="VipCardTypeID" class="easyui-combobox" data-options="width:230,height:32" >
                                                   </div>
                                                 </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">卡状态：</em>
                                                      <div class="selectBox">
                                                                <input id="payment" name="VipCardStatusId"/>
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">办卡门店：</em>
                                                      <div class="selectBox">

                                                                    <input id="unitTree" name="UnitID"  class="easyui-combotree" data-options="width:160,height:32"/>

                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">办卡日期：</em>
                                                      <div class="selectBox">
                                                          <input class="datebox" name="CVipCardBeginDate" id="startDate"/>
                                                          至
                                                           <input class="datebox" name="CVipCardEndDate" validType="compareDate[$('#startDate').datebox('getText'),'开始时间应小于结束时间']" />
                                                      </div>
                                                  </div>

                           </form>

                        </div>
                       <div class="itemBtn">

                          <div class="moreQueryWrap">
                              <a href="javascript:;" class="commonBtn queryBtn select">查询</a>
                            </div>
                            <div class="moreQueryWrap">
                                <a href="javascript:;" class="commonBtn queryBtn" data-flag="card">刷卡查询</a>
                               </div>
                       </div>
                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap">
                   <div  id="gridTable" class="gridLoading">
                         <div  class="loading">
                                  <span>
                                <img src="../static/images/loading.gif"></span>
                           </div>
                   </div>

                     <div class="dataMessage" >没有符合查询条件的记录</div>
                    <div id="pageContianer">
                        <div id="kkpager" style="text-align: center;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <div style="display: none">

      <div id="win" class="easyui-window" data-options="modal:true,collapsible:false,minimizable:false,maximizable:false,
      title:'刷卡查询',width:636,height:320,left:($(window).width()-636)/2,top:($(window).height()-320)/2" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding-top:62px;">
      			  <form id="payOrder">
                           <div class="commonSelectWrap">
                                 <em class="tit" style="width:167px">卡号：</em>
                                <div class="searchInput" style="width:306px">
                                   <input type="text" class="easyui-validatebox" id="VipCardISN" data-options="required:true" name="VipCardISN" placeholder="请刷卡/输入卡号" />
                               </div>
                           </div>

                           </form>
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
        <!--收款-->
         <script id="tpl_OrderPayMent" type="text/html">
            <form id="payOrder">


            <div class="optionclass">
               <div class="commonSelectWrap">
                             <em class="tit">订单金额:</em>
                                <div class="borderNone" >
                                 <input id="Amount" class=" easyui-numberbox " name="Amount" readonly="readonly" style="border:none"/>
                               </div>
                </div>
                  <div class="commonSelectWrap">
                                                              <em class="tit">电子优惠券抵扣:</em>
                                                              <div class="selectBox bodernone">
                                                                 <input id="coupon" class="easyui-combogrid" data-options="width:160,height:32,validType:'selectIndex'"  name="CouponID" />
                                                             </div>
                                        </div>
               <div class="commonSelectWrap">
                             <em class="tit">纸质优惠券抵扣:</em>
                             <div class="searchInput" >
                                    <input id="Deduction" class="easyui-numberbox" name="Deduction" value="" data-options="width:160,height:32,precision:0,groupSeparator:','" /><span style="
    float: right;
    margin-right: -24px;
    margin-top: -30px;
    font-size: 14px;
">元</span>
                            </div>
                            </div>
               <div class="commonSelectWrap">
                             <em class="tit">实付金额：</em>
                             <input id="ActualAmount" class="searchInput bodernone" name="ActualAmount" readonly="readonly" />
                            </div>
                <div class="commonSelectWrap">
                                        <em class="tit">付款方式：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="pay"  class="easyui-combobox" data-options="width:160,height:32,required:true"  name="PayID" />
                                       </div>
                                       </div>
                <div class="commonSelectWrap" id="ValueCard">
                            <em class="tit">实体储值卡号：</em>
                                  <div class="searchInput bodernone">
                                      <input  class="easyui-validatebox" data-options="width:160,height:32,validType:['englishCheckSub','length[5,12]']"  name="ValueCard" />
                                  </div>
                            </div>
               </div>
                </form>
                </script>
        <!--头部名称-->
        <script id="tpl_thead" type="text/html">

            <#for(var i in obj){#>
                <th><#=obj[i]#></th>
            <#}#>
      </script>

        <!--数据部分-->
       <script id="tpl_content" type="text/html">
           <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
           <tr data-yearamount="<#=list.otherItems[i].YearAmount #>" data-vipid="<#=list.otherItems[i].VIPID#>" data-vipcardid="<#=list.otherItems[i].VipCardID #>" data-vipcardtypeid="<#=list.otherItems[i].VipCardTypeID#>">
               <#for(var e in idata){#>
               <td>

                    <#if(e.toLowerCase()=='vipcardcode'){#>
                             <p class="textLeft"><#= idata[e]#>
                             <#if(list.finalList[i].PayStatus!='已付款'){#>
                                   <b class="fontC" data-type="payment">收款</b>
                              <#}#>
                              </p>
                   <# }else{#>
                       <#= idata[e]#>
                   <#}#>

              </td>
               <#}#>
           </tr>
           <#} #>
       </script>
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
