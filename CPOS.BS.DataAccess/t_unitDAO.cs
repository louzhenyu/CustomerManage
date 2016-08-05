/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-06-08 20:59:54
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
    /// 表t_unit的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class t_unitDAO : Base.BaseCPOSDAO, ICRUDable<t_unitEntity>, IQueryable<t_unitEntity>
    {
        public t_unitEntity GetMainUnit(string clientID)
        {
            string sql =string.Format(@" SELECT top 1 t_unit.unit_id ,
        t_unit.type_id ,
        t_unit.unit_code ,
        t_unit.unit_name ,
        t_unit.unit_name_en ,
        t_unit.unit_name_short ,
        t_unit.unit_city_id ,
        t_unit.unit_address ,
        t_unit.unit_contact ,
        t_unit.unit_tel ,
        t_unit.unit_fax ,
        t_unit.unit_email ,
        t_unit.unit_postcode ,
        t_unit.unit_remark ,
        t_unit.Status ,
        t_unit.unit_flag ,
        t_unit.CUSTOMER_LEVEL ,
        t_unit.create_user_id ,
        t_unit.create_time ,
        t_unit.modify_user_id ,
        t_unit.modify_time ,
        t_unit.status_desc , 
        t_unit.bat_id ,
        t_unit.if_flag ,
        t_unit.customer_id ,
        t_unit.longitude ,
        t_unit.dimension ,
        t_unit.imageURL ,
        t_unit.ftpImagerURL ,
        t_unit.webserversURL ,
        t_unit.weiXinId ,
        t_unit.dimensionalCodeURL ,
        t_unit.BizHoursStarttime ,
        t_unit.BizHoursEndtime ,
        t_unit.StoreType
FROM    dbo.t_unit WITH ( NOLOCK )
        INNER JOIN dbo.T_Type WITH ( NOLOCK ) ON T_Type.type_id = dbo.t_unit.type_id
WHERE   dbo.t_unit.customer_id = '{0}' AND type_Level=1 ", clientID);

            t_unitEntity m = null ;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                }
            }
            //返回结果
            return m;
        }

    }
}
