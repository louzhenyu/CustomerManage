<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>会员信息</title>
    
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/YesNoStatus.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipLevel.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipSource.js"%>" type="text/javascript"></script>
    <%--<script src="/Framework/javascript/Biz/VipStatus.js" type="text/javascript"></script>--%>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/SkuPropCfg.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Tags.js"%>" type="text/javascript"></script>

    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Model/VipVM.js"%>" type="text/javascript"></script>
    <%--<script src="/Module/Order/InoutOrders/Model/InoutOrderEntity.js" type="text/javascript"></script>--%>
    <%--<script src="Store/VipVMStore.js" type="text/javascript"></script>--%>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Store/VipEditVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/View/VipEditView.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipSearch/Controller/VipEditCtl.js"%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="">
        <div class="">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                
                <div id="tabProp" style="height:451px; background:rgb(241, 242, 245); display:none;">
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
                
                <div id="tabProp2" style="height:451px; background:rgb(241, 242, 245); display:none;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                            </div>
                        </div>
                        <div id="gridPosOrderListView"></div>
                    </div>
                </div>
                
                <div id="tabProp3" style="height:451px; background:rgb(241, 242, 245); display:none;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                            </div>
                        </div>
                        <div id="gridNextLevelUserListView"></div>
                    </div>
                </div>

                 <div id="tabProp4" style="height:451px; background:rgb(241, 242, 245); display:none;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div class="art-titbutton">
                            <div class="view_Button">
                            </div>
                        </div>
                        <div id="gridCollectionPropertyView"></div>
                    </div>
                </div>
                
                <div id="tabProp5" style="height:451px; background:rgb(241, 242, 245); display:none;">
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

        </div>
    </div>
</asp:Content>
