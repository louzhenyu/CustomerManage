<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextImageWapMobile.aspx.cs" Inherits="JIT.CPOS.Web.WeiXin.TextImageWapMobile" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, minimum-scale=1.0" />
    <title>
        <%=title%></title>
    <link href="news.css" type="text/css" rel="stylesheet" />
<style>

.bot_nav {
    background: none repeat scroll 0 0 #000000;
    bottom: 0;
    height: 44px;
    left: 0;
    padding: 10px;
    position: fixed;
    width: 100%;
    z-index: 102;
}

.bot_nav dt {
    float: left;
}

.bot_nav dt a,  {
    background-size: 120% auto;
    display: block;
    height: 30px;
    overflow: hidden;
    text-indent: -9999px;
    width: 30px;
    background: url("http://o2oapi.aladingyidong.com/fuxing/css/../images/sprites.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
}


</style>

</head>
<body style="margin-bottom:70px;">
    <div class="connext" style="padding-bottom: 0px;">
        <div class="AlumniConnext2" style="padding-bottom: 20px; width: 100%">
            <span class="blank1" style="height: 5px;"></span>
            <div style="width: 92%; margin-left: auto; margin-right: auto; line-height: 28px;
                text-shadow: 0 0 1px #999; font-size: 14px;">
                <h5 style="padding-top: 20px; padding-bottom: 5px; font-size: 26px;">
                    <asp:Literal ID="newsTitle" runat="server"></asp:Literal></h5>
                <p style="color: gray; padding-bottom: 10px;">
                    <asp:Literal ID="newsPublishTime" runat="server" ></asp:Literal></p>
                
                <asp:Literal ID="newsContent" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <dl class="bot_nav">
        <dt>
            <a href="javascript:history.go(-1);">返回</a>
        </dt>

    </dl>

    <script>
        if (document.addEventListener) {
            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
        function onBridgeReady() {

            WeixinJSBridge.call('hideToolbar');
            var 
            appId = '',
      imgUrl = "<%=imageUrl%>",
      link = document.URL,
title = "<%=title%>",
            desc = "<%=description%>",
            desc = decodeURIComponent(desc);
            title = decodeURIComponent(title);
            fakeid = "",
            desc = desc || link;
            // 发送给好友; 
            WeixinJSBridge.on('menu:share:appmessage', function (argv) {

                WeixinJSBridge.invoke('sendAppMessage', {
                    "appid": appId,
                    "img_url": imgUrl,
                    "img_width": "640",
                    "img_height": "640",
                    "link": link,
                    "desc": desc,
                    "title": title
                }, function (res) {

                });
            });
            WeixinJSBridge.on('menu:share:timeline', function (argv) {
                //  report(link, fakeid, 2);
                WeixinJSBridge.invoke('shareTimeline', {
                    "img_url": imgUrl,
                    "img_width": "640",
                    "img_height": "640",
                    "link": link,
                    "desc": desc,
                    "title": title
                }, function (res) {
                });

            });
            var weiboContent = '';
            WeixinJSBridge.on('menu:share:weibo', function (argv) {

                WeixinJSBridge.invoke('shareWeibo', {
                    "content": title + link,
                    "url": link
                }, function (res) {

                });
            });
        }
    </script>
</body>
</html>

