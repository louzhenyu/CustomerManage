/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/3 18:46:08
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class GLServiceOrderBLL
    {
        /// <summary>
        /// 根据订单号获取服务单号信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetServiceOrderByOrderNo(string pCustomerID, string pOrderNo)
        {
            return _currentDAO.GetServiceOrderByOrderNo(pCustomerID, pOrderNo);
        }

        /// <summary>
        /// 根据订单号获取服务单号信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetServiceOrderByServiceOrderNo(string pCustomerID, string pServiceOrderNo)
        {
            return _currentDAO.GetServiceOrderByServiceOrderNo(pCustomerID, pServiceOrderNo);
        }

        /// <summary>
        /// 根据OrderID获取订单服务对象
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pProductOrderID"></param>
        /// <returns></returns>
        public GLServiceOrderEntity GetGLServiceOrderEntityByOrderID(string pCustomerID, string pOrderID)
        {
            return _currentDAO.GetGLServiceOrderEntityByOrderID(pCustomerID, pOrderID);
        }

        /// <summary>
        /// 获取所有未分配师傅的预约服务单
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetGrabingServiceOrderList(string pCustomerID)
        {
            return _currentDAO.GetGrabingServiceOrderList(pCustomerID);
        }

        /// <summary>
        /// 根据师傅ID任务列表
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetServiceOrderByServicePersonID(string pCustomerID, string pServicePersonID)
        {
            return _currentDAO.GetServiceOrderByServicePersonID(pCustomerID, pServicePersonID);
        }

        /// <summary>
        /// 格力收款成功后推送评价师傅链接
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderID"></param>
        /// <param name="loggingSessionInfo"></param>
        public void GreePushPaymentOorder(string pCustomerID, string pOrderID, LoggingSessionInfo loggingSessionInfo)
        {
            System.Data.DataSet ds = GetServiceOrderByOrderNo(pCustomerID, pOrderID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string vipID = ds.Tables[0].Rows[0]["VipID"] == null ? "" : ds.Tables[0].Rows[0]["VipID"].ToString();
                JIT.CPOS.BS.Entity.VipEntity vipInfo = new VipBLL(loggingSessionInfo).GetByID(vipID);
                if (vipInfo == null || vipInfo.VIPID.Equals(""))
                    throw new JIT.CPOS.DTO.Base.APIException("用户不存在") { ErrorCode = 102 };

                #region 推送评论师傅url到用户的微信端
                string retUrl = System.Configuration.ConfigurationManager.AppSettings["GreeEvalWorkerUrl"].ToString();

                //动态参数
                retUrl += "?OrderID=" + ds.Tables[0].Rows[0]["ProductOrderSN"];
                retUrl += "&ServiceOrderID=" + ds.Tables[0].Rows[0]["ServiceOrderID"];
                retUrl += "&VipID=" + vipID;
                retUrl += "&UserID=" + ds.Tables[0].Rows[0]["UserID"];

                string message = "评价安装师傅的服务，请点击<a href='" + retUrl + "'></a>";
                string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, vipID, loggingSessionInfo, vipInfo);
                #endregion
            }
        }

        /// <summary>
        /// 验证是否是格力
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public bool ValidateGree(string pCustomerID, string pDbName)
        {
            return _currentDAO.ValidateGree(pCustomerID, pDbName);
        }
    }
}