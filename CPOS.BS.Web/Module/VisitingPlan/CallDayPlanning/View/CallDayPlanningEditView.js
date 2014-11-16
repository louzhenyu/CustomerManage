function InitCDPEditView() {

    Ext.create('Ext.form.Panel', {
        id: "cdpEditPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        items: [
        {
            xtype: "JITStoreSelectPannel",
            id: "cdpEditPanel_ClientUserID",
            fieldLabel: "<font color='red'>*</font>人员",
            layout: 'column',
            border: 0,
            CheckMode: 'SINGLE',
            CorrelationValue: 0, //所有人员
            KeyName: "ClientUserID",
            KeyText: "Name",
            
            ajaxPath: '/Module/BasicData/ClientUser/Handler/ClientUserPositionHandler.ashx'

        }, {
            xtype: "jitdatefield",
            fieldLabel: "<font color='red'>*</font>执行日期",
            id: "cdpEditPanel_CallDate",
            name: "CallDate",
            allowBlank: false
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>拜访目标",
            OptionName: 'POPType',
            name: "POPType",
            id: "cdpEditPanel_POPType",
            isDefault: false,
            listeners: {
                'change': fnPOPTypeChange
            },
            allowBlank: false
        }, {
            xtype: "JITStoreSelectPannel",
            id: "cdpEditPanel_StoreList",

            fieldLabel: "<font color='red'>*</font>终端-门店",
            layout: 'column',
            border: 0,
            CheckMode: 'MULTI',

            KeyName: "StoreID", //主健ID
            KeyText: "StoreName", //显示健值
            ajaxPath: '/Module/BasicData/Store/Handler/StoreSelectByClientUser.ashx',
            hidden: true
        }, {
            xtype: "JITStoreSelectPannel",
            id: "cdpEditPanel_DistributorList",

            fieldLabel: "<font color='red'>*</font>终端-经销商",
            layout: 'column',
            border: 0,
            CheckMode: 'MULTI',

            KeyName: "DistributorID", //主健ID
            KeyText: "Distributor", //显示健值
            ajaxPath: '/Module/BasicData/Distributor/Handler/DistributorSelectByClientUser.ashx',
            hidden: true
        }, {
            xtype: "jittextarea",
            fieldLabel: "备注",
            id: "cdpEditPanel_Remark",
            name: "Remark"
        }]
    });



    Ext.create('Jit.window.Window', {
        id: "cdpEditWin",
        title: '选项编辑',
        jitSize: "large",
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("cdpEditPanel")],
        border: 0,
        buttons: ['->', {
            xtype: "jitbutton",
            handler: fnSubmit,
            id: "cdpEditBtnSubmit",
            isImgFirst: true,
            imgName: 'save'
        }, {
            xtype: "jitbutton",
            handler: fnCancel,
            imgName: 'cancel'
        }],
        closeAction: 'hide'
    });
}