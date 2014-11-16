var id = JITMethod.getUrlParam("id");
var btncode = JITMethod.getUrlParam("btncode");
var urlparams = window.location.search;
var pageLanguage = new Object();

var pnlSearch; //查询pannel
var pnlWork; //操作pannel

var btnAdd; //增加

var gridStoreList; //终端数据表
var cellEditing;
var checkStore;
var selModelOrder;

var editMethod;

var callBack = {
    saveCallBack: function () { }
};

function fnSearch() {

    if (gridStoreList == undefined) {
        return;
    }
    //同步数据
    for (var i = 0; i < gridStoreList.getStore().getCount(); i++) {

        if (gridStoreList.getStore().data.items[i].data.MappingID != "") {

            if (checkStore.getById(gridStoreList.getStore().data.items[i].data.StoreID) != null) {

            }
            else {
                checkStore.insert(0, gridStoreList.getStore().data.items[i].data);
            }
        }
    }

    //选中数据
    for (var i = 0; i < gridStoreList.getStore().getCount(); i++) {
        if (checkStore.getById(gridStoreList.getStore().data.items[i].data.StoreID) != null
                && checkStore.getById(gridStoreList.getStore().data.items[i].data.StoreID).data.IsDelete == 0) {
            gridStoreList.getStore().getAt(i).set("Sequence", checkStore.getById(gridStoreList.getStore().data.items[i].data.StoreID).data.Sequence);
            selModelOrder.select(gridStoreList.getStore().data.items[i], true);
        }
    }
}

function fnSave() {
    var btn = this;
    btn.setDisabled(true);

    //new
    var array = new Array();
    if (checkStore.getCount() > 0) {
        for (var i = 0; i < checkStore.getCount(); i++) {
            if (checkStore.data.items[i].data.MappingID == ""
                    && checkStore.data.items[i].data.IsDelete == 1) {
            }
            else {
                var b = new Object();
                b.MappingID = checkStore.data.items[i].data.MappingID;
                b.StoreID = checkStore.data.items[i].data.StoreID;
                b.Sequence = checkStore.data.items[i].data.Sequence;
                b.IsDelete = checkStore.data.items[i].data.IsDelete;
                array.push(b);
            }
        }
    }

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue()
                                    + "&btncode=" + btncode + "&method=" + editMethod,
        params: {
            id: id,
            form: Ext.JSON.encode(array)
        },
        method: 'post',
        success: function (response) {
            Ext.Msg.alert("提示", "操作成功");
            btn.setDisabled(false);
            checkStore.removeAll();
            gridStoreList.pagebar.moveFirst();
            callBack.saveCallBack();
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            btn.setDisabled(false);
        }
    });
}