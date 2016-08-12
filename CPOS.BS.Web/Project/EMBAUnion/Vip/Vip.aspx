<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>中欧会员</title>
    <script src="/Framework/Javascript/Other/jquery-1.9.0.min.js"
        type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>
    <!--fancyboxbegin-->
    <script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/lib/jquery.mousewheel-3.0.6.pack.js"></script>
    <script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.js?v=2.1.4"
        charset="gb2312"></script>
    <link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.css?v=2.1.4"
        media="screen" />
    <link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" />
    <script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>
    <link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" />
    <script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>
    <script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-media.js?v=1.0.5"></script>
    <!--fancyboxend-->
    <script src="/Framework/Javascript/Other/editor/kindeditor.js" type="text/javascript"></script>
    <script src="Controller/VipCtl.js?v1" type="text/javascript"></script>
    <script src="Model/VipVM.js?v1" type="text/javascript"></script>
    <script src="Store/VipVMStore.js?v1" type="text/javascript"></script>
    <script src="View/VipView.js?v1" type="text/javascript"></script>
    <style type="text/css">
        .x-form-display-field { padding-top: 0 !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search">
                    <span id='search_form_panel'></span>
                </div>
                <div class="view_Search2">
                    <span id='search_button_panel'></span>
                </div>
            </div>
            <div class="art-titbutton" style="margin: 0px; background: #E6E4E1;">
                <input id="hStatus" type="hidden" value="0" />
                <div class="view_Button" style="margin: 0px; background: #E6E4E1;">
                    <div id="tab0" class="z_posorder_head" onclick="fnSearchVip('0')">
                        <div style="width: 100px; height: 20px;">
                            全部</div>
                        <div id="txtNum0" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab1" class="z_posorder_head" onclick="fnSearchVip('11')">
                        <div style="width: 100px; height: 20px;">
                            待提交</div>
                        <div id="txtNum1" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab2" class="z_posorder_head" onclick="fnSearchVip('12')">
                        <div style="width: 100px; height: 20px;">
                            待认证</div>
                        <div id="txtNum2" style="height: 24px;">
                            0</div>
                    </div>
                    <div id="tab3" class="z_posorder_head" onclick="fnSearchVip('13')">
                        <div style="width: 100px; height: 20px;">
                            已认证</div>
                        <div id="txtNum3" style="height: 24px;">
                            0</div>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="dvGrid">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>