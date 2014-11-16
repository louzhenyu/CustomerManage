<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>资讯内容</title>
    <script src="/Framework/javascript/Biz/NewsType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="Controller/NewsEditCtl.js?v=0.1" type="text/javascript"></script>
    <script src="Model/NewsVM.js" type="text/javascript"></script>
    <script src="Store/NewsEditVMStore.js" type="text/javascript"></script>
    <script src="View/NewsEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
    <script charset="utf-8" language="javascript" type="text/javascript" src="/Framework/Javascript/Biz/CheckboxList.js"></script>
    <script src="../../Framework/Javascript/Biz/LNewsTypeSelectTree.js" type="text/javascript"></script>
    <style type="text/css">
        .uploadPic
        {
            float: left;
            padding-top: 30px;
        }
        .viewImage
        {
            width: 179px;
            height: 100px;
            line-height: 100px;
            margin-right: 30px;
            text-align: center;
            font-size: 16px;
            background: #d0d0d0;
            color: #fff;
            float: left;
        }
        .picItem
        {
            height: 150px;
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
            float: left;
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
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 750px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>资讯类型
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 32px;">
                                    <div id="txtNewsType" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>发布时间
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtPublishTime">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                </td>
                                <td class="z_main_tb_td2" style="">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    设为首页
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="cmbIsDefault" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    设为置顶
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="cmbIsTop">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    作者
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtAuthor">
                                    </div>
                                </td>
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
                                    <font color="red">*</font>资讯标题
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtNewsTitle">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    资讯子标题
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtNewsSubTitle">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    内容链接
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtContentUrl">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <%--  <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    图片链接
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtImageUrl">
                                    </div>
                                    <div style="position: absolute; left: 550px; top: 150px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>--%>
                            <tr class="picItem">
                                <td colspan="6">
                                    <div>
                                        <span style="float: left; line-height: 36px; width: 85px">&nbsp&nbsp 图片链接</span>
                                        <div class="uploadPic">
                                            <p id="image" class="viewImage">
                                                暂无
                                            </p>
                                            <div class="info">
                                                <p class="exp">
                                                    建议上传的文件大小不超过100k的图片</p>
                                                <input type="button" id="uploadImage" class="uplaodbtn" value=" 选择图片 " />
                                                <%--    <span class="uplaodbtn" id="uploadImage">选择图片</span>--%>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    缩略图链接
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtThumbnailImageUrl">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    APPId
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtAPPId">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    标签
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="divTags" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    简介
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="divIntro" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" colspan="1" style="vertical-align: top; line-height: 22px;">
                                    资讯内容
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="div1" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" colspan="6" style="vertical-align: top; line-height: 22px;
                                    margin-top: 10px;">
                                    <div id="txtContent">
                                    </div>
                                </td>
                                <%--<td class="z_main_tb_td2"  style="padding-top: 0px;">
                                    <textarea name="content" style="width:800px;height:400px;visibility:hidden;">KindEditor</textarea>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="divBtn" style="height: 45px">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
