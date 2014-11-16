<%@ Page Title="" Language="C#"  AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<%@ Register Src="/Framework/WebControl/HeadRel.ascx" TagName="HeadRel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:HeadRel ID="HeadRel1" runat="server" />
      <script src="Controller/VipClearGroupCtl.js" type="text/javascript"></script>
      <style type="text/css">
            .jitgrid { width: 100%; margin: 0 auto; border: 1px solid #C6C6C6; border-width: 1px 0 0 1px; border-collapse: collapse; }
            .jitgrid caption { margin: 0 auto; width: 100%; line-height: 30px; text-align: left; }
            .jitgrid tr { background-color: expression('#ededed,#fff'.split(',')[rowIndex%2]); }
            .jitgrid td, .jitgrid th { border: 1px solid #C6C6C6; border-width: 0 1px 1px 0; padding: 3px 5px; }
            .jitgrid th { padding: 6px 3px; color: #C6C6C6; }
            .jitgrid td input { background: none; border: 0; border-bottom: 1px solid #C2C3C8; padding: 0; margin: 0; }
    </style>
</head>
<body>
    <div class="section" style="min-height: 0px; height: auto; border: 0;">
        <div class="m10 article">
            <div class="view_Search">
                <span id="span_panel"></span>
            </div>
            <div class="view_Search">
                <span id="span_panel2"></span>
            </div>
        </div>
    </div>
  
</body>
</html>
