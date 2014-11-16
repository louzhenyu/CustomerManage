<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>视频内容</title>
    <script src="/Framework/javascript/Biz/NewsType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <script src="Controller/LEventsAlbumEditCtl.js" type="text/javascript"></script>
    <%--    <script src="Model/LEventsAlbumVM.js" type="text/javascript"></script>
    <script src="Store/LEventsAlbumEditVMStore.js" type="text/javascript"></script>--%>
    <script src="View/LEventsAlbumEditView.js" type="text/javascript"></script>
    <script src="Store/LEventsAlbumEditVMStore.js" type="text/javascript"></script>
    <script src="Model/LEventsAlbumVM.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
    <script charset="utf-8" language="javascript" type="text/javascript" src="/Framework/Javascript/Biz/CheckboxList.js"></script>
    <script language="javascript" type="text/javascript">
   
    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 290px;">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 300px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>视频类型
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtNewsType">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtSortOrder" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>视频标题
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
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    图片链接
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtImageUrl">
                                    </div>
                                    <div style="position: absolute; left: 550px; top: 80px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    选择视频
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtfilepath">
                                    </div>
                                    <div style="position: absolute; left: 550px; top: 110px;">
                                        <div id="btnVideo">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    视频地址
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtVideoUrl">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtSortOrder" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    简介
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="divIntro" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" colspan="6" style="vertical-align: top; line-height: 22px;
                                    margin-top: 10px;">
                                    <div id="txtContent">
                                    </div>
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
