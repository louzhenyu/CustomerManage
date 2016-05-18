<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>配送方式</title>
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/artDialog.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <style>
	/***弹出，创建版块，创建期刊***/
	.dataTable tbody td{ height: 50px;}
	.jui-dialog{display:none;}
	.jui-dialog-dispatching{width:760px;min-height:468px;top:50px;margin-left:-380px;}
	.jui-dialog-dispatching .dispatchingContent{padding:30px 35px;}
	.jui-dialog-dispatching .commonSelectWrap{width:100%;margin-top:0px;}
	.jui-dialog-dispatching .commonSelectWrap .tit{width:155px;text-align:right;font-size:12px;color:#666;}
	
	.jui-dialog-dispatching .radioWrap{margin-left:100px;height:32px;}
	.jui-dialog-dispatching .searchInput{width:420px;height:32px;border:1px solid #ddd;}
	.jui-dialog-dispatching .searchInput input{height:32px;}
	.jui-dialog-dispatching .searchInput input[disabled='disabled'],
	.jui-dialog-dispatching .searchInput textarea[disabled='disabled']{background:#ccc;}
	
	.jui-dialog-dispatching .radioBox{float:left;height:24px;line-height:24px;margin:6px 0 0 24px;padding-left:30px;background:url(../styles/images/newYear/radio.png) no-repeat left center;cursor:pointer;}
	.jui-dialog-dispatching .radioBox.on{background:url(../styles/images/newYear/radioOn.png) no-repeat left center;}
	
	.jui-dialog-dispatching .uploadFileBox,.jui-dialog-dispatching .uploadFileBox01{position:relative;display:inline-block;width:90px;height:32px;line-height:32px;margin-left:8px;border-radius:5px;text-align:center;background:#CCC;color:#fff;cursor:pointer;}
	
	#dispatchingList{min-height:120px;}
	.tableWrap{border:none;}
	.tip-payment{float:right;height:30px;line-height:30px;font-size:12px;text-align:right;padding:0 60px 0 45px;background:url(images/icon-tip.png) no-repeat left center;color:#f00;}
	
	.jui-dialog-dispatching .btnWrap{display:inline-block;width:100%;}
	.tip-text{display:inline-block;line-height:32px;padding-left:10px;font-size:12px;color:#666;}
	.deliveryNumBox .tip-text,.addTimePassageBox .tip-text{float:left;margin-right:10px;}
	.timePassageArea{display:inline-block;font-size:12px;color:#666;}
	.timePassage{width:250px;max-height:100px;padding-bottom:5px;overflow-y:auto;}
	.timeItem{width:220px;height:25px;line-height:25px;margin-bottom:5px;}
	.timeItem > *{float:left;}
	.timeItem span{margin-left:17px;color:#00a0e8;cursor:pointer;}
	.timeItem .startTime{margin:0 10px 0 0;}
	.timeItem .endTime{margin:0 0 0 10px;}
	.addTimePassageBox{display:inline-block;margin-top:3px;background:#fff;}
	.addTimePassageBox .searchInput{background:url(images/icon-time.png) no-repeat 75px center;}
	.timeSaveBtn,.timeCancelBtn{line-height:32px;margin-left:17px;color:#00a0e8;cursor:pointer;}
	.checkBox{float:left;width:18px;height:18px;background:url(images/icon-check.png) no-repeat left center;}
	.checkBox.on{background:url(images/icon-checked.png) no-repeat left center;}
	.deliveryNumBox .checkBox{margin-top:7px;}
	.timePassageArea .checkBox{margin-right:10px;}
	.setTimeBox{padding-left:28px;}
	#jui-dialog-2 .tit{width:75px;}
	#jui-dialog-2 .radioWrap{margin-left:75px;}
	#jui-dialog-2 .radioBox{margin:4px 18px 0 2px;padding-left:25px;}
	#addTimeBtn{margin-top:10px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class="dispatchingListArea" id="section" data-js="js/dispatchingList">
    <div class="tableWrap cursorDef" >
       <!-- &lt;!&ndash; 已确认名单表格 &ndash;&gt;
        <table class="dataTable" style="display:inline-table;">
            <thead>
                <tr class="title">
                    <th width="20%">配送方式</th>
                    <th width="40%">状态</th>
                    <th width="20%">编辑</th>
                </tr>
            </thead>
            <tbody id="dispatchingList">

            </tbody>
        </table>-->
        <div id="dispatchingList">
        	<div class="loading" style="padding-top:40px;">
              <span><img src="../static/images/loading.gif"></span>
            </div>
        </div>
        <p class="tip-payment">至少配置一种配送方式，否则无法完成配送环节。</p>
    </div>
</div>
<div id="kkpager" style="padding-right:35px;text-align:right;"></div>

<!-- 遮罩层 -->
<div class="jui-mask"></div>
<!-- 弹出，送货到家 -->
<div id="jui-dialog-1" class="jui-dialog jui-dialog-dispatching">
	<div class="jui-dialog-tit">
    	<h2>送货到家</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="dispatchingContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
               <p class="radioBox startUs">启用</p>
               <p class="radioBox unstartUs">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap" style="height:100px">
            <em class="tit"><span class="fontRed">*</span> 配送费描述:</em>
            <p class="searchInput" style="height:100px"><textarea class="formInputBox" id="dispatching_describe" placeholder="例如：每笔订单消费满**元，本店包邮，除港澳台地区、内蒙古、新疆、西藏地区除外。每笔订单**元以下，快递费**元；**元以上，免快递费。" style="width:100%;height:100%;"></textarea></p>
        </div>
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span>默认配送费:</em>
            <p class="searchInput"><input class="formInputBox" id="dispatching_cost" type="text" value="" /></p><span style="display:inline-block;line-height:34px;padding-left:10px;font-size:16px;color:#ccc;">元</span>
        </div>
		<div class="commonSelectWrap">
            <em class="tit">免配送费最低订单金额:</em>
            <p class="searchInput"><input class="formInputBox" id="dispatching_mincost" type="text" value="" /></p><span style="display:inline-block;line-height:34px;padding-left:10px;font-size:16px;color:#ccc;">元</span>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>



<!-- 弹出，到店自提 -->
<div id="jui-dialog-2" class="jui-dialog jui-dialog-dispatching" style="width:580px;min-height:200px;margin-left:-290px;">
	<div class="jui-dialog-tit">
    	<h2>到店自提</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="dispatchingContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用：</em>
            <div class="radioWrap">
                <p class="radioBox startUs">启用</p>
                <p class="radioBox unstartUs">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit">备货期：</em>
            <p class="searchInput" style="width:120px;"><input class="formInputBox" id="dispatching_stockup" type="text" value=0/></p>
            <span class="tip-text">小时</span>
        </div>
        
        <div class="commonSelectWrap deliveryNumBox">
            <em class="tit">提货设置：</em>
            <span class="checkBox"></span>
            <span class="tip-text">可提货天数</span>
            <p class="searchInput" style="width:100px;">
            <input class="formInputBox" id="dispatching_pickup" type="text" value="" /></p>
            <span class="tip-text">天  用户可选择X天中任意一天提货</span>
        </div>
        
        <div class="commonSelectWrap" style="height:auto">
            <em class="tit"></em>
            <div class="timePassageArea">
            	<span class="checkBox"></span>
            	<p class="">提货时间段</p>
                <div class="setTimeBox">
                    <div class="timePassage">
                    	<!--
                        <div class="timeItem">
                            <p><span class="startTime">10:00</span> 至 <span class="endTime">14:00</span></p>
                            <span class="editBtn">修改</span>
                            <span class="removeBtn">删除</span>
                        </div>
                        <div class="timeItem">
                            <p><span class="startTime">10:00</span> 至 <span class="endTime">14:00</span></p>
                            <span class="editBtn">修改</span>
                            <span class="removeBtn">删除</span>
                        </div>
                        -->
                    </div>
                    
                    <div class="addTimePassageBox" style="display:none">
                        <p class="searchInput" style="width:100px;"><input class="formInputBox" id="dispatching_startTime"  type="text" /></p>
                        <span class="tip-text">至</span>
                        <p class="searchInput" style="width:100px;"><input class="formInputBox" id="dispatching_endTime" type="text" /></p>
                        <span class="timeSaveBtn">保存</span>
                        <span class="timeCancelBtn">取消</span>
                    </div>
                    
                    <a href="javascript:;" id="addTimeBtn" class="commonBtn w80">新增</a>
                </div>
            </div>
        </div>

        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>


<script id="tpl_dispatchingList" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        	<tr>
				<td><#=item.deliveryName#></td>
				<#if(item.IsOpen){#>
					<td class="unstart">已启用</td>
				<#}else{#>
					<td class="unstart blue">未启用</td>
				<#}#>
				<td class="operateWrap" title="编辑" data-typeid="<#=item.deliveryId#>" >
					<span class="editIcon"></span>
				</td>
			</tr>
    <#}#>
</script>

      <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
