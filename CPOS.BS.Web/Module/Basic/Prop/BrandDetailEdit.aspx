<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.Module.Basic.Item.ItemEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>属性信息</title>
    <%--<script src="/Framework/javascript/Biz/ItemCategorySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ItemPriceType.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/examples/jquery.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/kindeditor.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"%>"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Controller/BrandDetailEditCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Model/PropVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Store/PropVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/View/BrandDetailEditView.js"%>" type="text/javascript"></script>
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
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
                <div id="tabsMain" style="width: 100%; height: 571px;">
                </div>
                <div id="tabInfo" style="height: 571px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>名称
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 32px;">
                                    <div id="txtBrandName" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>代码
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtBrandCode" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                </td>
                                <td class="z_main_tb_td2" style="">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    联系电话
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtTel" style="margin-top: 0px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtDisplayIndex" style="margin-top: 0px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <%--                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">品牌LOGO</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px; padding-top:0px;">
                                    <div id="txtImageUrl"></div>
                                    <div style="position: absolute; left: 440px; top: 73px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
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
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    描述
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 32px;
                                    padding-top: 0px;">
                                    <div id="txtContent">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="tabImage" style="height: 1px; overflow: hidden;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlImage" style="height: 415px; overflow: auto;">
                            <div style="width: 400px; float: left; padding-top: 5px;">
                                <div id="gridImage">
                                </div>
                            </div>
                            <div style="width: 350px; padding-left: 50px; padding-right: 10px; float: left;">
                                <div style="height: 5px;">
                                </div>
                                <table class="z_main_tb">
                                    <tr class="z_main_tb_tr">
                                        <td>
                                            <font color="red">*</font>图片地址
                                        </td>
                                        <td colspan="3">
                                            <div id="txtImage_ImageUrl" style="margin-top: 10px;">
                                            </div>
                                        </td>
                                        <td colspan="2">
                                            <div style="float: ; width: 80px; line-height: 32px; margin-left: 15px;">
                                                <input type="button" id="uploadImage2" value="选择图片" style="margin-left: 20px;" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <font color="red">*</font>排序
                                        </td>
                                        <td colspan="3">
                                            <div id="txtImage_DisplayIndex" style="margin-top: 10px;">
                                            </div>
                                        </td>
                                        <td colspan="2">
                                            <div>
                                                <div id="btnAddImageUrl">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
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
