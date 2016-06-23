/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-13 14:00:31
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
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class InnerGroupNewsBLL
    {

        public DataSet GetInnerGroupNewsList(int pageIndex, int pageSize, string OrderBy, string sortType, string UserID, string CustomerID, string DeptID)
        {
            return _currentDAO.GetInnerGroupNewsList(pageIndex, pageSize, OrderBy, sortType, UserID, CustomerID, DeptID);
        }


        public void DeleteInnerGroupNews(string GroupNewsID)
        {
            _currentDAO.DeleteInnerGroupNews(GroupNewsID);
        }

        #region 会员金矿站内消息列表
        /// <summary>
        /// 获取 商户消息 列表 按照分页查询信息
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">煤业显示条数</param>
        /// <param name="UserID">用户编号</param>
        /// <param name="CustomerID">商户编号</param>
        /// <param name="NoticePlatformType">平台编号（1=微信用户2=APP员工）</param>
        /// <returns></returns>
        public PagedQueryResult<InnerGroupNewsEntity> GetVipInnerGroupNewsList(int pageIndex, int pageSize, string UserID, string CustomerID, int NoticePlatformType, int? BusType, DateTime BeginTime)
        {
            return _currentDAO.GetVipInnerGroupNewsList(pageIndex, pageSize, UserID, CustomerID, NoticePlatformType, BusType, BeginTime);
        }

        /// <summary>
        /// 获取未读站内信消息个数 默认过滤之前的消息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="CustomerID">商户编号</param>
        /// <param name="NoticePlatformType">平台编号（1=微信用户2=APP员工）</param>
        /// <param name="CreateTime">当前用户注册时间</param>
        /// <returns></returns>
        public int GetVipInnerGroupNewsUnReadCount(string UserID, string CustomerID, int NoticePlatformType, string NewsGroupId, DateTime CreateTime)
        {
            return _currentDAO.GetVipInnerGroupNewsUnReadCount(UserID, CustomerID, NoticePlatformType, NewsGroupId, CreateTime);
        }

        /// <summary>
        /// 根据 商户编号| 平台编号 获取商户总消息个数
        /// </summary>
        /// <param name="customerId">商户编号</param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public List<InnerGroupNewsEntity> GetMessageByCustomerId(string customerId, int NoticePlatformTypeId, LoggingSessionInfo loggingSessionInfo)
        {
            //计算 TotalCount 和  TotalPages
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = 0 });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "NoticePlatformType", Value = NoticePlatformTypeId });
            InnerGroupNewsBLL newslist = new InnerGroupNewsBLL(loggingSessionInfo);
            List<OrderBy> lstOrderByCondition = new List<OrderBy>();
            lstOrderByCondition.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            var list = newslist.Query(lstWhereCondition.ToArray(), lstOrderByCondition.ToArray()).ToList();//获取当前商户 有效消息 个数
            return list;
        }

        /// <summary>
        /// 获取上一条信息/下一条信息 或者当前 消息
        /// </summary>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="model">operationtype（0=当前消息 1=下一条消息 2=上一条消息）</param>
        /// <param name="NoticePlatformType">平台编号（1=微信用户 2=APP员工）</param>
        /// <param name="CreateTime">会员注册信息</param>
        /// 
        /// <returns>
        /// </returns>
        public InnerGroupNewsEntity GetVipInnerGroupNewsDetailsByPaging(int operationtype, string CustomerID, int NoticePlatformType, string GroupNewsId, DateTime CreateTime)
        {
            return _currentDAO.GetVipInnerGroupNewsDetailsByPaging(CustomerID, operationtype, NoticePlatformType, GroupNewsId, CreateTime);
        }
        /// <summary>
        /// 查看某个用户对某条信息时候 读取过了
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="KeyId"></param>
        /// <param name="newsusermappingService"></param>
        /// <returns></returns>
        public bool CheckUserIsReadMessage(string userId, string customerId, string GroupNewsId)
        {
            return this._currentDAO.GetVipInnerGroupNewsUnReadCount(userId, customerId, null, GroupNewsId,null) > 0;
        }
        #endregion

    }
}