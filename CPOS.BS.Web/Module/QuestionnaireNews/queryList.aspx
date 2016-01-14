<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>花样问卷</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/QuestionnaireNews/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">

            <!--个别信息查询-->
            
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <em class="tit">名称：</em>
                                  <label class="searchInput" style="width:387px;">
                                      <input data-text="名称" data-flag="item_name" name="QuestionnaireName" type="text" value="">
                                  </label>
                              </div>
                              <div class="commonSelectWrap">
                                  <em class="tit">类别：</em>
                                  <div class="selectBox">
                                      <input id="QuestionnaireType" data-text="类别" data-flag="item_type" name="QuestionnaireType" type="text" value="">
                                  </div>
                              </div>
                              <div class="moreQueryWrap">
                                 <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                              </div>
                          </form>

                        </div>
                </div>
                
            
            <div class="tableWrap" id="tableWrap">
                <div class="optionBtn" id="opt">
                	<div class="commonBtn" id="addUserBtn">+新增</div>
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
      				 <div class="optionclass" style="display:none">
                       <div class="commonSelectWrap SelectType">
                            <img data-typeid="1" data-nochecked="/Module/QuestionnaireNews/images/wj.png" data-checked="/Module/QuestionnaireNews/images/wj_bg.png" src="/Module/QuestionnaireNews/images/wj.png" />
                            <img data-typeid="4" data-nochecked="/Module/QuestionnaireNews/images/bm.png" data-checked="/Module/QuestionnaireNews/images/bm_bg.png"  src="/Module/QuestionnaireNews/images/bm.png" />
                            <img data-typeid="2" data-nochecked="/Module/QuestionnaireNews/images/tp.png" data-checked="/Module/QuestionnaireNews/images/tp_bg.png"  src="/Module/QuestionnaireNews/images/tp.png" />
                            <img data-typeid="3" data-nochecked="/Module/QuestionnaireNews/images/cs.png" data-checked="/Module/QuestionnaireNews/images/cs_bg.png"  src="/Module/QuestionnaireNews/images/cs.png" />
                        </div>
                    </div>
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;display:none;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      			</div>
      		</div>

      	</div>
      </div>

    

    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/Module/QuestionnaireNews/js/main.js"%>"></script>
</asp:Content>
