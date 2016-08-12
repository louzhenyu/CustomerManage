<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>礼品卡管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/openGiftCard/css/style.css?v=0.5"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.4">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="" id="simpleQuery" style="display: inline-block; width: 100%;">
                   <div class="optionBtn">
                       <div class="commonBtn icon icon_add w110 r" data-flag="add">新增礼品卡</div>
                   </div>


                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap cursorDef" id="tableWrap" >
                   <div  id="gridTable" class="gridLoading">
                         <div  class="loading">
                                  <span>
                                <img src="../static/images/loading.gif"></span>
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
      
      
      <!-- 快速上手 -->
      <div style="display:none;">
            <div id="winQuickly" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
                    <div data-options="region:'center'" style="padding:10px;">
                        <div class="quicklyBox">
                            <div><img src="images/card-quickly.png" alt="" /></div>
                        </div>
                        <div class="quicklyBtnBox">
                            <a href="http://help.chainclouds.cn/?p=874" style="text-indent:5px;" target="_blank"><span>操作指引</span></a>
                            <a href="http://help.chainclouds.cn/?p=1396" style="text-indent:13px;" target="_blank"><span>操作指引</span></a>
                            <a href="http://help.chainclouds.cn/?p=730" style="text-indent:25px;" target="_blank"><span>操作指引</span></a>
                        </div>
                        <p class="nextNotShow"><span>下次不再显示</span></p>
                    </div>
                </div>
            </div>
        </div>


        <!--收款-->
         <script id="tpl_AddNumber" type="text/html">
            <form id="optionForm">


            <div class="optionclass">
               <div class="commonSelectWrap">
                             <em class="tit">数量:</em>
                                <div class="borderNone" >
                                 <input id="Amount" class="easyui-numberbox" data-options="width:180,height:34,min:0,precision:0,max:10000" name="IssuedQty" />
                               </div>
                </div>
            </div>
                </form>
                </script>

</asp:Content>
