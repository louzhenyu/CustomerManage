<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>试题内容</title>
    <script src="/Framework/javascript/Biz/QuestionType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventRange.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventCheckinType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="Controller/QuestionEditCtl.js" type="text/javascript"></script>
    <script src="Model/QuestionListVM.js" type="text/javascript"></script>
    <script src="Store/QuestionEditVMStore.js" type="text/javascript"></script>
    <script src="View/QuestionEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0; background:#f00;">
                <div id="tabInfo" style="height: 291px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width:100%;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb" style="width:100%;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>试题类型
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtQuestionType" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>试题描述
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtQuestionDesc" style="margin-top: 0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:100px;">
                                    试题答案
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtQuestionValue" style="margin-top: 0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:100px;">
                                    <font color="red">*</font>最少选中项
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtMinSelected" style="margin-top: -0px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>最多选中项
                                </td>
                                <td class="z_main_tb_td2" style=" ">
                                    <div id="txtMaxSelected" style="margin-top: -0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:100px;">
                                    <font color="red">*</font>问题答案数量
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtQuestionValueCount" style="margin-top: -0px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>是否必填项
                                </td>
                                <td class="z_main_tb_td2" style=" ">
                                    <div id="txtIsRequired" style="margin-top: -0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:100px;">
                                    <font color="red">*</font>试题是否开放
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtIsOpen" style="margin-top: -0px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>是否公共的问题
                                </td>
                                <td class="z_main_tb_td2" style=" ">
                                    <div id="txtIsSaveOutEvent" style="margin-top: -0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:100px;">
                                    <font color="red"></font>cookie存储名字
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtCookieName" style="margin-top: 0px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" style=" ">
                                    <div id="txtDisplayIndex" style="margin-top: 0px;">
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
