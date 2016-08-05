/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:28:44
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_SplitProfitRecordBLL
    {
        private BasicUserInfo CurrentUserInfo;
        private T_SplitProfitRecordDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SplitProfitRecordBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new T_SplitProfitRecordDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_SplitProfitRecordEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_SplitProfitRecordEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity,pTran);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_SplitProfitRecordEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public T_SplitProfitRecordEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_SplitProfitRecordEntity pEntity , IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity,pTran);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(T_SplitProfitRecordEntity pEntity )
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SplitProfitRecordEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_SplitProfitRecordEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity,pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID,pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_SplitProfitRecordEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities,pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_SplitProfitRecordEntity[] pEntities)
        { 
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            _currentDAO.Delete(pIDs,pTran);
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public T_SplitProfitRecordEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
           return _currentDAO.Query(pWhereConditions,pOrderBys);
        }

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<T_SplitProfitRecordEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQuery(pWhereConditions,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public T_SplitProfitRecordEntity[] QueryByEntity(T_SplitProfitRecordEntity pQueryEntity, OrderBy[] pOrderBys)
        {
           return _currentDAO.QueryByEntity(pQueryEntity,pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<T_SplitProfitRecordEntity> PagedQueryByEntity(T_SplitProfitRecordEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQueryByEntity(pQueryEntity,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        #endregion
    }
}