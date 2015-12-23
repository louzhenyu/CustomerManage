<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>新建用户</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/staffManagement/css/style.css?v=0.62"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
	
	.contentArea{font-family:"黑体";}
	.queryTermArea{border-bottom:1px solid #dcdcdc;background:#fff;}
	.queryTermArea .item{display:inline-block;}
    .queryTermArea .commonSelectWrap{margin-right:0px;}
	.commonSelectWrap .selectBox{width:232px;}
	#opt{padding-top:17px;border-top:none;background:#fff;}
	.optionBtn .commonBtn{width:100px;}
	.commonSelectWrap .tit{width:145px;}
	.moreQueryWrap{float:left;margin-left:25px;}
	.moreQueryWrap .queryBtn{width:73px;}
	.textbox-addon-right{right:9px !important;}
	#addRoleBtn{width:115px; text-indent:20px;background:#0cc url(images/icon-add.png) no-repeat 20px center;}
	
	.datagrid-body .datagrid-row{height:45px;}
	.datagrid-body .handle{display:inline-block;width:30px;height:45px;margin-left:8px;}
	.deleteBtn{background:url(images/delete.png) no-repeat center center;}
	.editBtn{background:url(images/edit.png) no-repeat center center;}
	.resetBtn{background:url(images/icon-reset.png) no-repeat center center;}
	.pauseBtn{background:url(images/pause.png) no-repeat center center;}
	.runningBtn{background:url(images/running.png) no-repeat center center;}
	.setUnitBtn{background:url(images/icon-tree01.png) no-repeat center center;}
	.setUnitBtn.on{background:url(images/icon-tree02.png) no-repeat center center;}
	
	
	#addProm{margin-top:50px;}
	#win .commonSelectWrap{margin:12px 10px 10px 70px;}
	#win .searchInput{width:232px;}
	.textbox-invalid{border:none;background:none;}
	.limitsTreeBox{height:200px;width:460px;margin-left:110px;border:1px solid #d0d5d8;border-radius:3px;overflow-x:auto;}
	#btnWrap{height:60px !important;}
	.importBtn,.exportBtn{float:left;width:82px;height:32px;margin-left:20px;cursor:pointer;}
	.importBtn{background:url(images/icon-import.png) no-repeat center center;}
	.exportBtn{background:url(images/icon-export.png) no-repeat center center;}
	
	.bigTitle{height:65px;line-height:65px;padding:0 25px;font-size:18px;font-family:"黑体";border-bottom:1px dashed #dcdcdc;background:#fff;color:#333;}
	.smallTitle{height:55px;font-size:15px;border-bottom:1px solid #dcdcdc;background:#fafafa;color:#333;}
	.smallTitle span{display:inline-block;line-height:16px;margin:20px 0 0 50px;padding-left:10px;border-left:3px solid #07c8cf;}
	.addStaffBtn{float:right;width:82px;height:33px;line-height:33px;margin:11px 50px 0 0;text-align:center;border-radius:4px;background:#fc7a52;color:#fff;}
	.addStaffBtn:hover{color:#fff;}
	.tableWrap{margin:20px 50px 0;border-radius:5px;}
	.panel.datagrid{min-height:300px;width:100%;border-radius:5px;}
	.datagrid-btable{width:100%;}
	.datagrid-btable tr:nth-last-of-type(1) td{border:none;}
	.staffBtn a{width:145px;height:43px;line-height:43px;margin:0 15px;font-size:18px;font-family:"Micorsoft YaHei";border-radius:22px;}
	.staffBtn .cancelBtn{background:#07c8cf;}
	.tipBox{display:inline-block;width:400px;padding-left:45px;margin:20px 0 10px 125px;line-height:20px;font-size:14px;background:url(images/icon-tips.png) no-repeat left center;color:#fc7a52;}
	.panel.layout-panel.layout-panel-south{top:280px !important;}
	.btnWrap.staffBtn{padding:0 0 35px 0;}
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/staffDetail.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
            	<div class="bigTitle">新建用户</div>
                <div class="smallTitle"><span>基本信息</span></div>
                <!--信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <em class="tit">用户名：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="用户名" class="easyui-validatebox" data-flag="user_code" name="User_Code"   id="user_code" type="text" value="" data-options="required:true" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">姓名：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="姓名" class="easyui-validatebox" data-flag="user_name" name="User_Name" id="user_name" type="text" value="" data-options="required:true" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <br />
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">密码：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="密码" class="easyui-validatebox" data-flag="User_Password" name="User_Password"   id="User_Password" type="password" value="" data-options="required:true" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">手机：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="手机" class="easyui-validatebox" data-flag="User_Telephone" name="User_Telephone" id="User_Telephone" type="text" value="" data-options="required:true" placeholder="请输入">
                                  </label>
                              </div>
                              
                        </form>
                        </div>
                </div>
                <div class="smallTitle">
                	<a href="javascript:;" class="addStaffBtn">添加</a>
                	<span>角色/权限</span>
                </div>
                <div class="tableWrap" id="tableWrap">
                   <div class=""> <table class="dataTable" id="gridTable"></table>  </div>
                    <div id="pageContianer">
                    <div class="dataMessage" style="display:none">没有符合条件的查询记录</div>
                        <div id="kkpager" >
                        </div>
                    </div>
                </div>
                
                <div class="btnWrap staffBtn">
                    <a href="javascript:;" class="commonBtn" id="staffSaveBtn">保存</a>
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
					  <input data-text="单位" class="easyui-combobox" id="type_id" data-options="required:true"  name="UnitId" type="text" value="" placeholder="请选择" validType='selectIndex'>
				   </label>
				</div>
				
				
				<div class="commonSelectWrap">
					<em class="tit">系统：</em>
					<label class="searchInput clearBorder">
					  <input data-text="系统" class="easyui-combobox" id="app_sys_id" data-options="required:true" name="app_sys_id" type="text" value="" placeholder="请选择"  validType='selectIndex'>
					</label>
				</div>
				
				
				<div class="commonSelectWrap">
				  <em class="tit">角色：</em>
					<label class="searchInput clearBorder">
					  <input data-text="角色" class="easyui-combobox" id="role_id" data-options="required:true"  name="RoleId" type="text" value="" placeholder="请选择" validType='selectIndex'>
				   </label>
				</div>
				<p class="tipBox">角色是根据系统模块显示的，请先选择系统再选择角色。</p>

			</form>
		</script>
        
        
        <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/staffManagement/js/main.js"%>"></script>
</asp:Content>
