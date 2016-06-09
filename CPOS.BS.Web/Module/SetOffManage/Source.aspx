<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>集客来源</title>
    <link href="<%=StaticUrl+"/module/SetOffManage/css/source.css?v=0.2"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/Source.js" >
        <div class="contentArea_vipquery">
            <!-- 内容区域 -->
            <div class="sourceTotal">
                <!--集客来源-->
                <div class="soruceTitle">
                   <span>集客来源</span>
                   <a href="javascript:;" class="checkDay" id="checkDThi">近30天</a>
                   <a href="javascript:;" class="checkDay" id="checkDSeven">近7天</a>
                </div>
                <div class="dourcedata">
                    <div class="sourcebase" >
                       <div class="totalTitle num borderR2">
                           <img src="images/2.1_08.png" class="incos" />
                           <span>总计</span>
                        </div>
                        <div class="num bordergrey borderR2">
                            <div class="numTitle"><span>分享计划</span></div>
                            <div class="numNow" ><span ></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>集客总数</span></div>
                            <div class="numNow">
                                 <span></span>
                                 <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase1">
                       <div class="title num borderR2">
                           <img src="images/2.1_03.png" class="incos" />
                           <span>会员</span>
                       </div>
                        <div class="num bordergrey borderR2">
                            <div class="numTitle"><span>会员分享</span></div>
                            <div class="numNow2"><span></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>集客</span></div>
                            <div class="numNow2">
                                <span></span>
                                 <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase1" >
                        <div class="title num borderR2">
                           <img src="images/2.1_05.png" class="incos" />
                           <span>客服</span>
                        </div>
                        <div class="num bordergrey borderR2">
                            <div class="numTitle"><span>客服分享</span></div>
                            <div class="numNow2"><span></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>集客</span></div>
                            <div class="numNow2">
                                <span></span>
                                <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase1">
                        <div class="title num borderR2">
                           <img src="images/2.1_11.png" class="incos" />
                           <span>店员</span>
                        </div>
                        <div class="num bordergrey borderR2">
                            <div class="numTitle"><span>店员分享</span></div>
                            <div class="numNow2"><span></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>集客</span></div>
                            <div class="numNow2">
                                <span></span>
                                <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                </div>
                <!--集客趋势-->
                <div class="soruceChart" style="margin-bottom:20px" >
                    <div class="chartTitle">
                         <img src="images/2.1_30.png" class="incos" />
                        <span>最近5天集客趋势</span>
                    </div>
                    <div id="staffCharts" class="chart"></div>
                </div>
            </div>
            <!--查询表格数据-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;border-top:1px solid #f0f0f2">
                <div class="moreQueryWrap">
                   <form></form>
                   <form id="seach">
                       <div style="width:100%">
                           <div class="commonSelectWrap">
                                 <em class="tit">门店查询：</em>
                                 <div class="selectBox">
                                        <select id="storeList" name="source" class="easyui-combobox" data-options="width:200,height:30" ></select>
                                 </div>
                            </div>
                           <div class="commonSelectWrap"">
                                 <em class="tit">来源筛选：</em>
                                 <label class="selectBox">
                                     <input id="StatusList" name="Status" class="easyui-combobox" data-options="width:200,height:32"  >
                                 </label>
                           </div>
                           <div class="moreQueryWrap">
                                <a href="javascript:;" class="commonBtn queryBtn select" >查询</a>
                           </div>
                        </div>
                       <div style="width:100%">
                           <div class="commonSelectWrap" >
                                <em class="tit">开始时间：</em>
                                <div class="selectBox" >
                                      <input id="reserveDateBegin"  name="ReserveDateBegin" class="easyui-datebox"   data-options="width:200,height:32" />
                                 </div>
                            </div>
                         <div class="commonSelectWrap">
                                   <em class="tit">结束时间：</em>
                                   <div class="selectBox">
                                          <input id="reserveDateEnd" name="ReserveDateEnd" class="easyui-datebox" data-options="width:200,height:32" />
                                   </div>
                         </div>
                         <div class="moreQueryWrap messageLocation">
                              <a href="javascript:;" class="commonBtn" id="sendmessage" style=" width:90px">发送通知</a>
                         </div>
                       </div>
                    </form>
                </div>
            </div>
            <!--发送通知-->
             <!--表格无数据显示内容-->
            <div class="tableWrap" id="tableWrap">
            	<div class="cursorDef">
                    <div class="dataTable" id="gridTable">
                         <div  class="loading">
                               <span><img src="../static/images/loading.gif"></span>
                          </div>
                    </div>
                    <div id="pageContianer">
                        <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager" ></div>
                    </div>
                </div>    
           </div>
         </div>
    </div>

    <!--发送通知弹出window界面-->
        <div style="display:none">
            <div id="winmessage" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true">
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
      			    <div data-options="region:'center'" class="messagediv">
                          <div class="textdiv" >
                            <div class="checkBox l on" data-flag="ConditionValue" ><em></em></div>
                            <div class="linetext msgTitle">通知员工-发送到连锁掌柜APP-总部消息</div>
                            <div style="width:478px; margin:0px auto 10px">
                                <textarea id="textApp" class="textareastyle">集客行动已发布,快来参加获取丰厚福利！</textarea>
                            </div>
                            <div class="checkBox l on" data-flag="ConditionValue" ><em></em></div>
                            <div class="linetext msgTitle">通知会员-发送到微信会员中心-通知</div>
                            <div style="width:478px; margin:auto">
                                <textarea id="textwebCat" class="textareastyle">集客行动已发布,快来参加获取丰厚福利！</textarea>
                            </div>
                          </div>
      			    </div>
      			    <div class="btnWrap messageBtn" id="btnWrap" data-options="region:'south'">
                        <a class="easyui-linkbutton commonBtn saveBtn">确定</a>
      				    <a class="easyui-linkbutton commonBtn cancelBtn cancel" style="height:35px;line-height:35px;" href="javascript:void(0)" onclick="javascript:$('#winmessage').window('close')" >取消</a>
      			    </div>
      		    </div>
            </div>
        </div>
</asp:Content>
