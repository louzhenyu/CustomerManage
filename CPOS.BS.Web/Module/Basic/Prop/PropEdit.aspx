<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>属性信息</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropDomain.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropInputFlag.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/PropSelectTree.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/Module/basic/prop/Controller/PropEditCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Model/PropVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/Store/PropVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/basic/prop/View/PropEditView.js"%>" type="text/javascript"></script>

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
                                    <font color="red">*</font>属性域</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtDomain" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:120px;">
                                    上级节点</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px;">
                                    <div id="txtParentId" style="margin-top:0px;"></div>
                                    <div style="position:absolute; top:35px; left:580px;">
                                        <div id="txtReset" style="margin-top:5px;"></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px; width:120px;">
                                    <font color="red">*</font>名称</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:22px;">
                                    <div id="txtName" style="margin-top:0px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>代码</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtCode"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>类型</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtType"></div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>级别</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtLevel"></div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    输入类型</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtInputFlag"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    缺省值</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtDefaultValue"></div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    最大长度</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtMaxLength"></div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                    <font color="red">*</font>序号</td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top:0px;">
                                    <div id="txtDisplayIndex"></div>
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
