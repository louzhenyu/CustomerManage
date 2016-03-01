<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>首页管理列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/queryList.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.1">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->

                <div class="tableWrap" id="tableWrap">
                <div class="optionBtn" id="opt">
                 <div class="commonBtn sales icon icon_add w100 r" data-flag="add" id="sales"> 新增主页</div>


                </div>
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

      <div id="winT" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panelImgList">

      			<div data-options="region:'center'" style="padding:10px; text-align: center;">
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:90px; border-top:1px solid #ccc;text-align:center;padding:5px 0 0;">

                      <div class="imgOpt" data-multiple="1">
                           <div class="magnify" > </div> <div class="shrink"></div> <span>1:1 </span> </div>

      				<a class="easyui-linkbutton commonBtn saveBtn" >使用</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#winT').window('close')" >返回</a>
      			</div>
      		</div>

      	</div>


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


        <!--模板选择-->
         <script id="tpl_addTemplate" type="text/html">


             <div class="imgList">
             <div class="imgPanel">
             <img src="images/template01.png" width="180"  >
             <div class="show bg"></div>
              <img src="images/duihao.png" width="111" height="111" class="show imgT"/>
               <div class="radioPanel">
                          <div class="radio" data-name="r1" data-index="1"><em></em><span> 零售推荐版 <img data-index="1" src="images/yuelan.png"/></span> </div>

               </div>
</div>
             <div class="imgPanel center">
             <img src="images/template02.png" width="180">
                          <div class="show bg"></div>
                           <img src="images/duihao.png" width="111" height="111" class="show imgT"/>
               <div class="radioPanel">
                          <div class="radio" data-name="r1" data-index="2"><em></em><span> 服务推荐版 <img data-index="2"  src="images/yuelan.png"/></span> </div>
               </div>
</div>
             <div class="imgPanel">
             <img src="images/template03.png" width="180">
                          <div class="show bg"></div>
                           <img src="images/duihao.png" width="111" height="111" class="show imgT"/>
             <div class="radioPanel">
             <div class="radio" data-name="r1" data-index="3"><em></em><span> 促销推荐版 <img data-index="3"  src="images/yuelan.png"/></span> </div>
             </div>
</div>


</div>

        </script>

          <script id="tpl_Template" type="text/html">
                     <div class="imgBody" data-json="<#=JSON.stringify(item)#>">
                     <img src="<#=item.src#>" />
</div>

                 </script>
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/couponManage/js/main.js"%>"></script>
</asp:Content>
