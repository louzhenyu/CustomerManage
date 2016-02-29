<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>配送方式</title>
    <link href="../styles/css/reset-pc.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/artDialog.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <style>
	/***弹出，创建版块，创建期刊***/
	.dataTable tbody td{ height: 50px;}
	.jui-dialog{display:none;}
	.jui-dialog-dispatching{width:760px;min-height:468px;top:50px;margin-left:-380px;}
	.jui-dialog-dispatching .dispatchingContent{padding:30px 35px;}
	.jui-dialog-dispatching .commonSelectWrap{width:100%;margin-bottom:28px;}
	.jui-dialog-dispatching .commonSelectWrap .tit{width:155px;text-align:right;}
	
	.jui-dialog-dispatching .radioWrap{margin-left:100px;height:35px;}
	.jui-dialog-dispatching .searchInput{width:420px;height:35px;border:1px solid #dedede;}
	.jui-dialog-dispatching .searchInput input{height:32px;}
	.jui-dialog-dispatching .searchInput input[disabled='disabled'],
	.jui-dialog-dispatching .searchInput textarea[disabled='disabled']{background:#ccc;}
	
	.jui-dialog-dispatching .radioBox{float:left;height:24px;line-height:24px;margin:6px 0 0 24px;padding-left:30px;background:url(../styles/images/newYear/radio.png) no-repeat left center;cursor:pointer;}
	.jui-dialog-dispatching .radioBox.on{background:url(../styles/images/newYear/radioOn.png) no-repeat left center;}
	
	.jui-dialog-dispatching .uploadFileBox,.jui-dialog-dispatching .uploadFileBox01{position:relative;display:inline-block;width:90px;height:32px;line-height:32px;margin-left:8px;border-radius:5px;text-align:center;background:#CCC;color:#fff;cursor:pointer;}
	

	.tableWrap{border:none;}

	.tableWrap .operateWrap .editIcon{cursor:pointer;display:block;background:url(../styles/images/newYear/exit.png) no-repeat center center; width: 18px; height: 18px;  }
	

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
        <div id="dispatchingList"></div>
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
            <p class="searchInput" style="height:100px"><textarea class="formInputBox" id="dispatching_describe" style="width:100%;height:100%;"></textarea></p>
        </div>
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 默认配送费:</em>
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
<div id="jui-dialog-2" class="jui-dialog jui-dialog-dispatching">
	<div class="jui-dialog-tit">
    	<h2>到店自提</h2>
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
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 备货期:</em>
            <p class="searchInput"><input class="formInputBox" id="dispatching_stockup" type="text" /></p>
            <span style="display:inline-block;line-height:34px;padding-left:10px;font-size:16px;color:#ccc;">小时</span>
        </div>
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 门店工作时间:</em>
            <p class="searchInput" style="width:190px;"><input class="formInputBox" id="dispatching_startTime"  type="text" /></p>
            <span style="float:left;line-height:34px;padding:0 2px 0 12px;font-size:16px;color:#ccc;">到</span>
            <p class="searchInput" style="width:190px;"><input class="formInputBox" id="dispatching_endTime" type="text" /></p>
        </div>
		<div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 提货期最长:</em>
            <p class="searchInput"><input class="formInputBox" id="dispatching_pickup" type="text" value="" /></p><span style="display:inline-block;line-height:34px;padding-left:10px;font-size:16px;color:#ccc;">天内</span>
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
