Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    
    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 4
                },
        renderTo: 'span_panel',
        padding: '0 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        
        {
            xtype: 'panel',
            colspan: 2,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            bodyStyle: 'background:#F1F2F5;',
            width: 200,
            id: 'txtPublishTime',
            items: [{
                xtype: "jitdatefield",
                fieldLabel: "达人时间",
                id: "txtWorkDate",
                name: "WorkDate",
                jitSize: 'small'
            }
            ]
        },
        {
            xtype: "jittextfield",
            fieldLabel: "达人作者",
            id: "txtCreative",
            name: "Creative",
            jitSize: 'small'
        },
        {
            xtype: "jitbizdarentype",
            fieldLabel: "分类",
            id: "txtCategory",
            name: "Category",
            jitSize: 'small'
        }
        ],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    
    // btn_panel
    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        //width: 200,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "筛选",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: function() {
                fnSearch(1);
            }
        }
        //, {
        //    xtype: "jitbutton",
        //    id: "btnMoreSearchView",
        //    text: "高级查询",
        //    margin: '0 0 10 14',
        //    handler: fnMoreSearchView
        //}
        ]

    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        //,imgName: 'create'
        //,isImgFirst: true
        //,hidden: __getHidden("create")
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "围观达人名单",
        renderTo: "span_create2",
        width:120,
        handler: function() { fnView2(); }
        //,imgName: 'create'
        //,isImgFirst: true
        //,hidden: __getHidden("create")
        //, jitIsHighlight: true
        //, jitIsDefaultCSS: true
    });

    fnSearch(1);
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "LEventsEntriesEdit",
        title: "内容",
        url: "LEventsEntriesEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch(page, t) {
    if (page == undefined || page == null || page <= 0) page = 1;
    if (typeof(page) == "string") {
        if (page == "p") {
            page = Math.max(parseInt(get("hPage").value) - 1, 1);
        }
        else if (page == "n") {
            page = Math.min(parseInt(get("hPage").value) + 1, parseInt(get("hPageCount").value));
        }
    } 
    if (t && get("hPage") != null && page == Math.max(parseInt(get("hPage").value), 1)) return;
    var WorkDate = Ext.getCmp("txtWorkDate").jitGetValueText();
    var Creative = Ext.getCmp("txtCreative").jitGetValue();
    var Category = Ext.getCmp("txtCategory").jitGetValue();
    var EventId = "AC1DFF316EE44E72B11BB416A641E726";

    Ext.Ajax.request({
        url: "/Module/Daren/Handler/LEventsEntriesHandler.ashx?method=LEventsEntries_query",
        params: {
            WorkDate: WorkDate,
            Creative: Creative,
            Category: Category,
            page: page,
            EventId: EventId
        },
        method: 'post',
        success: function (response) {
            var d = response.responseText;
            get("grid").innerHTML = d;
            var totalCount = parseInt(get("hTotalCount").value);
            var pageCount = parseInt(get("hPageCount").value);
            var pageStr = "";
            pageStr += "<a href=\"#\" onclick=\"fnSearch('p',1);return false;\"><span class=\"DaRenIcnSkin DaRenIcnPrev\"></span></a>";
            for (var i = 1; i <= pageCount; i++) {
                if (i == page) pageStr += "<a href=\"#\" class=\"DaRenPageCur\" onclick=\"fnSearch(" + i + ",1);return false;\">" + i + "</a>";
                else pageStr += "<a href=\"#\" onclick=\"fnSearch(" + i + ",1);return false;\">" + i + "</a>";
            }
            pageStr += "<a href=\"#\" onclick=\"fnSearch('n',1);return false;\"><span class=\"DaRenIcnSkin DaRenIcnNext\"></span></a>";
            get("page").innerHTML = pageStr;
            get("hPage").value = page;
            if (parseInt(get("hTotalCount").value) > 0) get("nodata").style.display = "none";
            else get("nodata").style.display = "";
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取数据失败");
        }
    });
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "LEventsEntriesEdit",
        title: "内容",
        url: "LEventsEntriesEdit.aspx?EntriesId=" + id
    });

    win.show();
}

function fnDelete(id) {
    
}


function fnSave(id, check) {
    var news = {};
    news.EntriesId = id;
    news.IsWorkDaren = check ? 1 : 0;


    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/LEventsEntriesHandler.ashx?method=LEventsEntries_save&id=' + news.EntriesId,
        params: {
            "item": Ext.encode(news)
        },
        success: function (result, request) {
            //var d = Ext.decode(result.responseText);
            //if (d.success == false) {
            //    showError("保存数据失败：" + d.msg);
            //    flag = false;
            //} else {
            //    showSuccess("保存数据成功");
            //    flag = true;
            //    parent.fnSearch();
            //}
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    //if (flag) fnClose();
}

function fnSave2(id, check) {
    var news = {};
    news.EntriesId = id;
    news.IsMonthDaren = check ? 1 : 0;


    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/LEventsEntriesHandler.ashx?method=LEventsEntries_save&id=' + news.EntriesId,
        params: {
            "item": Ext.encode(news)
        },
        success: function (result, request) {
            //var d = Ext.decode(result.responseText);
            //if (d.success == false) {
            //    showError("保存数据失败：" + d.msg);
            //    flag = false;
            //} else {
            //    showSuccess("保存数据成功");
            //    flag = true;
            //    parent.fnSearch();
            //}
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    //if (flag) fnClose();
}

function fnView2(id) {
    if (id == undefined || id == null) id = "";
    var date = Ext.getCmp("txtWorkDate").jitGetValueText();
    //if (date == null || date.length == 0) {
    //    var d = new Date();
    //    date = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    //}
    var win = Ext.create('jit.biz.Window', { 
        jitSize: "large",
        height: 550,
        id: "LEventsEntriesComment",
        title: "围观达人",
        url: "LEventsEntriesComment.aspx?EventId=AC1DFF316EE44E72B11BB416A641E726&IsCrowdDaren=1&date=" + date
    });
    win.show();
}
function fnView3(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "LEventsEntriesComment",
        title: "围观达人评论",
        url: "LEventsEntriesComment.aspx?EventId=AC1DFF316EE44E72B11BB416A641E726&EntriesId=" + id
    });

    win.show();
}


