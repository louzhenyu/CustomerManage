<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>轮次</title>
    <script src="/Framework/javascript/Biz/EventType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Round.js" type="text/javascript"></script>
    <script src="Controller/RoundEditCtl.js" type="text/javascript"></script>
    <script src="Model/RoundVM.js" type="text/javascript"></script>
    <script src="Store/EventsPrizesListVMStore.js" type="text/javascript"></script>
    <script src="Store/EventsRoundListVMStore.js" type="text/javascript"></script>
    <script src="View/RoundEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 361px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width:100%;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb" style="width:100%;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width:100px; min-width:100px;">
                                    <font color="red">*</font>轮次
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width:100px;">
                                    <div id="txtRound" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; min-width:100px; width:100px;">
                                    <font color="red">*</font>启用
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width:100px;">
                                    <div id="txtStatus" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; min-width:100px; width:100px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; width:100px;">
                                    
                                </td>
                            </tr>
                        </table>
                        <div>
                            <div id="grid"></div>
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
