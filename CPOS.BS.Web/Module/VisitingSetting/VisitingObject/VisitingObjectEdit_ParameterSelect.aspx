<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<%@ Register src="/Framework/WebControl/HeadRel.ascx" tagname="HeadRel" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<uc1:HeadRel ID="HeadRel1" runat="server" />
    <script src="/Lib/Javascript/Ext4.1.0/ux/data/PagingMemoryProxy.js" type="text/javascript"></script>

<script src="Controller/ParameterSelectCtl.js" type="text/javascript"></script>
<script src="Model/ParameterSelectVM.js" type="text/javascript"></script>
<script src="Store/ParameterSelectVMStore.js" type="text/javascript"></script>
<script src="View/ParameterSelectView.js" type="text/javascript"></script>
</head>
<body>
<div class="section" style="min-height:0px;height:auto;border:0;">
        <div class="m10 article">
        <div class="art-titbutton">
             <div class="view_Button">
                    <span id='span_save'></span>
               </div>          
            </div>
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
</body>
</html>