var K;
var htmlEditor;

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/AlbumHandler.ashx?mid=");

    fnSearch();

    Ext.getCmp("gridView").getStore().on('load', setTdCls); //设置表格加载数据完毕后，更改表格TD样式为垂直居中  
});

function setTdCls() {
    var gridJglb = document.getElementById("gridView");
    var tables = gridJglb.getElementsByTagName("table"); //找到每个表格  
    for (var k = 0; k < tables.length; k++) {
        var tableV = tables[k];
        if (tableV.className == "x-grid-table x-grid-table-resizer") {
            var trs = tables[k].getElementsByTagName("tr"); //找到每个tr  
            for (var i = 0; i < trs.length; i++) {
                var tds = trs[i].getElementsByTagName("td"); //找到每个TD  
                for (var j = 1; j < tds.length; j++) {
                    tds[j].style.cssText = "width:202px;text-align:center;line-height:130px;vertical-align:middle;";
                }
            }
        };
    }
}

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 350,
        id: "AlbumImagesEdit",
        title: "相片信息",
        url: "AlbumImagesEdit.aspx?AlbumId=" + getStr(getUrlParam("AlbumId"))
    });

    win.show();
}

function fnView(id) {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 350,
        id: "AlbumImagesEdit",
        title: "相片信息",
        url: "AlbumImagesEdit.aspx?PhotoId=" + id + "&AlbumId=" + getStr(getUrlParam("AlbumId"))
    });

    win.show();
}

function fnSearch() {
    Ext.getStore("AlbumImagesStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=album_image_query";
    Ext.getStore("AlbumImagesStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("AlbumImagesStore").proxy.extraParams = {
        AlbumId: getStr(getUrlParam("AlbumId"))
    };
    Ext.getStore("AlbumImagesStore").load();
}

function fnClose() {
    CloseWin('AlbumImages');
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "PhotoId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=album_image_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "PhotoId"
            }),
            AlbumId: getUrlParam("AlbumId")
        },
        handler: function () {
            Ext.getStore("AlbumImagesStore").load();
        }
    });
}

