<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>营销活动列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <link href="<%=StaticUrl+"/module/marketing/css/style.css?v=0.1"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/activityQueryList.js">
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
                <div class="tableWrap cursorDef" id="tableWrap">
                     
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
</asp:Content>
