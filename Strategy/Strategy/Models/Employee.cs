using Strategy.Models.Base;

namespace Strategy.Models
{
    public class Employee : BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
