﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>配送商管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                    <div class="optionBtn" data-opttype="staus">
                                         <div class="commonBtn"  data-flag="add" data-showstaus="1" >添加</div>
                                         <!-- <div class="commonBtn" data-status="2" data-showstaus="1,3,4,5"  >取消申请</div>-->




                    </div>
                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap">

                    <table class="dataTable" id="gridTable"></table>
                    <div id="pageContianer">
                    <div class="dataMessage" >数据没有对应记录</div>
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
       <!-- 取消订单-->
       <script id="tpl_addExpress" type="text/html">
            <form id="addExpress">
           <div class="commonSelectWrap">
             <div class="radio on" data-name="r1" data-hide="inputName" data-opttype="1" data-issystem="1"  data-show="selectName"><em></em><span>选择预设的快递公司</span></div>
           </div>
            <div class="commonSelectWrap selectName">
            <em class="tit"></em>
                                      <div class="selectBox">
                                         <input type="text" name="LogisticsID" id="selectList" data-options="width:160,height:32,required:true,editable:false" class="easyui-combobox"  />
                                     </div>
                                 </div>
          <div class="commonSelectWrap">
                      <div class="radio " data-name="r1" data-opttype="2" data-issystem="2" data-hide="selectName" data-show="inputName"><em></em><span>新增快递公司</span></div>
                    </div>

           <div class="commonSelectWrap inputName">
                            <em class="tit">名称：</em>
                           <div class="searchInput">
                              <input type="text" id="name" name="LogisticsName" class="easyui-validatebox" data-options="required:true" />
                          </div>
                      </div>
             <div class="commonSelectWrap inputName">
                             <em class="tit">简称：</em>
                            <div class="searchInput">
                               <input type="text" name="LogisticsShortName" />
                           </div>
                       </div>

           </form>
       </script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>