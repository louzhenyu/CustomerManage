/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
    /// ҵ���� �ն�/�����̷��鶨�� 
    /// </summary>
    public partial class POPGroupBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private POPGroupDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public POPGroupBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new POPGroupDAO(pUserInfo);
        }
        #endregion
        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(POPGroupEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(POPGroupEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity,pTran);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public POPGroupEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public POPGroupEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(POPGroupEntity pEntity , IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity,pTran);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(POPGroupEntity pEntity )
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(POPGroupEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(POPGroupEntity pEntity, IDbTransaction pTran)
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
        public void Delete(POPGroupEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities,pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(POPGroupEntity[] pEntities)
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
        public POPGroupEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
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
        public PagedQueryResult<POPGroupEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQuery(pWhereConditions,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public POPGroupEntity[] QueryByEntity(POPGroupEntity pQueryEntity, OrderBy[] pOrderBys)
        {
           return _currentDAO.QueryByEntity(pQueryEntity,pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<POPGroupEntity> PagedQueryByEntity(POPGroupEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQueryByEntity(pQueryEntity,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        #endregion
    }
}