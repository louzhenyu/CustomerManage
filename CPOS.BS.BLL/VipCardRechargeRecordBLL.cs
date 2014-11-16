/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipCardRechargeRecordBLL
    {
        #region �б��ȡ
        /// <summary>
        /// �б��ȡ
        /// </summary>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public List<VipCardRechargeRecordEntity> GetList(VipCardRechargeRecordEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<VipCardRechargeRecordEntity> list = new List<VipCardRechargeRecordEntity>();

            DataSet ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipCardRechargeRecordEntity>(ds.Tables[0]);
            }

            return list;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetListCount(VipCardRechargeRecordEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region ��ֵ
        /// <summary>
        /// ��ֵ�ύ
        /// </summary>
        /// <param name="VipCardID">��Ա����ʶ�����롿</param>
        /// <param name="RechargeAmount">��ֵ�����롿</param>
        /// <param name="RechargeNo">СƱ�š����롿</param>
        /// <param name="PaymentTypeID">��ֵ��ʽ�����롿</param>
        /// <param name="UnitID">��ֵ�ŵ꡾���롿</param>
        /// <param name="strError">�����Ϣ</param>
        /// <returns></returns>
        public bool SetVipCardRecjargeRpecord(string VipCardID
                                            , decimal RechargeAmount
                                            , string RechargeNo
                                            , string PaymentTypeID
                                            , string UnitID
                                            , out string strError)
        {
            try
            {
                #region �ж�������Ϣ�Ƿ�Ϸ�
                if (VipCardID == null || VipCardID.Trim().Equals(""))
                {
                    strError = "��Ա���ű�ʶ����Ϊ��.";
                    return false;
                }
                if (RechargeAmount.ToString().Trim().Equals(""))
                {
                    strError = "��ֵ����Ϊ��.";
                    return false;
                }
                if (RechargeNo == null || RechargeNo.ToString().Trim().Equals(""))
                {
                    strError = "СƱ�Ų���Ϊ��.";
                    return false;
                }
                if (PaymentTypeID == null || PaymentTypeID.ToString().Trim().Equals(""))
                {
                    strError = "֧����ʽ����Ϊ��.";
                    return false;
                }
                if (UnitID == null || UnitID.ToString().Trim().Equals(""))
                {
                    strError = "��ֵ�ŵ겻��Ϊ��.";
                    return false;
                }
                #endregion
                //1.��ȡ��Ա����Ϣ
                #region ��ȡ��Ա����Ϣ
                VipCardBLL vipCardServer = new VipCardBLL(this.CurrentUserInfo);
                VipCardEntity vipCardInfo = new VipCardEntity();
                vipCardInfo = vipCardServer.GetByID(VipCardID);
                if (vipCardInfo == null)
                {
                    strError = "��Ա����Ϣ������.";
                    return false;
                }
                if (vipCardInfo.BalanceAmount == null || vipCardInfo.BalanceAmount.ToString().Equals(""))
                {
                    vipCardInfo.BalanceAmount = 0;
                }
                if (vipCardInfo.TotalAmount == null || vipCardInfo.TotalAmount.ToString().Equals(""))
                {
                    vipCardInfo.TotalAmount = 0;
                }
                #endregion
                //2.�޸Ļ�Ա����Ϣ
                #region
                VipCardEntity vipCard = new VipCardEntity();
                vipCard.VipCardID = vipCardInfo.VipCardID;
                vipCard.BalanceAmount = vipCardInfo.BalanceAmount + RechargeAmount;
                vipCard.TotalAmount = vipCardInfo.TotalAmount + RechargeAmount;
                #endregion
                //3.�����ֵ��¼
                #region
                VipCardRechargeRecordEntity vipCardRRInfo = new VipCardRechargeRecordEntity();
                vipCardRRInfo.RechargeRecordID = JIT.CPOS.BS.BLL.BaseService.NewGuidPub();
                vipCardRRInfo.RechargeNo = RechargeNo;
                vipCardRRInfo.PaymentTypeID = PaymentTypeID;
                vipCardRRInfo.RechargeAmount = RechargeAmount;
                vipCardRRInfo.RechargeTime = System.DateTime.Now;
                vipCardRRInfo.VipCardID = VipCardID;
                vipCardRRInfo.BalanceBeforeAmount = vipCardInfo.BalanceAmount;
                vipCardRRInfo.BalanceAfterAmount = vipCardInfo.BalanceAmount + RechargeAmount;
                vipCardRRInfo.RechargeUserID = this.CurrentUserInfo.UserID;
                vipCardRRInfo.UnitID = UnitID;
                vipCardRRInfo.CustomerID = this.CurrentUserInfo.CurrentUser.customer_id;
                #endregion
                //strError = "��ֵ�ɹ�.";
                //return true;
                return _currentDAO.SetVipCardRecjargeRpecord(vipCard, vipCardRRInfo, out strError);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

    }
}