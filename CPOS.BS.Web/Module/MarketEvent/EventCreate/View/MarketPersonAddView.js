var tagsData = [], tagsStr = "";
function InitView() {
    
    //operator area
    //Ext.create('Jit.button.Button', {
    //    text: "清空",
    //    renderTo: "btnReset",
    //    handler: fnReset
    //});
    Ext.create('Jit.button.Button', {
        text: "确认",
        renderTo: "btnNext",
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    Ext.create('Jit.button.Button', {
        text: "查询",
        renderTo: "btnSearch",
        handler: fnLoadMarketPerson
    });
    Ext.create('Jit.button.Button', {
        text: "清除",
        renderTo: "btnSearchReset",
        handler: fnSearchReset
    });
    Ext.create('Jit.button.Button', {
        text: "选取",
        renderTo: "btnAddGroup",
        handler: fnAddGroup
    });
    
    
    //Ext.create('Jit.form.field.Radio', {
    //    id: "chk1",
    //    text: "",
    //    renderTo: "chk1",
    //    name: "chkType",
    //    width: 100,
    //    //checked: true,
    //    handler: null
    //});
    //Ext.create('Jit.form.field.Radio', {
    //    id: "chk2",
    //    text: "",
    //    renderTo: "chk2",
    //    name: "chkType",
    //    width: 100,
    //    handler: null
    //});
    
    //Ext.create('Jit.form.field.Radio', {
    //    id: "chkYes1",
    //    text: "",
    //    renderTo: "chkYes1",
    //    name: "chkYes",
    //    width: 100,
    //    //checked: true,
    //    handler: null
    //});
    //Ext.create('Jit.form.field.Radio', {
    //    id: "chkYes2",
    //    text: "",
    //    renderTo: "chkYes2",
    //    name: "chkYes",
    //    width: 100,
    //    handler: null
    //});

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
    
    Ext.create('Jit.form.field.Text', {
        id: "txtUserName",
        text: "",
        renderTo: "txtUserName",
        width: 100
    });
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtCompany",
    //    text: "",
    //    renderTo: "txtCompany",
    //    width: 100
    //});
    //Ext.create('jit.biz.UnitSizeType', {
    //    id: "txtUnitSizeType",
    //    text: "",
    //    renderTo: "txtUnitSizeType",
    //    width: 100
    //});
    //Ext.create('jit.biz.YesNoStatus', {
    //    id: "txtIsWeiXinMarketing",
    //    text: "",
    //    renderTo: "txtIsWeiXinMarketing",
    //    width: 100
    //});
    Ext.create('jit.biz.UserGender', {
        id: "txtGender",
        text: "",
        renderTo: "txtGender",
        width: 100
    });
    Ext.create('jit.biz.Tags', {
        id: "txtTags",
        text: "",
        renderTo: "txtTags",
        width: 100
    });
    Ext.create('jit.biz.TagsGroup', {
        id: "txtTagsGroup",
        text: "",
        renderTo: "txtTagsGroup",
        width: 100
    });
    
    
    //5.定义SelModel
    Ext.define("SelModel", {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'MappingID', type: 'string' }, //关联字段
            { name: 'VIPID', type: 'string' },
            { name: 'VipCode', type: 'string' },
            { name: 'VipLevel', type: 'string' },
            { name: 'VipName', type: 'string' },
            { name: 'Phone', type: 'string' },
            { name: 'WeiXin', type: 'string' },
            { name: 'Integration', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'PurchaseAmount', type: 'string' },
            { name: 'PurchaseCount', type: 'string' },
            { name: 'IsDelete', type: 'int' }
        ],
        idProperty: 'VIPID'//store中getById方法必须属性
    });

    //6.创建要更新数据的store 并指定定义的SelModel
    updateStore = Ext.create("Ext.data.Store", { model: 'SelModel' });

    //2.创建grid的复选框
    selModel = Ext.create('Jit.selection.CheckboxModel', {
        idProperty: "VIPID",   //这个值是添加到数据库中数据
        idSelect: "MappingID",  //这个是判断是否选中的数据
        singleSelect: false,
        checkOnly: true,
        listeners: {
            'deselect': selModelObject.deselect,
            'select': selModelObject.select
        }
    });

    // gridStore list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipStore"),
        id: "gridMarketPerson",
        renderTo: "gridMarketPerson",
        columnLines: true,
        height: 389,
        stripeRows: true,
        width: "100%",
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        selModel: selModel,//使用自定义的复选框
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("VipStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        //listeners: {
        //    render: function (p) {
        //        p.setLoading({
        //            store: p.getStore()
        //        }).hide();
        //    }
        //},
        columns: [{
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'VIPID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '卡号',
            width: 110,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
        }
        ,{
            text: '等级',
            width: 110,
            sortable: true,
            dataIndex: 'VipLevel',
            align: 'left'
        }
        ,{
            text: '姓名',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        }
        ,{
            text: '手机',
            width: 110,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        }
       
        ,{
            text: '微信',
            width: 110,
            sortable: true,
            dataIndex: 'WeiXin',
            align: 'left'
        }
        ,{
            text: '积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integration',
            align: 'left'
        }, {
            text: '最新更新时间',
            width: 110,
            sortable: true,
            dataIndex: 'LastUpdateTime',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ,{
            text: '购买金额',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseAmount',
            align: 'left'
        }
        ,{
            text: '购买频次',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseCount',
            align: 'left'
        }
        //,{
        //    text: '您的姓名',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'UserName',
        //    align: 'left'
        //}
        //,{
        //    text: '您所在的企业名称',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'Enterprice',
        //    align: 'left'
        //}
        //,{
        //    text: '企业有多少家连锁门店', // 您偏好的白酒酒精度数
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'IsChainStores',
        //    align: 'left'
        //}
        //,{
        //    text: '您是否对基于微信的O2O营销感兴趣', // 您是否每天都会适量的饮酒 您是否对微信营销感兴趣
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'IsWeiXinMarketing',
        //    align: 'left'
        //}
        //,{
        //    text: '性别',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'GenderInfo',
        //    align: 'left'
        //}
        ]
    });
    
    Ext.getStore("VipStore").on("load", function () {
        selModel.jitSetValue();
        fnRenderPage();
    });
}