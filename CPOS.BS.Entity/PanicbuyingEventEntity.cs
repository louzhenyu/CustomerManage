/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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

namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class PanicbuyingEventEntity : BaseEntity 
    {
        #region ���Լ�
        public int Qty { get; set; }
        public int RemainQty { get; set; }
        public string EventStatusStr { get; set; }
        #endregion
    }
    public class KJEventItemInfo
    {
        public string EventId { get; set; }
        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string ImageUrl { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MinBasePrice { get; set; }

        public int Qty { get; set; }

        public int SoldQty { get; set; }

        public int PromotePersonCount { get; set; }
    }


    public class KJEventItemDetailInfo
    {    
        public string ItemId { get; set; }

        public string EventItemMappingID { get; set; }

        public string ItemName { get; set; }

        public int SinglePurchaseQty { get; set; }

        public decimal MinPrice { get; set; } //��Сԭ��

        public decimal MinBasePrice { get; set; } //��С�׼�

        public int PromotePersonCount { get; set; } //�ۼƲ�������

        public int CurrentQty { get; set; } //Sku�ۼƵ�ʣ����

        public int SoldQty { get; set; } //Sku�ۼƵ���������(+��������)

        public int RemainingQty { get; set; } //ʣ������

        public string StopReason { get; set; } //ֹͣԭ��

        public DateTime EventEndTime { get; set; } //�����ʱ��

        public DateTime EventBeginTime { get; set; } //���ʼʱ��

        public decimal BargaingingInterval { get; set; } //�ɿ�ʱ��
        public string Prop1Name { get; set; }

        public string Prop2Name { get; set; }


        public string ItemIntroduce { get; set; }

    }
}