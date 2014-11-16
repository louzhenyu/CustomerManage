<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动管理</title>
    
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>

    <script src="Controller/MarketPersonImportCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/MarketPersonImportView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section z_event">
        <div class="z_event_border" style="font-weight:bold; height:36px; line-height:36px; 
            padding-left:10px; border-top:0px;display:none; background:rgb(241, 242, 245);">
            <div id="btnSearch" style="padding-top:5px;"></div>
        </div>
        <div class="z_event_border" style="padding:0px; border-top:0px; padding-bottom:0px; 
            height:450px; border-top:1px solid #f0f0f0;">
            
            <div style="width:100%; padding-left:0px; padding-right:0px;">
                <div style="width:100%;">
                    <div style="float:left; width:180px; background:; display:;">
                        <div style="padding:10px;">
                            <div style="line-height:60px; height:60px;clear:both;">
                                文件模板：<a href="#"><font color="blue">下载</font></a>
                            </div>
                            <div style="height:100px;">
                                文件上传：
                                <div id="fileUpload" style="margin-top:10px;"></div>
                                <div id="btnUpload" style="margin-top:10px;"></div>
                            </div>
                        </div>
                    </div>
                    <div style="float:right; width:620px;">
                        <div id="gridMarketPerson"></div>
                    </div>
                </div>
            </div>

        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; clear:both; background:rgb(241, 242, 245);">
            <div id="btnNext" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
    </div>

</asp:Content>
