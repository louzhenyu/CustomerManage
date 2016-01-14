<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>问卷详细</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/QuestionnaireNews/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/QuestionnaireNews/css/QuestionnaireInforDetail.css?v=0.4"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="allPage" id="section" data-js="js/QuestionnaireInforDetail.js?ver=0.3">
            <!-- 内容区域 -->
      <div style="  display: inline-block;width: 100%;">
            <div class="contentArea_quetion">

                <div class="queryTermArea" id="simpleQuery" >
                        
                    <div class="navtit selected">明细数据</div>
                    <div class="navtit">报表统计</div>
                </div>
                <div class="questiontitle"><span></span><div class="QuestionnaireName">问卷</div></div>
                <div class="tableWrap showcontent" id="tableWrap" style="width:100%;">

                   <table class="dataTable" id="gridTable"></table>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager">
                        </div>
                    </div>
                </div>

                <div class="borderArea showcontent" style="display:inline-block;display:none;">
                <div class="inlineBlockArea">
                </div>
               
                
            </div>

            </div>
        </div>
      </div>
    
     <%-- 选项列表 --%>
    <script id="tpl_Optionlist" type="text/html">

         <# for(var i=0;i<Questionlist.length;i++){ var Question=Questionlist[i];  #>

         <div class="commonSelectWrap">
                      <div class="leftContent">
                          <div class="title"><#=(i+1) #>.<#=Question.Name #></div>
                          <div class="optinlist">
                               <# for(var j=0;j<Question.Optionlist.length;j++){ var Option=Question.Optionlist[j];  #>
                                <div class="option"><#=Option.number #>、<div ><span style="width:<#=Option.SelectedPercent #>%">&nbsp;</span></div><#=Option.SelectedPercent #>%</div>
                               <#}#>    
                          </div>
                      </div>
                      <div class="rightContent">
                          <div class="title">选项<span>数量</span></div>
                          <div class="optinlist">
                               <# for(var j=0;j<Question.Optionlist.length;j++){ var Option=Question.Optionlist[j];  #>
                                <div class="option"><#=Option.number #>、<#=Option.OptionName #><span><#=Option.SelectedCount #></span></div>
                               <#}#>  
                          </div>

                      </div>
                  </div>
              <#}#>      
        </script>

    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/Module/QuestionnaireNews/js/main.js"%>"></script>
</asp:Content>
