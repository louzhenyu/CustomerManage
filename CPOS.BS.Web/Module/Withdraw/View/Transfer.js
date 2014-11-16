function InitView() {


    /*查询按钮区域*/
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        items: [{
            xtype: "jitbutton",
            height: 22,
            isImgFirst: true,
            text: "批量代付",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: __getHidden("search"),
            handler: function () { fnPay("") }
        }],
        renderTo: 'search_button_panel',
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });



    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore('TransferStore'),
        id: 'gridView',
        renderTo: 'DivGridView',
        columnLines: true,
        height: 420,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("TransferStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '操作',
            width: 80,
            sortable: true,
            dataIndex: 'WithdrawalId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnPay('" + record.data.SerialNo + "')\">代付</a>";
                return str;
            }
        },
        {
            text: '流水号',
            width: 160,
            sortable: true,
            dataIndex: 'SerialNo',
            align: 'left'
        },
         {
             text: '提现申请时间',
             width: 160,
             sortable: true,
             dataIndex: 'WithdrawalTime',
             align: 'left'

         }, {
             text: '企业名称',
             width: 160,
             sortable: true,
             dataIndex: 'CustomerName',
             align: 'left'
         },
         {
             text: '提现银行',

             width: 160,
             sortable: true,
             dataIndex: 'ReceivingBank',
             align: 'left'
         }, {
             text: '收款银账号',

             width: 180,
             sortable: true,
             dataIndex: 'BankAccount',
             align: 'left'

         }, {
             text: '提现金额',
             width: 140,
             sortable: true,
             dataIndex: 'WithdrawalAmount',
             align: 'right'

         }],
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });





    // //创建弹出窗体
}