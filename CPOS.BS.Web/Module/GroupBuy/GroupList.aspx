<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title></title>
	
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
        
        <link href="css/style.css" rel="stylesheet" type="text/css" />
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="allPage" id="section" data-js="js/groupList.js?ver=0.3">
	<div class="commonTopTitle optionBtn " style="border-top: none;"><a href="javascript:;" id="addNew" class="commonBtn icon w110  icon_add r">添加新</a></div>
    <!--团购列表-->
    <div  class="groupList">
    	<ul id="goodsList">
        </ul>
        <div id="kkpager" style="text-align:right;"></div>
    </div>
    
</div>
<!-- 弹层，添加新团购 -->
<div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
<div id="addNewDiv" class="jui-dialog jui-dialog-addGroup" style="display:none;">
	<div class="jui-dialog-tit">
    	<h2 id="addText">添加新</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
	<div class="groupSetArea-box">
        <div>
            <h3>活动名称：</h3>
            <label><input type="text" id="name"  placeholder="请输入活动名称 " /></label>
            <em>例如：微商城7月上旬团购</em>				
        </div>
        <div>
            <h3>开始时间：</h3>
            <label><input type="text" id="beginTime"  placeholder="请选择活动开始时间" readonly/></label>
            <em>示例：2014.06.10-09:00</em>				
        </div>
        <div>
            <h3>结束时间：</h3>
            <label><input type="text" id="endTime"  placeholder="请选择活动结束时间"  readonly/></label>
            <em>示例：2014.06.10-09:00</em>				
        </div>
        <div>
            <h3>上架状态：</h3>
            <label class="selectBox">
                <span id="statusText" data-tt="5" data-shopstatus="20">在商城中显示</span>
                <span class="dropList">
                    <span data-status="10">不在商城中显示</span>
                    <span data-status="20">在商城中显示</span>
                </span>
            </label>
            <em>上架商品显示在推出的活动中</em>				
        </div>
    </div>
    <div class="btnWrap">
        <a href="javascript:;" id="btnSureAdd" class="commonBtn">确定</a>
    </div>
</div>
<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){
          var item=list[i];#>
        	<li>
<a href="GroupManage.aspx?eventId=<#=item.EventId#>&pageType=<#=item.EventTypeId#>&showPage=shopManage&pageName=<#=item.pageStr#>&mid=<#=Mid#>&PMenuID=<#=PMenuID#>">
                <div class="groupList-item">
                    <div class="markBox">
                        <#var index=parseInt(item.BeginTime.split("-")[1])-1,day=item.BeginTime.split("-")[2].split(" ")[0];#>
                	    <p class="month"><#=window.Month[index]#></p>
                        <p class="day"><#=day#></p>
                    </div>
                    <div class="infoBox">
                	    <div>
                    	    <span class="shelf"><#=item.EventStatus#></span>
                    	    <h3 class="tit t-overflow"><#=item.EventName#></h3>
                        </div>
                        <div class="other">
                    	    <p class="timeSlot"><span class="startTime"><#=item.BeginTime#></span>至<span class="endTime"><#=item.EndTime#></span></p>
                            <span class="surplus">剩余商品<strong><#=item.RemainQty#></strong></span>
                            <span>总库存<strong><#=item.Qty#></strong></span>
                        </div>
                    </div>
                </div>
            </a>
            </li>
    <#}#>
</script>
<script id="tpl_date" type="text/html">
    <span data-day="-1">请选择</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-day="<#=item.date#>"><#=item.dateName#></span>
    <#}#>
    <span data-day="0">自定义</span>
</script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/GroupBuy/js/main.js"></script>
</asp:Content>