<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseTest.aspx.cs" Inherits="JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.HouseTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
  <form action="http://222.66.40.26/huaan-worldunion/t/ReservationPurchase.action" method="post" >
    <input type="hidden" name="verNum" value="<%=Model.verNum %>" />
    <input type="hidden" name="merchantID" value="<%=Model.merchantID %>" />
    <input type="hidden" name="sysdate" value="<%=Model.sysdate %>" />
    <input type="hidden" name="systime" value="<%=Model.systime %>" />
    <input type="hidden" name="txcode" value="<%=Model.txcode %>" />
    <input type="hidden" name="seqNO" value="<%=Model.seqNO %>" />
    <input type="hidden" name="maccode" value="<%=Model.maccode %>" />
    <input type="hidden" name="content" value="<%=Model.content %>" />

    <input type="text" name="test" value="<%=Model.content %>" style="width:1200px;"/>
    <input type="submit" value="提 交" />
    </form>
   
</body>
</html>
