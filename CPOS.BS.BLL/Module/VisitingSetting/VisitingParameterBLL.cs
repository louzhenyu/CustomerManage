/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/27 11:57:36
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Reflection;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ParameterBLL 
    /// </summary>
    public partial class VisitingParameterBLL
    {
        #region GetList
        /// <summary>
        /// 获取采集参数列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingParameterViewEntity[] GetList(VisitingParameterViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && entity.ParameterType != null && Convert.ToInt32(entity.ParameterType) > 0)
            {
                wheres.Add(new EqualsCondition() { FieldName = "ParameterType", Value = entity.ParameterType });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ParameterName))
            {
                wheres.Add(new LikeCondition() { FieldName = "ParameterName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.ParameterName });
            }
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            PagedQueryResult<VisitingParameterViewEntity> pEntity = new VisitingParameterDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion        

        #region Delete
        public void Delete(VisitingParameterEntity pEntity, out string res)
        {
            #region 删除限制判断
            if (new DataOperateBLL(CurrentUserInfo).VisitingParameterDeleteCheck(pEntity.VisitingParameterID.Value, out res) != 0)
            {
                return;
            }
            #endregion

            this._currentDAO.Delete(pEntity);
        }
        #endregion

        #region Update
        public void Update(VisitingParameterEntity pEntity , out string res)
        {
            #region 修改限制判断
            if (new DataOperateBLL(CurrentUserInfo).VisitingParameterUpdateCheck(pEntity.VisitingParameterID.Value, out res) != 0)
            {
                return;
            }
            #endregion

            _currentDAO.Update(pEntity);
        }
        #endregion
    }

    public class DataOperateBLL
    {
        public DataOperateBLL(LoggingSessionInfo currentUserInfo)
        {
        }

        public decimal VisitingParameterDeleteCheck(Guid value, out string res)
        {
            res = "";
            return 0;
        }

        public decimal VisitingParameterUpdateCheck(Guid value, out string res)
        {
            res = "";
            return 0;
        }

        public decimal VisitingParameterOptionsDeleteCheck(string optionName, out string res)
        {
            res = "";
            return 0;
        }

        public decimal VisitingParameterOptionsUpdateCheck(string optionName, out string strRes)
        {
            strRes = "";
            return 0;
        }
    }
}