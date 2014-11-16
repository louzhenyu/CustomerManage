function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "eventsPrizesListStore",
        model: "PrizesViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: {
                read: 'POST'
            }
        }
    });
    /*类型*/
    Ext.create('Ext.data.Store', {
        model: 'PrizesTypeEntity',
        storeId: "PrizeTypeStore",
        data: [
                { Id: '1', Name: '普通奖品' },
                { Id: '2', Name: '优惠券' },
                { Id: '3', Name: '积分' },
                { Id: '4', Name: '现金' }
               ]
    });

    new Ext.data.Store({
        storeId: "CouponTypeStore",
        model: "CouponTypeEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: 'data'
            }
        }
    });
}