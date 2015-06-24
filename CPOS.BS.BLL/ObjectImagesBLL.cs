/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/26 17:11:20
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
    public partial class ObjectImagesBLL
    {
        #region ���ݶ����ʶ��ȡͼƬ����
        /// <summary>
        /// ���ݶ����ʶ��ȡͼƬ����
        /// </summary>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public IList<ObjectImagesEntity> GetObjectImagesByObjectId(string ObjectId)
        {
            IList<ObjectImagesEntity> list = new List<ObjectImagesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetObjectImagesByObjectId(ObjectId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<ObjectImagesEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ��ȡͬ������ͼƬ

        /// <summary>
        /// ��ȡͬ������ͼƬ
        /// </summary>
        /// <param name="latestTime">���ͬ��ʱ��</param>
        /// <returns></returns>
        public DataSet GetSynWelfareObjectImageList(string latestTime)
        {
            return this._currentDAO.GetSynWelfareObjectImageList(latestTime);
        }

        #endregion

        #region �б��ȡ
        /// <summary>
        /// �б��ȡ
        /// </summary>
        public IList<ObjectImagesEntity> GetList(ObjectImagesEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<ObjectImagesEntity> eventsList = new List<ObjectImagesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ObjectImagesEntity>(ds.Tables[0]);
            }
            return eventsList;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetListCount(ObjectImagesEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region
        /// <summary>
        /// ���ݿͻ���ȡ�ŵ�ͼƬ����
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public IList<ObjectImagesEntity> GetObjectImagesByCustomerId(string CustomerId)
        {
            IList<ObjectImagesEntity> list = new List<ObjectImagesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetObjectImagesByCustomerId(CustomerId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<ObjectImagesEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ͼƬͬ��
        /// <summary>
        /// ͼƬͬ��
        /// </summary>
        public IList<ObjectImagesEntity> GetClientImageList(ObjectImagesEntity entity)
        {
            IList<ObjectImagesEntity> eventsList = new List<ObjectImagesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetClientImageList(entity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ObjectImagesEntity>(ds.Tables[0]);
            }
            return eventsList;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetClientImageListCount(ObjectImagesEntity entity)
        {
            return _currentDAO.GetClientImageListCount(entity);
        }
        #endregion


        #region  ��ȡ������ϸUrl��
        /// <summary>
        /// ��ȡ������ϸurl��ַ��
        /// </summary>
        /// <param name="customerId">�ͻ�ID��</param>
        /// <param name="objectID">����ID��</param>
        /// <returns>������ϸҳ�������Ϣ��</returns>
        public ObjectImagesEntity GetObjectImagesByCustomerId(string customerId, string objectID)
        {
            ObjectImagesEntity entity = new ObjectImagesEntity();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetObjectImagesByCustomerId(customerId, objectID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = DataTableToObject.ConvertToObject<ObjectImagesEntity>(ds.Tables[0].Rows[0]);
            }
            return entity;
        }

        #endregion

        #region ���ݶ���IDɾ��ͼƬ
        public int DeleteByObjectID(string objectID)
        {
            return _currentDAO.DeleteByObjectID(objectID);
        }
        #endregion
    }
}