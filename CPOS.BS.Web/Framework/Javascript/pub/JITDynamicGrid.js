Ext.define('Ext.JITDynamicGrid.Panel', { //Jit.biz.DynamicGridPanel
    extend: 'Ext.grid.Panel',
    alias: 'widget.JITDynamicGrid',
    config: {
        BtnCode: null, //初始化Grid权根Code
        pageSize: null,
        pageIndex: null,
        RowsCount: null,
        PageCount: null,
        CheckStore: null,
        isPaging: true,
        isSelectKeyName: null,
        KeyName: null,
        KeyText: null,
        KeyValue: null,
        CorrelationValue: null, //关联值
        btnPageMsg: null,
        CheckMode: null,
        ajaxPath: null, //处理程序地址
        searchConditions: null,
        pagebar: null,
        gridCallBack: null

    },
    constructor: function (cfg) {
        var defaultConfig = {};
        var me = this;
        //显示业务控件数据

        //自己的配置项处理
        var myMask = new Ext.LoadMask(document.body, { msg: "系统处理中..." });
        myMask.show();
        Ext.Ajax.request({
            url: cfg.ajaxPath
                , params: { btncode: cfg.BtnCode, method: 'InitGridData', pPageIndex: cfg.pageIndex - 1, pPageSize: cfg.pageSize, pKeyValue: cfg.KeyValue, CorrelationValue: cfg.CorrelationValue }
                , method: 'POST'
                , async: false
                , callback: function (options, success, response) {
                    //获取数据
                    var json = Ext.JSON.decode(response.responseText);
                    var GridColumnDefinds = new Array();
                    for (var i = 0; i < json.GridColumnDefinds.length; i++) {
                        switch (json.GridColumnDefinds[i].ColumnControlType) {
                            case 1:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex });
                                break;
                            case 2:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Int', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex });
                                break;
                            case 3:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Decimal', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex });
                                break;
                            case 4:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Date', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex });
                                break;
                            case 30: //地图
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Coordinate', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex });
                                break;
                            default:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex });
                                break;
                        }
                    }

                    //获取数据定义
                    var GridDataDefinds = new Array();

                    for (var i = 0; i < json.GridDataDefinds.length; i++) {
                        switch (json.GridDataDefinds[i].DataType) {
                            case 1:
                                GridDataDefinds.push({ name: json.GridDataDefinds[i].DataIndex, type: 'string' });
                                break;
                            case 2:
                                GridDataDefinds.push({ name: json.GridDataDefinds[i].DataIndex, type: 'int' });
                                break;
                            case 3:
                                GridDataDefinds.push({ name: json.GridDataDefinds[i].DataIndex, type: 'float' });
                                break;
                            case 4:
                                GridDataDefinds.push({ name: json.GridDataDefinds[i].DataIndex, type: 'datetime' });
                                break;
                            default:
                                GridDataDefinds.push({ name: json.GridDataDefinds[i].DataIndex, type: 'string' });
                                break;

                        }

                    }
                    GridDataDefinds.push({ name: cfg.KeyName, type: 'string' }); //加一个主健ID
                    GridDataDefinds.push({ name: cfg.isSelectKeyName, type: 'string' });

                    var gridStore = new Ext.data.Store(
                                                         {
                                                             fields: GridDataDefinds
                                                         }
                                                     );


                    //设置选中值
                    //创建Model
                    var selModel = Ext.create('Jit.selection.CheckboxModel', {
                        mode: cfg.CheckMode,
                        idProperty: cfg.KeyName,   //这个值是添加到数据库中数据
                        idSelect: cfg.isSelectKeyName  //这个是判断是否选中的数据
                    });
                    cfg.pagebar = new Jit.panel.JITPagePannel({
                        displayInfo: true,
                        grid: me
                    });

                    defaultConfig.bbar = cfg.pagebar;
                    defaultConfig.columns = GridColumnDefinds;
                    defaultConfig.height = 400;
                    defaultConfig.columnLines = true;
                    defaultConfig.enableColumnHide = false;
                    defaultConfig.stripeRows = true;
                    defaultConfig.store = gridStore;
                    defaultConfig.selModel = selModel;

                }

        });
        myMask.hide();
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
        me.pagebar.moveFirst();
    },
    //查询
    fnPageSearch: function (me) {
        var myMask = new Ext.LoadMask(document.body, { msg: "系统处理中..." });
        myMask.show();
        Ext.Ajax.request({
            url: me.ajaxPath
                        , params: { btncode: me.BtnCode, method: 'PageGridData', pSearch: me.searchConditions, pPageIndex: me.pageIndex - 1, pPageSize: me.pageSize, pKeyValue: me.KeyValue, CorrelationValue: me.CorrelationValue }
                        , method: 'POST'
                        , callback: function (options, success, response) {
                            var json = Ext.JSON.decode(response.responseText);
                            me.store.loadData(json.GridData);
                            me.RowsCount = json.RowsCount;
                            me.PageCount = json.PageCount;
                            me.pageSize = me.pageSize;
                            me.currentPage = me.pageIndex;
                            me.pageIndex = me.pageIndex;
                            myMask.hide();
                            me.selModel.jitSetValue();
                            me.pagebar.updateInfo();
                            if (me.gridCallBack != null)
                                me.gridCallBack();
                        }


        });

    },
    //上一页
    fnPrv: function (me) {
        //上一页
        me.pageIndex = me.pageIndex - 1;
        if (me.pageIndex < 1) me.pageIndex = 1
        me.fnPageSearch(me);
    },
    //下一页
    fnNext: function (me) {
        //下一页
        me.pageIndex = me.pageIndex + 1;
        if (me.pageIndex > me.PageCount) me.pageIndex = me.PageCount
        me.fnPageSearch(me);
    },
    //第一页
    fnFirst: function (me) {
        //首页
        me.pageIndex = 1;

        me.fnPageSearch(me);
    },
    //最后一页
    fnLast: function (me) {
        //最后一页 
        me.pageIndex = me.PageCount;
        me.fnPageSearch(me);
    }
});


