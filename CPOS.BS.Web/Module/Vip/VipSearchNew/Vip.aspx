<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" CodeBehind="Vip.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Vip.VipSearchNew.Vip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="<%=StaticUrl+"/framework/CssNew/reset.css"%>" rel="stylesheet" type="text/css" />
            <link href="<%=StaticUrl+"/framework/CssNew/style.css"%>" rel="stylesheet" type="text/css" />
            <link href="<%=StaticUrl+"/framework/CssNew/webcontrol.css"%>" rel="stylesheet" type="text/css" />
            <script src="<%=StaticUrl+"/Framework/Javascript/pub/JTIPagePannel.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/Javascript/pub/JITStoreGrid.js?time=new Date()"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/Javascript/pub/JITStoreSearchPannel.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/Javascript/pub/JITVipFrmWindow.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/javascript/Biz/VipLevel.js"%>" type="text/javascript"></script> 
            <script src="<%=StaticUrl+"/Framework/javascript/Biz/Tags.js"%>" type="text/javascript"></script> 
            <script src="<%=StaticUrl+"/Framework/Javascript/Biz/VipSource.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/javascript/Biz/options.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/javascript/Biz/WindowPanel.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/Framework/javascript/Biz/CitySelectTree.js"%>" type="text/javascript"></script>            <script src="<%=StaticUrl+"/Framework/javascript/Biz/MapSelect.js"%>" type="text/javascript"></script>            <script src="<%=StaticUrl+"/Framework/javascript/Biz/FileUpload.js"%>" type="text/javascript"></script>

            <script src="<%=StaticUrl+"/module/Vip/VipSearchNew/View/VipDef.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/module/Vip/VipSearchNew/View/VipView.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/module/Vip/VipSearchNew/Model/VipVM.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/module/Vip/VipSearchNew/Store/VipVMStore.js"%>" type="text/javascript"></script>
            <script src="<%=StaticUrl+"/module/Vip/VipSearchNew/Controller/VipCtl.js"%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
        <div class="art-tit" id="dvRoleSelect">
                <div class="view_Search">
                    <span id='dvRole'></span>
                </div>
            </div>
            <div class="art-tit">
                <div class="view_Search">
                    <span id='dvSearch'></span>  <%-- //查询内容--%>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='dvWork'></span>
                </div>
            </div>
            <div class="DivGridView" id="dvGrid">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
      <div style="display: block; height: 0px; overflow: hidden;">
        <iframe id="tab1" frameborder="0" height="520px" width="100%" src=""></iframe>
        <iframe id="tab2" frameborder="0" height="520px" width="100%"  src=""></iframe>
        <iframe id="tab3" frameborder="0" height="520px" width="100%" src=""></iframe>
        <%--<iframe id="tab4" frameborder="0" height="520px" width="100%" src=""></iframe>--%>
        <iframe id="tab5" frameborder="0" height="520px" width="100%"  src=""></iframe>        
    </div>
    <script language="javascript" type="text/javascript">
        Ext.Loader.setConfig({ enabled: true });
        Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
        Ext.require([
            'Ext.data.*',
            'Ext.util.*',
            'Ext.view.View',
            'Ext.selection.CellModel',
            'Ext.ux.CheckColumn'
     ]);

        Ext.onReady(function () {
           // alert("真是这里吗？");
            JITPage.HandlerUrl.setValue("Handler/VipHandler.ashx?mid=1");
            tableName = getUrlParam("tablename");
            InitVE();
            InitStore();

            InitView();
            $("#dvRoleSelect").hide();
            Ext.getStore("RoleTableNameStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetRoleList";
            Ext.getStore("RoleTableNameStore").load(
            function () {
                if (tableName != null && tableName != "") {
                    $("#dvRoleSelect").show();
                    Ext.getCmp("RoleTableName").setValue(tableName);
                }
                else {
                    if (Ext.getStore("RoleTableNameStore").getCount() > 0) {
                        $("#dvRoleSelect").show();
                        Ext.getCmp("RoleTableName").setValue(Ext.getStore("RoleTableNameStore").first().data.table_name);
                    }
                    else {
                        $("#dvRoleSelect").hide();
                    }
                }
                Ext.getCmp("RoleTableName").addListener({
                    change: function (r, value) {
                        window.location = ("vip.aspx?mid=" + getUrlParam("mid") + "&tablename=" + value);
                    }
                });
            });


        });
    </script>
</asp:Content>
