var htmlEditor, htmlEditor2, htmlEditor3, htmlEditor4, htmlEditor5, htmlEditor6;

function InitView() {

    //Ext.create('jit.biz.ZCourse', {
    //    id: "txtZCourse",
    //    text: "",
    //    renderTo: "txtZCourse",
    //    width: 230
    //});

    
    Ext.create('Jit.Biz.ZCourseSelectTree', {
        id: "txtZCourse",
        renderTo: "txtZCourse",
        width: 230
    });
    
    
    var CourseDescContent = new Ext.form.TextArea({
        height: 255,
        id: 'txtCourseDesc',
        renderTo: "txtCourseDesc", 
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#txtCourseDesc', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 255,
                    width: '100%'
                });
            }
        }
    });
    
    var CourseSummaryContent = new Ext.form.TextArea({
        height: 255,
        id: 'txtCourseSummary',
        renderTo: "txtCourseSummary",
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor2 = K.create('#txtCourseSummary', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 255,
                    width: '100%'
                });
            }
        }
    });
    
    var CourseFeeContent = new Ext.form.TextArea({
        height: 155,
        id: 'txtCourseFee',
        renderTo: "txtCourseFee",
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor3 = K.create('#txtCourseFee', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 155,
                    width: '100%'
                });
            }
        }
    });
    
    var CourseStartTimeContent = new Ext.form.TextArea({
        height: 155,
        id: 'txtCourseStartTime',
        renderTo: "txtCourseStartTime",
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor4 = K.create('#txtCourseStartTime', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 155,
                    width: '100%'
                });
            }
        }
    });
    
    var CouseCapitalContent = new Ext.form.TextArea({
        height: 155,
        id: 'txtCouseCapital',
        renderTo: "txtCouseCapital",
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor5 = K.create('#txtCouseCapital', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 155,
                    width: '100%'
                });
            }
        }
    });
    
    var CouseContactContent = new Ext.form.TextArea({
        height: 155,
        id: 'txtCouseContact',
        renderTo: "txtCouseContact",
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor6 = K.create('#txtCouseContact', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 155,
                    width: '100%'
                });
            }
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCourseStartTime",
        text: "",
        renderTo: "txtCourseStartTime",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCouseCapital",
        text: "",
        renderTo: "txtCouseCapital",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCouseContact",
        text: "",
        renderTo: "txtCouseContact",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEmail",
        text: "",
        renderTo: "txtEmail",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEmailTitle",
        text: "",
        renderTo: "txtEmailTitle",
        width: 405
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
        items: [
        //{
        //    contentEl: 'tabInfo3',
        //    title: '课程描述',
        //    listeners: {
        //        activate: function (tab) {
        //            var tmp = get("tabInfo3");
        //            tmp.style.display = "";
        //            tmp.style.height = "371px";
        //            tmp.style.width = "100%";
        //            tmp.style.overflow = "";
        //            tmp.style.background = "rgb(241, 242, 245)";
        //            fnLoadItems1();
        //            tabs3.getActiveTab().doLayout();
        //            tabs3.getActiveTab().doComponentLayout();
        //        }
        //    }
        //}
        //, {
        //    contentEl: 'tabInfo3_2',
        //    title: '课程简介',
        //    listeners: {
        //        activate: function (tab) {
        //            var tmp = get("tabInfo3_2");
        //            tmp.style.display = "";
        //            tmp.style.height = "371px";
        //            tmp.style.width = "100%";
        //            tmp.style.overflow = "";
        //            tmp.style.background = "rgb(241, 242, 245)";
        //            fnLoadItems2();
        //            tabs3.getActiveTab().doLayout();
        //            tabs3.getActiveTab().doComponentLayout();
        //        }
        //    }
        //}
        //, {
        //    contentEl: 'tabInfo3_3',
        //    title: '课程费用',
        //    listeners: {
        //        activate: function (tab) {
        //            var tmp = get("tabInfo3_3");
        //            tmp.style.display = "";
        //            tmp.style.height = "371px";
        //            tmp.style.width = "100%";
        //            tmp.style.overflow = "";
        //            tmp.style.background = "rgb(241, 242, 245)";
        //            fnLoadItems3();
        //            tabs3.getActiveTab().doLayout();
        //            tabs3.getActiveTab().doComponentLayout();
        //        }
        //    }
        //}
        {
            contentEl: 'tabInfo3_4',
            title: '课程报名',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3_4");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadCourseApply();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        , {
            contentEl: 'tabInfo3_5',
            title: '学员感言',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3_5");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadCourseReflections();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        , {
            contentEl: 'tabInfo3_6',
            title: '课程新闻',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabInfo3_6");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.width = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadNews();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        ]
    });
    tabs3.render("tabsMain3");


    Ext.create('Jit.button.Button', {
        id: 'btnSearch',
        text: "查询",
        renderTo: "btnSearch",
        handler: fnSearch
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    ////Ext.create('Jit.button.Button', {
    ////    text: "清空",
    ////    renderTo: "btnReset",
    ////    handler: fnReset
    ////});
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "btnAddItem1",
    //    handler: fnAddItem1
    //    , jitIsHighlight: true
    //    , jitIsDefaultCSS: true
    //});
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "btnAddItem2",
    //    handler: fnAddItem2
    //    , jitIsHighlight: true
    //    , jitIsDefaultCSS: true
    //});
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddCourseReflections",
        handler: fnAddCourseReflections
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAddNews",
        handler: fnAddNews
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "btnAddItem4",
    //    handler: fnAddItem4
    //    , jitIsHighlight: true
    //    , jitIsDefaultCSS: true
    //});
    ////Ext.create('Jit.button.Button', {
    ////    text: "添加",
    ////    renderTo: "btnAddItem5",
    ////    handler: fnAddItem5
    ////    , jitIsHighlight: true
    ////    , jitIsDefaultCSS: true
    ////});
    
    //Ext.create('Jit.button.Button', {
    //    id: 'btnCourseApply',
    //    text: "课程报名",
    //    renderTo: "btnCourseApply",
    //    handler: fnSearch
    //    //, jitIsHighlight: true
    //    //, jitIsDefaultCSS: true
    //});
    //Ext.create('Jit.button.Button', {
    //    id: 'btnCourseReflections',
    //    text: "学员感言",
    //    renderTo: "btnCourseReflections",
    //    handler: fnSearch
    //    //, jitIsHighlight: true
    //    //, jitIsDefaultCSS: true
    //});
    //Ext.create('Jit.button.Button', {
    //    id: 'btnCourseNews',
    //    text: "课程新闻",
    //    renderTo: "btnCourseNews",
    //    handler: function() {
    //        fnCourseNews();
    //    }
    //    //, jitIsHighlight: true
    //    //, jitIsDefaultCSS: true
    //});

    // list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("ZCourseApplyStore"),
        id: "gv1",
        renderTo: "gridCourseApply",
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
            store: Ext.getStore("ZCourseApplyStore"),
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
        //{
        //    text: '操作',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'ApplyId',
        //    align: 'left',
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete1('" + value + "')\">删除</a>";
        //        return str;
        //    }
        //}
        {
            text: '用户名',
            width: 110,
            sortable: true,
            dataIndex: 'ApplyName',
            //flex: true,
            align: 'left'
            //, renderer: function (value, p, record) {
            //    var str = "";
            //    var d = record.data;
            //    str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView1('" + d.WritingId + "')\">" + value + "</a>";
            //    return str;
            //}
        }
        , {
            text: '公司',
            width: 110,
            sortable: true,
            dataIndex: 'Company',
            //flex: true,
            align: 'left'
        }
        , {
            text: '职位',
            width: 110,
            sortable: true,
            dataIndex: 'Post',
            //flex: true,
            align: 'left'
        }
        , {
            text: '邮箱',
            width: 110,
            sortable: true,
            dataIndex: 'Email',
            //flex: true,
            align: 'left'
        }
        , {
            text: '手机',
            width: 110,
            sortable: true,
            dataIndex: 'Phone',
            //flex: true,
            align: 'left'
        }
        , {
            text: '性别',
            width: 110,
            sortable: true,
            dataIndex: 'Gender',
            //flex: true,
            align: 'left'
        }
        , {
            text: '班级',
            width: 110,
            sortable: true,
            dataIndex: 'Class',
            //flex: true,
            align: 'left'
        }
        , {
            text: '电话',
            width: 110,
            sortable: true,
            dataIndex: 'Tel',
            //flex: true,
            align: 'left'
        }
        , {
            text: '地址',
            width: 110,
            sortable: true,
            dataIndex: 'Address',
            //flex: true,
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
        store: Ext.getStore("ZCourseReflectionsStore"),
        id: "gvCourseReflections",
        renderTo: "gridCourseReflections",
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
            store: Ext.getStore("ZCourseReflectionsStore"),
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
            dataIndex: 'ReflectionsId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteCourseReflections('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '学员名称',
            width: 150,
            sortable: true,
            dataIndex: 'StudentName',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewCourseReflections('" + d.ReflectionsId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '学员职位',
            width: 200,
            sortable: true,
            dataIndex: 'StudentPost',
            //flex: true,
            align: 'left'
        }
        , {
            text: '感言',
            width: 390,
            sortable: true,
            dataIndex: 'Content',
            //flex: true,
            align: 'left'
        }
        , {
            text: '视频',
            width: 110,
            sortable: true,
            dataIndex: 'VideoURL',
            //flex: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (value != null && value.length > 0)
                    str += "<a target=\"_blank\" href=\"" + value + "\">查看</a>";
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
        store: Ext.getStore("ZCourseNewsStore"),
        id: "gvNews",
        renderTo: "gridNews",
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
            store: Ext.getStore("ZCourseNewsStore"),
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
            dataIndex: 'NewsId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteNews('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '发布时间',
            width: 100,
            sortable: true,
            dataIndex: 'StrPublishTime',
            format: 'Y-m-d',
            align: 'left'
        },
        {
            text: '新闻标题',
            width: 300,
            sortable: true,
            dataIndex: 'NewsTitle',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnNewsView('" + d.NewsId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '新闻子标题',
            width: 150,
            sortable: true,
            dataIndex: 'NewsSubTitle',
            align: 'left'
        },
        {
            text: '新闻类型',
            width: 120,
            sortable: true,
            dataIndex: 'NewsTypeName',
            align: 'left'
        },
        {
            text: '内容链接',
            width: 150,
            sortable: true,
            dataIndex: 'ContentUrl',
            align: 'left'
        }]
    });


    //// list
    //Ext.create('Ext.grid.Panel', {
    //    store: Ext.getStore("WMenuAddItem4Store"),
    //    id: "gv4",
    //    renderTo: "grid4",
    //    columnLines: true,
    //    height: 311,
    //    stripeRows: true,
    //    width: "100%",
    //    selModel: Ext.create('Ext.selection.CheckboxModel', {
    //        mode: 'SINGLE'
    //    }),
    //    bbar: new Ext.PagingToolbar({
    //        displayInfo: true,
    //        id: "pageBar4",
    //        defaultType: 'button',
    //        store: Ext.getStore("WMenuAddItem4Store"),
    //        pageSize: JITPage.PageSize.getValue()
    //    }),
    //    listeners: {
    //        render: function (p) {
    //            p.setLoading({
    //                store: p.getStore()
    //            }).hide();
    //        }
    //    },
    //    columns: [
    //    {
    //        text: '操作',
    //        width: 110,
    //        sortable: true,
    //        dataIndex: 'VoiceId',
    //        align: 'left',
    //        renderer: function (value, p, record) {
    //            var str = "";
    //            var d = record.data;
    //            str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete4('" + value + "')\">删除</a>";
    //            return str;
    //        }
    //    }
    //    , {
    //        text: '音乐名称',
    //        width: 200,
    //        sortable: true,
    //        dataIndex: 'VoiceName',
    //        //flex: true,
    //        align: 'left'
    //        , renderer: function (value, p, record) {
    //            var str = "";
    //            var d = record.data;
    //            str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView4('" + d.VoiceId + "')\">" + value + "</a>";
    //            return str;
    //        }
    //    }
    //    , {
    //        text: '格式',
    //        width: 150,
    //        sortable: true,
    //        dataIndex: 'VoiceFormat',
    //        align: 'left'
    //    }
    //    , {
    //        text: '创建时间',
    //        width: 150,
    //        sortable: true,
    //        dataIndex: 'CreateTime',
    //        //flex: true,
    //        align: 'left'
    //        , renderer: function (value, p, record) {
    //            return getDate(value);
    //        }
    //    }
    //    ]
    //});

    //// list
    //Ext.create('Ext.grid.Panel', {
    //    store: Ext.getStore("WMenuAddItem5Store"),
    //    id: "gv5",
    //    renderTo: "grid5",
    //    columnLines: true,
    //    height: 311,
    //    stripeRows: true,
    //    width: "100%",
    //    selModel: Ext.create('Ext.selection.CheckboxModel', {
    //        mode: 'SINGLE'
    //    }),
    //    bbar: new Ext.PagingToolbar({
    //        displayInfo: true,
    //        id: "pageBar5",
    //        defaultType: 'button',
    //        store: Ext.getStore("WMenuAddItem5Store"),
    //        pageSize: JITPage.PageSize.getValue()
    //    }),
    //    listeners: {
    //        render: function (p) {
    //            p.setLoading({
    //                store: p.getStore()
    //            }).hide();
    //        }
    //    },
    //    columns: [
    //    {
    //        text: '操作',
    //        width: 110,
    //        sortable: true,
    //        dataIndex: 'VoiceId',
    //        align: 'left',
    //        renderer: function (value, p, record) {
    //            var str = "";
    //            var d = record.data;
    //            str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete5('" + value + "')\">删除</a>";
    //            return str;
    //        }
    //    }
    //    , {
    //        text: '标题',
    //        width: 400,
    //        sortable: true,
    //        dataIndex: 'VoiceName',
    //        //flex: true,
    //        align: 'left'
    //        , renderer: function (value, p, record) {
    //            var str = "";
    //            var d = record.data;
    //            str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView5('" + d.VoiceId + "')\">" + value + "</a>";
    //            return str;
    //        }
    //    }
    //    , {
    //        text: '创建时间',
    //        width: 150,
    //        sortable: true,
    //        dataIndex: 'CreateTime',
    //        //flex: true,
    //        align: 'left'
    //        , renderer: function (value, p, record) {
    //            return getDate(value);
    //        }
    //    }
    //    ]
    //});


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