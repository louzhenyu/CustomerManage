<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>绑定模块</title>
    <script src="/Framework/javascript/Biz/AlbumModuleType.js" type="text/javascript"></script>
    <script src="Controller/AlbumLinkCtl.js" type="text/javascript"></script>
    <script src="Model/AlbumVM.js" type="text/javascript"></script>
    <script src="Store/AlbumVMStore.js" type="text/javascript"></script>
    <script src="View/AlbumLinkView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="Div1" style="height: 441px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width: 620px;">
                        <div style="height: 5px;">
                        </div>
                        <div style="float: left; height: 32px;">
                            <div style="float: left; margin-left: 10px; width: 60px; margin-top: 9px;">
                                模块类型：</div>
                            <div style="float: left;">
                                <div id="txtAlbumModuleType" style="float: left; margin-top: 8px;">
                                </div>
                            </div>
                            <div id="divModuleName" style="float: left;">
                                <div style="float: left; margin-left: 34px; width: 60px; margin-top: 9px;">
                                    模块名称：</div>
                                <div style="float: left;">
                                    <div id="txtModuleName" style="float: left; margin-top: 8px;">
                                    </div>
                                </div>
                            </div>
                            <div style="float: left;">
                                <div id="btnSearch" style="padding-top: 7px; float: left;">
                                </div>
                                <div id="btnConfirm" style="padding-top: 7px; float: left;">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="gridView" style="margin-top: 50px;">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
