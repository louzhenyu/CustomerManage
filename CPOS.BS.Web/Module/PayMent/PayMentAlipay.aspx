<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script src="View/PayMentAlipayView.js" type="text/javascript"></script>
    <script src="Controller/PayMentEditCtl.js" type="text/javascript"></script>
    <style type="text/css">
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
        .wapclass
        {
            min-width: 115px;
            width: auto;
            _width: 115px;
            height: 36px;
            line-height: 36px;
            font-size: 12px;
            font-weight: bold;
            color: #828A96;
        }
        .rbclass
        {
            font-size: 12px;
            color: #828A96;
        }
        .CloseBut
        {
            width: 120px;
            height: 32.8px;
            background: rgb(218, 221, 221);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE HTML>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0"
            name="viewport">
    </head>
    <div class="weChatMenuManage">
        <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
            <div id="Div1" style="height: 200px;">
                <div class="z_detail_tb" style="">
                    <div style="height: 5px;">
                    </div>
                    <h2 style="font-size: 15px; font-weight: bold; color: #666; padding-left: 15px">
                        银联语音支付</h2>
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 32px;">
                                <div class="wapclass">
                                    <font color="red">*</font>账号ID
                                </div>
                            </td>
                            <td style="">
                                <div id="txtCupBackID" style="margin-top: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
                                    <font color="red">*</font>加密证书
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupEnCryption">
                                </div>
                                <div style="position: absolute; left: 420px; top: 80px;">
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
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
                                    <font color="red">*</font>加密密码
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupPassword">
                                </div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
                                    <font color="red">*</font>解密证书
                                </div>
                            </td>
                            <td style="padding-top: 0px;">
                                <div id="txtCupDecryption">
                                </div>
                                <div style="position: absolute; left: 420px; top: 150px;" id="absolute1">
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
                            <td class="z_main_tb_td1" style="vertical-align: top; line-height: 22px;">
                                <div class="wapclass">
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
        <%--<div id="btnSave" style="padding-left: 500px; padding-top: 40px; padding-bottom: 20px">
        </div>--%>
    </div>
    <div>
        <div id="btnClose" style="position: absolute; left: 360px; top: 330px;">
        </div>
        <div id="btnSave" style="position: absolute; left: 500px; top: 330px;">
        </div>
    </div>
</asp:Content>
