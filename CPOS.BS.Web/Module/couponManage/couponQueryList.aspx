<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>优惠券查询</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/couponManage/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">

        .CouponEventBtn {background: #fff;
            line-height: 32px;
            height: 32px;
            float: left;
            width: 72px;
            height: 32px;border-radius: 8px;
            border:solid 1px #7fe5e5;
            color:#7fe5e5;
            text-align:center;
            cursor:pointer;
        }
        .winfont {
        
        width: 100%;
line-height: 24px;
color: #999;
text-align:center;
margin-top: 50px;
font-size: 18px;
        }
        .cursorDef .datagrid-body td { cursor: default;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/couponQueryList.js">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                                            <div class="commonSelectWrap">
                                                      <em class="tit">手机号：</em>
                                                      <div class="searchInput bordernone">
                                                         <input data-text="手机号" validType="mobile"  class="easyui-numberbox" data-options=" width:200,height:32" name="Mobile" type="text" value="">
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">优惠券号：</em>
                                                      <label class="searchInput" >
                                                          <input data-text="优惠券名称"  name="CouponCode" type="text"
                                                              value="">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">状态：</em>
                                                      <label class=" selectBox">
                                                          <select id="item_status" name="Status" style="display:none;"></select>
                                                      </label>
                                                  </div>
                                                  <div class="moreQueryWrap">
                                                                             <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                                           </div>

                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap" style="display:inline-block;width:100%;">
                    <div class="cursorDef">
                   <table class="dataTable" id="gridTable"></table>
                   </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager">
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
      				    <a  class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
                   	</div>
      		</div>

      	</div>
      </div>

</asp:Content>
