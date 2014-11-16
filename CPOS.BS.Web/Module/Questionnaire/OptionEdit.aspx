<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>试题内容</title>
    <script src="/Framework/javascript/Biz/OptionType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventRange.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventCheckinType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="Controller/OptionEditCtl.js" type="text/javascript"></script>
    <script src="Model/OptionListVM.js" type="text/javascript"></script>
    <script src="Store/OptionEditVMStore.js" type="text/javascript"></script>
    <script src="View/OptionEditView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0; background:#f00;">
                <div id="tabInfo" style="height: 291px; background: rgb(241, 242, 245);">
                    <div class="" style="width:100%;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb" style="width:100%;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>文字描述
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtOptionDesc" style="margin-top: 0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:100px;">
                                    <font color="red">*</font>是否选中
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtIsSelect" style="margin-top: -0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>排序
                                </td>
                                <td class="z_main_tb_td2" style=" ">
                                    <div id="txtDisplayIndex" style="margin-top: -0px;">
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
