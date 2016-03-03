<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商品列表单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/commodity/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">商品名称：</em>
                                                      <label class="searchInput" style="width: 506px;">
                                                          <input data-text="商品名称" data-flag="item_name" name="item_name" type="text"
                                                              value="">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">状态：</em>
                                                      <div class="selectBox">
                                                                <input id="item_status" name="item_status" class="easyui-combobox" data-options="width:200,height:30"  />
                                                      </div>
                                                  </div>
                                                  <div class="moreQueryWrap">
                                                                             <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                                           </div>

                                                  <div class="commonSelectWrap">
                                                      <em class="tit">商品分类：</em>
                                                      <div class="selectBox">
                                                                <input id="item_category_id" class="easyui-combobox" data-options="width:200,height:30" name="item_category_id"/>
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">商品分组：</em>
                                                      <div class="selectBox">

                                                      <input id="SalesPromotion_id" class="easyui-combobox" data-options="width:200,height:30"  name="SalesPromotion_id" />

                                                      </div>
                                                  </div>



                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div> <div class="optionBtn" id="opt">
                                       <div class="commonBtn icon w80 icon_up" data-flag="putaway">上架</div>
                                       <div class="commonBtn icon w80 icon_downLoad" data-flag="soldOut">   下架</div>
                                       <div class="commonBtn icon w120 sales icon_playlist" data-flag="salesTooltip" id="sales"  >更改商品分组</div>
                                       <div class="commonBtn icon w100 icon_add r" data-flag="add"  >新增商品</div>
                                       <div style="display: none">
                                              <div id="Tooltip">
                                                     <div class="treeNode"></div>

                                                    <div class="btnList">
                                                      <div class="commonBtn opts l"  data-flag="sales"> 确定 </div>
                                                       <div class="commonBtn opts r"  data-flag="cannel">取消</div>
                                                     </div>
                                              </div>

                                       </div>

                                      </div>
                <div class="tableWrap" id="tableWrap">

                   <div class="imgTable"> <div class="dataTable" id="gridTable">
                                               <div  class="loading">
                                                        <span>
                                                      <img src="../static/images/loading.gif"></span>
                                                 </div>
</div>  </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager" >
                        </div>
                    </div>
                </div>
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
                                    <input id="Deduction" class="easyui-numberbox" name="Deduction" value="" data-options="width:160,height:32,precision:0,groupSeparator:','" /><span style="float: right; margin-right: -24px;margin-top: -30px;font-size: 14px;">元</span>
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
