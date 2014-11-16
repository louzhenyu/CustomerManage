function InitEditView() {
    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "txtParameterName",
            name: "Title",
            fieldLabel: "标题",
            jitSize: 'small'
        }, {


            xtype: "jitcombobox",
            fieldLabel: "分类",
            id: "cmbType",
            name: "ModelId",
            emptyText: "--请选择--",
            valueField: 'ModelId',
            displayField: 'ModelName',
            store: Ext.getStore("TypeStore")
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    //    Ext.create('jit.biz.WModel', {
    //        id: "txtWModel",
    //        text: "",
    //        renderTo: "txtWModel",
    //        width: 230,
    //        c: true,
    //        parent_id: "txtApplicationId"
    //    });


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
            handler: function () {
                Ext.getCmp("searchPanel").getForm().reset();
            }
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    Ext.create('Jit.button.Button', {
        imgName: 'create',
        renderTo: "span_create",
        hidden: __getHidden("create"),
        isImgFirst: true,
        handler: fnCreate
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WmateriaStore"),
        id: "gridView",
        columnLines: true,
        columns: [
         {
             text: '操作',
             width: JITPage.Layout.OperateWidth,
             sortable: true,
             dataIndex: 'TextId',
             align: 'left',
             hidden: __getHidden("delete"),
             hideable: false,
             renderer: function (value, p, record) {
                 var str = "";
                 str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";

                 return str;
             }
         }, {
             text: '标题',
             width: 200,
             sortable: true,
             dataIndex: 'Title',
             align: 'left',
             renderer: function (value, p, record) {
                 var str = "";
                 var d = record.data;
                 str += "<a class=\"pointer z_col_light_text\" onclick=\"fnEdit('" + d.TextId + "')\">" + value + "</a>";
                 return str;
             }
         }, {
             text: '图片',
             width: 110,
             sortable: true,
             dataIndex: 'CoverImageUrl',
             align: 'left',
             renderer: function (value, p, record) {
                 if (value != "" && value != null) {
                     var res = "<a  href=\"" + value + "\"><img height=45px width=45px src=\"" + value + "\" /></a>";
                     return res;
                 }
             }
         }, {
             text: '原文链接',
             width: 200,
             sortable: true,
             dataIndex: 'OriginalUrl',
             align: 'left',
             renderer: function (value, p, record) {
                 var str = "";
                 var d = record.data;
                 str += "<a class=\"pointer z_col_light_text\" href=\"" + d.OriginalUrl + "\" target=\"_blank\">" + d.OriginalUrl + "</a>";
                 return str;
             }
         }, {

             text: '最后修改人',
             width: 150,
             sortable: true,
             dataIndex: 'LastUpdateBy',
             align: 'left'

         }, {

             text: '最后修改时间',
             width: 150,
             sortable: true,
             dataIndex: 'LastUpdateTime',
             align: 'left'
             ,
             renderer: function (value, p, record) {
                 return getDate(value);
             }

         }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("WmateriaStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView",
        listeners: {
            'afterlayout': function () {
                setImg();
            },
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });
}