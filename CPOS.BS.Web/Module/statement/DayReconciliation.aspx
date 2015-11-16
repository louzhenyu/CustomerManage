<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>门店日结对账统计</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/DayReconciliation.js?ver=0.1">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" >
                <div class="item">
                   <div class="commonSelectWrap" style="display:none">
                                            <em class="tit">办卡门店：</em>
                                            <div class="selectBox">
                                                <input id="unitTree" name="UnitID" class="easyui-combotree" data-options="width:160,height:32" />
                                            </div>
                                        </div>
                    <form>
                    </form>
                    <form id="seach">
                    <div class="commonSelectWrap" >

                        <em class="tit">日期:</em>
                        <div class="selectBox">
                            <input class="datebox" name="StareDate" id="StareDate" data-options="required:true"/>
                            至
                            <input class="datebox" name="EndDate" data-options="required:true"  validtype="compareDate[$('#StareDate').datebox('getText'),'开始时间应小于结束时间']" />
                        </div>
                    </div>
                      <div class="commonSelectWrap" style="clear: both; margin: 10px 0 0 0;">
                                        <em class="tit">门店:</em>
                                        <div class="selectBox" >
                                        <input id="unitHtml" type="text" disabled="disabled">
                                       </div>
                          </div>
                    </form>


                </div>
                <div class="itemBtn">
                    <div class="moreQueryWrap">
                        <a href="javascript:;" class="commonBtn queryBtn select">查询</a>
                    </div>
                </div>
            </div>
            <div class="tableWrap cursorDef"  id="tableWrap">
                <div class="dataTable gridLoading" id="gridTable">
                   <!-- <div class="loading">
                        <span>
                            <img src="../static/images/loading.gif"></span>
                    </div>-->
                </div>
                <div id="pageContianer">
                    <div class="dataMessage">
                      没有符合条件的记录  </div>
                    <div id="kkpager">
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
