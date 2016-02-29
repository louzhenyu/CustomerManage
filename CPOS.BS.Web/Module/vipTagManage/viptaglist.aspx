<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员标签管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/vipTagManage/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/viptaglist.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                    <div class="tableWrapText" id="tableWrap">
                                   <div class="optionBtn">
                                   <div class="commonBtn icon icon_add w100 r" data-flag="add">新增标签</div>

                                    </div>
                                     <table class="dataTable" id="gridTable"></table>
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
      <em class="tit w100">标签分类名称：</em>
        <label class="searchInput" >
          <input data-text="标签" class="easyui-validatebox" data-options="required:true" data-flag="SkuProp" name="TypeName" type="text" value="">
          <input name="TypeId" type="text" style="display: none"/>
       </label>
 </div>
       <div class="commonSelectWrap">
       <em class="tit w100">标签分类值：</em>
         <label class="searchInput" id="TagsList">
          <div class="mainpanl">
           <div class="list">
           <input data-text="标签分类值" class="easyui-validatebox" data-options="required:true"  name="TagsName" type="text" value="">
            <input name="TagsId" type="text" style="display: none"/>
               <!--  <div class="optBtn"><div class="btn add" data-flag="add"></div> </div>-->
           </div>
           </div>
           <div class="btn add"  data-flag="add">+添加</div>
        </label>
  </div>
 </form>
</script>
     <script id="tpl_List" type="text/html">
             <div class="list">
                       <input data-text="标签分类值" class="easyui-validatebox"   name="TagsName" type="text" value="<#=TagsName#>">
                        <input name="TagsId" type="text" value="<#=TagsId#>" style="display: none"/>
                        <div class="optBtn"> <div class="btn del" data-flag="del"></div></div>
                       </div>
     </script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
