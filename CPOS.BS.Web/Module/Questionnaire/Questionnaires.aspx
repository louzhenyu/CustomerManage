<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>问题管理</title>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/EventType.js"%>" type="text/javascript"></script>
  <%--  <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>--%>
    <script src="<%=StaticUrl+"/Module/Questionnaire/Controller/QuestionnairesCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Questionnaire/Model/QuestionnairesVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Questionnaire/Store/QuestionnairesVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Questionnaire/View/QuestionnairesView.js"%>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 44px;">
                    <div id='span_panel' style="float: left; width: ; overflow: hidden;">
                    </div>
                    <div id='btn_panel' style="float: left; width: 200px;">
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
