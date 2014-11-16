/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:20
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
using System.Data.SqlClient;
using System.Text;
using System.Linq;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 拜访步骤中的对象 
    /// 表VisitingTaskStepObject的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VisitingTaskStepObjectDAO
    {
        #region GetStepObjectLevel
        public string GetStepObjectLevel(Guid stepID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top 1 * from [VisitingTaskStepObject] where VisitingTaskStepID='{0}' and IsDelete=0 and ClientID='{1}' and ClientDistributorID={2}", stepID.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            //读取数据
            VisitingTaskStepObjectEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            if (m != null)
            {
                return m.Level.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region GetStepBrandList
        public PagedQueryResult<VisitingTaskStepObjectViewEntity> GetStepBrandList(int brandLevel, int categoryLevel, Guid stepID, int pageIndex, int pageSize)
        {
            //四个dao 方法
            //获取品牌列表
            //获取子品牌列表
            //获取品牌、品类列表
            //获取子品牌、子品类列表
            StringBuilder sbSql = new StringBuilder();
            if (categoryLevel == 0)
            {
                //品牌
                sbSql.AppendFormat(@"
                                    select B.BrandID,B.BrandNo,B.BrandName,B.IsOwner,B.BrandLevel,B.ParentID,B.ClientID,B.ClientDistributorID,B.CreateTime,B.IsDelete,o.OptionText AS Firm,
                                    (select Brandname from Brand where BrandID=B.ParentID) as ParentBrandName,
                                    cast(A.BrandID as nvarchar(20)) as Target1ID,C.Target2ID,C.ObjectID,C.VisitingTaskStepID from (
                                    --查询客户的 brandid 集合
                                    select A.BrandID
                                    from Brand A 
                                    --inner join SKU B on A.BrandID=B.BrandID and B.IsDelete=0 and B.ClientID='{2}' and isnull(B.ClientDistributorID,0)={3}
                                    where A.BrandLevel={1} and A.IsDelete=0 and A.ClientID='{2}' and isnull(A.ClientDistributorID,0)={3}
                                    group by A.BrandID
                                    ) A
                                    inner join Brand B on A.BrandID=B.BrandID
                                    LEFT JOIN dbo.Options o on o.OptionValue=B.Firm AND o.OptionName='BrandCompany' AND o.ClientID='{2}' AND o.IsDelete=0
                                    left join VisitingTaskStepObject C 
                                    on A.BrandID=C.Target1ID and C.Target2ID is null 
                                    and C.IsDelete=0 
                                    and C.VisitingTaskStepID='{0}' 
                                    and C.ClientID='{2}' and isnull(C.ClientDistributorID,0)={3}
                                    ", stepID, brandLevel, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            }
            else
            {
                /*
                 select A.*,B.ObjectID,B.VisitingTaskStepID,B.Target2ID,
cast(A.BrandID as nvarchar(20))+'|'+cast(A.CategoryID as nvarchar(20)) as Target1ID from
(
select 
a.BrandName,a.BrandID,a.Remark,a.Firm,a.IsDelete,b.CategoryID,b.CategoryName,
(select Brandname from Brand where BrandID=A.ParentID) as ParentBrandName
from Brand a,Category b 
where a.BrandLevel={0} and b.CategoryLevel={1} and A.IsDelete=0 and B.IsDelete=0 
and A.ClientID='{3}' and A.ClientDistributorID={4} and B.ClientID='{3}' and B.ClientDistributorID={4} 
) as A 
left join VisitingTaskStepObject B 
on A.BrandID=B.Target1ID and A.CategoryID=B.Target2ID and B.IsDelete=0 and B.VisitingTaskStepID='{2}' 
and B.ClientID='{3}' and B.ClientDistributorID={4}
                 */

                //品牌+品类
                sbSql.AppendFormat(@"
                                select o.OptionText AS Firm,C.BrandName,C.BrandID,C.Remark,C.IsDelete,D.CategoryID,D.CategoryName,
                                (select Brandname from Brand where BrandID=C.ParentID) as ParentBrandName
                                ,B.ObjectID,B.VisitingTaskStepID,B.Target2ID,
                                cast(A.BrandID as nvarchar(20))+'|'+cast(A.CategoryID as nvarchar(20)) as Target1ID from
                                (
	                                --查询关联客户产品的品牌-品类id集合列表
	                                select distinct A.BrandID,A.CategoryID 
	                                from SKU A
	                                inner join Brand B on A.BrandID=B.BrandID
	                                inner join Category C on A.CategoryID=C.CategoryID
	                                where A.IsDelete=0 and B.IsDelete=0 and C.IsDelete=0
	                                and B.BrandLevel={0} and C.CategoryLevel={1}
	                                and A.ClientID='{3}' and isnull(A.ClientDistributorID,0)={4} and B.ClientID='{3}' and isnull(B.ClientDistributorID,0)={4} and C.ClientID='{3}' and isnull(C.ClientDistributorID,0)={4}
                                ) as A 
                                left join VisitingTaskStepObject B 
                                on A.BrandID=B.Target1ID and A.CategoryID=B.Target2ID and B.IsDelete=0 and B.VisitingTaskStepID='{2}' 
                                and B.ClientID='{3}' and isnull(B.ClientDistributorID,0)={4}
                                inner join Brand C on A.BrandID=C.BrandID
                                inner join Category D on A.CategoryID=D.CategoryID
                                INNER JOIN dbo.Options o on o.OptionValue=C.Firm AND o.OptionName='BrandCompany' AND o.ClientID='{3}' AND o.IsDelete=0
                                ", brandLevel, categoryLevel, stepID, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            }

            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + sbSql.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            if (categoryLevel == 0)
            {
                model.PageSort = "case when ObjectID is null then 1 else 0 end asc,A.BrandName";
            }
            else
            {
                model.PageSort = "case when ObjectID is null then 1 else 0 end asc,A.CategoryName";
            }
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<VisitingTaskStepObjectViewEntity> pEntity = new PagedQueryResult<VisitingTaskStepObjectViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VisitingTaskStepObjectViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion
        #region GetStepCategoryList
        public PagedQueryResult<VisitingTaskStepObjectViewEntity> GetStepCategoryList(int brandLevel, int categoryLevel, Guid stepID, int pageIndex, int pageSize)
        {
            StringBuilder sbSql = new StringBuilder();
            if (brandLevel == 0)
            {
                //品类
                sbSql.AppendFormat(@"

select b.item_category_id as CategoryID,
b.item_category_code as CategoryNo,
b.item_category_name as CategoryName,
'' as CategoryNameEn,
0 as CategoryLevel,
0 as IsLeaf,
parent_id as ParentID,
'' as Remark,ClientID,ClientDistributorID,CreateBy,CreateTime,LastUpdateBy,LastUpdateTime,0 as IsDelete,
(select item_category_name as Categoryname from T_Item_Category where item_category_id=B.Parent_ID) as ParentCategoryName,
cast(A.item_category_id as nvarchar(20)) as Target1ID,C.Target2ID,C.ObjectID,C.VisitingTaskStepID from (
--查询客户的 brandid 集合
select A.item_category_id
from dbo.T_Item_Category A 
group by A.item_category_id
) A
inner join T_Item_Category B on A.item_category_id=B.item_category_id
left join VisitingTaskStepObject C 
on A.item_category_id=C.Target1ID and C.Target2ID is null 
and C.IsDelete=0 
and C.VisitingTaskStepID='{0}' 
and C.ClientID='{2}' and isnull(C.ClientDistributorID,0)=0

", stepID, categoryLevel, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            }
            else
            {
                /*
                 select A.*,B.ObjectID,B.VisitingTaskStepID,B.Target2ID,
cast(A.CategoryID as nvarchar(20))+'|'+cast(A.BrandID as nvarchar(20)) as Target1ID from
(
select 
a.BrandName,a.BrandID,a.IsDelete,b.CategoryID,b.CategoryName,b.Remark,
(select CategoryName from Category where CategoryID=B.ParentID) as ParentCategoryName
from Brand a,Category b 
where a.BrandLevel={0} and b.CategoryLevel={1} and A.IsDelete=0 and B.IsDelete=0
and A.ClientID='{3}' and A.ClientDistributorID={4} and B.ClientID='{3}' and B.ClientDistributorID={4}
) as A 
left join VisitingTaskStepObject B 
on A.CategoryID=B.Target1ID and A.BrandID=B.Target2ID and B.IsDelete=0 and B.VisitingTaskStepID='{2}' and B.ClientID='{3}' and B.ClientDistributorID={4}
                 */
                //品类+品牌
                sbSql.AppendFormat(@"

select C.BrandName,C.BrandID,C.IsDelete,D.CategoryID,D.CategoryName,D.Remark,
(select CategoryName from Category where CategoryID=D.ParentID) as ParentCategoryName
,B.ObjectID,B.VisitingTaskStepID,B.Target2ID,
cast(A.CategoryID as nvarchar(20))+'|'+cast(A.BrandID as nvarchar(20)) as Target1ID from
(
	--查询关联客户产品的品牌-品类id集合列表
	select distinct A.BrandID,A.CategoryID 
	from SKU A
	inner join Brand B on A.BrandID=B.BrandID
	inner join Category C on A.CategoryID=C.CategoryID
	where A.IsDelete=0 and B.IsDelete=0 and C.IsDelete=0
	and B.BrandLevel={0} and C.CategoryLevel={1}
	and A.ClientID='{3}' and isnull(A.ClientDistributorID,0)={4} and B.ClientID='{3}' and isnull(B.ClientDistributorID,0)={4} and C.ClientID='{3}' and isnull(C.ClientDistributorID,0)={4}
) as A 
left join VisitingTaskStepObject B 
on A.CategoryID=B.Target1ID and A.BrandID=B.Target2ID and B.IsDelete=0 and B.VisitingTaskStepID='{2}' 
and B.ClientID='{3}' and isnull(B.ClientDistributorID,0)={4}
inner join Brand C on A.BrandID=C.BrandID
inner join Category D on A.CategoryID=D.CategoryID
", brandLevel, categoryLevel, stepID, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            }

            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + sbSql.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            if (brandLevel == 0)
            {
                model.PageSort = " case when ObjectID is null then 1 else 0 end asc,A.CategoryName";
            }
            else
            {
                model.PageSort = " case when ObjectID is null then 1 else 0 end asc,A.CategoryName";
            }
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<VisitingTaskStepObjectViewEntity> pEntity = new PagedQueryResult<VisitingTaskStepObjectViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VisitingTaskStepObjectViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion
        #region GetStepPositionList
        /// <summary>
        /// 获取拜访步骤职位对象列表
        /// </summary>
        /// <param name="stepid"> VisitingTaskStepID </param>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VisitingTaskStepObjectViewEntity> GetStepPositionList(Guid stepid, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
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
            string sql = @"select role_id as ClientPositionID,role_code as PositionNo,role_name as PositionName,status,0 as IsDelete,B.ObjectID,B.VisitingTaskStepID,CAST(A.role_id as nvarchar(200)) as Target1ID,B.Target2ID
            from t_role A 
            left join VisitingTaskStepObject B on A.role_id=B.Target1ID and B.IsDelete=0 and B.VisitingTaskStepID='{0}' and B.ClientID='{1}' and B.ClientDistributorID={2}
            ";

            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + string.Format(sql, stepid, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID) + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<VisitingTaskStepObjectViewEntity> pEntity = new PagedQueryResult<VisitingTaskStepObjectViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VisitingTaskStepObjectViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region DeleteStepObjectIn
        /// <summary>
        /// 根据target1Ids对步骤对象进行删除
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="target1Ids"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public int DeleteStepObjectIn(string stepID, string target1Ids, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID='{0}' and Target1ID in({1}) and Target2ID is null and ClientID='{2}' and ClientDistributorID={3} and isdelete=0", stepID, target1Ids, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
            return result;
        }
        #endregion
        #region DeleteStepObjectIn2
        /// <summary>
        /// 根据target1ID|target2ID进行删除
        /// 批量删除品牌、品类信息(brand category 使用)
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="oldList"></param>
        /// <param name="target1Ids"></param>
        /// <param name="pTran"></param>
        public void DeleteStepObjectIn2(string stepID, List<VisitingTaskStepObjectViewEntity> oldList, string target1Ids, IDbTransaction pTran)
        {
            if (target1Ids.Trim().Length == 0)
            {
                throw new Exception("缺少必须参数");
            }
            List<VisitingTaskStepObjectViewEntity> delList = new List<VisitingTaskStepObjectViewEntity>();
            string[] target1List = target1Ids.Split(',');
            for (int i = 0; i < target1List.Length; i++)
            {
                var query = oldList.Where(m =>
                        m.ObjectID != null
                        && m.VisitingTaskStepID == Guid.Parse(stepID)
                        && m.Target1ID == target1List[i]
                        && !string.IsNullOrEmpty(m.Target2ID)
                        );
                if (query.ToArray().Length == 1)
                {
                    //添加
                    delList.Add(query.ToArray()[0]);
                }
            }
            if (delList.ToArray().Length > 0)
            {
                this.Delete(delList.ToArray());
            }

            //            int result = 0;
            //            StringBuilder sql = new StringBuilder();
            //            sql.AppendFormat(@"
            //update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID='{0}' and Target1ID in({1}) and Target2ID in({2}) and ClientID='{3}'", stepID, target1Ids, target2Ids, CurrentUserInfo.ClientID);
            //            if (pTran != null)
            //            {
            //                result = this.SQLHelper.ExecuteNonQuery(
            //                    (SqlTransaction)pTran,
            //                    CommandType.Text, sql.ToString());
            //            }
            //            else
            //            {
            //                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            //            }
            //return result;
        }
        #endregion
        #region DeleteStepObjectNotIn
        /// <summary>
        /// 根据target1Ids 排除数据,target2Ids is null 进行删除
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="oldList"></param>
        /// <param name="target1Ids"></param>
        /// <param name="pTran"></param>
        public void DeleteStepObjectNotIn(string stepID, List<VisitingTaskStepObjectViewEntity> oldList, string target1Ids, IDbTransaction pTran)
        {
            if (target1Ids.Trim().Length == 0)
            {
                throw new Exception("缺少必须参数");
            }
            List<VisitingTaskStepObjectViewEntity> delList = oldList.Where(m => m.ObjectID != null).ToList();
            string[] target1List = target1Ids.Split(',');
            for (int i = 0; i < target1List.Length; i++)
            {
                var query = oldList.Where(m =>
                    m.ObjectID != null
                    && m.VisitingTaskStepID == Guid.Parse(stepID)
                    && m.Target1ID == target1List[i]
                    && string.IsNullOrEmpty(m.Target2ID)
                    );

                if (query.ToArray().Length == 1)
                {
                    //排除
                    delList.Remove(query.ToArray()[0]);
                }
            }
            if (delList.ToArray().Length > 0)
            {
                this.Delete(delList.ToArray());
            }

            //            int result = 0;
            //            StringBuilder sql = new StringBuilder();
            //            sql.AppendFormat(@"
            //update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID='{0}' and Target1ID not in({1}) and Target2ID is null and ClientID='{2}'", stepID, target1Ids, CurrentUserInfo.ClientID);
            //            if (pTran != null)
            //            {
            //                result = this.SQLHelper.ExecuteNonQuery(
            //                    (SqlTransaction)pTran,
            //                    CommandType.Text, sql.ToString());
            //            }
            //            else
            //            {
            //                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            //            }
            //            return result;
        }
        #endregion
        #region DeleteStepObjectNotIn2
        /// <summary>
        /// 根据target1ID|target2ID 排除数据 进行删除
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="oldList"></param>
        /// <param name="target1Ids"></param>
        /// <param name="target2Ids"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public void DeleteStepObjectNotIn2(string stepID, List<VisitingTaskStepObjectViewEntity> oldList, string target1Ids, IDbTransaction pTran)
        {
            if (target1Ids.Trim().Length == 0)
            {
                throw new Exception("缺少必须参数");
            }

            List<VisitingTaskStepObjectViewEntity> delList = oldList.Where(m => m.ObjectID != null).ToList();
            string[] target1List = target1Ids.Split(',');
            for (int i = 0; i < target1List.Length; i++)
            {
                var query = oldList.Where(m =>
                    m.ObjectID != null
                    && m.VisitingTaskStepID == Guid.Parse(stepID)
                    && m.Target1ID == target1List[i]
                    && !string.IsNullOrEmpty(m.Target2ID)
                    );
                if (query.ToArray().Length == 1)
                {
                    //排除
                    delList.Remove(query.ToArray()[0]);
                }
            }
            if (delList.ToArray().Length > 0)
            {
                this.Delete(delList.ToArray());
            }

            //            int result = 0;
            //            StringBuilder sql = new StringBuilder();
            //            sql.AppendFormat(@"
            //update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID='{0}' and Target1ID not in({1}) and Target2ID not in({2}) and ClientID='{3}'", stepID, target1Ids, target2Ids, CurrentUserInfo.ClientID);
            //            if (pTran != null)
            //            {
            //                result = this.SQLHelper.ExecuteNonQuery(
            //                    (SqlTransaction)pTran,
            //                    CommandType.Text, sql.ToString());
            //            }
            //            else
            //            {
            //                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            //            }
            //            return result;
        }
        #endregion
        #region DeleteStepObjectAll
        public int DeleteStepObjectAll(string stepID, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID='{0}' and ClientID='{1}' and ClientDistributorID={2} and isdelete=0", stepID, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
            return result;
        }
        #endregion

        #region DeleteStepObjectLevelNotIn
        /// <summary>
        /// 通过品牌、品类等级，删除其它等级信息
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="level"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public int DeleteStepObjectLevelNotIn(string stepID, string level, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID='{0}' and level<>'{1}' and ClientID='{2}' and ClientDistributorID={3} and isdelete=0", stepID, level, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
            return result;
        }
        #endregion
    }
}