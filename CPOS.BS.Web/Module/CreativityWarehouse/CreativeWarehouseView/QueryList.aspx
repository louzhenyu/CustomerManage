<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>计划活动管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/CreativityWarehouse/CreativeWarehouseView/css/queryList.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
           
           
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <div class="contentleft">
                    <div class="thisseason spacing">
                        <div class="content">
                           
                            <div class="touchsliderthisseason">
                                <div class="thisseasontouchslider-viewport" style="width:770px;overflow:hidden;position: relative; height: 340px;">
                                    <div class="InSeasonList" style="width: 100000px; position: absolute; left: 0px; height:340px;">
                                        
                                    </div>
                                </div>
                                
                                <div class="thisseasontouchslider-nav">
                                    
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                <div class="contentright ">
                    <div class="seasonlist spacing">
                         <div class="title">即将上线<span class="Annualplanbtn">查看全年计划></span></div>
                        <div class="content">
                            <ul class="seasonlist_ul">
                            </ul>

                        </div>
                    </div>
                    
                </div>

               
                  <div class="ActivityGroups">
                    <div id="Product"  data-code="Product" class="ActivityGroupName hotbordercolor graycolor">
                        <div class="ActivityGroupImg hotcolor "><img src="../MyActivity/images/ActivityGroup01.png" /></div>
                        
                    </div>

                    <div id="Holiday"   data-code="Holiday"  class="ActivityGroupName holidaybordercolor graycolor">
                        <div class="ActivityGroupImg holidaycolor"><img src="../MyActivity/images/ActivityGroup02.png" /></div>
                        
                    </div>

                    <div id="Unit"  data-code="Unit" class="ActivityGroupName stockbordercolor graycolor">
                        <div class="ActivityGroupImg stockcolor"><img src="../MyActivity/images/ActivityGroup03.png" /></div>
                       
                    </div>
                </div>

                    <div class="nextseason spacing">
                          <div class="content  NextSeasonList TemplatePreview" >
                         
                        </div>
                        
                    </div>
            </div>
   
        </div>

        <div id="Annualplanlayer" >
         <div class="planlayer">
             <div class="plantitle">全年活动计划</div>
             <img class="Annualplan" src="" />
             <img class="close" src="../../styles/images/newYear/hitClose1.png" />
             <div class="plandown">点击按钮，开始制作您自己的市场计划<a href="images/全年计划.xlsx" download="全年计划.xlsx"  class="commonBtn downmodel">下载市场计划模板</a></div>
         </div>
        </div>

         <%-- 当季 --%>
    <script id="tpl_InSeasonList" type="text/html">
        <# for(i=0;i<BannerList.length;i++){ _data=BannerList[i]; #>
            <# if(_data.ActivityGroupCode=="" ){#>
        <div class="thisseasontouchslider-item Seasondata"> <a href="<#=_data.BannerUrl #>"><img  src="<#=_data.ImageURL #>"  /></a></div>
             <#}else{ #>
        <div class="thisseasontouchslider-item Seasondata"> <img  src="<#=_data.ImageURL #>"  /></div>
            <#}} #>
    </script>



       <%-- 主题 --%>
    <script id="tpl_NextSeasonList" type="text/html">
         <# for(i=0;i<TemplateList.length;i++){ _data=TemplateList[i]; #>

             <div class="Activity">
                    <div class="ActivityContent">
                        <div class="Activityimg"><img src="<#=_data.ImageURL #>" /></div>
                        <div class="ActivityQRcode"><img src="<#=_data.RCodeUrl #>" /></div>
                        <div class="ActivityDesc"><span><#=_data.TemplateName #></span><span></span></div>
                    </div>
                    <div class="ActivityOpeartion"><div class="releasebtn"  data-id="<#=_data.TemplateId #>" >
                        发起活动
                     </div></div>
                </div>
             <#} #>

    </script>
     <%-- 年度活动计划 --%>
    <script id="tpl_seasonlist" type="text/html">
         <# for(i=0;i<PlanList.length;i++){ if(i<7){ _data=PlanList[i]; #>
              <li><span> <#=_data.PlanDate.substring(5) #></span> <#=_data.PlanName #></li>
            <#}} #>

           
    </script>


   

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>


