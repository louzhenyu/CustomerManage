<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" AutoEventWireup="true" 
Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title></title>
    
    <script src="/Framework/javascript/Biz/WMenuType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WMaterialType.js" type="text/javascript"></script>

    <script src="Controller/WMenuAddItemCtl.js" type="text/javascript"></script>
    <script src="Model/WMenuAddItemVM.js" type="text/javascript"></script>
    <script src="Store/WMenuAddItemVMStore.js" type="text/javascript"></script>
    <script src="View/WMenuAddItemView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">

            <div style="width:100%; padding:10px; padding-bottom:0px; 
                background:rgb(241, 242, 245); border:1px solid #d0d0d0;">
                <div style="width:100%; padding-left:10px; padding-right:10px;">
                    <table class="z_main_tb">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">
                                素材类型</td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtMaterialTypeId" style="margin-top:0px;"></div>
                            </td>
                            <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="span_create" style="margin-top:0px;"></div>
                            </td>
                        </tr>
                        <%--<tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">商品名称</td>
                            <td class="z_main_tb_td2" colspan="3" style="vertical-align:top; line-height:32px; width:auto;">
                                <div id="txtItemName"></div>
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </div>
            <div class="DivGridView" id="txtDiv" style="width:100%; height:250px;">
            </div>
            <div class="DivGridView" id="DivGridView1" style="width:100%; height:250px;">
            </div>
            <div class="DivGridView" id="DivGridView2">
            </div>
            <div class="DivGridView" id="DivGridView3">
            </div>
            <div class="DivGridView" id="DivGridView4">
            </div>
            <div class="DivGridView" id="DivGridView5">
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
