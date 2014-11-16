function InitView() {

    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 100
            ,listeners: {
                select: function (store) {
                    Ext.getCmp("txtWModel").setDefaultValue("");
                }
                //,'load': function (store, record, opts) {
                //    var firstValue = record[0].data.LawsAndRegulationsID;
                //    Ext.getCmp("txtApplicationId").setValue(firstValue);
                //}
            }
    });
    Ext.create('jit.biz.WModel', {
        id: "txtWModel",
        text: "",
        renderTo: "txtWModel",
        width: 230,
        c: true,
        parent_id: "txtApplicationId"
    });

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 66,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo',
            title: '查询'
        }
        ]
    });

    tabs3 = Ext.widget('tabpanel', {
        //renderTo: 'tabsMain3',
        id: 'tabs3',
        width: '100%',
        height: 371,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo3',
            title: '文本素材',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadItems1();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        , {
            contentEl: 'tabInfo3_2',
            title: '图片素材',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3_2");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadItems2();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        , {
            contentEl: 'tabInfo3_3',
            title: '图文素材',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3_3");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadItems3();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        , {
            contentEl: 'tabInfo3_4',
            title: '语音素材',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3_4");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadItems4();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        //,{
        //    contentEl:'tabInfo3_5', 
        //    title: '视频素材',
        //    listeners: {
        //        activate: function(tab) {
        //            var tmp = get("tabInfo3_5");
        //            tmp.style.display = "";
        //            tmp.style.height = "371px";
        //            tmp.style.overflow = "";
        //            tmp.style.background = "rgb(241, 242, 245)";
        //            tabs3.getActiveTab().doComponentLayout();
        //            fnLoadItems3();
        //        }
        //    }
        //}
        ]
    });
    tabs3.render("tabsMain3");


    Ext.create('Jit.button.Button', {
        id: 'btnSearch',
        text: "查询",
        renderTo: "btnSearch",
        handler: fnSearch
        //, jitIsHighlight: true
        //, jitIsDefaultCSS: true
    });
    //Ext.create('Jit.button.Button', {
    //    text: "清空",
    //    renderTo: "btnReset",
    //    handler: fnReset
    //});
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddItem1",
        handler: fnAddItem1
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddItem2",
        handler: fnAddItem2
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddItem3",
        handler: fnAddItem3
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddItem4",
        handler: fnAddItem4
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "btnAddItem5",
    //    handler: fnAddItem5
    //    , jitIsHighlight: true
    //    , jitIsDefaultCSS: true
    //});


    // list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem1Store"),
        id: "gv1",
        renderTo: "grid1",
        columnLines: true,
        height: 311,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'SINGLE'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar1",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem1Store"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            //render: function (p) {
            //    p.setLoading({
            //        store: p.getStore()
            //    }).hide();
            //}
        },
        columns: [
        {
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'WritingId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete1('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '文本',
            width: 400,
            sortable: true,
            dataIndex: 'Content',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.WritingId == get("MaterialId").value) str += "[当前使用] ";
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView1('" + d.WritingId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ]
    });


    // list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem2Store"),
        id: "gv2",
        renderTo: "grid2",
        columnLines: true,
        height: 311,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'SINGLE'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar2",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem2Store"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [
        {
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'ImageId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete2('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '名称',
            width: 300,
            sortable: true,
            dataIndex: 'ImageName',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.ImageId == get("MaterialId").value) str += "[当前使用] ";
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView2('" + d.ImageId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '图片',
            width: 280,
            sortable: true,
            dataIndex: 'ImageUrl',
            align: 'center',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a href=\"" + d.ImageUrl + "\" target=\"_blank\"><img alt=\"\" src=\"" + d.ImageUrl + "\" width=\"64px\" heigtht=\"64px\" /></a>";
                return str;
            }
        }
        , {
            text: '格式',
            width: 150,
            sortable: true,
            dataIndex: 'ImageFormat',
            align: 'left'
        }
        , {
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ]
    });

    // list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem3Store"),
        id: "gv3",
        renderTo: "grid3",
        columnLines: true,
        height: 311,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar3",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem3Store"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [
        {
            text: '操作',
            width: 60,
            sortable: true,
            dataIndex: 'TextId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a href=\"#\" onclick=\"fnDelete3('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '标题',
            width: 200,
            sortable: true,
            dataIndex: 'Title',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.TextId == get("MaterialId").value) str += "[当前使用] ";
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView3('" + d.TextId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '描述',
            width: 260,
            sortable: true,
            dataIndex: 'Author',
            align: 'left'
        }
        , {
            text: '图片',
            width: 130,
            sortable: true,
            dataIndex: 'CoverImageUrl',
            align: 'center',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a href=\"" + d.CoverImageUrl + "\" target=\"_blank\"><img alt=\"\" src=\"" + d.CoverImageUrl + "\" width=\"64px\" heigtht=\"64px\" /></a>";
                return str;
            }
        }
        , {
            text: '原文链接',
            width: 200,
            sortable: true,
            dataIndex: 'OriginalUrl',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" href=\"" + d.OriginalUrl + "\" target=\"_blank\">" + d.OriginalUrl + "</a>";
                return str;
            }
        }
        , {
            text: '排序',
            width: 60,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        , {
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                return getDate(value);
            }
        }

        ]
    });


    // list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem4Store"),
        id: "gv4",
        renderTo: "grid4",
        columnLines: true,
        height: 311,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'SINGLE'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar4",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem4Store"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [
        {
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'VoiceId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete4('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '音乐名称',
            width: 200,
            sortable: true,
            dataIndex: 'VoiceName',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.VoiceId == get("MaterialId").value) str += "[当前使用] ";
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView4('" + d.VoiceId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '格式',
            width: 150,
            sortable: true,
            dataIndex: 'VoiceFormat',
            align: 'left'
        }
        , {
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ]
    });

    // list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem5Store"),
        id: "gv5",
        renderTo: "grid5",
        columnLines: true,
        height: 311,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'SINGLE'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar5",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem5Store"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [
        {
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'VoiceId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete5('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '标题',
            width: 400,
            sortable: true,
            dataIndex: 'VoiceName',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.VoiceId == get("MaterialId").value) str += "[当前使用] ";
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView5('" + d.VoiceId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ]
    });


    Ext.create('Jit.button.Button', {
        text: "保存",
        id: "btnSave",
        renderTo: "btnSave",
        //disabled: true,
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

}