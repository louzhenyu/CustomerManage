<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FosunTest.aspx.cs" Inherits="JIT.CPOS.Web.ApplicationInterface.Product.CW.FosunTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../Javascript/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //绑定事件
            $("#btnTest").click(fnTestClick);
        });
        //登录 Login
        //用户列表 GetUsers
        function fnTestClick() {
            //var req = "{\"UserID\":\"6ca19df26ad749beb7f6244b9a033451\",\"CustomerID\":\"e703dbedadd943abacf864531decdac1\",\"Parameters\":{\"Email\":\"1111@132.com\",\"Password\":123,\"FriendlyName\":\"skong123458\" }}";
            var req = "{\"UserID\":\"9CA371688770476D892E22A719054D6F\",\"CustomerID\":\"e703dbedadd943abacf864531decdac1\",\"Parameters\":{\"PageIndex\":0,\"PageSize\":15 }}";
            $.ajax({
                url: "/ApplicationInterface/Product/CW/CWHandler.ashx?action=GetUsers&type=Product",
                data: {
                    "req": req
                },
                type: "POST",
                success: function (data) {
                    if (data) {
                        alert(data);
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="复星测试" id="btnTest" />
    </div>
    </form>
</body>
</html>
