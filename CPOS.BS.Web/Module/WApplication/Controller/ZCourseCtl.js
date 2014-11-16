var tabs3;
var course;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");

    htmlEditor.html('');
    htmlEditor.sync();
    
    get("pnlInfo").style.display = "none";
    //fnSearch();

    //Ext.getCmp("gv2").getStore().on('load', setTdCls); //设置表格加载数据完毕后，更改表格TD样式为垂直居中  
    //Ext.getCmp("gv3").getStore().on('load', setTdCls2); //设置表格加载数据完毕后，更改表格TD样式为垂直居中  
});

function setTdCls() {
    var gridJglb = document.getElementById("grid2");
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

function setTdCls2() {
    var gridJglb = document.getElementById("grid3");
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

fnSearch = function() {
    //var ApplicationId = Ext.getCmp("txtApplicationId").getValue();
    //if (ApplicationId == null || ApplicationId == "") {
    //    alert("请选择微信账号");
    //    return;
    //}
    //var ZCourseId = Ext.getCmp("txtZCourse").getValue();
    var ZCourseId = Ext.getCmp("txtZCourse").jitGetValue();
    if (ZCourseId == null || ZCourseId == "") {
        get("pnlSave").style.display = "none";
        get("pnlInfo").style.display = "none";
        alert("请选择课程");
        return;
    }
    get("pnlSave").style.display = "";
    get("pnlInfo").style.display = "";

    get("hCourseId").value = ZCourseId;
    
    //Ext.getCmp("btnSearch").setText("查询中...");
    //Ext.getCmp("btnSearch").disable(false);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=get_ZCourse_by_id",
        params: { id: ZCourseId },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText).data;
            //get("MaterialId").value = getStr(d.MaterialId);
            course = d;
            fnLoadItems1();
            fnLoadItems2();
            fnLoadItems3();
            fnLoadCourseApply();
            fnLoadCourseReflections();
            fnLoadNews();
            
            htmlEditor4.html('');
            htmlEditor4.insertHtml(getStr(course.CourseStartTime));

            htmlEditor5.html('');
            htmlEditor5.insertHtml(getStr(course.CouseCapital));
            
            htmlEditor6.html('');
            htmlEditor6.insertHtml(getStr(course.CouseContact));

            //Ext.getCmp("txtCourseStartTime").setValue(getStr(d.CourseStartTime));
            //Ext.getCmp("txtCouseCapital").setValue(getStr(d.CouseCapital));
            //Ext.getCmp("txtCouseContact").setValue(getStr(d.CouseContact));
            Ext.getCmp("txtEmail").setValue(getStr(d.Email));
            Ext.getCmp("txtEmailTitle").setValue(getStr(d.EmailTitle));

            get('tabInfo').focus();
            //Ext.getCmp("btnSearch").setText("查询");
            //Ext.getCmp("btnSearch").enable(true);
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
            Ext.getCmp("btnSearch").setText("查询");
        }
    });
}
fnReset = function() {
    
}

fnLoadItems1 = function() {
    if (course != null) {
        htmlEditor.html('');
        htmlEditor.insertHtml(getStr(course.CouseDesc));
    }
}
fnLoadItems2 = function() {
    if (course != null) {
        htmlEditor2.html('');
        htmlEditor2.insertHtml(getStr(course.CourseSummary));
    }
}
fnLoadItems3 = function() {
    if (course != null) {
        htmlEditor3.html('');
        htmlEditor3.insertHtml(getStr(course.CourseFee));
    }
}
fnLoadNews = function() {
    var id = get("hCourseId").value;
    if (id == undefined || id == null || id.length == 0) return;
    //var id = "1";
    var store = Ext.getStore("ZCourseNewsStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_ZCourse_news&CourseId=" + id;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            //if (records == null) return;
            //for (var i = 0; i < records.length; i++) {
            //    if (records[i].data.WritingId == MaterialId) {
            //        Ext.getCmp("gv1").getSelectionModel().select(i, true);
            //    }
            //}
        }
    });
}
fnNewsView = function(id) {
    var CourseId = get("hCourseId").value;
    if (id == undefined || id == null) return;
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "NewsEdit",
        title: "新闻内容",
        url: "NewsEdit.aspx?CourseId=" + CourseId + "&NewsId=" + id
    });
    win.show();
}

fnLoadCourseApply = function() {
    var id = get("hCourseId").value;
    if (id == undefined || id == null || id.length == 0) return;
    //var id = "1";
    var store = Ext.getStore("ZCourseApplyStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_ZCourse_Apply&CourseId=" + id;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            //if (records == null) return;
            //for (var i = 0; i < records.length; i++) {
            //    if (records[i].data.WritingId == MaterialId) {
            //        Ext.getCmp("gv1").getSelectionModel().select(i, true);
            //    }
            //}
        }
    });
}

fnLoadCourseReflections = function() {
    var id = get("hCourseId").value;
    if (id == undefined || id == null || id.length == 0) return;
    //var id = "1";
    var store = Ext.getStore("ZCourseReflectionsStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_ZCourse_Reflections&CourseId=" + id;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            //if (records == null) return;
            //for (var i = 0; i < records.length; i++) {
            //    if (records[i].data.WritingId == MaterialId) {
            //        Ext.getCmp("gv1").getSelectionModel().select(i, true);
            //    }
            //}
        }
    });
}
fnViewCourseReflections = function(id) {
    var CourseId = get("hCourseId").value;
    if (id == undefined || id == null) return;
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "CourseReflectionsEdit",
        title: "学员感言",
        url: "CourseReflectionsEdit.aspx?CourseId=" + CourseId + "&ReflectionsId=" + id
    });
    win.show();
}
  

function fnSave() {
    var flag;
    var item = {};
    item.CourseId = get("hCourseId").value;
    item.CouseDesc = htmlEditor.html();
    item.CourseSummary = htmlEditor2.html();
    item.CourseFee = htmlEditor3.html();
    item.CourseStartTime = htmlEditor4.html();
    item.CouseCapital = htmlEditor5.html();
    item.CouseContact = htmlEditor6.html();
    item.Email = Ext.getCmp("txtEmail").jitGetValue();
    item.EmailTitle = Ext.getCmp("txtEmailTitle").jitGetValue();

    if (item.CourseId == null || item.CourseId == "") {
        alert("请先选择课程");
        return;
    }

    //var activeTab = Ext.getCmp("tabs3").getActiveTab();
    //var typeId = Ext.getCmp("tabs3").items.findIndex('id', activeTab.id) + 1;
    //switch (typeId) {
    //    case 1:
    //        var list = Ext.getCmp('gv1').getSelectionModel().getSelection();
    //        if (list == null || list.length == 0) {
    //            alert("请选择素材");
    //            return;
    //        }
    //        item.MaterialTypeId = typeId;
    //        item.MaterialId = list[0].data.WritingId;
    //        break;
    //    case 2:
    //        var list = Ext.getCmp('gv2').getSelectionModel().getSelection();
    //        if (list == null || list.length == 0) {
    //            alert("请选择素材");
    //            return;
    //        }
    //        item.MaterialTypeId = typeId;
    //        item.MaterialId = list[0].data.ImageId;
    //        break;
    //    case 3:
    //        var list = Ext.getCmp('gv3').getSelectionModel().getSelection();
    //        if (list == null || list.length == 0) {
    //            alert("请选择素材");
    //            return;
    //        }
    //        else if (list.length > 10) {
    //            alert("最多只能选择10条数据");
    //            return;
    //        }
    //        item.MaterialTypeId = typeId;
    //        item.MaterialId = list[0].data.TextId;
    //        item.MaterialTextList = [];

    //        for (var i = 0; i < list.length; i++) {
    //            item.MaterialTextList.push(list[i].data);
    //        }

    //        break;
    //    case 4:
    //        var list = Ext.getCmp('gv4').getSelectionModel().getSelection();
    //        if (list == null || list.length == 0) {
    //            alert("请选择素材");
    //            return;
    //        }
    //        item.MaterialTypeId = typeId;
    //        item.MaterialId = list[0].data.VoiceId;
    //        break;
    //    case 5:
    //        break;
    //}
    
    //if (item.MaterialTypeId == null || item.MaterialTypeId == "") {
    //    alert("请选择素材");
    //    return;
    //}
    //if (item.MaterialId == null || item.MaterialId == "") {
    //    alert("请选择素材");
    //    return;
    //}

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=Course_save&CourseId=' + item.CourseId, 
        params: {
            "item": Ext.encode(item)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                //fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

fnAddItem1 = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var ModelId = Ext.getCmp("txtWModel").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请先选择微信账号");
        return;
    }
    if (ModelId == null || ModelId == "") {
        alert("请先选择模块");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialWritingEdit",
        title: "素材",
        url: "WMaterialWritingEdit.aspx?ModelId=" + ModelId + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
fnAddCourseReflections = function() {
    var CourseId = get("hCourseId").value;
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "CourseReflectionsEdit",
        title: "学员感言",
        url: "CourseReflectionsEdit.aspx?CourseId=" + CourseId
    });
	win.show();
}
fnAddNews = function() {
    var CourseId = get("hCourseId").value;
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "NewsEdit",
        title: "新闻",
        url: "NewsEdit.aspx?CourseId=" + CourseId
    });
	win.show();
}
fnAddItem4 = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var ModelId = Ext.getCmp("txtWModel").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请先选择微信账号");
        return;
    }
    if (ModelId == null || ModelId == "") {
        alert("请先选择模块");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialVoiceEdit",
        title: "素材",
        url: "WMaterialVoiceEdit.aspx?ModelId=" + ModelId + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
fnAddItem5 = function() {
    alert("123");
}

function fnView1(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialWritingEdit",
        title: "素材",
        url: "WMaterialWritingEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView2(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialImageEdit",
        title: "素材",
        url: "WMaterialImageEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView3(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialTextEdit",
        title: "素材",
        url: "MaterialTextEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView4(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialVoiceEdit",
        title: "素材",
        url: "WMaterialVoiceEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView5(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialTextEdit",
        title: "素材",
        url: "MaterialTextEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}


function fnDelete1(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv1"), id: "WritingId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialWriting_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv1"), id: "WritingId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem1Store").load();
        }
    });
}
function fnDeleteCourseReflections(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gvCourseReflections"), id: "ReflectionsId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=Course_Reflections_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gvCourseReflections"), id: "ReflectionsId" })
        },
        handler: function () {
            Ext.getStore("ZCourseReflectionsStore").load();
        }
    });
}
function fnDeleteNews(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gvNews"), id: "NewsId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=news_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gvNews"), id: "NewsId" })
        },
        handler: function () {
            Ext.getStore("ZCourseNewsStore").load();
        }
    });
}
function fnDelete4(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv4"), id: "VoiceId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialVoice_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv4"), id: "VoiceId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem4Store").load();
        }
    });
}
function fnDelete5(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv5"), id: "TextId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialVoice_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv5"), id: "TextId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem5Store").load();
        }
    });
}

