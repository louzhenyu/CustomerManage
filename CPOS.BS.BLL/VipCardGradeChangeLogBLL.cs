/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:41:32
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
    public partial class VipCardGradeChangeLogBLL
    {
        #region �б��ȡ
        /// <summary>
        /// �б��ȡ
        /// </summary>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public List<VipCardGradeChangeLogEntity> GetList(VipCardGradeChangeLogEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<VipCardGradeChangeLogEntity> list = new List<VipCardGradeChangeLogEntity>();

            DataSet ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipCardGradeChangeLogEntity>(ds.Tables[0]);
            }

            return list;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetListCount(VipCardGradeChangeLogEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region ������
        /// <summary>
        /// ������������¼
        /// </summary>
        /// <param name="VipCardID">��Ա����ʶ</param>
        /// <param name="ChangeBeforeGradeID">�䶯ǰ�ȼ�</param>
        /// <param name="NowGradeID">���տ��ȼ�</param>
        /// <param name="ChangeReason">�䶯ԭ��</param>
        /// <param name="UnitID">�����ŵ�</param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetVipCardGradeChange(string VipCardID
                                        , int ChangeBeforeGradeID
                                        , int NowGradeID
                                        , string ChangeReason
                                        , string UnitID
                                        , out string strError)
        {
            try
            {
                #region
                if (VipCardID == null || VipCardID.Trim().Equals(""))
                {
                    strError = "��Ա���ű�ʶ����Ϊ��.";
                    return false;
                }
                if (ChangeBeforeGradeID == 0 || ChangeBeforeGradeID.ToString().Trim().Equals(""))
                {
                    strError = "�䶯ǰ�ȼ���ʶ����Ϊ��.";
                    return false;
                }
                if (NowGradeID == 0 || NowGradeID.ToString().Trim().Equals(""))
                {
                    strError = "���տ��ȼ���ʶ����Ϊ��.";
                    return false;
                }
                if (UnitID == null || UnitID.Trim().Equals(""))
                {
                    strError = "�����ŵ��ʶ����Ϊ��.";
                    return false;
                }
                #endregion
                #region 1.���û�Ա������Ϣ�Ŀ��ȼ�
                VipCardEntity vipCardInfo = new VipCardEntity();
                vipCardInfo.VipCardID = VipCardID;
                vipCardInfo.VipCardGradeID = NowGradeID;
                vipCardInfo.LastUpdateBy = this.CurrentUserInfo.UserID;
                vipCardInfo.LastUpdateTime = System.DateTime.Now;
                #endregion
                #region 2.���ÿ��ȼ������¼
                VipCardGradeChangeLogEntity vipCardGCInfo = new VipCardGradeChangeLogEntity();
                vipCardGCInfo.ChangeLogID = JIT.CPOS.BS.BLL.BaseService.NewGuidPub();
                vipCardGCInfo.VipCardID = VipCardID;
                vipCardGCInfo.ChangeBeforeGradeID = ChangeBeforeGradeID;
                vipCardGCInfo.NowGradeID = NowGradeID;
                vipCardGCInfo.ChangeReason = ChangeReason;
                vipCardGCInfo.ChangeTime = System.DateTime.Now;
                vipCardGCInfo.UnitID = UnitID;
                vipCardGCInfo.OperationUserID = this.CurrentUserInfo.UserID;
                vipCardGCInfo.OperationType = 2;
                vipCardGCInfo.CreateBy = this.CurrentUserInfo.UserID;
                vipCardGCInfo.LastUpdateBy = this.CurrentUserInfo.UserID;
                #endregion
                return _currentDAO.SetVipCardStatusChange(vipCardInfo, vipCardGCInfo,out strError);
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

    }
}