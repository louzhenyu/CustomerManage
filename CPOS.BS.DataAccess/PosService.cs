using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.CPOS.BS.Entity.Pos;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class PosService : Base.BaseCPOSDAO
    {
         #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public PosService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 终端类型
        /// <summary>
        /// 获取所有终端类型
        /// </summary>
        /// <returns></returns>
        public DataSet SelectPostTypeList()
        {
            DataSet ds = new DataSet();
            string sql = "select * from t_pos_type order by pos_type_code";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 终端
        /// <summary>
        /// 判断是否存在相同的终端号码
        /// </summary>
        /// <param name="pos_id"></param>
        /// <param name="pos_code"></param>
        /// <returns></returns>
        public bool ExistPosCode(string pos_id, string pos_code)
        {
            string sql = "select count(*) from t_pos where pos_code='"+pos_code+"' ";
            if (pos_id != null && !pos_id.Equals(""))
            {
                sql = sql + "pos_id != '" + pos_id + "'";
            }

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            int i = 0;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                 i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            return i>0;
        }

        /// <summary>
        /// 检查终端序列号是否已经存在
        /// </summary>
        /// <param name="posID">如果是校验一个已经存在的终端,则传入该终端的ID,否则为空</param>
        /// <param name="posSN">终端序列号</param>
        /// <returns></returns>
        public bool ExistPosSN(string pos_id, string posSN)
        {
            string sql = "select count(*) from t_pos where pos_sn='" + posSN + "' ";
            if (pos_id != null && !pos_id.Equals(""))
            {
                sql = sql + "pos_id != '" + pos_id + "'";
            }

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            int i = 0;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            return i > 0;
        }
        /// <summary>
        /// 插入POS信息
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool InsertPos(PosInfo pos)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    string strError = string.Empty;
                    //插入仓库
                    if (!SetPosInsert(pos, tran))
                    {
                        strError = "插入终端失败";
                        throw (new System.Exception(strError));
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw (ex);
                }
            }
        }
        /// <summary>
        /// 修改pos信息
        /// </summary>
        /// <param name="posInfo"></param>
        /// <returns></returns>
        public bool UpdatePos(PosInfo posInfo)
        {
            try
            {
                #region
                string sql = " update t_pos set "
                              + " pos_type = '"+posInfo.Type+"', "
                              + " pos_brand = '"+posInfo.Brand+"', "
                              + " pos_model = '"+posInfo.Model+"', "
                              + " pos_code = '"+posInfo.Code+"', "
                              + " pos_sn = '"+posInfo.SN+"', "
                              + " pos_purchase_date = '"+posInfo.PurchaseDate+"', "
                              + " pos_insurance_date = '"+posInfo.InsuranceDate+"', "
                              + " pos_ws = '"+posInfo.WS+"', "
                              + " pos_ws2 = '"+posInfo.WS2+"', "
                              + " pos_db_version = '"+posInfo.DBVersion+"', "
                              + " pos_software_version = '"+posInfo.SoftwareVersion+"', "
                              + " pos_have_cashbox = '"+posInfo.HaveCashbox+"', "
                              + " pos_cashbox_no = '"+posInfo.CashboxNo+"', "
                              + " pos_have_ecard = '"+posInfo.HaveEcard+"', "
                              + " pos_ecard_no = '"+posInfo.EcardNo+"', "
                              + " pos_have_scanner = '"+posInfo.HaveScanner+"', "
                              + " pos_scanner_no = '"+posInfo.ScannerNo+"', "
                              + " pos_have_client_display = '"+posInfo.HaveClientDisplay+"', "
                              + " pos_client_display_no = '"+posInfo.ClientDisplayNo+"', "
                              + " pos_have_printer = '"+posInfo.HavePrinter+"', "
                              + " pos_printer_no = '"+posInfo.PrinterNo+"', "
                              + " pos_have_other_device = '"+posInfo.HaveOtherDevice+"', "
                              + " pos_other_device_no = '"+posInfo.OtherDeviceNo+"', "
                              + " pos_have_holder = '"+posInfo.HaveHolder+"', "
                              + " pos_holder_no = '"+posInfo.HolderNo+"', "
                              + " pos_remark = '"+posInfo.Remark+"', "
                              + " modify_user_id= '"+posInfo.ModifyUserID+"', "
                              + " modify_user_name = '"+posInfo.ModifyUserName+"', "
                              + " modify_time = getdate() "
                              + " where pos_id = '" + posInfo.ID + "'";
                #endregion
                
                this.SQLHelper.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 修改一个终端的版本
        /// </summary>
        /// <param name="posSN"></param>
        /// <param name="appVersion"></param>
        /// <param name="dbVersion"></param>
        /// <returns></returns>
        public bool ModifyPosVersion(string posSN, string appVersion, string dbVersion)
        {
            try
            {
                string sql = "update t_pos set "
                          + " pos_db_version = '" + dbVersion + "', "
                          + " pos_software_version = '" + appVersion + "'"
                          + " modify_user_id= '"+ loggingSessionInfo.CurrentUser.User_Id +"', "
                          + " modify_user_name = '"+ loggingSessionInfo.CurrentUser.User_Name +"', "
                          + " modify_time = getdate() "
                          + " where pos_sn = '" + posSN + "'";
                this.SQLHelper.ExecuteScalar(sql);
                return true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        private bool SetPosInsert(PosInfo posInfo,IDbTransaction pTran)
        {
            #region
            string sql = " insert into t_pos(pos_id, pos_hold_type, pos_type, pos_brand, pos_model, pos_code, pos_sn, "
                      + " pos_purchase_date, pos_insurance_date,  "
                      + " pos_ws, pos_ws2, pos_software_version, pos_db_version, "
                      + " pos_have_cashbox, pos_cashbox_no, pos_have_ecard, pos_ecard_no, pos_have_scanner, pos_scanner_no, "
                      + " pos_have_client_display, pos_client_display_no, pos_have_printer, pos_printer_no, "
                      + " pos_have_other_device, pos_other_device_no, pos_have_holder, pos_holder_no, "
                      + " pos_remark, create_user_id, create_user_name, create_time) "
                      + " values('"+posInfo.ID+"', '2', '"+posInfo.Type+"', '"+posInfo.Brand+"', '"+posInfo.Model+"', '"+posInfo.Code+"', '"+posInfo.SN+"', "
                      + " '"+posInfo.PurchaseDate+"', '"+posInfo.InsuranceDate+"',  "
                      + " 'http://192.168.0.55:8101','http://192.168.0.55:8101','1.1.1000','20120827080808', "
                      + " '"+posInfo.HaveCashbox+"', '"+posInfo.CashboxNo+"', '"+posInfo.HaveEcard+"', '"+posInfo.EcardNo+"', '"+posInfo.HaveScanner+"', '"+posInfo.ScannerNo+"', "
                      + " '"+posInfo.HaveClientDisplay+"', '"+posInfo.ClientDisplayNo+"', '"+posInfo.HavePrinter+"', '"+posInfo.PrinterNo+"', "
                      + " '" + posInfo.HaveOtherDevice + "', '" + posInfo.OtherDeviceNo + "', '" + posInfo.HaveHolder + "', '" + posInfo.HolderNo + "', "
                      + " '" + posInfo.Remark + "', '" + posInfo.CreateUserID + "', '" + posInfo.CreateUserName + "', getdate())"; 
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 获取满足查询条件的终端的记录总数
        /// </summary>
        /// <param name="HoldType"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <param name="SN"></param>
        /// <param name="PurchaseDateBegin"></param>
        /// <param name="PurchaseDateEnd"></param>
        /// <param name="InsuranceDateBegin"></param>
        /// <param name="InsuranceDateEnd"></param>
        /// <returns></returns>
        public int SelectPosListCount(string HoldType
                                    , string Type
                                    , string Code
                                    , string SN
                                    , string PurchaseDateBegin
                                    , string PurchaseDateEnd
                                    , string InsuranceDateBegin
                                    , string InsuranceDateEnd
                                    )
        {
            #region
            string sql = "select count(b.pos_id) from t_pos b, t_pos_type a where b.pos_type=a.pos_type_code ";
            PublicService pService = new PublicService();
            sql = pService.GetLinkSql(sql, "b.pos_hold_type ", HoldType, "=");
            sql = pService.GetLinkSql(sql, "b.pos_type ", Type, "=");
            sql = pService.GetLinkSql(sql, "b.pos_code ", Code, "=");
            sql = pService.GetLinkSql(sql, "b.pos_sn ", SN, "=");
            sql = pService.GetLinkSql(sql, "b.pos_purchase_date ", PurchaseDateBegin, ">=");
            sql = pService.GetLinkSql(sql, "b.pos_purchase_date ", PurchaseDateEnd, "<=");
            sql = pService.GetLinkSql(sql, "b.pos_insurance_date ", InsuranceDateBegin, ">=");
            sql = pService.GetLinkSql(sql, "b.pos_insurance_date ", InsuranceDateEnd, ">=");
            #endregion

            DataSet ds = new DataSet();
            int i = 0;
            ds = this.SQLHelper.ExecuteDataset(sql);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            return i;
        }
        /// <summary>
        /// 获取满足查询条件的终端的某页上的所有终端
        /// </summary>
        /// <param name="HoldType"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <param name="SN"></param>
        /// <param name="PurchaseDateBegin"></param>
        /// <param name="PurchaseDateEnd"></param>
        /// <param name="InsuranceDateBegin"></param>
        /// <param name="InsuranceDateEnd"></param>
        /// <param name="StartRow"></param>
        /// <param name="EndRow"></param>
        /// <returns></returns>
        public DataSet SelectPosList(string HoldType
                                    , string Type
                                    , string Code
                                    , string SN
                                    , string PurchaseDateBegin
                                    , string PurchaseDateEnd
                                    , string InsuranceDateBegin
                                    , string InsuranceDateEnd
                                    , int StartRow
                                    , int EndRow
                                    )
        {
            #region
            string sql = "select a.* from ( "
                       + " select rownum_=row_number() over(order by b.pos_hold_type,b.pos_type,b.pos_code), "
                       + " b.*, "
                       + " case b.pos_hold_type when '1' then '租赁' when '2' then '自有' else b.pos_hold_type end as pos_hold_type_desc, "
                       + " a.pos_type_name as pos_type_desc "
                       + " from t_pos b, t_pos_type a "
                       + " where b.pos_type=a.pos_type_code ";
            PublicService pService = new PublicService();
            sql = pService.GetLinkSql(sql, "b.pos_hold_type ", HoldType, "=");
            sql = pService.GetLinkSql(sql, "b.pos_type ", Type, "=");
            sql = pService.GetLinkSql(sql, "b.pos_code ", Code, "=");
            sql = pService.GetLinkSql(sql, "b.pos_sn ", SN, "=");
            sql = pService.GetLinkSql(sql, "b.pos_purchase_date ", PurchaseDateBegin, ">=");
            sql = pService.GetLinkSql(sql, "b.pos_purchase_date ", PurchaseDateEnd, "<=");
            sql = pService.GetLinkSql(sql, "b.pos_insurance_date ", InsuranceDateBegin, ">=");
            sql = pService.GetLinkSql(sql, "b.pos_insurance_date ", InsuranceDateEnd, ">=");
            sql = sql + "  ) a where rownum_ > '"+StartRow+"' and rownum_ <= '"+ EndRow +"' ";

            #endregion

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 根据POS标识获取pos详细信息
        /// </summary>
        /// <param name="pos_id"></param>
        /// <returns></returns>
        public DataSet GetPosInfoById(string pos_id)
        {
            DataSet ds = new DataSet();
            string sql = " select a.pos_type_name as pos_type_desc, "
                      + " b.*, "
                      + " case b.pos_hold_type when '1' then '租赁' when '2' then '自有' else b.pos_hold_type end as pos_hold_type_desc "
                      + " from t_pos_type a "
                      + " inner join t_pos b on a.pos_type_code=b.pos_type where b.pos_id= '"+pos_id+"'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 仓库

        #region 仓库查询
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public int SelectWarehouseListCount(Hashtable _ht)
        {
            PublicService pService = new PublicService();
            string sql = "select count(b.warehouse_id) from t_warehouse b, t_unit_warehouse c, t_unit d "
                       + " where b.warehouse_id=c.warehouse_id and c.unit_id=d.unit_id ";
            sql = pService.GetLinkSql(sql, "d.unit_name", _ht["UnitName"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_code", _ht["Code"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_name", _ht["Name"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_contacter", _ht["Contacter"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_tel", _ht["Tel"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_status", _ht["Status"].ToString(), "=");

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql)); 
        }
        /// <summary>
        /// 查询结果
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public DataSet SelectWarehouseList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = "select a.* from ( "
                      + " select rownum_=row_number() over(order by b.wh_code), "
                      + " b.*, "
                      + " case b.wh_status when 1 then '正常' else  '停用' end as wh_status_desc, "
                      + " case b.is_default when 1 then '是' else '否' end as is_default_desc, "
                      + " d.unit_id, d.unit_name, d.unit_code, d.unit_name_short "
                      + " from t_warehouse b, t_unit_warehouse c, t_unit d "
                      + " where b.warehouse_id=c.warehouse_id and c.unit_id=d.unit_id and d.customer_id='" + this.CurrentUserInfo.CurrentUser.customer_id + "'";
            PublicService pService = new PublicService();
            sql = pService.GetLinkSql(sql, "d.unit_name", _ht["UnitName"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_code", _ht["Code"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_name", _ht["Name"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_contacter", _ht["Contacter"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_tel", _ht["Tel"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "b.wh_status", _ht["Status"].ToString(), "=");
            sql = sql + " ) a where rownum_ > '" + _ht["StartRow"].ToString() + "' and rownum_ <= '" + _ht["EndRow"].ToString() + "'";

            ds = this.SQLHelper.ExecuteDataset(sql);

            return ds;
        }

        #endregion

        #region 保存
        public bool InsertWarehouse(WarehouseInfo warehouseInfo,UnitWarehouseInfo unitWarehouseInfo,out string strError,bool IsTran)
        {
            IDbTransaction tran = null;
            if (IsTran) {
                tran = this.SQLHelper.CreateTransaction();
            }
            using (tran)
            {
                try
                {
                    //1.判断重复
                    if (!IsExistWarehouseCode(warehouseInfo.wh_code, warehouseInfo.warehouse_id, tran))
                    {
                        strError = "编码已经存在。";
                        //throw (new System.Exception(strError));
                        return true;
                    }
                    //插入仓库
                    if (!SetWarehouseInsert(warehouseInfo, tran))
                    {
                        strError = "插入仓库失败";
                        throw (new System.Exception(strError));
                    }

                    //插入仓库
                    if (!SetUnitWarehouseInsert(unitWarehouseInfo, tran))
                    {
                        strError = "插入仓库与门店关系失败";
                        throw (new System.Exception(strError));
                    }

                    if (IsTran) tran.Commit();
                    strError = "成功";
                    return true;
                }
                catch (Exception ex)
                {
                    if (IsTran) tran.Rollback();
                    throw (ex);
                }
            }
        }
        
        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="warehouseInfo"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool ModifyWarehouse(WarehouseInfo warehouseInfo,UnitWarehouseInfo unitWarehouseInfo, out string strError)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    //插入仓库与门店关系
                    if (!SetWarehouseModify(warehouseInfo, tran))
                    {
                        strError = "修改仓库信息失败";
                        throw (new System.Exception(strError));
                    }
                    //删除仓库与门店信息
                    if (!SetUnitWarehouseDelete(warehouseInfo.warehouse_id, tran))
                    {
                        strError = "删除仓库信息失败";
                        throw (new System.Exception(strError));
                    }
                    //插入仓库与门店关系
                    if (!SetUnitWarehouseInsert(unitWarehouseInfo, tran))
                    {
                        strError = "插入仓库与门店关系失败";
                        throw (new System.Exception(strError));
                    }

                    tran.Commit();
                    strError = "成功";
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw (ex);
                }
            }
        }
        /// <summary>
        /// 判断是否重复
        /// </summary>
        /// <param name="warehouse_code"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public bool IsExistWarehouseCode(string warehouse_code, string warehouse_id, IDbTransaction pTran)
        {
            string sql = "select count(*) from t_warehouse where wh_code= '"+ warehouse_code +"' ";
            if (warehouse_id != null && !warehouse_id.Equals(""))
            {
                sql = sql + " and warehouse_id != '" + warehouse_id + "'";
            }

            int n = 0;
            if (pTran != null)
            {
                n = Convert.ToInt32(this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, sql, null));
            }
            else
            {
                n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            }
            return n > 0 ? false : true;
        }
        /// <summary>
        /// 插入仓库信息
        /// </summary>
        /// <param name="warehouseInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public bool SetWarehouseInsert(WarehouseInfo warehouseInfo, IDbTransaction pTran)
        {
            #region
            string sql = " insert into t_warehouse(warehouse_id, wh_code, wh_name, wh_name_en, "
                      + " wh_address, wh_contacter, wh_tel, wh_fax, wh_status, wh_remark, is_default, "
                      + " create_user_id, create_user_name, create_time,modify_user_name,modify_time) "
                      + " values('"+warehouseInfo.warehouse_id+"' , '"+warehouseInfo.wh_code+"', '"
                      + warehouseInfo.wh_name+"', '"+warehouseInfo.wh_name_en+"',  "
                      + " '" + warehouseInfo.wh_address + "', '" + warehouseInfo.wh_contacter + "', '" 
                      + warehouseInfo.wh_tel + "', '" + warehouseInfo.wh_fax + "', 1, '" 
                      + warehouseInfo.wh_remark + "', '" + warehouseInfo.is_default + "', "
                      + " '" + warehouseInfo.CreateUserID + "', '" + warehouseInfo.CreateUserName 
                      + "', CONVERT(nvarchar(20),getdate(),120) , '" 
                      + warehouseInfo.CreateUserName + "', CONVERT(nvarchar(20),getdate(),120)  )";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="warehouseInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public bool SetWarehouseModify(WarehouseInfo warehouseInfo, IDbTransaction pTran)
        {
            #region
            string sql = " update t_warehouse set "
                      + " wh_code = '"+warehouseInfo.wh_code+"', "
                      + " wh_name = '" + warehouseInfo.wh_name + "', "
                      + " wh_name_en = '" + warehouseInfo.wh_name_en + "', "
                      + " wh_address = '" + warehouseInfo.wh_address + "', "
                      + " wh_contacter = '" + warehouseInfo.wh_contacter + "', "
                      + " wh_tel = '" + warehouseInfo.wh_tel + "', "
                      + " wh_fax = '" + warehouseInfo.wh_fax + "', "
                      + " is_default = '" + warehouseInfo.is_default + "', "
                      + " wh_remark = '" + warehouseInfo.wh_remark + "', "
                      + " modify_user_id = '" + warehouseInfo.ModifyUserID + "', "
                      + " modify_user_name = '" + warehouseInfo.create_user_name + "', "
                      + " modify_time = CONVERT(nvarchar(20),getdate(),120)"
                      + " where warehouse_id = '" + warehouseInfo.warehouse_id + "'";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 仓库门店与仓库信息
        /// </summary>
        /// <param name="unitWarehouseInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public bool SetUnitWarehouseInsert(UnitWarehouseInfo unitWarehouseInfo, IDbTransaction pTran)
        { 
            #region
            string sql = "insert into t_unit_warehouse(unit_warehouse_id, warehouse_id, unit_id) "
                        + " values('" + unitWarehouseInfo.ID + "', '"+ unitWarehouseInfo.Warehouse.warehouse_id +"', '"+ unitWarehouseInfo.Unit.Id+"'); "
                        + " update t_unit set if_flag='0' where unit_id= '" + unitWarehouseInfo.Unit.Id + "';";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
            
        }

        public bool SetUnitWarehouseDelete(string warehouse_id, IDbTransaction pTran)
        {
            #region
            string sql = " update t_unit set if_flag='0' where unit_id in (select unit_id from t_unit_warehouse  where warehouse_id= '"+warehouse_id+"') ;"
                       + " delete from t_unit_warehouse where warehouse_id= '"+warehouse_id +"';";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        #endregion

        #region
        public bool EnableWarehouse(string warehouseID,string strStatus)
        {
            string sql = "update t_warehouse "
                      + " set wh_status = '" + strStatus + "', "
                      + " modify_user_id = '"+ this.loggingSessionInfo.CurrentUser.User_Id+"', "
                      + " modify_time = getdate() "
                      + " where warehouse_id = '"+warehouseID+"' ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        /// <summary>
        /// 根据仓库标识获取仓库信息
        /// </summary>
        /// <param name="warehouseID"></param>
        /// <returns></returns>
        public DataSet GetWarehouseByID(string warehouseID)
        {
            DataSet ds = new DataSet();
            string sql = "select a.warehouse_id, a.wh_code, a.wh_name, a.wh_name_en, a.wh_address, "
                      + " a.wh_contacter, a.wh_tel, a.wh_fax, a.wh_status, a.wh_remark, a.is_default, "
                      + " a.create_user_id, a.create_user_name, a.create_time, "
                      + " a.modify_user_id, a.modify_user_name, a.modify_time, a.sys_modify_stamp, "
                      + " case a.wh_status when 1 then '正常' else  '停用' end as wh_status_desc, "
                      + " case a.is_default when 1 then '是' else '否' end as is_default_desc, "
                      + " c.unit_id, c.unit_name, c.unit_code, c.unit_name_short "
                      + " from t_warehouse a, t_unit_warehouse b, t_unit c "
                      + " where a.warehouse_id=b.warehouse_id and b.unit_id=c.unit_id and a.warehouse_id= '"+ warehouseID+"' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
