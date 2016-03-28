<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会议签到</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/marketSignIn.js">
    	<div class="signinArea">
        	<div class="itemBox">
            	<a class="createSigninQR commonBtn w120" href="javascript:;">生成签到二维码</a>
            	<!--显示后台接口返回二维码路径-->
                <p class="signinQRImg">
                	<img src=""/>
                </p>
            </div>
            <div class="itemBox">
            	<a class="downloadVipQR commonBtn w120" target="_blank" href="javascript:;">下载会员二维码</a>
            </div>
        </div>
    	
    </div>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
