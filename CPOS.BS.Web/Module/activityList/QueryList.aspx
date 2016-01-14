<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>活动列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/activityList/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
<style type="text/css">
.textbox-addon-right{right:9px !important;top:-2px;}
.queryTermArea .commonSelectWrap{margin-right:0px;}
.commonSelectWrap .tit{width:102px;}
.moreQueryWrap .queryBtn{width:72px;margin:3px 70px 0 0;}

.datagrid-btable .handle{width:25px;height:39px;line-height:38px;cursor:pointer;  display: inline-block;}
.datagrid-btable .delete{background:url(images/delete.png) no-repeat center center;}
.datagrid-btable .running{background:url(images/running.png) no-repeat center center;}
.datagrid-btable .detail{background:url(images/detail.png) no-repeat center center;}
.datagrid-btable .pause{background:url(images/pause.png) no-repeat center center;}
.datagrid-btable .down{background-repeat:no-repeat;background-position:center center;}

#tableWrap2 .datagrid-row,#tableWrap2 .datagrid-header-row{height:62px;line-height:61px;}
#tableWrap2 .datagrid-btable{width:100%;}

#addNewGamesBtn{width:115px;text-indent:20px;background:#0cc url(images/icon-add2.png) no-repeat 20px center;}
/*活动参与，弹出*/
.jui-dialog .dataTable{border:none;}
.jui-dialog .dataTable tr{height:49px;line-height:48px;}
.jui-dialog .dataTable th{text-align:center;font-size:14px;color:#4d4d4d;}
.jui-dialog .dataTable td{font-size:13px;color:#666;}
.jui-dialog .dataTable .tableHead{border-bottom:2px solid #07c8cf;}

.jui-dialog-table{width:1026px;height:576px;position:fixed;top:30px;margin-left:-513px;}
.jui-dialog-tit{height:55px;}
.jui-dialog-tit h2{font:16px/55px "Microsoft YaHei";color:#66666d;}
.jui-dialog-close{position:absolute;top:0px;right:18px;width:30px;height:54px;background:url(images/icon-close-btn.png) no-repeat center center;cursor:pointer;}
.jui-dialog .tit{width:250px;}
.jui-dialog .activityContent{padding:10px 0 30px 0;}
.jui-dialog .searchInput{width:190px;}
.jui-dialog .commonSelectWrap{float:none;display:block;margin:20px 0 2px 0;}
.jui-dialog .hint-exp{padding-top:5px;text-indent:30px;text-align:center;font-size:14px;color:#999;}
.jui-dialog .btnWrap{padding:15px 0 0 0;}
.jui-dialog .commonBtn{width:150px;height:45px;line-height:45px;}
.jui-dialog .cancelBtn{border:none;background:#ccc;color:#fff;}

.exportTable{height:75px;background:#f4f8fa;}
.exportTable .exportBtn{display:none;width:110px;height:32px;line-height:32px;margin:22px 0 0 30px;text-align:center;font-size:14px;border-radius:3px;background:#0cc;color:#f4f8fa;}
/*弹层，分页*/
/*底部分页的重写*/
#kkpager2{clear:both;height:30px;line-height:30px;margin-top:10px;color:#999999;font-size:14px;}
#kkpager2 a{padding:4px 8px;margin:10px 3px;font-size:12px;color:#9d9d9d;text-decoration:none;}
#kkpager2 span{font-size:14px;}
#kkpager2 span.disabled{padding:4px 8px;margin:10px 3px;font-size:12px;border:1px solid #DFDFDF;background-color:#FFF;color:#DFDFDF;}
#kkpager2 span.curr{padding:4px 8px;margin:10px 3px;font-size:12px;border:1px solid #FF6600;background-color:#FF6600;color:#FFF;}
#kkpager2 a:hover{background-color:#FFEEE5;border:1px solid #FF6600;}
#kkpager2 span.normalsize{font-size:12px;}

#kkpager2{clear:both;height:30px;line-height: 23px;color:#323232;font-size:12px;text-align: right;padding-right: 65px;}
#kkpager2 span.normalsize{ color:#323232;}
#kkpager2 span.disabled,#kkpager a {border: none; color: #969696;}
#kkpager2 a:hover {border: none; background: none; color: #00cccc}
#kkpager2 span.curr {color: #00cccc; font-size: 14px; font-weight:600; border: none; background: none;}

.joinprize,.winprize{  text-decoration: underline;color:#0099ff;}
    
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
                                  <em class="tit">活动名称：</em>
                                  <label class="searchInput" style="width:387px;">
                                      <input data-text="活动名称" data-flag="item_name" name="item_name" type="text" value="">
                                  </label>
                              </div>
                              <div class="commonSelectWrap">
                                  <em class="tit">活动时间：</em>
                                  <div class="selectBox">
                                        <input type="text" data-flag="item_startTime" name="item_startTime" id="startDate" class="easyui-datebox" data-options="width:162,height:34"/>&nbsp;&nbsp;至&nbsp;&nbsp;<input type="text"  data-flag="item_endTime" name="item_endTime" class="easyui-datebox" validType="compareDate[$('#startDate').datebox('getText'),'前面选择的时间必须晚于该时间']" data-options="width:163,height:34"/>
                                  </div>
                              </div>
                              <div class="moreQueryWrap">
                                 <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                              </div>
                          </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="optionBtn">
                	<a class="commonBtn" id="addNewGamesBtn" href="javascript:;">添加游戏</a>
                </div>
                <div class="tableWrap" id="tableWrap" style="display:inline-block;width:100%;">

                   <table class="dataTable" id="gridTable"></table>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager">
                        </div>
                    </div>
                </div>
            </div>
        </div>
      
      
      
    <!-- 遮罩层 -->
    <div class="jui-mask" style="display:none;"></div>
    <!--显示参与人数，弹出-->
    <div class="jui-dialog jui-dialog-table" style="display:none;">
        <div class="jui-dialog-tit">
            <h2>人数统计</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="" id="tableWrap2">
        	<div class="exportTable">
            	<a href="javascript:;" class="exportBtn">全部导出</a>
            </div>
            <div>
            	<table class="dataTable" id="gridTable2"></table>
                <!--
              	<thead>
                	<tr class="tableHead">
                        <th>会员名称</th>
                        <th>抽奖次数</th>
                        <th>时间</th>
                    </tr>
                </thead>
                <tbody>
                  <tr class="">
                    <td>丁丁</td>
                    <td>10</td>
                    <td>2015-04-30 14:22:35</td>
                  </tr>
                </tbody>
                -->
                <div id="pageContianer">
                   <div class="dataMessage">没有符合条件的查询记录</div>
                   <div id="kkpager2"></div>
                </div>
            </div>
            <div class="btnWrap">
                <a href="javascript:;" class="commonBtn saveBtn">确定</a>
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

