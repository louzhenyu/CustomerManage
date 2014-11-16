/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:46:51
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
using System.Configuration;
using JIT.Utility.Web;
using JIT.Utility.Log;
using System.Data.SqlClient;
using JIT.CPOS.BS.Entity.WX;
using System.Transactions;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipBLL
    {
        #region ��Ա��ѯ
        /// <summary>
        /// ��Ա��ѯ Jermyn20130514+
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity SearchVipInfo(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.SearchVipInfoCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.SearchVipInfo(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);

                    foreach (VipEntity vipInfo1 in vipInfoList)
                    {
                        IList<TagsEntity> tagsList = new List<TagsEntity>();
                        tagsList = GetVipTags(vipInfo1.VIPID);
                        foreach (TagsEntity tagInfo in tagsList)
                        {
                            if (vipInfo1.VipTagsShort == null || vipInfo1.VipTagsShort.Equals(""))
                            {
                                vipInfo1.VipTagsShort = tagInfo.TagsName;
                                vipInfo1.VipTagsLong = tagInfo.TagsName;
                            }
                            else
                            {
                                if (vipInfo1.VipTagsShort.Length < 20)
                                {
                                    vipInfo1.VipTagsShort = vipInfo1.VipTagsShort + "," + tagInfo.TagsName;
                                }
                                vipInfo1.VipTagsLong = vipInfo1.VipTagsLong + "<br/>" + tagInfo.TagsName;
                            }
                        }
                        vipInfo1.VipTagsCount = tagsList.Count;
                    }
                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region ��ȡ��Ա����
        public VipEntity GetIntegralByVip(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.GetIntegralByVipCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.GetIntegralByVipList(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);


                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region ��Ա��ע
        public bool SetVipInfo(VipEntity vipInfo)
        {
            /*1.�����ע��־��
              2.�ж����»�Ա���Ǵ��ڵĻ�Ա
              3.�����»�Ա
              4.�����Ѵ��ڵĻ�Ա
            */

            return true;
        }
        #endregion

        #region ��ȡ��Ա��ϸ��Ϣ
        /// <summary>
        /// ����΢��OpenID��ȡ��Ա��ϸ��Ϣ
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public VipEntity GetVipDetail(string OpenID)
        {
            VipEntity vipInfo = new VipEntity();

            DataSet ds = _currentDAO.GetVipDetail(OpenID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }

            return vipInfo;
        }

        #region ��ȡ��ʼ������Ϣ

        /// <summary>
        /// ��ȡ��ʼ������Ϣ��ǰ̨�ã�
        /// </summary>
        /// <param name="OpenID">΢��ID</param>
        /// <returns></returns>
        public VipInfoRD GetVipInitInfo(string OpenID)
        {
            VipInfoRD vipInfo = new VipInfoRD();

            DataSet ds = _currentDAO.GetVipDetail(OpenID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipInfoRD>(ds.Tables[0].Rows[0]);
            }
            return vipInfo;
        }

        #endregion

        public VipEntity GetVipDetailByVipID(string vipID)
        {
            VipEntity vipInfo = new VipEntity();

            DataSet ds = _currentDAO.GetVipDetailByVipID(vipID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }

            return vipInfo;
        }
        #endregion


        /// <summary>
        /// ���ݺ���ģ����ѯ��Ա
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public List<VipEntity> GetVipByPhone(string phone)
        {
            List<VipEntity> vipInfo = new List<VipEntity>();

            DataSet ds = _currentDAO.GetVipByPhone(phone);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }

            return vipInfo;
        }


        #region ��ȡ��Ա��
        /// <summary>
        /// ��ȡvip����
        /// </summary>
        /// <returns></returns>
        public string GetVipCode()
        {
            return new AppSysService(this.CurrentUserInfo).GetNo("Vip");
        }
        #endregion

        #region ��ȡ��ע������
        /// <summary>
        /// ��ȡ���ڹ�ע�Ŀͻ�
        /// </summary>
        /// <param name="Timestamp"></param>
        /// <param name="NewTimestamp"></param>
        /// <returns></returns>
        public int GetShowCount(long Timestamp, out long NewTimestamp)
        {
            int icount = 0;
            DataSet ds = _currentDAO.GetShowCount(Timestamp);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString()) > 0)
            {
                NewTimestamp = Convert.ToInt64(ds.Tables[0].Rows[0]["NewTimestamp"].ToString());
                icount = Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString());
            }
            else
            {
                NewTimestamp = _currentDAO.GetNowTimestamp();
            }
            return icount;
        }
        /// <summary>
        /// ��ȡ������Դ��Ӧ�Ļ�Ա����VipSourceId Ϊ NULL ʱ �����绰�ͷ���Դ�Դ�
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetShowCount2(string userId, string clientId)
        {
            return _currentDAO.GetShowCount2(userId, clientId);
        }
        public JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity GetShowCountBySource(long Timestamp)
        {
            DataSet ds = _currentDAO.GetShowCountBySource(Timestamp);
            JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity info = new Entity.Interface.VIPAttentionEntity();
            IList<JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity> infoList = new List<JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                infoList = DataTableToObject.ConvertToList<JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity>(ds.Tables[0]);
            }
            info.VipAttentionInfoList = infoList;
            return info;
        }
        #endregion

        #region GetLjVipInfo
        /// <summary>
        /// ��ȡlj VIP��Ϣ
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public VipEntity GetLjVipInfo(VipEntity entity)
        {
            VipEntity vipInfo = new VipEntity();
            DataSet ds = _currentDAO.GetLjVipInfo(entity);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }
            return vipInfo;
        }
        #endregion

        #region GetVipItemList
        /// <summary>
        /// ��ȡVIP�һ���Ʒ�б���Ϣ
        /// </summary>
        public ItemInfo GetVipItemList(VipEntity entity)
        {
            try
            {
                ItemInfo obj = new ItemInfo();

                IList<ItemInfo> list = new List<ItemInfo>();
                DataSet ds = _currentDAO.GetVipItemList(entity);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<ItemInfo>(ds.Tables[0]);
                }

                obj.ICount = list.Count;
                obj.ItemInfoList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GetVipOrderList
        /// <summary>
        /// ��ȡVIP�����б���Ϣ
        /// </summary>
        public InoutInfo GetVipOrderList(VipEntity entity, int page, int pageSize)
        {
            try
            {
                InoutService inoutService = new InoutService(this.CurrentUserInfo);
                InoutInfo obj = new InoutInfo();

                IList<InoutInfo> list = new List<InoutInfo>();
                DataSet ds = _currentDAO.GetVipOrderList(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }

                foreach (var item in list)
                {
                    item.InoutDetailList = inoutService.GetInoutDetailInfoByOrderId(item.order_id);
                }

                obj.ICount = _currentDAO.GetVipOrdersCount(entity);
                obj.InoutInfoList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GetVipSkuPropList
        /// <summary>
        /// ��ȡVIP�һ���ƷSKU�б���Ϣ
        /// </summary>
        public SkuInfo GetVipSkuPropList(VipEntity entity, string itemId)
        {
            try
            {
                SkuInfo obj = new SkuInfo();

                IList<SkuInfo> list = new List<SkuInfo>();
                DataSet ds = _currentDAO.GetVipSkuPropList(entity, itemId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
                }

                obj.SkuInfoList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ��ȡ��Ա��ע����(�����Ͻ�)
        public int GetHasVipCount(string WeiXinId)
        {
            return _currentDAO.GetHasVipCount(WeiXinId);
        }
        #endregion

        #region ��ȡ�²ɼ���Ա����(�����Ͻ�)
        public int GetNewVipCount(string WeiXinId)
        {
            return _currentDAO.GetNewVipCount(WeiXinId);
        }
        #endregion

        #region getVipMonthAddup
        /// <summary>
        /// ��Ա�����ۼ�
        /// </summary>
        public LVipAddupEntity getVipMonthAddup(LVipAddupEntity entity, int page, int pageSize)
        {
            try
            {
                var result = new LVipAddupEntity();

                IList<LVipAddupEntity> list = new List<LVipAddupEntity>();
                DataSet ds = _currentDAO.getVipMonthAddup(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<LVipAddupEntity>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getVipMonthAddupCount(entity);
                result.EntityList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region getEventMonthEventAddup
        /// <summary>
        /// ��Ա�»����ͳ��
        /// </summary>
        public LEventAddupEntity getEventMonthEventAddup(LEventAddupEntity entity, int page, int pageSize)
        {
            try
            {
                var result = new LEventAddupEntity();

                IList<LEventAddupEntity> list = new List<LEventAddupEntity>();
                DataSet ds = _currentDAO.getEventMonthEventAddup(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<LEventAddupEntity>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getEventMonthEventAddupCount(entity);
                result.EntityList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ���ܲ���ȡvip��Ϣ Jermyn20130911
        public bool GetVipInfoFromApByOpenId(string OpenId, string VipId)
        {
            return _currentDAO.GetVipInfoFromApByOpenId(OpenId, VipId);
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<VipEntity> GetLotteryVipList()
        {
            IList<VipEntity> list = new List<VipEntity>();
            DataSet ds = _currentDAO.GetLotteryVipList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ��ȡ��Ա��ǩ����
        /// <summary>
        /// ��ȡ��Ա��ǩ����
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public IList<TagsEntity> GetVipTags(string VipId)
        {
            IList<TagsEntity> tagsList = new List<TagsEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipTags(VipId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagsList = DataTableToObject.ConvertToList<TagsEntity>(ds.Tables[0]);
            }
            return tagsList;
        }
        #endregion

        #region ��ȡ��Ա��ǩӳ�伯��
        /// <summary>
        /// ��ȡ��Ա��ǩӳ�伯��
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public IList<VipTagsMappingEntity> GetVipMappingTags(string VipId)
        {
            IList<VipTagsMappingEntity> tagsList = new List<VipTagsMappingEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipTagsMapping(VipId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagsList = DataTableToObject.ConvertToList<VipTagsMappingEntity>(ds.Tables[0]);
            }
            return tagsList;
        }
        #endregion

        #region Jermyn20131207 �ϲ��û���ʶ
        /// <summary>
        /// �����û��ϲ�����Ҫ����Ϊ֮ǰ���û�����ע���û���ע��֮����������ʺţ���Ҫ�ϲ�
        /// </summary>
        /// <param name="UserId">������Cookie�е�</param>
        /// <param name="VipId"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool SetMergerVipInfo(string UserId, string VipId, string OpenId)
        {
            return _currentDAO.SetMergerVipInfo(UserId, VipId, OpenId);
        }
        #endregion

        #region Jermyn20131219�ŵ꽱����ѯ
        /// <summary>
        /// �ŵ꽱����ѯ
        /// </summary>
        /// <param name="strError">������Ϣ</param>
        /// <returns>�ŵ�MembershipShop������SearchIntegral����Ա����UnitCount�����۽��UnitSalesAmount��ICount������</returns>
        public VipEntity GetUnitIntegral(VipEntity vipSearchInfo, out string strError)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {
                vipInfo.ICount = _currentDAO.GetUnitIntegralCount(vipSearchInfo);
                IList<VipEntity> list = new List<VipEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetUnitIntegral(vipSearchInfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }
                vipInfo.vipInfoList = list;
                strError = "ok";

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion

        #region Jermyn20131219����Ա����
        /// <summary>
        /// ����Ա����
        /// </summary>
        /// <param name="vipSearchInfo">��ѯ��������:�ŵ꣬�������ͣ���ѡ����Ӧ��SysIntegralSource ������ʼ���ڣ���������</param>
        /// <param name="strError"></param>
        /// <returns>����Ա���� UserName �ŵ� MembershipShop������SearchIntegral����Ա����VipCount���������۽��UnitSalesAmount</returns>
        public VipEntity GetPurchasingGuideIntegral(VipEntity vipSearchInfo, out string strError)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {
                vipInfo.ICount = _currentDAO.GetPurchasingGuideIntegralCount(vipSearchInfo);
                IList<VipEntity> list = new List<VipEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetPurchasingGuideIntegral(vipSearchInfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }
                vipInfo.vipInfoList = list;
                strError = "ok";

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion

        #region Jermyn20131219��Ա����
        /// <summary>
        /// ��Ա����
        /// </summary>
        /// <param name="vipSearchInfo">��ѯ��������:�ŵ�UnitId����Ա��VipName���������ͣ���ѡ����Ӧ��SysIntegralSource ������ʼ���ڣ���������</param>
        /// <param name="strError"></param>
        /// <returns>��Ա��VipName���Ἦ��MembershipShop������SearchIntegral����ԴVipSourceName����ǩVipTagsShort������ʱ��CreateTime���ȼ�VipLevelDesc���ƽ��Ա��VipCount��������PurchaseAmount</returns>
        public VipEntity GetVipIntegral(VipEntity vipSearchInfo, out string strError)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {
                vipInfo.ICount = _currentDAO.GetVipIntegralCount(vipSearchInfo);
                IList<VipEntity> list = new List<VipEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetVipIntegral(vipSearchInfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                    #region ��Ա��ǩ
                    foreach (VipEntity vipInfo1 in list)
                    {
                        IList<TagsEntity> tagsList = new List<TagsEntity>();
                        tagsList = GetVipTags(vipInfo1.VIPID);
                        foreach (TagsEntity tagInfo in tagsList)
                        {
                            if (vipInfo1.VipTagsShort == null || vipInfo1.VipTagsShort.Equals(""))
                            {
                                vipInfo1.VipTagsShort = tagInfo.TagsName;
                                vipInfo1.VipTagsLong = tagInfo.TagsName;
                            }
                            else
                            {
                                if (vipInfo1.VipTagsShort.Length < 20)
                                {
                                    vipInfo1.VipTagsShort = vipInfo1.VipTagsShort + "," + tagInfo.TagsName;
                                }
                                vipInfo1.VipTagsLong = vipInfo1.VipTagsLong + "<br/>" + tagInfo.TagsName;
                            }
                        }
                    }
                    #endregion
                }
                vipInfo.vipInfoList = list;
                strError = "ok";

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion

        #region DataTable paging
        /// <summary>
        /// DataTable��ҳ
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">ҳ����,ע�⣺��1��ʼ</param>
        /// <param name="PageSize">ÿҳ��С</param>
        /// <returns>�ֺ�ҳ��DataTable����</returns>
        private DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newdt; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
        /// <summary>
        /// ���ط�ҳ��ҳ��
        /// </summary>
        /// <param name="count">������</param>
        /// <param name="pageSize">ÿҳ��ʾ������</param>
        /// <returns>��� ��βΪ0���򷵻�1</returns>
        private int PageCount(int count, int pageSize)
        {
            int page = 0;
            if (count % pageSize == 0) { page = count / pageSize; }
            else { page = (count / pageSize) + 1; }
            if (page == 0) { page += 1; }
            return page;
        }
        #endregion

        public DataSet GetSaleFunnelData()
        {
            DataSet naviSalesData = new DataSet();
            naviSalesData = _currentDAO.GetSaleFunnelData();
            return naviSalesData;
        }

        #region ��ȡ��Ա�ĸ���״̬��������Jermyn201223
        /// <summary>
        /// ��ȡ��Ա�ĸ���״̬��������
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public VipEntity GetVipOrderByStatus(string OpenId)
        {
            VipEntity vipInfo = new VipEntity();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipOrderByStatus(OpenId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["Status"].ToString().Trim())
                    {
                        case "0":
                            vipInfo.Status0 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                        case "1":
                            vipInfo.Status1 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                        case "2":
                            vipInfo.Status1 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                        case "3":
                            vipInfo.Status1 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                    }
                }
            }
            else
            {
                vipInfo.Status0 = 0;
                vipInfo.Status1 = 1;
                vipInfo.Status2 = 2;
                vipInfo.Status3 = 3;
            }
            return vipInfo;
        }
        #endregion

        public string Register(string userID, string mobile, string name, string code, string customerID)
        {
            string result = "200";

            RegisterValidationCodeDAO registerValidationCodeDAO = new RegisterValidationCodeDAO(this.CurrentUserInfo);
            var validationCode = registerValidationCodeDAO.Query(new IWhereCondition[] { 
                new EqualsCondition(){ FieldName = "Mobile", Value = mobile}
            }, new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("validationCode: {0}", validationCode.ToJSON())
            });

            if (validationCode != null && validationCode.Code == code && validationCode.IsValidated.Value == 0)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("validationCode.Code: {0}", validationCode.Code)
                });

                validationCode.IsValidated = 1;
                validationCode.LastUpdateTime = DateTime.Now;
                registerValidationCodeDAO.Update(validationCode, false, null);

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("validationCode Updated: {0}", validationCode.Code)
                });

                VipBLL vipBLL = new VipBLL(this.CurrentUserInfo);
                var vip = vipBLL.GetByID(userID);
                if (vip != null)
                {
                    vip.Phone = mobile;
                    vip.Status = 2;
                    vip.VipRealName = name;

                    vipBLL.Update(vip,true);

                    //added by zhangwei ע����200����
                    #region ������� ��ֹӰ��ע�ᣬ����try catch �����Դ���

                    try
                    {
                        //�µĻ��ַ��� zhangwei2013-2-13
                        new VipIntegralBLL(CurrentUserInfo).ProcessPoint(2, customerID, vip.VIPID, null);
                    }
                    catch
                    {

                    }

                    #endregion

                    result = "200";
                }
                else
                    result = "102";
            }
            else
                result = "101";

            return result;
        }

        public VipEntity GetVipByPhone(string phone, string vipId, string status)
        {
            VipEntity tagsList = null;
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipByPhone(phone, vipId, status);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagsList = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }
            return tagsList;
        }

        #region ��ȡ��Ա����
        /// <summary>
        /// GetList
        /// </summary>
        public IList<VipEntity> GetList_Emba(string keyword, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<VipEntity> list = new List<VipEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList_Emba(keyword, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        public int GetListCount_Emba(string keyword)
        {
            return _currentDAO.GetListCount_Emba(keyword);
        }
        #endregion


        #region ������
        public void ModifyPWD(string pCustomerID, string phone, string sPwd, string newPWD)
        {
            var temp = this._currentDAO.GetByPhone(phone, pCustomerID);
            if (temp.Length > 0)
            {
                var entity = temp[0];
                if (entity.VipPasswrod == sPwd)
                {
                    entity.VipPasswrod = newPWD;
                    this._currentDAO.Update(entity);
                }
                else
                {
                    throw new Exception("������֤ʧ��");
                }
            }
            else
            {
                throw new Exception("δ�ҵ����û�");
            }
        }
        #endregion

        #region ��Ա��ѯLocation
        /// <summary>
        /// ��Ա��ѯ Jermyn20130514+
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity SearchVipInfoLocation(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                //int iCount = _currentDAO.SearchVipInfoCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.SearchVipInfoLocation(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }

                vipInfo.ICount = 1;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ͬ����������Ա��Ϣ

        /// <summary>
        /// ͬ����������Ա��Ϣ
        /// </summary>
        /// <param name="vipId">��ԱID</param>
        /// <returns></returns>
        public void SyncAladingUserInfo(string vipId, string customerId)
        {
            try
            {
                var vipEntity = this._currentDAO.GetByID(vipId);

                if (vipEntity == null)
                {
                    List<Guid> vipList = new List<Guid>();
                    vipList.Add(Guid.Parse(vipId));

                    var aldRequest = new ALDOrderRequest();
                    aldRequest.BusinessZoneID = 1;
                    aldRequest.Locale = 1;
                    aldRequest.Parameters = new { MemberIDs = vipList };

                    //���������û���Ϣ
                    var url = ConfigurationManager.AppSettings["ALDGatewayURL"];  //��ʽ
                    //var url = "http://121.199.42.125:5012/Gateway.ashx";        //����

                    if (string.IsNullOrEmpty(url))
                        throw new Exception("δ���ð�����ƽ̨�ӿ�URL:ALDGatewayURL");
                    var postContent = string.Format("Action=GetMemberList4PushMessage&ReqContent={0}", aldRequest.ToJSON());
                    var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                    var aldRsp = strAldRsp.DeserializeJSONTo<ALDResponse>();

                    if (aldRsp != null && aldRsp.Data != null && aldRsp.Data.Count!=0 && aldRsp.ResultCode == 200)
                    {
                        var entity = aldRsp.Data.FirstOrDefault();
                        //ͬ���û���Ϣ
                        this._currentDAO.SyncAladingUserInfo(customerId, entity.MemberID, entity.MemberNo, entity.MemberNo,
                            entity.MemberNo, entity.IOSDeviceToken, entity.BaiduChannelID, "",
                            entity.BaiduUserID, entity.Platform, entity.ChannelID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public class ALDOrderRequest
        {
            public int? Locale { get; set; }
            public Guid? UserID { get; set; }
            public int? BusinessZoneID { get; set; }
            public string Token { get; set; }
            public object Parameters { get; set; }
        }

        public class ALDResponse
        {
            public int? ResultCode { get; set; }
            public string Message { get; set; }
            public List<MemberEntity> Data { get; set; }
        }

        public class MemberEntity
        {
            public string MemberID { get; set; }    //��ԱID
            public string MemberNo { get; set; }    //��¼�ʺ�
            public string Platform { get; set; }    //�ͻ���ƽ̨��1=Android,2=IOS,3=����
            public string IOSDeviceToken { get; set; }  //IOS���豸��
            public string BaiduChannelID { get; set; }  //�ٶ���Ϣ���͵�����ID
            public string BaiduUserID { get; set; }     //�ٶ���Ϣ���͵��û�ID
            public string ChannelID { get; set; }       //����ID
        }

        #endregion

        #region �ͷ���¼

        /// <summary>
        /// �ͷ���¼
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        /// <param name="customerId">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet SetSignIn(string userName, string password, string customerId)
        {
            return this._currentDAO.SetSignIn(userName, password, customerId);
        }

        public DataSet GetRoleCodeByUserId(string userId, string customerId)
        {
            return this._currentDAO.GetRoleCodeByUserId(userId, customerId);
        }
        #region �ж��û��Ƿ����

        public bool JudgeUserExist(string userName, string customerId)
        {
            return this._currentDAO.JudgeUserExist(userName, customerId);
        }
        #endregion

        #region �ж������Ƿ���ȷ
        public bool JudgeUserPasswordExist(string userName, string customerId, string password)
        {
            return this._currentDAO.JudgeUserPasswordExist(userName, customerId, password);
        }
        #endregion

        public DataSet GetUserIdByUserNameAndPassword(string userName, string customerId, string password)
        {
            return this._currentDAO.GetUserIdByUserNameAndPassword(userName, customerId, password);
        }

        #region �жϸÿͷ���Ա�Ƿ��пͷ������������Ȩ��
        public bool JudgeUserRoleExist(string userName, string customerId, string password)
        {
            return this._currentDAO.JudgeUserRoleExist(userName, customerId, password);
        }
        #endregion

        #endregion

        public VipEntity GetByMobile(string pMobile, string pCustomerID)
        {
            var temp = this._currentDAO.GetByPhone(pMobile, pCustomerID);
            if (temp.Length > 0)
            {
                return temp[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// ��Ա��¼ Add by Alex Tian 2014-04-11
        /// </summary>
        /// <param name="VipNo"></param>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public VipEntity GetLoginInfo(string VipNo, string mobile, string password)
        {
            var temp = this._currentDAO.GetLoginInfo(VipNo, mobile, password);
            if (temp.Length > 0)
            {
                return temp[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݻ�Ա��š��ֻ��ţ��ϲ���Ա��Ϣ�� �����ɹ�����1��ʧ�ܷ���0
        /// </summary>
        /// <param name="pCustomerID">�ͻ�ID</param>
        /// <param name="pVipID">��ԱID</param>
        /// <param name="pPhone">�ֻ���</param>
        /// <returns></returns>
        public bool MergeVipInfo(string pCustomerID, string pVipID, string pPhone)
        {
            return this._currentDAO.MergeVipInfo(pCustomerID, pVipID, pPhone);
        }

        /// <summary>
        /// ��ȡһ���µĻ�Ա����
        /// </summary>
        /// <returns></returns>
        public string GetNewVipCode(string pCustomerID)
        {
            return this._currentDAO.GetVipCode(pCustomerID);
        }

        public int GetVipCoupon(string vipId)
        {
            return this._currentDAO.GetVipCoupon(vipId);
        }


        public string GetSettingValue(string customerId)
        {
            return this._currentDAO.GetSettingValue(customerId);
        }

        public DataSet GetVipColumnInfo(string eventCode, string customerId)
        {
            return this._currentDAO.GetVipColumnInfo(customerId, eventCode);
        }

        public DataSet GetVipInfo(string vipId)
        {
            return this._currentDAO.GetVipInfo(vipId);
        }

        public string GetVipLeave(string vipId)
        {
            return this._currentDAO.GetVipLeave(vipId);
        }

        public decimal GetIntegralBySkuId(string skuIdList)
        {
            return this._currentDAO.GetIntegralBySkuId(skuIdList);
        }

        public decimal GetTotalSaleAmountBySkuId(string skuIdList)
        {
            return this._currentDAO.GetTotalSaleAmountBySkuId(skuIdList);
        }

        public decimal GetIntegralAmountPre(string customerId)
        {
            return this._currentDAO.GetIntegralAmountPre(customerId);
        }

        public decimal GetTotalReturnAmountBySkuId(string skuIdList, SqlTransaction tran)
        {
            return this._currentDAO.GetTotalReturnAmountBySkuId(skuIdList, tran);
        }

        public decimal GetVipEndAmount(string vipId)
        {
            return this._currentDAO.GetVipEndAmount(vipId);
        }

        public DataSet GetVipCouponDataSet(string vipId, decimal totalPayAmount)
        {
            return this._currentDAO.GetVipCouponDataSet(vipId, totalPayAmount);
        }

        public void ProcSetCancelOrder(string customerId, string orderId, string vipId)
        {
            this._currentDAO.ProcSetCancelOrder(customerId, orderId, vipId);
        }

        public DataSet VipLandingPage(string customerId)
        {
            return this._currentDAO.VipLandingPage(customerId);
        }
        public DataSet GetVipIntegralDetail(string vipId, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetVipIntegralDetail(vipId, pPageIndex, pPageSize);
        }
        public DataSet GetVipEndAmountDetail(string vipId, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetVipEndAmountDetail(vipId, pPageIndex, pPageSize);
        }

        #region ��ֵ��ģ�飬���һ�Ա
        public DataSet GetCardVip(string criterion,string couponCode, int pageSize, int pageIndex)
        {
            return this._currentDAO.GetCardVip(criterion,couponCode, pageSize, pageIndex);
        }
        #endregion

        public string GetMaxVipCode()
        {
            return this._currentDAO.GetMaxVipCode();
        }
        public void AddVipWXDownload(UserInfoEntity item)
        {
            this._currentDAO.AddVipWXDownload(item);
        }
        public int WXToVip(string BatNo)
        {
            return this._currentDAO.WXToVip(BatNo);

        }

        public string GetVipSearchPropList(string customerId, string tableName,string unitId)
        {
            return this._currentDAO.GetVipSearchPropList(customerId, tableName,unitId);
        }
        /// <summary>
        /// ��ȡ���»�Ա��������Ϣ�Լ��ж�Ӧ��ֵ
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public DataSet GetExistVipInfo(string customerId,string userId ,string vipId)
        {
            return this._currentDAO.GetExistVipInfo(customerId, userId, vipId);
        }
        /// <summary>
        /// ��ȡ������Ա��̬��������
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <returns></returns>
        public string GetCreateVipPropList(string customerId,string userId)
        {
            return this._currentDAO.GetCreateVipPropList(customerId,userId);
        }
        public DataSet GetVipTagTypeList()
        {
            return this._currentDAO.GetVipTagTypeList();
        }
        public DataSet GetVipTagList(string customerId)
        {
            return this._currentDAO.GetVipTagList(customerId);
        }
        public DataSet GetVipDetailInfo(string vipId,string customerId)
        {
            return this._currentDAO.GetVipDetailInfo(vipId, customerId);
        }
        public void UpdateVipInfo(string vipId, SearchColumn[] columns)
        {
            var vipLogBll = new VipLogBLL(this.CurrentUserInfo);
            var snapShotBll = new VipSnapshotBLL(this.CurrentUserInfo);
            using (TransactionScope scope = new TransactionScope())
            {
                var vip = this.GetVipDetailByVipID(vipId);
                var viplog = new VipLogEntity()
                {
                    LogID = Guid.NewGuid().ToString("N"),
                    Action = "�޸���Ϣ",
                    VipID = vipId,
                    CreateBy = this.CurrentUserInfo.UserID,
                };
                vipLogBll.Create(viplog);
                snapShotBll.InsertSnapshotByVip(vip, this.CurrentUserInfo.UserID);
                this._currentDAO.UpdateVipInfo(vipId, columns);
                scope.Complete();
            }
        }
        public DataSet GetVipIntegralList(string vipId, int pageIndex, int pageSize,string sortType)
        {
            return this._currentDAO.GetVipIntegralList(vipId, pageIndex, pageSize,sortType);
        }

        public DataSet GetVipOrderList(string vipId, string customerId, int pageIndex, int pageSize,string sortType)
        {
            return this._currentDAO.GetVipOrderList(vipId, customerId, pageIndex, pageSize,sortType);
        }
        public DataSet GetVipConsumeCardList(string vipId, int pageIndex, int pageSize,string sortType)
        {
            return this._currentDAO.GetVipConsumeCardList(vipId, pageIndex, pageSize, sortType);
        }

        public DataSet GetVipAmountList(string vipid, int pageIndex, int pageSize,string sortType)
        {
            return this._currentDAO.GetVipAmountList(vipid, pageIndex, pageSize,sortType);
        }
        public DataSet GetVipOnlineOffline(string vipId, int pageIndex, int pageSize,string sortType)
        {
            return this._currentDAO.GetVipOnlineOffline(vipId, pageIndex, pageSize,sortType);
        }
        /// <summary>
        /// ���������ͱ�ǩ��ѯVIP�б�
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <param name="userId">�û�ID</param>
        /// <param name="pageIndex">�ڼ�ҳ</param>
        /// <param name="pageSize">ÿҳ��¼��</param>
        /// <param name="orderBy">�����ֶ���</param>
        /// <param name="isDesc">�Ƿ�Ϊ����</param>
        /// <param name="searchColumns">�в�ѯ��������</param>
        /// <param name="vipSearchTags">VIP��ǩ��������</param>
        /// <returns></returns>
        public DataSet SearchVipList(string customerId, string userId, int pageIndex, int pageSize, string orderBy,
                                        string sortType, SearchColumn[] searchColumns, VipSearchTag[] vipSearchTags)
        {
            return this._currentDAO.SearchVipList(customerId, userId, pageIndex, pageSize, orderBy,
                                         sortType, searchColumns, vipSearchTags);
        }
        /// <summary>
        /// ɾ����Ա
        /// </summary>
        /// <param name="vipIds">��Աid�б�</param>
        public void DeleteVip(string[] vipIds)
        {
            var vipLogBll = new VipLogBLL(this.CurrentUserInfo);
            var snapShotBll = new VipSnapshotBLL(this.CurrentUserInfo);
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var id in vipIds)
                {
                    var vip = this.GetVipDetailByVipID(id);
                    var viplog = new VipLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString("N"),
                        Action = "ɾ����Ա",
                        VipID = id,
                        CreateBy = this.CurrentUserInfo.UserID,
                    };
                    vipLogBll.Create(viplog);
                    snapShotBll.InsertSnapshotByVip(vip, this.CurrentUserInfo.UserID);
                }
                this._currentDAO.DeleteVips(vipIds, this.CurrentUserInfo.UserID);
                scope.Complete();
            }
        }
        /// <summary>
        /// ��������ӻ�Ա
        /// </summary>
        /// <param name="columns"></param>
        public void InsertVipEntity(SearchColumn[] columns,string clientId)
        {
            var bll = new VipBLL(this.CurrentUserInfo);
            var vipLogBll = new VipLogBLL(this.CurrentUserInfo);
            using (TransactionScope scope = new TransactionScope())
            {
                string vipCode = GetVipCode();
                var vipId = this._currentDAO.InsertVipEntity(columns, clientId, vipCode);
                var vip = bll.GetVipDetailByVipID(vipId);
                var viplog = new VipLogEntity()
                {
                    LogID = Guid.NewGuid().ToString("N"),
                    Action = "������Ա",
                    VipID = vipId,
                    CreateBy = this.CurrentUserInfo.UserID,
                };
                vipLogBll.Create(viplog);
                scope.Complete();
            }
        }
        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public VipEntity[] QueryByEntityAbsolute(VipEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntityAbsolute(pQueryEntity, pOrderBys);
        }
        private string newVipNotificationTemplateId = "eV8Sk890yc4tVtJUj43kOfdfpN1H4_QG6_Trb3V6jpM";
        /// <summary>
        /// ����ע���Աʱ������ ����Ϊ��Ա֪ͨ�� ��΢��ģ����Ϣ
        /// </summary>
        /// <param name="vipId"></param>
        public void SendNotification2NewVip(VipEntity vip)
        {
            var customerBll = new t_customerBLL(this.CurrentUserInfo);
            var customer = customerBll.GetByCustomerID(vip.ClientID);
            if(null == customer) return;
            var address = customer.customer_address;
            var remark = string.Format("��ע���������ʣ�����ѯ{0}��", customer.customer_tel);
            var first = string.Format("���ã����Ѿ���Ϊ{0}��Ա��", customer.customer_name);
            var message = new WeixinTemplateMessage(){
                touser=vip.WeiXinUserId,
                template_id=newVipNotificationTemplateId,
                url="",
                topcolor="#FF0000",
                data = new Dictionary<string,WeixinTemplateMessageData>()
                };
            message.Add("first",first,"#cccccc");
            message.Add("cardNumber", vip.VipCode,  "#cccccc" );
            message.Add("type",  "�̻�",  "#000000" );
            message.Add("address",  address,  "#cccccc");
            message.Add("VIPName",  vip.VipName, "#cccccc" );
            message.Add("VIPPhone", vip.Phone, "#cccccc");
            message.Add("expDate", DateTime.MaxValue.Date.ToSQLFormatString(), "#cccccc");
            message.Add("remark", remark, "#cccccc");
            var jsonContent = message.ToJSON();
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("zk weixin template message interface json result:{0}", jsonContent)});
            #region simple format template message for new vip
            /******* 
            var content = new
            {
                touser = vip.WeiXinUserId,
                template_id = newVipNotificationTemplateId,
                url = "",
                topcolor = "#FF0000",
                data = new
                {
                    first = new
                    {
                        value = first,
                        color = "#cccccc"
                    },
                    cardNumber = new
                    {
                        value = vip.VipCode,
                        color = "#cccccc"
                    },
                    type = new
                    {
                        value = "�̻�",
                        color = "#cccccc"
                    },
                    address = new
                    {
                        value = address,
                        color = "#cccccc"
                    },
                    VIPName = new
                    {
                        value = vip.VipName,
                        color = "#cccccc"
                    },
                    VIPPhone = new
                    {
                        value = vip.Phone,
                        color = "#cccccc"
                    },
                    expDate = new
                    {
                        vaue = DateTime.MaxValue,
                        color = "#cccccc"
                    },
                    remark = new
                    {
                        value = remark,
                        color = "#cccccc"
                    }
                }
            };
            ******************************/
            #endregion
            var appService = new WApplicationInterfaceBLL(this.CurrentUserInfo);
            var appList = appService.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = vip.ClientID }, null);
            if(appList == null || appList.Length == 0) return;
            var app = appList.FirstOrDefault();
            var commonBll = new CommonBLL();
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("��ʼ���ͳ�Ϊ��Ա֪ͨģ����Ϣ:{0}", jsonContent) });
            var result = commonBll.SendTemplateMessage(app.WeiXinID, jsonContent);
            Loggers.Debug(new DebugLogInfo(){Message = string.Format("���ͳ�Ϊ��Ա֪ͨģ����Ϣ���ؽ����{0}",result)});
        }

        #region RequestParameter 2014-10-16

        /// <summary>
        /// �û���ʼ��Ϣ
        /// </summary>
        public class VipInfoRP : IAPIRequestParameter
        {
            /// <summary>
            /// ΢��ID
            /// </summary>
            public string WeiXinUserID { get; set; }

            public void Validate()
            {
                //if (string.IsNullOrEmpty(WeiXinUserID))
                //    throw new APIException(201, "΢��ID����Ϊ�գ�");
            }
        }

        /// <summary>
        /// �û���ʼ��Ϣ
        /// </summary>
        public class VipInfoRD : IAPIResponseData
        {
            /// <summary>
            /// �û�ID
            /// </summary>
            public string VIPID { get; set; }
            /// <summary>
            /// �û�����
            /// </summary>
            public string VipName { get; set; }
            /// <summary>
            /// ��ν
            /// </summary>
            public string Gender { get; set; }
            /// <summary>
            /// �û��绰
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// ΢��ID
            /// </summary>
            public string WeiXinUserID { get; set; }
            /// <summary>
            /// �ͻ�ID
            /// </summary>
            public string ClientID { get; set; }
            /// <summary>
            /// �ŵ��б�
            /// </summary>
            public IList<JIT.CPOS.BS.BLL.UnitService.UnitInfoRD> UnitList { get; set; }

            /// <summary>
            /// ����Ϣ�б�
            /// </summary>
            public IList<VipExpandEntityInfoRD> CarInfoList { get; set; }

            /// <summary>
            /// ԤԼ����
            /// </summary>
            //public IList<JIT.CPOS.BS.BLL.ReserveTypeBLL.ReserveTypeInfoRD> ReserveTypeList { get; set; }

            ///// <summary>
            ///// ���ƺ�
            ///// </summary>
            //public string LicensePlateNo { get; set; }

            ///// <summary>
            ///// �û���ʵ����
            ///// </summary>
            public string VipRealName { get; set; }
        }

        public class VipExpandEntityInfoRD : IAPIResponseData
        {
            /// <summary>
            /// ����ID
            /// </summary>
            public String VipExpandID { get; set; }
            /// <summary>
            /// ���ƺ���
            /// </summary>
            public String LicensePlateNo { get; set; }
        }

        #endregion

        #region ���ݻ�ԱID��ȡ��Ա�ۿ���Ϣ 2014-11-5

        public decimal GetVipSale(string vipID)
        {
            DataSet ds = _currentDAO.GetVipSale(vipID);
            if (ds != null && ds.Tables[0].Rows.Count>0)
            {
                if (ds.Tables[0].Rows[0]["SalesPreferentiaAmount"] is DBNull)
                {
                    return 100;                    
                }
                else
                {
                    return (Convert.ToDecimal(ds.Tables[0].Rows[0]["SalesPreferentiaAmount"]) * 100);
                }
            }
            else
            {
                //�Ҳ�����Ϣ�򲻴��ۿ�
                return 100;
            }
        }

        #endregion
    }
}