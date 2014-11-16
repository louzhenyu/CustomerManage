function InitView() {

    //operator area
    Ext.create('Jit.button.Button', {
        text: "关闭",
        renderTo: "btnClose",
        handler: fnCloseWin,
        jitIsHighlight: false,
        jitIsDefaultCSS: true
    });

    // gridComment list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("CommentStore"),
        id: "gridComment",
        renderTo: "gridComment",
        columnLines: true,
        height: 400,
        stripeRows: true,
        width: "100%",
//        selModel: Ext.create('Ext.selection.CheckboxModel', {
//            mode: 'MULTI'
//        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("CommentStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '操作',
            width: 120,
            sortable: true,
            dataIndex: 'CommentId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" href=\"#\" onclick=\"fnDelete('" + d.CommentId + "')\">删除</a>";
                if (d.IsCrowdDaren == "1") {
                    str += "&nbsp;&nbsp;<a class=\"pointer z_col_light_text\" href=\"#\" onclick=\"fnSetCrowdDaren('" + d.CommentId + "',0)\">取消围观达人</a>";
                }
                else {
                    str += "&nbsp;&nbsp;<a class=\"pointer z_col_light_text\" href=\"#\" onclick=\"fnSetCrowdDaren('" + d.CommentId + "',1)\">设置围观达人</a>";
                }
                return str;
            }
        },
        {
            text: '用户名',
            width: 100,
            sortable: true,
            dataIndex: 'UserName',
            align: 'left'
        },
        {
            text: '手机号',
            width: 100,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        },
        {
            text: '评论内容',
            width: 400,
            sortable: true,
            dataIndex: 'Content',
            align: 'left'
        },
        {
            text: '时间',
            width: 130,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ]
    });

}