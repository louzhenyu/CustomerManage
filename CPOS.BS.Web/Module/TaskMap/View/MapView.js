function InitView() {

    //Ext.create('Ext.form.Panel', {
    //    id: 'searchPanel',
    //    items: [
    //     {
    //         xtype: "jitdatefield",
    //         id: "DateFrom",
    //         fieldLabel: "执行日期",
    //         jitSize: 'small'
    //     }
    //    // ,
    //    // {
    //    //     xtype: 'jitbizchannel'
    //    //, id: 'ChannelID'
    //    //    , name: 'ChannelName'
    //    //    ,multiSelect:true
    //    //    , fieldLabel: '渠道'
    //    //    , isDefault: false
    //    // },
    //    //{
    //    //    xtype: 'jitbizprovince'
    //    //    , id: 'Province'
    //    //    , name: 'ProvinceID'
    //    //    , fieldLabel: '省'
    //    //    , isDefault: true   //是否有默认值
    //    //    , storeId: 'ProvinceStoreID'   //必须，不能重复
    //    //    , CityID: "City"            //必须，为市的ID  
    //    //},
    //    //{
    //    //    xtype: 'jitbizcity'
    //    //    , id: 'City'
    //    //    , name: 'CityID'
    //    //    , fieldLabel: '市'
    //    //    , DistrictID: 'District' //可选，如果有县这个等级，则需要添加
    //    //    , storeId: 'CityStoreID'   //必须，不能重复
    //    //    , isDefault: true //是否有默认值

    //    //}, {
    //    //    xtype: 'jitbizsupplier'
    //    //    , id: 'Supplier'
    //    //    , name: 'SupplierID'
    //    //    , fieldLabel: '公司'
    //    //    , isDefault: true   //是否有默认值
    //    //    , storeId: 'SupplierStoreID'   //必须，不能重复
    //    //    , ClientUserID: "ClientUser"   //必须，为市的ID  
    //    //},
    //    //{
    //    //    xtype: 'jitbizsupplierclientuser'
    //    //    , id: 'ClientUser'
    //    //    , name: 'ClientUserID'
    //    //    , fieldLabel: '督导'
    //    //    , storeId: 'SupplierClientUserStoreID'   //必须，不能重复
    //    //    , isDefault: true //是否有默认值

    //    //}, {
    //    //    xtype: "jittextfield",
    //    //    id: "txt_ClientStoreName",
    //    //    name: 'ClientStoreName',
    //    //    fieldLabel: "点位名称",
    //    //    height: '22',
    //    //    maxLength: 20,
    //    //    jitSize: 'small'
    //    //}
    //    ],
    //    renderTo: 'span_panel',
    //    margin: '10 0 0 0',
    //    layout: 'column',
    //    border: 0
    //});
    //operator area
    //Ext.create('Ext.form.Panel', {
    //    width: '100%',
    //    cls: 'panel_search',
    //    renderTo: 'span_panel2',
    //    items: [{
    //        xtype: "jitbutton",
    //        id: "btnSearch",
    //        //imgName: 'search',
    //        text: "查询",
    //        //hidden: __getHidden("search"),
    //        handler: fnSearch
    //        , jitIsHighlight: true
    //        , jitIsDefaultCSS: true
    //        //isImgFirst: true
    //    }, {
    //        xtype: "jitbutton",
    //        imgName: 'reset',
    //        //hidden: __getHidden("search"),
    //        handler: fnReset
    //    }, {
    //        id:"btnFllScreen",
    //        xtype: "jitbutton",
    //        text: '全屏',
    //        //hidden: __getHidden("search"),
    //        handler: funFullScreen
    //    }],
    //    margin: '0 0 10 0',
    //    layout: 'column',
    //    border: 0
    //});
    //list area

    
    Ext.create('Jit.form.field.Text', {
        id: "txtName",
        value: "搜索订单号/地址/客户名等",
        renderTo: "txtName",
        width: 180
    });
    
    Ext.create('Jit.button.Button', {
        text: "全屏",
        renderTo: "btnFullScreen",
        width: 70,
        handler: funFullScreen
    });
    Ext.create('Jit.button.Button', {
        text: "查询",
        renderTo: "btnSearch",
        width: 50,
        handler: fnSearch
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

}