<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>菜单信息</title>
    
    <script src="/Framework/javascript/Biz/WMenuType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WMaterialType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WMenuSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WModel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>

    <script src="Controller/WMenuEditCtl.js" type="text/javascript"></script>
    <script src="Model/WMenuVM.js" type="text/javascript"></script>
    <script src="Store/WMenuVMStore.js" type="text/javascript"></script>
    <script src="View/WMenuEditView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div id="tabsMain" style="width:100%; height:460px;"></div>
                <div id="tabInfo" style="height:451px; background:rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:120px;">
                                    上级菜单</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtParentId" style="margin-top:5px;"></div>
                                    <div style="position:absolute; top:5px; left:580px;">
                                        <div id="txtReset" style="margin-top:5px;"></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:120px;">
                                    <font color="red">*</font>名称</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtName" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>微信账号</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtWeiXinID"></div>
                                </td>
                            </tr>--%>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>菜单KEY值</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtKey"></div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>类型</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtType"></div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>菜单级别</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtLevel"></div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>序号</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtDisplayColumn"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    模块</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtModelId"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:120px;">
                                    超链接</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtMenuURL" style="margin-top:5px;"></div>
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
