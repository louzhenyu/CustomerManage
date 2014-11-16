<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoSso.aspx.cs" Inherits="JIT.CPOS.BS.Web.GoSso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <script src="js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
    if (location.host.indexOf("lzlj.") > -1) location.href = "Login.aspx";
    //else if (location.host.indexOf("") > -1) location.href = "Login4.aspx";
    else if (location.host.indexOf("vanke2049.") > -1) location.href = "Login5.aspx";
    else if (location.host.indexOf("hotwind.") > -1) location.href = "Login6.aspx";
    else if (location.host.indexOf("i.fosun.com") > -1) location.href = "LoginFosun.aspx";
    else if (location.host.indexOf("aladinyidong.com") > -1) location.href = "http://www.aladinyidong.com/Login.aspx";
    else location.href = '<%=this.ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["sso_url"].ToString()) %>';
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
