<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>表单详情</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/global.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/style.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="section" data-js="js/formEdit" class="m10">
    <div class="allPage">
        <div class="commonTopTitle">
        </div>

        <div class="formDetailsArea">
            <!--修改原来信息-->
            <div class="formOriginalInfo">
                <div id="titleContainer" class="clearfix" >
                    <p class="picWrap"><img src="images/listIcon.png" alt=""/></p>
                    <p class="inputBox">
                        <span class="t-overflow"></span>
                        <input type="text" value="">
                    </p>
                    <a href="javascript:;" class="modifyBtn"></a>
                    <div class="btnWrap">
                        <span class="saveBtn">保存</span>
                        <span class="cancelBtn">取消</span>
                    </div>
                    <div class="backBtn" onclick="javascript:location.href='formList.aspx'">返回</div>
                </div>
                <!--<p class="clickMore">点击查看更多活动</p>-->
            </div>
            <!--表单菜单-->
            <div class="formDetailsNav">
                <ul id="menuList" class="clearfix">
                    <li class="formModule on" data-layer="editLayer"></li>
                    <li class="applyTo" data-layer="applyLayer"></li>
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
<$for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
<div id="<$=idata.PublicControlID $>" class="formShowArea-item" data-id="<$=idata.PublicControlID $>"  data-fid="<$=idata.FormControlID $>">
    <span class="closeBtn"></span>
    <em class="tit t-overflow"><$=idata.ColumnDesc$>:</em>
    <div class="inputArea">
        <label><input type="text" value=""></label>
        <div class="selectBox" data-val="<$=idata.IsMustDo $>">
            <span class="text"><$if(idata.IsMustDo==1){$>必填<$}else{$>选填<$}$></span>
            <div class="dropList">
                <span data-val="1">必填</span>
                <span data-val="0">选填</span>
            </div>
        </div>
    </div>
</div>
<$}$>
<%--<$if(list.length){ $>--%>
<a href="javascript:;" id="selectCommitBtn" class="commonBtn saveFormBtn">保存表单</a>
<%--<$}$>--%>
</script>

<script id="allItemTemp" type="text/html">
<$for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
    <li class="<$if(idata.IsUsed){$> exist <$}$>" data-id="<$=idata.PublicControlID $>">
        <i class="<$=classMap[idata.ControlType] $>"></i>
        <span><$=idata.ColumnDesc$></span>
        <span class="option"></span>
        <span class="unoption"></span>
    </li>
<$}$>
</script>

<script id="sceneItemTemp" type="text/html">
<$for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
    <p data-id="<$=idata.SceneValue$>" class="checkbox <$if(idata.IsSelected==1){ $> on <$}$>"><em></em><$=idata.SceneName$></p>
<$}$>
</script>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/DynamicForm/js/main.js"%>"></script>
</asp:Content>