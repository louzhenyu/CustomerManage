function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizclientstructure",
            fieldLabel: "部门",
            id: "ClientStructureID",
            isDefault: true
        }, {
            xtype: "jitbizclientposition",
            fieldLabel: "职位",
            id: "ClientPositionID",
            isDefault: true
        }, {
            xtype: "JITStoreSelectPannel",
            id: "ClientUserID",
            margin: '0 0 0 0',

            fieldLabel: '执行人员',
            layout: 'column',
            border: 0,
            CheckMode: 'SINGLE',
            CorrelationValue: 0,
            KeyName: "ClientUserID", //主健ID
            KeyText: "Name", //显示健值
            ajaxPath: '/Module/BasicData/ClientUser/Handler/ClientUserPositionHandler.ashx'
        }, {
            xtype: "jitmonthfield",
            id: "CallDate",
            fieldLabel: "月度",
            format: 'Y-m',
            jitSize: 'small'
        }
            ],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });
	
  Ext.create('Ext.form.Panel', {
            width: '100%',
			cls: 'panel_search',
			renderTo: 'span_panel2',
            items: [{
                xtype: "jitbutton",
                imgName: 'search',
                hidden: __getHidden("search"),
                handler: fnSearch,
                isImgFirst: true
            }, {
                xtype: "jitbutton",
                imgName: 'reset',
                hidden: __getHidden("search"),
                handler: fnReset
            }],
            margin: '0 0 10 0',
            layout: 'column',
            border: 0
        });
	
    //operator area
    Ext.create('Jit.button.Button', {
        imgName: 'create',
        renderTo: "span_create",
        id: "btnCreate",
        hidden: __getHidden("create"),
        isImgFirst: true,
        handler: fnCreate
    });
    //list area
//    Ext.create('Ext.panel.Panel', {
//        width:"100%",
//        bodyPadding: 10,
//        renderTo: "DivGridView",
//        items: [{
//            xtype: 'datepicker',
//            minDate: new Date(),
//            width: 500,
//            height:400,
//            //style: "width: 500px;height:100%", 
//            handler: function (picker, date) {
//                // do something with the selected date
//            },
//            listeners: {
////                'render': function (c, p) {
////                    var main = this.el.dom.firstChild;
////                    Ext.fly(main).setWidth("100%");
////                    Ext.fly(main).setHeight("100%");

////                    this.el.query(".x-date-inner")[0].style.width = "100%";
////                    this.el.query(".x-date-inner")[0].style.height = "100%";
////                } 
//            }
//        }]
//    });
}