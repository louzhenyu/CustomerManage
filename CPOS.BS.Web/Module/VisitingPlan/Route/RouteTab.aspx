<%@ Page Title="拜访路线管理" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
<div style="display:block;height:0px;overflow:hidden;">
    <iframe id="tab1" frameborder="0" height="520px" width="100%" src=""></iframe>
    <iframe id="tab2" frameborder="0" src=""></iframe>
    <iframe id="tab3" frameborder="0" src=""></iframe>
    </div>
<script language="javascript" type="text/javascript">
    var id = JITMethod.getUrlParam("id");
    var btncode = JITMethod.getUrlParam("btncode");
    var urlparams = window.location.search;

    var tab2State = false;
    var tab3State = false;
    var tabObject = {
        height: "700px",
        width: "100%",
        tab1url: "RouteEdit.aspx"
    };
    Ext.onReady(function () {
        Ext.widget('tabpanel', {
            width: '100%',
            height: 700,
            renderTo: "DivGridView",
            id: "tabs",
            activeTab: 0,
            border: 0,
            items: [{
                title: '基本信息',
                contentEl: "tab1",
                border: 0
            }, {
                title: '地图选择终端',
                contentEl: "tab2",
                border: 0,
                listeners: {
                    activate: function () {
                        if (!tab2State) {
                            tab2State = true;
                            document.getElementById("tab2").src = document.getElementById("tab2").src;
                        }
                    }
                }
            }, {
                title: '列表选择终端',
                contentEl: "tab3",
                border: 0,
                listeners: {
                    activate: function () {
                        if (!tab3State) {
                            tab3State = true;
                            document.getElementById("tab3").src = document.getElementById("tab3").src;
                        }
                    }
                }
            }]
        });

        tab2State = false;
        tab3State = false;

        Ext.getCmp("tabs").setActiveTab(0);

        var parameters = "?mid=" + __mid + "&btncode=" + btncode + "&r=" + Math.random();
        if (id != null && id != "") {
            //修改
            parameters += "&id=" + id;
            Ext.getCmp("tabs").items.items[1].setDisabled(true);
            Ext.getCmp("tabs").items.items[2].setDisabled(true);
        }
        else {
            //新增
            Ext.getCmp("tabs").items.items[1].setDisabled(true);
            Ext.getCmp("tabs").items.items[2].setDisabled(true);
        }
        document.getElementById("tab1").setAttribute("src", tabObject.tab1url + parameters);

        for (var i = 2; i <= 3; i++) {
            document.getElementById("tab" + i).style.height = tabObject.height;
            document.getElementById("tab" + i).style.width = tabObject.width;
        }

    });
</script>
</asp:Content>