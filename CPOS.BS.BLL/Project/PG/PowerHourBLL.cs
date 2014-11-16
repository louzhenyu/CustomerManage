/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/14 14:03:25
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
    public partial class PowerHourBLL
    {
        /// <summary>
        /// �������ȡ������Ч��PowerHour
        /// </summary>
        /// <param name="pFinanceYear">int Year������ĺ�һ����ݣ����磬2013-2014���꣬��д2014��</param>
        /// <param name="cityID">����id</param>
        /// <returns></returns>
        public DataSet GetPowerHourByCity(string customerID, int pFinanceYear, string cityID)
        {
            return _currentDAO.GetPowerHourByCity(customerID, pFinanceYear, cityID);
        }

        /// <summary>
        /// �������ȡ������Ч��PowerHour
        /// </summary>
        /// <param name="pFinanceYear">int Year�������ǰһ����ݣ����磬2013-2014���꣬��д2013��</param>
        /// <returns></returns>
        public DataSet GetAllAvailablePowerHour(string customerID, int pFinanceYear)
        {
            return _currentDAO.GetAllAvailablePowerHour(customerID, pFinanceYear);
        }

        /// <summary>
        /// ��ȡ�ض�����Ա����
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public int GetLocalEmployeeCount(string customerID, string cityId)
        {
            return _currentDAO.GetLocalEmployeeCount(customerID, cityId);
        }

        /// <summary>
        /// ��ȡPowerHour�õ���ȫ��Most Valuable  Comments.  
        /// 
        /// *��ʾ*
        /// Most Valuable  Comments.��PowerHourRemark���У�QuestionIndex�ֶε�ֵΪ12 ����13�ļ�¼
        /// </summary>
        /// <param name="TrainningID">��ʦUserID</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ÿҳ��ʾ����</param>
        /// <returns></returns>
        public DataSet GetTrainningComments(string customerID, string powerHourID, int index, int pageIndex, int pageSize)
        {
            return _currentDAO.GetTrainningComments(customerID, powerHourID, index, pageIndex, pageSize);
        }

        /// <summary>
        /// ��ȡ�����μ�PowerHour���˵ķ���״̬������/�ܾ���
        /// </summary>
        /// <param name="powerHourId">����ID</param>
        /// <returns></returns>
        public DataSet GetPowerHourInviteState(string customerID, string powerHourId)
        {
            return _currentDAO.GetPowerHourInviteState(customerID, powerHourId);
        }

        /// <summary>
        /// ��ȡ���н��������ѧԱ�ĳ�ϯ״̬.
        /// </summary>
        /// <param name="powerHourId">����ID</param>
        /// <returns></returns>
        public DataSet GetAcceptInviteState(string customerID, string powerHourId)
        {
            return _currentDAO.GetAcceptInviteState(customerID, powerHourId);
        }

        #region Power Hour ��ϸ

        /// <summary>
        /// ��֤Power Hour�Ƿ����
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public bool VerifyPowerHourIsEnd(string pPowerHourID, string pCustomerID)
        {
            return _currentDAO.VerifyPowerHourIsEnd(pPowerHourID, pCustomerID);
        }
        /// <summary>
        /// �Ƿ������
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public bool VerifyPowerHourIsInvite(string pPowerHourID, string pCustomerID)
        {
            return _currentDAO.VerifyPowerHourIsInvite(pPowerHourID, pCustomerID);
        }

        /// <summary>
        /// ��ʦ������������(PH����ǰ)
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourInfo(string pPowerHourID, string pCustomerID)
        {
            return _currentDAO.GetPowerHourInfo(pPowerHourID, pCustomerID);
        }

        /// <summary>
        /// ��֤ѧԱ�Ƿ�μ���ѵ״̬
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pUserID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet VerifyPowerHourInvite(string pCustomerID, string pUserID, string pPowerHourID)
        {
            return _currentDAO.VerifyPowerHourInvite(pCustomerID, pUserID, pPowerHourID);
        }

        /// <summary>
        ///  ��ѵ��ϸ���μ�������ȱϯ������
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourAttendStaffInfo(string pCustomerID, string pPowerHourID)
        {
            return _currentDAO.GetPowerHourAttendStaffInfo(pCustomerID, pPowerHourID);
        }

        /// <summary>
        ///  ��ѵ��ϸ����������ƽ���÷֣�
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourRemarkReviewScore(string pCustomerID, string pPowerHourID)
        {
            return _currentDAO.GetPowerHourRemarkReviewScore(pCustomerID, pPowerHourID);
        }

        /// <summary>
        /// ����ѧԱ������Ϣ
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourRemarkForMember(string pCustomerID, string pPowerHourID, string pUserID)
        {
            return _currentDAO.GetPowerHourRemarkForMember(pCustomerID, pPowerHourID, pUserID);
        }

        #endregion

        #region ����Powerhour״̬
        /// <summary>
        /// ����Powerhour״̬
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pFinanceYear"></param>
        /// <returns></returns>
        public DataSet GetCityPowerHourState(string pCustomerID, int pFinanceYear)
        {
            return _currentDAO.GetCityPowerHourState(pCustomerID, pFinanceYear);
        }
        #endregion

        #region �ض����е�Power Hour
        /// <summary>
        /// �ض����е�Power Hour
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pFinanceYear"></param>
        /// <param name="pCityID"></param>
        /// <returns></returns>
        public DataSet GetSpecificCityAvailablePowerHour(string pCustomerID, int pFinanceYear, string pCityID)
        {
            return _currentDAO.GetSpecificCityAvailablePowerHour(pCustomerID, pFinanceYear, pCityID);
        }
        #endregion

        #region ��ȡ�ֳ�ͼƬ��Ϣ
        /// <summary>
        /// ��ȡ�ֳ�ͼƬ��Ϣ
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pObjectID"></param>
        /// <param name="pImageID"></param>
        /// <returns></returns>
        public DataSet GetImageThumbsList(string pCustomerID, string pObjectID, string pImageID)
        {
            return _currentDAO.GetImageThumbsList(pCustomerID, pObjectID, pImageID);
        }
        #endregion
    }
}