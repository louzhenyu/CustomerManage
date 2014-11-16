<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="JIT.EMBAUnion.Web.NewsDetail" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,  maximum-scale=1.0, minimum-scale=1.0" />
    <link href="Framework/Css/news.css" type="text/css" rel="stylesheet" />
    <title><%=Request.QueryString["isFmba"]!=null?"学生故事":"新闻内容"%></title>
</head>
<body style="">
    <div class="connext" style="padding-bottom: 0px;">
        <div class="AlumniConnext2" style="padding-bottom: 20px; width: 100%">
            <%--<div class="AlumniConnext1Title">
                <a href="#" class="AlumniBack icnbg"></a>
            </div>--%>
            <span class="blank1" style="height: 5px;"></span>
            <div style="width: 92%; margin-left: auto; margin-right: auto; line-height: 28px;text-shadow: 0 0 1px #999; font-size: 14px;">
                <h2 style="padding-top: 20px; padding-bottom: 5px; font-size: 16px;">
                    <asp:Literal ID="newsTitle" runat="server"></asp:Literal></h2>
                <p style="color: gray; padding-bottom: 10px;">
                    <asp:Literal ID="newsPublishTime" runat="server"></asp:Literal></p>
                <div>
                    <img src="<%=imageUrl%>" alt="" width="100%" />
                </div>
                <asp:Literal ID="newsContent" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</body>
</html>
