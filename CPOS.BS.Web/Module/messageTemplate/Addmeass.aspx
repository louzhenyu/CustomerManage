<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>消息模板基础信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.1" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/addMeass.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
        <div class="lineTitle">模板设置</div>
            <!--个别信息查询-->
            <form></form>
             <form id="meassage">

             <div class="panelSubmit">
            <div class="commonSelectWrap">
                <em class="tit wh80">标题：</em>
                <label class="searchInput" style="width: 320px;">
                    <input data-text="标题" placeholder="请输入" class="easyui-validatebox" name="Title" data-options="required:true,validType:'stringCheck',width:320,height:32" />
                </label>
            </div>
            <div class="commonBtn" id="placeholder">插入占位符
            <ul>
            <li data-formInfo='#Name#'>会员名称</li>
            <li data-formInfo='#Card#'>会员卡</li>
            <li data-formInfo='#Birthday#'>生日日期</li>
            <li data-formInfo='#Point#'>当前积分</li>
            <li data-formInfo='#Cash#'>当前余额</li>
            <li data-formInfo='#Coupon#'>活动送券名称</li>
            </ul>

            </div>
             <div class="commonSelectWrap" style="height: 126px;">
                                         <em class="tit wh80">内容：</em>
                                         <div class="searchInput" style="width: 621px; height: 126px;border-radius:3px">
                                             <textarea data-text="内容" placeholder="请输入" class="easyui-validatebox" name="Content" data-options="required:true,width:621,height:126" ></textarea>
                                         </div>
                                     </div>
           <div class="optionBtn" style="width: 363px;margin: 0 auto;">
           <div class="btnOpt commonBtn" data-flag="save">保存</div>
           <div class="btnOpt commonBtn bg"  data-flag="preview">预览</div>
            </div>
         </div>
            </form>
        </div>
    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
