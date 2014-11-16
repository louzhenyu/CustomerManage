<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/DMS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="m10 article">
            <div class="art-tit" id="div_searchpanel" style="display:block">
            <div class="view_Search">
                <span id='span_panel'></span>
             </div>
            </div>
                <div class="DivGridView" id="DivGridView">
                </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <div style="display:block;height:0px;overflow:hidden;">
    <iframe id="tab1" frameborder="0" src="view/frameloading.txt" style="display:none;"></iframe>
    <iframe id="tab2" frameborder="0" src="view/frameloading.txt" style="display:none;"></iframe>
    </div>
<script language="javascript" type="text/javascript">
    var id = JITMethod.getUrlParam("id");
    var btncode = JITMethod.getUrlParam("btncode");

    var urlparams = window.location.search;
    JITPage.HandlerUrl.setValue("Handler/CallDayPlanningHandler.ashx?mid=" + __mid);

    Ext.onReady(function () {
        Ext.create('Ext.form.Panel', {
            id: 'searchPanel',
            items: [{
                xtype: "jitbizoptions",
                fieldLabel: "终端类型",
                OptionName: 'POPType',
                name: "POPType",
                id: "POPType",
                isDefault: false,
                listeners: {
                    'change': function () { initTab(this.getValue()) }
                }
            }, {
                xtype: "jitbutton",
                imgName: 'back',
                isImgFirst: true,
                margin: '0 0 10 10',
                handler: function () { window.location = 'CallDayPlanningUserDate.aspx' + urlparams }
            }],
            renderTo: 'span_panel',
            margin: '10 0 0 0',
            layout: 'column',
            border: 0
        });

        Ext.widget('tabpanel', {
            width: '100%',
            height: 700,
            renderTo: "DivGridView",
            id: "tabs",
            activeTab: 0,
            border: 0,
            items: [{
                title: '终端地图',
                contentEl: "tab1",
                border: 0,
                listeners: {
                    activate: function () {
                        if (!tab1State) {
                            tab1State = true;
                            document.getElementById("tab1").src = document.getElementById("tab1").src;
                        }
                    }
                }
            }, {
                title: '终端列表',
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
            }]
        });

        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=search&method=GetUserCDPPOPType",
            params: {
                ClientUserID: JITMethod.getUrlParam("ClientUserID"),
                CallDate: JITMethod.getUrlParam("CallDate")
            },
            method: 'post',
            success: function (response) {
                var jdata = eval(response.responseText);
                if (jdata != "") {
                    //加载dropdown
                    if (jdata.length == 1) {
                        Ext.getCmp("POPType").hide();
                    }
                    else {
                        Ext.getCmp("POPType").show();
                    }
                    //加载iframe
                    tab1State = true; //默认加载第一个
                    Ext.getCmp("POPType").jitSetValue(parseInt(jdata[0].POPType));
                }
                else {
                    Ext.Msg.alert("提示", "获取数据失败");
                }
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取数据失败");
            }
        });

    });

    var tab1State = false;
    var tab2State = false;
    var tabObject = {
        height: "700px",
        width: "100%"
    };
    function initTab(value) {
        tab1State = true;
        tab2State = false;

        var parameters = urlparams + "&r=" + Math.random();

        if (value == 1) {
            document.getElementById("tab1").setAttribute("src", "UserDateDetailMap_Store.aspx" + parameters);
            document.getElementById("tab2").setAttribute("src", "UserDateDetailList_Store.aspx" + parameters);
        }
        else if (value == 2) {
            document.getElementById("tab1").setAttribute("src", "UserDateDetailMap_Distributor.aspx" + parameters);
            document.getElementById("tab2").setAttribute("src", "UserDateDetailList_Distributor.aspx" + parameters);
        }

        for (var i = 1; i <= 2; i++) {
            document.getElementById("tab" + i).style.display = "block";
            document.getElementById("tab" + i).style.height = tabObject.height;
            document.getElementById("tab" + i).style.width = tabObject.width;
        }
    }
</script>
</asp:Content>