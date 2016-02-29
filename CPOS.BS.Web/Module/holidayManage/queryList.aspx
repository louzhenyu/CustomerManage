<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>假日管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                
                <div class="tableWrap" id="tableWrap">
                     <div class="optionBtn" data-opttype="staus">
                                         <div class="commonBtn icon w100  icon_add"  data-flag="add" data-showstaus="1" >添加假日</div>
                                         <!-- <div class="commonBtn" data-status="2" data-showstaus="1,3,4,5"  >取消申请</div>-->




                    </div>
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
       <script id="tpl_optionform" type="text/html">
          <form id="optionForm">

            <div class="optionDIv">
            <input type="text"  name="HolidayId" style="display: none"/>
                    <div class="commonSelectWrap inputName">
                                       <em class="tit">假日名称：</em>
                                      <div class="searchInput">
                                         <input type="text"  name="HolidayName" class="easyui-validatebox" data-options="required:true" />
                                     </div>
                                 </div>
                    <div class="commonSelectWrap inputName">
                                       <em class="tit">假日时间：</em>
                                      <div class="searchInput bonone" style="width: 183px;">
                                         <input type="text" id="BeginDate" name="BeginDate" class="easyui-datebox" data-options="required:true,width:160,height:32" />   至
                                     </div>

                                      <div class="searchInput bonone" style="margin-top: 2px;">
                                                                              <input type="text" id="name" name="EndDate" class="easyui-datebox" data-options="required:true,width:160,height:32"  validType="compareEqualityDate[$('#BeginDate').datebox('getText')]" />
                                                                          </div>
                                 </div>

                         <div class="commonSelectWrap" style="height: 124px;">
                            <em class="tit">描述：</em>
                           <div class="searchInput remark" >
                              <textarea type="text" name="Desciption" class="easyui-validatebox" data-options="validType:'maxLength[50]'" placeholder="请输入"></textarea>
                          </div>
                          </div>
                   </div>
           </form>
          </script>
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
