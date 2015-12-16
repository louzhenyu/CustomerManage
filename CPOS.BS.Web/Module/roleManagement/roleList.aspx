<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>角色管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/commodity/css/style.css?v=0.61"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
	.queryTermArea{border-bottom:1px dashed #dcdcdc;background:#fafafa;}
	.queryTermArea .item{display:inline-block;}
    .queryTermArea .commonSelectWrap{margin-right:0px;}
	.commonSelectWrap .selectBox{width:232px;}
	#opt{padding-top:17px;border-top:none;background:#fff;}
	.optionBtn .commonBtn{width:100px;}
	.commonSelectWrap .tit{width:105px;}
	.moreQueryWrap{float:left;margin-left:25px;}
	.optionBtn .exportBtn,.optionBtn .entranceBtn{float:right;}
	.optionBtn .exportBtn{margin-right:40px;}
	.moreQueryWrap .queryBtn{width:73px;}
	.textbox-addon-right{right:9px !important;}
	#addRoleBtn{width:115px; text-indent:20px;background:#0cc url(images/icon-add.png) no-repeat 20px center;}
	.datagrid-header td, .datagrid-body td, .datagrid-footer td{cursor:inherit;}
	.datagrid-body .datagrid-row{}
	.datagrid-body .handle{display:inline-block;width:30px;height:39px;}
	.deleteBtn{margin-left:15px;background:url(images/delete.png) no-repeat center center;}
	.editBtn{background:url(images/edit.png) no-repeat center center;}
	
	#win .commonSelectWrap{margin:12px 10px 32px 0;}
	#win .searchInput{width:232px;}
	#win .searchInput .combo{width:230px !important;border-width:0px;}
	.textbox-invalid{border:none;background:none;}
	.limitsTreeBox{float:left;height:200px;width:380px;margin-left:5px;border:1px solid #d0d5d8;border-radius:3px;overflow-x:auto;}
	#btnWrap{height:60px !important;}
	.icon-tipBox{display:none;float:left;width:41px;height:18px;margin-left:6px;background:url(images/icon-tipBox.png) no-repeat 0 0;}
	.loading{width:81%;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/roleList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <em class="tit">角色：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="角色" data-flag="role_name" name="role_name"   id="role_name" type="text" value="" placeholder="请输入">
                                  </label>
                              </div>
                              <div class="commonSelectWrap">
                                  <em class="tit">所属层级：</em>
                                  <div class="selectBox">
                                       <input id="type_id" name="type_id" />
                                  </div>
                              </div>
                              <div class="commonSelectWrap">
                                  <em class="tit">应用系统：</em>
                                  <div class="selectBox">
                                       <select id="app_sys_id" name="app_sys_id"></select>
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
                <div class="optionBtn" id="opt">
                	<div class="commonBtn" id="addRoleBtn">新增角色</div>
                </div>
                   <div class="">
                        <table class="dataTable" id="gridTable">
                        	<div  class="loading">
                               <span><img src="../static/images/loading.gif"></span>
                            </div>
                        </table>
                   </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager" >
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
			  <em class="tit">角色：</em>
				<label class="searchInput clearBorder">
				  <input data-text="角色" class="easyui-validatebox" id="role_name2" data-options="required:true"  name="Role_Name" type="text" value="" placeholder="请输入2-6位字符"  validType='length[2,6]'>
			   </label>
			</div>
			
			<div class="commonSelectWrap">
			  <em class="tit">所属层级：</em>
				<label class="searchInput clearBorder">
				  <input data-text="所属层级" class="easyui-combobox" id="type_id2" data-options="required:true,invalidMessage:'必填'"  name="type_id" type="text" value="" validType='selectIndex'>
			   </label>
			</div>

			<div class="commonSelectWrap">
				<em class="tit">应用系统：</em>
				<label class="searchInput clearBorder">
				  <input data-text="应用系统" class="easyui-combobox" id="app_sys_id2" data-options="required:true,invalidMessage:'必填'" name="Def_App_Id" type="text" value="" validType='selectIndex'>
				</label>
			</div>
			
			
			<div class="commonSelectWrap" style="clear:left;height:auto;">
			  	<em class="tit">拥有权限：</em>
				<div class="limitsTreeBox" id="limitsTreeBox">
				</div>
				<span class="icon-tipBox"></span>
			</div>
			
			  
			</form>
		</script>
        
        
        <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/roleManagement/js/main.js"%>"></script>
</asp:Content>
