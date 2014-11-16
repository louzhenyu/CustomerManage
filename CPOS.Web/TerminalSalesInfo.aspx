<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TerminalSalesInfo.aspx.cs"
    Inherits="JIT.CPOS.Web.wap.TerminalSalesInfo" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,  maximum-scale=1.0, minimum-scale=1.0" />
    <title></title>
    <style type="text/css">
        .ts_td { background:url('Images/bg.png'); height:22px; width:80px; border:1px solid #ccc; text-align:center; vertical-align:middle; font-size:13px; }
        .ts_td2 { background:url('Images/bg2.png'); height:22px; width:80px; border:1px solid #ccc; text-align:center; vertical-align:middle; font-size:13px;border-right:0px solid #ccc;border-left:0px solid #ccc; }
        .ts_td3 { height:32px; vertical-align:middle; padding:4px; color:gray; border-bottom:1px solid #ccc; text-align:center; }
    </style>
</head>
<body style=" padding:0px; margin:0px;font-size:13px;">
    <script type="text/javascript">
        
    </script>
    
    <div style="font-weight:bold; margin-top:10px; padding-left:10px;  padding-right:10px; width:96%; height:14px;">
        <div style="float:left;"><%=strUnit %></div>
        <div style="float:right;"><%=strToday %></div>
    </div>
    <div id="pnl" style="width:; height:; overflow-y:; display:; margin-top:10px;">
        <%=strDiv %>
    </div>

    <input type="hidden" id="hUnitId" runat="server" />

</body>
</html>
