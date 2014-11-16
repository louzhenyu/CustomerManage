<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title></title>
    
    <script src="/Framework/javascript/Biz/UnitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/DeliveryUnit.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>

    <script src="Model/InoutOrderEntity.js" type="text/javascript"></script>
    <script src="View/PosOrderDeliveryView.js" type="text/javascript"></script>
    <script src="Controller/PosOrderDeliveryCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div class="art-tit" style="width:100%; padding:0px; border:1px solid #d0d0d0; height:300px;">
                <div style="width:100%;">
                    <div class="" style="margin:10px;">
                        <div class="z_card_row" style="">
                            <div style="float:left; min-width:100px;text-align:right;">单号：</div>
                            <div style="float:left; min-width:120px;padding-left:10px;">
                                <div id="txtOrderNo" style="line-height:28px;">&nbsp;</div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left; min-width:100px;text-align:right;">单据日期：</div>
                            <div style="float:left; min-width:100px;padding-left:10px;">
                                <div id="txtOrderDate" style="float:left; line-height:28px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left; min-width:100px;text-align:right;">收件人：</div>
                            <div style="float:left; min-width:120px;padding-left:10px;">
                                <div id="txtField3" style="float:left; line-height:28px;">&nbsp;</div>
                            </div>
                            <div style="float:left; min-width:100px;text-align:right;">联系电话：</div>
                            <div style="float:left; min-width:100px;padding-left:10px;">
                                <div id="txtTel" style="float:left; line-height:28px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left; min-width:100px;text-align:right;">详细地址：</div>
                            <div style="float:left; min-width:100px;padding-left:10px;">
                                <div id="txtField6" style="float:left; line-height:28px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left; min-width:100px;text-align:right;">邮编：</div>
                            <div style="float:left; min-width:100px;padding-left:10px;">
                                <div id="txtField5" style="float:left; line-height:28px;"></div>
                            </div>
                        </div>
                        <div class="z_card_row" style="">
                            <div style="float:left; min-width:100px;text-align:right;"><font color="red">*</font>配送商：</div>
                            <div style="float:left; min-width:100px;">
                                <div id="txtCarrierId" style="float:left; line-height:28px; margin-top:5px;"></div>
                            </div>
                            <div style="float:left; min-width:100px;text-align:right;"><font color="red">*</font>配送单号：</div>
                            <div style="float:left; min-width:100px;">
                                <div id="txtField2" style="float:left; line-height:28px; margin-top:5px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                    <div style="height:30px; line-height:30px; padding-top:0px;">
                        <div id="btnSave" style="float:left;"></div>
                        <div id="btnClose" style="float:left;"></div>
                    </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
