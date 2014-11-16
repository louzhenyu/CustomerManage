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

    }
}
