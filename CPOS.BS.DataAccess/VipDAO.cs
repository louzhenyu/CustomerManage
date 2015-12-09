/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:46:51
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
using System.Linq;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.WX;


namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表Vip的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipDAO : Base.BaseCPOSDAO, ICRUDable<VipEntity>, JIT.Utility.DataAccess.IQueryable<VipEntity>
    {
        public void NewLoad(SqlDataReader rd, out VipEntity m)
        {
            this.Load(rd, out m);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipEntity NewGetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select *,CONVERT(date,Birthday,23) as NewBirthday from [Vip] where VIPID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    if (rdr["NewBirthday"] != DBNull.Value)
                    {
                        m.NewBirthday = Convert.ToDateTime(rdr["NewBirthday"]);
                    }
                    break;
                }
            }
            //返回
            return m;
        }


        #region 会员报表
        /// <summary>
        /// 会员生日统计
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="UnitID"></param>
        /// <param name="Gender"></param>
        /// <param name="CardStatusID"></param>
        /// <param name="StarDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet VipBirthdayCount(string Month, string UnitID, string Gender, int? CardStatusID, string StarDate, string EndDate, int? PageSize, int? PageIndex)
        {
            if (PageSize == null) { PageSize = 10; }
            if (PageIndex == null) { PageIndex = 1; }

            var parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@BirthdayMonth", System.Data.SqlDbType.NVarChar) { Value = Month };
            parm[1] = new SqlParameter("@UnitID", System.Data.SqlDbType.NVarChar) { Value = UnitID };
            parm[2] = new SqlParameter("@Gender", System.Data.SqlDbType.NVarChar) { Value = Gender };
            parm[3] = new SqlParameter("@CardStatusID", System.Data.SqlDbType.Int) { Value = CardStatusID };
            parm[4] = new SqlParameter("@StareDate", System.Data.SqlDbType.DateTime) { Value = StarDate };
            parm[5] = new SqlParameter("@EndDate", System.Data.SqlDbType.DateTime) { Value = EndDate };
            parm[6] = new SqlParameter("@PageSize", System.Data.SqlDbType.Int) { Value = PageSize };
            parm[7] = new SqlParameter("@PageIndex", System.Data.SqlDbType.Int) { Value = PageIndex };
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Report_VipBirthdayCount", parm);
        }
        #endregion

        #region 会员查询

        /// <summary>
        /// 获取查询会员的数量
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public int SearchVipInfoCount(VipSearchEntity vipSearchInfo)
        {
            string sql = SearchVipSql(vipSearchInfo);
            Loggers.Debug(new DebugLogInfo() { Message = "1" });
            sql = sql + " select count(*) as icount From #tmp; ";
            Loggers.Debug(new DebugLogInfo() { Message = sql });
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public int GetIntegralByVipCount(VipSearchEntity vipSearchInfo)
        {
            string sql = " select COUNT(*) icount From ( select distinct a.*  From vip a  inner join VipIntegralDetail b on(a.VIPID = b.VIPID)  where a.IsDelete = '0' and b.IsDelete = '0' and a.HigherVipID = '" + vipSearchInfo.HigherVipId + "' ) x";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet GetIntegralByVipList(VipSearchEntity vipSearchInfo)
        {
            DataSet ds = new DataSet();
            string sql = " select distinct a.* From vip a "
                      + " inner join VipIntegralDetail b on(a.VIPID = b.VIPID) "
                      + " where a.IsDelete = '0' and b.IsDelete = '0' " //and b.IntegralSourceID = '5' 
                      + " and a.HigherVipID = '" + vipSearchInfo.HigherVipId + "' ";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取查询会员的信息
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchVipInfo(VipSearchEntity vipSearchInfo)
        {
            int beginSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize;
            int endSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize + vipSearchInfo.PageSize;

            string sql = SearchVipSql(vipSearchInfo);
            //JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = "活动sql:" + sql.ToString().Trim() });
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize +
                  "' order by a.displayindex";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string SearchVipSql(VipSearchEntity vipSearchInfo)
        {
            PublicService pService = new PublicService();
            string sql = string.Empty;
            MarketSignInDAO mDao = new MarketSignInDAO(this.CurrentUserInfo);
            sql = mDao.SetTagsSql(vipSearchInfo);
            string strOrder = string.Empty;
            switch (vipSearchInfo.VipSourceId)
            {
                case "3":
                    strOrder = " order by a.CreateTime desc ";
                    break;
                case "2":
                    strOrder = " order by a.LastUpdateTime desc ";
                    break;
                default:
                    strOrder = " order by a.LastUpdateTime desc ";
                    break;
            }

            if (vipSearchInfo.OrderBy != null && vipSearchInfo.OrderBy.Trim().Length > 0)
            {
                strOrder = " order by " + vipSearchInfo.OrderBy + " ";
            }

            sql += " SELECT DISTINCT a.*,b.UnitId,(SELECT unit_id FROM dbo.t_unit x WHERE x.unit_name = a.TencentMBlog and x.customer_id = a.ClientID  ) LastSalesUnitId  into #VipTmp FROM #vip a "
                   + " LEFT JOIN dbo.VipUnitMapping b "
                   + " ON(a.VIPID = b.VIPID AND b.IsDelete = '0' ); "; //where a.weixin='gh_bf70d7900c28'
            //Jermyn20131123去掉写死的业务逻辑

            sql += "SELECT a.* "
                   +
                   " ,(SELECT x.VipSourceName FROM dbo.SysVipSource x WHERE x.VipSourceID = a.VipSourceId) VipSourceName "
                   + " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc "
                   + " ,'' LastUnit "
                   + " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc "
                   + " ,CASE WHEN a.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                //+ " ,DisplayIndex=row_number() over( " + strOrder + "  ) "
                   + " ,(select sum(Integral) from VipIntegralDetail where IsDelete='0' "
                   + "   and fromVipId=a.vipId and vipId='" + vipSearchInfo.HigherVipId + "') IntegralForHightUser "
                   + " ,(select unit_name From t_unit x where x.unit_id = a.unitId) MembershipShop ";
            //会籍店 Jermyn20130904

            if (vipSearchInfo.RoleCode != null && vipSearchInfo.RoleCode.Trim().Length > 0)
            {
                sql += " ,(case when '" + vipSearchInfo.Latitude + "' = '0' and '" + vipSearchInfo.Longitude +
                       "' = '0' then '0' else ABS(dbo.DISTANCE_TWO_POINTS(" + Convert.ToDouble(vipSearchInfo.Latitude) +
                       "," + Convert.ToDouble(vipSearchInfo.Longitude) + ",a.Latitude,a.longitude)) end) Distance  ";
            }
            else
            {
                sql += " ,'0' Distance ";
            }

            sql += " into #tmpVip2 FROM #VipTmp a ";

            if (vipSearchInfo.RoleCode != null && vipSearchInfo.RoleCode.Trim().Length > 0)
            {
                sql += " inner join VIPRoleMapping r1 on (r1.vipId=a.vipId and r1.isDelete='0') ";
                sql += " inner join t_role r2 on (r1.roleId=r2.role_id and r2.role_code='" + vipSearchInfo.RoleCode +
                       "') ";
            }

            sql += " WHERE a.IsDelete = 0 and a.ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (vipSearchInfo.VipInfo != null && !vipSearchInfo.VipInfo.Equals(""))
            {
                sql += " and (a.VipCode like '%" + vipSearchInfo.VipInfo + "%'  or a.VipName like '%" +
                       vipSearchInfo.VipInfo + "%' ) ";
            }
            //sql = pService.GetLinkSql(sql, "a.VipCode", vipSearchInfo.VipInfo, "%");
            //sql = pService.GetLinkSql(sql, "a.VipName", vipSearchInfo.VipInfo, "%");
            sql = pService.GetLinkSql(sql, "a.LastSalesUnitId", vipSearchInfo.UnitId, "=");
            sql = pService.GetLinkSql(sql, "a.Phone", vipSearchInfo.Phone, "%");
            sql = pService.GetLinkSql(sql, "a.VipSourceId", vipSearchInfo.VipSourceId, "=");
            sql = pService.GetLinkSql(sql, "a.higherVipId", vipSearchInfo.HigherVipId, "=");
            if (!vipSearchInfo.Status.ToString().Equals("0"))
            {
                sql = pService.GetLinkSql(sql, "a.Status", vipSearchInfo.Status.ToString(), "=");
            }
            if (!vipSearchInfo.VipLevel.ToString().Equals("0"))
            {
                sql = pService.GetLinkSql(sql, "a.VipLevel", vipSearchInfo.VipLevel.ToString(), "=");
            }
            sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RegistrationTime,120) ",
                vipSearchInfo.RegistrationDateBegin, ">");
            sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RegistrationTime,120) ",
                vipSearchInfo.RegistrationDateEnd, "<=");
            sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RecentlySalesTime,120) ",
                vipSearchInfo.RecentlySalesDateBegin, ">");
            sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RecentlySalesTime,120) ",
                vipSearchInfo.RecentlySalesDateEnd, "<=");
            if (!vipSearchInfo.IntegrationBegin.ToString().Equals("0"))
            {
                sql = pService.GetLinkSql(sql, "a.Integration ", vipSearchInfo.IntegrationBegin.ToString(), ">");
            }
            if (!vipSearchInfo.IntegrationEnd.ToString().Equals("0"))
            {
                sql = pService.GetLinkSql(sql, "a.Integration ", vipSearchInfo.IntegrationEnd.ToString(), "<=");
            }
            if (vipSearchInfo.Gender != null && vipSearchInfo.Gender.Trim().Length > 0)
            {
                sql = pService.GetLinkSql(sql, "a.Gender ", vipSearchInfo.Gender.Trim(), "=");
            }
            if (vipSearchInfo.MembershipShopId != null && vipSearchInfo.MembershipShopId.Trim().Length > 0)
            {
                sql = pService.GetLinkSql(sql, "a.unitId ", vipSearchInfo.MembershipShopId.Trim(), "=");
            }

            sql += ";";

            sql += " select a.* ";
            sql += " ,DisplayIndex=row_number() over( " + strOrder + "  ) ";
            sql += " into #tmp from #tmpVip2 a ; ";


            Loggers.Debug(new DebugLogInfo() { Message = "SearchVipSql sql: " + sql });

            return sql;
        }

        #endregion

        #region 获取会员详细信息

        /// <summary>
        /// 获取会员详细信息
        /// </summary>
        /// <param name="OpenID">微信ID</param>
        /// <returns></returns>
        public DataSet GetVipDetail(string OpenID)
        {
            GetVipInfoFromApByOpenId(OpenID, null);
            string sql = "select * From vip  "
                         + " where WeiXinUserId = '" + OpenID + "'"
                         + " or RIGHT(VipCode,8) = '" + OpenID + "' "
                         + " or Phone = '" + OpenID + "'";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }


        /// <summary>
        /// 根据号码模糊查询用户
        /// </summary>
        /// <param name="tell"></param>
        /// <returns></returns>
        public DataSet GetVipByPhone(string Phone)
        {
            string sql = string.Format(
                "SELECT TOP(10) * FROM Vip WHERE Phone LIKE '%{0}%' ORDER BY Phone desc"
                , Phone);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetVipDetailByVipID(string vipID)
        {
            string sql = "select a.* ";
            sql += " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc ";
            sql += " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc ";
            sql += " From vip a ";
            sql += " where vipID = '" + vipID + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取最新关注的人数

        /// <summary>
        /// 获取最新关注的人数
        /// </summary>
        /// <param name="Timestamp"></param>
        /// <returns></returns>
        public DataSet GetShowCount(long Timestamp)
        {
            string sql = "SELECT COUNT( distinct OpenID) count,isnull(MAX(dbo.DateToTimestamp(createtime)),0) NewTimestamp  FROM dbo.VipShowLog "
                         + " WHERE IsShow = 1 "
                         + " AND dbo.DateToTimestamp(createtime) > '" + Timestamp +
                         "' and OpenID IN (SELECT WeiXinUserId FROM dbo.Vip WHERE ClientID = '" +
                         this.CurrentUserInfo.CurrentUser.customer_id + "')";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取各个来源对应的会员数，VipSourceId 为 NULL 时 当作电话客服来源对待
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetShowCount2(string userId, string clientId)
        {
            string sql = @"  SELECT l.VipSourceID,L.VipSourceName,Rnt=ISNULL(R.Rnt,0) 
                        FROM SysVipSource L LEFT JOIN 
                        (SELECT VipSourceId=ISNULL(V.VipSourceId,5),Rnt=COUNT(*) 
                        FROM T_User_Role L CROSS APPLY 
                        FnGetUnitTreeView('{1}',L.unit_id) R 
                        INNER JOIN Vip V ON R.UnitID=V.CouponInfo OR ( R.TreeLevel=1 AND ParrentUnitID='-99' AND ISNULL(V.CouponInfo,'')='') 
                        WHERE L.user_id='{0}' AND V.isdelete=0 
                        AND V.ClientID='{1}' 
                        GROUP BY ISNULL(V.VipSourceId,5) ) R ON L.VipSourceID=R.VipSourceId 
                        ORDER BY CONVERT(INT,L.VipSourceID ) ";
            sql = string.Format(sql, userId, clientId);
            return SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 获取各类关注的总人数
        /// </summary>
        /// <param name="Timestamp"></param>
        /// <returns></returns>
        public DataSet GetShowCountBySource(long Timestamp)
        {
            string sql = "SELECT VipSourceID,VipSourceName "
                         + " ,(SELECT MAX(dbo.DateToTimestamp(LastUpdateTime)) FROM dbo.Vip) NewTimestamp "
                         + " ,ISNULL( (SELECT COUNT(*) icount FROM vip x WHERE  x.ClientID = '" +
                         this.CurrentUserInfo.CurrentUser.customer_id +
                         "' and x.IsDelete = '0' AND dbo.DateToTimestamp(createtime) > '" + Timestamp +
                         "' AND x.VipSourceId = a.vipSourceId GROUP BY VipSourceId ),CASE WHEN " + Timestamp +
                         " > 1 then 0 ELSE a.TmpCount end) iCount "
                         + " FROM dbo.SysVipSource a "
                         + " WHERE a.IsDelete = '0' "
                         + " ORDER BY VipSourceID ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public long GetNowTimestamp()
        {
            string sql = "SELECT dbo.DateToTimestamp(GETDATE());";
            return Convert.ToInt64(this.SQLHelper.ExecuteScalar(sql));
        }

        #endregion

        #region 获取会员OpenID

        public string GetOpenID(string vipID)
        {
            string sql = "select WeiXinUserId From vip where vipID = '" + vipID + "'";
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }

        #endregion

        #region 获取lj VIP信息

        public DataSet GetLjVipInfo(VipEntity entity)
        {
            string sql = "select a.* ";
            sql += " ,(select count(*) from t_inout where status<>'-1' and vip_no=a.vipId) OrderCount ";
            sql += " from vip a ";
            sql += " where a.isDelete='0' and ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            sql += " and a.WeiXin='" + entity.WeiXin + "' ";
            sql += " and a.WeiXinUserId='" + entity.WeiXinUserId + "' ";

            if (entity.QRVipCode != null && entity.QRVipCode.Trim() == "/Lj/")
            {
                sql += " and a.QRVipCode like '%" + entity.QRVipCode + "%' ";
            }

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取VIP兑换商品列表信息

        public DataSet GetVipItemList(VipEntity entity)
        {
            string sql = "SELECT a.item_id,a.item_name,b.prop_value integration,a.display_index FROM dbo.T_Item a ";
            sql += " INNER JOIN dbo.T_Item_Property b";
            sql += " ON(a.item_id = b.item_id)";
            sql += " WHERE b.prop_id = 'B0FE25EE79244E9DA813761D4947AC4B'";
            sql += " AND b.prop_value IS NOT NULL AND b.prop_value <> ''";
            sql += " order by a.display_index ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取VIP订单列表信息

        public DataSet GetVipOrderList(VipEntity entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = GetVipOrderSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize +
                   "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int GetVipOrdersCount(VipEntity entity)
        {
            string sql = GetVipOrderSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public string GetVipOrderSql(VipEntity entity)
        {
            string sql = "select a.* ";
            sql += " ,displayIndex=row_number() over(order by a.create_time desc) ";
            sql += " into #tmp from t_inout a ";
            sql += " where [status]<>'-1' ";
            sql += " and a.vip_no='" + entity.VIPID + "' AND a.Field16 IS NOT NULL ";
            return sql;
        }

        #endregion

        #region 获取VIP兑换商品SKU列表信息

        public DataSet GetVipSkuPropList(VipEntity entity, string itemId)
        {
            string sql =
                "SELECT a.sku_id,a.item_id,a.barcode,a.prop_1_detail_id,a.prop_2_detail_id,a.prop_3_detail_id,a.prop_4_detail_id,a.prop_5_detail_id FROM vw_sku a ";
            sql += " where a.status<>'-1' ";
            sql += " and a.item_id='" + itemId + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取会员关注人数(泸州老窖)

        public int GetHasVipCount(string WeiXinId)
        {
            string sql = "SELECT COUNT(*) FROM dbo.Vip WHERE IsDelete = 0 AND WeiXin = '" + WeiXinId + "';";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        #endregion

        #region 获取新采集会员数量(泸州老窖)

        public int GetNewVipCount(string WeiXinId)
        {
            string sql = "SELECT COUNT(*) FROM dbo.Vip WHERE IsDelete = 0 AND WeiXin = '" + WeiXinId +
                         "' and QRVipCode like '%/Lj/%';";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        #endregion

        #region 会员按月累计

        /// <summary>
        /// 会员按月累计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getVipMonthAddup(LVipAddupEntity entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getVipMonthAddupSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize +
                   "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int getVipMonthAddupCount(LVipAddupEntity entity)
        {
            string sql = getVipMonthAddupSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public string getVipMonthAddupSql(LVipAddupEntity entity)
        {
            string sql = "select a.* ";
            sql += " ,displayIndex=row_number() over(order by a.yearMonth desc) ";
            sql += " into #tmp from LVipAddup a where isDelete='0' ";
            sql += " order by displayIndex ";
            return sql;
        }

        #endregion

        #region 会员月活动销量统计

        /// <summary>
        /// 会员月活动销量统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventMonthEventAddup(LEventAddupEntity entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventMonthEventAddupSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize +
                   "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int getEventMonthEventAddupCount(LEventAddupEntity entity)
        {
            string sql = getEventMonthEventAddupSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public string getEventMonthEventAddupSql(LEventAddupEntity entity)
        {
            string sql = "select a.* ";
            sql += " ,displayIndex=row_number() over(order by a.yearMonth desc) ";
            sql += " into #tmp from LEventAddup a where isDelete='0' ";
            sql += " order by displayIndex ";
            return sql;
        }

        #endregion

        #region Jermyn 20130911 从总部导入vip信息

        /// <summary>
        /// 从总部导入vip信息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public bool GetVipInfoFromApByOpenId(string OpenId, string VipId)
        {
            string sql = @"INSERT INTO dbo.Vip(VIPID,VipName,VipLevel,VipCode,WeiXin,WeiXinUserId,Gender,Age,Phone,SinaMBlog,TencentMBlog,Birthday,Qq,Email,Status,
                            VipSourceId,Integration,ClientID,RecentlySalesTime,RegistrationTime,
                            CreateTime,CreateBy,LastUpdateTime,LastUpdateBy,IsDelete,APPID,HigherVipID,QRVipCode,City,CouponURL,
                            CouponInfo,PurchaseAmount,PurchaseCount,DeliveryAddress,Longitude,Latitude,VipPasswrod,HeadImgUrl,Col1,
                            Col2,Col3,Col4,Col5,Col6,Col7,Col8,Col9,Col10,Col11,Col12,Col13,Col14,Col15,Col16,Col17,Col18,Col19,
                            Col20,Col21,Col22,Col23,Col24,Col25,Col26,Col27,Col28,Col29,Col30,Col31,Col32,Col33,Col34,Col35,Col36,Col37,Col38,Col39,Col40,Col41,Col42,
                            Col43,Col44,Col45,Col46,Col47,Col48,Col49,Col50,
                            isActivate,VIPImportID,VipRealName,ShareVipId,SetoffUserId,ShareUserId ) "
                         + @" SELECT a.VIPID,a.VipName,a.VipLevel,a.VipCode,a.WeiXin,a.WeiXinUserId,a.Gender,a.Age,
                            a.Phone,a.SinaMBlog,a.TencentMBlog,a.Birthday,a.Qq,a.Email,a.Status,
                            a.VipSourceId,a.Integration,a.ClientID,a.RecentlySalesTime,a.RegistrationTime,a.CreateTime,a.CreateBy,a.LastUpdateTime,
                            a.LastUpdateBy,a.IsDelete,a.APPID,a.HigherVipID,a.QRVipCode,a.City,a.CouponURL,
                            a.CouponInfo,a.PurchaseAmount,a.PurchaseCount,a.DeliveryAddress,a.Longitude,a.Latitude,a.VipPasswrod,a.HeadImgUrl,a.Col1,
                            a.Col2,a.Col3,a.Col4,a.Col5,a.Col6,a.Col7,a.Col8,a.Col9,a.Col10,a.Col11,a.Col12,a.Col13,a.Col14,a.Col15,a.Col16,a.Col17,a.Col18,a.Col19,a.Col20,a.Col21,a.Col22,
                            a.Col23,a.Col24,a.Col25,a.Col26,a.Col27,a.Col28,a.Col29,a.Col30,a.Col31,a.Col32,a.Col33,a.Col34,a.Col35,a.Col36,a.Col37,a.Col38,a.Col39,a.Col40,a.Col41,a.Col42,a.Col43,a.Col44,a.Col45,a.Col46,
                            a.Col47,a.Col48,a.Col49,a.Col50,a.isActivate,a.VIPImportID,a.VipRealName,a.ShareVipId,a.SetoffUserId,a.ShareUserId
                            FROM cpos_ap.dbo.vip a "
                         + " LEFT JOIN vip b ON(a.WeiXinUserId = b.WeiXinUserId and b.isdelete='0') "
                         + " WHERE 1=1 ";
            if (OpenId != null && !OpenId.Equals(""))
            {
                sql += " and a.WeiXinUserId = '" + OpenId + "' ";
            }
            if (VipId != null && !VipId.Equals(""))
            {
                sql += " and a.vipid = '" + VipId + "' ";
            }
            sql += " AND b.VIPID IS NULL; ";
            this.SQLHelper.ExecuteNonQuery(sql);
            sql = "update vip  set  ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' where 1=1 and isdelete='0' ";
            if (OpenId != null && !OpenId.Equals(""))
            {
                sql += " and WeiXinUserId = '" + OpenId + "' ";
            }
            if (VipId != null && !VipId.Equals(""))
            {
                sql += " and vipid = '" + VipId + "' ";
            }
            sql += " ; ";
            this.SQLHelper.ExecuteNonQuery(sql);

            return true;
        }

        #endregion

        #region 获取中奖名单

        public DataSet GetLotteryVipList()
        {
            DataSet ds = new DataSet();
            string sql =
                "SELECT TOP 10 * FROM dbo.Vip WHERE  VipName IS NOT NULL AND VipName <> '' AND Phone IS NOT NULL AND Phone <> '' and ClientID = '" +
                this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取用户标签

        public DataSet GetVipTags(string VipId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT DISTINCT b.* FROM VipTagsMapping a "
                         + " INNER JOIN dbo.Tags b ON(a.TagsId = b.TagsId) "
                         + " WHERE a.IsDelete = '0' "
                         + " AND b.IsDelete = '0'  "
                         + " AND a.VipId = '" + VipId + "';";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取用户标签

        public DataSet GetVipTagsMapping(string VipId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.*,b.TagsName,b.TagsDesc,b.TagsFormula, tt.TypeName FROM VipTagsMapping a "
                         + " INNER JOIN dbo.Tags b ON(a.TagsId = b.TagsId) "
                         + " LEFT JOIN dbo.TagsType tt ON b.TypeId = tt.TypeId"
                         + " WHERE a.IsDelete = '0' "
                         + " AND b.IsDelete = '0'  "
                         + " AND a.VipId = '" + VipId + "';";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region Jermyn20131207 合并用户标识

        /// <summary>
        /// 处理用户合并，主要是因为之前的用户不是注册用户，注册之后存在两个帐号，需要合并
        /// </summary>
        /// <param name="UserId">缓存在Cookie中的</param>
        /// <param name="VipId"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool SetMergerVipInfo(string UserId, string VipId, string OpenId)
        {
            SqlParameter[] Parm = new SqlParameter[5];
            Parm[0] = new SqlParameter("@UserId", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = UserId;
            Parm[1] = new SqlParameter("@VipId", System.Data.SqlDbType.NVarChar, 100);
            Parm[1].Value = VipId;
            Parm[2] = new SqlParameter("@OpenId", System.Data.SqlDbType.NVarChar, 100);
            Parm[2].Value = OpenId;
            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ProcSetMergerVipInfo", Parm);
            return true;
        }

        #endregion

        #region Jermyn20131219 门店奖励

        /// <summary>
        /// 获取门店奖励数量
        /// </summary>
        /// <param name="vipInfo"></param>
        /// <returns></returns>
        public int GetUnitIntegralCount(VipEntity vipInfo)
        {
            string sql = GetUnitIntegralSql(vipInfo);
            sql += " select count(*) count From #tmp a ";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public DataSet GetUnitIntegral(VipEntity vipInfo)
        {
            vipInfo.Page = vipInfo.Page <= 0 ? 1 : vipInfo.Page;
            vipInfo.PageSize = vipInfo.PageSize <= 0 ? 15 : vipInfo.PageSize;
            int beginSize = (vipInfo.Page - 1) * vipInfo.PageSize + 1;
            int endSize = (vipInfo.Page - 1) * vipInfo.PageSize + vipInfo.PageSize;

            string sql = GetUnitIntegralSql(vipInfo);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize +
                   "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetUnitIntegralSql(VipEntity vipInfo)
        {
            if (vipInfo.BeginDate == null || vipInfo.BeginDate.Equals(""))
            {
                vipInfo.BeginDate = "null";
            }
            if (vipInfo.EndDate == null || vipInfo.EndDate.Equals(""))
            {
                vipInfo.EndDate = "null";
            }
            string sql = "select VIPID,MembershipShop,UnitId  "
                         +
                         " ,reverse(stuff(reverse(convert(varchar,convert(money,SearchIntegral),1)),1,3,'')) SearchIntegral,UnitCount "
                         +
                         " ,reverse(stuff(reverse(convert(varchar,convert(money,UnitSalesAmount),1)),1,3,''))  UnitSalesAmount,DisplayIndex=row_number() over(order by x.SearchIntegral desc) into #tmp From ( select a.* "
                         +
                         " ,(select UnitCount From (select UnitId,COUNT(*) UnitCount From VipUnitMapping vum inner join Vip v on v.VIPID=vum.VIPID and v.IsDelete=vum.IsDelete "
                         +
                         " where vum.IsDelete = '0' and vum.UnitId is not null and vum.VIPID is not null and vum.UnitId <> '' and vum.VIPID <> '' group by vum.UnitId "
                         + " )x where x.UnitId = a.VIPID) UnitCount "
                         +
                         " ,(select UnitSalesAmount From (select sales_unit_id,SUM(total_amount) UnitSalesAmount From T_Inout where order_type_id = '1F0A100C42484454BAEA211D4C14B80F' "
                         +
                         " and order_reason_id = '2F6891A2194A4BBAB6F17B4C99A6C6F5' and sales_unit_id is not null and sales_unit_id <> '' "
                         + " and ( (Field7 in ('2','3')) or status = '10') group by sales_unit_id "
                         + " )x where x.sales_unit_id = a.VIPID) UnitSalesAmount "
                         + " From ( "
                         +
                         " select a.VIPID,C.unit_id UnitId,c.unit_name MembershipShop,SUM(isnull(a.integral,0)) SearchIntegral  "
                         + " From VipIntegralDetail a "
                         + " inner join vw_unit_level b on(a.VipID = b.unit_id) "
                         +
                         " inner join t_unit c on(a.VIPID = c.unit_id and b.customer_id = c.customer_id and b.unit_id = c.unit_id) "
                         + " where a.IsDelete = '0' "
                         + " and c.status = '1' "
                         + " and b.customer_id=ISNULL('" + this.CurrentUserInfo.CurrentUser.customer_id +
                         "',b.customer_id) "
                         + " and b.path_unit_id like '%" + vipInfo.UnitId + "%' "
                         + " and a.CreateTime between isnull(" + vipInfo.BeginDate + ",'1990-01-01') and isnull(" +
                         vipInfo.EndDate + ",'9999-12-31') ";
            if (vipInfo.IntegralSourceIds != null && !vipInfo.IntegralSourceIds.Equals(""))
            {
                var idArray = vipInfo.IntegralSourceIds.Split(',');
                if (idArray != null && idArray.Length > 0)
                {
                    int i = 1;
                    sql += " and ( ";
                    foreach (var id in idArray)
                    {
                        if (i < idArray.Length)
                        {
                            sql += " a.IntegralSourceID = '" + id + "' or ";
                        }
                        else
                        {
                            sql += " a.IntegralSourceID = '" + id + "' ";
                        }
                        i = i + 1;
                    }
                    sql += " ) ";
                }
            }
            //+ " and (a.IntegralSourceID = '12' or a.IntegralSourceID = '15') "
            sql += " group by a.VIPID,c.unit_name,C.unit_id   "
                   + " )a)x;";


            return sql;
        }

        #endregion

        #region Jermyn20131219 导购员奖励

        /// <summary>
        /// 获取导购员奖励数量
        /// </summary>
        /// <param name="vipInfo"></param>
        /// <returns></returns>
        public int GetPurchasingGuideIntegralCount(VipEntity vipInfo)
        {
            string sql = GetPurchasingGuideIntegralSql(vipInfo);
            sql += " select count(*) count From #tmp a ";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public DataSet GetPurchasingGuideIntegral(VipEntity vipInfo)
        {
            vipInfo.Page = vipInfo.Page <= 0 ? 1 : vipInfo.Page;
            vipInfo.PageSize = vipInfo.PageSize <= 0 ? 15 : vipInfo.PageSize;
            int beginSize = (vipInfo.Page - 1) * vipInfo.PageSize + 1;
            int endSize = (vipInfo.Page - 1) * vipInfo.PageSize + vipInfo.PageSize;

            string sql = GetPurchasingGuideIntegralSql(vipInfo);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize +
                   "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetPurchasingGuideIntegralSql(VipEntity vipInfo)
        {
            #region 参数设置

            if (vipInfo.BeginDate == null || vipInfo.BeginDate.Equals(""))
            {
                vipInfo.BeginDate = "null";
            }
            if (vipInfo.EndDate == null || vipInfo.EndDate.Equals(""))
            {
                vipInfo.EndDate = "null";
            }
            string sql1 = string.Empty;
            if (vipInfo.IntegralSourceIds != null && !vipInfo.IntegralSourceIds.Equals(""))
            {
                var idArray = vipInfo.IntegralSourceIds.Split(',');
                if (idArray != null && idArray.Length > 0)
                {
                    int i = 1;
                    sql1 += " and ( ";
                    foreach (var id in idArray)
                    {
                        if (i < idArray.Length)
                        {
                            sql1 += " a.IntegralSourceID = '" + id + "' or ";
                        }
                        else
                        {
                            sql1 += " a.IntegralSourceID = '" + id + "' ";
                        }
                        i = i + 1;
                    }
                    sql1 += " ) ";
                }
            }

            #endregion

            string sql = string.Empty;
            sql = "select a.VIPID,a.UserId,a.UserName "
                  +
                  " ,reverse(stuff(reverse(convert(varchar,convert(money,a.SearchIntegral),1)),1,3,'')) SearchIntegral "
                  +
                  " ,reverse(stuff(reverse(convert(varchar,convert(money,b.UnitSalesAmount),1)),1,3,''))  UnitSalesAmount "
                  + " ,b.UnitId ,f.unit_name MembershipShop "
                  +
                  " ,(select VipCount From (select UserId,COUNT(*) VipCount From VipUnitMapping vum inner join Vip v on vum.VIPID=v.VIPID and vum.IsDelete=0"
                  +
                  " where VUM.IsDelete=0 and VUM.UnitId is not null and VUM.UnitId <> '' and VUM.VIPID is not null and VUM.VIPID <> '' and VUM.UserId is not null "
                  + " group by UserId)x where x.UserId = a.VIPID)  VipCount "
                  + " ,DisplayIndex=row_number() over(order by a.SearchIntegral desc) into #tmp "
                  + " From ( "
                  +
                  " select a.VIPID,b.user_id UserId,b.user_name UserName,SUM(isnull(a.Integral,0)) SearchIntegral From VipIntegralDetail a "
                  + " inner join T_User b on(a.VIPID = b.user_id) "
                  + " left join Vip c on(b.user_id = c.VIPID) "
                  + " where a.IsDelete = '0' "
                  + " and b.user_status = '1' "
                  + " and c.VIPID is null "
                  + " and b.customer_id = ISNULL('" + this.CurrentUserInfo.CurrentUser.customer_id + "',b.customer_id) "
                  + " and  a.CreateTime between isnull(" + vipInfo.BeginDate + ",'1900-01-01') and isnull(" +
                  vipInfo.EndDate + ",'9999-12-31') "
                //+ " and (a.IntegralSourceID = '14' or a.IntegralSourceID = '15') "
                  + sql1
                  + " group by a.VIPID,b.user_name,b.user_id "
                  + " )a "
                  +
                  " inner join (select create_user_id UserId,SUM(total_amount) UnitSalesAmount,MAX(sales_unit_id) UnitId From T_Inout where order_type_id = '1F0A100C42484454BAEA211D4C14B80F' "
                  + " and order_reason_id = '2F6891A2194A4BBAB6F17B4C99A6C6F5' "
                  + " and sales_unit_id is not null and sales_unit_id <> '' and data_from_id='2' "
                  + " and ( (Field7 in ('2','3')) or status = '10') group by create_user_id) b on(a.VIPID = b.UserId) "
                  + " inner join vw_unit_level c on(b.UnitId = c.unit_id) "
                  + " inner join t_unit f on( b.UnitId = f.unit_id and c.unit_id = f.unit_id) "
                  + " where f.Status = '1' "
                  + " and c.path_unit_id like '%" + vipInfo.UnitId + "%'";

            return sql;
        }

        #endregion

        #region Jermyn20131219 会员奖励

        /// <summary>
        /// 获取会员奖励数量
        /// </summary>
        /// <param name="vipInfo"></param>
        /// <returns></returns>
        public int GetVipIntegralCount(VipEntity vipInfo)
        {
            string sql = GetVipIntegralSql(vipInfo);
            sql += " select count(*) count From #tmp a ";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public DataSet GetVipIntegral(VipEntity vipInfo)
        {
            vipInfo.Page = vipInfo.Page <= 0 ? 1 : vipInfo.Page;
            vipInfo.PageSize = vipInfo.PageSize <= 0 ? 15 : vipInfo.PageSize;
            int beginSize = (vipInfo.Page - 1) * vipInfo.PageSize + 1;
            int endSize = (vipInfo.Page - 1) * vipInfo.PageSize + vipInfo.PageSize;

            string sql = GetVipIntegralSql(vipInfo);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize +
                   "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetVipIntegralSql(VipEntity vipInfo)
        {
            #region 参数设置

            if (vipInfo.BeginDate == null || vipInfo.BeginDate.Equals(""))
            {
                vipInfo.BeginDate = "null";
            }
            if (vipInfo.EndDate == null || vipInfo.EndDate.Equals(""))
            {
                vipInfo.EndDate = "null";
            }
            string sql1 = string.Empty;
            if (vipInfo.IntegralSourceIds != null && !vipInfo.IntegralSourceIds.Equals(""))
            {
                var idArray = vipInfo.IntegralSourceIds.Split(',');
                if (idArray != null && idArray.Length > 0)
                {
                    int i = 1;
                    sql1 += " and ( ";
                    foreach (var id in idArray)
                    {
                        if (i < idArray.Length)
                        {
                            sql1 += " a.IntegralSourceID = '" + id + "' or ";
                        }
                        else
                        {
                            sql1 += " a.IntegralSourceID = '" + id + "' ";
                        }
                        i = i + 1;
                    }
                    sql1 += " ) ";
                }
            }

            #endregion

            string sql = string.Empty;
            sql = "select a.VIPID,a.VipName,a.MembershipShop,reverse(stuff(reverse(convert(varchar,convert(money,a.SearchIntegral),1)),1,3,'')) SearchIntegral,a.VipSourceName,a.CreateTime,a.VipLevelDesc,a.VipCount,reverse(stuff(reverse(convert(varchar,convert(money,a.PurchaseAmount),1)),1,3,''))  SearchAmount "
                  + " ,DisplayIndex=row_number() over(order by a.SearchIntegral desc) into #tmp "
                  + " From ( "
                  + " select a.* "
                  + " ,b.unit_name MembershipShop "
                  + " ,b.path_unit_id "
                  + " From ( "
                  + " select a.VIPID "
                  + " ,case when b.VipName='' or b.VipName IS null then b.VipCode else b.VipName end VipName "
                  + " ,b.CreateTime,b.PurchaseAmount "
                  + " ,SUM(isnull(a.Integral,0)) SearchIntegral "
                  +
                  " ,(select x.VipCardGradeName From SysVipCardGrade x where x.CustomerID = b.ClientID and x.VipCardGradeID = b.VipLevel) VipLevelDesc "
                  + " ,(select x.VipSourceName From SysVipSource x where x.VipSourceID = b.VipSourceId) VipSourceName "
                  +
                  " ,(select VipCount From( select HigherVipID,COUNT(*) VipCount From Vip where HigherVipID is not null group by HigherVipID  )x "
                  + " where x.HigherVipID = a.VIPID)  VipCount "
                  + " From VipIntegralDetail a "
                  + " inner join Vip b on(a.VIPID = b.VIPID) "
                  + " where a.IsDelete = '0' "
                  + " and b.IsDelete = '0' "
                  + " and b.VipName like '%" + vipInfo.VipName + "%' "
                  + " and  isnull(a.CreateTime,GETDATE()) between isnull(" + vipInfo.BeginDate +
                  ",'1900-01-01') and isnull(" + vipInfo.EndDate + ",'9999-12-31') "
                //+ " and (a.IntegralSourceID = '1' or a.IntegralSourceID = '3') "
                  + sql1
                  +
                  " group by a.VIPID,b.VipName,b.CreateTime,b.PurchaseAmount,b.VipLevel,b.ClientID,b.VipSourceId,b.VipCode "
                  + " ) a "
                  + " left join (select a.VIPID,a.UnitId,c.unit_name,b.path_unit_id  From VipUnitMapping a "
                  + " inner join vw_unit_level b on(a.UnitId = b.unit_id) "
                  + " inner join t_unit c on(a.UnitId = c.unit_id) "
                  + " where a.IsDelete = '0' and c.status = '1' ) b on(a.VIPID = b.VIPID) "
                  + " ) a where 1=1 "
                  + " and a.path_unit_id like '%" + vipInfo.UnitId + "%'";

            return sql;
        }

        #endregion

        public DataSet GetSaleFunnelData()
        {
            string sql = "GetSaleFunnelData";

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql);
        }

        #region 获取店员门店
        public string GetUnitByUserId(string userId)
        {
            //            string sql = string.Format(@"SELECT unit_id FROM dbo.T_User_Role l
            //                        LEFT JOIN T_Role r ON l.role_id=r.role_id
            //                        WHERE l.user_id='{0}'
            //                        AND r.role_code='Unit'", userId);
            string sql = string.Format(@"SELECT unit_id FROM dbo.T_User_Role l
                                    LEFT JOIN T_Role r ON l.role_id=r.role_id
                                    WHERE l.user_id='{0}'
                                    and l.default_flag=1 ", userId);
            // AND r.role_code in ('CustomerOrders','CustomerService')", userId);
            object obj = SQLHelper.ExecuteScalar(sql);
            string ret = string.Empty;
            if (obj != null)
                ret = obj.ToString();
            return ret;
        }


        #endregion

        #region 根据会员角色获取App权限 add by Henry 2015-3-26
        public DataSet GetAppMenuByUserId(string userId)
        {
            string sql = string.Format(@"SELECT m.menu_code as MenuCode FROM dbo.T_User u
                                        INNER JOIN T_User_Role ur ON ur.user_id=u.user_id
                                        INNER JOIN dbo.T_Role_Menu rm ON rm.role_id=ur.role_id
                                        INNER JOIN dbo.T_Menu m ON m.menu_id=rm.menu_id
                                        INNER JOIN T_Def_App da ON da.def_app_id=m.reg_app_id
                                        WHERE u.user_id='{0}' AND da.def_app_code='APP' 
                                        AND rm.status=1 AND m.status=1 AND ur.status=1 AND u.user_status=1
                                        GROUP BY m.menu_code,m.display_index
                                        ORDER BY m.display_index ASC
                                        ", userId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取会员的各种状态订单数量Jermyn201223

        /// <summary>
        /// 获取会员的各种状态订单数量
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public DataSet GetVipOrderByStatus(string OpenId)
        {
            DataSet ds = new DataSet();
            string sql = "select a.Field7 Status,COUNT(*) ICount  From T_Inout a "
                         + " inner join Vip b on(a.vip_no = b.vipid) "
                         + " where a.status = '1' "
                         + " and a.order_type_id = '1F0A100C42484454BAEA211D4C14B80F' "
                         + " and a.order_reason_id = '2F6891A2194A4BBAB6F17B4C99A6C6F5' "
                         + " and b.WeiXinUserId = '" + OpenId + "' "
                         + " group by a.Field7";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        public DataSet GetVipByPhone(string phone, string vipId, string status)
        {
            DataSet ds = new DataSet();
            string sql = "select a.* from vip a where a.isDelete='0' and a.phone='" + phone + "' ";
            if (vipId != null && vipId.Trim().Length > 0)
            {
                sql += " and a.vipId<>'" + vipId + "' ";
            }
            if (status != null && status.Trim().Length > 0)
            {
                sql += " and a.status='" + status + "' ";
            }
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #region 获取列表

        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount_Emba(string keyword)
        {
            string sql = GetListSql_Emba(keyword);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList_Emba(string keyword, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = (Page - 1) * PageSize + PageSize;

            DataSet ds = new DataSet();
            string sql = GetListSql_Emba(keyword);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                   beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetListSql_Emba(string keyword)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from Vip a ";
            sql += " where a.IsDelete='0' ";
            sql += " and a.ClientID='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            sql += " and (a.vipRealName like '%" + keyword + "%' or a.phone = '%" + keyword + "%' or a.col43 = '%" +
                   keyword + "%') ";
            return sql;
        }

        #endregion

        #region 俄丽亚

        public VipEntity[] GetByPhone(string pPhone, string pCustomerID)
        {
            List<VipEntity> list = new List<VipEntity> { };
            string sql = string.Format("select * from vip where phone='{0}' and ClientID='{1}'", pPhone, pCustomerID);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    VipEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        #endregion

        #region 会员查询Location

        /// <summary>
        /// 获取查询会员的信息
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchVipInfoLocation(VipSearchEntity vipSearchInfo)
        {
            int beginSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize;
            int endSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize + vipSearchInfo.PageSize;

            string sql = SearchVipSqlLocation(vipSearchInfo);
            //JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = "活动sql:" + sql.ToString().Trim() });
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize +
                  "' order by a.displayindex";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string SearchVipSqlLocation(VipSearchEntity vipSearchInfo)
        {
            PublicService pService = new PublicService();
            string sql = string.Empty;
            MarketSignInDAO mDao = new MarketSignInDAO(this.CurrentUserInfo);

            sql =
                " select VIPID,VipCode,VipLevel,VipName,Phone,WeiXin,Integration,LastUpdateTime,PurchaseAmount,PurchaseCount,weiXinUserId,isDelete,VipSourceId,Status,Gender,ClientID,DeliveryAddress,TencentMBlog,Latitude,longitude into #vip From vip  ";
            sql += " WHERE Longitude is not null and Latitude is not null and IsDelete = '0' and ClientID = '" +
                   this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            string strOrder = string.Empty;


            if (vipSearchInfo.OrderBy != null && vipSearchInfo.OrderBy.Trim().Length > 0)
            {
                strOrder = " order by " + vipSearchInfo.OrderBy + " ";
            }

            sql += " SELECT DISTINCT a.*,b.UnitId,(SELECT unit_id FROM dbo.t_unit x WHERE x.unit_name = a.TencentMBlog and x.customer_id = a.ClientID  ) LastSalesUnitId  into #VipTmp FROM #vip a "
                   + " LEFT JOIN dbo.VipUnitMapping b "
                   + " ON(a.VIPID = b.VIPID AND b.IsDelete = '0' ); "; //where a.weixin='gh_bf70d7900c28'
            //Jermyn20131123去掉写死的业务逻辑

            sql += "SELECT a.* "
                   +
                   " ,(SELECT x.VipSourceName FROM dbo.SysVipSource x WHERE x.VipSourceID = a.VipSourceId) VipSourceName "
                   + " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc "
                   + " ,'' LastUnit "
                   + " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc "
                   + " ,CASE WHEN a.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                //+ " ,DisplayIndex=row_number() over( " + strOrder + "  ) "
                   + " ,(select sum(Integral) from VipIntegralDetail where IsDelete='0' "
                   + "   and fromVipId=a.vipId and vipId='" + vipSearchInfo.HigherVipId + "') IntegralForHightUser "
                   + " ,(select unit_name From t_unit x where x.unit_id = a.unitId) MembershipShop ";
            //会籍店 Jermyn20130904

            if (vipSearchInfo.RoleCode != null && vipSearchInfo.RoleCode.Trim().Length > 0)
            {
                sql += " ,(case when '" + vipSearchInfo.Latitude + "' = '0' and '" + vipSearchInfo.Longitude +
                       "' = '0' then '0' else ABS(dbo.DISTANCE_TWO_POINTS(" + Convert.ToDouble(vipSearchInfo.Latitude) +
                       "," + Convert.ToDouble(vipSearchInfo.Longitude) + ",a.Latitude,a.longitude)) end) Distance  ";
            }
            else
            {
                sql += " ,'0' Distance ";
            }

            sql += " into #tmpVip2 FROM #VipTmp a ";

            if (vipSearchInfo.RoleCode != null && vipSearchInfo.RoleCode.Trim().Length > 0)
            {
                sql += " inner join VIPRoleMapping r1 on (r1.vipId=a.vipId and r1.isDelete='0') ";
                sql += " inner join t_role r2 on (r1.roleId=r2.role_id and r2.role_code='" + vipSearchInfo.RoleCode +
                       "') ";
            }

            sql += " WHERE a.IsDelete = 0 and a.ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            //if (vipSearchInfo.VipInfo != null && !vipSearchInfo.VipInfo.Equals(""))
            //{
            //    sql += " and (a.VipCode like '%" + vipSearchInfo.VipInfo + "%'  or a.VipName like '%" + vipSearchInfo.VipInfo + "%' ) ";
            //}
            //sql = pService.GetLinkSql(sql, "a.VipCode", vipSearchInfo.VipInfo, "%");
            //sql = pService.GetLinkSql(sql, "a.VipName", vipSearchInfo.VipInfo, "%");
            //sql = pService.GetLinkSql(sql, "a.LastSalesUnitId", vipSearchInfo.UnitId, "=");
            //sql = pService.GetLinkSql(sql, "a.Phone", vipSearchInfo.Phone, "%");
            //sql = pService.GetLinkSql(sql, "a.VipSourceId", vipSearchInfo.VipSourceId, "=");
            //sql = pService.GetLinkSql(sql, "a.higherVipId", vipSearchInfo.HigherVipId, "=");
            //if (!vipSearchInfo.Status.ToString().Equals("0"))
            //{
            //    sql = pService.GetLinkSql(sql, "a.Status", vipSearchInfo.Status.ToString(), "=");
            //}
            //if (!vipSearchInfo.VipLevel.ToString().Equals("0"))
            //{
            //    sql = pService.GetLinkSql(sql, "a.VipLevel", vipSearchInfo.VipLevel.ToString(), "=");
            //}
            //sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RegistrationTime,120) ", vipSearchInfo.RegistrationDateBegin, ">");
            //sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RegistrationTime,120) ", vipSearchInfo.RegistrationDateEnd, "<=");
            //sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RecentlySalesTime,120) ", vipSearchInfo.RecentlySalesDateBegin, ">");
            //sql = pService.GetLinkSql(sql, "CONVERT(NVARCHAR(10),a.RecentlySalesTime,120) ", vipSearchInfo.RecentlySalesDateEnd, "<=");
            //if (!vipSearchInfo.IntegrationBegin.ToString().Equals("0"))
            //{
            //    sql = pService.GetLinkSql(sql, "a.Integration ", vipSearchInfo.IntegrationBegin.ToString(), ">");
            //}
            //if (!vipSearchInfo.IntegrationEnd.ToString().Equals("0"))
            //{
            //    sql = pService.GetLinkSql(sql, "a.Integration ", vipSearchInfo.IntegrationEnd.ToString(), "<=");
            //}
            //if (vipSearchInfo.Gender != null && vipSearchInfo.Gender.Trim().Length > 0)
            //{
            //    sql = pService.GetLinkSql(sql, "a.Gender ", vipSearchInfo.Gender.Trim(), "=");
            //}
            //if (vipSearchInfo.MembershipShopId != null && vipSearchInfo.MembershipShopId.Trim().Length > 0)
            //{
            //    sql = pService.GetLinkSql(sql, "a.unitId ", vipSearchInfo.MembershipShopId.Trim(), "=");
            //}

            sql += ";";

            sql += " select a.* ";
            sql += " ,DisplayIndex=row_number() over( " + strOrder + "  ) ";
            sql += " into #tmp from #tmpVip2 a ; ";


            Loggers.Debug(new DebugLogInfo() { Message = "SearchVipSqlLocation sql: " + sql });

            return sql;
        }

        #endregion

        #region 同步阿拉丁会员信息

        public void SyncAladingUserInfo(string customerId, string vipId, string vipName, string vipRealName,
            string phone,
            string deviceToken, string channelIdBaidu, string baiduPushAppId, string userIdBaidu, string platform,
            string chanelId)
        {
            string sql = "spVipInfo";

            if (platform != null)
            {
                platform = platform.Equals("1") ? "2" : "1";
            }
            else
            {
                platform = "1";
            }

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@CustomerID", customerId));
            parameter.Add(new SqlParameter("@VipID", vipId));
            parameter.Add(new SqlParameter("@VipName", vipName));
            parameter.Add(new SqlParameter("@VipRealName", vipRealName));
            parameter.Add(new SqlParameter("@Phone", phone));
            parameter.Add(new SqlParameter("@DeviceToken", deviceToken));
            parameter.Add(new SqlParameter("@ChannelIDBaiDu", channelIdBaidu));
            parameter.Add(new SqlParameter("@BaiduPushAppID", baiduPushAppId));
            parameter.Add(new SqlParameter("@UserIDBaiDu", userIdBaidu));
            parameter.Add(new SqlParameter("@Platform", platform));
            parameter.Add(new SqlParameter("@Plat", string.Empty));
            parameter.Add(new SqlParameter("@Channel", chanelId));

            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, parameter.ToArray());
        }

        #endregion

        #region 客服登录

        /// <summary>
        /// 客服登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet SetSignIn(string userName, string password, string customerId)
        {
            string sql = string.Empty;
            sql += " SELECT * FROM dbo.T_User a ";
            sql += " INNER JOIN dbo.T_User_Role b ON a.user_id = b.user_id ";
            sql += " INNER JOIN dbo.T_Role c ON b.role_id = c.role_id ";
            sql += " WHERE c.role_code = 'CustomerService' ";
            sql += " AND a.customer_id = '" + customerId + "' ";
            sql += " AND (user_code = '" + userName + "' OR user_telephone = '" + userName + "') ";
            sql += " AND user_password = '" + password + "' ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #region 判断用户是否存在

        public bool JudgeUserExist(string userName, string customerId)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pUserName", Value = userName });
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });

            StringBuilder sql = new StringBuilder();
            sql.Append(" select 1 from t_user where customer_id = @pCustomerId and ");
            sql.Append(" (user_code = @pUserName or user_telephone = @pUserName)");

            int result = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));

            if (result == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region 判断密码是否正确

        public bool JudgeUserPasswordExist(string userName, string customerId, string password)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pUserName", Value = userName });
            paras.Add(new SqlParameter() { ParameterName = "@pPassword", Value = password });
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });

            StringBuilder sql = new StringBuilder();
            sql.Append(" select 1 from t_user where customer_id = @pCustomerId and ");
            sql.Append(" (user_code = @pUserName or user_telephone = @pUserName)");
            sql.Append(" and user_password = @pPassword");

            int result = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));

            if (result == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region 判断该客服人员是否有客服或操作订单的权限

        public bool JudgeUserRoleExist(string userName, string customerId, string password)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pUserName", Value = userName });
            paras.Add(new SqlParameter() { ParameterName = "@pPassword", Value = password });
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });

            StringBuilder sql = new StringBuilder();
            sql.Append(" select 1 from t_user a inner join t_user_role b on a.user_id = b.user_id");
            sql.Append(" inner join t_role c on b.role_id = c.role_id");
            sql.Append(" where a.customer_id = @pCustomerId and ");
            sql.Append(" (user_code = @pUserName or user_telephone = @pUserName)");
            sql.Append(" and user_password = @pPassword");
            sql.Append(" and c.role_code in ('CustomerService','CustomerOrders')");

            int result = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));

            if (result == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region 获取UserId

        public DataSet GetUserIdByUserNameAndPassword(string userName, string customerId, string password)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pUserName", Value = userName });
            paras.Add(new SqlParameter() { ParameterName = "@pPassword", Value = password });
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });

            StringBuilder sql = new StringBuilder();
            sql.Append(" select user_id,user_name,user_status,user from t_user where customer_id = @pCustomerId and ");
            sql.Append(" (user_code = @pUserName or user_telephone = @pUserName)");
            sql.Append(" and user_password = @pPassword ");
            sql.Append(" order by user_status desc ");//按照状态倒序排，如果有一个账号已经被停用了，又用这个账号建了一个，先取没被停用的

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        #endregion

        #region 获取角色列表

        public DataSet GetRoleCodeByUserId(string userId, string customerId)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pUserId", Value = userId });
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });


            StringBuilder sql = new StringBuilder();

            sql.Append(" select distinct role_code,a.role_id from T_Role a,T_User_Role b where");
            sql.Append(" a.role_id = b.role_id and a.customer_id = @pCustomerId");
            sql.Append(" and b.user_id = @pUserId");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        #endregion

        #endregion

        #region 会员登录 Add by Alex Tian 2014-04-11

        public VipEntity[] GetLoginInfo(string VipNo, string mobile, string password)
        {
            List<VipEntity> list = new List<VipEntity> { };
            StringBuilder sbSQL = new StringBuilder();
            string strWhere = "1=1";
            if (!string.IsNullOrEmpty(VipNo))
            {
                strWhere += " And ((VIPID='" + VipNo + "')";
            }
            if (!string.IsNullOrEmpty(mobile))
            {
                strWhere += " Or (Phone='" + mobile + "') ";
            }
            strWhere += ")";
            if (!string.IsNullOrEmpty(password))
            {
                strWhere += " And VipPasswrod='" + password + "'";
            }
            sbSQL.Append("select * from VIP Where " + strWhere);
            using (var rd = this.SQLHelper.ExecuteReader(sbSQL.ToString()))
            {
                while (rd.Read())
                {
                    VipEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        #endregion

        /// <summary>
        /// 根据会员编号、手机号，合并会员信息； 操作成功返回1，失败返回0
        /// </summary>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pVipID">会员ID</param>
        /// <param name="pPhone">手机号</param>
        /// <returns></returns>
        public bool MergeVipInfo(string pCustomerID, string pVipID, string pPhone)
        {
            string sql = string.Format("exec spMergeVipInfo '{0}','{1}','{2}'", pCustomerID, pVipID, pPhone);
            var result = this.SQLHelper.ExecuteScalar(sql);
            return result.ToString() == "1";
        }

        public string GetVipCode(string pCustomerID)
        {
            string sql = string.Format(@"declare @ReturnValue nvarchar(50)
exec spGetNextCode '{0}','VipCode',7,'',@ReturnValue output
select @ReturnValue", pCustomerID);
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }

        #region 获取会员的优惠券数量

        public int GetVipCoupon(string vipId)
        {
            var sql = new StringBuilder();

            sql.Append("select isnull(COUNT(1),0) as num from VipCouponMapping a,Coupon b");
            sql.Append(" where a.CouponID = b.CouponID and a.IsDelete =0 and b.status = 0  ");
            sql.AppendFormat(" and b.IsDelete = 0 and a.vipId = '{0}' and  EndDate > GETDATE() ", vipId);//没有被使用，并且没有过期

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));
        }

        #endregion

        public string GetSettingValue(string customerId)
        {
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId } };

            var sql = new StringBuilder();
            sql.Append("select settingValue from CustomerBasicSetting where CustomerID = @pCustomerId");
            sql.Append(" and isdelete = 0 and  SettingCode='SMSSign'");

            return Convert.ToString(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));
        }

        public DataSet GetVipColumnInfo(string eventCode, string customerId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter() {ParameterName = "@pEventCode", Value = eventCode}
            };

            var sql = new StringBuilder();

            sql.Append(" create table #tmp1(id uniqueidentifier);");
            sql.Append(" insert into #tmp1 select distinct mpb.MobilePageBlockID  from ");
            sql.Append(" MobilePageBlock mpb inner join MobileModule mm on mpb.MobileModuleID = mm.MobileModuleID");
            sql.Append(" inner join MobileModuleObjectMapping mmo on mm.MobileModuleID = mmo.MobileModuleID");
            sql.Append(" inner join LEvents l on l.EventID = mmo.objectid");
            sql.Append(" inner join LEventsGenre le on l.EventGenreId = le.EventGenreId");
            sql.Append(" where mpb.IsDelete = 0 and mm.isdelete =0 and mmo.isdelete = 0 ");
            sql.Append(" and l.IsDelete=0 and le.isdelete = 0");
            sql.Append(
                " and le.GenreCode = @pEventCode and (isnull(l.CustomerId,'') = '' or l.CustomerId = @pCustomerId);");

            //表单页信息
            //sql.Append(
            //    " select b.ID ,isnull(a.sort,0) as DisplayIndex from MobilePageBlock a,#tmp1 b where a.MobilePageBlockID = b.id and type = 1;");

            sql.Append(" create table #tmp2(id uniqueidentifier);");
            sql.Append(" insert into #tmp2");
            sql.Append(
                " select distinct b.ID from MobilePageBlock a,#tmp1 b where a.MobilePageBlockID = b.id and type = 1;");

            //块信息
            //sql.Append(
            //    " select a.MobilePageBlockID as ID,a.ParentID as PageId, isnull(a.sort,0) as DisplayIndex from MobilePageBlock a,#tmp2 b where a.ParentID = b.id and type = 2;");


            sql.Append(" create table #tmp3(id uniqueidentifier);");
            sql.Append(" insert into #tmp3");
            sql.Append(
                " select distinct a.MobilePageBlockID from MobilePageBlock a,#tmp1 b where a.ParentID = b.id and type = 2;");


            //扩展属性定义信息
            //sql.Append(
            //    " select b.MobileBussinessDefinedID ,c.id as BlockId,a.Title,isnull(b.EditOrder,0) as DisplayIndex");
            //sql.Append(" from MobilePageBlock a, MobileBussinessDefined b,#tmp3 c where a.MobilePageBlockID = c.id");
            //sql.Append(" and a.MobilePageBlockID = b.MobilePageBlockID and a.isdelete = 0 and b.isdelete = 0;");

            //控件信息
            sql.Append(
                " select  a.MobileBussinessDefinedID,ColumnName,ControlType,columnDesc,CorrelationValue from MobileBussinessDefined a,#tmp3 b where");
            sql.Append(" a.MobilePageBlockID = b.id and a.IsDelete = 0");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public DataSet GetVipInfo(string vipId)
        {
            var paras = new List<SqlParameter>();

            var sql = "select * from vip where vipid = @pVipId and isdelete = 0";

            paras.Add(new SqlParameter() { ParameterName = "@pVipId", Value = vipId });

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, paras.ToArray());
        }


        public string GetVipLeave(string vipId)
        {
            string sql = "select isnull(VipCardGradeName,'') VipLevelName From SysVipCardGrade x,vip a where x.vipcardGradeId = a.vipLevel and a.vipId = @pVipId";
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pVipId", Value = vipId } };

            return Convert.ToString(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, paras.ToArray()));
        }


        #region 根据skuId和qty获取商品的可用积分

        public decimal GetIntegralBySkuId(string skuIdList)
        {
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pSkuIdList", Value = skuIdList } };

            var sql = new StringBuilder();
            sql.Append("select sum(isnull(a.IntegralExchange,0)*c.qty) as Integral ");
            sql.Append(" from vw_item_detail a inner join t_sku b on a.item_id = b.item_id");
            sql.Append(" inner join dbo.[SplitStr](@pSkuIdList,';') c on b.sku_id = c.skuId");
            sql.Append(" where a.IsExchange = 1");

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());
            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }
        }

        #endregion

        #region 根据skuId和qty获取商品的金额

        public decimal GetTotalSaleAmountBySkuId(string skuIdList)
        {
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pSkuIdList", Value = skuIdList } };

            var sql = new StringBuilder();
            sql.Append("select sum(isnull(a.SalesPrice,0)*c.qty) as amount ");
            sql.Append(" from vw_item_detail a inner join t_sku b on a.item_id = b.item_id");
            sql.Append(" inner join dbo.[SplitStr](@pSkuIdList,';') c on b.sku_id = c.skuId");
            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());
            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }
        }

        #region 根据skuId和qty获取商品的返现总金额
        public decimal GetTotalReturnAmountBySkuId(string skuIdList, SqlTransaction tran)
        {
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pSkuIdList", Value = skuIdList } };

            var sql = new StringBuilder();
            sql.Append("select sum(isnull(a.gg,0)*c.qty) as ReturnAmount ");
            sql.Append(" from vw_item_detail a inner join t_sku b on a.item_id = b.item_id");
            sql.Append(" inner join dbo.[SplitStr](@pSkuIdList,';') c on b.sku_id = c.skuId");
            object result;
            if (tran != null)
            {
                result = this.SQLHelper.ExecuteScalar(tran, CommandType.Text, sql.ToString(), paras.ToArray());
            }
            else
            {
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());
            }

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }
        }
        #endregion

        #endregion

        #region 获取积分的兑换比例
        public decimal GetIntegralAmountPre(string customerId)
        {
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId } };

            var sql = new StringBuilder();
            sql.Append(" select SettingValue From CustomerBasicSetting where IsDelete = '0'");
            sql.Append(" and CustomerID = @pCustomerId");
            sql.Append(" and SettingCode = 'IntegralAmountPer';");

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());
            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }

        }
        #endregion

        //获取会员账户余额
        public decimal GetVipEndAmount(string vipId)
        {
            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pVipId", Value = vipId } };

            var sql = "select EndAmount from VipAmount where IsDelete = 0 and VipId =@pVipId";

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());
            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }
        }

        /// <summary>
        /// 获取会员优惠券集合
        /// </summary>
        /// <param name="vipId">会员ID</param>
        /// <param name="totalPayAmount">支付金额</param>
        /// <param name="usableRange">适用范围(1=购物券；2=服务券)</param>
        /// <param name="objectID">优惠券使用门店/分销商ID</param>
        /// <param name="type">是否包含抵用券（0=包含抵用券；1=不包含抵用券）</param>
        /// <returns></returns>
        public DataSet GetVipCouponDataSet(string vipId, decimal totalPayAmount, int usableRange, string objectID, int type)
        {
            //var paras = new List<SqlParameter>
            //{
            //    new SqlParameter() { ParameterName = "@pVipId", Value = vipId } ,
            //    new SqlParameter(){ParameterName = "@pTotalPayAmount",Value = totalPayAmount}

            //};

            var sql = new StringBuilder();
            //sql.Append("select displayIndex = ROW_NUMBER() over (order by enableFlag,parValue desc), * from (");
            //sql.Append(" select a.CouponID,a.CouponDesc,convert(nvarchar(10),a.BeginDate,121) BeginDate,convert(nvarchar(10),a.EndDate,121) EndDate,b.ParValue , ");
            //sql.Append(" ValidDateDesc = '有效期：' + convert(nvarchar(10),a.BeginDate,121) +'--' +convert(nvarchar(10),a.EndDate,121), ");
            //sql.Append(" EnableFlag = case when b.ConditionValue<= @pTotalPayAmount then 1 else  0 end from Coupon a,CouponType b,VipCouponMapping c");
            //sql.Append(" where  CONVERT(nvarchar(200), a.CouponTypeID) = CONVERT(nvarchar(200), b.CouponTypeID)");   //将CouponType表主键GUID转换为字符串再进行比较
            //sql.Append(" and c.CouponID = a.CouponID");
            //sql.Append(" and a.Status = 0 and a.IsDelete = 0");
            //sql.Append(" and EndDate>GETDATE()");
            //sql.Append(" and c.VIPID = @pVipId");
            //sql.Append(" and c.IsDelete = 0) t");

            sql.Append(" SELECT  displayIndex = ROW_NUMBER() OVER ( ORDER BY enableFlag, parValue DESC ) ,* ");
            sql.Append(" FROM    ( SELECT    a.CouponID , ");
            sql.Append(" a.CouponCode , ");
            sql.Append(" a.CoupnName , ");
            sql.Append(" a.CouponDesc , ");
            sql.Append(" CONVERT(NVARCHAR(10), a.BeginDate, 121) BeginDate , ");
            sql.Append(" CONVERT(NVARCHAR(10), a.EndDate, 121) EndDate , ");
            sql.Append(" b.ParValue , ");
            sql.Append(" ValidDateDesc = '有效期：' ");
            sql.Append(" + CONVERT(NVARCHAR(10), a.BeginDate, 121) + '--' ");
            sql.Append(" + CONVERT(NVARCHAR(10), a.EndDate, 121) , ");
            sql.Append(" EnableFlag = CASE WHEN b.ConditionValue <= @pTotalPayAmount ");
            sql.Append(" THEN 1 ");
            sql.Append(" ELSE 0 ");
            sql.Append(" END ");
            sql.Append(" FROM Coupon a ");
            sql.Append(" INNER JOIN CouponType b ON CONVERT(NVARCHAR(200), a.CouponTypeID) = CONVERT(NVARCHAR(200), b.CouponTypeID) ");
            sql.Append(" INNER JOIN VipCouponMapping c ON c.CouponID = a.CouponID ");
            sql.Append(" LEFT JOIN dbo.CouponTypeUnitMapping d ON d.CouponTypeID = b.CouponTypeID ");
            sql.Append(" WHERE     a.Status = 0 ");
            sql.Append(" AND a.IsDelete = 0 ");
            sql.Append(" AND EndDate > GETDATE() ");
            sql.Append(" AND c.VIPID = @pVipId ");
            sql.Append(" AND c.IsDelete = 0 ");
            sql.Append(" AND b.UsableRange = @UsableRange ");
            sql.Append(" AND ( b.SuitableForStore = 1 OR d.ObjectID = @ObjectID  )");
            if (type > 0)
            {
                sql.Append(" AND b.ParValue>0 ");
            }
            sql.Append(" ) t");

            var paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pVipId", Value = vipId });
            paras.Add(new SqlParameter() { ParameterName = "@pTotalPayAmount", Value = totalPayAmount });
            paras.Add(new SqlParameter() { ParameterName = "@UsableRange", Value = usableRange });
            paras.Add(new SqlParameter() { ParameterName = "@ObjectID", Value = objectID });

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }


        public void ProcSetCancelOrder(string customerId, string orderId, string vipId)
        {
            var parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@VipId", System.Data.SqlDbType.NVarChar, 100) { Value = vipId };
            parm[1] = new SqlParameter("@OrderId", System.Data.SqlDbType.NVarChar, 100) { Value = orderId };
            parm[2] = new SqlParameter("@CustomerId", System.Data.SqlDbType.NVarChar, 100) { Value = customerId };

            this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcSetCancelOrder", parm);

        }

        public DataSet VipLandingPage(string customerId)
        {
            var parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@CustomerId", System.Data.SqlDbType.NVarChar, 100) { Value = customerId };

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "LandingPage", parm);
        }


        public DataSet GetVipIntegralDetail(string vipId, int pPageIndex, int pPageSize)
        {
            var sql = new StringBuilder();
            sql.Append(" select * from (");
            sql.Append(" select row_number()over(order by a.createTime desc) as _row, a.Integral ,");
            sql.Append(" b.IntegralSourceName,convert(nvarchar(10),a.createTime,121) CreateTime");
            sql.Append("  from VipIntegralDetail a,SysIntegralSource b where a.IntegralSourceID = b.IntegralSourceID");
            sql.Append(" and VIPID = @pVipId");
            sql.Append(" and b.isdelete = 0 and a.isdelete = 0");
            sql.Append(" ) t;");
            sql.Append("");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pPageIndex * pPageSize + 1, (pPageIndex + 1) * pPageSize);

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter() { ParameterName = "@pVipId", Value = vipId });
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        public DataSet GetVipEndAmountDetail(string vipId, int pPageIndex, int pPageSize)
        {
            var sql = new StringBuilder();
            sql.Append(" select * from (");
            sql.Append(" select row_number()over(order by a.createTime desc) as _row, a.Amount ,");
            sql.Append(" case AmountSourceId when '1' then '订单消费' when '2' then '订单返现' when '3' then '抽奖金额' end  ");
            sql.Append(" IntegralSourceName,convert(nvarchar(10),a.createTime,121) CreateTime");
            sql.Append("  from VipAmountDetail a where");
            sql.Append(" VIPID = @pVipId");
            sql.Append(" and a.isdelete = 0");
            sql.Append(" )");
            sql.Append("t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pPageIndex * pPageSize + 1, (pPageIndex + 1) * pPageSize);

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter() { ParameterName = "@pVipId", Value = vipId });
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        #region 充值卡模块，查找会员
        public DataSet GetCardVip(string criterion, string couponCode, int pageSize, int pageIndex)
        {
            var sql = new StringBuilder();

            if (string.IsNullOrEmpty(criterion) && (!string.IsNullOrEmpty(couponCode)))
            {
                sql.Append("select row_number() over(order by a.createTime desc) as _row, a.*, b.EndAmount  into #tmp  from Vip a ");
                sql.Append("left join VipAmount b on a.VipID = b.VipID  where ");
                sql.Append("a.VipId=(select VIPID from VipCouponMapping where ");
                sql.Append("CouponID=(select CouponID from Coupon where Status =0 and CouponCode='" + couponCode + "'))");
            }
            else
            {
                sql.Append(" select row_number() over(order by a.createTime desc) as _row, a.*, b.EndAmount ");
                sql.Append(" into #tmp from Vip a left join VipAmount b on a.VipID = b.VipID where");
                sql.Append(" (Phone = @criterion OR VipRealName = @criterion OR Col2 = @criterion)");
                sql.Append(" and a.isdelete = 0 and a.LastUpdateBy <> 'MERGE' ");
                if (!string.IsNullOrEmpty(couponCode))
                {
                    sql.Append(" and a.VipId=(select VIPID from VipCouponMapping ");
                    sql.Append("where CouponID=(select CouponID from Coupon where Status =0 and CouponCode='" + couponCode + "'))");
                }
            }
            sql.Append(" ; ");
            sql.Append(" select * from #tmp");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sql.AppendFormat(" select floor(COUNT(1) / {0}) + 1 AS TotalPage, COUNT(1) AS TotalCount from #tmp;", pageSize);

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter() { ParameterName = "@criterion", Value = criterion });
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }
        #endregion

        public string GetMaxVipCode()
        {
            string sql = "select MAX(VipCode)  from Vip";
            return this.SQLHelper.ExecuteScalar(sql).ToString();

        }
        public void AddVipWXDownload(UserInfoEntity item)
        {

            string sql = @"  insert into VipWXDownload(VipId,VipCode,VipName,OpenId,WeiXin,Gender,City,Age,BatNo,HeadImgUrl,CustomerId,IsDelete,CreateBy) values(
                             '" + item.VipId + "','" + item.VipCode + "','" + item.nickname + "','" + item.openid + @"','" + item.WeXin + "','" + item.sex + @"','" + item.city + @"','','" + item.BatNo + @"','" + item.headimgurl + @"'
                             ,'" + item.CustomerId + @"','0','" + this.CurrentUserInfo.UserID + "' )";
            this.SQLHelper.ExecuteNonQuery(sql);


        }

        public int WXToVip(string BatNo)
        {
            string sql = "ProcWXToVip";
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@BatNo", BatNo));
            return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, parameter.ToArray());

        }


        public string GetVipSearchPropList(string customerId, string tableName, string unitId)
        {
            string sql = "ProcGetVipSelectAttr";
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@CustomerId", customerId));
            parameter.Add(new SqlParameter("@TableName", tableName));
            parameter.Add(new SqlParameter("@UnitID", unitId));
            var result = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, parameter.ToArray());

            if (result == null || string.IsNullOrEmpty(result.ToString()))
            {
                return "";
            }
            else
            {
                return result.ToString();
            }

        }
        /// <summary>
        /// 获取新增会员动态配置属性
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public string GetCreateVipPropList(string customerId, string userId)
        {
            var sql = "ProcGetVipEditAttr";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustomerId", customerId));
            parameters.Add(new SqlParameter("@UserID", userId));
            parameters.Add(new SqlParameter("@TableName", "VIP"));
            var result = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, parameters.ToArray());
            if (result == null || string.IsNullOrEmpty(result.ToString()))
                return "";
            return result.ToString();
        }
        /// <summary>
        /// 获取更新会员所需列信息以及列对应的值
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public DataSet GetExistVipInfo(string customerId, string userId, string vipId)
        {
            var sql = @" exec ProcGetVipEditAttr '{0}','{1}', 'VIP';
                        declare @columns nvarchar(4000);
                        select @columns = dbo.FnGetCustomerTableColumn('{0}','vip',2);
                        set @columns = 'select '+@columns + ' from vip where vipid='''+'{2}'+'''';
                        exec (@columns)";
            return this.SQLHelper.ExecuteDataset(string.Format(sql, customerId, userId, vipId));
        }

        public DataSet GetVipTagTypeList()
        {
            var sql = @"select TypeId,TypeName from TagsType 
                        where Isdelete = 0
                        order by CreateTime 
                        ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetVipTagList(string customerId)
        {
            var sql = string.Format(@"select a.TagsId,a.TagsName,b.TypeId,a.TagsDesc
                        from Tags a,TagsType b
                        where a.TypeId = b.TypeId
                        and a.isdelete = 0 
                        and b.isdelete = 0 
                        and a.customerId = '{0}'
                        order by a.CreateTime
                        ", customerId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetVipIntegralList(string vipId, int pageIndex, int pageSize, string sortType)
        {
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";
            var sql = new StringBuilder();
            sql.Append(" select * from (");
            sql.AppendFormat(" select ROW_NUMBER()over(order by a.CreateTime {0}) _row, a.VIpIntegralDetailId,a.Integral,a.UnitID,a.UnitName,a.VipCardCode,Reason,a.integralSourceId,a.CreateBy,IntegralSource = b.IntegralSourceName,oi.ImageUrl", sortType);
            sql.Append(" ,ISNULL(a.Remark,'无') AS Remark,a.CreateTime from");
            sql.Append(" VipIntegralDetail a left join SysIntegralSource  b");
            sql.Append(" on a.IntegralSourceId = b.IntegralSourceId  and b.isdelete = 0 ");
            sql.Append(" left join ObjectImages oi on oi.ObjectID=a.VipIntegralDetailID and oi.isdelete=0");
            sql.Append(" where a.isdelete = 0");
            if (!string.IsNullOrEmpty(vipId))
            {
                sql.AppendFormat(" and a.vipId = '{0}'", vipId);
            }
            sql.Append(") t");
            sql.AppendFormat(" where t._row>{0}*{1} and t._row<=({0}+1)*{1}", pageIndex - 1, pageSize);


            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }

        public DataSet GetVipOrderList(string vipId, string customerId, int pageIndex, int pageSize, string sortType)
        {
            if (string.IsNullOrEmpty(sortType))
                sortType = "desc";

            #region 花间堂定制功能 获取房态表信息 2014-11-5
            string countSql = "select COUNT(*) from T_Inout i ";
            countSql += " left join T_Inout_Detail ind on i.order_id=ind.order_id "
            + " left join T_Sku s on ind.sku_id=s.sku_id "
            + " left join StoreItemDailyStatus sis on  sis.SkuID=ind.sku_id "
            + " where i.customer_id='" + customerId + "' ";

            //string sql = string.Empty;
            var sql = new StringBuilder();

            DataSet dsTemp = this.SQLHelper.ExecuteDataset(countSql);

            if (dsTemp.Tables[0].Rows[0][0].ToString() != "0")
            {
                //房态表中有信息 花间堂定制
                //sql += SearchInoutDetailSqlNew(orderSearchInfo);
                sql.Append(" select * from (");
                sql.AppendFormat("select ROW_NUMBER()over(order by a.Create_Time {0}) _row,a.order_id,a.order_no,a.create_time,a.status_desc,a.VipCardCode,a.total_amount,", sortType);
                sql.AppendFormat("((SELECT sum(sis.LowestPrice) AS priceNew FROM T_Inout i LEFT JOIN T_Inout_Detail ind ON i.order_id=ind.order_id LEFT JOIN T_Sku s ON ind.sku_id=s.sku_id LEFT JOIN StoreItemDailyStatus sis ON sis.SkuID=ind.sku_id WHERE (sis.StatusDate BETWEEN ind.Field1 AND DATEADD(DAY,-1,convert(date,ind.Field2)))  AND i.order_id =a.order_id  AND i.customer_id='{0}')* a.total_qty * tid.discount_rate /100) actual_amount, ", customerId);
                sql.Append(" case a.Field1 when 1 then '已付款' else  '未付款' end PayStatus,");
                sql.Append(" payTypeName = b.Payment_Type_Name,v.vipsourcename,'UnitName'= c.unit_name");
                sql.Append(" from t_inout a left join SysVipSource v on a.data_from_id = v.VipSourceId ");
                sql.Append(" left join T_Payment_Type b on a.pay_id = b.Payment_type_id");
                sql.Append(" and b.isdelete = 0 ");
                sql.Append(" left join t_unit c on a.carrier_id = c.unit_id");
                sql.Append(" left join T_Inout_Detail tid on a.order_id=tid.order_id");
                sql.AppendFormat(" where a.field7 is not null and a.field7<>-99 and a.Vip_no = '{0}' and a.customer_id = '{1}'", vipId, customerId);
                sql.Append(") t");
                sql.AppendFormat(" where t._row>{0}*{1} and t._row<=({0}+1)*{1}", pageIndex - 1, pageSize, sortType);

            }
            else
            {
                sql.Append(" select * from (");
                sql.AppendFormat("select ROW_NUMBER()over(order by a.Create_Time {0}) _row,a.order_id,a.order_no,a.create_time,a.status_desc,a.actual_amount,a.VipCardCode,a.total_amount,", sortType);
                sql.Append(" case a.Field1 when 1 then '已付款' else  '未付款' end PayStatus,");
                sql.Append(" payTypeName = b.Payment_Type_Name,v.vipsourcename,'UnitName'= c.unit_name");
                sql.Append(" from t_inout a left join SysVipSource v on a.data_from_id = v.VipSourceId ");
                sql.Append(" left join T_Payment_Type b on a.pay_id = b.Payment_type_id");
                sql.Append(" and b.isdelete = 0 ");
                sql.Append(" left join t_unit c on a.carrier_id = c.unit_id");
                sql.AppendFormat(" where a.field7 is not null and a.field7<>-99 and a.Vip_no = '{0}' and a.customer_id = '{1}'", vipId, customerId);
                sql.Append(") t");
                sql.AppendFormat(" where t._row>{0}*{1} and t._row<=({0}+1)*{1}", pageIndex - 1, pageSize, sortType);
            }

            #endregion

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        public DataSet GetVipConsumeCardList(string vipId, int pageIndex, int pageSize, string sortType)
        {
            #region comment
            //var sql = new StringBuilder();
            //sql.Append(" select * from (");
            //sql.Append(" select ROW_NUMBER()over(order by a.CreateTime) _row, a.CouponID,");
            //sql.Append(" CouponStatus = case a.status when '1' then '已使用' else '未使用' end,");
            //sql.Append(" CouponDate = convert(nvarchar(10),a.BeginDate,111) + '--' + convert(nvarchar(10),a.EndDate,111),");
            //sql.Append(" c.ParValue ,c.ConditionValue ,");
            //sql.Append(" c.CouponTypeName,CouponName = '满' + CONVERT(nvarchar(20),c.ConditionValue)");
            //sql.Append(" + '抵' + CONVERT(nvarchar(20),c.ParValue) from Coupon a ,VipCouponMapping b,");
            //sql.Append(" CouponType c");
            //sql.Append(" where a.CouponID = b.CouponID ");
            //sql.Append(" and a.CouponTypeID = c.CouponTypeID");
            //sql.Append(" and a.IsDelete = 0 and b.IsDelete = 0 ");
            //sql.Append(" and c.IsDelete = 0 ");
            //sql.AppendFormat(" and b.vipId = '{0}'", vipId);
            //sql.Append(") t");
            //sql.AppendFormat(" where t._row>{0}*{1} and t._row<=({0}+1)*{1}", pageIndex, pageSize);
            #endregion
            if (string.IsNullOrEmpty(sortType))
                sortType = "desc";
            var sql = @"declare @totalPages int
                select @totalPages = count(1)  from
                (
                 select  
                a.CouponID,a.CouponCode,CouponStatus = case a.status when '1' then '已使用' else '未使用' end,
                 c.CouponTypeName,o.OptionText, a.CouponDesc,
                 a.CoupnName,CONVERT(varchar(50),EndDate,23) EndDate 
                 from Coupon a inner join VipCouponMapping b on a.couponid=b.couponid left join CouponType c on 
                 cast(a.CouponTypeID as nvarchar(100))=c.coupontypeid left join options o on a.CollarCardMode=o.OptionValue and o.optionname='CollarCardMode'
                 --where o.optionname='CollarCardMode'
                  where  a.IsDelete = 0 and b.IsDelete = 0  
                 and c.IsDelete = 0  and b.vipId = '{0}'
                ) tmp
                select @totalPages as totalPages
                select * from ( select ROW_NUMBER()over(order by a.CreateTime {3}) _row, 
                a.CouponID,a.CouponCode,CouponStatus = case a.status when '1' then '已使用' else '未使用' end,
                 c.CouponTypeName,o.OptionText, a.CouponDesc,
                 a.CoupnName,CONVERT(varchar(50),EndDate,23) EndDate
                 from Coupon a inner join VipCouponMapping b on a.couponid=b.couponid left join CouponType c on 
                 cast(a.CouponTypeID as nvarchar(100))=c.coupontypeid left join options o on a.CollarCardMode=o.OptionValue and o.optionname='CollarCardMode'
                where  a.IsDelete = 0 and b.IsDelete = 0  
                 and c.IsDelete = 0  and b.vipId = '{0}'
                 ) t 
                where t._row between {1} and {2}";

            return this.SQLHelper.ExecuteDataset(string.Format(sql.ToString(), vipId, (pageIndex - 1) * pageSize, pageIndex * pageSize, sortType));
        }
        public DataSet GetVipOnlineOffline(string vipId, int pageIndex, int pageSize, string sortType)
        {
            if (string.IsNullOrEmpty(sortType))
                sortType = "desc";
            string sql = @"declare @totalCount int
                    select @totalCount = 
                      count(1)  from(
                    select v.vipid,v.vipcode,v.vipname,v.viprealname,g.vipcardgradename,p.endintegral,x.highercount,v.createtime
                    from vip v left join vipintegral p
                    on v.vipid=p.vipid 
                    left join  sysvipcardgrade g
                    on v.viplevel = g.vipcardgradeid
                    left join (select highervipid,higherCount=count(*) from vip where highervipid is not null group by highervipid) x
                    on v.vipid=x.highervipid
                    where v.highervipid = '{0}'
                    ) t
                    select @totalCount
                    select * from (
                    select row_number() over(order by v.createtime {3}) _row, v.vipid,v.vipcode,v.vipname,v.viprealname,g.vipcardgradename,p.endintegral,x.highercount,convert(varchar(10),v.createtime,120) createtime
                    from vip v left join vipintegral p
                    on v.vipid=p.vipid left join  sysvipcardgrade g
                    on v.viplevel = g.vipcardgradeid
                    left join (select highervipid,higherCount=count(*) from vip where highervipid is not null group by highervipid) x
                    on v.vipid=x.highervipid
                    where v.highervipid = '{0}'
                    ) tb where tb._row between {1} and {2}";
            return this.SQLHelper.ExecuteDataset(string.Format(sql, vipId, (pageIndex - 1) * pageSize, pageIndex * pageSize, sortType));

        }
        public DataSet GetVipDetailInfo(string vipId, string customerId)
        {
            var sql = new StringBuilder();

            sql.Append("select a.VipId,a.VipName,a.Phone,a.VipRealName,a.VipCode,a.Integration,c.vipcardgradename,a.CouponInfo,'UnitName'= b.unit_name");
            sql.AppendFormat(" from Vip a left join t_unit b on a.couponinfo = b.unit_id left join sysvipcardgrade c on a.viplevel=c.vipcardgradeid where a.VipId = '{0}';", vipId);
            sql.AppendFormat(@"SELECT DISTINCT b.TagsId,b.TagsName,b.TagsDesc FROM VipTagsMapping a 
                                INNER JOIN dbo.Tags b ON(a.TagsId = b.TagsId) 
                                WHERE a.IsDelete = '0' 
                                AND b.IsDelete = '0'  
                                AND a.VipId = '{0}';", vipId);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }

        public DataSet GetVipAmountList(string vipid, int pageIndex, int pageSize, string sortType)
        {
            if (string.IsNullOrEmpty(sortType))
                sortType = "desc";
            var sql = string.Format(@"select * from ( select ROW_NUMBER()over(order by a.CreateTime {3}) _row,a.createtime,a.VipAmountDetailId,b.optiontext,a.Amount,a.Remark  
                                    from vipamountdetail a left join options b on b.optionvalue=a.amountsourceid where b.optionname='VipAmountFrom' and a.VipId = '{0}' and a.isdelete = 0 ) t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}", vipid, pageIndex - 1, pageSize, sortType);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="columns"></param>
        public void UpdateVipInfo(string vipId, SearchColumn[] columns)
        {
            var sql = new StringBuilder();
            sql.Append("update vip set ");
            var len = columns.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                var c = columns[i];
                if (i < len - 1)
                    sql.Append(",");
                sql.Append(" ").Append(c.ColumnName).Append("=");
                switch (c.ControlType)
                {
                    case 2:
                    case 5:
                        sql.Append(string.IsNullOrEmpty(c.ColumnValue1) ? "NULL" : c.ColumnValue1);
                        break;
                    case 4:
                    case 6:
                    case 7:
                    case 205:
                    case 1:
                    case 3:
                        sql.Append("'").Append(c.ColumnValue1.Replace("'", "''")).Append("'");
                        break;
                }
            }
            sql.AppendFormat(" where vipid='{0}'", vipId);
            SQLHelper.ExecuteNonQuery(sql.ToString());
        }
        /// <summary>
        /// 根据动态配置，添加会员
        /// </summary>
        /// <param name="columns"></param>
        public string InsertVipEntity(SearchColumn[] columns, string clientId, string vipCode)
        {
            var vipId = Guid.NewGuid().ToString();
            var sql = new StringBuilder();
            sql.Append("insert vip(vipid,weixinuserid,clientId,vipCode");
            var sqlValue = new StringBuilder();
            //? 因为 [uq_weixinguserid] 的约束，所以目前写死插入和VIPID一样的值，不然会报错，
            //而且动态属性中不能出现列 weixinuserid
            sqlValue.Append(string.Format("values('{0}','{1}','{2}','{3}'", vipId, vipId, clientId, vipCode));
            foreach (var c in columns)
            {
                /*****
                1 = 文本输入 
                2 = 数值类型 
                3 = 手机号码类型 
                4 = 邮件类型 
                5 = 下拉选择类型 
                6 = 日期类型
	            7= 手机
                205 = 会籍店
                ******/
                sql.Append(",").Append(c.ColumnName);
                switch (c.ControlType)
                {
                    case 2:
                        if (string.IsNullOrEmpty(c.ColumnValue1))
                        {
                            sqlValue.Append(",").Append("NULL");
                            break;
                        }
                        sqlValue.Append(",").Append(c.ColumnValue1);
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(c.ColumnValue1))
                        {
                            //如果状态没有填，默认是正式会员
                            if (c.ColumnName.ToLower() == "status")
                            {
                                sqlValue.Append(",").Append("2");
                                break;
                            }
                            sqlValue.Append(",").Append("NULL");
                            break;
                        }
                        sqlValue.Append(",").Append(c.ColumnValue1);
                        break;
                    case 1:
                    case 3:
                    case 4:
                    case 6:
                    case 7:
                    case 205:
                        if (string.IsNullOrEmpty(c.ColumnValue1))
                        {
                            if (c.ColumnName.ToLower() == "createtime")
                            {
                                sqlValue.Append(",'").Append(DateTime.Now.ToSQLFormatString()).Append("'");
                                break;
                            }
                            sqlValue.Append(",''");
                            break;
                        }
                        sqlValue.Append(",N'").Append(c.ColumnValue1.Replace("'", "''")).Append("'");
                        break;
                }
            }
            //后台添加会员，默认状态为正式会员
            if (columns.Count(e => e.ColumnName.ToLower() == "status") <= 0)
            {
                sql.Append(",").Append("Status");
                sqlValue.Append(",").Append("2");
            }
            //后台添加时，如果没有指定会员来源，默认vip来源为电话客户
            if (columns.Count(e => e.ColumnName.ToLower() == "vipsourceid") <= 0)
            {
                sql.Append(",").Append("VipSourceId");
                sqlValue.Append(",").Append("5");
            }
            sql.Append(") ");
            sqlValue.Append(");");
            SQLHelper.ExecuteNonQuery(sql.ToString() + sqlValue.ToString());
            return vipId;
        }
        /// <summary>
        /// 查询会员列表信息
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少项</param>
        /// <param name="orderBy">排序列名</param>
        /// <param name="isDesc">是否降序</param>
        /// <param name="searchColumns">列信息</param>
        /// <param name="vipSearchTags">标签信息</param>
        /// <returns></returns>
        public DataSet SearchVipList(string customerId, string userId, int pageIndex, int pageSize, string orderBy,
                                        string sortType, SearchColumn[] searchColumns, VipSearchTag[] vipSearchTags)
        {
            string procName = "ProcSearchVipAttr";
            var parameters = new List<SqlParameter>();
            string conditionSql = GetVipSearchConditionSql(searchColumns, vipSearchTags);
            parameters.Add(new SqlParameter("@CustomerId", customerId));
            parameters.Add(new SqlParameter("@UserId", userId));
            parameters.Add(new SqlParameter("@PageIndex", pageIndex));
            parameters.Add(new SqlParameter("@PageSize", pageSize));
            parameters.Add(new SqlParameter("@SortItem", orderBy));
            parameters.Add(new SqlParameter("@SortType", sortType));
            parameters.Add(new SqlParameter("@SearchConditional", conditionSql));
            var result = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, procName, parameters.ToArray());
            return result;
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="vipIds">会员id列表</param>
        public void DeleteVips(string[] vipIds, string userId)
        {
            var sql = @"update vip set isdelete=1, LastUpdateBy='{1}' where vipid in ({0})";
            SQLHelper.ExecuteNonQuery(string.Format(sql, vipIds.ToSqlInString(), userId));
        }

        private string GetVipTagLogicOperator(string tagOperator)
        {
            var r = "";
            switch (tagOperator.ToLower())
            {
                case "=":
                case "include":
                    r = "=";
                    break;
                case "!=":
                case "<>":
                case "exclude":
                    r = "<>";
                    break;
                default:
                    break;
            }
            if (r == "")
                throw new Exception("操作符不能为空.");
            return r;
        }
        /// <summary>
        /// 根据列和标签获取sql查询条件语句
        /// </summary>
        /// <param name="searchColumns">列信息</param>
        /// <param name="vipSearchTags">标签信息</param>
        /// <returns></returns>
        private string GetVipSearchConditionSql(SearchColumn[] searchColumns, VipSearchTag[] vipSearchTags)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            if (searchColumns.Length > 0)
            {
                foreach (var sc in searchColumns)
                {
                    string lVal = string.IsNullOrEmpty(sc.ColumnValue1) ? "" : sc.ColumnValue1.Trim();
                    string rVal = string.IsNullOrEmpty(sc.ColumnValue2) ? "" : sc.ColumnValue2.Trim();
                    switch (sc.ControlType)
                    {
                        //1:文本类型，3：手机号码类型，4：邮件类型，7：手机
                        case 1:
                        case 3:
                        case 4:
                        case 7:
                            if (string.IsNullOrEmpty(lVal))
                                break;
                            sb.Append(" and ").Append(sc.ColumnName).Append(" like '%").Append(lVal.Replace("'", "''"))
                                .Append("%'");
                            break;
                        //6:日期类型
                        case 6:
                            if (string.IsNullOrEmpty(lVal) && string.IsNullOrEmpty(rVal))
                                break;
                            if (string.IsNullOrEmpty(lVal))
                            {
                                sb.Append(" and ").Append(sc.ColumnName).Append(" <= '")
                                    .Append(rVal).Append("'");
                                break;
                            }
                            if (string.IsNullOrEmpty(rVal))
                            {
                                sb.Append(" and ").Append(sc.ColumnName).Append(" >= '").Append(lVal).Append("'");
                                break;
                            }
                            sb.Append(" and ").Append(sc.ColumnName).Append(" between '").Append(lVal)
                                .Append("' and '").Append(sc.ColumnValue2).Append("'");
                            break;
                        //2：数值类型
                        case 2:
                            lVal = lVal.Replace("'", "");
                            rVal = rVal.Replace("'", "");
                            if (string.IsNullOrEmpty(lVal) && string.IsNullOrEmpty(rVal))
                                break;
                            if (string.IsNullOrEmpty(lVal))
                            {
                                sb.Append(" and ").Append(sc.ColumnName).Append(" <= ")
                                    .Append(rVal);
                                break;
                            }
                            if (string.IsNullOrEmpty(rVal))
                            {
                                sb.Append(" and ").Append(sc.ColumnName).Append(" >= ").Append(lVal);
                                break;
                            }
                            sb.Append(" and ").Append(sc.ColumnName).Append(" between ").Append(lVal)
                                .Append(" and ").Append(rVal);
                            break;
                        //5：下拉选择类型
                        case 5:
                            lVal = lVal.Replace("'", "");
                            rVal = rVal.Replace("'", "");
                            if (string.IsNullOrEmpty(lVal))
                                break;
                            //对会员来源特殊处理，来源为NULL认定为电话客服来源：5
                            if (sc.ColumnName.ToLower() == "vipsourceid" && lVal == "5")
                            {
                                sb.Append(" and (VIPSOURCEID = 5 OR VIPSOURCEID IS NULL)");
                                break;
                            }
                            sb.Append(" and ").Append(sc.ColumnName).Append("=").Append(lVal);
                            break;
                        //205：会籍店树状类型
                        case 205:
                            if (string.IsNullOrEmpty(lVal))
                                break;
                            sb.Append(" and ").Append(sc.ColumnName).Append("='").Append(lVal).Append("'");
                            break;
                    }
                }
            }
            if (vipSearchTags.Length > 0)
            {
                sb.Append(" and VIPID IN (select vipid from VIPTagsMapping where 1=1 and (");
                for (int i = 0; i < vipSearchTags.Length; i++)
                {
                    var tag = vipSearchTags[i];
                    sb.Append(tag.LeftBracket).Append("VipTagsMapping.TagsId")
                        .Append(GetVipTagLogicOperator(tag.EqualFlag)).Append("'").Append(tag.TagId).Append("'")
                        .Append(tag.RightBracket);
                    if (i != vipSearchTags.Length - 1)
                        sb.Append(" ").Append(tag.AndOrStr).Append(" ");
                }
                sb.Append(") )");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取客户查询会员列表动态配置显示的列信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string[] GetVipListColomns(string customerId)
        {
            string sql = "select dbo.FnGetCustomerTableColumn('{0}','vip')";
            var result = this.SQLHelper.ExecuteDataset(CommandType.Text, string.Format(sql, customerId));
            if (result.Tables[0].Rows.Count > 0)
                return result.Tables[0].Rows[0][0].ToString().Split(new char[] { ',' });
            return null;
        }
        /// <summary>
        /// 根据实体条件查询实体,不过滤IsDelete字段
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public VipEntity[] QueryByEntityAbsolute(VipEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return this.QueryAbsolute(queryWhereCondition, pOrderBys);
        }
        /// <summary>
        /// 执行查询,不过滤IsDelete字段
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public VipEntity[] QueryAbsolute(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Vip] where 1=1 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            List<VipEntity> list = new List<VipEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }

        #region 根据用户ID获取用户折扣信息 2014-11-5

        public DataSet GetVipSale(string vipID)
        {
            var sql = "select v.Col13,vg.SalesPreferentiaAmount,v.VipName,v.VIPID from Vip v left join SysVipCardGrade vg on v.Col13=vg.VipCardGradeName where v.IsDelete=0 and v.VIPID='" + vipID + "'";
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }

        #endregion


        public int GetInviteCount(string userID)
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.Vip WHERE SetoffUserId='{0}'", userID);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 会员卡信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="idnumber"></param>
        /// <param name="vipcardcode"></param>
        /// <returns></returns>
        public DataSet GetVipCardInfo(string phone, string idnumber, string vipcardcode)
        {
            string sql = @"SELECT vc.VipCardCode cardno, v.VipName vipname, svct.VipCardTypeCode viplevel, vc.VipCardStatusId status
                        FROM dbo.Vip v INNER JOIN dbo.VipCardVipMapping vcvm ON v.VIPID = vcvm.VIPID 
                        INNER JOIN dbo.VipCard vc ON vcvm.VipCardID = vc.VipCardID
                        LEFT JOIN dbo.SysVipCardType svct ON vc.VipCardTypeID = svct.VipCardTypeID
                        WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(phone))
                sql += " AND (v.Phone = '" + phone + "') ";
            if (!string.IsNullOrWhiteSpace(idnumber))
                sql += " AND (v.IDNumber = '" + idnumber + "') ";
            if (!string.IsNullOrWhiteSpace(vipcardcode))
                sql += " AND (vc.VipCardCode = '" + vipcardcode + "') ";
            sql += " AND (v.ClientID = '" + this.CurrentUserInfo.ClientID + "') ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 会员卡详情
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="idnumber"></param>
        /// <param name="vipcardcode"></param>
        /// <returns></returns>
        public DataSet GetVipCardDetail(string phone, string idnumber, string vipcardcode)
        {
            string sql = @"SELECT vc.VipCardCode cardno, v.VipName vipname, svct.VipCardTypeCode viplevel, vc.VipCardStatusId status,
                        v.birthday, v.gender, v.idnumber, v.phone, v.DeliveryAddress address, v.email
                        FROM dbo.Vip v INNER JOIN dbo.VipCardVipMapping vcvm ON v.VIPID = vcvm.VIPID 
                        INNER JOIN dbo.VipCard vc ON vcvm.VipCardID = vc.VipCardID
                        LEFT JOIN dbo.SysVipCardType svct ON vc.VipCardTypeID = svct.VipCardTypeID
                        WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(phone))
                sql += " AND (v.Phone = '" + phone + "') ";
            if (!string.IsNullOrWhiteSpace(idnumber))
                sql += " AND (v.IDNumber = '" + idnumber + "') ";
            if (!string.IsNullOrWhiteSpace(vipcardcode))
                sql += " AND (vc.VipCardCode = '" + vipcardcode + "') ";
            sql += " AND (v.ClientID = '" + this.CurrentUserInfo.ClientID + "') ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #region 导入Vip信息

        /// 导入用户临时表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="column_count"></param>
        /// <param name="conn"></param>
        public void insertToSql(DataRow dr, int column_count, SqlConnection conn, string strCustomerId, string strCreateUserId)
        {

            string sql = "insert into [ImportUserTemp] values";
            sql += "('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "',";
            sql += "'" + dr[5].ToString() + "','" + dr[6].ToString() + "',";
            sql += "'" + strCustomerId + "','" + strCreateUserId + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 调用sp将临时表中的用户信息导入正式表T_User,并返回未导入的信息
        /// </summary>
        /// <returns></returns>
        public DataSet ExcelImportToDB()
        {
            string sql = "Proc_ExcelImportToUser";
            var ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql);
            return ds;
        }
        #endregion
        #region 获取云店会员卡包
        public DataSet GetCardBag(string weixinUserId, string cloudCustomerId)
        {
            string sql = string.Format(@"
                        SELECT v.ClientID CustomerID,v.VIPID userId,c.customer_name CustomerName,s.SettingValue ImageUrl FROM dbo.Vip v
                        LEFT JOIN cpos_ap..t_customer c ON c.customer_id=v.ClientID 
                        LEFT JOIN dbo.CustomerBasicSetting s ON v.ClientID=s.CustomerID AND s.SettingCode='AppLogo'
                        WHERE v.WeiXinUserId='{0}' and v.ClientID!='{1}'
                    ", weixinUserId, cloudCustomerId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        /// <summary>
        /// 获取会员列表-新版
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pCurrentPageIndex"></param>
        /// <returns></returns>
        public PagedQueryResult<VipEntity> GetVipList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder hqUnitSql = new StringBuilder();
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            hqUnitSql.AppendFormat(@" --查询总部门店ID
                                    DECLARE @HQUnitID VARCHAR(50)
                                    SELECT TOP 1
                                            @HQUnitID = unit_id
                                    FROM    t_unit
                                    WHERE   customer_id = '{0}'
                                            AND STATUS = 1
                                            AND TYPE_ID = ( SELECT  type_id
                                                            FROM    t_type
                                                            WHERE   customer_Id = '{0}'
                                                                    AND type_code = '总部'); ", CurrentUserInfo.ClientID);
            pagedSql.Append(hqUnitSql);
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [VIPID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(" ) as ___rn,v.VIPID ,v.VipCode,v.VipName ,v.VipRealName ,v.Gender ,v.Phone ,v.CreateTime,u.unit_name ,ct.VipCardTypeName ,vc.VipCardID,vc.VipCardCode,vc.VipCardStatusId ");
            pagedSql.AppendFormat(" from [Vip] v ");
            pagedSql.AppendFormat(@" INNER JOIN vw_unit_level ul ON --会籍店为空时，只有总部用户可以查询
						    ( CASE WHEN ISNULL(v.CouponInfo, '') = '' THEN @HQUnitID
                            ELSE v.CouponInfo END ) = ul.unit_id AND ul.customer_id = '{0}' ", CurrentUserInfo.ClientID);
            pagedSql.AppendFormat(" LEFT JOIN VipCardVipMapping AS m ON m.VipID = v.VipID AND m.IsDelete = 0 ");
            pagedSql.AppendFormat(" LEFT JOIN VipCard AS vc ON vc.VipCardID = m.VipcardID AND vc.IsDelete=0 ");
            pagedSql.AppendFormat(" LEFT JOIN SysVipCardType AS ct ON vc.VipCardTypeID = ct.VipCardTypeID AND ct.IsDelete = 0 ");
            pagedSql.AppendFormat(" LEFT JOIN T_Unit u ON v.couponInfo=u.unit_id ");
            pagedSql.AppendFormat(" where 1=1  and v.IsDelete=0 ");

            //总记录数SQL
            totalCountSql.Append(hqUnitSql);
            totalCountSql.AppendFormat("select count(1) from [Vip] v");
            totalCountSql.AppendFormat(@" INNER JOIN vw_unit_level ul ON --会籍店为空时，只有总部用户可以查询
						    ( CASE WHEN ISNULL(v.CouponInfo, '') = '' THEN @HQUnitID
                            ELSE v.CouponInfo END ) = ul.unit_id AND ul.customer_id = '{0}' ", CurrentUserInfo.ClientID);
            totalCountSql.AppendFormat(" LEFT JOIN VipCardVipMapping AS m ON m.VipID = v.VipID AND m.IsDelete = 0 ");
            totalCountSql.AppendFormat(" LEFT JOIN VipCard AS vc ON vc.VipCardID = m.VipcardID AND vc.IsDelete=0 ");
            totalCountSql.AppendFormat(" LEFT JOIN SysVipCardType AS ct ON vc.VipCardTypeID = ct.VipCardTypeID AND ct.IsDelete = 0 ");
            totalCountSql.AppendFormat(" LEFT JOIN T_Unit u ON v.couponInfo=u.unit_id ");
            totalCountSql.AppendFormat(" where 1=1  and v.IsDelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<VipEntity> result = new PagedQueryResult<VipEntity>();
            List<VipEntity> list = new List<VipEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipEntity m = new VipEntity() { };

                    if (rdr["VIPID"] != DBNull.Value)
                    {
                        m.VIPID = rdr["VIPID"].ToString();
                    }
                    if (rdr["VipCode"] != DBNull.Value)
                    {
                        m.VipCode = rdr["VipCode"].ToString();
                    }
                    if (rdr["Gender"] != DBNull.Value)
                    {
                        m.Gender = int.Parse(rdr["Gender"].ToString());
                    }
                    if (rdr["VipName"] != DBNull.Value)
                    {
                        m.VipName = rdr["VipName"].ToString();
                    }
                    if (rdr["VipRealName"] != DBNull.Value)
                    {
                        m.VipRealName = rdr["VipRealName"].ToString();
                    }
                    if (rdr["Phone"] != DBNull.Value)
                    {
                        m.Phone = rdr["Phone"].ToString();
                    }
                    if (rdr["CreateTime"] != DBNull.Value)
                    {
                        m.CreateTime =Convert.ToDateTime(rdr["CreateTime"].ToString());
                    }
                    if (rdr["unit_name"] != DBNull.Value)
                    {
                        m.UnitName = rdr["unit_name"].ToString();
                    }
                    if (rdr["VipCardID"] != DBNull.Value)
                    {
                        m.VipCardID = rdr["VipCardID"].ToString();
                    }
                    if (rdr["VipCardCode"] != DBNull.Value)
                    {
                        m.VipCardCode = rdr["VipCardCode"].ToString();
                    }
                    if (rdr["VipCardTypeName"] != DBNull.Value)
                    {
                        m.VipCardTypeName = rdr["VipCardTypeName"].ToString();
                    }
                    if (rdr["VipCardStatusId"] != DBNull.Value)
                    {
                        m.VipCardStatusId = int.Parse(rdr["VipCardStatusId"].ToString());
                    }
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }
    }

}
