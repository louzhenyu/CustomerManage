/*
 * Author		:zhongbao.xiao
 * EMail		:zhongbao.xiao@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/6 10:02:54
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理： 品牌 
    /// </summary>
    public partial class BrandBLL
    {   
        #region GetList
        /// <summary>
        /// 获取品牌信息列表
        /// </summary>
        /// <param name="entity">参数：Brand实体</param>
        /// <param name="pageIndex">参数：当前页</param>
        /// <param name="pageSize">参数：分页个数</param>
        /// <param name="rowCount">参数：数据集的总数</param>
        /// <returns>返回Brand实体信息</returns>
        public BrandViewEntity[] GetList(BrandViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && entity.BrandLevel != null && Convert.ToInt32(entity.BrandLevel) > 0)
            {
                wheres.Add(new EqualsCondition() { FieldName = "b.BrandLevel", Value = entity.BrandLevel });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.BrandName))
            {
                wheres.Add(new LikeCondition() { FieldName = "b.BrandName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.BrandName });
            }

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            PagedQueryResult<BrandViewEntity> pEntity = new BrandDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);
                
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion        

        #region Delete
        /// <summary>
        /// 删除品牌信息
        /// 删除规则：只要产品里有相关数据不可删除
        /// </summary>
        /// <param name="ids">参数：品牌ID</param>
        /// <returns>返回真假值</returns>
        public void Delete(int ids, out string res)
        {
            res = "";
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction(); ;
            using (tran.Connection)
            {
                try
                {
                    this._currentDAO.DeleteBrand(ids, tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region GetAllLevel
        /// <summary>
        /// 获取品牌等级
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllLevel()
        {
            return new BrandDAO(CurrentUserInfo).GetAllLevel();
        }        
        #endregion

        #region GetBrandName
        /// <summary>
        /// 根据品牌等级获取上级品牌
        /// </summary>
        /// <param name="pBrandLevel">品牌等级</param>
        /// <returns></returns>
        public BrandEntity[] GetBrandName(string pBrandLevel,string pIso)
        {
            return new BrandDAO(CurrentUserInfo).GetBrandName(pBrandLevel,pIso);
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
            return new BrandDAO(CurrentUserInfo).ValidateNo(entity);                 
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
            return new BrandDAO(CurrentUserInfo).ValidateNoUpdate(entity);
        }
        #endregion

        #region ValBrandID
        /// <summary>
        /// 验证删除规则：只要产品里有相关数据不可删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataSet ValBrandID(string ids)
        {
            return new BrandDAO(CurrentUserInfo).ValBrandID(ids);
        }
        #endregion
    }
}