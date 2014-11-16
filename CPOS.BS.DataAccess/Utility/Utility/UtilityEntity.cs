/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2012/12/5 18:27:23
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
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;
using System.Data;

namespace JIT.CPOS.BS.DataAccess.Utility
{
    /// <summary>
    /// 用户登录信息实体 
    /// </summary>
    public partial class UtilityEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UtilityEntity()
        {
        }
        #endregion

        #region 属性集
        #region 基本数据
        /// <summary>
        /// 用户基础表[基本字段]
        /// </summary>
        public int? UsersID { get; set; }
        /// <summary>
        /// 数据库表名[基本字段]
        /// </summary>
        public string TableName { get; set; }       
        /// <summary>
        /// 操作类型，1修改删除，2清空，3为查询,4为自定义Sql查询[基本字段]
        /// </summary>
        public int? OperationType { get; set; }
        #endregion

        #region 查询字段
        /// <summary>
        /// 当前页,第一页为0 [查询字段]
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 显示数量 [查询字段]
        /// </summary>
        public int  PageSize { get; set; }
        /// <summary>
        /// 是否分页,0否1是 [查询字段]
        /// </summary>
        public int? PageIs { get; set; }
        /// <summary>
        /// 排序条件 ID desc,NID asc [查询字段]
        /// </summary>
        public string PageSort { get; set; }
        /// <summary>
        /// Where条件，传的时候不需要Where isdelete=0 ，直接传条件示例：and ID=1 and ID!=2 [基本字段]
        /// </summary>
        public string PageWhere { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int PageTotal { get; set; }
        public int PageCount { get; set; }
        public DataSet PageDataSet { get; set; }
        #endregion

        #region 修改字段
        /// <summary>
        /// 字段名称[修改字段]
        /// </summary>
        public string UptField { get; set; }
        /// <summary>
        /// 数值数据[添加，修改字段]
        /// </summary>
        public string UptValue { get; set; }

        /// <summary>
        /// 条件字段-- Where 条件字段 in 条件 [修改字段]
        /// </summary>
        public string UptWhereField { get; set; }
        /// <summary>
        ///  条件1,2,3-- Where 条件字段 in 条件 [修改字段]
        /// </summary>
        public string UptWhereValue { get; set; }
        #endregion

        #region 删除数据
        /// <summary>
        /// 清空数据的字段
        /// </summary>
        public string DeleteField { get; set; }
        /// <summary>
        /// 清空数据的条件
        /// </summary>
        public string DeleteValue { get; set; }
        #endregion

        #region 自定义sql查询
        /// <summary>
        /// 自定义查询
        /// </summary>
        public string CustomSql { get; set; }
        #endregion

        #region 结果字段
        /// <summary>
        /// 返回受影响的行数，或者是第一行第一列的值 （1*1位置）[结果字段]
        /// </summary>
        public int ResultNum { set; get; }
        /// <summary>
        /// 返回查询的结果[结果字段]
        /// </summary>
        public string ResultError { set; get; }
        public int? OpResultID { set; get; }
        #endregion
        #endregion
    }
}