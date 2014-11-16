/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/6 13:39:19
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
    /// 表MarketSignIn的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketSignInDAO : Base.BaseCPOSDAO, ICRUDable<MarketSignInEntity>, IQueryable<MarketSignInEntity>
    {
        #region 查询
        /// <summary>
        /// 获取查询会员的数量
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public int WebGetListCount(VipSearchEntity vipSearchInfo)
        {
            string sql = WebGetListSql(vipSearchInfo);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取查询会员的信息
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public DataSet WebGetList(VipSearchEntity vipSearchInfo)
        {
            int beginSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize;
            int endSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize + vipSearchInfo.PageSize;

            string sql = WebGetListSql(vipSearchInfo);
            //JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = "活动sql:" + sql.ToString().Trim() });
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int WebGetListCountAdd(VipSearchEntity vipSearchInfo)
        {
            string sql = WebGetListSqlAdd(vipSearchInfo, -1, -1);
            //sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        public DataSet WebGetListAdd(VipSearchEntity vipSearchInfo)
        {
            int beginSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize;
            int endSize = (vipSearchInfo.Page - 1) * vipSearchInfo.PageSize + vipSearchInfo.PageSize;

            string sql = WebGetListSqlAdd(vipSearchInfo, beginSize, endSize);
            //sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string WebGetListSql(VipSearchEntity vipSearchInfo)
        {
            PublicService pService = new PublicService();
            string sql = string.Empty;
            //处理循环处理标签
            sql = SetTagsSql(vipSearchInfo); 
            #region
            sql += " select a.*,DisplayIndex=row_number() over(order by a.LastUpdateTime desc ) into #tmp from ( ";
            sql += "SELECT a.*, b.EventId "
                + " ,(SELECT x.VipSourceName FROM dbo.SysVipSource x WHERE x.VipSourceID = a.VipSourceId) VipSourceName "
                + " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc "
                + " ,'' LastUnit "
                + " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc "
                + "  "
                + " ,(select sum(Integral) from VipIntegralDetail where IsDelete='0' "
                + "   and fromVipId=a.vipId and vipId='" + vipSearchInfo.HigherVipId + "') IntegralForHightUser "
                + " ,CASE WHEN a.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                + " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '41' AND x.OpenID = a.WeiXinUserId) UserName "
                + " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '42' AND x.OpenID = a.WeiXinUserId) Enterprice "
                + " ,(SELECT y.OptionsId FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = a.WeiXinUserId) IsChainStoresId "
                + " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = a.WeiXinUserId) IsChainStores "
                + " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '45' AND x.OpenID = a.WeiXinUserId) IsWeiXinMarketing "
                + " from  #vip a "
                + " left join MarketSignIn b on (b.openId=a.weiXinUserId and a.isDelete='0') "
                + " WHERE a.IsDelete = '0') a where 1=1 and ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (vipSearchInfo.Gender != null && vipSearchInfo.Gender.Trim().Length > 0)
            {
                sql = pService.GetLinkSql(sql, "a.Gender ", vipSearchInfo.Gender.Trim(), "=");
            }
            if (vipSearchInfo.UserName != null && vipSearchInfo.UserName.Trim().Length > 0)
            {
                sql += " and a.VIPName like '%" + vipSearchInfo.UserName + "%' ";
            }
            if (vipSearchInfo.Enterprice != null && vipSearchInfo.Enterprice.Trim().Length > 0)
            {
                sql += " and a.Enterprice like '%" + vipSearchInfo.Enterprice + "%' ";
            }
            if (vipSearchInfo.IsChainStores != null && vipSearchInfo.IsChainStores.Trim().Length > 0)
            {
                sql += " and a.IsChainStoresId = '" + vipSearchInfo.IsChainStores + "' ";
            }
            if (vipSearchInfo.IsWeiXinMarketing != null && vipSearchInfo.IsWeiXinMarketing.Trim().Length > 0)
            {
                sql += " and a.IsWeiXinMarketing = '" + vipSearchInfo.IsWeiXinMarketing + "' ";
            }
            //if (vipSearchInfo.EventId != null && vipSearchInfo.EventId.Trim().Length > 0)
            //{
            //    sql += " and (a.EventID = '" + vipSearchInfo.EventId + "' or a.EventID = '4') ";
            //}
            //sql += " order by a.LastUpdatetime desc";
            #endregion
            return sql;
        }
        private string WebGetListSqlAdd(VipSearchEntity vipSearchInfo, int beginSize, int endSize)
        {
            PublicService pService = new PublicService();
            string sql = string.Empty;
            //处理循环处理标签
            sql = SetTagsSqlAdd(vipSearchInfo);
            #region
            sql += " select a.*,DisplayIndex=row_number() over(order by a.LastUpdateTime desc ) into #tmp2 from ( ";
            sql += "SELECT a.* "
            //sql += "SELECT a.*, b.EventId "
                //+ " ,(SELECT x.VipSourceName FROM dbo.SysVipSource x WHERE x.VipSourceID = a.VipSourceId) VipSourceName "
                //+ " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc "
                //+ " ,'' LastUnit "
                //+ " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc "
                //+ "  "
                //+ " ,(select sum(Integral) from VipIntegralDetail where IsDelete='0' "
                //+ "   and fromVipId=a.vipId and vipId='" + vipSearchInfo.HigherVipId + "') IntegralForHightUser "
                //+ " ,CASE WHEN a.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                //+ " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '41' AND x.OpenID = a.WeiXinUserId) UserName "
                //+ " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '42' AND x.OpenID = a.WeiXinUserId) Enterprice "
                //+ " ,(SELECT y.OptionsId FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                //+ " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = a.WeiXinUserId) IsChainStoresId "
                //+ " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                //+ " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = a.WeiXinUserId) IsChainStores "
                //+ " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                //+ " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '45' AND x.OpenID = a.WeiXinUserId) IsWeiXinMarketing "
                + " from  #vip a "
                //+ " left join MarketSignIn b on (b.openId=a.weiXinUserId and a.isDelete='0') "
                + " WHERE a.IsDelete = '0') a where 1=1 and ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (vipSearchInfo.Gender != null && vipSearchInfo.Gender.Trim().Length > 0)
            {
                sql = pService.GetLinkSql(sql, "a.Gender ", vipSearchInfo.Gender.Trim(), "=");
            }
            if (vipSearchInfo.UserName != null && vipSearchInfo.UserName.Trim().Length > 0)
            {
                sql += " and a.VIPName like '%" + vipSearchInfo.UserName + "%' ";
            }
            if (vipSearchInfo.Enterprice != null && vipSearchInfo.Enterprice.Trim().Length > 0)
            {
                sql += " and a.Enterprice like '%" + vipSearchInfo.Enterprice + "%' ";
            }
            if (vipSearchInfo.IsChainStores != null && vipSearchInfo.IsChainStores.Trim().Length > 0)
            {
                sql += " and a.IsChainStoresId = '" + vipSearchInfo.IsChainStores + "' ";
            }
            if (vipSearchInfo.IsWeiXinMarketing != null && vipSearchInfo.IsWeiXinMarketing.Trim().Length > 0)
            {
                sql += " and a.IsWeiXinMarketing = '" + vipSearchInfo.IsWeiXinMarketing + "' ";
            }
            //if (vipSearchInfo.EventId != null && vipSearchInfo.EventId.Trim().Length > 0)
            //{
            //    sql += " and (a.EventID = '" + vipSearchInfo.EventId + "' or a.EventID = '4') ";
            //}
            //sql += " order by a.LastUpdatetime desc";

            sql += "; ";
            if (beginSize == -1)
            {
                sql = sql + " select count(*) as icount From #tmp2; ";
            }
            else
            {
                sql = sql + " select * into #tmp From #tmp2 a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex ;";
                sql += " SELECT a.*, b.EventId "
                    + " ,(SELECT x.VipSourceName FROM dbo.SysVipSource x WHERE x.VipSourceID = a.VipSourceId) VipSourceName "
                    + " ,CASE WHEN a.Status = '1' THEN '潜在会员' ELSE '正式会员' END StatusDesc "
                    + " ,'' LastUnit "
                    + " ,CASE WHEN a.VipLevel = '1' THEN '基本' ELSE '高级' END VipLevelDesc "
                    + "  "
                    + " ,(select sum(Integral) from VipIntegralDetail where IsDelete='0' "
                    + "   and fromVipId=a.vipId and vipId='" + vipSearchInfo.HigherVipId + "') IntegralForHightUser "
                    + " ,CASE WHEN a.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                    + " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '41' AND x.OpenID = a.WeiXinUserId) UserName "
                    + " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '42' AND x.OpenID = a.WeiXinUserId) Enterprice "
                    + " ,(SELECT y.OptionsId FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                    + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = a.WeiXinUserId) IsChainStoresId "
                    + " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                    + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = a.WeiXinUserId) IsChainStores "
                    + " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                    + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '45' AND x.OpenID = a.WeiXinUserId) IsWeiXinMarketing "
                    + " from #tmp a "
                    + " left join MarketSignIn b on (b.openId=a.weiXinUserId and a.isDelete='0') ";
                sql += "; ";
            }

            #endregion
            return sql;
        }
        /// <summary>
        /// 处理循环处理标签
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public string SetTagsSql(VipSearchEntity vipSearchInfo)
        {
            string sql = string.Empty;
            sql = " select * into #vip From vip  ";
            if (vipSearchInfo == null || vipSearchInfo.Tags == null || vipSearchInfo.Tags.Length < 5)
            {
            }
            else {
                string[] tagsArr = vipSearchInfo.Tags.Split(';');
                foreach (string tag in tagsArr)
                {
                    if (!tag.Trim().Equals(""))
                    {
                        string[] tagArr = tag.Split(',');
                        string tagId = tagArr[0];
                        string strlinkType = tagArr[1];
                        if (!tagId.Equals(""))
                        {
                            JIT.CPOS.BS.DataAccess.TagsDAO server = new TagsDAO(this.CurrentUserInfo);
                            JIT.CPOS.BS.Entity.TagsEntity tagsInfo = new TagsEntity();
                            tagsInfo = server.GetByID(tagId);
                            if(tagsInfo != null && tagsInfo.TagsFormula != null && !tagsInfo.TagsFormula.Equals(""))
                            {
                                switch (strlinkType)
                                {
                                    case "1":
                                        sql += " and " + tagsInfo.TagsFormula.ToString();
                                        break;
                                    case "2":
                                        sql += " or " + tagsInfo.TagsFormula.ToString();
                                        break;
                                    case "3":
                                        sql += " where " + tagsInfo.TagsFormula.ToString();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            sql += " ;";
            return sql;
        }

        /// <summary>
        /// 处理循环处理标签
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public string SetTagsSqlAdd(VipSearchEntity vipSearchInfo)
        {
            string sql = string.Empty;
            sql = " select * into #vip From ( select VIPID,VipCode,VipLevel,VipName,Phone,WeiXin,Integration,LastUpdateTime,PurchaseAmount,PurchaseCount,weiXinUserId,isDelete,VipSourceId,Status,Gender,ClientID  From vip  ";
            sql += " WHERE IsDelete = '0' and ClientID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ) x ";
            if (vipSearchInfo == null || vipSearchInfo.Tags == null || vipSearchInfo.Tags.Length < 5)
            {
            }
            else
            {
                string[] tagsArr = vipSearchInfo.Tags.Split(';');
                foreach (string tag in tagsArr)
                {
                    if (!tag.Trim().Equals(""))
                    {
                        string[] tagArr = tag.Split(',');
                        string tagId = tagArr[0];
                        string strlinkType = tagArr[1];
                        if (!tagId.Equals(""))
                        {
                            JIT.CPOS.BS.DataAccess.TagsDAO server = new TagsDAO(this.CurrentUserInfo);
                            JIT.CPOS.BS.Entity.TagsEntity tagsInfo = new TagsEntity();
                            tagsInfo = server.GetByID(tagId);
                            if (tagsInfo != null && tagsInfo.TagsFormula != null && !tagsInfo.TagsFormula.Equals(""))
                            {
                                switch (strlinkType)
                                {
                                    case "1":
                                        sql += " and " + tagsInfo.TagsFormula.ToString();
                                        break;
                                    case "2":
                                        sql += " or " + tagsInfo.TagsFormula.ToString();
                                        break;
                                    case "3":
                                        sql += " where " + tagsInfo.TagsFormula.ToString();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            //sql += " order by LastUpdateTime desc ";
            sql += "  ;";
            return sql;
        }
        

        #endregion
    }
}
