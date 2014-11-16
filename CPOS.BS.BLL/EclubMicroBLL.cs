/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/23 17:41:01
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
    public partial class EclubMicroBLL
    {
        /// <summary>
        /// ����ʵ��������ѯʵ�弯
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <returns>����������ʵ�弯</returns>
        public EclubMicroEntity[] MicroIssueListGet(EclubMicroEntity pQueryEntity)
        {
            OrderBy[] pOrderBys = new OrderBy[] { 
            new OrderBy(){ FieldName="Sequence" ,Direction= OrderByDirections.Asc}
            };
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <returns>����������ʵ��</returns>
        public EclubMicroEntity MicroIssueDetailGet(EclubMicroEntity pQueryEntity, ref List<Guid?> microIDS)
        {
            //Instance Obj
            var Id = pQueryEntity.MicroID;

            //Get Micro IDs
            OrderBy[] pOrderBys = new OrderBy[] { 
            new OrderBy(){ FieldName="Sequence" ,Direction= OrderByDirections.Asc}
            };
            pQueryEntity.MicroID = null;
            microIDS = _currentDAO.QueryByEntity(pQueryEntity, pOrderBys).Select(r => r.MicroID).ToList();

            //Get Detail Info
            return _currentDAO.GetByID_V1(Id);
        }
        /// <summary>
        /// ����ʵ��������ѯ��ҳ����
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <returns>����������ʵ��</returns>
        public PagedQueryResult<EclubMicroEntity> MicroIssuePageGet(EclubMicroEntity pQueryEntity, int pageIndex, int pageSize, string sortField, int? sortOrder)
        {
            //Get Micro IDs
            OrderBy[] pOrderBys = null;
            if (!string.IsNullOrEmpty(sortField))
            {
                OrderByDirections dir = OrderByDirections.Asc;
                if (sortOrder != null && sortOrder == 0)
                {
                    dir = OrderByDirections.Desc;
                }
                pOrderBys = new OrderBy[] { new OrderBy() { FieldName = sortField, Direction = dir } };
            }
            //Get Detail Info
            PagedQueryResult<EclubMicroEntity> result = _currentDAO.PagedQueryByEntity(pQueryEntity, pOrderBys, pageSize, pageIndex);
            foreach (EclubMicroEntity entity in result.Entities)
            {
                entity.PublishDate = entity.PublishTime != null ? entity.PublishTime.Value.ToString("yyyy-MM-dd") : string.Empty;
            }
            return result;
        }

        /// <summary>
        /// ��¼΢���Ķ������������
        /// </summary>
        /// <param name="microID">΢����־ID</param>
        /// <param name="field">�ֶΣ�Goods��Shares��</param>
        /// <returns></returns>
        public int AddMicroStats(Guid? microID, string field)
        {
            return _currentDAO.AddMicroStats(microID, field);
        }

        /// <summary>
        /// ȡ��΢���Ķ������������
        /// </summary>
        public int GetMicroStats(Guid? microID, string field)
        {
            return _currentDAO.GetMicroStats(microID, field);
        } 
    }
}