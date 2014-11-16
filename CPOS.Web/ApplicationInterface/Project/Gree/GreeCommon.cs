using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree
{
    public class GreeCommon
    {
        /// <summary>
        /// 格力CustomerID
        /// </summary>
        public const string GL_CUSTOMER_ID = "6c1ce52aa43441a3a13c87b41fcafd54";//13d00139887f4160a298c4015e713bf7

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static ServicePersonViewModel GetServicePerson(string customerId, string userId, string servicePersonId)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);
            var bll = new GLServicePersonInfoBLL(loggingSessionInfo);
            var person = bll.GetByID(servicePersonId);
            var taskBll = new GLServiceTaskBLL(loggingSessionInfo);
            var list = taskBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "UserID", Value = servicePersonId} 
                    },
                null);
            var orderCount = list.Count(task => task.ServiceDate != null && (task.ServiceDate.Value.Date == DateTime.Now.Date));
            return new ServicePersonViewModel
            {
                Mobile = person.Mobile,
                Name = person.Name,
                OrderCount = person.OrderCount,
                Picture = person.Picture,
                ServicePersonId = servicePersonId,
                Star = person.Star,
                TodayOrder = orderCount
            };
        }
    }
}