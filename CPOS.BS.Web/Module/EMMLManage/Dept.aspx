﻿<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Module/EMMLManage/Qixin.Master" CodeBehind="Dept.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.EMMLManage.Dept" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../static/css/artDialog.css" rel="stylesheet" />
    <link href="css/kkpager.css" rel="stylesheet" type="text/css" />
    <link href="../styles/css/qixin/private.css" rel="stylesheet" />
    <style>
        #sureTable .dataTable tr,td { text-align:left;  }
        .jui-dialog .tit{text-indent:30px; background:url();}
    </style>
     <section id="section" data-js="js/Dept.js">
        	<!--详情菜单-->
            <div class="subMenu">
                <ul class="clearfix">
                    <li class="nav01 on">组织架构</li>
                    <li class="nav02"> 职务管理</li>
                    <li class="nav03">人员管理</li>
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
                            <em class="tit">关键字</em>
                            <label class="searchInput"><input id="searchKeyword" type="text" value=""></label>
                        </div>
                    </div>
                </div>
                
                <!--表格操作按钮-->
                <div class="tableWrap">
                    <div class="tablehandle">
                    	<a href="javascript:;" class="commonBtn exportBtn" style="display:none" >导出</a>
                        <a id="importBtn" href="javascript:;" class="commonBtn importBtn" style="display:none">导入</a>
                        <div class="selectBox fl" >
                           <span data-val="oper" class="text">操作</span>
                            <div class="selectList">
                                <p data-val="del" id="delSelect" >批量删除</p>
                            </div>
                        </div>
                        <a id="appInfoBtn" href="javascript:;" class="commonBtn appInfoBtn">添加部门</a>
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
<div id="maskDisplay" class="jui-mask" style="display:none"></div>
<div id="dialogEditDiv" class="jui-dialog jui-dialog-staff" style="display:none; width:500px;">
	<div class="jui-dialog-tit"><h2>添加部门</h2><span id="dialogEditClose" class="jui-dialog-close"></span></div>
    <div class="fromContent">
    	  <div class="addInfoArea">
              
                <div class="item-line">
                  <div class="commonSelectWrap">
                    <em class="tit"></em>
                    <label class="searchInput"></label>
                   </div>
                </div>
                <div class="item-line">
                    <div class="commonSelectWrap">
                    <em class="tit"></em>
                    <label class="searchInput"></label>
                   </div>
                </div>
                <div class="item-line">
                    <div class="commonSelectWrap">
                    <em class="tit"></em>
                    <label class="searchInput"><</label>
                   </div>
                </div>
          </div>
          <div class="btnWrap">
            	<a id="saveBtn" href="javascript:;" class="commonBtn">保存</a>
                <a id="cancelBtn" href="javascript:;" class="commonBtn cancelBtn">取消</a>
            </div>
        </div>
</div>

        <div id="dvEditMessage" style="display: none;">
        <div class="commdialog dig-number" style="width:500px">
            <div class="row">
                <em class="tit"><span class="fontRed">*</span>部门编码</em>
                <p class="input">
                    <input id="txtDeptCode" type="text" /></p>
            </div>
            <div class="row">
                <em class="tit"><span class="fontRed">*</span>部门名称</em>
                <p class="input">
                    <input id="txtDeptName" type="text" class="w320" /></p>
            </div>
             <div class="row">
                <em class="tit"><span class="fontRed"></span>负责人</em>
                <p class="input">
                    <input id="txtLeader" type="text" class="w120" /></p>
            </div>
             <div class="row">
                <em class="tit"><span class="fontRed"></span>部门描述</em>
                <p class="input">
                    <input id="txtDeptDesc" type="text" class="w320" /></p>
            </div>
            <div class="btns">
                <a class="commonBtn addBtn" href="javascript:;">保存</a> <a class="commonBtn cancelBtn"
                    href="javascript:;">取消</a>
            </div>
        </div>
    </div>

     <script id="sureTheadTemp" type="text/html">
       <th class="selectListBox" style="text-align:center; width:80px"><span>选择</span>
           <div class="minSelectBox">
               <em class="minArr"></em>
               <p id="selectedAllCurPage">全选本页</p>
               <p id="selectedAllPage" >全选所有页</p>
               <p id="removeSelected">取消选择</p>
           </div></th>
       <th  style="text-align:center">操作</th>
        <%--<th class="checkBox" ><em></em></th>--%>
           <#for(var i in obj){#>
           <th><#=obj[i]#>
           </th>
        <#}#>
    </script>

     <script id="sureTbodyTemp" type="text/html">
    <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
    <tr>
        <td class="checkBox" data-id="<#=list.otherItems[i].UnitID #>"><em></em></td>
        <td class="operateWrap">
            <span class="editIcon"></span>
            <span class="delIcon"></span></td>

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

<div id="confirmMask" class="jui-mask" style="display:none;"></div>
<div id="idConfirm" class="jui-dialog" style="display:none; top:200px; height:200px; width:400px;">
    <div class="jui-dialog-tit" style="height:30px;">
    	<h2 style="height:30px;font:bold 12px;">提示</h2>
        <a href="javascript:;" class="jui-dialog-close hintClose"></a>
    </div>
    <div class=" height:180px; " id="message">
    	<div class="stepWrap" style="text-align:center" >
        	确定要删除吗？
        </div>
        <div class="btnWrap" style="height:20px;">
        	<a href="javascript:;" id="delOk" class="commonBtn"  style="line-height:26px; height:26px; width:80px;">确定</a>
            <a href="javascript:;" id="delCancel" class="commonBtn" style="line-height:26px; height:26px; width:80px;">取消</a>
        </div>
    </div>
</div>
</asp:Content>