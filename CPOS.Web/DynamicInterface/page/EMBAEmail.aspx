<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMBAEmail.aspx.cs" Inherits="JIT.CPOS.Web.DynamicInterface.page.EMBAEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">
<title>EMBA会员详细</title>
<link href="css/global.css" rel="stylesheet" type="text/css" />
<link href="css/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div class="page-720">
	<div class="header">
    	<h1 class="logo">联盟注册</h1>
        <h2 class="tit">会员详细信息</h2>
    </div>
    
   <div class="content">
   		<div class="wrapArea">
        	<p><em>姓名：</em><span><asp:Literal runat="server" ID="lt_VipName"></asp:Literal></span></p>
            <p><em>手机：</em><span><asp:Literal runat="server" ID="lt_Phone"></asp:Literal></span></p>
            <p><em>学校：</em><span><asp:Literal runat="server" ID="lt_Col1"></asp:Literal></span></p>
            <p><em>课程：</em><span><asp:Literal runat="server" ID="lt_Col2"></asp:Literal></span></p>
            <p><em>期/班级：</em><span><asp:Literal runat="server" ID="lt_Col3"></asp:Literal></span></p>
            <p><em>邮箱：</em><span><asp:Literal runat="server" ID="lt_Email"></asp:Literal></span></p>
        </div>
        

        <div class="wrapArea">
            <p><em>行业：</em><span><asp:Literal runat="server" ID="lt_Col4"></asp:Literal></span></p>
        	<p><em>公司：</em><span><asp:Literal runat="server" ID="lt_Col5"></asp:Literal></span></p>
            <p><em>职位：</em><span><asp:Literal runat="server" ID="lt_Col6"></asp:Literal></span></p>      
            <p><em>活动城市：</em><span><asp:Literal runat="server" ID="lt_Col7"></asp:Literal></span></p>
        </div>
        
        
        <div class="wrapArea">
        	<p><em>微信号：</em><span><asp:Literal runat="server" ID="lt_WeiXin"></asp:Literal></span></p>
            <p><em>微博号：</em><span><asp:Literal runat="server" ID="lt_SinaMBlog"></asp:Literal></span></p>
            <p><em>出生日期：</em><span><asp:Literal runat="server" ID="lt_Birthday"></asp:Literal></span></p>
            <p><em>籍贯：</em><span><asp:Literal runat="server" ID="lt_Col8"></asp:Literal></span></p>
            <p><em>婚否：</em><span><asp:Literal runat="server" ID="lt_Col9"></asp:Literal></span></p>
         </div>
         
         
         
         <div class="wrapArea">
        	<p class="tit">个人简介：</p>
            <p>
           <asp:Literal runat="server" ID="lt_Col10"></asp:Literal> 
            </p>
            
            <p class="tit">我能为联盟提供的价值：</p>
            <p>
           <asp:Literal runat="server" ID="lt_Col11"></asp:Literal>
           </p>
            
            <p class="tit">我最希望从联盟获得的价值：</p>
            <p>
            <asp:Literal runat="server" ID="lt_Col12"></asp:Literal>
            </p>
            
            <p class="tit">能为其他校友提供的行业经验分享主题：</p>
            <p>
           <asp:Literal runat="server" ID="lt_Col13"></asp:Literal>
            </p>
           
         </div>
         
        
        <p class="copyright">技术提供：协会宝--专注于协会/商会、俱乐部的微信和APP会员平台，技术支持请联系我们</p>
          
   </div>
   
</div>
    </form>
</body>
</html>
