<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>积分规则信息</title>
    
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SysIntegralSource.js" type="text/javascript"></script>

    <script src="Model/IntegralRuleVM.js" type="text/javascript"></script>
    <script src="Store/IntegralRuleVMStore.js" type="text/javascript"></script>
    <script src="View/IntegralRuleEditView.js" type="text/javascript"></script>
    <script src="Controller/IntegralRuleEditCtl.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="section">
        <div class="">
            <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                
                <div id="tabProp" style="height:451px; background:rgb(241, 242, 245);">
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>积分变动行为
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtIntegralSourceID" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    <font color="red">*</font>积分公式
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtIntegral" style="margin-top: 0px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>有效日期
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtBeginDate" style="float:left;"></div>
                                    <div style="float:left;">至</div>
                                    <div id="txtEndDate" style="float:left;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    积分说明
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtIntegralDesc" style="margin-top: -10px;">
                                    </div>
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
