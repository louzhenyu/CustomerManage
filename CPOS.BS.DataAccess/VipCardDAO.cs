/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
    /// 表VipCard的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardDAO : Base.BaseCPOSDAO, ICRUDable<VipCardEntity>, IQueryable<VipCardEntity>
    {

        public VipCardEntity GetByID(object pID,string CustomerID)
        {
            //参数检查
            if (pID == null)
                return null;
            if (string.IsNullOrWhiteSpace(CustomerID))
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCard] where VipCardID='{0}' and CustomerID='{1}'  and isdelete=0 ", id.ToString(),CustomerID);
            //读取数据
            VipCardEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        #region 查询会员卡信息
        public DataSet SearchTopVipCard(VipCardEntity entity)
        {
            string sql = "select top 1 a.*, c.vipId, e.UnitID, f.unit_name UnitName, g.VipCardGradeName ";
            sql += " ,h.VipCardStatusName VipStatusName, h.VipCardStatusCode ";
            sql += " from VipCard a ";
            sql += " left join VipCardVipMapping b on (b.VipCardID=a.VipCardID and b.isDelete='0') ";
            sql += " left join Vip c on (c.VipID=b.VipID and c.isDelete='0') ";
            sql += " left join VipExpand d on (d.VipID=c.VipID and d.isDelete='0') ";
            sql += " left join VipCardUnitMapping e on (e.VipCardID=a.VipCardID and e.isDelete='0') ";
            sql += " left join t_unit f on (f.Unit_Id=e.UnitId and f.status='1') ";
            sql += " left join SysVipCardGrade g on (g.VipCardGradeID=a.VipCardGradeID and g.isDelete='0') ";
            sql += " left join SysVipCardStatus h on (h.VipCardStatusId=a.VipCardStatusId and h.isDelete='0') ";
            sql += " where a.isDelete='0' ";
            if (entity.VipCardID != null && entity.VipCardID.Trim().Length > 0)
            {
                sql += " and a.VipCardID = '" + entity.VipCardID.Trim() + "' ";
            }
            if (entity.VipCardCode != null && entity.VipCardCode.Trim().Length > 0)
            {
                if (entity.VipCardCode.Trim().Length <= 6)
                {
                    sql += " and substring(a.VipCardCode,LEN(a.VipCardCode)-5,6)='" + entity.VipCardCode.Trim() + "' ";
                }
                else
                {
                    sql += " and a.VipCardCode like '%" + entity.VipCardCode.Trim() + "%' ";
                }
            }
            if (entity.VipName != null && entity.VipName.Trim().Length > 0)
            {
                sql += " and c.VipName like '%" + entity.VipName.Trim() + "%' ";
            }
            if (entity.CarCode != null && entity.CarCode.Trim().Length > 0)
            {
                sql += " and d.LicensePlateNo like '%" + entity.CarCode.Trim() + "%' ";
            }
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 查询会员卡信息
        public DataSet SearchVipCardByVip(string vipid)
        {
            string sql = "select top 1 a.*, c.vipId";
            //sql += " ,h.VipCardStatusName VipStatusName, h.VipCardStatusCode ";
            sql += " from VipCard a ";
            sql += " left join VipCardVipMapping b on (b.VipCardID=a.VipCardID and b.isDelete='0') ";
            sql += " left join Vip c on (c.VipID=b.VipID and c.isDelete='0') ";
            //sql += " left join VipExpand d on (d.VipID=c.VipID and d.isDelete='0') ";
            //sql += " left join VipCardUnitMapping e on (e.VipCardID=a.VipCardID and e.isDelete='0') ";
            //sql += " left join t_unit f on (f.Unit_Id=e.UnitId and f.status='1') ";
            //sql += " left join SysVipCardGrade g on (g.VipCardGradeID=a.VipCardGradeID and g.isDelete='0') ";
            //sql += " left join SysVipCardStatus h on (h.VipCardStatusId=a.VipCardStatusId and h.isDelete='0') ";
            sql += " where a.isDelete='0' ";
            if (!string.IsNullOrEmpty(vipid))
            {
                sql += " and c.VIPID = '" + vipid + "' ";
            }
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 会员卡列表查询
        public int SearchVipCardCount(VipCardEntity searchInfo)
        {
            string sql = SearchVipCardSql(searchInfo);
            sql += " select count(*) From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet SearchVipCardList(VipCardEntity searchInfo)
        {
            int beginSize = searchInfo.startRowIndex - 1;
            int endSize = searchInfo.startRowIndex * searchInfo.maxRowCount + searchInfo.maxRowCount;

            string sql = SearchVipCardSql(searchInfo);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("GetEventListSql:{0}", sql)
            });
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);

            return ds;
        }

        private string SearchVipCardSql(VipCardEntity searchInfo)
        {
            string sql = "SELECT a.* "
                    + " ,b.VIPID "
                    + " ,c.VipName "
                    + " ,(SELECT x.VipCardTypeName FROM SysVipCardType x WHERE x.VipCardTypeID = a.VipCardTypeID AND x.IsDelete=0) VipCardTypeName "
                    + " ,(SELECT x.VipCardStatusName FROM SysVipCardStatus x WHERE x.VipCardStatusId = a.VipCardStatusId AND x.IsDelete=0 ) VipStatusName "
                    + " ,(SELECT x.VipCardGradeName FROM SysVipCardGrade x WHERE x.VipCardGradeID = a.VipCardGradeID AND x.IsDelete=0 ) VipCardGradeName "
                    + " ,(SELECT y.unit_name FROM dbo.VipCardUnitMapping x INNER JOIN t_unit y ON(x.UnitID=y.unit_id) WHERE x.VipCardID = a.vipcardid AND x.IsDelete=0) UnitName "
                    + " ,(SELECT y.unit_id FROM dbo.VipCardUnitMapping x INNER JOIN t_unit y ON(x.UnitID=y.unit_id) WHERE x.VipCardID = a.vipcardid AND x.IsDelete=0) UnitID "
                    + " ,DisplayIndex = row_number() over(order by a.VipCardCode desc) "
                    + " into #tmp"
                    + " FROM dbo.VipCard a "
                    + " INNER JOIN dbo.VipCardVipMapping b ON(a.VipCardID = b.VipCardID) "
                    + " INNER JOIN dbo.Vip c ON(b.VIPID = c.VIPID) "
                    + " WHERE 1 = 1 AND a.IsDelete = 0 ";

            if (searchInfo.VipCardCode != null && !searchInfo.VipCardCode.Equals(""))
            {
                sql += " and a.vipcardcode like '%" + searchInfo.VipCardCode + "%'";
            }
            if (searchInfo.VipName != null && !searchInfo.VipName.Equals(""))
            {
                sql += " and c.VipName like '%" + searchInfo.VipName + "%'";
            }
            if (searchInfo.VipCardGradeID != null && !searchInfo.VipCardGradeID.Equals(""))
            {
                sql += " and a.VipCardGradeID = '" + searchInfo.VipCardGradeID + "'";
            }
            if (searchInfo.VipCardStatusId != null && !searchInfo.VipCardStatusId.Equals(""))
            {
                sql += " and a.VipCardStatusId = '" + searchInfo.VipCardStatusId + "'";
            }
            if (searchInfo.VipId != null && !searchInfo.VipId.Equals(""))
            {
                sql += " and b.VIPID = '" + searchInfo.VipId + "'";
            }
            if (searchInfo.VipCardID != null && !searchInfo.VipCardID.Equals(""))
            {
                sql += " and a.VipCardID = '" + searchInfo.VipCardID + "'";
            }

            return sql;
        }
        #endregion

        #region 会员查询
        /// <summary>
        /// 获取查询会员的数量
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public int GetVipListCount(VipSearchEntity vipSearchInfo)
        {
            string sql = GetVipListSql(vipSearchInfo);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取查询会员的信息
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public DataSet GetVipList(VipSearchEntity vipSearchInfo)
        {
            int beginSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize;
            int endSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize + vipSearchInfo.PageSize;

            string sql = GetVipListSql(vipSearchInfo);
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetVipListSql(VipSearchEntity vipSearchInfo)
        {
            PublicService pService = new PublicService();
            string sql = string.Empty;

            sql += " select a.*, DisplayIndex=row_number() over(order by a.VipName desc ) into #tmp from ( ";
            sql += "SELECT a.* "
                + " ,(SELECT x.VipSourceName FROM dbo.SysVipSource x WHERE x.VipSourceID = a.VipSourceId) VipSourceName "
                + " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc "
                + " ,'' LastUnit "
                + " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc "
                + " ,(select sum(Integral) from VipIntegralDetail where IsDelete='0' "
                + "   and fromVipId=a.vipId and vipId='" + vipSearchInfo.HigherVipId + "') IntegralForHightUser "
                + " ,CASE WHEN a.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                + " from vip a  "
                + " WHERE a.IsDelete = '0') a where 1=1 ";

            if (vipSearchInfo.VipInfo != null && !vipSearchInfo.VipInfo.Equals(""))
            {
                sql += " and (a.VipCode like '%" + vipSearchInfo.VipInfo + "%'  or a.VipName like '%" + vipSearchInfo.VipInfo + "%' ) ";
            }
            sql = pService.GetLinkSql(sql, "a.Phone", vipSearchInfo.Phone, "%");

            return sql;
        }
        #endregion

        #region 根据会员卡ID删除会员卡与门店关系表

        /// <summary>
        /// 根据会员卡ID删除会员卡与门店关系表
        /// </summary>
        /// <param name="vipCardID">会员卡ID</param>
        public void DeteleVipCardUnitMapping(string vipCardID)
        {
            string sql = "update [VipCardUnitMapping] set LastUpdateTime='" + DateTime.Now
                + "',LastUpdateBy='" + CurrentUserInfo.CurrentUser.User_Id + "',IsDelete=1 where VipCardID='" + vipCardID + "'";

            this.SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion

        #region 根据会员卡ID删除会员卡与VIP关系

        /// <summary>
        /// 根据会员卡ID删除会员卡与VIP关系
        /// </summary>
        /// <param name="vipCardID">会员卡ID</param>
        public void DeteleVipCardVipMapping(string vipCardID)
        {
            string sql = "update [VipCardVipMapping] set LastUpdateTime='" + DateTime.Now
                + "',LastUpdateBy='" + CurrentUserInfo.CurrentUser.User_Id + "',IsDelete=1 where VipCardID='" + vipCardID + "'";

            this.SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion

        #region 更新会员卡与门店关系表

        /// <summary>
        /// 新会员卡与门店关系表
        /// </summary>
        /// <param name="vipCardID">会员卡ID</param>
        /// <param name="unitID">门店ID</param>
        public void UpdateVipCardUnitMapping(string vipCardID, string unitID)
        {
            string sql = "update [VipCardUnitMapping] set LastUpdateTime='" + DateTime.Now
                + "' ,LastUpdateBy='" + CurrentUserInfo.CurrentUser.User_Id + "',IsDelete=0 "
                + " where VipCardID='" + vipCardID + "' and UnitID = '" + unitID + "' ";

            this.SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion

        public DataSet GetVipCardTypeList(string customerId)
        {
            var sql = string.Format(@"select a.* from SysVipCardType a
                                 WHERE 1 = 1 and   a.isdelete = 0   
                and a.CustomerID='{0}'       
                             ", customerId);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        /// <summary>
        /// 导出卡号
        /// </summary>
        /// <param name="p_BatchNo"></param>
        /// <returns></returns>
        public DataSet ExportVipCardCode(string p_BatchNo)
        {
            string sql = string.Format(@"select VipCardCode from VipCard WHERE isdelete = 0 and BatchNo={0}", p_BatchNo);
            return this.SQLHelper.ExecuteDataset(sql);
        }


        #region 会员卡
        /// <summary>
        /// 会员卡列表查询
        /// </summary>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序数组</param>
        /// <param name="pPageSize">叶记录数</param>
        /// <param name="pCurrentPageIndex">叶索引</param>
        /// <returns>集合</returns>
        public PagedQueryResult<VipCardEntity> GetVipCardList(string VIPID, string Phone, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {

            #region 组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            pagedSql.AppendFormat("select top {0} * from ( ", pPageSize);
            pagedSql.Append("select ROW_NUMBER() OVER (ORDER BY  ");
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
                pagedSql.AppendFormat(" a.VipCardID desc"); //默认为主键值倒序
            }
            pagedSql.Append(") AS RowNumber,a.*,e.VipCardTypeName,g.VipName,g.VipRealName,g.Gender,g.Phone,u.unit_name,");
            pagedSql.Append("e.picUrl,g.VIPID,g.VipCode from VipCard as a ");
            pagedSql.Append("left join VipCardVipMapping as b on a.VipCardID=b.VipCardID and b.IsDelete=0 ");
            pagedSql.Append("left join Vip as g on b.VIPID=g.VIPID and g.IsDelete=0 ");
            //pagedSql.Append("left join SysVipCardStatus as c on a.VipCardStatusId=c.VipCardStatusId and c.IsDelete=0 ");
            pagedSql.Append("left join SysVipCardType as e on a.VipCardTypeID=e.VipCardTypeID and e.IsDelete=0 ");
            pagedSql.Append("left join t_unit as u on a.MembershipUnit =u.unit_id ");
            pagedSql.Append("where a.IsDelete =0 and ISNULL(a.MembershipUnit,'')<>'' ");
            if (!string.IsNullOrWhiteSpace(Phone))
            {
                pagedSql.AppendFormat("and g.Phone like '{0}%'", Phone);
            }
            if (!string.IsNullOrWhiteSpace(VIPID))
            {
                pagedSql.AppendFormat("and g.VipID = '{0}'", VIPID);
            }
            #region 记录总数sql
            totalCountSql.Append("select count(1) from VipCard as a ");
            totalCountSql.Append("left join VipCardVipMapping as b on a.VipCardID=b.VipCardID and b.IsDelete=0 left join Vip as g on b.VIPID=g.VIPID and g.IsDelete=0 ");

            //totalCountSql.Append("left join SysVipCardStatus as c on a.VipCardStatusId=c.VipCardStatusId and c.IsDelete=0 left join SysVipCardType as e on a.VipCardTypeID=e.VipCardTypeID and e.IsDelete=0 ");
            totalCountSql.Append("left join t_unit as u on a.MembershipUnit =u.unit_id ");
            totalCountSql.Append("where a.IsDelete =0 and ISNULL(a.MembershipUnit,'')<>'' ");
            if (!string.IsNullOrWhiteSpace(Phone))
            {
                totalCountSql.AppendFormat("and g.Phone like '{0}%'", Phone);
            }
            if (!string.IsNullOrWhiteSpace(VIPID))
            {
                pagedSql.AppendFormat("and g.VipID = '{0}'", VIPID);
            }
            #endregion
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
            pagedSql.AppendFormat(") as h where RowNumber >{0}*({1}-1) ", pPageSize, pCurrentPageIndex);
            #endregion


            #region 执行,转换实体,分页属性赋值
            PagedQueryResult<VipCardEntity> result = new PagedQueryResult<VipCardEntity>();
            List<VipCardEntity> list = new List<VipCardEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardEntity m;
                    this.Load(rdr, out m);
                    if (rdr["VipCardTypeName"] != DBNull.Value)
                    {
                        m.VipCardTypeName = rdr["VipCardTypeName"].ToString();
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
                    if (rdr["unit_name"] != DBNull.Value)
                    {
                        m.UnitName = rdr["unit_name"].ToString();
                    }
                    if (rdr["picUrl"] != DBNull.Value)
                    {
                        m.picUrl = rdr["picUrl"].ToString();
                    }
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
            #endregion


            return result;
        }

        /// <summary>
        /// 根据卡号码或者内码获取会员卡信息
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardEntity GetByCodeOrISN(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCard] where VipCardID='{0}' or VipCardISN='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipCardEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }
        #endregion

        #region 报表
        /// <summary>
        /// 日结售卡统计
        /// </summary>
        /// <param name="EndDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetDayVendingCount(string StareDate, string EndDate)
        {
            var parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@StareDate", System.Data.SqlDbType.NVarChar) { Value = StareDate };
            parm[1] = new SqlParameter("@EndDate", System.Data.SqlDbType.NVarChar) { Value = EndDate };

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Report_DayVendingCount", parm);

        }
        /// <summary>
        /// 日结对账统计
        /// </summary>
        /// <param name="StareDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public DataSet GetDayReconciliation(DateTime StareDate, DateTime EndDate,int Days,string UnitID, string CustomerID)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@StareDate", System.Data.SqlDbType.DateTime) { Value = StareDate };
            parm[1] = new SqlParameter("@EndDate", System.Data.SqlDbType.DateTime) { Value = EndDate };
            parm[2] = new SqlParameter("@Days", System.Data.SqlDbType.Int) { Value = Days };
            parm[3] = new SqlParameter("@UnitID", System.Data.SqlDbType.NVarChar) { Value = UnitID };
            parm[4] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CustomerID };
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Report_DayReconciliation", parm);

        }
        #endregion

        public VipCardEntity GetVipCardByVipMapping(string vipId)
        {
            string sql = @"select top 1 b.* from vipcardvipmapping a inner join vipcard b on a.vipcardid = b.vipcardid where b.VipCardStatusId = 1 and a.vipid = '" + vipId + "'";

            VipCardEntity vipCardEntity = new VipCardEntity();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr,out vipCardEntity);
                    break;
                }
            }
            return vipCardEntity;
        }
    }
}
