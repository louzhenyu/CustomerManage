/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/18 14:05:11
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表MobileModule的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MobileModuleDAO : Base.BaseCPOSDAO, ICRUDable<MobileModuleEntity>, IQueryable<MobileModuleEntity>
    {
        /// <summary>
        /// 获取客户订单的表单列表
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        public DataTable GetFormsTable(string clientID, int type, int page, int pageSize, out int totalRow)
        {


            const string sql = @"
Select 
    ROW_NUMBER() Over( Order by m.CreateTime desc) RowNO,
	Cast(m.MobileModuleID as varchar(50)) as MobileModuleID, 
	m.ModuleName , 
	ISNULL( m.IsTemplate, 0 ) IsTemplate,  
	mm.ObjectID
into #tmp
from MobileModule m 
left join MobileModuleObjectMapping mm on CAST( mm.ObjectID as varchar(40)) =CAST( m.MobileModuleID as varchar(40)) and mm.IsDelete = 0
where m.IsDelete = 0 
and m.CustomerID = @CustomerID and m.ModuleType = @ModuleType 

select 
	MobileModuleID, 
	ModuleName, 
	IsTemplate, 
	Case IsTemplate when 1 then 1 else COUNT( distinct ObjectID) end UsedCount  
from #tmp 
where RowNO > @Start and RowNO <= @End
group by MobileModuleID, ModuleName, IsTemplate ;

select COUNT(0) from #tmp;

drop table #tmp;
";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerID",SqlDbType.NVarChar,50), 
                new SqlParameter("@ModuleType",SqlDbType.Int),
                new SqlParameter("@Start",SqlDbType.Int),
                new SqlParameter("@End",SqlDbType.Int)
            };
            parameters[0].Value = clientID;
            parameters[1].Value = type;
            parameters[2].Value = (page) * pageSize;
            parameters[3].Value = (page + 1) * pageSize;

            var ds = SQLHelper.ExecuteDataset(CommandType.Text, sql, parameters);
            if (ds.Tables.Count < 2 || ds.Tables[1].Rows.Count < 1)
            {
                totalRow = 0;
            }
            else
            {
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out totalRow);
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取客户属性列表
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        public DataTable GetClientBunessDefined(string clientID, int page, int pageSize, out int totalRow)
        {

            const string sql = @"
Select 
    ROW_NUMBER() Over( Order by ListOrder ) RowNO,
    TableName,
	ColumnName,
	ControlType,
	ColumnDesc,
	--CorrelationValue,
	AttributeTypeID,
	ListOrder 
into #tmp
from ClientBussinessDefined 
where IsDelete = 0
and ClientID = @CustomerID ;

select 
    TableName,
	ColumnName,
	ControlType,
	ColumnDesc,
	--CorrelationValue,
	AttributeTypeID as AttributeType ,
	ListOrder  
from #tmp 
where RowNO > @Start and RowNO <= @End;

select COUNT(1) from #tmp;

drop table #tmp;
";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerID",SqlDbType.VarChar, 50), 
                new SqlParameter("@Start",SqlDbType.Int),
                new SqlParameter("@End",SqlDbType.Int)
            };
            parameters[0].Value = clientID;
            parameters[1].Value = page * pageSize;
            parameters[2].Value = (page + 1) * pageSize;

            var ds = SQLHelper.ExecuteDataset(CommandType.Text, sql, parameters);
            if (ds.Tables.Count < 2 || ds.Tables[1].Rows.Count < 1)
            {
                totalRow = 0;
            }
            else
            {
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out totalRow);
            }
            return ds.Tables[0];
        }

        public DataTable GetLeventSignUpAttri(string clientID, int page, int pageSize, out int totalRow)
        {
            const string sql = @"
Select 
    ROW_NUMBER() Over( Order by ListOrder ) RowNO,
    TableName,
	ColumnName,
	ControlType,
	ColumnDesc,
	ListOrder  
into #tmp
from ClientBussinessDefined 
where IsDelete = 0
and ClientID = @CustomerID ;

select 
    TableName,
	ColumnName,
	ControlType,
	ColumnDesc,
	ListOrder  
from #tmp 
where RowNO > @Start and RowNO <= @End;

select COUNT(1) from #tmp;

drop table #tmp;
";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerID",SqlDbType.NVarChar, 50), 
                new SqlParameter("@Start",SqlDbType.Int),
                new SqlParameter("@End",SqlDbType.Int)
            };
            parameters[0].Value = clientID;
            parameters[1].Value = page * pageSize;
            parameters[2].Value = (page + 1) * pageSize;

            var ds = SQLHelper.ExecuteDataset(CommandType.Text, sql, parameters);
            if (ds.Tables.Count < 2 || ds.Tables[1].Rows.Count < 1)
            {
                totalRow = 0;
            }
            else
            {
                int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out totalRow);
            }
            return ds.Tables[0];
        }

        public void CreateWithMobilePageBlock(MobileModuleEntity entity)
        {
            using (var tran = SQLHelper.CreateTransaction())
            using (tran.Connection)
            {
                try
                {
                    const string sql = @"
INSERT INTO [MobilePageBlock]
    ([MobilePageBlockID]
    ,[TableName]
    ,[Title]
    ,[Type]
    ,[Sort]
    ,[ParentID]
    ,[Remark]
    ,[CustomerID]
    ,[CreateBy]
    ,[CreateTime]
    ,[LastUpdateBy]
    ,[LastUpdateTime]
    ,[IsDelete]
    ,[MobileModuleID])
VALUES
    (NewID()
    ,@TableName
    ,''
    ,NULL
    ,1
    ,NULL
    ,NULL
    ,@CustomerID
    ,@CreateBy
    ,GetDate()
    ,NULL
    ,GetDate()
    ,0
    ,@MobileModuleID);
";
                    Create(entity, tran);

                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@TableName", SqlDbType.NVarChar, 100), 
                        new SqlParameter("@CustomerID", SqlDbType.NVarChar, 50), 
                        new SqlParameter("@CreateBy", SqlDbType.Int), 
                        new SqlParameter("@MobileModuleID", SqlDbType.VarChar, 100), 
                    };

                    parameters[0].Value = entity.ModuleType == 1 ? "VIP" : "LEventSignUp";//1注册表单（VIP），2活动（SignUp）
                    parameters[1].Value = entity.CustomerID;
                    parameters[2].Value = CurrentUserInfo.UserID;
                    parameters[3].Value = entity.MobileModuleID.ToString();
                    SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

        }

        public DataSet DynamicVipFormLoad(string formID)
        {
            DataSet dataSet = new DataSet();

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@FormID", formID));
            parameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "DynamicVipFormLoad", parameter.ToArray());

            return dataSet;
        }

        public DataSet DynamicFormLoad(string formID, string tableName)
        {
            DataSet dataSet = new DataSet();

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@FormID", formID));
            parameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            parameter.Add(new SqlParameter("@TableName", tableName));
            dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "DynamicFormLoad", parameter.ToArray());

            return dataSet;
        }

        public string DynamicVipFormSave(string formID, DataTable dataTable)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@FormID", formID));
            parameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            parameter.Add(new SqlParameter("@UserID", CurrentUserInfo.UserID));
            parameter.Add(new SqlParameter("@FieldList", SqlDbType.Structured) { Value = dataTable });

            return this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "DynamicVipFormSave", parameter.ToArray()).ToString();
        }

        public string DynamicFormSave(string formID, string tableName, DataTable dataTable, SqlTransaction tran = null)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@FormID", formID));
            parameter.Add(new SqlParameter("@TableName", tableName));
            parameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            parameter.Add(new SqlParameter("@UserID", CurrentUserInfo.UserID));
            parameter.Add(new SqlParameter("@FieldList", SqlDbType.Structured) { Value = dataTable });

            return this.SQLHelper.ExecuteScalar(tran, CommandType.StoredProcedure, "DynamicFormSave", parameter.ToArray()).ToString();
        }
    }
}
