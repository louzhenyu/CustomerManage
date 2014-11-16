using System;
using System.Collections.Generic;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree
{
    public class ServiceOrderDataAccess
    {
        private readonly LoggingSessionInfo _loggingSessionInfo;
        public ServiceOrderDataAccess(LoggingSessionInfo loggingSessionInfo)
        {
            _loggingSessionInfo = loggingSessionInfo;
        }

        /// <summary>
        /// 获取预约单
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public SubscribeOrderViewModel GetSubscribeOrder(string pCustomerId, string pOrderNo)
        {
            var smodel = new SubscribeOrderViewModel();
            var glsobll = new GLServiceOrderBLL(_loggingSessionInfo);
            DataSet ds = glsobll.GetServiceOrderByOrderNo(pCustomerId, pOrderNo);
            if (ds.Tables[0] != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                smodel.ServiceOrderNO = row["ServiceOrderID"].ToString();
                smodel.ServiceType = int.Parse(row["ServiceType"].ToString());
                smodel.ServiceOrderDate = Convert.ToDateTime(row["ServiceDate"].ToString());
                smodel.ServiceOrderDateEnd = Convert.ToDateTime(row["ServiceDateEnd"].ToString());
                smodel.ServiceAddress = row["ServiceAddress"].ToString();
                smodel.CustomerName = row["CustomerName"].ToString();
                smodel.CustomerPhone = row["CustomerPhone"] == null ? "" : row["CustomerPhone"].ToString();
                smodel.Message = row["CustomerMessage"] == null ? "" : row["CustomerMessage"].ToString();
                smodel.Longitude = Convert.ToDecimal(row["Longitude"] == null ? "0.000000" : row["Longitude"].ToString());
                smodel.Latitude = Convert.ToDecimal(row["Latitude"] == null ? "0.000000" : row["Latitude"].ToString());
                smodel.VipID = row["VipID"] == null ? "" : row["VipID"].ToString();
                smodel.Distance = "";

                if (row["InstallOrderDate"] != null && !string.IsNullOrEmpty(row["InstallOrderDate"].ToString()))
                    smodel.InstallOrderDate = Convert.ToDateTime(row["InstallOrderDate"].ToString());
            }
            smodel.DeviceList = GetInstallDevice(pCustomerId, pOrderNo);
            smodel.InstallCount = smodel.DeviceList.Count;
            return smodel;
        }

        public SubscribeOrderViewModel GetSubscribeOrderDetail(string pCustomerId, string pServiceOrderNo)
        {
            var smodel = new SubscribeOrderViewModel();
            var glsobll = new GLServiceOrderBLL(_loggingSessionInfo);
            DataSet ds = glsobll.GetServiceOrderByServiceOrderNo(pCustomerId, pServiceOrderNo);
            if (ds.Tables[0] != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                smodel.ServiceOrderNO = row["ServiceOrderID"].ToString();
                smodel.ServiceType = int.Parse(row["ServiceType"].ToString());
                smodel.ServiceOrderDate = Convert.ToDateTime(row["ServiceDate"].ToString());
                smodel.ServiceOrderDateEnd = Convert.ToDateTime(row["ServiceDateEnd"].ToString());
                smodel.ServiceAddress = row["ServiceAddress"].ToString();
                smodel.CustomerName = row["CustomerName"].ToString();
                smodel.CustomerPhone = row["CustomerPhone"] == null ? "" : row["CustomerPhone"].ToString();
                smodel.Message = row["CustomerMessage"] == null ? "" : row["CustomerMessage"].ToString();
                smodel.Longitude = Convert.ToDecimal(row["Longitude"] == null ? "0.000000" : row["Longitude"].ToString());
                smodel.Latitude = Convert.ToDecimal(row["Latitude"] == null ? "0.000000" : row["Latitude"].ToString());
                smodel.VipID = row["VipID"] == null ? "" : row["VipID"].ToString();
                smodel.Distance = "";

                if (row["InstallOrderDate"] != null && !string.IsNullOrEmpty(row["InstallOrderDate"].ToString()))
                    smodel.InstallOrderDate = Convert.ToDateTime(row["InstallOrderDate"].ToString());
            }
            //smodel.DeviceList = GetInstallDeviceByServiceOrderNO(pCustomerId, pServiceOrderNo);
            smodel.DeviceList = GetInstallDeviceByServiceOrderId(pCustomerId, pServiceOrderNo);
            smodel.InstallCount = smodel.DeviceList.Count;
            return smodel;
        }


        /// <summary>
        /// 获取安装设备
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public List<InstallDeviceViewModel> GetInstallDevice(string pCustomerId, string pOrderNo)
        {
            var gldiibll = new GLDeviceInstallItemBLL(_loggingSessionInfo);
            DataSet dsDevice = gldiibll.GetDeviceInstallItemByOrderNo(pCustomerId, pOrderNo);
            if (dsDevice.Tables[0] != null && dsDevice.Tables.Count > 0 && dsDevice.Tables[0].Rows.Count > 0)
            {
                return DataTableToObject.ConvertToList<InstallDeviceViewModel>(dsDevice.Tables[0]);
            }
            return new List<InstallDeviceViewModel>();
        }
        //public List<InstallDeviceViewModel> GetInstallDeviceByServiceOrderNO(string pCustomerId, string pServiceOrderNo)
        //{
        //    var gldiibll = new GLDeviceInstallItemBLL(_loggingSessionInfo);
        //    DataSet dsDevice = gldiibll.GetDeviceInstallItemByServiceOrderNo(pCustomerId, pServiceOrderNo);
        //    if (dsDevice.Tables[0] != null && dsDevice.Tables.Count > 0 && dsDevice.Tables[0].Rows.Count > 0)
        //    {
        //        return DataTableToObject.ConvertToList<InstallDeviceViewModel>(dsDevice.Tables[0]);
        //    }
        //    return new List<InstallDeviceViewModel>();
        //}

        /// <summary>
        /// 根据ServiceOrderID获取安装设备
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <param name="pServiceOrderId"></param>
        /// <returns></returns>
        public List<InstallDeviceViewModel> GetInstallDeviceByServiceOrderId(string pCustomerId, string pServiceOrderId)
        {
            var gldiibll = new GLDeviceInstallItemBLL(_loggingSessionInfo);
            DataSet dsDevice = gldiibll.GetDeviceInstallItemByServiceOrderID(pCustomerId, pServiceOrderId);
            if (dsDevice.Tables[0] != null && dsDevice.Tables.Count > 0 && dsDevice.Tables[0].Rows.Count > 0)
            {
                return DataTableToObject.ConvertToList<InstallDeviceViewModel>(dsDevice.Tables[0]);
            }
            return new List<InstallDeviceViewModel>();
        }

        /// <summary>
        /// 保存服务订单号+设备列表
        /// </summary>
        /// <param name="pServiceOrder"></param>
        /// <param name="pInstallDeviceList"></param>
        /// <returns></returns>
        public string SaveSubscribeOrder(GLServiceOrderEntity pServiceOrder, List<InstallDeviceViewModel> pInstallDeviceList)
        {
            var glsobll = new GLServiceOrderBLL(_loggingSessionInfo);
            var glsoe = glsobll.GetGLServiceOrderEntityByOrderID(pServiceOrder.CustomerID, pServiceOrder.ProductOrderID);
            if (glsoe == null)
                glsobll.Create(pServiceOrder);
            else
                return "0";
            //保存设备
            SaveInstallDeviceList(pServiceOrder, pInstallDeviceList);

            return "1";
        }

        /// <summary>
        /// 保存服务单号对应的设备列表
        /// </summary>
        /// <param name="pServiceOrder"></param>
        /// <param name="pInstallDeviceList"></param>
        public void SaveInstallDeviceList(GLServiceOrderEntity pServiceOrder, List<InstallDeviceViewModel> pInstallDeviceList)
        {
            var gldiibll = new GLDeviceInstallItemBLL(_loggingSessionInfo);
            for (var i = 0; i < pInstallDeviceList.Count; i++)
            {
                var gldiie = new GLDeviceInstallItemEntity
                {
                    ServiceOrderID = pServiceOrder.ServiceOrderID,
                    DeviceInstallID = GreeCommon.GetGuid(),
                    DeviceItemID = pInstallDeviceList[i].DeviceID,
                    DeviceFullName = pInstallDeviceList[i].DeviceName,
                    InstallPosition = pInstallDeviceList[i].InstallPosition,
                    CustomerID = pServiceOrder.CustomerID,
                    IsDelete = 0
                };
                //服务订单号

                //gldiie.DeviceCount = pInstallDeviceList[i].DeviceCount;
                gldiibll.Create(gldiie);
            }
        }

        /// <summary>
        /// 获取师傅的当前任务列表
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <param name="pServicePersonId"></param>
        /// <returns></returns>
        public List<SubscribeOrderViewModel> GetSubscribeOrderList(string pCustomerId, string pServicePersonId)
        {
            var listServiceOrder = new List<SubscribeOrderViewModel>();
            var glsobll = new GLServiceOrderBLL(_loggingSessionInfo);
            DataSet ds = glsobll.GetServiceOrderByServicePersonID(pCustomerId, pServicePersonId);
            if (ds.Tables[0] != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                listServiceOrder = DataTableToObject.ConvertToList<SubscribeOrderViewModel>(ds.Tables[0]);
            }
            foreach (var item in listServiceOrder)
            {
                item.DeviceList = GetInstallDeviceByServiceOrderId(pCustomerId, item.ServiceOrderNO);
                item.InstallCount = item.DeviceList.Count;
            }
            return listServiceOrder;
        }

        /// <summary>
        /// 删除服务单号对应的设备列表
        /// </summary>
        /// <param name="pServiceOrder"></param>
        /// <param name="pInstallDeviceList"></param>
        public void DeleteInstallDeviceList(string pCustomerID, string pServiceOrderID)
        {
            var gldiibll = new GLDeviceInstallItemBLL(_loggingSessionInfo);
            gldiibll.DelDeviceInstallItemByServerOrderID(pCustomerID, pServiceOrderID);
        }
    }
}