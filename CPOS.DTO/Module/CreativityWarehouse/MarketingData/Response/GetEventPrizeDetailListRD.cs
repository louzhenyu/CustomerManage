using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetEventPrizeDetailListRD : IAPIResponseData
    {

          public List<EventPrizeDetailInfo> EventPrizeDetailList { get; set; }
        
            public int TotalCount { get; set; }
         public int TotalPage { get; set; }
    }

    public class EventPrizeDetailInfo{
    
            
             public string vipname { get; set; }
             public string winTime { get; set; }
             public string PrizeUsed { get; set; }
             public string subscribe { get; set; }  



             #region 属性集
             /// <summary>
             /// 
             /// </summary>
             public Guid? CTWEventId { get; set; }

             /// <summary>
             /// 这个模板Id是ap库里的，引入到这边以便可以追溯
             /// </summary>
             public Guid? TemplateId { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String Name { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String Desc { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public Guid ActivityGroupId { get; set; }

             /// <summary>
             /// 1. 吸粉   2.促销
             /// </summary>
             public Int32? InteractionType { get; set; }

             /// <summary>
             /// 引用AP库的图片URL
             /// </summary>
             public String ImageURL { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String OnlineQRCodeId { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String OfflineQRCodeId { get; set; }

             /// <summary>
             /// 10=待发布   20=运行中   30=暂停   40=结束
             /// </summary>
             public Int32? Status { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public DateTime? CreateTime { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String CreateBy { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public DateTime? LastUpdateTime { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String LastUpdateBy { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public String CustomerId { get; set; }

             /// <summary>
             /// 
             /// </summary>
             public Int32? IsDelete { get; set; }


             #endregion


 

    }
}
