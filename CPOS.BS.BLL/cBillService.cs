using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 表单类
    /// </summary>
    public class cBillService : BaseService
    {
         JIT.CPOS.BS.DataAccess.BillService billService = null;
        #region 构造函数
         public cBillService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            billService = new DataAccess.BillService(loggingSessionInfo);
        }
        #endregion

        #region 表单种类
        /// <summary>
        /// 获取某种表单种类的下一个表单编码
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="billKindCode">表单种类的编码</param>
        /// <returns></returns>
        public string GetBillNextCode(string billKindCode)
        {
            JIT.CPOS.BS.BLL.AppSysService appSysService = new JIT.CPOS.BS.BLL.AppSysService(loggingSessionInfo);
            switch (billKindCode)
            {
                case BillKindModel.CODE_USER_NEW:
                    return appSysService.GetNo("UN");
                case BillKindModel.CODE_USER_MODIFY:
                    return appSysService.GetNo("UM");
                case BillKindModel.CODE_USER_DISABLE:
                    return appSysService.GetNo("UD");
                case BillKindModel.CODE_USER_ENABLE:
                    return appSysService.GetNo("UA");
                case "CreateUnit": //组织新建
                    return appSysService.GetNo("UNIT");
                case "CreateAdjustmentPrice"://调价单
                    return appSysService.GetNo("AOP");
                case "POSINOUT": //pos小票
                    return appSysService.GetNo("POS");
                case "DO":
                    return appSysService.GetNo("DO");
                case "RO":
                    return appSysService.GetNo("RO");
                case "PO":
                    return appSysService.GetNo("PO");
                case "SO":
                    return appSysService.GetNo("SO");
                case "RT":
                    return appSysService.GetNo("RT");
                case "WS":
                    return appSysService.GetNo("WS");
                case "CC":
                    return appSysService.GetNo("CC");
                case "AJ":
                    return appSysService.GetNo("AJ");
                case "MV":
                    return appSysService.GetNo("MV");
                case "CA":
                    return appSysService.GetNo("CA");
                default:
                    return appSysService.GetNo("BI");
            }
        }
        /// <summary>
        /// 获取所有的Bill种类
        /// </summary>
        /// <returns></returns>
        public IList<BillKindModel> GetAllBillKinds()
        {
            IList<BillKindModel> billKindInfoList = new List<BillKindModel>();
            DataSet ds = new DataSet();
            ds = billService.GetAllBillKinds();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                billKindInfoList = DataTableToObject.ConvertToList<BillKindModel>(ds.Tables[0]);
            }
            return billKindInfoList;
        }

        /// <summary>
        /// 根据Bill种类的Id获取Bill种类
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindId">Bill种类的Id</param>
        /// <returns></returns>
        public BillKindModel GetBillKindById(string billKindId)
        {
            BillKindModel billKindInfo = new BillKindModel();
            DataSet ds = billService.GetBillKindById(billKindId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                billKindInfo = DataTableToObject.ConvertToObject<BillKindModel>(ds.Tables[0].Rows[0]);
            }
            return billKindInfo;
           
        }

        /// <summary>
        /// 根据Bill种类的编码获取Bill种类
        /// </summary>
        /// <param name="billKindCode">Bill种类的编码</param>
        /// <returns></returns>
        public BillKindModel GetBillKindByCode(string billKindCode)
        {
            BillKindModel billKindInfo = new BillKindModel();
            DataSet ds = billService.GetBillKindByCode(billKindCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                billKindInfo = DataTableToObject.ConvertToObject<BillKindModel>(ds.Tables[0].Rows[0]);
            }
            return billKindInfo;

        }
        #endregion

        #region 表单状态
        /// <summary>
        /// 获取某种表单的起始状态
        /// </summary>
        /// <param name="loggingSession">登录model</param>
        /// <param name="billKindId">Bill的种类</param>
        /// <returns></returns>
        private BillStatusModel GetBillBeginStatus(string billKindId)
        {
            BillStatusModel billStatusInfo = new BillStatusModel();
            DataSet ds = billService.GetBillBeginStatus(billKindId);
            if(ds!=null && ds.Tables[0].Rows.Count > 0){
                billStatusInfo = DataTableToObject.ConvertToObject<BillStatusModel>(ds.Tables[0].Rows[0]);
            }
            return billStatusInfo;
        }

        /// <summary>
        /// 获取某种Bill的某种状态
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindId">Bill的种类</param>
        /// <param name="billStatus">Bill的某种状态</param>
        /// <returns></returns>
        public BillStatusModel GetBillStatusByKindIdAndStatus(string billKindId, string billStatus)
        {
            BillStatusModel billStatusInfo = new BillStatusModel();
            DataSet ds = billService.GetBillStatusByKindIdAndStatus(billKindId,billStatus);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                billStatusInfo = DataTableToObject.ConvertToObject<BillStatusModel>(ds.Tables[0].Rows[0]);
            }
            return billStatusInfo;
        }

        /// <summary>
        /// 获取某种表单的删除状态
        /// </summary>
        /// <param name="loggingSession">语言</param>
        /// <param name="billKindId">Bill的种类</param>
        /// <returns></returns>
        private BillStatusModel GetBillDeleteStatus(string billKindId)
        {
            BillStatusModel billStatusInfo = new BillStatusModel();
            DataSet ds = new DataSet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                billStatusInfo = DataTableToObject.ConvertToObject<BillStatusModel>(ds.Tables[0].Rows[0]);
            }
            return billStatusInfo;
        }

        /// <summary>
        /// 获取某种单据的所有状态
        /// </summary>
        /// <param name="loggingSession">登录信息</param>
        /// <param name="bill_kind_code">单据种类</param>
        /// <returns></returns>
        public IList<BillStatusModel> GetBillStatusByKindCode(string bill_kind_code)
        {
            IList<BillStatusModel> billStatusInfoList = new List<BillStatusModel>();
            if (this.loggingSessionInfo.CurrentLoggingManager.IsApprove == null || this.loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals(""))
            {
                BillStatusModel billStatusInfo = new BillStatusModel();
                billStatusInfo.Status = "-1";
                billStatusInfo.Description = "删除";
                billStatusInfoList.Add(billStatusInfo);
                BillStatusModel billStatusInfo1 = new BillStatusModel();
                billStatusInfo1.Status = "1";
                billStatusInfo1.Description = "未审批";
                billStatusInfoList.Add(billStatusInfo1);
                BillStatusModel billStatusInfo2 = new BillStatusModel();
                billStatusInfo2.Status = "10";
                billStatusInfo2.Description = "已审批";
                billStatusInfoList.Add(billStatusInfo2);

            }
            else {
                DataSet ds = billService.GetBillStatusByKindCode(bill_kind_code);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    billStatusInfoList = DataTableToObject.ConvertToList<BillStatusModel>(ds.Tables[0]);
                }
            }
            
            return billStatusInfoList;
        }

        /// <summary>
        /// 获取某种表单类型的所有状态
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billKindID">表单类型ID</param>
        /// <returns></returns>
        public IList<BillStatusModel> GetAllBillStatusesByBillKindID( string billKindID)
        {
            IList<BillStatusModel> billStatusInfoList = new List<BillStatusModel>();
            if (this.loggingSessionInfo.CurrentLoggingManager.IsApprove == null || this.loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals(""))
            {
                BillStatusModel billStatusInfo = new BillStatusModel();
                billStatusInfo.Status = "-1";
                billStatusInfo.Description = "删除";
                billStatusInfoList.Add(billStatusInfo);
                BillStatusModel billStatusInfo1 = new BillStatusModel();
                billStatusInfo1.Status = "1";
                billStatusInfo1.Description = "未审批";
                billStatusInfoList.Add(billStatusInfo1);
                BillStatusModel billStatusInfo2 = new BillStatusModel();
                billStatusInfo2.Status = "10";
                billStatusInfo2.Description = "已审批";
                billStatusInfoList.Add(billStatusInfo2);

            }
            else
            {
                DataSet ds = billService.GetAllBillStatusesByBillKindID(billKindID);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    billStatusInfoList = DataTableToObject.ConvertToList<BillStatusModel>(ds.Tables[0]);
                }
            }

            return billStatusInfoList;
        }

        /// <summary>
        /// 判断能否添加一个表单状态
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billStatus">要添加的表单状态</param>
        /// <returns></returns>
        public BillStatusCheckState CheckAddBillStatus(LoggingSessionInfo loggingSession, BillStatusModel billStatus)
        {
            //获取表单类型下的所有的表单状态
            IList<BillStatusModel> bill_stauts_lst = this.GetAllBillStatusesByBillKindID( billStatus.KindId);
            //为空，可以添加
            if (bill_stauts_lst == null || bill_stauts_lst.Count == 0)
            {
                return BillStatusCheckState.Successful;
            }

            //检查状态
            foreach (BillStatusModel m in bill_stauts_lst)
            {
                if (m.Status == billStatus.Status)
                {
                    return BillStatusCheckState.ExistBillStatus;
                }
            }

            //一个表单类型下，只能有一个开始标志为1的表单状态
            if (billStatus.BeginFlag == 1)
            {
                foreach (BillStatusModel m in bill_stauts_lst)
                {
                    if (m.BeginFlag==1)
                    {
                        return BillStatusCheckState.ExistBegin;
                    }
                }
                return BillStatusCheckState.Successful;
            }

            //一个表单类型下，只能有一个结束标志为1的表单状态
            if (billStatus.EndFlag == 1)
            {
                foreach (BillStatusModel m in bill_stauts_lst)
                {
                    if (m.EndFlag == 1)
                    {
                        return BillStatusCheckState.ExistEnd;
                    }
                }
                return BillStatusCheckState.Successful;
            }
            //一个表单类型下，只能有一个删除标志为1的表单状态
            if (billStatus.DeleteFlag == 1)
            {
                foreach (BillStatusModel m in bill_stauts_lst)
                {
                    if (m.DeleteFlag == 1)
                    {
                        return BillStatusCheckState.ExistDelete;
                    }
                }
                return BillStatusCheckState.Successful;
            }
            //一个表单类型下，只能有一个自定义标志的表单状态
            foreach (BillStatusModel m in bill_stauts_lst)
            {
                if (m.CustomFlag == billStatus.CustomFlag)
                {
                    return BillStatusCheckState.ExistCustom;
                }
            }
            return BillStatusCheckState.Successful;
        }

        /// <summary>
        /// 添加一个表单的状态
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="billStatus">要添加的表单状态的信息</param>
        /// <returns></returns>
        public bool InsertBillStatus(BillStatusModel billStatus)
        {
            if (string.IsNullOrEmpty(billStatus.Id))
                billStatus.Id = this.NewGuid();

            billStatus.CreateUserID = this.loggingSessionInfo.CurrentUser.User_Id;
            billStatus.CreateTime = this.GetCurrentDateTime();
            billStatus.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            return billService.InsertBillStatus(billStatus);
        }

        /// <summary>
        /// 检查是否能够删除某个表单状态
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="billStatusID">要删除的表单状态ID</param>
        /// <returns></returns>
        public bool CanDeleteBillStatus(string billStatusID)
        {
            //检查是否存在表单的状态=要删除的表单状态
            if (billService.GetBillStatusCountUsingBillsById(billStatusID) > 0)
                return false;
            return billService.GetBillStatusCountUsingBillActionRolesById(billStatusID) == 0;
        }

        /// <summary>
        /// 删除一个表单状态
        /// </summary>
        /// <param name="billStatusID">要删除的表单状态ID</param>
        /// <returns></returns>
        public bool DeleteBillStatus(string billStatusID)
        {
            return billService.DeleteBillStatus(billStatusID);
        }

        #endregion

        #region 表单操作
        /// <summary>
        /// 查询某种表单类型下的所有的表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billKindID">表单类型ID</param>
        /// <returns></returns>
        public IList<BillActionModel> GetAllBillActionsByBillKindId(string billKindID)
        {
            IList<BillActionModel> billActionInfoList = new List<BillActionModel>();
            DataSet ds = new DataSet();
            ds = billService.GetAllBillActionsByBillKindId(billKindID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                billActionInfoList = DataTableToObject.ConvertToList<BillActionModel>(ds.Tables[0]);
            }
            return billActionInfoList;
        }
        /// <summary>
        /// 获取表单动作
        /// </summary>
        /// <param name="loggingSession">登录信息</param>
        /// <param name="billKindId">类型标识</param>
        /// <param name="billActionType">动作标识</param>
        /// <returns></returns>
        public BillActionModel GetBillAction(string billKindId, BillActionType billActionType)
        {
            
            BillActionModel billAction = null;
            string conn = "";
            switch (billActionType)
            {
                case BillActionType.Create:
                    conn = " and a.create_flag=1 ";
                    break;
                case BillActionType.Modify:
                    conn = " and a.modify_flag=1 ";
                    break;
                case BillActionType.Approve:
                    conn = " and a.approve_flag=1 ";
                    break;
                case BillActionType.Reject:
                    conn = " and a.reject_flag=1 ";
                    break;
                case BillActionType.Cancel:
                    conn = " and a.cancel_flag=1 ";
                    break;
                case BillActionType.Open:
                    conn = " and 1=1 ";
                    break;
                case BillActionType.Stop:
                    conn = " and 1=1 ";
                    break;
                default:
                    conn = " and 1=1";
                    return null;
            }
            DataSet ds = billService.GetBillAction(billKindId, billActionType, conn);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                billAction = DataTableToObject.ConvertToObject<BillActionModel>(ds.Tables[0].Rows[0]);
            }
            return billAction;
        }

        /// <summary>
        /// 根据表单标识，获取表单的当前按钮（操作）
        /// </summary>
        /// <param name="billId">表单标识</param>
        /// <param name="billStatus">表单当前状态值（1=未审批，10=已审批）</param>
        /// <returns></returns>
        public BillActionInfo GetBillActionByBillId(string billId,string billStatus)
        {
            BillActionInfo billActionInfo = new BillActionInfo();
            if (this.loggingSessionInfo.CurrentLoggingManager.IsApprove == null || this.loggingSessionInfo.CurrentLoggingManager.IsApprove.ToString().Equals("0"))
            {
                if (billStatus.Equals("1"))
                {
                    switch (billStatus)
                    {
                        case  "1":
                            billActionInfo.bill_id = billId;
                            billActionInfo.modify_flag = 1;
                            billActionInfo.delete_flag = 1;
                            billActionInfo.approve_flag = 1;
                            break;
                        case "10":
                            billActionInfo.bill_id = billId;
                            billActionInfo.modify_flag = 0;
                            billActionInfo.delete_flag = 0;
                            billActionInfo.approve_flag = 0;
                            break;
                        case "-1":
                            billActionInfo.bill_id = billId;
                            billActionInfo.modify_flag = 0;
                            billActionInfo.delete_flag = 0;
                            billActionInfo.approve_flag = 0;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                DataSet ds = billService.GetBillActionByBillId(billId, GetBasicRoleId(loggingSessionInfo));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    billActionInfo = DataTableToObject.ConvertToObject<BillActionInfo>(ds.Tables[0].Rows[0]);
                }
            }
            return billActionInfo;
        }

        /// <summary>
        /// 判断能否添加一个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billAction">要添加的表单操作</param>
        /// <returns></returns>
        public BillActionCheckState CheckAddBillAction(LoggingSessionInfo loggingSession, BillActionModel billAction)
        {
            //获取表单类型下的所有的表单状态
            IList<BillActionModel> bill_action_lst = this.GetAllBillActionsByBillKindId(billAction.KindId);
            //为空，可以添加
            if (bill_action_lst == null || bill_action_lst.Count == 0)
            {
                return BillActionCheckState.Successful;
            }

            //检查编码
            foreach (BillActionModel m in bill_action_lst)
            {
                if (m.Code == billAction.Code)
                {
                    return BillActionCheckState.ExistCode;
                }
            }

            //一个表单类型下，只能有一个新建标志为1的表单操作
            if (billAction.CreateFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.CreateFlag == 1)
                    {
                        return BillActionCheckState.ExistCreateAction;
                    }
                }
                return BillActionCheckState.Successful;
            }

            //一个表单类型下，只能有一个修改标志为1的表单操作
            if (billAction.ModifyFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.ModifyFlag == 1)
                    {
                        return BillActionCheckState.ExistModifyAction;
                    }
                }
                return BillActionCheckState.Successful;
            }
            //一个表单类型下，只能有一个审核标志为1的表单操作
            if (billAction.ApproveFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.ApproveFlag == 1)
                    {
                        return BillActionCheckState.ExistApproveAction;
                    }
                }
            }
            //一个表单类型下，只能有一个退回标志为1的表单操作
            if (billAction.RejectFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.RejectFlag == 1)
                    {
                        return BillActionCheckState.ExistRejectAction;
                    }
                }
            }
            //一个表单类型下，只能有一个删除标志为1的表单操作
            if (billAction.CancelFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.CancelFlag == 1)
                    {
                        return BillActionCheckState.ExistCancelAction;
                    }
                }
                return BillActionCheckState.Successful;
            }

            return BillActionCheckState.Successful;
        }


        /// <summary>
        /// 添加一个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billAction"></param>
        /// <returns></returns>
        public bool InsertBillAction( BillActionModel billAction)
        {
            if (string.IsNullOrEmpty(billAction.Id))
                billAction.Id = this.NewGuid();

            billAction.CreateUserID = this.loggingSessionInfo.CurrentUser.User_Id;
            billAction.CreateTime = this.GetCurrentDateTime();
            billAction.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            return billService.InsertBillAction(billAction);
        }

        /// <summary>
        /// 判断能否删除某个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionID">表单操作ID</param>
        /// <returns></returns>
        public bool CanDeleteBillAction(string billActionID)
        {
            return billService.CanDeleteBillAction(billActionID);
        }

        /// <summary>
        /// 删除一个表单操作
        /// </summary>
        /// <param name="billActionID">要删除的表单操作ID</param>
        /// <returns></returns>
        public bool DeleteBillAction(string billActionID)
        {
            return billService.DeleteBillAction(billActionID);
        }
        #endregion

        #region 表单
        /// <summary>
        /// 插入一个Bill的动作日志
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="bill">Bill信息</param>
        /// <param name="billAction">Bill的动作信息</param>
        /// <param name="billActionRole">Bill在当前角色下与Bill的动作的关联信息</param>
        /// <param name="remark">批注</param>
        private void InsertBillActionLog(LoggingSessionInfo loggingSession
            , BillModel bill
            , BillActionModel billAction,
            BillActionRoleModel billActionRole, string remark)
        {
            //BillActionLogInfo billActionLog = new BillActionLogInfo();
            //billActionLog.Id = NewGuid();
            //billActionLog.ActionUserId = loggingSession.CurrentUser.Id;
            //billActionLog.ActionDate = GetCurrentDateTime();
            //billActionLog.BillId = bill.Id;
            //billActionLog.BillActionId = billAction.Id;
            //billActionLog.PreviousStatus = billActionRole.PreviousStatus;
            //billActionLog.CurrentStatus = billActionRole.CurrentStatus;
            //billActionLog.ActionComment = remark;
            //OraMapper.Instance().Insert("BillActionLog.Insert", billActionLog);
        }

        /// <summary>
        /// 新建一个Bill
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="bill">Bill</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService InsertBill(BillModel bill)
        {
            //获取表单类别
            BillKindModel billKind = GetBillKindById(bill.KindId);
            if (billKind == null)
            {
                return BillOperateStateService.NotExistKind;
            }
            //获取表单状态
            BillStatusModel beginBillStatus = GetBillBeginStatus(bill.KindId);
            if (beginBillStatus == null)
            {
                return BillOperateStateService.NotSetBeginStatus;
            }
            BillActionModel billCreateAction = GetBillAction(bill.KindId, BillActionType.Create);
            if (billCreateAction == null)
            {
                return BillOperateStateService.NotSetCreateAction;
            }
            //获取角色与操作集合
            BillActionRoleModel billCreateActionRole = new BillActionRoleModel();
            DataSet ds = new DataSet();
            ds = billService.GetbillCreateActionRole(bill.KindId, billCreateAction.Id, GetBasicRoleId(this.loggingSessionInfo.CurrentUserRole.RoleId), beginBillStatus.Status);
            if (ds != null && ds.Tables[0].Rows.Count > 0) {
                billCreateActionRole = DataTableToObject.ConvertToObject<BillActionRoleModel>(ds.Tables[0].Rows[0]);
            }
            if (billCreateActionRole == null)
            {
                return BillOperateStateService.NotAllowCreate;
            }
      
            //if (billKind.MoneyFlag == 1)
            //{
            //    if ((bill.Money < billCreateActionRole.MinMoney) || (bill.Money > billCreateActionRole.MaxMoney))
            //    {
            //        return BillOperateStateService.OutOfMoneyScope;
            //    }
            //}

            bill.AddUserId = this.loggingSessionInfo.CurrentUser.User_Id;
            bill.AddDate = GetCurrentDateTime();
            bill.Status = billCreateActionRole.CurrentStatus;
            //插入bill信息
            billService.InsertBill(bill);

            //InsertBillActionLog(loggingSession, bill, billCreateAction, billCreateActionRole, null);

            return BillOperateStateService.CreateSuccessful;
        }


        /// <summary>
        /// 根据Bill的Id查询Bill
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill的Id</param>
        /// <returns></returns>
        public BillModel GetBillById(string billId)
        {
            BillModel billInfo = new BillModel();
            DataSet ds = billService.GetBillById(billId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                billInfo = DataTableToObject.ConvertToObject<BillModel>(ds.Tables[0].Rows[0]);
            }
            return billInfo;
        }

        #region delete bill
        /// <summary>
        /// 删除一个Bill(事务由调用方负责)
        /// </summary>
        /// <param name="billId">Bill</param>
        /// <param name="remark">批注</param>
        /// <returns>操作的状态</returns>
        private BillOperateStateService DeleteBill(string billId, string remark)
        {
            BillModel bill = GetBillById(billId);
            if (bill == null)
            {
                return BillOperateStateService.NotExist;
            }

            BillKindModel billKind = GetBillKindById( bill.KindId);
            if (billKind == null)
            {
                return BillOperateStateService.NotExistKind;
            }
            BillStatusModel billStatus = GetBillStatusByKindIdAndStatus(billKind.Id, bill.Status);
            if (billStatus == null)
            {
                return BillOperateStateService.NotExistStatus;
            }
            BillStatusModel billDeleteStatus = GetBillDeleteStatus( billKind.Id);
            if (billDeleteStatus == null)
            {
                return BillOperateStateService.NotSetDeleteStatus;
            }
            BillActionModel billDeleteAction = GetBillAction(bill.KindId, BillActionType.Cancel);
            if (billDeleteAction == null)
            {
                return BillOperateStateService.NotSetModifyAction;
            }
            Hashtable hashTable = new Hashtable();
            BillActionRoleModel billDeleteActionRole = new BillActionRoleModel();
            DataSet ds = new DataSet();
            ds = billService.GetbillCreateActionRole(bill.KindId, billDeleteAction.Id, GetBasicRoleId(this.loggingSessionInfo.CurrentUserRole.RoleId), billStatus.Status);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                billDeleteActionRole = DataTableToObject.ConvertToObject<BillActionRoleModel>(ds.Tables[0].Rows[0]);
            }
            if (billDeleteActionRole == null)
            {
                return BillOperateStateService.NotAllowCancel;
            }

            //if (billKind.MoneyFlag == 1)
            //{
            //    if ((bill.Money < billDeleteActionRole.MinMoney) || (bill.Money > billDeleteActionRole.MaxMoney))
            //    {
            //        return BillOperateStateService.OutOfMoneyScope;
            //    }
            //}

            bill.ModifyUserId = this.loggingSessionInfo.CurrentUser.User_Id;
            bill.ModifyDate = GetCurrentDateTime();
            bill.Status = billDeleteActionRole.CurrentStatus;
            billService.UpdateBill(bill);
            //InsertBillActionLog(loggingSession, bill, billDeleteAction, billDeleteActionRole, remark);

            return BillOperateStateService.CancelSuccessful;
        }
        /// <summary>
        /// 批量删除Bill(含事务)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billList">要删除的Bill的Id的列表</param>
        /// <param name="remark">批注</param>
        /// <param name="billId">输出标识</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService DeleteBills( IList<string> billList, string remark, out string billId)
        {
            billId = null;
            if (billList == null || billList.Count == 0)
            {
                return BillOperateStateService.CancelSuccessful;
            }
            //cSqlMapper.Instance().BeginTransaction();
            try
            {
                BillOperateStateService state;
                foreach (string deleteBillId in billList)
                {
                    state = DeleteBill(deleteBillId, remark);
                    if (state != BillOperateStateService.CancelSuccessful)
                    {
                        //cSqlMapper.Instance().RollBackTransaction();
                        billId = deleteBillId;
                        return state;
                    }
                }
                //cSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                //cSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            billId = "";
            return BillOperateStateService.CancelSuccessful;
        }
        #endregion delete bill

        #region approve bill
        /// <summary>
        /// 批准一个Bill(事务由调用方负责)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill</param>
        /// <param name="remark">批注</param>
        /// <param name="strBillActionType">状态</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService ApproveBill( string billId, string remark, BillActionType strBillActionType)
        {
            if (strBillActionType.Equals(""))
            {
                strBillActionType = BillActionType.Approve;
            }
            BillModel bill = GetBillById(billId);
            if (bill == null)
            {
                return BillOperateStateService.NotExist;
            }

            BillKindModel billKind = GetBillKindById( bill.KindId);
            if (billKind == null)
            {
                return BillOperateStateService.NotExistKind;
            }
            BillStatusModel currentBillStatus = GetBillStatusByKindIdAndStatus(billKind.Id, bill.Status);
            if (currentBillStatus == null)
            {
                return BillOperateStateService.NotExistStatus;
            }
            BillActionModel billApproveAction = GetBillAction( bill.KindId, strBillActionType);
            if (billApproveAction == null)
            {
                return BillOperateStateService.NotSetApproveAction;
            }
            Hashtable hashTable = new Hashtable();
            BillActionRoleModel billApproveActionRole = new BillActionRoleModel();
            DataSet ds = new DataSet();
            ds = billService.GetbillCreateActionRole(bill.KindId, billApproveAction.Id, GetBasicRoleId(this.loggingSessionInfo.CurrentUserRole.RoleId), currentBillStatus.Status);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                billApproveActionRole = DataTableToObject.ConvertToObject<BillActionRoleModel>(ds.Tables[0].Rows[0]);
            }

            if (billApproveActionRole == null)
            {
                return BillOperateStateService.NotAllowApprove;
            }

            //if (billKind.MoneyFlag == 1)
            //{
            //    if ((bill.Money < billApproveActionRole.MinMoney) || (bill.Money > billApproveActionRole.MaxMoney))
            //    {
            //        return BillOperateStateService.OutOfMoneyScope;
            //    }
            //}

            bill.ModifyUserId = this.loggingSessionInfo.CurrentUser.User_Id;
            bill.ModifyDate = GetCurrentDateTime();
            bill.Status = billApproveActionRole.CurrentStatus;

            BillStatusModel nextBillStatus = GetBillStatusByKindIdAndStatus(billKind.Id, billApproveActionRole.CurrentStatus);
            if (nextBillStatus == null)
            {
                return BillOperateStateService.NotExistStatus;
            }
            billService.UpdateBill(bill);
            //InsertBillActionLog(loggingSession, bill, billApproveAction, billApproveActionRole, remark);
        
            return BillOperateStateService.ApproveSuccessful;
        }


        /// <summary>
        /// 批准一个Bill(事务由调用方负责) Jermyn2012-09-17
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill</param>
        /// <param name="remark">批注</param>
        /// <param name="strBillActionType">状态</param>
        /// <param name="strError">输出错误参数</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService ApproveBill(string billId, string remark, BillActionType strBillActionType, out string strError)
        {
            if (strBillActionType.Equals(""))
            {
                strError = "BillActionType.Approve";
                strBillActionType = BillActionType.Approve;
            }
            BillModel bill = GetBillById(billId);
            if (bill == null)
            {
                strError = "BillOperateStateService.NotExist";
                return BillOperateStateService.NotExist;
            }

            BillKindModel billKind = GetBillKindById( bill.KindId);
            if (billKind == null)
            {
                strError = "BillOperateStateService.NotExistKind";
                return BillOperateStateService.NotExistKind;
            }
            BillStatusModel currentBillStatus = GetBillStatusByKindIdAndStatus(billKind.Id, bill.Status);
            if (currentBillStatus == null)
            {
                strError = "BillOperateStateService.NotExistStatus";
                return BillOperateStateService.NotExistStatus;
            }
            BillActionModel billApproveAction = GetBillAction( bill.KindId, strBillActionType);
            if (billApproveAction == null)
            {
                strError = "BillOperateStateService.NotSetApproveAction";
                return BillOperateStateService.NotSetApproveAction;
            }
            //获取审批权限
            DataSet ds = billService.GetbillCreateActionRole(bill.KindId, billApproveAction.Id, GetBasicRoleId(this.loggingSessionInfo.CurrentUserRole.RoleId), currentBillStatus.Status);
            BillActionRoleModel billApproveActionRole = new BillActionRoleModel();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                billApproveActionRole = DataTableToObject.ConvertToObject<BillActionRoleModel>(ds.Tables[0].Rows[0]);
            }
            if (billApproveActionRole == null)
            {
                strError = "BillOperateStateService.NotAllowApprove--"
                    //+ "RoleId:" + GetBasicRoleId(loggingSession.CurrentUserRole.RoleId).ToString() + "----"
                    //+ "BillKindId:" + bill.KindId + "----"
                    //+ "BillActionId:" + billApproveAction.Id + "----"
                    //+ "PreviousBillStatus:" + currentBillStatus.Status + "----"
                         + "InoutStatus" + bill.Status + "--" + bill.BillStatusDescription + "--" + bill.Id
                          ;
                return BillOperateStateService.NotAllowApprove;
            }
            
            //if (billKind.MoneyFlag == 1)
            //{
            //    if ((bill.Money < billApproveActionRole.MinMoney) || (bill.Money > billApproveActionRole.MaxMoney))
            //    {
            //        strError = "BillOperateStateService.OutOfMoneyScope";
            //        return BillOperateStateService.OutOfMoneyScope;
            //    }
            //}

            bill.ModifyUserId = this.loggingSessionInfo.CurrentUser.User_Id;
            bill.ModifyDate = GetCurrentDateTime();
            bill.Status = billApproveActionRole.CurrentStatus;

            BillStatusModel nextBillStatus = GetBillStatusByKindIdAndStatus(billKind.Id, billApproveActionRole.CurrentStatus);
            if (nextBillStatus == null)
            {
                strError = "BillOperateStateService.NotExistStatus";
                return BillOperateStateService.NotExistStatus;
            }
            //修改bill
            billService.UpdateBill(bill);
            //InsertBillActionLog(loggingSession, bill, billApproveAction, billApproveActionRole, remark);
            strError = "BillOperateStateService.ApproveSuccessful";
            return BillOperateStateService.ApproveSuccessful;
        }
       

        /// <summary>
        /// 批量批准Bill(含事务)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billList">要批准的Bill的Id的列表</param>
        /// <param name="remark">批注</param>
        /// <param name="strBillActionType">第一个不能批准的Bill的Code</param>
        /// <param name="billId">输出标识</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService ApproveBills(IList<string> billList, string remark, BillActionType strBillActionType, out string billId)
        {
            billId = null;
            //如果没有审核的,则直接返回成功
            if (billList == null || billList.Count == 0)
            {
                return BillOperateStateService.ApproveSuccessful;
            }
            //查找审核后的状态是最终状态的表单的种类的编码列表, 主要是为了刷新物化视图用
            //IList<string> bill_kind_code_list = GetApprovedBillKindCodes(billList);

            //cSqlMapper.Instance().BeginTransaction();
            try
            {
                BillOperateStateService state;
                foreach (string approveBillId in billList)
                {
                    state = ApproveBill(approveBillId, remark, strBillActionType);
                    if (state != BillOperateStateService.ApproveSuccessful)
                    {
                        //cSqlMapper.Instance().RollBackTransaction();
                        billId = approveBillId;
                        return state;
                    }
                }
                //cSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                //cSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }

           
            return BillOperateStateService.ApproveSuccessful;
        }

        #endregion approve bill
        #endregion


        #region 判断是否有新建权限
        /// <summary>
        /// 查看当前用户能否新建一个Bill
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="unitRelationModeId">单位的关联模式</param>
        /// <param name="billKindId">新建的Bill的种类的Id</param>
        /// <returns></returns>
        public bool CanCreateBill(string billKindId)
        {
            if (this.loggingSessionInfo.CurrentLoggingManager.IsApprove == null || this.loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
            {
                return true;
            }
            else
            {
                return billService.CanCreateBill(billKindId, GetBasicRoleId(this.loggingSessionInfo)) > 0;
            }
        }

        /// <summary>
        /// 查看当前用户能否新建一个表单种类的表单
        /// </summary>
        /// <param name="billKindCode">新建的Bill的种类的编码</param>
        /// <returns></returns>
        public bool CanCreateBillKind(string billKindCode)
        {
            int count = 0;
            if (this.loggingSessionInfo.CurrentLoggingManager.IsApprove == null || this.loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
            {
                count = 1;
            }
            else
            {
                count = billService.CanCreateBillKind(billKindCode, GetBasicRoleId(this.loggingSessionInfo));
            }
            return count > 0;
        }
        /// <summary>
        /// 根据标识判断Bill是否存在
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="billId"></param>
        /// <returns></returns>
        public bool CanHaveBill(string billId)
        {
            return billService.CanHaveBill(billId);   
        }

        /// <summary>
        /// 查看当前用户能否批准一个Bill
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录用户的Session信息</param>
        /// <param name="unitRelationModeId">单位的关联模式</param>
        /// <param name="billId">Bill的Id</param>
        /// <returns></returns>
        public bool CanApproveBill( string billId)
        {
            return billService.CanApproveBill(billId);
        }
        #endregion 

        #region 角色与表单操作

        /// <summary>
        /// 查询角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="condition">BillKindID:表单类型;RoleID:角色;</param>
        /// <returns></returns>
        public IList<SelectBillActionRoleInfo> SelectBillActionRoleList(Hashtable condition)
        {
            IList<SelectBillActionRoleInfo> selelInfoList = new List<SelectBillActionRoleInfo>();
            DataSet ds = new DataSet();
            ds = billService.SelectBillActionRoleList(condition);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                selelInfoList = DataTableToObject.ConvertToList<SelectBillActionRoleInfo>(ds.Tables[0]);
            }
            return selelInfoList;
        }

        /// <summary>
        /// 添加一个角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionRole">角色与表单操作的关系</param>
        /// <returns></returns>
        public bool InsertBillActionRole(BillActionRoleModel billActionRole)
        {
            if (string.IsNullOrEmpty(billActionRole.Id))
                billActionRole.Id = this.NewGuid();

            billActionRole.CreateUserID = this.loggingSessionInfo.CurrentUser.User_Id;
            billActionRole.CreateTime = this.GetCurrentDateTime();

            return billService.InsertBillActionRole(billActionRole);
        }

        /// <summary>
        /// 删除一个角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionRoleID">角色与表单操作的关系的ID</param>
        /// <returns></returns>
        public bool DeleteBillActionRole(string billActionRoleID)
        {
            return billService.DeleteBillActionRole(billActionRoleID);
        }

        /// <summary>
        /// 判断能否添加一个角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionRole">要添加的角色与表单操作的关系</param>
        /// <returns></returns>
        public BillActionRoleCheckState CheckAddBillActionRole(LoggingSessionInfo loggingSession, BillActionRoleModel billActionRole)
        {
            Hashtable ht = new Hashtable();
            //查询表单操作
            ht.Add("BillActionId", billActionRole.ActionId);
            BillActionModel bill_action = new BillActionModel();
            DataSet ds = billService.GetBillActionByActionId(billActionRole.ActionId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                bill_action = DataTableToObject.ConvertToObject<BillActionModel>(ds.Tables[0].Rows[0]);
            }
            if (bill_action == null)
            {
                return BillActionRoleCheckState.NotExistAction;
            }
            
            //查询角色与表单的关系列表
            ht.Add("BillKindID", billActionRole.KindId);
            ht.Add("RoleID", billActionRole.RoleId);
            IList<SelectBillActionRoleInfo> bill_action_role_lst = this.SelectBillActionRoleList(ht);
            //角色与表单的关系为空，可以添加
            if (bill_action_role_lst == null || bill_action_role_lst.Count == 0)
            {
                return BillActionRoleCheckState.Successful;
            }

            //一个角色对一个表单，只能有一个新建操作
            if (bill_action.CreateFlag == 1)
            {
                foreach (BillActionRoleModel m in bill_action_role_lst)
                {
                    if (m.RoleId == billActionRole.RoleId && m.ActionId == billActionRole.ActionId)
                    {
                        return BillActionRoleCheckState.ExistCreate;
                    }
                }
                return BillActionRoleCheckState.Successful;
            }

            //一个角色对一个表单，对同一个前置状态，只能有一个修改、审核、退回操作
            foreach (BillActionRoleModel m in bill_action_role_lst)
            {
                if (m.RoleId == billActionRole.RoleId && m.ActionId == billActionRole.ActionId && m.PreviousStatus == billActionRole.PreviousStatus)
                {
                    //修改操作
                    if (bill_action.ModifyFlag == 1)
                    {
                        return BillActionRoleCheckState.ExistModify;
                    }
                    else
                    {
                        //审核操作
                        if (bill_action.ApproveFlag == 1)
                        {
                            return BillActionRoleCheckState.ExistApprove;
                        }
                        else
                        {
                            //退回操作
                            if (bill_action.RejectFlag == 1)
                            {
                                return BillActionRoleCheckState.ExistReject;
                            }
                            else
                            {
                                //删除操作
                                return BillActionRoleCheckState.ExistCancel;
                            }
                        }
                    }
                }
            }
            return BillActionRoleCheckState.Successful;
        }
        #endregion

        #region 批量插入（临时）
        public bool SetBatBillActionRole(string RoleId)
        {
            return billService.SetBatBillActionRole(RoleId);
        }
        #endregion

    }
}
