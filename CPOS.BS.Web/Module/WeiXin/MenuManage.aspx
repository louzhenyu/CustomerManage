<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>微信菜单管理</title>
        <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()"%>" />
    <link href="<%=StaticUrl+"/module/WeiXin/css/style.css?v=Math.random()"%>" rel="stylesheet" type="text/css" />
    
    <link href="<%=StaticUrl+"/Module/static/css/pagination.css"%>" rel="stylesheet" type="text/css" />

     <style>
            .groupWrapper
            {
	            width: 32%;
	            float: left;
	            margin-right: 1%;
	            min-height: 400px;
            }
            .serializer
            {
	            clear: both;
            }
            .groupItem
            {
	            margin-bottom: 20px;
            }
            .groupItem .itemHeader
            {
	            line-height: 28px;
	            background-color: #DAFF9F;
	            border-top: 2px solid #B5EF59;
	            color: #000;
	            padding: 0 10px;
	            cursor: move;
	            font-weight: bold;
	            font-size: 16px;
	            height: 28px;
	            position: relative;
            }

            .groupItem .itemHeader a
            {
	            position: absolute;
	            right: 10px;
	            top: 0px;
	            font-weight: normal;
	            font-size: 11px;
	            text-decoration: none;
            }
            .sortHelper
            {
	            border: 3px dashed #666;
	            width: auto !important;
            }
            .groupWrapper p
            {
	            height: 1px;
	            overflow: hidden;
	            margin: 0;
	            padding: 0;
            }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<!DOCTYPE HTML>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">
</head>
<div class="weChatTitle" id="section" data-js="js/MenuManage">
	<span class="digTit">微信菜单管理</span>
</div>

<div class="weChatMenuManage" >
	<div class="commonTitleWrap" id="weixinAccount">
    	<h2>微信账号</h2>
        <select class="selectBox">
            
        </select>
        <span id="publishMenu" class="releaseBtn">发布</span>
    </div>
    
    <!--菜单区域-->
    <div id="menuArea" class="leftArea">
    	
    </div>
    
    
    
    
    <div class="rightArea" id="contentArea">
    	<div class="commonTitleWrap">
            <h2 id="menuContent">微官网</h2>
            <span style="display:none;" id="delBtn" class="delBtn">删除</span>
            <span id="btnSaveData" class="saveBtn">保存</span>
        </div>
        
        <div class="tempEditArea">
            <div class="commonItem clearfix">
                <span class="tit">菜单名称</span>
                <div class="handleWrap">
                    <input type="text" id="menuTitle" class="inputBox" />
                </div>
            </div>
    
            <div class="commonItem clearfix">
                <span class="tit">序号</span>
                <div class="handleWrap">
                    <input type="text" id="no" class="inputBox" />
                </div>
            </div>
            
            <div class="commonItem clearfix">
                <span class="tit">是否启用</span>
                <div class="handleWrap">
                    <select class="selectBox" id="isUse">
                        <option selected value="1">启用</option>
                        <option value="0">不启用</option>
                    </select>
                </div>
            </div>
            
            <div class="commonItem clearfix" id="unionCategory">
                <span class="tit">点击关联项</span>
                <div class="handleWrap">
                    <select id="category" class="selectBox">
                        <option selected value="1">链接</option>
                        <option value="2">回复消息</option>
                        <option value="3">系统模块</option>
                    </select>
                </div>
            </div>
            
            
            <div class="commonItem clearfix" name="elems" id="contentUrl">
                <span class="tit">链接地址</span>
                <div class="handleWrap">
                    <input type="text" id="urlAddress" class="inputBox" />
                </div>
            </div>
            
            
            <!-- 动态信息 -->
            <div class="commonItem clearfix hide" id="message" name="elems">
                <span class="tit">消息类型</span>
                <div class="handleWrap">
                    <select class="selectBox">
                        <option selected value="1">文本</option>
                        <option value="2">图片</option>
                        <option value="3">图文</option>
                    </select>
                </div>
            </div>

            
            <!-- 选择图片 -->
            <div class="commonItem clearfix hide " id="toUploadImage"  name="elems">
                <span class="tit">选择图片</span>
                <div class="handleWrap uploadWrap">
                    <p class="viewPic" id="upImage">暂无</p>
                    <div class="info">
                        <p class="exp">建议上传尺寸为536*300,文件大小不超过100k的图片。</p>
                        <span class="uploadBtn" id="uploadImage">上传</span>
                    </div>
                </div>
            </div>
            
            <!-- 文本编辑 -->
            <div class="commonItem clearfix hide" id="contentEditor" name="elems">
                <span class="tit">文本编辑</span>
                <div class="handleWrap mt-45">
                	<textarea id="text"></textarea>
                </div>
            </div>
            
            
            <div class="commonItem clearfix hide" id="moduleName" name="elems">
                    <span class="tit">模块名</span>
                    <div class="handleWrap">
                        <select class="selectBox" data-load=false>
                            <option value="default" selected>请选择</option>
                            <option value="$HomeIndex">微商城</option>
                            <option value="EventList" >活动列表</option>
                            <option value="EventDetail">活动详页</option>
                            <option value="EventLottory">活动抽奖</option>
                            <option value="Recommend">推荐有礼</option>
                            <option value="NewsList">资讯列表</option>
                            <option value="NewsDetail">资讯详页</option>
                            <option value="GetVipCard">会员卡</option>
                            <option value="GoodsList">商城商品列表</option>
                            <option value="ShopList">门店列表</option>
                            <option value="RepositoryIndex">知识库</option>
                        </select>
                    </div>
                </div>
                
                <div class="commonItem clearfix hide" id="eventsType" save="toSave" name="elems">
                    <span class="tit">活动分类</span>
                    <div class="handleWrap">
                        <select class="selectBox">
                            <option value="0" selected>类别1</option>
                            <option value="1" selected>类别2</option>
                            <option value="1" selected>类别3</option>
                        </select>
                    </div>
                </div>
                <div class="commonItem clearfix hide" id="lottoryWay" save="toSave" name="elems">
                    <span class="tit">活动名称</span>
                    <div class="handleWrap">
                        <input type="text" class="inputBox">
                    </div>
                </div>
                <div class="commonItem clearfix hide" id="newsType" save="toSave" name="elems">
                    <span class="tit">资讯分类</span>
                    <div class="handleWrap">
                        <select class="selectBox">
                            <option value="0" selected>类别1</option>
                            <option value="1" selected>类别2</option>
                            <option value="1" selected>类别3</option>
                        </select>
                    </div>
                </div>
                <div class="commonItem clearfix hide" id="shopsType" save="toSave" name="elems">
                    <span class="tit">门店分类</span>
                    <div class="handleWrap">
                        <select class="selectBox">
                            <option value="0" selected>类别1</option>
                            <option value="1" selected>类别2</option>
                            <option value="1" selected>类别3</option>
                        </select>
                    </div>
                </div>
                <div class="commonItem clearfix hide" id="eventsDetail" save="toSave" name="elems">
                    <span class="tit">选择活动</span>
                    <div class="handleWrap">
                        <input type="text" class="inputBox">
                    </div>
                </div>
                
                <div class="commonItem clearfix hide" save="toSave" id="newsDetail" name="elems">
                    <span class="tit">选择资讯</span>
                    <div class="handleWrap">
                        <input type="text" class="inputBox">
                    </div>
                </div>
                
                
                <div class="commonItem clearfix hide" save="toSave" name="elems">
                    <span class="tit">资讯列表</span>
                    <div class="handleWrap">
                        <select class="selectBox">
                            <option selected>请选择</option>
                        </select>
                    </div>
                </div>
           <!-- 图文消息 -->
           <div class="imgTextMessage hide" id="imageContentMessage" name="elems">
           		<h2>提示:按住鼠标左键可拖拽排序图文消息显示的顺序 <b>已选图文</b>&nbsp;&nbsp;<b id="hasChoosed" style="color:Red">0</b>&nbsp;&nbsp;个</h2>
                <div class="list">
       
                    
                </div>
                <span class="addBtn">添加</span>	
            </div>

         </div> 
    </div>
</div>


<!-- 添加图文消息-弹层 -->
<div class="ui-mask hide" id="ui-mask"></div>
<div class="activeListPopupArea hide" id="chooseEvents">
	
</div>
<div class="addImgMessagePopup" id="addImageMessage">
	<div class="commonTitleWrap">
        <h2>添加图文消息</h2>
        <span class="cancelBtn">取消</span>
        <span class="saveBtn">确定</span>
    </div>
    
    <div class="addImgMessageWrap clearfix">
        <span class="tit">标题</span>
        <input type="text" id="theTitle" class="inputName" />
        <span class="tit">分类</span>
        <select class="selectBox" id="imageCategory">
        	<option selected>请选择</option>
        </select>
        <span class="queryBtn">查询</span>
    </div>
    
    
    <div class="radioList" id="imageContentItems">
        
        
    </div>
    
</div>
<div id="sortHelper" style="display: none;">&nbsp;</div>
<div id="dragHelper" style="position: absolute; display: none; cursor: move; list-style: none; overflow: hidden;">
</div>
<script id="pageTmpl" type="text/html">
    <div class="pagination">
      <a href="#" class="first" data-action="first">&laquo;</a>
      <a href="#" class="previous" data-action="previous">&lsaquo;</a>
      <input type="text" readonly="readonly" data-max-page="40" />
      <a href="#" class="next" data-action="next">&rsaquo;</a>
      <a href="#" class="last" data-action="last">&raquo;</a>
    </div>
</script>
<!--弹出的图文项-->
<script id="addImageItemTmpl" type="text/html">
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
            <div id="addImage_<#=(currentPage-1)*pageSize+i#>" data-id="addImage_<#=item.TestId#>" data-flag="<#=showAdd?'add':''#>" data-displayIndex="<#=i#>" data-obj="<#=JSON.stringify(item)#>" class="item">
        	    <em class="radioBox"></em>
                <p class="picWrap"><img src="<#=item.ImageUrl#>"></p>
                <div class="textInfo">
                    <span class="name"><#=item.Title?item.Title:"未设置图文名称"#></span>
                    <span><#=item.Text?item.Text:"未设置图文内容"#></span>
                    <span class="delBtn"></span>
                </div>
            </div>
        <#}#>
</script> 
<!--菜单模板-->
<script id="menuTmpl" type="text/html">
    <div class="modelBox">
        <div class="menuWrap">
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <span id="<#=item.MenuId#>" data-menu="<#=JSON.stringify(item)#>"  class="<#=i==0?'on':''#> <#=((item.Status==1)?'select':'')#>">
                    <b><#=item.Name#></b>
                    <div data-menu="<#=JSON.stringify(item)#>"  class="subMenuWrap">
                        <em class="pointer"></em>
                        <a href="javascript:;" data-parentId="<#=item.MenuId#>" class="addBtn">添加</a>
                        <#for(var j=0;j<item.SubMenus.length;j++){ var subItem=item.SubMenus[j];if(subItem!=null){#>
                            <a href="javascript:;"   data-menu="<#=JSON.stringify(subItem)#>" class="tempSubMenu <#=subItem.Status==1?'select':''#>"><#=subItem.Name#></a>
                            <#}#>
                        <#}#>
                    </div>
                </span>
            <#}#>
        </div>
    </div>
</script>
<script id="accountTmpl" type="text/html">
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value=<#=item.ApplicationId#>><#=item.WeiXinName#></option>
    <#}#>
</script>
<!--option模板-->
<script id="optionTmpl" type="text/html">
    <#showAll=showAll?showAll:false;if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value="<#=item.TypeId#>" data-obj=<#=JSON.stringify(item)#>><#=item.TypeName#></option>
    <#}#>
</script>
<!--option模板  模块-->
<script id="moduleTmpl" type="text/html">
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.PageID#>" value=<#=JSON.stringify(item)#>><#=item.ModuleName#></option>
    <#}#>
</script>
<!--活动类别模板-->
<script id="eventTypeTmpl" type="text/html">
    <#if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.EventTypeId#>" value=<#=JSON.stringify(item)#>><#=item.EventTypeName#></option>
    <#}#>
</script>
<!--资讯类别模板-->
<script id="NewsTypeTmpl" type="text/html">
    <#if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.NewsTypeId#>" value=<#=JSON.stringify(item)#>><#=item.NewsTypeName#></option>
    <#}#>
</script>
<!--弹出层的模板-->
<script id="popDivTmpl" type="text/html">
    <div class="commonTitleWrap">
    	<h2><#=topTitle#></h2>
        <span id="cancelBtn" class="cancelBtn">取消</span>
        <span id="saveBtn" class="saveBtn">确定</span>
    </div>

    <div class="activeQueryWrap clearfix">
        <span class="tit" ><#=popupName?popupName:"活动名称"#></span>
        <input id="eventName" type="text" class="inputName" />
        <span class="tit"><#=popupSelectName?popupSelectName:"活动分类"#></span>
        <select id="pop_eventsType" class="selectBox">
        	<option selected>请选择</option>
        </select>
        <span id="searchEvents" class="queryBtn">搜索</span>
    </div>
    
    <div class="activeListWrap">
        <table width="1038" border="1" id="itemsTable">
          
        </table>
    </div>

</script>
<!--弹层的每行数据-->
<script id="itemTmpl" type="text/html">
    <tr>
    <th width="65">操作</th>
    <#for(var i=0;i<title.length;i++){ var item=title[i];#>
    <th><#=item#></th>
    <#}#>
    </tr>
    <#if(type=="chooseNews"){#>
        <#if(itemList.length){#>
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <tr id="temp_<#=i#>">
                <td><input  data-id="temp_<#=i#>" type="radio" name="item" value="<#=JSON.stringify(item)#>"></td>
            
                    <td><#=item.NewsName#></td>
                    <td><#=item.NewsTypeName#></td>
                    <td><#=item.PublishTime#></td>
                </tr>
            <#}#>
        <#}#>
    <#}#>
     <#if(type=="chooseEvents"){#>
        <#if(itemList.length){#>
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <tr id="temp_<#=i#>">
                <td><input data-id="temp_<#=i#>" type="radio" name="item" value="<#=JSON.stringify(item)#>"></td>
            
                    <td><#=item.EventName#></td>
                    <td><#=item.EventTypeName#></td>
                    <td><#var result="";switch(item.EventStatus){case 10:result="未开始";break;case 20:result="运行中";break;case 30:result="暂停";break;case 40:result="停止";break;default:result="未定义";break;}#><#=result#></td>
                    <td><#=item.DrawMethod#></td>
                </tr>
            <#}#>
        <#}#>
    <#}#>
</script>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/WeiXin/js/main"%>"></script>
     
</asp:Content>
