<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>创意仓库</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/CreativeWarehouse/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.4">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <div class="contentleft">
                    <div class="thisseason spacing">
                        <div class="title"><span class="titleline"></span>当季活动</div>
                        <div class="content">
                           
                            <div class="touchsliderthisseason">
                                <div class="thisseasontouchslider-viewport" style="width:800px;overflow:hidden;position: relative; height: 340px;">
                                    <div class="InSeasonList" style="width: 100000px; position: absolute; left: 0px; height:340px;">
                                        
                                    </div>
                                </div>
                                
                                <div class="thisseasontouchslider-nav">
                                    
                                </div>
                            </div>
                            <div class="qcode" style="display:none;"><img  src=""  /></div>
                        </div>
                    </div>
                    <div class="nextseason spacing">
                         <div class="title"><span class="titleline" ></span>下季活动</div>
                        <div class="content  NextSeasonList">
                           <%-- <div class="activitys">
                            <div class="activityimg"><div>什么情况</div><img  src="../CreativeWarehouse/images/2_09.png"  /></div>
                            <div class="activityimg"><img  src="../CreativeWarehouse/images/2_11.png"  /></div>
                             </div>
                                <div class="activitys">
                            <div class="activityimg"><img  src="../CreativeWarehouse/images/2_13.png"  /></div>
                            <div class="activityimg"><img  src="../CreativeWarehouse/images/2_19.png"  /></div>
                                     </div>
                                <div class="activitys">
                            <div class="activityimg"><img  src="../CreativeWarehouse/images/2_22.png"  /></div>
                            <div class="activityimg"><img  src="../CreativeWarehouse/images/2_23.png"  /></div>
                                     </div>--%>
                              
                            
                        </div>
                    </div>
                </div>
                <div class="contentright ">
                    <div class="seasonlist spacing">
                         <div class="title"><span class="titleline"></span>2016年度活动计划</div>
                        <div class="content">
                            <ul class="seasonlist_ul">
                            </ul>

                        </div>
                    </div>
                    <div class="seasonbannerlist spacing">
                        <div>

                            <div class="touchslider">
                                <div class="touchslider-viewport" style="width:285px;overflow:hidden;position: relative; height: 150px;">
                                    <div style="width: 100000px; position: absolute; left: 0px; height:150px;">
                                        <div class="touchslider-item"><img style="width:284px;height:150px;"  src="../CreativeWarehouse/images/2_06.jpg"  /></div>
                                   
                                    </div>
                                </div>

                                <div class="touchslider-nav">
                                   
                                    <span class="touchslider-nav-item touchslider-nav-item-current"></span>
                                   
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="QRcode spacing"><img src="../CreativeWarehouse/images/2_17.png" />扫码获取更多</div>
                </div>


            </div>
        </div>
    <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent1">

      			<div data-options="region:'center'" style="padding:10px;">
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
        
      <!-- 快速上手 -->
      <div style="display:none;">
    <div id="winQuickly" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
        <div class="easyui-layout" data-options="fit:true" id="panlconent">
            <div data-options="region:'center'" style="padding:10px;">
                <div class="quicklyBox">
                	<div><img src="images/creative-quickly.png" alt="" /></div>
                </div>
                <div class="quicklyBtnBox">
                    <a href="http://help.chainclouds.cn/?p=1444" style="text-indent:78px;" target="_blank"><span>操作指引</span></a>
                    <a href="http://help.chainclouds.cn/?p=814" style="text-indent:-35px;" target="_blank"><span>操作指引</span></a>
                </div>
                <p class="nextNotShow"><span>下次不再显示</span></p>
            </div>
        </div>
    </div>  
</div>  
        
        
        <%-- 当季 --%>
    <script id="tpl_InSeasonList" type="text/html">
        <# for(i=0;i<ThemeList.length;i++){ _data=ThemeList[i]; #>
        <div class="thisseasontouchslider-item Seasondata"> <img  src="<#=_data.ImageURL #>"  /><div class="apply" data-themeid="<#=_data.ThemeId #>">申请活动</div><div class="Scan" data-src="<#=_data.RCodeURL #>">扫码预览</div></div>
            <#} #>
    </script>



       <%-- 下季 --%>
    <script id="tpl_NextSeasonList" type="text/html">
         <# for(i=0;i<ThemeList.length;i++){ _data=ThemeList[i]; #>
             <# if(i==0){ #>
             <div class="activitys">
             <#}else  if(i%2==0){ #>
                </div><div class="activitys">
             <#}#>

                <div class="activityimg"><div style="display:none;"><#=_data.ThemeName #></div><img  src="<#=_data.ImageURL #>"  /></div>
        
             <#} #>
    </script>
     <%-- 年度活动计划 --%>
    <script id="tpl_seasonlist" type="text/html">
         <# for(i=0;i<ThemeList.length;i++){ _data=ThemeList[i]; #>
              <li><span> <#=_data.StartDate.substring(5,10) #></span> <#=_data.ThemeName #></li>
            <#} #>

           
    </script>


       <%-- 申请活动 --%>
    <script id="tpl_Apply" type="text/html">
           <div class="applycontent">
               <div class="Applyimg"><img src="../CreativeWarehouse/images/apply.png" /></div>
               <div class="Applystatus">申请已成功提交！</div>
               <div class="Applysdesc">我们营销顾问会第一时间联系您哦！</div>
           </div>
        </script>
       <%-- 申请活动失败 --%>
    <script id="tpl_Applyfail" type="text/html">
           <div class="applycontent">
               <div class="Applystatus"><#=errMsg #></div>
           </div>
        </script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>

