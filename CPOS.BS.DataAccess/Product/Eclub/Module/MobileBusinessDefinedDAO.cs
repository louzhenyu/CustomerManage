using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess.Product.Eclub.Module
{
    public class MobileBusinessDefinedDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MobileBusinessDefinedDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region 获取个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="moduleCode">问卷编号</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public DataSet getUserByID(string mobileModuleID, string userID)
        {
            string sql = string.Format(@"
                            DECLARE @customerID NVARCHAR(400)='{0}' 
                            DECLARE @vipID NVARCHAR(400)='{1}' 
                            DECLARE @mobilemoduleID NVARCHAR(400)='{2}'

                            --取mobilepageblock信息
                            select MobilePageBlockID, TableName, Title, Type, Sort, ParentID, Remark, CustomerID, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete, MobileModuleID 
                            INTO #pageblock 
                            from mobilePageBlock mpb  
                            WHERE mpb.CustomerID=@customerID and mpb.IsDelete=0 AND mpb.MobileModuleID=@mobilemoduleID 
                            order by mpb.Type,mpb.Sort 

                            SELECT MobilePageBlockID, TableName, Title, Type, Sort, ParentID, Remark, CustomerID, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete, MobileModuleID FROM #pageblock 

                            --取mobilebusinessdefined信息
                            select mbd.MobileBussinessDefinedID, mbd.TableName, mbd.MobilePageBlockID, mbd.ColumnDesc, mbd.ColumnDescEn, mbd.ColumnName, mbd.LinkageItem, mbd.CorrelationValue, 
                            mbd.ExampleValue, mbd.ControlType, mbd.AuthType, mbd.MinLength, mbd.MaxLength, mbd.MinSelected, mbd.MaxSelected, mbd.IsMustDo, mbd.IsEdit, mbd.IsPrivacy, mbd.SearchOrder, mbd.ListShowOrder, 
                            mbd.EditOrder, mbd.ViewOrder, mbd.TypeID, mbd.CustomerID, mbd.CreateBy, mbd.CreateTime, mbd.LastUpdateBy, mbd.LastUpdateTime, mbd.IsDelete
                            ,pr.PrivacyRightID,pr.ObjectID,pr.VipID,pr.OperationStatus  
                            into #defined 
                            from MobileBussinessDefined as mbd 
                            INNER JOIN #pageblock as mpb on mpb.IsDelete=mbd.IsDelete and mbd.CustomerID=mpb.CustomerID and mbd.mobilePageBlockID=mpb.mobilePageBlockID 
                            LEFT JOIN EclubPrivacyRight pr ON mbd.MobileBussinessDefinedID=pr.ObjectID AND pr.VipID=@vipID AND mbd.IsPrivacy=1 AND mbd.CustomerID=pr.CustomerId AND mbd.IsDelete=pr.IsDelete 
                            where mbd.IsDelete=0 and mbd.CustomerID=@customerID 

                            select MobileBussinessDefinedID, TableName, MobilePageBlockID, ColumnDesc, ColumnDescEn, ColumnName, LinkageItem, CorrelationValue, 
                            ExampleValue, ControlType, AuthType, MinLength, MaxLength, MinSelected, MaxSelected, IsMustDo, IsEdit, IsPrivacy, SearchOrder, ListShowOrder, EditOrder, ViewOrder, TypeID, 
                            CustomerID, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete
                            ,PrivacyRightID,ObjectID,VipID,OperationStatus
                            from #defined 

                            --取Options信息
                            select OptionsID, DefinedID, OptionName, OptionValue, OptionText, OptionTextEn, Sequence, ClientID, ClientDistributorID, op.CreateBy, op.CreateTime, op.LastUpdateBy, op.LastUpdateTime, 
                            op.IsDelete, op.CustomerID from Options  as op 
                            inner join #defined as tt on tt.CorrelationValue=op.OptionName and ISNULL(tt.CorrelationValue,'')<>'' 
                            where ISNULL(op.CustomerID,@customerID)=@customerID and op.isdelete=0 
                            order by op.OptionText 

                            --取会员信息
                            select top 1 VIPID, VipName, VipLevel, VipCode, WeiXin, WeiXinUserId, Gender, Age, Phone, SinaMBlog, TencentMBlog, Birthday, Qq, Email, Status, VipSourceId, Integration, ClientID, RecentlySalesTime, RegistrationTime, CreateTime, CreateBy, LastUpdateTime, LastUpdateBy, IsDelete, APPID, HigherVipID, QRVipCode, City, CouponURL, CouponInfo, PurchaseAmount, PurchaseCount, DeliveryAddress, Longitude, Latitude, VipPasswrod, HeadImgUrl, Col1, Col2, Col3, Col4, Col5, Col6, Col7, Col8, Col9, Col10, Col11, Col12, Col13, Col14, Col15, Col16, Col17, Col18, Col19, Col20, Col21, Col22, Col23, Col24, Col25, Col26, Col27, Col28, Col29, Col30, Col31, Col32, Col33, Col34, Col35, Col36, Col37, Col38, Col39, Col40, Col41, Col42, Col43, Col44, Col45, Col46, Col47, Col48, Col49, Col50, VipRealName, isActivate, VIPImportID 
                            from Vip where VipId=@vipID AND ClientID=@customerID AND IsDelete=0 

                            --获取课程、等等
                            select class.ClassInfoID,Class.ClassInfoName,Course.CourseInfoID,Course.CourseInfoName,VipID
                            from EclubCourseInfo Course 
                            inner join EclubClassInfo Class on Class.CourseInfoID=Course.CourseInfoID and Class.CustomerId=Course.CustomerId and Class.IsDelete=0 
                            inner join EclubVipClassMapping Map on Map.ClassInfoID=Class.ClassInfoID and Map.CustomerId=Class.CustomerId and Map.IsDelete=0 
                            where VipID =@vipID and Map.CustomerId=@customerID and Course.IsDelete=0
                            order by Class.Sequence,Course.Sequence ;

                            --获取：27.省,28.市,29.县信息
                            select city_id,city1_name,city2_name,city3_name,city_code from T_City

                            --获取：104.籍贯（市），105.常驻，106.常往来信息
                            select VipCityMappingID, VipID, CityType, CityName, CityCode, Sequence, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete from EclubVipCityMapping Map
                            where Map.CustomerId=@customerID and Map.VipID=@vipID and Map.IsDelete=0;

                            --获取：行业信息
                            select Industry.IndustryID, ParentID, Industry.IndustryType, IndustryName, Industry.Sequence, Description, Industry.CustomerId, Industry.CreateBy, Industry.CreateTime, 
                            Industry.LastUpdateBy, Industry.LastUpdateTime, Industry.IsDelete,Map.IndustryType as IndustryType2 
                            from EclubIndustry Industry
                            inner join EclubVipIndustryMapping Map on Map.IndustryID=Industry.IndustryID and Map.CustomerId=Industry.CustomerId and Map.IsDelete=0
                            where Map.VipID=@vipID and Map.CustomerId=@customerID and Map.IsDelete=0

                            DROP TABLE #pageblock 
                            DROP TABLE #defined 

                            ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, userID, mobileModuleID);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 执行提交数据
        /// <summary>
        /// 执行提交数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int SubmitSql(string pSql)
        {
            return this.SQLHelper.ExecuteNonQuery(pSql);
        }
        #endregion

    }
}
