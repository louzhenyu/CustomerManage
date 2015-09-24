<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>销售员提现管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/sendingTogether.css"/>
</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<body cache>
<div class="allPage" id="section" data-js="js/vipDeposit.js?ver=0.1">
    <!-- 内容区域 -->
    <div class="contentArea_vipquery">
        <!--个别信息查询-->
        <div class="queryTermArea" id="simpleQuery" style="display:inline-block;width: 100%; " >
             <form></form>
            <form id="queryFrom">
            <div class="commonSelectWrap">
                <em class="tit">提现单号：</em>
                <label class="searchInput"><input  name="WithdrawNo" type="text" value=""></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">销售员名称：</em>
                <label class="searchInput"><input   name="VipName" type="text" value=""></label>
            </div>
            
            <div class="commonSelectWrap">
                <em class="tit">状态：</em>
                <div class="selectBox bordernone">
                  <select id="cc" class="easyui-combobox" name="Status" style="width:159px; height: 29px;">
                      <option value="-1">全部</option>
                      <option value="0">待确认</option>
                      <option value="1">已确认</option>
                      <option value="2">已完成</option>

                  </select>

                </div>
            </div>
             </form>
            
              <a href="javascript:;" class="commonBtn queryBtn">查询</a>
        </div>

        <!--表格操作按钮-->
        <div id="menuItems" class="tableHandleBox">
            <!--<span class="commonBtn _addVip">添加新会员</span>-->
            <span class="commonBtn exportBtn">打印</span>
            <span class="commonBtn affirmBtn" data-statusid=1>确认</span>
            <span class="commonBtn finishBtn" data-statusid=2>完成</span>

        </div>    
        <div class="tableWrap">
            <table class="dataTable"  id="dataTable">

            </table>
            <div id="pageContianer">
             <div class="dataMessage" >没有符合条件的查询记录</div>
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>
    </div>
</div>
    <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onClick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async data-main="<%=StaticUrl+"/module/sendingTogether/js/main.js"%>" ></script>
    </body>
</asp:Content>