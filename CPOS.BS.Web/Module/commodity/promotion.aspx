<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商品分组管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/commodity/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/promotion.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                    <div class="tableWrapText" id="tableWrap">
                                   <div class="tableWrapTextBtn optionBtn">
                                   <div class="commonBtn icon icon_add w100 r" data-flag="add">新增分组</div>

                                    </div>
                                    <div class="cursorDef">
                                     <table class="dataTable" id="gridTable">
                                                <div  class="loading">
                                                         <span>
                                                       <img src="../static/images/loading.gif"></span>
                                                  </div>
</table>
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
      <em class="tit w120">分组名称：</em>
        <label class="searchInput" >
          <input data-text="商品分组名称" class="easyui-validatebox" data-options="required:true" data-flag="Item_Category_Name" name="Item_Category_Name" type="text" value="">
          <input name="Item_Category_Id" type="text" style="display: none"/>
       </label>
 </div>
 </form>
</script>

</asp:Content>
