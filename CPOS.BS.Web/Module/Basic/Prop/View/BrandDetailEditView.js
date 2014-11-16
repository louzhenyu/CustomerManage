function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 561,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '基本信息'
        }
        , {
            contentEl: 'tabImage',
            title: '上传图片',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabImage");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        ]
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtBrandName",
        text: "",
        renderTo: "txtBrandName",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtBrandCode",
        text: "",
        renderTo: "txtBrandCode",
        width: 100
    });
    
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtBrandEngName",
    //    text: "",
    //    renderTo: "txtBrandEngName",
    //    width: 100
    //});
    
    Ext.create('Jit.form.field.Text', {
        id: "txtTel",
        text: "",
        renderTo: "txtTel",
        width: 100
    });

    
    Ext.create('Jit.form.field.Text', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        value: "1",
        width: 100
    });

    //Ext.create('Jit.form.field.TextArea', {
    //    id: "txtRemark",
    //    text: "",
    //    renderTo: "txtRemark",
    //    width: 315,
    //    height: 70,
    //    margin: '0 0 10 10'
    //});
   
//    Ext.create('Jit.form.field.Text', {
//        id: "txtImageUrl",
//        text: "",
//        renderTo: "txtImageUrl",
//        //readOnly: true,
//        width: 315
//    }); 

    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:0px;padding-bottom:0px;border:0px;',
        //layout: 'anchor',
        layout: {
            type: 'table'
            , columns: 3
            , align: 'right'
        },
        defaults: {},

        items: [
        ]
        ,buttonAlign: "left"
        ,buttons: [
        {
            xtype: "jitbutton",
            text: "保存",
            formBind: true,
            disabled: true,
            handler: fnSave
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });


    //上传图片
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("itemEditImageStore"),
        id: "gridImage",
        renderTo: "gridImage",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: 400,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBarImage",
            defaultType: 'button',
            store: Ext.getStore("itemEditImageStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            //            render: function (p) {
            //                p.setLoading({
            //                    store: p.getStore()
            //                }).hide();
            //            }
        },
        columns: [{
            text: '操作',
            width: 60,
            sortable: true,
            dataIndex: 'ImageId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItemImage('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '图片地址',
            width: 210,
            sortable: true,
            dataIndex: 'ImageURL',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '排序',
            width: 60,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        ]
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtImage_ImageUrl",
        text: "",
        readOnly: true,
        renderTo: "txtImage_ImageUrl",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtImage_DisplayIndex",
        text: "",
        renderTo: "txtImage_DisplayIndex",
        width: 100
    });

    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddImageUrl",
        //hidden: __getHidden("create"),
        handler: function () {
            fnAddImageUrl();
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
                    height: 300,
                    width: '100%'
                });
            }
        }
    });

}