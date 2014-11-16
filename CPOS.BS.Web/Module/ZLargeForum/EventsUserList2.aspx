<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动人员</title>
    <script src="Model/EventsVM.js" type="text/javascript"></script>
    <script src="Model/UsersVM.js" type="text/javascript"></script>
    <script src="Store/EventsUserListVMStore.js" type="text/javascript"></script>
    <script src="View/EventsUserListView2.js" type="text/javascript"></script>
    <script src="Controller/EventsUserListCtl2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="DivGridView" id="DivGridView">
                <table id="pnlOptions" class="z_tb2" style="width:100%; margin-bottom:10px;">
                    <tr>
                        <td class="">
                        </td>
                    </tr>
                </table>
                <table id="pnlSearch" class="z_tb2" style="width:100%; margin-bottom:10px;">
                    <tr>
                        <td class="">
                            <div id="btnSearch"></div>
                        </td>
                    </tr>
                </table>
                <table id="pnlUserList" class="z_tb1" style="width:100%;">
                    <tr>
                        <td class="z_tb1_th"></td>
                    </tr>
                </table>
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
        </div>
    </div>

</asp:Content>
