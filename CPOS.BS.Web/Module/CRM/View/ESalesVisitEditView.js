function InitView() {

    
    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 371,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '信息录入'
        }
        , {
            contentEl: 'tabImage',
            title: '上传附件',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabImage");
                    tmp.style.display = "";
                    tmp.style.height = "371px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        ]
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtSalesName",
        text: "",
        renderTo: "txtSalesName",
        readOnly: true,
        width: 100
    });
    Ext.create('jit.biz.ESalesVisitType', {
        id: "txtVisitTypeId",
        text: "",
        renderTo: "txtVisitTypeId",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEnterpriseCustomerId",
        text: "",
        renderTo: "txtEnterpriseCustomerId",
        readOnly: true,
        width: 100
    });
    Ext.create('jit.biz.ESalesVisitVip', {
        id: "txtSalesVisitVip",
        text: "",
        renderTo: "txtSalesVisitVip",
        multiSelect: true,
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtVisitLog",
        text: "",
        renderTo: "txtVisitLog",
        width: 505,
        height: 230,
        margin: "0 0 0 10"
    });
    Ext.create('jit.biz.ESalesStage', {
        id: "txtStageId",
        text: "",
        renderTo: "txtStageId",
        width: 200
    });
   
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtImageUrl",
    //    text: "",
    //    renderTo: "txtImageUrl",
    //    //readOnly: true,
    //    width: 315
    //}); 

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
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
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
        height: 266,
        stripeRows: true,
        width: 300,
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
            dataIndex: 'DownloadId',
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
            text: '附件名',
            flex:true,
            sortable: true,
            dataIndex: 'DownloadName',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"" + d.DownloadUrl + "\" target=\"_blank\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '排序',
            width: 100,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        ]
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtImage_DownloadName",
        text: "",
        renderTo: "txtImage_DownloadName",
        width: 150
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

    
	Ext.create('Ext.form.Panel', {
		renderTo: "spanUpload",
		fileUpload: true,
		layout: 'column',
		width: "180",
		margin: '10 0 0 0',
		border: 0,
		id: "fileSpan",
		items: [{
			xtype: "jittextfield",
			fieldLabel: "",
			name: "file",
			id: "txt_Attach",
			inputType: 'file',
			allowBlank: false,
            //acceptMimes: ['doc', 'xls', 'xlsx', 'pdf', 'zip', 'rar'],
            //maxFileSize: 1 * 1024 * 1024,
            acceptSize: 2,
			width: 170
		}, {
			xtype: 'displayfield',
			id: "filehidden",
			name: 'AttacnmentFileName'
		}]
	});

	Ext.create('Ext.form.Panel', {
		width: 90,
		cls: 'panel_search',
		renderTo: "spanUploadButton",
		layout: 'column',
		margin: '0 10 7 0',
		name: "upload",
		border: 0,
		items: [{
			xtype: "jitbutton",
			text: '开始上传',
			jitIsHighlight: true,
			jitIsDefaultCSS: true,
			handler: fnUploadFile
		}]
	});

}