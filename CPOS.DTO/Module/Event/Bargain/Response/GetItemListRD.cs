﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetItemListRD : IAPIResponseData
    {
        public List<EventItem> ItemList { get; set; } 
    }
}