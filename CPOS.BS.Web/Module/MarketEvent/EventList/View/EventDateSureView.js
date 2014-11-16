function InitView() {
    //list area



    var formatDate = function (value) {
        
        return value ? Ext.Date.dateFormat(value, 'Y-m-d') : '';
    }
    var cellEditing = Ext.create('Ext.grid.plugin.CellEditing', {
        clicksToEdit: 1
    });



    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("EventDateSureStrone"),
        id: "gridView",
        renderTo: "EventDateSure",
        columnLines: true,
        height: 450,
        width: "100%",
        stripeRows: true,
        // selModel: Ext.create('Ext.selection.CheckboxModel', {
        //   mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("EventDateSureStrone"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [{
            text: '序号',
            width: 100,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'center'
        },
        {
            text: '计划开始时间',
            width: 220,
            sortable: true,
            dataIndex: 'BeginTime',
            align: 'left'
        },
        {

            text: '实际开始时间',
            width: 220,
            dataIndex: 'FactBeginTime',
            renderer: formatDate,

            editor: {
                xtype: 'datefield',
                format: 'Y-m-d',
                minValue: '2006-01-01',
                disabledDaysText: 'Plants are not available on the weekends'
            }
        },
        {
            text: '计划结束时间',
            width: 220,
            sortable: true,
            dataIndex: 'EndTime',
            align: 'left'
        },
         {
             text: '实际结束时间',
             width: 270,
             sortable: true,

             renderer: formatDate,
             editor: {
                 xtype: 'datefield',
                 format: 'Y-m-d',
                 minValue: '2006-01-01',
                

                 disabledDaysText: 'Plants are not available on the weekends'
             },
             dataIndex: 'FactEndTime',
             align: 'left'
         },
         {
             text: 'WaveBandID',
              width: 100,
              hidden:true,
              dataIndex: 'WaveBandID'
         }
        ],
        plugins: [cellEditing]
    });
}
