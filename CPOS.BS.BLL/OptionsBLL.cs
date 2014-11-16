/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:17
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
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ���� ����ѡ��ö�� 
    /// </summary>
    public partial class OptionsBLL
    {
        #region GetOptionByName
        /// <summary>
        /// ͨ��optionname ��ȡ�б����� optionname  Ϊ�ش�����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OptionsEntity[] GetOptionByName(OptionsEntity entity)
        {
            IWhereCondition[] whereCondition = new IWhereCondition[4];
            if (!string.IsNullOrEmpty(entity.ClientID) && entity.ClientID != "0")
            {
                EqualsCondition Conditioin = new EqualsCondition();
                Conditioin.FieldName = "ClientID";
                Conditioin.Value = entity.ClientID;
                whereCondition[0] = Conditioin;
            }
            if (entity.OptionName != null && entity.OptionName != "")
            {
                EqualsCondition Conditioin = new EqualsCondition();
                Conditioin.FieldName = "OptionName";
                Conditioin.Value = entity.OptionName;
                whereCondition[1] = Conditioin;
            }
            if (entity.DefinedID > 0)
            {
                EqualsCondition Conditioin = new EqualsCondition();
                Conditioin.FieldName = "DefinedID";
                Conditioin.Value = entity.DefinedID;
                whereCondition[2] = Conditioin;
            }
            MoreThanCondition Conditioin1 = new MoreThanCondition();
            Conditioin1.FieldName = "OptionValue";
            Conditioin1.Value = 0;
            Conditioin1.IncludeEquals = false;
            whereCondition[3] = Conditioin1;

            OrderBy[] orderByCondition = new OrderBy[2];
            OrderBy orderBy = new OrderBy();
            orderBy.Direction = OrderByDirections.Asc;
            orderBy.FieldName = "OptionValue";
            orderByCondition[0] = orderBy;
            OrderBy orderBy2 = new OrderBy();
            orderBy2.Direction = OrderByDirections.Asc;
            orderBy2.FieldName = "CreateTime";
            orderByCondition[1] = orderBy2;
            return this.PagedQuery(whereCondition, orderByCondition, 1000, 1).Entities;
        }
        /// <summary>
        /// ͨ��optionname ��ȡDataSet������ optionname  Ϊ�ش�����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet GetOptionByName(string optionName, bool? isSort)
        {
            if (isSort != null && isSort == true)
            {
                return _currentDAO.GetOptionByName_V2(optionName);
            }
            return _currentDAO.GetOptionByName(optionName);
        }
        /// <summary>
        /// ͨ��optionname ��ȡDataSet������ optionname  Ϊ�ش�����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet GetOptionByName_V1(string optionName)
        {
            return _currentDAO.GetOptionByName_V1(optionName);
        }
        #endregion

        #region GetOptionNameList
        /// <summary>
        /// ��ȡoptionnamelist
        /// </summary>
        /// <param name="entity">clientID  �ش�,optionName�Ǳش�</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public OptionsViewEntity[] GetOptionNameList(OptionsEntity entity, string ClientID, string ClientDistributorID, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && !string.IsNullOrEmpty(entity.OptionName))
            {
                wheres.Add(new LikeCondition() { FieldName = "OptionName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.OptionName });
            }
            wheres.Add(new EqualsCondition() { FieldName = "ClientID", Value = entity.ClientID });
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "ClientID", Direction = OrderByDirections.Desc });
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            PagedQueryResult<OptionsViewEntity> pEntity = new OptionsDAO(this.CurrentUserInfo).GetOptionNameList(wheres.ToArray(), ClientID, ClientDistributorID, orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region DeleteOptionByName
        /// <summary>
        /// ͨ��optionnameɾ��option
        /// </summary>
        /// <param name="entity"> clientID optionName �ش�</param>
        public void DeleteOptionByName(OptionsEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.ClientID) && entity.ClientID != "0")
            {
                return;
            }
            OptionsEntity[] pEntity = this.GetOptionByName(entity);
            this.Delete(pEntity);
        }
        #endregion

        #region OptionsDefinedEdit
        public int OptionsDefinedEdit(OptionsDefinedEntity optionsDefinedEntity, OptionsEntity[] optionEntity)
        {
            int result = 1;
            int type = 0;
            if (optionsDefinedEntity.DefinedID > 0)
            {
                new OptionsDefinedBLL(CurrentUserInfo).Update(optionsDefinedEntity);
                type = 2;
            }
            else
            {
                optionsDefinedEntity.ClientID = CurrentUserInfo.ClientID;
                if (new OptionsDefinedBLL(CurrentUserInfo).GetByOptionName(optionsDefinedEntity.OptionName, CurrentUserInfo.UserID) == null)
                {
                    new OptionsDefinedBLL(CurrentUserInfo).Create(optionsDefinedEntity);
                }
                else
                {
                    return 2;
                }
                type = 1;
            }
            if (optionsDefinedEntity.DefinedID > 0)
            {
                //���ݲ�����BLL parameterentity optionentity
                new OptionsBLL(CurrentUserInfo).Edit(optionEntity, optionsDefinedEntity, type);
            }
            return result;
        }
        #endregion

        #region Edit
        /// <summary>
        /// ѡ��༭
        /// </summary>
        /// <param name="pEntityList"></param>
        /// <param name="entity">optionName,clientID�ش�</param>
        /// <param name="type">�༭���� 1���� 2�޸�</param>
        /// <returns>1name�Ѿ����� 2�ɹ�</returns>
        public int Edit(OptionsEntity[] pEntityList, OptionsDefinedEntity definedEntity, int type)
        {
            int res = 0;
            if (type == 1)
            {
                foreach (OptionsEntity oEntity in pEntityList)
                {
                    oEntity.OptionName = definedEntity.OptionName;
                    oEntity.ClientID = definedEntity.ClientID;
                    oEntity.DefinedID = definedEntity.DefinedID;
                    new OptionsBLL(this.CurrentUserInfo).Create(oEntity);
                }
            }
            else
            {
                //�޸�
                OptionsEntity entity = new OptionsEntity();
                entity.OptionName = definedEntity.OptionName;
                entity.ClientID = definedEntity.ClientID;
                entity.DefinedID = definedEntity.DefinedID;
                List<OptionsEntity> oldList = this.GetOptionByName(entity).ToList();
                //��ӡ��޸ġ�ɾ��
                foreach (OptionsEntity oEntity in pEntityList)
                {
                    oEntity.OptionName = definedEntity.OptionName;
                    oEntity.ClientID = definedEntity.ClientID;
                    oEntity.DefinedID = definedEntity.DefinedID;
                    if (oEntity.OptionsID == null || oEntity.OptionsID == 0)
                    {
                        this.Create(oEntity);
                    }
                    if (oldList.ToArray().Where(i => i.OptionsID == oEntity.OptionsID).ToArray().Length > 0)
                    {
                        this.Update(oEntity);
                        oldList.Remove(oldList.ToArray().Where(m => m.OptionsID == oEntity.OptionsID).ToArray()[0]);
                    }
                }
                if (oldList.ToArray().Length > 0)
                {
                    this.Delete(oldList.ToArray());
                }
            }
            return res;
        }
        #endregion

        public DynamicVipPropertyOptionListRD DynamicVipPropertyOptionList(DynamicVipPropertyOptionListRP dynamicVipPropertyOptionListRP)
        {
            DynamicVipPropertyOptionListRD dynamicVipPropertyOptionListRD = new DynamicVipPropertyOptionListRD();

            DataSet optionList = this._currentDAO.DynamicVipPropertyOptionList(dynamicVipPropertyOptionListRP.PropertyID);

            if (Utils.IsDataSetValid(optionList))
            {
                dynamicVipPropertyOptionListRD.OptionList = (from o in optionList.Tables[0].AsEnumerable()
                                                             select new Option() { OptionText = o["OptionText"].ToString() }
                                                            ).ToArray();
            }

            return dynamicVipPropertyOptionListRD;
        }

        #region ResponseData

        public class DisplayTypeListRD : IAPIResponseData
        {
            public DisplayTypeEntity[] DisplayTypeList { get; set; }
        }

        public class DisplayTypeEntity
        {
            public string DisplayType { get; set; }
            public string DisplayName { get; set; }
        }

        public class DynamicVipPropertyOptionListRD : IAPIResponseData
        {
            public Option[] OptionList { get; set; }
        }

        public class Option
        {
            public string OptionText { get; set; }
        }

        public class OptionListRD : IAPIResponseData
        {
            public OptionsEntity[] OptionList { get; set; }
        }

        #endregion

        #region RequestParameter
        public class DynamicVipPropertyOptionListRP : IAPIRequestParameter
        {
            public string PropertyID { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(PropertyID))
                    throw new APIException(201, "����ID����Ϊ�գ�");
            }
        }

        #endregion
    }
}