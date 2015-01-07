function InitView() {
    Ext.create('Ext.form.Panel', {
        collapsible: true,
        height: 1450,
        id: 'cuserinfo',
        header: {
            xtype: "header",
            headerAsText: false,
            items: [{
                id: "lab_info",
                labelPad: 5,
                lableWidth: 10,
                fieldStyle: "color:red;font-weight:bold;",
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>基本信息</b>',
                width: '200',
                value: ''
            }],
            layout: "hbox"
        },
        renderTo: "divmain",
        layout: 'anchor',
        width: '100%',
        bodyStyle: "background:#F1F2F5;padding-top:10px",
        items: [{
            xtype: "jitdisplayfield",
            fieldLabel: "客户号码",
            name: "userCode",
            id: "txtuserCode"

        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "客户名称",
            name: "userName",
            id: "txtuserName"
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "客户标识",
            name: "userID",
            id: "txtuserID"
        },
        {
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jitdisplayfield",
                labelWidth: 71,
                width: 71,
                fieldLabel: "<font color='red'>*</font>客户Logo"
            }, {
                html: '<img id="logopicture" src="" width="150px" height="80px">'    //这里直接用的html

            }, {
                xtype: 'label',
                html: '<input type="button" id="uploadImageUrl" value=" 选择图片 " style="margin-left: 10px" /><p style="line-height: 28px;font-size: 15px;color: red;margin-top: 20px;margin-left: 20px;">建议图片大小,高40\宽自动</p>'
            }]
        }, {

            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>客户分类",
            store: Ext.getStore("customeTypeStore"),//设置Store
            name: "CustomerType",
            id: "cmbCustomerType",
            valueField: "UnitSortId",   //设置值属性和显示属性
            displayField: "SortName"
        }, {
            xtype: "jittextarea",
            fieldLabel: "关于我们",       //<font color='red'>*</font>
            name: "kindeditorcontent",
            id: "txtAboutUs"

        }, {
            xtype: "jittextarea",
            fieldLabel: "品牌故事",      //<font color='red'>*</font>
            name: "kindeditorcontent",
            id: "txtBrandStory"
        }, {
            xtype: "jittextarea",
            fieldLabel: "品牌相关",               // <font color='red'>*</font>
            name: "kindeditorcontent",
            id: "txtBrandRelated"
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>积分抵用金额的比率",
            name: "IntegralAmountPer",
            id: "txtIntegralAmountPer",
               mouseWheelEnabled: false,   //鼠标不能滑动
                hideTrigger: true,
        keyNavEnabled: false,
           minValue: 0
   
        
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>手机短信签名",
            name: "SMSSign",
             width: 480,
            id: "txtSMSSign"
        },{
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jitdisplayfield",
                labelWidth: 71,
                width: 71,
                fieldLabel: "<font color='red'>*</font>转发消息图标"
            }, {
                html: '<img id="ForwardingMessageLogopicture" src="" width="150px" height="80px">'

            }, {
                xtype: 'label',
                html: '<input type="button" id="uploadForwardingImageUrl" value=" 选择图片 " style="margin-left: 10px" /><p style="line-height: 28px;font-size: 15px;color: red;margin-top: 20px;margin-left: 20px;">建议图片大小,高40\宽自动</p>'
            }]
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>转发消息默认标题",
            name: "ForwardingMessageTitle",
             width: 480,
            id: "txtForwardingMessageTitle"
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>转发消息默认摘要文字",
            name: "ForwardingMessageSummary",
             width: 480,
            id: "txtForwardingMessageSummary",
      
        }, {
            xtype: "jittextarea",
            fieldLabel: "<font color='red'>*</font>什么是通用积分",
            name: "kindeditorcontent",
            id: "txtWhatCommonPoints"
        }, {
            xtype: "jittextarea",
            fieldLabel: "<font color='red'>*</font>如何获得积分",
            name: "kindeditorcontent",
            id: "txtGetPoints"
        }, {
            xtype: "jittextarea",
            fieldLabel: "<font color='red'>*</font>如何消费积分",
            name: "kindeditorcontent",
            id: "txtSetSalesPoints"
        }
        ]
    });

    Ext.create('Ext.form.Panel', {
        collapsible: true,
        id: 'cuserinfocf',
        height: 380,
        header: {
            xtype: "header",
            headerAsText: false,
            items: [{
                id: "lbl_",
                labelPad: 5,
                lableWidth: 20,
                fieldStyle: "color:red;font-weight:bold;",
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>会员配置</b>',
                width: '200',
                value: ''
            }],
            layout: "hbox"
        },
        renderTo: "divmain",
        layout: 'anchor',
        width: '100%',
        bodyStyle: "background:#F1F2F5;padding-top:10px",
        items: [
            {
                layout: 'column',
                border: 0,
                valign: 'centre',
                items: [{
                    xtype: "jitdisplayfield",
                    labelWidth: 71,
                    width: 71,
                    fieldLabel: "<font color='red'>*</font>会员卡图片"
                },
  
{
html: '<img id="vippicture" src="" width="179px" height="100px">'

}
            , {
                xtype: 'label',
                html: '<p><input type="button" id="uploadCusImageUrl" value=" 选择图片 " /><p style="line-height: 28px;font-size: 15px;color: red;margin-top: 20px;margin-left: 20px;">建议图片大小,533*318</p>'

            }]
            },
           {
               xtype: "jittextfield",
               fieldLabel: "<font color='red'>*</font>客服电话",
               name: "CustomerMobile",
               id: "txtCustomerMobile",
               width:250
           },
           {
               xtype: "label",
               text: "如： 电话：13588888888 、 座机：029-888888 、 400电话：4008888888",
               style: "margin-left:90px; color:red;",
               width: 550
           },
            {
                xtype: "jittextarea",
                fieldLabel: "<font color='red'>*</font>会员权益",
                name: "kindeditorcontent",
                id: "txtMemberBenefits"
            }
             ]
    });

    Ext.create('Ext.form.Panel', {
        id: 'cuserserch',
        collapsible: true,
        header: {
            xtype: "header",
            headerAsText: false,
            height: '40',
            items: [{
                id: "lbl_Search",
                cls: 'divllabl',
                xtype: 'label',
                text: '会员查询配置',
                margin: '6 6 6 10'
            }],
            layout: "hbox"
        },
        renderTo: "divmain",
        layout: 'anchor',
        width: '100%',
        bodyStyle: "background:#F1F2F5;padding-top:10px",
        items: [{
            xtype: "jitnumberfield",
            fieldLabel: "附近门店范围设置",
            name: "RangeAccessoriesStores",
            id: "txtRangeAccessoriesStores"

        }, {
            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>顶部搜索及城市切换",
            store: Ext.getStore('serchCityStore'),
            valueField: "typevalue",
            displayField: "typename",
            name: "IsSearchAccessoriesStores",
            id: "cmbIsSearchAccessoriesStores"
        }, {

            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>全部门店",
            store: Ext.getStore("isAll"),
            valueField: "typevalue",
            displayField: "typename",
            name: "IsAllAccessoriesStores",
            id: "cmbIsAllAccessoriesStores"
        }]


    });

    Ext.create('Ext.form.Panel', {
        id: 'CsutomerWeixinPage',
        collapsible: true,
        header: {
            xtype: "header",
            headerAsText: false,
            height: '40',
            items: [{
                id: "WeixinPage",
                cls: 'divllabl',
                xtype: 'label',
                text: '用户微信配置',
                margin: '6 6 6 10'
            }],
            layout: "hbox"
        },
        renderTo: "divmain",
        layout: 'anchor',
        width: '100%',
        bodyStyle: "background:#F1F2F5;padding-top:10px",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "用户微信分享加关注页面",
            name: "ShareWeixinPage",
            id: "ShareWeixinPage",
            width: 600
        }]
    });


        Ext.create('Ext.form.Panel', {
        id: 'App',
        collapsible: true,
          hidden:false,
        header: {
            xtype: "header",
            headerAsText: false,
            height: '40',
            items: [{
                id: "lbl_App",
                cls: 'divllabl',
                xtype: 'label',
                text: '会员APP配置',
                margin: '6 6 6 10'
            }],
            layout: "hbox"
        },
        renderTo: "divmain",
        layout: 'anchor',
        width: '100%',
        bodyStyle: "background:#F1F2F5;padding-top:10px",
        items: [{
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jitdisplayfield",
                labelWidth: 71,
                width: 71,
                fieldLabel: "<font color='red'>*</font>主页Logo"
            }, {
                html: '<img id="AppLogo" src="" width="60px" height="60px">'

            }, {
                xtype: 'label',
                html: '&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<input type="button" id="uploadAppLogoImageUrl" value=" 选择图片 " style="margin-left: 10px;padding-left: 70px" />&nbsp&nbsp&nbsp<p style="line-height: 28px;font-size: 15px;color:                          red;margin-                        top: 20px;margin-left: 20px;padding-left: 70px;">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp建议图片大小112*112</p>'
            }]
        },{
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jitdisplayfield",
                labelWidth: 71,
                width: 71,
                fieldLabel: "<font color='red'>*</font>头部背景图片"
            }, {
                html: '<img id="AppTopBackground" src="" width="179px" height="100px">'

            }, {
                xtype: 'label',
                html: '<input type="button" id="uploadAppTopBackgroundImageUrl" value=" 选择图片 " style="margin-left: 10px"  /><p style="line-height: 28px;font-size: 15px;color:                          red;margin-                        top: 20px;margin-left: 20px;">建议图片大小640*235</p>'
            }]
        },
        {
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jittextfield",
                width: 480,
                fieldLabel: "配送费描述",
                name: "deliveryStrategy",
                id: "txtDeliveryStrategy",
            }]
        },
        {
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jittextfield",
                labelWidth:100,
                width: 150,
                fieldLabel: "订单金额大于等于",
                name: "amountEnd",
                id: "txtAmountEnd",
            }, {
                xtype: "jittextfield",
                labelWidth: 210,
                width:260,
                fieldLabel: "元时，配送费免费；否则，收取配送费",
                name: "deliveryAmount",
                id: "txtDeliveryAmount",
            }, {
                xtype: "jitdisplayfield",
                fieldLabel: "元",
            }

            ]
        }

        ]
    });


    Ext.create('Ext.form.Panel', {
        renderTo: "divmain",
        width: "100%",
        height: "100%",
        border: 1,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            text: "保存",
            formBind: true,
            disabled: true,
            hidden: false
            , jitIsHighlight: true
            , jitIsDefaultCSS: true,
            handler: fnSave
        }]
    });


}