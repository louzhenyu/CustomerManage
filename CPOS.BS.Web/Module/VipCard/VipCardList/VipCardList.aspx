<%@ Page Title="会员卡列表" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true"
Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/javascript/Biz/VipCardGrade.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipCardType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/VipCardStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>

   <script src="Controller/VipCardListCtl.js" type="text/javascript"></script>
    <script src="Model/VipCardVM.js" type="text/javascript"></script>
    <script src="Store/VipCardListStore.js" type="text/javascript"></script>
    <script src="View/VipCardListView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height:44px;">
                    <div id='span_panel' style="float:left; width:820px; overflow:hidden;">
                      <%--<span id='span_panel'>会员卡查询</span>--%>
                      <%-- <table class="z_main_tb" style=" border:2px;">
                        <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td" >
                                会员卡卡号
                            </td>
                            <td class="z_main_tb_td2" >
                                <div id="txtVipCardCode" ></div>
                            </td>
                            <td class="z_main_tb_td" >
                                会员姓名（昵称）
                            </td>
                            <td class="z_main_tb_td2">
                                <div id="txtVipName" ></div>
                            </td>
                        </tr>
                       </table>--%>
                    </div>
                    <div id='btn_panel' style="float:left; width:200px;"></div>
                </div>

            </div>
            <div class="art-titbutton" >
                <div class="view_Button" >
                    <%--<span id='span_create'><H3>会员卡列表</H3></span>--%>
                    <span id='span_create'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>
