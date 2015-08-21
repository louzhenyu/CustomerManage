﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <body></body>--%>
    <div class="allPage" id="section" data-js="js/vipDetail">
        <!-- 内容区域 -->
        <div style="padding-top: 10px;">
            <div class="vipDetailInfo">
                <div class="commonTitle">
                    <a class="before" href="javascript:history.back();">会员查询</a>
                    <img src="images/pointerTit.png" />
                    <span class="after">会员详情</span>
                        <em class="tit">动态标签：</em>
                        <p id="labels" class="lab clearfix">
                        </p>
                    </div>
                </div>
                <div>
                    <div class="item">
                        <div class="itemBox">
                            <em class="tit">会员编号：</em>
                            <p class="itemText" id="vipCode">
                                --</p>
                        </div>
                        <div class="itemBox">
                            <em class="tit">会员姓名：</em>
                            <p class="itemText" id="vipName">
                                --</p>
                        </div>
                        <div class="itemBox">
                            <em class="tit">微信昵称：</em>
                            <p class="itemText" id="vipWeixin">
                                --</p>
                        </div>
                    </div>
                    <div class="item">
                        <div class="itemBox">
                            <em class="tit">会员等级：</em>
                            <p class="itemText" id="vipLevel">
                                --</p>
                        </div>
                        <div class="itemBox">
                            <em class="tit">会籍店：</em>
                            <p class="itemText" id="vipUnit">
                                --</p>
                        </div>
                        <div class="itemBox">
                            <em class="tit">会员积分：</em>
                            <p class="itemText" id="vipPoint">
                                0积分</p>
                        </div>
                    </div>
                    <div class="item line">
                        <div class="itemBox">
                            <em class="tit">余额：</em>
                            <p class="itemText" id="vipBalance">
                                0元</p>
                        </div>
                    </div>
                    <div class="tagItem">
                        <em class="tit">动态标签：</em>
                        <p id="labels" class="lab clearfix">
                        </p>
                    </div>
                </div>
            </div>
            <!--会员详情菜单-->
            <div class="subMenu">
                <ul class="clearfix">
                    <li data-id="nav01" class="nav01 on">基本信息</li>
                    <li data-id="nav02" class="nav02">交易记录</li>
                    <li data-id="nav03" class="nav03">积分明细</li>
                    <li data-id="nav04" class="nav04">帐内余额</li>
                    <li data-id="nav05" class="nav05">消费卡</li>
                    <li data-id="nav06" class="nav06">下线会员</li>
                    <li data-id="nav07" class="nav07">客服记录</li>
                    <li data-id="nav08" class="nav08">变更记录</li>
                    <li data-id="nav09" class="nav09">会员标签</li>
                </ul>
            </div>
            <div id="nav09" style="display: none;">
                <div class="panlnav09">
                    <p>
                        已添加标签</p>
                    <div class="tagbtnList" id="tagList">

                    </div>
                    <div id="selectTag" class="selectTag">
                        <p>
                            可选标签</p>
                        <div class="panltaglist">
                            <ul class="groupTag">
                                <li class="on">职业</li>
                                <li>年龄层</li>
                                <li>性别</li>
                            </ul>
                            <div class="fontC">
                                换一批</div>
                        </div>
                        <div class="groupTaglist" id="groupTagList">

                        </div>
                        <!--groupTaglist-->
                    </div>
                </div>
                <!--panlnav09-->
                <div class="btnList">
                    <div class="commonBtn" data-flag="save" data-type="groupsubmit">
                        保存</div>
                </div>
            </div>
            <!--idnav09-->
            <script type="text/html" id="tagTypeList">
                       <#for(var i=0;i<list.length;i++){var item=list[i];#>
                        <li  data-tagid="<#=item.TypeId#>" ><#=item.TypeName#></li>
                     <#}#>
            </script>
            <script type="text/html" id="groupTagBtn">
               <#for(var i=0;i<list.length;i++){var item=list[i];#>
                <div class="tagbtn" data-tagid="<#=item.TagsId#>"><#=item.TagsName#></div>
             <#}#>
              <div class="textbtn">
                <input type="text" id="TagsName" value=""> <div class="saveTagBtn tagbtn" data-flag="add">添加</div>
              <div>
            </script>
            <script type="text/html" id="tagListBtn">
           <#for(var i=0;i<list.length;i++){var item=list[i];#>
               <div class="tagbtn" data-tagid="<#=item.TagId#>" data-tagname="<#=item.TagName#>"> <#=item.TagName#> <em class="icon" style="display: none;"></em></div>
           <#}#>
            </script>
            <div id="nav07" style="display: none;">
                <div class="tableHandleBox">
                    <!-- <span class="commonBtn export">全部导出</span>-->
                    <span class="commonBtn addICon L">新增客服记录</span>
                </div>
                <table class="dataTable" id="servicesLog" style="display: inline-table;">
                </table>
            </div>
            <div id="nav08" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                时间
                            </th>
                            <th>
                                操作人
                            </th>
                            <th>
                                操作事项
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblLogs">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="4"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <script id="tpl_logs" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.logid#>"><em></em></td>
                        <td><#=item.createtime#></td>
                        <td><#=item.cu_name#></td>
                        <td><#=item.action#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--上线与下线-->
            <div id="nav06" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                会员编号
                            </th>
                            <th>
                                微信昵称
                            </th>
                            <th>
                                姓名
                            </th>
                            <th>
                                等级
                            </th>
                            <th>
                                积分
                            </th>
                            <th>
                                下线数
                            </th>
                            <th>
                                入会时间
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblOnline">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="7"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <script id="tpl_online" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipId#>"><em></em></td>
                        <td><#=item.VipCode#></td>
                        <td><#=item.VipName#></td>
                        <td><#=item.VipRealName#></td>
                        <td><#=item.VipCardGradeName#></td>
                        <td><#=item.EndIntegral#></td>
                        <td><#=item.OfflineCount#></td>
                        <td><#=item.CreateTime#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <div id="nav01">
                <div class="baseInfoArea">
                    <%--<div class="commonSelectWrap">
            	    <em class="tit">姓名:</em>
                    <p class="searchInput"><input type="text" id="editVipRealName" value=""></p>
                </div>
                <div class="commonSelectWrap">
            	    <em class="tit">昵称:</em>
                    <p class="searchInput"><input type="text" id="editVipName" value=""></p>
                </div>
                <div class="commonSelectWrap">
            	    <em class="tit">手机号:</em>
                    <p class="searchInput"><input type="text" id="editPhone" value=""></p>
                </div>
                <div class="commonSelectWrap">
            	    <em class="tit">会籍店:</em>
                    <p class="searchInput"><input type="text" id="editStore" value=""></p>
                </div>--%>
                    <div class="promptContent">
                    </div>
                    <div class="btnWrap">
                        <a href="javascript:;" class="commonBtn saveBtn">保存</a>
                    </div>
                </div>
                <script id="tpl_EditVipForm" type="text/html">
            <#var jsonColumns = JSON.parse(Data.JsonColumns);console.log(jsonColumns)#>
            <#var vipInfo = Data.VipInfo[0];console.log("vipInfo",vipInfo);#>
            <#var subRoot=jsonColumns.Root.SubRoot;subRoot=subRoot?subRoot:[];#>
                <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];var theCount=0;var findResult="";var theCount2=0;var findResult=""#>
                        <#if(item.DisplayType==1||item.DisplayType==3 || item.DisplayType==4 || item.DisplayType==7 ||item.DisplayType==2){#>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <label class="searchInput">
                                <input <#=(item.IsRead==1)?"disabled":""#> data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo" type="text" value="<#=vipInfo[item.ColumnName]?vipInfo[item.ColumnName]:''#>"></label>
                            </div>
                        <#}#>
                        <#if(item.DisplayType==5){#>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <div class="selectBox">
                                    <#for(var kt=0,ktlength=item.Fn.length;kt<ktlength;kt++){ var ktitem=item.Fn[kt];#>
                                        <#if(ktitem.OptionID==vipInfo[item.ColumnName]){#>
                                            <#theCount2++;findResult2=ktitem;break;#>                               
                                        <#}#>
                                    <#}#>
                                    <#if(theCount2>0&&findResult2!=""){#>
                                        <span class="text" <#=(item.IsRead==1)?"disabled":""#> data-forminfo="<#=JSON.stringify(item)#>" name="editvipinfo"  optionid="<#=ktitem.OptionID#>"><#=ktitem.OptionValue#></span>
                                    <#}else{#>
                                        <span class="text" <#=(item.IsRead==1)?"disabled":""#> data-forminfo="<#=JSON.stringify(item)#>" name="editvipinfo"   optionid="">请选择</span>
                                    <#}#>
                                    
                                    <div class="selectList">
                                        <#if(item.Fn instanceof Array){#>
                                             <#if(item.Fn&&item.Fn.length){#>
                                                <#for(var j=0;j<item.Fn.length;j++){var sel=item.Fn[j];#>
                                                    <p data-optionid="<#=sel.OptionID#>"><#=sel.OptionValue#></p>
                                                <#}#>
                                            <#}#>
                                        <#}else{#>
                                            <p data-optionid="<#=item.Fn.OptionID#>"><#=item.Fn.OptionValue#></p>
                                        <#}#>        
                                    </div>
                                </div>
                            </div>
                        <#}#>
                        <#if(item.DisplayType==205){ #>
                            <div class="commonSelectWrap">     
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <label class="searchInput">
                                <#for(var kk=0,length2=item.Fn[0].Tree.length;kk<length2;kk++){ var kitem=item.Fn[0].Tree[kk];#>
                                    <#if(kitem.UnitID==vipInfo[item.ColumnName]){ theCount++;findResult=kitem;#>
                                        
                                    <# break;}else{#>
                                       
                                    <#}#>
                                <#}#>
                                <#if(findResult!=""&&theCount>0){#>
                                    <input <#=(item.IsRead==1)?"disabled":""#>  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"   name="editvipinfo" unitId="<#=findResult.UnitID#>"  type="text" value="<#=findResult.UnitName#>">
                                <#}else{#>
                                    <input <#=(item.IsRead==1)?"disabled":""#>  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"   name="editvipinfo"   type="text" >
                                <#}#>
                                </label>
                                <ul id="ztree<#=Math.floor(Math.random()*9999999999+1)#>"  data-forminfo="<#=JSON.stringify(item.Fn[0].Tree)#>" class="ztree" style="display:none;position: absolute;left: 120px;background:#FFF;margin-top: 31px;width:173px;z-index:100;"></ul>
                            </div>
                        <#}#>
                        <#if(item.DisplayType==6){#>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <label class="searchInput"><input class="datepicker"  <#=(item.IsRead==1)?"disabled":""#> data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo" type="text" value="<#=vipInfo[item.ColumnName]#>"></label>
                            </div>
                        <#}#>
                    <#}#>
                </script>
            </div>
            <!--积分明细-->
            <div id="nav03" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                时间
                            </th>
                            <th>
                                积分变更
                            </th>
                            <th>
                                变更类型
                            </th>
                            <th>
                                备注
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblPoint">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="10"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <!--分页-->
                <div id="kkpager" style="text-align: center;">
                </div>
                <script id="tpl_point" type="text/html">
                <#for(var i=0;i<list.length; i++){ var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipIntegralId#>"><em></em></td>
                        <td><#=item.Date#></td>
                        <td><#=item.Integral#></td>
                        <td><#=item.VipIntegralSource#></td>
                        <td>
                            <#=item.Remark#>
                        </td>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--帐内余额-->
            <div id="nav04" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                时间
                            </th>
                            <th>
                                余额
                            </th>
                            <th>
                                变更类型
                            </th>
                            <th>
                                备注
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblAmount">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="10"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <script id="tpl_amount" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipAmountId#>"><em></em></td>
                        <td><#=item.Date#></td>
                        <td><#=item.Amount#>元</td>
                        <td><#=item.VipAmountSource#></td>
                        <td><#=item.Remark#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--消费卡-->
            <div id="nav05" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                卡类型
                            </th>
                            <th>
                                卡名称
                            </th>
                            <th>
                                领卡方式
                            </th>
                            <th>
                                备注
                            </th>
                            <th>
                                状态
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblConsumer">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="10"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <script id="tpl_consumer" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.CouponId#>"><em></em></td>
                        <td><#=item.CouponType#></td>
                        <td><#=item.CouponName#></td>
                        <td><#=item.CollarCardMode#></td>
                        <td><#=item.Remark#></td>
                        <td><#=item.CouponStatus#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--交易记录-->
            <div id="nav02" style="display: none;">
                <div class="tableHandleBox">
                    <%--<span class="commonBtn">添加新会员</span>--%>
                    <span class="commonBtn export">全部导出</span>
                </div>
                <div class="tableWrap">
                    <%--<div class="tablehandle">
                <div class="selectBox">
                    <span class="text">按最近时间升序</span>
                    <div class="selectList">
                        <p>按最近时间降序</p>
                        <p>按最近时间升序</p>
                    </div>
                </div>
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
                
            </div>--%>
                    <!-- 已确认名单表格 -->
                    <table class="dataTable" style="display: inline-table;">
                        <thead>
                            <tr class="title">
                                <th class="selectListBox">
                                    选择<div class="minSelectBox">
                                        <em class="minArr"></em>
                                        <p>
                                            全选本页</p>
                                        <p>
                                            全选所有页</p>
                                        <p>
                                            取消选择</p>
                                    </div>
                                </th>
                                <th>
                                    操作
                                </th>
                                <th>
                                    订单编号
                                </th>
                                <th>
                                    交易时间
                                </th>
                                <th>
                                    下单方式
                                </th>
                                <th>
                                    交易门店
                                </th>
                                <th>
                                    交易金额
                                </th>
                                <th>
                                    支付状态
                                </th>
                                <th>
                                    支付方式
                                </th>
                                <th>
                                    订单状态
                                </th>
                            </tr>
                        </thead>
                        <tbody id="content">
                            <tr>
                                <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="10"
                                    align="center">
                                    <span>
                                        <img src="../static/images/loading.gif"></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <!--分页-->
                </div>
            </div>
            <!--表格操作按钮-->
        </div>
        <!---easy ui  弹框---->
        <div style="display: none">
            <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
                    <div data-options="region:'center'" style="padding: 10px;">
                        指定的模板添加内容
                    </div>
                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                        text-align: center; padding: 5px 0 0;">
                        <a class="easyui-linkbutton commonBtn saveBtn">确定</a> <a class="easyui-linkbutton commonBtn cancelBtn"
                            href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--百度模板渲染模板 数据部分-->
    <!--//新增客服界面-->
    <script id="tpl_addCustomer" type="text/html">
    <form id="optionform">
     <input name="ServicesLogID" type="text" style="display: none" title="编辑时候保存id">
               <div class="commonSelectWrap">
                     <em class="tit">服务时间：</em>
                    <div class="searchInput bonone">
                       <input type="text" class="easyui-datetimebox" name="ServicesTime" data-options="width:160,height:32,showSeconds:false" />
                   </div>
               </div>

           <div class="commonSelectWrap"style="width: 628px">
                 <em class="tit">服务方式：</em>
                <div class="searchInput" style="width: 500px; border: none">
                    <div class="radio L on" data-name="r1"><em></em> <span> 到店</span></div>
                    <div class="radio L" data-name="r1"><em></em> <span> 电话</span></div>
                    <div class="radio L" data-name="r1"><em></em> <span> 微信</span></div>
                    <div class="radio L out" data-name="r1"><em></em> <span> 其他</span></div>
                    <input type="text" class="easyui-validatebox L" name="ServicesMode"  style="border:1px solid #ccc;width:160px;height:32px "  />
               </div>
           </div>
 <!--            <div class="commonSelectWrap">
                                <em class="tit">服务门店：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-combobox" name="UnitID" data-options="width:160,height:32" />
                              </div>
                          </div>
             <div class="commonSelectWrap">
                                <em class="tit">服务类型：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-combobox" name="ServicesType" data-options="width:160,height:32" />
                              </div>
                          </div>
             <div class="commonSelectWrap">
                                <em class="tit">服务时长：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-numberbox" name="Duration" data-options="width:160,height:32,min:0,precision:2,max:48" />
                              </div>
                          </div>-->
              <div class="commonSelectWrap">
                 <em class="tit">服务内容：</em>
                <div class="searchInput remark" >
                   <textarea type="text" name="Content" class="easyui-validatebox" data-options="validType:'maxLength[500]'"> </textarea>
               </div>
           </div>


     </form>
    </script>
    <script id="tpl_content" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){ var item=list[i]; #>
        <tr>
            <td class="checkBox"><em></em></td>
            <td class="seeIcon" data-orderid="<#=item.OrderId#>"></td>
            <td class="fontC"><#=item.OrderNo#></td>
            <td><#=item.CreateTime#></td>
            <td></td>
            <td><#=item.PayUnitName#></td>
            <td><#=item.PayAmount#></td>
            <td><#=item.PayStatus#></td>
            <td class="fontF"><#=item.PayType#></td>
            <td class="fontF"><#=item.OrderStatus#></td>
        </tr>
<#} #>
    </script>
    <!--会员标签-->
    <script id="tpl_vipTag" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){  var item=list[i];#>
<span><#=item.TagName#></span>
<#}#>
    </script>
    <!--table没数据的提示-->
    <script id="tpl_noContent" type="text/html">
<tr >
    <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><#=tips#></span></td>
</tr>
    </script>
    <script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true"
        data-main="js/main.js"></script>
</asp:Content>
