var isKindeditor = false; //是否已经加载了编辑器
var kindeditorArray = new Array();

function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 451,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo',
            title: '基本信息'
        }
        , {
            contentEl: 'tabProp',
            title: '属性信息',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabProp");
                    tmp.style.display = "";
                    tmp.style.height = "551px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    if (isKindeditor) { return; };
                    $("textarea[name='kindeditorcontent']").each(function (i, obj) {
                        var editor = KindEditor.create(obj, {
                            //id: "editor" + i,
                            resizeType: 2,
                            uploadJson: "/Framework/Javascript/Other/editor/EditorFileHandler.ashx?method=EditorFile&FileUrl=unit",
                            allowFileManager: false
                        });
                        editor.html(obj.value);
                        kindeditorArray.push(editor);
                    });

                    var detailUploadInfo = $('.uploadImageUrl');
                    if (detailUploadInfo && detailUploadInfo.length > 0) {
                        $("input[name='fileupload']").each(function (i, obj) {
                            debugger;
                            var KEParam = "KE" + (parseInt(i) + 30);
                            var detailUploadButton = "uploadbutton" + (parseInt(i) + 30);
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
             contentEl: 'tabQcode',
             title: '二维码',

             listeners: {
                 activate: function (tab) {
                     var tmp = get("tabQcode");
                     tmp.style.display = "";
                     tmp.style.height = "430px";
                     tmp.style.overflow = "";
                     tmp.style.background = "rgb(241, 242, 245)";
                     tmp.style.overflow = "auto";
                     bindEvent();
                 }
             }
         }

        ]
    });

    Ext.create('Jit.Biz.UnitSelectTree', {
        id: "txtParentUnit",
        text: "",
        renderTo: "txtParentUnit",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUnitCode",
        text: "",
        renderTo: "txtUnitCode",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUnitName",
        text: "",
        renderTo: "txtUnitName",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUnitEnglish",
        text: "",
        renderTo: "txtUnitEnglish",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUnitShortName",
        text: "",
        renderTo: "txtUnitShortName",
        width: 100
    });

    Ext.create('jit.biz.UnitType', {
        id: "txtUnitType",
        text: "",
        renderTo: "txtUnitType",
        width: 100
    });

    Ext.create('Jit.Biz.CitySelectTree', {
        id: "txtCity",
        text: "",
        renderTo: "txtCity",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtPostcode",
        text: "",
        renderTo: "txtPostcode",
        width: 100
    });

    //    Ext.create('Jit.form.field.TextArea', {
    //        id: "txtContact",
    //        text: "",
    //        renderTo: "txtContact",
    //        width: 530,
    //        height: 100,
    //        margin: '0 0 10 10'
    //    });

    Ext.create('Jit.form.field.Text', {
        id: "txtContact",
        text: "",
        renderTo: "txtContact",
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtTelephone",
        text: "",
        renderTo: "txtTelephone",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtFax",
        text: "",
        renderTo: "txtFax",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtEmail",
        text: "",
        renderTo: "txtEmail",
        width: 100
    });

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtLongitude",
    //    text: "",
    //    renderTo: "txtLongitude",
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtDimension",
    //    text: "",
    //    renderTo: "txtDimension",
    //    width: 100
    //});
    //var mapSelect = Ext.create('Jit.biz.MapSelect', {
    //    id: 'txtLongitude'   //panel 的id 默认为mapSelect
    //    , fieldLabel: ''  //textField 的fieldLabel

    //        , text: '选 择'  //button的显示文本

    //        , mapTitle: '地图位置选取'   //地图的显示title
    //    , width: 330
    //        , renderTo: 'txtLongitude' //panel的renderTo   没有则不需要添加      

    //});

    Ext.create('Jit.form.field.Text', {
        id: 'txtLongitude'   //
        ,fieldLabel: ''  //textField 的fieldLabel
        ,text: ''  //
        , width: 330
        ,style:{
            marginRight: '0px',
            paddingRight:'0px'
        }
        ,disabled:true
        ,renderTo: 'txtLongitude' //panel的renderTo   没有则不需要添加      
    });
    Ext.create('Ext.Button', {
        renderTo: Ext.get('mapTrigger'),
        text: '...',
        style: {
            marginBottom: '10px',
            marginLeft:'0px'
        },
        handler: function () {
            mo();
        }
    });
    var cmp = Ext.getCmp('txtLongitude');
    cmp.el.on('click', mo, cmp);
    function mo()
    {
        var coordinate = Ext.getCmp("txtLongitude").jitGetValue().split(',');
        var point = {};
        if (coordinate.length > 1) {
            point.lng = coordinate[0];
            point.lat = coordinate[1];
        }
        else {
            point.lng = point.lat = '';
        }
        var coordinate = Ext.getCmp("txtLongitude").jitGetValue().split(',');
        if (coordinate.length > 1) {
            $('#longitude').val(coordinate[0]);
            $('#latitude').val(coordinate[1]);
        }
        $('#mapSection').show();
        initMap();
    }
    Ext.create('Jit.form.field.Text', {
        id: "txtAddress",
        text: "",
        renderTo: "txtAddress",
        width: 330
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 530,
        height: 100,
        margin: '0 0 10 10'
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCreateUserName",
        text: "",
        renderTo: "txtCreateUserName",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCreateTime",
        text: "",
        renderTo: "txtCreateTime",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtModifyUserName",
        text: "",
        renderTo: "txtModifyUserName",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtModifyTime",
        text: "",
        renderTo: "txtModifyTime",
        readOnly: true,
        width: 100
    });

    //    Ext.create('Jit.form.field.Number', {
    //        id: "txtUnitFlag",
    //        value: "0",
    //        renderTo: "txtUnitFlag",
    //        width: 100,
    //        allowDecimals: true,
    //        decimalPrecision: 2
    //    });

    //    Ext.create('jit.biz.WApplicationInterface2', {
    //        id: "txtWeiXinId",
    //        text: "",
    //        renderTo: "txtWeiXinId",
    //        width: 100
    //            , fn: function () {
    //                Ext.getCmp("txtWXCode").setDefaultValue("");
    //                Ext.Ajax.request({
    //                    url: "/Module/Basic/Unit/Handler/UnitHandler.ashx?method=get_unit_by_id",
    //                    params: { EventID: getUrlParam("unit_id") },
    //                    method: 'POST', sync: true, async: false,
    //                    success: function (response) {
    //                        //var d = Ext.decode(response.responseText).topics;
    //                        //if (d != null) {
    //                        //    Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
    //                        //}
    //                        var d = Ext.decode(response.responseText).data;
    //                        if (d != null) {
    //                            //Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
    //                            //Ext.getCmp("txtModelId").fnLoad(function(){
    //                            //        if (Ext.getCmp("txtWeiXinId").jitGetValue() != "") 
    //                            //            Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
    //                            //        else
    //                            //            Ext.getCmp("txtModelId").jitSetValue("");
    //                            //    });
    //                            Ext.getCmp("txtWXCode").fnLoad(function () {
    //                                if (Ext.getCmp("txtWeiXinId").jitGetValue() != "")
    //                                    Ext.getCmp("txtWXCode").jitSetValue(getStr(d.QRCodeTypeId));
    //                                else
    //                                    Ext.getCmp("txtWXCode").jitSetValue("");
    //                            });
    //                        }
    //                    },
    //                    failure: function () {
    //                        Ext.Msg.alert("提示", "获取参数数据失败");
    //                    }
    //                });
    //            }
    //            , listeners: {
    //                select: function (store) {
    //                    //Ext.Ajax.request({
    //                    //    url: "/Module/WApplication/Handler/WApplicationHandler.ashx?method=search_wapplication",
    //                    //    params: { form: "{ \"WeiXinID\":\"" + Ext.getCmp("txtWeiXinPublic").jitGetValue() + "\" }" },
    //                    //    method: 'POST',
    //                    //    sync: true, async: false,
    //                    //    success: function (response) {
    //                    //        var d = Ext.decode(response.responseText).data;
    //                    //        if (d != null) {
    //                    //            //Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
    //                    //        }
    //                    //    },
    //                    //    failure: function () {
    //                    //        Ext.Msg.alert("提示", "获取参数数据失败");
    //                    //    }
    //                    //});
    //                    //Ext.getCmp("txtModelId").setDefaultValue("");
    //                    Ext.getCmp("txtWXCode").setDefaultValue("");
    //                }
    //            }
    //    });
    Ext.create('jit.biz.WQRCodeType', {
        id: "txtWXCode",
        text: "",
        renderTo: "txtWXCode",
        width: 100,
        c: true,
        parent_id: "txtWeiXinId"
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtWXCode2",
        text: "",
        renderTo: "txtWXCode2",
        readOnly: true,
        width: 100
    });
    //    Ext.create('Jit.form.field.Text', {
    //        id: "txtWebserversURL",
    //        text: "",
    //        renderTo: "txtWebserversURL",
    //        width: 330
    //    });

    Ext.create('Jit.form.field.Text', {
        id: "txtDimensionalCodeURL",
        text: "",
        renderTo: "txtDimensionalCodeURL",
        readOnly: true,
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        readOnly: true,
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtFtpImagerURL",
        text: "",
        renderTo: "txtFtpImagerURL",
        width: 330
    });
    Ext.create('jit.biz.UnitSort', {
        id: "txtUnitSort",
        text: "",
        renderTo: "txtUnitSort",
        multiSelect: true,
        width: 300
    });

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
            text: "保存",
            formBind: true,
            disabled: true,
            handler: fnSave
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });

    //上传图片
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("itemEditImageStore"),
        id: "gridImage",
        renderTo: "gridImage",
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
    columns: [{
        text: '操作',
        width: 60,
        sortable: true,
        dataIndex: 'ImageId',
        align: 'left',
        //hidden: __getHidden("delete"),
        renderer: function (value, p, record) {
            var str = "";
            var d = record.data;
            str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItemImage('" + value + "')\">删除</a>";
            return str;
        }
    }
        , {
            text: '图片地址',
            width: 210,
            sortable: true,
            dataIndex: 'ImageURL',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '排序',
            width: 60,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        ]
});

Ext.create('Jit.form.field.Text', {
    id: "txtImage_ImageUrl",
    text: "",
    readOnly: true,
    renderTo: "txtImage_ImageUrl",
    width: 100
});

Ext.create('Jit.form.field.Text', {
    id: "txtImage_DisplayIndex",
    text: "",
    renderTo: "txtImage_DisplayIndex",
    width: 100
});

Ext.create('Jit.button.Button', {
    text: "添加",
    renderTo: "btnAddImageUrl",
    //hidden: __getHidden("create"),
    handler: function () {
        fnAddImageUrl();
    }
});


// Brand
Ext.create('Ext.grid.Panel', {
    store: Ext.getStore("itemEditBrandStore"),
    id: "gridBrand",
    renderTo: "gridBrand",
    columnLines: true,
    height: 366,
    stripeRows: true,
    width: 400,
    selModel: Ext.create('Ext.selection.CheckboxModel', {
        mode: 'MULTI'
    }),
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBarBrand",
        defaultType: 'button',
        store: Ext.getStore("itemEditBrandStore"),
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
    width: 60,
    sortable: true,
    dataIndex: 'MappingId',
    align: 'left',
    //hidden: __getHidden("delete"),
    renderer: function (value, p, record) {
        var str = "";
        var d = record.data;
        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItemBrand('" + value + "')\">删除</a>";
        return str;
    }
}
        , {
            text: '品牌',
            width: 210,
            sortable: true,
            dataIndex: 'BrandName',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        ]
});

Ext.create('jit.biz.BrandDetail', {
    id: "txtBrand_List",
    text: "",
    renderTo: "txtBrand_List",
    width: 100
});

Ext.create('Jit.button.Button', {
    text: "添加",
    renderTo: "btnAddBrand",
    //hidden: __getHidden("create"),
    handler: function () {
        fnAddBrand();
    }
});

Ext.create('Jit.button.Button', {
    text: "获取二维码",
    renderTo: "btnWXImage",

    //hidden: __getHidden("create"),
    handler: function () {
        fnGetWXCode();
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