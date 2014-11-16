/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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


namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// 业务处理： 拜访参数选项值 
    /// </summary>
    public partial class VisitingParameterOptionsBLL
    {
        #region GetOptionByName
        /// <summary>
        /// 通过optionname 获取列表，其中 optionname  为必传参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public VisitingParameterOptionsEntity[] GetOptionByName(VisitingParameterOptionsEntity entity)
        {
            IWhereCondition[] whereCondition = new IWhereCondition[2];
            if (!string.IsNullOrEmpty(entity.ClientID) && entity.ClientID != "0")
            {
                EqualsCondition Conditioin = new EqualsCondition();
                Conditioin.FieldName = "ClientID";
                Conditioin.Value = entity.ClientID;
                whereCondition[0] = Conditioin;
            }
            EqualsCondition Conditioin1 = new EqualsCondition();
            Conditioin1.FieldName = "OptionName";
            Conditioin1.Value = entity.OptionName;
            whereCondition[1] = Conditioin1;

            OrderBy[] orderByCondition = new OrderBy[1];
            OrderBy orderBy = new OrderBy();
            orderBy.Direction = OrderByDirections.Asc;
            orderBy.FieldName = "Sequence";
            orderByCondition[0] = orderBy;
            return this.PagedQuery(whereCondition, orderByCondition, 1000, 1).Entities;
        }
        #endregion

        #region GetOptionNameList
        /// <summary>
        /// 获取optionnamelist
        /// </summary>
        /// <param name="entity">clientID  必传,optionName非必传</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingParameterOptionsViewEntity[] GetOptionNameList(VisitingParameterOptionsEntity entity, string ClientID, string ClientDistributorID, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && !string.IsNullOrEmpty(entity.OptionName))
            {
                wheres.Add(new LikeCondition() { FieldName = "OptionName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.OptionName });
            }
            //wheres.Add(new EqualsCondition() { FieldName = "ClientID", Value = entity.ClientID });
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "ClientID", Direction = OrderByDirections.Desc });
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            PagedQueryResult<VisitingParameterOptionsViewEntity> pEntity = this._currentDAO.GetOptionNameList(wheres.ToArray(), ClientID, ClientDistributorID, orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region DeleteOptionByName
        /// <summary>
        /// 通过optionname删除option
        /// </summary>
        /// <param name="entity"> clientID optionName 必传</param>
        public void DeleteOptionByName(VisitingParameterOptionsEntity entity,out string res)
        {
            #region 删除限制判断
            if (new DataOperateBLL(CurrentUserInfo).VisitingParameterOptionsDeleteCheck(entity.OptionName, out res) != 0)
            {
                return;
            }
            #endregion

            if (string.IsNullOrEmpty(entity.ClientID) || entity.ClientID == "0")
            {
                return;
            }
            VisitingParameterOptionsEntity[] pEntity = this.GetOptionByName(entity);
            this.Delete(pEntity);
        }
        #endregion

        #region Edit
        /// <summary>
        /// 选项编辑
        /// </summary>
        /// <param name="pEntityList"></param>
        /// <param name="entity">optionName,clientID必传</param>
        /// <param name="type">编辑类型 1新增 2修改</param>
        /// <returns>0成功 101name已经存在 102修改限制判断失败</returns>
        public int Edit(VisitingParameterOptionsEntity[] pEntityList, VisitingParameterOptionsEntity entity, int type, out string strRes)
        {
            #region 修改限制判断
            if (new DataOperateBLL(CurrentUserInfo).VisitingParameterOptionsUpdateCheck(entity.OptionName, out strRes) != 0)
            {
                return 102;
            }
            #endregion

            int res = 0;
            if (type == 1)
            {
                //新增
                if (this.GetOptionByName(entity).Length > 0)
                {
                    //name已经存在
                    res = 101;
                }
                else
                {
                    foreach (VisitingParameterOptionsEntity oEntity in pEntityList)
                    {
                        oEntity.OptionName = entity.OptionName;                      
                        oEntity.ClientID = CurrentUserInfo.ClientID;
                        oEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        this._currentDAO.Create(oEntity);
                    }
                    res = 0;
                }
            }
            else
            {
                //修改
                List<VisitingParameterOptionsEntity> oldList = this.GetOptionByName(entity).ToList();
                //添加、修改、删除
                foreach (VisitingParameterOptionsEntity oEntity in pEntityList)
                {
                    if (oEntity.VisitingParameterOptionsID == null 
                        || string.IsNullOrEmpty(oEntity.VisitingParameterOptionsID.Value.ToString()))
                    {
                        oEntity.OptionName = entity.OptionName;
                        oEntity.ClientID = CurrentUserInfo.ClientID;                    
                        oEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        this.Create(oEntity);
                    }
                    if (oldList.ToArray().Where(i => i.VisitingParameterOptionsID == oEntity.VisitingParameterOptionsID).ToArray().Length > 0)
                    {
                        oEntity.OptionName = entity.OptionName;
                        oEntity.ClientID = CurrentUserInfo.ClientID;                
                        oEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        this.Update(oEntity);

                        oldList.Remove(oldList.ToArray().Where(m => m.VisitingParameterOptionsID == oEntity.VisitingParameterOptionsID).ToArray()[0]);
                    }
                }
                if (oldList.ToArray().Length > 0)
                {
                    this.Delete(oldList.ToArray());
                }
                res = 0;
            }
            return res;
        }
        #endregion

        
    }
}