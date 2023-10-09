using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CEGOV.Domain
{
    /// <summary>
    /// Enum để căn chỉnh phần tử trong file excel
    /// Created by: ntlong (04/08/2023)
    /// </summary>
    [Flags]
    public enum AlignEnum
    {
        // Không căn
        None = 0,

        // Căn giữa nội dung theo chiều dọc
        Middle = 1,

        // Căn trên nội dung theo chiều dọc
        Top = 2,

        // Căn dưới nội dung theo chiều dọc
        Bottom = 4,

        // Căn giữa nội dung theo chiều ngang
        Center = 8,

        // Căn trái nội dung theo chiều ngang
        Left = 16,

        // Căn phải nội dung theo chiều ngang
        Right = 32,

        // Căn trên trái
        TopLeft = Top | Left, // 18

        // Căn trên giữa
        TopCenter = Top | Center, // 10

        // Căn trên phải
        TopRight = Top | Right, // 34

        // Căn giữa trái
        MiddleLeft = Middle | Left, // 17

        // Căn giữa giữa
        MiddleCenter = Middle | Center, // 9

        // Căn giữa phải
        MiddleRight = Middle | Right, // 33

        // Căn dưới trái
        BottomLeft = Bottom | Left, // 20

        // Căn dưới giữa
        BottomCenter = Bottom | Center, // 12

        // Căn dưới phải
        BottomRight = Bottom | Right // 36
    }
}
