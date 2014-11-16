/*定义页面*/
var pagesize = 0;
Ext.onReady(function () {
    InitVE();//先运行model
    InitStore();//运行store，这里用到了model
    InitView();//这里用到了store
    var me = this;
    JITPage.HandlerUrl.setValue("Handler/PrintPicingHander.ashx?");//设置后台方法的路径

    Ext.Ajax.request({

        method: 'GET',
        url: JITPage.HandlerUrl.getValue() + "&method=GetPrintPickingInfo",//传参数
        params: {
            orderID: JITMethod.getUrlParam('orderID')   //getUrlParam有什么用？接受页面传过来的参数
        },
        success: function (data) {

            var jdata = Ext.JSON.decode(data.responseText);
            if (jdata.success == true) {
                debugger
                loadData(jdata);//加载数据//组成表
              //  console.log(data.responseText);
                Ext.getCmp("lblOrdersNo").jitSetValue(jdata.data[0]["orderno"]);
                Ext.getCmp("lbluser").jitSetValue(jdata.data[0]["username"]);
                Ext.getCmp("lbltime").jitSetValue(JITMethod.getDateTimeNotSS(jdata.data[0]["deliveryTime"]));
                Ext.getCmp("lblTel").jitSetValue(jdata.data[0]["tel"]);
                Ext.getCmp("lblremark").jitSetValue(jdata.data[0]["orderRemark"]);//订单的remark
                var htmltypename = "<font style=font-size:x-large>订单明细</font>"
                Ext.getCmp("lbltypename").jitSetValue(htmltypename);
                var o = Ext.getCmp("panel").items.items[0].items.items[3];
                o.jitSetValue("<img src=\"" + jdata.image + "\" />")
                bindStore();//绑定store



            }
        }
    });
});

function bindStore() {
    var store = Ext.getStore("PrintPickingStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetPrintPickingInfo";//设置后台路径
    store.proxy.extraParams = {
        orderID: JITMethod.getUrlParam('orderID')//传参数
    };
    store.load();
}

//这个什么用？
function loadData(jdata) {
   // alert("zhou");到这里了
    debugger;
    var page = 0;
    if (jdata.dataType != "null") {
        page = jdata.dataType.length;//
        pagesize = jdata.dataType.length + 1;
    }
    else {
        pagesize = 1;
    }
    var htmlpages = "<font style=font-size:x-large>1/" + pagesize + "</font>"
    Ext.getCmp("lblpage").jitSetValue(htmlpages);


    if (page > 0) {//大于一页
        for (var i = 0; i < page; i++) {
            debugger;

            new Ext.data.Store({
                storeId: "PrintPickingStore"+i ,//一页创建一个store，并且ID不一样
                model: "PrintPickingEntity",
                proxy: {
                    type: 'ajax',
                    reader: {
                        type: 'json',
                        root: "data",
                        totalProperty: "totalCount"
                    },
                    extraParams: {
                        form: "",
                        id: ""
                    },
                    actionMethods: { read: 'POST' }
                }
            });

           

            Ext.create('Ext.form.Panel', {
                id: 'panelxx'+i,
                style:'page-break-after:always;',
                renderTo: "DivGridView",
                height:0
               });

            Ext.create('Ext.form.Panel', {
                id: 'panel'+i,
                width: "920px",
                height: "40%",
                border: 1,
                bodyStyle: 'background:#F1F2F5;padding-top:0px',
                layout: "anchor",
                items: [     //内容项
                {
                    layout: { type: 'table', columns: 3   //以表格的形式展示
                    },
                    items: [//上面的概要项

                         {
                             xtype: "jitdisplayfield",
                             fieldLabel: "订单编号",
                             name: "OrdersNo",
                             id: "lblOrdersNo" + i
                         },
                         {
                             xtype: 'jitdisplayfield',
                             fieldLabel: "",
                             name: "pgee" ,
                             id: "lblpage"+i,
                             margin: '0 0 0 400',
                             jitSize: 'small'

                         }, {
                             rowspan: 4,
                             xtype: 'jitdisplayfield',
                             jitSize: 'small',
                             width: 10,
                             height: 10,
                             id: 'iamge'+i

                         },

                     {
                         xtype: "jitdisplayfield",
                         fieldLabel: "提货时间",
                         name: "time",
                         id: "lbltime" + i
                     }, {
                         width:200,
                         xtype: 'jitdisplayfield',
                         name: "typename",
                         id: "lbltypename"+i,
                         margin: '0 0 0 360'
                     },
                      {
                        colspan:2, 
                         xtype: 'jitdisplayfield',
                         name: "typename",
                         margin: '0 0 25 380'
                     }
                  ]

                }, Ext.create('Ext.grid.Panel', {
                    id: 'gridview' + i,     //分页时，一次一个gridview
                    store: Ext.getStore("PrintPickingStore" + i),//获取对应的store
                    height: 'auto',
                    columnLines: true,
                    columns: [
                    //    {
                    //    text: '商品代码',
                    //    width: 140,
                    //    sortable: true,
                    //    dataIndex: 'itemcode'
                    //}, 
                    {
                        text: '商品名称',
                        width: 140,
                        sortable: true,
                        dataIndex: 'itemname'
                    },{
                        text: '商品规格',
                        width: 200,
                        sortable: true,
                        dataIndex: 'ItemType'
                    }
                    , {
                        text: '单价',
                        width: 80,
                        sortable: true,
                        xtype: "jitcolumn",
                        jitDataType: "Decimal",
                        dataIndex: 'price'
                    }, {
                        text: '数量',
                        width: 80,
                        sortable: true,
                        xtype: "jitcolumn",
                        jitDataType: "int",
                        dataIndex: 'orderqty'
                    }, {
                        text: '总金额',
                        width: 130,
                        sortable: true,
                        xtype: "jitcolumn",
                        jitDataType: "Decimal",
                        dataIndex: 'money'
                    }, {
                        text: '备注',
                        width: 275,
                        sortable: true,
                        dataIndex: 'remark'
                    }],
                    height: 375,
                    stripeRows: true,
                    renderTo: "DivGridView",
                    listeners: {
                        render: function (p) {
                            p.setLoading({
                                store: p.getStore()
                            }).hide();
                        }
                    }
                })],
                renderTo: "DivGridView"
            });
          
            var htmlpage="<font style=font-size:x-large>"+(i + 2) + "/" + pagesize+"</font>"
            Ext.getCmp("lblOrdersNo" + i).jitSetValue(jdata.data[i]["orderno"]);
           // Ext.getCmp("lblpage" + i).jitSetValue((i + 2) + "/" + pagesize);
            Ext.getCmp("lblpage" + i).jitSetValue(htmlpage);
            //Ext.getCmp("lbltypename" + i).jitSetValue(jdata.data[i]["ItemTagName"]);
            var htmltypename = "<font style=font-size:x-large>" + jdata.dataType[i]["ItemTagName"] + "</font>"
            Ext.getCmp("lbltypename" + i).jitSetValue(htmltypename);
          //  Ext.getCmp("lbltime" + i).jitSetValue(jdata.data[0]["deliveryTime"]);
            Ext.getCmp("lbltime"+i).jitSetValue(JITMethod.getDateTimeNotSS(jdata.data[0]["deliveryTime"]));
            var o = Ext.getCmp("panel"+i).items.items[0].items.items[2];
            o.jitSetValue("<img src=\"" + jdata.image + "\" />")


            var storel = Ext.getStore("PrintPickingStore"+i) ;   //给storel赋值
            storel.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetPrintPickingTypeInfo";
            storel.proxy.extraParams = {
                orderID: JITMethod.getUrlParam('orderID'),
                itemTagID: jdata.dataType[i].ItemTagID
            };
            storel.load();

        }
    }
}

function fnPickingPrint() {

    window.print();
}