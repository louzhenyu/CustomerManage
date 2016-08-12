using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity.Eliya;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Response;
using JIT.CPOS.DTO.Base;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 单位服务
    /// </summary>
    public class UnitService : BaseService
    {
        JIT.CPOS.BS.DataAccess.UnitService unitService = null;
        #region 构造函数
        public UnitService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            unitService = new DataAccess.UnitService(loggingSessionInfo);//实例化DAL层
        }
        #endregion

        #region 单位

        /// <summary>
        /// 根据单位的Id获取单位信息
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录用户的Session信息</param>
        /// <param name="unitId">单位的Id</param>
        /// <returns></returns>
        public UnitInfo GetUnitById(string unitId)
        {
            DataSet ds = unitService.GetUnitById(unitId);
            UnitInfo unitInfo = new UnitInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfo = DataTableToObject.ConvertToObject<UnitInfo>(ds.Tables[0].Rows[0]);
            }
          //  unitInfo.PropertyList = new PropertyUnitService(this.loggingSessionInfo).GetUnitPropertyListByUnit(unitId);
            return unitInfo;
        }

        /// <summary>
        /// 保存组织信息（新建修改）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public string SetUnitInfo(LoggingSessionInfo loggingSessionInfo, UnitInfo unitInfo, bool IsTran = true)
        {
            string strResult = string.Empty;
            int iType;
            try
            {
                unitInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

                //1.判断号码唯一
                if (unitService.IsExistUnitCode(unitInfo.Code, unitInfo.Id) == 1)
                {
                    strResult = "该门店编码已经存在或已停用，请更换门店编码。";
                    return strResult;
                }

                if (unitInfo.strDo == null)
                {
                    //处理是新建还是修改
                    if (unitInfo.Id == null || unitInfo.Id.Equals(""))
                    {
                        unitInfo.Id = NewGuid();
                        iType = 1;
                    }
                    else
                    {
                        iType = 2;
                    }
                }
                else
                {
                    iType = 1;
                }

                //2.提交用户信息至表单
                if (iType.ToString().Equals("1"))
                {
                    if (!SetUnitInsertBill(unitInfo))
                    {
                        strResult = "组织表单提交失败。";
                        return strResult;
                    }
                }

                //3.获取用户表单信息,设置用户状态与状态描述
                BillModel billInfo = new cBillService(loggingSessionInfo).GetBillById(unitInfo.Id);
                unitInfo.Status = billInfo.Status;
                unitInfo.Status_Desc = billInfo.BillStatusDescription;

                //4.提交用户信息
                if (!SetUnitTableInfo(unitInfo))
                {
                    strResult = "提交用户表失败";
                    return strResult;
                }

                //5.处理门店图片  qianzhi  2013-08-08
                if (!new JIT.CPOS.BS.DataAccess.ItemPriceService(loggingSessionInfo).SetUnitImageInfo(unitInfo, null, out strResult))
                {
                    throw (new System.Exception(strResult));
                }

                //6.处理Brand
                if (!new JIT.CPOS.BS.DataAccess.ItemPriceService(loggingSessionInfo).SetUnitBrandInfo(unitInfo, null, out strResult))
                {
                    throw (new System.Exception(strResult));
                }

                //6.处理UnitSortMapping
                if (!new JIT.CPOS.BS.DataAccess.ItemPriceService(loggingSessionInfo).SetUnitSortMappingInfo(unitInfo, null, out strResult))
                {
                    throw (new System.Exception(strResult));
                }

                unitService.SetUnitInfo(unitInfo, out strResult, IsTran);
                if (IsTran)
                {
                    //#if SYN_AP
                    // 单位类型是门店，提交管理平台
                    if (unitInfo.TypeId != null)  // && unitInfo.TypeId.Equals("EB58F1B053694283B2B7610C9AAD2742")（暂时去掉了“单位类型是门店，提交管理平台”的限制***）
                    {
                        if (!SetManagerExchangeUnitInfo(loggingSessionInfo, unitInfo, iType))
                        {
                            return "提交管理平台失败";
                        }
                    }
                }
                //#endif
                //                ////刷新单位级别数据
                //                //cSqlMapper.Instance().QueryForObject("Unit.RefreshUnitLevel", null);
                //                cSqlMapper.Instance().CommitTransaction();

                return strResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 判断组织号码是否唯一
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="UnitCode">组织号码</param>
        /// <param name="UnitId">组织标识</param>
        /// <returns></returns>
        private bool IsExistUnitCode(LoggingSessionInfo loggingSessionInfo, string UnitCode, string UnitId)
        {
            //try
            //{
            //    Hashtable _ht = new Hashtable();
            //    _ht.Add("Unit_Code", UnitCode);
            //    _ht.Add("Unit_Id", UnitId);
            //    _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);

            //    int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Unit.IsExsitUnitCode", _ht);
            //    return n > 0 ? false : true;
            //}
            //catch (Exception ex) {
            //    throw (ex);
            //}
            return false;
        }

        /// <summary>
        /// 提交组织表单单据
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        private bool SetUnitInsertBill(UnitInfo unitInfo)
        {
            try
            {
                BillModel bill = new BillModel();
                cBillService bs = new cBillService(loggingSessionInfo);

                bill.Id = unitInfo.Id;//order_id
                string order_type_id = bs.GetBillKindByCode("UNITMANAGER").Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode("CreateUnit"); //BillKindCode
                bill.KindId = order_type_id;
                bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                BillOperateStateService state = bs.InsertBill(bill);

                if (state == BillOperateStateService.CreateSuccessful)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        /// <summary>
        /// 提交组织单据
        /// </summary>
        /// <param name="unitInfo">组织类</param>
        /// <returns></returns>
        private bool SetUnitTableInfo(UnitInfo unitInfo)
        {
            try
            {
                if (unitInfo != null)
                {
                    if (unitInfo.Create_User_Id == null || unitInfo.Create_User_Id.Equals(""))
                    {
                        unitInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        unitInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (unitInfo.Modify_User_Id == null || unitInfo.Modify_User_Id.Equals(""))
                    {
                        unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        unitInfo.Modify_Time = GetCurrentDateTime();
                    }
                    if (unitInfo.PropertyList != null)
                    {
                        foreach (UnitPropertyInfo unitPropInfo in unitInfo.PropertyList)
                        {
                            if (unitPropInfo.UnitId == null || unitPropInfo.UnitId.Equals(""))
                                unitPropInfo.UnitId = unitInfo.Id;
                            if (unitPropInfo.Id == null || unitPropInfo.Id.Equals(""))
                                unitPropInfo.Id = NewGuid();

                            unitPropInfo.Create_User_id = unitInfo.Create_User_Id;
                            unitPropInfo.Create_Time = unitInfo.Create_Time;

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 组织提交管理平台（只是提交门店级别的）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitInfo"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public bool SetManagerExchangeUnitInfo(LoggingSessionInfo loggingSessionInfo, UnitInfo unitInfo, int TypeId)
        {
            try
            {
                UnitInfo utInfo = new UnitInfo();
                if (TypeId.ToString().Equals("1") || TypeId.ToString().Equals("2"))
                {
                    if (unitInfo.CityId != null)
                    {
                        //获取城市
                        CityService cityService = new CityService(loggingSessionInfo);
                        CityInfo cityInfo = new CityInfo();
                        cityInfo = cityService.GetCityById(unitInfo.CityId);
                        utInfo.ProvinceName = ToStr(cityInfo.City1_Name);
                        utInfo.CityName = ToStr(cityInfo.City3_Name);
                        utInfo.StateName = ToStr(cityInfo.City2_Name);

                    }
                    utInfo.Id = ToStr(unitInfo.Id);
                    utInfo.unit_id = unitInfo.Id;
                    utInfo.Code = ToStr(unitInfo.Code);
                    utInfo.Name = ToStr(unitInfo.Name);
                    utInfo.EnglishName = ToStr(unitInfo.EnglishName);
                    utInfo.ShortName = ToStr(unitInfo.ShortName);

                    utInfo.Address = ToStr(unitInfo.Address);
                    utInfo.Postcode = ToStr(unitInfo.Postcode);
                    if (utInfo.Contact == null || utInfo.Contact.Equals(""))
                    {
                        utInfo.Contact = "--";
                    }
                    else
                    {
                        utInfo.Contact = ToStr(unitInfo.Contact).Substring(0, 10);
                    }
                    utInfo.Telephone = ToStr(unitInfo.Telephone);
                    utInfo.Fax = ToStr(unitInfo.Fax);
                    utInfo.Email = ToStr(unitInfo.Email);
                    utInfo.Remark = ToStr(unitInfo.Remark);
                    utInfo.Status_Desc = ToStr(unitInfo.Status_Desc);
                    utInfo.longitude = ToStr(unitInfo.longitude);
                    utInfo.dimension = ToStr(unitInfo.dimension);
                    if (utInfo.Status_Desc == null || utInfo.Status_Desc.Equals(""))
                    {
                        utInfo.Status_Desc = "正常";
                    }
                }
                if (TypeId.ToString().Equals("3") || TypeId.ToString().Equals("4"))
                {
                    utInfo.Id = unitInfo.Id;
                    utInfo.Status_Desc = unitInfo.Status_Desc;
                }
                string strUnitInfo = cXMLService.Serialiaze(utInfo);

                WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService cdxService = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
                cdxService.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";

                return cdxService.SynUnit(loggingSessionInfo.CurrentLoggingManager.Customer_Id, TypeId, strUnitInfo);

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            return false;
        }

        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }
        #endregion 单位

        #region 单位树
        /// <summary>
        /// 获取缺省关联模式下的根单位列表(只含已启用的)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <returns></returns>
        public IList<UnitInfo> GetRootUnitsByDefaultRelationMode()
        {
            return GetRootUnitsByUnitRelationMode();
        }

        /// <summary>
        /// 获取某种关联模式下的根单位列表(只含已启用的)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <returns></returns>
        public IList<UnitInfo> GetRootUnitsByUnitRelationMode()
        {
            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            DataSet ds = unitService.GetRootUnitsByUnitRelationMode();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfoList = DataTableToObject.ConvertToList<UnitInfo>(ds.Tables[0]);
            }
            return unitInfoList;
        }



        /// <summary>
        /// 获取单位的下一级子单位列表(只含已启用的)
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetSubUnitsByDefaultRelationMode(string unitId)
        {
            return GetSubUnitsByUnitRelationMode(unitId);
        }

        /// <summary>
        /// 获取某种关联模式下的下一级子单位列表(只含已启用的)
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetSubUnitsByUnitRelationMode(string unitId)
        {
            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            DataSet ds = unitService.GetSubUnitsByUnitRelationMode(unitId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfoList = DataTableToObject.ConvertToList<UnitInfo>(ds.Tables[0]);
            }
            return unitInfoList;
        }

        public IList<UnitInfo> GetUnitByUser(string CustomerID, string loginUserID)
        {
            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            DataSet ds = unitService.GetUnitByUser(CustomerID, loginUserID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfoList = DataTableToObject.ConvertToList<UnitInfo>(ds.Tables[0]);
            }
            return unitInfoList;
        }
        #endregion

        #region 单位查询
        /// <summary>
        /// 单位查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="unit_code">组织号码</param>
        /// <param name="unit_name">组织名称</param>
        /// <param name="unit_type_id">组织类型</param>
        /// <param name="unit_tel">电话</param>
        /// <param name="unit_city_id">城市标识</param>
        /// <param name="unit_status">状态</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public UnitInfo SearchUnitInfo(LoggingSessionInfo loggingSessionInfo
                                             , string unit_code
                                             , string unit_name
                                             , string unit_type_id
                                             , string unit_tel
                                             , string unit_city_id
                                             , string unit_status
                                             , int maxRowCount
                                             , int startRowIndex
           , string StoreType, string Parent_Unit_ID, string OnlyShop)
        {
            Hashtable _ht = new Hashtable();
            if (unit_code == null) unit_code = "";
            if (unit_name == null) unit_name = "";
            if (unit_type_id == null) unit_type_id = "";
            if (unit_tel == null) unit_tel = "";
            if (unit_city_id == null) unit_city_id = "";
            if (unit_status == null) unit_status = "";
            if (Parent_Unit_ID == null) Parent_Unit_ID = "";
            if (StoreType == null) StoreType = "";
            if (OnlyShop == null) OnlyShop = "0";//默认所有都读出
            _ht.Add("unit_code", unit_code);
            _ht.Add("unit_name", unit_name);
            _ht.Add("unit_type_id", unit_type_id);
            _ht.Add("unit_tel", unit_tel);
            _ht.Add("unit_city_id", unit_city_id);
            _ht.Add("unit_status", unit_status);
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount-1);//还需要再减去1
            _ht.Add("Parent_Unit_ID", Parent_Unit_ID);
            _ht.Add("StoreType", StoreType);
            _ht.Add("OnlyShop", OnlyShop);

            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);//还传了CustomerId
            UnitInfo unitInfo = new UnitInfo();

            int iCount = unitService.SearchCount(_ht, loggingSessionInfo.UserID, loggingSessionInfo.ClientID);//查找数量

            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            DataSet ds = new DataSet();
            ds = unitService.SearchList(_ht, loggingSessionInfo.UserID, loggingSessionInfo.ClientID);//查找列表
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfoList = DataTableToObject.ConvertToList<UnitInfo>(ds.Tables[0]);
            }

            unitInfo.ICount = iCount;
            unitInfo.UnitInfoList = unitInfoList;
            //取模
            int mo=iCount%maxRowCount;
            unitInfo.TotalPage = iCount/maxRowCount+(mo==0?0:1);
            return unitInfo;
        }
        #endregion

        #region 单位停用启用
        /// <summary>
        /// 单位停用启用
        /// </summary>
        /// <param name="unit_id">组织标识</param>
        /// <param name="iStatus">状态</param>
        /// <param name="loggingSession">登录model</param>
        /// <returns></returns>
        public bool SetUnitStatus(string unit_id, string iStatus)
        {
            try
            {
                cBillService bs = new cBillService(loggingSessionInfo);
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.Id = unit_id;
                //int iType = 0;
                BillActionType billActionType;
                if (iStatus.Equals("1"))
                {
                    billActionType = BillActionType.Open;
                    unitInfo.Status_Desc = "正常";
                    //iType = 3;
                }
                else
                {
                    billActionType = BillActionType.Stop;
                    unitInfo.Status_Desc = "停用";
                    //iType = 4;
                }
                BillOperateStateService state = bs.ApproveBill(unit_id, "", billActionType);

                if (state == BillOperateStateService.ApproveSuccessful)
                {
                    if (SetUnitTableStatus(unit_id))
                    {
                        UnitInfo u2 = this.GetUnitById(unit_id);
                        if (u2 != null && u2.TypeId == "EB58F1B053694283B2B7610C9AAD2742")
                        {
                            //#if SYN_AP
                            //                            //单位类型是门店， 提交管理平台
                            //                            if (!SetManagerExchangeUnitInfo(loggingSession, unitInfo, iType))
                            //                            {
                            //                                return false;
                            //                            }
                            //#endif
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return DeleteUnit(unit_id, iStatus); //Jermyn20131031
                    //return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region Jermyn20131031删除门店
        public bool DeleteUnit(string unitId, string status)
        {
            UnitInfo unitInfo = new UnitInfo();
            unitInfo.Status = status;
            if (status.Equals("1"))
            {
                unitInfo.Status_Desc = "正常";
            }
            else
            {
                unitInfo.Status_Desc = "停用";
            }
            unitInfo.Id = unitId;
            unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            unitInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
            return unitService.SetUnitTableStatus(unitInfo);
        }

        public bool physicalDeleteUnit(string unitId)
        {
            return unitService.physicalDeleteUnit(unitId);
        }
        #endregion
        /// <summary>
        /// 修改单位状态
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        private bool SetUnitTableStatus(string unit_id)
        {
            try
            {
                //获取要改变的表单信息
                BillModel billInfo = new cBillService(loggingSessionInfo).GetBillById(unit_id);
                //设置要改变的用户信息
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.Status = billInfo.Status;
                unitInfo.Status_Desc = billInfo.BillStatusDescription;
                unitInfo.Id = unit_id;
                unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                unitInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                return unitService.SetUnitTableStatus(unitInfo);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 根据用户获取该用户的所有门店
        /// <summary>
        /// 根据用户获取该用户的所有门店
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UnitInfo> GetUnitListByUserId(string userId)
        {
            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            DataSet ds = new DataSet();
            ds = unitService.GetUnitListByUserId(userId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfoList = DataTableToObject.ConvertToList<UnitInfo>(ds.Tables[0]);
            }
            return unitInfoList;
        }


        #endregion

        #region 获取单个类型的组织（譬如：门店，供应商，客户，渠道，代理商.....)
        /// <summary>
        /// 根据组织类型，获取集合
        /// </summary>
        /// <param name="type_code">类型号码：（门店，供应商，客户，总部，代理商.）</param>
        /// <returns></returns>
        public IList<UnitInfo> GetUnitInfoListByTypeCode(string type_code)
        {
            DataSet ds = new DataSet();
            ds = unitService.GetUnitInfoListByTypeCode(type_code);

            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfoList = DataTableToObject.ConvertToList<UnitInfo>(ds.Tables[0]);
            }
            return unitInfoList;
        }
        #endregion

        #region 中间层
        //#region 商品数据处理
        ///// <summary>
        ///// 获取未打包的Unit数量
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <returns></returns>
        //public int GetUnitNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        //{
        //    //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Unit.SelectUnDownloadCount", "");
        //    return 0;
        //}
        ///// <summary>
        ///// 需要打包的Unit信息
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="maxRowCount">当前页数量</param>
        ///// <param name="startRowIndex">开始数量</param>
        ///// <returns></returns>
        //public IList<UnitInfo> GetUnitListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        //{
        //    Hashtable _ht = new Hashtable();
        //    _ht.Add("StartRow", startRowIndex);
        //    _ht.Add("EndRow", startRowIndex + maxRowCount);
        //    _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
        //    //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UnitInfo>("Unit.SelectUnDownload", _ht);
        //    IList<UnitInfo> unitInfoList = new List<UnitInfo>();
        //    return unitInfoList;
        //}

        ///// <summary>
        ///// 设置打包批次号
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次号</param>
        ///// <param name="UnitInfoList">商品集合</param>
        ///// <returns></returns>
        //public bool SetUnitBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<UnitInfo> UnitInfoList)
        //{
        //    UnitInfo unitInfo = new UnitInfo();
        //    unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        //    unitInfo.Modify_Time = GetCurrentDateTime();
        //    unitInfo.bat_id = bat_id;
        //    unitInfo.UnitInfoList = UnitInfoList;
        //    //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Unit.UpdateUnDownloadBatId", unitInfo);
        //    return true;
        //}
        ///// <summary>
        ///// 更新Unit表打包标识方法
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次标识</param>
        ///// <returns></returns>
        //public bool SetUnitIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id)
        //{
        //    UnitInfo unitInfo = new UnitInfo();
        //    unitInfo.bat_id = bat_id;
        //    unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        //    unitInfo.Modify_Time = GetCurrentDateTime();
        //    //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Unit.UpdateUnDownloadIfFlag", unitInfo);
        //    return false;
        //}
        //#endregion
        #endregion

        #region 获取在线商城 Jermyn20130913
        /// <summary>
        /// 根据类型获取相关门店
        /// </summary>
        /// <param name="unitTypeCode"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public UnitInfo GetUnitByUnitType(string unitTypeCode, string unitId)
        {
            DataSet ds = unitService.GetUnitByUnitType(unitTypeCode, unitId);
            UnitInfo unitInfo = new UnitInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfo = DataTableToObject.ConvertToObject<UnitInfo>(ds.Tables[0].Rows[0]);
            }
            //unitInfo.PropertyList = new PropertyUnitService(this.loggingSessionInfo).GetUnitPropertyListByUnit(unitId);
            return unitInfo;
        }
        public UnitInfo GetUnitByUnitTypeForWX(string unitTypeCode, string unitId)
        {
            DataSet ds = unitService.GetUnitByUnitTypeForWX(unitTypeCode, unitId);
            UnitInfo unitInfo = new UnitInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                unitInfo = DataTableToObject.ConvertToObject<UnitInfo>(ds.Tables[0].Rows[0]);
            }
            //unitInfo.PropertyList = new PropertyUnitService(this.loggingSessionInfo).GetUnitPropertyListByUnit(unitId);
            return unitInfo;
        }
        #endregion

        #region 获取总部门店信息
        /// <summary>
        /// 获取总部门店信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string GetHeadStoreId(string customerId)
        {
            DataSet ds = new DataSet();
            ds = unitService.GetHeadStoreId(customerId);
            string storeId = string.Empty;
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                storeId = ds.Tables[0].Rows[0]["unit_id"].ToString();
            }
            return storeId;
        }
        #endregion


        public IList<VwVipPosOrderEntity> GetPosOrder(string unitId, int top)
        {
            DataSet ds = unitService.GetPosOrder(unitId, top);
            IList<VwVipPosOrderEntity> list = new List<VwVipPosOrderEntity>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VwVipPosOrderEntity>(ds.Tables[0]);
            }
            return list;
        }

        #region 俄丽亚
        public StoreInfo[] FuzzyQueryStores(string pSposition, string pCustomerID, string pLike, string pStoreID, string pDistrictID, bool pIncludeHQ, int? pagesize, int? pageindex)
        {
            List<StoreInfo> list = new List<StoreInfo> { };
            var ds = unitService.FuzzyQueryStores(pLike, pCustomerID, pStoreID, pDistrictID, pIncludeHQ, pageindex ?? 0, pagesize ?? 5);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var store = new StoreInfo()
                {
                    SPosition = pSposition,
                    imageURL = item["ImageURL"].ToString(),
                    lng = item["longitude"].ToString(),
                    lat = item["dimension"].ToString(),
                    storeId = item["unit_id"].ToString(),
                    storeName = item["unit_name"].ToString(),
                    tel = item["unit_tel"].ToString(),
                    address = item["unit_address"].ToString(),
                    brandStory = item["unitPT"].ToString(),
                    brandRelation = item["unitRD"].ToString(),
                    description = item["unit_remark"].ToString(),
                    typeCode = item["type_code"].ToString()
                };
                var imagelist = ds.Tables[1].AsEnumerable()
                    .Where(t => t["ObjectId"].ToString() == store.storeId)
                    .Select(t => new ImageInfo { imageUrl = t["ImageURL"].ToString(), imageId = t["ImageId"].ToString() });
                store.imageList = imagelist.ToArray();
                list.Add(store);
            }
            var tempitem = list.Find(t => t.typeCode == "总部");
            if (tempitem != null)
            {
                list.Remove(tempitem);
                list.Insert(0, tempitem);
            }
            return list.ToArray();
        }
        /// <summary>
        /// Add by Alex Tian 2014-04-14
        /// </summary>
        /// <param name="NameLike"></param>
        /// <param name="CityName"></param>
        /// <param name="Position"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="StoreID"></param>
        /// <param name="IncludeHQ"></param>
        /// <returns></returns>
        public StoreListInfo[] FuzzyQueryStores(string pNameLike, string pCityCode, string pPosition, int? pPageIndex, int? pPageSize, string pStoreID, bool? pIncludeHQ, string pCustomerID, double RangeAccessoriesStores)
        {
            List<StoreListInfo> list = new List<StoreListInfo> { };
            var ds = new DataSet();
            if (pCityCode == "-99") //查询全部
                ds = unitService.FuzzyQueryStoresALL(pNameLike, pCityCode, pPosition, pPageIndex ?? 0, pPageSize ?? 15, pStoreID, pIncludeHQ ?? true, pCustomerID);
            else if (pCityCode == "-00") //查询附近
            {
                ds = unitService.FuzzyQueryStoresNearby(pNameLike, pCityCode, pPosition, pPageIndex ?? 0, pPageSize ?? 15, pStoreID, pIncludeHQ ?? true, pCustomerID, RangeAccessoriesStores);
            }
            else
                //按城市查询
                ds = unitService.FuzzyQueryStoresCity(pNameLike, pCityCode, pPosition, pPageIndex ?? 0, pPageSize ?? 15, pStoreID, pIncludeHQ ?? true, pCustomerID, RangeAccessoriesStores);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var store = new StoreListInfo()
                {
                    SPosition = pPosition,
                    ImageURL = item["ImageURL"].ToString(),
                    StoreID = item["unit_id"].ToString(),
                    StoreName = item["unit_name"].ToString(),
                    Tel = item["unit_tel"].ToString(),
                    Address = item["unit_address"].ToString(),
                    BrandStory = item["unitPT"].ToString(),
                    BrandRelation = item["unitRD"].ToString(),
                    Description = item["unit_remark"].ToString(),
                    TypeCode = item["type_code"].ToString(),
                    Distance = pCityCode == "-00" ? (item["distance"] != DBNull.Value ? Convert.ToDecimal(item["distance"]) : 0) : 0
                };
                var imagelist = ds.Tables[1].AsEnumerable()
                    .Where(t => t["ObjectId"].ToString() == store.StoreID)
                    .Select(t => new imageInfo
                    {
                        ImageUrl = t["ImageURL"].ToString(),
                        ImageId
                            = t["ImageId"].ToString()
                    });
                store.ImageList = imagelist.ToArray();
                list.Add(store);
            }
            return list.ToArray();
        }

        public StoreInfo[] GetRecentlyUsedStore(string pSposition, string pCustomerid, string pUserid, string pOpenid)
        {
            List<StoreInfo> list = new List<StoreInfo> { };
            var ds = unitService.GetRecentlyUsedStore(pCustomerid, pUserid, pOpenid);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var store = new StoreInfo()
                {
                    SPosition = pSposition,
                    imageURL = item["ImageURL"].ToString(),
                    lng = item["longitude"].ToString(),
                    lat = item["dimension"].ToString(),
                    storeId = item["unit_id"].ToString(),
                    storeName = item["unit_name"].ToString(),
                    tel = item["unit_tel"].ToString(),
                    address = item["unit_address"].ToString()
                };
                list.Add(store);
            }
            return list.ToArray();
        }
        #endregion

        #region 获取客户行业类型
        public string GetCustomerDataDeploy(string pCustomerID)
        {
            DataSet ds = this.unitService.GetCustomerDataDeploy(pCustomerID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
                return null;
        }
        #endregion

        #region

        public class UnitInfoRD : IAPIResponseData
        {
            public string unit_ID { get; set; }
            public string unit_name { get; set; }
        }

        #endregion

        #region 导入门店信息
        /// <summary>
        /// excel导入数据库
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="CurrentUserInfo"></param>
        public DataSet ExcelToDb(string strPath, LoggingSessionInfo CurrentUserInfo)
        {

            DataSet ds; //要插入的数据  
            DataSet dsResult = new DataSet(); //要插入的数据  
            DataTable dt;

            DataTable table = new DataTable("Error");
            //获取列集合,添加列  
            DataColumnCollection columns = table.Columns;
            columns.Add("ErrMsg", typeof(String));


            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + strPath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn); //连接excel              
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
            conn.Open();    //外部表不是预期格式，不兼容2010的excel表结构  
            string s = conn.State.ToString();
            OleDbDataAdapter myCommand = null;
            ds = null;

            string strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, conn);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();
            try
            {
                dt = ds.Tables[0];
                string connString = loggingSessionInfo.CurrentLoggingManager.Connection_String;// System.Configuration.ConfigurationManager.AppSettings["Conn_alading"]; //@"user id=dev;password=JtLaxT7668;data source=182.254.219.83,3433;database=cpos_bs_alading;";   //连接数据库的路径方法  
                SqlConnection connSql = new SqlConnection(connString);
                connSql.Open();
                DataRow dr = null;
                int C_Count = dt.Columns.Count;//获取列数  
                if (C_Count == 13)
                {
                    DataTable dtUnit = new DataTable();
                    dtUnit.Columns.Add("UnitType", typeof(string));
                    dtUnit.Columns.Add("UnitName", typeof(string));
                    dtUnit.Columns.Add("UnitCode", typeof(string));
                    dtUnit.Columns.Add("UnitContact", typeof(string));
                    dtUnit.Columns.Add("UnitTel", typeof(string));
                    dtUnit.Columns.Add("UintSuperiors", typeof(string));
                    dtUnit.Columns.Add("UnitProvince", typeof(string));
                    dtUnit.Columns.Add("UnitCity", typeof(string));
                    dtUnit.Columns.Add("UnitDistrict", typeof(string));
                    dtUnit.Columns.Add("UnitAddress", typeof(string));
                    dtUnit.Columns.Add("Longitude", typeof(string));
                    dtUnit.Columns.Add("Dimension", typeof(string));
                    dtUnit.Columns.Add("UnitRemark", typeof(string));
                    dtUnit.Columns.Add("CustomerId", typeof(string));
                    dtUnit.Columns.Add("CreateUserId", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++)  //记录表中的行数，循环插入  
                    {
                        dr = dt.Rows[i];
                        DataRow dr_Unit = dtUnit.NewRow();

                        if (dr[0].ToString() != "" && dr[1].ToString() != "")
                        {
                            //this.unitService.insertToSql(dr, C_Count, connSql, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
                            dr_Unit["UnitType"] = dr[0].ToString();
                            dr_Unit["UnitName"] = dr[1].ToString();
                            dr_Unit["UnitCode"] = dr[2].ToString();
                            dr_Unit["UnitContact"] = dr[3].ToString();
                            dr_Unit["UnitTel"] = dr[4].ToString();
                            dr_Unit["UintSuperiors"] = dr[5].ToString();
                            dr_Unit["UnitProvince"] = dr[6].ToString();
                            dr_Unit["UnitCity"] = dr[7].ToString();
                            dr_Unit["UnitDistrict"] = dr[8].ToString();
                            dr_Unit["UnitAddress"] = dr[9].ToString();
                            dr_Unit["Longitude"] = dr[10].ToString();
                            dr_Unit["Dimension"] = dr[11].ToString();
                            dr_Unit["UnitRemark"] = dr[12].ToString();
                            dr_Unit["CustomerId"] = CurrentUserInfo.ClientID;
                            dr_Unit["CreateUserId"] = CurrentUserInfo.UserID;

                            dtUnit.Rows.Add(dr_Unit);
                        }
                    }
                    Utils.SqlBulkCopy(connString, dtUnit, "ImportUnitTemp");
                    connSql.Close();
                    //临时表导入正式表
                    dsResult = this.unitService.ExcelImportToDB(CurrentUserInfo.ClientID);
                }
                else
                {

                    DataRow row = table.NewRow();
                    row["ErrMsg"] = "模板列数不对";
                    table.Rows.Add(row);
                    dsResult.Tables.Add(table);

                    DataTable tableCount = new DataTable("Count");
                    DataColumnCollection columns1 = tableCount.Columns;
                    columns1.Add("TotalCount", typeof(Int16));
                    columns1.Add("ErrCount", typeof(Int16));
                    row = tableCount.NewRow();
                    row["TotalCount"] = "0";
                    row["ErrCount"] = dt.Rows.Count.ToString();
                    tableCount.Rows.Add(row);
                    dsResult.Tables.Add(tableCount);
                }
            }
            catch (Exception ex)
            {
                dsResult = new DataSet();
                DataRow row = table.NewRow();
                row["ErrMsg"] = ex.Message.ToString();
                table.Rows.Add(row);
                dsResult.Tables.Add(table);

                DataTable tableCount = new DataTable("Count");
                DataColumnCollection columns1 = tableCount.Columns;
                columns1.Add("TotalCount", typeof(Int16));
                columns1.Add("ErrCount", typeof(Int16));
                row = tableCount.NewRow();
                row["TotalCount"] = "0";
                row["ErrCount"] = "0";
                tableCount.Rows.Add(row);
                dsResult.Tables.Add(tableCount);
            }

            return dsResult;
        }
            
        

        #endregion

    }
}
