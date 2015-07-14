/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/2 13:33:22
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

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表Tags的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TagsDAO : Base.BaseCPOSDAO, ICRUDable<TagsEntity>, IQueryable<TagsEntity>
    {
        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebTagsCount(TagsEntity entity)
        {
            string sql = GetWebTagsSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebTags(TagsEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebTagsSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebTagsSql(TagsEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " ,c.TypeName TypeName ";
            sql += " ,d.StatusName StatusName ";
            sql += " ,(select count(*) from VipTagsMapping where a.TagsId=VipTagsMapping.TagsId and VipTagsMapping.IsDelete='0') VipCount ";
            sql += " into #tmp ";
            sql += " from [Tags] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " left join [TagsType] c on a.TypeId=c.TypeId ";
            sql += " left join [TagsStatus] d on a.StatusId=d.StatusId ";
            sql += " where a.IsDelete='0' and a.customerId = '"+this.CurrentUserInfo.CurrentUser.customer_id+"' ";
            if (entity.TagsName != null && entity.TagsName.Trim().Length > 0)
            {
                sql += " and a.TagsName like '%" + entity.TagsName.Trim() + "%' ";
            }
            if (entity.TagsDesc != null && entity.TagsDesc.Trim().Length > 0)
            {
                sql += " and a.TagsDesc like '%" + entity.TagsDesc.Trim() + "%' ";
            }
            if (entity.TypeId != null && entity.TypeId.Trim().Length > 0)
            {
                sql += " and a.TypeId = '" + entity.TypeId.Trim() + "' ";
            }
            if (entity.StatusId != null && entity.StatusId.Trim().Length > 0)
            {
                sql += " and a.StatusId = '" + entity.StatusId.Trim() + "' ";
            }
            sql += " order by a.TagsName desc ";
            return sql;
        }
        #endregion

        #region Jermyn20131127 处理初始化时自动复制固定标签
        public bool setCopyTag(string CustomerId)
        {
            //string sql = "exec ProcSetCustomerWeiXinMapping '" + UserID + "','" + UserName + "'," + Longitude + "," + Latitude + ",'" + EventID + "' ";

            SqlParameter[] Parm = new SqlParameter[5];
            Parm[0] = new SqlParameter("@customerId", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = CustomerId;
            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ProcCopyFixedTags", Parm);
            return true;
        }
        #endregion

        #region  查询出属于当前标签的人数 2014-11-12
        /// <summary>
        /// 查询出属于当前标签的人数
        /// </summary>
        public int GetTagsCount(string tagsIDs)
        {
            string sql = "select COUNT(distinct(v.VipName)) from Tags t "
                        + "left join VipTagsMapping m on t.TagsId=m.TagsId "
                        + "left join Vip v on m.VipId=v.VIPID "
                        + "where t.TagsId in (" + tagsIDs + ")";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        #endregion

        public DataSet GetVipTagsList(string TypeId, string VipID)
        {

            List<SqlParameter> ls = new List<SqlParameter>();
 
            ls.Add(new SqlParameter("@VipID", VipID));

            var sql = @"select b.TypeId,b.TypeName,a.* from	  Tags a 
        left join TagsType b  on a.typeid=b.typeid
        inner join  VipTagsMapping c 
        on c.TagsId=a.TagsId        
where  c.vipID=@VipID
and c.isdelete=0
and a.isdelete=0
             ";
            if (!string.IsNullOrEmpty(TypeId) && TypeId != "")
            {
                sql += " and a.TypeId=@TypeId ";
                ls.Add(new SqlParameter("@TypeId", TypeId));
            }

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), ls.ToArray());
        }



        public DataSet GetTagsList(string TypeId,  string CustomerId)
        {

            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@TypeId", TypeId));
            ls.Add(new SqlParameter("@CustomerId", CustomerId));

            var sql = @"select b.TypeId,b.TypeName,a.* from	  Tags a inner join TagsType b 
on a.typeid=b.typeid
where a.TypeId=@TypeId
and a.customerid=@CustomerId
and a.isDelete=0

             ";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), ls.ToArray());
       
        }

        public bool DeleteByIds(string propIds, TagsTypeEntity TagsTypeEn)
        {
            string sql = "update Tags set ";
            sql += " isdelete=1 ";
            sql += " ,LastUpdateBy='" + TagsTypeEn.LastUpdateBy + "' ";
            sql += " ,LastUpdateTime='" + TagsTypeEn.LastUpdateTime + "' ";
            sql += " where TypeId='" + TagsTypeEn.TypeId + "'";
            sql += " and TagsId not in (" + propIds + ") ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
    }
}
