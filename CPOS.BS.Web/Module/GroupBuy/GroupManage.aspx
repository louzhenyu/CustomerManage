<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>团购管理</title>
        <link href="css/style.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="allPage" id="section" data-js="js/groupManage">
	<div class="commonTopTitle">
    <span style="color: #fff;font-size: 14px;margin-left: 20px;"><a id="toBrowser" href="GroupList.aspx" style="color:#fff;text-decoration: underline;">活动列表</a></span><em style="color: #fff;font-size: 12px;">&nbsp;&nbsp;》&nbsp;&nbsp;</em><span style="color: #fff;font-size: 14px;" id="curEventName"></span>
    </div>
    <div class="groupHandleArea">
    	<!--团购原来信息-->
    	<div class="groupOriginalInfo" id="detailInfo">
            
        </div>
        <!--团购设置菜单-->
    	<div class="groupNav" >
        	<ul class="clearfix" >
            	<li class="groupSet">团购设置</li>
                <li class="shopManage">团购商品管理</li>
              <%--  <li class="orderManage">活动订单管理</li>--%>
            </ul>
        </div>
        
        <!--团购设置项-->
        <div class="groupSetArea" data-table="block" id="groupSet" style="display:none;">
        	<div class="groupSetArea-box">
                <div>
                    <h3>活动名称：</h3>
                    <label><input type="text" id="eventName"  placeholder="请输入活动名称" /></label>
                    <em>例如：微商城7月上旬团购</em>				
                </div>
                <div>
                    <h3>开始时间：</h3>
                    <label><input type="text" id="beginTime"  placeholder="2014-06-13  09:00" /></label>
                    <em>示例：2014.06.10-09:00</em>				
                </div>
                <div>
                    <h3>结束时间：</h3>
                    <label><input type="text" id="endTime"  placeholder="2014-06-13  09:00" /></label>
                    <em>示例：2014.06.10-09:00</em>				
                </div>
                <div>
                    <h3>上架状态：</h3>
                    <label id="status" class="selectBox">
                    	<span id="statusText">上架</span>
                    	<span class="dropList">
                        	<span data-status="10">不在商城中显示</span>
                        	<span data-status="20">在商城中显示</span>
                        </span>
                    </label>
                    <em>上架商品显示在推出的活动中</em>				
                </div>
                <a href="javascript:;" id="btnSureUpdate" class="commonBtn">确定</a>
            </div>
        </div>
        
        <!--团购商品管理-->
        <div class="groupShopManage" data-table="block" id="shopManage">
        	<div class="titleBox clearfix" style="height:50px;">
            	<h2 id="textName">团购商品管理</h2>
                <a class="addShopBtn" href="javascript:;"></a>
            </div>
            <div id="goodsList" class="groupShopManage-list">
            	
                
            </div>
        </div>
        
    </div>
</div>


<!-- 弹层，活动产品新规则 -->
<div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
<div id="addNewRules" class="jui-dialog jui-dialog-addRule" style="display:none;">
    <div class="jui-dialog-tit">
    	<h2>商品编辑</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div id="goodsInfo" class="jui-dialog-addRule-info">
    	<p class="shopTit">商品信息</p>
        <div>
        	<div class="quotaBox">
                <em>每人限购</em>
                <p class="inputBox"><input type="text" value=""></p>
            </div>
        	<p class="shopName"></p>
        </div>
    </div>
    <div class="ruleTitText">规格信息</div>
    <div id="rules" class="jui-dialog-addRule-edit">
    	

    </div>
    <div class="btnWrap">
        <a href="javascript:;" id="saveRules" class="commonBtn">提交</a>
        <a href="javascript:;" class="commonBtn cancelBtn hintClose">取消</a>
    </div>
</div>
<!--添加商品-->
<div id="addNewGoods" class="jui-dialog jui-dialog-addShop" style="display:none;">
    <div class="jui-dialog-tit">
    	<h2>添加商品</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div class="contentArea">
    	<div class="searchWrap">
        	<span class="tit">请选择分类</span>
            <div class="selectBox">
            	<em></em>
            	<p id="categoryText">全部</p>
                <div id="category" class="dropList">
                	
                </div>
            </div>
            <label class="inputBox"><input type="text" id="keyword" placeholder="商品名称"></label>
            <a href="javascript:;" id="searchBtn" class="commonBtn searchBtn">查询</a>
        </div>
        <div class="searchResult clearfix">
        	<div class="title">
            	<h2 class="classify">商品分类</h2>
                <h2 class="shopName">商品名</h2>
            </div>
            <div id="searchList" class="searchList">
            	
            </div>
            
        </div>
        <div id="kkpager" style="text-align:right;"></div>
    </div>
</div>
<!--图片上传-->
<div id="uploadPic" class="jui-dialog jui-dialog-upload" style="display: none;">
    <div class="jui-dialog-tit">
    	<h2>图片上传</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div  class="uploadArea" >
    	<div class="picView" id="uploadContainer">
        
        </div>
    </div>
</div>
<!--商品item模板-->
<script id="tpl_goodsItem" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <div data-position="<#=i+1#>" class="groupShopManage-list-item" data-fixed="<#=i+1#>" data-itemid="<#=item.ItemID#>" data-eventmappingid="<#=item.EventItemMappingId#>">
            
                <p class="picWrap"><img src="<#=item.ImageUrl#>" /></p>
                <div class="textInfo">
                    <div class="topText">
                        <h3 class="tit"><#=item.ItemName#></h3>
                        <a class="delShopBtn" href="javascript:;"></a>
                        <# if(item.SinglePurchaseQty){ #>
                        <span  style="padding:5px;font-weight: bold;color:red;font-weight: bold;">每人限购<#=item.SinglePurchaseQty #>件</span>
                        <#}#>
                    </div>
                    <div class="handleShop">
                        <a class="addSizeBtn" href="javascript:;"></a>
                        <span class="uploadPicBtn"></span>
                        <span class="moveUp <#=(i==0)?"first":""#>">上移</span>
                        <span class="moveDown <#=(i==list.length-1)?"last":""#>">下移</span>
                    </div>
                
            </div>
            <div class="showHide">
            <#var SkuList=(item.SkuList&&(item.SkuList.length>0))?item.SkuList:[];for(var j=0;j<SkuList.length;j++){ var sku=SkuList[j];#>
                <div class="groupShopNorms" data-position="<#=j+1#>" style="display:none;"  data-mappingid="<#=sku.MappingId#>">
                    <div class="sizeText">
                        <a class="delSizeBtn" href="javascript:;"></a>
                        <h3>规格<label class="position"><#=(j+1)+": "#></label><label><#=sku.SkuName#></label></h3>
                        <em>在售</em>
                    </div>
                    <div class="editArea mtmb">
                        <label><em>商品数量：</em><input type="text" value="<#=sku.Qty#>" disabled  /></label>
                        <label class="mlmr"><em>已售数量基数：</em><input type="text" value="<#=sku.KeepQty#>" disabled /></label>
                        <span class="commonMinBtn modifyBtn">修改</span>
                        <div class="btnWrap">
                            <span class="commonMinBtn commonSave">保存</span>
                            <span  class="commonMinBtn commonCancel">取消</span>
                        </div>
                    </div>
                    <div class="editArea">
                        <label><em>真实销量：</em><input type="text" value="<#=sku.SoldQty#>" disabled /></label>
                        <label class="mlmr"><em>当前库存：</em><input type="text" value="<#=sku.InverTory#>" disabled /></label>
                    </div>
                    <div class="editArea" style="margin-top: 5px;">
                        <label><em>原价：</em><input type="text" value="<#=sku.price#>" disabled /></label>
                        <label class="mlmr"><em>活动价：</em><input type="text" value="<#=sku.SalesPrice#>" disabled /></label>
                    </div> 
                </div>
            <#}#>
            <a href="javascript:;" class="_showMore">
                <p class="clickMore">下拉查看更多</p>
            </a>
            </div>
        </div>
    <#}#>
</script>
<script id="tpl_rule" type="text/html">
 <#for(var i=0;i<list.length;i++){ var item=list[i];#>
    <div class="item <#=item.IsSelected=="true"?"on":""#>" data-mappingid="<#=item.MappingId#>" data-skuid="<#=item.SkuID#>">
        <div class="editArea mtmb">
            <label  class="checkBox"><span></span><#=item.SkuName#></label>
            <label><em>商品数量：</em><input type="text" disabled value="<#=item.Qty#>" /></label>
            <label class="ml"><em>已售数量基数：</em><input disabled type="text" value="<#=item.KeepQty#>" /></label>
        </div>
        <div class="editArea pl">
            <label><em>原价：</em><input type="text" value="<#=item.price#>" disabled  /></label>
            <label class="mlmr"><em>活动价：</em><input disabled type="text" value="<#=item.SalesPrice#>" /></label>
        </div>
    </div>
<#}#>
</script>
<script id="tpl_category" type="text/html">
    <span data-categoryid="">全部</span>
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <span data-categoryid="<#=item.CategoryId#>"><#=item.CategoryName#></span>
    <#}#>
</script>
<script id="tpl_goods_list" type="text/html">
    <ul>
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <li data-itemid="<#=item.ItemId#>" data-name="<#=item.ItemName#>">
            <p class="classify"><#=item.ItemCategoryName#></p>
            <p class="shopName"><#=item.ItemName#></p>
        </li>
    <#}#>
    </ul>
</script>

<script id="tpl_detail" type="text/html">
     <div class="clearfix" style="height:20px;">
                <h3 class="tit t-overflow"><#=item.EventName#></h3>
                <span class="shelf"><#=item.EventStatus#></span>
                
            </div>
            <div class="other">
                <p class="timeSlot"><span class="startTime"><#=item.BeginTime#></span>至<span class="endTime"><#=item.EndTime#></span></p>
                <span class="surplus">剩余商品<strong><#=item.RemainQty#></strong></span>
                <span>总库存<strong><#=item.Qty#></strong></span>
            </div>
            <!--<a href="GroupList.aspx?pageType=<#=item.EventTypeId#>&pageName=<#=item.pageName#>">
                <p class="clickMore">查看更多活动</p>
            </a>-->

</script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/GroupBuy/js/main.js"></script>
</asp:Content>