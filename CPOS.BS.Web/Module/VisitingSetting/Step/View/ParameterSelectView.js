
var CheckBoxModel = Ext.create('Jit.selection.CheckboxModel', {
    mode: 'MULTI',
    idProperty: 'VisitingParameterID',   //这个值是添加到数据库中数据
    idSelect: 'MappingID',  //这个是判断是否选中的数据
    rowSelect: false
});

function InitView() {

    //searchpanel area
    /*Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitcombobox",
            fieldLabel: "参数类型",
            name: "ParameterType",
            store: Ext.getStore("parameterTypeStore"),
            valueField: "OptionsID",
            displayField: "OptionText"
        }, {
            xtype: "jittextfield",
            id: "txtParameterName",
            name: "ParameterName",
            fieldLabel: "参数名称",
            jitSize: 'small'
        }, {
            xtype: "jitbutton",
            text: __getText("search"),
            hidden: __getHidden("search"),
            handler: fnSearch
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });
    */
    //operator area
    Ext.create('Jit.button.Button', {
        imgName: 'save',
        id: "btnSave",
        renderTo: "span_save",
        isImgFirst: true,
        handler: fnSave
    });

    
    //list area

    Ext.getStore("parameterStoreMemory").on("load", function () {
         CheckBoxModel.jitSetValue();
     });
     
     var cellEditing = Ext.create('Ext.grid.plugin.CellEditing', {
         clicksToEdit: 1,
         listeners: {
             "beforeedit": function (a, b, c) {
                 return fnGetValue(CheckBoxModel.jitGetValue(), b.record.data.VisitingParameterID);
             },
             "edit": function (a, b, c) {
                 if (pagedData != null && pagedData.length > 0) {
                     for (var i = 0; i < pagedData.length; i++) {
                         if (pagedData[i].VisitingParameterID == b.record.data.VisitingParameterID) {
                             pagedData[i].ParameterOrder = b.record.data.ParameterOrder;
                             pagedData[i].VisitingTaskStepID = b.record.data.VisitingTaskStepID;
                             updateData.push(pagedData[i]);
                         }
                     }
                 }
             }
         }
     });


     Ext.create('Ext.grid.Panel', {
         store: Ext.getStore("parameterStoreMemory"),
         id: "gridView",
         columnLines: true,
         plugins: [cellEditing],
         selModel: CheckBoxModel,
         columns: [{
             text: '数据名称',
             width: 110,
             sortable: true,
             dataIndex: 'ParameterName',
             align: 'left'
         }, {
             text: '参数类型',
             width: 110,
             sortable: true,
             dataIndex: 'ParameterTypeText',
             align: 'left'
         }, {
             text: '控件类型',
             width: 110,
             sortable: true,
             dataIndex: 'ControlTypeText',
             align: 'left'
         }, {
             xtype: 'jitcolumn',
             jitDataType: 'int',
             text: '执行顺序',
             width: 110,
             sortable: true,
             dataIndex: 'ParameterOrder',
             align: 'left',
             editor: {
                 allowBlank: false,
                 vtype: "number"
             }
         }],
         height: 430,
         stripeRows: true,

         bbar: new Ext.PagingToolbar({
             displayInfo: true,
             id: "pageBar",
             defaultType: 'button',
             store: Ext.getStore("parameterStoreMemory"),
             pageSize: JITPage.PageSize.getValue()
         }),
         renderTo: "DivGridView",
         listeners: {
             render: function (p) {
                 p.setLoading({
                     msg: JITPage.Msg.GetData,
                     store: p.getStore()
                 }).hide();
             }
         }
     });    
}