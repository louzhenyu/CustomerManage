<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员活动列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                                                      <em class="tit  w100">活动名称：</em>
                                                      <label class="searchInput">
                                                          <input data-text="活动名称" data-flag="ActivityName" name="ActivityName" type="text"
                                                              value="">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">活动状态：</em>
                                                      <div class="searchInput bordernone">
                                                         <input data-text="Status" id="Status" data-flag="Status" class="easyui-combobox" name="ActivityType" type="text" value="">
                                                      </div>
                                                  </div>
                                                  <div class="moreQueryWrap">
                                                                             <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                                           </div>

                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                 <div class="optionBtn" id="opt">
                 <div class="commonBtn icon w120  icon_add  sales r" data-flag="add" id="sales"> 新增会员活动</div>


                </div>
                <div class="tableWrap" id="tableWrap">
               
                <div class="cursorDef">
                   <div  id="gridTable" class="gridLoading">
                         <div  class="loading">
                                  <span>
                                <img src="../static/images/loading.gif"></span>
                           </div>
                   </div>
                   </div>
                    <div id="pageContianer">
                    <div class="dataMessage" style="margin-top: -54px;" >没有符合条件的查询记录</div>
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
        <!--下载说明框-->
    <div style="display:none">
        <div id="DownPaper" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true">
               <div style="width:370px;margin:50px auto 0px;">
                    <div style="margin-bottom:10px">
                        <span style="color:#999999">优惠券名称：</span>
                        <span ></span>
                    </div>
                    <div style="padding-left:10px;margin-bottom:10px;position:relative;">
                          <span>下载数量:</span>
                          <input placeholder="请输入不超过xxx的数量"  type="text" style="margin-left:15px; width:200px" id="num" />
                          <span style="color:red;visibility:hidden;  position: absolute;width: 120px;line-height: 27px;padding-left: 5px;" id="checknum">请输入1-1000数字</span>
                    </div>
                    <div style="margin:50px 0px 10px 5px;">下载说明</div>
                    <div style="margin-bottom:10px; color:#999999"><span>1.每次下载不超过1000张；</span></div>
                    <div style="margin-bottom:50px; color:#999999"><span>2.下载的券可通过异业合作等渠道发放给客户</span></div>
                </div>
            <div style=" border-bottom:1px solid #e5e5e5;margin-bottom:15px"></div>
                <div class="btnWrap"  data-options="region:'south',border:false" style="text-align:center;padding:5px 0 0;">
      			    <a class="easyui-linkbutton commonBtn saveBtn" style="margin-bottom:0px;margin-right:15px;color:#00a0e8;border:1px solid #00a0e8  ;background-color:#ffffff" id="DownCancle" >取消</a>
      		        <a class="easyui-linkbutton commonBtn closeBtn" style="margin-bottom:0px" id="DownSure">确定</a>
      		    </div>
            
        </div>
        </div>
        <!--收款-->
         <script id="tpl_AddNumber" type="text/html">
            <form id="optionForm">


            <div class="optionclass">
               
                             <div class="title">数量:</div>
                                <div class="borderNone" >
                                 <input id="Amount" class="easyui-numberbox" data-options="width:180,height:34,min:1,precision:0,max:10000" name="IssuedQty" />
                               </div>
               
            </div>
                </form>
                </script>

</asp:Content>
