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
                result = this._currentDAO.SetVipAmountChange(CustomerId,AmountSourceId,VipId,Amount,ObjectId,Remark,tran,InOut,out strError) ?? "0";

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
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        public decimal GetVipByEndAmount(string vipId,System.Data.SqlClient.SqlTransaction tran)
        {
            var sql = string.Format("select * from VipAmount where VipID='{0}' and isdelete=0 ",vipId);
            return _currentDAO.GetVipByEndAmount(vipId,tran);

        }
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <param name="tran"></param>
        /// <param name="type">����</param>
        /// <param name="objectId">��Դid</param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public bool AddVipEndAmount(string userId, decimal amount, System.Data.SqlClient.SqlTransaction tran, string type, string objectId, LoggingSessionInfo loggingSessionInfo)
        {
            bool b = false;
            //���¸����˻��Ŀ�ʹ����� 
            try
            {
                var vipAmountBll = new VipAmountBLL(loggingSessionInfo);

                var vipAmountEntity = vipAmountBll.GetByID(userId);

                if (vipAmountEntity == null)
                {
                    vipAmountEntity = new VipAmountEntity
                    {
                        VipId = userId,
                        BeginAmount = amount,
                        InAmount = amount,
                        EndAmount = amount,
                        TotalAmount=amount,
                        IsLocking = 0
                    };

                    vipAmountBll.Create(vipAmountEntity, tran);


                    // throw new APIException("����δ��ͨ�����˻�") { ErrorCode = 121 };
                }
                else
                {
                    vipAmountEntity.EndAmount = (vipAmountEntity.EndAmount == null ? 0 : vipAmountEntity.EndAmount.Value) + amount;
                    vipAmountEntity.InAmount = (vipAmountEntity.InAmount == null ? 0 : vipAmountEntity.InAmount.Value) + amount;
                    vipAmountEntity.TotalAmount = (vipAmountEntity.TotalAmount == null ? 0 : vipAmountEntity.TotalAmount.Value) + amount;

                    vipAmountBll.Update(vipAmountEntity, tran);
                }


                //Insert VipAmountDetail

                var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);

                var vipAmountDetailEntity = new VipAmountDetailEntity
                {
                    AmountSourceId = type,
                    Amount = amount,
                    VipAmountDetailId = Guid.NewGuid(),
                    VipId = userId,
                    ObjectId = objectId
                };

                vipamountDetailBll.Create(vipAmountDetailEntity, tran);
            }
            catch (Exception ex)
            {                
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            finally
            {
                b = true;
            }

            return b;
        }
        /// <summary>
        /// ��ֵ(�޸Ķ���״̬ʹ�ã�����ͬ)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <param name="tran"></param>
        /// <param name="type">����</param>
        /// <param name="objectId">��Դid</param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public bool AddVipEndAmount(string userId, decimal amount, IDbTransaction tran, string type, string objectId, LoggingSessionInfo loggingSessionInfo)
        {
            bool b = false;
            //���¸����˻��Ŀ�ʹ����� 
            try
            {
                var vipAmountBll = new VipAmountBLL(loggingSessionInfo);

                var vipAmountEntity = vipAmountBll.GetByID(userId);

                if (vipAmountEntity == null)
                {
                    vipAmountEntity = new VipAmountEntity
                    {
                        VipId = userId,
                        BeginAmount = amount,
                        InAmount = amount,
                        EndAmount = amount,
                        TotalAmount = amount,
                        IsLocking = 0
                    };

                    vipAmountBll.Create(vipAmountEntity, tran);


                    // throw new APIException("����δ��ͨ�����˻�") { ErrorCode = 121 };
                }
                else
                {
                    vipAmountEntity.EndAmount = (vipAmountEntity.EndAmount == null ? 0 : vipAmountEntity.EndAmount.Value) + amount;
                    vipAmountEntity.InAmount = (vipAmountEntity.InAmount == null ? 0 : vipAmountEntity.InAmount.Value) + amount;
                    vipAmountEntity.TotalAmount = (vipAmountEntity.TotalAmount == null ? 0 : vipAmountEntity.TotalAmount.Value) + amount;

                    vipAmountBll.Update(vipAmountEntity, tran);
                }


                //Insert VipAmountDetail

                var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);

                var vipAmountDetailEntity = new VipAmountDetailEntity
                {
                    AmountSourceId = type,
                    Amount = amount,
                    VipAmountDetailId = Guid.NewGuid(),
                    VipId = userId,
                    ObjectId = objectId
                };

                vipamountDetailBll.Create(vipAmountDetailEntity, tran);

                tran.Commit();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }

            return b;
        }

            /// <summary>
        /// ���ִ���ͨ�÷�����
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="retuanAmount"></param>
        /// <param name="orderId"></param>
        /// <param name="amountSourceId"></param>
        public void AddReturnAmount(string vipId, decimal returnAmount, string orderId, string amountSourceId, LoggingSessionInfo loggingSessionInfo)
        {
            var vipAmountDao = new VipAmountBLL(loggingSessionInfo);
            var vipAmountDetailDao = new VipAmountDetailBLL(loggingSessionInfo);
            var vipAmountInfo = vipAmountDao.GetByID(vipId);
            if (vipAmountInfo == null)  //���˻�����
            {
                vipAmountInfo = new VipAmountEntity
                {
                    VipId = vipId,
                    ReturnAmount = returnAmount,
                    IsLocking = 0
                };
                vipAmountDao.Create(vipAmountInfo);
            }
            else
            {
                vipAmountInfo.ReturnAmount = (vipAmountInfo.ReturnAmount == null ? 0 : vipAmountInfo.ReturnAmount.Value) + returnAmount;
                vipAmountDao.Update(vipAmountInfo);
            }
            //���������¼
            var vipAmountDetailEntity = new VipAmountDetailEntity
            {
                AmountSourceId = amountSourceId,
                Amount = returnAmount,
                VipAmountDetailId = Guid.NewGuid(),
                VipId = vipId,
                ObjectId = orderId
            };
            vipAmountDetailDao.Create(vipAmountDetailEntity);

        }
    }
}