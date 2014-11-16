<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<%@ Register src="/Framework/WebControl/HeadRel.ascx" tagname="HeadRel" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<uc1:HeadRel ID="HeadRel1" runat="server" />

<script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>

<script src="Controller/StepEditCtl.js" type="text/javascript"></script>
<script src="Model/StepEditVM.js" type="text/javascript"></script>
<script src="Store/StepEditVMStore.js" type="text/javascript"></script>
<script src="View/StepEditView.js" type="text/javascript"></script>
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