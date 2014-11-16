<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Role.js" type="text/javascript"></script>

<script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
<%--<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>--%>
<script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
 <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script><%--
   <script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>
--%>
<!--fancyboxbegin-->
<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/lib/jquery.mousewheel-3.0.6.pack.js"></script>

<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.js?v=2.1.4" charset="gb2312"></script>
<link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.css?v=2.1.4" media="screen" />

<link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" />
<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>

<link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" />
<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>

<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-media.js?v=1.0.5"></script>
<!--fancyboxend-->

<script src="Controller/TaskDataView_ListCtl.js" type="text/javascript"></script>
<script src="Model/TaskDataView_ListVM.js" type="text/javascript"></script>
<script src="Store/TaskDataView_ListVMStore.js" type="text/javascript"></script>
<script src="View/TaskDataView_ListView.js" type="text/javascript"></script>
<style type="text/css">
.POPLIST{margin:5px;border:1px solid #dbdbdb;padding:5px; background-color:White; cursor:pointer;}

.POPLISTselected{margin:5px;border:1px solid #dbdbdb;padding:5px; background-color:#dbdbdb; cursor:pointer;}

.POPLISTS{margin:5px;border:1px solid black;padding:5px; background-color:Black; cursor:pointer;}
.POPLISTS{margin:5px;border:1px solid black;padding:5px; background-color:Black; cursor:pointer;}

.visitphoto{ float:left; width:125px; height:140px ;margin:5px; border:1px solid #dbdbdb; overflow:hidden;text-align:center;}
.visitphoto img{ padding:2px;}
.visitphotodiv{ height:120px; }
.visitphototext 
{
    text-align:center;
    margin:none;
}
.fancyimg
{
    max-width:120px;
    max-height:120px; 
}
    
/*合计行的样式 begin*/
.x-grid-row-summary .x-grid-cell-inner {

            color:Red;
            font-weight:bold;
            border-bottom:1px solid #c6c6c6;
}
/*合计行的样式 end*/


</style>
<script language="javascript" type="text/javascript">
var __imgpath="<%=CurrentUserInfo.ImgPath %>";
var __clientid='<%=CurrentUserInfo.ClientID %>';</script>
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
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>

    <div style="display:block;height:0px;overflow:hidden;">
    <div id="tab1" class="DivGridView"></div>
    <div id="tab2" class="DivGridView"></div>
    </div>

</asp:Content>