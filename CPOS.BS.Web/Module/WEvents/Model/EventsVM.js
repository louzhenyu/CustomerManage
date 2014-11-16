function InitVE() {
    Ext.define("EventsViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'EventID', type: 'string' },
            { name: 'EventType', type: 'string' },
            { name: 'EventTypeName', type: 'string' },
            { name: 'EventSubType', type: 'string' },
            { name: 'ItemID', type: 'string' },
            { name: 'EventStatus', type: 'int' },
            { name: 'Title', type: 'string' },
            { name: 'Description', type: 'string' },
            { name: 'FeeDesc', type: 'string' },
            { name: 'IsHotShow', type: 'string' },
            { name: 'HotShowEndDate', type: 'string' },
            { name: 'Longitude', type: 'string' },
            { name: 'Latitude', type: 'string' },
            { name: 'LongitudeBaidu', type: 'string' },
            { name: 'LatitudeBaidu', type: 'string' },
            { name: 'BeginTime', type: 'string' },
            { name: 'EndTime', type: 'string' },
            { name: 'ApplyCount', type: 'string' },
            { name: 'CheckInCount', type: 'string' },
            { name: 'RoundCount', type: 'string' },
            { name: 'PostCount', type: 'string' },
            { name: 'CityID', type: 'string' },
            { name: 'CityName', type: 'string' },
            { name: 'Contact', type: 'string' },
            { name: 'Content', type: 'string' },
            { name: 'PhoneNumber', type: 'string' },
            { name: 'Email', type: 'string' },
            { name: 'CheckinRange', type: 'string' },
            { name: 'Status', type: 'string' },
            { name: 'CheckinPriv', type: 'string' },
            { name: 'LastPostId', type: 'string' },
            { name: 'ApplyPriv', type: 'string' },
            { name: 'ImageUrl', type: 'string' },
            { name: 'ThumbnailImageUrl', type: 'string' },
            { name: 'Address', type: 'string' },
            { name: 'HasPrize', type: 'string' },
            { name: 'CommentCount', type: 'string' },
            { name: 'Originator', type: 'string' },
            { name: 'OriginatorType', type: 'string' },
            { name: 'IsDefault', type: 'string' },
            { name: 'IsTop', type: 'string' },
            { name: 'Organizer', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' },
            { name: 'ApplyQuesID', type: 'string' },
            { name: 'PollQuesID', type: 'string' },
            { name: 'AppliesCount', type: 'string' },
            { name: 'PrizesCount', type: 'string' },
            { name: 'CheckinsCount', type: 'string' }
            , { name: 'WXCode', type: 'string' }
            , { name: 'WXCodeImageUrl', type: 'string' }
            , { name: 'CustomerId', type: 'string' }
            , { name: 'IsPointsLottery', type: 'string' }
            , { name: 'PointsLottery', type: 'string' }
            , { name: 'RewardPoints', type: 'string' }
        ]
    });

    Ext.define("EventVipViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'SignUpID', type: 'string' }
            , { name: 'VipId', type: 'string' }
            , { name: 'UserName', type: 'string' }
            , { name: 'VipCompany', type: 'string' }
            , { name: 'VipPost', type: 'string' }
            , { name: 'Phone', type: 'string' }
            , { name: 'Email', type: 'string' }
            , { name: 'Seats', type: 'string' }
            , { name: 'Profile', type: 'string' }
            , { name: 'HeadImage', type: 'string' }
            , { name: 'CreateTime', type: 'string' }
            , { name: 'CreateBy', type: 'string' }
            , { name: 'LastUpdateTime', type: 'string' }
            , { name: 'LastUpdateBy', type: 'string' }
            , { name: 'IsDelete', type: 'string' }
            , { name: 'CustomerId', type: 'string' }
            , { name: 'DCodeImageUrl', type: 'string' }
            , { name: 'VIPID', type: 'string' }
            , { name: 'IsRegistered', type: 'boolean' }
            , { name: 'IsSigned', type: 'boolean' }
            , { name: 'CanLottery', type: 'boolean' }

        ]
    })

    Ext.define("DrawMethodEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'DrawMethodID', type: 'int' },
            { name: 'DrawMethodName', type: 'string' }
        ]
    });
    Ext.define("ModelEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });
    Ext.define("FlagEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });

    Ext.define("ModuleEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MobileModuleID', type: 'string' },
            { name: 'ModuleName', type: 'string' }
        ]
    });
}