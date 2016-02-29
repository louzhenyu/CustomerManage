<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/chainCloudOrder/css/style.css?v=0.1"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
          <div style="display: none">
              <div id="winPwd" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
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
          </div>
      </div>
      <script id="tpl_OrderCancel" type="text/html">
          <form id="payOrder">
              <div class="commonSelectWrap">
                  <em class="tit">用户名：</em>
                  <div  class="searchInput">
                      <input type="text" value="用户名" readonly="readonly" />
                  </div>
              </div>
              <div class="commonSelectWrap">
                  <em class="tit">旧密码：</em>
                  <div  class="searchInput">
                      <input type="text" name="Remark" />
                  </div>
              </div>
              <div class="commonSelectWrap">
                  <em class="tit">新密码：</em>
                  <div  class="searchInput">
                      <input type="text" name="NewPassword" id="pwd" />
                  </div>
              </div>
              <div class="commonSelectWrap">
                  <em class="tit">新密码确认：</em>
                  <div  class="searchInput">
                      <input type="text" class="easyui-validatebox"
                             required="required" validType="equals['#pwd']" />
                  </div>
              </div>
          </form>
      </script>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
