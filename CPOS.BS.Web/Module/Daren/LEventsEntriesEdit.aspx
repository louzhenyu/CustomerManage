<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>内容</title>
    <script src="/Framework/javascript/Biz/NewsType.js" type="text/javascript"></script>
    <script src="Controller/LEventsEntriesEditCtl.js" type="text/javascript"></script>
    <script src="Model/LEventsEntriesVM.js" type="text/javascript"></script>
    <script src="Store/LEventsEntriesVMStore.js" type="text/javascript"></script>
    <script src="View/LEventsEntriesEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 451px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>作品名称
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWorkTitle" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>作品日期
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWorkDate" style="margin-top: 0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>作者名称
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtCreative">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    手机号码
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtPhone">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    作者地址
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCreativeAddress">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    序号
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtDisplayIndex">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>作品图片
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtImageUrl">
                                    </div>
                                    <div style="position: absolute; left: 570px; top: 213px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; text-align:left; padding-left:15px;">
                                    <img id="imgPre" style="max-width:300px; max-height:200px; display:none;" />
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
