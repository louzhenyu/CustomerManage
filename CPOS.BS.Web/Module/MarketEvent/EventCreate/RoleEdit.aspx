<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>角色信息</title>
    
    <script src="/Framework/javascript/Biz/AppSys.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>

    <script src="Controller/RoleEditCtl.js" type="text/javascript"></script>
    <script src="Model/RoleVM.js" type="text/javascript"></script>
    <script src="Model/RoleDetailVM.js" type="text/javascript"></script>
    <script src="Store/RoleVMStore.js" type="text/javascript"></script>
    <script src="View/RoleEditView.js" type="text/javascript"></script>

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
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>应用系统</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtAppSys" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>角色编码</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtRoleCode" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>角色名称</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtRoleName" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">英文名</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtRoleEnglish"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>系统保留</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtIsSys"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">菜单</td>
                                <td class="z_main_tb_td2" style="padding-top:0px; padding-left:15px;" colspan="5">
                                    <div id="treeMenu" style="width:374px; height:270px; border:1px solid #d0d0d0;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; padding-top:10px;">创建人</td>
                                <td class="z_main_tb_td2" style="padding-top:10px;">
                                    <div id="txtCreateUserName"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; padding-top:10px;">创建时间</td>
                                <td class="z_main_tb_td2" style="padding-top:10px;">
                                    <div id="txtCreateTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:10px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">最后修改人</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtModifyUserName"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">最后修改时间</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtModifyTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    
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
