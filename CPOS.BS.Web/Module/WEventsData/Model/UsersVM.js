function InitVE() {
    Ext.define("UsersViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'UserID', type: 'string' },
            { name: 'UserName', type: 'string' },
            { name: 'Company', type: 'string' },
            { name: 'JobTitle', type: 'string' },
            { name: 'Description', type: 'string' },
            { name: 'Status', type: 'string' },
            { name: 'UserType', type: 'string' },
            { name: 'ApprovedBy', type: 'string' },
            { name: 'ApprovedTime', type: 'string' },
            { name: 'ApprovedRemark', type: 'string' },
            { name: 'ImageUrl', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'Mobile', type: 'string' },
            { name: 'MobileOpenLevel', type: 'string' },
            { name: 'Email', type: 'string' },
            { name: 'EmailOpenLevel', type: 'string' },
            { name: 'CompanyAddress', type: 'string' },
            { name: 'IndustryId', type: 'string' },
            { name: 'SinaWeibo', type: 'string' },
            { name: 'WeiXin', type: 'string' },
            { name: 'LinkedIn', type: 'string' },
            { name: 'SinaWeiboOpenLevel', type: 'string' },
            { name: 'WeiXinOpenLevel', type: 'string' },
            { name: 'LinkedInOpenLevel', type: 'string' },
            { name: 'CurrentCity', type: 'string' },
            { name: 'HomeTown', type: 'string' },
            { name: 'TravelCities', type: 'string' },
            { name: 'TagDescription', type: 'string' },
            { name: 'ThumbnailImageUrl', type: 'string' },
            { name: 'ThumbnailHeight', type: 'string' },
            { name: 'ThumbnailWidth', type: 'string' },
            { name: 'MiddleImageUrl', type: 'string' },
            { name: 'MiddleImageHeight', type: 'string' },
            { name: 'MiddleImageWidth', type: 'string' },
            { name: 'OriginalImageUrl', type: 'string' },
            { name: 'OriginalImageHeight', type: 'string' },
            { name: 'OriginalImageWidth', type: 'string' },
            { name: 'SchoolID', type: 'string' },
            { name: 'YearID', type: 'string' },
            { name: 'SchoolInfo', type: 'string' },
            { name: 'DisplayIndex', type: 'string' },
            { name: 'IndustryName', type: 'string' },
            { name: 'YearName', type: 'string' },
            { name: 'CourseID', type: 'string' },
            { name: 'TagIds', type: 'string' },
            { name: 'ApplyTime', type: 'string' },
            { name: 'CheckInTime', type: 'string' },
            { name: 'ConnUserName', type: 'string' },
            { name: 'isNewPushBind', type: 'string' },
            { name: 'CheckInStatus', type: 'string' }
        ]
    });

    
    Ext.define("WEventUserMappingEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'SignUpID', type: 'string' },
            { name: 'EventID', type: 'string' },
            { name: 'VipID', type: 'string' },
            { name: 'UserName', type: 'string' },
            { name: 'Phone', type: 'string' },
            { name: 'City', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' }
        ]
    });

}