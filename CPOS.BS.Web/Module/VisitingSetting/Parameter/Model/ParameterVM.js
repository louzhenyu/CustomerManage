﻿function InitVE() {
    Ext.define("OrderViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "OptionsID",
            type: "int"
        }, {
            name: "OptionName",
            type: "string"
        }, {
            name: "OptionValue",
            type: "int"
        }, {
            name: "OptionText",
            type: "string"
        }, {
            name: "OptionTextEn",
            type: "string"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }, {
            name: "CreateBy",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }, {
            name: "LastUpdateBy",
            type: "int"
        }, {
            name: "LastUpdateTime",
            type: "datetime"
        }]
    });

    Ext.define("VisitingParameterEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingParameterID",
            type: "string"
        }, {
            name: "ParameterType",
            type: "int"
        }, {
            name: "ParameterName",
            type: "string"
        }, {
            name: "ParameterNameEn",
            type: "string"
        }, {
            name: "ControlType",
            type: "int"
        }, {
            name: "ControlName",
            type: "string"
        }, {
            name: "MaxValue",
            type: "string"
        }, {
            name: "MinValue",
            type: "string"
        }, {
            name: "DefaultValue",
            type: "string"
        }, {
            name: "Scale",
            type: "int"
        }, {
            name: "UnitID",
            type: "int"
        }, {
            name: "Weight",
            type: "string"
        }, {
            name: "IsMustDo",
            type: "int"
        }, {
            name: "IsRemember",
            type: "int"
        }, {
            name: "IsVerify",
            type: "int"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }, {
            name: "CreateBy",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }, {
            name: "LastUpdateBy",
            type: "int"
        }, {
            name: "LastUpdateTime",
            type: "datetime"
        }, {
            name: "ParameterTypeText",
            type: "string"
        }, {
            name: "ControlTypeText",
            type: "string"
        }]
    });

    Ext.define("UnitEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "UnitID",
            type: "int"
        }, {
            name: "UnitName",
            type: "string"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }]
    });
}