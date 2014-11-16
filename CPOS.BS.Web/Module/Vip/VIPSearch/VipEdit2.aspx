<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员信息</title>
    
    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipLevel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Tags.js" type="text/javascript"></script>

    <script src="Model/VipVM.js" type="text/javascript"></script>
    <%--<script src="/Module/Order/InoutOrders/Model/InoutOrderEntity.js" type="text/javascript"></script>--%>
    <script src="Store/VipVMStore.js" type="text/javascript"></script>
    <script src="Store/VipEditVMStore.js" type="text/javascript"></script>
    <script src="View/VipEdit2View.js" type="text/javascript"></script>
    <script src="Controller/VipEdit2Ctl.js" type="text/javascript"></script>

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
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">会员号码</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtVipCode" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">会员名称</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtVipName" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">手机</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtPhone" style="margin-top:5px;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">来源</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtVipSource"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">会员等级</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtVipLevel"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">有效积分</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtIntegration"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">注册时间</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtRegistrationTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">最近消费时间</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtRecentlySalesTime"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">状态</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtVipStatus"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div id="tabProp" style="height:1px; overflow:hidden;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                                <div style="float:left;width:100px;text-align:center;"><font color="red">*</font>手动调整积分</div>
                                <div id="txtChangeIntegral" style="float:left;"></div>
                                <div style="float:left;width:100px;text-align:center;"><font color="red">*</font>积分变动原因</div>
                                <div id="txtChangeIntegralRemark" style="float:left;"></div>
                                <div id="txtChangeIntegralAdd" style="float:left;"></div>
                            </div>
                        </div>
                        <div id="gridVipIntegralDetailView"></div>
                    </div>
                </div>
                
                <div id="tabProp2" style="height:1px; overflow:hidden;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                            </div>
                        </div>
                        <div id="gridPosOrderListView"></div>
                    </div>
                </div>
                
                <div id="tabProp3" style="height:1px; overflow:hidden;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                            </div>
                        </div>
                        <div id="gridNextLevelUserListView"></div>
                    </div>
                </div>
                 <div id="tabProp4" style="height:1px; overflow:hidden;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                            </div>
                        </div>
                        <div id="gridCollectionPropertyView"></div>
                    </div>
                </div>
                
                <div id="tabProp5" style="height:1px; overflow:hidden;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button" style="display:none;">
                                <div style="float:left;width:60px;text-align:center;">标签</div>
                                <div id="txtTags" style="float:left;"></div>
                                <div id="txtTagsAdd" style="float:left;"></div>
                            </div>
                        </div>
                        <div id="gridVipTagsView"></div>
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
