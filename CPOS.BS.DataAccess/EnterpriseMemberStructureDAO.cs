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
    /// 数据访问：  
    /// 表EnterpriseMemberStructure的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EnterpriseMemberStructureDAO
    {
        public PagedQueryResult<EnterpriseMemberStructureEntity> GetList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
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

            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            //TODO:活动媒体，关联活动表
            model.TableName = "(" + @"select a.*,b.MemberName as MemberTitle,c.StructureTitle as ParentTitle from dbo.EnterpriseMemberStructure a 
left join EnterpriseMember b
on a.EnterpriseMemberID=b.EnterpriseMemberID
left join EnterpriseMemberStructure c
on a.parentID=c.EnterpriseMemberStructureID
where a.IsDelete=0 and b.IsDelete=0 and c.IsDelete=0 " + sqlWhere + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<EnterpriseMemberStructureEntity> pEntity = new PagedQueryResult<EnterpriseMemberStructureEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<EnterpriseMemberStructureEntity>(model.PageDataSet.Tables[0], new DirectPropertyNameMapping());
            }
            return pEntity;
        }
    }
}
