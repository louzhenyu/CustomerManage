<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动管理</title>
    
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>

    <script src="Controller/MarketPersonCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/MarketPersonView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section z_event">
       <div class="art-tit" style="background:#F6F6F6">
        <div class="z_event_step z_event_border" style="width:830px; border:0;">
            <div class="z_event_step_item pointer" onclick="fnGoto1()">
                <div class="z_event_step_item_icon z_event_step1"></div>
                <div class="z_event_step_item_text ">定义</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto2()">
                <div class="z_event_step_item_icon z_event_step2"></div>
                <div class="z_event_step_item_text ">时间</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnPre()">
                <div class="z_event_step_item_icon z_event_step3"></div>
                <div class="z_event_step_item_text ">门店</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto4()">
                <div class="z_event_step_item_icon z_event_step4_h"></div>
                <div class="z_event_step_item_text ">人群</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnSave()">
                <div class="z_event_step_item_icon z_event_step5"></div>
                <div class="z_event_step_item_text ">邀约</div>
            </div>
        </div>
        </div>
        <div class="z_event_border" style="font-weight:bold; height:36px; line-height:36px; 
            padding-left:10px; background:rgb(241, 242, 245);">
            <div id="btnImport" style="padding-top:5px;"></div>
            <div id="pnlImport" style="display:none;position:absolute; left:243px; top:185px; z-index:999999; 
                border:1px solid #ddd; width:130px; background:#f0f0f0; font-weight:normal;">
                <div style=" width:130px;text-align:center; border-bottom:1px solid #ddd;">
                    <a href="#" onclick="fnOpenImport('MarketPersonAdd.aspx', '会员筛选')">会员筛选</a>
                </div>
                <div style=" width:130px;text-align:center;">
                    <a href="#" onclick="fnOpenImport('MarketPersonImport.aspx', '人群导入')">人群导入</a>
                </div>
            </div>
        </div>
        <div class="z_event_border" style="padding:0px; border-top:0px; padding-bottom:0px; height:420px;">
            
            <div style="width:100%; padding-left:0px; padding-right:0px;">
                <div style="width:100%;">
                    <div style="float:left; width:180px; background:; display:none;">
                        <div style="float:left; height:30px; width:270px; background:#f0f0f0; padding:4px;">1. 地图方式</div>
                        <div style="clear:both;padding:10px;">
                            <div style="float:left; margin-bottom:10px;">门店：</div>
                            <div style="float:left;"></div>
                        </div>
                        <div style="float:left; height:30px; width:270px; background:#f0f0f0; padding:4px;">2. 文件方式</div>
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
                    <div style="float:left; width:100%;">
                        <div id="gridMarketPerson"></div>
                    </div>
                </div>
            </div>

        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; clear:both; background:rgb(241, 242, 245);">
            <div id="btnNext" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnPre" style="float:right; margin-top:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
    </div>

</asp:Content>
