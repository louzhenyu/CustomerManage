<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="JIT.CPOS.Web.RateLetterInterface.demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Javascript/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //alert("ddd")
            //绑定事件
            $("#btnTest").click(fnTestClick);
        });
        //登录 Login
        //用户列表 GetUsers
        function fnTestClick() {
            //var req = "{\"UserID\":\"6ca19df26ad749beb7f6244b9a033451\",\"CustomerID\":\"e703dbedadd943abacf864531decdac1\",\"Parameters\":{\"Email\":\"1111@132.com\",\"Password\":123,\"FriendlyName\":\"skong123458\" }}";
            var req = "{\"UserID\":\"9CA371688770476D892E22A719054D6F\",\"CustomerID\":\"e703dbedadd943abacf864531decdac1\",\"Parameters\":{\"PageIndex\":0,\"PageSize\":15 }}";
            $.ajax({
                url: "UserHandler.ashx?action=GetUsers",
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
    <form id="f1" enctype="multipart/form-data" action="../ApplicationInterface/Product/QiXinManage/QiXinManageHandler.ashx?action=UploadFile&type=Product&req={}"
    method="post">
    <div>
        <input id="File1" type="file" name="FileUp" />
        <input type="button" value="复星测试" id="btnTest" />
        <input id="Submit1" type="submit" value="上传" />
    </div>
    <%--   <asp:Button ID="Button1" runat="server" Text="批量云帐户注册" OnClick="Button1_Click" />--%></form>

    <form runat="server">
        <asp:Button runat="server" ID="TestButton" Text="IOS消息推送测试"/>

        <br />
        <br />
        <asp:Button runat="server" ID="TestButton0" Text="Android消息推送测试"/>

    </form>
    
</body>
</html>
