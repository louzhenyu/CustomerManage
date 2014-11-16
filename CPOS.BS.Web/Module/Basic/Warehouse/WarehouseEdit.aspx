<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>仓库信息</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/YesNoStatus.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/basic/Warehouse/Controller/WarehouseEditCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Warehouse/Model/WarehouseVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Warehouse/Model/WarehouseDetailVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Warehouse/Store/WarehouseVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Warehouse/Store/WarehouseEditVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/basic/Warehouse/View/WarehouseEditView.js"%>" type="text/javascript"></script>

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
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>所属单位</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtParentUnit" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>仓库编码</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtWarehouseCode" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>仓库名称</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtWarehouseName" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">英文名称</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtWarehouseEnglish"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">联系人</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtWarehouseContacter"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>电话</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtWarehouseTel"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">传真</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtWarehouseFax"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">默认仓库</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtIsDefaultWarehouse"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">状态</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtWarehouseStatus"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">地址</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;" colspan="5">
                                    <div id="txtAddress"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">备注</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px; padding-top:0px;">
                                    <div id="txtRemark"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">创建人</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtCreateUserName"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">创建时间</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtCreateTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    
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
