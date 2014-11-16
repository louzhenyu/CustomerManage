function vipVM() {
    Ext.define("vipEntity", {
        extend: 'Ext.data.Model',
        proxy: {
            type: 'ajax',
            url: '/Framework/Javascript/Biz/Handler/VipSourceHandler.ashx?method=GetVipByPhone',
            reader: {
                type: 'json',
                root: 'data'
            }
        },

        fields: [
            { name: 'VIPID', mapping: 'VIPID' },
            { name: 'VipName', mapping: 'VipName' },
            { name: 'VipCode', mapping: 'VipCode' }
        ]
    });
}