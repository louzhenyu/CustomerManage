/*
 * Author		:yong.liu
 * EMail		:yong.liu@jitmarketing.cn
 * Company		:JIT
 * Create On	:11/1/2012 1:19:19 PM
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
using System.Text;
using System.Data;
using JIT.Utility;
using JIT.Utility.DataAccess;
using JIT.ManagementPlatform.DataAccess.Base;
using JIT.ManagementPlatform.Entity.User;
using System.Data.SqlClient;
using JIT.Utility.DataAccess.Query;

namespace JIT.ManagementPlatform.DataAccess
{
    /// <summary>
    /// 表Resource的数据访问类   表未定义，逻辑代码未书写
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ResourceDAO : BaseManagementPlatformDAO, ICRUDable<ResourceEntity>, IQueryable<ResourceEntity>
    {
        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ResourceEntity pEntity)
        {
            this.Create(pEntity, null);
        }
        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ResourceEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");            

        }
        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ResourceEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            Guid id = Guid.Parse(pID.ToString());
           
            ResourceEntity m = null;
           
            //返回
            return m;
        }
        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public ResourceEntity[] GetAll()
        {
           
            //读取数据
            List<ResourceEntity> list = new List<ResourceEntity>();
          
            //返回
            return list.ToArray();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(ResourceEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");           
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(ResourceEntity pEntity)
        {
            this.Update(pEntity, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ResourceEntity pEntity)
        {
            this.Delete(pEntity, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ResourceEntity pEntity, IDbTransaction pTran)
        {
            
         
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {            
        }

        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public ResourceEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            
            List<ResourceEntity> list = new List<ResourceEntity>();
           
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<ResourceEntity> PagedQuery(Utility.DataAccess.Query.IWhereCondition[] pWhereConditions, Utility.DataAccess.Query.OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {       
            
         

            return null;
        }

        #endregion

        #region 装载实体
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out ResourceEntity pInstance)
        {
            pInstance = new ResourceEntity();
        }
        #endregion
    }
}
