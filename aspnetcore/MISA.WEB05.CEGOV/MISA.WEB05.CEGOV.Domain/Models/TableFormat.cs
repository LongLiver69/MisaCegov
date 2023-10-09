
namespace MISA.WEB05.CEGOV.Domain
{
    public class TableFormat
    {
        public string TableTitle { get; set; } = string.Empty;

        public string SheetName { get; set; } = string.Empty;

        public IList<ColumnFormat> Columns { get; set; } = new List<ColumnFormat>();

    }
}
