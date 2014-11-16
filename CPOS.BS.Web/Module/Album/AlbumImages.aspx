<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>相片管理</title>
    <script src="Controller/AlbumImagesCtl.js" type="text/javascript"></script>
    <script src="Model/AlbumVM.js" type="text/javascript"></script>
    <script src="Store/AlbumVMStore.js" type="text/javascript"></script>
    <script src="View/AlbumImagesView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 461px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="width: 600px;">
                        <div style="height: 5px;">
                        </div>
                        <div style="float: left; height: 32px; clear: both;">
                            <div style="float: left;">
                                <div id="btnCreate" style="padding-top: 7px; float: left;">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="DivGridView" id="DivGridView" style="margin-top: 45px;">
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</asp:Content>
