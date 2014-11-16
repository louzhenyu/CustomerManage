<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>业务拜访</title>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EIndustry.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EScale.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipEnterpriseExpandStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UserGender.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EPolicyDecisionRole.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ECCustomerSelect.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesProduct.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesStage.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesChargeVip.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesVisitType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ESalesVisitVip.js" type="text/javascript"></script>
    <script src="Controller/ESalesVisitEditCtl.js" type="text/javascript"></script>
    <script src="Model/EEnterpriseCustomersVM.js" type="text/javascript"></script>
    <script src="Store/EEnterpriseCustomersVMStore.js" type="text/javascript"></script>
    <script src="View/ESalesVisitEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="m10 article">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div id="tabsMain" style="width:100%; height:370px;"></div>

                <div id="tabInfo" style="height:361px; background:rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="padding:0px;">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:150px;"><font color="red">*</font>销售线索</td>
                                <td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="txtSalesName" style="margin-top:5px;"></div>
                                    <input id="hSalesId" type="hidden" value="" />
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"><font color="red">*</font>拜访方式</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtVisitTypeId" style="margin-top:5px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"></td>
                                <td class="z_main_tb_td2" style="">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>所属客户</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtEnterpriseCustomerId" style="margin-top:0px;"></div>
                                    <input id="hEnterpriseCustomerId" type="hidden" value="" />
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>拜访人群</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtSalesVisitVip" style="margin-top:0px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"><font color="red">*</font>拜访记录</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px; padding-top:0px;">
                                    <div id="txtVisitLog"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:13px; padding:0px;padding-top:13px;">业务阶段变更</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    <div id="txtStageId" style="margin-top:10px;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                                    
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;"></td>
                                <td class="z_main_tb_td2" style="padding-top:0px;">
                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div id="tabImage" style="height:1px; overflow:hidden;">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <div id="pnlImage" style="height:315px; overflow:auto;">
                            <div style="width:300px; float:left; padding-top:5px;">
                                <div id="gridImage"></div>
                            </div>
                            <div style="width:250px; padding-left:10px; padding-right:10px; float:left;">
                                <div style="height:5px;"></div>
                                <table class="z_main_tb" style="width:100%;">
                                    <tr class="">
                                        <td style="width:40px;">
                                            <font color="red">*</font>附加文件
                                        </td>
                                        <td>
                                            <div id="txtImage_UploadStatus" style="color:blue;"></div>
                                        </td>
                                    </tr>
                                    <tr class="z_main_tb_tr">
                                        <td colspan="2">
                                            <input type="hidden" id="hImage_DownloadUrl" />
                                            <div id="spanOpenUpload" style="width:295px; overflow-x:hidden; background:;">
                                                <div id="spanUpload" style="float:left;"></div>
                                                <div id="spanUploadButton" style="float:left; margin-top:10px; border-bottom:1px solid #fff;"></div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="">
                                        <td style="width:40px;">
                                            <font color="red">*</font>附件名
                                        </td>
                                        <td>
                                            <div id="txtImage_DownloadName" style="margin-top:10px;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <font color="red">*</font>排序
                                        </td>
                                        <td>
                                            <div id="txtImage_DisplayIndex" style="margin-top:10px;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top:10px;">
                                            <div id="btnAddImageUrl"></div>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
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
