<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商品管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/superRetailStore/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">商品名称：</em>
                                                      <label class="searchInput">
                                                          <input data-text="商品名称" data-flag="ItemName" name="ItemName" type="text"
                                                              value="">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">状态：</em>
                                                      <div class="selectBox">
                                                                <input id="item_status" name="Status" class="easyui-combobox" data-options="width:200,height:30"  />
                                                      </div>
                                                  </div>
                                                  <div class="moreQueryWrap">
                                                                             <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                                           </div>

                                                  </form>



                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->
                    <div class="tipPanel">
                      <em class="tipInfo r">i</em>
                      <div class="tipInfoText">
                       <p><b>分销售价：</b><span>分销商对外销售商品的价格，合理设置可以帮助分销商更好的销售分销商品哦！</span></p>
                       <p><b>商家利润：</b><span>商家可以在这个商品中实际所赚取的金额，帮助商家更为了解自己商品的利润空间可以更为合理的设置分销的奖励哦！</span></p>
                       <p><b>分销库存：</b><span>分销商品虚拟库存和现有系统中商品库存无任何关系，请合理进行设置以免对实际库存照成影响！</span></p>
                       <p><b>计算公式：</b><span> <br/> 1、分销售价=成本价/分销商品成本占比 <br/>
                                            （您在分润管理中设置的比例） <br/>
                                            2、商家利润=分销售价*商家利润占比 <br/>
                                            （您在分润管理中设置的比例）</span></p>

</div>  <!--tipInfoText-->
                  </div><!--tipPanel-->
                </div>
                <div class="optionBtn" id="opt">
                                                        <div class="commonBtn w100 batch optionCheck" style="position: relative" data-flag="batch"   >批量操作
                                                        <div class="panelTab" >
                                                        <p class="optionCheck check01" data-optiontype="audit"  >批量上架</p>
                                                        <p class="optionCheck check03" data-optiontype="soldOut" >批量下架</p>
                                                         <p class="optionCheck check02" data-optiontype="del" data-show="0,500">批量移除</p>
                                   </div>
                                                        </div>   <!--optionCheck-->
                                       <div class="commonBtn w100 " data-flag="begEdit"  >分销设置</div>
                                       <div class="commonBtn w70 submit" data-flag="save"  >保存</div>
                                       <div class="commonBtn w70 submit" data-flag="cancel"  >取消</div>
                                      <div class="commonBtn w100 r" data-flag="add"  >选择商品</div>

                                      </div>
                <div class="tableWrap cursorDef" id="tableWrap">
                  <form id="girdForm">
                    <div class="dataTable" id="gridTable">
                                               <div  class="loading">
                                                        <span>
                                                      <img src="../static/images/loading.gif"></span>
                                                 </div>
</div>
  </form>

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
       <!-- 取消订单-->
       <script id="tpl_OrderCancel" type="text/html">
            <form id="payOrder">
           <div class="commonSelectWrap">
                 <em class="tit">备注：</em>
                <div class="searchInput">
                   <input type="text" name="Remark" />
               </div>
           </div>

           <p class="winfont">你确认取消此笔订单吗？</p>
           </form>
       </script>

               <!--选择商品-->
       <script id="tpl_AddProductList" type="text/html">
           <div class="optionPanel">
                 <div class="contentDiv" style="width: 200px;">
                 <p class="explain">选择品类</p>
                 <div class="content">
                 <div id="unitParentTree"></div>
        </div><!--content-->
        </div> <!--contentDiv-->
                  <div class="contentDiv center">
                    <div class="explain"><em>选择商品</em><span id="unitName">
                    <a href="/module/commodity/release.aspx" target="_blank">添加商品</a>
                    <a href="javaScript:void(0)" class="searchBtn">刷新</a></span>
                    <div class="commonBtn searchBtn"> 搜索</div>
                                           <div class="commonSelectWrap r">
                            				   <div class=" searchInput">
                            					 <input  name="ItemName" id="ItemName" type="text" value="" placeholder="请输入商品名称">
                            				   </div>
                                            </div>


                     </div>
                              <div class="content"  >
                               <div id="productGrid">
                               <div class="loading Refresh" > <span><img src="../static/images/loading.gif"></span> </div>
</div>
                                 <div id="pageContianer1">
                                                                        <div class="dataMessage1" >没有符合条件的查询记录</div>
                                                                            <div id="kkpager2" >
                                                                            </div>
                                                                        </div>
                     </div><!--content-->

        </div> <!--contentDiv-->
                   <div class="contentDiv" style="width: 330px;">
                              <div class="explain"><em>已选择商品</em> <a class="optBtn">批量删除</a></div>
                              <div class="content">
                              <div id="unitTreeSelect"></div>
                     </div><!--content-->
        </div> <!--contentDiv-->
        </div>
    </script>


</asp:Content>
