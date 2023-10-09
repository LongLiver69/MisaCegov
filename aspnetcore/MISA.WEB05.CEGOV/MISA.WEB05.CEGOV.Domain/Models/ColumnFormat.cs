using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CEGOV.Domain
{
    public class ColumnFormat
    {
        public string FieldName { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        //public bool IsRequired { get; set; } = false;

        public ContentFormatEnum Format { get; set; } = ContentFormatEnum.None;

        public AlignEnum Align { get; set; } = AlignEnum.MiddleLeft;
    }
}
