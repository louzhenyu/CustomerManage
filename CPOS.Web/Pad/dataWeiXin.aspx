<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataWeiXin.aspx.cs" Inherits="JIT.CPOS.Web.Pad.dataWeiXin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
<meta http-equiv="Expires" CONTENT="0">
<meta http-equiv="Cache-Control" CONTENT="no-cache">
<meta http-equiv="Pragma" CONTENT="no-cache">
<meta http-equiv='X-UA-Compatible' content='IE=EmulateIE7' />
<meta name="viewport" content="width=device-width,  maximum-scale=1.0, minimum-scale=1.0" />
<title></title>
<style type="text/css">
body, h1, h2, h3, h4, h5, h6, hr, p, blockquote, dl, dt, dd, ul, ol, li, pre, form, fieldset, legend, button, input, textarea, th, td, img { border: none; margin: 0; padding: 0;  }
body, button, input, select, textarea { font-size: 14px; font-family: "微软雅黑",Arial, Helvetica, sans-serif; word-break: break-all; word-wrap: break-word; color: #565656; resize: none; outline: none; }
body { background:#fefefe url(../images/bg.gif) repeat left top;}
h1, h2, h3, h4, h5, h6 { font-size: 16px; font-weight: normal; }
em { font-style: normal; }
ul, ol, img { list-style: none; border: 0; }
table, th, td, tr { border-collapse: collapse; border-spacing: 0; border: 0; font-size: 16px; margin: 0; padding: 0; }
textarea, input[type="text"],input[type="password"],input[type="button"],input[type="submit"]{ resize: none; outline: none; -webkit-appearance: none;   }
.zoom { overflow: hidden; zoom: 1 }
.frdisplay{ float:right; display:block;}
a,a:hover{ text-decoration:none; color:#565656;}
.PcBox{ position:relative}
.blank1{ height:1px; overflow:hidden; display:block; clear:both;}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" width:320px; margin-left:auto; margin-right:auto; position:relative; height:100%;">
    	<div style="position:absolute; left:0; top:0; z-index:1">
        	<img src="images/bg.png" width="320" >
        </div>
<%--    	<div style="position:absolute; left:50px; top:70px; z-index:2;  line-height:30px;font-size:18px; font-weight:bold; color:#697d7d; display:none;">
        您还未关注微信账号，<br>请联系门店工作人员
        </div>
        <div style="position:absolute; left:50px; top:70px; line-height:30px; z-index:2; font-size:18px; font-weight:bold; color:#697d7d">
       某某某，<br> 您的本次积分为****，<br> 累积积分为**** <br>请向门店工作人员展示：<br> 流水号：13448
        </div>--%>
        <%=strText%>
    </div>
    </form>
</body>
</html>
