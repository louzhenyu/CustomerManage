/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-10-17 14:03:32
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
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表T_Type的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_TypeDAO : Base.BaseCPOSDAO, ICRUDable<T_TypeEntity>, IQueryable<T_TypeEntity>
    {


        public void UpdateShop(int LevelCount, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@type_level", LevelCount));

            string sql = @"update t_type set type_level=@type_level where customer_id=@CustomerId and type_code='门店'
                            update t_role set org_level=@type_level where customer_id=@CustomerId 
                                and type_id=( select type_id from t_type  where customer_id=@CustomerId and type_code='门店')
                                    ";

            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());    //计算总行数

        }

        public DataSet GetUnitStructList(string CustomerID, string loginUserID, int hasShop)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@customer_id", CustomerID));
            ls.Add(new SqlParameter("@loginUserID", loginUserID));
            string sql = @"----在这里要把用户的权限能看到的数据加上
                    DECLARE @AllUnit NVARCHAR(200)

                    CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
                     INSERT #UnitSET (UnitID)                  
                       SELECT DISTINCT R.UnitID                   
                       FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  (@customer_id,UR.unit_id,205)  R                  
                       WHERE user_id=@loginUserID          ---根据账户的角色去查角色对应的  unit_id
                    ";

            sql += @"
                select a.*
                ,b.TYPE_NAME
                ,b.type_level
               ,(case when (select COUNT(1) from t_type x where x.customer_id=@customer_id
                    and type_code!='OnlineShopping')<= b.type_level+1 then 0 else 1 end) canAddChild
                ,c.type_id next_type_id
                ,c.type_name next_type_name
                ,d.src_unit_id parent_id
                ,f.unit_name parentUnit_Name
                ,(select COUNT(1) from T_Unit_Relation y where y.src_unit_id=a.unit_id) childCount
                ,(select COUNT(1) from T_User_Role z where z.unit_id=a.unit_id) userRoleCount
                 from 
                t_unit  a
                inner join t_type b on a.type_id=b.type_id
                left join t_type c on ((b.type_level+1)=c.type_level and c.customer_id=@customer_id)
                left join T_Unit_Relation d on (a.unit_id=d.dst_unit_id and d.status=1)
                left join t_unit f on d.src_unit_id =f.unit_id
                inner join cpos_ap..t_customer e on a.customer_id=e.customer_id
                inner join #UnitSET  g on(a.unit_id = g.unitid) 
                where 1=1
                and  a.customer_id=@customer_id
                and a.unit_code!=e.customer_code -----去掉默认的门店
                and a.status =1
                and b.type_code !='OnlineShopping'  
";//或者unit_code!='ONLINE'


            if (hasShop!= 1)
            {
                sql += " and b.type_code !='门店'";
            }
            sql += @"
  order by b.type_level
";
            sql += @" drop table #UnitSET                ";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数

        }


        

        public DataSet GetUnitStructByID(string CustomerID, string unit_id)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@customer_id", CustomerID));
            ls.Add(new SqlParameter("@unit_id", unit_id));

            string sql = @"

                              select a.*
                ,b.TYPE_NAME
                ,b.type_level
                ,(case when (select COUNT(1) from t_type x where x.customer_id=@customer_id
                    and type_code!='OnlineShopping')<= b.type_level+1 then 0 else 1 end) canAddChild
                ,c.type_id next_type_id
                ,c.type_name next_type_name
                ,d.src_unit_id parent_id
                ,f.unit_name parentUnit_Name
                ,(select COUNT(1) from T_Unit_Relation y where y.src_unit_id=a.unit_id and y.status=1) childCount
                ,(select COUNT(1) from T_User_Role z where z.unit_id=a.unit_id) userRoleCount
                 from 
                t_unit  a
                inner join t_type b on a.type_id=b.type_id
                left join t_type c on ((b.type_level+1)=c.type_level and c.customer_id=@customer_id)
                left join T_Unit_Relation d on (a.unit_id=d.dst_unit_id and d.status=1)
                left join t_unit f on d.src_unit_id =f.unit_id
                inner join cpos_ap..t_customer e on a.customer_id=e.customer_id
                where 1=1
            and  a.customer_id=@customer_id
            --and b.type_code not in ('门店','OnlineShopping')
            and a.status =1
                 and a.unit_id=@unit_id
";

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数

        }


        public DataSet GetTypeTree(string CustomerID, string user_id)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@customer_id", CustomerID));
            ls.Add(new SqlParameter("@user_id", user_id));


            string sql = @"
               select * from T_Type 
                where  type_Level>=
                (select MIN(type_Level) from T_Type a inner join t_unit b on a.type_id=b.type_id
	                 inner join T_User_Role c on   c.unit_id=b.unit_id
	                 where c.user_id=@user_id and a.type_code!='OnlineShopping')
                 and type_code!='OnlineShopping'
                 and customer_id=@customer_id
                order by type_level 
            ";

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数

        }



    }
}
