<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="JIT.CPOS.Web.WifiInterface.data.Data"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        (function () {
            var skey = '<%=SKey %>';
            if (!skey) {
                alert('很抱歉获取不到路由器信息,请联系客服人员.');
                return false;
            };
            //延时读取微信对象
            var weiXinTime = 0, exeCount = 0;
            weiXinTime = setInterval(function () {
                if (typeof WeixinJSBridge == 'object') {
                    var customerId = getUrlParam('customerId'), userId = getUrlParam('userId'), driveId = skey, toUrl = '';
                    //                    var toUrl = 'http://o2oapi.aladingyidong.com/WXOAuth/NoAuthGoto.aspx?'
                    //					+ 'customerId=' + customerId +
                    //					+'&pageName=Init&' + '&userId=' + userId + '&driveId=' + driveId
                    //					+ 'Url=http://o2oapi.aladingyidong.com/HtmlApps/html/_pageName_';
                    var toUrl = "http://o2oapi.aladingyidong.com/HtmlApps/Auth.html?pageName=Init&customerId=f6a7da3d28f74f2abedfc3ea0cf65c01&userId="+userId+"&driveId="+skey;
                    location.href = toUrl;
                    clearInterval(weiXinTime);
                }


                if (exeCount >= 5) {
                    debugger;
                    clearInterval(weiXinTime);
                    var wifitip = document.getElementById('wifitip');
                    wifitip.style.display = 'block';
                }
                exeCount++;
            }, 100);



            //获取URL参数
            function getUrlParam(key) {

                var value = "", itemarr = [],
				urlstr = window.location.href.split("?");

                if (urlstr[1]) {

                    var item = urlstr[1].split("&");

                    for (i = 0; i < item.length; i++) {

                        itemarr = item[i].split("=");

                        if (key == itemarr[0]) {

                            value = itemarr[1];
                        }
                    }
                }

                return value;
            };

        })();




    </script>
    <form id="form1" >
    <div id="wifitip" style="display:none" >
    您当前免费Wifi上网5分钟，微信关注杰亦特即可享受永久免费上网
    </div>
    </form>
</body>
</html>
