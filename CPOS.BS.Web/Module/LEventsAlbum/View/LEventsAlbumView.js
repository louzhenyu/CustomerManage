function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',

        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "视频标题",
            id: "txtTitle",
            name: "Title",
            jitSize: 'small'
        },
    , {
            width: '100%',
            items: [{
                xtype: "jitbutton",
                text: "查询",
                margin: '0 0 10 14',
                handler: fnSearch
                , jitIsHighlight: true
                , jitIsDefaultCSS: true
            }, {
                xtype: "jitbutton",
                text: "重置",
                margin: '0 0 10 14',
                handler: fnReset
                , jitIsHighlight: false
                , jitIsDefaultCSS: true
            }],
            margin: '0 0 10 0',
            layout: 'column',
            border: 0
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        , imgName: 'create'
        , isImgFirst: true
        , hidden: __getHidden("create")
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });


    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("newsStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 387,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("newsStore"),
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
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'AlbumId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },       
        {
            text: '视频标题',
            width: 200,
            sortable: true,
            dataIndex: 'Title',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.AlbumId + "')\">" + value + "</a>";
                return str;
            }
        },
          {
                text: '浏览数',
                width: 60,
                sortable: true,
                dataIndex: 'BrowseNum',
                align: 'left'
            },
          {
              text: '收藏数',
              width: 60,
              sortable: true,
              dataIndex: 'BookmarkNum',
              align: 'left'
          },
           {
               text: '点赞数',
               width: 60,
               sortable: true,
               dataIndex: 'PraiseNum',
               align: 'left'
           },
            {
                text: '分享数',
                width: 60,
                sortable: true,
                dataIndex: 'ShareNum',
                align: 'left'
            },      
          {
              text: '图片地址',
              width: 200,
              sortable: true,
              dataIndex: 'ImageUrl',
              align: 'left'
          },
           {
               text: '视频地址',
               width: 200,
               sortable: true,
               dataIndex: 'Description',
               align: 'left'
           }
      ]
    });
}