<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<%@ Register Src="/Framework/WebControl/HeadRel.ascx" TagName="HeadRel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:HeadRel ID="HeadRel1" runat="server" />
    
    <script src="/Framework/Javascript/pub/JITStoreFrmWindow.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITDynamicGrid.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
    <%--<script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>--%>
    <%--<script src="/Framework/javascript/Biz/Category.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>--%>
    <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/HierarchyItem.js" type="text/javascript"></script>

    <script src="Controller/SKUSelectCtl.js" type="text/javascript"></script>
    <script src="View/SKUSelectView.js" type="text/javascript"></script>
</head>
<body>
    <div class="section" style="min-height: 0px; height: auto; border: 0;">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='dvSearch'></span>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='dvWork'></span>
                </div>
            </div>
            <div class="DivGridView" id="dvGrid">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</body>
</html>
