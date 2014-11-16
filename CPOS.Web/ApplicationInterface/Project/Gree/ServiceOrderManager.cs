using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree
{
    public class ServiceOrderManager
    {
        /// <summary>
        /// 预约单挂牌时间(秒)
        /// </summary>
        private const int ServiceOrderRunningTime = 60;

        /// <summary>
        /// 预约单公示时间(秒)
        /// </summary>
        private const int ServiceOrderShowTime = 3600;

        /// <summary>
        /// the Singleton object
        /// </summary>
        private static ServiceOrderManager _manager;

        /// <summary>
        /// 预约单容器
        /// </summary>
        private readonly Dictionary<String, ServiceOrderLifeTimeItem> _serviceOrderDict;

        private static readonly AutoResetEvent AutoEvent = new AutoResetEvent(false);
        private static readonly TimerCallback Tcb = StateCheckCallback;
        private static readonly Timer StateCheckTimer = new Timer(Tcb, AutoEvent, 300000, 300000);

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ServiceOrderManager()
        {
            _serviceOrderDict = new Dictionary<string, ServiceOrderLifeTimeItem>();
        }

        ~ServiceOrderManager()
        {
            StateCheckTimer.Dispose();
        }

        /// <summary>
        /// 单例属性
        /// </summary>
        public static ServiceOrderManager Instance
        {
            get { return _manager ?? (_manager = new ServiceOrderManager()); }
        }

        /// <summary>
        /// 提交预约单
        /// </summary>
        /// <param name="order"></param>
        public void AddServiceOrder(SubscribeOrderViewModel order)
        {
            var item = new ServiceOrderLifeTimeItem(order, DateTime.Now.AddSeconds(ServiceOrderRunningTime));
            if (!_serviceOrderDict.ContainsKey(order.ServiceOrderNO))
            {
                _serviceOrderDict.Add(order.ServiceOrderNO, item);
            }
            else
            {
                _serviceOrderDict[order.ServiceOrderNO] = item;
            }
        }

        /// <summary>
        /// 申请预约单
        /// </summary>
        /// <param name="serviceOrderId"></param>
        /// <param name="servicePersonId"></param>
        public void ApplyOrder(String serviceOrderId, String servicePersonId)
        {
            if (_serviceOrderDict.ContainsKey(serviceOrderId))
            {
                _serviceOrderDict[serviceOrderId].Apply(servicePersonId);
            }
        }

        /// <summary>
        /// 获取预约单列表
        /// </summary>
        /// <returns></returns>
        public List<SubscribeOrderViewModel> GetRunningServiceOrder()
        {
            return (from item in _serviceOrderDict.Values where item.IsActive select item.ServiceOrder).ToList();
        }

        /// <summary>
        /// 获取接单师傅列表
        /// </summary>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        public List<String> GetAppliedServicePerson(String serviceOrderId)
        {
            return !_serviceOrderDict.ContainsKey(serviceOrderId) ? new List<String>() : _serviceOrderDict[serviceOrderId].ApplyList;
        }

        /// <summary>
        /// 选定师傅
        /// </summary>
        /// <param name="serviceOrderId"></param>
        /// <param name="servicePersonId"></param>
        public void SelectServicePerson(String serviceOrderId, String servicePersonId)
        {
            if (!_serviceOrderDict.ContainsKey(serviceOrderId))
            {
                return;
            }

            _serviceOrderDict[serviceOrderId].OrderWinner = servicePersonId;
        }

        /// <summary>
        /// 查询是否赢得预约单
        /// </summary>
        /// <param name="serviceOrderId"></param>
        /// <param name="servicePersonId"></param>
        /// <returns></returns>
        public int IsOrderWinner(String serviceOrderId, String servicePersonId)
        {
            if (!_serviceOrderDict.ContainsKey(serviceOrderId))
            {
                return 0;
            }

            if (String.IsNullOrEmpty(_serviceOrderDict[serviceOrderId].OrderWinner))
            {
                return -1;
            }

            return _serviceOrderDict[serviceOrderId].OrderWinner.EndsWith(servicePersonId) ? 1 : 0;
        }

        /// <summary>
        /// 获取预约单
        /// </summary>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        public SubscribeOrderViewModel GetServiceOrder(String serviceOrderId)
        {
            return _serviceOrderDict.ContainsKey(serviceOrderId) ? _serviceOrderDict[serviceOrderId].ServiceOrder : null;
        }

        /// <summary>
        /// 查询接单人数
        /// </summary>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        public int GetAppliedCount(String serviceOrderId)
        {
            if (!_serviceOrderDict.ContainsKey(serviceOrderId))
            {
                return 0;
            }

            return _serviceOrderDict[serviceOrderId].ApplyList.Count;
        }

        /// <summary>
        /// 回调函数，预约单结束ServiceOrderShowTime(秒)后，从内存移除。
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void StateCheckCallback(Object stateInfo)
        {
            try
            {
                var removeList = new List<string>();
                foreach (var item in Instance._serviceOrderDict.Values)
                {
                    if (item.LifeTime.AddSeconds(ServiceOrderShowTime) < DateTime.Now)
                    {
                        removeList.Add(item.ServiceOrder.ServiceOrderNO);
                    }
                }

                foreach (var serviceOrderId in removeList)
                {
                    Instance._serviceOrderDict.Remove(serviceOrderId);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}