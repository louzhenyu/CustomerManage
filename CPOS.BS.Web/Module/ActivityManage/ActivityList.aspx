<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link  href="/module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section id="section" data-js="js/activityManage.js">
        <!-- 内容区域 -->
        <div class="contentArea">
        	<!--详情菜单-->
<%--            <div class="subMenu">
                <ul class="clearfix">
                    <li class="nav01">组织架构</li>
                    <li class="nav02">职务管理</li>
                    <li class="nav03 on">人员管理</li>
                </ul>
            </div>--%>
        	
            <div class="subMenu-content">
                <!--个别查询-->
                <form action="" method="post" id="queryForm">
                    <div class="queryTermArea">
                        <div class="item">
                            <div class="moreQueryWrap">
                                <a id="queryBtn" href="javascript:;" class="commonBtn queryBtn">查询</a>
                                <%--<a href="javascript:;" class="more">更多查询条件</a>--%>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">活动类型</em>
                                <div id="eventTypeSelect" name="EventTypeID" class="selectBox">
                                </div>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">主办方</em>
                                <div id="sponsorSelect" name="Sponsor" class="selectBox">
                                </div>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">活动标题</em>
                                <label class="searchInput"><input id="eventTitle" name="EventTitle" type="text" value=""></label>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">开始时间</em>
                                <label class="searchInput"><input type="text" id="beginTime" name="BeginTime" /></label>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">结束时间</em>
                                <label class="searchInput"><input type="text" id="endTime" name="EndTime" /></label>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">活动状态</em>
                                <div id="statusSelect" name="EventStatus" class="selectBox">
                                </div>
                            </div>
                        </div>
                    </div>
                </form>

                <!--表格操作按钮-->
                <div class="tableWrap">
                    <div class="tablehandle">
<%--                    	<a href="javascript:;" class="commonBtn exportBtn">导出员工</a>
                        <a href="javascript:;" class="commonBtn importBtn">导入员工</a>--%>
<%--                        <div class="selectBox fl">
                            <span class="text">操作</span>
                            <div class="selectList">
                                <p>操作1</p>
                                <p>操作2</p>
                            </div>
                        </div>--%>
                        <a id="addBtn" href="ActivityEdit.aspx" class="commonBtn appInfoBtn">添加活动</a>
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
        </div>

<!-- 弹层，添加员工 -->
<div id="maskDisplay" class="ui-mask" style="display:none"></div>
<div id="dialogStaffDiv" class="ui-dialog ui-dialog-staff" style="display:none">
	<div class="ui-dialog-tit"><h2>编辑员工</h2><span id="dialogStaffClose" class="ui-dialog-close"></span></div>
    <div class="fromContent">
    	<div class="fromWrap">
           <a id="logOutBtn" href="javascript:;" class="logOut">离职注销</a>

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
 
           <%-- <div class="from-item">
            	<p class="tit email">密码：</p>
                <div class="from-item-input">
                	<input id="txtUserPassword" type="password" value="" />
                </div>
            </div>--%>
         
            <div class="from-item">
            	<p class="tit sex">性别：</p>
                <div id="ddlUserGender" class="from-item-select">
                	<p data-val="1" class="textBox">男</p>
                    <div class="listBox">
                    	<p id="gender-item-1" data-val="1" >男</p>
                        <p id="gender-item-2"data-val="2" >女</p>
                    </div>
                </div>
            </div>
            
            <div class="from-item">
            	<p class="tit branch">所属部门：</p>
                <div id="deptSelect"  class="from-item-select">
                	<%--<p class="textBox">研发</p>
                    <div class="listBox">
                    	<p>研发</p>
                        <p>人事</p>
                    </div>--%>
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
                </div>
            </div>
            
            <div class="from-item">
            	<p class="tit phone">手机：</p>
                <div class="from-item-input">
                	<input id="txtUserTelephone" type="text" value="" />
                </div>
            </div>
            
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

     <script id="sureTheadTemp" type="text/html">
       <th class="selectListBox">选择<img src="images/selectIcon02.png" alt="" />
           <div class="minSelectBox">
               <em class="minArr"></em>
               <p id="selectedAllCurPage">全选本页</p>
               <p id="selectedAllPage" >全选所有页</p>
               <p id="removeSelected">取消选择</p>
           </div></th>
       <th>操作</th>
        <%--<th class="checkBox" ><em></em></th>--%>
        <#for(var i in obj){#>
           <th><#=obj[i]#>
               <# if(i=="UserEmail"||i=="UserName"||i=="UserGender"){#>
               <img src="images/selectIcon02.png" data-id="<#=i#>"  alt="" />
               <#}#>
           </th>
           <%-- <th><#=obj[i]#></th>--%>
        <#}#>
    </script>

     <script id="sureTbodyTemp" type="text/html">
    <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
    <tr>
        <td class="checkBox" data-id="<#=list.otherItems[i].EventID #>"><em></em></td>
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
                result="正常";
            }else if(idata[e]==0){
                result="注销";
            }
        }            
        #>
        <td>
            <# if(e=="HeadImageUrl"){#>
            <img src="<#=list.otherItems[i].HeadImageUrl #>"  style=" width:40px; height:40px" />
            <# }else if(e=="UserName"){#>
               <!-- <input type="text" class="group" data-id="<#=list.otherItems[i].UnitCode #>" value="<#=idata[e]#>">-->
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
        <span class="selected text">全部</span> <%--data-val="<#=list[0].id#>"<#=list[0].name#>--%>
        <div class="selectList">
            <p class="option" data-val="">全部</p>
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.id#>"><#=idata.name#></p>
        <#}#>
        </div>
    </script>

 </section>
</asp:Content>
