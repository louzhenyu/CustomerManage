/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    /// ҵ����  
    /// </summary>
    public partial class MarketEventResponseBLL : BaseService
    {
        #region ��ȡ��Ӧ��Ⱥ��Ϣ
        public MarketEventResponseEntity GetEventResponseInfo(string EventID, int Page, int PageSize)
        {
            MarketEventResponseEntity info = new MarketEventResponseEntity();
            IList<MarketEventResponseEntity> infoList = new List<MarketEventResponseEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetEventResponseInfo(EventID, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                infoList = DataTableToObject.ConvertToList<MarketEventResponseEntity>(ds.Tables[0]);
                info.MarketEventResponseInfoList = infoList;
                info.ICount = _currentDAO.GetEventResponseInfoCount(EventID);
            }

            return info;
        }
        #endregion

        #region �г����Ӧ

        /// <summary>
        /// �г����Ӧ
        /// </summary>
        /// <param name="openID">�û�΢��ID</param>
        /// <param name="eventID">�ID</param>
        public void MarketEventResponse(string openID, string eventID)
        {
            IWhereCondition[] whereCondition = new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "OpenID", Value = openID },
                    new EqualsCondition() { FieldName = "MarketEventID", Value = eventID }
                };

            var responses = this.Query(whereCondition, null);

            //�û�û����Ӧ�����ӻ��Ӧ��Ϣ
            if (responses.Length == 0)
            {
                //�����û�΢��ID����ȡVIPID
                whereCondition = new IWhereCondition[] { new EqualsCondition() { FieldName = "WeiXinUserId", Value = openID } };
                var vips = new VipBLL(this.CurrentUserInfo).Query(whereCondition, null);

                string vipID = string.Empty;
                if (vips.Length > 0)
                {
                    vipID = vips.FirstOrDefault().VIPID;
                }

                MarketEventResponseEntity entity = new MarketEventResponseEntity()
                {
                    ReponseID = this.NewGuid(),
                    OpenID = openID,
                    MarketEventID = eventID,
                    VIPID = vipID
                };

                this.Create(entity);
            }
            else
            {
                //���»��Ӧ��Ϣ
                MarketEventResponseEntity entity = new MarketEventResponseEntity()
                {
                    ReponseID = responses.FirstOrDefault().ReponseID,
                };

                this.Update(entity, false);
            }
        }

        #endregion

        #region �г������

        /// <summary>
        /// �г������
        /// </summary>
        /// <param name="openID">�û�΢��ID</param>
        /// <param name="eventID">�ID</param>
        /// <param name="productName">��Ʒ����</param>
        /// <param name="purchaseAmount">������</param>
        public void MarketEventPurchase(string openID, string eventID, string productName, string purchaseAmount)
        {
            //�����û�΢��ID����ȡVIPID
            IWhereCondition[] whereCondition = new IWhereCondition[] { new EqualsCondition() { FieldName = "WeiXinUserId", Value = openID } };
            var vips = new VipBLL(this.CurrentUserInfo).Query(whereCondition, null);

            string vipID = string.Empty;
            if (vips.Length > 0)
            {
                vipID = vips.FirstOrDefault().VIPID;
            }

            //�����Ʒ������Ϣ
            MarketEventResponseEntity entity = new MarketEventResponseEntity()
            {
                ReponseID = this.NewGuid(),
                OpenID = openID,
                MarketEventID = eventID,
                VIPID = vipID,
                ProductName = productName,
                PurchaseAmount = ToDecimal(purchaseAmount),
                IsSales = 1
            };

            this.Create(entity);
        }

        #endregion
    }
}