Ext.define("ContorlQuestionnaireTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping: "QuestionnaireID",
        type: "string"
    }, {
        name: "value",
        mapping: "QuestionnaireID",
        type: "string"
    }, {
        name: "text",
        mapping: "QuestionnaireName",
        type: "string"
    }]
});

// QuestionnaireType 业务控件
Ext.define('jit.biz.QuestionnaireType', {
    alias: 'widget.jitbizquestionnairetype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlQuestionnaireTypeEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store) {
                    store.insert(0, {
                        "name": args.Name,
                        "value": '',
                        "text": "--请选择--"
                    });
                }
            }
        });
        if (args.dataType == undefined || args.dataType == null) {
            args.dataType = "QuestionnaireType";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/QuestionnaireTypeHandler.ashx?method1=" + args.dataType;
        //store.load({
        //    //limit: 10,
        //    //page: 0
        //});
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);

        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.setDefaultValue = function (defValue) {
            store.load({
                params: {}
                , callback: function (r, options, success) {
                    for (var i = 0; i < r.length; i++) {
                        var rawValue = r[i].data.id;
                        if (rawValue == defValue) {
                            result.setValue(rawValue);
                        }
                    }
                }
            });
        };

        return result;
    }
})