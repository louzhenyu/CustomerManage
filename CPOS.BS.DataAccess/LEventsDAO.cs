/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表LEvents的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventsDAO : Base.BaseCPOSDAO, ICRUDable<LEventsEntity>, IQueryable<LEventsEntity>
    {
        #region 获取定制酒介绍
        /// <summary>
        /// 获取定制酒介绍
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkus()
        {
            string sql = "  "
                + " SELECT skuId = s.sku_id, imageURL = i.imageUrl, gg = s.sku_prop_id1,  "
                + "        degree = s.sku_prop_id2, baseWineYear = s.sku_prop_id3,  "
                + "        itemName = i.item_name, agePitPits = s.sku_prop_id5, "
                + "        itemId = i.item_id "
                + " FROM dbo.T_Sku s  "
                + " LEFT JOIN dbo.T_Item i ON s.item_id = i.item_id "
                + " WHERE s.status = 1 "
                + " AND s.sku_id = (SELECT TOP 1 sku_id FROM dbo.T_Sku s1 WHERE s1.status = 1 AND s1.item_id = i.item_id) ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取定制酒详情
        /// <summary>
        /// 获取定制酒详情
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkuDetail(string skuId)
        {
            string sql = "  "
                + " SELECT skuId = s.sku_id, imageURL = i.imageUrl, gg = s.sku_prop_id1,  "
                + "        degree = s.sku_prop_id2, baseWineYear = s.sku_prop_id3,   "
                + "        itemName = i.item_name, agePitPits = s.sku_prop_id5, "
                + "        salesPrice = CONVERT(VARCHAR, CONVERT(INT, vsp1.price)), "
                + "        purchasePrice = CONVERT(VARCHAR, CONVERT(INT, vsp2.price)), "
                + "        itemRemark = i.item_remark, itemId=i.item_Id, "
                + "        unit = '元/'+(SELECT x.prop_value FROM dbo.T_Item_Property x WHERE x.item_id = i.item_id AND x.prop_id = '4F18DDB482654F4A9C01E607AFE0A236')"
                + " FROM dbo.T_Sku s  "
                + " LEFT JOIN dbo.T_Item i ON s.item_id = i.item_id "
                + " LEFT JOIN vw_sku_price vsp1 ON vsp1.sku_id = s.sku_id AND vsp1.item_price_type_id = '8A327C5BB7A44FC6B96AB1A4A70EEAC9' "
                + " LEFT JOIN vw_sku_price vsp2 ON vsp2.sku_id = s.sku_id AND vsp2.item_price_type_id = 'F52356521CEC4194872784F8E26C69DE' "
                + " WHERE s.status = 1 AND s.sku_id = '" + skuId + "' ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取活动详情(活动介绍)
        /// <summary>
        /// 获取活动详情(活动介绍)
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventInfo(string eventId)
        {
            string sql = "  "
                + " SELECT l1.EventID, l2.ImageURL, l1.Title,  "
                + "        l1.BeginTime, l1.EndTime, l1.Address,   "
                + "        l2.Description, l1.CityID "
                + " FROM dbo.LEvents l1 "
                + " LEFT JOIN dbo.LEvents l2 ON l2.EventID = l1.ParentEventID  "
                + " WHERE l1.EventID = '" + eventId + "'";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 是否在现场
        /// <summary>
        /// 是否在现场
        /// </summary>
        /// <param name="eventId">活动ID</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <returns></returns>
        public int IsSite(string eventId, string latitude, string longitude, float distance)
        {
            string sql = "  "
                + " SELECT COUNT(*) FROM dbo.LEvents "
                + " WHERE EventID = '87747791A95442F5B8D5AC205D51BDC3' "
                + " AND dbo.DISTANCE_TWO_POINTS('" + latitude + "','" + longitude + "',Latitude,Longitude) < " + distance;

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取列表信息
        public DataSet GetMessageEventList(LEventsEntity entity)
        {
            string sql = "select a.*  ";
            sql += " ,(select count(EventLevel) from LEvents where EventID=a.EventID) signUpCount ";
            sql += " ,(SELECT SUM(total_amount) FROM dbo.T_Inout WHERE Field18 =a.EventID) salesAmount ";
            sql += " ,DATEDIFF(dd,GETDATE(),a.BeginTime) distanceDays ";
            sql += " from LEvents a ";
            sql += " where a.IsDelete='0' ";
            if (entity.EventID != null && entity.EventID.Trim().Length > 0)
            {
                sql += " and a.EventID='" + entity.EventID + "' ";
            }
            else
            {
                sql += " and DATEDIFF(dd,GETDATE(),a.BeginTime) > 0";
            }
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 活动相册集合
        /// <summary>
        /// 活动相册集合
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventAlbums(ActivityMediaEntity entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventAlbumsSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getEventAlbumsCount(ActivityMediaEntity entity)
        {
            string sql = getEventAlbumsSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getEventAlbumsSql(ActivityMediaEntity entity)
        {
            string sql = "select a.* ";
            sql += " ,b.FileName,b.Path ";
            sql += " ,displayIndex=row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp from ActivityMedia a ";
            sql += " left join Attachment b on b.AttachmentID=a.AttachmentID ";
            sql += " where a.isDelete='0' ";
            sql += " and a.ActivityID='" + entity.ActivityID.ToString() + "' ";
            sql += " order by displayIndex ";
            return sql;
        }
        #endregion

        #region 活动订单集合
        /// <summary>
        /// 活动订单集合
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventOrders(InoutInfo entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventOrdersSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getEventOrdersCount(InoutInfo entity)
        {
            string sql = getEventOrdersSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getEventOrdersSql(InoutInfo entity)
        {
            string sql = "select a.* ";
            sql += "  ";
            sql += " ,displayIndex=row_number() over(order by a.Create_Time desc) ";
            sql += " into #tmp from t_inout a ";
            sql += " where a.status<>'-1' ";
            sql += " and a.Field18='" + entity.Field18.ToString() + "' ";
            sql += " order by displayIndex ";
            return sql;
        }
        #endregion

        #region 产品销量汇总
        /// <summary>
        /// 产品销量汇总
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventItemSales(InoutInfo entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventItemSalesSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getEventItemSalesCount(InoutInfo entity)
        {
            string sql = getEventItemSalesSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getEventItemSalesSql(InoutInfo entity)
        {
            string sql = "select a.*,c.item_id,d.imageUrl ";
            sql += " ,displayIndex=row_number() over(order by a.Create_Time desc) ";
            sql += " into #tmp from t_inout_detail a ";
            sql += " left join t_inout b on a.order_id=b.order_id ";
            sql += " left join vw_sku c on a.sku_id=c.sku_id ";
            sql += " left join t_item d on d.item_id=c.item_id ";
            sql += " where a.order_detail_status<>'-1' and b.status<>'-1' ";
            sql += " and b.Field18='" + entity.Field18.ToString() + "' ";
            sql += " order by displayIndex ";
            return sql;
        }
        #endregion

        #region 按照年月与活动状态统计活动信息
        /// <summary>
        /// 统计活动信息数量
        /// </summary>
        /// <param name="yearMonth">活动年月(格式：2013-07)</param>
        /// <param name="eventStatus">活动状态(1=已结束，0=未结束)</param>
        /// <returns></returns>
        public int GetEventListCount(string yearMonth, string eventStatus)
        {
            string sql = GetEventListSql(yearMonth, eventStatus);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取统计活动信息列表
        /// </summary>
        /// <param name="yearMonth">活动年月(格式：2013-07)</param>
        /// <param name="eventStatus">活动状态(1=已结束，0=未结束)</param>
        /// <param name="Page">页码</param>
        /// <param name="PageSize">页面数量</param>
        /// <returns></returns>
        public DataSet GetEventList(string yearMonth, string eventStatus, int Page, int PageSize)
        {
            if (PageSize == 0) PageSize = 15;
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventListSql(yearMonth, eventStatus);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 公共查询
        /// </summary>
        /// <param name="yearMonth">活动年月(格式：2013-07)</param>
        /// <param name="eventStatus">活动状态(1=已结束，0=未结束)</param>
        /// <returns></returns>
        private string GetEventListSql(string yearMonth, string eventStatus)
        {
            string sql = string.Empty;
            sql += " SELECT eventId = e.EventID, eventTitle = e.Title, e.BeginTime ";
            sql += " ,EndTime = CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END ";
            sql += " ,salesTotal = ISNULL((SELECT SUM(i1.total_amount) FROM dbo.T_Inout i1 WHERE i1.Field18 = e.EventID),0) ";
            sql += " ,signUpCount = (SELECT COUNT(*) FROM dbo.LEventSignUp s WHERE s.EventID = e.EventID AND s.IsDelete = '0') ";
            sql += " ,salesCount = (SELECT COUNT(DISTINCT ISNULL(vip_no, '')) FROM dbo.T_Inout i2 WHERE i2.Field18 = e.EventID) ";
            sql += " ,eventStartTime = CONVERT(NVARCHAR(20), CONVERT(DATETIME, ISNULL(e.BeginTime,GETDATE())),120) ";
            sql += " ,imageURL = ImageURL ";
            sql += " ,displayIndex = row_number() over(order by e.BeginTime ) ";
            sql += " INTO #tmp ";
            sql += " FROM dbo.LEvents e ";
            sql += " WHERE 1 = 1 AND e.IsDelete = '0' ";

            if (!string.IsNullOrEmpty(yearMonth))
            {
                sql += " AND CONVERT(VARCHAR(7), BeginTime, 120) = '" + yearMonth + "' ";
            }

            if (!string.IsNullOrEmpty(eventStatus))
            {
                if (eventStatus.Equals("1"))    //已结束
                {
                    sql += " AND (BeginTime < GETDATE() AND CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END < GETDATE()) ";
                }
                else    //未结束
                {
                    sql += " AND (BeginTime > GETDATE() OR CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END > GETDATE()) ";
                }
            }

            return sql;
        }
        #endregion

        #region 获取活动详细信息
        /// <summary>
        /// 获取活动详细信息
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <returns></returns>
        public DataSet GetEventDetail(string eventId)
        {
            string sql = "  "
                + " SELECT eventId = e.EventID "
                + " ,eventTitle = e.Title "
                + " ,e.BeginTime ,e.description ,e.imageURL ,e.address"
                + " ,EndTime = CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END "
                + " ,salesCount = (SELECT COUNT(DISTINCT ISNULL(vip_no, '')) FROM dbo.T_Inout i2 WHERE i2.Field18 = e.EventID) "
                + " ,totalAmount = ISNULL((SELECT SUM(i1.total_amount) FROM dbo.T_Inout i1 WHERE i1.Field18 = e.EventID),0) "
                + " ,signUpCount = (SELECT COUNT(*) FROM dbo.LEventSignUp s WHERE s.EventID = e.EventID AND s.IsDelete = '0') "
                + " FROM dbo.LEvents e "
                + " WHERE 1 = 1 AND e.IsDelete = '0' "
                + " AND e.EventID = '" + eventId + "' ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取活动报名人员详细信息
        /// <summary>
        /// 统计活动报名人员数量
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <returns></returns>
        public int GetEventSignUpUserInfoCount(string eventId)
        {
            string sql = GetEventSignUpUserInfoSql(eventId);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取活动报名人员列表
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <param name="Page">页码</param>
        /// <param name="PageSize">页面数量</param>
        /// <returns></returns>
        public DataSet GetEventSignUpUserInfo(string eventId, int Page, int PageSize)
        {
            if (PageSize == 0) PageSize = 15;
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventSignUpUserInfoSql(eventId);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 公共查询
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <returns></returns>
        private string GetEventSignUpUserInfoSql(string eventId)
        {
            string sql = string.Empty;
            sql += " SELECT vipId = e.VipID ";
            sql += " ,vipName = v.VipName ";
            sql += " ,signUpTime = CONVERT(VARCHAR, e.CreateTime, 120) ";
            sql += " ,displayIndex = row_number() over(order by e.CreateTime DESC ) ";
            sql += " INTO #tmp ";
            sql += " FROM LEventSignUp e ";
            sql += " LEFT JOIN dbo.Vip v ON v.VIPID = e.VipID ";
            sql += " WHERE e.IsDelete = '0' AND EventID = '" + eventId + "' ";

            return sql;
        }
        #endregion

        #region 后台Web获取活动列表
        /// <summary>
        /// 获取活动列表数量
        /// </summary>
        public int WEventGetWebEventsCount(LEventsEntity eventsEntity)
        {
            string sql = WEventGetWebEventsSql(eventsEntity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取活动列表
        /// </summary>
        public DataSet WEventGetWebEvents(LEventsEntity eventsEntity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize;
            int endSize = Page * PageSize + PageSize + 1;
            DataSet ds = new DataSet();
            string sql = WEventGetWebEventsNewsSql(eventsEntity);
            sql += @"  select * from (
                select *, DisplayIndexLast=row_number()over( order by isnull(istop,0) desc,btss desc,bt )
                    From #tmp  ) as BCD  where DisplayIndexLast > " +
                    beginSize + " and  DisplayIndexLast< " + endSize + " ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string WEventGetWebEventsNewsSql(LEventsEntity eventsEntity)
        {
            //var projectName = ConfigurationManager.AppSettings["ProjectName"];
            string sql = string.Empty;
            sql = @"select a.* ,lt.Title EventTypeName,
                    case  when DATEDIFF(DD,GETDATE(),BeginTime)>-1 then 1 else 0 end as btss,
                    abs(DATEDIFF(DD,GETDATE(),BeginTime)) as bt";
            if (CurrentUserInfo.ClientID == "a2573925f3b94a32aca8cac77baf6d33" || CurrentUserInfo.ClientID == "75a232c2cf064b45b1b6393823d2431e" || CurrentUserInfo.ClientID == "376f4455b43647fd8bda39a3bb39eb73" || CurrentUserInfo.ClientID == "1c6a39e4a9e54fecb508abfa5cda9eaa" || CurrentUserInfo.ClientID == "56319f7e9c74424dba95b8e96d2487bc")
            {
                sql += " ,(select count(*) from LEventSignUp where IsDelete='0' and eventId=a.eventId) AppliesCount ";
            }
            else
            {
                sql += " ,(select count(*) from WEventUserMapping where IsDelete='0' and eventId=a.eventId) AppliesCount ";
            }
            sql += " ,(select count(*) from LPrizes where IsDelete='0' and eventId=a.eventId) PrizesCount ";
            sql += " ,(select count(*) from LEventRound where IsDelete='0' and eventId=a.eventId) RoundCount ";

            sql += " into #tmp ";
            sql += " from [LEvents] a ";
            sql += " left join LEventsType lt on lt.EventTypeID=a.EventTypeID and lt.IsDelete='0' and lt.ClientID='" + this.CurrentUserInfo.CurrentUser.customer_id + "'";

            sql += " where a.IsDelete='0' and a.customerId= '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";

            if (eventsEntity.ParentEventID != null && eventsEntity.ParentEventID != "")
            {
                sql += " and a.ParentEventID = '" + eventsEntity.ParentEventID + "' ";
            }
            if (eventsEntity.Title != null && eventsEntity.Title.Trim().Length > 0)
            {
                sql += " and a.Title like '%" + eventsEntity.Title + "%' ";
            }
            if (eventsEntity.StartTimeText != null && eventsEntity.StartTimeText.Trim().Length > 0)
            {
                sql += " and convert(datetime, a.beginTime, 120)>=convert(datetime, '" + eventsEntity.StartTimeText + "', 120) ";
            }
            if (eventsEntity.EndTimeText != null && eventsEntity.EndTimeText.Trim().Length > 0)
            {
                sql += " and convert(datetime, a.endTime, 120)<=convert(datetime, '" + eventsEntity.EndTimeText + "', 120) ";
            }
            if (!string.IsNullOrEmpty(eventsEntity.EventTypeID))
            {
                sql += " and a.EventTypeID = '" + eventsEntity.EventTypeID + "' ";
            }

            return sql;
        }

        private string WEventGetWebEventsSql(LEventsEntity eventsEntity)
        {
            //var projectName = ConfigurationManager.AppSettings["ProjectName"];
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex asc) ";

            if (CurrentUserInfo.ClientID == "a2573925f3b94a32aca8cac77baf6d33" || CurrentUserInfo.ClientID == "75a232c2cf064b45b1b6393823d2431e" || CurrentUserInfo.ClientID == "376f4455b43647fd8bda39a3bb39eb73" || CurrentUserInfo.ClientID == "1c6a39e4a9e54fecb508abfa5cda9eaa" || CurrentUserInfo.ClientID == "56319f7e9c74424dba95b8e96d2487bc")
            {
                sql += " ,(select count(*) from LEventSignUp where IsDelete='0' and eventId=a.eventId) AppliesCount ";
            }
            else
            {
                sql += " ,(select count(*) from WEventUserMapping where IsDelete='0' and eventId=a.eventId) AppliesCount ";
            }
            sql += " ,(select count(*) from LPrizes where IsDelete='0' and eventId=a.eventId) PrizesCount ";
            sql += " ,(select count(*) from LEventRound where IsDelete='0' and eventId=a.eventId) RoundCount ";

            sql += " into #tmp ";
            sql += " from [LEvents] a ";
            //sql += " left join Cities b on a.cityId=b.cityId ";
            sql += " where a.IsDelete='0' and a.customerId= '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            //if (eventsEntity.CreateBy != null && eventsEntity.CreateBy.Trim().Length > 0 &&
            //    eventsEntity.CreateBy.Trim() != "admin")
            //{
            //    sql += " and a.CreateBy = '" + eventsEntity.CreateBy + "' ";
            //}
            if (eventsEntity.ParentEventID != null && eventsEntity.ParentEventID != "")
            {
                sql += " and a.ParentEventID = '" + eventsEntity.ParentEventID + "' ";
            }
            if (eventsEntity.Title != null && eventsEntity.Title.Trim().Length > 0)
            {
                sql += " and a.Title like '%" + eventsEntity.Title + "%' ";
            }
            if (eventsEntity.StartTimeText != null && eventsEntity.StartTimeText.Trim().Length > 0)
            {
                sql += " and convert(datetime, a.beginTime, 120)>=convert(datetime, '" + eventsEntity.StartTimeText + "', 120) ";
            }
            if (eventsEntity.EndTimeText != null && eventsEntity.EndTimeText.Trim().Length > 0)
            {
                sql += " and convert(datetime, a.endTime, 120)<=convert(datetime, '" + eventsEntity.EndTimeText + "', 120) ";
            }
            if (!string.IsNullOrEmpty(eventsEntity.EventTypeID))
            {
                sql += " and a.EventTypeID = '" + eventsEntity.EventTypeID + "' ";
            }
            //sql += " order by a.DisplayIndexLast desc ";
            return sql;
        }
        #endregion

        #region 活动报名人员列表
        /// <summary>
        /// 根据标识获取活动人员数量
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int WEventGetEventAppliesCount(string EventID)
        {
            string sql = WEventGetEventAppliesSql(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 根据标识获取活动人员信息
        /// </summary>
        public DataSet WEventGetEventAppliesList(string EventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = WEventGetEventAppliesSql(EventID);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string WEventGetEventAppliesSql(string EventID)
        {
            //BasicUserInfo pUserInfo = new BasicUserInfo();
            //UsersDAO userDAO = new UsersDAO(pUserInfo);

            string sql = "";
            sql += "SELECT a.* "
                + " ,DisplayIndex = row_number() over(order by a.UserName ) "
                + " into #tmp FROM WEventUserMapping a "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            return sql;
        }
        #endregion


        #region 获取我的认购数量
        public int GetMyOrderCount(string eventId, string openId)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_Inout a "
                        + " INNER JOIN dbo.Vip b ON(a.vip_no = b.VIPID) "
                        + " WHERE a.Field18 = '" + eventId + "' "
                        + " AND b.WeiXinUserId = '" + openId + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region Jermyn20130813 微活动移植
        /// <summary>
        /// 微活动移植
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetEventDetailById(string EventID)
        {
            string sql = " select * From LEvents where eventId = '" + EventID + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取返校日活动
        /// <summary>
        /// 获取返校日活动主信息
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetEventFXInfo(string eventId, string userId)
        {
            string sql = "SELECT a.EventID,a.Title,a.Description "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x "
                        + " INNER JOIN dbo.LEvents y ON(x.ObjectId = y.EventID ) "
                        + " WHERE x.OpenId = '" + userId + "'  "
                        + " AND x.IsDelete='0' AND y.EventLevel='2' AND y.IsDelete='0')  haveSignedCount "
                        + " FROM dbo.LEvents a "
                        + " WHERE EventID = '" + eventId + "';";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet getSchoolEventList(string eventId, string userId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.EventID ,a.Title ,a.Description,a.EventLevel,a.ParentEventID, AllowCount,a.DisplayIndex,a.HaveCount AppliesCount,a.IsSignUp "
                        + " ,CASE WHEN a.AllowCount > a.HaveCount THEN a.AllowCount - a.HaveCount ELSE 0 END OverCount "
                        + " FROM ( "
                        + " SELECT *,CASE WHEN a.PersonCount IS NULL OR a.PersonCount = '' THEN 0 ELSE a.PersonCount END  AllowCount "
                        + " , (SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x WHERE x.ObjectId = a.eventid AND x.IsDelete='0') HaveCount "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM dbo.ZCourseApply x WHERE x.ObjectId = a.eventid AND x.OpenId = '" + userId + "' AND x.IsDelete = '0') IsSignUp "
                        + " FROM dbo.LEvents a WHERE a.ParentEventID = '" + eventId + "' AND a.IsDelete = '0' "
                        + " UNION ALL "
                        + " SELECT * ,CASE WHEN a.PersonCount IS NULL OR a.PersonCount = '' THEN 0 ELSE a.PersonCount END  AllowCount "
                        + " , (SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x WHERE x.ObjectId = a.eventid AND x.IsDelete='0') HaveCount "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM dbo.ZCourseApply x WHERE x.ObjectId = a.eventid AND x.OpenId = '" + userId + "' AND x.IsDelete = '0') IsSignUp "
                        + " FROM dbo.LEvents a "
                        + " WHERE a.ParentEventID IN (SELECT EventID FROM dbo.LEvents WHERE ParentEventID = '" + eventId + "') "
                        + " UNION ALL "
                        + " SELECT * ,CASE WHEN a.PersonCount IS NULL OR a.PersonCount = '' THEN 0 ELSE a.PersonCount END  AllowCount "
                        + " , (SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x WHERE x.ObjectId = a.eventid AND x.IsDelete='0') HaveCount "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM dbo.ZCourseApply x WHERE x.ObjectId = a.eventid AND x.OpenId = '" + userId + "' AND x.IsDelete = '0') IsSignUp "
                        + " FROM dbo.LEvents a "
                        + " WHERE a.ParentEventID IN (SELECT EventID FROM dbo.LEvents x WHERE x.ParentEventID IN (SELECT EventID FROM dbo.LEvents  WHERE ParentEventID = '" + eventId + "'))"
                        + " AND IsDelete = '0') a ORDER BY a.EventLevel ,a.DisplayIndex";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public bool SetApplyEventDelete(string userId)
        {
            string sql = "UPDATE dbo.ZCourseApply SET IsDelete = '1' WHERE OpenId ='" + userId + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 根据微信的固定二维码获取活动信息
        /// <summary>
        /// 根据微信的固定二维码获取活动信息
        /// </summary>
        /// <param name="wxCode"></param>
        /// <returns></returns>
        public DataSet GetEventInfoByWX(string wxCode)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT TOP 1 a.* FROM dbo.LEvents a "
                    + " INNER JOIN LEventsWX b ON(a.EventID = b.EventID) "
                    + " WHERE b.IsDelete = '0' "
                    + " AND b.wxcode = '" + wxCode + "' "
                    + " AND a.CustomerId = '" + this.CurrentUserInfo.CurrentUser.customer_id + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion


        public DataSet GetLeventsInfoDataSet(string eventId)
        {
            var sql = "select IsShare,BootURL,ShareRemark,PosterImageUrl,OverRemark,ShareLogoUrl  from levents where eventId = @pEventId";

            var paras = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@pEventId", Value = eventId}
            };

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, paras.ToArray());
        }

        public DataSet GetBootUrlByEventId(string eventId)
        {
            var sql = new StringBuilder();
            sql.Append("select isnull(ShareRemark,'') ShareRemark,");
            sql.Append("isnull(PosterImageUrl,'') PosterImageUrl,isnull(OverRemark,'') OverRemark");
            sql.Append(",isnull(BootURL,'') BootURL from levents where eventId = @pEventId");
            var para = new List<SqlParameter>();

            para.Add(new SqlParameter() { ParameterName = "@pEventId", Value = eventId });

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());

        }

        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        #region 搜索活动
        public DataSet EventList(string beginTime, string endTime, string eventTypeID, string sponsor, string eventStatus, string eventTitle, string pageSize, string pageIndex)
        {
            DataSet dataSet = new DataSet();

            List<SqlParameter> spl = new List<SqlParameter>();
            spl.Add(new SqlParameter("@BeginTime", beginTime));
            spl.Add(new SqlParameter("@EndTime", endTime));
            spl.Add(new SqlParameter("@EventTypeID", eventTypeID));
            spl.Add(new SqlParameter("@Organizer", sponsor));
            spl.Add(new SqlParameter("@EventStatus", eventStatus));
            spl.Add(new SqlParameter("@Title", eventTitle));
            spl.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            spl.Add(new SqlParameter("@PageSize", pageSize));
            spl.Add(new SqlParameter("@PageIndex", pageIndex));

            dataSet = SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "EventListPagedSearch", spl.ToArray());

            return dataSet;
        }
        #endregion

        #region 获取活动详情

        #endregion
        #region 活动列表
        public DataSet GetEventList(int pageIndex, int pageSize, string strTitle, string strDrawMethodName, string strBeginTime, string strEndTime)
        {


            SqlParameter[] parameters = new SqlParameter[7] 
            { 
                new SqlParameter{ParameterName="@PageIndex",SqlDbType=SqlDbType.Int,Value=pageIndex},
                new SqlParameter{ParameterName="@PageSize",SqlDbType=SqlDbType.Int,Value=pageSize},
                new SqlParameter{ParameterName="@Title",SqlDbType=SqlDbType.NVarChar,Value=strTitle},
                new SqlParameter{ParameterName="@DrawMethodName",SqlDbType=SqlDbType.NVarChar,Value=strDrawMethodName},
                new SqlParameter{ParameterName="@BeginTime",SqlDbType=SqlDbType.NVarChar,Value=strBeginTime},
                new SqlParameter{ParameterName="@EndTime",SqlDbType=SqlDbType.NVarChar,Value=strEndTime},
                new SqlParameter{ParameterName="@CustomerId",SqlDbType=SqlDbType.NVarChar,Value=CurrentUserInfo.ClientID},
            };

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_GetEventList", parameters);
        }
        #endregion
        #region 删除活动
        public void DeleteByProc(string strEventId)
        {
            if (String.IsNullOrEmpty(strEventId))
                return;
            IDbTransaction pTran = null;
            string strSql = "Proc_DeleteEvent";
            SqlParameter[] parameters = new SqlParameter[2] 
            { 
                new SqlParameter{ParameterName="@UpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.VarChar,Value=strEventId}
            };

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.StoredProcedure, strSql, parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, strSql, parameters);
            return;
        }
        #endregion
      
        /// <summary>
        /// 启用停用
        /// </summary>
        /// <param name="strEventId"></param>
        /// <param name="intEventStatus"></param>

        public void UpdateEventStatus(string strEventId, int intEventStatus)
        {
            var sql = string.Format("UPDATE dbo.LEvents SET EventStatus={0} WHERE EventID='{1}'", intEventStatus, strEventId);
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }
        /// <summary>
        /// 更新活动是否分享
        /// </summary>
        /// <param name="strEventId"></param>
        public void UpdateEventIsShare(string strEventId)
        {
            var sql = string.Format("UPDATE dbo.LEvents SET IsShare=1 WHERE EventID='{0}'",strEventId);
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }

        public DataSet GetNewEventInfo(string eventId)
        {
            string sql = @"select   t1.EventID ,
			   t1.Title EventName,
                t1.[DrawMethodId] ,
                t2.DrawMethodName  
                ,DrawMethodCode      ,         
                Convert(NVARCHAR(10),t1.BeginTime,120)BeginTime ,
                Convert(NVARCHAR(10),t1.EndTime,120)EndTime ,
                  t1.[VipCardType] ,
                t1.[VipCardGrade] ,
				t1.EventStatus,
				t1.EventTypeId,
				t3.Title EventTypeName,
                t1.PersonCount ,
                t1.LotteryNum ,
				CASE	
						WHEN CAST(t1.BeginTime AS DATETIME)>GETDATE()  THEN	'未开始'
						WHEN GETDATE()>=CAST(t1.BeginTime AS DATETIME) AND GETDATE()<=CAST(t1.EndTime AS DATETIME) AND t1.EventStatus=20 THEN	'运行中'
						WHEN GETDATE()>=CAST(t1.BeginTime AS DATETIME) AND GETDATE()<=CAST(t1.EndTime AS DATETIME) AND t1.EventStatus=30 THEN	'暂停'
						WHEN CAST(t1.EndTime AS DATETIME)<GETDATE()  THEN	'结束'
				END	statusDESC
   from levents t1
   left join LEventDrawMethod t2  ON t1.[DrawMethodId] = t2.DrawMethodID
   left join LEventsType t3 on  t1.EventTypeID = t3.EventTypeID WHERE t1.EventID = '" + eventId + "'";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }


        public DataSet GetMaterialTextInfo(string eventId)
        {
            string sql = @"   	    select d.*,b.ReplyId,c.MappingId from WQRCodeManager  a
	                    inner join WKeywordReply b on CONVERT(varchar(50), a.QRCodeId)=b.Keyword
	                    inner join  WMenuMTextMapping c on b.ReplyId=c.MenuId
	                    inner join WMaterialText d on c.TextId=d.TextId
	                    where a.ObjectId = '" + eventId + "'";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #region 获取正在运行中的活动列表
        public DataSet GetWorkingEventList()
        {
            string strSql = "Select EventID,Title FROM LEvents WHERE [EventStatus] in(20,30) and CustomerId='" + CurrentUserInfo.ClientID + "' ORDER BY CreateTime DESC";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, strSql);
        }
        #endregion
        #region 获取未开始和正在运行中的活动列表
        public DataSet GetNoStartAndWorkingEventList()
        {
            string strSql = "Select EventID,Title FROM LEvents WHERE [EventStatus] in(10,20,30) and CustomerId='" + CurrentUserInfo.ClientID + "' ORDER BY CreateTime DESC";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, strSql);
        }
        #endregion
    }
}
