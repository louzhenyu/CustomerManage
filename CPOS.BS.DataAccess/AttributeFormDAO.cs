/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/5 14:58:10
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
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表AttributeForm的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AttributeFormDAO : Base.BaseCPOSDAO, ICRUDable<AttributeFormEntity>, IQueryable<AttributeFormEntity>
    {
        public AttributeFormEntity[] GetAttributeFormList(string pName, int? pOperationTypeID, int? pStatus,
            int? pAttributeTypeID, int? pPageIndex, int? pPageSize,out int pPageCount)
        {
            pPageCount = 0;

            #region 判断是否为添加默认数据
            string pSql = @"
                            if not  exists (select * from  ClientBussinessDefined where IsDelete=0 and ClientID='{0}')
                            begin 
                            INSERT INTO ClientBussinessDefined
                                       ([ClientBussinessDefinedID],[TableName],[ColumnName],[ColumnType],[ControlType],[MinLength]
                                       ,[MaxLength],[ColumnDesc],[ColumnDescEn],[HierarchyID],[CorrelationValue],[IsRead]
                                       ,[IsMustDo],[IsUse],[IsRepeat],[EditOrder],[ListOrder],[ConditionOrder],[GridWidth]
                                       ,[SqlDesc],[Remark],[AttributeTypeID],[IsTemplate],[ClientID]
                                       ,[CreateBy],[CreateTime],[IsDelete])
                            select NewID(),[TableName],[ColumnName],[ColumnType],[ControlType],[MinLength]
                                       ,[MaxLength],[ColumnDesc],[ColumnDescEn],[HierarchyID],[CorrelationValue],[IsRead]
                                       ,[IsMustDo],[IsUse],[IsRepeat],[EditOrder],[ListOrder],[ConditionOrder],[GridWidth]
                                       ,[SqlDesc],[Remark],[AttributeTypeID],0,'{0}'
                                       ,{1},getdate(),[IsDelete] from ClientBussinessDefined 
                                       where IsDelete=0 and  IsTemplate=1
                            insert AttributeForm (ClientBussinessDefinedID,Name,Sequence,OperationTypeID,[Status],AttributeTypeID,CreateBy,Customerid)
                            select cbd.ClientBussinessDefinedID,ColumnDesc,ListOrder,1,1,cbd.AttributeTypeID,'{1}','{0}' 
                            from ClientBussinessDefined  as cbd
                            left join AttributeForm as abf
                            on abf.Name=cbd.ColumnDesc and abf.IsDelete=0
                            where cbd.IsDelete=0 and  cbd.IsTemplate=0 and cbd.ClientID='{0}' and  abf.AttributeFormID is null
                            end 
                            else
                            begin
                            if not exists(select  * from AttributeForm as a where Customerid='{0}' and a.IsDelete=0 )
                            begin
                            insert AttributeForm (ClientBussinessDefinedID,Name,Sequence,OperationTypeID,[Status],AttributeTypeID,CreateBy,Customerid)
                            select ClientBussinessDefinedID,ColumnDesc,ListOrder,1,1,AttributeTypeID,'{1}','{0}' 
                            from ClientBussinessDefined 
                            where IsDelete=0 and  IsTemplate=0 and ClientID='{0}'
                            end
                            end
                            ";
            if (pPageIndex == 0)
            {
                pSql = string.Format(pSql,this.CurrentUserInfo.ClientID,this.CurrentUserInfo.UserID);
                this.SQLHelper.ExecuteNonQuery(pSql);
            }
            #endregion

            StringBuilder sub = new StringBuilder();
            if (!string.IsNullOrEmpty(this.CurrentUserInfo.ClientID))
            {
                sub.AppendLine(string.Format(" and a.CustomerId='{0}'", this.CurrentUserInfo.ClientID));
            }
            if (!string.IsNullOrEmpty(pName))
            {
                sub.AppendLine(string.Format(" and a.Name like '%{0}%'", pName));
            }
            if (pOperationTypeID > 0)
            {
                sub.AppendLine(string.Format(" and a.OperationTypeID='{0}'", pOperationTypeID));
            }
            if (pStatus > 0)
            {
                sub.AppendLine(string.Format(" and a.Status='{0}'", pStatus));
            }
            if (pAttributeTypeID > 0)
            {
                sub.AppendLine(string.Format(" and a.AttributeTypeID='{0}'", pAttributeTypeID));
            }

            StringBuilder sql = new StringBuilder(string.Format(@"select row_number() over(order by Sequence,createtime desc) _row, a.*
                                                                  from AttributeForm a where a.IsDelete=0 {0}", sub));
            sql = new StringBuilder(string.Format("select * from ({0}) t where t._row>{1}*{2} and t._row<=({1}+1)*{2};", sql, pPageIndex, pPageSize));
            sql.AppendLine(string.Format(@"select count(*) from AttributeForm a
                                            where a.IsDelete=0 
                                            {0}", sub));
            DataSet ds;
            ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            var count = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    try
                    {
                        pPageCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                    }
                    catch (Exception)
                    {
                        pPageCount = 0;
                    }
                }             
                return DataLoader.LoadFrom<AttributeFormEntity>(ds.Tables[0]);
            }
            return null;
        }
    }
}
