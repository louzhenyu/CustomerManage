<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>门店管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/storeManagement/css/style.css?v=0.64"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
	.queryTermArea{background:#ecf1f5;}
    .queryTermArea .commonSelectWrap{margin-right:0px;}
	#opt{padding-top:17px;background:#f4f8fb;}
	.optionBtn .commonBtn{width:100px;}
	.commonSelectWrap .tit{width:105px;}
	.moreQueryWrap{float:left;margin-left:20px;}
	.optionBtn .exportBtn{display:none;margin-right:40px;}
	.moreQueryWrap .queryBtn{width:73px;}
	.iconPlay,.iconPause{display:inline-block;width:30px;height:39px;}
	.iconPlay{background:url(images/running.png) no-repeat center center;}
	.iconPause{background:url(images/pause.png) no-repeat center center;}
    .importBtn, .exportBtn {
float: left;
width: 82px;
height: 32px;
margin-left: 20px;
cursor: pointer;
}
    .importBtn {
background: url(images/icon-import.png) no-repeat center center;
}
    .exportBtn {
background: url(images/icon-export.png) no-repeat center center;
}
	.loading{width:81%;}
    </style>
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
                                  <em class="tit">店名：</em>
                                  <label class="searchInput" style="width:250px;">
                                      <input data-text="店名" data-flag="unit_name" name="unit_name" type="text" value="" placeholder="请输入店名">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">类型：</em>
                                  <div class="selectBox">
                                            <!--item_category_id-->
                                            <select id="StoreType" name="StoreType"></select>
                                  </div>
                              </div>
                              
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">状态：</em>
                                  <div class="selectBox">
                                            <select id="unit_status" name="unit_status"></select>
                                  </div>
                              </div>
                              <div class="moreQueryWrap">
                                                         <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                       </div>

                              
                              <div class="commonSelectWrap">
                                  <em class="tit">上级组织：</em>
                                  <div class="selectBox">
                                  <input id="Parent_Unit_ID" name="Parent_Unit_ID" />
                                  </div>
                              </div>



                        </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap">
                <div class="optionBtn" id="opt">
                	<div class="commonBtn" id="addStoreBtn">添加门店</div>

                    
                    <div class="importBtn"  id="inportStoreBtn"></div>
                    <div class="exportBtn"></div>
                    
                    <!--
                    <div class="commonBtn " data-flag="putaway"><img src="images/jiatous.png"  >  上架</div>
                    <div class="commonBtn" data-flag="soldOut"><img src="images/jiatoux.png"  >   下架</div>
                    <div class="commonBtn sales" data-flag="salesTooltip" id="sales"> <img src="images/cxfzbtn.png"  >更改商品分组</div>
                    -->
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
                   <div>
                   		<table class="dataTable" id="gridTable">
                        	<div  class="loading">
                               <span><img src="../static/images/loading.gif"></span>
                            </div>
                        </table>
                   </div>
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

      			<div data-options="region:'center'" style="padding:0px;">


			            <div class="qb_member">
                            <div id="step1" class="member step">
                                <div class="menber_title"><img src="images/lc_1.png" /></div>
                                <div class="menber_center">
                                    <div class="menber_centernr">
                                        <div class="menber_centernrt">
                                            <p>请按照数据模板的格式准备要导入的数据<a href="#">（如何导入？）</a></p>
                                            <p><a href="/File/ExcelTemplate/门店导入数据模板.xlsx">下载模板</a></p>
                                            <div class="attention"><span>注意事项：</span>
                                                1.模板中的表头名称不可更改、表头行不能删除。<br />
                                                2.项目顺序可以调整，不需要的项目可以删除。<br />
                                                3.表中的会员姓名、手机号为必填项目，必须保留。<br />
                                                4.导入文件请勿超过 1 MB。
                                            </div>
                                        </div>
                                        <div class="menber_centernrb" id="editLayer">
                                            选择需要导入的xlsx文件
                                            <p id="nofiletext" >未选择文件</p>
                                             <div class="CSVFilelist"></div>
                                            <input id="CSVFileurl" value="" type="hidden"  />
                                           <input type="file" class="uploadCSVFileBtn" />
                                        </div>
                                    </div>  
	                            </div>
                            </div>

                             <div id="step2"  class="member step" style="display:none">
                                <div class="menber_title"><img src="images/lc_2.png" /></div>
                                    <div class="menber_center">
                                        <div class="menber_centernr">
                                            <div class="loading">导入中...</div>
                                            <div class="attention"><span>提示：</span>
                                                1.导入过程中请勿关闭此页面；<br />
                                                2.数据导入结束后，可能下载错误报告，以便重新处理。
                                            </div>
        	                            </div>
		                            </div>
                            </div>


                            <div id="step3"  class="member step" style="display:none">
                                <div class="menber_title"><img src="images/lc_3.png" /></div>
                                <div class="menber_center">
                                    <div class="menber_centernr">
                                        <div class="succeed">导入完成<p>共<span id="inputTotalCount" class="inputCount"> 0</span> 条，成功导入<span  id="inputErrCount" class="red inputCount"> 0</span> 条</p></div>
                                        <div class="menber_centernrb1">
                	                        下载错误报告，查看失败原因
                                            <p><a id="error_report" href="javascipt:void(0)">error_report.xlsx</a><span>选择文件</span></p>
                    
                                        </div>
        	                        </div>
		                        </div>
                            </div>


                            </div>
			
			  


      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;padding:5px 20px 0;">
      				<a id="startinport"  class="easyui-linkbutton commonBtn saveBtn" >开始导入</a>  
                      <a id="closebutton" style="display:none;" class="easyui-linkbutton commonBtn closeBtn close" >关闭</a>
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
