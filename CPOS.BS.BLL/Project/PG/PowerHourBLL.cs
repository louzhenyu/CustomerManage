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
    /// 业务处理：  
    /// </summary>
    public partial class PowerHourBLL
    {
        /// <summary>
        /// 按财年获取所有有效的PowerHour
        /// </summary>
        /// <param name="pFinanceYear">int Year（财年的后一个年份，比如，2013-2014财年，就写2014）</param>
        /// <param name="cityID">城市id</param>
        /// <returns></returns>
        public DataSet GetPowerHourByCity(string customerID, int pFinanceYear, string cityID)
        {
            return _currentDAO.GetPowerHourByCity(customerID, pFinanceYear, cityID);
        }

        /// <summary>
        /// 按财年获取所有有效的PowerHour
        /// </summary>
        /// <param name="pFinanceYear">int Year（财年的前一个年份，比如，2013-2014财年，就写2013）</param>
        /// <returns></returns>
        public DataSet GetAllAvailablePowerHour(string customerID, int pFinanceYear)
        {
            return _currentDAO.GetAllAvailablePowerHour(customerID, pFinanceYear);
        }

        /// <summary>
        /// 获取特定城市员工数
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public int GetLocalEmployeeCount(string customerID, string cityId)
        {
            return _currentDAO.GetLocalEmployeeCount(customerID, cityId);
        }

        /// <summary>
        /// 获取PowerHour得到的全部Most Valuable  Comments.  
        /// 
        /// *提示*
        /// Most Valuable  Comments.是PowerHourRemark表中，QuestionIndex字段的值为12 或者13的记录
        /// </summary>
        /// <param name="TrainningID">讲师UserID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public DataSet GetTrainningComments(string customerID, string powerHourID, int index, int pageIndex, int pageSize)
        {
            return _currentDAO.GetTrainningComments(customerID, powerHourID, index, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取受邀参加PowerHour的人的反馈状态（接受/拒绝）
        /// </summary>
        /// <param name="powerHourId">讲座ID</param>
        /// <returns></returns>
        public DataSet GetPowerHourInviteState(string customerID, string powerHourId)
        {
            return _currentDAO.GetPowerHourInviteState(customerID, powerHourId);
        }

        /// <summary>
        /// 获取所有接受邀请的学员的出席状态.
        /// </summary>
        /// <param name="powerHourId">讲座ID</param>
        /// <returns></returns>
        public DataSet GetAcceptInviteState(string customerID, string powerHourId)
        {
            return _currentDAO.GetAcceptInviteState(customerID, powerHourId);
        }

        #region Power Hour 详细

        /// <summary>
        /// 验证Power Hour是否结束
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public bool VerifyPowerHourIsEnd(string pPowerHourID, string pCustomerID)
        {
            return _currentDAO.VerifyPowerHourIsEnd(pPowerHourID, pCustomerID);
        }
        /// <summary>
        /// 是否可邀请
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public bool VerifyPowerHourIsInvite(string pPowerHourID, string pCustomerID)
        {
            return _currentDAO.VerifyPowerHourIsInvite(pPowerHourID, pCustomerID);
        }

        /// <summary>
        /// 讲师界面所需数据(PH结束前)
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourInfo(string pPowerHourID, string pCustomerID)
        {
            return _currentDAO.GetPowerHourInfo(pPowerHourID, pCustomerID);
        }

        /// <summary>
        /// 验证学员是否参加培训状态
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
        ///  培训详细（参加人数，缺席人数）
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourAttendStaffInfo(string pCustomerID, string pPowerHourID)
        {
            return _currentDAO.GetPowerHourAttendStaffInfo(pCustomerID, pPowerHourID);
        }

        /// <summary>
        ///  培训详细（评论数，平均得分）
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourRemarkReviewScore(string pCustomerID, string pPowerHourID)
        {
            return _currentDAO.GetPowerHourRemarkReviewScore(pCustomerID, pPowerHourID);
        }

        /// <summary>
        /// 返回学员评论信息
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

        #region 城市Powerhour状态
        /// <summary>
        /// 城市Powerhour状态
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pFinanceYear"></param>
        /// <returns></returns>
        public DataSet GetCityPowerHourState(string pCustomerID, int pFinanceYear)
        {
            return _currentDAO.GetCityPowerHourState(pCustomerID, pFinanceYear);
        }
        #endregion

        #region 特定城市的Power Hour
        /// <summary>
        /// 特定城市的Power Hour
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

        #region 获取现场图片信息
        /// <summary>
        /// 获取现场图片信息
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