﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DividendLiberty.Excel
{
    class ExcelWriterException:Exception
    {
        public ExcelWriterException(string message)
            : base(message)
        {
        }

        public ExcelWriterException(string message, Exception innerexception)
            : base(message, innerexception)
        {
        }
    }
}
