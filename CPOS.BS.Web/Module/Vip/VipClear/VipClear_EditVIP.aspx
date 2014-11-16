<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<%@ Register Src="/Framework/WebControl/HeadRel.ascx" TagName="HeadRel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:HeadRel ID="HeadRel1" runat="server" />
    <!--for vip control-->
    <link href="/framework/CssNew/reset.css" rel="stylesheet" type="text/css" />
    <link href="/framework/CssNew/style.css" rel="stylesheet" type="text/css" />
    <link href="/framework/CssNew/webcontrol.css" rel="stylesheet" type="text/css" />
    <script src="/Framework/Javascript/pub/JTIPagePannel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITVipFrmWindow.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Tags.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
    <!--for vip control-->
    <script src="Controller/VipClear_EditVIPCtl.js" type="text/javascript"></script>
    <script src="View/VipClear_EditVIPView.js" type="text/javascript"></script>
</head>
<body>
    <div class="section" style="min-height: 0px; height: auto; border: 0;">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='dvSearch'></span>
                </div>
            </div>
            <div class="DivGridView" id="dvGrid">
            </div>
        </div>
        <div style="display: block; height: 0px; overflow: hidden;">
            <iframe id="tab1" frameborder="0" height="520px" width="100%" src=""></iframe>
            <iframe id="tab2" frameborder="0" height="520px" width="100%" src=""></iframe>
            <iframe id="tab3" frameborder="0" height="520px" width="100%" src=""></iframe>
            <iframe id="tab4" frameborder="0" height="520px" width="100%" src=""></iframe>
            <iframe id="tab5" frameborder="0" height="520px" width="100%" src=""></iframe>
        </div>
    </div>
</body>
</html>
