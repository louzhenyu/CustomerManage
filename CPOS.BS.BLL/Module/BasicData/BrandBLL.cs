/*
 * Author		:zhongbao.xiao
 * EMail		:zhongbao.xiao@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/6 10:02:54
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
using System.Data;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ���� Ʒ�� 
    /// </summary>
    public partial class BrandBLL
    {   
        #region GetList
        /// <summary>
        /// ��ȡƷ����Ϣ�б�
        /// </summary>
        /// <param name="entity">������Brandʵ��</param>
        /// <param name="pageIndex">��������ǰҳ</param>
        /// <param name="pageSize">��������ҳ����</param>
        /// <param name="rowCount">���������ݼ�������</param>
        /// <returns>����Brandʵ����Ϣ</returns>
        public BrandViewEntity[] GetList(BrandViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && entity.BrandLevel != null && Convert.ToInt32(entity.BrandLevel) > 0)
            {
                wheres.Add(new EqualsCondition() { FieldName = "b.BrandLevel", Value = entity.BrandLevel });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.BrandName))
            {
                wheres.Add(new LikeCondition() { FieldName = "b.BrandName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.BrandName });
            }

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            PagedQueryResult<BrandViewEntity> pEntity = new BrandDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);
                
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion        

        #region Delete
        /// <summary>
        /// ɾ��Ʒ����Ϣ
        /// ɾ������ֻҪ��Ʒ����������ݲ���ɾ��
        /// </summary>
        /// <param name="ids">������Ʒ��ID</param>
        /// <returns>�������ֵ</returns>
        public void Delete(int ids, out string res)
        {
            res = "";
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction(); ;
            using (tran.Connection)
            {
                try
                {
                    this._currentDAO.DeleteBrand(ids, tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region GetAllLevel
        /// <summary>
        /// ��ȡƷ�Ƶȼ�
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllLevel()
        {
            return new BrandDAO(CurrentUserInfo).GetAllLevel();
        }        
        #endregion

        #region GetBrandName
        /// <summary>
        /// ����Ʒ�Ƶȼ���ȡ�ϼ�Ʒ��
        /// </summary>
        /// <param name="pBrandLevel">Ʒ�Ƶȼ�</param>
        /// <returns></returns>
        public BrandEntity[] GetBrandName(string pBrandLevel,string pIso)
        {
            return new BrandDAO(CurrentUserInfo).GetBrandName(pBrandLevel,pIso);
        }
        #endregion

        #region ValidateNo
        /// <summary>
        /// �ڱ�Ų�Ϊ�յ�ǰ���� ��֤���ݿ����Ƿ������ͬ��Ʒ�Ʊ��
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet ValidateNo(BrandEntity entity)
        {
            return new BrandDAO(CurrentUserInfo).ValidateNo(entity);                 
        }
        #endregion

        #region ValidateNoUpdate
        /// <summary>
        /// �ڱ�Ų�Ϊ�յ�ǰ���� ��֤���ݿ����Ƿ������ͬ��Ʒ�Ʊ��
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet ValidateNoUpdate(BrandEntity entity)
        {
            return new BrandDAO(CurrentUserInfo).ValidateNoUpdate(entity);
        }
        #endregion

        #region ValBrandID
        /// <summary>
        /// ��֤ɾ������ֻҪ��Ʒ����������ݲ���ɾ��
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataSet ValBrandID(string ids)
        {
            return new BrandDAO(CurrentUserInfo).ValBrandID(ids);
        }
        #endregion
    }
}