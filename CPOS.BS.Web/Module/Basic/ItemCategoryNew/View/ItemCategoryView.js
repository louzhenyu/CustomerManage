function InitView() {
    Ext.create('Ext.panel.Panel', {
        id: 'pnlContainer',
        renderTo: 'dvPlaceholder',
        layout: {
            type: 'hbox',
            align: 'stretch'
        },
        border: 0,
        margin: '5 0 5 5',
        items: [{
            xtype: 'treepanel',//用的树面板
            width: 400,
            height: 550,
            margin: '0 2 0 0',
            id: 'trpItemCategoryTree',
            title: '商品分类',
            store: Ext.getStore('itemCategoryTreeStore'),
            rootVisible: false,
            dockedItems: [{
                xtype: 'toolbar',
                items: [{
                    xtype: 'buttongroup',
                    items: [{                        
                        xtype: 'button',
                        text: '新增',
                        id: 'btnAdd',
                        width:50,
                        height: 20,
                        
                    }]                    
                }]
            }]
        },

        {
            xtype: 'panel',
            flex: 1,
            border: 0,
            items: [
            //    {
            //    xtype: 'panel',
            //    layout: {
            //        type: 'hbox',
            //        align: 'middle'
            //    },
            //    title: '操作区域',
            //    height: 80,
            //    items: [{
            //        xtype: 'jitbutton',
            //        text: '新增',
            //        id: 'btnAdd'
            //    }, {
            //        xtype: 'jitbutton',
            //        text: '保存',
            //        id: 'btnSave'
            //    }, {
            //        xtype: 'jitbutton',
            //        text: '停用',
            //        id: 'btnDelete'
            //    }]
            //},

            {
                xtype: 'panel',
                layout: 'vbox',
                title: '新增',
                id: 'pnlEdit',
                margin: '0 0 0 0',
                height: 550,
                 labelWidth:100,
                items: [{
                    xtype: 'jittextfield',
                    id: 'txtCode',
                    fieldLabel: '<font color="red">*</font>类型编码',
                    margin: '10 10 10 10'
                }, {
                    xtype: 'jittextfield',
                    id: 'txtName',
                    fieldLabel: '<font color="red">*</font>类型名称'
                }, {
                    xtype: 'jittextfield',
                    id: 'txtZJM',
                    fieldLabel: '拼音助记码'
                }, {
                    xtype: 'jitbizstatus',
                    id: 'cmbStatus',
                    fieldLabel: '<font color="red">*</font>状态'
                }, {
                    xtype: 'jitcombotree',//这里可以使用别名，如果直接create就只能使用全部的命名空间的。
                    id: 'cmbParent',
                    fieldLabel: '<font color="red">*</font>上级分类',  //取的是一个树数据
                    emptyText: '--请选择--',
                    multiSelect: false,
                    isAddPleaseSelectItem: true,
                    pleaseSelectText: '--请选择--',
                    pickCfg: {
                        minHeight: 100,
                        maxHeight: 120,
                        width: 500
                    }
                    , url: 'Handler/ItemCategoryTreeHandler.ashx?Status=1'  //获取数据
                }, {
                    xtype: 'jitnumberfield',
                    id: 'nmbNO',
                    fieldLabel: '排序',
                    minValue: 0,
                    value: 0
                }, {
                    xtype: 'panel',
                    layout: 'hbox',
                    border: 0,
                    width: 600,
                    height:90,
                    items: [
                    {
                        xtype: 'panel',
                        border: 0,
                        width: 100,
                        html: '&nbsp&nbsp&nbsp&nbsp商品分类图片'

                    },
                    {
                        //                        xtype: 'jittextfield',
                        //                        id: 'txtImageUrl',
                        //                        fieldLabel: '图片链接',
                        //                        jitSize: 'big',
                        //                        readOnly: true
                        xtype: 'panel',
                        border: 0,
                        width: 126,
                        html: '<div style="border:solid 1px rgb(118, 164, 182) "><img src="" id="txtImageUrl" width="126px" height="80px"></div>'
                    }, {
                        xtype: 'panel',
                        border: 0,
                        width: 100,
                        html: '<input type="button" value="选择图片" name="btnUploadImage" id="btnUploadImage" />'
                    }]
                }
                , {
                    xtype: 'jittextfield',
                    id: 'txtGUID',
                    fieldLabel: '类型标识',
                      labelWidth:95,
                    width: 320,
                    readOnly: true,
                    cls: "margin-top: 3px"
                }
                , {
                    xtype: 'hiddenfield',
                    id: 'hddID'
                }
                //取阿拉丁的分类。
                , {
                    xtype: 'jitcombotree',
                    id: 'aldCategory',
                    fieldLabel: '<font color="red" >*</font>对应阿拉丁分类',  //取的是一个树数据
                    labelWidth:95,
                    emptyText: '--请选择--',
                    width:320,
                    multiSelect: false,
                    isAddPleaseSelectItem: true,
                    pleaseSelectText: '--请选择--',
                    isSelectLeafOnly:true,//设置只能选择叶子结点。
                    pickCfg: {
                        minHeight: 100,
                        maxHeight: 120,
                        width: 500
                    }, url: 'Handler/ALDCategoryTreeHandler.ashx?Status=1'  //获取数据
                }
                , {
                    xtype: 'fieldcontainer',
                    combineErrors: true,
                    msgTarget: 'side',
                    layout: 'hbox',
                    defaults: {
                        flex: 1,
                        hideLabel: true
                    },
                    width:250,
                    items: [
                        {
                            xtype: 'button',
                            text: '保存',
                            id: 'btnSave',
                            margin: '10 10 10 10',
                            height:30
                        },
                        {
                            xtype: 'jitbutton',
                            text: '停用',
                            id: 'btnDelete',
                            margin: '10 10 10 10',
                            height:30
                        }
                    ]
                }]
            }]
        }]
    });

    Ext.create('Ext.menu.Menu', {
        id: 'ctnMenu', items: [{
            id: 'ctnMenuItemAdd',
            text: '添加子类别'
        }]
    });
}