<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>新增用户</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/staffManagement/css/staffList.css?v=0.65"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/staffDetail.js?ver=0.5">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
            	<div class="bigTitle">新增用户</div>
                <div class="smallTitle"><span>基本信息</span></div>
                <!--信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <em class="tit">用户名：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="用户名" class="easyui-validatebox" data-flag="User_Code" name="User_Code"   id="User_Code" type="text" value="" data-options="required:true,validType:'maxLength[20]',invalidMessage:'输入的用户名长度不能超20个字符'" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">姓名：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="姓名" class="easyui-validatebox" name="User_Name" id="user_name" type="text" value="" data-options="required:true,validType:'maxLength[10]',invalidMessage:'输入的姓名长度不能超10个字符'" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <br />
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">密码：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="密码" class="easyui-validatebox" data-flag="User_Password" name="User_Password"   id="User_Password" type="password" value="" data-options="required:true,validType:'maxLength[32]',invalidMessage:'输入的密码长度不能超32位'" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">手机：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="手机" class="easyui-validatebox" data-flag="User_Telephone" name="User_Telephone" id="User_Telephone" type="text" value="" data-options="required:true,validType:'mobile'" placeholder="请输入">
                                  </label>
                              </div>
                              
                        </form>
                        </div>
                </div>
                <div class="smallTitle">
                	<a href="javascript:;" class="addStaffBtn commonBtn w80 icon icon_add">新增</a>
                	<span>角色/权限</span>
                </div>
                <div class="tableWrap cursorDef" id="tableWrap">
                   <div class=""> <table class="dataTable" id="gridTable"></table>  </div>
                    <div id="pageContianer">
                    <div class="dataMessage" style="display:none">没有符合条件的查询记录</div>
                        <div id="kkpager" >
                        </div>
                    </div>
                </div>
                
                <div class="btnWrap staffBtn">
                    <a href="javascript:;" class="commonBtn saveBtn" id="staffSaveBtn">保存</a>
                    <a href="javascript:history.go(-1);" class="commonBtn cancelBtn">取消</a>
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
				  <em class="tit">单位：</em>
					<label class="searchInput clearBorder">
					  <input data-text="单位" class="easyui-combobox" id="type_id" data-options="required:true,invalidMessage:'必填'" name="UnitId" type="text" value="" validType='selectIndex'>
				   </label>
				</div>
				
				
				<div class="commonSelectWrap">
					<em class="tit">系统：</em>
					<label class="searchInput clearBorder">
					  <input data-text="系统" class="easyui-combobox" id="app_sys_id" data-options="required:true,invalidMessage:'必填'" name="app_sys_id" type="text" value=""  validType='selectIndex'>
					</label>
				</div>
				
				<div class="commonSelectWrap">
				  <em class="tit">角色：</em>
				   <label class="searchInput clearBorder" id="roleIdBox">
					  <input data-text="角色" class="easyui-combobox" id="role_id" data-options="required:true,invalidMessage:'必填'"  name="RoleId" type="text" value="" validType='selectIndex'>
				   </label>
				</div>
				<p class="tipBox">角色是根据系统模块显示的，请先选择系统再选择角色。</p>
				
			</form>
		</script>
        
        
        <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/staffManagement/js/main.js"%>"></script>
</asp:Content>
