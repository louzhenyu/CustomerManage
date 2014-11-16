Ext.define('Jit.panel.JITPagePannel', {
    extend: 'Ext.toolbar.Paging',
    alias: 'widget.JITPagePannel',
    config: {
        grid: null
    },
    constructor: function (cfg) {
        var defaultConfig = {};
        var me = this;
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    onLoad: function () {
        return;
    },
    onPagingKeyDown: function (field, e) {
        var me = this;
        var k = e.getKey();
        if (k == e.RETURN) {
            var v = me.child('#inputItem').getValue();
            me.grid.pageIndex = v;
            me.grid.fnPageSearch(me.grid);
        }
    },
    moveFirst: function () {
        //第一页
        var me = this;
        me.grid.fnFirst(me.grid);
    },
    moveNext: function () {
        //下一页
        var me = this;
        me.grid.fnNext(me.grid);
    },
    movePrevious: function () {
        var me = this;
        me.grid.fnPrv(me.grid);
    },
    moveLast: function () {
        //最后一页
        var me = this;
        me.grid.fnLast(me.grid);
        me.updateInfo();
    },
    doRefresh: function () {
        me.grid.fnPageSearch(me.grid);
    },
    updateInfo: function () {
        me = this;
        var pageCount = me.grid.PageCount;
        var pageIndex = me.grid.pageIndex;
        var pageSize = me.grid.pageSize;
        var totalCount = me.grid.RowsCount;
        var displayItem = me.child('#displayItem');
        var msg = Ext.String.format(
            me.displayMsg,
            pageIndex == 1 ? 1 : (pageIndex - 1) * pageSize,
            pageIndex == pageCount ? totalCount : pageSize * pageIndex,
            totalCount);
        displayItem.setText(msg);
        isEmpty = me.grid.store.getCount() === 0;
        var afterText = Ext.String.format(me.afterPageText, isNaN(pageCount) ? 1 : pageCount);
        me.child('#afterTextItem').setText(afterText);
        me.child('#inputItem').setDisabled(isEmpty).setValue(pageIndex);
        me.child('#first').setDisabled(pageIndex === 1 || isEmpty);
        me.child('#prev').setDisabled(pageIndex === 1 || isEmpty);
        me.child('#next').setDisabled(pageIndex === pageCount || isEmpty);
        me.child('#last').setDisabled(pageIndex === pageCount || isEmpty);
        me.child('#refresh').enable();
    }
});