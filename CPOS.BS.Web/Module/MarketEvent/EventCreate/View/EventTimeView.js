function InitView() {
    
    //operator area
    Ext.create('Jit.button.Button', {
        text: "清空",
        renderTo: "btnReset",
        handler: fnReset
    });
    Ext.create('Jit.button.Button', {
        text: "下一步",
        renderTo: "btnNext",
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "上一步",
        renderTo: "btnPre",
        handler: fnPre
    });

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 441,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '按周期'
        }
        ,{
            contentEl:'tabWave', 
            title: '按波段',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabWave");
                    tmp.style.display = "";
                    tmp.style.height = "441px";
                    tmp.style.overflow = "";
                    //tmp.style.background = "rgb(241, 242, 245)";

                    //fnLoadWave();
                }
            }
        }
        ]
    });

    
    Ext.create('Jit.form.field.Date', {
        text: "",
        renderTo: "txtBeginDate",
        id: "txtBeginDate",
        jitSize: 'small',
        width: 100,
        format: 'Y-m-d'
    });
    Ext.create('Jit.form.field.Date', {
        text: "",
        renderTo: "txtEndDate",
        id: "txtEndDate",
        jitSize: 'small',
        width: 100,
        format: 'Y-m-d'
    });
    
    var fileUpload = Ext.create('Ext.form.field.File', {
        renderTo: 'fileUpload',
        width: 200,
        hideLabel: true
    });
    Ext.create('Ext.button.Button', {
        text: '上传',
        renderTo: 'btnUpload',
        handler: function(){
        }
    });
    
    // gridWave list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("MarketWaveBandStore"),
        id: "gridWave",
        renderTo: "gridWave",
        columnLines: true,
        height: 416,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("MarketWaveBandStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        //listeners: {
        //    render: function (p) {
        //        p.setLoading({
        //            store: p.getStore()
        //        }).hide();
        //    }
        //},
        plugins: [
	        Ext.create('Ext.grid.plugin.CellEditing', {
	            clicksToEdit: 1//设置鼠标单击1次进入编辑状态
	        })
        ],
        columns: [{
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '开始时间',
            width: 110,
            sortable: true,
            dataIndex: 'BeginTime',
            align: 'left'
            ,flex: true
            ,xtype: 'datecolumn'
            ,format: 'Y-m-d'
            ,field: {
                xtype: 'datefield'
                ,allowBlank: false
                ,format: 'Y-m-d'
            }
        }
        ,{
            text: '结束时间',
            width: 110,
            sortable: true,
            dataIndex: 'EndTime',
            align: 'left'
            ,flex: true
            ,xtype: 'datecolumn'
            ,format: 'Y-m-d'
            ,field: {
                xtype: 'datefield'
                ,allowBlank: false
                ,format: 'Y-m-d'
            }
        }
        //,{
        //    text: '创建时间',
        //    width: 150,
        //    sortable: true,
        //    dataIndex: 'CreateTime',
        //    align: 'left',
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        str += getDate(value);
        //        return str;
        //    }
        //}
        ]
    });

}