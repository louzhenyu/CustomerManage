/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:56
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
    public partial class EclubClientCitySequenceBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private EclubClientCitySequenceDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EclubClientCitySequenceBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new EclubClientCitySequenceDAO(pUserInfo);
        }
        #endregion
        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(EclubClientCitySequenceEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(EclubClientCitySequenceEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity,pTran);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public EclubClientCitySequenceEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public EclubClientCitySequenceEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>        
        public void Update(EclubClientCitySequenceEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true, pTran);
        }
        public void Update(EclubClientCitySequenceEntity pEntity, bool pIsUpdateNullField , IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity,pIsUpdateNullField,pTran);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(EclubClientCitySequenceEntity pEntity)
        {
            Update(pEntity , true);
        }
        public void Update(EclubClientCitySequenceEntity pEntity , bool pIsUpdateNullField)
        {
            _currentDAO.Update(pEntity,pIsUpdateNullField);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(EclubClientCitySequenceEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(EclubClientCitySequenceEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity,pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID,pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(EclubClientCitySequenceEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities,pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(EclubClientCitySequenceEntity[] pEntities)
        { 
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            _currentDAO.Delete(pIDs,pTran);
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public EclubClientCitySequenceEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
           return _currentDAO.Query(pWhereConditions,pOrderBys);
        }

        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<EclubClientCitySequenceEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQuery(pWhereConditions,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public EclubClientCitySequenceEntity[] QueryByEntity(EclubClientCitySequenceEntity pQueryEntity, OrderBy[] pOrderBys)
        {
           return _currentDAO.QueryByEntity(pQueryEntity,pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<EclubClientCitySequenceEntity> PagedQueryByEntity(EclubClientCitySequenceEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQueryByEntity(pQueryEntity,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        #endregion
    }
}