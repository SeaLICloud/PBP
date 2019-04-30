using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class File:Entity<File>
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public string ContentType { get; set; }
    }
}
