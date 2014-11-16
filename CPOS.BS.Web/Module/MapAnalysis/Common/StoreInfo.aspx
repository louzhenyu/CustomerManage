<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreInfo.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.MapAnalysis.Common.StoreInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="LayoutResource/css/tt.css"/>
    <link rel="stylesheet" type="text/css" href="./LayoutResource/js/fancybox/jquery.fancybox-1.3.4.css" media="screen"/>
    <link rel="stylesheet" href="LayoutResource/css/superfish.css" media="screen" />
    
    <!--// Begin:日期控件样式表-->
    <link rel="stylesheet" href="./LayoutResource/js/date/date_input.css" type="text/css">
    <!--// End:日期控件样式表-->
    <script type="text/javascript" src="./LayoutResource/js/plugin/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="./LayoutResource/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="./LayoutResource/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="./LayoutResource/js/superfish/hoverIntent.js" type="text/javascript"></script>
    <script src="./LayoutResource/js/superfish/superfish.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="winWraper">
        <div class="winBoxer" style="width:492px;">
            <%--<div class="winCaption">
                <span class="winCaption_icon tShop"></span>
                <a class="btnCloseWin" href="###" onclick="closeMapPointWin(this);">关闭</a>
            </div>--%>
            <div class="winContent contShare">
                <b class="h3"><span class="tH3"><%=this.StoreName %></span> <span class="tDate"><%=DateTime.Now.ToString("yyyy-MM-dd") %></span></b>
                <asp:Literal ID="ltStoreContent" runat="server"></asp:Literal>
            </div>
            <div class="clearFix_12"></div>
        </div>
    </div>
    </form>
</body>
</html>
