/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/15 13:37:22
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
    /// 表LNewsMicroMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LNewsMicroMappingDAO : Base.BaseCPOSDAO, ICRUDable<LNewsMicroMappingEntity>, IQueryable<LNewsMicroMappingEntity>
    {
        #region 获取微刊资讯关联列表
        /// <summary>
        /// 获取微刊资讯关联列表
        /// </summary>
        /// <param name="microNumberId">刊号ID</param>
        /// <param name="microTypeId">类别ID</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortOrder">排序方式：0 升序 1 降序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public DataSet GetNewsMappList(string microNumberId, string microTypeId, string sortField, int sortOrder, int pageIndex, int pageSize)
        {
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            //Instance Obj
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select MappingId,NewsId,PublishTime,NewsTitle,MicroTypeName,BrowseCount,PraiseCount,CollCount,Sequence from (");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by N.{0} {1}) rowNum,MappingId,N.NewsId,PublishTime,NewsTitle,MicroTypeName,BrowseCount,PraiseCount,CollCount,NMM.Sequence from LNews N ", sortField, sort);
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and NMM.NewsId=N.NewsId ");
            sbSQL.Append("inner join EclubMicroType NT on NT.IsDelete=0 and NT.CustomerId=NMM.CustomerId and NT.MicroTypeID=NMM.MicroTypeId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and NT.CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and NMM.MicroNumberId='{0}' and NMM.MicroTypeId='{1}'", microNumberId, microTypeId);
            sbSQL.Append(") as Res ");
            sbSQL.AppendFormat("where rowNum between {0} and {1} ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.Append("select COUNT(N.NewsId) from LNews N ");
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and NMM.NewsId=N.NewsId ");
            sbSQL.Append("inner join EclubMicroType NT on NT.IsDelete=0 and NT.CustomerId=NMM.CustomerId and NT.MicroTypeID=NMM.MicroTypeId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and NT.CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and NMM.MicroNumberId='{0}' and NMM.MicroTypeId='{1}' ;", microNumberId, microTypeId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        #endregion

        #region 设置资讯微刊关联列表
        /// <summary>
        /// 设置资讯微刊关联列表
        /// </summary>
        /// <param name="numberId">刊号ID</param>
        /// <param name="typeId">类别ID</param>
        /// <param name="newsIds">资讯ID</param>
        /// <returns>受影响的行数</returns>
        public int SetNewsMap(string numberId, string typeId, string[] newsIds)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            if (newsIds == null || newsIds.Length <= 0)
            {
                return 0;
            }
            //批量执行
            foreach (var newsId in newsIds)
            {
                sbSQL.AppendFormat("IF NOT EXISTS(SELECT TOP 1 1 FROM LNewsMicroMapping where IsDelete=0 and CustomerId='{0}' and MicroNumberId='{1}' and MicroTypeId='{2}' and NewsId='{3}') ", CurrentUserInfo.ClientID, numberId, typeId, newsId);
                sbSQL.Append("INSERT INTO LNewsMicroMapping(NewsId,MicroNumberId,MicroTypeId,CustomerId,CreateBy,LastUpdateBy) ");
                sbSQL.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{4}');", newsId, numberId, typeId, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
            }
            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }

        /// <summary>
        /// 设置旧的新闻关联数据
        /// 老的关联直接删除
        /// </summary>
        public int SetOldNewsMap(string newsId, string oldNumberId, string oldTypeId, SqlTransaction trans)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();

            //批量执行
            sbSQL.AppendFormat("delete from LNewsMicroMapping where NewsId = '{0}' and MicroNumberId = '{1}' and MicroTypeId = '{2}'", newsId, oldNumberId, oldTypeId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL.ToString());
        }

        /// <summary>
        /// 设置资讯微刊关联列表：判存【没有直接插入】
        /// </summary>
        /// <param name="microMapEn">数据实体</param>
        /// <param name="trans">事务对象</param>
        /// <returns>受影响的行数</returns>
        public int SetNewsMap(LNewsMicroMappingEntity microMapEn, SqlTransaction trans)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            if (microMapEn == null)
            {
                return 0;
            }
            //批量执行
            sbSQL.AppendFormat("IF NOT EXISTS(SELECT TOP 1 1 FROM LNewsMicroMapping where IsDelete=0 and CustomerId='{0}' and MicroNumberId='{1}' and MicroTypeId='{2}' and NewsId='{3}' )", CurrentUserInfo.ClientID, microMapEn.MicroNumberId, microMapEn.MicroTypeId, microMapEn.NewsId);
            sbSQL.Append("BEGIN ");
            sbSQL.Append("INSERT INTO	LNewsMicroMapping(NewsId,MicroNumberId,MicroTypeId,CustomerId,CreateBy,LastUpdateBy) ");
            sbSQL.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{4}') ;", microMapEn.NewsId, microMapEn.MicroNumberId, microMapEn.MicroTypeId, microMapEn.CustomerId, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
            sbSQL.Append("END ");

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL.ToString());
        }
        #endregion

        #region 刊号、类别统计：关系映射表
        /// <summary>
        /// 刊号、类别统计
        /// </summary>
        /// <param name="numberId">刊号Id</param>
        /// <param name="typeId">类别Id</param>
        /// <returns>返回受影响的行数</returns>
        public int MicroNumberTypeCollect(string numberId, string typeId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update LNumberTypeMapping set LastUpdateBy = '{0}',LastUpdateTime=GETDATE(),TypeCount = (", CurrentUserInfo.UserID);
            sbSQL.Append("select COUNT(*) from LNewsMicroMapping ");
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{2}'and MicroNumberId='{0}' and MicroTypeId='{1}' ", numberId, typeId, CurrentUserInfo.ClientID);
            sbSQL.AppendFormat(") where IsDelete=0 and CustomerId='{2}' and NumberId='{0}' and TypeId='{1}' ;", numberId, typeId, CurrentUserInfo.ClientID);

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }
        #endregion

        #region 删除资讯微刊关联列表
        /// <summary>
        /// 删除资讯微刊关联列表
        /// </summary>
        /// <param name="newsId">新闻标志Id</param>
        /// <param name="trans">事务对象</param>
        /// <returns>返回受影响的函数</returns>
        public int DelNewsMap(string newsId, SqlTransaction trans)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("DELETE FROM LNewsMicroMapping ");
            sbSQL.AppendFormat("where NewsId='{0}' ;", newsId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL.ToString());
        }

        /// <summary>
        /// 根据主键ID删除关联表数据
        /// </summary>
        public int DelNewsMap(string[] mappingId)
        {
            string ids = mappingId.ToJoinString(',', '\'');
            return this.SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format("delete from LNewsMicroMapping where MappingId in (" + ids + ")"));
        }
        #endregion
    }
}
