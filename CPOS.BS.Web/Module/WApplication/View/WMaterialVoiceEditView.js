function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 401,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '语音素材信息'
        }
        ]
    });
    
    
    Ext.create('Jit.form.field.Text', {
        id: "txtVoiceName",
        text: "",
        renderTo: "txtVoiceName",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtVoiceUrl",
        text: "",
        renderTo: "txtVoiceUrl",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtVoiceSize",
        text: "",
        renderTo: "txtVoiceSize",
        width: 110
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtVoiceFormat",
        text: "",
        renderTo: "txtVoiceFormat",
        width: 110
    });
    
    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 110
    });
    
    
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
            handler: function() {
                fnSave();
            }
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "返回",
            handler: fnClose
        }
        ]
    });

    //Ext.create('Jit.button.Button', {
    //    text: "保存",
    //    renderTo: "span_save",
    //    //type: 'button',
    //    handler: fnSave
    //    , jitIsHighlight: true
    //    , jitIsDefaultCSS: true
    //});
    ////Ext.create('Jit.button.Button', {
    ////    text: "返回",
    ////    renderTo: "span_close",
    ////    handler: fnClose
    ////});

    Ext.create('Jit.button.Button', {
        text: "上传文件",
        renderTo: "btnOpenUpload",
        handler: fnOpenUpload
    });

    
	Ext.create('Ext.form.Panel', {
		renderTo: "spanUpload",
		fileUpload: true,
		layout: 'column',
		//width: "400",
		margin: '10 0 10 10',
		border: 0,
		id: "fileSpan",
		items: [{
			xtype: "jittextfield",
			fieldLabel: "文件",
			name: "file",
			id: "txt_Attach",
			inputType: 'file',
			allowBlank: false,
            //acceptMimes: ['doc', 'xls', 'xlsx', 'pdf', 'zip', 'rar'],
            //maxFileSize: 1 * 1024 * 1024,
            acceptSize: 2,
			width: 300
		}, {
			xtype: 'displayfield',
			id: "filehidden",
			name: 'AttacnmentFileName'
		}]
	});

	Ext.create('Ext.form.Panel', {
		width: '100%',
		cls: 'panel_search',
		renderTo: "spanUploadButton",
		layout: 'column',
		margin: '0 30 7 0',
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