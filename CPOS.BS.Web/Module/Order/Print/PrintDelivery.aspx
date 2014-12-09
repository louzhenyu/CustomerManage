<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDelivery.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Order.Print.PrintDelivery" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打印配送单</title>
    <link href="/framework/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="/framework/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/framework/css/webcontrol.css" rel="stylesheet" type="text/css" />
    <link href="/Lib/Css/jit-all.css" rel="stylesheet" type="text/css" />
    <!-- javaScript -->
    <script type="text/javascript" src="../../../Framework/Javascript/Other/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="../../static/js/lib/bdTemplate.js"></script>
    <style type="text/css">
        @media print
        {
            .ipt
            {
                display: none;
            }
        }
        table {
            text-align:center;
            width:100%;
        }
        td,th {
            border:solid 1px #ccc;
            border-collapse:collapse;
        }
        td.number
        {
            text-align:right;
        }
        tbody {
            border:solid 3px #ccc;
        }
        .article {
            padding:1px;
            width:900px;
        }
        table th {
            font-weight:bold;
        }
        table tr.footer th {
            color:red;
            text-align:left;
        }
    </style>
    <style media="print">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>
</head>
<body>
     <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" id="wb" name="wb" width="0"
        height="0">
    </object>
    <div class="section" style="min-height: 0px; height: auto; border: 0;">
         <div id="btnDiv" style="text-align: left; margin-top: 20px; margin-left: 10px" class="Noprint">
            <input type="button" value="打印" onclick='window.print();' style="width: 80px; height: 30px;
                background-color: rgba(134, 192, 47, 0)" />
            <input type="button" value="打印预览" style="width: 80px; height: 30px" onclick='window.print();' />
        </div>
        <div  class="m10 article">
             <table  border="1" cellspacing="0" cellpadding="0">
                 <tbody id="delivery_body">
                 </tbody>
             </table>
            <div class="cb">
            </div>
        </div>
    </div>
    <script id="tpl_noContent" type="text/html">
        <tr >
            <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><#=tips#></span></td>
        </tr>
    </script>
    <script id="tpl_delivery" type="text/html">
        <tr><th colspan="10"><#=entity.A1#></th></tr>
        <tr><td colspan="10"><#=entity.A2#></td></tr>
        <tr><td colspan="10"><span>&nbsp;</span></td></tr>
        <tr>
            <th  colspan="2">客户名称</th>
            <td><#=entity.C4#></td>
            <th>联系电话</th>
            <td colspan="2"><#=entity.E4#></td>
            <th>订单单号</th>
            <td><#=entity.H4#></td>
            <th>配送单号</th>
            <td><#=entity.J4#></td>
        </tr>
        <tr>
            <th colspan="2">送货地址</th><td colspan="4"><#=entity.C5#></td>
            <th>订单时间</th><td><#=entity.H5#></td>
            <th>支付方式</th><td><#=entity.J5#></td>
        </tr>
        <tr><td colspan="10"><span>&nbsp;</span></td></tr>
        <tr>
            <th>序号</th><th>商品编码</th><th>商品条码</th><th style="width:150px;" colspan="2">商品名称</th><th>单位</th><th>商品单价(元)</th><th>商品数量</th><th>金额(元)</th><th>备注</th>
        </tr>
        <#var list = entity.Details;#>
        <#for(var i=0;i<list.length;i++){var item=list[i];#>
            <tr>
                <td><#=i+1#></td>
                <td><#=item.ItemCode#></td>
                <td><#=item.BarCode#></td>
                <td colspan="2"><#=item.ItemName#></td>
                <td><#=item.SalesUnitName#></td>
                <td class="number"><#=item.EnterPrice?item.EnterPrice:0#></td>
                <td class="number"><#=item.qty#></td>
                <td class="number"><#=item.EnterAmount?item.EnterAmount:0#></td>
                <td><#=item.Remark#></td>
            </tr>
        <#}#>
        <#if(list.length<8){#>
            <#for(var c=8-list.length;c>0;c--){#>
                <tr>
                    <td><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                    <td colspan="2"><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                    <td><span>&nbsp;</span></td>
                </tr>
            <#}#>
        <#}#>

        <tr>
            <th style="text-align:center;" colspan="2">小&nbsp;&nbsp;计</th>
            <th colspan="5"></th>
            <td class="number"><#=entity.SumQty#></td>
            <td class="number"><#=entity.SumAmount#></td>
            <td></td>
        </tr>        
        <tr>
            <th style="text-align:center;" colspan="2">配送费</th>
            <th colspan="6">
                
            </th>
            <td class="number"><#=entity.E9#></td>
            <td></td>
        </tr>
       <tr>
            <th style="text-align:center;" colspan="2">抵&nbsp;&nbsp;扣</th>
            <th colspan="5">
                <%--<#if(entity.couponAmount!=0){#>
                    <font>优惠券抵扣：<#=entity.couponAmount#>；</font>
                <#}#>

                <#if(entity.payPointsAmount!=0){#>
                    <font>积分抵扣：<#=entity.payPointsAmount#>（<#=entity.payPoints#>积分）；</font>
                <#}#>

                <#if(entity.vipEndAmount!=0){#>
                    <font>余额抵扣：<#=entity.vipEndAmount#>；</font>
                <#}#>

                <#if(entity.ALBAmount!=0){#>
                    <font>阿拉币：<#=entity.ALBAmount#>（<#=entity.ALB#>阿拉币）；</font>
                <#}#>--%>
                使用阿拉币
                <#if(entity.ALBAmount!=0){#>
                    <font>：<#=entity.ALB#>（<#=entity.ALBAmount#>元）</font>
                <#}#>
                、优惠券
                <#if(entity.couponAmount!=0){#>
                    <font>：<#=entity.couponAmount#></font>
                <#}#>
                、积分
                <#if(entity.payPointsAmount!=0){#>
                    <font>：<#=entity.payPoints#>（<#=entity.payPointsAmount#>元）</font>
                <#}#>
                、余额抵扣
                <#if(entity.vipEndAmount!=0){#>
                    <font>：<#=entity.vipEndAmount#></font>
                <#}#>
            </th>
            <th>总抵扣</th>
            <td class="number"><#=entity.AllDeduction#></td>
            <td></td>
        </tr>
        <tr>
            <th style="text-align:center;" colspan="2">整单实付</th>
            <th colspan="6">
                
            </th>
            <td class="number"><#=entity.actualAmount#></td>
            <td></td>
        </tr>




        <tr class="footer">
            <th colspan="2">配送部门:</th><td></td>
            <th>捡单员:</th>
            <td style="width:70px;"><span>&nbsp;</span></td>
            <th>送货员:</th>
            <td><span>&nbsp;</span></td>
            <td><span>&nbsp;</span></td>
            <th>客户:</th>
            <td><span>&nbsp;</span></td>
        </tr>
    </script>
    <script type="text/javascript" src="Controller/PrintDelivery.js"></script>
</body>
</html>
