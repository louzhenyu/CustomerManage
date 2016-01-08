<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/queryList.js?ver=0.1">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" >
                <div class="item">

                    <form>
                    </form>
                    <form id="seach">
                    <div class="commonSelectWrap">
                     <em class="tit">会员编号</em>
                    <div class="searchInput"><input type="text" name="VipCardCode"  placeholder="请输入卡号" value=""/> </div>
</div><!--commonSelectWrap-->
                     <div class="commonSelectWrap">
                      <em class="tit">会员姓名</em>
                     <div class="searchInput"><input type="text"  name="VipName" placeholder="请输入姓名/昵称" value=""/> </div>
 </div><!--commonSelectWrap-->
                      <div class="commonSelectWrap">
                       <em class="tit">手机号</em>
                      <div class="searchInput"><input type="text"  name="Phone" placeholder="请输入手机号" value=""/> </div>
  </div><!--commonSelectWrap-->
                    </form>


                </div>
                <div class="itemBtn">
                    <div class="moreQueryWrap">
                        <a href="javascript:;" class="commonBtn queryBtn select">查询</a>
                    </div>
                </div>
            </div>
            <div class="tableWrap"  id="tableWrap">
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
