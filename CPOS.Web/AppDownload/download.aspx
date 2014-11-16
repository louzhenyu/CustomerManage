<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download.aspx.cs" Inherits="JIT.CPOS.Web.AppDownload.download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv='X-UA-Compatible' content='IE=EmulateIE7' />
    <meta name="viewport" content="width=device-width,  maximum-scale=1.0, minimum-scale=1.0" />
    <title>APP下载</title>
    <style type="text/css">
    body, h1, h2, h3, h4, h5, h6, hr, p, blockquote, dl, dt, dd, ul, ol, li, pre, form, fieldset, legend, button, input, textarea, th, td, img { border: none; margin: 0; padding: 0;  }
    body, button, input, select, textarea { font-size: 12px; font-family:THeiti,Arial, Helvetica, sans-serif; color: #565656; resize: none; outline: none; }
    body { }
    h1, h2, h3, h4, h5, h6 { font-size: 16px; font-weight: normal; }
    em { font-style: normal; }
    ul, ol, img { list-style: none; border: 0; }
    table, th, td, tr { border-collapse: collapse; border-spacing: 0; border: 0; font-size: 14px; margin: 0; padding: 0; }
    textarea, input[type="text"],input[type="password"],input[type="button"],input[type="submit"]{ resize: none; outline: none; -webkit-appearance: none;   }
    .zoom { overflow: hidden; zoom: 1 }
    .frdisplay{ float:right; display:block;}
    a,a:hover{ text-decoration:none; color:#565656;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="gohigh" id="gohigh" style="margin-left:auto; margin-right:auto; ">
	<div class="gohighBanner"><img src="<%=ImageUrl %>" width="100%" /></div>
    <div class="gohighDownloadinc" style="padding-top:40px;">
    	<Div style="width:40%; float:left; display:block; _display:inline; margin: 0 5%;">
        <a href="<%=IOSUrl %>"><img src="images/iphone.jpg" width="100%" /></a></div>
    <Div style="width:40%; float:left; display:block; _display:inline;"><a href="<%=AndroidUrl %>">
    <img src="images/android.jpg" width="100%" /></a>
    </Div>
  

    </div>

</div>
<script type="text/javascript">
    function IsPC() {
        var userAgentInfo = navigator.userAgent;

        var Agents = new Array("Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod");
        var flag = true;
        for (var v = 0; v < Agents.length; v++) {
            if (userAgentInfo.indexOf(Agents[v]) > 0) {
                flag = false;
                break;
            }
        }
        return flag;
    }
    if (IsPC()) {
        document.getElementById("gohigh").style.width = "420px";
    }
</script>
    </form>
</body>
</html>
