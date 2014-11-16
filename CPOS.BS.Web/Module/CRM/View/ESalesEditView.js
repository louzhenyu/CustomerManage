function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtSalesName",
        text: "",
        renderTo: "txtSalesName",
        width: 100
    });
    Ext.create('jit.biz.ESalesProduct', {
        id: "txtSalesProductId",
        text: "",
        renderTo: "txtSalesProductId",
        width: 100
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtEndDate",
        text: "",
        renderTo: "txtEndDate",
        width: 100
    });
    Ext.create('jit.biz.ECCustomerSelect', {
        id: "txtEnterpriseCustomerId",
        text: "",
        renderTo: "txtEnterpriseCustomerId",
        fnCallback: function(d) {
            get("hECCustomerId").value = d.id;
        },
        width: 100
    });
    //Ext.create('jit.biz.ESalesVisitVip', {
    //    id: "txtSalesPerson",
    //    text: "",
    //    renderTo: "txtSalesPerson",
    //    multiSelect: true,
    //    width: 100
    //});
    Ext.create('jit.biz.ESalesChargeVip', {
        id: "txtSalesVipId",
        text: "",
        renderTo: "txtSalesVipId",
        width: 100
    });
    Ext.create('jit.biz.ESalesStage2', {
        id: "txtStageId",
        text: "",
        renderTo: "txtStageId",
        width: 200
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtPossibility",
        text: "",
        renderTo: "txtPossibility",
        width: 100
    });
    Ext.create('jit.biz.EEnterpriseCustomerSource', {
        id: "txtECSourceId",
        text: "",
        renderTo: "txtECSourceId",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtForecastAmount",
        text: "",
        renderTo: "txtForecastAmount",
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 550,
        height: 100,
        margin: "0 0 0 10"
    });


    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "300",
        height: "100%",
        border: 0,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [
        {
            xtype: "jitbutton",
            id: "btnSave",
            text: "保存销售线索",
            formBind: true,
            disabled: true,
            hidden: false,
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            id: "btnNewVisit",
            text: "新增拜访",
            formBind: true,
            disabled: true,
            hidden: false,
            handler: function() {
                return fnCreateSalesVisit();
            }
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        }
        //{
        //    xtype: "jitbutton",
        //    text: "关闭",
        //    handler: fnClose
        //}
        ]
    });

    
    Ext.create('Jit.form.field.Text', {
        id: "tbECSearchCustomerName",
        text: "",
        renderTo: "tbECSearchCustomerName",
        width: 180
    });
    Ext.create('Jit.button.Button', {
        text: "搜索",
        renderTo: "tbECSearchCustomerGo",
        width: 70,
        handler: fnECSearchCustomerGo
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "清除",
        renderTo: "tbECSearchCustomerClear",
        width: 70,
        handler: fnECSearchCustomerClear
    });
    
}