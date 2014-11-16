<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>会员清洗</title>
    <script src="<%=StaticUrl+"/module/Vip/VipClear/Controller/VipClearCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipClear/Model/VipClearVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipClear/Store/VipClearStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/module/Vip/VipClear/View/VipClearView.js"%>" type="text/javascript"></script>
      <style type="text/css">
        td {
        vertical-align: middle; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='span_panel'></span>
                </div>
                <div class="view_Search">
                    <span id='span_panel2'></span>
                </div>
            </div>
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
