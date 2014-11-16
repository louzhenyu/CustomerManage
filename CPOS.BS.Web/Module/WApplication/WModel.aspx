<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>内容管理</title>
    
    
    <script src="/Framework/javascript/Biz/WModel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WApplicationInterface.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>

    <script src="Model/WMenuAddItemVM.js" type="text/javascript"></script>
    <script src="Store/WMenuAddItemVMStore.js" type="text/javascript"></script>
    <script src="View/WModelView.js" type="text/javascript"></script>
    <script src="Controller/WModelCtl.js" type="text/javascript"></script>
    <style type="text/css">
    .contentArea {}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div>
                <div id="tabsMain" style="width:100%; height:70px;"></div>
                <div id="tabInfo" style="height:61px; background:rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb" style="width:700px;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:100px; text-align:right;">微信账号：</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtApplicationId" style="margin-top:5px;font-weight:;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:100px; text-align:right;">模块：</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtWModel" style="margin-top:5px;font-weight:;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                    <div id="btnSearch" style="margin-top:5px;"></div>
                                    <input id="MaterialId" type="hidden" value=""></input>
                                </td>
                                <%--<td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="btnReset" style="margin-top:5px;"></div>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
            <div style="margin-top:10px; ">
                <div id="tabsMain3" style="width:100%; height:370px;"></div>
                <div id="tabInfo3" style="height:351px; background:rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddItem1" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="grid1">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_2" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddItem2" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="grid2">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_3" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddItem3" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="grid3">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_4" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddItem4" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="grid4">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_5" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddItem5" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="grid5">
                        </div>
                    </div>
                </div>
            </div>

            <div style="height:48px; background:rgb(241, 242, 245); border:1px solid #ddd; 
                padding-top:10px; padding-right:10px; margin-top:10px;">
                <div id="btnSave" style="float:left;"></div>
            </div>

        </div>
    </div>

</asp:Content>
