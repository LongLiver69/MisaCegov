using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CEGOV.Domain
{
    /// <summary>
    /// Kiểu dữ liệu của phần tử trong file excel
    /// Created by: ntlong (04/08/2023)
    /// </summary>
    public enum ContentFormatEnum
    {
        None = 0,
        String = 1,
        Int = 2,
        Boolean = 3,
        Time = 4,
        Date = 5,
        DateTime = 6,
        Enum = 7,
        PersonalId = 8,
        Name = 9,
        Gender = 10,
        DateOfBirth = 11,
        Number = 12,
        Phone = 13,
        Currency = 14,
    }
}
