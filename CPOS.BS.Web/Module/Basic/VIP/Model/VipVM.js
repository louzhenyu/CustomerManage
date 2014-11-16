function InitVE() {
    Ext.define("VipViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "VIPID", type: 'string' },
            { name: "VipName", type: 'string' },
            { name: "VipLevel", type: 'string' },
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
            { name: "VipSourceId", type: 'string' },
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
            { name: "CouponURL", type: 'string' },
            { name: "CouponInfo", type: 'string' },
            { name: "PurchaseAmount", type: 'string' },
            { name: "PurchaseCount", type: 'string' },
            { name: "CustomerId", type: 'string' },
            { name: "VipStatusId", type: 'string' }
            ]
    });

}