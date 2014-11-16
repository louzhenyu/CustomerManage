<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>人员内容</title>
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
    <script src="Controller/VipEnterpriseEditCtl.js" type="text/javascript"></script>
    <script src="Model/EEnterpriseCustomersVM.js" type="text/javascript"></script>
    <script src="Store/EEnterpriseCustomersVMStore.js" type="text/javascript"></script>
    <script src="View/VipEnterpriseEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 451px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width:700px;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width:120px;">
                                    <font color="red">*</font>姓名
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtName" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>性别
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtGender" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    <font color="red">*</font>所属客户
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtEnterpriseCustomerId">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>部门
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtDepartment" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>职务
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtPosition" style=""></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>联系电话
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtPhone" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    传真
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtFax" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    电邮
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtEmail" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    状态
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtStatus" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>决策作用
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtPDRoleId" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    个人描述<br />（如兴趣爱好等）
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtRemark">
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
    
    <div style="position:absolute; left: 262px; top:54px;">
        <img src="../../Framework/Image/search.png" style="height:22px;width:22px; cursor:pointer;"
            onclick="fnECShowSearch()" />
        <input id="hECCustomerId" type="hidden" value="" />
    </div>
    <div id="cusECSearch" style="border:1px solid #666; width:400px; height:320px; display:none;
        position:absolute; left: 157px; top:80px; z-index:10000; background:#fff;">
        <div style="background:#1b8cf2; color:#fff; height:30px; line-height:30px; padding-left:10px; font-weight:bold;">
            <div style="float:left;width:200px;">搜索客户</div>
            <div style="float:right;width:30px; padding-top:3px;">
                <img src="../../Framework/Image/close.png" style="height:24px;width:24px; cursor:pointer;"
                    onclick="fnECCloseSearch()" />
            </div>
        </div>
        <div style="height: 40px; padding-top:10px;">
            <div style="float:left; width:190px;">
                <div id="tbECSearchCustomerName"></div>
            </div>
            <div style="float:left; width:70px;">
                <div id="tbECSearchCustomerGo"></div>
            </div>
            <div style="float:left; width:70px; padding-left:10px;">
                <div id="tbECSearchCustomerClear"></div>
            </div>
        </div>
        <div style="height:20px; padding-left:10px; color:#d0d0d0; clear:both; width:350px;">
            可使用"*"作为通配符跟在其它字符后面以提高搜索效率。
        </div>
        <div id="pnlECSearchCustomer" style="height:200px; clear:both; width:380px; height:200px; margin:10px; overflow:auto;">
        </div>
    </div>

</asp:Content>
