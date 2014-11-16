<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<%@ Register src="/Framework/WebControl/HeadRel.ascx" tagname="HeadRel" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<uc1:HeadRel ID="HeadRel1" runat="server" />

<script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>

<script src="Controller/VisitingObjectEditCtl.js" type="text/javascript"></script>
<script src="Model/VisitingObjectEditVM.js" type="text/javascript"></script>
<script src="Store/VisitingObjectEditVMStore.js" type="text/javascript"></script>
<script src="View/VisitingObjectEditView.js" type="text/javascript"></script>
</head>
<body>
<div class="section" style="min-height:0px;height:auto;border:0;">
        <div class="m10 article">
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
</body>
</html>