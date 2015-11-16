/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/22 14:32:02
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

using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipAmountBLL
    {

        #region Jermyn20140710 �����
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="CustomerId">�ͻ���ʶ</param>
        /// <param name="AmountSourceId">��Դ</param>
        /// <param name="VipId">��Ա��ʶ</param>
        /// <param name="Amount">���</param>
        /// <param name="ObjectId">������Դ</param>
        /// <param name="remark">˵��</param>
        /// <param name="InOut">��ӻ������ѣ�In ���� Out</param>
        /// <param name="strError">�������</param>
        /// <param name="tran">������</param>
        /// <returns></returns>
        public bool SetVipAmountChange(string CustomerId
                                    , int AmountSourceId
                                    , string VipId
                                    , decimal Amount
                                    , string ObjectId
                                    , string Remark
                                    , string InOut
                                    , out string strError
                                    , System.Data.SqlClient.SqlTransaction tran = null
                                    )
        {
            try
            {
                strError = "Success!";
                bool bReturn = true;
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("soureId: {0}, customerId: {1}, vipId: {2}, objectId: {3}, point: {4}", AmountSourceId, CustomerId, VipId, ObjectId, Amount) });

                #region
                if (VipId == null || VipId.Equals(""))
                {
                    strError = "û�л�Ա��ʶ.";
                    return false;
                }

                if (Amount.Equals(""))
                {
                    strError = "û�н��.";
                    return false;
                }

                if (InOut == null || InOut.Equals(""))
                {
                    strError = "û��˵�����ۼӻ���֧��.";
                    return false;
                }
                #endregion

                #region
                string result = "0";
                result = this._currentDAO.SetVipAmountChange(CustomerId, AmountSourceId, VipId, Amount, ObjectId, Remark, tran, InOut, out strError) ?? "0";

                if (result.Equals("0"))
                {
                    return bReturn;
                }
                else
                {
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        public decimal GetVipByEndAmount(string vipId, System.Data.SqlClient.SqlTransaction tran)
        {
            var sql = string.Format("select * from VipAmount where VipID='{0}' and isdelete=0 ", vipId);
            return _currentDAO.GetVipByEndAmount(vipId, tran);

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="vipInfo">��Ա��Ϣ</param>
        /// <param name="unitInfo">�ŵ���Ϣ</param>
        /// <param name="detailInfo">�������ϸ</param>
        /// <param name="tran">����</param>
        /// <param name="loggingSessionInfo">��¼��Ϣ</param>
        /// <returns></returns>
        public string AddVipAmount(VipEntity vipInfo, t_unitEntity unitInfo, VipAmountEntity vipAmountEntity, VipAmountDetailEntity detailInfo, SqlTransaction tran, LoggingSessionInfo loggingSessionInfo)
        {
            string vipAmountDetailId = string.Empty;//�����ϸID
            //���¸����˻��Ŀ�ʹ����� 
            try
            {
                var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                //var vipAmountEntity = vipAmountBll.GetByID(vipInfo);
                if (vipAmountEntity == null)
                {
                    vipAmountEntity = new VipAmountEntity
                    {
                        VipId = vipInfo.VIPID,
                        VipCardCode = vipInfo.VipCode,
                        BeginAmount = 0,
                        InAmount = detailInfo.Amount,
                        OutAmount = 0,
                        EndAmount = detailInfo.Amount,
                        TotalAmount = detailInfo.Amount,
                        BeginReturnAmount = 0,
                        InReturnAmount = 0,
                        OutReturnAmount = 0,
                        ReturnAmount = 0,
                        ImminentInvalidRAmount = 0,
                        InvalidReturnAmount = 0,
                        ValidReturnAmount = 0,
                        TotalReturnAmount = 0,
                        IsLocking = 0,
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    vipAmountBll.Create(vipAmountEntity, tran);
                    // throw new APIException("����δ��ͨ�����˻�") { ErrorCode = 121 };
                }
                else
                {
                    if (detailInfo.Amount > 0)
                    {
                        vipAmountEntity.InAmount = (vipAmountEntity.InAmount == null ? 0 : vipAmountEntity.InAmount.Value) + detailInfo.Amount;
                        vipAmountEntity.TotalAmount = (vipAmountEntity.TotalAmount == null ? 0 : vipAmountEntity.TotalAmount.Value) + detailInfo.Amount;
                    }
                    else
                        vipAmountEntity.OutAmount = (vipAmountEntity.OutAmount == null ? 0 : vipAmountEntity.OutAmount.Value) + System.Math.Abs(detailInfo.Amount.Value);
                    vipAmountEntity.EndAmount = (vipAmountEntity.EndAmount == null ? 0 : vipAmountEntity.EndAmount.Value) + detailInfo.Amount;

                    vipAmountBll.Update(vipAmountEntity, tran);
                }
                //Insert VipAmountDetail
                var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);
                var vipAmountDetailEntity = new VipAmountDetailEntity
                {
                    VipAmountDetailId = Guid.NewGuid(),
                    VipId = vipInfo.VIPID,
                    VipCardCode = vipInfo.VipCode,
                    UnitID = unitInfo != null ? unitInfo.unit_id : "",
                    UnitName = unitInfo != null ? unitInfo.unit_name : "",
                    Amount = detailInfo.Amount,
                    UsedReturnAmount = 0,
                    EffectiveDate = DateTime.Now,
                    DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
                    AmountSourceId = detailInfo.AmountSourceId,
                    ObjectId = detailInfo.ObjectId,
                    Reason = detailInfo.Reason,
                    Remark = detailInfo.Remark,
                    CustomerID = loggingSessionInfo.ClientID
                };
                vipamountDetailBll.Create(vipAmountDetailEntity, tran);

                vipAmountDetailId = vipAmountDetailEntity.VipAmountDetailId.ToString();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            return vipAmountDetailId;
        }
        /// <summary>
        /// ���ִ���
        /// </summary>
        /// <param name="vipInfo">��Ա��Ϣ</param>
        /// <param name="unitInfo">�ŵ���Ϣ</param>
        /// <param name="detailInfo">�������ϸ</param>
        /// <param name="tran">����</param>
        /// <param name="loggingSessionInfo">��¼��Ϣ</param>
        /// <param name="amountSourceId"></param>
        public string AddReturnAmount(VipEntity vipInfo, t_unitEntity unitInfo, VipAmountEntity vipAmountInfo, VipAmountDetailEntity detailInfo, SqlTransaction tran, LoggingSessionInfo loggingSessionInfo)
        {
            string vipAmountDetailId = string.Empty;//�����ϸID
            var vipAmountDao = new VipAmountBLL(loggingSessionInfo);
            var vipAmountDetailDao = new VipAmountDetailBLL(loggingSessionInfo);
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

            //��ȡ������Ч��
            int cashValidPeriod = 2;  //Ĭ��Ϊ1��ҵ����ʱ���ȥ1
            var cashValidPeriodInfo = customerBasicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "CashValidPeriod", CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
            if (cashValidPeriodInfo != null)
                cashValidPeriod = int.Parse(cashValidPeriodInfo.SettingValue);

            //var vipAmountInfo = vipAmountDao.GetByID(vipId);
            try
            {
                if (vipAmountInfo == null)  //���˻�����
                {
                    vipAmountInfo = new VipAmountEntity
                    {
                        VipId = vipInfo.VIPID,
                        VipCardCode = vipInfo.VipCode,
                        BeginAmount = 0,
                        InAmount = 0,
                        OutAmount = 0,
                        EndAmount = 0,
                        TotalAmount = 0,
                        BeginReturnAmount = 0,
                        InReturnAmount = detailInfo.Amount,
                        OutReturnAmount = 0,
                        ReturnAmount = detailInfo.Amount,
                        ImminentInvalidRAmount = 0,
                        InvalidReturnAmount = 0,
                        ValidReturnAmount = detailInfo.Amount,
                        TotalReturnAmount = detailInfo.Amount,
                        IsLocking = 0,
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    vipAmountDao.Create(vipAmountInfo, tran);
                }
                else
                {
                    if (detailInfo.Amount > 0)
                    {
                        vipAmountInfo.InReturnAmount = (vipAmountInfo.InReturnAmount == null ? 0 : vipAmountInfo.InReturnAmount.Value) + detailInfo.Amount;
                        vipAmountInfo.TotalReturnAmount = (vipAmountInfo.TotalReturnAmount == null ? 0 : vipAmountInfo.TotalReturnAmount.Value) + detailInfo.Amount;
                    }
                    else
                        vipAmountInfo.OutReturnAmount = (vipAmountInfo.OutReturnAmount == null ? 0 : vipAmountInfo.OutReturnAmount.Value) + System.Math.Abs(detailInfo.Amount.Value);
                    vipAmountInfo.ValidReturnAmount = (vipAmountInfo.ValidReturnAmount == null ? 0 : vipAmountInfo.ValidReturnAmount.Value) + detailInfo.Amount;
                    vipAmountInfo.ReturnAmount = (vipAmountInfo.ReturnAmount == null ? 0 : vipAmountInfo.ReturnAmount.Value) + detailInfo.Amount;

                    vipAmountDao.Update(vipAmountInfo);
                }
                //���������¼
                var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);
                var vipAmountDetailEntity = new VipAmountDetailEntity
                {
                    VipAmountDetailId = Guid.NewGuid(),
                    VipId = vipInfo.VIPID,
                    VipCardCode = vipInfo.VipCode,
                    UnitID = unitInfo != null ? unitInfo.unit_id : "",
                    UnitName = unitInfo != null ? unitInfo.unit_name : "",
                    Amount = detailInfo.Amount,
                    UsedReturnAmount = 0,
                    Reason = detailInfo.Reason,
                    Remark = detailInfo.Remark,
                    EffectiveDate = DateTime.Now,
                    DeadlineDate = Convert.ToDateTime((DateTime.Now.Year + cashValidPeriod - 1) + "-12-31 23:59:59 "),//ʧЧʱ��,
                    AmountSourceId = detailInfo.AmountSourceId,
                    ObjectId = detailInfo.ObjectId,
                    CustomerID = loggingSessionInfo.ClientID
                };
                vipamountDetailBll.Create(vipAmountDetailEntity, tran);

                vipAmountDetailId = vipAmountDetailEntity.VipAmountDetailId.ToString();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            return vipAmountDetailId;
        }

    }
}