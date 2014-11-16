function InitView() {

    //editPanel area
    //Ext.create('Jit.form.field.Date', {
    //    id: "txtCreateTime",
    //    text: "",
    //    renderTo: "txtCreateTime",
    //    width: 100
    //});


    Ext.create('Jit.form.field.Text', {
        id: "txtVipName",
        text: "",
        renderTo: "txtVipName",
        readOnly: true,
        width: 420
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtPraiseCount",
        text: "",
        renderTo: "txtPraiseCount",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCreateTime",
        text: "",
        renderTo: "txtCreateTime",
        readOnly: true,
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtPass",
        text: "",
        renderTo: "txtPass",
        readOnly: true,
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtLotteryCode",
        text: "",
        renderTo: "txtLotteryCode",
        readOnly: true,
        width: 100
    });

    Ext.create('jit.biz.ItemSelect', {
        id: "txtItemCode",
        //text: "",
        width: 420,
        renderTo: "txtItemCode",
        nameId: 'txtItemName',
        fnCallback: function(d) {
            get("hItemId").value = sku_selected_data.item_id;
        }
    });

    
    var content = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent',
        renderTo: "txtContent",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#txtContent', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });


    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            text: "保存",
            formBind: true,
            disabled: true,
            hidden: false,
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}