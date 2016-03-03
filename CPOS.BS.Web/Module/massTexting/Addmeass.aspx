<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>消息模板基础信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/massTexting/css/style.css?v=0.1"%>" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/addMeass.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                <div class="item">
                    <form>
                    </form>
                    <form id="seach">
                    <div class="commonSelectWrap">
                        <em class="tit">门店：</em>
                        <label class="selectBox">
                                 <input  id="UnitList"  name="UnitID" >
                        </label>
                    </div>
                    <div class="commonSelectWrap" style="height: 80px;">
                        <em class="tit">手机号：</em>
                        <div class="searchInput" style="width: 260px; height: 80px;">
                            <textarea name="PhoneList" type="text" value="" placeholder="请输入完整的11位手机号数字且多个手机号用逗号隔开"></textarea>
                        </div>
                    </div>
                    <div class="moreQueryWrap">
                        <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                    </div>
                    </form>
                </div>
                <!--<h2 class="commonTitle">会员查询</h2>-->
            </div>
            <div class="tableWrap" id="tableWrap">
                <p class="title">
                    员工信息</p>
                <table class="dataTable" id="gridTable">
                </table>
            </div>
            <div id="pageContianer">
                <div class="dataMessage">请点击查询按钮,加载符合查询条件的数据</div>
                <div id="kkpager">
                </div>
            </div>

             <form id="meassage">


            <div class="commonSelectWrap">
                <em class="tit wh80">分组：</em>
                <label class="selectBox">
                    <input data-text="分组"  id="DeptID" class="easyui-combobox" name="DeptID" data-options="width:200,height:32"
                        value="">
                </label>
            </div>
            <div style="width: 100%; height: 1px; float: left">
            </div>
            <div class="commonSelectWrap">
                <em class="tit wh80">标题：</em>
                <label class="searchInput" style="width: 320px;">
                    <input data-text="标题" class="easyui-validatebox" name="Title" data-options="required:true,validType:'stringCheck',width:320,height:32" />
                </label>
            </div>
            <textarea name="Text"  class="info" style="width: 100%;"></textarea>
           <div class="btnopt commonBtn saveBtn" id="submitBtn" data-flag="#nav02">发送</div>
            </form>
        </div>
    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
