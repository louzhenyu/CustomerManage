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
    /// 业务处理：  
    /// </summary>
    public partial class VipBLL
    {
        #region 会员查询
        /// <summary>
        /// 会员查询 Jermyn20130514+
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


        #region 获取会员积分
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


        #region 会员关注
        public bool SetVipInfo(VipEntity vipInfo)
        {
            /*1.插入关注日志表
              2.判断是新会员还是存在的会员
              3.插入新会员
              4.更新已存在的会员
            */

            return true;
        }
        #endregion

        #region 获取会员详细信息
        /// <summary>
        /// 根据微信OpenID获取会员详细信息
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

        #region 获取初始载入信息

        /// <summary>
        /// 获取初始载入信息（前台用）
        /// </summary>
        /// <param name="OpenID">微信ID</param>
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
        /// 根据号码模糊查询会员
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


        #region 获取会员号
        /// <summary>
        /// 获取vip编码
        /// </summary>
        /// <returns></returns>
        public string GetVipCode()
        {
            return new AppSysService(this.CurrentUserInfo).GetNo("Vip");
        }
        #endregion

        #region 获取关注的人数
        /// <summary>
        /// 获取近期关注的客户
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
        /// 获取各个来源对应的会员数，VipSourceId 为 NULL 时 当作电话客服来源对待
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
        /// 获取lj VIP信息
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
        /// 获取VIP兑换商品列表信息
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
        /// 获取VIP订单列表信息
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
        /// 获取VIP兑换商品SKU列表信息
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

        #region 获取会员关注人数(泸州老窖)
        public int GetHasVipCount(string WeiXinId)
        {
            return _currentDAO.GetHasVipCount(WeiXinId);
        }
        #endregion

        #region 获取新采集会员数量(泸州老窖)
        public int GetNewVipCount(string WeiXinId)
        {
            return _currentDAO.GetNewVipCount(WeiXinId);
        }
        #endregion

        #region getVipMonthAddup
        /// <summary>
        /// 会员按月累计
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
        /// 会员月活动销量统计
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

        #region 从总部获取vip信息 Jermyn20130911
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

        #region 获取会员标签集合
        /// <summary>
        /// 获取会员标签集合
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

        #region 获取会员标签映射集合
        /// <summary>
        /// 获取会员标签映射集合
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

        #region Jermyn20131207 合并用户标识
        /// <summary>
        /// 处理用户合并，主要是因为之前的用户不是注册用户，注册之后存在两个帐号，需要合并
        /// </summary>
        /// <param name="UserId">缓存在Cookie中的</param>
        /// <param name="VipId"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool SetMergerVipInfo(string UserId, string VipId, string OpenId)
        {
            return _currentDAO.SetMergerVipInfo(UserId, VipId, OpenId);
        }
        #endregion

        #region Jermyn20131219门店奖励查询
        /// <summary>
        /// 门店奖励查询
        /// </summary>
        /// <param name="strError">错误信息</param>
        /// <returns>门店MembershipShop，积分SearchIntegral，会员数量UnitCount，销售金额UnitSalesAmount，ICount总数量</returns>
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

        #region Jermyn20131219导购员奖励
        /// <summary>
        /// 导购员奖励
        /// </summary>
        /// <param name="vipSearchInfo">查询条件集合:门店，奖励类型（复选，对应表SysIntegralSource ），开始日期，结束日期</param>
        /// <param name="strError"></param>
        /// <returns>导购员姓名 UserName 门店 MembershipShop，积分SearchIntegral，会员数量VipCount，导购销售金额UnitSalesAmount</returns>
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

        #region Jermyn20131219会员奖励
        /// <summary>
        /// 会员奖励
        /// </summary>
        /// <param name="vipSearchInfo">查询条件集合:门店UnitId，会员名VipName，奖励类型（复选，对应表SysIntegralSource ），开始日期，结束日期</param>
        /// <param name="strError"></param>
        /// <returns>会员名VipName，会籍店MembershipShop，积分SearchIntegral，来源VipSourceName，标签VipTagsShort，加入时间CreateTime，等级VipLevelDesc，推介会员数VipCount，购买金额PurchaseAmount</returns>
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
                    #region 会员标签
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
        /// DataTable分页
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从1开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns>分好页的DataTable数据</returns>
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
        /// 返回分页的页数
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <returns>如果 结尾为0：则返回1</returns>
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

        #region 获取会员的各种状态订单数量Jermyn201223
        /// <summary>
        /// 获取会员的各种状态订单数量
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

                    //added by zhangwei 注册送200积分
                    #region 插入积分 防止影响注册，增加try catch 并忽略错误

                    try
                    {
                        //新的积分方法 zhangwei2013-2-13
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

        #region 获取会员集合
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


        #region 俄丽亚
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
                    throw new Exception("密码验证失败");
                }
            }
            else
            {
                throw new Exception("未找到此用户");
            }
        }
        #endregion

        #region 会员查询Location
        /// <summary>
        /// 会员查询 Jermyn20130514+
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

        #region 同步阿拉丁会员信息

        /// <summary>
        /// 同步阿拉丁会员信息
        /// </summary>
        /// <param name="vipId">会员ID</param>
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

                    //请求阿拉丁用户信息
                    var url = ConfigurationManager.AppSettings["ALDGatewayURL"];  //正式
                    //var url = "http://121.199.42.125:5012/Gateway.ashx";        //测试

                    if (string.IsNullOrEmpty(url))
                        throw new Exception("未配置阿拉丁平台接口URL:ALDGatewayURL");
                    var postContent = string.Format("Action=GetMemberList4PushMessage&ReqContent={0}", aldRequest.ToJSON());
                    var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                    var aldRsp = strAldRsp.DeserializeJSONTo<ALDResponse>();

                    if (aldRsp != null && aldRsp.Data != null && aldRsp.Data.Count!=0 && aldRsp.ResultCode == 200)
                    {
                        var entity = aldRsp.Data.FirstOrDefault();
                        //同步用户信息
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
            public string MemberID { get; set; }    //会员ID
            public string MemberNo { get; set; }    //登录帐号
            public string Platform { get; set; }    //客户端平台：1=Android,2=IOS,3=其他
            public string IOSDeviceToken { get; set; }  //IOS的设备号
            public string BaiduChannelID { get; set; }  //百度消息推送的渠道ID
            public string BaiduUserID { get; set; }     //百度消息推送的用户ID
            public string ChannelID { get; set; }       //渠道ID
        }

        #endregion

        #region 客服登录

        /// <summary>
        /// 客服登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet SetSignIn(string userName, string password, string customerId)
        {
            return this._currentDAO.SetSignIn(userName, password, customerId);
        }

        public DataSet GetRoleCodeByUserId(string userId, string customerId)
        {
            return this._currentDAO.GetRoleCodeByUserId(userId, customerId);
        }
        #region 判断用户是否存在

        public bool JudgeUserExist(string userName, string customerId)
        {
            return this._currentDAO.JudgeUserExist(userName, customerId);
        }
        #endregion

        #region 判断密码是否正确
        public bool JudgeUserPasswordExist(string userName, string customerId, string password)
        {
            return this._currentDAO.JudgeUserPasswordExist(userName, customerId, password);
        }
        #endregion

        public DataSet GetUserIdByUserNameAndPassword(string userName, string customerId, string password)
        {
            return this._currentDAO.GetUserIdByUserNameAndPassword(userName, customerId, password);
        }

        #region 判断该客服人员是否有客服或操作订单的权限
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
        /// 会员登录 Add by Alex Tian 2014-04-11
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
        /// 根据会员编号、手机号，合并会员信息； 操作成功返回1，失败返回0
        /// </summary>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pVipID">会员ID</param>
        /// <param name="pPhone">手机号</param>
        /// <returns></returns>
        public bool MergeVipInfo(string pCustomerID, string pVipID, string pPhone)
        {
            return this._currentDAO.MergeVipInfo(pCustomerID, pVipID, pPhone);
        }

        /// <summary>
        /// 获取一个新的会员卡号
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

        #region 充值卡模块，查找会员
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
        /// 获取更新会员所需列信息以及列对应的值
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
        /// 获取新增会员动态配置属性
        /// </summary>
        /// <param name="customerId">客户ID</param>
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
                    Action = "修改信息",
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
        /// 根据条件和标签查询VIP列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderBy">排序字段名</param>
        /// <param name="isDesc">是否为降序</param>
        /// <param name="searchColumns">列查询条件集合</param>
        /// <param name="vipSearchTags">VIP标签条件集合</param>
        /// <returns></returns>
        public DataSet SearchVipList(string customerId, string userId, int pageIndex, int pageSize, string orderBy,
                                        string sortType, SearchColumn[] searchColumns, VipSearchTag[] vipSearchTags)
        {
            return this._currentDAO.SearchVipList(customerId, userId, pageIndex, pageSize, orderBy,
                                         sortType, searchColumns, vipSearchTags);
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="vipIds">会员id列表</param>
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
                        Action = "删除会员",
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
        /// 根据列添加会员
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
                    Action = "创建会员",
                    VipID = vipId,
                    CreateBy = this.CurrentUserInfo.UserID,
                };
                vipLogBll.Create(viplog);
                scope.Complete();
            }
        }
        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public VipEntity[] QueryByEntityAbsolute(VipEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntityAbsolute(pQueryEntity, pOrderBys);
        }
        private string newVipNotificationTemplateId = "eV8Sk890yc4tVtJUj43kOfdfpN1H4_QG6_Trb3V6jpM";
        /// <summary>
        /// 当新注册会员时，发送 【成为会员通知】 的微信模板消息
        /// </summary>
        /// <param name="vipId"></param>
        public void SendNotification2NewVip(VipEntity vip)
        {
            var customerBll = new t_customerBLL(this.CurrentUserInfo);
            var customer = customerBll.GetByCustomerID(vip.ClientID);
            if(null == customer) return;
            var address = customer.customer_address;
            var remark = string.Format("备注：如有疑问，请咨询{0}。", customer.customer_tel);
            var first = string.Format("您好，您已经成为{0}会员。", customer.customer_name);
            var message = new WeixinTemplateMessage(){
                touser=vip.WeiXinUserId,
                template_id=newVipNotificationTemplateId,
                url="",
                topcolor="#FF0000",
                data = new Dictionary<string,WeixinTemplateMessageData>()
                };
            message.Add("first",first,"#cccccc");
            message.Add("cardNumber", vip.VipCode,  "#cccccc" );
            message.Add("type",  "商户",  "#000000" );
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
                        value = "商户",
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
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("开始发送成为会员通知模板消息:{0}", jsonContent) });
            var result = commonBll.SendTemplateMessage(app.WeiXinID, jsonContent);
            Loggers.Debug(new DebugLogInfo(){Message = string.Format("发送成为会员通知模板消息返回结果：{0}",result)});
        }

        #region RequestParameter 2014-10-16

        /// <summary>
        /// 用户初始信息
        /// </summary>
        public class VipInfoRP : IAPIRequestParameter
        {
            /// <summary>
            /// 微信ID
            /// </summary>
            public string WeiXinUserID { get; set; }

            public void Validate()
            {
                //if (string.IsNullOrEmpty(WeiXinUserID))
                //    throw new APIException(201, "微信ID不能为空！");
            }
        }

        /// <summary>
        /// 用户初始信息
        /// </summary>
        public class VipInfoRD : IAPIResponseData
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public string VIPID { get; set; }
            /// <summary>
            /// 用户姓名
            /// </summary>
            public string VipName { get; set; }
            /// <summary>
            /// 称谓
            /// </summary>
            public string Gender { get; set; }
            /// <summary>
            /// 用户电话
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 微信ID
            /// </summary>
            public string WeiXinUserID { get; set; }
            /// <summary>
            /// 客户ID
            /// </summary>
            public string ClientID { get; set; }
            /// <summary>
            /// 门店列表
            /// </summary>
            public IList<JIT.CPOS.BS.BLL.UnitService.UnitInfoRD> UnitList { get; set; }

            /// <summary>
            /// 车信息列表
            /// </summary>
            public IList<VipExpandEntityInfoRD> CarInfoList { get; set; }

            /// <summary>
            /// 预约类型
            /// </summary>
            //public IList<JIT.CPOS.BS.BLL.ReserveTypeBLL.ReserveTypeInfoRD> ReserveTypeList { get; set; }

            ///// <summary>
            ///// 车牌号
            ///// </summary>
            //public string LicensePlateNo { get; set; }

            ///// <summary>
            ///// 用户真实姓名
            ///// </summary>
            public string VipRealName { get; set; }
        }

        public class VipExpandEntityInfoRD : IAPIResponseData
        {
            /// <summary>
            /// 汽车ID
            /// </summary>
            public String VipExpandID { get; set; }
            /// <summary>
            /// 车牌号码
            /// </summary>
            public String LicensePlateNo { get; set; }
        }

        #endregion

        #region 根据会员ID获取会员折扣信息 2014-11-5

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
                //找不到信息则不打折扣
                return 100;
            }
        }

        #endregion
    }
}