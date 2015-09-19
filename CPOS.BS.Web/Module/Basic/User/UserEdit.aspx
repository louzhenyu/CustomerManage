<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>用户信息</title>

    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UserGender.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/user/Controller/UserEditCtl.js?v=1.0"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Model/UserVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Model/UserDetailVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Store/UserVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/Store/UserEditVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/user/View/UserEditView.js"%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
                <div id="tabsMain" style="width: 100%; height: 460px;"></div>
                <div id="tabInfo" style="height: 451px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
                        <div style="height: 5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;"><font color="red">*</font>姓名</td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 32px;">
                                    <div id="txtUserName" style="margin-top: 5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;"><font color="red">*</font>工号/登录名</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtUserCode" style="margin-top: 5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">性别</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtUserGender" style="margin-top: 5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">英文名</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUserEnglish"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">身份证号</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUserIdentity"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">生日</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUserBirthday"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"><font color="red">*</font>密码</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUserPwd"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"><font color="red">*</font>有效日期</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtFailDate"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">固定电话</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCellPhone"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"><font color="red">*</font>手机</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtTelPhone"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"> </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                <div style="display:none">  <font color="red">*</font>邮箱 <div id="txtEmail"></div></div> 
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                   <div style="display:none">  QQ   <div id="txtQQ"></div></div> 
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">MSN</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="3">
                                    <div id="txtMSN"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;"></td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">Blog</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="3">
                                    <div id="txtBlog"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;"></td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">地址</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="3">
                                    <div id="txtAddress"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">邮编</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtPostcode"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">备注</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="5">
                                    <div id="txtRemark"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">创建人</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCreateUserName"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">创建时间</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCreateTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;"></td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">最后修改人</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtModifyUserName"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">最后修改时间</td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtModifyTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div id="tabRole" style="height: 1px; overflow: hidden;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;"></div>

                        <div class="art-titbutton">
                            <div class="view_Button">
                                <span id="btnRoleCreate"></span>
                            </div>
                        </div>
                        <div id="gridRoleView"></div>

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
