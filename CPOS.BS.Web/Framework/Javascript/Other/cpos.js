
var DefaultPageSize = 15;
var DetailPageSize = 10;
var MaxPageSize = 10000;
var DefaultGridWidth = "100%"; // 620
var DefaultGridHeight = 367; //284 399
var DetailGridWidth = "100%"; // 620
var DetailGridHeight = 255; //284 399
var defaultCtrlWidth = 100;
var defaultCtrlHeight = 100;
var detailWinWidth = 840;
var detailWinHeight = 550;
var SearchPanelWidth = 820;
var SearchPanelHeight = 44;
var sp = ",";
var spMsg = "$";

var SearchPanelMoreBtnText = "高级查询";
var SearchPanelMoreBtnHideText = "隐藏高级查询";

//Ext.lib.Ajax.defaultPostHeader += ";charset=utf-8";

var timeoutMsg = "页面超时，请重新登录";
function SetLogoInfo() {
    Ext.Ajax.request({
        method: 'GET',
        async: true,
        url: '/Module/CustomerBasicSetting/Handler/CustomerBasicSettingHander.ashx?mid=' + getUrlParam('mid') + '&method=GetCustomerList',
        success: function (response) {
            debugger;
            var data = Ext.decode(response.responseText);
            var logo = $('#img_logo');
            //  debugger;
            logo.attr('alt', data.data.loadInfo.customerName);
            logo.closest('a').attr('title', data.data.loadInfo.customerName);
            if (data.data.loadInfo.BusinessLogo) {
                $(".logoWrap").css({ 'background-image': 'url("' + data.data.loadInfo.BusinessLogo + '")' });
            } else {
                $(".logoWrap").css({ 'background-image': 'url("../../images/newYear/logo.png")' })
            }
            //$('#unitName').html(data.data.loadInfo.customerName); //title是全称html是简写名;        
            //window.customerNameAttr=data.data.loadInfo.customerName;
            var str = $("#lblLoginUserName").html();
            var UnitName = window.UnitShortName ? window.UnitShortName : window.UnitName;
            window.UserName = str;
            UnitName = UnitName.length > 8 ? UnitName.substring(0, 8) + "..." : UnitName
            $("#lblLoginUserName").html( UnitName + "&nbsp;&nbsp;&nbsp;" + str).attr("title", window.UnitName + "(" + window.RoleName + ")");
            if (data.data.requset != null) {
                for (var i = 0; i < data.data.requset.length; i++) {
                    var code = data.data.requset[i].SettingCode;
                    if (code == 'WebLogo') {
                        var val = ''; //data.data.requset[i].SettingValue;
                        if (val == '') break;
                        logo.attr('src', val).css({ 'margin-top': 'auto', 'max-width': '179px', 'max-height': '50px' });
                        break;
                    }
                }
            }

            var menuZoom = function () {
                $("#commonNav").show();
                var countWidth = $(".commonHeader").outerWidth(true);  //总的的宽度

                var handleWrapWidth = $(".handleWrap").outerWidth(true); //右侧门店名字的宽度
                var logoWrapWidth = $(" .commonHeader .logoWrap").outerWidth(true);    //logo的宽度

                var autoWidth = countWidth - handleWrapWidth - logoWrapWidth;
                if ($("#commonNav li.dropDown li").length > 0) {
                    $("#commonNav .addul").append($("#commonNav li.dropDown li"));
                    $("#commonNav li.dropDown").remove();
                }



                var ulList = $("<ul></ul>");
                var liCountWidth = $("#commonNav").outerWidth(true) - $("#commonNav").width() + 36; //包含边框宽度
                liCountWidth += $("#commonNav li.dropDown").outerWidth(true) ? $("#commonNav li.dropDown").outerWidth("true") : 0
                $("#commonNav ul li").each(function () {
                    liCountWidth += $(this).outerWidth(true);
                    if (liCountWidth >= autoWidth) {
                        ulList.append($(this));
                    }
                });

                if (ulList.find("li").length > 0) {
                    $("#commonNav li.dropDown").remove();
                    $("#commonNav ul.clearfix").append("<li class='dropDown'></li>");
                    $("#commonNav li.dropDown").append(ulList);
                    $("#commonNav li.dropDown").find("ul").hide();

                }


            };
            menuZoom();


            /*   $(window).resize(function () {
            menuZoom();
            });*/

        }
    });
}
$(function () {
    //处理右侧高度不够整屏，
    var height = $(window).outerHeight() - $(".commonHeader").outerHeight();

    $("#contentArea").css({ "min-height": height + "px" })

    if ($("#contentArea").outerHeight() < $("#leftMenu").outerHeight()) {
        $("#contentArea").css({ "min-height": $("#leftMenu").outerHeight() + "px" })
    }
    $(window).resize(function () {
        var height = $(window).outerHeight() - $(".commonHeader").outerHeight();
        if ($("#contentArea").outerHeight() < height) {
            $("#contentArea").css({ "min-height": height + "px" })
        }
        if ($("#contentArea").outerHeight() < $("#leftMenu").outerHeight()) {
            $("#contentArea").css({ "min-height": $("#leftMenu").outerHeight() + "px" })
        }
    });

    $("#commonNav").delegate(".dropDown", 'click', function () {
        $(this).find("ul").stop().show();
    }).delegate(".dropDown", 'mouseenter', function () {
        $(this).find("ul").stop().show();
    }).delegate(".dropDown", 'mouseleave', function () {
        var me = $(this);
        setTimeout(function () {
            me.find("ul").stop().hide();
        }, 300) //设置一个超时对象

    }).delegate(".dropDown ul", 'mouseenter', function () {
        $(this).stop().show();
    }).delegate(".dropDown ul", 'mouseleave', function () {
        $(this).stop().hide();
    });
    SetLogoInfo();

    $("#section").delegate(".datagrid-header-check", "mousedown", function (e) {
        var dom = $(this);
        if (!dom.find("input").get(0).checked) {
            $(this).addClass("on");
        } else {
            $(this).removeClass("on");
        }
        return false;
    }).delegate(".datagrid-cell-check", "mousedown", function (e) {
        var dom = $(this);
        var nondes = dom.parents(".datagrid-body-inner").find(".datagrid-cell-check input");
        //验证是否是全选
        var isSeletAll = true;
        for (var i = 0; i < nondes.length; i++) {
            if (!nondes.get(i).checked && nondes.get(i) !== dom.find("input").get(0)) { //排除当前的
                isSeletAll = false;

                break;
            }
        }
        if (isSeletAll) {    //其他都是选中状态
            if (dom.find("input").get(0).checked) { //如果当前的是选中装态。
                isSeletAll = false;
            }
        }

        if (isSeletAll) {
            var allCheckBox = dom.parents(".datagrid-body").siblings(".datagrid-header").find(".datagrid-header-check").addClass("on")
            allCheckBox.find("input").get(0).checked = true;
        } else {
            var allCheckBox = dom.parents(".datagrid-body").siblings(".datagrid-header").find(".datagrid-header-check").removeClass("on")
            allCheckBox.find("input").get(0).checked = true;
        }


        return false;
    });
    $.ajax({
        url: '/ApplicationInterface/Gateway.ashx?type=Product&action=Basic.Menu.GetMenuList&req={"Locale":null,"CustomerID":null,"UserID":null,"OpenID":null,"Token":null,"ChannelID":null,"Parameters":{},"random":0.7135860174894333}',
        method: 'post',
        async: true,
        success: function (response) {
            var menuData = JSON.parse(response);
            if (menuData.IsSuccess && menuData.ResultCode == 0) {
                var data = menuData.Data ;

                if (data&&data.MenuList) {
                    $("#leftsead").show();
                    for (var k = 0; k< data.MenuList.length; k++) {
                        var menu = data.MenuList[k];

                        $("[data-menucode]").each(function () {
                            var menucode = $(this).data("menucode");
                            var me = $(this);
                            if (menu && menu.Menu_Code) {
                                if (menu.Menu_Code == menucode) {
                                    var urlstr = window.location.href.split("?"), params = {};
                                    if (urlstr[1]) {
                                        var items = urlstr[1].split("&");
                                        for (var j = 0; j < items.length; j++) {
                                            var itemarr = items[j].split("=");
                                            params[itemarr[0]] = itemarr[1];
                                        }
                                    }
                                    debugger;
                                   if (params["MMenuID"] == menu.Menu_Id) {
                                        me.find(".menusrc .shows").attr("src", "/Framework/Image/leftImgList/" +menucode+ "on.png")
                                    }

                                    if (menu.SubMenuList.length > 0 && menu.SubMenuList[0].SubMenuList && menu.SubMenuList[0].SubMenuList.length > 0) {
                                        me.find(".menusrc").attr("href", menu.SubMenuList[0].SubMenuList[0].Url_Path + "?CustomerId=" + getUrlParam("CustomerId") + "&mid=" + menu.SubMenuList[0].SubMenuList[0].Menu_Id + "&PMenuID=" + menu.SubMenuList[0].Menu_Id + "&MMenuID=" + menu.Menu_Id);
                                    } else {
                                        me.find(".menusrc").attr("href", "JavaScript:void(0)");
                                    }
                                }
                            }

                        });

                    }
                } else {
                    console.log("模块数据加载失败")
                }

            } else {
                debugger;
                alert(menuData.Message);
            }
        }
    }); 
});
//{"order_no":"","vip_no":"","sales_unit_id":"","order_date_begin":"","order_date_end":"","data_from_id":null,"DeliveryId":null,"ModifyTime_begin":"","ModifyTime_end":""}
 /***未处理订单统计接口****/
function GetUnAuditCount() {
    Ext.Ajax.request({
        method: 'GET',
        //sync: false,
        async: true,
        url: '/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?method=GetPosOrderUnAuditTotalCount',
        params: {
            form: ""
            , sales_unit_id: ""
            , Field7: '100'
            , Count: UnAuditCount
        },
        //timeout: 2400000, //2400000
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                $("#unAuditCoutID").html("-");
            } else {
                $("#unAuditCoutID").html(d.data);
                UnAuditCount = d.data;
            }
            //GetUnAuditCount();
        },
        failure: function (result) {
            $("#unAuditCoutID").html("-");
            //GetUnAuditCount();
        }
    });
}
var UnAuditCount = 0;
//GetUnAuditCount();
//setInterval("GetUnAuditCount()",60000);


function goLoginPage() {
    location.href = "/GoSso.aspx";
}
function getUrlParam(strname) {
    var val = new String(JITMethod.getUrlParam(strname)).toString();
    if (val == undefined || val == null || val == "null" || val == "undefined") return "";
    return val;
}
function checkRow(chk, value, idsCtrlId) {
    //alert(chk.checked, value);
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var exValue = sp + value + sp;
    var checkedIdsCtrl = document.getElementById(idsCtrlIdTemp);
    var checkedIds = checkedIdsCtrl.value;

    if (chk.checked && checkedIds.indexOf(exValue) == -1) {
        checkedIds += exValue;
        checkedIdsCtrl.value = checkedIds.replace(sp + sp, sp);
    }
    else if (!chk.checked) {
        checkedIdsCtrl.value = checkedIds.replace(sp + value, "");
    }
    if (checkedIdsCtrl.value.length < 2)
        checkedIdsCtrl.value = "";
}
function checkAll(obj) {
    var objList = document.getElementsByName("chkSel");
    var objHideIDS = document.getElementById("ExCheckedIds");
    if (objList != null && objList.length > 0) {
        for (var i = 0; i < objList.length; i++) {
            if (obj.checked) {
                if (!objList.item(i).checked) {
                    objList.item(i).click();
                }
            } else {
                if (objList.item(i).checked) {
                    objList.item(i).click();
                }
            }
        }
    }
}
function CheckAllRows(obj, checkboxName, idsCtrlId) {
    var checkboxNameTemp = checkboxName == null ? "chkSel" : checkboxName;
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var objList = document.getElementsByName(checkboxNameTemp);
    var objHideIDS = document.getElementById(idsCtrlIdTemp);
    //objHideIDS.values = "";
    if (objList != null && objList.length > 0) {
        for (var i = 0; i < objList.length; i++) {
            if (obj.checked) {
                if (!objList.item(i).checked) {
                    objList.item(i).click();
                }
            } else {
                if (objList.item(i).checked) {
                    objList.item(i).click();
                }
            }
        }
    }
}
function GetGridCheckedIds(idsCtrlId) {
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var ctrl = document.getElementById(idsCtrlIdTemp);
    return ctrl.value;
}
function SetGridCheckedIds(Ids, idsCtrlId) {
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var ctrl = document.getElementById(idsCtrlIdTemp);
    ctrl.value = Ids;
}
function AddGridCheckedId(idsCtrlId, id) {
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var ctrl = document.getElementById(idsCtrlIdTemp);
    if (ctrl.value.indexOf(sp + id + sp) == -1) {
        ctrl.value += id + sp;
    }
    return ctrl.value;
}
function RemoveGridCheckedId(idsCtrlId, id) {
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var ctrl = document.getElementById(idsCtrlIdTemp);
    if (ctrl.value.indexOf(sp + id + sp) != -1) {
        ctrl.value = ctrl.value.replace(sp + id + sp, ',');
    }
    return ctrl.value;
}
function ResetCacheData(idsCtrlId) {
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var ctrl = document.getElementById(idsCtrlIdTemp);
    ctrl.value = "";
    ShowDebug(ctrl.value);
}
function IsCheckedGridRow(value, idsCtrlId) {
    var idsCtrlIdTemp = idsCtrlId == null ? "ExCheckedIds" : idsCtrlId;
    var checkedIdsCtrl = document.getElementById(idsCtrlIdTemp);
    var checkedIds = checkedIdsCtrl.value;
    var exValue = sp + value + sp;
    if (checkedIds.indexOf(exValue) != -1) {
        return true;
    }
    return false;
}
function CheckSelectedOne(grid) {
    var ids = GetGridCheckedIds();
    if (ids.split(sp).length == 3)
        return "1" + spMsg + spMsg + ids.split(sp)[1];
    return "0" + spMsg + "请选择一条记录！" + spMsg + "-1";
}
function CheckSelectedOneOrMore(grid) {
    var ids = GetGridCheckedIds();
    var count = GetCheckedGridRowsCount(ids);
    if (ids.split(sp).length >= 3)
        return "1" + spMsg + spMsg + ids + spMsg + count;
    return "0" + spMsg + "请选择记录！" + spMsg + "-1";
}
function GetCheckedGridRowsCount(ids) {
    return ids.split(sp).length - 2;
}
Ext.define('TreeComboBox', {
    extend: 'Ext.form.field.ComboBox',
    url: '',
    tree: {},
    textProperty: 'text',
    valueProperty: '',

    initComponent: function () {
        Ext.apply(this, {
            editable: false,
            queryMode: 'local',
            select: Ext.emptyFn
        });

        this.labelWidth = 230;
        this.matchFieldWidth = false;
        this.hid = this.hid;
        this.sid = this.sid;
        this.id_key = this.id_key == undefined ? 'id' : this.id_key;
        this.text_key = this.text_key == undefined ? 'name' : this.text_key;

        this.displayField = this.displayField || 'text';
        this.treeid = Ext.String.format('tree-combobox-{0}', Ext.id());
        this.tpl = Ext.String.format('<div id="{0}"></div>', this.treeid);

        this.listConfig = {
            loadingText: '正在加载数据...',
            width: 400,
            autoHeight: true
        }

        if (this.url) {
            var me = this;
            var store = Ext.create('Ext.data.TreeStore', {
                root: { expanded: true },
                proxy: new Ext.data.HttpProxy({
                    type: 'ajax',
                    url: this.url,
                    headers: { 'Content-type': 'application/json' },
                    reader: {
                        type: 'json',
                        root: 'data'
                    }
                }),
                fields: [
                    { name: "id", mapping: this.id_key },
                    { name: "text", mapping: this.text_key }
                //{name:"leaf", defaultValue:true}
                //{name:"checked", defaultValue:false}
                ]
            });
            this.tree = Ext.create('Ext.tree.Panel', {
                rootVisible: false,
                autoScroll: true,
                height: 200,
                store: store
            });
            this.tree.on('itemclick', function (view, record) {
                try {
                    if (me.hid == null || me.hid.length == 0) me.hid = me.cid + "_hide";
                    if (me.hid != undefined && me.hid != null && me.hid.length > 0) {
                        var hctrl = get(me.hid);
                        hctrl.value = record.data.id;
                    }
                    if (me.sid != undefined && me.sid != null && me.sid.length > 0) {
                        var sctrl = Ext.getCmp(me.sid);
                        var shctrl = get(me.sid + "_hide");
                        if (shctrl != null) {
                            shctrl.value = "";
                        }
                        sctrl.clearValue();
                        sctrl.store.load({ params: { pid: record.data.id} });
                    }
                }
                catch (ex) {
                    alert("加载数据失败:" + ex);
                    return;
                }
                me.setValue(record);
                me.collapse();
            });
            me.on('expand', function () {
                loadCurUser();
                if (!this.tree.rendered) {
                    this.tree.render(this.treeid);
                } else {
                    me.expand();
                }
                if (this.tree != null && this.tree.id != null && this.tree.id.length > 0) {
                    var p1 = get(this.tree.id + "-body");
                    var p2 = p1 ? p1.childNodes[0] : null;
                    if (p2) {
                        p1.style.width = "auto";
                        p2.style.width = "auto";
                        //p2.style.backgroundColor = "rgb(242, 242, 242)";
                    }
                }
            });

        }
        this.callParent(arguments);
    }
});

function ShowWin(title, url, width, height, isModal, id, scrolling) {
    var url2 = url;
    if (url.indexOf('?') == -1) {
        url += "?rand=" + Math.round(Math.random() * 10000);
    }
    else {
        url += "&rand=" + Math.round(Math.random() * 10000);
    }
    //
    //var frm_main = top.frames["frm_main"];
    //var wobj = top.window.document.getElementById(id);
    //if (wobj == null) {
    //    wobj = document.createElement("div");
    //    wobj.id = id;
    //    top.window.document.body.appendChild(wobj);
    //}

    win = new top.Ext.Window({
        id: id,
        //renderTo: wobj.id,
        title: title,
        width: width || 680,
        height: height || 250,
        //plain:true,
        //layout:"form",
        iconCls: "editicon",
        resizable: false,
        draggable: false,
        defaultType: "textfield",
        labelWidth: 100,
        collapsible: false,
        closeAction: 'destroy',
        closable: true,
        modal: isModal,
        buttonAlign: "center",
        bodyStyle: "padding:0 0 0 0",
        html: '<iframe scrolling="' + scrolling + '" style="margin-left:0px;margin-top:0px;border:0;' +
	        'border-style:solid;border-color:red;" width="100%" height="100%" ' +
	        ' id="frmWin' + id + '" src="' + url + '" name="' + id + '" />'
    });
    win.show();

    return win;
}
function ShowWinIn(title, url, width, height, isModal, id) {
    var url2 = url;
    if (url.indexOf('?') == -1) {
        url += "?rand=" + Math.round(Math.random() * 10000);
    }
    else {
        url += "&rand=" + Math.round(Math.random() * 10000);
    }
    var win = new Ext.Window({
        id: id,
        title: title,
        width: width || 680,
        height: height || 250,
        //plain:true,
        //layout:"form",
        iconCls: "editicon",
        resizable: false,
        draggable: false,
        defaultType: "textfield",
        labelWidth: 100,
        collapsible: false,
        closeAction: 'destroy',
        closable: true,
        modal: isModal,
        buttonAlign: "center",
        bodyStyle: "padding:0 0 0 0",
        html: '<iframe scrolling="auto" style="margin-left:0px;margin-top:0px;border:0;' +
	        'border-style:solid;border-color:red;" width="100%" height="100%" ' +
	        ' id="frmWin" src="' + url2 + '"/>'
    });
    win.show();
}
function CloseWin(id) {
    parent.Ext.getCmp(id).close();
}
function HideWin(id) {
    top.Ext.getCmp(id).close();
}
function GetParentWin(id) {
    return top.frames[id].window;
}
function get(id) {
    return document.getElementById(id);
}

function getNow() {
    var d = new Date();
    return d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate()) + " " + addZero(d.getHours()) +
        ":" + addZero(d.getMinutes()) + ":" + addZero(d.getSeconds());
}
function getDate(str) {
    if (str == null || str == "") return "";
    str = str.replace(/-/g, "/");
    str = str.replace("T", " ");
    if (str.length >= 20) str = str.substring(0, 19);
    var d = new Date(str);
    if (isNaN(d)) return str;
    return d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate()) + " " + addZero(d.getHours()) +
        ":" + addZero(d.getMinutes()) + ":" + addZero(d.getSeconds());
}
function toDate(str) {
    if (str == null || str == "") return "";
    str = str.replace(/-/g, "/");
    str = str.replace("T", " ");
    if (str.length >= 20) str = str.substring(0, 19);
    return new Date(str);
}
function getDateHour(date) {
    return date.getHours();
}
function addZero(num) {
    return (num >= 0 && num < 10) ? "0" + num : num;
}
function getDateStr(date) {
    var d = new Date();
    if (date != undefined) d = date;
    return d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate()); ;
}


function createSupplierUnitTree(cid) {
    var _ctrl, _store;
    _store = new Ext.data.TreeStore({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=supplier',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" },
            { name: "leaf", defaultValue: true },
            { name: "checked", defaultValue: false }
        ]
    });

    _ctrl = Ext.create('Ext.tree.Panel', {
        id: cid,
        store: _store,
        rootVisible: false,
        useArrows: true,
        frame: false,
        title: '',
        renderTo: cid,
        width: 200,
        height: 250
    });

    return { store: _store, ctrl: _ctrl };
}

function createSupplierUnitSelect(cid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=supplier',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: defaultCtrlWidth,
        labelWidth: defaultCtrlWidth,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

createPurchaseUnitSelect = function (cid, sid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=purchase',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 130,
        labelWidth: 130,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    if (sid == undefined || sid == null || sid.length == 0) return;
                    var sctrl = Ext.getCmp(sid);
                    sctrl.clearValue();
                    sctrl.store.load({ params: { pid: this.value} });
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "加载数据失败:" + ex);
                }
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}
var createUnitSelect = createPurchaseUnitSelect;

createPurchaseUnitSelectTree = function (cid, hid, sid) {
    var _ctrl = Ext.create('TreeComboBox', {
        renderTo: cid,
        hid: hid,
        sid: sid,
        width: defaultCtrlWidth,
        url: '/Controls/Data.aspx?data_type=purchase'
    });

    return { store: _ctrl.store, ctrl: _ctrl };
}
var createUnitSelectTree = createPurchaseUnitSelectTree;

function createCustomerUnitSelect(cid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=customer',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: defaultCtrlWidth,
        labelWidth: defaultCtrlWidth,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createDateSelect(cid, width, format, value) {
    if (format == undefined || format == null)
        format = 'Y-m-d';
    if (width == undefined || width == null)
        width = 100;
    var _ctrl = new Ext.form.DateField(
        { id: cid, width: width, height: 22, format: format, emptyText: '' });
    _ctrl.render(cid);
    if (value != undefined && value != null) _ctrl.setValue(value);
    return { ctrl: _ctrl };
}

function createButton() {
    var button = new Ext.Button({
        renderTo: cid,
        text: "确定",
        listeners: {
            "click": function () {
                alert("Hello");
            }
        }
    });
    button.minWidth = 200;
    button.setText("EasyPass");
}

function createTextbox(cid, width, value) {
    if (value == undefined || value == null)
        value = "";
    if (width == undefined || width == null)
        width = 100;
    var _ctrl = new Jit.form.field.Text(
        { id: cid, width: width, height: 22, value: '' });
    _ctrl.render(cid);
    return { ctrl: _ctrl };
}

function createOrderStatusSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=' + type,
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "status" },
            { name: "text", mapping: "description" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}
function createOrderTypeSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=' + type,
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "order_type_id" },
            { name: "text", mapping: "order_type_name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createUserStatusSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=' + type,
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "description" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createUserGenderSelect(cid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=user_gender',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "status" },
            { name: "text", mapping: "description" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createAppSysSelect(cid, width, roleCtrlId) {
    var _ctrl, _store, _fn;
    if (typeof roleCtrlId == "function") {
        _fn = roleCtrlId;
    }
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=get_app_sys_list',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "def_App_Id" },
            { name: "text", mapping: "def_App_Name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }

                    if (_fn != undefined) _fn();

                    // sid
                    //alert(roleCtrlId);
                    //                     
                    //                     try {
                    //                         if (roleCtrlId == undefined || roleCtrlId == null || roleCtrlId.length == 0) return;
                    //                         var rolectrl = Ext.getCmp(roleCtrlId);
                    //                         rolectrl.clearValue();
                    //                         rolectrl.store.load({params:{app_sys_id:this.value}});
                    //                     }
                    //                     catch (ex) {
                    //                         Ext.MessageBox.alert("错误", "加载数据失败:" + ex);
                    //                     }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createRoleSelect(cid, pid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=get_role_list',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "role_Id" },
            { name: "text", mapping: "role_Name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();

                if (pid != null) {
                    try {
                        var pCtrl = get(pid);
                        _ctrl.clearValue();
                        _store.load({ params: { app_sys_id: pCtrl.value} });
                    }
                    catch (ex) {
                        Ext.MessageBox.alert("错误", "加载数据失败:" + ex);
                    }
                }
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createWarehouseSelect(cid, type, opt) {
    var _ctrl, _store;
    if (opt == undefined || opt == null) opt = {};
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=warehouse',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "warehouse_id" },
            { name: "text", mapping: "wh_name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: defaultCtrlWidth,
        labelWidth: defaultCtrlWidth,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        queryMode: 'local',
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: opt.emptyText || '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    _store.load({ params: {} });

    return { store: _store, ctrl: _ctrl };
}

function createOrderGrid(cid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=supplier',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 130,
        labelWidth: 130,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',

        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

var sku_selected_data = null;
function createSkuSelect(cid, width, stockId, valueKey) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=sku',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "sku_id" },
            { name: "text", mapping: "item_name" },
            { name: "sku_id", mapping: "sku_id" },
            { name: "item_name", mapping: "item_name" },
            { name: "item_id", mapping: "item_id" },
            { name: "item_code", mapping: "item_code" },
            { name: "prop_1_detail_id", mapping: "prop_1_detail_id" },
            { name: "prop_1_detail_code", mapping: "prop_1_detail_code" },
            { name: "prop_1_detail_name", mapping: "prop_1_detail_name" },
            { name: "prop_2_detail_id", mapping: "prop_2_detail_id" },
            { name: "prop_2_detail_code", mapping: "prop_2_detail_code" },
            { name: "prop_2_detail_name", mapping: "prop_2_detail_name" },
            { name: "prop_3_detail_id", mapping: "prop_3_detail_id" },
            { name: "prop_3_detail_code", mapping: "prop_3_detail_code" },
            { name: "prop_3_detail_name", mapping: "prop_3_detail_name" },
            { name: "prop_4_detail_id", mapping: "prop_4_detail_id" },
            { name: "prop_4_detail_code", mapping: "prop_4_detail_code" },
            { name: "prop_4_detail_name", mapping: "prop_4_detail_name" },
            { name: "prop_5_detail_id", mapping: "prop_5_detail_id" },
            { name: "prop_5_detail_code", mapping: "prop_5_detail_code" },
            { name: "prop_5_detail_name", mapping: "prop_5_detail_name" },
            { name: "prop_1_id", mapping: "prop_1_id" },
            { name: "prop_1_code", mapping: "prop_1_code" },
            { name: "prop_1_name", mapping: "prop_1_name" },
            { name: "prop_2_id", mapping: "prop_2_id" },
            { name: "prop_2_code", mapping: "prop_2_code" },
            { name: "prop_2_name", mapping: "prop_2_name" },
            { name: "prop_3_id", mapping: "prop_3_id" },
            { name: "prop_3_code", mapping: "prop_3_code" },
            { name: "prop_3_name", mapping: "prop_3_name" },
            { name: "prop_4_id", mapping: "prop_4_id" },
            { name: "prop_4_code", mapping: "prop_4_code" },
            { name: "prop_4_name", mapping: "prop_4_name" },
            { name: "prop_5_id", mapping: "prop_5_id" },
            { name: "prop_5_code", mapping: "prop_5_code" },
            { name: "prop_5_name", mapping: "prop_5_name" },
            { name: "barcode", mapping: "barcode" },
            { name: "status", mapping: "status" },
            { name: "display_name", mapping: "display_name" },
            { name: "create_time", mapping: "create_time" },
            { name: "create_user_id", mapping: "create_user_id" },
            { name: "modify_time", mapping: "modify_time" },
            { name: "modify_user_id", mapping: "modify_user_id" }
        ]
    });

    _ctrl = Ext.create('Jit.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: valueKey != undefined && valueKey != null ? valueKey : 'id',
        displayField: 'display_name',
        minChars: 1,
        width: width || 100,
        labelWidth: width,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',

        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{display_name}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var d = record[0].data;
                    sku_selected_data = d;

                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }

                    var tbItemName = get("tbItemName");
                    if (tbItemName != null) tbItemName.value = d.text;

                    var tbStockCtrl = get(stockId);
                    if (tbStockCtrl != null) {
                        tbStockCtrl.value = getStockNum(d.sku_id, sku_selected_data.unit_id, sku_selected_data.warehouse_id);
                    }

                    setSkuPropsDisplay(d);
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

setSkuPropsDisplay = function (d, args) {
    if (args == undefined || args == null) {
        args = {};
        args.skuPropId = {
            rowSkuProp1: 'rowSkuProp1',
            rowSkuProp2: 'rowSkuProp2',
            rowSkuProp3: 'rowSkuProp3',

            lblSkuProp1: 'lblSkuProp1',
            lblSkuProp2: 'lblSkuProp2',
            lblSkuProp3: 'lblSkuProp3',
            lblSkuProp4: 'lblSkuProp4',
            lblSkuProp5: 'lblSkuProp5',

            txtSkuProp1: 'txtSkuProp1',
            txtSkuProp2: 'txtSkuProp2',
            txtSkuProp3: 'txtSkuProp3',
            txtSkuProp4: 'txtSkuProp4',
            txtSkuProp5: 'txtSkuProp5'
        };
    }

    sku_selected_data.prop_1_detail_id = d.prop_1_detail_id;
    sku_selected_data.prop_1_detail_code = d.prop_1_detail_code;
    sku_selected_data.prop_1_detail_name = d.prop_1_detail_name;
    sku_selected_data.prop_1_id = d.prop_1_id;
    sku_selected_data.prop_1_code = d.prop_1_code;
    sku_selected_data.prop_1_name = d.prop_1_name;

    sku_selected_data.prop_2_detail_id = d.prop_2_detail_id;
    sku_selected_data.prop_2_detail_code = d.prop_2_detail_code;
    sku_selected_data.prop_2_detail_name = d.prop_2_detail_name;
    sku_selected_data.prop_2_id = d.prop_2_id;
    sku_selected_data.prop_2_code = d.prop_2_code;
    sku_selected_data.prop_2_name = d.prop_2_name;

    sku_selected_data.prop_3_detail_id = d.prop_3_detail_id;
    sku_selected_data.prop_3_detail_code = d.prop_3_detail_code;
    sku_selected_data.prop_3_detail_name = d.prop_3_detail_name;
    sku_selected_data.prop_3_id = d.prop_3_id;
    sku_selected_data.prop_3_code = d.prop_3_code;
    sku_selected_data.prop_3_name = d.prop_3_name;

    sku_selected_data.prop_4_detail_id = d.prop_4_detail_id;
    sku_selected_data.prop_4_detail_code = d.prop_4_detail_code;
    sku_selected_data.prop_4_detail_name = d.prop_4_detail_name;
    sku_selected_data.prop_4_id = d.prop_4_id;
    sku_selected_data.prop_4_code = d.prop_4_code;
    sku_selected_data.prop_4_name = d.prop_4_name;

    sku_selected_data.prop_5_detail_id = d.prop_5_detail_id;
    sku_selected_data.prop_5_detail_code = d.prop_5_detail_code;
    sku_selected_data.prop_5_detail_name = d.prop_5_detail_name;
    sku_selected_data.prop_5_id = d.prop_5_id;
    sku_selected_data.prop_5_code = d.prop_5_code;
    sku_selected_data.prop_5_name = d.prop_5_name;

    if (args.skuPropId == undefined ||
        args.skuPropId == null ||
        args.skuPropId.rowSkuProp1 == undefined ||
        args.skuPropId.rowSkuProp1 == null) return;

    var rowSkuProp1 = get(args.skuPropId.rowSkuProp1);
    var rowSkuProp2 = get(args.skuPropId.rowSkuProp2);
    var rowSkuProp3 = get(args.skuPropId.rowSkuProp3);

    var lblSkuProp1 = get(args.skuPropId.lblSkuProp1);
    var lblSkuProp2 = get(args.skuPropId.lblSkuProp2);
    var lblSkuProp3 = get(args.skuPropId.lblSkuProp3);
    var lblSkuProp4 = get(args.skuPropId.lblSkuProp4);
    var lblSkuProp5 = get(args.skuPropId.lblSkuProp5);

    var tbSkuProp1 = Ext.getCmp(args.skuPropId.txtSkuProp1);
    var tbSkuProp2 = Ext.getCmp(args.skuPropId.txtSkuProp2);
    var tbSkuProp3 = Ext.getCmp(args.skuPropId.txtSkuProp3);
    var tbSkuProp4 = Ext.getCmp(args.skuPropId.txtSkuProp4);
    var tbSkuProp5 = Ext.getCmp(args.skuPropId.txtSkuProp5);

    if (d.prop_1_id != null && d.prop_1_id.length > 0) {
        rowSkuProp1.style.display = "";
        lblSkuProp1.style.display = "";
        tbSkuProp1.setVisible(true);
        lblSkuProp1.innerHTML = d.prop_1_name;
        tbSkuProp1.setValue(d.prop_1_detail_name);
    } else {
        lblSkuProp1.style.display = "none";
        tbSkuProp1.setVisible(false);
        lblSkuProp1.innerHTML = "";
        tbSkuProp1.setValue("");
    }

    if (d.prop_2_id != null && d.prop_2_id.length > 0) {
        rowSkuProp1.style.display = "";
        lblSkuProp2.style.display = "";
        tbSkuProp2.setVisible(true);
        lblSkuProp2.innerHTML = d.prop_2_name;
        tbSkuProp2.setValue(d.prop_2_detail_name);
    } else {
        lblSkuProp2.style.display = "none";
        tbSkuProp2.setVisible(false);
        lblSkuProp2.innerHTML = "";
        tbSkuProp2.setValue("");
    }

    if (d.prop_3_id != null && d.prop_3_id.length > 0) {
        rowSkuProp2.style.display = "";
        lblSkuProp3.style.display = "";
        tbSkuProp3.setVisible(true);
        lblSkuProp3.innerHTML = d.prop_3_name;
        tbSkuProp3.setValue(d.prop_3_detail_name);
    } else {
        lblSkuProp3.style.display = "none";
        tbSkuProp3.setVisible(false);
        lblSkuProp3.innerHTML = "";
        tbSkuProp3.setValue("");
    }

    if (d.prop_4_id != null && d.prop_4_id.length > 0) {
        rowSkuProp2.style.display = "";
        lblSkuProp4.style.display = "";
        tbSkuProp4.setVisible(true);
        lblSkuProp4.innerHTML = d.prop_4_name;
        tbSkuProp4.setValue(d.prop_4_detail_name);
    } else {
        lblSkuProp4.style.display = "none";
        tbSkuProp4.setVisible(false);
        lblSkuProp4.innerHTML = "";
        tbSkuProp4.setValue("");
    }

    if (d.prop_5_id != null && d.prop_5_id.length > 0) {
        rowSkuProp3.style.display = "";
        lblSkuProp5.style.display = "";
        tbSkuProp5.setVisible(true);
        lblSkuProp5.innerHTML = d.prop_5_name;
        tbSkuProp5.setValue(d.prop_5_detail_name);
    } else {
        lblSkuProp5.style.display = "none";
        tbSkuProp5.setVisible(false);
        lblSkuProp5.innerHTML = "";
        tbSkuProp5.setValue("");
    }

    if (d.prop_1_id == null && d.prop_2_id == null) {
        rowSkuProp1.style.display = "none";
    }
    if (d.prop_3_id == null && d.prop_4_id == null) {
        rowSkuProp2.style.display = "none";
    }
    if (d.prop_5_id == null) {
        rowSkuProp3.style.display = "none";
    }
}

function checkIsNum(str) {
    var result = { status: false, value: 0 };
    try {
        result.value = parseInt(str);
        if (isNaN(result.value)) return result;
    } catch (ex) {
        return result;
    }
    result.status = true;
    return result;
}
function checkIsFloat(str) {
    var result = { status: false, value: 0 };
    try {
        result.value = parseFloat(str);
        if (isNaN(result.value)) return result;
    } catch (ex) {
        return result;
    }
    result.status = true;
    return result;
}

function showError(value) {
    alert(value); return;
    Ext.Msg.show({
        title: "错误",
        msg: value,
        draggable: false,
        resizable: false,
        icon: Ext.Msg.ERROR,
        buttons: Ext.Msg.OK,
        minWidth: 260,
        minHeight: 180
    });
}
function showInfo(value) {
    alert(value); return;
    Ext.Msg.show({
        title: "信息",
        msg: value,
        draggable: false,
        resizable: false,
        icon: Ext.Msg.INFO,
        buttons: Ext.Msg.OK,
        minWidth: 260,
        minHeight: 180
    });
}
function showSuccess(value, fn) {
    alert(value); return;
    Ext.Msg.show({
        title: "信息",
        msg: value,
        icon: Ext.Msg.INFO,
        buttons: Ext.Msg.OK,
        minWidth: 260,
        modal: true,
        fn: function (btn, text) {
            if (btn == "ok") {
                eval(fn);
            }
        }
    });
}
function getStr(val) {
    if (typeof val == "string") return val;
    if (isNaN(val) || val == undefined || val == null)
        return "";
    else
        return val;
}
function getFloat(val) {
    if (typeof val == "number") return val;
    if (isNaN(val) || val == undefined || val == null || val == "" || val.length == 0)
        return 0;
    else if (typeof val == "string")
        return parseFloat(val);
    else
        return val;
}
function getNum(val) {
    if (typeof val == "number") return val;
    if (isNaN(val) || val == undefined || val == null || val == "" || val.length == 0)
        return 0;
    else if (typeof val == "string")
        return parseInt(val);
    else
        return val;
}
function getDateValue(obj) {
    if (obj.getValue() == null || obj.getValue() == "") return "";
    return obj.getValue().format('yyyy-MM-dd');
}
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
    }
    return guid;
};
var z_cur_user = null;
function loadCurUser() {
    Ext.Ajax.request({
        method: 'GET',
        url: '/Controls/Data.aspx?data_type=cur_user',
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                z_cur_user = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function () {
            showInfo("页面超时，请重新登录");
        }
    });
    return z_cur_user;
};
var z_sku_prop_cfg = null;
function loadSkuPropCfg() {
    if (z_sku_prop_cfg != null) return z_sku_prop_cfg;
    Ext.Ajax.request({
        method: 'GET',
        url: '/Controls/Data.aspx?data_type=sku_prop_cfg',
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                z_sku_prop_cfg = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showInfo("错误：" + result);
        }
    });
    return z_sku_prop_cfg;
};
function newOrderNo(type) {
    var order_no = "";
    Ext.Ajax.request({
        method: 'GET',
        url: '/Controls/Data.aspx?data_type=' + type,
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                order_no = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showError(result);
        }
    });
    return order_no;
};
function createBtn(cid, text, fn) {
    var _ctrl = Ext.create('Ext.Button', {
        id: cid + "_ext",
        text: text,
        renderTo: cid,
        width: 80,
        height: 25,
        handler: function () {
            eval(fn);
        }
    });
    return { ctrl: _ctrl };
};
function getStockNum(id, unit_id, warehouse_id) {
    var num = 0;
    if (unit_id === undefined || unit_id == "undefined") {
        unit_id = getUrlParam("unit_id");
    }
    if (warehouse_id === undefined || warehouse_id == "undefined") {
        warehouse_id = getUrlParam("warehouse_id");
    }
    Ext.Ajax.request({
        method: 'GET',
        url: '/Controls/Data.aspx?data_type=stock_num&id=' + id +
            '&unit_id=' + unit_id + '&warehouse_id=' + warehouse_id,
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                num = parseFloat(d.data);
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showError(result);
        }
    });
    return num;
};


function createItemCategoryTree(cid) {
    var _ctrl, _store;
    _store = new Ext.data.TreeStore({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=item_category',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" },
            { name: "leaf", defaultValue: true },
            { name: "checked", defaultValue: false }
        ]
    });

    _ctrl = Ext.create('Ext.tree.Panel', {
        id: cid,
        store: _store,
        rootVisible: false,
        useArrows: true,
        frame: false,
        title: '',
        renderTo: cid,
        width: 200,
        height: 250
    });

    return { store: _store, ctrl: _ctrl };
}

function createItemCategorySelect(cid) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=item_category',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: defaultCtrlWidth,
        labelWidth: defaultCtrlWidth,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createItemCategorySelectTree(cid, hid, sid) {
    var _ctrl = Ext.create('TreeComboBox', {
        renderTo: cid,
        hid: hid,
        sid: sid,
        width: defaultCtrlWidth,
        id_key: 'item_Category_Id',
        text_key: 'item_Category_Name',
        url: '/Controls/Data.aspx?data_type=item_category'
    });

    return { store: _ctrl.store, ctrl: _ctrl };
}

function createStatusSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=normal_status',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "description" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createYNStatusSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=yn_status',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "description" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createNYStatusSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=ny_status',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "id" },
            { name: "text", mapping: "description" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function createItemPriceTypeSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=get_item_price_list',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "item_price_type_id" },
            { name: "text", mapping: "item_price_type_name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}


//保存属性数据
function savePropData(type) {
    var unit = [];
    var checkStatus = true;
    switch (type) {
        case "UNIT": saveUnitData(); break;
        case "ITEM": saveItemData(); break;
        default: break;
    }
    function saveItemData() {
        if (kindeditorArray.length > 0) {//把editor的值给到对应的原始控件
            for (var i = 0; i < kindeditorArray.length; i++) {
                kindeditorArray[i].sync();
            }
        }

        $("._prop_detail").each(function () {
            var para = {};
            para.PropertyCodeId = $(this).attr("id");
            para.PropertyDetailId = $(this).attr("PropertyDetailId");
            if ($(this).attr("type") == "date") {
                if (Ext.getCmp(para.PropertyCodeId).getValue())
                    para.PropertyCodeValue = getDateStr(Ext.getCmp(para.PropertyCodeId).getValue());
                else
                    para.PropertyCodeValue = "";
            }
            else if ($(this).attr("type") == "textnumber") {
                if (Ext.getCmp(para.PropertyCodeId).getValue())
                    para.PropertyCodeValue = Ext.getCmp(para.PropertyCodeId).getValue();
                else
                    para.PropertyCodeValue = "";
            }
            else if ($(this).attr("type") == "keyvalue") {

                var jdata = getKeyValueJDATA(para.PropertyCodeId);
                if (jdata) {
                    para.PropertyCodeValue = Ext.JSON.encode(jdata);
                }
                else {
                    checkStatus = false;
                }
            }
            else {
                para.PropertyCodeValue = this.value;
                if ($(this).attr("name") == "kindeditorcontent")
                    para.IsEditor = true;
            }
            unit.push(para);
        });
        $("._prop_detail_radio").each(function () {
            if ($(this).attr("checked")) {
                var para = {};
                para.PropertyCodeId = $(this).attr("PropertyDetailId");
                para.PropertyDetailId = $(this).attr("id");
                unit.push(para);
            }
        });
    }
    function saveUnitData() {
        if (kindeditorArray.length > 0) {//把editor的值给到对应的原始控件
            for (var i = 0; i < kindeditorArray.length; i++) {
                kindeditorArray[i].sync();
            }
        }

        $("._prop_detail").each(function () {
            var para = {};
            para.PropertyCodeId = $(this).attr("id");
            para.PropertyDetailId = $(this).attr("PropertyDetailId");
            //para.PropertyDetailCode = this.value;
            if ($(this).attr("type") == "date") {
                para.PropertyDetailCode = getDateStr(Ext.getCmp(para.PropertyCodeId).getValue());
            }
            else if ($(this).attr("type") == "textnumber") {
                if (Ext.getCmp(para.PropertyCodeId).getValue())
                    para.PropertyDetailCode = Ext.getCmp(para.PropertyCodeId).getValue();
                else
                    para.PropertyDetailCode = "";
            }
            else if ($(this).attr("type") == "keyvalue") {
                var jdata = getKeyValueJDATA(para.PropertyCodeId);
                if (jdata) {
                    para.PropertyDetailCode = Ext.JSON.encode(jdata);
                }
                else {
                    checkStatus = false;
                }
            }
            else {
                para.PropertyDetailCode = this.value;
            }

            unit.push(para);
        });
        $("._prop_detail_radio").each(function () {
            if ($(this).attr("checked")) {
                var para = {};
                para.PropertyCodeId = $(this).attr("PropertyDetailId");
                para.PropertyDetailCode = $(this).attr("id");
                unit.push(para);
            }
        });
    }

    if (checkStatus) {
        return unit;
    }
    else {
        return null;
    }
}

//加载属性数据
function loadPropData(data, type) {
    var $data = $(data);
    switch (type) {
        case "UNIT": loadUnitProp(data); break;
        case "ITEM": loadItemProp(data); break;
        default: break;
    }
    function loadItemProp(data) {
        $data.each(function (index, item) {
            var value = "";
            switch ($("#" + item.PropertyCodeId).attr("type")) {
                case "text":
                case "textarea":
                    value = item.PropertyCodeValue;
                    break;
                case "date":
                case "textnumber":
                    Ext.getCmp(item.PropertyCodeId).setValue(item.PropertyCodeValue);
                    break;
                case "keyvalue":
                    createKeyValueByJDATA(item.PropertyCodeId, item.PropertyCodeValue);
                    break;
                default:
                    value = item.PropertyDetailId;
                    break;
            }

            if (value == undefined || value == null) value = "";
            $("#" + item.PropertyCodeId).val(value).attr("checked", "checked").attr("PropertyDetailId", item.PropertyDetailId);
            if ($("#" + item.PropertyCodeId).length == 0) {
                $("#" + item.PropertyDetailId).attr("checked", "checked");
            }
        });
    }
    function loadUnitProp(data) {
        $data.each(function (index, item) {
            var value = "";
            switch ($("#" + item.PropertyCodeId).attr("type")) {
                case "text":
                case "textarea":
                    value = item.PropertyDetailCode;
                    break;
                case "date":
                case "textnumber":
                    Ext.getCmp(item.PropertyCodeId).setValue(item.PropertyDetailCode);
                    break;
                case "keyvalue":
                    createKeyValueByJDATA(item.PropertyCodeId, item.PropertyDetailCode);
                default:
                    value = item.PropertyDetailId;
                    break;
            }

            if (value == undefined || value == null) value = "";
            $("#" + item.PropertyCodeId).val(value).attr("checked", "checked").attr("PropertyDetailId", item.PropertyDetailId);
            if ($("#" + item.PropertyCodeId).length == 0) {
                $("#" + item.PropertyDetailId).attr("checked", "checked");
            }
        });
    }
}


var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
var reg_is_char = new RegExp(test);
var reg_is_cn = /^[\u0391-\uFFE5]+$/;
//英文
var reg_is_en = /^[\s,a-z,A-Z]*$/;
//添加sku信息
function addSkuPost(updateSKUIndex) {
    if (reg_is_char.test(Ext.getCmp("txtBarcode").getValue()) | reg_is_cn.test(Ext.getCmp("txtBarcode").getValue())) {
        infobox.showPop("条码不允许输入汉字或特殊字符!");
        Ext.getCmp("txtBarcode").focus();
        return;
    }

    var index = parseInt($("#tbTableSku tbody tr").last().attr("index") == null ? "0" : $("#tbTableSku tbody tr").last().attr("index")) + 1;

    if (updateSKUIndex !=null) {
        index = updateSKUIndex+1;
    }

    var skuData = getSkuInfo(index);

    if (!skuData) {
        return;
    }

    if (checkBarcodeExists(skuData) && updateSKUIndex == null) {//如果响应条码已经存在
        alert("条码已存在!");
        Ext.getCmp("txtBarcode").focus();
        return;
    }

    if (checkSkuExists(skuData) && updateSKUIndex == null) {
        alert("SKU信息已存在！");
        return;
    }
     
    if (!(skuData == undefined || skuData == null) && updateSKUIndex == null) {

        //if (skuData.sku_id == undefined || skuData.sku_id == null || skuData.sku_id.length == 0) skuData.sku_id = newGuid();
        //modify by donal 这个不用给新增的sku赋id，后台会自动生成。
        skuData.sku_id = fnSaveOne(skuData, "edit");
        itemSkuData.push(skuData);
    }
    else {
        skuData.sku_id = itemSkuData[updateSKUIndex].sku_id;
        var skuID = fnSaveOne(skuData, "edit");

        if (skuID != "") {
            skuData.sku_id = skuID;
        }
        itemSkuData[updateSKUIndex] = skuData;//变成原来的  
    }
        
        


    buildSkuTable();//重建skuTable*****

    clearSKUInput();
}
//添加Sku列表
function buildSkuTable() {
    $("#tbTableSku tr[index]").remove();
    for (var i = 0; i < itemSkuData.length; i++) {
        addSkuTr(itemSkuData[i], i + 1);
    }
}
function addSkuTr(data, index) {
    var length = $("#tbTableSku tr").length;
    var _class = "";
    if (index % 2 == 0) {
        _class = "b_c4";
    } else {
        _class = "b_c5";
    }
    var show = "";
    if ('<%=ViewState["action"] %>' == 'Visible') {
        show = "none";
    }
    var skuTr = "<tr class=\"" + _class + "\" sku_id=\"" + data.sku_id + "\" index='" + index + "'>";
    skuTr += "<td><a href=\"javascript:void(0);\" onclick=\"go_Del(this,'itemSku');\" style=\"display:" + show + "\"><font color=\"blue\">删除</font></a>";
    //SKU不允许修改，避免歧义     2014-04-18  Willie
    //修改sku，把当前的的td里的对象传过去
    skuTr += "<br><a href=\"javascript:void(0);\" onclick=\"updateSKU(this,'itemSku');\" style=\"display:" + show + "\"><font color=\"blue\">修改</font></a></td>";
    skuTr += "<td class='hidden'>" + (data.prop_1_detail_name == null ? "&nbsp;" : data.prop_1_detail_name) + "</td>";
    skuTr += "<td class='hidden'>" + (data.prop_2_detail_name == null ? "&nbsp;" : data.prop_2_detail_name) + "</td>";
    skuTr += "<td class='hidden'>" + (data.prop_3_detail_name == null ? "&nbsp;" : data.prop_3_detail_name) + "</td>";
    skuTr += "<td class='hidden'>" + (data.prop_4_detail_name == null ? "&nbsp;" : data.prop_4_detail_name) + "</td>";
    skuTr += "<td class='hidden'>" + (data.prop_5_detail_name == null ? "&nbsp;" : data.prop_5_detail_name) + "</td>";
    skuTr += "<td>" + data.barcode + "</td>";
    //-----begin sku价格处理(jifeng.cao)
    for (var i = 0; i < data.sku_price_list.length; i++) {
        skuTr += "<td>" + (data.sku_price_list[i].sku_price == -1 || data.sku_price_list[i].sku_price == "" || data.sku_price_list[i].sku_price == null ? "/" : data.sku_price_list[i].sku_price) + "</td>";
        if (data.sku_price_list[i].sku_price == "" || data.sku_price_list[i].sku_price == null) {
            data.sku_price_list[i].sku_price = -1;
        }
    }
    //-----end
    skuTr += "</tr>";
    $("#tbTableSku").append(skuTr);
    //getTableShowOrHide("tbTableSku","title2");
    $(".itemSku").each(function () {
        //判断是否为价格类型文本框(jifeng.cao)
        if ($(this).attr("is_price") != "1") {
            var display_index = $(this).attr("columnIndex");
            $($("#tbTableSku tr[index=" + index + "]")[0].cells[display_index]).removeClass("hidden");
        }
    });
}
//获取Sku对象
function getSkuInfo(index) {
    var sku_flag = true;
    //不同类型的价格集合
    var price_type_list = [];

    var para = "{";
    $(".itemSku").each(function () {
        var ctrl = Ext.getCmp(this.id);
        var ctrlVal = "";
        if (ctrl != undefined) {
            ctrlVal = Ext.getCmp(this.id).getValue();
        } else {
            ctrl = get(this.id);
            ctrlVal = get(this.id).value;
        }

        //判断是否为价格类型文本框(jifeng.cao)
        if ($(this).attr("is_price") != "1") {
            //if (ctrlVal == "--" | ctrlVal == undefined | ctrlVal == "") {
            //    sku_flag = false;
            //    $(this).focus();
            //    var input_flag = $(this).attr("input_flag");
            //    var prop_name = $(this).attr("prop_name");
            //    if (input_flag == "text") {
            //        alert(prop_name + "不能为空");
            //        return false;
            //    } else if (input_flag == "select") {
            //        alert("必须选择" + prop_name);
            //        return false;
            //    }
            //}
            var dispaly_index = $(this).attr("columnIndex");
            var prop_id = $(this).attr("id");
            for (var i = 1; i <= 5; i++) {
                if (i == dispaly_index) {
                    var prop_name = "";
                    if ($(this).attr("type") == "text") {
                        prop_name = Ext.getCmp(this.id) == undefined ? get(this.id).value : Ext.getCmp(this.id).getValue();
                    } else if ($(this).attr("type") == "radio") {
                        prop_name = $(this).attr("prop_name");
                    }
                    else {
                        prop_name = $("#" + this.id + ">option:selected").html();
                    }
                    para += "\"prop_" + i + "_id\":\"" + prop_id + "\",\"prop_" + dispaly_index + "_detail_id\":\"" +
                    (Ext.getCmp(this.id) == undefined ? get(this.id).value : Ext.getCmp(this.id).getValue()) +
                    "\",\"prop_" + i + "_detail_name\":\"" + prop_name + "\",";
                    break;
                }
            }

        } else {
            var sku_price = {};
            sku_price.item_price_type_id = this.id;
            sku_price.sku_price = ctrlVal;
            price_type_list.push(sku_price);
        }
    });
    if (sku_flag) {
        if (Ext.getCmp("txtBarcode").getValue() == "") {
            alert("条码不能为空");
            $("#txtBarcode").focus();
            sku_flag = false;
        }
    }
    para += "\"barcode\":\"" + Ext.getCmp("txtBarcode").getValue() + "\",";


    //-----begin sku价格处理(jifeng.cao)
    var price_list = "";
    for (var i = 0; i < price_type_list.length; i++) {
        price_list += "{\"item_price_type_id\":\"" + price_type_list[i].item_price_type_id + "\",\"sku_price\":\"" + price_type_list[i].sku_price + "\"},";
    }
    if (price_list.length > 0) {
        price_list = price_list.substring(0, price_list.length - 1);
    }
    para += "\"sku_price_list\":[" + price_list + "],";
    //-----end


    para += "\"sku_id\":\"\",";
    para += "\"index\":\"" + index + "\"";
    para += "}";

    return sku_flag ? JSON.parse(para) : null;
}
//清空sku输入***
function clearSKUInput() {
    $(".itemSku").each(function () {
        var ctrl = Ext.getCmp(this.id);
        if (ctrl != undefined) {
            ctrl.setValue("");
        } else {
            ctrl = get(this.id);
            $("#" + this.id).val("");
        }
    })

    Ext.getCmp("txtBarcode").setValue("");
}

//检查Barcode是否存在
function checkBarcodeExists(data) {
    if (data == null) {
        return false;
    }
    var is_Exists = false;
    $(itemSkuData).each(function () {
        if (data.barcode == this.barcode) {
            is_Exists = true;
            return false;
        }
    });
    return is_Exists;
}
//检查Sku是否存在
function checkSkuExists(data) {
    if (!data) {
        return false;
    }
    var is_Exists = false;
    for (var i = 0; i < itemSkuData.length; i++) {
        if (data.prop_1_id == (itemSkuData[i].prop_1_id == null ? undefined : itemSkuData[i].prop_1_id) &
            data.prop_1_detail_id == (itemSkuData[i].prop_1_detail_id == null ? undefined : itemSkuData[i].prop_1_detail_id) &
            data.prop_2_id == (itemSkuData[i].prop_2_id == null ? undefined : itemSkuData[i].prop_2_id) &
            data.prop_2_detail_id == (itemSkuData[i].prop_2_detail_id == null ? undefined : itemSkuData[i].prop_2_detail_id) &
            data.prop_3_id == (itemSkuData[i].prop_3_id == null ? undefined : itemSkuData[i].prop_3_id) &
            data.prop_3_detail_id == (itemSkuData[i].prop_3_detail_id == null ? undefined : itemSkuData[i].prop_3_detail_id) &
            data.prop_4_id == (itemSkuData[i].prop_4_id == null ? undefined : itemSkuData[i].prop_4_id) &
            data.prop_4_detail_id == (itemSkuData[i].prop_4_detail_id == null ? undefined : itemSkuData[i].prop_4_detail_id) &
            data.prop_5_id == (itemSkuData[i].prop_5_id == null ? undefined : itemSkuData[i].prop_5_id) &
            data.prop_5_detail_id == (itemSkuData[i].prop_5_detail_id == null ? undefined : itemSkuData[i].prop_5_detail_id)) {
            is_Exists = true;
            break;
        }
    }
    return is_Exists;
}
//删除sku信息
function deleteSku(sender) {
    var parentId = $(sender).parent().parent();
    var index = parentId.attr("index");
    var sku_id = parentId.attr("sku_id");
    
    if (methedType == "Edit") {
        if (!itemSkuData || itemSkuData.length == 1) {
            Ext.Msg.alert('删除失败', '最后一个sku不能删除！');
            return;
        }
    }

    $(itemSkuData).each(function () {
        //if (this.index) {
        //    if (index == this.index) {
        //        itemSkuData.removeValue(this);
        //        $(sender).parent().parent().remove()
        //        return false;
        //    }
        //} else {
        //    if (sku_id == this.sku_id) {
        //        itemSkuData.removeValue(this);
        //        $(sender).parent().parent().remove()
        //        return false;
        //    }
        //}

        if (this.sku_id) {           
            if (sku_id == this.sku_id) {
                itemSkuData.removeValue(this);
                $(sender).parent().parent().remove();
                fnSaveOne(this, "del");
                return false;
            }            
        } else {
            if (index == this.index) {
                itemSkuData.removeValue(this);
                $(sender).parent().parent().remove()
                return false;
            }
        }

    });

    

}

//给数组写扩展方法，删除
Array.prototype.remove = Array.prototype.removeValue = function () {
    var what, a = arguments, L = a.length, ax;
    while (L && this.length) {
        what = a[--L];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};


function createSysMenuTree(cid) {
    var _ctrl, _store;
    _store = new Ext.data.TreeStore({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=sys_menu',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "menu_Id" },
            { name: "text", mapping: "menu_Name" },
            { name: "leaf", mapping: "leaf_flag", defaultValue: true },
            { name: "expanded", mapping: "expanded_flag", defaultValue: true },
            { name: "cls", mapping: "cls_flag", defaultValue: "" },
            { name: "checked", mapping: "check_flag", defaultValue: false }
        ]
    });

    _ctrl = Ext.create('Ext.tree.Panel', {
        id: cid,
        store: _store,
        rootVisible: false,
        useArrows: true,
        frame: false,
        title: '',
        renderTo: cid,
        width: 200,
        height: 250
    });

    return { store: _store, ctrl: _ctrl };
}


function createCitySelectTree(cid, hid, sid) {
    var _ctrl = Ext.create('TreeComboBox', {
        renderTo: cid,
        hid: hid,
        sid: sid,
        width: defaultCtrlWidth,
        id_key: 'city_Code',
        text_key: 'city_Name',
        url: '/Controls/Data.aspx?data_type=city_query_by_city_code'
    });

    return { store: _ctrl.store, ctrl: _ctrl };
}

function createUnitTypeSelect(cid, type) {
    var _ctrl, _store;
    _store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            url: '/Controls/Data.aspx?data_type=get_unit_type_list',
            headers: { 'Content-type': 'application/json' },
            reader: {
                type: 'json',
                root: 'data'
            }
        }),
        fields: [
            { name: "id", mapping: "type_Id" },
            { name: "text", mapping: "type_Name" }
        ]
    });

    _ctrl = Ext.create('Ext.form.field.ComboBox', {
        id: cid,
        renderTo: cid,
        store: _store,
        valueField: 'id',
        displayField: 'text',
        minChars: 1,
        width: 100,
        labelWidth: 100,
        typeAhead: false,
        hideLabel: true,
        hideTrigger: false,
        anchor: '100%',
        editable: false,
        listConfig: {
            loadingText: '正在加载数据...',
            emptyText: '未查找到数据',
            getInnerTpl: function () {
                return '<div>{text}</div>';
            }
        },
        listeners: {
            select: function (combo, record, index) {
                try {
                    var hctrl = get(cid + "_hide");
                    if (hctrl != null) {
                        hctrl.value = this.value;
                    }
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            },
            expand: function () {
                loadCurUser();
            }
        }
    });

    return { store: _store, ctrl: _ctrl };
}

function setPropReadOnly(id, readonly) {
    var dis = readonly ? "disabled" : "";
    //var list = $("#" + id + " .input");
    if (true) {
        var list = get(id).getElementsByTagName("input");
        for (var i in list) {
            list[i].readOnly = readonly;
            //list[i].disabled = dis;
            //if (list[i].type == "radio") {
            list[i].disabled = readonly;
            //}
        }
    }
    if (true) {
        var list = get(id).getElementsByTagName("select");
        for (var i in list) {
            list[i].disabled = dis;
        }
    }
    //if (true) {
    //    var list = get(id).getElementsByTagName("radio");
    //    for (var i in list) {
    //        //list[i].readOnly = readonly;
    //        list[i].disabled = readonly;
    //    }
    //}
    if (true) {
        //var list = get(id).getElementsByTagName("div");
        //for (var i in list) {
        //    if (list[i].attributes["name"] == "_z_prop_date") {
        //        list[i].disabled = "disabled";
        //    }
        //}
    }
}


fnGoto1 = function () {
    location.href = "Event.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}
fnGoto2 = function () {
    location.href = "EventTime.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}
fnGoto3 = function () {
    location.href = "MarketStore.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}
fnGoto4 = function () {
    location.href = "MarketPerson.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}
fnGoto5 = function () {
    location.href = "MarketTemplate.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}


fnECShowSearch = function () {
    var cusSearch = get("cusECSearch");
    if (cusSearch.style.display == "none")
        cusSearch.style.display = "";
    else
        cusSearch.style.display = "none";
}
fnECCloseSearch = function () {
    get("cusECSearch").style.display = "none";
}
fnECSearchCustomerGo = function () {
    var name = Ext.getCmp("tbECSearchCustomerName").jitGetValue();
    Ext.Ajax.request({
        url: "/Module/CRM/Handler/CRMHandler.ashx?method=ECSearch",
        params: { name: name },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            get("pnlECSearchCustomer").innerHTML = response.responseText;
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}
fnECSearchCustomerClear = function () {
    Ext.getCmp("tbECSearchCustomerName").jitSetValue("");
    get("pnlECSearchCustomer").innerHTML = "";
}
fnECSelect = function (id, name) {
    get("hECCustomerId").value = id;
    Ext.getCmp("txtEnterpriseCustomerId").setValue(getStr(name));
    fnECCloseSearch();
}

fnChangePwd = function () {
    if ($("#padIframe").length == 0) {
        $("body").append('<iframe src="../../Module/changePWD/changePWD.html" width="100%" id="padIframe" height="100%" style="position: absolute; top: 0; left: 0;z-index:9999999" ></iframe>');
    }
}
fnStatement = function () {
    location.href = "/module/statementList/querylist.aspx?mid=1";
}


var toScrollFrame = function (iFrame, mask) {
    if (!navigator.userAgent.match(/iPad|iPhone/i))
        return false;
    //do nothing if not iOS devie  

    var mouseY = 0;
    var mouseX = 0;
    jQuery(iFrame).ready(function () {//wait for iFrame to load  
        //remeber initial drag motition  
        jQuery(iFrame).contents()[0].body.addEventListener('touchstart', function (e) {
            mouseY = e.targetTouches[0].pageY;
            mouseX = e.targetTouches[0].pageX;
        });

        //update scroll position based on initial drag position  
        jQuery(iFrame).contents()[0].body.addEventListener('touchmove', function (e) {
            e.preventDefault();
            //prevent whole page dragging  

            var box = jQuery(mask);
            box.scrollLeft(box.scrollLeft() + mouseX - e.targetTouches[0].pageX);
            box.scrollTop(box.scrollTop() + mouseY - e.targetTouches[0].pageY);
            //mouseX and mouseY don't need periodic updating, because the current position  
            //of the mouse relative to th iFrame changes as the mask scrolls it.  
        });
    });

    return true;
};

toScrollFrame('.myFrame', '.myMask');


/*键值对事件相关begin*/
function getKeyValueJDATA(id) {
    var jdata = "[]";
    var keyArray = new Array();
    var valueArray = new Array();
    var checkStatus = true;

    $.each($("input[name='" + id + "keyvalue']"), function (i, obj) {
        if (i % 2 == 0 || i == 0) {//key
            if ($(obj).val() != "") {
                if (arrayContainsKey(keyArray, $(obj).val())) {
                    alert("一个键值对里不能填写相同的key值");
                    checkStatus = false;
                    return false;
                }
                else {
                    keyArray.push($(obj).val());
                }
            }
        }
        else { //value
            valueArray.push($(obj).val());
        }
    });
    if (keyArray.length > 0) { //当填写了key值的情况
        if (keyArray.length != valueArray.length) {
            alert("key值不能为空");
            checkStatus = false;
            return false;
        }
        else {

            jdata = "[";
            for (var i = 0; i < keyArray.length; i++) {
                if (true) {

                }
                jdata += "{\"Name\":\"" + ckstr(keyArray[i]) + "\",";
                jdata += "\"Value\":\"" + ckstr(valueArray[i]) + "\"},";
            }
            jdata = jdata.substr(0, jdata.length - 1);
            jdata += "]";
        }
    }
    if (checkStatus) {
        return jdata;
    }
    else {
        return false;
    }
}

function ckstr(s) {
    
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&mdash;—|{}[]';:'。，、？]")
    var rs = "";
    for (var i = 0; i < s.length; i++) {
        rs = rs + s.substr(i, 1).replace(pattern, '').replace('"', '').replace('[', '').replace(']').replace('/', '').replace('\\', '');
    }
    return rs;
}
function createKeyValue(id, defaultValue) {
    $("#" + id).append("<table id='" + id + "_table' rules='none' border='1' border-color='blanchedalmond'  cellpadding='0' cellspacing='0' width=100%></table>");
    addKeyValueTopRow(id, "", "");
}
function createKeyValueByJDATA(id, data) {
    $("#" + id + "_table").remove();
    $("#" + id).append("<table id='" + id + "_table' rules='none' border='1' border-color='blanchedalmond' cellpadding='0' cellspacing='0'></table>");
    
    try {
        jdata = eval(data);
    } catch (e) {
        jdata = null;
    }
    if (jdata != undefined && jdata != null && jdata.length > 0 && jdata != "[]") {
        //jdata = jdata[0];
        jdata = $.parseJSON(jdata)
        var count = 0;
        $.each(jdata, function (i) {
            
            if (count == 0) {
                addKeyValueTopRow(id, jdata[i].Name, jdata[i].Value);
            }
            else {
                addKeyValueRow(id, jdata[i].Name, jdata[i].Value);
            }
            count++;
        });
    }
    else {
        addKeyValueTopRow(id, "", "");
    }
}

//添加第一行
function addKeyValueTopRow(id, key, value) {
    
    $("#" + id + "_table").append("<tr style=\"line-height: 1\"><td><font style='font-size:16px;cursor:pointer;margin-right: 10px'>名&nbsp&nbsp</font><input style='width:140px;border: 1px solid #cecedc;margin-right:10px;height:21px' type='text' name='" + id + "keyvalue' value='" + key + "'></td><td><font style='font-size:16px;cursor:pointer;margin-right: 10px'>值&nbsp&nbsp</font><input style='width:140px;border: 1px solid #cecedc;margin-right:10px;height:21px' type='text' name='" + id + "keyvalue' value='" + value + "'></td><td onclick=\"addKeyValueRow('" + id + "','','')\"><font style='font-size:23px;cursor:pointer;'>&nbsp+</font></td></tr>");
}

//添加下面的删除行
function addKeyValueRow(id, key, value) {
    $("#" + id + "_table").append("<tr style=\"line-height:1\"><td><font style='font-size:16px;cursor:pointer;margin-right: 10px'>名&nbsp&nbsp</font><input style='width:140px;border: 1px solid #cecedc;margin-right:10px;height:21px' type='text' name='" + id + "keyvalue' value='" + key + "'></td><td><font style='font-size:16px;cursor:pointer;margin-right: 10px'>值&nbsp&nbsp</font><input style='width:140px;border: 1px solid #cecedc;margin-right:10px;height:21px' type='text' name='" + id + "keyvalue' value='" + value + "'></td><td onclick=\"delKeyValueRow(this)\"><font style='font-size:28px;cursor:pointer;'>&nbsp-</font></td></tr>");
}

//删除行
function delKeyValueRow(obj) {
    $(obj).parent().remove();
}

function arrayContainsKey(array, key) {
    var res = false;
    for (var i = 0; i < array.length; i++) {
        if (array[i] == key) {
            res = true;
            return res;
        }
    }
}
/*键值对事件相关end*/