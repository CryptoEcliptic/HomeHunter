using HomeHunter.Domain.Common;
using System.Collections.Generic;

namespace HomeHunter.Domain
{
    public class Village : BaseModel<int>
    {
        public Village()
        {
            this.Addresses = new List<Address>();
        }

        public string Name { get; set; }

        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
