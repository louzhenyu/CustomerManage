<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
AutoEventWireup="true"  Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="example-data.js" type="text/javascript"></script>
<script src="Column.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Bar Column Sample</h1>
    <div style="margin: 10px;">
        <p>
        Display a sets of random data in a column series. Reload data will randomly generate a new set of data in the store.  <a href="Column.js">View Source</a>
        </p>
    </div>
    <div class="cb" id="divPanel"></div>
</asp:Content>
