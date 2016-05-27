<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>配送商管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <link href="<%=StaticUrl+"/module/expressManage/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                    <div class="tableWrapTextBtn optionBtn" data-opttype="staus">
                                         <div class="commonBtn icon icon_add w80 r"  data-flag="add" data-showstaus="1" >新增</div>
                    </div>
                <div class="tableWrap cursorDef" id="tableWrap">

                    <div class="dataTable" id="gridTable">
                    	<div class="loading" style="padding-top:35px;">
                          <span><img src="../static/images/loading.gif"></span>
                        </div>
                    </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >暂时没有数据！</div>
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
                                         <input type="text" name="LogisticsID" id="selectList" data-options="width:160,height:32,required:true,editable:false" class="easyui-combobox"/>
                                     </div>
                                 </div>
          <div class="commonSelectWrap">
                      <div class="radio " data-name="r1" data-opttype="2" data-issystem="2" data-hide="selectName" data-show="inputName"><em></em><span>新增快递公司</span></div>
                    </div>

           <div class="commonSelectWrap inputName">
                            <em class="tit">名称：</em>
                           <div class="searchInput">
                              <input type="text" id="name" name="LogisticsName" class="easyui-validatebox" data-options="required:true,validType:'maxLength[50]'" />
                          </div>
                      </div>
             <div class="commonSelectWrap inputName">
                             <em class="tit">简称：</em>
                            <div class="searchInput">
                               <input type="text" name="LogisticsShortName"  data-options="validType:'maxLength[20]'" />
                           </div>
                       </div>

           </form>
       </script>

</asp:Content>
