using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class BillService : Base.BaseCPOSDAO
    {
         #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public BillService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 表单
        /// <summary>
        /// 根据标识判断Bill是否存在
        /// </summary>
        /// <param name="billId">表单标识</param>
        /// <returns></returns>
        public bool CanHaveBill(string billId)
        {
            string sql = "select isnull(count(*),0) from t_bill where bill_id= '"+billId+"';";
            int n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return n > 0;
        }
        /// <summary>
        /// 插入Bill
        /// </summary>
        /// <param name="billInfo"></param>
        public void InsertBill(BillModel billInfo)
        {
            string sql = " insert into t_bill (bill_id, unit_id, bill_kind_id, bill_Status, bill_code, work_month, "
                        + " work_date, bill_remark, bill_money, add_user_id, add_time) "
                    + " values "
                    + " ('" + billInfo.Id + "', '" + billInfo.UnitId + "', '" + billInfo.KindId + "', '" + billInfo.Status + "', '" + billInfo.Code + "', '" + billInfo.WorkMonth + "', "
                    + " '" + billInfo.WorkDate + "', '" + billInfo.Remark + "','" + billInfo.Money + "', '" + billInfo.AddUserId + "', '" + billInfo.AddDate + "') ;"; 
            this.SQLHelper.ExecuteNonQuery(sql);
        }

        
        /// <summary>
        /// 修改bill
        /// </summary>
        /// <param name="billInfo"></param>
        public void UpdateBill(BillModel billInfo)
        {
            string sql = "update t_bill "
                      + " set unit_id= '"+billInfo.UnitId+"', bill_status= '"+billInfo.Status+"', bill_code= '"+billInfo.Code+"', work_month= '"+billInfo.WorkMonth+"', "
                      + " work_date= '"+billInfo.WorkDate+"', bill_remark= '"+billInfo.Remark+"', bill_money= '"+billInfo.Money+"', "
                      + " modify_user_id= '"+billInfo.ModifyUserId+"', modify_time= '"+billInfo.ModifyDate+"' "
                      + " where bill_id= '"+billInfo.Id+"'; ";
            this.SQLHelper.ExecuteNonQuery(sql);

        }

        /// <summary>
        /// 获取Bill单个详细信息
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        public DataSet GetBillById(string billId)
        {
            DataSet ds = new DataSet();
            string sql = "select a.bill_id Id "
                      + " , a.unit_id UnitId "
                      + " , a.bill_kind_id KindId "
                      + " , a.bill_status Status "
                      + " , a.bill_code Code "
                      + " ,a.work_month WorkMonth, a.work_date WorkDate, a.upload_batch_id UploadBatchId, a.bill_remark Remark "
                      + " , a.bill_money Money "
                      + " ,a.add_user_id AddUserId "
                      + " , a.add_time AddDate "
                      + " , a.modify_user_id ModifyUserId, a.modify_time  ModifyDate "
                      + " ,b.user_name as AddUserName, c.user_name as ModifyUserName, "
                      + " d.unit_name_short UnitName "
                      + " , e.bill_kind_name as BillKindDescription "
                      + " ,f.bill_status_name as BillStatusDescription "
                      + " from t_bill a left join t_user b on a.add_user_id=b.user_id "
                      + " left join t_user c on a.modify_user_id=c.user_id "
                      + " left join t_unit d on a.unit_id=d.unit_id "
                      + " inner join T_Def_Bill_Kind e "
                      + " on a.bill_kind_id=e.bill_kind_id "
                      + " inner join t_def_bill_status f "
                      + " on a.bill_kind_id=f.bill_kind_id and a.bill_Status=f.bill_status where a.bill_id='"+billId+"'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        
        #endregion

        #region 表单种类
        /// <summary>
        /// 根据Bill种类的编码获取Bill种类
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindCode">Bill种类的编码</param>
        /// <returns></returns>
        public DataSet GetBillKindByCode(string billKindCode)
        {
            string sql = "select a.bill_kind_id Id, a.bill_kind_code Code, a.money_flag MoneyFlag, a.create_url CreateUrl, a.modify_url ModifyUrl, a.query_url QueryUrl, a.approve_url ApproveUrl, "
                       + " a.bill_kind_name Description"
                       + " from t_def_bill_kind a "
                       + " where a.bill_kind_code= '"+billKindCode+"'";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;        
            
        }
        /// <summary>
        /// 获取表单种类
        /// </summary>
        /// <param name="billKindId"></param>
        /// <returns></returns>
        public DataSet GetBillKindById(string billKindId)
        {
            DataSet ds = new DataSet();
            string sql = "select a.bill_kind_id Id, a.bill_kind_code Code, a.money_flag MoneyFlag, a.create_url CreateUrl, a.modify_url ModifyUrl, a.query_url QueryUrl, a.approve_url ApproveUrl, "
                       + " a.bill_kind_name Description"
                       + " from t_def_bill_kind a "
                       + " where a.bill_kind_id= '" + billKindId + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取所有表单种类
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllBillKinds()
        {
            string sql = "select a.bill_kind_id Id, a.bill_kind_code Code, a.money_flag MoneyFlag "
                        + " , a.create_url CreateUrl, a.modify_url ModifyUrl, a.query_url QueryUrl "
                        + " , a.approve_url  ApproveUrl "
                        + " ,a.bill_kind_name as Description "
                        + "  from t_def_bill_kind a "
                        + "  order by a.display_index";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 表单状态
        /// <summary>
        /// 获取某种表单的起始状态
        /// </summary>
        /// <param name="billKindId"></param>
        /// <returns></returns>
        public DataSet GetBillBeginStatus(string billKindId)
        {
            string sql = "select a.bill_status_id Id "
                        + " ,a.bill_kind_id KindId "
                        + " ,a.bill_status Status "
                        + " ,a.begin_flag BeginFlag "
                        + " ,a.end_flag EndFlag "
                        + " ,a.delete_flag DeleteFlag "
                        + " ,a.custom_flag CustomFlag "
                        + " ,a.bill_status_name Description "
                        + " , c.bill_kind_name BillKindDescription "
                        + " ,a.customer_id "
                        + " from t_def_bill_status a "
                        + " left join t_def_bill_kind c on a.bill_kind_id=c.bill_kind_id "
                        + " where a.bill_kind_id='"+billKindId+"' and a.begin_flag=1 and a.customer_id = '"+loggingSessionInfo.CurrentLoggingManager.Customer_Id+"' ;";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取某种Bill的某种状态
        /// </summary>
        /// <param name="billKindId"></param>
        /// <param name="billStatus"></param>
        /// <returns></returns>
        public DataSet GetBillStatusByKindIdAndStatus(string billKindId, string billStatus)
        {
            string sql = "select a.bill_status_id Id "
                       + " ,a.bill_kind_id KindId "
                       + " ,a.bill_status Status "
                       + " ,a.begin_flag BeginFlag "
                       + " ,a.end_flag EndFlag "
                       + " ,a.delete_flag DeleteFlag "
                       + " ,a.custom_flag CustomFlag "
                       + " ,a.bill_status_name Description "
                       + " , c.bill_kind_name BillKindDescription "
                       + " ,a.customer_id "
                       + " from t_def_bill_status a "
                       + " left join t_def_bill_kind c on a.bill_kind_id=c.bill_kind_id "
                       + " where a.bill_kind_id='" + billKindId + "' and a.bill_status= '" + billStatus + "' and a.customer_id = '" + loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' ;";

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 根据单据号码获取单据状态集合
        /// </summary>
        /// <param name="bill_kind_code"></param>
        /// <returns></returns>
        public DataSet GetBillStatusByKindCode(string bill_kind_code)
        {
            DataSet ds = new DataSet();
            string sql =  "select a.bill_status_id Id "
                        + " ,a.bill_kind_id KindId "
                        + " ,a.bill_status Status "
                        + " ,a.begin_flag BeginFlag "
                        + " ,a.end_flag EndFlag "
                        + " ,a.delete_flag DeleteFlag "
                        + " ,a.custom_flag CustomFlag "
                        + " ,a.bill_status_name Description "
                        + " , c.bill_kind_name BillKindDescription "
                        + " ,a.customer_id "
                        + " from t_def_bill_status a "
                        + " inner join t_def_bill_kind c on a.bill_kind_id=c.bill_kind_id "
                        + " where a.bill_kind_code='" + bill_kind_code + "' and a.delete_flag = '0'  and a.customer_id = '" + loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' order by a.bill_kind_id, a.bill_status;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取某种单据的所有表单状态
        /// </summary>
        /// <param name="billKindID"></param>
        /// <returns></returns>
        public DataSet GetAllBillStatusesByBillKindID(string billKindID)
        {
            DataSet ds = new DataSet();
            string sql = "select a.bill_status_id Id "
                        + " ,a.bill_kind_id KindId "
                        + " ,a.bill_status Status "
                        + " ,a.begin_flag BeginFlag "
                        + " ,a.end_flag EndFlag "
                        + " ,a.delete_flag DeleteFlag "
                        + " ,a.custom_flag CustomFlag "
                        + " ,a.bill_status_name Description "
                        + " , c.bill_kind_name BillKindDescription "
                        + " ,a.customer_id "
                        + " from t_def_bill_status a "
                        + " inner join t_def_bill_kind c on a.bill_kind_id=c.bill_kind_id "
                        + " where a.bill_kind_id='" + billKindID + "' and a.delete_flag = '0'  and a.customer_id = '" + loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' order by a.bill_kind_id, a.bill_status;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取某种表单的删除状态
        /// </summary>
        /// <param name="billKindId"></param>
        /// <returns></returns>
        public DataSet GetBillDeleteStatus(string billKindId)
        {
            DataSet ds = new DataSet();
            string sql = "select a.bill_status_id Id "
                        + " ,a.bill_kind_id KindId "
                        + " ,a.bill_status Status "
                        + " ,a.begin_flag BeginFlag "
                        + " ,a.end_flag EndFlag "
                        + " ,a.delete_flag DeleteFlag "
                        + " ,a.custom_flag CustomFlag "
                        + " ,a.bill_status_name Description "
                        + " , c.bill_kind_name BillKindDescription "
                        + " ,a.customer_id "
                        + " from t_def_bill_status a "
                        + " inner join t_def_bill_kind c on a.bill_kind_id=c.bill_kind_id "
                        + " where a.bill_kind_id='" + billKindId + "' and a.delete_flag = '1'  and a.customer_id = '" + loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' order by a.bill_kind_id, a.bill_status;";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 新建表单状态
        /// </summary>
        /// <param name="billStatus"></param>
        /// <returns></returns>
        public bool InsertBillStatus(BillStatusModel billStatus)
        {
            string sql = "insert into t_def_bill_status "
                      + " (bill_status_id, bill_kind_id, bill_status, bill_status_name, begin_flag, end_flag, delete_flag, custom_flag, "
                      + " status, create_user_id, create_time,customer_id) "
                      + " values "
                      + " ('" + billStatus.Id + "', '" + billStatus.KindId + "', '" + billStatus.Status + "' , '" + billStatus.Description + "' , '" + billStatus.BeginFlag + "' , '" + billStatus.EndFlag + "' , '" + billStatus.DeleteFlag + "' , '" + billStatus.CustomFlag + "' , "
                      + " 1, '" + billStatus.CreateUserID + "' , '" + billStatus.CreateTime + "' ,'" + billStatus.customer_id + "' )";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        /// <summary>
        /// 删除一个表单状态
        /// </summary>
        /// <param name="billStatusID"></param>
        /// <returns></returns>
        public bool DeleteBillStatus(string billStatusID)
        {
            string sql = "delete from t_def_bill_status where bill_status_id = '"+billStatusID+"'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }

        /// <summary>
        /// 状态对应的单子数量
        /// </summary>
        /// <param name="billStatusId"></param>
        /// <returns></returns>
        public int GetBillStatusCountUsingBillsById(string billStatusId)
        {
            string sql = "select isnull(count(a.bill_id),0)  From T_Bill a inner join T_Def_Bill_Status b on(a.bill_kind_id = b.bill_kind_id and a.bill_Status = b.bill_status)  "
                       + " where b.bill_status_id = '" + billStatusId + "' and b.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' ";
            int icount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return icount;
        }
        /// <summary>
        /// 存在角色与表单操作的关系中的前置状态或者当前状态=要删除的表单状态
        /// </summary>
        /// <param name="billStatusId"></param>
        /// <returns></returns>
        public int GetBillStatusCountUsingBillActionRolesById(string billStatusId)
        {
            string sql = "select isnull(count(a.bill_action_role_id),0)  From T_Def_Bill_Action_Role a "
                      + " inner join t_def_bill_status b on(a.bill_kind_id = b.bill_kind_id and a.customer_id = b.customer_id "
                      + " and (a.pre_bill_status = b.bill_status or a.cur_bill_Status = b.bill_status)) "
                      + " where b.bill_status_id = ''";
            int icount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return icount;
        }
        #endregion

        #region 表单操作
        /// <summary>
        /// 根据表单标识，获取表单的当前按钮（操作）
        /// </summary>
        /// <param name="billId">表单标识</param>
        /// <returns></returns>
        public DataSet GetBillActionByBillId(string billId,string roleId)
        {
            string sql = "select isnull(max(c.create_flag),0) create_flag "
                       + " ,isnull(max(c.modify_flag),0) modify_flag "
                       + " ,isnull(max(c.approve_flag),0) approve_flag "
                       + "    ,isnull(max(c.reject_flag),0) reject_flag "
                       + "    ,isnull(max(c.cancel_flag),0) delete_flag "
                       + "    ,a.bill_id "
                       + "    From T_Bill a "
                       + "    inner join T_Def_Bill_Action_Role b  "
                       + "    on(a.bill_kind_id = b.bill_kind_id "
                       + "    and a.bill_Status = b.pre_bill_status) "
                       + "    inner join T_Def_Bill_Action c "
                       + "    on(b.bill_action_id = c.bill_action_id) "
                       + "    where 1=1 "
                       + "    and a.bill_id = '"+billId+"' "
                       + "    and b.role_id = '"+roleId+"' "
                       + "    and c.[status] = '1' "
                       + "    group by a.bill_id;";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取表单动作
        /// </summary>
        /// <param name="billKindId"></param>
        /// <param name="billActionType"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public DataSet GetBillAction(string billKindId, BillActionType billActionType, string con)
        {
            string sql = " select a.bill_action_id Id, a.bill_kind_id KindId, a.bill_action_code Code "
                       + " ,a.create_flag CreateFlag, a.modify_flag ModifyFlag, a.approve_flag ApproveFlag "
                       + " , a.reject_flag RejectFlag, a.cancel_flag CancelFlag,a.bill_action_name Description,a.display_index display_index "
                       + " from t_def_bill_action a "
                       + "where a.bill_kind_id= '"+billKindId+"'  and a.customer_id = '"+this.loggingSessionInfo.CurrentLoggingManager.Customer_Id+"'"
                       + con;
            PublicService p = new PublicService();
            sql = p.GetLinkSql(sql, "a.bill_action_code", billActionType.ToString(), "=");
                
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 删除一个表单操作
        /// </summary>
        /// <param name="billActionID"></param>
        /// <returns></returns>
        public bool CanDeleteBillAction(string billActionID)
        {
            string sql = "select isnull(count(*),0) from t_def_bill_action_role  where bill_action_id= '"+billActionID+"' and customer_id = '"+this.loggingSessionInfo.CurrentLoggingManager.Customer_Id+"' ";
            int icount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return icount == 1;
        }
        /// <summary>
        /// 查询某种表单类型下的所有的表单操作
        /// </summary>
        /// <param name="billKindID"></param>
        /// <returns></returns>
        public DataSet GetAllBillActionsByBillKindId(string billKindId)
        {
            DataSet ds = new DataSet();
            string sql = " select a.bill_action_id Id, a.bill_kind_id KindId, a.bill_action_code Code "
                       + " ,a.create_flag CreateFlag, a.modify_flag ModifyFlag, a.approve_flag ApproveFlag "
                       + " , a.reject_flag RejectFlag, a.cancel_flag CancelFlag,a.bill_action_name Description,a.display_index display_index "
                       + " from t_def_bill_action a "
                       + "where a.bill_kind_id= '" + billKindId + "'  and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' order by display_index ;";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 添加一个表单操作
        /// </summary>
        /// <param name="billAction"></param>
        /// <returns></returns>
        public bool InsertBillAction(BillActionModel billActionInfo)
        {
            string sql = "insert into t_def_bill_action "
                     + " (bill_action_id, bill_kind_id, bill_action_code, bill_action_name, "
                     + "  create_flag, modify_flag, approve_flag, reject_flag, cancel_flag, "
                     + "  status, create_user_id, create_time,customer_id,display_index) "
                     + "  values "
                     + "  ('" + billActionInfo.Id + "','" + billActionInfo.KindId + "' ,'" + billActionInfo.Code + "' ,'" + billActionInfo.Description + "' , "
                     + "  '" + billActionInfo.CreateFlag + "' , '" + billActionInfo.ModifyFlag + "' ,'" + billActionInfo.ApproveFlag + "', '" + billActionInfo.RejectFlag + "' ,'" + billActionInfo.CancelFlag + "' , "
                     + "  1, '" + billActionInfo.CreateUserID + "' , '" + billActionInfo.CreateTime + "','" + billActionInfo.customer_id + "','" + billActionInfo.display_index + "')";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        /// <summary>
        /// 删除一个表单操作
        /// </summary>
        /// <param name="billActionID"></param>
        /// <returns></returns>
        public bool DeleteBillAction(string billActionID)
        {
            string sql = "delete from t_def_bill_action where bill_action_id= '"+billActionID+"' ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 角色与表单
        /// <summary>
        /// 获取角色动作
        /// </summary>
        /// <param name="bill_kind_id"></param>
        /// <param name="bill_action_id"></param>
        /// <param name="role_id"></param>
        /// <param name="pre_bill_status"></param>
        /// <returns></returns>
        public DataSet GetbillCreateActionRole(string bill_kind_id,string bill_action_id,string role_id,string pre_bill_status)
        {
            DataSet ds = new DataSet();
            string sql = "select bill_action_role_id Id, bill_kind_id KindId, bill_action_id ActionId, role_id RoleId, "
                      + " pre_bill_status PreviousStatus, cur_bill_status CurrentStatus, min_money MinMoney, max_money MaxMoney, "
                      + "  '0' ValidateDate "
                      + " from t_def_bill_action_role "
                      + " where bill_kind_id= '" + bill_kind_id + "'"
                      + " and bill_action_id= '" + bill_action_id + "' "
                      + " and role_id=substring('" + role_id + "',1,32)"
                      + " and pre_bill_status= '" + pre_bill_status + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 查询角色与表单操作的关系
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public DataSet SelectBillActionRoleList(Hashtable _ht)
        {
            string sql = "select a.bill_action_role_id Id, a.bill_kind_id KindId, a.bill_action_id ActionId, a.role_id RoleId, "
                      + " a.pre_bill_status PreviousStatus, a.cur_bill_status CurrentStatus, a.min_money MinMoney, a.max_money MaxMoney, "
                      + "  '0' ValidateDate, "
                      + " b.bill_kind_name as KindDescription, "
                      + " c1.bill_status_name as PreviousStatusDescription, c2. bill_status_name as CurrentStatusDescription, "
                      + " d.role_name as RoleDescription, "
                      + " e.bill_action_name as ActionDescription, "
                      + " '' as DateControlTypeName "
                      + " from t_def_bill_action_role a, t_def_bill_kind b, t_def_bill_status c1, "
                      + " t_def_bill_status c2, t_role d, t_def_bill_action e "
                      + " where a.bill_kind_id=b.bill_kind_id "
                      + " and a.bill_kind_id=c1.bill_kind_id and a.pre_bill_status=c1.bill_status "
                      + " and a.bill_kind_id=c2.bill_kind_id and a.cur_bill_status=c2.bill_status "
                      + " and a.role_id=d.role_id "
                      + " and a.bill_action_id=e.bill_action_id and a.bill_kind_id = '" + _ht["BillKindID"] + "' and a.role_id = '" + _ht["RoleID"] + "' and a.customer_id = '"+this.loggingSessionInfo.CurrentLoggingManager.Customer_Id+"'"
                      + " order by b.bill_kind_code, d.role_code, e.bill_action_code";

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 添加一个角色与表单操作的关系
        /// </summary>
        /// <param name="billActionRole"></param>
        /// <returns></returns>
        public bool InsertBillActionRole(BillActionRoleModel billActionRole)
        {
            string sql = "insert into t_def_bill_action_role "
                      + " (bill_action_role_id, bill_kind_id, bill_action_id, role_id, "
                      + " pre_bill_status, cur_bill_status, min_money, max_money,  "
                      + " create_user_id, create_time,customer_id) "
                      + " values "
                      + " ('" + billActionRole.Id + "','" + billActionRole.KindId + "','" + billActionRole.ActionId + "','" + billActionRole.RoleId + "','" + billActionRole.PreviousStatus + "','" + billActionRole.CurrentStatus + "','" + billActionRole.MinMoney + "','" + billActionRole.MaxMoney + "', "
                      + " '" + billActionRole.CreateUserID + "','" + billActionRole.CreateTime + "','" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' customer_id)"; 
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        /// <summary>
        /// 删除一个角色与表单操作的关系
        /// </summary>
        /// <param name="billActionRoleID"></param>
        /// <returns></returns>
        public bool DeleteBillActionRole(string billActionRoleID)
        {
            string sql = "delete from t_def_bill_action_role where bill_action_role_id= '" + billActionRoleID + "' ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;

        }
        #endregion

        #region 权限操作
        /// <summary>
        /// 判断登录用户针对某种表单是否有操作权限
        /// </summary>
        /// <param name="billKindCode"></param>
        /// <returns></returns>
        public int CanCreateBillKind(string billKindCode,string roleId)
        {
            string sql = "select isnull(count(a.bill_action_role_id),0)  From  t_def_bill_action_role a "
                      + " inner join t_def_bill_action b "
                      + " on(a.bill_action_id=b.bill_action_id) "
                      + " inner join t_def_bill_kind c "
                      + " on(a.bill_kind_id=c.bill_kind_id ) "
                      + " where b.create_flag=1 "
                      + " and c.bill_kind_code= '"+billKindCode+"'"
                      + " and a.role_id=substring('"+roleId+"',1,32)";
            int count = Convert.ToInt32(this.SQLHelper.ExecuteDataset(sql));
            return count;
        }
        /// <summary>
        /// 查看当前用户能否新建一个Bill
        /// </summary>
        /// <param name="billKindId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int CanCreateBill(string billKindId,string roleId)
        {
            string sql = "select nvl(count(a.bill_action_role_id),0) from t_def_bill_action_role a, t_def_bill_action b "
                      + " where a.role_id=substr('"+roleId+"',1,32) and a.bill_kind_id= '"+billKindId+"'"
                      + " and a.bill_action_id=b.bill_action_id and b.create_flag=1 "
                      + " and FN_BILL_GET_DATETIME(a.date_control_type,a.date_time)=1";
            int count = Convert.ToInt32(this.SQLHelper.ExecuteDataset(sql));
            return count;
        }
        /// <summary>
        /// 根据标识获取表单操作信息
        /// </summary>
        /// <param name="billActionId"></param>
        /// <returns></returns>
        public DataSet GetBillActionByActionId(string billActionId)
        {
            string sql = "select a.bill_action_id Id"
                      + " , a.bill_kind_id KindId, a.bill_action_code Code, "
                      + " a.create_flag CreateFlag, a.modify_flag ModifyFlag, a.approve_flag ApproveFlag, a.reject_flag RejectFlag, a.cancel_flag  CancelFlag, " 
                      + " a.bill_action_name Description,a.display_index "
                      + " from t_def_bill_action a where a.bill_action_id= '" + billActionId + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 判断用户是否有审批权限
        /// </summary>
        /// <param name="billId">单据标识</param>
        /// <returns></returns>
        public bool CanApproveBill(string billId)
        {
            string sql = "select isnull(COUNT(a.bill_id),0) From T_Bill a "
                      + " inner join T_Def_Bill_Action_Role b "
                      + " on(a.bill_kind_id = b.bill_kind_id "
                      + " and a.bill_Status = b.pre_bill_status) "
                      + " inner join T_Def_Bill_Action c "
                      + " on(b.bill_kind_id = c.bill_kind_id "
                      + " and b.bill_action_id = c.bill_action_id) "
                      + " where 1=1 and c.approve_flag = '1' "
                      + " and a.bill_id = '" + billId + "' and c.role_id = substring('"+this.loggingSessionInfo.CurrentUserRole.RoleId+"',1,32)";
            int icount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return icount > 0;
        }

       
        #endregion

        #region
        public bool SetBatBillActionRole(string roleId)
        {
            #region T_Def_Bill_Action_Role
            string sql = "insert into T_Def_Bill_Action_Role(bill_action_role_id,bill_kind_id,bill_action_id,role_id,pre_bill_status,cur_bill_Status,customer_id) "
                      + " select a.* From ( "
                      + " select REPLACE(newid(),'-','') bill_action_role_id " 
                      + " ,bill_kind_id "
                      + " ,bill_action_id "
                      + " ,'" + roleId + "' role_id "
                      + " ,pre_bill_status "
                      + " ,cur_bill_Status ,'" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "'  customer_id "
                      + " From T_Def_Bill_Action_Role where role_id = '7064243380E24B0BA24E4ADC4E03968B') a "
                      + " left join T_Def_Bill_Action_Role b "
                      + " on(a.bill_kind_id = b.bill_kind_id "
                      + " and a.bill_action_id = b.bill_action_id "
                      + " and a.role_id = b.role_id "
                      + " and a.pre_bill_status = b.pre_bill_status "
                      + " and a.cur_bill_Status = b.cur_bill_Status) "
                      + " where 1=1 "
                      + " and b.bill_action_role_id is null;";
            this.SQLHelper.ExecuteNonQuery(sql);
            #endregion
            #region T_Def_Bill_Action
            sql = "INSERT INTO dbo.T_Def_Bill_Action "
                + " ( bill_action_id , "
                + "   bill_kind_id , "
                + "   bill_action_code , "
                + "   bill_action_name , "
                + "   create_flag , "
                + "   modify_flag , "
                + "   approve_flag , "
                + "   reject_flag , "
                + "   cancel_flag , "
                + "   display_index , "
                + "   status , "
                + "   create_user_id , "
                + "   create_time , "
                + "   modify_user_id , "
                + "   modify_time , "
                + "   customer_id "
                + " ) "
                + " SELECT REPLACE(NEWID(),'-','') bill_action_id , "
                + "  x.bill_kind_id , "
                + "   x.bill_action_code , "
                + "   x.bill_action_name , "
                + "   x.create_flag , "
                + "   x.modify_flag , "
                + "   x.approve_flag , "
                + "   x.reject_flag , "
                + "   x.cancel_flag , "
                + "   x.display_index , "
                + "   x.status , "
                + "   x.create_user_id , "
                + "  GETDATE() create_time , "
                + "   x.modify_user_id , "
                + "  GETDATE() modify_time , "
                + " '"+this.CurrentUserInfo.CurrentLoggingManager.Customer_Id+"'  customer_id  "
                + " FROM T_Def_Bill_Action x "
                + " INNER JOIN ( "
                + " SELECT TOP 1 customer_id  FROM T_Def_Bill_Action "
                + " group BY customer_id HAVING COUNT(*) = 34) y ON(x.customer_id = y.customer_id) ;";
            this.SQLHelper.ExecuteNonQuery(sql);
            #endregion

            #region T_Def_Bill_Status
            sql = "INSERT INTO dbo.T_Def_Bill_Status "
                    + " ( bill_status_id , "
                    + "   bill_kind_id , "
                    + "   bill_status , "
                    + "   bill_status_name , "
                    + "   begin_flag , "
                    + "   end_flag , "
                    + "   delete_flag , "
                    + "   custom_flag , "
                    + "   status , "
                    + "   create_user_id , "
                    + "   create_time , "
                    + "   modify_user_id , "
                    + "   modify_time , "
                    + "   customer_id "
                    + " ) "
                    + " SELECT REPLACE(NEWID(),'-','') bill_status_id , "
                    + "   x.bill_kind_id , "
                    + "   x.bill_status , "
                    + "   x.bill_status_name , "
                    + "   x.begin_flag , "
                    + "   x.end_flag , "
                    + "   x.delete_flag , "
                    + "   x.custom_flag , "
                    + "   x.status , "
                    + "   x.create_user_id , "
                    + "   GETDATE() create_time , "
                    + "   x.modify_user_id , "
                    + "   GETDATE() modify_time , "
                    + "   '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' customer_id FROM T_Def_Bill_Status x "
                    + " INNER JOIN (SELECT TOP 1 customer_id FROM dbo.T_Def_Bill_Status GROUP BY customer_id HAVING COUNT(*) = 25 ) y "
                    + " ON(x.customer_id = y.customer_id) ";
            this.SQLHelper.ExecuteNonQuery(sql);
            #endregion
            return true;
        }
        #endregion
    }
}
