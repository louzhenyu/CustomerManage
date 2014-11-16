<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>客户管理</title>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EIndustry.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EScale.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerSource.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EEnterpriseCustomerStatus.js" type="text/javascript"></script>
    <script src="Controller/EEnterpriseCustomersCtl.js" type="text/javascript"></script>
    <script src="Model/EEnterpriseCustomersVM.js" type="text/javascript"></script>
    <script src="Store/EEnterpriseCustomersVMStore.js" type="text/javascript"></script>
    <script src="View/EEnterpriseCustomersView.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 86px;">
                    <div id='span_panel' style="float:left; width: 820px; overflow: hidden;">
                    </div>
                    <div id='btn_panel2' style="float:left; width: 200px;  ">
                    </div>
                    <div id='btn_panel' style="clear:both; width: 220px;">
                    </div>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
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
