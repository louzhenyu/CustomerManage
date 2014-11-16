/*
内存分页的Store
*/
Ext.define('Jit.data.PagingMemoryStore', {
    extend: 'Ext.data.Store'
    , alias: 'widget.jitPagingMemoryStore'
    /*
    构造函数
    */
    , constructor: function (config) {
        config = config || {};
        this.callParent([config]);
    }
    /*
    覆写filterBy方法，以实现从包含所有数据的数据集中获取当页数据
    */
    , filterBy: function (fn, scope) {
        var me = this;
        //从proxy中获取所有的数据进行筛选
        var allDatas = Ext.create('Ext.util.MixedCollection', false, function (record) { return record.internalId; });
        allDatas.addAll(me.proxy.getReader().readRecords(me.proxy.data).records);
        var filteredDatas = allDatas.filterBy(fn, scope || me);
        filteredDatas.sort(me.sorters.items);
        //获取当页数据
        var pageSize = me.pageSize;
        var totalRowCount = filteredDatas.length || 0;
        var totalPageCount = Math.ceil(totalRowCount / pageSize);

        if (me.currentPage > totalPageCount && me.currentPage != 1) {
            me.currentPage = totalPageCount
        }
        var currentPageIndex = me.currentPage - 1;

        var start = currentPageIndex * pageSize;
        var limit = (currentPageIndex + 1) * pageSize;
        var pagedData = new Array();
        for (var i = start; i < filteredDatas.length && i < limit; i++) {
            pagedData.push(filteredDatas.items[i]);
        }

        var data = Ext.create('Ext.util.MixedCollection', false, function (record) { return record.internalId; });
        data.addAll(pagedData);
        me.snapshot = me.snapshot || data;
        me.data = data;

        //重新设置store的一些属性以便于分页控件重新计算
        me.totalCount = totalRowCount;
        me.fireEvent('datachanged', me);
    }
});