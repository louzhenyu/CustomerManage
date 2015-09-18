<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商品分类管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/commodity/css/style.css?v=0.5"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/static/css/kkpager.css"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/Category.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                    <div class="tableWrapText" id="tableWrap">
                                   <div class="tableWrapTextBtn">
                                   <div class="commonBtn addCategory" data-flag="add">新建分类</div>

                                    </div>
                                     <table class="dataTable" id="gridTable"></table>
                                     <div style="display: none"> <table class="dataTable" id="gridTable1"></table>
                                       </div>
                                     <div id="pageContianer">
                                         <div id="kkpager" style="text-align: center;">
                                         </div>
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
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>

<script id="tpl_addProm" type="text/html">
           <form id="addProm">
      <div class="commonSelectWrap">
      <em class="tit">分类名称：</em>
        <label class="searchInput" >
          <input data-text="促销分组名称" class="easyui-validatebox" data-options="required:true" data-flag="Item_Category_Name" name="Item_Category_Name" type="text" value="">
          <input name="Item_Category_Id" type="text" style="display: none"/>
       </label>
 </div>
  <div class="commonSelectWrap">
                           <em class="tit">上级分类：</em>
                           <label class="searchInput">
                             <input data-text="上级分类" id="Category" data-options="required:true" data-flag="Parent_Id" name="Parent_Id" type="text" value="">
                           </label>
                       </div>
  <div class="commonSelectWrap">
                           <em class="tit">商品图片：</em>
                          <div class="handleLayer" id="editLayer">
                              <div class="jsAreaItem">
                               <div class="wrapPic">

                                  <span class="uploadBtn"><input class="uploadImgBtn" type="file" /></span>
                                  <div class="imgPanl"><img src=""></div>
                                </div>

                           </div>
                          </div>
                       </div>
 </form>
</script>


       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
