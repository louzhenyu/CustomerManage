<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>报表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css?v=0.1" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
           <div class="panelMenu" id="menList">
              <div class="menuBody" style="display: none">
              <div class="menuTitle on test" data-flag="[on test]类名默认值"><img src="images/Cart.png" width="16" height="16">报表集合</div>
               <ul class="menuUl" >
              <li><p data-href="/WebReport/ReportServer?reportlet=会员卡销售报表.cpt&op=fs_load&cmd=sso" class="icon_a icon on" data-name="会员卡销售报表">会员卡销售报表</p></li>
             <!-- <li><p data-href="/WebReport/ReportServer?reportlet=会员卡追踪.cpt&op=fs_load&cmd=sso" class="icon_b icon" data-name="会员卡追踪">会员卡追踪</p></li>-->
              <li><p data-href="/WebReport/ReportServer?reportlet=会员总结.cpt&op=fs_load&cmd=sso" class="icon_c icon" data-name="会员总结">会员总结</p></li>
              <li><p data-href="/WebReport/ReportServer?reportlet=休眠客户查询.cpt&op=fs_load&cmd=sso" class="icon_d icon" data-name="休眠客户查询">休眠客户查询</p></li>
              <li><p data-href="/WebReport/ReportServer?reportlet=员工售卡查询.cpt&op=fs_load&cmd=sso" class="icon_e icon" data-name="员工售卡查询">员工售卡查询</p></li>
              <li><p data-href="/WebReport/ReportServer?reportlet=会员手动调整报表.cpt&op=fs_load&cmd=sso" class="icon_f icon" data-name="会员手动调整报表">会员手动调整报表</p></li>
              </ul>

              </div> <!--menuBody-->
            </div>
            <div id="tabPanel" class="easyui-tabs" data-options="tools:'#tab-tools'" >

            	</div>
            <iframe width="1000px" id="iframe" height="0px" ></iframe>
           <div class="zsy"> </div>
        </div>
        <div class="loading" >
          <span><img src="../static/images/loading.gif"></span>
         </div>
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
