<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>二维码管理信息</title>
    
    <script src="/Framework/javascript/Biz/WQRCodeType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    
    <script src="Controller/WQRCodeManagerEditCtl.js" type="text/javascript"></script>
    <script src="Model/WQRCodeManagerVM.js" type="text/javascript"></script>
    <script src="Model/WQRCodeManagerDetailVM.js" type="text/javascript"></script>
    <script src="Store/WQRCodeManagerVMStore.js" type="text/javascript"></script>
    <script src="View/WQRCodeManagerEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div id="tabsMain" style="width:100%; height:460px;"></div>
                <div id="tabInfo" style="height:427px; background:rgb(241, 242, 245);overflow:auto;">
                    <div class="z_detail_tb" style="width:700px;height:427px;"> 
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>二维码号码</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtQRCode" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>使用状态</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtIsUse" style="margin-top:5px;"></div>

                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"></td>
                                <td class="z_main_tb_td2" style="">


                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:120px;"><font color="red">*</font>微信公众平台</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtApplicationId" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>类型</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtQRCodeTypeId" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"></td>
                                <td class="z_main_tb_td2" style="">

                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">二维码图片</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtImageUrl" style="margin-top:5px;"></div>
                                    <div style="position: absolute; left: 530px; top: 83px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">二维码</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;" colspan="5">
                                    <div id="txtImageUrl" style="float:left;"></div>
                                    <div style=" float:left;">
                                        <div id="btnWXImage"></div>
                                        <%--<a href="" id="wxDownload" onclick="savepic();return false;" style="cursor:hand">点击下载</a> --%>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    图片预览
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; padding-left:14px" colspan="5">
                                    <img id="imgView" alt="" src=""
                                        width="256px" height="256px" />
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">备注</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtRemark" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"></td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    
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
