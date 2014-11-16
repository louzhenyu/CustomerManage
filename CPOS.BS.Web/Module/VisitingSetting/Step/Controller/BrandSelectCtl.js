var id = "";
var btncode = JITMethod.getUrlParam("btncode");

Ext.onReady(function () {
    //加载需要的文件
    JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/StepHandler.ashx?mid=" + __mid);

    InitVE();
    InitStore();
    Ext.getStore("typeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetStepBrandType";
    Ext.getStore("typeStore").load();

    InitView();
    //页面加载
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    id = JITMethod.getUrlParam("id");
    if (id != null && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetStepLevel",
            params: {
                stepid: id
            },
            method: 'post',
            success: function (response) {
                if (response.responseText != "") {
                    Ext.getCmp("comboType").setValue(response.responseText);
                }
                else {
                    Ext.getCmp("comboType").setValue(Ext.getStore("typeStore").first().data.value);
                }
            },
            failure: function () {
            }
        });
    };
});


function fnSearch(id) {

    CheckBoxModel.jitClearValue();
    CheckBoxModel.deselectAll();
    Ext.getStore("brandStore").removeAll();
    Ext.getCmp("gridView").reconfigure(Ext.getStore("brandStore"), gridCloumn);

    // 重新加载数据
    store = Ext.getCmp("gridView").getStore();
    Ext.getCmp("pageBar").bind(store);

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetStepBrandList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.getCmp("searchPanel").getValues(),
        id: id
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnSave() {
    var v = CheckBoxModel.jitGetValue();

    var btn = this;
    btn.setDisabled(true);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditStepObject_Brand",
        params: {
            id: id,
            type: Ext.getCmp("comboType").getValue(),
            allSelectorStatus: v.allSelectorStatus,
            defaultList: v.defaultList.toString(),
            includeList: v.includeList.toString(),
            excludeList: v.excludeList.toString()
        },
        method: 'post',
        success: function (response) {
            Ext.Msg.alert("提示", "操作成功");
            btn.setDisabled(false);
            fnSearch(id);
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            btn.setDisabled(false);
        }
    });
}

function fnTypeChange() {
    var typeTitle = this.getRawValue().split('+');
    var type = this.getValue().split(',');
    var brandLevel = parseInt(type[0]);
    var categoryLevel = parseInt(type[1]);

    if (brandLevel == 1) {
        //品牌
        if (categoryLevel == 0) {
            gridCloumn = [{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'BrandName',
                align: 'left'
            }, {
                text: '自有品牌',
                width: 110,
                sortable: true,
                dataIndex: 'IsOwner',
                align: 'left',
                renderer: fnColumnBrand
            }, {
                text: '客户名称',
                width: 110,
                sortable: true,
                dataIndex: 'Firm',
                align: 'left'
            }, {
                text: '备注',
                width: 110,
                sortable: true,
                dataIndex: 'Remark',
                align: 'left'
            }];
        }
        else if (categoryLevel >= 1) {
            //品类
            gridCloumn = [{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'CategoryName',
                align: 'left'
            }, {
                text: typeTitle[1],
                width: 110,
                sortable: true,
                dataIndex: 'BrandName',
                align: 'left'
            }, {
                text: '自有品牌',
                width: 110,
                sortable: true,
                dataIndex: 'IsOwner',
                align: 'left',
                renderer: fnColumnBrand
            }, {
                text: '客户名称',
                width: 110,
                sortable: true,
                dataIndex: 'Firm',
                align: 'left'
            }, {
                text: '备注',
                width: 110,
                sortable: true,
                dataIndex: 'Remark',
                align: 'left'
            }];
        }

    }
    else if (brandLevel > 1) {
        //子品牌
        if (categoryLevel == 0) {
            gridCloumn = [{
                text: '上级品牌名称',
                width: 110,
                sortable: true,
                dataIndex: 'ParentBrandName',
                align: 'left'
            }, {
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'BrandName',
                align: 'left'
            }, {
                text: '自有品牌',
                width: 110,
                sortable: true,
                dataIndex: 'IsOwner',
                align: 'left',
                renderer: fnColumnBrand
            }, {
                text: '客户名称',
                width: 110,
                sortable: true,
                dataIndex: 'Firm',
                align: 'left'
            }, {
                text: '备注',
                width: 110,
                sortable: true,
                dataIndex: 'Remark',
                align: 'left'
            }];
        }
        else if (categoryLevel >= 1) {
            //品类
            gridCloumn = [{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'CategoryName',
                align: 'left'
            }, {
                text: typeTitle[1],
                width: 110,
                sortable: true,
                dataIndex: 'BrandName',
                align: 'left'
            }, {
                text: '上级品牌名称',
                width: 110,
                sortable: true,
                dataIndex: 'ParentBrandName',
                align: 'left'
            }, {
                text: '自有品牌',
                width: 110,
                sortable: true,
                dataIndex: 'IsOwner',
                align: 'left',
                renderer: fnColumnBrand
            }, {
                text: '客户名称',
                width: 110,
                sortable: true,
                dataIndex: 'Firm',
                align: 'left'
            }, {
                text: '备注',
                width: 110,
                sortable: true,
                dataIndex: 'Remark',
                align: 'left'
            }];
        }

    }
    fnSearch(id);
}

/*
*验证是否自有品牌
*/
function fnColumnBrand(v) {
    var html = "是";
    if (v.toString() == 1) {
        html = "否";
    }
    return html;
}