function InitView() {

//{
//             xtype: "JITStoreSelectPannel",
//             id: "ClientUserID",
//             margin: '0 0 0 0',
//             fieldLabel: '人员',
//             layout: 'column',
//             border: 0,
//             CheckMode: 'SINGLE',
//             CorrelationValue: 0,
//             KeyName: "ClientUserID", //主健ID
//             KeyText: "Name", //显示健值
//             ajaxPath: '/Module/BasicData/ClientUser/Handler/ClientUserPositionHandler.ashx'
//         },
    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
         {
             xtype: "jitbizunitselecttree",
             fieldLabel: "部门",
             id: "ClientStructureID",
             isDefault: true
         }, {
             xtype: "jitbizrole",
             fieldLabel: "职位",
             id: "ClientPositionID",
             isDefault: true,
             isClientPosition: true  //需要任务中存在的职位
         }, {
             xtype: "jittextfield",
             id: "txt_ClientUserName",
             name:'ClientUserName',
             fieldLabel: "人员名称",
             height: '22',
             maxLength:20,
             jitSize: 'small'
         }, {
             xtype: "jitdatefield",
             id: "DateFrom",
             fieldLabel: "开始时间",
             jitSize: 'small'
         }, {
             xtype: "jitdatefield",
             id: "DateTo",
             fieldLabel: "结束时间",
             jitSize: 'small',
             vtype: "enddate",
             begindateField: "DateFrom"
         }, {
             xtype: "jitcombobox",
             fieldLabel: "拜访任务",
             emptyText: '--请选择--',
             id: "VisitingTaskID",
             name: "VisitingTaskID",
             store: Ext.getStore("taskStore"),
             displayField: 'VisitingTaskName',
             valueField: 'VisitingTaskID',   
             hidden:true,       
             listeners: {
                 'change': fnTaskChange
             }
         }, {
             xtype: "jittextfield",
             id: "txt_ClientStoreName",
             name: 'ClientStoreName',
             fieldLabel: "终端名称",
             height: '22',
             maxLength: 20,            
             jitSize: 'small'
         }, {
             xtype: "jitcombobox",
             fieldLabel: "拜访步骤",
             id: "VisitingTaskStepID",
             emptyText: '--请选择--',
             hidden: true, 
             name: "VisitingTaskStepID",
             store: Ext.getStore("taskStepStore"),
             displayField: 'StepName',
             valueField: 'VisitingTaskStepID'
         }, {
             xtype: "jittextfield",
             id: "txt_VisitingTaskUserName",        
             name: 'VisitingTaskUserID',
             fieldLabel: "拜访对象",
             height: '22',
             maxLength: 20,
             jitSize: 'small'
         }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });
    //operator area
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: 'span_panel2',
        items: [{
            xtype: "jitbutton",
            //imgName: 'search',
            text: "查询",
            hidden: __getHidden("search"),
            handler: fnSearch,
            isImgFirst: true
        }, {
            xtype: "jitbutton",
            //imgName: 'reset',
            text: "重置",
            hidden: __getHidden("search"),
            handler: fnReset
        }, {
            xtype: "jitbutton",
            text: '返&nbsp;&nbsp;回',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnBack
        }, {
            xtype: "jitbutton",
            text: '导&nbsp;&nbsp;出',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: __getHidden("export"),
            handler: fnExport
        }, {
            xtype: "label",
            id: "lbl_Count",
            style: "margin-left:10px",
            text: "当前照片数量:0张"
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });
    //list area

   
}