/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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

using System.Text;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��ActivityMedia�����ݷ����� 
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ActivityMediaDAO
    {

        #region GetList
        public PagedQueryResult<ActivityMediaEntity> GetList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            StringBuilder sqlOrder = new StringBuilder();
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    sqlOrder.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sqlOrder.Remove(sqlOrder.Length - 1, 1);
            }

            //ͨ�÷�ҳ��ѯ
            UtilityEntity model = new UtilityEntity();
            //TODO:�ý�壬�������
            model.TableName = "(" + "select a.*,b.FileName,b.Path,'���Ի' as ActivityTitle,case a.MediaType when 1 then 'ͼƬ' when 2 then '��Ƶ' end as MediaTypeText from dbo.ActivityMedia a left join dbo.Attachment b on a.AttachmentID=b.AttachmentID WHERE a.IsDelete=0" + sqlWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //����ֵ
            PagedQueryResult<ActivityMediaEntity> pEntity = new PagedQueryResult<ActivityMediaEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<ActivityMediaEntity>(model.PageDataSet.Tables[0], new DirectPropertyNameMapping());
            }
            return pEntity;
        }
        #endregion

    }
}
