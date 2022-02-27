
using NetCoreStartProject.Enums;

namespace NetCoreStartProject.Domain
{
    public class Media : BaseEntity
    {
        public MediaType MediaType { get; set; }
        public int typeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }

    }
}
