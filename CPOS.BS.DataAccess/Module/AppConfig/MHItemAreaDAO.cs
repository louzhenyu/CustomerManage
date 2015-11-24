/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 15:58:37
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
    /// ���ݷ��ʣ�  
    /// ��MHItemArea�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MHItemAreaDAO : BaseCPOSDAO, ICRUDable<MHItemAreaEntity>, IQueryable<MHItemAreaEntity>
    {
        public MHItemAreaEntity[] GetItemDetails()
        {
            List<MHItemAreaEntity> list = new List<MHItemAreaEntity> { };
            string sql = string.Format(@"select a.*,b.ItemName,b.ImageUrl,b.Price,b.SalesPrice,b.AddedTime,b.EndTime,b.BeginTime,b.EventTypeID
                                        from MHItemArea a left join vwPEventItemDetail b on a.EventId=b.EventId and a.ItemId=b.ItemID
                                        inner join MobileHome c on a.HomeID =c.HomeID and c.isdelete=0  AND c.IsActivate=1
                                        where c.CustomerID='{0}'  AND c.IsActivate=1 and a.isdelete=0 and datediff(day,getdate(),b.EndTime )>=0	
            ", this.CurrentUserInfo.ClientID);//datediff(day,getdate(),b.EndTime )>=0	�����ʱ�����ѡ�е�����
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    MHItemAreaEntity m;
                    this.Load(rd, out m);
                    m.ItemName = Convert.ToString(rd["ItemName"]);
                    m.ImageUrl = Convert.ToString(rd["ImageUrl"]);
                    m.Price = Convert.ToDecimal(rd["Price"] == DBNull.Value ? 0 : rd["Price"]);
                    m.SalesPrice = Convert.ToDecimal(rd["SalesPrice"] == DBNull.Value ? 0 : rd["SalesPrice"]);
                    m.AddedTime = Convert.ToDateTime(rd["AddedTime"]);
                    m.BeginTime = Convert.ToDateTime(rd["BeginTime"]);
                    m.EndTime = Convert.ToDateTime(rd["EndTime"]);
                    m.TypeId = Convert.ToString(rd["EventTypeID"]);
                    m.areaFlag = Convert.ToString(rd["areaFlag"]);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
        public DataSet GetItemDetails(string strHomeId, string strGroupId)
        {
            List<MHItemAreaEntity> list = new List<MHItemAreaEntity> { };
            string sql = string.Format(@"select a.*,b.ItemName,(CASE WHEN a.areaFlag='eventList' THEN b.ImageUrl ELSE  a.ItemImageUrl END ) ImageUrl,b.Price,b.SalesPrice,b.AddedTime,b.EndTime,b.BeginTime,b.EventTypeID as TypeID,b.RemainingSec
                                        from MHItemArea a 
                                                INNER JOIN dbo.MHCategoryAreaGroup mc ON a.GroupId=mc.GroupId AND mc.HomeId = a.HomeId
                                                LEFT JOIN vwPEventItemDetail b on a.EventId=b.EventId and a.ItemId=b.ItemID
                                        where a.HomeId='{0}' and a.GroupId={1} and a.isdelete=0  order by a.DisplayIndex asc
            ", strHomeId, strGroupId);//�����ʱ�����ѡ�е����� and datediff(day,getdate(),b.EndTime )>=0	
            return this.SQLHelper.ExecuteDataset(sql);
            
        }
    }
}
