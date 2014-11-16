/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
    /// 表EclubVipBookMark的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubVipBookMarkDAO : Base.BaseCPOSDAO, ICRUDable<EclubVipBookMarkEntity>, IQueryable<EclubVipBookMarkEntity>
    {
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <returns></returns>
        public int InsertEclubVipBookMarkInfo(EclubVipBookMarkEntity pEntity)
        {
            //参数校验
            if (pEntity == null)
            {
                throw new ArgumentNullException("pEntity");
            }
            //SQL Script Append
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("if not exists(");
            strSQL.Append("select top 1 1 from EclubVipBookMark ");
            strSQL.Append("where IsDelete=0 and ObjectID=@ObjectID ");
            strSQL.Append("and VipID = @VipID and CustomerId=@CustomerId ) ");
            strSQL.Append("begin ");
            strSQL.Append("insert into EclubVipBookMark(VipID, BookMarkType, ObjectID, [Description], CustomerId, CreateBy,LastUpdateBy) ");
            strSQL.Append("values(@VipID, @BookMarkType, @ObjectID, @Description, @CustomerId, @CreateBy,@LastUpdateBy) ");
            strSQL.Append("end ");

            //参数集合
            SqlParameter[] parameter = 
            {
                    new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@BookMarkType",SqlDbType.Int),
					new SqlParameter("@ObjectID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
                    new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar)
            };
            parameter[0].Value = pEntity.VipID;
            parameter[1].Value = pEntity.BookMarkType;
            parameter[2].Value = pEntity.ObjectID;
            parameter[3].Value = pEntity.Description;
            parameter[4].Value = pEntity.CustomerId;
            parameter[5].Value = pEntity.CreateBy;
            parameter[6].Value = pEntity.LastUpdateBy;

            //Return Execute Result
            return this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), parameter);
        }
    }
}
