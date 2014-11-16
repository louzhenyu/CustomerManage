<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StructureData.aspx.cs" Inherits="JIT.CPOS.Web.ApplicationInterface.Project.PG.StructureData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>注册城市</h1>
            <p style="width: 500px; height: 100px;">
                城市名
                <asp:TextBox runat="server" ID="CityName" /><br />
                当地员工人数<asp:TextBox runat="server" ID="LocalStaffCount" />
                <br />
                <asp:Button runat="server" ID="AddCityButton" Text="Add City" />

            </p>
        </div>
        <br />

        <div>
            <h1>注册默认Topic</h1>
            <p style="width: 500px; height: 100px;">
                默认可选主题
                <asp:TextBox runat="server" ID="TopicText" Width="500px" /><br />
                Topic排序索引(保留)
                  <asp:TextBox runat="server" ID="IndexTextBox" /><br />
                <br />
                <asp:Button runat="server" ID="DefaultTopicButton" Text="Add Default Topic" />
        </div><br />

        <asp:Button runat="server" ID="SearchButton" Text="根据财年查询" />
    </form>
</body>
</html>
