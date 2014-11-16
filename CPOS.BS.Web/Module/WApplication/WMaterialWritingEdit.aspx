﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>信息</title>
    
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>

    <script src="Controller/WMaterialWritingEditCtl.js" type="text/javascript"></script>
    <script src="Model/WMaterialWritingVM.js" type="text/javascript"></script>
    <script src="Store/WMaterialWritingVMStore.js" type="text/javascript"></script>
    <script src="View/WMaterialWritingEditView.js" type="text/javascript"></script>
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
                    <div class="z_detail_tb" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:80px;">
                                    <font color="red">*</font>文本</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtContent" style="margin-top:5px;"></div>
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
