<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>信息</title>
    
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>

    <script src="Controller/WMaterialVoiceEditCtl.js" type="text/javascript"></script>
    <script src="Model/WMaterialVoiceVM.js" type="text/javascript"></script>
    <script src="Store/WMaterialVoiceVMStore.js" type="text/javascript"></script>
    <script src="View/WMaterialVoiceEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div id="tabsMain" style="width:100%; height:400px;"></div>
                <div id="tabInfo" style="height:671px; background:rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width:100%;">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:80px;">
                                    <font color="red">*</font>名称</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px; width:600px;">
                                    <div id="txtVoiceName" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:80px;">
                                    <font color="red">*</font>链接地址</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtVoiceUrl"></div>
                                    <div style="position: absolute; left: 544px; top: 41px;">
                                        <div id="btnOpenUpload"></div>
                                    </div>
                                    <div id="spanOpenUpload" style="width:535px; overflow-x:hidden;">
                                        <div id="spanUpload" style="float:left;"></div>
                                        <div id="spanUploadButton" style="float:left; margin-top:10px; border-bottom:1px solid #fff;"></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:80px;">
                                    大小</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtVoiceSize" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:80px;">
                                    格式</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtVoiceFormat" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none;">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:80px;">
                                    申请接口</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtApplicationId"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>

            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
