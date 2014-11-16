<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true" CodeBehind="CallDayPlanningUserDate.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.VisitingPlan.CallDayPlanning.CallDayPlanningUserDate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>

<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>

<script src="Controller/CallDayPlanningUserDateCtl.js" type="text/javascript"></script>
<script src="Model/CallDayPlanningUserDateVM.js" type="text/javascript"></script>
<script src="Store/CallDayPlanningUserDateVMStore.js" type="text/javascript"></script>
<script src="View/CallDayPlanningUserDateView.js" type="text/javascript"></script>

<!--拜访计划编辑页 begin-->
<!--人员-->
<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>

<!--终端-->
<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Channel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Chain.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Category.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/ClientStructure.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/HierarchyItem.js" type="text/javascript"></script>
<link rel="Stylesheet" type="text/css" href="/Lib/Javascript/Ext4.1.0/ux/css/CheckHeader.css" />
<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>

<script src="Controller/CallDayPlanningEditCtl.js" type="text/javascript"></script>
<script src="Model/CallDayPlanningEditVM.js" type="text/javascript"></script>
<script src="Store/CallDayPlanningEditVMStore.js" type="text/javascript"></script>
<script src="View/CallDayPlanningEditView.js" type="text/javascript"></script>
<!--拜访计划编辑页 end-->

<style type="text/css">
       #DivGridView table tr th
       {
         height :40px;
          text-align:center;
          border:1px solid #dbdbdb;
       }
        #DivGridView table tr td
       {
                     height :40px;
          text-align:center;
          border:1px solid #dbdbdb;
       }
        .monthother
        {
           
            color:#dbdbdb;
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
			  <div class="view_Search2">
                <span id='span_panel2'></span>
             </div>
            </div>
            <div class="art-titbutton">
             <div class="view_Button">
                    <span id='span_create'></span>
                    <span id='span_cancel'></span>
               </div>
            </div>
                <div class="DivGridView" id="DivGridView" style="margin:10px;">
                <asp:Calendar runat="server" ID="userCalendar" Width="600" Height="300" OnPreRender="userCalendar_OnPreRender" OnDayRender="userCalendar_DayRender" PrevMonthText="<a href='javascript:;' onclick='fnPreMonth();'>&nbsp;&nbsp;<&nbsp;&nbsp;</a>" NextMonthText="<a href='javascript:;' onclick='fnNextMonth();'>&nbsp;&nbsp;>&nbsp;&nbsp;</a>"></asp:Calendar>
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>