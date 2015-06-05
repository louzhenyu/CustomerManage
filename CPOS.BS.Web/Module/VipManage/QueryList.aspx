<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="<%=StaticUrl+"/module/VipManage/css/reset-pc.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/VipManage/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/kkpager.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/jquery.datetimepicker.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/artDialog.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/tip-yellowsimple/tip-yellowsimple.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/zTreeStyle/zTreeStyle.css"%>" rel="stylesheet" type="text/css" />
        <%--<link href="../static/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />--%>
        <style type="text/css">

            .commonSelectWrap input {
                border:1px solid #ccc;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<body cache>
<div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
    <!-- 内容区域 -->
    <div class="contentArea_vipquery">
        <!--个别信息查询-->
        <div class="queryTermArea" id="simpleQuery" style="display:inline-block;width: 100%; " >
            <!--<h2 class="commonTitle">会员查询</h2>-->
            <div class="item clearfix">
                <div class="moreQueryWrap">
                	<a href="javascript:;" class="commonBtn queryBtn">查询</a>
                    <a href="javascript:;" class="more">更多查询条件</a>
                </div>
            </div>
        </div>
        
        <!--全部信息查询-->
        <div class="allQueryTermArea" id="allQuery" style="display:none; height:30px">
        	<a href="javascript:;" class="drawBtn slideUp">收起</a>
        	
        </div>
        
        <!--设置菜单-->
        <!--
        <div class="subMenu" style="display:none">
            <ul class="clearfix">
                <li class="modify">修改活动</li>
                <li class="apply on">报名管理</li>
                <li class="sign">签到管理</li>
            </ul>
        </div>
        -->
        <!--表格操作按钮-->
        <div id="menuItems" class="tableHandleBox">
            <span class="commonBtn _addVip">添加新会员</span>
            <span class="commonBtn _exportVip">全部导出</span>
            <%--<span class="commonBtn _delVip">删除</span>--%>
        </div>    
        <div class="tableWrap">
            <div class="tablehandle">
                <!--
                <div class="selectBox">
                    <span class="text">按最近时间升序</span>
                    <div class="selectList">
                        <p>按最近时间降序</p>
                        <p>按最近时间升序</p>
                    </div>
                </div>
                -->
                <h3 class="count">共查询到<span id="resultCount">--</span>条数据</h3>  
                <!--
                <div class="selectBox fl">
                    <span class="text">操作</span>
                    <div class="selectList">
                        <p>操作1</p>
                        <p>操作2</p>
                    </div>
                </div>
                
                <div class="selectBox filterIcon fl">
                    <span class="text">筛选</span>
                    <div class="selectList">
                        <p>筛选1</p>
                        <p>筛选2</p>
                    </div>
                </div>
                -->
            </div>
            <table class="dataTable" style="display:inline-table;">
                <thead  id="thead">
                    <!--
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>操作</th>
                        <th>会员编号</th>
                        <th>姓名</th>
                        <th>微信昵称</th>
                        <th>会籍店</th>
                        <th>状态</th>
                        <th>等级</th>
                        <th>积分</th>
                        <th>手机号</th>
                    </tr>-->
                </thead>
                <tbody id="content">
                    <!--
                    <tr>
                        <td class="checkBox on"><em></em></td>
                        <td class="seeIcon"></td>
                        <td class="fontC">HYVIP00001</td>
                        <td>王晓刚</td>
                        <td>大黄蜂</td>
                        <td>静安店</td>
                        <td>正常</td>
                        <td>高级</td>
                        <td class="fontF">998</td>
                        <td class="fontF">138****7765</td>
                    </tr>-->
                </tbody>
            </table>
            <div id="pageContianer">
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>
    </div>
</div>



<!-- 弹层,遮罩 -->
<div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
<!--弹层,添加新会员-->
<div class="jui-dialog jui-dialog-addNewVip" id="addNewVip" style="display:none">
    <div class="jui-dialog-tit">
    	<h2>添加新会员</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div class="promptContent" style="height:320px;">
        
    </div>
    <div class="btnWrap">
        <a href="javascript:;" class="commonBtn saveBtn">保存</a>
        <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
    </div>
</div>
<!-- 弹层，选择标签-->
<div class="jui-dialog jui-dialog-selectTag" id="dialogLabel" style="display:none">
    <div class="jui-dialog-tit">
    	<h2>选择标签</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div class="promptContent">

    </div>
    <div class="btnWrap">
        <a href="javascript:;" class="commonBtn _sureBtn">确定</a>
        <a href="javascript:;" class="commonBtn cancelBtn hintClose">取消</a>
    </div>
</div>
<!--查询动态form配置模板-->
<script id="tpl_dyniform" type="text/html">
<div class="commonSelectWrap" style="float:right;">
    <a href="javascript:;" class="commonBtn queryBtn">查询</a>
</div>
    
        <#var subRoot=Data.Root.SubRoot;subRoot=subRoot?subRoot:[];#>
        <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];#>
            <#if(item.ConditionOrder>=20){#>
                <#if(item.DisplayType==1||item.DisplayType==7){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <label class="searchInput"><input data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text"  data-flag=save value=""></label>
                    </div>
                <#}#>
                <#if(item.DisplayType==5){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <div class="selectBox">
                            <span data-flag class="text" data-forminfo="<#=JSON.stringify(item)#>" name="vipinfo" optionid="">全部</span>
                            <div class="selectList">
                                <p optionid="">全部</p>
                                <#if(item.Fn instanceof Array){#>
                                    <#for(var fo=0;fo<item.Fn.length;fo++){var sel=item.Fn[fo];#>
                                        <p optionid="<#=sel.OptionID#>"><#=sel.OptionValue#></p>
                                    <#}#>
                                <#}else{#>
                                    <p optionid="<#=item.Fn.OptionID#>"><#=item.Fn.OptionValue#></p>
                                <#}#>    
                            </div>
                        </div>
                    </div>
                <#}#>
                <#if(item.DisplayType==2){#>
                    <label class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <label style="margin-left: 5px;"><input style="width:75px;text-indent:0px;padding: 0px;border: 1px solid #ccc;" data-order="left" data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text"  data-flag=save value=""></label>
                    <label>   -</label>
                        <label><input style="width:75px;text-indent:0px;padding: 0px;border: 1px solid #ccc;float:none" data-order="right" data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text"  data-flag=save value=""></label>
                    </div>
                <#}#>
                <#if(item.DisplayType==205){ #>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <label class="searchInput"><input data-flag data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo"  type="text" value=""></label>
                        <ul id="ztree<#=Math.floor(Math.random()*9999999999+1)#>" class="ztree" data-forminfo="<#=JSON.stringify(item.Fn[0].Tree)#>" style="display:none;position: absolute;left: 120px;background:#FFF;margin-top: 31px;width:173px;z-index:100;"></ul>
                    </div>
                <#}#>
                <#if(item.DisplayType==6){#>
                    <label class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <label style="margin-left: 5px;"><input data-flag data-order="left" style="width:75px;text-indent:0px;padding: 0px;" class="datepicker" data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text" value=""></label>
                         <label> ~</label>
                        <label><input data-flag data-order="right" style="width:75px;text-indent:0px;padding: 0px; float:none"  class="datepicker" data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text" value=""></label>
                    </div>
                <#}#>
            <#}#>
        <#}#>
    <div class="fromWrap" style="margin-top:15px">
        <div class="labelWrap">
            <em class="tit">会员标签：</em>
            <a href="javascript:;" id="showSelectLabel" style="color: #3f97fe;font-size: 14px;">点击此处显示标签</a>
            <div class="pointerArea clearfix" id="lables"  style="display:none;">
                    
            </div>
        </div>
    </div>    
</script>
<!--表示的是基础查询条件-->
<!--DisplayType   1  7  -->
<!--DisplayType   2  数值类型  给范围-->
<!--DisplayType   5表示下拉-->
<!--DisplayType   6表示生日  查询给范围-->
<!--DisplayType   205表示tree-->
<script id="tpl_simpleDyniform" type="text/html">
    <#var subRoot=Data.Root.SubRoot;subRoot=subRoot?subRoot:[];#>
    <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];#>
        <#if(item.ConditionOrder<20){#>
            <#if(item.DisplayType==1||item.DisplayType==7||item.DisplayType==3|| item.DisplayType==4){#>
                <div class="commonSelectWrap">
                    <em class="tit"><#=item.ColumnDesc#>：</em>
                    <label class="searchInput"><input data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>" data-flag  name="vipinfo"  type="text" value=""></label>
                </div>
            <#}#>
            <#if(item.DisplayType==5){#>
                <div class="commonSelectWrap">
                    <em class="tit"><#=item.ColumnDesc#>：</em>
                    <div class="selectBox" >
                        <span class="text" name="vipinfo" data-flag data-forminfo="<#=JSON.stringify(item)#>" optionid="">全部</span>
                        <div class="selectList">
                            <p optionid="">全部</p>
                            <#if(item.Fn instanceof Array){#>
                                <#for(var j=0;j<item.Fn.length;j++){var sel=item.Fn[j];#>
                                    <p optionid="<#=sel.OptionID#>"><#=sel.OptionValue#></p>
                                <#}#>
                            <#}else{#>
                                <p optionid="<#=item.Fn.OptionID#>"><#=item.Fn.OptionValue#></p>
                            <#}#>    
                        </div>
                    </div>
                </div>
            <#}#>
            <#if(item.DisplayType==205){ #>
                <div class="commonSelectWrap">
                    <em class="tit"><#=item.ColumnDesc#>：</em>
                    <label class="searchInput"><input data-flag data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo"  type="text" value=""></label>
                    <ul id="ztree<#=Math.floor(Math.random()*9999999999+1)#>"  data-forminfo="<#=JSON.stringify(item.Fn[0].Tree)#>" class="ztree" style="display:none;position: absolute;left: 120px;background:#F3F3F3;margin-top: 31px;z-index:100;max-height:250px;overflow-x: hidden;  overflow-y: auto; "></ul>
                </div>
            <#}#>
            <#if(item.DisplayType==2){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <div class="searchTime">
                        <label><input data-order="left" style="width:75px;text-indent:0px;padding: 0px;" data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text"  data-flag=save value=""></label>
                       <label>-</label>
                        <label><input data-order="right" style="width:75px;text-indent:0px;padding: 0px;float:none" data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text"  data-flag=save value=""></label>
                      </div>
                    </div>
                <#}#>
            <#if(item.DisplayType==6){#>
                <div class="commonSelectWrap">
                    <em class="tit"><#=item.ColumnDesc#>：</em>
                    <div class="searchTime">
                    <label ><input data-order="left" data-flag style="width:75px;text-indent:0px;padding: 0px;" class="datepicker" data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text" value=""></label>
                    <label>-</label>
                    <label><input data-flag data-order="right" style="width:75px;text-indent:0px;padding: 0px;float:none"  class="datepicker" data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="vipinfo" type="text" value=""></label>
                   </div>
                </div>
            <#}#>
        <#}#>
    <#}#>
</script>
<!--添加会员的动态表单-->
<script id="tpl_addVipForm" type="text/html">
    <#var subRoot=Data.Root.SubRoot;subRoot=subRoot?subRoot:[];#>
        <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];#>
                <#if(item.DisplayType==1||item.DisplayType==7||item.DisplayType==3 || item.DisplayType==4){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：
                            <#if(item.IsMustDo==1){#>
                            <span style="color: red;position: relative;top: 3px;">*</span>
                            <#}#>
                        </em>
                        <label class="searchInput"><input data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="newvipinfo" type="text" value=""></label>
                    </div>
                <#}#>
                <#if(item.DisplayType==5){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：
                            <#if(item.IsMustDo==1){#>
                            <span style="color: red;position: relative;top: 3px;">*</span>
                            <#}#>
                        </em>
                        <div class="selectBox">
                            <span class="text" optionid="" data-forminfo="<#=JSON.stringify(item)#>" name="newvipinfo">请选择</span>
                            <div class="selectList">
                                <p optionid="">请选择</p>
                                <#if(item.Fn instanceof Array){#>
                                     <#if(item.Fn&&item.Fn.length){#>
                                        <#for(var j=0;j<item.Fn.length;j++){var sel=item.Fn[j];#>
                                            <p optionid="<#=sel.OptionID#>"><#=sel.OptionValue#></p>
                                        <#}#>
                                    <#}#>
                                <#}else{#>
                                    <p optionid="<#=item.Fn.OptionID#>"><#=item.Fn.OptionValue#></p>
                                <#}#>        
                            </div>
                        </div>
                    </div>
                <#}#>
                <#if(item.DisplayType==205){ #>
                    <div class="commonSelectWrap">
                       <em class="tit"><#=item.ColumnDesc#>：
                            <#if(item.IsMustDo==1){#>
                            <span style="color: red;position: relative;top: 3px;">*</span>
                            <#}#>
                        </em>
                        <label class="searchInput"><input  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="newvipinfo"  type="text" value=""></label>
                        <ul id="ztree<#=Math.floor(Math.random()*9999999999+1)#>"  data-forminfo="<#=JSON.stringify(item.Fn[0].Tree)#>" class="ztree" style="display:none;position: absolute;left: 120px;background:#FFF;margin-top: 31px;width:173px;z-index:100;"></ul>
                    </div>
                <#}#>
                 <#if(item.DisplayType==2){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：</em>
                        <label class="searchInput"><input data-order="left" data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="newvipinfo" type="text"  data-flag=save value=""></label>
                    </div>
                <#}#>
                <#if(item.DisplayType==6){#>
                    <div class="commonSelectWrap">
                        <em class="tit"><#=item.ColumnDesc#>：
                            <#if(item.IsMustDo==1){#>
                            <span style="color: red;position: relative;top: 3px;">*</span>
                            <#}#>
                        </em>
                        <label class="searchInput"><input class="datepicker" readonly data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="newvipinfo" type="text" value=""></label>
                    </div>
                <#}#>
        <#}#>


</script>
<!--
    <div class="inputWrap">
        <em class="tit">会员编号：</em>
        <p class="inputBox"><input type="text" placeholder=""></p>
    </div>-->
<!--头部名称-->
<script id="tpl_thead" type="text/html">
    <th class="selectListBox">
        选择
        <div class="minSelectBox">
            <em class="minArr"></em>
            <p class="_selCurPage">全选本页</p>
            <p class="_selAllPage">全选所有页</p>
            <p class="_cancelSel">取消选择</p>
        </div>
    </th>
    <th>操作</th>
    <#for(var i in obj){#>
        <th><#=obj[i]#></th>
    <#}#>
</script>
<!--数据部分-->
<script id="tpl_content" type="text/html">
    <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
    <tr data-id="<#=list.otherItems[i].VIPID #>">
        <td class="checkBox" ><em></em></td>
        <td class="seeIcon"></td>
        <#for(var e in idata){#>
        <td>
             <#var d= idata[e];if(e.toLowerCase()=='age' && d==0) d='';#>
             <a data-href="/module/vipmanage/Vipdetail.aspx?vipId=<#=list.otherItems[i].VIPID#>&mid=<#=mid#>" href="javascript:;"><#=d#></a>
       </td>
        <#}#>
    </tr>
    <#} #>
</script>
<!--添加新标签模板   SearchColumns长度不为0  表示修改过来的  否则为第一次过来-->
<script id="tpl_newItem" type="text/html">
<#if(VipSearchTags.length){#>
   <#for(var sc=0,exlength=VipSearchTags.length;sc<exlength;sc++){ var sitem=VipSearchTags[sc];#>
    <div class="tagWrap">
            <div class="jui-dialog-selectBox selectBox">
                <span class="text kuohao" data-direct="right" name="condition"><#=sitem.LeftBracket#></span>
                <div class="selectList">
                    <p class="kuohaoItem">
                    
                    </p>
                    <p class="kuohaoItem">(</p>
                </div>
            </div>
            <div class="jui-dialog-selectBox selectBox">
                <span class="text" name="condition"><#=sitem.EqualFlagStr#></span>
                <div class="selectList">
                    <p>包含</p>
                    <p>不含</p>
                </div>
            </div>
            <div class="selectValueBox">
            	<input type="text" readonly value="<#=sitem.TagName#>" tagName="<#=sitem.TagName#>" tagId="<#=sitem.TagId#>" name="condition"  class="_showTips"/>
                <!--<span class="tipTit"></span>-->
                <div class="selectValueList">
                	<em class="pointer03"></em>
                	<ul>
                        <#for(var i=0,length=tagType.length;i<length;i++){ var tagTypeItem=tagType[i];#>
                            <li data-tagid="<#=tagTypeItem.TagTypeId#>" class="<#=i==0?"on":""#>"><a href="javascript:;"><#=tagTypeItem.TagTypeName#></a></li>
                        <#}#>
                    </ul>
                    <div class="selectValueInfo">
                        <#for(var j=0,length2=tagList.length;j<length2;j++){ var tagListItem=tagList[j];#>
                            <#if(tagListItem.TagTypeId==tagType[0].TagTypeId){#>
                    	        <p data-taginfo="<#=JSON.stringify(tagListItem)#>" class="_showTips" title="<#=tagListItem.TagDesc?tagListItem.TagDesc:""#>">
                        	        <span><#=tagListItem.TagName#></span>
                                    <!--<#if(tagListItem.TagDesc){#>
                                        <span class="tipTit2"><#=tagListItem.TagDesc#></span>
                                    <#}#>-->
                                </p>
                            <#}#>
                        <#}#>
                    </div>
                </div>
            </div>
            <div class="jui-dialog-selectBox selectBox" >
                <span class="text kuohao" name="condition" data-direct="right"><#=sitem.RightBracket#></span>
                <div class="selectList">
                    <p class="kuohaoItem">
                    
                    </p>
                    <p class="kuohaoItem">)</p>
                </div>
            </div>
            <div class="jui-dialog-selectBox  selectBox">
                <span class="text" name="condition"><#=sitem.AndOrString#></span>
                <div class="selectList">
                    <p>并且</p>
                    <p>或者</p>
                </div>
            </div>
            <div class="handleWrap">
            	<a href="javascript:;" class="addIcon"></a>
                <a href="javascript:;" class="minusIcon"></a>
            </div>
        </div>
    <#}#>
<#}else{#>
<div class="tagWrap">
            <div class="jui-dialog-selectBox selectBox">
                <span class="text kuohao" data-direct="right" name="condition"></span>
                <div class="selectList">
                    <p class="kuohaoItem">
                    
                    </p>
                    <p class="kuohaoItem">(</p>
                </div>
            </div>
            <div class="jui-dialog-selectBox selectBox">
                <span class="text" name="condition">包含</span>
                <div class="selectList">
                    <p>包含</p>
                    <p>不含</p>
                </div>
            </div>
            <div class="selectValueBox">
            	<input type="text" readonly   name="condition"  class="_showTips"/>

                <div class="selectValueList">
                	<em class="pointer03"></em>
                	<ul>
                        <#for(var i=0,length=tagType.length;i<length;i++){ var tagTypeItem=tagType[i];#>
                            <li data-tagid="<#=tagTypeItem.TagTypeId#>" class="<#=i==0?"on":""#>"><a href="javascript:;"><#=tagTypeItem.TagTypeName#></a></li>
                        <#}#>
                    </ul>
                    <div class="selectValueInfo">
                        <#for(var j=0,length2=tagList.length;j<length2;j++){ var tagListItem=tagList[j];#>
                            <#if(tagListItem.TagTypeId==tagType[0].TagTypeId){#>
                    	        <p data-taginfo="<#=JSON.stringify(tagListItem)#>" class="_showTips" title="<#=tagListItem.TagDesc?tagListItem.TagDesc:""#>">
                        	        <span><#=tagListItem.TagName#></span>
                                </p>
                            <#}#>
                        <#}#>
                    </div>
                </div>
            </div>
            <div class="jui-dialog-selectBox selectBox" >
                <span class="text kuohao" name="condition" data-direct="right"></span>
                <div class="selectList">
                    <p class="kuohaoItem">
                    
                    </p>
                    <p class="kuohaoItem">)</p>
                </div>
            </div>
            <div class="jui-dialog-selectBox selectBox">
                <span class="text" name="condition">并且</span>
                <div class="selectList">
                    <p>并且</p>
                    <p>或者</p>
                </div>
            </div>
            <div class="handleWrap">
            	<a href="javascript:;" class="addIcon"></a>
                <#if(!!!isFirst){#><a href="javascript:;" class="minusIcon"></a><#}#>
            </div>
        </div>
<#}#>
</script>
<!--标签列表-->
<script id="tpl_tagList" type="text/html">
<#for(var j=0,length2=tagList.length;j<length2;j++){ var tagListItem=tagList[j];#>
    <#if(tagListItem.TagTypeId==typeId){#>
        <p data-taginfo="<#=JSON.stringify(tagListItem)#>" class="_showTips" title="<#=tagListItem.TagDesc?tagListItem.TagDesc:""#>">
            <span><#=tagListItem.TagName#></span>
        </p>
    <#}#>
<#}#>
</script>



<!--动态标签拼接-->
<script id="tpl_lables" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){ var item=list[i];#>
    <#if(item.LeftBracket=="("){#>
        <img src="images/leftBracket.png">
    <#}#>
    <span class="minF"><#=item.EqualFlagStr#></span>
    <span class="pointerIcon <#=(item.TagName.length>=5)?"weird":""#>"><#=item.TagName#></span>
    <#if(item.RightBracket==")"){#>
        <img src="images/rightBracket.png">
    <#}#>
    <#if(i!=(length-1)){#>
        <span class="digF"><#=item.AndOrString#></span>
    <#}#>
<#}#>
<a href="javascript:;" class="commonBtn modifyBtn">修改</a>
<a href="javascript:;" class="_resetLables" style="color:#3f97fe">重置</a>
</script>
<!--
    <span class="minF">不包含</span>
    <span class="pointerIcon">中价值</span>
    <span class="digF">且</span>
    <span class="pointerIcon weird">活跃会员中价值高价值</span>
    <a href="javascript:;" class="commonBtn modifyBtn">修改</a>
    <a href="javascript:;" class="commonBtn recentBtn">近期使用</a>
    <img src="images/rightBracket.png">
-->
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/VipManage/js/main.js"%>" ></script>
    </body>
</asp:Content>