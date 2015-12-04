/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 15:58:37
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class MHAdAreaBLL
    {
        #region ��ȡ��漯��

        /// <summary>
        /// ��ȡ��漯��
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetAdList(string homeId)
        {
            return this._currentDAO.GetAdList(homeId);
        }
        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetMHSearchArea(string homeId)
        {
            return this._currentDAO.GetMHSearchArea(homeId);
        }

        /// <summary>
        /// ��ȡ�ͻ��µ���������
        /// </summary>
        /// <returns></returns>
        public MHAdAreaEntity[] GetByCustomerID()
        {
            return this._currentDAO.GetByCustomerID();
        }
        public MHAdAreaEntity[] GetAdByHomeId(string strHomeId)
        {
            return this._currentDAO.GetAdByHomeId(strHomeId);
        }
        #endregion

        #region ��ȡ���������

        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetEventInfo(string homeId)
        {
            return this._currentDAO.GetEventInfo(homeId);
        }
        public DataSet GetEventInfoByGroupId(string homeId, string strGroupId)
        {
            return this._currentDAO.GetEventInfoByGroupId(homeId, strGroupId);
        }
        public DataSet GetEventListInfoByGroupId(string homeId, string strGroupId)
        {
            return this._currentDAO.GetEventListInfoByGroupId(homeId, strGroupId);
        }
        #endregion

        #region ��ȡ�������ID

        /// <summary>
        /// ��ȡ�������ID
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetCategoryGroupId(string homeId)
        {
            return this._currentDAO.GetCategoryGroupId(homeId);
        }

        #endregion

        #region ��ȡ��Ʒ����

        /// <summary>
        /// ��ȡ��Ʒ����
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataSet GetItemList(string groupId, string homeId)
        {
            return this._currentDAO.GetItemList(groupId,homeId);
        }
        public DataSet GetCategoryProductList(string groupId, string homeId, int intShowCount)
        {
            return this._currentDAO.GetCategoryProductList(groupId, homeId, intShowCount);
        }
        public DataSet GetGroupProductList(string groupId, string homeId, int intShowCount)
        {
            return this._currentDAO.GetGroupProductList(groupId, homeId, intShowCount);
        }
        
        public DataSet GetModelTypeIdByGroupId(string groupId,string strHomeId)
        {
            return this._currentDAO.GetModelTypeIdByGroupId(groupId, strHomeId);
        }
        public DataSet GetModelTypeIdByGroupId(string groupId)
        {
            return this._currentDAO.GetModelTypeIdByGroupId(groupId);
        }
        #endregion

        #region ������Ʒ���������

        /// <summary>
        /// ������Ʒ���������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateMHCategoryArea(MHCategoryAreaEntity entity)
        {
            this._currentDAO.UpdateMHCategoryArea(entity);
        }

        public void DeleteItemCategoryByGroupIdandHomeID(int GroupID, Guid HomeId)
        {
            this._currentDAO.DeleteItemCategoryByGroupIdandHomeID(GroupID, HomeId);
        }
        public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupID, string customerId,string strHomeId)
        {
            this._currentDAO.DeleteCategoryGroupByGroupIdandCustomerId(GroupID, customerId, strHomeId);
        }
        #endregion
    }
}