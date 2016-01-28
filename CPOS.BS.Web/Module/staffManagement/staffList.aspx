<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>员工管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/staffManagement/css/style.css?v=0.62"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
	.queryTermArea{border-bottom:1px dashed #dcdcdc;background:#fafafa;}
	.queryTermArea .item{display:inline-block;}
    .queryTermArea .commonSelectWrap{margin-right:0px;}
	.commonSelectWrap .selectBox{width:232px;}
	#opt{padding-top:17px;border-top:none;background:#fff;}
	.optionBtn .commonBtn{width:100px;}
	.commonSelectWrap .tit{width:105px;}
	.moreQueryWrap{float:left;margin-left:25px;}
	.moreQueryWrap .queryBtn{width:73px;}
	.textbox-addon-right{right:9px !important;}
	#addRoleBtn{width:115px; text-indent:20px;background:#0cc url(images/icon-add.png) no-repeat 20px center;}
	
	.datagrid-body .datagrid-row{}
	.datagrid-body .handle{display:inline-block;width:30px;height:39px;margin-left:8px;}
	.deleteBtn{background:url(images/delete.png) no-repeat center center;}
	.editBtn{background:url(images/edit.png) no-repeat center center;}
	.resetBtn{background:url(images/icon-reset.png) no-repeat center center;}
	.pauseBtn{background:url(images/pause.png) no-repeat center center;}
	.runningBtn{background:url(images/running.png) no-repeat center center;}
	
	#win .commonSelectWrap{margin:12px 10px 32px 0;}
	#win .searchInput{width:232px;}
	.textbox-invalid{border:none;background:none;}
	.limitsTreeBox{height:200px;width:460px;margin-left:110px;border:1px solid #d0d5d8;border-radius:3px;overflow-x:auto;}
	#btnWrap{height:60px !important;}
	.importBtn,.exportBtn{float:left;width:82px;height:32px;margin-left:20px;cursor:pointer;}
	.importBtn{background:url(images/icon-import.png) no-repeat center center;}
	.exportBtn{background:url(images/icon-export.png) no-repeat center center;}
	.loading{width:81%;}
	
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/staffList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <em class="tit">用户名：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="用户名" data-flag="user_code" name="user_code"   id="user_code" type="text" value="" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">姓名：</em>
                                  <label class="searchInput" style="width:232px;">
                                      <input data-text="姓名" data-flag="user_name" name="user_name" id="user_name" type="text" value="" placeholder="请输入">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">所属单位：</em>
                                  <div class="selectBox">
                                       <input id="type_id" name="unit_id"  class="easyui-combobox"   data-options="width:232,height:32" />
                                  </div>
                              </div>
                              
                              <div class="moreQueryWrap">
                                   <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit">所属角色：</em>
                                  <div class="selectBox">
                                       <select id="role_id" name="role_id" class="easyui-combobox"   data-options="width:232,height:32"></select>
                                  </div>
                              </div>
                              
                        </form>
                        </div>
                    <!--<h2 class="commonTitle">会员查询</h2>-->
                </div>
                <div class="tableWrap" id="tableWrap">
                   <div class="optionBtn" id="opt">
                       <div class="commonBtn" id="addUserBtn">新增员工</div>
                       <div class="importBtn"  id="inportvipmanageBtn"></div>
                       <div class="exportBtn" style="display:none"></div>
                   </div>
                   <div class="">
                       <table class="dataTable" id="gridTable">
                           <div class="loading">
                              <span><img src="../static/images/loading.gif"></span>
                           </div>
                       </table>
                   </div>
                   <div id="pageContianer">
                       <div class="dataMessage">没有符合条件的查询记录</div>
                       <div id="kkpager"></div>
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

            <div id="win1" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="Div2">

      			<div data-options="region:'center'" style="padding:0px;">


			            <div class="qb_member">
                            <div id="step1" class="member step">
                                <div class="menber_title"><img src="images/lc_1.jpg" /></div>
                                <div class="menber_center">
                                    <div class="menber_centernr">
                                        <div class="menber_centernrt">
                                            <p>请按照数据模板的格式准备要导入的数据<a href="#">（如何导入？）</a></p>
                                            <p><a href="/File/ExcelTemplate/用户导入数据模板.xlsx">下载模板</a></p>
                                            <div class="attention"><span>注意事项：</span>
                                                1.模板中的表头名称不可更改、表头行不能删除。<br />
                                                2.项目顺序可以调整，不需要的项目可以删除。<br />
                                                3.表中的会员姓名、手机号为必填项目，必须保留。<br />
                                                4.导入文件请勿超过 1 MB。
                                            </div>
                                        </div>
                                        <div class="menber_centernrb" id="editLayer">
                                            选择需要导入的CSV文件
                                            <p id="nofiletext" >未选择文件</p>
                                             <div class="CSVFilelist"></div>
                                            <input id="CSVFileurl" value="" type="hidden"  />
                                           <input type="file" class="uploadCSVFileBtn" />
                                        </div>
                                    </div>  
	                            </div>
                            </div>

                             <div id="step2"  class="member step" style="display:none">
                                <div class="menber_title"><img src="images/lc_2.jpg" /></div>
                                    <div class="menber_center">
                                        <div class="menber_centernr">
                                            <div class="memberloading">导入中...</div>
                                            <div class="attention"><span>提示：</span>
                                                1.导入过程中请勿关闭此页面；<br />
                                                2.数据导入结束后，可能下载错误报告，以便重新处理。
                                            </div>
        	                            </div>
		                            </div>
                            </div>


                            <div id="step3"  class="member step" style="display:none">
                                <div class="menber_title"><img src="images/lc_3.jpg" /></div>
                                <div class="menber_center">
                                    <div class="menber_centernr">
                                        <div class="succeed">导入完成<p>共<span id="inputTotalCount" class="inputCount"> 0</span> 条，成功导入<span  id="inputErrCount" class="red inputCount"> 0</span> 条</p></div>
                                        <div class="menber_centernrb1">
                	                        下载错误报告，查看失败原因
                                            <p><a id="error_report" href="javascipt:void(0)">error_report.csv<span>选择文件</span></a></p>
                    
                                        </div>
        	                        </div>
		                        </div>
                            </div>


                            </div>
			
			  


      			</div>
      			<div class="btnWrap" id="btnWrap1" data-options="region:'south',border:false" style="height:80px;text-align:right;padding:5px 20px 0;">
      				<a id="startinport"  class="easyui-linkbutton commonBtn saveBtn" >开始导入</a>  
                      <a id="closebutton" style="display:none;" class="easyui-linkbutton commonBtn closeBtn close" >关闭</a>
      			</div>
      		</div>

      	</div>

        </div>
        
        
        <script id="tpl_addProm" type="text/html">
			<form id="addProm">
			<div class="commonSelectWrap">
			  <em class="tit">角色：</em>
				<label class="searchInput clearBorder">
				  <input data-text="角色" class="easyui-validatebox" id="role_name2" data-options="required:true"  name="role_name" type="text" value="" placeholder="请输入2-6位字符">
			   </label>
			</div>
			
			<div class="commonSelectWrap">
			  <em class="tit">所属层级：</em>
				<label class="searchInput clearBorder">
				  <input data-text="所属层级" class="easyui-validatebox" id="type_id2" data-options="required:true"  name="type_id" type="text" value="" placeholder="请选择" validType='selectIndex'>
			   </label>
			</div>
			
			
			
			<div class="commonSelectWrap">
				<em class="tit">应用系统：</em>
				<label class="searchInput clearBorder">
				  <input data-text="应用系统" class="easyui-combobox" id="app_sys_id2" data-options="required:true" name="app_sys_id2" type="text" value="" placeholder="请选择"  validType='selectIndex'>
				</label>
			</div>
			
			
			
			<div class="commonSelectWrap" style="clear:left;height:auto;">
			  <em class="tit">拥有权限：</em>
			  	<!--
				<label class="searchInput clearBorder">
				  <input data-text="拥有权限" class="easyui-validatebox" id="limitsTreeBox" data-options="required:true"  name="limitsTreeBox" type="text" value="" validType='selectIndex'>
			    </label>
			    -->
				<div class="limitsTreeBox" id="limitsTreeBox">
					
				</div>
				
				
			</div>
			
			  
			</form>
		</script>
        
        
        <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/staffManagement/js/main.js"%>"></script>
</asp:Content>
