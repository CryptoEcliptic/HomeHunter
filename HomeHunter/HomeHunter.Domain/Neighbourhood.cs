using HomeHunter.Domain.Common;
using System.Collections.Generic;

namespace HomeHunter.Domain
{
    public class Neighbourhood : BaseModel<int>
    {
        public Neighbourhood()
        {
            this.Addresses = new List<Address>();
        }
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}

