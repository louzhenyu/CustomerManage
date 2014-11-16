function InitView() {
    Ext.create('Ext.panel.Panel', {
        id: 'pnlContainer'
        , renderTo: 'dvPlaceholder'
        , layout: {
            type: 'hbox'
            , align: 'stretch'
        }
        , border: 0
        , margin: '5 0 5 5'
        , items: [
            {
                xtype: 'treepanel'
                , width: 300
                , height: 550
                , margin: '0 2 0 0'
                , id: 'trpItemCategoryTree', title: '商品分类', store: Ext.getStore('itemCategoryTreeStore'), rootVisible: false
            }
            , { xtype: 'panel'
                , flex: 1
                , border: 0
                , items: [
                { xtype: 'panel'
                    , layout: {
                        type: 'hbox'
                        , align: 'middle'
                    }
                    , title: '操作区域'
                    , height: 80
                    , items: [
                        { xtype: 'jitbutton', text: '新增', id: 'btnAdd' }
                        , { xtype: 'jitbutton', text: '保存', id: 'btnSave' }
                        , { xtype: 'jitbutton', text: '停用', id: 'btnDelete' }
                    ]
                }
                , { xtype: 'panel', layout: 'vbox', title: '编辑区域',id:'pnlEdit'
                    , margin: '5 0 0 0'
                    , height: 465
                    , items: [
                        { xtype: 'jittextfield', id: 'txtCode', fieldLabel: '<font color="red">*</font>类型编码',margin:'10 10 10 10' }
                        , { xtype: 'jittextfield', id: 'txtName', fieldLabel: '<font color="red">*</font>类型名称' }
                        , { xtype: 'jittextfield', id: 'txtZJM', fieldLabel: '拼音助记码' }
                        , { xtype: 'jitbizstatus', id: 'cmbStatus', fieldLabel: '<font color="red">*</font>状态' }
                        , { xtype: 'jitcombotree', id: 'cmbParent', fieldLabel: '<font color="red">*</font>上级分类', emptyText: '--请选择--', multiSelect: false, isAddPleaseSelectItem: true, pleaseSelectText: '--请选择--', pickCfg: {
                            minHeight: 100
                            , maxHeight: 120
                            , width: 500
                        }
                            , url: 'Handler/ItemCategoryTreeHandler.ashx?Status=1'
                        }
                        , { xtype: 'jitnumberfield', id: 'nmbNO', fieldLabel: '排序' }
                        , { xtype: 'panel', layout: 'hbox'
                            , border: 0
                            , width: 600
                            , items: [
                                { xtype: 'jittextfield', id: 'txtImageUrl', fieldLabel: '图片链接', jitSize: 'big', readOnly: true }
                                , { xtype: 'panel', border: 0,width:100, html: '<input type="button" value="选择图片" name="btnUploadImage" id="btnUploadImage" />' }
                            ]
                        }
                        , { xtype: 'hiddenfield', id: 'hddID' }
                    ]
                }
            ]
            }
        ]
    });

    Ext.create('Ext.menu.Menu', {
        id: 'ctnMenu'
        , items: [
            { id:'ctnMenuItemAdd', text: '添加子类别'  }
            //, { id: 'ctnMenuItemDelete', text: '停用'  }
        ]
    });
}