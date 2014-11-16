Ext.define('Ext.JITStoreGrid.Panel', {//Jit.biz.SelectionDynamicGridPanel
    extend: 'Ext.grid.Panel'
    , alias: 'widget.JITStoreGrid'
    , config: {
        BtnCode: null,
        pageSize: null,
        pageIndex: null,
        RowsCount: null,
        PageCount: null,
        CheckStore: null,
        isPaging: true,
        KeyName: null,
        KeyText: null,
        KeyValue: null,
        CorrelationValue: null, //关联值
        btnPageMsg: null,
        CheckMode: null,
        ajaxPath: '/Module/BasicData/Store/Handler/StoreHandler.ashx',
        searchConditions: null,
        otherGridColumns: null,
        otherGridDataDefinds: null,
        isHaveCheckMode: null,
        editKeyName: null,
        pagebar: null,
        gridCallBack: null
    }
    , constructor: function (cfg) {
        var defaultConfig = {};
        var me = this;
        //显示业务控件数据

        //自己的配置项处理
        var myMask = new Ext.LoadMask(Ext.getBody(), { msg: '系统处理中...' });
        myMask.show();
        Ext.Ajax.request({
            url: cfg.ajaxPath
                , params: { btncode: cfg.BtnCode, method: 'InitGridData', pPageIndex: cfg.pageIndex - 1, pPageSize: cfg.pageSize, pKeyValue: cfg.KeyValue, CorrelationValue: cfg.CorrelationValue }
                , method: 'POST'
                , async: false
                , callback: function (options, success, response) {
                   // alert(11);
                    //获取数据
                    var json = Ext.JSON.decode(response.responseText);
                    var GridColumnDefinds = new Array();
                    ///增加其他列
                    if (cfg.otherGridColumns != null) {
                        for (var i = 0; i < cfg.otherGridColumns.length; i++) {
                            GridColumnDefinds.push(cfg.otherGridColumns[i]);

                        }

                    }
                    for (var i = 0; i < json.GridColumnDefinds.length; i++) {
                        switch (json.GridColumnDefinds[i].ColumnControlType) {
                            case 1:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                            case 2:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Int', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                            case 3:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Decimal', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                            case 4:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Date', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                            case 30: //地图
                                GridColumnDefinds.push({ getMapTitle: function (val, r) { if (r.data.VipName != null && r.data.VipName != "undefined" && r.data.VipName != "") { return "地图-" + r.data.VipName; } }, xtype: 'jitcolumn', jitDataType: 'Coordinate', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                            case 34:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Photo', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                            case 204:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: function (value, p, record) {
                                    var str = "";
                                    // var d = record.data;
                                    p.tdAttr = 'data-qtip="' + value.replace(/\,/g, "</br>") + '"';
                                    if (value != "") {
                                        str = "<font color=\"blue\">" + value.split(",").length + "项</font>";
                                    }
                                    return str;
                                }
                                });
                                break;


                            default:
                                GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: json.GridColumnDefinds[i].ColumnText, width: json.GridColumnDefinds[i].ColumnWdith, sortable: false, dataIndex: json.GridColumnDefinds[i].DataIndex, renderer: me.fnRenderEdit });
                                break;
                        }
                    }
                    //获取数据定义
                    var GridDataDefinds = new Array();
                    if (cfg.otherGridDataDefinds != null) {
                        for (var i = 0; i < cfg.otherGridDataDefinds.length; i++) {
                            GridDataDefinds.push(cfg.otherGridDataDefinds[i]);
                        }

                    }

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
                    GridDataDefinds.push({ name: cfg.KeyName, type: 'string' })
                    var gridStore = new Ext.data.Store(
                                                         {
                                                             fields: GridDataDefinds

                                                         }
                                                     );


                    //设置选中值
                    var selModel;
                    if (cfg.isHaveCheckMode == null || cfg.isHaveCheckMode == true) {
                        selModel = Ext.create('Ext.selection.CheckboxModel',
                                        {
                                            mode: cfg.CheckMode,
                                            onRowMouseDown: function (view, record, item, index, e) {
                                                view.el.focus();
                                                var me = this,
                                                checker = e.getTarget('.' + Ext.baseCSSPrefix + 'grid-row-checker'),
                                                mode;
                                                if (!me.allowRightMouseSelection(e)) {
                                                    return;
                                                }
                                                if (me.checkOnly && !checker) {
                                                    return;
                                                }
                                                if (checker || true) {
                                                    mode = me.getSelectionMode();
                                                    if (mode !== 'SINGLE') {
                                                        me.setSelectionMode('SIMPLE');
                                                    }
                                                    me.selectWithEvent(record, e);
                                                    me.setSelectionMode(mode);
                                                }
                                            },
                                            listeners: {
                                                'deselect': function (a, b, c) {
                                                    var r = me.CheckStore.getById(b.data[me.KeyName]);
                                                    me.CheckStore.remove(r);
                                                },

                                                'select': function (a, b, c) {
                                                    var o = new Object();
                                                    o.Value = b.data[me.KeyName];
                                                    o.Text = b.data[me.KeyText];
                                                    if (me.CheckMode == 'SINGLE') {
                                                        me.CheckStore.removeAll();
                                                    }
                                                    if (me.CheckStore.getById(b.data[me.KeyName]) == null)
                                                        me.CheckStore.insert(0, o);
                                                }
                                            }

                                        }
                                       );
                    }
                    if (cfg.CheckStore == undefined || cfg.CheckStore == null) {
                        Ext.define("CheckStoreModel", {
                            extend: 'Ext.data.Model',
                            fields: [{ name: 'Value', type: 'String' },
                                             { name: 'Text', type: 'String'}],
                            idProperty: 'Value'
                        });
                        cfg.CheckStore = new Ext.data.Store({ model: 'CheckStoreModel' });

                    }




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


    }
    //不分页查询
    , fnSearch: function (me) {
        Ext.Ajax.request({
            url: me.ajaxPath
                        , params: { btncode: me.BtnCode, method: 'GridData', pSearch: me.searchConditions, CorrelationValue: me.CorrelationValue }
                        , method: 'POST'
                        , callback: function (options, success, response) {
                            var json = Ext.JSON.decode(response.responseText);
                            me.store.loadData(json);
                        }
        });

    }
    //分页查询
    , fnPageSearch: function (me) {
        //alert("分页查询");，第一次加载和后面的查询都是这个分页查询
       // alert(me.searchConditions);//查看查询条件
        var myMask = new Ext.LoadMask(document.body, { msg: '系统处理中...' });
        myMask.show();
        Ext.Ajax.request({
            url: me.ajaxPath
                        , params: { btncode: me.BtnCode, method: 'PageGridData', pSearch: me.searchConditions, pPageIndex: me.pageIndex - 1, pPageSize: me.pageSize, pKeyValue: me.KeyValue, CorrelationValue: me.CorrelationValue }
                        , method: 'POST'
                        , callback: function (options, success, response) {
                            if (me.isHaveCheckMode == null || me.isHaveCheckMode == true) {
                                me.columns[0].removeCls('x-grid-hd-checker-on');
                                me.getSelectionModel().clearSelections();
                            }
                            var json = Ext.JSON.decode(response.responseText);//通过ext的json解析方式
                            console.log(response.responseText);//打印返回的数据内容****
                            me.store.loadData(json.GridData);//获得返回值
                            me.RowsCount = json.RowsCount;
                            me.PageCount = json.PageCount;
                            me.pageSize = me.pageSize;
                            me.currentPage = me.pageIndex;
                            me.pageIndex = me.pageIndex;

                            if (me.isHaveCheckMode == null || me.isHaveCheckMode == true) {
                                me.store.each(function (rec) {
                                    if (me.CheckStore.getById(rec.data[me.KeyName]) != null) {
                                        me.selModel.select(rec, true);
                                    }

                                });
                            }
                            me.pagebar.updateInfo();
                            if (me.gridCallBack != null)
                                me.gridCallBack();
                            myMask.hide();
                        }

        });

    },
    fnPrv: function (me) {
        //上一页
        me.pageIndex = me.pageIndex - 1;
        if (me.pageIndex < 1) me.pageIndex = 1
        me.fnPageSearch(me);
    }
    ,
    fnNext: function (me) {
        //下一页
        me.pageIndex = me.pageIndex + 1;
        if (me.pageIndex > me.PageCount) me.pageIndex = me.PageCount
        me.fnPageSearch(me);
    },
    fnFirst: function (me) {
        //首页
        me.pageIndex = 1;

        me.fnPageSearch(me);
    },
    fnLast: function (me) {
        //最后一页 
        me.pageIndex = me.PageCount;
        me.fnPageSearch(me);
    },
    fnRenderEdit: function (a, b, c, d, e) {
        if (this.columns[e].dataIndex == this.editKeyName) {
            return "<a href='#' style='color:blue;' onclick='fnUpdateEditView()'>" + a + "</a>";
        }
        else {
            return a;
        }
    }
});


