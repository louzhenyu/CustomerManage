/*
* Author		:zhongbao.xiao
 * EMail		:zhongbao.xiao@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/7 17:31:42
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

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    public partial class BrandDAO
    {
        #region GetList
        /// <summary>
        /// 获取品牌信息列表方法
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<BrandViewEntity> GetList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            StringBuilder sqlOrder = new StringBuilder();
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    sqlOrder.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sqlOrder.Remove(sqlOrder.Length - 1, 1);
            }

            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + string.Format(SqlMap.SQL_GETBRANDLIST, CurrentUserInfo.ClientID) + sqlWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<BrandViewEntity> pEntity = new PagedQueryResult<BrandViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<BrandViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region GetAllLevel
        public DataSet GetAllLevel()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct Brandlevel from Brand where isdelete=0 and Brandlevel is not null and clientid='{0}' ", CurrentUserInfo.ClientID);
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion

        #region GetBrandName
        /// <summary>
        /// 根据品牌等级获取上级品牌
        /// </summary>
        /// <returns></returns>
        public BrandEntity[] GetBrandName(string pBrandLevel,string pIso)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from Brand where ClientID='{0}' and IsDelete=0 and BrandLevel=" + pBrandLevel + " -1 and IsOwner={1}", CurrentUserInfo.ClientID,pIso);
            //读取数据
            List<BrandEntity> list = new List<BrandEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    BrandEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }
        #endregion

        #region ValidateNo
        /// <summary>
        /// 在编号不为空的前提下 验证数据库中是否存在相同的品牌编号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet ValidateNo(BrandEntity entity)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            if (entity.BrandNo != null && entity.BrandNo != "")
            {
                sql.AppendFormat(@"select * from Brand where ClientID='{0}' and IsDelete=0 and BrandNo is not null and BrandNo='{1}'", CurrentUserInfo.ClientID, entity.BrandNo);
            }
            else
            {
                sql.AppendFormat(@"select * from Brand where ClientID='{0}' and IsDelete=0 and BrandNo is not null and BrandNo=null", CurrentUserInfo.ClientID);
            }
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion

        #region ValidateNoUpdate
        /// <summary>
        /// 在编号不为空的前提下 验证数据库中是否存在相同的品牌编号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet ValidateNoUpdate(BrandEntity entity)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            if (entity.BrandNo != null && entity.BrandNo != "")
            {
                sql.AppendFormat(@"select * from Brand where ClientID='{0}' and IsDelete=0 and BrandNo is not null and BrandNo='{1}' and BrandID!={2}", CurrentUserInfo.ClientID, entity.BrandNo,entity.BrandID);
            }
            else
            {
                sql.AppendFormat(@"select * from Brand where ClientID='{0}' and IsDelete=0 and BrandNo is not null and BrandNo=null and BrandID!={1}", CurrentUserInfo.ClientID,entity.BrandID);
            }
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion

        #region DeleteBrand
        /// <summary>
        /// 删除品牌信息
        /// 删除规则：只要产品里有相关数据不可删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void DeleteBrand(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"update [Brand] set LastUpdateTime=getdate(),LastUpdateBy={0},IsDelete=1 where BrandID={1};", CurrentUserInfo.UserID, pID.ToString());
            //执行语句
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
        }
        #endregion

        #region ValBrandID
        /// <summary>
        /// 验证删除规则：只要产品里有相关数据不可删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet ValBrandID(string ids)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select
                            *
                            from SKU
                            where ClientID='{0}' and IsDelete=0 and BrandID={1}",CurrentUserInfo.ClientID,ids);
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion
    }
}
