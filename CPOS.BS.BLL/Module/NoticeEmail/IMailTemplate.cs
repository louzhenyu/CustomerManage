﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.NoticeEmail
{
    public interface IMailTemplate
    {
       string Render( object data );
    }
}
