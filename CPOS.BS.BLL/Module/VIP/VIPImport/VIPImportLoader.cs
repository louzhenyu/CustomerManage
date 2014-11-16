using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Interface;
using JIT.CPOS.BS.BLL.Utility.ETCL;
using System.Data;
using JIT.Utility;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL.Module.VIP.VIPImport
{
    public class VIPImportLoader : ILoader
    {
        #region 重载ILoader
        public bool Process(IETCLDataItem[] pDataRow, LoggingSessionInfo pUser, out IETCLResultItem[] oResult, IDbTransaction pTran, string pProjectID)
        {

            bool isPass = true;
            List<TenantPlatformETCLResultItem> lstErrorCell = new List<TenantPlatformETCLResultItem>();

            oResult = lstErrorCell.ToArray();
            return isPass;

        }
        #endregion

        #region Process
        /// <summary>
        /// 导入路线及详细信息
        /// </summary>
        /// <param name="pRouteImportEntityList"></param>
        /// <param name="pDataRow"></param>
        /// <param name="pUser"></param>
        /// <param name="oResult"></param>
        /// <param name="pTran"></param>
        /// <param name="pProjectID"></param>
        /// <returns></returns>
        public bool Process(List<VipEntity> pRouteImportEntityList, IETCLDataItem[] pDataRow, LoggingSessionInfo pUser, out IETCLResultItem[] oResult, IDbTransaction pTran, string pProjectID)
        {
            if (pTran == null)
            {
                pTran = new TransactionHelper(pUser).CreateTransaction();
            }
            bool isPass = true;

            //V routeDao = new RouteDAO(pUser);
            List<TenantPlatformETCLResultItem> lstErrorCell = new List<TenantPlatformETCLResultItem>();
            StringBuilder sql = new StringBuilder();
            //foreach (var item in pRouteImportEntityList)
            //{
            //    sql.AppendLine(" insert into Route(RouteID,RouteName,Status,StartDate,POPType,TripMode,Remark,ClientID,ClientDistributorID,CreateBy,CreateTime)");
            //    sql.AppendFormat(@"values('{0}','{1}',{2},'{3}',{4},{5},'{6}',{7},{8},{9},'{10}')", item.RouteID, item.RouteName, item.Status, item.StartDate, item.POPType
            //        , (item.TripMode == null ? "NULL" : item.TripMode.ToString()), item.Remark, item.ClientID, item.ClientDistributorID, item.CreateBy, item.CreateTime);
            //    sql.AppendLine("");
            //    sql.AppendLine(" insert into RouteCycleMapping(MappingID,RouteID,CycleDetailID,ClientID,ClientDistributorID,CreateBy,CreateTime)");
            //    sql.AppendFormat(@"values('{0}','{1}','{2}',{3},{4},{5},'{6}')", item.RouteCycleMappingEntity.MappingID, item.RouteCycleMappingEntity.RouteID, item.RouteCycleMappingEntity.CycleDetailID
            //                                , item.ClientID, item.ClientDistributorID, item.RouteCycleMappingEntity.CreateBy, item.RouteCycleMappingEntity.CreateTime);
            //    sql.AppendLine("");
            //    sql.AppendLine("insert into RouteUserMapping(MappingID,RouteID,ClientUserID,ClientID,ClientDistributorID,CreateBy,CreateTime)");
            //    sql.AppendFormat(@"values('{0}','{1}','{2}',{3},{4},{5},'{6}')", item.RouteUserMappingEntity.MappingID, item.RouteUserMappingEntity.RouteID, item.RouteUserMappingEntity.ClientUserID
            //                                , item.ClientID, item.ClientDistributorID, item.RouteUserMappingEntity.CreateBy, item.RouteUserMappingEntity.CreateTime);

            //    foreach (var routePopItem in item.RoutePopMappingEntity)
            //    {
            //        sql.AppendLine("");
            //        sql.AppendLine("insert into RoutePOPMapping(MappingID,RouteID,POPID,Sequence,ClientID,ClientDistributorID,CreateBy,CreateTime)");
            //        sql.AppendFormat(@"values('{0}','{1}','{2}',{3},{4},{5},{6},'{7}')", routePopItem.MappingID, routePopItem.RouteID, routePopItem.POPID, routePopItem.Sequence
            //                                    , routePopItem.ClientID, routePopItem.ClientDistributorID, routePopItem.CreateBy, routePopItem.CreateTime);
            //    }
            //}
            //UtilityEntity uEntity = new UtilityEntity();
            //uEntity.CustomSql = sql.ToString();
            //using (pTran.Connection)
            //{
            //    try
            //    {
            //        new UtilityDAO(pUser).Query(uEntity, pTran);
            //        foreach (var item in pRouteImportEntityList)
            //        {
            //            routeDao.GenerateCallDayPlanning((Guid)item.RouteID, pUser.UserID, pTran);
            //        }
            //        pTran.Commit();
            //    }
            //    catch (Exception)
            //    {
            //        pTran.Rollback();
            //        isPass = false;
            //        TenantPlatformETCLResultItem errorItem = new TenantPlatformETCLResultItem();
            //        errorItem.Message = "路线导入时调用存储过程spCallDayPlanning出现异常";
            //        lstErrorCell.Add(errorItem);
            //    }
            //}
            oResult = lstErrorCell.ToArray();
            return isPass;
        }
        #endregion

        #region ILoader 成员

        public bool Process(IETCLDataItem[] pDataRow, BasicUserInfo pUserInfo, out IETCLResultItem[] oResult, IDbTransaction pTran)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
