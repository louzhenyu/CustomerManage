using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;
using System.Collections;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.DataAccess
{
    public class AlumniDetailDAO : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public AlumniDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {

        }
        #endregion

        #region 获取校友详细
        /// <summary>
        /// 获取校友原始信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="alumniID">校友ID</param>
        /// <param name="mobileModuleID">问卷ID</param>
        /// <returns></returns>
        public DataSet GetAlumniUntreatedInfo_V1(string userID, string alumniID, string mobileModuleID)
        {
            //Declar Sql Variant
            string sql = string.Format(@"
                            DECLARE @customerID NVARCHAR(50)='{0}';
                            DECLARE @vipID NVARCHAR(50)='{1}' ;
                            DECLARE @mobilemoduleID NVARCHAR(50)='{2}';
                            DECLARE @aluminiVipID NVARCHAR(50)='{3}' ;

                            --取mobilepageblock信息
                            select MobilePageBlockID, TableName, Title, Type, Sort, ParentID, Remark, CustomerID, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete, MobileModuleID  
                            INTO #pageblock 
                            from mobilePageBlock mpb  
                            WHERE mpb.CustomerID=@customerID and mpb.IsDelete=0 AND mpb.MobileModuleID=@mobilemoduleID 
                            order by mpb.Type,mpb.Sort ;

                            SELECT MobilePageBlockID, TableName, Title, Type, Sort, ParentID, Remark, CustomerID, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete, MobileModuleID
                            FROM #pageblock ;

                            --取mobilebusinessdefined信息
                            select mbd.MobileBussinessDefinedID, mbd.TableName, mbd.MobilePageBlockID, mbd.ColumnDesc, mbd.ColumnDescEn, mbd.ColumnName, mbd.LinkageItem, mbd.CorrelationValue, 
                            mbd.ExampleValue, mbd.ControlType, mbd.AuthType, mbd.MinLength, mbd.MaxLength, mbd.MinSelected, mbd.MaxSelected, mbd.IsMustDo, mbd.IsEdit, mbd.IsPrivacy, mbd.SearchOrder, mbd.ListShowOrder, 
                            mbd.EditOrder, mbd.ViewOrder, mbd.TypeID, mbd.CustomerID, mbd.CreateBy, mbd.CreateTime, mbd.LastUpdateBy, mbd.LastUpdateTime, mbd.IsDelete
                            ,pr.PrivacyRightID,pr.ObjectID,pr.VipID,pr.OperationStatus
                            into #defined 
                            from MobileBussinessDefined as mbd 
                            INNER JOIN #pageblock as mpb on mpb.IsDelete=mbd.IsDelete and mbd.CustomerID=mpb.CustomerID and mbd.mobilePageBlockID=mpb.mobilePageBlockID 
                            LEFT JOIN EclubPrivacyRight pr ON mbd.MobileBussinessDefinedID=pr.ObjectID AND pr.VipID=@aluminiVipID AND mbd.IsPrivacy=1 AND mbd.CustomerID=pr.CustomerId AND mbd.IsDelete=pr.IsDelete 
                            where mbd.IsDelete=0 and mbd.CustomerID=@customerID 

                            select MobileBussinessDefinedID, TableName, MobilePageBlockID, ColumnDesc, ColumnDescEn, ColumnName, LinkageItem, CorrelationValue, 
                            ExampleValue, ControlType, AuthType, MinLength, MaxLength, MinSelected, MaxSelected, IsMustDo, IsEdit, IsPrivacy, SearchOrder, ListShowOrder, EditOrder, ViewOrder, TypeID, 
                            CustomerID, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete
                            ,PrivacyRightID,ObjectID,VipID,OperationStatus 
                            from #defined ;

                            --取Options信息
                            select OptionsID, DefinedID, OptionName, OptionValue, OptionText, OptionTextEn, Sequence, ClientID, ClientDistributorID, op.CreateBy, op.CreateTime, op.LastUpdateBy, op.LastUpdateTime, 
                            op.IsDelete, op.CustomerID  
                            from Options  as op 
                            inner join #defined as tt on tt.CorrelationValue=op.OptionName and ISNULL(tt.CorrelationValue,'')<>'' 
                            where ISNULL(op.CustomerID,@customerID)=@customerID and op.isdelete=0 
                            order by op.OptionText ;

                            --取会员(用户、校友)信息
                            select distinct top 2 v.*,mark.BookMarkType from Vip v
                            left join EclubVipBookMark mark on v.VIPID=mark.ObjectID and mark.CustomerId=v.ClientID and mark.IsDelete=0 
                            and mark.VIPID=@vipID 
                            where v.VIPID in(@vipID,@aluminiVipID) AND ClientID=@customerID AND v.IsDelete=0 ;

                            --获取课程、等等
                            select class.ClassInfoID,Class.ClassInfoName,Course.CourseInfoID,Course.CourseInfoName,VipID
                            from EclubCourseInfo Course 
                            inner join EclubClassInfo Class on Class.CourseInfoID=Course.CourseInfoID and Class.CustomerId=Course.CustomerId and Class.IsDelete=0 
                            inner join EclubVipClassMapping Map on Map.ClassInfoID=Class.ClassInfoID and Map.CustomerId=Class.CustomerId and Map.IsDelete=0 
                            where VipID in(@vipID,@aluminiVipID) and Map.CustomerId=@customerID and Course.IsDelete=0
                            order by Class.Sequence,Course.Sequence ;

                            --获取：27.省,28.市,29.县信息
                            select city_id,city1_name,city2_name,city3_name,city_code from T_City

                            --获取：104.籍贯（市），105.常驻，106.常往来信息
                            select VipCityMappingID, VipID, CityType, CityName, CityCode, Sequence, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete from EclubVipCityMapping Map 
                            where Map.CustomerId=@customerID and Map.VipID=@aluminiVipID and Map.IsDelete=0;

                            --获取：行业信息
                            select Industry.IndustryID, ParentID, Industry.IndustryType, IndustryName, Industry.Sequence, Description, Industry.CustomerId, Industry.CreateBy, Industry.CreateTime, 
                            Industry.LastUpdateBy, Industry.LastUpdateTime, Industry.IsDelete,Map.IndustryType as IndustryType2  
                            from EclubIndustry Industry
                            inner join EclubVipIndustryMapping Map on Map.IndustryID=Industry.IndustryID and Map.CustomerId=Industry.CustomerId and Map.IsDelete=0
                            where Map.VipID=@aluminiVipID and Map.CustomerId=@customerID and Map.IsDelete=0

                            DROP TABLE #pageblock;
                            DROP TABLE #defined; ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, userID, mobileModuleID, alumniID);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 创建用户
        /// <summary>
        /// 根据班级ID创建用户
        /// </summary>
        /// <param name="classInfoID">班级ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>Return UserID</returns>
        public string CreateUserOper(string classInfoID, string userID)
        {
            //Declare Variable
            string vipID = string.Empty;

            //Instance Append Object
            StringBuilder sbSQL_Vip = new StringBuilder();
            StringBuilder sbSQL_Map = new StringBuilder();
            //Transaction Operation
            using (SqlTransaction trans = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(userID))
                    {
                        vipID = NewGuid();
                    }
                    else
                    {
                        vipID = userID;
                    }

                    int execRes = 0;
                    sbSQL_Vip.AppendFormat("if not exists(select top 1 1 from Vip where VIPID = '{0}' ) ", vipID);
                    sbSQL_Vip.Append(" begin ");
                    sbSQL_Vip.Append("insert into Vip(VIPID,VipLevel,[Status],ClientID,WeiXinUserId) ");
                    sbSQL_Vip.AppendFormat("values('{0}',100,1,'{1}','{0}') ; ", vipID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbSQL_Vip.Append(" end else begin ");
                    sbSQL_Vip.AppendFormat("update Vip set VipLevel = 100,[Status] = 1,ClientID='{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbSQL_Vip.AppendFormat("where VIPID='{0}' ; ", vipID);
                    sbSQL_Vip.Append(" end ");

                    //Vip Record Information
                    execRes += this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL_Vip.ToString());

                    sbSQL_Map.AppendFormat("if not exists(select top 1 1 from EclubVipClassMapping where VipID='{0}' and ClassInfoID='{1}' ) ", vipID, classInfoID);
                    sbSQL_Map.Append(" begin ");
                    sbSQL_Map.Append("insert into EclubVipClassMapping(VipID,ClassInfoID,CustomerId) ");
                    sbSQL_Map.AppendFormat("Values ('{0}','{1}','{2}'); ", vipID, classInfoID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbSQL_Map.Append(" end else begin ");
                    sbSQL_Map.AppendFormat("update EclubVipClassMapping set IsDelete = 0,ClassInfoID='{0}' ", classInfoID);
                    sbSQL_Map.AppendFormat("where VipID = '{0}' and CustomerId = '{1}' ; ", vipID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbSQL_Map.Append(" end ");

                    //Vip Mapping Class Relation
                    execRes += this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL_Map.ToString());

                    if (execRes >= 2)
                    {
                        //Commit
                        trans.Commit();
                    }
                    else
                    {
                        //Rollback
                        trans.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    vipID = string.Empty;
                    //Rollback
                    trans.Rollback();
                    throw;
                }
            }
            return vipID;
        }
        #endregion

        #region 用户审核
        /// <summary>
        /// 审核该校友是否班级正确
        /// </summary>
        /// <param name="alumniID"></param>
        /// <param name="classInfoID"></param>
        /// <param name="isAudit"></param>
        /// <returns></returns>
        public int AuditUserOper(string alumniID, string classInfoID, bool isAudit)
        {
            //Instance Append Obj
            StringBuilder sbSQL = new StringBuilder();

            if (isAudit)
            {
                //Authorize
                sbSQL.Append("update Vip set [Status] = 2 ");
                sbSQL.AppendFormat("where Vip='{1}' ;", alumniID);
            }
            else
            {
                //Not Pass
                sbSQL.Append("update EclubVipClassMapping set IsDelete = 1 ");
                sbSQL.AppendFormat("where VipID='{0}' and ClassInfoID='{1}' and CustomerId='{2}' ;", alumniID, classInfoID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            }

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }
        #endregion
    }
}
