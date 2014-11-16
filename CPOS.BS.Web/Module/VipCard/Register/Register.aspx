<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>会员卡管理_发卡</title>
    <script src="Controller/RegisterCtl.js" type="text/javascript"></script>
    <script src="Model/RegisterVM.js" type="text/javascript"></script>
    <script src="Store/RegisterVMStore.js" type="text/javascript"></script>
    <script src="View/RegisterView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                会员查询</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;">
                <div id="view_Search" class="view_Search" style="height: 48px;">
                    <div id='span_panel' style="float: left; overflow: hidden;">
                    </div>
                    <div id='btn_panel' style="float: right; width: 200px;">
                    </div>
                </div>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                会员基本信息</div>
            <div class="z_event_border" style="padding-left: 0px; border-top: 0px;">
                <table class="z_main_tb">
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td" style="">
                            会员姓名：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labVipName">
                                会员姓名</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            联系电话：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labPhone">
                                联系电话</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            会员性别：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labGender">
                                会员性别</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            生日：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labBirthday">
                                生日</div>
                        </td>
                    </tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td" style="">
                            Email：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labEmail">
                                Email</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            QQ：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labQq">
                                QQ</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            新浪微博：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labSinaMBlog">
                                新浪微博</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            腾讯微博：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labTencentMBlog">
                                腾讯微博</div>
                        </td>
                    </tr>
                    <tr class="z_main_tb_tr">
                        <td class="z_main_tb_td" style="">
                            注册时间：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labRegistrationTime">
                                注册时间</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            会员来源：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labVipSourceId">
                                会员来源</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                            积分：
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labIntegration">
                                积分</div>
                        </td>
                        <td class="z_main_tb_td" style="">
                        </td>
                        <td class="z_main_tb_td2" style="width: 150px;">
                            <div id="labVIPID" style="display: none">
                                会员ID</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                会员卡信息
                <div id="btnAddVipCard" style="float: right; margin-top: 4px; margin-right: 10px;">
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="z_event_border" style="font-weight: bold; height: 36px; line-height: 36px;
                padding-left: 10px; background: rgb(241, 242, 245);">
                车信息
                <div id="btnAddVipExpand" style="float: right; margin-top: 4px; margin-right: 10px;">
                </div>
            </div>
            <div class="DivGridView" id="DivGridView2">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
