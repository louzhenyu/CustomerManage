/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/28 10:52:06
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// RouteBLL 
    /// </summary>
    public partial class RouteBLL
    {
        #region GetRouteList
        /// <summary>
        /// 获取路线分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public RouteViewEntity[] GetRouteList(RouteViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            PagedQueryResult<RouteViewEntity> pEntity = this._currentDAO.GetRouteList(entity, orderbys.ToArray(), pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region DeleteRoute
        /// <summary>
        /// 删除路线
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRoute(Guid id)
        {
            RouteUserMappingEntity usermapEntity = new RouteBLL(CurrentUserInfo).GetRouteUser(id);
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    this._currentDAO.DeleteRoute(id, tran);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            this.GenerateCallDayPlanning_RouteDelete(id, usermapEntity);
        }
        #endregion

        #region GetRouteByID
        /// <summary>
        /// 通过路线ID获取路线信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RouteViewEntity GetRouteByID(Guid id)
        {
            return null;
        }
        #endregion
        
        #region EditRoute
        public void EditRoute(RouteEntity entity, string CycleDetailIDS, int ClientUserID)
        {
            Guid? routeID = entity.RouteID;
            Guid generateRouteID = new Guid();//用于调用生成calldayplanning函数
            RouteCycleMappingEntity[] oldcyclemapEntity = null;//数据库的route cycle 信息
            RouteUserMappingEntity oldusermapEntity = null;//数据库的 route user 信息
            RouteEntity oldentity = this._currentDAO.GetByID(entity.RouteID);
            if (routeID != null)
            {
                oldcyclemapEntity = this.GetRouteCycle(routeID.Value);
                oldusermapEntity = this.GetRouteUser(routeID.Value);
            }

            bool isUpdate = false;
            isUpdate = (routeID != null && !string.IsNullOrEmpty(routeID.ToString()));

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                 /*
                  * 编辑路线信息   路线周期关联信息   路线人员关联信息
                  */

                    #region 路线信息
                    if (isUpdate)
                    {
                        this._currentDAO.Update(entity, tran);
                        generateRouteID = entity.RouteID.Value;
                    }
                    else
                    {
                        this._currentDAO.Create(entity, tran);
                        generateRouteID = entity.RouteID.Value;
                    }
                    #endregion

                    #region 路线信息-周期

                    //前台选择的id
                    Guid[] frontSelectedIDS = Array.ConvertAll<string, Guid>(CycleDetailIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                              , x => Guid.Parse(x)
                          );
                    if (isUpdate)
                    {
                        //数据库已有id
                        IEnumerable<Guid> selectedIDS = oldcyclemapEntity.Select(x => x.CycleDetailID.Value);
                        
                        //需要新增、删除的id
                        IEnumerable<Guid> deletedList = selectedIDS.Except(frontSelectedIDS);
                        IEnumerable<Guid> createList = frontSelectedIDS.Except(selectedIDS);

                        //删除
                        foreach (Guid id in deletedList)
                        {
                            Guid mappingID = oldcyclemapEntity.Where(x => x.CycleDetailID == id).Select(x => x.MappingID.Value).ToList()[0];
                            new RouteCycleMappingBLL(CurrentUserInfo).Delete(mappingID, tran);
                        }

                        //新增
                        foreach (Guid id in createList)
                        {
                            RouteCycleMappingEntity cyclemapEntity = new RouteCycleMappingEntity();
                            cyclemapEntity.RouteID = entity.RouteID;
                            cyclemapEntity.CycleDetailID = id;
                            cyclemapEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            cyclemapEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            new RouteCycleMappingBLL(CurrentUserInfo).Create(cyclemapEntity, tran);
                        }

                    }
                    else
                    {
                        //新增
                        foreach (Guid id in frontSelectedIDS)
                        {
                            RouteCycleMappingEntity cyclemapEntity = new RouteCycleMappingEntity();
                            cyclemapEntity.RouteID = entity.RouteID;
                            cyclemapEntity.CycleDetailID = id;
                            cyclemapEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            cyclemapEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            new RouteCycleMappingBLL(CurrentUserInfo).Create(cyclemapEntity, tran);
                        }
                    }
                    #endregion

                    #region 路线信息-人员
                    if (isUpdate)
                    {
                        if (oldcyclemapEntity != null && oldusermapEntity != null)
                        {
                            int olduserid = oldusermapEntity.ClientUserID.Value;

                            oldusermapEntity.RouteID = entity.RouteID;
                            oldusermapEntity.ClientUserID = ClientUserID;
                            oldusermapEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            oldusermapEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            new RouteUserMappingBLL(CurrentUserInfo).Update(oldusermapEntity, tran);

                            //删除路线终端信息(如果人员变化 或者 终端类型变化 就清空终端信息)
                            if (olduserid != ClientUserID || oldentity.POPType != entity.POPType)
                            {
                                new RoutePOPMappingDAO(CurrentUserInfo).DeleteRoutePOPMappingByRouteID(routeID.Value, tran);
                            }
                        }
                        else
                        {
                            tran.Rollback();
                            throw new Exception("数据问题，无路线周期、路线人员 关系数据");
                        }
                    }
                    else
                    {
                        
                        RouteUserMappingEntity usermapEntity = new RouteUserMappingEntity();
                        usermapEntity.RouteID = entity.RouteID;
                        usermapEntity.ClientUserID = ClientUserID;
                        usermapEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                        usermapEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        new RouteUserMappingBLL(CurrentUserInfo).Create(usermapEntity, tran);
                    }
                    #endregion
                    
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            #region 生成calldayplanning
            if (isUpdate)
            {//修改时调用生成计划
                this.GenerateCallDayPlanning(generateRouteID);
            }
            #endregion
            
        }
        #endregion

        #region GetUserPOPList

        #endregion
        #region GetRouteCycle
        public RouteCycleMappingEntity[] GetRouteCycle(Guid routeid)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (routeid != null)
            {
                wheres.Add(new EqualsCondition() { FieldName = "routeid", Value = routeid.ToString() });
            }

            PagedQueryResult<RouteCycleMappingEntity> pEntity = new RouteCycleMappingDAO(CurrentUserInfo).PagedQuery(wheres.ToArray(), null, int.MaxValue, 1);
            int rowCount = pEntity.RowCount;

            return pEntity.Entities;
        }
        #endregion
        #region GetRouteUser
        public RouteUserMappingEntity GetRouteUser(Guid routeid)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (routeid != null)
            {
                wheres.Add(new EqualsCondition() { FieldName = "routeid", Value = routeid.ToString() });
            }

            PagedQueryResult<RouteUserMappingEntity> pEntity = new RouteUserMappingDAO(CurrentUserInfo).PagedQuery(wheres.ToArray(), null, 2, 1);
            int rowCount = pEntity.RowCount;
            if (pEntity.RowCount == 1)
            {
                return pEntity.Entities[0];
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region GenerateCallDayPlanning
        public void GenerateCallDayPlanning(Guid id)
        {
            RouteUserMappingEntity usermapEntity = new RouteBLL(CurrentUserInfo).GetRouteUser(id);
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    if (usermapEntity.ClientUserID.HasValue)
                    {
                        this._currentDAO.GenerateCallDayPlanning(id, usermapEntity.ClientUserID.Value.ToString(), tran);
                    }
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

        #region GenerateCallDayPlanning_RouteDelete
        /// <summary>
        /// 删除路线时使用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usermapEntity"></param>
        public void GenerateCallDayPlanning_RouteDelete(Guid id, RouteUserMappingEntity usermapEntity)
        {
            //RouteUserMappingEntity usermapEntity = new RouteBLL(CurrentUserInfo).GetRouteUser(id);
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    if (usermapEntity.ClientUserID.HasValue)
                    {
                        this._currentDAO.GenerateCallDayPlanning(id, usermapEntity.ClientUserID.Value.ToString(), tran);
                    }
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
    }
}