<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Module/EMMLManage/Qixin.Master" CodeBehind="Sample.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.EMMLManage.WebForm1" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../static/css/artDialog.css" rel="stylesheet" />
    <link href="css/kkpager.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Framework/swfuploadfile/uploadify.css" />
    <link href="../styles/css/qixin/private.css" rel="stylesheet" />
       <style>
           #sureTable .dataTable tr, td{ text-align: left;}
           .jui-dialog .tit{text-indent:30px; background:url();}
       </style>
     <section id="section" data-js="js/userList.js">
        	<!--详情菜单-->
            <div class="subMenu">
                <ul class="clearfix">
                    <li class="nav01">组织架构</li>
                    <li class="nav02"> 职务管理</li>
                    <li class="nav03 on">人员管理</li>
                </ul>
            </div>
        	
            <div class="subMenu-content">
                <!--个别查询-->
                <div class="queryTermArea">
                    <div class="item">
                        <div class="moreQueryWrap">
                            <a id="queryBtn" href="javascript:;" class="commonBtn queryBtn">查询</a>
                            <a href="javascript:;" class="more" style="display:none" >更多查询条件</a>
                        </div>
                        <div class="commonSelectWrap">
                            <em class="tit">部门</em>
                            <div id="deptSelectBox"  class="selectBox">
                            	<%--<span class="text">部门1</span>
                                <div class="selectList">
                                	<p>部门1</p>
                                    <p>分类2</p>
                                </div>--%>
                            </div>
                        </div>
                        <div class="commonSelectWrap">
                            <em class="tit">关键字</em>
                            <label class="searchInput"><input id="searchKeyword" type="text" value=""></label>
                        </div>
                        <div class="commonSelectWrap selectDateBox" style="display:none" >
                        	<span class="tit">从</span>
                            <p><input type="text" disabled  value=""/></p>
                            <span class="tit">至</span>
                            <p><input type="text" disabled value=""/></p>
                        </div>
                    </div>
                </div>
                
                <!--表格操作按钮-->
                <div class="tableWrap">
                    <div class="tablehandle">
                    	<a id="exportBtn" href="javascript:;" class="commonBtn exportBtn">导出员工</a>
                        <a id="importBtn" href="javascript:;" class="commonBtn importBtn">导入员工</a>
                        <div class="selectBox fl" style="z-index:auto" >
                            <span data-val="oper" class="text">操作</span>
                            <div class="selectList">
                                <p data-val="logout" id="logoutAll" >注销</p>
                            </div>
                        </div>
                        <a id="appInfoBtn" href="javascript:;" class="commonBtn appInfoBtn">添加员工</a>
                    </div>
                    <!-- 已确认名单表格 -->
                    <table id="sureTable" class="dataTable" style="display:inline-table;">
                        <thead>
                        </thead>
                        <tbody>
                             <tr>
                                 <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="9" align="center"><span>
                                   <img src="../static/images/loading.gif"></span></td>
                             </tr>
                        </tbody>
                    </table>
                    <div id="pageContianer">
                         <div id="kkpager" style="text-align:center;"></div>
                     </div>

                </div>
            </div>

<!-- 弹层，添加员工 -->
<div id="maskDisplay" class="jui-mask" style="display:none"></div>
<div id="dialogStaffDiv" class="jui-dialog jui-dialog-staff" style="display:none">
	<div class="jui-dialog-tit"><h2>编辑员工</h2><span id="dialogStaffClose" class="jui-dialog-close"></span></div>
    <div class="fromContent">
    	<div class="fromWrap">
           <a id="logOutBtn" href="javascript:;" class="logOut" style="display:none">离职注销</a>

            <div class="from-item">
            	<p class="tit email">邮箱：</p>
                <div class="from-item-input">
                	<input id="txtUserEmail" type="text" value="" />
                </div>
            </div>

            <div class="from-item">
            	<p class="tit idNumber">员工编码：</p>
                <div class="from-item-input">
                	<input id="txtUserCode" type="text" value="" />
                </div>
            </div>

            <div class="from-item">
            	<p class="tit name">姓名：</p>
                <div class="from-item-input">
                	<input id="txtUserName" type="text" value="" />
                </div>
            </div>

            <div class="from-item">
            	<p id="sexIcon" class="tit sex">性别：</p>
                <div id="ddlUserGender" class="from-item-select">
                	<p data-val="1" class="textBox">男</p>
                    <div class="listBox">
                    	<p id="gender-item-1" data-val="1" >男</p>
                        <p id="gender-item-2" data-val="2" >女</p>
                    </div>
                </div>
            </div>
            
            <div class="from-item">
            	<p class="tit branch">所属部门：</p>
                <div id="deptSelect"  class="from-item-select">
                </div>
            </div>

            <div class="from-item">
            	<p class="tit duty">职务：</p>
                <div id="jobFuncSelect"  class="from-item-select">
                </div>
            </div>
            
            <div class="from-item">
            	<p class="tit superior">直属上级：</p>
                <div id="lineManagerSelect"  class="from-item-select">
                    <input id="txtLineManage"  type="text" value=""  />
                    <div class="listBox"></div>
                     <%--<div class="listBox">
                    	<p id="P1" data-val="1" >男</p>
                        <p id="P2"data-val="2" >女</p>
                    </div>--%>
                </div>
            </div>
            
            <div class="from-item">
            	<p class="tit phone">手机：</p>
                <div class="from-item-input">
                	<input id="txtUserTelephone" type="text" value="" />
                </div>
            </div>
            
            <%--<div class="from-item">
            	<p class="tit jurisdiction">创建讨论组权限：</p>
                <div id="ddlIsGroupCreater" class="from-item-select">
                	<p data-val="0" class="textBox">不允许</p>
                    <div class="listBox">
                    	<p data-val="0">不允许</p>
                        <p data-val="1">允许</p>
                    </div>
                </div>
            </div>--%>
            
            <div class="from-item" >
            	<p class="tit time">入职日期：</p>
                <div class="from-item-input dateIcon">
                	<input id="txtEntryDate" disabled="disabled" type="date" value="" />
                    <input id="txtUserBirthday" style=" display:none"  type="date" value="" />
                </div>
            </div>
            <div class="btnWrap">
            	<a id="saveBtn" href="javascript:;" class="commonBtn">保存</a>
                <a id="dialogStaffCancleBtn" href="javascript:;" class="commonBtn cancelBtn">取消</a>
            </div>
        </div>
    </div>
</div>


<!-- 弹层，导入-->
<div class="jui-mask" style="display:none;"></div>
<div id="importDiv" class="jui-dialog jui-dialog-import" style="display:none">
    <div class="jui-dialog-tit">
    	<h2>导入</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div class="promptContent" style="display:block" id="step1">
    	<div class="stepWrap">
        	<span class="step01"></span>
            <span class="step02"><em></em></span>
        </div>
        <p class="exp">点击下载标准导入模板，并根据模板填写内容，如已完成此项可点击下一步</p>
        <a href="javascript:;" id="downloadTmpl" class="downTmplBtn"></a>
        <div class="btnWrap">
            <a href="javascript:;" id="nextStep" class="commonBtn">下一步</a>
        </div>
    </div>
    
    <div class="promptContent stepTwo" id="step2" style="display:none">
    	<div class="stepWrap">
        	<span class="step01"></span>
            <span class="step02 "><em></em></span>
        </div>
        <p class="exp">请导入标准格式，您可以点击返回按钮来下载模板</p>
        <div class="uploadArea">
           	<a href="javascript:;" class="uploadBtn"></a>
            <input id="file_upload" name="file_upload" type="file" />

        	<p class="uploadInput"><input id="uploadText" type="text" value=""></p>
        </div>
        <div><p id="exportStatus"></p></div>
        <div class="btnWrap">
        	<a href="javascript:;" id="backStep" class="commonBtn cancelBtn">返回</a>
            <a href="javascript:;" id="comitUpload" class="commonBtn">完成</a>
        </div>
    </div>
 
</div>

     <script id="sureTheadTemp" type="text/html">
       <th class="selectListBox" style="text-align:center"><span>选择</span>
           <div class="minSelectBox">
               <em class="minArr"></em>
               <p id="selectedAllCurPage">全选本页</p>
               <p id="selectedAllPage" >全选所有页</p>
               <p id="removeSelected">取消选择</p>
           </div></th>
       <th  style="text-align:center">操作</th>
        <%--<th class="checkBox" ><em></em></th>--%>
           <#for(var i in obj){#>
           <th>
               <# if(i=="UserEmail"||i=="UserName"||i=="UserGender"){#>
               <%--<img data-sort="asc" class="imgSort" src="images/selectIcon02.png" data-id="<#=i#>"  alt="" />--%>
                <span data-sort="asc" class="imgSort" data-id="<#=i#>" ><#=obj[i]#></span>
               <#}else {#>
                 <#=obj[i]#>
               <#}#>
           </th>
        <#}#>
    </script>

     <script id="sureTbodyTemp" type="text/html">
    <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
    <tr>
        <td class="checkBox" data-id="<#=list.otherItems[i].UserID #>"><em></em></td>
        <td class="operateWrap">
            <span class="editIcon"></span>
            <span class="logoutIcon"></span></td>

        <#for(var e in idata){#>
            <# var result=0;
             if(e=="UserGender"){
                if(idata[e]==1){
                    result="男";
                }else if(idata[e]==2){
                    result="女";
                }else{
                    result="未知";
                } 
        }else  if(e=="UserStatus"){
            if(idata[e]==1){
                result="在职";
            }else if(idata[e]==0){
                result="离职";
            }
        }            
        #>
        <td>
            <# if(e=="HeadImageUrl"){#>
            <img src="<#=list.otherItems[i].HeadImageUrl #>"  style=" width:40px; height:40px" />
            <# }else if(e=="UserName"){#>
             <#=idata[e]#>
             <#}else{#>
                <#=result?result:idata[e]#>
            <#}#>
       </td>
        <#}#>
    </tr>
    <#} #>
</script>

     <script id="selectTemp" type="text/html">
          <%--<p class="textBox" data-val="<#=list[0].id #>" ><#=list[0].name #></p>--%>
         <p class="textBox" data-val="" ></p>
          <div class="listBox">
            <#for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
               <p id="<#=idprefix #>-<#=idata.id #>" data-val="<#=idata.id #>"><#=idata.name#></p>
            <#}#>
           </div>
    </script>

     <script id="leaderSelectTemp" type="text/html">
            <#for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
               <p id="<#=idprefix #>-<#=idata.id #>" data-val="<#=idata.id #>"><#=idata.name#></p>
            <#}#>
    </script>

    <script id="selectTemp2" type="text/html">
       <%-- <span class="text" data-val="<#=list[0].id #>"><$=list[0].name #></span>--%>
        <span class="text" data-val=""></span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p data-val="<#=idata.id #>"><#=idata.name #></p>
        <#}#>
        </div>
    </script>

 </section>

<div id="confirmMask" class="ui-pc-mask" style="display:none;"></div>
<div id="idConfirm" class="jui-dialog" style="display:none; top:200px; height:200px; width:400px;">
    <div class="jui-dialog-tit" style="height:30px;">
    	<h2 style="height:30px;font:bold 12px;">提示</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div class=" height:180px; " id="message">
    	<div class="stepWrap" style="text-align:center" >
        	确定要注销吗？
        </div>
        <div class="btnWrap" style="height:20px;">
        	<a href="javascript:;" id="logOutOk" class="commonBtn"  style="line-height:26px; height:26px; width:80px;">确定</a>
            <a href="javascript:;" id="logOutCancel" class="commonBtn" style="line-height:26px; height:26px; width:80px;">取消</a>
        </div>
    </div>
</div>

</asp:Content>
