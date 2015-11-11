<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>职务</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/AppSys.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/YesNoStatus.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SupplierUnit.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Role.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/user/Controller/UserEditRoleCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Model/UserVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Model/UserDetailVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Store/UserEditVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/View/UserEditRoleView.js"%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">

            <div style="width:100%; padding:10px; padding-bottom:0px; 
                background:rgb(241, 242, 245); border:1px solid #d0d0d0;">
                <div style="width:100%; padding-left:10px; padding-right:10px;">
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:100px;"><font color="red">*</font>应用系统</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:200px;">
                                <div id="txtAppSys" style="margin-top:0px;"></div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>角色</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtRole" style="margin-top:0px;"></div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>单位</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtUnit" style="margin-top:0px;"></div>
                            </td>
                        </tr>
                        <tr class="z_main_tb_tr" >
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>缺省标志</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtDefaultFlag" style="margin-top:0px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="width:200px; line-height:22px; margin:10px;">
                <div style="float:left; width:80px; line-height:32px; margin-left:0px;">
                    <div id="btnSave" class="button" style="float:left; padding:0px;"></div>
                </div>
                <div style="float:left; width:80px; line-height:32px; margin-left:10px;">
                    <div id="btnClose" class="button" style="float:left; margin-left:0px; padding:0px;"></div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
