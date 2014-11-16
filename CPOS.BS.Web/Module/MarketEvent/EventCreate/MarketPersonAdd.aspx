<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动管理</title>
    
    <%--<script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>--%>
    <%--<script src="/Framework/javascript/Biz/UnitSizeType.js" type="text/javascript"></script>--%>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UserGender.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Tags.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/TagsGroup.js" type="text/javascript"></script>

    <script src="Controller/MarketPersonAddCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/MarketPersonAddView.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section z_event">
        <div class="z_event_border" style="font-weight:normal; min-height:120px; line-height:38px; 
            padding-left:10px; display:; background:rgb(241, 242, 245); clear:both;">
            <div style="float:left; height:32px;">
                <div style="float:left; margin-left:10px; width:60px;">姓名：</div>
                <div style="float:left;">
                    <div id="txtUserName" style="float:left; margin-top:8px;"></div>
                </div>
                <div style="float:left; margin-left:34px; width:60px;">性别：</div>
                <div style="float:left;">
                    <div id="txtGender" style="float:left; margin-top:8px;"></div>
                </div>
            </div>
            <div style="float:left; height:32px; clear:both;">
                <div style="float:left; margin-left:10px; width:60px;">标签：</div>
                <div style="float:left;">
                    <div id="txtTags" style="float:left; margin-top:8px;"></div>
                </div>
                <div style="float:left; margin-left:34px; width:60px;">组合关系：</div>
                <div style="float:left;">
                    <div id="txtTagsGroup" style="float:left; margin-top:8px;"></div>
                </div>
                <div style="float:left;">
                    <div id="btnAddGroup" style="float:left; margin-top:8px;"></div>
                </div>
            </div>
            <div style="float:left; height:32px; clear:both;">
                <div style="float:left; margin-left:10px; width:60px;">已选：</div>
                <div style="float:left; width:570px;">
                    <div id="txtAddedTags" style="float:left; margin-top:8px; margin-left:10px; 
                        min-width:550px;max-width:550px; min-height:26px; max-height:42px; overflow:auto;
                        border:1px solid #d0d0d0; line-height:24px; padding-left:4px; padding-right:4px;"></div>
                </div>
            </div>
            <div id="btnSearch" style="padding-top:7px;float:left;"></div>
            <div id="btnSearchReset" style="padding-top:7px;float:left;"></div>
        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; clear:both; background:rgb(241, 242, 245);">
            <div id="btnNext" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
        <div class="z_event_border" style="padding:0px; border-top:0px; padding-bottom:0px; 
            height:390px;">
            
            <div style="width:100%; padding-left:0px; padding-right:0px;">
                <div style="width:100%;">
                    <div style="float:left; width:180px; background:; display:none;">
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
                    <div style="float:right; width:100%;">
                        <div id="gridMarketPerson"></div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
