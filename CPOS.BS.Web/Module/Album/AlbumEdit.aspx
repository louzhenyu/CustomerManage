<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>图片推荐内容</title>
    <script src="/Framework/javascript/Biz/AlbumType.js" type="text/javascript"></script>
    <script src="Controller/AlbumEditCtl.js" type="text/javascript"></script>
    <script src="Model/AlbumVM.js" type="text/javascript"></script>
    <script src="Store/AlbumVMStore.js" type="text/javascript"></script>
    <script src="View/AlbumEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
    <style type="text/css">
        .tr-Image
        {
            height: 150px;
        }
        .imagetit
        {
            float: left;
            width: 110px;
            line-height: 16px;
        }
        .uploadWarp
        {
            float: left;
            margin-top: 30px;
        }
        .viewImage
        {
            float: left;
            width: 179px;
            height: 100px;
            line-height: 100px;
            margin-right: 30px;
            text-align: center;
            font-size: 16px;
            background: #d0d0d0;
            color: #fff;
        }
        .info
        {
            float: left;
            width: 200px;
        }
        .exp
        {
            line-height: 28px;
            font-size: 15px;
            color: #828282;
        }
        .uplaodbtn
        {
            display: block;
            width: 96px;
            height: 30px;
            line-height: 30px;
            margin-top: 15px;
            text-align: center;
            border-radius: 7px;
            background: #b2c7ab center center;
            color: #fff;
            cursor: pointer;
            float: left;
            margin-top: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 600px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width: 850px;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width: 70px;">
                                    <font color="red">*</font>相册类型
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtAlbumType" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td id="fpicture" class="z_main_tb_td" style="display: none; vertical-align: top;
                                    line-height: 32px; width: 70px;">
                                    <font color="red">*</font>相片类型
                                </td>
                                <td id="tPicType" class="z_main_tb_td2" style="padding-top: 0px; display: none">
                                    <div id="txtPicType" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 70px;">
                                    序号
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtSortOrder">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 70px;">
                                    <font color="red">*</font>相册标题
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtTitle">
                                    </div>
                                </td>
                            </tr>
                            <%--          <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 70px;">
                                    <font color="red">*</font>封面图片
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div style="position: relative;">
                                        <div id="txtImageURL" style="overflow: hidden">
                                        </div>
                                        <div style="position: absolute; right: 110px; top: -2px;">
                                            <input type="button" id="uploadImage" value=" 选择图片 " />
                                        </div>
                                    </div>
                                </td>
                            </tr>--%>
                            <tr class="tr-Image">
                                <td colspan="6">
                                    <div class="uploadImageItem">
                                        <span class="imagetit">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<font style="color: Red">*</font>封面图片</span>
                                        <div class="uploadWarp">
                                            <p class="viewImage" id="image">
                                                暂无
                                            </p>
                                            <div class="info">
                                                <p class="exp">
                                                    建议上传的文件大小不超过100k的图片
                                                </p>
                                                <input type="button" id="uploadImage" class="uplaodbtn" value=" 选择图片 " />
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 70px;">
                                    <font color="red">*</font>模块标题
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div style="position: relative;">
                                        <div id="txtModuleName" style="margin-top: 0px; overflow: hidden;">
                                        </div>
                                        <div style="position: absolute; right: 110px; top: -2px;">
                                            <div id="btnCreateLink">
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" id="inputContent" style="display: none;">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 70px;">
                                    视频内容
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px; padding-bottom: 10px;
                                    padding-left: 15px;">
                                    <div id="txtContent">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="divBtn" style="height:45px">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
