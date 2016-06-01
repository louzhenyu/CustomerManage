<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>活动列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/bargainManage/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />

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
                                                      <em class="tit">活动名称：</em>
                                                      <label class="searchInput">
                                                          <input data-text="活动名称" data-flag="EventName" name="EventName" type="text" placeholder="请输入活动名称" value="">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">状态：</em>
                                                      <div class="searchInput bordernone">
                                                         <input id="item_status" data-text="状态" data-flag="Status" class="easyui-combobox" data-options="min:0,precision:2, width:198,height:32" name="EventStatus" type="text" placeholder="请输入状态" value="" >
                                                      </div>
                                                  </div>
                                                  <div class="moreQueryWrap">
                                                     <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                   </div>
                                                   <div class="clear"></div>
                                                   

                                                   <div class="commonSelectWrap">
                                                      <em class="tit">开始时间：</em>
                                                      <div class="searchInput bordernone" style="border:1px solid #ccc;border-radius:3px;background-color:#fff;">
                                                         <input id="BeginTime"  name="BeginTime"  data-options="width:198,height:32"/>
														 <span class='date'><b></b></span>
                                                      </div>
                                                  </div>

                                                  <div class="commonSelectWrap">
                                                      <em class="tit">结束时间：</em>
                                                      <div class="searchInput bordernone" style="border:1px solid #ccc;border-radius:3px;background-color:#fff;">
                                                         <input id="EndTime" name="EndTime" data-options="width:198,height:32"  />
														 <span class='date'><b></b></span>
                                                      </div>
                                                  </div>
                           
                              
                              </div>


                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap">
                  <div class="optionBtn" id="opt">
                    <div class="commonBtn r sales" data-flag="add" id="sales"> <img src="images/add.png"  >新增砍价</div>


                  </div>
                <div class="cursorDef">
                   <div  id="gridTable" class="gridLoading">
                         <div  class="loading">
                                  <span>
                                <img src="../static/images/loading.gif"></span>
                           </div>
                   </div>
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
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>
      <!--商品基本信息编辑-->
      <div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
      <div id="goodsBasic_exit"  class="jui-dialog jui-dialog-addGoogs" style="display:none;">
        
          <div class="jui-dialog-tit">
            <h2>添加砍价</h2>
              <a href="javascript:;" class="jui-dialog-close hintClose"></a>
          </div>
          <div class="optionclass">
             
            <div class="title">活动名称:</div>
            <div class="borderNone" >
             <input id="campaignName" data-options="width:260,height:34,min:1,precision:0,max:10000" name="IssuedQty" style="width:260px" placeholder="请选择活动名称" />
            </div>
          </div>
          <div class="optionclass">
            <div class="title">开始时间:</div>
            <div class="searchInput bordernone">
              <input id="campaignBegin"  name="order_date_begin" style="width:260px" placeholder="请选择活动开始时间" />
            </div>
            
          </div>
          <div class="optionclass">
            <div class="title">结束时间:</div>
            <div class="searchInput bordernone">
              <input id="campaignEnd" name="order_date_end" style="width:260px" placeholder="请选择活动结束时间" />
            </div>
            
          </div>
          <div class="btnWrap">
              <a href="javascript:;" id="saveCampaign" class="commonBtn saveBtn">提交</a>
              <a href="javascript:;" class="commonBtn cancelBtn hintClose">取消</a>
          </div>
        
        
      </div>

</asp:Content>