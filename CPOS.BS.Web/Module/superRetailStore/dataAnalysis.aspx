<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>分销商数据分析</title>
    <link href="<%=StaticUrl+"/module/superRetailStore/css/dataAnalysis.css?v=0.2"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/dataAnalysis.js?ver=0.1" >
        <!-- 内容区域 -->

         <!--分销商30天数据-->
         <div class="Module day30Data">
            <div class="ModuleHead">
                <div class="title">
                    <img src="images/list.png">
                    <span>近30天数据</span>
                </div>
            </div>
            <div class="contents"  id="activeCharts">
                <p>活跃分销商<span class="acount"></span><i>人</i></p>
                <div class="chartsData">
                    <div class="charts">
                        <p>无数据</p>
                    </div>
                    <div class="chartsLegend">
                        <p><span></span>活跃分销商</p>
                        <p><span></span>非活跃分销商</p>
                    </div>
                </div>
            </div>
            <div class="contents"  id="completeCharts">
                <p>成交分销商<span class="acount"></span><i>人</i></p>
                <div class="chartsData">
                    <div class="charts">
                        <p>无数据</p>
                    </div>
                    <div class="chartsLegend">
                        <p><span></span>成交新增下线</p>
                        <p><span></span>成交未新增下线</p>
                    </div>
                </div>
            </div>
            <div class="contents"  id="expandCharts">
                <p>拓展下线的分销商<span class="acount"></span><i>人</i></p>
                <div class="chartsData">
                    <div class="charts">
                        <p>无数据</p>
                    </div>
                    <div class="chartsLegend">
                        <p><span></span>已成交下线</p>
                        <p><span></span>未成交下线</p>
                    </div>
                </div>
            </div>
         </div>
         <!--分销商成交排名列表-->
         <div class="Module queryList">
            <div class="tableWrap" id="tableWrap">
                <div class="tableList" id="opt">
                   <ul><li class="on" data-Field7="1"><em > 分销商成交排名 </em></li>
                        <li data-Field7="2"><em>分销商新增下线排名 </em></li>
                   </ul>
                </div>
                <div class="dataTable" id="gridTable">
                    <div class="loading" style="padding-top:10px;">
                         <span>
                       <img src="../static/images/loading.gif"></span>
                    </div>
                </div>
                <div id="pageContianer">
                    <div class="dataMessage">无数据</div>
                </div>
            </div>
         </div>

         <!-- 下线人数弹层 -->
         <div style="display: none">
             <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
                 <div class="easyui-layout" data-options="fit:true" >
                     <div data-options="region:'center'">
                         <div class="tableWrap" id="tableWrap">
                             <div class="dataTable" id="gridRank">
                                 <div class="loading" style="padding-top:10px;">
                                      <span>
                                    <img src="../static/images/loading.gif"></span>
                                 </div>
                             </div>
                             <div id="pagecontianer">
                                 <div class="dataMessage" style="position:relative;margin:0;">没有符合条件的查询记录</div>
                                 <div id="kkpager" ></div>
                             </div>
                         </div>
                     </div>
                     <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="text-align: center; padding: 20px 0 0;margin:0;border-top: 1px solid #e1e7ea;">
                          <a class="easyui-linkbutton commonBtn cancelBtn"href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                          <a class="easyui-linkbutton commonBtn saveBtn" onclick="javascript:$('#win').window('close')">确定</a>
                     </div>
                 </div>
             </div>
         </div>

    </div>
</asp:Content>
