<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOrders.aspx.cs" ValidateRequest="true"
    Inherits="JIT.CPOS.BS.Web.Module.Orders.Orders.PrintOrders" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Framework/Javascript/Other/jquery-1.9.0.min.js"></script>
    <script src="/Framework/Javascript/Utility/CommonMethod.js" type="text/javascript"></script>
    <style type="text/css">
        @media print {
            .ipt { display: none; }
        }
        table { border-collapse: collapse; }
        .ebooking_list_t { width: 946px; margin: 0 auto; text-align: left; }
        .ebooking_list_t h2 { width: 500px; padding-left: 16px; font-weight: 700; font-size: 14px; }
        .ebooking_list_t .pages_spilt { margin-top: -12px; text-align: right; }
        .form_area_detail { width: 946px; margin: 12px auto 30px auto; padding: 8px 0; border: 1px solid #06c; background: #e9f1fe; }
        .form_area_detail table { width: 880px; margin: 0 auto; }
        .form_area_detail th, .form_area_detail td { padding: 3px; text-align: left; }
        .form_area_detail th { height: 20px; width: 100px; font-weight: 700; text-align: right; }
    </style>
    <script>
        $(function () {
            HiddenDiv();
        });
    </script>
    <script for="window" event="onload" language="javascript" type="text/javascript">
	    if (window.print) {
		    window.print();
	    }
	    else{
		    alert("您的浏览器不支持自动打印，请使用浏览器的打印功能！")
	    }
    </script>
</head>
<body>
    <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" id="wb" name="wb" width="0" height="0">
    </object>
    <br />
    <%=res %>
    <form id="form1" runat="server">
    <div id="btnDiv" style="text-align: center; margin-top: 20px">
        <input type="button" value="打印" onclick='HiddenDiv();window.print();' style="width: 80px;
            height: 30px" />
    </div>
    </form>
    <script type="text/javascript">
        function HiddenDiv() {
            document.getElementById("btnDiv").style.display = "none";
            setTimeout("document.getElementById('btnDiv').style.display=''", 2000);
        }
    </script>
</body>
</html>
