<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>商户基础信息配置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.6">
        <form></form>
        <form id="setForm">
          <div class="title">商户信息</div>
        <div class="lineTitle borderTS">
        <em class="tit">基础信息</em>
</div> <!--lineTitle-->
<div class="panelText">
    <div class="lineText">
    <em class="tit">商户名称：</em>
    <div class="inputBox" id="customerName">

    </div> <!--inputBox-->
</div><!--lineText-->
    <div class="lineText">
    <em class="tit">商户简称：</em>
    <div class="inputBox">
    <input type="text" class="easyui-validatebox" name="CustomerShortName" data-options="required:true,validType:'maxLength[10]'" placeholder="请输入"/>
    </div> <!--inputBox-->
</div><!--lineText-->


    <div class="lineText">
    <em class="tit">LOGO：</em>
    <div class="inputBox">
      <div class="logo" style="background-color: #1074bd;" data-name="WebLogo"><img src="images/imgDefault.png"></div>
      <div class="uploadTip">
        <div class="uploadBtn btn">
            <em class="upTip">上传</em>
             <div class="jsUploadBtn" ></div>
         </div><!--uploadBtn-->
         <div class="tip">建议尺寸187px*67px，50KB以内</div>
         </div> <!--uploadTip-->
    </div> <!--inputBox-->
</div><!--lineText-->
    <div class="lineText" style="display: none">
    <em class="tit">客服电话：</em>
    <div class="inputBox">
    <input type="text" class="easyui-validatebox" name="CustomerPhone" data-options="validType:'mobileTelephone'" placeholder="请输入"/>
    </div> <!--inputBox-->
</div><!--lineText-->

</div> <!--panelText-->

        <div class="lineTitle">
        <em class="tit">微商城首页分享设置</em>
</div> <!--lineTitle-->
<div class="panelText">
    <div class="lineText">
    <em class="tit">分享标题：</em>
    <div class="inputBox">
    <input type="text" placeholder="请输入" class="easyui-validatebox" data-options="validType:'maxLength[30]'"  name="ForwardingMessageTitle"/>
    </div> <!--inputBox-->
</div><!--lineText-->
    <div class="lineText">
    <em class="tit" >分享图标：</em>
    <div class="inputBox">
      <div class="logo" data-name="ForwardingMessageLogo" style="width: 90px; height: 90px;"><img src="images/imgDefault.png"></div>
      <div class="uploadTip" style="left: 110px;">
        <div class="uploadBtn btn">
            <em class="upTip">上传</em>
             <div class="jsUploadBtn" ></div>
         </div><!--uploadBtn-->
         <div class="tip">建议尺寸120px*120px，50KB以内</div>
         </div> <!--uploadTip-->
    </div> <!--inputBox-->
</div><!--lineText-->
    <div class="lineText">
    <em class="tit">分享摘要：</em>
    <div class="inputBox">
    <textarea placeholder="请输入" name="ForwardingMessageSummary"  class="easyui-validatebox" data-options="validType:'maxLength[300]'"></textarea>
    </div> <!--inputBox-->
</div><!--lineText-->

</div> <!--panelText-->

        <div class="lineTitle">
        <em class="tit">引导关注</em>
</div> <!--lineTitle-->
<div class="panelText">
    <div class="lineText">
    <em class="tit">引导关注链接：</em>
    <div class="inputBox">
    <input type="text"  placeholder="请输入" name="GuideLinkUrl"  class="easyui-validatebox long"  data-options="validType:'url'"/>
    </div> <!--inputBox-->
</div><!--lineText-->
    <div class="lineText">
    <em class="tit">引导关注二维码：</em>
    <div class="inputBox">
      <div class="logo" style="width: 90px; height: 90px;" data-name="GuideQRCode"><img src="images/imgDefault.png"></div>
      <div class="uploadTip" style="left: 110px; width: 508px;">
        <div class="uploadBtn btn">
            <em class="upTip">上传</em>
             <div class="jsUploadBtn" ></div>
         </div><!--uploadBtn-->
         <div class="tip">请上传微信公众号二维码 建议尺寸500px*500px，50KB以内</div>
         </div> <!--uploadTip-->
    </div> <!--inputBox-->
</div><!--lineText-->


</div> <!--panelText-->
          <div class="lineTitle">
          <em class="tit">客服</em>
  </div> <!--lineTitle-->
  <div class="panelText">
      <div class="lineText">
      <em class="tit">欢迎语：</em>
      <div class="inputBox">
      <textarea type="text" placeholder="请输入" name="CustomerGreeting"  class="easyui-validatebox" data-options="validType:'maxLength[30]'"></textarea>
      </div> <!--inputBox-->
  </div><!--lineText-->


  </div> <!--panelText-->

                    <div class="subMitButton commonBtn">保存</div>
                    </form>
        </div>

       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
