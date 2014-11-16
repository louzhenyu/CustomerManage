<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>支付管理</title>
    <script src="Controller/PayMentCtl.js" type="text/javascript"></script>
    <script src="View/PayMentView.js" type="text/javascript"></script>
    <style>
        .weChatTitle
        {
            width: 100%;
            height: 60px;
            line-height: 60px;
            padding: 0px 25px;
            border-bottom: 1px solid #d0cedc;
            background: #f6f6f7;
        }
        .weChatMenuManage
        {
            float: left;
            margin: 15px;
            border: 1px solid #cecedc;
            border-radius: 4px;
        }
        .z_main_tb_td
        {
            width: 220px;
            line-height: 32px;
            text-align: right;
            padding-left: 5px;
            padding-right: 5px;
        }
        .z_main_tb_td1
        {
            width: 150px;
            line-height: 32px;
            text-align: right;
            padding-left: 5px;
            padding-right: 5px;
        }
        .transparentBut
        {
            width: 120px;
            height: 32.8px;
            background: rgb(196, 224, 224);
        }
        .left
        {
            width: 300px;
            float: left;
        }
        .right
        {
            margin-left: 100px;
            float: left;
        }
        .wapclass
        {
            float: left;
            min-width: 115px;
            width: auto;
            _width: 115px;
            height: 36px;
            line-height: 36px;
            font-size: 15px;
            font-weight: bold;
            color: #828A96;
        }
        .rbclass
        {
            font-size: 12px;
            color: #828A96;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE HTML>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0"
            name="viewport">
    </head>
    <div class="weChatTitle">
        <span style="font-size: 18px; font-weight: bold; color: #666;">后台支付管理</span>
    </div>
    <div class="weChatMenuManage" style="padding-left: 15%; padding-right: 15%">
        <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
            <div id="tabInfo" style="height: 240px; background: rgb(241, 242, 245);">
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <h2 style="font-size: 15px; font-weight: bold; color: #666; padding-left: 15px">
                        支付宝WAP支付</h2>
                </div>
                <div style="float: left; width: 230px">
                    <div id="rbwapNo" style="padding-top: 5px">
                    </div>
                    <div id="rbwapMarketing" style="padding-top: 5px">
                    </div>
                    <div id="rbwapCustom" style="padding-top: 5px">
                    </div>
                </div>
                <div style="float: left">
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <%--  <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                            </td>--%>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 32px;">
                                <div class="wapclass">
                                    <font color="red">*</font>账号
                                </div>
                            </td>
                            <td style="">
                                <div id="txtwapBank" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <%--<td class="z_main_tb_td" style="padding-top: 0px;">
                            </td>--%>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
                                    卖家淘宝账号
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwaptbBack">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <%--  <td class="z_main_tb_td" style="padding-top: 0px;">
                            </td>--%>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
                                    <font color="red">*</font>支付宝公钥
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwapPublic">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <%-- <td style="padding-top: 0px;">
                                <div id="div">
                                </div>
                            </td>--%>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
                                    <font color="red">*</font>私钥
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwapPrivate">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
            <div id="Div3" style="height: 170px;">
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <h2 style="font-size: 15px; font-weight: bold; color: #666; padding-left: 15px">
                        支付宝线下支付</h2>
                </div>
                <div style="float: left; width: 230px">
                    <div id="rblineNo" style="padding-top: 5px">
                    </div>
                    <div id="rblineMarketing" style="padding-top: 5px">
                    </div>
                    <div id="rblineCustom" style="padding-top: 5px">
                    </div>
                </div>
                <div style="float: left">
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <%-- <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                            </td>--%>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 32px;">
                                <div class="lineclass">
                                    <font color="red">*</font>账号
                                </div>
                            </td>
                            <td style="">
                                <div id="txtlineBank" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <%--<td class="z_main_tb_td" style="padding-top: 0px;">
                            </td>--%>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="lineclass">
                                    <font color="red">*</font>秘钥
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtlinePrivate">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <%-- <td class="z_main_tb_td" style="padding-top: 0px;">
                            </td>--%>
                            <td style="vertical-align: top; line-height: 22px;">
                            </td>
                            <td style="padding-top: 0px;">
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
            <div id="Div1" style="height: 200px; background: rgb(241, 242, 245);">
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <h2 style="font-size: 15px; font-weight: bold; color: #666; padding-left: 15px">
                        银联语音支付</h2>
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                <div id="rbCupNo" style="margin-top: 5px;">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 32px;">
                                <div class="CupClass">
                                    <font color="red">*</font>账号ID
                                </div>
                            </td>
                            <td style="">
                                <div id="txtCupBackID" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="padding-top: 0px;">
                                <div id="rbCupMarketing">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="CupClass">
                                    <font color="red">*</font>加密证书
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupEnCryption">
                                </div>
                                <div style="position: absolute; left: 950px; top: 540px;">
                                    <div id="btnOpenUpload">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="z_main_tb_td1" colspan="3" style="vertical-align: top; line-height: 22px;">
                                <div id="spanOpenUpload" style="display: block; width: 500px">
                                    <div id="spanUpload" style="float: left;">
                                    </div>
                                    <div id="spanUploadButton" style="float: left; margin-top: 10px; border-bottom: 1px solid #fff;">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="padding-top: 0px;">
                                <div id="rbCupCustom">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="CupClass">
                                    <font color="red">*</font>加密密码
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupPassword">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td style="padding-top: 0px;">
                                <div id="div9">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="CupClass">
                                    <font color="red">*</font>解密证书
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupDecryption">
                                </div>
                                <div style="position: absolute; left: 950px; top: 600px;" id="absolute1">
                                    <div id="btnOpenUpload1">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="z_main_tb_td1" colspan="3" style="vertical-align: top; line-height: 22px;">
                                <div id="spanOpenUpload1" style="display: block; width: 500px">
                                    <div id="spanUpload1" style="float: left;">
                                    </div>
                                    <div id="spanUploadButton1" style="float: left; margin-top: 10px; border-bottom: 1px solid #fff;">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td style="padding-top: 0px;">
                                <div id="div11">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="CupClass">
                                    <font color="red">*</font>解密密码
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupDecryptionPassword">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
            <div id="Div2" style="height: 200px;">
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <h2 style="font-size: 15px; font-weight: bold; color: #666; padding-left: 15px">
                        银联网页支付</h2>
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                <div id="rbwebCupNo" style="margin-top: 5px;">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 32px;">
                                <div class="webCupClass">
                                    <font color="red">*</font>账号ID
                                </div>
                            </td>
                            <td style="">
                                <div id="txtwebCupBackID" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="padding-top: 0px;">
                                <div id="rbwebCupMarketing">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="webCupClass">
                                    <font color="red">*</font>加密证书
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwebCupEnCryption">
                                </div>
                                <div style="position: absolute; left: 950px; top: 740px;" id="absolute2">
                                    <div id="btnOpenUpload2">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="z_main_tb_td1" colspan="3" style="vertical-align: top; line-height: 22px;">
                                <div id="spanOpenUpload2" style="display: block; width: 500px">
                                    <div id="spanUpload2" style="float: left;">
                                    </div>
                                    <div id="spanUploadButton2" style="float: left; margin-top: 10px; border-bottom: 1px solid #fff;">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="padding-top: 0px;">
                                <div id="rbwebCupCustom">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="webCupClass">
                                    <font color="red">*</font>加密密码
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwebCupPassword">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td style="padding-top: 0px;">
                                <div id="div12">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="webCupClass">
                                    <font color="red">*</font>解密证书
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwebCupDecryption">
                                </div>
                                <div style="position: absolute; left: 950px; top: 810px;" id="absolute3">
                                    <div id="btnOpenUpload3">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="z_main_tb_td1" colspan="3" style="vertical-align: top; line-height: 22px;">
                                <div id="spanOpenUpload3" style="display: block; width: 500px">
                                    <div id="spanUpload3" style="float: left;">
                                    </div>
                                    <div id="spanUploadButton3" style="float: left; margin-top: 10px; border-bottom: 1px solid #fff;">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td style="padding-top: 0px;">
                                <div id="div14">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="webCupClass">
                                    <font color="red">*</font>解密密码
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtwebCupDecryptionPassword">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
            <div id="Div4" style="height: 200px; background: rgb(241, 242, 245);">
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <h2 style="font-size: 15px; font-weight: bold; color: #666; padding-left: 15px">
                        微信支付</h2>
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                <div id="rbMicroCupNo" style="margin-top: 5px;">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 32px;">
                                <div class="Microclass">
                                    <font color="red">*</font>身份标识
                                </div>
                            </td>
                            <td style="">
                                <div id="txtMicroLendtity" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="padding-top: 0px;">
                                <div id="rbMicroCupMarketing">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="Microclass">
                                    <font color="red">*</font> 公众平台秘钥
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtMricroPublic">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="padding-top: 0px;">
                                <div id="rbMricroCupCustom">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="Microclass">
                                    <font color="red">*</font>财付商户通身份标识别
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtMricroStoreLendtity">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td style="padding-top: 0px;">
                                <div id="div15">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="Microclass">
                                    <font color="red">*</font>财付通商户权限秘钥
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtMricroStoreCompotence">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td style="padding-top: 0px;">
                                <div id="div17">
                                </div>
                            </td>
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="Microclass">
                                    支付加密密码
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtMricroParsword">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="btnSave" style="padding-left: 500px; padding-top: 40px; padding-bottom: 20px">
        </div>
    </div>
</asp:Content>
