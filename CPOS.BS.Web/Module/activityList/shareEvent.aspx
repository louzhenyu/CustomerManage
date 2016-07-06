<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>分享设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/activityList/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
<style type="text/css">
.textbox-addon-right{right:9px !important;top:1px;}
.queryTermArea .commonSelectWrap{margin-right:0px;}
.commonSelectWrap .tit{width:102px;}
.moreQueryWrap .queryBtn{width:72px;margin:3px 70px 0 0;}

.datagrid-btable .handle{width:100%;height:39px;line-height:38px;cursor:pointer;}
.datagrid-btable .delete{background:url(images/delete.png) no-repeat center center;}
.datagrid-btable .running{background:url(images/running.png) no-repeat center center;}
.datagrid-btable .pause{background:url(images/pause.png) no-repeat center center;}
.datagrid-btable .down{background-repeat:no-repeat;background-position:center center;}

#tableWrap2 .datagrid-row,#tableWrap2 .datagrid-header-row{height:62px;line-height:61px;}
#tableWrap2 .datagrid-btable{width:100%;}
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
.exportTable .exportBtn{display:inline-block;width:110px;height:32px;line-height:32px;margin:22px 0 0 30px;text-align:center;font-size:14px;border-radius:3px;background:#0cc;color:#f4f8fa;}
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


/*添加分享设置*/
.addShareArea{height:75px;padding:21px 37px;background:#f4f8fa;}
.commonHandleBtn{display:block;width:132px;height:32px;line-height:32px;text-align:center;font-size:14px;border-radius:4px;background:#07c8cf;color:#fff;}
.commonHandleBtn:hover{color:#fff;}
#tableWrap .datagrid-header,#tableWrap .datagrid-htable{height:50px;}
#tableWrap .datagrid-btable tr{height:91px;}
#tableWrap .addBtn{display:block;width:100%;height:49px;background:url(images/icon-add.png) no-repeat center center;cursor:pointer;}
#tableWrap .delBtn{display:block;width:100%;height:49px;background:url(images/delete.png) no-repeat center center;cursor:pointer;}
/*追加奖品数量*/
.jui-dialog-prizeCountAdd{width:636px;height:322px;position:fixed;top:20%;margin-left:-318px;}
.prizeCountAddContent .commonSelectWrap{margin:60px 0 45px 0;}
/*添加分享设置*/
.jui-dialog-addShare{width:636px;height:518px;position:fixed;top:50px;margin-left:-318px;}
.ruleSetTit{margin:21px;padding:15px 0 0;font-size:16px;font-family:"黑体";border-top:1px solid #ccc;color:#66666d;}
.jui-dialog-addShare .btnWrap {padding:38px 0 0 0;}
.jui-dialog .textbox{border:none;background:none;}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/shareEvent.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <!--
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
                </div>
                -->
                
                <div class="addShareArea">
                	<a href="javascript:;" class="commonHandleBtn" id="addShareBtn">+添加分享</a>
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
    <!--奖品数量追加，弹出-->
    <div class="jui-dialog jui-dialog-prizeCountAdd" style="display:none">
        <div class="jui-dialog-tit">
            <h2>奖品数量追加</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="prizeCountAddContent">
             <div class="commonSelectWrap">
                <em class="tit" style="width:172px;">数量：</em>
                <label class="searchInput clearBorder" style="width:308px">
                  <input data-text="奖品数量追加" class="easyui-numberbox" id="prizeCountAdd" data-options="required:true,width:308,height:32" data-flag="" name="" type="text" value="">
                </label>
            </div>
            <div class="btnWrap">
                <a href="javascript:;" class="commonBtn saveBtn">保存</a>
                <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
            </div>
        </div>
    </div>
    
    
    <!--添加分享设置，弹出-->
    <div class="jui-dialog jui-dialog-addShare" style="display:none">
        <div class="jui-dialog-tit">
            <h2>分享配置</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="redPackageContent">
        	<form></form>
            <form id="addShareForm">
            <!--
            <div class="commonSelectWrap">
                <em class="tit">奖品等级：</em>
                <label class="searchInput clearBorder">
                  <input data-text="奖品等级" class="easyui-combobox" id="prizeLevel" data-options="required:true,width:190,height:32" data-flag="" name="PrizeLevel" type="text" value="">
                </label>
            </div>
            -->
            <div class="commonSelectWrap">
                <em class="tit">奖品选择：</em>
                <label class="searchInput clearBorder">
                  <input data-text="奖品选择" class="easyui-combobox" id="prizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeID" type="text" value="">
                </label>
            </div>
            
            <div class="commonSelectWrap" id="couponItem">
                <em class="tit">优惠券：</em>
                <label class="searchInput clearBorder">
                  <input data-text="优惠券" class="easyui-combobox" id="couponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
                </label>
            </div>
            
            
            <div class="commonSelectWrap"  id="integralItem" style="display:none">
                <em class="tit">积分：</em>
                <label class="searchInput clearBorder">
                  <input data-text="积分"  class="easyui-numberbox" data-options="width:190,height:32,min:0,precision:0" id="IntegralCount" name="Point"  type="text" value="">
                </label>
            </div>
            
            <div class="commonSelectWrap">
                <em class="tit">奖品数量：</em>
                <label class="searchInput clearBorder">
                  <input data-text="奖品数量" class="easyui-numberbox" id="prizeCount" data-options="required:true,width:190,height:32" data-flag="" name="TotalCount" type="text" value="">
                </label>
            </div>
            
            <p class="hint-exp">提示：奖品数量不能超过券的生成数量！</p>
            
            
            <div class="commonSelectWrap">
                <em class="tit">活动选择：</em>
                <label class="searchInput clearBorder">
                  <input data-text="活动选择" class="easyui-combobox" id="workingEvent" data-options="required:true,width:190,height:32" data-flag="" name="EventId" type="text" value="">
                </label>
            </div>
            
            
            <div class="ruleSetTit">规则设置</div>
            
            <div class="commonSelectWrap">
                <em class="tit">分享奖励次数：</em>
                <label class="searchInput clearBorder"> 
                  <input data-text="分享奖励次数" class="easyui-combobox" id="shareTimes" data-flag="shareTimes" name="ShareTimes" type="text" value="" data-options="required:true,width:190,height:32"  validType='selectIndex'>
                </label>
            </div>
            
            <!--
            <div class="commonSelectWrap">
                <em class="tit">奖品名称：</em>
                <label class="searchInput clearBorder">
                  <input data-text="奖品名称" class="easyui-validatebox" id="prizeName" data-options="required:true,width:190,height:32" data-flag="" name="PrizeName" type="text" value="">
                </label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">中奖概率：</em>
                <label class="searchInput clearBorder">
                  <input data-text="中奖概率" class="easyui-numberbox" id="prizeProbability" data-options="required:true,width:190,height:32" data-flag="" name="Probability" type="text" value="">
                </label>
            </div>
            -->
            </form>
            <div class="btnWrap">
                <a href="javascript:;" class="commonBtn saveBtn">保存</a>
                <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
            </div>
        </div>
    </div>
    
       
    <!--头部名称-->
   	<script id="tpl_thead" type="text/html">
         <#for(var i in obj){#>
             <th><#=obj[i]#></th>
         <#}#>
    </script>

 
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
