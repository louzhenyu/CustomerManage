function InitVE() {
    Ext.define("VipViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "VIPID", type: 'string' },
            { name: "VipName", type: 'string' },
            { name: "VipLevel", type: 'string' },
            { name: "VipLevelDesc", type: 'string' },
            { name: "VipCode", type: 'string' },
            { name: "WeiXin", type: 'string' },
            { name: "WeiXinUserId", type: 'string' },
            { name: "Gender", type: 'string' },
            { name: "Age", type: 'string' },
            { name: "Phone", type: 'string' },
            { name: "SinaMBlog", type: 'string' },
            { name: "TencentMBlog", type: 'string' },
            { name: "Birthday", type: 'string' },
            { name: "Qq", type: 'string' },
            { name: "Email", type: 'string' },
            { name: "Status", type: 'string' },
            { name: "StatusDesc", type: 'string' },
            { name: "VipSourceId", type: 'string' },
            { name: "VipSourceName", type: 'string' },
            { name: "Integration", type: 'string' },
            { name: "ClientID", type: 'string' },
            { name: "RecentlySalesTime", type: 'string' },
            { name: "RegistrationTime", type: 'string' },
            { name: "CreateTime", type: 'string' },
            { name: "CreateBy", type: 'string' },
            { name: "LastUpdateTime", type: 'string' },
            { name: "LastUpdateBy", type: 'string' },
            { name: "IsDelete", type: 'string' },
            { name: "APPID", type: 'string' },
            { name: "HigherVipID", type: 'string' },
            { name: "QRVipCode", type: 'string' },
            { name: "City", type: 'string' },
            { name: "LastUnit", type: 'string' },
            { name: "NextLevelIntegralAmount", type: 'string' },
            { name: "IntegralAmount", type: 'string' },
            { name: 'IntegralForHightUser', type: 'string' }
            , { name: 'MembershipShop', type: 'string' }
            , { name: 'VipTagsShort', type: 'string' }
            , { name: 'VipTagsLong', type: 'string' }
            , { name: 'VipTagsCount', type: 'string' }
            , { name: 'SearchIntegral', type: 'string' }
            , { name: 'UnitCount', type: 'string' }
            , { name: 'UnitSalesAmount', type: 'string' }
            , { name: 'VipCount', type: 'string' }
            , { name: 'PurchaseAmount', type: 'string' }
            , { name: 'UserName', type: 'string' }
            , { name: 'UserId', type: 'string' }
            , { name: 'UnitId', type: 'string' }
            , { name: 'SearchAmount', type:'string' }
            ]
    });

    
    Ext.define("VipIntegralDetailViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'VipIntegralDetailID', type: 'string' }, 
            { name: 'VIPID', type: 'string' }, 
            { name: 'SalesAmount', type: 'string' }, 
            { name: 'Integral', type: 'string' }, 
            { name: 'IntegralSourceID', type: 'string' }, 
            { name: 'IntegralSourceName', type: 'string' },
            { name: 'FromVipName', type: 'string' },
            { name: 'Remark', type: 'string' },
            { name: 'DeadlineDate', type: 'string' }, 
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }, 
            { name: 'EffectiveDate', type: 'string' }, 
            { name: 'IsAdd', type: 'string' }
            ]
    });
    
    
    Ext.define("InoutOrderDetailItemViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'order_detail_id', type: 'string' },
            { name: 'sku_id', type: 'string' },
            { name: 'item_code', type: 'string' },
            { name: 'item_name', type: 'string' },
            { name: 'enter_price', type: 'string' },
            { name: 'std_price', type: 'string' },
            { name: 'discount_rate', type: 'string' },
            { name: 'retail_price', type: 'string' },
            { name: 'remark', type: 'string' },
            { name: 'enter_qty', type: 'string' },
            { name: 'order_qty', type: 'string' },
            { name: 'enter_amount', type: 'string' },
            { name: 'retail_amount', type: 'string' },
            { name: 'prop_1_detail_name', type: 'string' },
            { name: 'prop_2_detail_name', type: 'string' },
            { name: 'prop_3_detail_name', type: 'string' },
            { name: 'prop_4_detail_name', type: 'string' },
            { name: 'prop_5_detail_name', type: 'string' },
            { name: 'display_name', type: 'string' },
            { name: 'create_time', type: 'string' },
            { name: 'unit_name', type: 'string' }
            ]
    });

    Ext.define("VipCollectionDataViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'VIPID', type: 'string' },
            { name: 'ParameterName', type: 'string' },
            { name: 'ParameterValue', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });
    
    
    Ext.define("VipTagsViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MappingId', type: 'string' },
            { name: 'VipId', type: 'string' },
            { name: 'TagsId', type: 'string' },
            { name: 'TagsName', type: 'string' },
            { name: 'TagsDesc', type: 'string' },
            { name: 'TagsFormula', type: 'string' },
            { name: 'TypeName', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });
}