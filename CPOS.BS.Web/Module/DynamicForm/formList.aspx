<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>表单列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/global.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/DynamicForm/css/style.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/static/css/pagination.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" data-js="js/formList" class="section m10">
        <div class="allPage">
            <div class="commonTopTitle">
                <ul>
                    <li class="first on"><a href="formList.aspx">动态表单</a></li>
                    <li><a href="javascript:void(0)" onclick='AddMid("attrManage.aspx")' >会员动态属性</a></li>
                    
                    <li class=""><a  href="javascript:void(0)" onclick='AddMid("vipForm.aspx")' >会员中心属性配置</a></li>
                </ul>
            <a id="createFormBtn" href="javascript:;" class="createFormBtn"></a></div>
            <!--团购列表-->
            <div class="formList">
                <ul id="formList">

                </ul>
            </div>
            <div class="pageWrap" style="display:none;">

            </div>
        </div>



        <!-- 弹层，添加新表单 -->
        <div class="ui-pc-mask" style="display:none;"></div>
        <div id="createFormLayer" class="ui-dialog ui-dialog-addForm" style="display: none;">
            <div class="ui-dialog-tit">
                <h2>添加新表单</h2>
                <a href="javascript:;" class="ui-dialog-close jsCancelBtn"></a>
            </div>
            <div class="createForm-box">
                <p>
                    <em class="name">表单名称</em>
                    <input class="jsFormName infoBox" type="text" placeholder="">
                </p>
            </div>
            <div class="btnWrap">
                <a href="javascript:;" class="jsSubmitBtn commonBtn">确定</a>
                <a href="javascript:;" class="jsCancelBtn commonBtn cancelBtn">取消</a>
            </div>
        </div>

    </div>
    <script id="formListTemp" type="text/html">
        <$ for(var i=0,idata;i<list.length;i++){ idata=list[i] ;$>
            <li class="listItem"  data-formid="<$=idata.FormID$>">
                <div class="formList-item">
                    <a data-formid="<$=idata.FormID$>" href="javascript:;" class="delBtn delListBtn"></a>
                    <p class="markBox"><img src="images/listIcon.png" alt=""/></p>
                    <div class="infoBox">
                        <h3 class="tit t-overflow"><$=idata.FormName$></h3>
                        <p class="createTime">修改时间:<$=idata.CreatedDate$></p>
                    </div>
                </div>
            </li>
        <$}$>
    </script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/DynamicForm/js/main"%>"></script>
</asp:Content>
