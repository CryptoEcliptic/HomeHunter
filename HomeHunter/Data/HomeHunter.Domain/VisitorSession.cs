using HomeHunter.Domain.Common;

namespace HomeHunter.Domain
{
    public class VisitorSession : BaseModel<string>
    {
        public string VisitorId { get; set; }

        public string IpAddress { get; set; }
    }
}
