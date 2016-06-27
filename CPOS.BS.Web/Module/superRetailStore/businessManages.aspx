<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>分销商管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/superRetailStore/css/businessManages.css?v1.1"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="allPage" id="section" data-js="js/businessManages.js">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                          <div class="searchLineBox">
                              <div class="commonSelectWrap">
                                  <em class="tit">分销商姓名：</em>
                                  <label class="searchInput" >
                                      <input data-text="分销商姓名"  name="SuperRetailTraderName" type="text" value="" placeholder="">
                                  </label>
                              </div>
                              
                              <div class="commonSelectWrap">
                                  <em class="tit" style="width:150px;">分销商发展来源：</em>
                                  <div class="selectBox">
                                        <!--item_category_id-->
                                        <select id="SuperRetailTraderFrom" name="SuperRetailTraderFrom"></select>
                                  </div>
                              </div>

                              <div class="moreQueryWrap">
                                   <a href="javascript:;" class="commonBtn queryBtn w80">查询</a>
                              </div>
                           </div>   
                              
                          <div class="commonSelectWrap joinTimeBox">
                               <em class="tit">加盟时间：</em>
                               <div class="searchInput bordernone" style="width:auto">
                                    <input id="JoinSatrtTime"  name="JoinSatrtTime"  class="joinTime"/>
                                    <span class="toText"> 至 </span>
                                    <input id="JoinEndTime" name="JoinEndTime"  class="joinTime"/>
                               </div>
                         </div>

                        </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap">
                <div class="optionBtn" id="opt">
                	<div class="icon icon_import commonBtn w80 r"  id="inportStoreBtn">导入</div>
                    <div class="exportBtn commonBtn w80" id="exportBtn">导出</div>
                </div>
						<div class="cursorDef">
                            <div class="dataTable" id="gridTable">
                                <div class="loading" style="margin-top:-1px;">
                                   <span><img src="../static/images/loading.gif"></span>
                                </div>
                            </div>
                        </div>

                    <div id="pageContianer">
                    <div class="dataMessage">没有符合条件的查询记录</div>
                        <div id="kkpager" >
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
        
        
        
        
        <div class="easyuiWindow" style="display: none">
            <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
                	<div class="cursorDef">
                        <div id="gridTable2">
    
                        </div>
                    </div>
                    <div id="pageContianer2">
                    	<div class="dataMessage">没有符合条件的查询记录</div>
                        <div id="kkpager2">
                        </div>
                    </div>
                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:20px 0 0;">
                        <a class="easyui-linkbutton commonBtn cancelBtn">取消</a>
                        <a class="easyui-linkbutton commonBtn saveBtn" href="javascript:;" >确定</a>

                    </div>
                </div>
        
            </div>
        </div>
        
        
       

       

</asp:Content>
