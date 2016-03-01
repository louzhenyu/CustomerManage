<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>营销活动列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

  <link href="css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/activityQueryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                   <div class="moreQueryWrap">
                   <form></form>
                     <form id="seach">

                                                                       <div class="commonSelectWrap">
                                                                           <em class="tit">活动名称：</em>
                                                                           <label class="searchInput">
                                                                               <input data-forminfo=""  data-flag="ActivityName" name="ActivityName" type="text" value="">
                                                                           </label>
                                                                       </div>
                                                                       <div class="commonSelectWrap">
                                                                           <em class="tit">活动状态：</em>
                                                                           <label class="searchInput selectBox">
                                                                               <input id="StatusList" class="easyui-combobox" data-options="width:200,height:32" data-text="活动状态" data-flag="Status" name="Status" type="text">
                                                                           </label>
                                                                       </div>

                                                                       <div class="moreQueryWrap">
                                                                                                   <a href="javascript:;" class="commonBtn queryBtn select">查询</a>
                                                                                                 </div>

                                                                       </form>



                   </div>


                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="optionBtn">
                      <div class="commonBtn  icon w100  icon_add r" data-flag="add">新增活动</div>


                     </div>
                <div class="tableWrap" id="tableWrap">
                     
                    <div class="dataTable" id="gridTable">
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


       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/couponManage/js/main.js"%>"></script>
</asp:Content>
