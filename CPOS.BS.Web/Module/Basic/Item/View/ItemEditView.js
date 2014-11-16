var isKindeditor = false; //是否已经加载了编辑器
var kindeditorArray = new Array();
var tabs;

function InitView() {
    tabs = new Ext.TabPanel({  //选项卡
        renderTo: 'tabsMain',
        width: '100%',
        height: 400,
        plain: true,
        activeTab: 0,//激活的第一个
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo',//基本信息
            title: '基本信息'
        }
        , {
            contentEl: 'tabProp',
            title: '属性信息',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabProp");
                    tmp.style.display = "";
                    tmp.style.height = "100%";
                    tmp.style.overflow = "auto";
                    tmp.style.background = "rgb(241, 242, 245)";
                    if (isKindeditor) { return; };
                    $("textarea[name='kindeditorcontent']").each(function (i, obj) {
                        var editor = KindEditor.create(obj, {
                            resizeType: 2,
                            uploadJson: "/Framework/Javascript/Other/editor/EditorFileHandler.ashx?method=EditorFile&FileUrl=unit",
                            allowFileManager: false,
                            items: ['source', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', 'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|', 'emoticons', 'image', 'link', 'fullscreen']
 
                        });
                        editor.html(obj.value);
                        kindeditorArray.push(editor);
                    });

                    var detailUploadInfo = $('.uploadImageUrl');
                    if (detailUploadInfo && detailUploadInfo.length > 0) {
                        $("input[name='fileupload']").each(function (i, obj) {

                            var KEParam = "KE" + (parseInt(i) + 3);
                            var detailUploadButton = "uploadbutton" + (parseInt(i) + 3);
                            var controlID = $(detailUploadInfo[i]).attr('id');

                            KEParam = KindEditor;
                            detailUploadButton = KEParam.uploadbutton({
                                button: KEParam("#" + controlID),
                                fieldName: 'imgFile',
                                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx?dir=image',
                                afterUpload: function (data) {
                                    if (data.error === 0) {
                                        Ext.Msg.show({
                                            title: '提示',
                                            msg: '图片上传成功',
                                            buttons: Ext.Msg.OK,
                                            icon: Ext.Msg.INFO
                                        });
                                        //取原图地址
                                        var url = data.url;
                                        $("#" + controlID.split('_')[1] + "").val(getStr(url));
                                    } else {
                                        Ext.Msg.show({
                                            title: '提示',
                                            msg: data.message,
                                            buttons: Ext.Msg.OK,
                                            icon: Ext.Msg.INFO
                                        });
                                    }
                                },
                                afterError: function (str) {
                                    Ext.Msg.show({
                                        title: '自定义错误信息' + str,
                                        msg: jdata.msg,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.ERROR
                                    });
                                }
                            });

                            detailUploadButton.fileBox.change(function (e) {
                                detailUploadButton.submit();
                            });
                        });
                    }

                    isKindeditor = true;
                }
            }
        }
        , {
            contentEl: 'tabPrice',
            title: '商品价格',
            hidden: SKUExists,//如果客户关注sku就隐藏商品价格
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabPrice");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        , {
            contentEl: 'tabSku',
            title: '商品SKU',
            hidden: !SKUExists,//如果客户不关注sku就隐藏
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabSku");
                    tmp.style.display = "";
                    tmp.style.height = "100%";
                    tmp.style.overflow = "auto";//在这里设置带滚动条
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        , {
            contentEl: 'tabImage',
            title: '上传图片',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabImage");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        , {
            contentEl: 'tabUnit',
            //hidden: true,
            title: '门店',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabUnit");
                           tmp.style.overflow = "auto";
                    tmp.style.display = "";
                    tmp.style.height = "100%";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        //,
        //{
        //    contentEl: 'tabQcode',
        //    title: '二维码',

        //    listeners: {
        //        activate: function (tab) {
        //            var tmp = get("tabQcode");
        //            tmp.style.display = "";
        //            tmp.style.height = "100%";
        //            tmp.style.overflow = "auto";
        //            tmp.style.background = "rgb(241, 242, 245)";
        //            bindEvent();
        //        }
        //    }
        //}
        ]
    });    //tabs
    //添加按钮，其实是下面的显示
    Ext.create('Ext.Button', {
        id: 'btnAddDisplayInn',
        text: '添加sku',
        renderTo: "btnAddDisplay",
        width:70,
        handler: function () {
          //  alert('You clicked the button!');
          //  $("#z_sku_tb").hide();
            Ext.get("z_sku_tb").show();
            Ext.getCmp("btnAddDisplayInn").hide();
            $("#tbTableSku").hide();
        }
    });

    Ext.create('Jit.Biz.ItemCategorySelectTree', {
        id: "txtItemCategory",
        text: "",
        renderTo: "txtItemCategory",
        width: 100
    });

    /* ItemCategoryMapping begin */

    //    Ext.create('Jit.Biz.ItemCategorySelectTree', {
    //        id: "txtItemCategoryMapping",
    //        text: "",
    //        renderTo: "txtItemCategoryMapping",
    //        multiSelect: true,
    //        width: 315
    //    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCategoryMapping",
        hidden:true,//隐藏
        text: "",
        renderTo: "txtItemCategoryMapping",
        readOnly: true
        , width: 222
    });

    Ext.create('Jit.button.Button', {
        text: "选择标签",
        renderTo: "btnItemCategoryMapping",
        width: 80,
        height: 23,
        margin: "0",
        hidden:true,
        handler: function () {
            fnOptionCategory();
        }
    });

    Ext.create('Ext.tree.Panel', {
        layout: 'fit',
     
        height: 288,
        width: "100%",
        id: "categoryGrid",
        store: Ext.getStore("itemTagStore"),
        multiSelect: true,
        columns: [{
            xtype: 'treecolumn',
            sortable: false,
            text: '商品标签',
            flex: 2,
            dataIndex: 'ItemTagName'
        }],
        listeners: {
            "checkchange": function (node, state) {
                //设置节点切换状态
                NodeCheckChange(node, state);
            }
        }
    });

    Ext.create('Ext.form.Panel', {
        id: "categorySelectPanel",
        autoScroll: true,
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        layout: 'anchor',
        items: [Ext.getCmp("categoryGrid")]
    });
    //弹出框
    Ext.create('Ext.window.Window', {
        height: 350,
        id: "categorySelectWin",
        title: '商品标签选择',
        width: 500,
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("categorySelectPanel")],
        border: 0,
        buttons: ['->', {
            xtype: "jitbutton",
            id: "btnSave",
            handler: fnSubmit,
            isImgFirst: true,
            imgName: "save"
        }, {
            xtype: "jitbutton",
            handler: fnCancel,
            imgName: "cancel"
        }],
        closeAction: 'hide'
    });

    /* ItemCategoryMapping end */

    Ext.create('Jit.form.field.Text', {
        id: "txtItemCode",
        text: "",
        renderTo: "txtItemCode",
        width: 100
    });

    Ext.create('Ext.tip.ToolTip', {
        target: "txtItemCode",
        html: "不填时系统将自动生成"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtItemName",
        text: "",
        renderTo: "txtItemName",
        width: 350,
        emptyText:"请包含品牌、品名和规格"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtItemEnglish",
        text: "",
        renderTo: "txtItemEnglish",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtItemNameShort",
        text: "",
        renderTo: "txtItemNameShort",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtPyzjm",
        hidden:true,
        text: "",
        renderTo: "txtPyzjm",
        width: 100
    });

    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIfgifts",
        text: "",
        renderTo: "txtIfgifts",
        dataType: "yn",
        width: 100
    });

    Ext.getCmp("txtIfgifts").setDefaultValue(0);

    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIfoften",
        text: "",
        renderTo: "txtIfoften",
        dataType: "yn",
        width: 100
    });

    Ext.getCmp("txtIfoften").setDefaultValue(0);

    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIfservice",
        text: "",
        renderTo: "txtIfservice",
        dataType: "yn",
        width: 100
    });

    Ext.getCmp("txtIfservice").setDefaultValue(0);

    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsGB",
        text: "",
        renderTo: "txtIsGB",
        dataType: "yn",
        width: 100
    });

    Ext.getCmp("txtIsGB").setDefaultValue(0);

    Ext.create('Jit.form.field.Text', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        value: "1",
        width: 100
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 500,
        height: 70,
        margin: '0 0 10 10'
    });

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtCreateUserName",
    //    text: "",
    //    renderTo: "txtCreateUserName",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtCreateTime",
    //    text: "",
    //    renderTo: "txtCreateTime",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtModifyUserName",
    //    text: "",
    //    renderTo: "txtModifyUserName",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtModifyTime",
    //    text: "",
    //    renderTo: "txtModifyTime",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtImageUrl",
    //    text: "",
    //    renderTo: "txtImageUrl",
    //    //readOnly: true,
    //    width: 315
    //}); 

    //button
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:0px;padding-bottom:0px;border:0px;',
        //layout: 'anchor',
        layout: {
            type: 'table'
            , columns: 3
            , align: 'right'
        },
        defaults: {},
        items: [
        ]
        , buttonAlign: "left"
        , buttons: [
        {
            xtype: "jitbutton",
            text: "保存商品",
            formBind: true,
            disabled: true,
            handler: fnSave    //保存事件
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        },
        {
            xtype: "jitbutton",
            text: "下载二维码",//作为panel的一个属性
            handler: fnDownloadQRCode
        }
        
        ]
    });

    //price
    Ext.create('Ext.grid.GridPanel', {
        store: Ext.getStore("itemEditPriceStore"),
        id: "gridPrice",
        renderTo: "gridPrice",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: 400,
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        plugins: [
            Ext.create('Ext.grid.plugin.CellEditing', {
                clicksToEdit: 1 //设置单击单元格编辑  
            })
        ],
        disableSelection: true,
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("itemEditPriceStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
        //            render: function (p) {
        //                p.setLoading({
        //                    store: p.getStore()
        //                }).hide();
        //            }
    },
    columns: [
    //{
    //    text: '操作',
    //    width: 110,
    //    sortable: true,
    //    dataIndex: 'item_price_id',
    //    align: 'left',
    //    //hidden: __getHidden("delete"),
    //    renderer: function (value, p, record) {
    //        var str = "";
    //        var d = record.data;
    //        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItemPrice('" + value + "')\">删除</a>";
    //        return str;
    //    }
    //}
    //,
        {
        text: '价格类型',
        width: 110,
        sortable: true,
        dataIndex: 'item_price_type_name',
        align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
    }
        , {
            text: '价格',
            width: 110,
            sortable: true,
            dataIndex: 'item_price',
            align: 'left'
            , editor: {
                allowBlank: true
            }
        }
        ]
});

Ext.create('jit.biz.ItemPriceType', {
    id: "txtItemPrice_TypeList",
    text: "",
    renderTo: "txtItemPrice_TypeList",
    width: 100
});

Ext.create('Jit.form.field.Text', {
    id: "txtItemPrice_Price",
    text: "",
    renderTo: "txtItemPrice_Price",
    width: 100
});

Ext.create('Jit.button.Button', {
    text: "添加",
    renderTo: "btnAddItemPrice",
    //hidden: __getHidden("create"),
    handler: function () {
        fnAddItemPrice();
    }
});

Ext.create('Jit.form.field.Text', {
    id: "txtBarcode",
    text: "",
    renderTo: "txtBarcode",
    width: 100
});

Ext.create('Jit.button.Button', {
    text: "提交",
    renderTo: "btnAddItemSku",
    //hidden: __getHidden("create"),
    handler: fnAddItemSku
});
Ext.create('Jit.button.Button', {
    text: "取消",
    renderTo: "btnCancelItemSku",
    //hidden: __getHidden("create"),
    handler: function(){
        //    Ext.get("z_sku_tb").hide();//虽然隐藏掉了，占的空间还在
        //可以Ext.getCmp("btnAddDisplayInn").show();
        $("#z_sku_tb").hide();
        Ext.getCmp("btnAddDisplayInn").show();
        $("#tbTableSku").show();
    }
});

//上传图片
Ext.create('Ext.grid.Panel', {
    store: Ext.getStore("itemEditImageStore"),//数据来源
    id: "gridImage",
    renderTo: "gridImage",
    autoScroll: true,
	scroll: true,
    columnLines: true,
    height: 366,
    stripeRows: true,
    width: 400,
    selModel: Ext.create('Ext.selection.CheckboxModel', {
        mode: 'MULTI'
    }),
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBarImage",
        defaultType: 'button',
        store: Ext.getStore("itemEditImageStore"),
        pageSize: JITPage.PageSize.getValue()
    }),
    listeners: {
    //            render: function (p) {
    //                p.setLoading({
    //                    store: p.getStore()
    //                }).hide();
    //            }
},
columns: [
            {
                text: '操作',//表头
                width: 100,
                sortable: false,
                dataIndex: 'ImageId',//对应的列数据
                align: 'center',
                //hidden: __getHidden("delete"),
                renderer: function (value, p, record) {//value存的是ImageId的值
                   // debugger;
                    var str = "";
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItemImage('" + value + "')\">删除</a>";
                    str += "<br><br><a class=\"z_op_link\" href=\"#\" onclick=\"fnUpdateItemImage('" + escape(Ext.encode(record.data)) + "')\">修改</a>";
                    //传的是整行的数据
                    return str;
                }
            }
        , {
            text: '图片预览',
            width: 120,
            sortable: false,
            dataIndex: 'ImageURL',
            align: 'left'
            , renderer: function (value, p, record) {
                return "<a href=\"" + value + "\" target=\"_blank\"><img alt=\"\" src=\"" + value + "\" width=\"64px\" heigtht=\"64px\" /></a>";
            }
        }
        , {
            text: '排序',
            width: 50,
            sortable: true,
            dataIndex: 'DisplayIndex',//对应的列数据
            align: 'left'
        }
        , {
            text: '标题',
            width: 70,
            sortable: false,
            dataIndex: 'Title',
            align: 'left'
            , renderer: columnWrap
            ,hidden:true
        }
        , {
            text: '说明',
            width: 130,
            sortable: false,
            dataIndex: 'Description',
            align: 'left'
            , renderer: columnWrap
               , hidden: true
        }
        ]
});

Ext.create('Jit.form.field.Text', {
    id: "txtImage_ImageUrl",
    text: "",
    readOnly: true,
    renderTo: "txtImage_ImageUrl",
    width: 150
});

Ext.create('Jit.form.field.Text', {
    id: "txtImage_DisplayIndex",
    text: "",
    renderTo: "txtImage_DisplayIndex",
    width: 150
});

Ext.create('Jit.form.field.Text', {
    id: "txtImage_Title",
    text: "",
    renderTo: "txtImage_Title",
    width: 250
});

Ext.create('Jit.form.field.TextArea', {
    id: "txtImage_Description",
    text: "",
    renderTo: "txtImage_Description",
    width: 250,
    height: 150
});

Ext.create('Jit.button.Button', {
    text: "确定",
    renderTo: "btnAddImageUrl",
    //hidden: __getHidden("create"),
    handler: function () {
        fnAddImageUrl();
    }
});

Ext.create('Jit.button.Button', {
    text: "清除",
    renderTo: "btnAddImageUrlClear",
    //hidden: __getHidden("create"),
    handler: function () {
        fnAddImageUrlClear();
    }
});

Ext.create('Ext.grid.Panel', {
    store: Ext.getStore("itemEditUnitStore"),
    id: "gridUnit",
    renderTo: "gridUnit",
    columnLines: true,
    height: 366,
    stripeRows: true,
    width: 400,
    selModel: Ext.create('Ext.selection.CheckboxModel', {
        mode: 'MULTI'
    }),
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBarUnit",
        defaultType: 'button',
        store: Ext.getStore("itemEditUnitStore"),
        pageSize: JITPage.PageSize.getValue()
    }),
    listeners: {
    //            render: function (p) {
    //                p.setLoading({
    //                    store: p.getStore()
    //                }).hide();
    //            }
},
columns: [{
    text: '操作',
    width: 110,
    sortable: true,
    dataIndex: 'MappingId',
    align: 'left',
    //hidden: __getHidden("delete"),
    renderer: function (value, p, record) {
        var str = "";
        var d = record.data;
        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItemUnit('" + value + "')\">删除</a>";
        return str;
    }
}
        , {
            text: '名称',
            width: 110,
            sortable: true,
            dataIndex: 'UnitName',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        ]
});

Ext.create('Jit.Biz.UnitSelectTree', {
    id: "txtItemUnit_List",
    text: "",
    renderTo: "txtItemUnit_List",
    width: 100
});

Ext.create('Jit.button.Button', {
    text: "添加",
    renderTo: "btnAddItemUnit",
    //hidden: __getHidden("create"),
    handler: function () {
        fnAddItemUnit();
    }
});

Ext.create('Jit.button.Button', {
    id: "btnSelectSku",
    text: "确定",
    renderTo: "btnSelectSku",
    //hidden: __getHidden("create"),
    handler: function () {

        var id = $('input[name="order"]:checked').val();
        if (id == undefined || id == null || id.length == 0) {
            alert("请选择SKU");
            return;
        }
        fnSelectSku(id, sImageUrl, sImageTitle, sImageDescription);
    }
});


$("div[class^='collapseHeader']").live("click", function () {
    var suffix = $(this).attr("class").substr(14);
    //console.log(suffix);
    if (this.textContent == "点击展开↓")
        this.textContent = "点击隐藏↑";
    else
        this.textContent = "点击展开↓"

    $(".collapse" + suffix).toggle();  // 隐藏/显示所谓的子行
});
}
var rowcount = 0;
$("div[class='add']").live("click", function () {
    rowcount = rowcount + 1;

    var value = $(this).attr("id");
    var values = value.split('_');
    var id = values[0];
    // var index = parseInt(values[2]); //获取当前行的索引
    // var add_index = parseInt(values[3]); //添加的行数
    // var length = $("#" + id + "_add").length;
    var length = $("#" + id + "_add>tbody>tr").length;

    var index = $($("#" + id + "_add>tbody>tr")[parseInt(length) - 1]).attr("id").split('_')[1];
    var columnindex = $(this).attr("columnindex")
    //获取当前行

    var str = "<tr style='line-height:30px;' id=\"" + values[0] + "_" + (parseInt(index) + 1) + "\">";
    str += "<td width = '100%' colspan='4'>";
    str += "<div style='float:left; width:80px;'>&nbsp</div>";
    str += "<div style='float:left;margin-top:5px' prop_name=\"key\" columnindex=\"" +
                        columnindex + "\" input_flag=\"keyvalue\" sku_prop_id=\"" + id +
                        "\" class=\"itemSku text\" type=\"text\" id=\"" + id + "_key_" + length + "\" ></div>";
    str += "<script>Ext.onReady(function() { createTextbox(\"" +
                        id + "_key_" + (parseInt(index) + 1) + "\", null, \"\"); });</script>";

    str += "<div style='float:left;margin-top:5px;' prop_name=\"value\" columnindex=\"" +
                       columnindex + "\" input_flag=\"keyvalue\" sku_prop_id=\"" + id +
                        "\" class=\"itemSku text\" type=\"text\" id=\"" + id + "_value_" + (parseInt(index) + 1) + "\" ></div>"
    str += "<script>Ext.onReady(function() { createTextbox(\"" +
                       id + "_value_" + (parseInt(index) + 1) + "\", null, \"\"); });</script>";
    str += "<div style='float:left; width:80px;margin-left: 50px' id=\"" + id + "_lessen_" + (parseInt(index) + 1) + "\" class=lessen>-</div>";
    str += "</tr>";

    var $tr = $("#" + id + "_" + index).eq(0);
    if ($tr.size() == 0) {
        alert("指定的table id或行数不存在！");
        return;
    }
    $tr.after(str);


});

$("div[class='lessen']").live("click", function () {

    var value = $(this).attr("id").split('_');
    var id = value[0];
    var index = parseInt(value[2]); //获取当前行的索引
    var add_index = parseInt(value[3]); //添加的行数
    var t = $("#" + id + "_" + index).remove();
    $("#" + id + "_value_" + index + "-inputEl").remove();
    $("#" + id + "_key_" + index + "-inputEl").remove();
})

function columnWrap(val) {
    return '<div style="white-space:normal !important;">' + val + '</div>';
}