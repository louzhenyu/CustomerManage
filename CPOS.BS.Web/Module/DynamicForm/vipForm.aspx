<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>表单详情</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/global.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/style.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="section" data-js="js/vipForm" class="m10">
    <div class="allPage">
        <div class="commonTopTitle">
                <ul>
                    <li><a href="javascript:void(0)" onclick='AddMid("formList.aspx")'>动态表单</a></li>
                    <li><a href="javascript:void(0)" onclick='AddMid("attrManage.aspx")'>会员动态属性</a></li>
                    <li class="on"><a href="javascript:void(0)" onclick='AddMid("vipForm.aspx")'>会员中心属性配置</a></li>
                </ul>
        </div>

        <div class="formDetailsArea">
            <!--表单菜单-->
            <div class="formDetailsNav2">
                <ul id="menuList" class="clearfix">
                    <li class="normalSearch on" data-type="1" data-layer="editLayer"></li>
                    <li class="unusualSearch" data-type="2" data-layer="editLayer"></li>
                    <li class="newEdit" data-type="3" data-layer="editLayer"></li>
                    <li class="listShow" data-type="4" data-layer="editLayer"></li>
                </ul>
            </div>

            <!--应用到模块-->
            <div id="applyLayer" class="tabContainer applyToHandle clearfix" style="display:none;">
                <h2 class="tit">应用到：</h2>
                <div id="sceneList" class="checkBoxArea">
                    <!--<p class="on"><em></em>会员卡</p>-->
                    <!--<p><em></em>WiFi</p>-->
                    <!--<p><em></em>PAD导购</p>-->
                </div>
                <a id="sceneCommitBtn" href="javascript:;" class="commonBtn">确定</a>
            </div>

            <!--表单组件模块-->
            <div id="editLayer" class="tabContainer formModuleHandle">
                <div class="handleWrap clearfix">
                    <!--表单显示区域-->
                    <div id="selectList" class="formShowArea">
                        <!--<div class="formShowArea-item">-->
                            <!--<span class="closeBtn"></span>-->
                            <!--<em class="tit">手机号:</em>-->
                            <!--<div class="inputArea">-->
                                <!--<label><input type="text" value="18211326622"></label>-->
                                <!--<div class="selectBox">-->
                                    <!--<span class="text">必填</span>-->
                                    <!--<div class="dropList">-->
                                        <!--<span>必填</span>-->
                                        <!--<span>选填</span>-->
                                    <!--</div>-->
                                <!--</div>-->
                            <!--</div>-->
                        <!--</div>-->
                    </div>
                    <!--垂直线-->
                    <div class="vertical-line" style="position:absolute;left:490px;"></div>
                    <!--组件选择区域-->
                    <div class="moduleArea">
                        <h2 class="tit">选择组件</h2>
                        <ul id="allList">
                            <!--<li class="exist">-->
                                <!--<i class="iconName"></i>-->
                                <!--<span>姓名</span>-->
                                <!--<span class="option"></span>-->
                                <!--<span class="unoption"></span>-->
                            <!--</li>-->
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script id="selectItemTemp" type="text/html">
<#for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
    <#if(typeId==1){#>
        <div id="<#=idata.FormControlID #>" class="formShowArea-item" data-id="<#=idata.FormControlID #>"  data-fid="<#=idata.FormControlID #>">
            <span class="closeBtn"></span>
            <em class="tit t-overflow"><#=idata.ColumnDesc#>:</em>
    
            <div class="inputArea">
                <label><input type="text" value=""></label>
                <#if(typeId&&typeId==3){#>
                <div class="selectBox" data-val="<#=idata.IsMustDo #>">
                    <span class="text"><#if(idata.IsRead==1){#>只读<#}else{#>可写<#}#></span>
                    <div class="dropList">
                        <span data-val="1">只读</span>
                        <span data-val="0">可写</span>
                    </div>
                </div>
                <#}#>
            </div>
        </div>
    <#}else if(typeId==2){#>
        <#if(idata.EditOrder>20){#>
            <div id="<#=idata.FormControlID #>" class="formShowArea-item" data-id="<#=idata.FormControlID #>"  data-fid="<#=idata.FormControlID #>">
                <span class="closeBtn"></span>
                <em class="tit t-overflow"><#=idata.ColumnDesc#>:</em>
    
                <div class="inputArea">
                    <label><input type="text" value=""></label>
                    <#if(typeId&&typeId==3){#>
                    <div class="selectBox" data-val="<#=idata.IsMustDo #>">
                        <span class="text"><#if(idata.IsRead==1){#>只读<#}else{#>可写<#}#></span>
                        <div class="dropList">
                            <span data-val="1">只读</span>
                            <span data-val="0">可写</span>
                        </div>
                    </div>
                    <#}#>
                </div>
            </div>
        <#}#>
    <#}else{#>
        <div id="<#=idata.FormControlID #>" class="formShowArea-item" data-id="<#=idata.FormControlID #>"  data-fid="<#=idata.FormControlID #>">
            <span class="closeBtn"></span>
            <em class="tit t-overflow"><#=idata.ColumnDesc#>:</em>
            <div class="inputArea">
                <label><input type="text" value=""></label>
                <#if(typeId&&typeId==3){#>
                <div class="selectBox" data-val="<#=idata.IsMustDo #>">
                    <span class="text"><#if(idata.IsRead==1){#>只读<#}else{#>可写<#}#></span>
                    <div class="dropList">
                        <span data-val="1">只读</span>
                        <span data-val="0">可写</span>
                    </div>
                </div>
                <#}#>
            </div>
        </div>
    <#}#>

<#}#>
<%--<#if(list.length){ #>--%>
<a href="javascript:;" id="selectCommitBtn" class="commonBtn saveFormBtn">保存表单</a>
<%--<#}#>--%>
</script>

<script id="allItemTemp" type="text/html">
<#for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
        <#if(typeId==1){#>
            <li class="<#if(idata.EditOrder>0&&idata.EditOrder<=20){#> exist <#}#>" data-id="<#=idata.FormControlID #>">
        <#}else if(typeId==2){#>
            <li class="<#if(idata.EditOrder>0&&idata.EditOrder>20){#> exist <#}#>" data-id="<#=idata.FormControlID #>">
        <#}else{#>
            <li class="<#if(idata.EditOrder>0){#> exist <#}#>" data-id="<#=idata.FormControlID #>">
        <#}#>
        <i class="<#=classMap[idata.ControlType] #>"></i>
        <span><#=idata.ColumnDesc#></span>
        <span class="option"></span>
        <span class="unoption"></span>
    </li>
<#}#>
</script>

<script id="sceneItemTemp" type="text/html">
<#for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
    <p data-id="<#=idata.SceneValue#>" class="checkbox <#if(idata.IsSelected==1){ #> on <#}#>"><em></em><#=idata.SceneName#></p>
<#}#>
</script>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/DynamicForm/js/main.js"%>"></script>
</asp:Content>