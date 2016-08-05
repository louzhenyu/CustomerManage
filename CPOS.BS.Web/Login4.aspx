<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login4.aspx.cs" Inherits="JIT.CPOS.BS.Web.Login4" %>

<!doctype html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script src="js/jquery.js"></script>
<title>阿拉丁会员管理</title>
  
    <!--[if lte IE 6]>  
    <style type="text/css">  
#ie6-warning{background:#FF0; position:absolute;top:0; left:0;font-size:12px; line-height:24px; color:#F00;
padding:0 10px;}  
#ie6-warning img{float:right; cursor:pointer; margin-top:4px;}
#ie6-warning a{text-decoration:none;}  
</style>
<div id="ie6-warning">
<img src="" width="14" height="14" onclick="closeme();" alt="关闭提示" />
您正在使用 Internet Explorer 6 低版本的IE浏览器。为更好的浏览本页，建议您将浏览器升级到
<a href="http://www.microsoft.com/china/windows/internet-explorer/ie8howto.aspx" target="_blank">IE8</a>
 或以下浏览器：<a href="http://www.firefox.com.cn/download/">Firefox</a> / <a href="http://www.google.cn/chrome">
Chrome</a> / <a href="http://www.apple.com.cn/safari/">Safari</a> / <a href="http://www.Opera.com/">Opera</a>  
</div>  
<script type="text/javascript">  
function closeme()
{
   var div = document.getElementById("ie6-warning");
   div.style.display ="none";
}
function position_fixed(el, eltop, elleft){  
// check if this is IE6  
if(!window.XMLHttpRequest)  
window.onscroll = function(){  
el.style.top = (document.documentElement.scrollTop + eltop)+"px";  
el.style.left = (document.documentElement.scrollLeft + elleft)+"px";  
}  
else el.style.position = "fixed";  
}  
position_fixed(document.getElementById("ie6-warning"),0, 0);  
</script>  
<![endif]-->

<link href="/css/ald_base.css" rel="stylesheet" type="text/css" />
<link href="/css/ald_style.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        
<div class="header">
  <h1><a href="#">阿拉丁会员管理</a></h1>
</div>
<div class="banner">
  <div class="banner_wrap"> <img src="images/banner_ald.jpg" width="1350" height="410" alt="" />
    <div class="login_bar">
            <div style=" font-weight:100; text-align:left; color:red;">
            <b><asp:Label ID="lblInfor" runat="server" style="color:red"></asp:Label></b>
                     
             </div>
      <ul>
        <li><i class="i01"></i>
          <input type="text" value="用户名" class="inp_txt" runat="server" id="txtUsername" />
        </li>
        <li><i class="i02"></i>
          <input type="password" value="" class="inp_txt" runat="server" id="txtPassword" />
        </li>
        <li style="height:50px">
           <input type="submit" id="btnLogin" runat="server" class="button br-5" style="clear:both" onserverclick="btnLogin_Click" value="登 录" />
        </li>
        <li>
          <input type="checkbox" id="chkRemember" runat="server" />
          保持登录状态</li>
      </ul>
    </div>
  </div>
</div>
<div class="blank40"></div>
<div class="wrap">
  <div class="ser_list clearfix">
    <h2><b>4</b> 大服务亮点</h2>
    <dl>
      <dt class="dt01">创新体验</dt>
      <dd>利用移动技术实现创新的购物体验和消费者互动。</dd>
    </dl>
    <dl>
      <dt class="dt02">微信服务</dt>
      <dd>发挥线下门店优势，借助微信把线下人流导引到品牌会员中心。</dd>
    </dl>
    <dl>
      <dt class="dt03">O2O融合</dt>
      <dd>利用移动互联网技术实现品牌企业的线上、线下融合。</dd>
    </dl>
    <dl>
      <dt class="dt04">精准营销</dt>
      <dd>通过专业的会员数据分析实现精准会员营销。</dd>
    </dl>
  </div>
</div>
        
<input type="hidden" id="hdPwd" runat="server" />
        </form>
<script type="text/javascript">
$(function(){
	(function(){
		var iBtn=true;
		$('#<%= txtUsername.ClientID%>').focus(function(){
			if(iBtn){
				this.value='';
				iBtn=false;
				$(this).css({color:'#333',borderColor:'#666'});
				$(this).prev().css('backgroundColor','#666');
			}
		});
		$('#<%= txtUsername.ClientID%>').blur(function(){
			if(this.value==''){
				this.value='用户名';
				iBtn=true;
				$(this).css({color:'#aaa',borderColor:'#e9e9e9'});
				$(this).prev().css('backgroundColor','#ccc');
			}
		});
	})();
	
	(function(){
		var iBtn=true;
		$('#<%= txtPassword.ClientID%>').focus(function(){
			if(iBtn){
				this.value='';
				iBtn=false;
				$(this).css({color:'#333',borderColor:'#666'});
				$(this).prev().css('backgroundColor','#666');
			}
		});
		$('#<%= txtPassword.ClientID%>').blur(function(){
			if(this.value==''){
				//this.value='密码';
				iBtn=true;
				$(this).css({color:'#aaa',borderColor:'#e9e9e9'});
				$(this).prev().css('backgroundColor','#ccc');
			}
		});
	})()
	
})
fnBindPwd = function(p) {
    document.getElementById("<%= txtPassword.ClientID%>").value = p;
}
</script>
</body>
</html>

