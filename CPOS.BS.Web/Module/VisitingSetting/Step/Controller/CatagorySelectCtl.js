var id = "";
var btncode = JITMethod.getUrlParam("btncode");

Ext.onReady(function () {
    //加载需要的文件
    //页面加载
    JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/StepHandler.ashx?mid=" + __mid);

    InitVE();
    InitStore();
    Ext.getStore("typeStore").proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetStepCategoryType";
    Ext.getStore("typeStore").load();

    InitView();

    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    id = JITMethod.getUrlParam("id");
    if (id != null && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetStepLevel",
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

    Ext.getStore("categoryStore").removeAll();
    Ext.getCmp("gridView").reconfigure(Ext.getStore("categoryStore"), gridCloumn);

    // 重新加载数据
    store = Ext.getCmp("gridView").getStore();
    Ext.getCmp("pageBar").bind(store);

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetStepCategoryList";
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
        url: JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=EditStepObject_Category",
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
    //alert(this.getValue());
    var typeTitle = this.getRawValue().split('+');
    var type = this.getValue().split(',');
    var brandLevel = parseInt(type[1]);
    var categoryLevel = parseInt(type[0]);

    if (categoryLevel == 1) {
        //品类
        if (brandLevel == 0) {
            gridCloumn = [{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'CategoryName',
                align: 'left'
            }, {
                text: '备注',
                width: 110,
                sortable: true,
                dataIndex: 'Remark',
                align: 'left'
            }];
        }
        else if (brandLevel >= 1) {
            //品牌
            gridCloumn = [{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'BrandName',
                align: 'left'
            }, {
                text: typeTitle[1],
                width: 110,
                sortable: true,
                dataIndex: 'CategoryName',
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
    else if (categoryLevel > 1) {
        //子品类
        if (brandLevel == 0) {
            gridCloumn = [ {
                text: '上级品类名称',
                width: 110,
                sortable: true,
                dataIndex: 'ParentCategoryName',
                align: 'left'
            },{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'CategoryName',
                align: 'left'
            }, {
                text: '备注',
                width: 110,
                sortable: true,
                dataIndex: 'Remark',
                align: 'left'
            }];
        }
        else if (brandLevel >= 1) {
            //子品牌
            gridCloumn = [{
                text: typeTitle[0],
                width: 110,
                sortable: true,
                dataIndex: 'BrandName',
                align: 'left'
            },  {
                text: typeTitle[1],
                width: 110,
                sortable: true,
                dataIndex: 'CategoryName',
                align: 'left'
            }, {
                text: '上级品类名称',
                width: 110,
                sortable: true,
                dataIndex: 'ParentCategoryName',
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