<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TerminalSalesInfo2.aspx.cs"
    Inherits="JIT.CPOS.Web.wap.TerminalSalesInfo2" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,  maximum-scale=1.0, minimum-scale=1.0" />
    <title></title>
    <style type="text/css">
        body, h1, h2, h3, h4, h5, h6, hr, p, blockquote, dl, dt, dd, ul, ol, li, pre, form, fieldset, legend, button, input, textarea, th, td, img
        {
            border: none;
            margin: 0;
            padding: 0;
        }
        body, button, input, select, textarea
        {
            font-size: 100%;
            font-family: "Microsoft YaHei" ,Arial, Helvetica, sans-serif;
            word-break: break-all;
            word-wrap: break-word;
            color: #4d4d4d;
            resize: none;
            outline: none;
        }
        body
        {
            background: url(images/bg.png) repeat left top;
        }
        h1, h2, h3, h4, h5, h6
        {
            font-size: 100%;
            font-weight: normal;
        }
        em
        {
            font-style: normal;
        }
        ul, ol, img
        {
            list-style: none;
            border: 0;
        }
        a
        {
            text-decoration: none;
            color: #377CA2;
        }
        a:hover
        {
            text-decoration: underline;
        }
        table, th, td, tr
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 0;
            font-size: 18px;
            margin: 0;
            padding: 0;
        }
        textarea, input
        {
            resize: none;
            outline: none;
        }
        .zoom
        {
            overflow: hidden;
            zoom: 1;
        }
        .Apptitle
        {
            background: -webkit-gradient(linear, left top, left bottom, from(#eeeeee), to(#fefefe), color-stop(0.3, #fff), color-stop(0.35, #fff), color-stop(0.6, #d9d9d9), color-stop(0.8, #eeeeee));
            height: 48px;
            border-bottom: 1px solid #c4c4c4;
            line-height: 48px;
            text-align: center;
        }
        .AppBanner
        {
            margin-left: auto;
            margin-right: auto;
            position: relative;
        }
        .AppBannerDec
        {
            position: absolute;
            bottom: 5%;
            right: 2%;
            font-size: 14px;
            line-height: 36px;
            padding: 0 5px;
        }
        .AppConnext
        {
            background-color: #F7F7F7;
            overflow: hidden;
        }
        .AppBannerDec h2
        {
            opacity: 0.53;
            height: 100%;
            width: 100%;
            position: absolute;
            left: 0;
            top: 0;
            background-color: #fff;
            z-index: 0;
        }
        .AppBannerDec span
        {
            color: #000;
            position: absolute;
            left: 0;
            top: 0;
            z-index: 1;
            height: 100%;
            padding: 0 5px;
        }
        .AppMf
        {
            background: -webkit-gradient(linear, left top, left bottom, from(#525252), to(#242424), color-stop(0.5, #3c3c3c));
            height: 45px;
            color: #fff;
            font-size: 22px;
            line-height: 45px;
            overflow: hidden;
            border-top: 1px solid #7b7b7b;
            text-align: center;
        }
        .AppConnextDiv
        {
            padding: 16px 3%;
        }
        .AppConnextDiv td, .AppConnextText td
        {
            font-size: 14px;
            text-align: left;
            padding: 5px 0;
        }
        .AppConnextText
        {
            background-color: #E0E0E0;
            padding: 16px 3%;
        }
        .inputstyle
        {
            border: 1px solid #c1c6cc;
            border-radius: 10px;
            height: 32px;
            padding-left: 2%;
            font-size: 14px;
            background: -webkit-gradient(linear, left top, left bottom, from(#f2f4f5), to(#fefefe));
            color: #393939;
            text-shadow: 0 0 1px #fff;
        }
        .labelSelect
        {
            position: relative;
            font-family: Helvetica, arial;
        }
        .labelSelect:after
        {
            position: absolute;
            top: -8px;
            right: 4px;
            display: inline-block;
            content: " ▼";
            color: #687280;
            font-size: 18px;
            text-align: center;
            width: 30px;
            height: 34px;
            line-height: 34px;
            border-left: 1px solid #c1c6cc;
            pointer-events: none;
            text-shadow: 0 0 2px #fff;
        }
        .labelSelect select
        {
            font-size: 16px;
            color: #000;
            width: 95%;
            line-height: 34px;
            border-radius: 10px;
            height: 34px;
            -webkit-appearance: none;
            border: 1px solid #c1c6cc;
            padding-left: 12px;
            background: -webkit-gradient(linear, left top, left bottom, from(#fefefe), to(#f2f4f5));
            color: #393939;
            text-shadow: 0 0 1px #fff;
        }
        .btnSubmit
        {
            width: 100%;
            border: 1px solid #ca541d;
            background: -webkit-gradient(linear, left top, left bottom, from(#ff9000), to(#f06000), color-stop(0.5, #f77700));
            height: 120px;
            border-radius: 8px;
            font-size: 22px;
            color: #fff;
            cursor: pointer;
        }
        .AppHj
        {
            font-size: 12px;
            line-height: 16px;
        }
        .AppHj h3
        {
            font-size: 16px;
            padding: 16px ;
            border-bottom: 1px solid #e1e1e1;
        }
        
        .AppHj li
        {
            overflow: hidden;
            padding: 6px 3%;
            border-bottom: 1px solid #e1e1e1;
        }
        .date
        {
            float: right;
        }
        .more
        {
            font-size: 18px;
            text-align: center;
            line-height: 30px;
            padding: 8px 0;
            border-top: 1px dashed #a3a3a3;
            cursor: pointer;
        }
        
        .AppEnd
        {
            text-align: center;
            padding: 26px 3%;
            background-color: #eedea6;
        }
        .TdEndBtn
        {
            width: 50%;
            margin-left: auto;
            margin-right: auto;
            background: -webkit-gradient(linear, left top, left bottom, from(#f1f1f1), to(#bdb8b0), color-stop(0.5, #d5d2ce));
            height: 62px;
            padding-top: 12px;
            border: 1px solid #8d8d8d;
            border-radius: 5px;
            font-size: 20px;
            margin-top: 12px;
        }
        
        a.Back
        {
            background: url(images/icn.png) no-repeat left top;
            width: 50px;
            height: 50px;
            position: absolute;
            left: 0;
            top: 0;
            display: block;
        }
        .AppLuckyDraw
        {
            border: 1px dashed #454545;
            width: 92%;
            padding-bottom: 15px;
            background: url(images/jbg.png) repeat left top;
            margin-left: auto;
            margin-right: auto;
            margin-top: 20px;
            margin-bottom: 20px;
            box-shadow: 0 0 15px #a5a5a5;
            position: relative;
        }
        .icnJw
        {
            background: url(images/icn.png) no-repeat 0 -107px;
            width: 35px;
            height: 25px;
            position: absolute;
            top: -16px;
            left: -22px;
        }
        .AppLuckTitle
        {
            background: #4a4a4a;
            text-align: center;
            height: 36px;
            padding: 10px 0;
            font-size: 22px;
            color: #fff;
            font-weight: bold;
        }
        .AppLuckS
        {
            line-height: 36px;
            font-size: 20px;
            width: 90%;
            margin-left: auto;
            margin-right: auto;
            padding-top: 40px;
            position: relative;
        }
        a.MdQing, a.MdQing:hover
        {
            font-size: 18px;
            color: #fff;
            font-weight: bold;
            height: 34px;
            background-color: #000;
            text-decoration: none;
            border-radius: 5px;
            border: 1px solid #393939;
            box-shadow: 0 0 5px #999;
            line-height: 34px;
            display: block;
            text-align: center;
            margin-right: 2%;
        }
        
        .AppTip
        {
            line-height: 30px;
            background-color: #e3e3e3;
            width: 88%;
            margin-left: auto;
            margin-right: auto;
            padding: 12px 3%;
        }
        .AppLogin
        {
            border: 1px solid #a3a3a3;
            -webkit-box-shadow: 2px 2px 2px #dbdbdb inset;
            background-color: #fff;
            width: 96%;
            margin-left: auto;
            margin-right: auto;
            margin-top: 0px;
        }
        .AppInputcc
        {
            border-bottom: 1px solid #d4d4d4;
            padding: 10px 0;
            position: relative;
        }
        .z_btn1 {  padding:0px; text-align:center; cursor:pointer; color:#fff; font-weight:bold; overflow:hidden; font-size:13px;
		float:left; margin:0; padding:0; height:32px; line-height:32px; background:#69c0ff; width:80px; border:1px solid #007cce; position:relative; z-index:2
		 }
        .z_btn2 {  text-align:center; cursor:pointer; color:#000; font-weight:normal; overflow:hidden; font-size:13px; 
		float:left; margin:0; padding:0; height:32px; line-height:32px; background:#f8f8f8; width:80px; border:1px solid #bbbbbb; position:relative; z-index:1;
		 }
		
		
    </style>
    <script type="text/javascript" src="Javascript/ext-all.js"></script>
    <script type="text/javascript" src="Javascript/example-data.js"></script>
    <script type="text/javascript" src="Javascript/Pie.js"></script>
</head>
<body style=" padding-bottom:0px;">
    <script type="text/javascript">
        var z_topData = Ext.decode('<%=topDataStr %>');
        var z_topData2 = Ext.decode('<%=topDataStr2 %>');
    </script>
    <div style="height:32px; margin-left:20px; margin-top:10px;  overflow:hidden;">
        <div id="btn1" class="z_btn1" onclick="fnChange(1)">销量</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
        <div id="btn2" class="z_btn2" onclick="fnChange(2)">采购</div>
       <span style="height:1px; background-color:#bbbbbb; margin-top:31px; overflow:hidden; display:block;"></span>
    </div>
    <span style="height:12px; overflow:hidden; display:block; clear:both; "></span>
    <div id="pnl1" style="width:500px; height:350px; overflow-y:;">
        <div style="padding: 10px 0; padding-top:0px; clear: both;">
            <table style="width: 100%">
                <tr>
                    <td style="font-weight: bold; padding-left: 25px; margin-top: 0px; width: 50%; text-align: left">
                        <div id="lblUnitName" runat="server"></div>
                    </td>
                    <td style="font-weight: bold; padding-right: 25px; margin-top: 10px; width: 50%;
                        text-align: right">
                        <div id="lblDate" runat="server"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="AppLogin" style="background-color: #F7F7F7;">
            <table style="width: 100%">
                <tr>
                    <td style="font-weight: bold; padding-left: 20px; margin-top: 30px; width: 40%; text-align: left; 
                        padding-top:10px;">
                        当日销量
                    </td>
                    <td style="font-weight: bold; padding-left: 35px; margin-top: 10px; 
                        text-align: left; padding-top:10px;">
                        销量前三的商品
                    </td>
                </tr>
                <tr style="height: 140px;">
                    <td style="font-weight: bold; padding-left: 20px; margin-top: 30px;  text-align: left">
                        <span id="lblTotalAmount" runat="server" style="color: #FFA406; font-size: 30px;">1,410.80</span>
                    </td>
                    <td style="">
                        <div id="charts"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="AppHj" style="background-color: #F7F7F7; margin-top:10px; margin-bottom:10px; margin-left:auto; margin-right:auto;
            border:1px solid #a3a3a3; width:96%;">
            <ul>
                <%--<li style="background:#E5E3E0;">
                    <div style="float: left; width: 30%; font-weight:bold;">
                        交易单号
                    </div>
                    <div style="float: left; width: 20%; font-weight:bold;">
                        金额(元)
                    </div>
                    <div style="float: left; width: 20%; font-weight:bold;">
                        状态
                    </div>
                    <div style="float: left; width: 30%; font-weight:bold;">
                        时间
                    </div>
                </li>--%>
                <li style="background:#E5E3E0;">
                    <div style="float: left; width: 30%; font-weight:bold; text-align:center;">
                        订单号
                    </div>
                    <div style="float: left; width: 10%; font-weight:bold; text-align:center;">
                        姓名
                    </div>
                    <div style="float: left; width: 17%; font-weight:bold; text-align:center;">
                        金额
                    </div>
                    <div style="float: left; width: 25%; font-weight:bold; text-align:center;">
                        配送方式
                    </div>
                    <div style="float: left; width: 18%; font-weight:bold; text-align:center;">
                        订单时间
                    </div>
                </li>
                <%=strDiv%>
                <li style="display:none;">
                    <div style="float: left; width: 30%;">
                        AD20130001
                    </div>
                    <div style="float: left; width: 20%;">
                        201.5
                    </div>
                    <div style="float: left; width: 20%;">
                        交易成功
                    </div>
                    <div style="float: left; width: 30%;">
                        2013-06-04 10:29
                    </div>
                </li>
                <li style="display:none;">
                    <div style="float: left; width: 30%;">
                        AD20130002
                    </div>
                    <div style="float: left; width: 20%;">
                        301.5
                    </div>
                    <div style="float: left; width: 20%;">
                        交易成功
                    </div>
                    <div style="float: left; width: 30%;">
                        2013-06-04 10:29
                    </div>
                </li>
                <li style="display:none;">
                    <div style="float: left; width: 30%;">
                        AD20130003
                    </div>
                    <div style="float: left; width: 20%;">
                        401.5
                    </div>
                    <div style="float: left; width: 20%;">
                        交易成功
                    </div>
                    <div style="float: left; width: 30%;">
                        2013-06-04 10:29
                    </div>
                </li>
            </ul>
        </div>
        <div style="float:right; margin-right:30px;">
            <a href="http://bs.aladingyidong.com/Login/LoginManager.aspx" target="_blank">
                <font color="blue">JIT CPOS > </font>
            </a>
        </div>
    </div>
    
    <div id="pnl2" style="width:500px; height:350px; overflow-y:; display:;">
        <div style="padding: 10px 0; padding-top:0px; clear: both;">
            <table style="width: 100%">
                <tr>
                    <td style="font-weight: bold; padding-left: 25px; margin-top: 0px; width: 50%; text-align: left">
                        <div id="lblUnitName2" runat="server"></div>
                    </td>
                    <td style="font-weight: bold; padding-right: 25px; margin-top: 10px; width: 50%;
                        text-align: right">
                        <div id="lblDate2" runat="server"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="AppLogin" style="background-color: #F7F7F7;">
            <table style="width: 100%">
                <tr>
                    <td style="font-weight: bold; padding-left: 20px; margin-top: 30px; width: 40%; text-align: left; 
                        padding-top:10px;">
                        当日采购金额
                    </td>
                    <td style="font-weight: bold; padding-left: 35px; margin-top: 10px; 
                        text-align: left; padding-top:10px;">
                        采购前三的商品
                    </td>
                </tr>
                <tr style="height: 140px;">
                    <td style="font-weight: bold; padding-left: 20px; margin-top: 30px;  text-align: left">
                        <span id="lblTotalAmount2" runat="server" style="color: #FFA406; font-size: 30px;">1,410.80</span>
                    </td>
                    <td style="">
                        <div id="charts2"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="AppHj" style="background-color: #F7F7F7; margin-top:10px; margin-bottom:10px; margin-left:auto; margin-right:auto;
            border:1px solid #a3a3a3; width:96%;">
            <ul>
                <%--<li style="background:#E5E3E0;">
                    <div style="float: left; width: 30%; font-weight:bold;">
                        交易单号
                    </div>
                    <div style="float: left; width: 20%; font-weight:bold;">
                        金额(元)
                    </div>
                    <div style="float: left; width: 20%; font-weight:bold;">
                        状态
                    </div>
                    <div style="float: left; width: 30%; font-weight:bold;">
                        时间
                    </div>
                </li>--%>
                <li style="background:#E5E3E0;">
                    <div style="float: left; width: 30%; font-weight:bold; text-align:center;">
                        订单号
                    </div>
                    <div style="float: left; width: 10%; font-weight:bold; text-align:center;">
                        经销商
                    </div>
                    <div style="float: left; width: 17%; font-weight:bold; text-align:center;">
                        金额
                    </div>
                    <div style="float: left; width: 25%; font-weight:bold; text-align:center;">
                        状态
                    </div>
                    <div style="float: left; width: 18%; font-weight:bold; text-align:center;">
                        订单时间
                    </div>
                </li>
                <%=strDiv2%>
                <li style="display:none;">
                    <div style="float: left; width: 30%;">
                        AD20130001
                    </div>
                    <div style="float: left; width: 20%;">
                        201.5
                    </div>
                    <div style="float: left; width: 20%;">
                        交易成功
                    </div>
                    <div style="float: left; width: 30%;">
                        2013-06-04 10:29
                    </div>
                </li>
                <li style="display:none;">
                    <div style="float: left; width: 30%;">
                        AD20130002
                    </div>
                    <div style="float: left; width: 20%;">
                        301.5
                    </div>
                    <div style="float: left; width: 20%;">
                        交易成功
                    </div>
                    <div style="float: left; width: 30%;">
                        2013-06-04 10:29
                    </div>
                </li>
                <li style="display:none;">
                    <div style="float: left; width: 30%;">
                        AD20130003
                    </div>
                    <div style="float: left; width: 20%;">
                        401.5
                    </div>
                    <div style="float: left; width: 20%;">
                        交易成功
                    </div>
                    <div style="float: left; width: 30%;">
                        2013-06-04 10:29
                    </div>
                </li>
            </ul>
        </div>
        <div style="float:right; margin-right:30px;">
            <a href="http://bs.aladingyidong.com/Login/LoginManager.aspx" target="_blank">
                <font color="blue">JIT CPOS > </font>
            </a>
        </div>
    </div>

</body>
</html>
