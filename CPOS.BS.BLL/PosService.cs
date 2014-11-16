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

namespace JIT.CPOS.BS.BLL
{
    public class PosService : BaseService
    {
        JIT.CPOS.BS.DataAccess.PosService posService = null;
        #region 构造函数
        public PosService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            posService = new DataAccess.PosService(loggingSessionInfo);
        }
        #endregion

        #region 终端类型
        /// <summary>
        /// 查询终端类型列表
        /// </summary>
        /// <returns></returns>
        public IList<PosTypeInfo> SelectPostTypeList()
        {
            IList<PosTypeInfo> posTypeInfoList = new List<PosTypeInfo>();
            DataSet ds = new DataSet();
            ds = posService.SelectPostTypeList();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                posTypeInfoList = DataTableToObject.ConvertToList<PosTypeInfo>(ds.Tables[0]);
            }
            return posTypeInfoList;
        }
        #endregion

        #region 终端
        /// <summary>
        /// 检查终端编码是否已经存在
        /// </summary>
        /// <param name="posID">如果是校验一个已经存在的终端,则传入该终端的ID,否则为空</param>
        /// <param name="posCode">终端编码</param>
        /// <returns></returns>
        public bool ExistPosCode(string posID, string posCode)
        {
            return posService.ExistPosCode(posID, posCode);
        }

        /// <summary>
        /// 检查终端序列号是否已经存在
        /// </summary>
        /// <param name="posID">如果是校验一个已经存在的终端,则传入该终端的ID,否则为空</param>
        /// <param name="posSN">终端序列号</param>
        /// <returns></returns>
        public bool ExistPosSN(string posID, string posSN)
        {
            return posService.ExistPosSN(posID, posSN);
        }

        /// <summary>
        /// 插入一个终端
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="pos">终端信息</param>
        /// <returns></returns>
        public bool InsertPos(PosInfo pos)
        {
            if (string.IsNullOrEmpty(pos.ID))
            {
                pos.ID = this.NewGuid();
            }
            pos.CreateUserID = loggingSessionInfo.CurrentUser.User_Id;
            pos.CreateUserName = loggingSessionInfo.CurrentUser.User_Name;

            bool bReturn = posService.InsertPos(pos);
            if (bReturn) {
                DataSet ds = new DataSet();
                ds = posService.GetPosInfoById(pos.ID);
                
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    pos = DataTableToObject.ConvertToObject<PosInfo>(ds.Tables[0].Rows[0]);
                    //交至管理平台
                    this.synPosToAP(this.loggingSessionInfo.CurrentLoggingManager.Customer_Id, 1, pos);
                }
            }

            return true;
        }

        /// <summary>
        /// 修改一个终端
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="pos">终端信息</param>
        /// <returns></returns>
        public bool ModifyPos(PosInfo pos)
        {
            if (posService.UpdatePos(pos))
            {
                //提交至管理平台
                return this.synPosToAP(this.loggingSessionInfo.CurrentLoggingManager.Customer_Id, 2, pos);
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// 修改一个终端的版本
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="posSN">序列号</param>
        /// <param name="appVersion">程序版本</param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        public bool ModifyPosVersion(LoggingSessionInfo loggingSession, string posSN, string appVersion, string dbVersion)
        {
            return posService.ModifyPosVersion(posSN,appVersion,dbVersion);
        }

        private bool synPosToAP(string customerID, int type, PosInfo pos)
        {
//#if SYN_AP
            //提交至管理平台
            //cPos.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService service = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
            //service.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";
            //this.Log(LogLevel.DEBUG, "bs", "service", "synPosToAP", "url", service.Url);
            //string s = cXMLService.Serialiaze(pos);
            //this.Log(LogLevel.DEBUG, "bs", "service", "synPosToAP", "pos", s);
            //bool ret = service.SynTerminal(customerID, type, s);
            //return ret;
//#else
            return true;
//#endif
        }

        /// <summary>
        /// 根据终端ID，获取终端信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="posID">终端ID</param>
        /// <returns></returns>
        public PosInfo GetPosByID(LoggingSessionInfo loggingSession, string posID)
        {
            PosInfo pos = new PosInfo();
            DataSet ds = new DataSet();
            ds = posService.GetPosInfoById(posID);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                pos = DataTableToObject.ConvertToObject<PosInfo>(ds.Tables[0].Rows[0]);
            }
            return pos;
        }

        /// <summary>
        /// 添加从管理平台发过来的终端
        /// </summary>
        /// <param name="pos">终端信息</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public bool InsertPosFromAP(PosInfo pos, string customerID)
        {
            //LoggingSessionInfo ls = new LoggingSessionInfo();
            //ls.CurrentLoggingManager = new cLoggingManager().GetLoggingManager(customerID);
            ////未找到,则插入,否则修改
            //PosInfo pp = this.GetPosByID(ls, pos.ID);
            //if (pp != null)
            //{
            //    int ret = cSqlMapper.Instance(ls.CurrentLoggingManager).Update("Pos.Pos.UpdatePosFromAP", pos);
            //    return ret == 1;
            //}
            //else
            //{
            //    cSqlMapper.Instance(ls.CurrentLoggingManager).Insert("Pos.Pos.InsertPosFromAP", pos);

            //    return true;
            //}
            return false;
        }

        /// <summary>
        /// 修改从管理平台发过来的终端
        /// </summary>
        /// <param name="pos">终端信息</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public bool ModifyPosFromAP(PosInfo pos, string customerID)
        {
            //LoggingSessionInfo ls = new LoggingSessionInfo();
            //ls.CurrentLoggingManager = new cLoggingManager().GetLoggingManager(customerID);
            ////未找到,则插入,否则修改
            //PosInfo pp = this.GetPosByID(ls, pos.ID);
            //if (pp == null)
            //{
            //    cSqlMapper.Instance(ls.CurrentLoggingManager).Insert("Pos.Pos.InsertPosFromAP", pos);
            //    return true;
            //}
            //else
            //{
            //    int ret = cSqlMapper.Instance(ls.CurrentLoggingManager).Update("Pos.Pos.UpdatePosFromAP", pos);
            //    return ret == 1;
            //}
            return false;
        }

        /// <summary>
        /// 获取满足查询条件的终端的记录总数
        /// </summary>
        /// <param name="condition">HoldType:终端持有方式；Type：终端类型；Code：终端编码；SN：终端序列号；PurchaseDateBegin：起始购买日期；PurchaseDateEnd：终止购买日期；InsuranceDateBegin：起始出保日期；InsuranceDateEnd：起始出保日期；</param>
        /// <returns></returns>
        public int SelectPosListCount(string HoldType
                                    , string Type
                                    , string Code
                                    , string SN
                                    , string PurchaseDateBegin
                                    , string PurchaseDateEnd
                                    , string InsuranceDateBegin
                                    , string InsuranceDateEnd)
        {
            return posService.SelectPosListCount(HoldType,Type,Code,SN,PurchaseDateBegin,PurchaseDateEnd,InsuranceDateBegin,InsuranceDateEnd);
        }

        /// <summary>
        /// 获取满足查询条件的终端的某页上的所有终端
        /// </summary>
        /// <param name="condition">HoldType:终端持有方式；Type：终端类型；Code：终端编码；SN：终端序列号；PurchaseDateBegin：起始购买日期；PurchaseDateEnd：终止购买日期；InsuranceDateBegin：起始出保日期；InsuranceDateEnd：起始出保日期；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的终端的列表</returns>
        public IList<PosInfo> SelectPosList(string HoldType
                                    , string Type
                                    , string Code
                                    , string SN
                                    , string PurchaseDateBegin
                                    , string PurchaseDateEnd
                                    , string InsuranceDateBegin
                                    , string InsuranceDateEnd, int maxRowCount, int startRowIndex)
        {
            IList<PosInfo> posInfoList = new List<PosInfo>();
            DataSet ds = new DataSet();
            ds = posService.SelectPosList(HoldType
                                        , Type
                                        , Code
                                        , SN
                                        , PurchaseDateBegin
                                        , PurchaseDateEnd
                                        , InsuranceDateBegin
                                        , InsuranceDateEnd
                                        , startRowIndex
                                        , startRowIndex + maxRowCount);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                posInfoList = DataTableToObject.ConvertToList<PosInfo>(ds.Tables[0]);
            }
            return posInfoList;
        }

        /// <summary>
        /// 判断当前客户下能否新建一个终端
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <returns></returns>
        public bool CanCreatePos(LoggingSessionInfo loggingSession)
        {
            //调用管理平台
            //cPos.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService service = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
            //service.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";
            //bool ret = service.CanAddTerminal(loggingSession.CurrentLoggingManager.Customer_Id);
            //return ret;
            return true;
        }
        #endregion

        #region 终端与门店的关系
        /// <summary>
        /// 查询终端与门店的关系列表
        /// </summary>
        /// <param name="condition">UnitID:门店ID;UnitName:门店编码;UnitCode:门店编码;PosID:终端ID;PosCode:终端编码;PosSN:终端序列号;</param>
        /// <returns></returns>
        public IList<PosUnitInfo> SelectPosUnitList(LoggingSessionInfo loggingSessionInfo,Hashtable condition)
        {
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<PosUnitInfo>("Pos.PosUnit.SelectPosUnitList", condition);
            IList<PosUnitInfo> posUnitInfoList = new List<PosUnitInfo>();

            return posUnitInfoList;
        }

        /// <summary>
        /// 获取终端的编号
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="posSN">终端序列号</param>
        /// <returns></returns>
        public string GetPosNo(string customerID, string unitID, string posSN)
        {
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "cid", customerID);
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "uid", unitID);
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "pid", posSN);
             
            //if (string.IsNullOrEmpty(customerID))
            //    throw new ArgumentNullException("customerID");
            //if (string.IsNullOrEmpty(unitID))
            //    throw new ArgumentNullException("unitID");
            //if (string.IsNullOrEmpty(posSN))
            //    throw new ArgumentNullException("posSN");

            //string pos_no = "";
            //bool is_new_pos = false;
            //PosInfo pos = null;
            ////从POS的序列号中取第一位作为POS的类型
            //string pos_type = posSN.Substring(0, 1);
            //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customerID);
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "cs", loggingManager.Connection_String);
            //IBatisNet.DataMapper.ISqlMapper sqlMpper =  cSqlMapper.Instance(loggingManager);
            //try
            //{
            //    sqlMpper.BeginTransaction();

            //    //根据终端序列号，在终端表中查找有没有被登记过
            //    pos = sqlMpper.QueryForObject<PosInfo>("Pos.Pos.SelectPosBySN", posSN);
            //    //如果没有被登记过，则插入终端信息
            //    if (pos == null)
            //    {
            //        is_new_pos = true;
            //        pos = new PosInfo();
            //        pos.ID = this.NewGuid();
            //        pos.Type = pos_type;
            //        pos.SN = posSN;
            //        sqlMpper.Insert("Pos.Pos.InsertPosFromCT", pos);
            //        //重新查询出
            //        pos = sqlMpper.QueryForObject<PosInfo>("Pos.Pos.SelectPosBySN", posSN);
            //    }
            //    PosUnitInfo pos_unit = new PosUnitInfo(pos);
            //    pos_unit.ID = this.NewGuid();
            //    pos_unit.Unit.Id = unitID;
            //    //如果不存在终端与门店的关系，则插入，并产生编号
            //    sqlMpper.Insert("Pos.PosUnit.Insert", pos_unit);
            //    //取终端编号
            //    pos_no = sqlMpper.QueryForObject<string>("Pos.PosUnit.GetPosNoByUnitIDAndPosID", pos_unit);

            //    sqlMpper.CommitTransaction();
            //}
            //catch(Exception ex)
            //{
            //    sqlMpper.RollBackTransaction();
            //    throw ex;
            //}

            //如果是新的终端，则传入管理平台
            //if (is_new_pos)
            //{
            //    //提交至管理平台
            //    this.Log(LogLevel.DEBUG, "bs", "service", "GetPosNo", "is_new_pos", "true");
            //    this.synPosToAP(customerID, 1, pos);
            //}
            //return pos_no;
            return "";
        }

        public string GetPosNo(string customerID, string unitID, string posSN,bool IsTran)
        {
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "cid", customerID);
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "uid", unitID);
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "pid", posSN);

            //if (string.IsNullOrEmpty(customerID))
            //    throw new ArgumentNullException("customerID");
            //if (string.IsNullOrEmpty(unitID))
            //    throw new ArgumentNullException("unitID");
            //if (string.IsNullOrEmpty(posSN))
            //    throw new ArgumentNullException("posSN");

            //string pos_no = "";
            //bool is_new_pos = false;
            //PosInfo pos = null;
            ////从POS的序列号中取第一位作为POS的类型
            //string pos_type = posSN.Substring(0, 1);
            //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customerID);
            //this.Log(LogLevel.DEBUG, "", "", "GetPosNo", "cs", loggingManager.Connection_String);
            //IBatisNet.DataMapper.ISqlMapper sqlMpper = cSqlMapper.Instance(loggingManager);
            //try
            //{
            //    if(IsTran) sqlMpper.BeginTransaction();

            //    //根据终端序列号，在终端表中查找有没有被登记过
            //    pos = sqlMpper.QueryForObject<PosInfo>("Pos.Pos.SelectPosBySN", posSN);
            //    //如果没有被登记过，则插入终端信息
            //    if (pos == null)
            //    {
            //        is_new_pos = true;
            //        pos = new PosInfo();
            //        pos.ID = this.NewGuid();
            //        pos.Type = pos_type;
            //        pos.SN = posSN;
            //        sqlMpper.Insert("Pos.Pos.InsertPosFromCT", pos);
            //        //重新查询出
            //        //pos = sqlMpper.QueryForObject<PosInfo>("Pos.Pos.SelectPosBySN", posSN);
            //    }
            //    PosUnitInfo pos_unit = new PosUnitInfo(pos);
            //    pos_unit.ID = this.NewGuid();
            //    pos_unit.Unit.Id = unitID;
            //    pos_unit.Pos = pos;
            //    //如果不存在终端与门店的关系，则插入，并产生编号
            //    sqlMpper.Insert("Pos.PosUnit.Insert", pos_unit);
            //    //取终端编号
            //    pos_no = sqlMpper.QueryForObject<string>("Pos.PosUnit.GetPosNoByUnitIDAndPosID", pos_unit);

            //    if (IsTran) sqlMpper.CommitTransaction();
            //}
            //catch (Exception ex)
            //{
            //    if (IsTran) sqlMpper.RollBackTransaction();
            //    throw ex;
            //}

            ////如果是新的终端，则传入管理平台
            ////if (is_new_pos)
            ////{
            ////    //提交至管理平台
            ////    this.Log(LogLevel.DEBUG, "bs", "service", "GetPosNo", "is_new_pos", "true");
            ////    this.synPosToAP(customerID, 1, pos);
            ////}
            //return pos_no;
            return "";
        }
        #endregion

        #region 仓库
        /// <summary>
        /// 根据仓库ID，获取仓库信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="warehouseID">仓库ID</param>
        /// <returns></returns>
        public WarehouseInfo GetWarehouseByID(LoggingSessionInfo loggingSession, string warehouseID)
        {
            WarehouseInfo warehouseInfo = new WarehouseInfo();
            DataSet ds = new DataSet();
            ds = posService.GetWarehouseByID(warehouseID);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                warehouseInfo = DataTableToObject.ConvertToObject<WarehouseInfo>(ds.Tables[0].Rows[0]);
            }
            return warehouseInfo;
        }

        

        /// <summary>
        /// 插入一个仓库
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="warehouse">仓库信息</param>
        /// <returns></returns>
        //public bool InsertWarehouse(LoggingSessionInfo loggingSession, WarehouseInfo warehouse)
        //{
        //    //if (string.IsNullOrEmpty(warehouse.ID))
        //    //{
        //    //    warehouse.ID = this.NewGuid();
        //    //}
        //    //warehouse.CreateUserID = loggingSession.CurrentUser.User_Id;
        //    //warehouse.CreateUserName = loggingSession.CurrentUser.User_Name;

        //    //UnitWarehouseInfo unit_warehouse = new UnitWarehouseInfo(warehouse.Unit, warehouse);
        //    //unit_warehouse.ID = this.NewGuid();

        //    ////保存
        //    //ISqlMapper sqlMapper = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
        //    //try
        //    //{
        //    //    sqlMapper.BeginTransaction();
        //    //    //添加仓库
        //    //    sqlMapper.Insert("Pos.Warehouse.Insert", warehouse);
        //    //    //添加仓库与单位的关系
        //    //    sqlMapper.Update("Pos.UnitWarehouse.Insert", unit_warehouse);

        //    //    sqlMapper.CommitTransaction();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    sqlMapper.RollBackTransaction();
        //    //    throw ex;
        //    //}
        //    return true;
        //}

        //public bool InsertWarehouse(LoggingManager loggingManager, WarehouseInfo warehouse)
        //{
        //    //if (string.IsNullOrEmpty(warehouse.ID))
        //    //{
        //    //    warehouse.ID = this.NewGuid();
        //    //}
            

        //    //UnitWarehouseInfo unit_warehouse = new UnitWarehouseInfo(warehouse.Unit, warehouse);
        //    //unit_warehouse.ID = this.NewGuid();

        //    ////保存
        //    //ISqlMapper sqlMapper = cSqlMapper.Instance(loggingManager);
        //    //try
        //    //{
        //    //    sqlMapper.BeginTransaction();
        //    //    //添加仓库
        //    //    sqlMapper.Insert("Pos.Warehouse.Insert", warehouse);
        //    //    //添加仓库与单位的关系
        //    //    sqlMapper.Update("Pos.UnitWarehouse.Insert", unit_warehouse);

        //    //    sqlMapper.CommitTransaction();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    sqlMapper.RollBackTransaction();
        //    //    throw ex;
        //    //}
        //    return true;
        //}

        /// <summary>
        /// 插入仓库信息
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public bool InsertWarehouse(WarehouseInfo warehouse,bool IsTran=true)
        {
            if (string.IsNullOrEmpty(warehouse.warehouse_id))
            {
                warehouse.warehouse_id = this.NewGuid();
            }
            UnitWarehouseInfo unit_warehouse = new UnitWarehouseInfo(warehouse.Unit, warehouse);
            unit_warehouse.ID = this.NewGuid();
            string strError = string.Empty;
            bool b = posService.InsertWarehouse(warehouse, unit_warehouse, out strError, IsTran);
            if (!b) {
                throw (new System.Exception(strError));
            }
            return b;
        }

        /// <summary>
        /// 修改一个仓库
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="warehouse">仓库信息</param>
        /// <returns></returns>
        public bool ModifyWarehouse( WarehouseInfo warehouse)
        {
            //保存

            UnitWarehouseInfo unit_warehouse = new UnitWarehouseInfo(warehouse.Unit, warehouse);
            unit_warehouse.ID = this.NewGuid();

            warehouse.ModifyUserID = loggingSessionInfo.CurrentUser.User_Id;
            warehouse.ModifyUserName = loggingSessionInfo.CurrentUser.User_Name;
            string strError = string.Empty;
            bool bReturn = posService.ModifyWarehouse(warehouse, unit_warehouse, out strError);
            
            return bReturn;
        }


        /// <summary>
        /// 获取满足查询条件的仓库的记录总数
        /// </summary>
        /// <param name="condition">UnitName:所属单位名称；Code：仓库编码；Name：仓库名称；Contacter：仓库联系人；Tel：仓库电话；Status：仓库状态；</param>
        /// <returns></returns>
        public int SelectWarehouseListCount(Hashtable condition)
        {
            return posService.SelectWarehouseListCount(condition);
        }

        /// <summary>
        /// 获取满足查询条件的仓库的某页上的所有仓库
        /// </summary>
        /// <param name="condition">UnitName:所属单位名称；Code：仓库编码；Name：仓库名称；Contacter：仓库联系人；Tel：仓库电话；Status：仓库状态；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的终端的列表</returns>
        public IList<WarehouseInfo> SelectWarehouseList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);
            IList<WarehouseInfo> warehouseInfoList = new List<WarehouseInfo>();
            DataSet ds = new DataSet();
            ds = posService.SelectWarehouseList(condition);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                warehouseInfoList = DataTableToObject.ConvertToList<WarehouseInfo>(ds.Tables[0]);
            }
            return warehouseInfoList;

        }

        /// <summary>
        /// 获取满足查询条件的仓库的某页上的所有仓库
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="condition">UnitID:所属单位ID；Code：仓库编码；Name：仓库名称；Contacter：仓库联系人；Tel：仓库电话；Status：仓库状态；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的终端的列表</returns>
        //public cPos.Model.Exchange.SelectObjectResultsInfo<WarehouseQueryInfo> SearchWarehouseList(
        //    LoggingSessionInfo loggingSession, Hashtable condition, int maxRowCount, int startRowIndex)
        //{
        //    condition.Add("StartRow", startRowIndex);
        //    condition.Add("EndRow", startRowIndex + maxRowCount);
        //    condition.Add("MaxRowCount", maxRowCount);

        //    cPos.Model.Exchange.SelectObjectResultsInfo<WarehouseQueryInfo> ret = new cPos.Model.Exchange.SelectObjectResultsInfo<WarehouseQueryInfo>();

        //    ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
        //    try
        //    {
        //        sqlMap.BeginTransaction();
        //        //插入满足条件的单位进临时表
        //        cPos.Model.Unit.UnitQueryCondition unitQueryCondition = new Model.Unit.UnitQueryCondition();
        //        if (condition.Contains("UnitID"))
        //        {
        //            unitQueryCondition.SuperUnitIDs.Add(condition["UnitID"].ToString());
        //        }
        //        condition.Add("UnitSQL", this.GenInsertUnitTemporaryTableSQL(loggingSession, unitQueryCondition));
        //        //查某页上的通告
        //        ret.Data = sqlMap.QueryForList<WarehouseQueryInfo>("Pos.Warehouse.SelectWarehouseList2", condition);
        //        sqlMap.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlMap.RollBackTransaction();
        //        throw ex;
        //    }

        //    return ret;
        //}

        /// <summary>
        /// 启用一个仓库
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="warehouseID">仓库ID</param>
        /// <returns></returns>
        public bool EnableWarehouse(LoggingSessionInfo loggingSession, string warehouseID)
        {
            return posService.EnableWarehouse(warehouseID,"1");
        }

        /// <summary>
        /// 停用一个仓库
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="warehouseID">仓库ID</param>
        /// <returns></returns>
        public bool DisableWarehouse(LoggingSessionInfo loggingSession, string warehouseID)
        {
            return posService.EnableWarehouse(warehouseID, "-1");
        }

        /// <summary>
        /// 查询某个单位下的仓库列表
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="unitID">单位ID</param>
        /// <returns></returns>
        public IList<WarehouseInfo> GetWarehouseByUnitID( string unitID)
        {
            WarehouseService warehouseService = new WarehouseService(loggingSessionInfo);
            return warehouseService.GetWarehouseByUnitID(unitID);
        }

        #endregion
    }
}
