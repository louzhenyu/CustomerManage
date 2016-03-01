<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>退款列表管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/refundList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">

                                                  <div class="commonSelectWrap">
                                                      <em class="tit">退款单号：</em>
                                                      <label class="searchInput">
                                                          <input data-text="退款单号" data-flag="item_name" name="RefundNo" type="text" value="">
                                                      </label>
                                                  </div>
                                                   <div class="commonSelectWrap">
                                                       <em class="tit">商户单号：</em>
                                                       <div class="searchInput">
                                                          <input  name="paymentcenterId" data-text="会员" data-flag="vip_no" type="text" value=""  placeholder="请输入">
                                                       </div>
                                                   </div>
                                                    <div class="commonSelectWrap">
                                                       <em class="tit" >支付方式：</em>
                                                     <div class="selectBox">
                                                                <select id="txtDataFromID" name="payId"></select>
                                                      </div>
                                                    </div>
                                                  <div class="moreQueryWrap">
                                                                             <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                                           </div>
                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                  <div class="optionBtn ">
                    <div class="commonBtn icon icon_export w80 export r" data-flag="export">导出</div>
                  </div>
                <div class="tableWrap" id="tableWrap">
                  

                <div class="tableList" id="opt">
                   <ul><li data-status="0" class="on"><em> 全部 </em></li>
                        <li data-status="1"><em>待退款 </em></li>
                        <li data-status="2"><em>已完成 </em></li>

                   </ul>

                </div>
                   <div class=""> <table class="dataTable" id="gridTable">
                        <div class="dataTable" id="dataTable">
                          <div  class="loading">
                                   <span>
                                 <img src="../static/images/loading.gif" style="  padding-top: 50px;"></span>
                            </div>
                        </div>

                                  </table>  </div>
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

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
