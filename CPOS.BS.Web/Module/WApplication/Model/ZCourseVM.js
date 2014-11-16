function InitVE() {
    Ext.define("ZCourseViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "CourseId", type: 'string' }, 
            { name: "CouseDesc", type: 'string' }, 
            { name: "CourseTypeId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "CourseName", type: 'string' }, 
            { name: "CourseSummary", type: 'string' }, 
            { name: "CourseFee", type: 'string' }
            ]
    });
    
    Ext.define("ZCourseApplyViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ApplyId", type: 'string' }, 
            { name: "ApplyName", type: 'string' }, 
            { name: "Company", type: 'string' }, 
            { name: "Post", type: 'string' }, 
            { name: "Email", type: 'string' }, 
            { name: "Phone", type: 'string' }, 
            { name: "CouseId", type: 'string' }, 
            { name: "IsPushEmail", type: 'string' }, 
            { name: "PushEmailFailure", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "OpenId", type: 'string' }, 
            { name: "Gender", type: 'string' }, 
            { name: "Class", type: 'string' }, 
            { name: "Tel", type: 'string' }, 
            { name: "Address", type: 'string' }
            ]
    });
    Ext.define("ZCourseReflectionsViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ReflectionsId", type: 'string' }, 
            { name: "CourseId", type: 'string' }, 
            { name: "StudentName", type: 'string' }, 
            { name: "StudentPost", type: 'string' }, 
            { name: "Content", type: 'string' }, 
            { name: "VideoURL", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

    Ext.define("ZCourseNewsViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "NewsId", type: 'string' }, 
            { name: "NewsType", type: 'string' }, 
            { name: "NewsTitle", type: 'string' }, 
            { name: "NewsSubTitle", type: 'string' }, 
            { name: "Content", type: 'string' }, 
            { name: "PublishTime", type: 'string' }, 
            { name: "ContentUrl", type: 'string' }, 
            { name: "ImageUrl", type: 'string' }, 
            { name: "ThumbnailImageUrl", type: 'string' }, 
            { name: "APPId", type: 'string' }, 
            { name: "NewsLevel", type: 'string' }, 
            { name: "ParentNewsId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });
}