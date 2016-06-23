/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 13:59:40
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表SetoffTools的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SetoffToolsDAO : Base.BaseCPOSDAO, ICRUDable<SetoffToolsEntity>, IQueryable<SetoffToolsEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GeSetoffToolsListCount(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, string pSetoffEventID)
        {
            string sql = string.Empty;
            if (ApplicationType != "4")
            {
                sql = GetSetoffToolsListSql(entity, ApplicationType, pBeShareVipID, pSetoffEventID);
            }
            else
            {
                sql = GetSetoffToolsListSqlForSRT(entity, ApplicationType, pBeShareVipID, pSetoffEventID);
            }            
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetSetoffToolsList(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, int Page, int PageSize, string pSetoffEventID)
        {
            if (Page == 0)
            {
                Page = 1;
            }
            if (PageSize == 0)
            {
                PageSize = 10;
            }
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (ApplicationType != "4")
            {
                sql = GetSetoffToolsListSql(entity, ApplicationType, pBeShareVipID, pSetoffEventID);
            }
            else
            {
                sql = GetSetoffToolsListSqlForSRT(entity, ApplicationType, pBeShareVipID, pSetoffEventID);
            }   
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #region 供会员金矿使用
        private string GetSetoffToolsListSql(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, string pSetoffEventID)
        {
            StringBuilder sbSql = new StringBuilder();
            string CustomerID = this.CurrentUserInfo.CurrentUser.customer_id;
            string strApplicationType = string.Empty;
            string NoticePlatType = string.Empty;//平台类型  1=微信，2=APP员工，3=超级分销商
            switch (ApplicationType)
            {
                case "1":
                    strApplicationType = "3";//会员
                    NoticePlatType = "1";
                    break;
                case "2":
                    strApplicationType = "1";//员工
                    NoticePlatType = "2";
                    break;
                case "3":
                    strApplicationType = "2";//客服
                    NoticePlatType = "2";
                    break;
                case "4":
                    strApplicationType = "4";//超级分销
                    NoticePlatType = "3";
                    break;
            }
            sbSql.AppendFormat(@"select DisplayIndex = row_number() over(order by StartTime desc), * into #tmp from
                         (SELECT distinct ST.SetoffEventID,ST.CustomerId, ST.SetoffToolID AS 'SetoffToolID' ,ST.ObjectId, Name As 'SetOffToolName',CONVERT(varchar(100),cast(T.StartDate as datetime),23) AS 'StartTime',CONVERT(varchar(100),cast(T.EndDate as datetime),23) AS 'EndTime',ST.ToolType,T.OnLineRedirectUrl AS 'URL',
                         T.[Desc] AS 'Description', SUW.NoticePlatformType,ISNULL(NULL,0) AS 'ServiceLife',ISNULL(SP.BeShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN T_CTW_LEvent T ON  ST.ObjectId=T.CTWEventId
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{1}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.BeShareVipID='{2}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='CTW' AND ST.[Status]='10'
                        UNION ALL
                        SELECT distinct ST.SetoffEventID,ST.CustomerId,ST.SetoffToolID AS 'SetoffToolID',ST.ObjectId,CouponType.CouponTypeName AS 'SetOffToolName',CONVERT(varchar(100),cast(CouponType.BeginTime as datetime),23) AS 'StartTime',CONVERT(varchar(100),cast(CouponType.EndTime as datetime),23) AS 'EndTime',ST.ToolType,ISNULL(null,null) AS 'URL',
                        CouponType.CouponTypeDesc AS 'Description',SUW.NoticePlatformType,CouponType.ServiceLife AS 'ServiceLife',ISNULL(SP.BeShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN CouponType ON ST.ObjectId=CouponType.CouponTypeId
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{1}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.BeShareVipID='{2}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='Coupon' AND ST.[Status]='10'
                        UNION ALL
                        SELECT distinct  ST.SetoffEventID,ST.CustomerId,ST.SetoffToolID AS 'SetoffToolID',ST.ObjectId,Name AS 'SetOffToolName',CONVERT(varchar(100),cast(SetoffPoster.CreateTime as datetime),23) AS 'StartTime',  IsNull(null,null)as 'EndTime',ST.ToolType,ISNULL(null,null) AS 'URL',
                        ISNULL(SetoffPoster.[Desc],'') AS 'Description',SUW.NoticePlatformType,ISNULL(NULL,0) AS 'ServiceLife',ISNULL(SP.BeShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN SetoffPoster ON ST.ObjectId=SetoffPoster.SetoffPosterID
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{1}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.BeShareVipID='{2}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='SetoffPoster' AND ST.[Status]='10'
                        UNION ALL
                        SELECT distinct ST.SetoffEventID,ST.CustomerId,ST.SetoffToolID AS 'SetoffToolID',ST.ObjectId,Title AS 'SetOffToolName',CONVERT(varchar(100),cast(WMaterialText.CreateTime as datetime),23) AS 'StartTime',  IsNull(null,null)as 'EndTime',ST.ToolType,ISNULL(OriginalUrl,null) AS 'URL',
                        ISNULL(WMaterialText.[Text],'') AS 'Description',SUW.NoticePlatformType,ISNULL(NULL,0) AS 'ServiceLife',ISNULL(SP.BeShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN WMaterialText ON ST.ObjectId=WMaterialText.TextId AND ISNULL(WMaterialText.IsAuth,0)!=1
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{1}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.BeShareVipID='1' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='Material' AND ST.[Status]='10'
                        )AS a where 1=1 and SetoffEventID='{3}'
                        ", CurrentUserInfo.UserID, ApplicationType, pBeShareVipID, pSetoffEventID);
            if (CustomerID != null && CustomerID != "")
            {
                sbSql.Append(" AND CustomerID='" + CustomerID + "' ");
            }
            if (entity.ToolType != null && entity.ToolType != "")
            {
                sbSql.Append(" AND ToolType='" + entity.ToolType + "' ");
            }
            return sbSql.ToString();
        }
        #endregion

        #region 供超级分销使用
        private string GetSetoffToolsListSqlForSRT(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, string pSetoffEventID)
        {
            StringBuilder sbSql = new StringBuilder();
            string CustomerID = this.CurrentUserInfo.CurrentUser.customer_id;
            string strApplicationType = string.Empty;
            string NoticePlatType = string.Empty;//平台类型  1=微信，2=APP员工，3=超级分销商
            switch (ApplicationType)
            {
                case "1":
                    strApplicationType = "3";//会员
                    NoticePlatType = "1";
                    break;
                case "2":
                    strApplicationType = "1";//员工
                    NoticePlatType = "2";
                    break;
                case "3":
                    strApplicationType = "2";//客服
                    NoticePlatType = "2";
                    break;
                case "4":
                    strApplicationType = "4";//超级分销
                    NoticePlatType = "3";
                    break;
            }
            sbSql.AppendFormat(@"select DisplayIndex = row_number() over(order by StartTime desc), * into #tmp from
                         (SELECT distinct ST.SetoffEventID,ST.CustomerId, ST.SetoffToolID AS 'SetoffToolID' ,ST.ObjectId, Name As 'SetOffToolName',CONVERT(varchar(100),cast(T.StartDate as datetime),23) AS 'StartTime',CONVERT(varchar(100),cast(T.EndDate as datetime),23) AS 'EndTime',ST.ToolType,T.OnLineRedirectUrl AS 'URL',
                         T.[Desc] AS 'Description', SUW.NoticePlatformType,ISNULL(NULL,0) AS 'ServiceLife',ISNULL(SP.ShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN T_CTW_LEvent T ON  ST.ObjectId=T.CTWEventId
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{3}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='CTW' AND ST.[Status]='10'
                        UNION ALL
                        SELECT distinct ST.SetoffEventID,ST.CustomerId,ST.SetoffToolID AS 'SetoffToolID',ST.ObjectId,CouponType.CouponTypeName AS 'SetOffToolName',CONVERT(varchar(100),cast(CouponType.BeginTime as datetime),23) AS 'StartTime',CONVERT(varchar(100),cast(CouponType.EndTime as datetime),23) AS 'EndTime',ST.ToolType,ISNULL(null,null) AS 'URL',
                        CouponType.CouponTypeDesc AS 'Description',SUW.NoticePlatformType,CouponType.ServiceLife AS 'ServiceLife',ISNULL(SP.ShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN CouponType ON ST.ObjectId=CouponType.CouponTypeId
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{3}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='Coupon' AND ST.[Status]='10'
                        UNION ALL
                        SELECT distinct  ST.SetoffEventID,ST.CustomerId,ST.SetoffToolID AS 'SetoffToolID',ST.ObjectId,Name AS 'SetOffToolName',CONVERT(varchar(100),cast(SetoffPoster.CreateTime as datetime),23) AS 'StartTime',  IsNull(null,null)as 'EndTime',ST.ToolType,ISNULL(null,null) AS 'URL',
                        ISNULL(SetoffPoster.[Desc],'') AS 'Description',SUW.NoticePlatformType,ISNULL(NULL,0) AS 'ServiceLife',ISNULL(SP.ShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN SetoffPoster ON ST.ObjectId=SetoffPoster.SetoffPosterID
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{3}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='SetoffPoster' AND ST.[Status]='10'
                        UNION ALL
                        SELECT distinct ST.SetoffEventID,ST.CustomerId,ST.SetoffToolID AS 'SetoffToolID',ST.ObjectId,Title AS 'SetOffToolName',CONVERT(varchar(100),cast(WMaterialText.CreateTime as datetime),23) AS 'StartTime',  IsNull(null,null)as 'EndTime',ST.ToolType,ISNULL(OriginalUrl,null) AS 'URL',
                        ISNULL(WMaterialText.[Text],'') AS 'Description',SUW.NoticePlatformType,ISNULL(NULL,0) AS 'ServiceLife',ISNULL(SP.ShareVipID,0) as IsPush,ISNULL(IsOpen,0)as IsRead FROM SetoffTools ST
                        LEFT JOIN WMaterialText ON ST.ObjectId=WMaterialText.TextId AND ISNULL(WMaterialText.IsAuth,0)!=1
                        LEFT JOIN SetoffToolUserView SUW ON SUW.SetoffToolID=ST.SetoffToolID AND  UserID='{0}' AND NoticePlatformType='{3}'
                        LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID='{0}' AND SP.ShareVipType='{1}' WHERE 1=1 AND ST.ToolType='Material' AND ST.[Status]='10'
                        )AS a where 1=1 and SetoffEventID='{2}'
                        ", CurrentUserInfo.UserID, strApplicationType, pSetoffEventID, NoticePlatType);
            if (CustomerID != null && CustomerID != "")
            {
                sbSql.Append(" AND CustomerID='" + CustomerID + "' ");
            }
            if (entity.ToolType != null && entity.ToolType != "")
            {
                sbSql.Append(" AND ToolType='" + entity.ToolType + "' ");
            }
            return sbSql.ToString();
        }
        #endregion

        #endregion


        #region 获取集客工具列表
        /// <summary>
        /// 获取未读集客工具个数
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="NoticePlatformTypeId">平台编号1=微信用户 2=APP员工</param>
        /// <param name="SetoffTypeId">1=会员集客 2=员工集客 3=超级分销商</param>
        /// <returns></returns>
        public int GetUnReadSetoffToolsCount(string UserId, string CustomerId, int NoticePlatformTypeId, int SetoffTypeId)
        {
            var sql = @"SELECT COUNT(*) AS 'UnReadCount'
                        FROM SetoffTools AS st
						LEFT JOIN SetoffEvent AS se ON se.SetoffEventID=st.SetoffEventID
                        LEFT JOIN SetoffToolUserView  AS stuv ON 
                        st.SetoffToolID=stuv.SetoffToolID AND  stuv.UserId=@UserId 
						AND stuv.CustomerId=@CustomerId  AND stuv.NoticePlatformType=@NoticePlatformType --用户条件  商户条件
                        WHERE 
                        st.[Status]='10'  AND st.IsDelete='0' AND st.CustomerId=@CustomerId
						AND se.[Status]=10 AND se.IsDelete=0 AND se.SetoffType=@SetoffTypeId
                        AND (stuv.IsOpen IS NULL OR stuv.IsOpen=0)";

            sql = string.Format(sql, UserId, CustomerId, NoticePlatformTypeId, SetoffTypeId);

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@UserId",UserId),
                new SqlParameter("@CustomerId",CustomerId),
                new SqlParameter("@NoticePlatformType",NoticePlatformTypeId),
                new SqlParameter("@SetoffTypeId",SetoffTypeId)

            };

            var ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), parameter);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["UnReadCount"]);
            }
            return 0;
        }
        #endregion

        #region App展示是否分享或推送红点
        /// <summary>
        /// 获取未推送或分享数量
        /// </summary>
        /// <param name="ShareVipType">分享人ID</param>
        /// <param name="BeShareVipID">被分享人ID</param>
        /// <param name="BusTypeCode">分享类型</param>
        /// <returns></returns>
        public int GetIsPushCount(string ShareVipType, string BeShareVipID, string BusTypeCode,string SetOffEventID)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@ShareVipID", CurrentUserInfo.UserID);
            parm[1] = new SqlParameter("@BeShareVipID", BeShareVipID);
            parm[2] = new SqlParameter("@ShareVipType", ShareVipType);
            parm[3] = new SqlParameter("@BusTypeCode", BusTypeCode);
            parm[4] = new SqlParameter("@SetoffEventID", SetOffEventID);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat(@"select Count(ISNULL(SP.BeShareVipID,0))-Count(SP.BeShareVipID) FROM SetoffTools ST
	                            LEFT JOIN T_LEventsSharePersonLog SP ON SP.ObjectId=ST.ObjectId AND SP.ShareVipID=@ShareVipID 
                                AND SP.BeShareVipID=@BeShareVipID 
                                AND SP.ShareVipType=@ShareVipType
                                AND SP.BusTypeCode=@BusTypeCode
                                WHERE ST.SetoffEventID=@SetoffEventID AND ST.ToolType=@BusTypeCode ");
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text,sbSql.ToString(),parm));
        }
        #endregion

        #region 获取集客行动工具及关联信息
        public DataSet GetToolsDetails(string SetoffEventID)
        {

            string sql = string.Format(@"
                select a.SetoffToolID,a.ToolType,(isnull(b.IssuedQty,0)-isnull(b.IsVoucher,0)) as 'SurplusCount',b.ServiceLife,o.ImageUrl as 'SetoffPosterUrl',a.ObjectId,
	                case a.ToolType 
	                when 'CTW' then c.Name 
	                when 'Coupon' then b.CouponTypeName
	                else  d.Name end as 'Name',
	                case a.ToolType 
	                when 'CTW' then c.StartDate 
	                else  b.BeginTime end as 'BeginData',
	                case a.ToolType 
	                when 'CTW' then c.EndDate 
	                else  b.EndTime end as 'EndData',
	                a.ObjectId
                from SetoffTools as a 
                left join CouponType as b on a.objectid=b.coupontypeID and b.IsDelete=0 
                left join T_CTW_LEvent as c on a.objectid=c.CTWEventId and c.IsDelete=0 
                left join SetoffPoster as d on a.objectid=d.SetoffPosterID and d.IsDelete=0 
                left join ObjectImages as o on d.ImageId=o.ImageId and o.IsDelete=0  
                where a.IsDelete=0 and a.Status='10' and a.CustomerId='{1}'
                and a.setoffeventid='{0}'", SetoffEventID, this.CurrentUserInfo.ClientID);

            return this.SQLHelper.ExecuteDataset(sql);

        }
        #endregion
    }
}
