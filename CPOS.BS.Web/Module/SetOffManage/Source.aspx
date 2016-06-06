<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>集客行动</title>
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
                       <div class="totalTitle num">
                           <img src="images/2.1_08.png" class="incos" />
                           <span>总计</span>
                        </div>
                        <div class="num bordergrey">
                            <div class="numTitle"><span>分享计划</span></div>
                            <div class="numNow" ><span ></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>新增会员总数</span></div>
                            <div class="numNow">
                                 <span></span>
                                 <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase1">
                       <div class="title num">
                           <img src="images/2.1_03.png" class="incos" />
                           <span>会员</span>
                       </div>
                        <div class="num bordergrey">
                            <div class="numTitle"><span>会员分享</span></div>
                            <div class="numNow2"><span></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>新增会员</span></div>
                            <div class="numNow2">
                                <span></span>
                                 <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase1" >
                        <div class="title num">
                           <img src="images/2.1_05.png" class="incos" />
                           <span>客服</span>
                       </div>
                        <div class="num bordergrey">
                            <div class="numTitle"><span>客服分享</span></div>
                            <div class="numNow2"><span></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>新增会员</span></div>
                            <div class="numNow2">
                                <span></span>
                                 <img src="" class="incoB" />
                                <span class="fontcolor"></span>
                            </div>
                        </div>
                    </div>
                    <div class="sourcebase1">
                        <div class="title num">
                           <img src="images/2.1_11.png" class="incos" />
                           <span>店员</span>
                       </div>
                        <div class="num bordergrey">
                            <div class="numTitle"><span>店员分享</span></div>
                            <div class="numNow2"><span></span></div>
                            <div class="border"></div>
                            <div class="numTitle"><span>新增会员</span></div>
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
                        <span>最近5天新增会员趋势</span>
                    </div>
                    <div id="staffCharts" class="chart"></div>
                </div>
            </div>
            <!--查询表格数据-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
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
                                 <label class="searchInput selectBox">
                                     <input id="StatusList" class="easyui-combobox" data-options="width:200,height:32" data-text="门店筛选" data-flag="Status" name="Status" type="text">
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
                                      <input id="reserveDateBegin"  name="ReserveDateBegin" class="easyui-datebox"  data-options="width:200,height:32" />
                                 </div>
                            </div>
                         <div class="commonSelectWrap">
                                   <em class="tit">结束时间：</em>
                                   <div class="selectBox">
                                          <input id="reserveDateEnd" name="ReserveDateEnd" class="easyui-datebox"data-options="width:200,height:32" validType="compareDate[$('#reserveDateBegin').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/>
                                   </div>
                         </div>
                         <div class="moreQueryWrap" style="margin-left: 160px;">
                              <a href="javascript:;" class="commonBtn" id="sendmessage">发送通知</a>
                         </div>
                       </div>
                    </form>
                </div>
            </div>
            <!--发送通知-->
             <!--表格无数据显示内容-->
            <div class="tableWrap" id="tableWrap">
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

    <!--发送通知弹出window界面-->
        <div style="display:none">
            <div id="winmessage" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true">
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
      			    <div data-options="region:'center'" class="messagediv">
                          <div class="textdiv" >
                            <div class="radio on" data-name="r1" data-UsableRange="1"><em></em><span>连锁掌柜APP-总部消息</span></div>
                            <div style="width:478px; margin:0px auto 10px">
                                <textarea id="textApp" class="textareastyle">集客行动已发布,快来参加获取半富福利！</textarea>
                            </div>
                            <div  style="margin-bottom:5px;" class="radio on" data-name="r1" data-UsableRange="1"><em></em><span>微信商城-会员中心</span></div>
                            <div style="width:478px; margin:auto">
                                <textarea id="textwebCat" class="textareastyle">集客行动已发布,快来参加获取半富福利！</textarea>
                            </div>
                          </div>
      			    </div>
      			    <div class="btnWrap messageBtn" id="btnWrap" data-options="region:'south'">
                        <a class="easyui-linkbutton commonBtn saveBtn"  >确定</a>
      				    <a class="easyui-linkbutton commonBtn cancelBtn cancel" href="javascript:void(0)" onclick="javascript:$('#winmessage').window('close')" >取消</a>
      			    </div>
      		    </div>
            </div>
        </div>
</asp:Content>
