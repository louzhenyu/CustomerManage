<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>图文素材_图文编辑</title>
        <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()" />
    <link href="css/style2.css?v=Math.random()" rel="stylesheet" type="text/css" />
    <link href="../static/css/pagination.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         .contentArea{margin-left:0px;float:left;}
		 #contentDigestText{padding:5px;border:1px solid #cecedc;}
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="imageTextEditArea" id="section" data-js="js/ImageContent">
	<div class="commonTitleWrap">
    	<h2>图文素材编辑详页</h2>
        <span id="cancelBtn" class="cancelBtn commonBtn w80">取消</span>
        <span id="btnSaveData" class="saveBtn commonBtn w80">保存</span>
    </div>
    
    <div class="titleUploadArea">
    	
        <div class="commonItem clearfix" id="weixinAccount" >
            <span class="tit">微信账号</span>
            <div class="handleWrap">
                <select class="selectBox weirdBox">
                    <option selected>杰亦特微信账号</option>
                </select>
            </div>
        </div>
        
		<div class="commonItem clearfix" >
        	<span class="tit">标题</span>
            <div class="handleWrap"><input type="text" id="imageTitle" class="inputBox" /></div>
        </div>
        <!--
        <div class="commonItem clearfix">
            <span class="tit">分类</span>
            <div id="imageCategory" class="handleWrap">
                <select class="selectBox">
                    <option selected value="-1">请选择</option>
                </select>
            </div>
        </div>
        -->
        <div class="commonItem clearfix">
            <span class="tit">显示封面图片</span>
            <div id="Div1" class="handleWrap">
                <select class="selectBox" id="showCover">
                    <option selected  value="1">是</option>
                    <option  value="0">否</option>
                </select>
            </div>
        </div>
        <div class="commonItem clearfix">
        	<span class="tit">封面图片</span>
            <div class="handleWrap uploadWrap">
            	<p class="viewPic" id="upImage">暂无</p>
				<div class="info">
                    <p class="exp">建议上传尺寸为536*300,文件大小不超过100k的图片。</p>
                    <span  class="uploadBtn" id="uploadImage">上传</span>
            	</div>
            </div>
        </div>
        
        <div class="commonItem clearfix"  id="contentDigest">
            <span class="tit">摘要</span>
            <div class="richTextBox">
            <textarea id="contentDigestText" name="content" style="width:700px;height:150px;"></textarea>
            </div>
        </div>
        
    </div>
    
    <div class="tempEditArea">
    	<div class="areaWrap">
        	<div class="commonItem clearfix">
                <span class="tit">点击关联到</span>
                <div class="handleWrap">
                    <select id="category" class="selectBox">
                       <option value="0" >请选择</option>
                        <option  value="1">链接</option>
                        <option selected value="2">详情页面</option>
                        <option value="3">系统模块</option>
                    </select>
                </div>
            </div>
                <div class="commonItem clearfix hide" id="moduleName" name="elems">
                    <span class="tit">模块名</span>
                    <div class="handleWrap">
                        <select class="selectBox" data-load=false>
                            <option value="default" selected>请选择</option>
                        </select>
                    </div>
                </div>
                
                <div class="commonItem clearfix hide" id="eventsType" save="toSave" name="elems">
                    <span class="tit">活动分类</span>
                    <div class="handleWrap">
                        <select class="selectBox">
                            <option value="-1" selected>请选择</option>
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
                            <option value="-1" selected>请选择</option>
                        </select>
                    </div>
                </div>
                <div class="commonItem clearfix hide" id="shopsType" save="toSave" name="elems">
                    <span class="tit">门店分类</span>
                    <div class="handleWrap">
                        <select class="selectBox">
                            <option value="-1" selected>请选择</option>
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
                            <option selected value="-1">请选择</option>
                        </select>
                    </div>
                </div>
                
                
                <div class="commonItem clearfix hide" name="elems" id="contentUrl">
                    <span class="tit">输入链接</span>
                    <div class="handleWrap">
                        <input type="text" id="urlAddress" class="inputBox" />
                    </div>
                </div>
                
                
                <div class="commonItem clearfix hide" name="elems" style="display:none;">
                    <span class="tit">模板选择</span>
                    <div class="handleWrap">
                        <div class="moudle">
                            <p><span class="mask"></span></p>
                            <span class="name">模板1</span>
                        </div>
                        <div class="moudle on">
                            <p><span class="mask"></span></p>
                            <span class="name">模板2</span>
                        </div>
                        <div class="moudle">
                            <p><span class="mask"></span></p>
                            <span class="name">模板3</span>
                        </div>
                    </div>
                </div>
                
                
                <div class="commonItem clearfix" name="elems" id="contentEditor">
                    <span class="tit">内容编辑</span>
                    <div class="richTextBox">
                    <textarea id="editor_id" name="content" style="width:700px;height:300px;"></textarea>
                    </div>
                    <div id="editLayer" class="handleLayer" style=""></div>
                </div>
                
                
                
            
            </div>
            
            
        </div>
    </div>
    
<!-- 弹层 -->
<div class="ui-mask hide" id="ui-mask"></div> 
<div class="activeListPopupArea hide" id="chooseEvents">
	
</div>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
<script id="pageTmpl" type="text/html">
    <div class="pagination">
      <a href="#" class="first" data-action="first">&laquo;</a>
      <a href="#" class="previous" data-action="previous">&lsaquo;</a>
      <input type="text" readonly="readonly" data-max-page="40" />
      <a href="#" class="next" data-action="next">&rsaquo;</a>
      <a href="#" class="last" data-action="last">&raquo;</a>
    </div>
</script>
<script id="accountTmpl" type="text/html">
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value=<#=item.ApplicationId#>><#=item.WeiXinName#></option>
    <#}#>
</script>
<!--option模板-->
<script id="optionTmpl" type="text/html">
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value="<#=item.TypeId#>" data-obj=<#=JSON.stringify(item)#>><#=item.TypeName#></option>
    <#}#>
</script>
<!--option模板  模块-->
<script id="moduleTmpl" type="text/html">
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
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
        <span id="cancelBtn" class="cancelBtn commonBtn w80">取消</span>
        <span id="saveBtn" class="saveBtn commonBtn w80">确定</span>
    </div>

    <div class="activeQueryWrap clearfix">
        <span class="tit" ><#=popupName?popupName:"活动名称"#></span>
        <input id="eventName" type="text" class="inputName" />
        <span class="tit"><#=popupSelectName?popupSelectName:"活动分类"#></span>
        <select id="pop_eventsType" class="selectBox">
        	<option selected>请选择</option>
        </select>
        <span id="searchEvents" class="queryBtn commonBtn w80">搜索</span>
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
    <#itemList=itemList?itemList:[];if(type=="chooseNews"){#>
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
</asp:Content>
