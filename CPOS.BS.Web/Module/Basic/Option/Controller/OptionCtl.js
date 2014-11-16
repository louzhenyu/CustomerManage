var isFixed=true;
var pDefinedID=0;
var pOptionType = 0;
var btncode = "";
var valuei = 1;

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/OptionHandler.ashx?mid=" + __mid);
    fnSearch();
 
});

function fnReset(){
       Ext.getCmp("searchPanel").getForm().reset();
       fnSearch();
}


function fnSearch(){
    fnActiveSearch();
    fnFixedSearch();
}
function fnActiveSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetListByActive";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("pageBar").moveFirst();

}

function fnFixedSearch(){
    Ext.getCmp("fixedpageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetListByFixed";
    Ext.getCmp("fixedpageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("fixedpageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("fixedpageBar").moveFirst();
}

function fnDelete() {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "DefinedID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            definedID: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "DefinedID" })
        },
        handler: function () {
           fnSearch();
        }
    });
}

function fnColumnUpdate(value, p, r) {
    if (!__getHidden("update")) {
        //有权限
        var DefinedID = r.data.DefinedID;
        var OptionType = r.data.OptionType;
        var OptionName = r.data.OptionName;
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + value + "','" + DefinedID + "','" + OptionType + "','" + OptionName + "','update')\">" + value + "</a>";
    } else if (__getHidden("update") && !__getHidden("search")) {
        var DefinedID = r.data.DefinedID;
        var OptionType = r.data.OptionType;
        var OptionName = r.data.OptionName;
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + value + "','" + DefinedID + "','" + OptionType + "','" + OptionName + "','search')\">" + value + "</a>";
    }
    else {
        //无权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
}

function fnColumnDelete(value, p, r) {
    if (__getHidden("delete")) {
        //无权限
        return "<a href=\"javascript:;\">删除</a>";
    } else {
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
    }
}

function fnColumnOptionDel(value, p, r) {
    if (btncode == "update") {
        if (!__getHidden("update")) {
            if (!(value != null && value != "")) {
                return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnColumnOptionAdd()\">添加</a>";
            } else {

                return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnColumnOptionDelete()\">删除</a>";
            }
        } else {
            if (value != null && value != "") {
                return "<a href=\"javascript:;\">删除</a>";
            } else {
                return "<a href=\"javascript:;\">添加</a>";
            }
        }
    } else if (btncode == "create") {
        if (!__getHidden("create")) {
            if (!(value != null && value != "")) {
                return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnColumnOptionAdd()\">添加</a>";
            } else {

                return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnColumnOptionDelete()\">删除</a>";
            }
        } else {
            if (value != null && value != "") {
                return "<a href=\"javascript:;\">删除</a>";
            } else {
                return "<a href=\"javascript:;\">添加</a>";
            }
        }
    } else {
        if (value != null && value != "") {
            return "<a href=\"javascript:;\">删除</a>";
        } else {
            return "<a href=\"javascript:;\">添加</a>";
        }
    }
}


function fnCreate() {
    btncode = "create";
    Ext.getCmp("btnSave").show();
    valuei = 1;
    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("OptionName").setDisabled(false);
    Ext.getCmp("Title").setDisabled(false);
    Ext.getStore("optionsEditStore").removeAll();
    pDefinedID=0;
    pOptionType=3;
    fnColumnOptionCreate();
    Ext.getCmp("editWin").show();
}


function fnUpdate(name, pdefinedID, poptionType, pOptionName,bcode) {
    btncode = bcode;
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

     var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();
    if (pOptionType == 2) {       
          Ext.getCmp("Title").setDisabled(true);
          Ext.getCmp("OptionName").setDisabled(true);
    } else if (pOptionType == 3) {   
        Ext.getCmp("Title").setDisabled(false);
        Ext.getCmp("OptionName").setDisabled(true);
    }
  
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetOptionByName",
        params: { 
        name: pOptionName,
        definedID:pdefinedID
        },
        method: 'post',
        success: function (response) {
            var jdata = eval(response.responseText);
            //加载form
            Ext.getCmp("OptionName").setValue(pOptionName);  
            Ext.getCmp("Title").setValue(name);        
            pDefinedID=pdefinedID;
            pOptionType=poptionType;
            //加载grid
            Ext.getStore("optionsEditStore").removeAll();
            Ext.getStore("optionsEditStore").add(jdata);
            valuei = jdata.length + 1;
             var r = Ext.create('OptionsEntity', {
                        OptionText: '',
                        OptionValue: null
                    });
            Ext.getStore("optionsEditStore").insert(Ext.getStore("optionsEditStore").getCount(), r);   
            Ext.getCmp("editWin").show();
             myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
             myMask.hide();
        }
    });
}

function fnColumnOptionDelete() {
    var sm = Ext.getCmp("optionGrid").getSelectionModel();
    Ext.getStore("optionsEditStore").remove(sm.getSelection());   
    valuei--;
}

function fnColumnOptionCreate() {
    var r = Ext.create('OptionsEntity', {
        OptionText: '',
        OptionValue: 0
    });
    Ext.getStore("optionsEditStore").insert((Ext.getStore("optionsEditStore").getCount() - 1), r);   
}

function fnColumnOptionAdd() {
    var r = Ext.create('OptionsEntity', {
        OptionText: '默认数据',
        OptionValue: valuei
    });
    Ext.getStore("optionsEditStore").insert((Ext.getStore("optionsEditStore").getCount() - 1), r);
    valuei++;
}


function fnSubmit() {
    var form = Ext.getCmp('editPanel').getForm();
    if (Ext.getCmp("Title").getValue() == null || Ext.getCmp("Title").getValue() == "") {
        Ext.Msg.show({
            title: '提示',
            msg: '"标题"不能为空',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO,
            fn:function(){
             Ext.getCmp("Title").focus(true, 10); 
            }
        });
        return false;
    }
    if (Ext.getCmp("OptionName").getValue() == null || Ext.getCmp("OptionName").getValue() == "") {
        Ext.Msg.show({
            title: '提示',
            msg: '"名称"不能为空',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO,
            fn:function(){
             Ext.getCmp("OptionName").focus(true, 10); 
            }
        });
        return false;
    }
    if (!form.isValid()) {
        return false;
    }
    var oplist = [];
    for (var i = 0; i < Ext.getStore("optionsEditStore").getCount(); i++) {
        if (Ext.getStore("optionsEditStore").getAt(i).data.OptionText != null && Ext.getStore("optionsEditStore").getAt(i).data.OptionText != "") {
            oplist.push(Ext.getStore("optionsEditStore").getAt(i).data);
        }
    }
    if (oplist.length == 0) {
        Ext.Msg.show({
            title: '提示',
            msg: "请增加选项",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });         
        return false;
    }


    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=Edit",
        waitTitle:JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            options: Ext.JSON.encode(oplist),
            OptionName: Ext.getCmp("OptionName").getValue(),
            Title:Ext.getCmp("Title").getValue(),
            DefinedID:pDefinedID,
            OptionType:pOptionType
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });                
                fnSearch();
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

function fnCancel() {
    Ext.getCmp("editWin").hide();
}