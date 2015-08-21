<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>图文素材管理</title>
    <%--    <script type="text/javascript" src="/framework/javascript/Other/jquery-1.9.0.min.js"></script>--%>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WApplicationInterface.js"%>" type="text/javascript"></script>


    <script type="text/javascript" src="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/lib/jquery.mousewheel-3.0.6.pack.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.js?v=2.1.4"%>"
        charset="gb2312"></script>
    <link rel="stylesheet" type="text/css" href="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.css?v=2.1.4"%>"
        media="screen" />
    <link rel="stylesheet" type="text/css" href="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.css?v=1.0.5"%>" />
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"%>"></script>
    <link rel="stylesheet" type="text/css" href="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7"%>" />
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-media.js?v=1.0.5"%>"></script>

    <script src="<%=StaticUrl+"/Module/WMaterialText/Controller/WMaterialTextCtl.js?v=0.2"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WMaterialText/Model/WMaterialTextVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WMaterialText/Store/WMaterialTextVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WMaterialText/View/WMaterialTextView.js"%>" type="text/javascript"></script>
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
                <div class="view_Search2">
                    <span id='span_panel2'></span>
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
