function InitParameterEditView() {

    Ext.create('Ext.form.Panel', {
        id: "parameterEditPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>参数名称",
            name: "ParameterName",
            allowBlank: false
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>参数类型",
            OptionName: 'ParameterType',
            id: "ddl_ParameterType",
            isDefault: false,
            allowBlank: false
        },
        {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>控件类型",
            OptionName: 'ControlType',
            id: "ControlType",
            isDefault: false,
            listeners: {
                "change": fnControlTypeChange
            },
            allowBlank: false
        }, {
            columnWidth: 1,
            layout: 'column',
            id: "columnControl",
            hidden: true,
            border: 0,
            items: [{
                xtype: "jittextfield",
                fieldLabel: "数据选项",
                name: "ControlName",
                id: "ControlName",
                allowBlank: true,
                width: 147,
                disabled: true
            },
            {
                xtype: "jitbutton",
                text: "...",
                width: 26,
                height: 22,
                margin: "0",
                handler: fnOperator
            }]
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "最大值",
            name: "MaxValue",
            id: "MaxValue",
            hidden: true,
            minValue: 0,
            allowDecimals: true,
            decimalPrecision: 2
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "最小值",
            name: "MinValue",
            id: "MinValue",
            minValue: 0,
            hidden: true,
            allowDecimals: true,
            decimalPrecision: 2

        }, {
            xtype: "jitnumberfield",
            fieldLabel: "最大值",
            id: "Num_MaxValue",
            minValue: 0,
            hidden: true,
            listeners: {
                "change": function () { Ext.getCmp("MaxValue").jitSetValue(Ext.getCmp("Num_MaxValue").jitGetValue()) }
            }
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "最小值",
            id: "Num_MinValue",
            minValue: 0,
            hidden: true,
            listeners: {
                "change": function () { Ext.getCmp("MinValue").jitSetValue(Ext.getCmp("Num_MinValue").jitGetValue()) }
            }
        }, {
            xtype: "jittextfield",
            fieldLabel: "缺省值",
            id: "txt_DefaultValue",
            name: "DefaultValue"
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "缺省值",
            OptionName: 'YES/NO',
            id: "YN_DefaultValue",
            isDefault: false,
            listeners: {
                "select": fnYesNoChange
            }
        }, {
            xtype: "jitdatefield",
            fieldLabel: "缺省值",
            id: "Date_DefaultValue",
            listeners: {
                "change": fnDateChange
            },
            hidden: true
        }, {
            xtype: "jittimefield",
            fieldLabel: "缺省值",
            id: "Time_DefaultValue",
            listeners: {
                "change": fnTimeChange
            },
            format: 'H:i',
            hidden: true
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "缺省值",
            listeners: {
                "change": fnNumChange
            },
            id: "Num_DefaultValue",
            hidden: true
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "缺省值",
            id: "Del_DefaultValue",
            hidden: true,
            listeners: {
                "change": fnDelChange
            },
            allowDecimals: true,
            decimalPrecision: 2
        }, {
            xtype: "jitcombobox",
            fieldLabel: "小数位",
            id: "Scale",
            name: "Scale",
            store: Ext.getStore("scaleStore"),
            displayField: 'name',
            valueField: 'value',
            hidden: true
        }, {
            //xtype: "jitcombobox",
            //fieldLabel: "后缀",
            //id: "UnitID",
            //name: "UnitID",
            //store: Ext.getStore("unitStore"),
            //displayField: 'UnitName',
            //valueField: 'UnitID',
            //hidden: true
            xtype: "jittextfield",
            fieldLabel: "后缀",
            id: 'txtUnit',
            name: "Unit"
        }, {
            xtype: "jittextfield",
            fieldLabel: "权重",
            id: 'TxtWeight',
            name: "Weight",
            vtype: "number"
        }, {
            xtype: "jitcheckbox",
            fieldLabel: "必填",
            name: "IsMustDo",
            inputValue: "1"
        }, {
            xtype: "jitcheckbox",
            fieldLabel: "记忆",
            name: "IsRemember",
            inputValue: "1"
        }, {
            xtype: "jitcheckbox",
            id: 'IsVerify',
            fieldLabel: "强制校验",
            name: "IsVerify",
            inputValue: "1"
        }]
    });

    Ext.create('Jit.window.Window', {
        height: 450,
        id: "parameterEditWin",
        title: '参数编辑',
        jitSize: 'big',
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("parameterEditPanel")],
        border: 0,
        modal: false,
        closeAction: 'hide',
        buttons: [{
            xtype: "jitbutton",
            imgName: 'save',
            id: "btnSave",
            isImgFirst: true,
            handler: fnSubmit
        }, {
            xtype: "jitbutton",
            imgName: 'cancel',
            handler: fnCancel
        }]
    });
}