function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 66,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '会员编号查询'
        }
        ]
    });
    
    var tabs2 = Ext.widget('tabpanel', {
        renderTo: 'tabsMain2',
        width: '100%',
        height: 131,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo2', 
            title: '会员卡基本信息'
        }
        ]
    });
    
    tabs3 = Ext.widget('tabpanel', {
        //renderTo: 'tabsMain3',
        id: 'tabs3',
        width: '100%',
        height: 251,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo3', 
            title: '卡消费',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabInfo3");
                    tmp.style.display = "";
                    tmp.style.height = "251px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fnLoadVipCardSales();
                    tabs3.getActiveTab().doLayout();
                    tabs3.getActiveTab().doComponentLayout();
                }
            }
        }
        ,{
            contentEl:'tabInfo3_2', 
            title: '卡充值',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabInfo3_2");
                    tmp.style.display = "";
                    tmp.style.height = "251px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    tabs3.getActiveTab().doComponentLayout();
                    fnLoadVipCardRechargeRecord();
                }
            }
        }
        ,{
            contentEl:'tabInfo3_3', 
            title: '卡等级',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabInfo3_3");
                    tmp.style.display = "";
                    tmp.style.height = "251px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    tabs3.getActiveTab().doComponentLayout();
                    fnLoadVipCardGradeChangeLog();
                }
            }
        }
        ,{
            contentEl:'tabInfo3_4', 
            title: '卡状态',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabInfo3_4");
                    tmp.style.display = "";
                    tmp.style.height = "251px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    tabs3.getActiveTab().doComponentLayout();
                    fnLoadVipCardStatusChangeLog();
                }
            }
        }
        ,{
            contentEl:'tabInfo3_5', 
            title: '车信息',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabInfo3_5");
                    tmp.style.display = "";
                    tmp.style.height = "251px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    tabs3.getActiveTab().doComponentLayout();
                    fnLoadVipExpand();
                }
            }
        }
        ]
    });
    tabs3.render("tabsMain3");

    Ext.create('Jit.form.field.Text', {
        id: "txtSearchVipCardCode",
        text: "",
        renderTo: "txtSearchVipCardCode",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtSearchVipName",
        text: "",
        renderTo: "txtSearchVipName",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtSearchCarCode",
        text: "",
        renderTo: "txtSearchCarCode",
        width: 100
    });
    
    Ext.create('Jit.button.Button', {
        id: 'btnSearch',
        text: "查询",
        renderTo: "btnSearch",
        handler: fnSearch
    });
    Ext.create('Jit.button.Button', {
        text: "清空",
        renderTo: "btnReset",
        handler: fnReset
    });

    
    // VipCardSales list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipCardSalesStore"),
        id: "gvVipCardSales",
        renderTo: "gridVipCardSales",
        columnLines: true,
        height: 220,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar1",
            defaultType: 'button',
            store: Ext.getStore("VipCardSalesStore"),
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
            text: '消费时间',
            width: 100,
            sortable: true,
            dataIndex: 'SalesTime',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ,{
            text: '消费额',
            width: 100,
            sortable: true,
            dataIndex: 'SalesAmount',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '消费前余额',
            width: 100,
            sortable: true,
            dataIndex: 'SalesBeforeAmount',
            flex: true,
            align: 'left'
        }
        ,{
            text: '消费后余额',
            width: 100,
            sortable: true,
            dataIndex: 'SalesAfterAmount',
            flex: true,
            align: 'left'
        }
        ,{
            text: '关联订单号',
            width: 100,
            sortable: true,
            dataIndex: 'OrderNo',
            flex: true,
            align: 'left'
        }
        ,{
            text: '消费门店',
            width: 100,
            sortable: true,
            dataIndex: 'UnitName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '操作人',
            width: 100,
            sortable: true,
            dataIndex: 'OperationUserName',
            flex: true,
            align: 'left'
        }
        ]
    });
    
    
    // VipCardRechargeRecord list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipCardRechargeRecordStore"),
        id: "gvVipCardRechargeRecord",
        renderTo: "gridVipCardRechargeRecord",
        columnLines: true,
        height: 220,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar2",
            defaultType: 'button',
            store: Ext.getStore("VipCardRechargeRecordStore"),
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
            text: '充值时间',
            width: 100,
            sortable: true,
            dataIndex: 'RechargeTime',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ,{
            text: '充值金额',
            width: 100,
            sortable: true,
            dataIndex: 'RechargeAmount',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '充值前余额',
            width: 100,
            sortable: true,
            dataIndex: 'BalanceBeforeAmount',
            flex: true,
            align: 'left'
        }
        ,{
            text: '充值后余额',
            width: 100,
            sortable: true,
            dataIndex: 'BalanceAfterAmount',
            flex: true,
            align: 'left'
        }
        ,{
            text: '支付方式',
            width: 100,
            sortable: true,
            dataIndex: 'PaymentTypeName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '小票号',
            width: 100,
            sortable: true,
            dataIndex: 'RechargeNo',
            flex: true,
            align: 'left'
        }
        ,{
            text: '充值门店',
            width: 100,
            sortable: true,
            dataIndex: 'UnitName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '操作人',
            width: 100,
            sortable: true,
            dataIndex: 'RechargeUserName',
            flex: true,
            align: 'left'
        }
        ]
    });
    
    // VipCardGradeChangeLog list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipCardGradeChangeLogStore"),
        id: "gvVipCardGradeChangeLog",
        renderTo: "gridVipCardGradeChangeLog",
        columnLines: true,
        height: 220,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar3",
            defaultType: 'button',
            store: Ext.getStore("VipCardGradeChangeLogStore"),
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
            text: '变更时间',
            width: 100,
            sortable: true,
            dataIndex: 'ChangeTime',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ,{
            text: '旧等级',
            width: 100,
            sortable: true,
            dataIndex: 'ChangeBeforeGradeName',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '新等级',
            width: 100,
            sortable: true,
            dataIndex: 'NowGradeName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '类型',
            width: 100,
            sortable: true,
            dataIndex: 'OperationTypeName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '变更原因',
            width: 100,
            sortable: true,
            dataIndex: 'ChangeReason',
            flex: true,
            align: 'left'
        }
        ,{
            text: '操作门店',
            width: 100,
            sortable: true,
            dataIndex: 'UnitName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '操作人',
            width: 100,
            sortable: true,
            dataIndex: 'OperationUserName',
            flex: true,
            align: 'left'
        }
        ]
    });

    
    // VipCardStatusChangeLog list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipCardStatusChangeLogStore"),
        id: "gvVipCardStatusChangeLog",
        renderTo: "gridVipCardStatusChangeLog",
        columnLines: true,
        height: 220,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar4",
            defaultType: 'button',
            store: Ext.getStore("VipCardStatusChangeLogStore"),
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
            text: '变更时间',
            width: 100,
            sortable: true,
            dataIndex: 'CreateTime',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ,{
            text: '原状态',
            width: 100,
            sortable: true,
            dataIndex: 'OldStatusName',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '新状态',
            width: 100,
            sortable: true,
            dataIndex: 'VipCardStatusName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '操作门店',
            width: 100,
            sortable: true,
            dataIndex: 'UnitName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '操作人',
            width: 100,
            sortable: true,
            dataIndex: 'OperationUserName',
            flex: true,
            align: 'left'
        }
        ]
    });
    
    // VipExpand list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipExpandStore"),
        id: "gvVipExpand",
        renderTo: "gridVipExpand",
        columnLines: true,
        height: 220,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar5",
            defaultType: 'button',
            store: Ext.getStore("VipExpandStore"),
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
            text: '车牌号',
            width: 100,
            sortable: true,
            dataIndex: 'LicensePlateNo',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '品牌',
            width: 100,
            sortable: true,
            dataIndex: 'CarBarndName',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '车型',
            width: 100,
            sortable: true,
            dataIndex: 'CarModelsName',
            flex: true,
            align: 'left'
        }
        ,{
            text: '车架号',
            width: 100,
            sortable: true,
            dataIndex: 'ChassisNumber',
            flex: true,
            align: 'left'
        }
        ,{
            text: '车厢形式',
            width: 100,
            sortable: true,
            dataIndex: 'CompartmentsForm',
            flex: true,
            align: 'left'
        }
        ,{
            text: '购买时间',
            width: 100,
            sortable: true,
            dataIndex: 'PurchaseTime',
            flex: true,
            align: 'left'
            ,renderer: function (value, p, record) {
                return getDate(value);
            }
        }
        ,{
            text: '备注',
            width: 100,
            sortable: true,
            dataIndex: 'Remark',
            flex: true,
            align: 'left'
        }
        ]
    });

    
    Ext.create('Jit.button.Button', {
        text: "充值",
        id: "btnOp1",
        renderTo: "btnOp1",
        disabled: true,
        handler: fnVipCardRecharge
    });
    Ext.create('Jit.button.Button', {
        text: "延期",
        id: "btnOp2",
        renderTo: "btnOp2",
        disabled: true,
        handler: fnVipCardExtension
    });
    Ext.create('Jit.button.Button', {
        text: "升降级",
        id: "btnOp3",
        renderTo: "btnOp3",
        disabled: true,
        handler: fnCheckChangeLevel
    });
    Ext.create('Jit.button.Button', {
        text: "激活",
        id: "btnOp4",
        renderTo: "btnOp4",
        disabled: true,
        handler: fnVipCardActive
    });
    Ext.create('Jit.button.Button', {
        text: "冻结",
        id: "btnOp5",
        renderTo: "btnOp5",
        fontColor:'red',
        disabled: true,
        handler: fnVipCardFozen
    });
    Ext.create('Jit.button.Button', {
        text: "注销",
        id: "btnOp6",
        renderTo: "btnOp6",
        disabled: true,
        handler: fnVipCardDisable
    });
    Ext.create('Jit.button.Button', {
        text: "休眠",
        id: "btnOp7",
        renderTo: "btnOp7",
        disabled: true,
        handler: fnVipCardSleep
    });
    Ext.create('Jit.button.Button', {
        text: "挂失",
        id: "btnOp8",
        renderTo: "btnOp8",
        disabled: true,
        handler: fnVipCardReportLoss
    });
}