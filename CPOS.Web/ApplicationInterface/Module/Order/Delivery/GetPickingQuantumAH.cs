using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Delivery.Request;
using JIT.CPOS.DTO.Module.Order.Delivery.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;


namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Delivery
{
    public class GetPickingQuantumAH : BaseActionHandler<GetPickingQuantumRP, GetPickingQuantumRD>
    {
        /// <summary>
        /// 提货时间段
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetPickingQuantumRD ProcessRequest(APIRequest<GetPickingQuantumRP> pRequest)
        {
            GetPickingQuantumRP getPickingQuantunRP = pRequest.Parameters;
            GetPickingQuantumRD getPickingQuantunRD = new GetPickingQuantumRD();

            CustomerTakeDeliveryBLL customerTakeDeliveryBll = new CustomerTakeDeliveryBLL(CurrentUserInfo);
            CustomerTakeDeliveryEntity customerTakeDeliveryEntity = customerTakeDeliveryBll.QueryByEntity(new CustomerTakeDeliveryEntity() { CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
            SysTimeQuantumBLL sysTimeQuantumBll = new SysTimeQuantumBLL(CurrentUserInfo);
            SysTimeQuantumEntity[] sysTimeQuantumEntity = sysTimeQuantumBll.QueryByEntity(new SysTimeQuantumEntity() { CustomerID = CurrentUserInfo.ClientID }, new OrderBy[] { new OrderBy() { FieldName = "Quantum", Direction = OrderByDirections.Asc } });
            string beginDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string pickingDate = "";
            getPickingQuantunRD.QuantumList = new List<QuantumInfo>();

            if (sysTimeQuantumEntity.Length == 0)
            {
                return getPickingQuantunRD;
            }
            if (!string.IsNullOrEmpty(getPickingQuantunRP.BeginDate))
            {
                beginDate = Convert.ToDateTime(getPickingQuantunRP.BeginDate).ToString("yyyy-MM-dd");
            }
            if(!string.IsNullOrEmpty(getPickingQuantunRP.PickingDate))
            {
                pickingDate = Convert.ToDateTime(getPickingQuantunRP.PickingDate).ToString("yyyy-MM-dd");
            }
            else
            {      
                return getPickingQuantunRD;
            }
            
            List<QuantumInfo> list = new List<QuantumInfo>();
            //判断选择的是否为开始日期
            if (beginDate.Equals(pickingDate))
            {
                string date = DateTime.Now.AddHours(Convert.ToDouble(customerTakeDeliveryEntity.StockUpPeriod)).ToString("yyyy-MM-dd");
                //如果允许选择的开始日期是备货结束的日期的后一天，则时间段全部可以选择
                if (date.CompareTo(beginDate) < 0) 
                {
                    for (int i = 0; i < sysTimeQuantumEntity.Length; i++)
                    {
                        QuantumInfo quantumInfo = new QuantumInfo() { Quantum = sysTimeQuantumEntity[i].Quantum };
                        list.Add(quantumInfo);
                    }
                }
                else 
                {
                    string beginTime = DateTime.Now.AddHours(Convert.ToDouble(customerTakeDeliveryEntity.StockUpPeriod)).ToString("HH:mm");
                    for (int i = 0; i < sysTimeQuantumEntity.Length; i++)
                    {
                        string time = sysTimeQuantumEntity[i].Quantum.Split('-')[0];
                        if (time.CompareTo(beginTime) >= 1)
                        {
                            QuantumInfo quantumInfo = new QuantumInfo() { Quantum = sysTimeQuantumEntity[i].Quantum };
                            list.Add(quantumInfo);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < sysTimeQuantumEntity.Length; i++)
                {
                    QuantumInfo quantumInfo = new QuantumInfo() { Quantum = sysTimeQuantumEntity[i].Quantum };
                    list.Add(quantumInfo);
                }
            }
            getPickingQuantunRD.QuantumList = list;

            return getPickingQuantunRD;
        }
    }
}