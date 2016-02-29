function InitView() {
    Ext.create('Ext.form.Panel', {
        id: 'panel',
        width: "980px",
        height: "40%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:0px',
        layout: "anchor",
        items: [
         {
             layout: { type: 'table', columns: 4
             },
             items: [
             {
                 xtype: "jitdisplayfield",
                 fieldLabel: "订单编号",
                 name: "OrdersNo",
                 id: "lblOrdersNo"
             }, {
                 xtype: "jitdisplayfield",
                 fieldLabel: "提货人",
                 name: "user",
                 id: "lbluser"
             },
             {
                 xtype: 'jitdisplayfield',
                 fieldLabel: "",
                 name: "pgee",
                 id: "lblpage",
                 margin: '0 0 0 200',
                 jitSize: 'small'
             }, {
                 rowspan: 3,
                 xtype: 'jitdisplayfield',
                 jitSize: 'small',
                 width: 10,
                 height: 10,
                 id: 'iamge'
             },
             
             {
                 xtype: "jitdisplayfield",
                 fieldLabel: "提货时间",
                 name: "time",
                 id: "lbltime"
             },{

                 xtype: "jitdisplayfield",
                 fieldLabel: "手机号",
                 name: "Tel",
                 id: "lblTel"
             },
             {
                 xtype: 'jitdisplayfield',
                 name: "typename",
                 id: "lbltypename",
                 margin: '0 0 0 160'
             },
             {
                 xtype: "jitdisplayfield",
                 fieldLabel: "备注",
                 name: "remark",
                 id: "lblremark"
             },
             {
                colspan:3, 
                 xtype: 'jitdisplayfield',
                 name: "typename",
                 margin: '0 0 25 380'
             }
            ]

         },
        Ext.create('Ext.grid.Panel', {//这个和上面的元素放在了一个panel里
            id: 'gridview',
            store: Ext.getStore("PrintPickingStore"),
            height:'auto',
            columnLines: true,
            columns: [
                //{
                //text: '商品代码',
                //width: 140,
                //sortable: true,
                //dataIndex: 'itemcode'
                //},
            {
                text: '商品名称',
                width: 140,
                sortable: true,
                dataIndex: 'itemname'
            }
            , {
                text: '商品规格',
                width: 260,
                sortable: true,
                dataIndex: 'PropDetailName'
            }
                    ,
            {
                text: '单价',
                width: 80,
                sortable: true,
                xtype: "jitcolumn",
                jitDataType: "Decimal",
                dataIndex: 'price'
            }, {
                text: '数量',
                width: 60,
                sortable: true,
                xtype: "jitcolumn",
                jitDataType: "int",
                dataIndex: 'orderqty'
            }, {
                text: '总金额',
                width: 100,
                sortable: true,
                xtype: "jitcolumn",
                jitDataType: "Decimal",
                dataIndex: 'money'
            }, {
                text: '备注',
                width: 255,
                sortable: true,
                dataIndex: 'remark'
            }],
            height: 375,
            stripeRows: true,
            renderTo: "DivGridView",
            listeners: {
                render: function (p) {
                    p.setLoading({
                        store: p.getStore()
                    }).hide();
                }
            }
        })],
        renderTo: "DivGridView"
    });

}