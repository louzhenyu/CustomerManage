using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.Pos;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 属性类
    /// </summary>
    public class PropService : BaseService
    {
        JIT.CPOS.BS.DataAccess.PropService propService = null;
        #region 构造函数
        public PropService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            propService = new DataAccess.PropService(loggingSessionInfo);
        }
        #endregion

        #region 查询域下信息
        /// <summary>
        /// 获取某个域下的第一层
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="propDomain">作用域</param>
        /// <returns></returns>
        public IList<PropInfo> GetPropListFirstByDomain(string propDomain)
        {
            try
            {
                IList<PropInfo> propInfoList = new List<PropInfo>();
                DataSet ds = new DataSet();
                ds = propService.GetPropListFirstByDomain(propDomain);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    propInfoList = DataTableToObject.ConvertToList<PropInfo>(ds.Tables[0]);
                }
                return propInfoList;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取某个节点下的下一层所有节点信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="propDomain">作用域</param>
        /// <param name="parentPropId">节点标识</param>
        /// <returns></returns>
        public IList<PropInfo> GetPropListByParentId(string propDomain, string parentPropId)
        {
            try
            {
                IList<PropInfo> propInfoList = new List<PropInfo>();
                DataSet ds = new DataSet();
                ds = propService.GetPropListByParentId(propDomain,parentPropId);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    propInfoList = DataTableToObject.ConvertToList<PropInfo>(ds.Tables[0]);
                }
                return propInfoList;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取某个节点的详细信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="propId">节点标识</param>
        /// <returns></returns>
        public PropInfo GetPropInfoById(string propId)
        {
            try
            {
                PropInfo propInfo = new PropInfo();
                DataSet ds = new DataSet();
                ds = propService.GetPropInfoById(propId);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    propInfo = DataTableToObject.ConvertToObject<PropInfo>(ds.Tables[0].Rows[0]);
                }
                return propInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<PropInfo> GetWebProp(PropInfo entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<PropInfo> list = new List<PropInfo>();
            DataSet ds = new DataSet();
            ds = propService.GetWebProp(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<PropInfo>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebPropCount(PropInfo entity)
        {
            return propService.GetWebPropCount(entity);
        }
        #endregion

        #region CreateIndex
        /// <summary>
        /// 根据父ID生成序号
        /// </summary>
        /// <param name="partentPropID"></param>
        /// <returns></returns>
        public int CreateIndex(string partentPropID, string propID)
        {
            return propService.CreateIndex(partentPropID, propID);
        }
        #endregion

        #region CheckSkuLast
      /// <summary>
      /// 
      /// </summary>
      /// <param name="partentID"></param>
      /// <returns></returns>
        public bool CheckSkuLast(string partentID)
        {
            DataSet ds = propService.CheckSkuLast(partentID);
            if (ds!=null&&ds.Tables.Count>0&&ds.Tables[0]!=null&&ds.Tables[0].Rows.Count>0)
            {
                if (int.Parse(ds.Tables[0].Rows[0][0].ToString())>=5)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        #endregion

        #region SaveProp
        /// <summary>
        /// SaveProp
        /// </summary>
        public bool SaveProp(PropInfo propInfo, ref string error)
        {
            propInfo.Create_Time = Utils.GetNow();
            propInfo.Modify_Time = Utils.GetNow();
            propInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            propInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            return propService.SaveProp(propInfo, ref error);
        }
        /// <summary>
        /// AddProp
        /// </summary>
        public bool AddProp(PropInfo propInfo)
        {
            return propService.AddProp(propInfo);
        }
        /// <summary>
        /// UpdateProp
        /// </summary>
        public bool UpdateProp(PropInfo propInfo)
        {
            return propService.UpdateProp(propInfo);
        }
        /// <summary>
        /// CheckProp
        /// </summary>
        public bool CheckProp(PropInfo propInfo)
        {
            return propService.CheckProp(propInfo);
        }
        /// <summary>
        /// CheckProp
        /// </summary>
        public bool CheckProp(string propId)
        {
            return propService.CheckProp(propId);
        }
        #endregion

        #region DeleteProp
        public bool DeleteProp(PropInfo propInfo)
        {
            propInfo.Modify_Time = Utils.GetNow();
            propInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            return propService.DeleteProp(propInfo);
        }
        #endregion


        public bool DeletePropByIds(string propIds, PropInfo propInfo )
        {
            return propService.DeletePropByIds(propIds, propInfo);
        }
 

        #region 获取web业务系统的主界面图标 Jermyn20131202
        public string GetWebLogo()
        {
            return propService.GetWebLogo();
        }
        #endregion

    }
}
