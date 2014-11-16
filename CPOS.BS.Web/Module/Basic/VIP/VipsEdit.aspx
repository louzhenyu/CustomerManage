<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员信息</title>
    
    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>

    <script src="Controller/TagsEditCtl.js" type="text/javascript"></script>
    <script src="Model/TagsVM.js" type="text/javascript"></script>
    <script src="Model/TagsDetailVM.js" type="text/javascript"></script>
    <script src="Store/TagsVMStore.js" type="text/javascript"></script>
    <script src="Store/TagsEditVMStore.js" type="text/javascript"></script>
    <script src="View/TagsEditView.js" type="text/javascript"></script>

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
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                    <font color="red">*</font>会员名称</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtTagsName" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>会员描述</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtTagsDesc"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>会员公式</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtTagsFormula"></div>
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
