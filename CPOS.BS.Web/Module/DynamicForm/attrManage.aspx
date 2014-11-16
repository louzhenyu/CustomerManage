<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>属性管理</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/global.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/styleAttr.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="section" data-js="js/attrManage" class="m10">
    <div class="allPage">
        <div class="commonTopTitle">
            <ul>
                <li class="first"><a href="javascript:void(0)" onclick='AddMid("formList.aspx")'>动态表单</a></li>
                <li class="on"><a href="javascript:void(0)" onclick='AddMid("attrManage.aspx")'>会员动态属性</a></li>
                <li class=""><a href="javascript:void(0)" onclick='AddMid("vipForm.aspx")'>会员中心属性配置</a></li>
            </ul> 
        </div>

        <!--会员动态属性区域-->
        <div class="vipDynamicAttr">
            <!--表单显示区域-->
            <div id="propertyContainer" class="vipDynamicForm">
                <!--<p class="tipText">您还可以添加18个属性</p>-->
                <!--<div class="vipDynamicForm-box">-->
                    <!--<div class="vipDynamicForm-item">-->
                        <!--<em class="tit">姓名:</em>-->
                        <!--<label><input type="text" value="王孟孟"></label>-->
                    <!--</div>-->
                    <!--<div class="vipDynamicForm-item">-->
                        <!--<em class="tit">性别:</em>-->
                        <!--<div class="selectBox">-->
                            <!--<span class="arr"></span>-->
                            <!--<span class="text">男</span>-->
                            <!--<div class="dropList">-->
                                <!--<span>男</span>-->
                                <!--<span>女</span>-->
                            <!--</div>-->
                        <!--</div>-->
                    <!--</div>-->

                    <!--<div class="vipDynamicForm-item">-->
                        <!--<em class="tit">手机:</em>-->
                        <!--<label><input type="text" value=""></label>-->
                    <!--</div>-->
                    <!--<div class="vipDynamicForm-item on">-->
                        <!--<em class="tit">邮箱:</em>-->
                        <!--<label><input type="text" value=""></label>-->
                    <!--</div>-->
                    <!--<div class="vipDynamicForm-item">-->
                        <!--<em class="tit">QQ:</em>-->
                        <!--<label><input type="text" value=""></label>-->
                    <!--</div>-->
                    <!--<div class="vipDynamicForm-item">-->
                        <!--<em class="tit">微信号:</em>-->
                        <!--<label><input type="text" value=""></label>-->
                    <!--</div>-->
                    <!--<div class="vipDynamicForm-item on">-->
                        <!--<em class="tit">年龄:</em>-->
                        <!--<div class="radioHandle">-->
                           <!--<div class="item">-->
                                <!--<i class="on"></i>-->
                                <!--<label>18-25岁</label>-->
                           <!--</div>-->
                           <!--<div class="item">-->
                                <!--<i></i>-->
                                <!--<label>26-30岁</label>-->
                           <!--</div>-->
                        <!--</div>-->
                    <!--</div>-->
                <!--</div>-->
            </div>

            <!--选择区域-->
            <div class="moduleAreaWrap">
                <div id="menuList" class="menuList">
                    <a href="javascript:;" class="on tabHead" data-layer="addLayer">添加属性</a>
                    <a href="javascript:;" class="tabHead" data-layer="editLayer">编辑属性</a>
                </div>
                <!--添加属性区域-->
                <div id="addLayer" class="moduleArea tabContainer" style="display:block;">
                    <ul id="basicList">
                        <!--<li class="exist">-->
                            <!--<i class="iconName"></i>-->
                            <!--<span>文本输入框</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconMenu"></i>-->
                            <!--<span>下拉菜单</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconSex01"></i>-->
                            <!--<span>多选框</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconDate"></i>-->
                            <!--<span>日期</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconSex02"></i>-->
                            <!--<span>单选框</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconEmail"></i>-->
                            <!--<span>邮箱</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconMobile"></i>-->
                            <!--<span>手机</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                        <!--<li>-->
                            <!--<i class="iconOther"></i>-->
                            <!--<span>其他</span>-->
                            <!--<span class="option"></span>-->
                            <!--<span class="unoption"></span>-->
                        <!--</li>-->
                    </ul>
                </div>

                <!--编辑属性区域-->
                <!--编辑文本框-->
                <div id="editLayer" class="tabContainer" style="display: none;">


                </div>


            </div>
        </div>
    </div>
</div>

<script id="editTemp" type="text/html">
<div class="editInputArea ">
    <$if(mode=="add"){ $>
    <div class="inputBox">
        <p class="tit">属性名称</p>
        <label><input class="editTitle" type="text" placeholder="请输入属性名"></label>
    </div>
    <$if(type=="5"){ $>
    <div class="radioHandle optionSection">
       <div class="item optionItem">
            <i></i>
            <label><input type="text" placeholder="请输入选项"></label>
            <span class="minus"></span>
            <span class="plus"></span>
       </div>
    </div>
    <$} $>
    <div class="btnWrap">
        <a href="javascript:;" class="editSubmitBtn commonBtn">保存</a>
    </div>
    <$}else if(mode=="edit"){ $>
        <div class="inputBox">
            <p class="tit">属性名称</p>
            <label><input class="editTitle" type="text" placeholder="" value="<$=name $>" disabled="disabled" ></label>
        </div>
        <$if(type=="5"&&optionList&&optionList.length){ $>
        <div class="radioHandle optionSection">
            <$for(var i=0;i<optionList.length;i++){var idata=optionList[i];$>
                <div class="item optionItem">
                    <i></i>
                    <label><input type="text" placeholder="" disabled="disabled" value="<$=idata.OptionText $>"></label>
                    <span class="plus"></span>
                </div>
            <$} $>
        </div>
        <$} $>
        <div class="btnWrap">
            <a href="javascript:;" class="commonBtn <$if(!flag){ $> grayBtn <$}else{ $> editSubmitBtn<$} $>">保存</a>
        </div>
    <$} $>
</div>
</script>
<script id="basicListTemp" type="text/html">
<$for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
    <$if(idata.DisplayType==205) continue;$>
    <li class="type<$=idata.DisplayType $>" data-type="<$=idata.DisplayType $>">
        <i class="<$=(classMap[idata.DisplayType]?classMap[idata.DisplayType]:classMap.other) $>"></i>
        <span><$=idata.DisplayName $></span>
        <span class="option"></span>
        <span class="unoption"></span>
    </li>
<$}$>
</script>

<script id="propertyTemp" type="text/html">
    <p class="tipText">您还可以添加<$=AvailableSlot $>个属性</p>
    <div id="propertyList" class="vipDynamicForm-box">
    <$if(PropertyList&&PropertyList.length){ $>
    <$for(var i=0,idata;i<PropertyList.length;i++){ idata=PropertyList[i] ;$>
        <div data-property="<$=idata.PropertyID $>" class="vipDynamicForm-item propertyItem" data-name="<$=idata.PropertyName $>" data-type="<$=idata.DisplayType $>">
            <em class="tit t-overflow"><$=idata.PropertyName $>:</em>
            <$if(idata.DisplayType==5){ $>
                <div class="selectBox">
                    <span class="arr"></span>
                    <span class="text"></span>
                </div>
            <$}else{$>
                <label><input type="text" value="" disabled="disabled" ></label>
            <$}$>

        </div>
    <$}$>
    <$}$>
    </div>
</script>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/DynamicForm/js/main.js"%>"></script>
</asp:Content>